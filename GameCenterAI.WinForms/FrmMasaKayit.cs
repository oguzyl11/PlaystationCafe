using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for table registration and editing.
    /// </summary>
    public partial class FrmMasaKayit : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit _txtMasaAdi;
        private SpinEdit _spinSaatlikUcret;
        private ComboBoxEdit _cmbDurum;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private SMasalar _masaService;
        private Masalar _duzenlenecekMasa;
        private bool _duzenlemeModu;

        /// <summary>
        /// Initializes a new instance of the FrmMasaKayit class for new table registration.
        /// </summary>
        public FrmMasaKayit()
        {
            InitializeComponent();
            _masaService = new SMasalar();
            _duzenlemeModu = false;
            this.Text = "Yeni Masa Kaydı";
            _cmbDurum.SelectedIndex = 0;
        }

        /// <summary>
        /// Initializes a new instance of the FrmMasaKayit class for editing existing table.
        /// </summary>
        /// <param name="masa">The table to edit.</param>
        public FrmMasaKayit(Masalar masa) : this()
        {
            _duzenlenecekMasa = masa;
            _duzenlemeModu = true;
            this.Text = "Masa Düzenle";
            LoadMasaBilgileri();
        }

        /// <summary>
        /// Loads table information for editing.
        /// </summary>
        private void LoadMasaBilgileri()
        {
            if (_duzenlenecekMasa != null)
            {
                _txtMasaAdi.Text = _duzenlenecekMasa.MasaAdi;
                _spinSaatlikUcret.Value = _duzenlenecekMasa.SaatlikUcret;
                _cmbDurum.Text = _duzenlenecekMasa.Durum;
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtMasaAdi.Text))
                {
                    XtraMessageBox.Show("Lütfen masa adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtMasaAdi.Focus();
                    return;
                }

                if (_spinSaatlikUcret.Value <= 0)
                {
                    XtraMessageBox.Show("Lütfen geçerli bir saatlik ücret giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _spinSaatlikUcret.Focus();
                    return;
                }

                Masalar masa = new Masalar
                {
                    MasaAdi = _txtMasaAdi.Text.Trim(),
                    SaatlikUcret = _spinSaatlikUcret.Value,
                    Durum = _cmbDurum.Text
                };

                string hata = null;
                if (_duzenlemeModu && _duzenlenecekMasa != null)
                {
                    masa.MasaID = _duzenlenecekMasa.MasaID;
                    hata = _masaService.Guncelle(masa);
                }
                else
                {
                    hata = _masaService.Ekle(masa);
                }

                if (hata == null)
                {
                    XtraMessageBox.Show(_duzenlemeModu ? "Masa güncellendi!" : "Masa eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("İşlem başarısız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

