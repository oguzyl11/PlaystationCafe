namespace GameCenterAI.WinForms
{
    partial class FrmUrunKayit
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
            this._txtUrunAdi = new DevExpress.XtraEditors.TextEdit();
            this._cmbKategori = new DevExpress.XtraEditors.ComboBoxEdit();
            this._spinFiyat = new DevExpress.XtraEditors.SpinEdit();
            this._spinStok = new DevExpress.XtraEditors.SpinEdit();
            this._cmbDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._txtUrunAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbKategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinFiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinStok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtUrunAdi
            // 
            this._txtUrunAdi.Location = new System.Drawing.Point(120, 20);
            this._txtUrunAdi.Name = "_txtUrunAdi";
            this._txtUrunAdi.Size = new System.Drawing.Size(250, 20);
            this._txtUrunAdi.TabIndex = 0;
            // 
            // _cmbKategori
            // 
            this._cmbKategori.Location = new System.Drawing.Point(120, 50);
            this._cmbKategori.Name = "_cmbKategori";
            this._cmbKategori.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbKategori.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this._cmbKategori.Size = new System.Drawing.Size(250, 20);
            this._cmbKategori.TabIndex = 1;
            // 
            // _spinFiyat
            // 
            this._spinFiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinFiyat.Location = new System.Drawing.Point(120, 80);
            this._spinFiyat.Name = "_spinFiyat";
            this._spinFiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinFiyat.Properties.IsFloatValue = true;
            this._spinFiyat.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._spinFiyat.Size = new System.Drawing.Size(250, 20);
            this._spinFiyat.TabIndex = 2;
            // 
            // _spinStok
            // 
            this._spinStok.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinStok.Location = new System.Drawing.Point(120, 110);
            this._spinStok.Name = "_spinStok";
            this._spinStok.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinStok.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._spinStok.Size = new System.Drawing.Size(250, 20);
            this._spinStok.TabIndex = 3;
            // 
            // _cmbDurum
            // 
            this._cmbDurum.Location = new System.Drawing.Point(120, 140);
            this._cmbDurum.Name = "_cmbDurum";
            this._cmbDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbDurum.Properties.Items.AddRange(new object[] {
            "Aktif",
            "Pasif"});
            this._cmbDurum.Size = new System.Drawing.Size(250, 20);
            this._cmbDurum.TabIndex = 4;
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Location = new System.Drawing.Point(214, 180);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 5;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Location = new System.Drawing.Point(295, 180);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 6;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Ürün Adı:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Kategori:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Fiyat:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(25, 13);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "Stok:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 143);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(35, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Durum:";
            // 
            // FrmUrunKayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(400, 230);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._cmbDurum);
            this.Controls.Add(this._spinStok);
            this.Controls.Add(this._spinFiyat);
            this.Controls.Add(this._cmbKategori);
            this.Controls.Add(this._txtUrunAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUrunKayit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Ürün Kaydı";
            ((System.ComponentModel.ISupportInitialize)(this._txtUrunAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbKategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinFiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinStok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbDurum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}

