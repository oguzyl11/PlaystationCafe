namespace GameCenterAI.WinForms
{
    partial class FrmFaturalar
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
            this._gridControlFaturalar = new DevExpress.XtraGrid.GridControl();
            this._gridViewFaturalar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this._btnGoster = new DevExpress.XtraEditors.SimpleButton();
            this._btnYazdir = new DevExpress.XtraEditors.SimpleButton();
            this._btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this._dateEditBaslangic = new DevExpress.XtraEditors.DateEdit();
            this._dateEditBitis = new DevExpress.XtraEditors.DateEdit();
            this._lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this._lblBitis = new DevExpress.XtraEditors.LabelControl();
            this._btnFiltrele = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlFaturalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewFaturalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBitis.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlFaturalar
            // 
            this._gridControlFaturalar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlFaturalar.Location = new System.Drawing.Point(12, 80);
            this._gridControlFaturalar.MainView = this._gridViewFaturalar;
            this._gridControlFaturalar.Name = "_gridControlFaturalar";
            this._gridControlFaturalar.Size = new System.Drawing.Size(1000, 600);
            this._gridControlFaturalar.TabIndex = 0;
            this._gridControlFaturalar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewFaturalar});
            // 
            // _gridViewFaturalar
            // 
            this._gridViewFaturalar.GridControl = this._gridControlFaturalar;
            this._gridViewFaturalar.Name = "_gridViewFaturalar";
            this._gridViewFaturalar.OptionsView.ShowGroupPanel = false;
            // 
            // _btnYeni
            // 
            this._btnYeni.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnYeni.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnYeni.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnYeni.Appearance.Options.UseFont = true;
            this._btnYeni.Appearance.Options.UseBackColor = true;
            this._btnYeni.Appearance.Options.UseForeColor = true;
            this._btnYeni.Location = new System.Drawing.Point(12, 12);
            this._btnYeni.Name = "_btnYeni";
            this._btnYeni.Size = new System.Drawing.Size(100, 40);
            this._btnYeni.TabIndex = 1;
            this._btnYeni.Text = "➕ Yeni Fatura";
            this._btnYeni.Visible = false; // Manual creation disabled, invoices are auto-created
            // 
            // _btnGoster
            // 
            this._btnGoster.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnGoster.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnGoster.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnGoster.Appearance.Options.UseFont = true;
            this._btnGoster.Appearance.Options.UseBackColor = true;
            this._btnGoster.Appearance.Options.UseForeColor = true;
            this._btnGoster.Location = new System.Drawing.Point(12, 12);
            this._btnGoster.Name = "_btnGoster";
            this._btnGoster.Size = new System.Drawing.Size(120, 40);
            this._btnGoster.TabIndex = 2;
            this._btnGoster.Text = "👁️ Detay Göster";
            this._btnGoster.Click += new System.EventHandler(this.BtnGoster_Click);
            // 
            // _btnYazdir
            // 
            this._btnYazdir.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnYazdir.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this._btnYazdir.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnYazdir.Appearance.Options.UseFont = true;
            this._btnYazdir.Appearance.Options.UseBackColor = true;
            this._btnYazdir.Appearance.Options.UseForeColor = true;
            this._btnYazdir.Location = new System.Drawing.Point(138, 12);
            this._btnYazdir.Name = "_btnYazdir";
            this._btnYazdir.Size = new System.Drawing.Size(120, 40);
            this._btnYazdir.TabIndex = 3;
            this._btnYazdir.Text = "🖨️ Yazdır";
            this._btnYazdir.Click += new System.EventHandler(this.BtnYazdir_Click);
            // 
            // _btnYenile
            // 
            this._btnYenile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnYenile.Appearance.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnYenile.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnYenile.Appearance.Options.UseFont = true;
            this._btnYenile.Appearance.Options.UseBackColor = true;
            this._btnYenile.Appearance.Options.UseForeColor = true;
            this._btnYenile.Location = new System.Drawing.Point(264, 12);
            this._btnYenile.Name = "_btnYenile";
            this._btnYenile.Size = new System.Drawing.Size(100, 40);
            this._btnYenile.TabIndex = 4;
            this._btnYenile.Text = "🔄 Yenile";
            this._btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // _dateEditBaslangic
            // 
            this._dateEditBaslangic.EditValue = null;
            this._dateEditBaslangic.Location = new System.Drawing.Point(500, 20);
            this._dateEditBaslangic.Name = "_dateEditBaslangic";
            this._dateEditBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Size = new System.Drawing.Size(150, 20);
            this._dateEditBaslangic.TabIndex = 5;
            // 
            // _dateEditBitis
            // 
            this._dateEditBitis.EditValue = null;
            this._dateEditBitis.Location = new System.Drawing.Point(700, 20);
            this._dateEditBitis.Name = "_dateEditBitis";
            this._dateEditBitis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBitis.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBitis.Size = new System.Drawing.Size(150, 20);
            this._dateEditBitis.TabIndex = 6;
            // 
            // _lblBaslangic
            // 
            this._lblBaslangic.Location = new System.Drawing.Point(500, 5);
            this._lblBaslangic.Name = "_lblBaslangic";
            this._lblBaslangic.Size = new System.Drawing.Size(60, 13);
            this._lblBaslangic.TabIndex = 7;
            this._lblBaslangic.Text = "Başlangıç:";
            // 
            // _lblBitis
            // 
            this._lblBitis.Location = new System.Drawing.Point(700, 5);
            this._lblBitis.Name = "_lblBitis";
            this._lblBitis.Size = new System.Drawing.Size(33, 13);
            this._lblBitis.TabIndex = 8;
            this._lblBitis.Text = "Bitiş:";
            // 
            // _btnFiltrele
            // 
            this._btnFiltrele.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnFiltrele.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnFiltrele.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnFiltrele.Appearance.Options.UseFont = true;
            this._btnFiltrele.Appearance.Options.UseBackColor = true;
            this._btnFiltrele.Appearance.Options.UseForeColor = true;
            this._btnFiltrele.Location = new System.Drawing.Point(870, 12);
            this._btnFiltrele.Name = "_btnFiltrele";
            this._btnFiltrele.Size = new System.Drawing.Size(100, 40);
            this._btnFiltrele.TabIndex = 9;
            this._btnFiltrele.Text = "🔍 Filtrele";
            this._btnFiltrele.Click += new System.EventHandler(this.BtnFiltrele_Click);
            // 
            // FrmFaturalar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1024, 692);
            this.Controls.Add(this._btnFiltrele);
            this.Controls.Add(this._lblBitis);
            this.Controls.Add(this._lblBaslangic);
            this.Controls.Add(this._dateEditBitis);
            this.Controls.Add(this._dateEditBaslangic);
            this.Controls.Add(this._btnYenile);
            this.Controls.Add(this._btnYazdir);
            this.Controls.Add(this._btnGoster);
            this.Controls.Add(this._btnYeni);
            this.Controls.Add(this._gridControlFaturalar);
            this.Name = "FrmFaturalar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Faturalar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this._gridControlFaturalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewFaturalar)).EndInit();
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

