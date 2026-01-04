namespace GameCenterAI.WinForms
{
    partial class FrmTurnuvaKayit
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
            this._txtTurnuvaAdi = new DevExpress.XtraEditors.TextEdit();
            this._dateEditBaslangic = new DevExpress.XtraEditors.DateEdit();
            this._spinOdul = new DevExpress.XtraEditors.SpinEdit();
            this._cmbDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._txtTurnuvaAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinOdul.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtTurnuvaAdi
            // 
            this._txtTurnuvaAdi.Location = new System.Drawing.Point(120, 20);
            this._txtTurnuvaAdi.Name = "_txtTurnuvaAdi";
            this._txtTurnuvaAdi.Size = new System.Drawing.Size(250, 20);
            this._txtTurnuvaAdi.TabIndex = 0;
            // 
            // _dateEditBaslangic
            // 
            this._dateEditBaslangic.EditValue = null;
            this._dateEditBaslangic.Location = new System.Drawing.Point(120, 50);
            this._dateEditBaslangic.Name = "_dateEditBaslangic";
            this._dateEditBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._dateEditBaslangic.Size = new System.Drawing.Size(250, 20);
            this._dateEditBaslangic.TabIndex = 1;
            // 
            // _spinOdul
            // 
            this._spinOdul.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinOdul.Location = new System.Drawing.Point(120, 80);
            this._spinOdul.Name = "_spinOdul";
            this._spinOdul.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinOdul.Properties.IsFloatValue = true;
            this._spinOdul.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._spinOdul.Size = new System.Drawing.Size(250, 20);
            this._spinOdul.TabIndex = 2;
            // 
            // _cmbDurum
            // 
            this._cmbDurum.Location = new System.Drawing.Point(120, 110);
            this._cmbDurum.Name = "_cmbDurum";
            this._cmbDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbDurum.Properties.Items.AddRange(new object[] {
            "Planlanıyor",
            "Devam Ediyor",
            "Tamamlandı",
            "İptal Edildi"});
            this._cmbDurum.Size = new System.Drawing.Size(250, 20);
            this._cmbDurum.TabIndex = 3;
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Location = new System.Drawing.Point(214, 150);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 4;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Location = new System.Drawing.Point(295, 150);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 5;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Turnuva Adı:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Başlangıç Tarihi:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Ödül:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Durum:";
            // 
            // FrmTurnuvaKayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._cmbDurum);
            this.Controls.Add(this._spinOdul);
            this.Controls.Add(this._dateEditBaslangic);
            this.Controls.Add(this._txtTurnuvaAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTurnuvaKayit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Turnuva Kaydı";
            ((System.ComponentModel.ISupportInitialize)(this._txtTurnuvaAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dateEditBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinOdul.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}

