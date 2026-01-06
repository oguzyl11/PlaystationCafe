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
            this._gridControlTurnuvalar = new DevExpress.XtraGrid.GridControl();
            this._gridViewTurnuvalar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this._btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this._btnSil = new DevExpress.XtraEditors.SimpleButton();
            this._btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this._btnSonrakiTur = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._cmbTurnuvalar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlTurnuvalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewTurnuvalar)).BeginInit();
            this.SuspendLayout();
            // 
            // _scrollableControl
            // 
            this._scrollableControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scrollableControl.Location = new System.Drawing.Point(360, 50);
            this._scrollableControl.Name = "_scrollableControl";
            this._scrollableControl.Size = new System.Drawing.Size(400, 640);
            this._scrollableControl.TabIndex = 0;
            // 
            // _btnTurnuvayiBaslat
            // 
            this._btnTurnuvayiBaslat.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnTurnuvayiBaslat.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnTurnuvayiBaslat.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTurnuvayiBaslat.Appearance.Options.UseFont = true;
            this._btnTurnuvayiBaslat.Appearance.Options.UseBackColor = true;
            this._btnTurnuvayiBaslat.Appearance.Options.UseForeColor = true;
            this._btnTurnuvayiBaslat.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(0, 82, 184);
            this._btnTurnuvayiBaslat.AppearanceHovered.Options.UseBackColor = true;
            this._btnTurnuvayiBaslat.Location = new System.Drawing.Point(460, 10);
            this._btnTurnuvayiBaslat.Name = "_btnTurnuvayiBaslat";
            this._btnTurnuvayiBaslat.Size = new System.Drawing.Size(170, 40);
            this._btnTurnuvayiBaslat.TabIndex = 1;
            this._btnTurnuvayiBaslat.Text = "🚀 Turnuvayı Başlat";
            this._btnTurnuvayiBaslat.Click += new System.EventHandler(this.BtnTurnuvayiBaslat_Click);
            // 
            // _gridControlTurnuvalar
            // 
            this._gridControlTurnuvalar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._gridControlTurnuvalar.Location = new System.Drawing.Point(0, 50);
            this._gridControlTurnuvalar.MainView = this._gridViewTurnuvalar;
            this._gridControlTurnuvalar.Name = "_gridControlTurnuvalar";
            this._gridControlTurnuvalar.Size = new System.Drawing.Size(350, 640);
            this._gridControlTurnuvalar.TabIndex = 4;
            this._gridControlTurnuvalar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewTurnuvalar});
            // 
            // _gridViewTurnuvalar
            // 
            this._gridViewTurnuvalar.GridControl = this._gridControlTurnuvalar;
            this._gridViewTurnuvalar.Name = "_gridViewTurnuvalar";
            this._gridViewTurnuvalar.OptionsView.ShowGroupPanel = false;
            // 
            // _btnYeni
            // 
            this._btnYeni.Location = new System.Drawing.Point(20, 10);
            this._btnYeni.Name = "_btnYeni";
            this._btnYeni.Size = new System.Drawing.Size(100, 35);
            this._btnYeni.TabIndex = 5;
            this._btnYeni.Text = "Yeni";
            this._btnYeni.Click += new System.EventHandler(this.BtnYeni_Click);
            // 
            // _btnDuzenle
            // 
            this._btnDuzenle.Location = new System.Drawing.Point(130, 10);
            this._btnDuzenle.Name = "_btnDuzenle";
            this._btnDuzenle.Size = new System.Drawing.Size(100, 35);
            this._btnDuzenle.TabIndex = 6;
            this._btnDuzenle.Text = "Düzenle";
            this._btnDuzenle.Click += new System.EventHandler(this.BtnDuzenle_Click);
            // 
            // _btnSil
            // 
            this._btnSil.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnSil.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this._btnSil.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnSil.Appearance.Options.UseFont = true;
            this._btnSil.Appearance.Options.UseBackColor = true;
            this._btnSil.Appearance.Options.UseForeColor = true;
            this._btnSil.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(200, 35, 51);
            this._btnSil.AppearanceHovered.Options.UseBackColor = true;
            this._btnSil.Location = new System.Drawing.Point(240, 10);
            this._btnSil.Name = "_btnSil";
            this._btnSil.Size = new System.Drawing.Size(100, 40);
            this._btnSil.TabIndex = 7;
            this._btnSil.Text = "🗑️ Sil";
            this._btnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // _btnYenile
            // 
            this._btnYenile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnYenile.Appearance.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnYenile.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnYenile.Appearance.Options.UseFont = true;
            this._btnYenile.Appearance.Options.UseBackColor = true;
            this._btnYenile.Appearance.Options.UseForeColor = true;
            this._btnYenile.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(90, 99, 107);
            this._btnYenile.AppearanceHovered.Options.UseBackColor = true;
            this._btnYenile.Location = new System.Drawing.Point(350, 10);
            this._btnYenile.Name = "_btnYenile";
            this._btnYenile.Size = new System.Drawing.Size(100, 40);
            this._btnYenile.TabIndex = 8;
            this._btnYenile.Text = "🔄 Yenile";
            this._btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // _btnSonrakiTur
            // 
            this._btnSonrakiTur.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnSonrakiTur.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnSonrakiTur.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnSonrakiTur.Appearance.Options.UseFont = true;
            this._btnSonrakiTur.Appearance.Options.UseBackColor = true;
            this._btnSonrakiTur.Appearance.Options.UseForeColor = true;
            this._btnSonrakiTur.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(30, 150, 60);
            this._btnSonrakiTur.AppearanceHovered.Options.UseBackColor = true;
            this._btnSonrakiTur.Location = new System.Drawing.Point(640, 10);
            this._btnSonrakiTur.Name = "_btnSonrakiTur";
            this._btnSonrakiTur.Size = new System.Drawing.Size(120, 40);
            this._btnSonrakiTur.TabIndex = 9;
            this._btnSonrakiTur.Text = "➡️ Sonraki Tura Geç";
            this._btnSonrakiTur.Click += new System.EventHandler(this.BtnSonrakiTur_Click);
            // 
            // _cmbTurnuvalar
            // 
            this._cmbTurnuvalar.Location = new System.Drawing.Point(360, 50);
            this._cmbTurnuvalar.Name = "_cmbTurnuvalar";
            this._cmbTurnuvalar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbTurnuvalar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this._cmbTurnuvalar.Size = new System.Drawing.Size(200, 20);
            this._cmbTurnuvalar.TabIndex = 2;
            this._cmbTurnuvalar.SelectedIndexChanged += new System.EventHandler(this.CmbTurnuvalar_SelectedIndexChanged);
            // 
            // _lblTurnuvaSec
            // 
            this._lblTurnuvaSec.Location = new System.Drawing.Point(360, 30);
            this._lblTurnuvaSec.Name = "_lblTurnuvaSec";
            this._lblTurnuvaSec.Size = new System.Drawing.Size(124, 13);
            this._lblTurnuvaSec.TabIndex = 3;
            this._lblTurnuvaSec.Text = "Turnuva Seçiniz:";
            // 
            // FrmTurnuva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this._btnSonrakiTur);
            this.Controls.Add(this._btnYenile);
            this.Controls.Add(this._btnSil);
            this.Controls.Add(this._btnDuzenle);
            this.Controls.Add(this._btnYeni);
            this.Controls.Add(this._gridControlTurnuvalar);
            this.Controls.Add(this._lblTurnuvaSec);
            this.Controls.Add(this._cmbTurnuvalar);
            this.Controls.Add(this._btnTurnuvayiBaslat);
            this.Controls.Add(this._scrollableControl);
            this.Name = "FrmTurnuva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Turnuva Yönetimi - GameCenter AI";
            ((System.ComponentModel.ISupportInitialize)(this._cmbTurnuvalar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlTurnuvalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewTurnuvalar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

