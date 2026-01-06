using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for managing invoices (Faturalar).
    /// </summary>
    public partial class FrmFaturalar : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlFaturalar;
        private GridView _gridViewFaturalar;
        private SFatura _faturaService;
        private SHareket _hareketService;
        private SUyeler _uyeService;
        private SMasalar _masaService;
        private SimpleButton _btnYeni;
        private SimpleButton _btnGoster;
        private SimpleButton _btnYazdir;
        private SimpleButton _btnYenile;
        private DateEdit _dateEditBaslangic;
        private DateEdit _dateEditBitis;
        private LabelControl _lblBaslangic;
        private LabelControl _lblBitis;
        private SimpleButton _btnFiltrele;

        /// <summary>
        /// Initializes a new instance of the FrmFaturalar class.
        /// </summary>
        public FrmFaturalar()
        {
            InitializeComponent();
            _faturaService = new SFatura();
            _hareketService = new SHareket();
            _uyeService = new SUyeler();
            _masaService = new SMasalar();
            
            _dateEditBaslangic.DateTime = DateTime.Now.AddDays(-30);
            _dateEditBitis.DateTime = DateTime.Now;
            
            LoadFaturalar();
        }

        /// <summary>
        /// Loads all invoices into the grid.
        /// </summary>
        private void LoadFaturalar()
        {
            try
            {
                string hata = _faturaService.TarihAraligindaGetir(_dateEditBaslangic.DateTime, _dateEditBitis.DateTime, out List<Faturalar> faturalar);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Faturalar yüklenirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _gridControlFaturalar.DataSource = faturalar;
                _gridViewFaturalar.PopulateColumns();
                
                // Column formatting
                if (_gridViewFaturalar.Columns["ToplamTutar"] != null)
                {
                    _gridViewFaturalar.Columns["ToplamTutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    _gridViewFaturalar.Columns["ToplamTutar"].DisplayFormat.FormatString = "N2";
                }
                if (_gridViewFaturalar.Columns["KdvTutari"] != null)
                {
                    _gridViewFaturalar.Columns["KdvTutari"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    _gridViewFaturalar.Columns["KdvTutari"].DisplayFormat.FormatString = "N2";
                }
                if (_gridViewFaturalar.Columns["GenelToplam"] != null)
                {
                    _gridViewFaturalar.Columns["GenelToplam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    _gridViewFaturalar.Columns["GenelToplam"].DisplayFormat.FormatString = "N2";
                }
                if (_gridViewFaturalar.Columns["FaturaTarihi"] != null)
                {
                    _gridViewFaturalar.Columns["FaturaTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    _gridViewFaturalar.Columns["FaturaTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Faturalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the filter button click event.
        /// </summary>
        private void BtnFiltrele_Click(object sender, EventArgs e)
        {
            LoadFaturalar();
        }

        /// <summary>
        /// Handles the show invoice details button click event.
        /// </summary>
        private void BtnGoster_Click(object sender, EventArgs e)
        {
            if (_gridViewFaturalar.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen bir fatura seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int faturaID = Convert.ToInt32(_gridViewFaturalar.GetFocusedRowCellValue("FaturaID"));
                string hata = _faturaService.Getir(faturaID, out Faturalar fatura);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Fatura getirilirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (fatura == null)
                {
                    XtraMessageBox.Show("Fatura bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (fatura != null)
                {
                    // Get transaction details
                    string hataHareket = _hareketService.Getir(fatura.HareketID, out Hareketler hareket);
                    Uyeler uye = null;
                    Masalar masa = null;
                    if (hataHareket == null && hareket != null)
                    {
                        string hataUye = _uyeService.Getir(hareket.UyeID, out uye);
                        string hataMasa = _masaService.Getir(hareket.MasaID, out masa);
                    }

                    // Show invoice details
                    string detay = $"📄 FATURA DETAYI\n\n";
                    detay += $"Fatura No: {fatura.FaturaNo}\n";
                    detay += $"Tarih: {fatura.FaturaTarihi:dd.MM.yyyy HH:mm}\n";
                    detay += $"Durum: {fatura.Durum}\n\n";
                    
                    if (uye != null)
                    {
                        detay += $"Müşteri: {uye.AdSoyad}\n";
                    }
                    if (masa != null)
                    {
                        detay += $"Masa: {masa.MasaAdi}\n";
                    }
                    if (hareket != null)
                    {
                        detay += $"Başlangıç: {hareket.Baslangic:dd.MM.yyyy HH:mm}\n";
                        if (hareket.Bitis.HasValue)
                        {
                            detay += $"Bitiş: {hareket.Bitis.Value:dd.MM.yyyy HH:mm}\n";
                        }
                    }
                    
                    detay += $"\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                    detay += $"Ara Toplam: {fatura.ToplamTutar:N2} TL\n";
                    detay += $"KDV (%{fatura.KdvOrani}): {fatura.KdvTutari:N2} TL\n";
                    detay += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                    detay += $"GENEL TOPLAM: {fatura.GenelToplam:N2} TL\n";
                    
                    if (!string.IsNullOrEmpty(fatura.Notlar))
                    {
                        detay += $"\nNotlar: {fatura.Notlar}";
                    }

                    XtraMessageBox.Show(detay, "Fatura Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Fatura detayı gösterilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the print invoice button click event.
        /// </summary>
        private void BtnYazdir_Click(object sender, EventArgs e)
        {
            if (_gridViewFaturalar.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen bir fatura seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int faturaID = Convert.ToInt32(_gridViewFaturalar.GetFocusedRowCellValue("FaturaID"));
                string hata = _faturaService.Getir(faturaID, out Faturalar fatura);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Fatura getirilirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (fatura == null)
                {
                    XtraMessageBox.Show("Fatura bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (fatura != null)
                {
                    // Print invoice (placeholder - can be extended with report generation)
                    XtraMessageBox.Show($"Fatura yazdırma özelliği yakında eklenecek.\n\nFatura No: {fatura.FaturaNo}", "Yazdır", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Fatura yazdırma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        private void BtnYenile_Click(object sender, EventArgs e)
        {
            LoadFaturalar();
        }
    }
}

