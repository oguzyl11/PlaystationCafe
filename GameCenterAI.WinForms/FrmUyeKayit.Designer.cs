namespace GameCenterAI.WinForms
{
    partial class FrmUyeKayit
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
            this._txtAdSoyad = new DevExpress.XtraEditors.TextEdit();
            this._txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this._txtSifre = new DevExpress.XtraEditors.TextEdit();
            this._txtSifreTekrar = new DevExpress.XtraEditors.TextEdit();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._txtAdSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifreTekrar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtAdSoyad
            // 
            this._txtAdSoyad.Location = new System.Drawing.Point(120, 20);
            this._txtAdSoyad.Name = "_txtAdSoyad";
            this._txtAdSoyad.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtAdSoyad.Properties.Appearance.Options.UseFont = true;
            this._txtAdSoyad.Properties.NullValuePrompt = "Ad Soyad giriniz...";
            this._txtAdSoyad.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtAdSoyad.Size = new System.Drawing.Size(290, 22);
            this._txtAdSoyad.TabIndex = 0;
            // 
            // _txtKullaniciAdi
            // 
            this._txtKullaniciAdi.Location = new System.Drawing.Point(120, 50);
            this._txtKullaniciAdi.Name = "_txtKullaniciAdi";
            this._txtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            this._txtKullaniciAdi.Properties.NullValuePrompt = "Kullanıcı adı giriniz...";
            this._txtKullaniciAdi.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtKullaniciAdi.Size = new System.Drawing.Size(290, 22);
            this._txtKullaniciAdi.TabIndex = 1;
            // 
            // _txtSifre
            // 
            this._txtSifre.Location = new System.Drawing.Point(120, 80);
            this._txtSifre.Name = "_txtSifre";
            this._txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtSifre.Properties.Appearance.Options.UseFont = true;
            this._txtSifre.Properties.NullValuePrompt = "Şifre giriniz...";
            this._txtSifre.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtSifre.Properties.PasswordChar = '●';
            this._txtSifre.Size = new System.Drawing.Size(290, 22);
            this._txtSifre.TabIndex = 2;
            // 
            // _txtSifreTekrar
            // 
            this._txtSifreTekrar.Location = new System.Drawing.Point(120, 110);
            this._txtSifreTekrar.Name = "_txtSifreTekrar";
            this._txtSifreTekrar.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtSifreTekrar.Properties.Appearance.Options.UseFont = true;
            this._txtSifreTekrar.Properties.NullValuePrompt = "Şifreyi tekrar giriniz...";
            this._txtSifreTekrar.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtSifreTekrar.Properties.PasswordChar = '●';
            this._txtSifreTekrar.Size = new System.Drawing.Size(290, 22);
            this._txtSifreTekrar.TabIndex = 3;
            this._txtSifreTekrar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSifreTekrar_KeyDown);
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnKaydet.Appearance.Options.UseFont = true;
            this._btnKaydet.Appearance.Options.UseBackColor = true;
            this._btnKaydet.Appearance.Options.UseForeColor = true;
            this._btnKaydet.Location = new System.Drawing.Point(200, 150);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(100, 40);
            this._btnKaydet.TabIndex = 4;
            this._btnKaydet.Text = "💾 Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnIptal.Appearance.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnIptal.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnIptal.Appearance.Options.UseFont = true;
            this._btnIptal.Appearance.Options.UseBackColor = true;
            this._btnIptal.Appearance.Options.UseForeColor = true;
            this._btnIptal.Location = new System.Drawing.Point(310, 150);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(100, 40);
            this._btnIptal.TabIndex = 5;
            this._btnIptal.Text = "❌ İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 15);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "👤 Ad Soyad:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(20, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 15);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "👤 Kullanıcı Adı:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(20, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 15);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "🔒 Şifre:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(20, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 15);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "🔒 Şifre Tekrar:";
            // 
            // FrmUyeKayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(430, 210);
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._txtSifreTekrar);
            this.Controls.Add(this._txtSifre);
            this.Controls.Add(this._txtKullaniciAdi);
            this.Controls.Add(this._txtAdSoyad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUyeKayit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Üye Kaydı";
            ((System.ComponentModel.ISupportInitialize)(this._txtAdSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifreTekrar.Properties)).EndInit();
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

