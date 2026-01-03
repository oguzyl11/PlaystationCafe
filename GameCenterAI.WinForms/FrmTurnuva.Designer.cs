namespace GameCenterAI.WinForms
{
    partial class FrmTurnuva
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
            this._scrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this._btnTurnuvayiBaslat = new DevExpress.XtraEditors.SimpleButton();
            this._cmbTurnuvalar = new DevExpress.XtraEditors.ComboBoxEdit();
            this._lblTurnuvaSec = new DevExpress.XtraEditors.LabelControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this._cmbTurnuvalar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // _scrollableControl
            // 
            this._scrollableControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scrollableControl.Location = new System.Drawing.Point(0, 150);
            this._scrollableControl.Name = "_scrollableControl";
            this._scrollableControl.Size = new System.Drawing.Size(1000, 600);
            this._scrollableControl.TabIndex = 0;
            // 
            // _btnTurnuvayiBaslat
            // 
            this._btnTurnuvayiBaslat.Location = new System.Drawing.Point(400, 100);
            this._btnTurnuvayiBaslat.Name = "_btnTurnuvayiBaslat";
            this._btnTurnuvayiBaslat.Size = new System.Drawing.Size(200, 40);
            this._btnTurnuvayiBaslat.TabIndex = 1;
            this._btnTurnuvayiBaslat.Text = "Turnuvayı Başlat";
            this._btnTurnuvayiBaslat.Click += new System.EventHandler(this.BtnTurnuvayiBaslat_Click);
            // 
            // _cmbTurnuvalar
            // 
            this._cmbTurnuvalar.Location = new System.Drawing.Point(150, 110);
            this._cmbTurnuvalar.Name = "_cmbTurnuvalar";
            this._cmbTurnuvalar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbTurnuvalar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this._cmbTurnuvalar.Size = new System.Drawing.Size(230, 20);
            this._cmbTurnuvalar.TabIndex = 2;
            this._cmbTurnuvalar.SelectedIndexChanged += new System.EventHandler(this.CmbTurnuvalar_SelectedIndexChanged);
            // 
            // _lblTurnuvaSec
            // 
            this._lblTurnuvaSec.Location = new System.Drawing.Point(20, 113);
            this._lblTurnuvaSec.Name = "_lblTurnuvaSec";
            this._lblTurnuvaSec.Size = new System.Drawing.Size(124, 13);
            this._lblTurnuvaSec.TabIndex = 3;
            this._lblTurnuvaSec.Text = "Turnuva Seçiniz:";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1000, 150);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Turnuva Yönetimi";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "İşlemler";
            // 
            // FrmTurnuva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this._lblTurnuvaSec);
            this.Controls.Add(this._cmbTurnuvalar);
            this.Controls.Add(this._btnTurnuvayiBaslat);
            this.Controls.Add(this._scrollableControl);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "FrmTurnuva";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Turnuva Yönetimi - GameCenter AI";
            ((System.ComponentModel.ISupportInitialize)(this._cmbTurnuvalar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    }
}

