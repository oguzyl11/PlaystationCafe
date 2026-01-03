using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for generating and viewing reports (Raporlar).
    /// </summary>
    public partial class FrmRaporlar : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlRaporlar;
        private GridView _gridViewRaporlar;
        private SimpleButton _btnGunlukRapor;
        private SimpleButton _btnAylikRapor;
        private SimpleButton _btnUyeRaporu;
        private SimpleButton _btnMasaRaporu;
        private DateEdit _dateEditBaslangic;
        private DateEdit _dateEditBitis;
        private LabelControl _lblBaslangic;
        private LabelControl _lblBitis;
        private SRapor _raporService;

        /// <summary>
        /// Initializes a new instance of the FrmRaporlar class.
        /// </summary>
        public FrmRaporlar()
        {
            InitializeComponent();
            _raporService = new SRapor();
            _dateEditBaslangic.DateTime = DateTime.Now.Date;
            _dateEditBitis.DateTime = DateTime.Now.Date;
        }

        /// <summary>
        /// Handles the daily report button click event.
        /// </summary>
        private void BtnGunlukRapor_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = _raporService.GunlukRapor(_dateEditBaslangic.DateTime);
                _gridControlRaporlar.DataSource = dt;
                _gridViewRaporlar.PopulateColumns();
                this.Text = $"Günlük Rapor - {_dateEditBaslangic.DateTime:dd.MM.yyyy}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the monthly report button click event.
        /// </summary>
        private void BtnAylikRapor_Click(object sender, EventArgs e)
        {
            try
            {
                int yil = _dateEditBaslangic.DateTime.Year;
                int ay = _dateEditBaslangic.DateTime.Month;
                var dt = _raporService.AylikRapor(yil, ay);
                _gridControlRaporlar.DataSource = dt;
                _gridViewRaporlar.PopulateColumns();
                this.Text = $"Aylık Rapor - {ay:00}.{yil}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the member report button click event.
        /// </summary>
        private void BtnUyeRaporu_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = _raporService.UyeRaporu(_dateEditBaslangic.DateTime, _dateEditBitis.DateTime);
                _gridControlRaporlar.DataSource = dt;
                _gridViewRaporlar.PopulateColumns();
                this.Text = $"Üye Raporu - {_dateEditBaslangic.DateTime:dd.MM.yyyy} - {_dateEditBitis.DateTime:dd.MM.yyyy}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the table report button click event.
        /// </summary>
        private void BtnMasaRaporu_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = _raporService.MasaRaporu(_dateEditBaslangic.DateTime, _dateEditBitis.DateTime);
                _gridControlRaporlar.DataSource = dt;
                _gridViewRaporlar.PopulateColumns();
                this.Text = $"Masa Raporu - {_dateEditBaslangic.DateTime:dd.MM.yyyy} - {_dateEditBitis.DateTime:dd.MM.yyyy}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

