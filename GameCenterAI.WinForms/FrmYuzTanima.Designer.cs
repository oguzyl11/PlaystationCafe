namespace GameCenterAI.WinForms
{
    partial class FrmYuzTanima
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
            this._pictureBoxKamera = new System.Windows.Forms.PictureBox();
            this._btnKamerayiAc = new DevExpress.XtraEditors.SimpleButton();
            this._btnYuzuKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnGirisYap = new DevExpress.XtraEditors.SimpleButton();
            this._btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this._lblDurum = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxKamera)).BeginInit();
            this.SuspendLayout();
            // 
            // _pictureBoxKamera
            // 
            this._pictureBoxKamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBoxKamera.Location = new System.Drawing.Point(12, 12);
            this._pictureBoxKamera.Name = "_pictureBoxKamera";
            this._pictureBoxKamera.Size = new System.Drawing.Size(640, 480);
            this._pictureBoxKamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBoxKamera.TabIndex = 0;
            this._pictureBoxKamera.TabStop = false;
            // 
            // _btnKamerayiAc
            // 
            this._btnKamerayiAc.Location = new System.Drawing.Point(12, 510);
            this._btnKamerayiAc.Name = "_btnKamerayiAc";
            this._btnKamerayiAc.Size = new System.Drawing.Size(150, 40);
            this._btnKamerayiAc.TabIndex = 1;
            this._btnKamerayiAc.Text = "Kamerayı Aç";
            this._btnKamerayiAc.Click += new System.EventHandler(this.BtnKamerayiAc_Click);
            // 
            // _btnYuzuKaydet
            // 
            this._btnYuzuKaydet.Location = new System.Drawing.Point(180, 510);
            this._btnYuzuKaydet.Name = "_btnYuzuKaydet";
            this._btnYuzuKaydet.Size = new System.Drawing.Size(150, 40);
            this._btnYuzuKaydet.TabIndex = 2;
            this._btnYuzuKaydet.Text = "Yüzü Kaydet";
            this._btnYuzuKaydet.Click += new System.EventHandler(this.BtnYuzuKaydet_Click);
            // 
            // _btnGirisYap
            // 
            this._btnGirisYap.Location = new System.Drawing.Point(348, 510);
            this._btnGirisYap.Name = "_btnGirisYap";
            this._btnGirisYap.Size = new System.Drawing.Size(150, 40);
            this._btnGirisYap.TabIndex = 3;
            this._btnGirisYap.Text = "Giriş Yap";
            this._btnGirisYap.Click += new System.EventHandler(this.BtnGirisYap_Click);
            // 
            // _btnKapat
            // 
            this._btnKapat.Location = new System.Drawing.Point(516, 510);
            this._btnKapat.Name = "_btnKapat";
            this._btnKapat.Size = new System.Drawing.Size(136, 40);
            this._btnKapat.TabIndex = 4;
            this._btnKapat.Text = "Kapat";
            this._btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            // 
            // _lblDurum
            // 
            this._lblDurum.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._lblDurum.Appearance.Options.UseFont = true;
            this._lblDurum.Location = new System.Drawing.Point(12, 560);
            this._lblDurum.Name = "_lblDurum";
            this._lblDurum.Size = new System.Drawing.Size(200, 16);
            this._lblDurum.TabIndex = 5;
            this._lblDurum.Text = "Kamera kapalı. Başlamak için kamerayı açın.";
            // 
            // FrmYuzTanima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(664, 590);
            this.Controls.Add(this._lblDurum);
            this.Controls.Add(this._btnKapat);
            this.Controls.Add(this._btnGirisYap);
            this.Controls.Add(this._btnYuzuKaydet);
            this.Controls.Add(this._btnKamerayiAc);
            this.Controls.Add(this._pictureBoxKamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmYuzTanima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yüz Tanıma - GameCenter AI";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxKamera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

