namespace GameCenterAI.WinForms
{
    partial class FrmSiparisEkle
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
            this._gridControlUrunler = new DevExpress.XtraGrid.GridControl();
            this._gridViewUrunler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._gridControlSiparisDetay = new DevExpress.XtraGrid.GridControl();
            this._gridViewSiparisDetay = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnUrunEkle = new DevExpress.XtraEditors.SimpleButton();
            this._btnUrunCikar = new DevExpress.XtraEditors.SimpleButton();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this._spinAdet = new DevExpress.XtraEditors.SpinEdit();
            this._lblToplam = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUrunler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUrunler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinAdet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlUrunler
            // 
            this._gridControlUrunler.Location = new System.Drawing.Point(12, 50);
            this._gridControlUrunler.MainView = this._gridViewUrunler;
            this._gridControlUrunler.Name = "_gridControlUrunler";
            this._gridControlUrunler.Size = new System.Drawing.Size(400, 300);
            this._gridControlUrunler.TabIndex = 0;
            this._gridControlUrunler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewUrunler});
            // 
            // _gridViewUrunler
            // 
            this._gridViewUrunler.GridControl = this._gridControlUrunler;
            this._gridViewUrunler.Name = "_gridViewUrunler";
            this._gridViewUrunler.OptionsView.ShowGroupPanel = false;
            // 
            // _gridControlSiparisDetay
            // 
            this._gridControlSiparisDetay.Location = new System.Drawing.Point(430, 50);
            this._gridControlSiparisDetay.MainView = this._gridViewSiparisDetay;
            this._gridControlSiparisDetay.Name = "_gridControlSiparisDetay";
            this._gridControlSiparisDetay.Size = new System.Drawing.Size(400, 300);
            this._gridControlSiparisDetay.TabIndex = 1;
            this._gridControlSiparisDetay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewSiparisDetay});
            // 
            // _gridViewSiparisDetay
            // 
            this._gridViewSiparisDetay.GridControl = this._gridControlSiparisDetay;
            this._gridViewSiparisDetay.Name = "_gridViewSiparisDetay";
            this._gridViewSiparisDetay.OptionsView.ShowGroupPanel = false;
            // 
            // _btnUrunEkle
            // 
            this._btnUrunEkle.Location = new System.Drawing.Point(12, 360);
            this._btnUrunEkle.Name = "_btnUrunEkle";
            this._btnUrunEkle.Size = new System.Drawing.Size(100, 30);
            this._btnUrunEkle.TabIndex = 2;
            this._btnUrunEkle.Text = "Ürün Ekle";
            this._btnUrunEkle.Click += new System.EventHandler(this.BtnUrunEkle_Click);
            // 
            // _btnUrunCikar
            // 
            this._btnUrunCikar.Location = new System.Drawing.Point(430, 360);
            this._btnUrunCikar.Name = "_btnUrunCikar";
            this._btnUrunCikar.Size = new System.Drawing.Size(100, 30);
            this._btnUrunCikar.TabIndex = 3;
            this._btnUrunCikar.Text = "Ürün Çıkar";
            this._btnUrunCikar.Click += new System.EventHandler(this.BtnUrunCikar_Click);
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Location = new System.Drawing.Point(655, 360);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 4;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Location = new System.Drawing.Point(736, 360);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 5;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // _spinAdet
            // 
            this._spinAdet.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._spinAdet.Location = new System.Drawing.Point(118, 363);
            this._spinAdet.Name = "_spinAdet";
            this._spinAdet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinAdet.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._spinAdet.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._spinAdet.Size = new System.Drawing.Size(80, 20);
            this._spinAdet.TabIndex = 6;
            // 
            // _lblToplam
            // 
            this._lblToplam.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._lblToplam.Appearance.Options.UseFont = true;
            this._lblToplam.Location = new System.Drawing.Point(430, 400);
            this._lblToplam.Name = "_lblToplam";
            this._lblToplam.Size = new System.Drawing.Size(80, 16);
            this._lblToplam.TabIndex = 7;
            this._lblToplam.Text = "Toplam: 0 TL";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Ürün Listesi:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(430, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Sipariş Detayları:";
            // 
            // FrmSiparisEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(842, 430);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._lblToplam);
            this.Controls.Add(this._spinAdet);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._btnUrunCikar);
            this.Controls.Add(this._btnUrunEkle);
            this.Controls.Add(this._gridControlSiparisDetay);
            this.Controls.Add(this._gridControlUrunler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSiparisEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sipariş Ekle";
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUrunler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUrunler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinAdet.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}

