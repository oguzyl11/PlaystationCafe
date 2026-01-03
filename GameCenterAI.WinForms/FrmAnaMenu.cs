using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Main menu form with ribbon interface for navigation.
    /// </summary>
    public partial class FrmAnaMenu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Uyeler _aktifUye;

        /// <summary>
        /// Initializes a new instance of the FrmAnaMenu class.
        /// </summary>
        /// <param name="uye">The logged-in user.</param>
        public FrmAnaMenu(Uyeler uye)
        {
            InitializeComponent();
            _aktifUye = uye;
            this.Load += FrmAnaMenu_Load;
        }

        private FrmMasalar _frmMasalar;

        /// <summary>
        /// Handles the form load event - opens Masalar form automatically.
        /// </summary>
        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            // Ana sayfada masaları göster - tek pencere olarak
            MasalariGoster();
        }

        /// <summary>
        /// Shows the Masalar form as a single window (not MDI).
        /// </summary>
        private void MasalariGoster()
        {
            // Tüm içeriği temizle
            _contentPanel.Controls.Clear();
            
            if (_frmMasalar == null || _frmMasalar.IsDisposed)
            {
                _frmMasalar = new FrmMasalar();
            }
            
            _frmMasalar.TopLevel = false;
            _frmMasalar.FormBorderStyle = FormBorderStyle.None;
            _frmMasalar.Dock = DockStyle.Fill;
            _frmMasalar.Visible = true;
            
            _contentPanel.Controls.Add(_frmMasalar);
            _frmMasalar.BringToFront();
        }

        /// <summary>
        /// Handles the Kafe (Cafe) button click event.
        /// </summary>
        private void BarButtonItemKafe_Click(object sender, ItemClickEventArgs e)
        {
            MasalariGoster();
        }

        /// <summary>
        /// Handles the Cari/Üye (Member) button click event.
        /// </summary>
        private void BarButtonItemCariUye_Click(object sender, ItemClickEventArgs e)
        {
            FrmUyeler frmUyeler = new FrmUyeler();
            frmUyeler.Show();
        }

        /// <summary>
        /// Handles the Stok (Stock) button click event.
        /// </summary>
        private void BarButtonItemStok_Click(object sender, ItemClickEventArgs e)
        {
            FrmUrunler frmUrunler = new FrmUrunler();
            frmUrunler.Show();
        }

        /// <summary>
        /// Handles the Muhasebe (Accounting) button click event.
        /// </summary>
        private void BarButtonItemMuhasebe_Click(object sender, ItemClickEventArgs e)
        {
            FrmRaporlar frmRaporlar = new FrmRaporlar();
            frmRaporlar.Show();
        }

        /// <summary>
        /// Handles the Kontrol (Control) button click event.
        /// </summary>
        private void BarButtonItemKontrol_Click(object sender, ItemClickEventArgs e)
        {
            MasalariGoster();
        }

        /// <summary>
        /// Handles the Birim İşlemleri (Unit Operations) button click event.
        /// </summary>
        private void BarButtonItemBirimIslemleri_Click(object sender, ItemClickEventArgs e)
        {
            MasalariGoster();
        }

        /// <summary>
        /// Handles the SMS button click event.
        /// </summary>
        private void BarButtonItemSMS_Click(object sender, ItemClickEventArgs e)
        {
            FrmUyeler frmUyeler = new FrmUyeler();
            frmUyeler.Text = "SMS Gönderimi - Üye Listesi";
            frmUyeler.Show();
        }

        /// <summary>
        /// Handles the Ayarlar (Settings) button click event.
        /// </summary>
        private void BarButtonItemAyarlar_Click(object sender, ItemClickEventArgs e)
        {
            FrmAyarlar frmAyarlar = new FrmAyarlar();
            frmAyarlar.ShowDialog();
        }

        /// <summary>
        /// Handles the Yetkili (Authorized) button click event.
        /// </summary>
        private void BarButtonItemYetkili_Click(object sender, ItemClickEventArgs e)
        {
            FrmUyeler frmUyeler = new FrmUyeler();
            frmUyeler.Text = "Yetkili Yönetimi - Üye Listesi";
            frmUyeler.Show();
        }

        /// <summary>
        /// Handles the Araçlar (Tools) button click event.
        /// </summary>
        private void BarButtonItemAraclar_Click(object sender, ItemClickEventArgs e)
        {
            FrmRaporlar frmRaporlar = new FrmRaporlar();
            frmRaporlar.Show();
        }

        /// <summary>
        /// Handles the Notlar (Notes) button click event.
        /// </summary>
        private void BarButtonItemNotlar_Click(object sender, ItemClickEventArgs e)
        {
            MasalariGoster();
        }

        /// <summary>
        /// Handles the Yardım (Help) button click event.
        /// </summary>
        private void BarButtonItemYardim_Click(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("GameCenter AI v1.0\n\nYardım menüsü.", "Yardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the Turnuva (Tournament) button click event.
        /// </summary>
        private void BarButtonItemTurnuva_Click(object sender, ItemClickEventArgs e)
        {
            FrmTurnuva frmTurnuva = new FrmTurnuva();
            frmTurnuva.Show();
        }

        /// <summary>
        /// Handles the Yüz Tanıma (Face Recognition) button click event.
        /// </summary>
        private void BarButtonItemYuzTanima_Click(object sender, ItemClickEventArgs e)
        {
            FrmYuzTanima frmYuzTanima = new FrmYuzTanima();
            frmYuzTanima.ShowDialog();
        }

        /// <summary>
        /// Handles the exit button click event.
        /// </summary>
        private void BarButtonItemCikis_Click(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Çıkmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
