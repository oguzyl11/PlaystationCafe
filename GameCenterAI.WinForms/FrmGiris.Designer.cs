namespace GameCenterAI.WinForms
{
    partial class FrmGiris
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
            this._grpGiris = new DevExpress.XtraEditors.GroupControl();
            this._txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this._txtSifre = new DevExpress.XtraEditors.TextEdit();
            this._btnGiris = new DevExpress.XtraEditors.SimpleButton();
            this._btnKayit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._grpGiris)).BeginInit();
            this._grpGiris.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _grpGiris
            // 
            this._grpGiris.Controls.Add(this._txtKullaniciAdi);
            this._grpGiris.Controls.Add(this._txtSifre);
            this._grpGiris.Controls.Add(this._btnGiris);
            this._grpGiris.Controls.Add(this._btnKayit);
            this._grpGiris.Location = new System.Drawing.Point(12, 12);
            this._grpGiris.Name = "_grpGiris";
            this._grpGiris.Size = new System.Drawing.Size(360, 200);
            this._grpGiris.TabIndex = 0;
            this._grpGiris.Text = "Giriş";
            // 
            // _txtKullaniciAdi
            // 
            this._txtKullaniciAdi.Location = new System.Drawing.Point(20, 40);
            this._txtKullaniciAdi.Name = "_txtKullaniciAdi";
            this._txtKullaniciAdi.Properties.NullValuePrompt = "Kullanıcı Adı";
            this._txtKullaniciAdi.Size = new System.Drawing.Size(320, 20);
            this._txtKullaniciAdi.TabIndex = 0;
            // 
            // _txtSifre
            // 
            this._txtSifre.Location = new System.Drawing.Point(20, 80);
            this._txtSifre.Name = "_txtSifre";
            this._txtSifre.Properties.NullValuePrompt = "Şifre";
            this._txtSifre.Properties.PasswordChar = '*';
            this._txtSifre.Size = new System.Drawing.Size(320, 20);
            this._txtSifre.TabIndex = 1;
            // 
            // _btnGiris
            // 
            this._btnGiris.Location = new System.Drawing.Point(20, 120);
            this._btnGiris.Name = "_btnGiris";
            this._btnGiris.Size = new System.Drawing.Size(150, 40);
            this._btnGiris.TabIndex = 2;
            this._btnGiris.Text = "Giriş Yap";
            this._btnGiris.Click += new System.EventHandler(this.BtnGiris_Click);
            // 
            // _btnKayit
            // 
            this._btnKayit.Location = new System.Drawing.Point(190, 120);
            this._btnKayit.Name = "_btnKayit";
            this._btnKayit.Size = new System.Drawing.Size(150, 40);
            this._btnKayit.TabIndex = 3;
            this._btnKayit.Text = "Kayıt Ol";
            this._btnKayit.Click += new System.EventHandler(this.BtnKayit_Click);
            // 
            // FrmGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(384, 224);
            this.Controls.Add(this._grpGiris);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Giriş";
            ((System.ComponentModel.ISupportInitialize)(this._grpGiris)).EndInit();
            this._grpGiris.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifre.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}

