using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for selecting a member.
    /// </summary>
    public partial class FrmUyeSec : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlUyeler;
        private GridView _gridViewUyeler;
        private SimpleButton _btnSec;
        private SimpleButton _btnIptal;
        private SUyeler _uyeService;
        public Uyeler SecilenUye { get; private set; }

        /// <summary>
        /// Initializes a new instance of the FrmUyeSec class.
        /// </summary>
        public FrmUyeSec()
        {
            InitializeComponent();
            _uyeService = new SUyeler();
            LoadUyeler();
        }

        /// <summary>
        /// Loads all members into the grid.
        /// </summary>
        private void LoadUyeler()
        {
            try
            {
                List<Uyeler> uyeler = _uyeService.Listele();
                _gridControlUyeler.DataSource = uyeler;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Üyeler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the select button click event.
        /// </summary>
        private void BtnSec_Click(object sender, EventArgs e)
        {
            if (_gridViewUyeler.GetSelectedRows().Length > 0)
            {
                int uyeID = Convert.ToInt32(_gridViewUyeler.GetRowCellValue(_gridViewUyeler.GetSelectedRows()[0], "UyeID"));
                SecilenUye = _uyeService.Getir(uyeID);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir üye seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

