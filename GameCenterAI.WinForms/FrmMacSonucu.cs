using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for entering match results and scores.
    /// </summary>
    public partial class FrmMacSonucu : DevExpress.XtraEditors.XtraForm
    {
        private LabelControl _lblUye1;
        private LabelControl _lblUye2;
        private SpinEdit _spinSkor1;
        private SpinEdit _spinSkor2;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;
        private TurnuvaMaclari _mac;
        private STurnuva _turnuvaService;
        private SUyeler _uyeService;

        /// <summary>
        /// Initializes a new instance of the FrmMacSonucu class.
        /// </summary>
        /// <param name="mac">The match to enter results for.</param>
        public FrmMacSonucu(TurnuvaMaclari mac)
        {
            InitializeComponent();
            _mac = mac;
            _turnuvaService = new STurnuva();
            _uyeService = new SUyeler();
            LoadMacBilgileri();
        }

        /// <summary>
        /// Loads match information.
        /// </summary>
        private void LoadMacBilgileri()
        {
            if (_mac != null)
            {
                string hataUye1 = _uyeService.Getir(_mac.Uye1ID, out Uyeler uye1);
                string hataUye2 = _uyeService.Getir(_mac.Uye2ID, out Uyeler uye2);

                _lblUye1.Text = (hataUye1 == null && uye1 != null) ? uye1.AdSoyad : $"Üye ID: {_mac.Uye1ID}";
                _lblUye2.Text = (hataUye2 == null && uye2 != null) ? uye2.AdSoyad : $"Üye ID: {_mac.Uye2ID}";

                if (_mac.Skor1.HasValue)
                {
                    _spinSkor1.Value = _mac.Skor1.Value;
                }
                if (_mac.Skor2.HasValue)
                {
                    _spinSkor2.Value = _mac.Skor2.Value;
                }
            }
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                int skor1 = (int)_spinSkor1.Value;
                int skor2 = (int)_spinSkor2.Value;

                if (skor1 == skor2)
                {
                    XtraMessageBox.Show("Skorlar eşit olamaz! Beraberlik durumunda tekrar oynatın veya farklı skor girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _mac.Skor1 = skor1;
                _mac.Skor2 = skor2;
                _mac.Durum = "Sonuçlandı";
                _mac.MacTarihi = DateTime.Now;
                _mac.KazananID = skor1 > skor2 ? _mac.Uye1ID : _mac.Uye2ID;

                string hata = _turnuvaService.MacSonucuKaydet(_mac);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Maç sonucu kaydedilirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XtraMessageBox.Show("Maç sonucu kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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

