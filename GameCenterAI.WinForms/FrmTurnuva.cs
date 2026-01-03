using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for tournament management with dynamic bracket visualization.
    /// </summary>
    public partial class FrmTurnuva : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private XtraScrollableControl _scrollableControl;
        private SimpleButton _btnTurnuvayiBaslat;
        private ComboBoxEdit _cmbTurnuvalar;
        private LabelControl _lblTurnuvaSec;
        private STurnuva _turnuvaService;
        private SUyeler _uyeService;
        private List<KeyValuePair<int, int>> _eslestirmeler;
        private int _seciliTurnuvaID;

        /// <summary>
        /// Initializes a new instance of the FrmTurnuva class.
        /// </summary>
        public FrmTurnuva()
        {
            InitializeComponent();
            _turnuvaService = new STurnuva();
            _uyeService = new SUyeler();
            _eslestirmeler = new List<KeyValuePair<int, int>>();
            _seciliTurnuvaID = -1;
            LoadTurnuvalar();
        }

        /// <summary>
        /// Loads all tournaments into the combo box.
        /// </summary>
        private void LoadTurnuvalar()
        {
            try
            {
                var turnuvalar = _turnuvaService.Listele();
                _cmbTurnuvalar.Properties.Items.Clear();
                _cmbTurnuvalar.Properties.Items.Add("-- Turnuva Seçiniz --");

                foreach (var turnuva in turnuvalar)
                {
                    _cmbTurnuvalar.Properties.Items.Add($"{turnuva.TurnuvaAdi} (ID: {turnuva.TurnuvaID})");
                }

                _cmbTurnuvalar.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Turnuvalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the start tournament button click event.
        /// </summary>
        private void BtnTurnuvayiBaslat_Click(object sender, EventArgs e)
        {
            if (_cmbTurnuvalar.SelectedIndex <= 0)
            {
                XtraMessageBox.Show("Lütfen bir turnuva seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get selected tournament ID
                var turnuvalar = _turnuvaService.Listele();
                if (_cmbTurnuvalar.SelectedIndex > 0 && _cmbTurnuvalar.SelectedIndex <= turnuvalar.Count)
                {
                    _seciliTurnuvaID = turnuvalar[_cmbTurnuvalar.SelectedIndex - 1].TurnuvaID;
                }
                else
                {
                    XtraMessageBox.Show("Geçersiz turnuva seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create pairings
                _eslestirmeler = _turnuvaService.EslestirmeleriOlustur(_seciliTurnuvaID);

                if (_eslestirmeler.Count == 0)
                {
                    XtraMessageBox.Show("Yeterli üye bulunamadı. En az 2 üye gereklidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Visualize bracket
                BracketGoster();

                XtraMessageBox.Show($"Turnuva başlatıldı! {_eslestirmeler.Count} eşleşme oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Turnuva başlatma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Displays the tournament bracket dynamically.
        /// </summary>
        private void BracketGoster()
        {
            // Clear existing controls
            _scrollableControl.Controls.Clear();

            if (_eslestirmeler == null || _eslestirmeler.Count == 0)
            {
                return;
            }

            int yPosition = 20;
            int matchWidth = 300;
            int matchHeight = 100;
            int spacing = 20;

            // Create match controls for each pairing
            for (int i = 0; i < _eslestirmeler.Count; i++)
            {
                var eslestirme = _eslestirmeler[i];

                // Get user names
                Uyeler uye1 = _uyeService.Getir(eslestirme.Key);
                Uyeler uye2 = _uyeService.Getir(eslestirme.Value);

                string uye1Adi = uye1 != null ? uye1.AdSoyad : $"Üye ID: {eslestirme.Key}";
                string uye2Adi = uye2 != null ? uye2.AdSoyad : $"Üye ID: {eslestirme.Value}";

                // Create GroupControl for match
                GroupControl matchGroup = new GroupControl();
                matchGroup.Text = $"Maç {i + 1}";
                matchGroup.Location = new Point(20, yPosition);
                matchGroup.Size = new Size(matchWidth, matchHeight);

                // Create labels for players
                LabelControl lblUye1 = new LabelControl();
                lblUye1.Text = $"1. {uye1Adi}";
                lblUye1.Location = new Point(10, 30);
                lblUye1.Size = new Size(250, 20);
                lblUye1.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold);

                LabelControl lblUye2 = new LabelControl();
                lblUye2.Text = $"2. {uye2Adi}";
                lblUye2.Location = new Point(10, 55);
                lblUye2.Size = new Size(250, 20);
                lblUye2.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold);

                LabelControl lblVS = new LabelControl();
                lblVS.Text = "VS";
                lblVS.Location = new Point(10, 75);
                lblVS.Size = new Size(250, 20);
                lblVS.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Italic);
                lblVS.Appearance.ForeColor = Color.Red;

                matchGroup.Controls.Add(lblUye1);
                matchGroup.Controls.Add(lblUye2);
                matchGroup.Controls.Add(lblVS);

                _scrollableControl.Controls.Add(matchGroup);

                yPosition += matchHeight + spacing;
            }

            // Add connecting lines (visual representation)
            if (_eslestirmeler.Count > 1)
            {
                // Draw lines between matches (simplified - in production, use Graphics)
                LabelControl lblBracketInfo = new LabelControl();
                lblBracketInfo.Text = "Çeyrek Final Eşleşmeleri";
                lblBracketInfo.Location = new Point(20, yPosition);
                lblBracketInfo.Size = new Size(300, 30);
                lblBracketInfo.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Bold);
                lblBracketInfo.Appearance.ForeColor = Color.Blue;

                _scrollableControl.Controls.Add(lblBracketInfo);
            }
        }

        /// <summary>
        /// Handles tournament selection change event.
        /// </summary>
        private void CmbTurnuvalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            _scrollableControl.Controls.Clear();
            _eslestirmeler.Clear();
        }
    }
}

