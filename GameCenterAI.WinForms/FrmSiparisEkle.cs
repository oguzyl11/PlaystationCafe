using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for adding orders to a transaction.
    /// </summary>
    public partial class FrmSiparisEkle : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlUrunler;
        private GridView _gridViewUrunler;
        private GridControl _gridControlSiparisDetay;
        private GridView _gridViewSiparisDetay;
        private SimpleButton _btnUrunEkle;
        private SimpleButton _btnUrunCikar;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private SpinEdit _spinAdet;
        private LabelControl _lblToplam;
        
        private SUrun _urunService;
        private SSiparis _siparisService;
        private int _hareketID;
        private List<SiparisDetaylar> _siparisDetaylar;
        private List<Urunler> _urunler;

        /// <summary>
        /// Initializes a new instance of the FrmSiparisEkle class.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        public FrmSiparisEkle(int hareketID)
        {
            InitializeComponent();
            _hareketID = hareketID;
            _urunService = new SUrun();
            _siparisService = new SSiparis();
            _siparisDetaylar = new List<SiparisDetaylar>();
            _spinAdet.Value = 1;
            
            LoadUrunler();
            HesaplaToplam();
        }

        /// <summary>
        /// Loads all products into the grid.
        /// </summary>
        private void LoadUrunler()
        {
            try
            {
                _urunler = _urunService.Listele();
                _gridControlUrunler.DataSource = _urunler;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ürünler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the add product button click event.
        /// </summary>
        private void BtnUrunEkle_Click(object sender, EventArgs e)
        {
            if (_gridViewUrunler.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen bir ürün seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int urunID = Convert.ToInt32(_gridViewUrunler.GetRowCellValue(_gridViewUrunler.GetSelectedRows()[0], "UrunID"));
            Urunler urun = _urunler.FirstOrDefault(u => u.UrunID == urunID);
            
            if (urun == null) return;

            int adet = (int)_spinAdet.Value;
            if (adet <= 0)
            {
                XtraMessageBox.Show("Lütfen geçerli bir adet giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aynı ürün varsa adet artır
            var mevcutDetay = _siparisDetaylar.FirstOrDefault(d => d.UrunID == urunID);
            if (mevcutDetay != null)
            {
                mevcutDetay.Adet += adet;
                mevcutDetay.ToplamFiyat = mevcutDetay.Adet * mevcutDetay.BirimFiyat;
            }
            else
            {
                SiparisDetaylar yeniDetay = new SiparisDetaylar
                {
                    UrunID = urunID,
                    Adet = adet,
                    BirimFiyat = urun.Fiyat,
                    ToplamFiyat = adet * urun.Fiyat
                };
                _siparisDetaylar.Add(yeniDetay);
            }

            _gridControlSiparisDetay.DataSource = null;
            _gridControlSiparisDetay.DataSource = _siparisDetaylar;
            HesaplaToplam();
            _spinAdet.Value = 1;
        }

        /// <summary>
        /// Handles the remove product button click event.
        /// </summary>
        private void BtnUrunCikar_Click(object sender, EventArgs e)
        {
            if (_gridViewSiparisDetay.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen bir ürün seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seciliDetay = _gridViewSiparisDetay.GetFocusedRow() as SiparisDetaylar;
            if (seciliDetay != null)
            {
                _siparisDetaylar.Remove(seciliDetay);
                _gridControlSiparisDetay.DataSource = null;
                _gridControlSiparisDetay.DataSource = _siparisDetaylar;
                HesaplaToplam();
            }
        }

        /// <summary>
        /// Calculates and displays the total amount.
        /// </summary>
        private void HesaplaToplam()
        {
            decimal toplam = _siparisDetaylar.Sum(d => d.ToplamFiyat);
            _lblToplam.Text = $"Toplam: {toplam:N2} TL";
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (_siparisDetaylar.Count == 0)
            {
                XtraMessageBox.Show("Lütfen en az bir ürün ekleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Sipariş oluştur
                Siparisler siparis = new Siparisler
                {
                    HareketID = _hareketID,
                    SiparisTarihi = DateTime.Now,
                    ToplamTutar = _siparisDetaylar.Sum(d => d.ToplamFiyat),
                    Durum = "Aktif"
                };

                int siparisID = _siparisService.Olustur(siparis);

                if (siparisID > 0)
                {
                    // Sipariş detaylarını ekle
                    foreach (var detay in _siparisDetaylar)
                    {
                        detay.SiparisID = siparisID;
                        _siparisService.DetayEkle(detay);
                    }

                    // Hareket tablosunu güncelle
                    SHareket hareketService = new SHareket();
                    hareketService.SiparisToplamiGuncelle(_hareketID, siparis.ToplamTutar);

                    XtraMessageBox.Show("Sipariş başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Sipariş ekleme işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the cancel button click event.
        /// </summary>
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

