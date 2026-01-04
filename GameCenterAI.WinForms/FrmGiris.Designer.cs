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
            this._grpGiris.Location = new System.Drawing.Point(50, 50);
            this._grpGiris.Name = "_grpGiris";
            this._grpGiris.Size = new System.Drawing.Size(400, 300);
            this._grpGiris.TabIndex = 0;
            this._grpGiris.Text = "🎮 GameCenter AI - Giriş Yap";
            this._grpGiris.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this._grpGiris.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._grpGiris.AppearanceCaption.Options.UseFont = true;
            this._grpGiris.AppearanceCaption.Options.UseForeColor = true;
            this._grpGiris.AppearanceCaption.Options.UseTextOptions = true;
            this._grpGiris.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // _txtKullaniciAdi
            // 
            this._txtKullaniciAdi.Location = new System.Drawing.Point(50, 80);
            this._txtKullaniciAdi.Name = "_txtKullaniciAdi";
            this._txtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this._txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            this._txtKullaniciAdi.Properties.NullValuePrompt = "👤 Kullanıcı Adı";
            this._txtKullaniciAdi.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtKullaniciAdi.Properties.ShowNullValuePromptWhenFocused = true;
            this._txtKullaniciAdi.Size = new System.Drawing.Size(300, 30);
            this._txtKullaniciAdi.TabIndex = 0;
            // 
            // _txtSifre
            // 
            this._txtSifre.Location = new System.Drawing.Point(50, 130);
            this._txtSifre.Name = "_txtSifre";
            this._txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this._txtSifre.Properties.Appearance.Options.UseFont = true;
            this._txtSifre.Properties.NullValuePrompt = "🔒 Şifre";
            this._txtSifre.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtSifre.Properties.ShowNullValuePromptWhenFocused = true;
            this._txtSifre.Properties.PasswordChar = '●';
            this._txtSifre.Size = new System.Drawing.Size(300, 30);
            this._txtSifre.TabIndex = 1;
            // 
            // _btnGiris
            // 
            this._btnGiris.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this._btnGiris.Appearance.Options.UseFont = true;
            this._btnGiris.Location = new System.Drawing.Point(50, 190);
            this._btnGiris.Name = "_btnGiris";
            this._btnGiris.Size = new System.Drawing.Size(140, 45);
            this._btnGiris.TabIndex = 2;
            this._btnGiris.Text = "✅ Giriş Yap";
            this._btnGiris.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnGiris.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnGiris.Appearance.Options.UseBackColor = true;
            this._btnGiris.Appearance.Options.UseForeColor = true;
            this._btnGiris.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(0, 82, 184);
            this._btnGiris.AppearanceHovered.Options.UseBackColor = true;
            this._btnGiris.Click += new System.EventHandler(this.BtnGiris_Click);
            // 
            // _btnKayit
            // 
            this._btnKayit.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this._btnKayit.Appearance.Options.UseFont = true;
            this._btnKayit.Location = new System.Drawing.Point(210, 190);
            this._btnKayit.Name = "_btnKayit";
            this._btnKayit.Size = new System.Drawing.Size(140, 45);
            this._btnKayit.TabIndex = 3;
            this._btnKayit.Text = "📝 Kayıt Ol";
            this._btnKayit.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnKayit.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnKayit.Appearance.Options.UseBackColor = true;
            this._btnKayit.Appearance.Options.UseForeColor = true;
            this._btnKayit.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(30, 150, 60);
            this._btnKayit.AppearanceHovered.Options.UseBackColor = true;
            this._btnKayit.Click += new System.EventHandler(this.BtnKayit_Click);
            // 
            // FrmGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this._grpGiris);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🎮 GameCenter AI - Giriş";
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Appearance.Options.UseBackColor = true;
            ((System.ComponentModel.ISupportInitialize)(this._grpGiris)).EndInit();
            this._grpGiris.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSifre.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}

