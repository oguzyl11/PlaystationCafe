namespace GameCenterAI.WinForms
{
    partial class FrmMasalar
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
            this._tileControlMasalar = new DevExpress.XtraEditors.TileControl();
            this._tileGroupMasalar = new DevExpress.XtraEditors.TileGroup();
            this._grpMasaDetay = new DevExpress.XtraEditors.GroupControl();
            this._lblToplam = new DevExpress.XtraEditors.LabelControl();
            this._btnTarifeDegistir = new DevExpress.XtraEditors.SimpleButton();
            this._btnMasaEkle = new DevExpress.XtraEditors.SimpleButton();
            this._btnMasaSil = new DevExpress.XtraEditors.SimpleButton();
            this._btnSiparisEkle = new DevExpress.XtraEditors.SimpleButton();
            this._btnOdemeAl = new DevExpress.XtraEditors.SimpleButton();
            this._btnMasaAcKapat = new DevExpress.XtraEditors.SimpleButton();
            this._txtPesinAlinan = new DevExpress.XtraEditors.TextEdit();
            this._txtSiparisToplami = new DevExpress.XtraEditors.TextEdit();
            this._txtKullanimUcreti = new DevExpress.XtraEditors.TextEdit();
            this._txtKalanSure = new DevExpress.XtraEditors.TextEdit();
            this._txtSureSiniri = new DevExpress.XtraEditors.TextEdit();
            this._txtTarife = new DevExpress.XtraEditors.TextEdit();
            this._txtGecenSure = new DevExpress.XtraEditors.TextEdit();
            this._txtBaslamaSaati = new DevExpress.XtraEditors.TextEdit();
            this._txtMusteri = new DevExpress.XtraEditors.TextEdit();
            this._lblMasaAdi = new DevExpress.XtraEditors.LabelControl();
            this._cmbTarifeler = new DevExpress.XtraEditors.ComboBoxEdit();
            this._gridControlSiparisDetay = new DevExpress.XtraGrid.GridControl();
            this._gridViewSiparisDetay = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this._grpMasaDetay)).BeginInit();
            this._grpMasaDetay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtPesinAlinan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSiparisToplami.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKullanimUcreti.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKalanSure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSureSiniri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtTarife.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtGecenSure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtBaslamaSaati.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtMusteri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbTarifeler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisDetay)).BeginInit();
            this.SuspendLayout();
            // 
            // _tileControlMasalar
            // 
            this._tileControlMasalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tileControlMasalar.Groups.Add(this._tileGroupMasalar);
            this._tileControlMasalar.Location = new System.Drawing.Point(300, 0);
            this._tileControlMasalar.Name = "_tileControlMasalar";
            this._tileControlMasalar.Size = new System.Drawing.Size(700, 800);
            this._tileControlMasalar.TabIndex = 1;
            this._tileControlMasalar.Text = "tileControl1";
            this._tileControlMasalar.ItemSize = 150;
            // 
            // _tileGroupMasalar
            // 
            this._tileGroupMasalar.Name = "_tileGroupMasalar";
            this._tileGroupMasalar.Text = "Masalar";
            // 
            // _grpMasaDetay
            // 
            this._grpMasaDetay.Controls.Add(this._gridControlSiparisDetay);
            this._grpMasaDetay.Controls.Add(this._cmbTarifeler);
            this._grpMasaDetay.Controls.Add(this._lblToplam);
            this._grpMasaDetay.Controls.Add(this._btnOdemeAl);
            this._grpMasaDetay.Controls.Add(this._btnTarifeDegistir);
            this._grpMasaDetay.Controls.Add(this._btnSiparisEkle);
            this._grpMasaDetay.Controls.Add(this._btnMasaAcKapat);
            this._grpMasaDetay.Controls.Add(this._txtPesinAlinan);
            this._grpMasaDetay.Controls.Add(this._txtSiparisToplami);
            this._grpMasaDetay.Controls.Add(this._txtKullanimUcreti);
            this._grpMasaDetay.Controls.Add(this._txtKalanSure);
            this._grpMasaDetay.Controls.Add(this._txtSureSiniri);
            this._grpMasaDetay.Controls.Add(this._txtTarife);
            this._grpMasaDetay.Controls.Add(this._txtGecenSure);
            this._grpMasaDetay.Controls.Add(this._txtBaslamaSaati);
            this._grpMasaDetay.Controls.Add(this._txtMusteri);
            this._grpMasaDetay.Controls.Add(this._lblMasaAdi);
            this._grpMasaDetay.Controls.Add(this._btnMasaEkle);
            this._grpMasaDetay.Controls.Add(this._btnMasaSil);
            this._grpMasaDetay.Dock = System.Windows.Forms.DockStyle.Left;
            this._grpMasaDetay.Location = new System.Drawing.Point(0, 0);
            this._grpMasaDetay.Name = "_grpMasaDetay";
            this._grpMasaDetay.Size = new System.Drawing.Size(300, 800);
            this._grpMasaDetay.TabIndex = 0;
            this._grpMasaDetay.Text = "Masa Yönetimi";
            this._grpMasaDetay.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._grpMasaDetay.AppearanceCaption.Options.UseFont = true;
            // 
            // _lblMasaAdi
            // 
            this._lblMasaAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this._lblMasaAdi.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._lblMasaAdi.Appearance.Options.UseFont = true;
            this._lblMasaAdi.Appearance.Options.UseForeColor = true;
            this._lblMasaAdi.Location = new System.Drawing.Point(20, 70);
            this._lblMasaAdi.Name = "_lblMasaAdi";
            this._lblMasaAdi.Size = new System.Drawing.Size(100, 27);
            this._lblMasaAdi.TabIndex = 0;
            this._lblMasaAdi.Text = "MASA_01";
            // 
            // _txtMusteri
            // 
            this._txtMusteri.Location = new System.Drawing.Point(20, 110);
            this._txtMusteri.Name = "_txtMusteri";
            this._txtMusteri.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtMusteri.Properties.Appearance.Options.UseFont = true;
            this._txtMusteri.Properties.ReadOnly = true;
            this._txtMusteri.Size = new System.Drawing.Size(260, 20);
            this._txtMusteri.TabIndex = 1;
            // 
            // _txtBaslamaSaati
            // 
            this._txtBaslamaSaati.Location = new System.Drawing.Point(20, 150);
            this._txtBaslamaSaati.Name = "_txtBaslamaSaati";
            this._txtBaslamaSaati.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtBaslamaSaati.Properties.Appearance.Options.UseFont = true;
            this._txtBaslamaSaati.Properties.ReadOnly = true;
            this._txtBaslamaSaati.Size = new System.Drawing.Size(260, 20);
            this._txtBaslamaSaati.TabIndex = 2;
            // 
            // _txtGecenSure
            // 
            this._txtGecenSure.Location = new System.Drawing.Point(20, 170);
            this._txtGecenSure.Name = "_txtGecenSure";
            this._txtGecenSure.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._txtGecenSure.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._txtGecenSure.Properties.Appearance.Options.UseFont = true;
            this._txtGecenSure.Properties.Appearance.Options.UseForeColor = true;
            this._txtGecenSure.Properties.ReadOnly = true;
            this._txtGecenSure.Size = new System.Drawing.Size(260, 20);
            this._txtGecenSure.TabIndex = 3;
            // 
            // _txtTarife
            // 
            this._txtTarife.Location = new System.Drawing.Point(20, 210);
            this._txtTarife.Name = "_txtTarife";
            this._txtTarife.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtTarife.Properties.Appearance.Options.UseFont = true;
            this._txtTarife.Properties.ReadOnly = true;
            this._txtTarife.Size = new System.Drawing.Size(260, 20);
            this._txtTarife.TabIndex = 4;
            // 
            // _txtSureSiniri
            // 
            this._txtSureSiniri.Location = new System.Drawing.Point(20, 250);
            this._txtSureSiniri.Name = "_txtSureSiniri";
            this._txtSureSiniri.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtSureSiniri.Properties.Appearance.Options.UseFont = true;
            this._txtSureSiniri.Properties.ReadOnly = true;
            this._txtSureSiniri.Size = new System.Drawing.Size(260, 20);
            this._txtSureSiniri.TabIndex = 5;
            // 
            // _txtKalanSure
            // 
            this._txtKalanSure.Location = new System.Drawing.Point(20, 290);
            this._txtKalanSure.Name = "_txtKalanSure";
            this._txtKalanSure.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._txtKalanSure.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this._txtKalanSure.Properties.Appearance.Options.UseFont = true;
            this._txtKalanSure.Properties.Appearance.Options.UseForeColor = true;
            this._txtKalanSure.Properties.ReadOnly = true;
            this._txtKalanSure.Size = new System.Drawing.Size(260, 20);
            this._txtKalanSure.TabIndex = 6;
            // 
            // _txtKullanimUcreti
            // 
            this._txtKullanimUcreti.Location = new System.Drawing.Point(20, 330);
            this._txtKullanimUcreti.Name = "_txtKullanimUcreti";
            this._txtKullanimUcreti.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._txtKullanimUcreti.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._txtKullanimUcreti.Properties.Appearance.Options.UseFont = true;
            this._txtKullanimUcreti.Properties.Appearance.Options.UseForeColor = true;
            this._txtKullanimUcreti.Properties.ReadOnly = true;
            this._txtKullanimUcreti.Size = new System.Drawing.Size(260, 20);
            this._txtKullanimUcreti.TabIndex = 7;
            // 
            // _txtSiparisToplami
            // 
            this._txtSiparisToplami.Location = new System.Drawing.Point(20, 370);
            this._txtSiparisToplami.Name = "_txtSiparisToplami";
            this._txtSiparisToplami.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtSiparisToplami.Properties.Appearance.Options.UseFont = true;
            this._txtSiparisToplami.Properties.ReadOnly = true;
            this._txtSiparisToplami.Size = new System.Drawing.Size(260, 20);
            this._txtSiparisToplami.TabIndex = 8;
            // 
            // _txtPesinAlinan
            // 
            this._txtPesinAlinan.Location = new System.Drawing.Point(20, 410);
            this._txtPesinAlinan.Name = "_txtPesinAlinan";
            this._txtPesinAlinan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._txtPesinAlinan.Properties.Appearance.Options.UseFont = true;
            this._txtPesinAlinan.Properties.ReadOnly = true;
            this._txtPesinAlinan.Size = new System.Drawing.Size(260, 20);
            this._txtPesinAlinan.TabIndex = 9;
            // 
            // _btnMasaAcKapat
            // 
            this._btnMasaAcKapat.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this._btnMasaAcKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._btnMasaAcKapat.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnMasaAcKapat.Appearance.Options.UseBackColor = true;
            this._btnMasaAcKapat.Appearance.Options.UseFont = true;
            this._btnMasaAcKapat.Appearance.Options.UseForeColor = true;
            this._btnMasaAcKapat.Location = new System.Drawing.Point(20, 480);
            this._btnMasaAcKapat.Name = "_btnMasaAcKapat";
            this._btnMasaAcKapat.Size = new System.Drawing.Size(120, 45);
            this._btnMasaAcKapat.TabIndex = 10;
            this._btnMasaAcKapat.Text = "▶ Masa Aç";
            this._btnMasaAcKapat.Click += new System.EventHandler(this.BtnMasaAcKapat_Click);
            // 
            // _btnSiparisEkle
            // 
            this._btnSiparisEkle.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this._btnSiparisEkle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._btnSiparisEkle.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnSiparisEkle.Appearance.Options.UseBackColor = true;
            this._btnSiparisEkle.Appearance.Options.UseFont = true;
            this._btnSiparisEkle.Appearance.Options.UseForeColor = true;
            this._btnSiparisEkle.Location = new System.Drawing.Point(20, 535);
            this._btnSiparisEkle.Name = "_btnSiparisEkle";
            this._btnSiparisEkle.Size = new System.Drawing.Size(120, 40);
            this._btnSiparisEkle.TabIndex = 12;
            this._btnSiparisEkle.Text = "➕ Sipariş Ekle";
            this._btnSiparisEkle.Click += new System.EventHandler(this.BtnSiparisEkle_Click);
            // 
            // _btnTarifeDegistir
            // 
            this._btnTarifeDegistir.Appearance.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnTarifeDegistir.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._btnTarifeDegistir.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnTarifeDegistir.Appearance.Options.UseBackColor = true;
            this._btnTarifeDegistir.Appearance.Options.UseFont = true;
            this._btnTarifeDegistir.Appearance.Options.UseForeColor = true;
            this._btnTarifeDegistir.Location = new System.Drawing.Point(160, 535);
            this._btnTarifeDegistir.Name = "_btnTarifeDegistir";
            this._btnTarifeDegistir.Size = new System.Drawing.Size(120, 40);
            this._btnTarifeDegistir.TabIndex = 13;
            this._btnTarifeDegistir.Text = "🔄 Tarife Değiştir";
            this._btnTarifeDegistir.Click += new System.EventHandler(this.BtnTarifeDegistir_Click);
            // 
            // _btnOdemeAl
            // 
            this._btnOdemeAl.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnOdemeAl.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._btnOdemeAl.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnOdemeAl.Appearance.Options.UseBackColor = true;
            this._btnOdemeAl.Appearance.Options.UseFont = true;
            this._btnOdemeAl.Appearance.Options.UseForeColor = true;
            this._btnOdemeAl.Location = new System.Drawing.Point(160, 480);
            this._btnOdemeAl.Name = "_btnOdemeAl";
            this._btnOdemeAl.Size = new System.Drawing.Size(120, 45);
            this._btnOdemeAl.TabIndex = 16;
            this._btnOdemeAl.Text = "💰 Ödeme Al";
            this._btnOdemeAl.Enabled = false;
            this._btnOdemeAl.Click += new System.EventHandler(this.BtnOdemeAl_Click);
            // 
            // _btnMasaEkle
            // 
            this._btnMasaEkle.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this._btnMasaEkle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._btnMasaEkle.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnMasaEkle.Appearance.Options.UseBackColor = true;
            this._btnMasaEkle.Appearance.Options.UseFont = true;
            this._btnMasaEkle.Appearance.Options.UseForeColor = true;
            this._btnMasaEkle.Location = new System.Drawing.Point(20, 25);
            this._btnMasaEkle.Name = "_btnMasaEkle";
            this._btnMasaEkle.Size = new System.Drawing.Size(120, 35);
            this._btnMasaEkle.TabIndex = 18;
            this._btnMasaEkle.Text = "+ Masa Ekle";
            this._btnMasaEkle.Click += new System.EventHandler(this.BtnMasaEkle_Click);
            // 
            // _btnMasaSil
            // 
            this._btnMasaSil.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this._btnMasaSil.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._btnMasaSil.Appearance.ForeColor = System.Drawing.Color.White;
            this._btnMasaSil.Appearance.Options.UseBackColor = true;
            this._btnMasaSil.Appearance.Options.UseFont = true;
            this._btnMasaSil.Appearance.Options.UseForeColor = true;
            this._btnMasaSil.Location = new System.Drawing.Point(160, 25);
            this._btnMasaSil.Name = "_btnMasaSil";
            this._btnMasaSil.Size = new System.Drawing.Size(120, 35);
            this._btnMasaSil.TabIndex = 19;
            this._btnMasaSil.Text = "× Masa Sil";
            this._btnMasaSil.Click += new System.EventHandler(this.BtnMasaSil_Click);
            // 
            // _gridControlSiparisDetay
            // 
            this._gridControlSiparisDetay.Location = new System.Drawing.Point(20, 615);
            this._gridControlSiparisDetay.MainView = this._gridViewSiparisDetay;
            this._gridControlSiparisDetay.Name = "_gridControlSiparisDetay";
            this._gridControlSiparisDetay.Size = new System.Drawing.Size(260, 165);
            this._gridControlSiparisDetay.TabIndex = 17;
            this._gridControlSiparisDetay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewSiparisDetay});
            // 
            // _gridViewSiparisDetay
            // 
            this._gridViewSiparisDetay.GridControl = this._gridControlSiparisDetay;
            this._gridViewSiparisDetay.Name = "_gridViewSiparisDetay";
            this._gridViewSiparisDetay.OptionsView.ShowGroupPanel = false;
            // 
            // _lblToplam
            // 
            this._lblToplam.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this._lblToplam.Appearance.ForeColor = System.Drawing.Color.Red;
            this._lblToplam.Appearance.Options.UseFont = true;
            this._lblToplam.Appearance.Options.UseForeColor = true;
            this._lblToplam.Location = new System.Drawing.Point(20, 440);
            this._lblToplam.Name = "_lblToplam";
            this._lblToplam.Size = new System.Drawing.Size(200, 26);
            this._lblToplam.TabIndex = 14;
            this._lblToplam.Text = "Toplam: 0,00 TL";
            // 
            // _cmbTarifeler
            // 
            this._cmbTarifeler.Location = new System.Drawing.Point(20, 585);
            this._cmbTarifeler.Name = "_cmbTarifeler";
            this._cmbTarifeler.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._cmbTarifeler.Properties.Appearance.Options.UseFont = true;
            this._cmbTarifeler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._cmbTarifeler.Size = new System.Drawing.Size(260, 20);
            this._cmbTarifeler.TabIndex = 15;
            // 
            // FrmMasalar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this._tileControlMasalar);
            this.Controls.Add(this._grpMasaDetay);
            this.Name = "FrmMasalar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Kafe Durum";
            ((System.ComponentModel.ISupportInitialize)(this._grpMasaDetay)).EndInit();
            this._grpMasaDetay.ResumeLayout(false);
            this._grpMasaDetay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtPesinAlinan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSiparisToplami.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKullanimUcreti.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtKalanSure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtSureSiniri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtTarife.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtGecenSure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtBaslamaSaati.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtMusteri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cmbTarifeler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisDetay)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
