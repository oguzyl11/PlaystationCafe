using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for application settings (Ayarlar).
    /// </summary>
    public partial class FrmAyarlar : DevExpress.XtraEditors.XtraForm
    {
        private XtraTabControl _tabControlAyarlar;
        private XtraTabPage _tabPageGenel;
        private XtraTabPage _tabPageVeritabani;
        private XtraTabPage _tabPageTarifeler;
        private SimpleButton _btnKaydet;
        private SimpleButton _btnIptal;

        /// <summary>
        /// Initializes a new instance of the FrmAyarlar class.
        /// </summary>
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the save button click event.
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // TODO: Ayarlar kaydedilecek
            XtraMessageBox.Show("Ayarlar kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        /// <summary>
        /// Handles the cancel button click event.
        /// </summary>
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

