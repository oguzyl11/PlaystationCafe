using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for managing orders (Siparisler).
    /// </summary>
    public partial class FrmSiparisler : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlSiparisler;
        private GridView _gridViewSiparisler;
        private SSiparis _siparisService;
        private SimpleButton _btnYeni;
        private SimpleButton _btnDetay;
        private SimpleButton _btnYenile;

        /// <summary>
        /// Initializes a new instance of the FrmSiparisler class.
        /// </summary>
        public FrmSiparisler()
        {
            InitializeComponent();
            _siparisService = new SSiparis();
            LoadSiparisler();
        }

        /// <summary>
        /// Loads all orders into the grid.
        /// </summary>
        private void LoadSiparisler()
        {
            try
            {
                List<Siparisler> siparisler = _siparisService.Listele();
                _gridControlSiparisler.DataSource = siparisler;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Siparişler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the new order button click event.
        /// </summary>
        private void BtnYeni_Click(object sender, EventArgs e)
        {
            // Sipariş ekleme FrmMasalar'dan yapılacak
            FrmMasalar frmMasalar = new FrmMasalar();
            frmMasalar.MdiParent = this.MdiParent;
            frmMasalar.Show();
        }

        /// <summary>
        /// Handles the detail button click event.
        /// </summary>
        private void BtnDetay_Click(object sender, EventArgs e)
        {
            if (_gridViewSiparisler.GetSelectedRows().Length > 0)
            {
                int siparisID = Convert.ToInt32(_gridViewSiparisler.GetRowCellValue(_gridViewSiparisler.GetSelectedRows()[0], "SiparisID"));
                // Sipariş detayları gösterilecek
                List<SiparisDetaylar> detaylar = _siparisService.GetDetaylar(siparisID);
                string detayBilgi = $"Sipariş ID: {siparisID}\n\nDetaylar:\n";
                foreach (var detay in detaylar)
                {
                    detayBilgi += $"- {detay.UrunID}: {detay.Adet} adet x {detay.BirimFiyat} = {detay.ToplamFiyat}\n";
                }
                XtraMessageBox.Show(detayBilgi, "Sipariş Detayları", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Lütfen detayını görmek için bir sipariş seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        private void BtnYenile_Click(object sender, EventArgs e)
        {
            LoadSiparisler();
        }
    }
}

