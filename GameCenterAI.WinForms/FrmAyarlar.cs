using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Service;
using GameCenterAI.Entity;
using System.Collections.Generic;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for application settings (Ayarlar).
    /// </summary>
    public partial class FrmAyarlar : DevExpress.XtraEditors.XtraForm
    {
        private XtraTabControl _tabControlAyarlar;
        private XtraTabPage _tabPageGenel;
        private XtraTabPage _tabPageVeritabani;
        private XtraTabPage _tabPageTarifeler;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;

        // Genel Ayarlar
        private LabelControl _lblKdvOrani;
        private SpinEdit _spinKdvOrani;
        private LabelControl _lblFaturaBaslik;
        private TextEdit _txtFaturaBaslik;
        private LabelControl _lblFaturaAltBilgi;
        private MemoEdit _txtFaturaAltBilgi;

        // Veritabanı Ayarları
        private LabelControl _lblServer;
        private TextEdit _txtServer;
        private LabelControl _lblDatabase;
        private TextEdit _txtDatabase;
        private SimpleButton _btnTestBaglanti;
        private LabelControl _lblBaglantiDurumu;

        // Tarife Ayarları
        private GridControl _gridControlTarifeler;
        private GridView _gridViewTarifeler;
        private SimpleButton _btnTarifeEkle;
        private SimpleButton _btnTarifeDuzenle;
        private SimpleButton _btnTarifeSil;
        private STarife _tarifeService;

        /// <summary>
        /// Initializes a new instance of the FrmAyarlar class.
        /// </summary>
        public FrmAyarlar()
        {
            InitializeComponent();
            _tarifeService = new STarife();
            LoadAyarlar();
            LoadTarifeler();
        }

        /// <summary>
        /// Loads current settings.
        /// </summary>
        private void LoadAyarlar()
        {
            try
            {
                // Default values
                _spinKdvOrani.Value = 20; // %20 KDV
                _txtFaturaBaslik.Text = "GameCenter AI - Playstation Cafe";
                _txtFaturaAltBilgi.Text = "Teşekkür ederiz!";
                
                // Database settings (read-only for now)
                _txtServer.Text = ".";
                _txtDatabase.Text = "GameCenterDB";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ayarlar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads tariffs into the grid.
        /// </summary>
        private void LoadTarifeler()
        {
            try
            {
                string hata = _tarifeService.Listele(out List<Tarifeler> tarifeler);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _gridControlTarifeler.DataSource = tarifeler;
                _gridViewTarifeler.PopulateColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Ayarları veritabanına veya config dosyasına kaydet
                // Şimdilik sadece mesaj göster
                XtraMessageBox.Show("Ayarlar kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ayarlar kaydedilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the cancel button click event.
        /// </summary>
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the test connection button click event.
        /// </summary>
        private void BtnTestBaglanti_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = GameCenterAI.DataAccess.Tools.TestConnection();
                if (result)
                {
                    _lblBaglantiDurumu.Text = "✅ Bağlantı Başarılı";
                    _lblBaglantiDurumu.Appearance.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    _lblBaglantiDurumu.Text = "❌ Bağlantı Başarısız";
                    _lblBaglantiDurumu.Appearance.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                _lblBaglantiDurumu.Text = $"❌ Hata: {ex.Message}";
                _lblBaglantiDurumu.Appearance.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Handles the add tariff button click event.
        /// </summary>
        private void BtnTarifeEkle_Click(object sender, EventArgs e)
        {
            // TODO: Tarife ekleme formu açılacak
            XtraMessageBox.Show("Tarife ekleme özelliği yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the edit tariff button click event.
        /// </summary>
        private void BtnTarifeDuzenle_Click(object sender, EventArgs e)
        {
            if (_gridViewTarifeler.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir tarife seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Tarife düzenleme formu açılacak
            XtraMessageBox.Show("Tarife düzenleme özelliği yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the delete tariff button click event.
        /// </summary>
        private void BtnTarifeSil_Click(object sender, EventArgs e)
        {
            if (_gridViewTarifeler.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen silmek için bir tarife seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Tarife silme işlemi
            XtraMessageBox.Show("Tarife silme özelliği yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
