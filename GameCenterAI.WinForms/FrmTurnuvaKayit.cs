using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for tournament registration and editing.
    /// </summary>
    public partial class FrmTurnuvaKayit : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit _txtTurnuvaAdi;
        private DateEdit _dateEditBaslangic;
        private SpinEdit _spinOdul;
        private ComboBoxEdit _cmbDurum;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private STurnuva _turnuvaService;
        private Turnuvalar _duzenlenecekTurnuva;
        private bool _duzenlemeModu;

        /// <summary>
        /// Initializes a new instance of the FrmTurnuvaKayit class for new tournament registration.
        /// </summary>
        public FrmTurnuvaKayit()
        {
            InitializeComponent();
            _turnuvaService = new STurnuva();
            _duzenlemeModu = false;
            this.Text = "Yeni Turnuva Kaydı";
            _dateEditBaslangic.DateTime = DateTime.Now;
            _cmbDurum.SelectedIndex = 0;
        }

        /// <summary>
        /// Initializes a new instance of the FrmTurnuvaKayit class for editing existing tournament.
        /// </summary>
        /// <param name="turnuva">The tournament to edit.</param>
        public FrmTurnuvaKayit(Turnuvalar turnuva) : this()
        {
            _duzenlenecekTurnuva = turnuva;
            _duzenlemeModu = true;
            this.Text = "Turnuva Düzenle";
            LoadTurnuvaBilgileri();
        }

        /// <summary>
        /// Loads tournament information for editing.
        /// </summary>
        private void LoadTurnuvaBilgileri()
        {
            if (_duzenlenecekTurnuva != null)
            {
                _txtTurnuvaAdi.Text = _duzenlenecekTurnuva.TurnuvaAdi;
                _dateEditBaslangic.DateTime = _duzenlenecekTurnuva.BaslangicTarihi;
                _spinOdul.Value = _duzenlenecekTurnuva.Odul;
                _cmbDurum.Text = _duzenlenecekTurnuva.Durum;
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtTurnuvaAdi.Text))
                {
                    XtraMessageBox.Show("Lütfen turnuva adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtTurnuvaAdi.Focus();
                    return;
                }

                if (_spinOdul.Value < 0)
                {
                    XtraMessageBox.Show("Lütfen geçerli bir ödül miktarı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _spinOdul.Focus();
                    return;
                }

                Turnuvalar turnuva = new Turnuvalar
                {
                    TurnuvaAdi = _txtTurnuvaAdi.Text.Trim(),
                    BaslangicTarihi = _dateEditBaslangic.DateTime,
                    Odul = _spinOdul.Value,
                    Durum = _cmbDurum.Text
                };

                bool result = false;
                if (_duzenlemeModu && _duzenlenecekTurnuva != null)
                {
                    turnuva.TurnuvaID = _duzenlenecekTurnuva.TurnuvaID;
                    result = _turnuvaService.Guncelle(turnuva);
                }
                else
                {
                    result = _turnuvaService.Olustur(turnuva);
                }

                if (result)
                {
                    XtraMessageBox.Show(_duzenlemeModu ? "Turnuva güncellendi!" : "Turnuva eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

