namespace GameCenterAI.WinForms
{
    partial class FrmAyarlar
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
            this._tabControlAyarlar = new DevExpress.XtraTab.XtraTabControl();
            this._tabPageGenel = new DevExpress.XtraTab.XtraTabPage();
            this._tabPageVeritabani = new DevExpress.XtraTab.XtraTabPage();
            this._tabPageTarifeler = new DevExpress.XtraTab.XtraTabPage();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            
            // Genel Ayarlar
            this._lblKdvOrani = new DevExpress.XtraEditors.LabelControl();
            this._spinKdvOrani = new DevExpress.XtraEditors.SpinEdit();
            this._lblFaturaBaslik = new DevExpress.XtraEditors.LabelControl();
            this._txtFaturaBaslik = new DevExpress.XtraEditors.TextEdit();
            this._lblFaturaAltBilgi = new DevExpress.XtraEditors.LabelControl();
            this._txtFaturaAltBilgi = new DevExpress.XtraEditors.MemoEdit();
            
            // Veritabanı Ayarları
            this._lblServer = new DevExpress.XtraEditors.LabelControl();
            this._txtServer = new DevExpress.XtraEditors.TextEdit();
            this._lblDatabase = new DevExpress.XtraEditors.LabelControl();
            this._txtDatabase = new DevExpress.XtraEditors.TextEdit();
            this._btnTestBaglanti = new DevExpress.XtraEditors.SimpleButton();
            this._lblBaglantiDurumu = new DevExpress.XtraEditors.LabelControl();
            
            // Tarife Ayarları
            this._gridControlTarifeler = new DevExpress.XtraGrid.GridControl();
            this._gridViewTarifeler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnTarifeEkle = new DevExpress.XtraEditors.SimpleButton();
            this._btnTarifeDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this._btnTarifeSil = new DevExpress.XtraEditors.SimpleButton();
            
