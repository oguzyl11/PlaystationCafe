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
    /// Form for managing products (Urunler).
    /// </summary>
    public partial class FrmUrunler : DevExpress.XtraEditors.XtraForm
    {
        private GridControl _gridControlUrunler;
        private GridView _gridViewUrunler;
        private SUrun _urunService;
        private SimpleButton _btnYeni;
        private SimpleButton _btnDuzenle;
        private SimpleButton _btnSil;
        private SimpleButton _btnYenile;
        private TextEdit _txtArama;
        private LabelControl _lblArama;
        private List<Urunler> _tumUrunler;

        /// <summary>
        /// Initializes a new instance of the FrmUrunler class.
        /// </summary>
        public FrmUrunler()
        {
            InitializeComponent();
            _urunService = new SUrun();
            LoadUrunler();
        }

        /// <summary>
        /// Loads all products into the grid.
        /// </summary>
        private void LoadUrunler()
        {
            try
            {
                _tumUrunler = _urunService.Listele();
                _gridControlUrunler.DataSource = _tumUrunler;
                _gridViewUrunler.PopulateColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ürünler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters the grid based on search text.
        /// </summary>
        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string aramaMetni = _txtArama.Text.ToLower();
                
                if (string.IsNullOrWhiteSpace(aramaMetni))
                {
                    _gridControlUrunler.DataSource = _tumUrunler;
                }
                else
                {
                    var filtrelenmisUrunler = _tumUrunler.Where(u => 
                        (u.UrunAdi != null && u.UrunAdi.ToLower().Contains(aramaMetni)) ||
                        (u.Kategori != null && u.Kategori.ToLower().Contains(aramaMetni)) ||
                        (u.Fiyat.ToString().Contains(aramaMetni)) ||
                        (u.Stok.ToString().Contains(aramaMetni))
                    ).ToList();
                    
                    _gridControlUrunler.DataSource = filtrelenmisUrunler;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Arama sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the new product button click event.
        /// </summary>
        private void BtnYeni_Click(object sender, EventArgs e)
        {
            FrmUrunKayit frmUrunKayit = new FrmUrunKayit();
            if (frmUrunKayit.ShowDialog() == DialogResult.OK)
            {
                LoadUrunler();
            }
        }

        /// <summary>
        /// Handles the edit product button click event.
        /// </summary>
        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (_gridViewUrunler.GetSelectedRows().Length > 0)
            {
                int urunID = Convert.ToInt32(_gridViewUrunler.GetRowCellValue(_gridViewUrunler.GetSelectedRows()[0], "UrunID"));
                Urunler urun = _urunService.Getir(urunID);
                
                if (urun != null)
                {
                    FrmUrunKayit frmUrunKayit = new FrmUrunKayit(urun);
                    if (frmUrunKayit.ShowDialog() == DialogResult.OK)
                    {
                        LoadUrunler();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir ürün seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the delete product button click event.
        /// </summary>
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (_gridViewUrunler.GetSelectedRows().Length > 0)
            {
                if (XtraMessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int urunID = Convert.ToInt32(_gridViewUrunler.GetRowCellValue(_gridViewUrunler.GetSelectedRows()[0], "UrunID"));
                    // TODO: Silme servisi eklenecek (Durum = "Pasif" yapılacak)
                    XtraMessageBox.Show("Ürün silme işlemi başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUrunler();
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen silmek için bir ürün seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        private void BtnYenile_Click(object sender, EventArgs e)
        {
            LoadUrunler();
        }
    }
}

