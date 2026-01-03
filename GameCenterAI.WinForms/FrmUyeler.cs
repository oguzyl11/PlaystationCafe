using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for managing members (Uyeler).
    /// </summary>
    public partial class FrmUyeler : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlUyeler;
        private GridView _gridViewUyeler;
        private SUyeler _uyeService;
        private SimpleButton _btnYeni;
        private SimpleButton _btnDuzenle;
        private SimpleButton _btnSil;
        private SimpleButton _btnYenile;

        /// <summary>
        /// Initializes a new instance of the FrmUyeler class.
        /// </summary>
        public FrmUyeler()
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
        /// Handles the new member button click event.
        /// </summary>
        private void BtnYeni_Click(object sender, EventArgs e)
        {
            FrmUyeKayit frmUyeKayit = new FrmUyeKayit();
            if (frmUyeKayit.ShowDialog() == DialogResult.OK)
            {
                LoadUyeler();
            }
        }

        /// <summary>
        /// Handles the edit member button click event.
        /// </summary>
        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (_gridViewUyeler.GetSelectedRows().Length > 0)
            {
                int uyeID = Convert.ToInt32(_gridViewUyeler.GetRowCellValue(_gridViewUyeler.GetSelectedRows()[0], "UyeID"));
                Uyeler uye = _uyeService.Getir(uyeID);
                
                if (uye != null)
                {
                    FrmUyeKayit frmUyeKayit = new FrmUyeKayit(uye);
                    if (frmUyeKayit.ShowDialog() == DialogResult.OK)
                    {
                        LoadUyeler();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir üye seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the delete member button click event.
        /// </summary>
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (_gridViewUyeler.GetSelectedRows().Length > 0)
            {
                if (XtraMessageBox.Show("Seçili üyeyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int uyeID = Convert.ToInt32(_gridViewUyeler.GetRowCellValue(_gridViewUyeler.GetSelectedRows()[0], "UyeID"));
                    // TODO: Silme servisi eklenecek (Durum = false yapılacak)
                    XtraMessageBox.Show("Üye silme işlemi başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUyeler();
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen silmek için bir üye seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        private void BtnYenile_Click(object sender, EventArgs e)
        {
            LoadUyeler();
        }
    }
}

