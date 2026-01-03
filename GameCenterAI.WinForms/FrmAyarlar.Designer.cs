namespace GameCenterAI.WinForms
{
    partial class FrmAyarlar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tabControlAyarlar = new DevExpress.XtraTab.XtraTabControl();
            this._tabPageGenel = new DevExpress.XtraTab.XtraTabPage();
            this._tabPageVeritabani = new DevExpress.XtraTab.XtraTabPage();
            this._tabPageTarifeler = new DevExpress.XtraTab.XtraTabPage();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._tabControlAyarlar)).BeginInit();
            this._tabControlAyarlar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControlAyarlar
            // 
            this._tabControlAyarlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tabControlAyarlar.Location = new System.Drawing.Point(12, 12);
            this._tabControlAyarlar.Name = "_tabControlAyarlar";
            this._tabControlAyarlar.SelectedTabPage = this._tabPageGenel;
            this._tabControlAyarlar.Size = new System.Drawing.Size(700, 450);
            this._tabControlAyarlar.TabIndex = 0;
            this._tabControlAyarlar.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this._tabPageGenel,
            this._tabPageVeritabani,
            this._tabPageTarifeler});
            // 
            // _tabPageGenel
            // 
            this._tabPageGenel.Name = "_tabPageGenel";
            this._tabPageGenel.Size = new System.Drawing.Size(694, 422);
            this._tabPageGenel.Text = "Genel Ayarlar";
            // 
            // _tabPageVeritabani
            // 
            this._tabPageVeritabani.Name = "_tabPageVeritabani";
            this._tabPageVeritabani.Size = new System.Drawing.Size(694, 422);
            this._tabPageVeritabani.Text = "Veritabanı Ayarları";
            // 
            // _tabPageTarifeler
            // 
            this._tabPageTarifeler.Name = "_tabPageTarifeler";
            this._tabPageTarifeler.Size = new System.Drawing.Size(694, 422);
            this._tabPageTarifeler.Text = "Tarife Ayarları";
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnKaydet.Location = new System.Drawing.Point(556, 468);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 1;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnIptal.Location = new System.Drawing.Point(637, 468);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 2;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(724, 510);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._tabControlAyarlar);
            this.Name = "FrmAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Ayarlar";
            ((System.ComponentModel.ISupportInitialize)(this._tabControlAyarlar)).EndInit();
            this._tabControlAyarlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

