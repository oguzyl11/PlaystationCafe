using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Login form for user authentication.
    /// </summary>
    public partial class FrmGiris : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit _txtKullaniciAdi;
        private TextEdit _txtSifre;
        private SimpleButton _btnGiris;
        private SimpleButton _btnKayit;
        private GroupControl _grpGiris;
        private SUyeler _uyeService;

        /// <summary>
        /// Initializes a new instance of the FrmGiris class.
        /// </summary>
        public FrmGiris()
        {
            InitializeComponent();
            _uyeService = new SUyeler();
        }

        /// <summary>
        /// Handles the login button click event.
        /// </summary>
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciAdi = _txtKullaniciAdi.Text.Trim();
                string sifre = _txtSifre.Text.Trim();

                if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
                {
                    XtraMessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Uyeler uye = _uyeService.GirisYap(kullaniciAdi, sifre);

                if (uye != null)
                {
                    XtraMessageBox.Show($"Hoş geldiniz, {uye.AdSoyad}!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Open main menu form
                    FrmAnaMenu frmAnaMenu = new FrmAnaMenu(uye);
                    this.Hide();
                    frmAnaMenu.ShowDialog();
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _txtSifre.Text = string.Empty;
                    _txtKullaniciAdi.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Giriş işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the register button click event.
        /// </summary>
        private void BtnKayit_Click(object sender, EventArgs e)
        {
            FrmUyeKayit frmUyeKayit = new FrmUyeKayit();
            if (frmUyeKayit.ShowDialog() == DialogResult.OK)
            {
                _txtSifre.Text = string.Empty;
            }
        }
    }
}

