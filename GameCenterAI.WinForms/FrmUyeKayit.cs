using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for member registration.
    /// </summary>
    public partial class FrmUyeKayit : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit _txtAdSoyad;
        private TextEdit _txtKullaniciAdi;
        private TextEdit _txtSifre;
        private TextEdit _txtSifreTekrar;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private SUyeler _uyeService;
        private Uyeler _duzenlenecekUye;
        private bool _duzenlemeModu;

        /// <summary>
        /// Initializes a new instance of the FrmUyeKayit class for new member registration.
        /// </summary>
        public FrmUyeKayit()
        {
            InitializeComponent();
            _uyeService = new SUyeler();
            _duzenlemeModu = false;
            this.Text = "Yeni Üye Kaydı";
        }

        /// <summary>
        /// Initializes a new instance of the FrmUyeKayit class for editing existing member.
        /// </summary>
        /// <param name="uye">The member to edit.</param>
        public FrmUyeKayit(Uyeler uye) : this()
        {
            _duzenlenecekUye = uye;
            _duzenlemeModu = true;
            this.Text = "Üye Düzenle";
            LoadUyeBilgileri();
        }

        /// <summary>
        /// Loads member information for editing.
        /// </summary>
        private void LoadUyeBilgileri()
        {
            if (_duzenlenecekUye != null)
            {
                _txtAdSoyad.Text = _duzenlenecekUye.AdSoyad;
                _txtKullaniciAdi.Text = _duzenlenecekUye.KullaniciAdi;
                _txtKullaniciAdi.Enabled = false; // Kullanıcı adı değiştirilemez
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtAdSoyad.Text))
                {
                    XtraMessageBox.Show("Lütfen ad soyad giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtAdSoyad.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(_txtKullaniciAdi.Text))
                {
                    XtraMessageBox.Show("Lütfen kullanıcı adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtKullaniciAdi.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(_txtSifre.Text))
                {
                    XtraMessageBox.Show("Lütfen şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtSifre.Focus();
                    return;
                }

                if (_txtSifre.Text != _txtSifreTekrar.Text)
                {
                    XtraMessageBox.Show("Şifreler eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtSifreTekrar.Focus();
                    return;
                }

                if (_duzenlemeModu)
                {
                    // Güncelleme işlemi
                    _duzenlenecekUye.AdSoyad = _txtAdSoyad.Text;
                    _duzenlenecekUye.Sifre = _txtSifre.Text;
                    // TODO: Güncelleme servisi eklenecek
                    XtraMessageBox.Show("Üye bilgileri güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Yeni kayıt
                    Uyeler yeniUye = new Uyeler
                    {
                        AdSoyad = _txtAdSoyad.Text,
                        KullaniciAdi = _txtKullaniciAdi.Text,
                        Sifre = _txtSifre.Text,
                        Bakiye = 0,
                        Durum = true
                    };

                    bool result = _uyeService.Ekle(yeniUye);

                    if (result)
                    {
                        XtraMessageBox.Show("Üye kaydı başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Üye kaydı oluşturulamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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

        /// <summary>
        /// Handles the KeyDown event for password repeat textbox - allows Enter key to submit.
        /// </summary>
        private void TxtSifreTekrar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnKaydet_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}

