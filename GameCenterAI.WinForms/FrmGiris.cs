using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private SAiService _aiService;
        private System.Windows.Forms.Timer _yuzTanimaTimer;
        private bool _kameraAcik;
        private bool _girisYapildi;

        /// <summary>
        /// Initializes a new instance of the FrmGiris class.
        /// </summary>
        public FrmGiris()
        {
            InitializeComponent();
            _uyeService = new SUyeler();
            _aiService = new SAiService();
            _kameraAcik = false;
            _girisYapildi = false;
            InitializeYuzTanima();
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

                string hata = _uyeService.GirisYap(kullaniciAdi, sifre, out Uyeler uye);
                
                if (hata != null)
                {
                    XtraMessageBox.Show($"Giriş işlemi sırasında hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (uye != null)
                {
                    // Stop face recognition timer
                    if (_yuzTanimaTimer != null)
                    {
                        _yuzTanimaTimer.Stop();
                    }
                    _girisYapildi = true;

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

        /// <summary>
        /// Handles the KeyDown event for password textbox - allows Enter key to login.
        /// </summary>
        private void TxtSifre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGiris_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Initializes face recognition components and starts automatic face detection.
        /// </summary>
        private void InitializeYuzTanima()
        {
            _yuzTanimaTimer = new System.Windows.Forms.Timer();
            _yuzTanimaTimer.Interval = 500; // Check every 500ms
            _yuzTanimaTimer.Tick += YuzTanimaTimer_Tick;
            
            // Start camera and face recognition automatically
            try
            {
                _kameraAcik = true;
                _yuzTanimaTimer.Start();
                _lblYuzTanimaDurum.Text = "Yüz tanıma aktif... Kameraya bakın.";
                _lblYuzTanimaDurum.Appearance.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                _lblYuzTanimaDurum.Text = "Kamera açılamadı: " + ex.Message;
                _lblYuzTanimaDurum.Appearance.ForeColor = Color.Red;
                _kameraAcik = false;
            }
        }

        /// <summary>
        /// Timer tick event for automatic face recognition.
        /// </summary>
        private void YuzTanimaTimer_Tick(object sender, EventArgs e)
        {
            if (!_kameraAcik || _girisYapildi)
            {
                return;
            }

            try
            {
                byte[] faceImage = _aiService.CaptureAndDetectFace();
                
                if (faceImage != null && faceImage.Length > 0)
                {
                    // Display face image
                    using (MemoryStream ms = new MemoryStream(faceImage))
                    {
                        Image img = Image.FromStream(ms);
                        _pictureBoxKamera.Image?.Dispose();
                        _pictureBoxKamera.Image = new Bitmap(img);
                    }

                    // Try to recognize the face
                    string hataListe = _uyeService.Listele(out List<Uyeler> uyeler);
                    if (hataListe != null)
                    {
                        return;
                    }
                    
                    int bulunanUyeID = -1;

                    foreach (var uye in uyeler)
                    {
                        if (uye.FaceEncoding != null && uye.FaceEncoding.Length > 0)
                        {
                            string hataKarsilastirma = _aiService.CompareFaces(uye.FaceEncoding, faceImage, out bool eslesme);
                            
                            if (hataKarsilastirma == null && eslesme)
                            {
                                bulunanUyeID = uye.UyeID;
                                break;
                            }
                        }
                    }

                    if (bulunanUyeID > 0)
                    {
                        // Face recognized - auto login
                        _yuzTanimaTimer.Stop();
                        _girisYapildi = true;
                        _lblYuzTanimaDurum.Text = "Yüz tanındı! Giriş yapılıyor...";
                        _lblYuzTanimaDurum.Appearance.ForeColor = Color.Green;

                        string hataGetir = _uyeService.Getir(bulunanUyeID, out Uyeler uye);
                        if (hataGetir != null)
                        {
                            return;
                        }
                        
                        // Auto login
                        XtraMessageBox.Show($"Hoş geldiniz, {uye.AdSoyad}!", "Yüz Tanıma Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Open main menu form
                        FrmAnaMenu frmAnaMenu = new FrmAnaMenu(uye);
                        this.Hide();
                        frmAnaMenu.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        _lblYuzTanimaDurum.Text = "Yüz tespit edildi ancak tanınmadı. Lütfen kullanıcı adı ve şifre ile giriş yapın.";
                        _lblYuzTanimaDurum.Appearance.ForeColor = Color.Orange;
                    }
                }
                else
                {
                    _lblYuzTanimaDurum.Text = "Yüz tespit edilemedi. Lütfen kameraya bakın.";
                    _lblYuzTanimaDurum.Appearance.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                // Silently handle errors - don't interrupt user if they want to login manually
                if (_lblYuzTanimaDurum != null)
                {
                    _lblYuzTanimaDurum.Text = "Yüz tanıma hatası: " + ex.Message;
                    _lblYuzTanimaDurum.Appearance.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Cleanup when form is closing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_yuzTanimaTimer != null)
            {
                _yuzTanimaTimer.Stop();
                _yuzTanimaTimer.Dispose();
            }

            if (_pictureBoxKamera?.Image != null)
            {
                _pictureBoxKamera.Image.Dispose();
                _pictureBoxKamera.Image = null;
            }

            if (_aiService != null)
            {
                _aiService.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}

