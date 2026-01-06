namespace GameCenterAI.WinForms
{
    partial class FrmUyeler
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
            this._gridControlUyeler = new DevExpress.XtraGrid.GridControl();
            this._gridViewUyeler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this._btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this._btnSil = new DevExpress.XtraEditors.SimpleButton();
            this._btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this._txtArama = new DevExpress.XtraEditors.TextEdit();
            this._lblArama = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUyeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUyeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtArama.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlUyeler
            // 
            this._gridControlUyeler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlUyeler.Location = new System.Drawing.Point(12, 60);
            this._gridControlUyeler.MainView = this._gridViewUyeler;
            this._gridControlUyeler.Name = "_gridControlUyeler";
            this._gridControlUyeler.Size = new System.Drawing.Size(746, 630);
            this._gridControlUyeler.TabIndex = 0;
            this._gridControlUyeler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewUyeler});
            // 
            // _gridViewUyeler
            // 
            this._gridViewUyeler.GridControl = this._gridControlUyeler;
            this._gridViewUyeler.Name = "_gridViewUyeler";
            this._gridViewUyeler.OptionsView.ShowGroupPanel = false;
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
            this._btnYeni.Size = new System.Drawing.Size(120, 40);
            this._btnYeni.TabIndex = 1;
            this._btnYeni.Text = "➕ Yeni Üye";
            this._btnYeni.Click += new System.EventHandler(this.BtnYeni_Click);
            // 
            // _btnDuzenle
            // 
            this._btnDuzenle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnDuzenle.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this._btnDuzenle.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnDuzenle.Appearance.Options.UseFont = true;
            this._btnDuzenle.Appearance.Options.UseBackColor = true;
            this._btnDuzenle.Appearance.Options.UseForeColor = true;
            this._btnDuzenle.Location = new System.Drawing.Point(138, 12);
            this._btnDuzenle.Name = "_btnDuzenle";
            this._btnDuzenle.Size = new System.Drawing.Size(120, 40);
            this._btnDuzenle.TabIndex = 2;
            this._btnDuzenle.Text = "✏️ Düzenle";
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
            this._btnSil.Location = new System.Drawing.Point(264, 12);
            this._btnSil.Name = "_btnSil";
            this._btnSil.Size = new System.Drawing.Size(120, 40);
            this._btnSil.TabIndex = 3;
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
            this._btnYenile.Location = new System.Drawing.Point(390, 12);
            this._btnYenile.Name = "_btnYenile";
            this._btnYenile.Size = new System.Drawing.Size(120, 40);
            this._btnYenile.TabIndex = 4;
            this._btnYenile.Text = "🔄 Yenile";
            this._btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // _lblArama
            // 
            this._lblArama.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblArama.Appearance.Options.UseFont = true;
            this._lblArama.Location = new System.Drawing.Point(520, 25);
            this._lblArama.Name = "_lblArama";
            this._lblArama.Size = new System.Drawing.Size(38, 15);
            this._lblArama.TabIndex = 5;
            this._lblArama.Text = "🔍 Ara:";
            // 
            // _txtArama
            // 
            this._txtArama.Location = new System.Drawing.Point(560, 22);
            this._txtArama.Name = "_txtArama";
            this._txtArama.Properties.NullValuePrompt = "Ad, Soyad, Kullanıcı Adı veya Bakiye ara...";
            this._txtArama.Properties.NullValuePromptShowForEmptyValue = true;
            this._txtArama.Size = new System.Drawing.Size(200, 20);
            this._txtArama.TabIndex = 6;
            this._txtArama.TextChanged += new System.EventHandler(this.TxtArama_TextChanged);
            // 
            // FrmUyeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this._txtArama);
            this.Controls.Add(this._lblArama);
            this.Controls.Add(this._btnYenile);
            this.Controls.Add(this._btnSil);
            this.Controls.Add(this._btnDuzenle);
            this.Controls.Add(this._btnYeni);
            this.Controls.Add(this._gridControlUyeler);
            this.Name = "FrmUyeler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Üye Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUyeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUyeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtArama.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

