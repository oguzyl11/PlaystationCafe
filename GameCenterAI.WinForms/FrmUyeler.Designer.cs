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
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUyeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUyeler)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlUyeler
            // 
            this._gridControlUyeler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlUyeler.Location = new System.Drawing.Point(12, 50);
            this._gridControlUyeler.MainView = this._gridViewUyeler;
            this._gridControlUyeler.Name = "_gridControlUyeler";
            this._gridControlUyeler.Size = new System.Drawing.Size(900, 500);
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
            this._btnYeni.Location = new System.Drawing.Point(12, 12);
            this._btnYeni.Name = "_btnYeni";
            this._btnYeni.Size = new System.Drawing.Size(100, 30);
            this._btnYeni.TabIndex = 1;
            this._btnYeni.Text = "Yeni";
            this._btnYeni.Click += new System.EventHandler(this.BtnYeni_Click);
            // 
            // _btnDuzenle
            // 
            this._btnDuzenle.Location = new System.Drawing.Point(118, 12);
            this._btnDuzenle.Name = "_btnDuzenle";
            this._btnDuzenle.Size = new System.Drawing.Size(100, 30);
            this._btnDuzenle.TabIndex = 2;
            this._btnDuzenle.Text = "Düzenle";
            this._btnDuzenle.Click += new System.EventHandler(this.BtnDuzenle_Click);
            // 
            // _btnSil
            // 
            this._btnSil.Location = new System.Drawing.Point(224, 12);
            this._btnSil.Name = "_btnSil";
            this._btnSil.Size = new System.Drawing.Size(100, 30);
            this._btnSil.TabIndex = 3;
            this._btnSil.Text = "Sil";
            this._btnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // _btnYenile
            // 
            this._btnYenile.Location = new System.Drawing.Point(330, 12);
            this._btnYenile.Name = "_btnYenile";
            this._btnYenile.Size = new System.Drawing.Size(100, 30);
            this._btnYenile.TabIndex = 4;
            this._btnYenile.Text = "Yenile";
            this._btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // FrmUyeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(924, 562);
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
            this.ResumeLayout(false);
        }

        #endregion
    }
}

