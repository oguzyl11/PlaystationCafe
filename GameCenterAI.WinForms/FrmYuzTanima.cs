using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for face recognition using webcam and EmguCV.
    /// </summary>
    public partial class FrmYuzTanima : DevExpress.XtraEditors.XtraForm
    {
        private PictureBox _pictureBoxKamera;
        private SimpleButton _btnKamerayiAc;
        private SimpleButton _btnYuzuKaydet;
        private SimpleButton _btnGirisYap;
        private SimpleButton _btnKapat;
        private LabelControl _lblDurum;
        private System.Windows.Forms.Timer _timer;
        private SAiService _aiService;
        private SUyeler _uyeService;
        private bool _kameraAcik;
        private int _seciliUyeID;

        /// <summary>
        /// Initializes a new instance of the FrmYuzTanima class.
        /// </summary>
        public FrmYuzTanima()
        {
            InitializeComponent();
            _aiService = new SAiService();
            _uyeService = new SUyeler();
            _kameraAcik = false;
            _seciliUyeID = -1;
            InitializeTimer();
        }

        /// <summary>
        /// Initializes the timer for camera frame updates.
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100; // 10 FPS
            _timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Timer tick event handler for capturing frames from camera.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_kameraAcik)
            {
                try
                {
                    byte[] faceImage = _aiService.CaptureAndDetectFace();
                    
                    if (faceImage != null && faceImage.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(faceImage))
                        {
                            Image img = Image.FromStream(ms);
                            _pictureBoxKamera.Image?.Dispose();
                            _pictureBoxKamera.Image = new Bitmap(img);
                        }
                        _lblDurum.Text = "Yüz tespit edildi!";
                        _lblDurum.Appearance.ForeColor = Color.Green;
                    }
                    else
                    {
                        _lblDurum.Text = "Yüz tespit edilemedi. Lütfen kameraya bakın.";
                        _lblDurum.Appearance.ForeColor = Color.Orange;
                    }
                }
                catch (Exception ex)
                {
                    _lblDurum.Text = $"Hata: {ex.Message}";
                    _lblDurum.Appearance.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Handles the open camera button click event.
        /// </summary>
        private void BtnKamerayiAc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_kameraAcik)
                {
                    _timer.Start();
                    _kameraAcik = true;
                    _btnKamerayiAc.Text = "Kamerayı Kapat";
                    _lblDurum.Text = "Kamera açıldı. Yüzünüzü kameraya gösterin.";
                    _lblDurum.Appearance.ForeColor = Color.Blue;
                }
                else
                {
                    _timer.Stop();
                    _kameraAcik = false;
                    _btnKamerayiAc.Text = "Kamerayı Aç";
                    _pictureBoxKamera.Image?.Dispose();
                    _pictureBoxKamera.Image = null;
                    _lblDurum.Text = "Kamera kapatıldı.";
                    _lblDurum.Appearance.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kamera açma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the save face button click event.
        /// </summary>
        private void BtnYuzuKaydet_Click(object sender, EventArgs e)
        {
            if (!_kameraAcik)
            {
                XtraMessageBox.Show("Önce kamerayı açmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Üye seçimi
            FrmUyeSec frmUyeSec = new FrmUyeSec();
            if (frmUyeSec.ShowDialog() != DialogResult.OK || frmUyeSec.SecilenUye == null)
            {
                return;
            }

            try
            {
                byte[] faceImage = _aiService.CaptureAndDetectFace();
                
                if (faceImage == null || faceImage.Length == 0)
                {
                    XtraMessageBox.Show("Yüz tespit edilemedi. Lütfen kameraya bakın ve tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update user's face encoding in database
                Uyeler uye = frmUyeSec.SecilenUye;
                uye.FaceEncoding = faceImage;
                
                bool result = _uyeService.Guncelle(uye);

                if (result)
                {
                    XtraMessageBox.Show("Yüz başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _seciliUyeID = uye.UyeID;
                }
                else
                {
                    XtraMessageBox.Show("Yüz kaydedilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Yüz kaydetme işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the login button click event.
        /// </summary>
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            if (!_kameraAcik)
            {
                XtraMessageBox.Show("Önce kamerayı açmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] currentFace = _aiService.CaptureAndDetectFace();
                
                if (currentFace == null || currentFace.Length == 0)
                {
                    XtraMessageBox.Show("Yüz tespit edilemedi. Lütfen kameraya bakın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get all users with face encoding
                var uyeler = _uyeService.Listele();
                int bulunanUyeID = -1;

                foreach (var uye in uyeler)
                {
                    if (uye.FaceEncoding != null && uye.FaceEncoding.Length > 0)
                    {
                        bool eslesme = _aiService.CompareFaces(uye.FaceEncoding, currentFace);
                        
                        if (eslesme)
                        {
                            bulunanUyeID = uye.UyeID;
                            break;
                        }
                    }
                }

                if (bulunanUyeID > 0)
                {
                    Uyeler uye = _uyeService.Getir(bulunanUyeID);
                    XtraMessageBox.Show($"Giriş başarılı! Hoş geldiniz {uye.AdSoyad}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Yüz tanınmadı. Lütfen kayıt olun veya tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Giriş işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the close button click event.
        /// </summary>
        private void BtnKapat_Click(object sender, EventArgs e)
        {
            if (_kameraAcik)
            {
                _timer.Stop();
                _kameraAcik = false;
            }

            this.Close();
        }

        /// <summary>
        /// Cleanup when form is closing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            if (_pictureBoxKamera.Image != null)
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

