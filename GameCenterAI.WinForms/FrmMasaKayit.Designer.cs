namespace GameCenterAI.WinForms
{
    partial class FrmMasaKayit
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
            this._txtMasaAdi = new DevExpress.XtraEditors.TextEdit();
            this._spinSaatlikUcret = new DevExpress.XtraEditors.SpinEdit();
            this._cmbDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._txtMasaAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinSaatlikUcret.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtMasaAdi
            // 
            this._txtMasaAdi.Location = new System.Drawing.Point(120, 20);
            this._txtMasaAdi.Name = "_txtMasaAdi";
            this._txtMasaAdi.Size = new System.Drawing.Size(250, 20);
            this._txtMasaAdi.TabIndex = 0;
            // 
            // _spinSaatlikUcret
            // 
            this._spinSaatlikUcret.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinSaatlikUcret.Location = new System.Drawing.Point(120, 50);
            this._spinSaatlikUcret.Name = "_spinSaatlikUcret";
            this._spinSaatlikUcret.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinSaatlikUcret.Properties.IsFloatValue = true;
            this._spinSaatlikUcret.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._spinSaatlikUcret.Size = new System.Drawing.Size(250, 20);
            this._spinSaatlikUcret.TabIndex = 1;
            // 
            // _cmbDurum
            // 
            this._cmbDurum.Location = new System.Drawing.Point(120, 80);
            this._cmbDurum.Name = "_cmbDurum";
            this._cmbDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbDurum.Properties.Items.AddRange(new object[] {
            "Boş",
            "Dolu"});
            this._cmbDurum.Size = new System.Drawing.Size(250, 20);
            this._cmbDurum.TabIndex = 2;
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Location = new System.Drawing.Point(214, 120);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 3;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Location = new System.Drawing.Point(295, 120);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 4;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Masa Adı:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Saatlik Ücret:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Durum:";
            // 
            // FrmMasaKayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(400, 180);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._cmbDurum);
            this.Controls.Add(this._spinSaatlikUcret);
            this.Controls.Add(this._txtMasaAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMasaKayit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Masa Kaydı";
            ((System.ComponentModel.ISupportInitialize)(this._txtMasaAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinSaatlikUcret.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}

