using System;
using System.Linq;
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
            // Shown event'ini bir kere bağla
            this.Shown += FrmAnaMenu_Shown;
        }

        /// <summary>
        /// Handles the form shown event - loads Masalar after form is fully displayed.
        /// </summary>
        private void FrmAnaMenu_Shown(object sender, EventArgs e)
        {
            try
            {
                // Ana sayfada masaları göster - tek pencere olarak
                MasalariGoster();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Masalar yüklenirken hata oluştu: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Shows the Masalar form as a single window (not MDI).
        /// </summary>
        private void MasalariGoster()
        {
            try
            {
                // Panel'in hazır olduğundan emin ol
                if (_contentPanel == null || !_contentPanel.IsHandleCreated)
                {
                    System.Threading.Thread.Sleep(100); // Kısa bir bekleme
                }

                // Dispose existing forms first (frmMasalar hariç)
                var formsToDispose = _contentPanel.Controls.OfType<Form>().Where(f => f != _frmMasalar).ToList();
                foreach (var form in formsToDispose)
                {
                    form.Hide();
                    form.Dispose();
                }

                // Tüm içeriği temizle (frmMasalar hariç)
                var formsToRemove = _contentPanel.Controls.OfType<Form>().Where(f => f != _frmMasalar).ToList();
                foreach (var form in formsToRemove)
                {
                    _contentPanel.Controls.Remove(form);
                }
                
                Application.DoEvents(); // UI'ı güncelle
                
                // Eğer frmMasalar zaten panel içindeyse, sadece öne getir
                if (_frmMasalar != null && !_frmMasalar.IsDisposed && _contentPanel.Controls.Contains(_frmMasalar))
                {
                    _frmMasalar.BringToFront();
                    _contentPanel.Invalidate();
                    _contentPanel.Update();
                    return;
                }
                
                // Yeni form oluştur
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
                
                // Force refresh and update
                _contentPanel.Invalidate();
                _contentPanel.Update();
                _frmMasalar.Invalidate();
                _frmMasalar.Update();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Masalar formu yüklenirken hata oluştu: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            ShowFormInPanel<FrmUyeler>();
        }

        /// <summary>
        /// Handles the Stok (Stock) button click event.
        /// </summary>
        private void BarButtonItemStok_Click(object sender, ItemClickEventArgs e)
        {
            ShowFormInPanel<FrmUrunler>();
        }

        /// <summary>
        /// Handles the Muhasebe (Accounting) button click event.
        /// </summary>
        private void BarButtonItemMuhasebe_Click(object sender, ItemClickEventArgs e)
        {
            ShowFormInPanel<FrmRaporlar>();
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
            ShowFormInPanel<FrmUyeler>();
            // Set title if needed
            foreach (Control ctrl in _contentPanel.Controls)
            {
                if (ctrl is FrmUyeler)
                {
                    ((FrmUyeler)ctrl).Text = "SMS Gönderimi - Üye Listesi";
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Ayarlar (Settings) button click event.
        /// </summary>
        private void BarButtonItemAyarlar_Click(object sender, ItemClickEventArgs e)
        {
            // Ayarlar modal olarak kalabilir veya panel içinde açılabilir
            // Kullanıcı tercihine göre panel içinde açıyoruz
            ShowFormInPanel<FrmAyarlar>();
        }

        /// <summary>
        /// Handles the Yetkili (Authorized) button click event.
        /// </summary>
        private void BarButtonItemYetkili_Click(object sender, ItemClickEventArgs e)
        {
            ShowFormInPanel<FrmUyeler>();
            // Set title if needed
            foreach (Control ctrl in _contentPanel.Controls)
            {
                if (ctrl is FrmUyeler)
                {
                    ((FrmUyeler)ctrl).Text = "Yetkili Yönetimi - Üye Listesi";
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Araçlar (Tools) button click event.
        /// </summary>
        private void BarButtonItemAraclar_Click(object sender, ItemClickEventArgs e)
        {
            ShowFormInPanel<FrmRaporlar>();
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
            ShowFormInPanel<FrmTurnuva>();
        }

        /// <summary>
        /// Shows a form inside the content panel (single window approach).
        /// </summary>
        /// <typeparam name="T">The form type to show.</typeparam>
        private void ShowFormInPanel<T>() where T : Form, new()
        {
            try
            {
                // Panel'in hazır olduğundan emin ol
                if (_contentPanel == null || !_contentPanel.IsHandleCreated)
                {
                    System.Threading.Thread.Sleep(100); // Kısa bir bekleme
                }

                // Dispose existing forms first
                var formsToDispose = _contentPanel.Controls.OfType<Form>().ToList();
                foreach (var existingForm in formsToDispose)
                {
                    existingForm.Hide();
                    existingForm.Dispose();
                }

                // Clear existing controls
                _contentPanel.Controls.Clear();
                Application.DoEvents(); // UI'ı güncelle

                // Create and show new form
                T form = new T();
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.Visible = true;

                _contentPanel.Controls.Add(form);
                form.BringToFront();
                
                // Force refresh and update
                _contentPanel.Invalidate();
                _contentPanel.Update();
                form.Invalidate();
                form.Update();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Yüz Tanıma (Face Recognition) button click event.
        /// </summary>
        private void BarButtonItemYuzTanima_Click(object sender, ItemClickEventArgs e)
        {
            // Yüz tanıma da panel içinde açılsın (tek pencere mantığı)
            ShowFormInPanel<FrmYuzTanima>();
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