            ((System.ComponentModel.ISupportInitialize)(this._tabControlAyarlar)).BeginInit();
            this._tabControlAyarlar.SuspendLayout();
            this._tabPageGenel.SuspendLayout();
            this._tabPageVeritabani.SuspendLayout();
            this._tabPageTarifeler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._spinKdvOrani.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFaturaBaslik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFaturaAltBilgi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlTarifeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewTarifeler)).BeginInit();
            this.SuspendLayout();
            // 
            // _tabControlAyarlar
            // 
            this._tabControlAyarlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tabControlAyarlar.Location = new System.Drawing.Point(12, 12);
            this._tabControlAyarlar.Name = "_tabControlAyarlar";
            this._tabControlAyarlar.SelectedTabPage = this._tabPageGenel;
            this._tabControlAyarlar.Size = new System.Drawing.Size(900, 550);
            this._tabControlAyarlar.TabIndex = 0;
            this._tabControlAyarlar.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this._tabPageGenel,
            this._tabPageVeritabani,
            this._tabPageTarifeler});
            // 
            // _tabPageGenel
            // 
            this._tabPageGenel.Controls.Add(this._txtFaturaAltBilgi);
            this._tabPageGenel.Controls.Add(this._lblFaturaAltBilgi);
            this._tabPageGenel.Controls.Add(this._txtFaturaBaslik);
            this._tabPageGenel.Controls.Add(this._lblFaturaBaslik);
            this._tabPageGenel.Controls.Add(this._spinKdvOrani);
            this._tabPageGenel.Controls.Add(this._lblKdvOrani);
            this._tabPageGenel.Name = "_tabPageGenel";
            this._tabPageGenel.Size = new System.Drawing.Size(894, 522);
            this._tabPageGenel.Text = "⚙️ Genel Ayarlar";
            // 
            // _lblKdvOrani
            // 
            this._lblKdvOrani.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblKdvOrani.Appearance.Options.UseFont = true;
            this._lblKdvOrani.Location = new System.Drawing.Point(20, 20);
            this._lblKdvOrani.Name = "_lblKdvOrani";
            this._lblKdvOrani.Size = new System.Drawing.Size(60, 17);
            this._lblKdvOrani.TabIndex = 0;
            this._lblKdvOrani.Text = "KDV Oranı:";
            // 
            // _spinKdvOrani
            // 
            this._spinKdvOrani.EditValue = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._spinKdvOrani.Location = new System.Drawing.Point(120, 18);
            this._spinKdvOrani.Name = "_spinKdvOrani";
            this._spinKdvOrani.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinKdvOrani.Properties.IsFloatValue = false;
            this._spinKdvOrani.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._spinKdvOrani.Properties.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinKdvOrani.Size = new System.Drawing.Size(200, 20);
            this._spinKdvOrani.TabIndex = 1;
            // 
            // _lblFaturaBaslik
            // 
            this._lblFaturaBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblFaturaBaslik.Appearance.Options.UseFont = true;
            this._lblFaturaBaslik.Location = new System.Drawing.Point(20, 60);
            this._lblFaturaBaslik.Name = "_lblFaturaBaslik";
            this._lblFaturaBaslik.Size = new System.Drawing.Size(75, 17);
            this._lblFaturaBaslik.TabIndex = 2;
            this._lblFaturaBaslik.Text = "Fatura Başlık:";
            // 
            // _txtFaturaBaslik
            // 
            this._txtFaturaBaslik.Location = new System.Drawing.Point(120, 58);
            this._txtFaturaBaslik.Name = "_txtFaturaBaslik";
            this._txtFaturaBaslik.Size = new System.Drawing.Size(400, 20);
            this._txtFaturaBaslik.TabIndex = 3;
            // 
            // _lblFaturaAltBilgi
            // 
            this._lblFaturaAltBilgi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblFaturaAltBilgi.Appearance.Options.UseFont = true;
            this._lblFaturaAltBilgi.Location = new System.Drawing.Point(20, 100);
            this._lblFaturaAltBilgi.Name = "_lblFaturaAltBilgi";
            this._lblFaturaAltBilgi.Size = new System.Drawing.Size(80, 17);
            this._lblFaturaAltBilgi.TabIndex = 4;
            this._lblFaturaAltBilgi.Text = "Fatura Alt Bilgi:";
            // 
            // _txtFaturaAltBilgi
            // 
            this._txtFaturaAltBilgi.Location = new System.Drawing.Point(120, 98);
            this._txtFaturaAltBilgi.Name = "_txtFaturaAltBilgi";
            this._txtFaturaAltBilgi.Size = new System.Drawing.Size(400, 100);
            this._txtFaturaAltBilgi.TabIndex = 5;
            // 
            // _tabPageVeritabani
            // 
            this._tabPageVeritabani.Controls.Add(this._lblBaglantiDurumu);
            this._tabPageVeritabani.Controls.Add(this._btnTestBaglanti);
            this._tabPageVeritabani.Controls.Add(this._txtDatabase);
            this._tabPageVeritabani.Controls.Add(this._lblDatabase);
            this._tabPageVeritabani.Controls.Add(this._txtServer);
            this._tabPageVeritabani.Controls.Add(this._lblServer);
            this._tabPageVeritabani.Name = "_tabPageVeritabani";
            this._tabPageVeritabani.Size = new System.Drawing.Size(894, 522);
            this._tabPageVeritabani.Text = "🗄️ Veritabanı Ayarları";
            // 
            // _lblServer
            // 
            this._lblServer.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblServer.Appearance.Options.UseFont = true;
            this._lblServer.Location = new System.Drawing.Point(20, 20);
            this._lblServer.Name = "_lblServer";
            this._lblServer.Size = new System.Drawing.Size(40, 17);
            this._lblServer.TabIndex = 0;
            this._lblServer.Text = "Server:";
            // 
            // _txtServer
            // 
            this._txtServer.Location = new System.Drawing.Point(120, 18);
            this._txtServer.Name = "_txtServer";
            this._txtServer.Properties.ReadOnly = true;
            this._txtServer.Size = new System.Drawing.Size(300, 20);
            this._txtServer.TabIndex = 1;
            // 
            // _lblDatabase
            // 
            this._lblDatabase.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblDatabase.Appearance.Options.UseFont = true;
            this._lblDatabase.Location = new System.Drawing.Point(20, 60);
            this._lblDatabase.Name = "_lblDatabase";
            this._lblDatabase.Size = new System.Drawing.Size(60, 17);
            this._lblDatabase.TabIndex = 2;
            this._lblDatabase.Text = "Veritabanı:";
            // 
            // _txtDatabase
            // 
            this._txtDatabase.Location = new System.Drawing.Point(120, 58);
            this._txtDatabase.Name = "_txtDatabase";
            this._txtDatabase.Properties.ReadOnly = true;
            this._txtDatabase.Size = new System.Drawing.Size(300, 20);
            this._txtDatabase.TabIndex = 3;
            // 
            // _btnTestBaglanti
            // 
            this._btnTestBaglanti.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnTestBaglanti.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnTestBaglanti.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTestBaglanti.Appearance.Options.UseFont = true;
            this._btnTestBaglanti.Appearance.Options.UseBackColor = true;
            this._btnTestBaglanti.Appearance.Options.UseForeColor = true;
            this._btnTestBaglanti.Location = new System.Drawing.Point(120, 100);
            this._btnTestBaglanti.Name = "_btnTestBaglanti";
            this._btnTestBaglanti.Size = new System.Drawing.Size(150, 40);
            this._btnTestBaglanti.TabIndex = 4;
            this._btnTestBaglanti.Text = "🔌 Bağlantıyı Test Et";
            this._btnTestBaglanti.Click += new System.EventHandler(this.BtnTestBaglanti_Click);
            // 
            // _lblBaglantiDurumu
            // 
            this._lblBaglantiDurumu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblBaglantiDurumu.Appearance.Options.UseFont = true;
            this._lblBaglantiDurumu.Location = new System.Drawing.Point(280, 110);
            this._lblBaglantiDurumu.Name = "_lblBaglantiDurumu";
            this._lblBaglantiDurumu.Size = new System.Drawing.Size(0, 17);
            this._lblBaglantiDurumu.TabIndex = 5;
            // 
            // _tabPageTarifeler
            // 
            this._tabPageTarifeler.Controls.Add(this._btnTarifeSil);
            this._tabPageTarifeler.Controls.Add(this._btnTarifeDuzenle);
            this._tabPageTarifeler.Controls.Add(this._btnTarifeEkle);
            this._tabPageTarifeler.Controls.Add(this._gridControlTarifeler);
            this._tabPageTarifeler.Name = "_tabPageTarifeler";
            this._tabPageTarifeler.Size = new System.Drawing.Size(894, 522);
            this._tabPageTarifeler.Text = "💰 Tarife Ayarları";
            // 
            // _gridControlTarifeler
            // 
            this._gridControlTarifeler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlTarifeler.Location = new System.Drawing.Point(20, 60);
            this._gridControlTarifeler.MainView = this._gridViewTarifeler;
            this._gridControlTarifeler.Name = "_gridControlTarifeler";
            this._gridControlTarifeler.Size = new System.Drawing.Size(850, 440);
            this._gridControlTarifeler.TabIndex = 0;
            this._gridControlTarifeler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewTarifeler});
            // 
            // _gridViewTarifeler
            // 
            this._gridViewTarifeler.GridControl = this._gridControlTarifeler;
            this._gridViewTarifeler.Name = "_gridViewTarifeler";
            this._gridViewTarifeler.OptionsView.ShowGroupPanel = false;
            // 
            // _btnTarifeEkle
            // 
            this._btnTarifeEkle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnTarifeEkle.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnTarifeEkle.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTarifeEkle.Appearance.Options.UseFont = true;
            this._btnTarifeEkle.Appearance.Options.UseBackColor = true;
            this._btnTarifeEkle.Appearance.Options.UseForeColor = true;
            this._btnTarifeEkle.Location = new System.Drawing.Point(20, 12);
            this._btnTarifeEkle.Name = "_btnTarifeEkle";
            this._btnTarifeEkle.Size = new System.Drawing.Size(120, 40);
            this._btnTarifeEkle.TabIndex = 1;
            this._btnTarifeEkle.Text = "➕ Yeni Tarife";
            this._btnTarifeEkle.Click += new System.EventHandler(this.BtnTarifeEkle_Click);
            // 
            // _btnTarifeDuzenle
            // 
            this._btnTarifeDuzenle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnTarifeDuzenle.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this._btnTarifeDuzenle.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTarifeDuzenle.Appearance.Options.UseFont = true;
            this._btnTarifeDuzenle.Appearance.Options.UseBackColor = true;
            this._btnTarifeDuzenle.Appearance.Options.UseForeColor = true;
            this._btnTarifeDuzenle.Location = new System.Drawing.Point(150, 12);
            this._btnTarifeDuzenle.Name = "_btnTarifeDuzenle";
            this._btnTarifeDuzenle.Size = new System.Drawing.Size(120, 40);
            this._btnTarifeDuzenle.TabIndex = 2;
            this._btnTarifeDuzenle.Text = "✏️ Düzenle";
            this._btnTarifeDuzenle.Click += new System.EventHandler(this.BtnTarifeDuzenle_Click);
            // 
            // _btnTarifeSil
            // 
            this._btnTarifeSil.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnTarifeSil.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this._btnTarifeSil.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTarifeSil.Appearance.Options.UseFont = true;
            this._btnTarifeSil.Appearance.Options.UseBackColor = true;
            this._btnTarifeSil.Appearance.Options.UseForeColor = true;
            this._btnTarifeSil.Location = new System.Drawing.Point(280, 12);
            this._btnTarifeSil.Name = "_btnTarifeSil";
            this._btnTarifeSil.Size = new System.Drawing.Size(120, 40);
            this._btnTarifeSil.TabIndex = 3;
            this._btnTarifeSil.Text = "🗑️ Sil";
            this._btnTarifeSil.Click += new System.EventHandler(this.BtnTarifeSil_Click);
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnKaydet.Appearance.Options.UseFont = true;
            this._btnKaydet.Appearance.Options.UseBackColor = true;
            this._btnKaydet.Appearance.Options.UseForeColor = true;
            this._btnKaydet.Location = new System.Drawing.Point(756, 568);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 40);
            this._btnKaydet.TabIndex = 1;
            this._btnKaydet.Text = "💾 Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnIptal.Appearance.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnIptal.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnIptal.Appearance.Options.UseFont = true;
            this._btnIptal.Appearance.Options.UseBackColor = true;
            this._btnIptal.Appearance.Options.UseForeColor = true;
            this._btnIptal.Location = new System.Drawing.Point(837, 568);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 40);
            this._btnIptal.TabIndex = 2;
            this._btnIptal.Text = "❌ İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(924, 620);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._tabControlAyarlar);
            this.Name = "FrmAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Ayarlar";
            ((System.ComponentModel.ISupportInitialize)(this._tabControlAyarlar)).EndInit();
            this._tabControlAyarlar.ResumeLayout(false);
            this._tabPageGenel.ResumeLayout(false);
            this._tabPageGenel.PerformLayout();
            this._tabPageVeritabani.ResumeLayout(false);
            this._tabPageVeritabani.PerformLayout();
            this._tabPageTarifeler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._spinKdvOrani.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFaturaBaslik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFaturaAltBilgi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlTarifeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewTarifeler)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
