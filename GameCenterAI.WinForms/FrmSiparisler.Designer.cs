namespace GameCenterAI.WinForms
{
    partial class FrmSiparisler
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
            this._gridControlSiparisler = new DevExpress.XtraGrid.GridControl();
            this._gridViewSiparisler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this._btnDetay = new DevExpress.XtraEditors.SimpleButton();
            this._btnYenile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisler)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlSiparisler
            // 
            this._gridControlSiparisler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlSiparisler.Location = new System.Drawing.Point(12, 50);
            this._gridControlSiparisler.MainView = this._gridViewSiparisler;
            this._gridControlSiparisler.Name = "_gridControlSiparisler";
            this._gridControlSiparisler.Size = new System.Drawing.Size(746, 640);
            this._gridControlSiparisler.TabIndex = 0;
            this._gridControlSiparisler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewSiparisler});
            // 
            // _gridViewSiparisler
            // 
            this._gridViewSiparisler.GridControl = this._gridControlSiparisler;
            this._gridViewSiparisler.Name = "_gridViewSiparisler";
            this._gridViewSiparisler.OptionsView.ShowGroupPanel = false;
            // 
            // _btnYeni
            // 
            this._btnYeni.Location = new System.Drawing.Point(12, 12);
            this._btnYeni.Name = "_btnYeni";
            this._btnYeni.Size = new System.Drawing.Size(100, 30);
            this._btnYeni.TabIndex = 1;
            this._btnYeni.Text = "Yeni Sipariş";
            this._btnYeni.Click += new System.EventHandler(this.BtnYeni_Click);
            // 
            // _btnDetay
            // 
            this._btnDetay.Location = new System.Drawing.Point(118, 12);
            this._btnDetay.Name = "_btnDetay";
            this._btnDetay.Size = new System.Drawing.Size(100, 30);
            this._btnDetay.TabIndex = 2;
            this._btnDetay.Text = "Detay";
            this._btnDetay.Click += new System.EventHandler(this.BtnDetay_Click);
            // 
            // _btnYenile
            // 
            this._btnYenile.Location = new System.Drawing.Point(224, 12);
            this._btnYenile.Name = "_btnYenile";
            this._btnYenile.Size = new System.Drawing.Size(100, 30);
            this._btnYenile.TabIndex = 3;
            this._btnYenile.Text = "Yenile";
            this._btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // FrmSiparisler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this._btnYenile);
            this.Controls.Add(this._btnDetay);
            this.Controls.Add(this._btnYeni);
            this.Controls.Add(this._gridControlSiparisler);
            this.Name = "FrmSiparisler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameCenter AI - Sipariş Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this._gridControlSiparisler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewSiparisler)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}

