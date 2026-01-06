namespace GameCenterAI.WinForms
{
    partial class FrmRaporlar
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
            this._gridControlRaporlar = new DevExpress.XtraGrid.GridControl();
            this._gridViewRaporlar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnGunlukRapor = new DevExpress.XtraEditors.SimpleButton();
            this._btnAylikRapor = new DevExpress.XtraEditors.SimpleButton();
            this._btnUyeRaporu = new DevExpress.XtraEditors.SimpleButton();
            this._btnMasaRaporu = new DevExpress.XtraEditors.SimpleButton();
            this._dateEditBaslangic = new DevExpress.XtraEditors.DateEdit();
            this._dateEditBitis = new DevExpress.XtraEditors.DateEdit();
            this._lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this._lblBitis = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlRaporlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewRaporlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlRaporlar
            // 
            this._gridControlRaporlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlRaporlar.Location = new System.Drawing.Point(12, 80);
            this._gridControlRaporlar.MainView = this._gridViewRaporlar;
            this._gridControlRaporlar.Name = "_gridControlRaporlar";
            this._gridControlRaporlar.Size = new System.Drawing.Size(746, 610);
            this._gridControlRaporlar.TabIndex = 0;
            this._gridControlRaporlar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewRaporlar});
            // 
            // _gridViewRaporlar
            // 
            this._gridViewRaporlar.GridControl = this._gridControlRaporlar;
            this._gridViewRaporlar.Name = "_gridViewRaporlar";
            this._gridViewRaporlar.OptionsView.ShowGroupPanel = false;
            // 
            // _btnGunlukRapor
            // 
            this._btnGunlukRapor.Location = new System.Drawing.Point(12, 12);
            this._btnGunlukRapor.Name = "_btnGunlukRapor";
            this._btnGunlukRapor.Size = new System.Drawing.Size(120, 30);
            this._btnGunlukRapor.TabIndex = 1;
            this._btnGunlukRapor.Text = "Günlük Rapor";
            this._btnGunlukRapor.Click += new System.EventHandler(this.BtnGunlukRapor_Click);
            // 
            // _btnAylikRapor
            // 
            this._btnAylikRapor.Location = new System.Drawing.Point(138, 12);
            this._btnAylikRapor.Name = "_btnAylikRapor";
            this._btnAylikRapor.Size = new System.Drawing.Size(120, 30);
            this._btnAylikRapor.TabIndex = 2;
            this._btnAylikRapor.Text = "Aylık Rapor";
            this._btnAylikRapor.Click += new System.EventHandler(this.BtnAylikRapor_Click);
            // 
            // _btnUyeRaporu
            // 
            this._btnUyeRaporu.Location = new System.Drawing.Point(264, 12);
            this._btnUyeRaporu.Name = "_btnUyeRaporu";
            this._btnUyeRaporu.Size = new System.Drawing.Size(120, 30);
            this._btnUyeRaporu.TabIndex = 3;
            this._btnUyeRaporu.Text = "Üye Raporu";
            this._btnUyeRaporu.Click += new System.EventHandler(this.BtnUyeRaporu_Click);
            // 
            // _btnMasaRaporu
            // 
            this._btnMasaRaporu.Location = new System.Drawing.Point(390, 12);
            this._btnMasaRaporu.Name = "_btnMasaRaporu";
            this._btnMasaRaporu.Size = new System.Drawing.Size(120, 30);
            this._btnMasaRaporu.TabIndex = 4;
            this._btnMasaRaporu.Text = "Masa Raporu";
            this._btnMasaRaporu.Click += new System.EventHandler(this.BtnMasaRaporu_Click);
            // 
            // _lblBaslangic
            // 
            this._lblBaslangic.Location = new System.Drawing.Point(12, 55);
            this._lblBaslangic.Name = "_lblBaslangic";
            this._lblBaslangic.Size = new System.Drawing.Size(60, 13);
            this._lblBaslangic.TabIndex = 5;
            this._lblBaslangic.Text = "Başlangıç:";
            // 
            // _dateEditBaslangic
            // 
            this._dateEditBaslangic.EditValue = null;
            this._dateEditBaslangic.Location = new System.Drawing.Point(78, 52);
            this._dateEditBaslangic.Name = "_dateEditBaslangic";
            this._dateEditBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Size = new System.Drawing.Size(120, 20);
            this._dateEditBaslangic.TabIndex = 6;
            // 
            // _lblBitis
            // 
            this._lblBitis.Location = new System.Drawing.Point(220, 55);
            this._lblBitis.Name = "_lblBitis";
            this._lblBitis.Size = new System.Drawing.Size(30, 13);
            this._lblBitis.TabIndex = 7;
            this._lblBitis.Text = "Bitiş:";
            // 
            // _dateEditBitis
            // 
            this._dateEditBitis.EditValue = null;
            this._dateEditBitis.Location = new System.Drawing.Point(256, 52);
            this._dateEditBitis.Name = "_dateEditBitis";
            this._dateEditBitis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBitis.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBitis.Size = new System.Drawing.Size(120, 20);
            this._dateEditBitis.TabIndex = 8;
            // 
            // FrmRaporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this._dateEditBitis);
            this.Controls.Add(this._lblBitis);
            this.Controls.Add(this._dateEditBaslangic);
            this.Controls.Add(this._lblBaslangic);
            this.Controls.Add(this._btnMasaRaporu);
            this.Controls.Add(this._btnUyeRaporu);
            this.Controls.Add(this._btnAylikRapor);
            this.Controls.Add(this._btnGunlukRapor);
            this.Controls.Add(this._gridControlRaporlar);
            this.Name = "FrmRaporlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Raporlar";
            ((System.ComponentModel.ISupportInitialize)(this._gridControlRaporlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewRaporlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

