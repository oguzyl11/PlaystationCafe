using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for product registration and editing.
    /// </summary>
    public partial class FrmUrunKayit : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit _txtUrunAdi;
        private ComboBoxEdit _cmbKategori;
        private SpinEdit _spinFiyat;
        private SpinEdit _spinStok;
        private ComboBoxEdit _cmbDurum;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private SUrun _urunService;
        private Urunler _duzenlenecekUrun;
        private bool _duzenlemeModu;

        /// <summary>
        /// Initializes a new instance of the FrmUrunKayit class for new product registration.
        /// </summary>
        public FrmUrunKayit()
        {
            InitializeComponent();
            _urunService = new SUrun();
            _duzenlemeModu = false;
            this.Text = "Yeni Ürün Kaydı";
            _cmbDurum.SelectedIndex = 0;
            LoadKategoriler();
        }

        /// <summary>
        /// Loads predefined categories into the combo box.
        /// </summary>
        private void LoadKategoriler()
        {
            _cmbKategori.Properties.Items.AddRange(new string[] 
            { 
                "İçecek", 
                "Yiyecek", 
                "Atıştırmalık", 
                "Tatlı", 
                "Kahve", 
                "Çay", 
                "Soğuk İçecek",
                "Sıcak İçecek",
                "Fast Food",
                "Salata",
                "Diğer"
            });
        }

        /// <summary>
        /// Initializes a new instance of the FrmUrunKayit class for editing existing product.
        /// </summary>
        /// <param name="urun">The product to edit.</param>
        public FrmUrunKayit(Urunler urun) : this()
        {
            _duzenlenecekUrun = urun;
            _duzenlemeModu = true;
            this.Text = "Ürün Düzenle";
            LoadUrunBilgileri();
        }

        /// <summary>
        /// Loads product information for editing.
        /// </summary>
        private void LoadUrunBilgileri()
        {
            if (_duzenlenecekUrun != null)
            {
                _txtUrunAdi.Text = _duzenlenecekUrun.UrunAdi;
                _cmbKategori.Text = _duzenlenecekUrun.Kategori;
                _spinFiyat.Value = _duzenlenecekUrun.Fiyat;
                _spinStok.Value = _duzenlenecekUrun.Stok;
                _cmbDurum.Text = _duzenlenecekUrun.Durum;
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtUrunAdi.Text))
                {
                    XtraMessageBox.Show("Lütfen ürün adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtUrunAdi.Focus();
                    return;
                }

                if (_spinFiyat.Value <= 0)
                {
                    XtraMessageBox.Show("Lütfen geçerli bir fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _spinFiyat.Focus();
                    return;
                }

                if (_duzenlemeModu)
                {
                    // Güncelleme işlemi
                    _duzenlenecekUrun.UrunAdi = _txtUrunAdi.Text;
                    _duzenlenecekUrun.Kategori = _cmbKategori.Text;
                    _duzenlenecekUrun.Fiyat = _spinFiyat.Value;
                    _duzenlenecekUrun.Stok = (int)_spinStok.Value;
                    _duzenlenecekUrun.Durum = _cmbDurum.Text;
                    // TODO: Güncelleme servisi eklenecek
                    XtraMessageBox.Show("Ürün bilgileri güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Aynı isimde ürün kontrolü
                    var mevcutUrun = _urunService.GetirByUrunAdi(_txtUrunAdi.Text);
                    if (mevcutUrun != null)
                    {
                        // Aynı isimde ürün var - stok artır veya fiyat değiştir
                        if (XtraMessageBox.Show($"'{_txtUrunAdi.Text}' isimli ürün zaten mevcut.\n\nStok artırılsın mı? (Hayır derseniz fiyat güncellenecek)", "Ürün Mevcut", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Stok artır
                            mevcutUrun.Stok += (int)_spinStok.Value;
                            _urunService.StokGuncelle(mevcutUrun.UrunID, mevcutUrun.Stok);
                            XtraMessageBox.Show($"Stok artırıldı! Yeni stok: {mevcutUrun.Stok}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Fiyat güncelle
                            mevcutUrun.Fiyat = _spinFiyat.Value;
                            _urunService.FiyatGuncelle(mevcutUrun.UrunID, mevcutUrun.Fiyat);
                            XtraMessageBox.Show($"Fiyat güncellendi! Yeni fiyat: {mevcutUrun.Fiyat:N2} TL", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Yeni kayıt
                        Urunler yeniUrun = new Urunler
                        {
                            UrunAdi = _txtUrunAdi.Text,
                            Kategori = _cmbKategori.Text,
                            Fiyat = _spinFiyat.Value,
                            Stok = (int)_spinStok.Value,
                            Durum = _cmbDurum.Text
                        };

                        bool result = _urunService.Ekle(yeniUrun);

                        if (result)
                        {
                            XtraMessageBox.Show("Ürün kaydı başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show("Ürün kaydı oluşturulamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"İşlem sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

