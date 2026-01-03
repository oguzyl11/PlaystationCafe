using System;

namespace GameCenterAI.WinForms
{
    partial class FrmUyeSec
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
            this._btnSec = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUyeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUyeler)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlUyeler
            // 
            this._gridControlUyeler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridControlUyeler.Location = new System.Drawing.Point(12, 12);
            this._gridControlUyeler.MainView = this._gridViewUyeler;
            this._gridControlUyeler.Name = "_gridControlUyeler";
            this._gridControlUyeler.Size = new System.Drawing.Size(600, 400);
            this._gridControlUyeler.TabIndex = 0;
            this._gridControlUyeler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewUyeler});
            // 
            // _gridViewUyeler
            // 
            this._gridViewUyeler.GridControl = this._gridControlUyeler;
            this._gridViewUyeler.Name = "_gridViewUyeler";
            this._gridViewUyeler.OptionsView.ShowGroupPanel = false;
            this._gridViewUyeler.DoubleClick += new System.EventHandler(this._gridViewUyeler_DoubleClick);
            // 
            // _btnSec
            // 
            this._btnSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSec.Location = new System.Drawing.Point(456, 418);
            this._btnSec.Name = "_btnSec";
            this._btnSec.Size = new System.Drawing.Size(75, 30);
            this._btnSec.TabIndex = 1;
            this._btnSec.Text = "Seç";
            this._btnSec.Click += new System.EventHandler(this.BtnSec_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnIptal.Location = new System.Drawing.Point(537, 418);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 2;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmUyeSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(624, 460);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnSec);
            this.Controls.Add(this._gridControlUyeler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUyeSec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Üye Seç";
            ((System.ComponentModel.ISupportInitialize)(this._gridControlUyeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewUyeler)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private void _gridViewUyeler_DoubleClick(object sender, EventArgs e)
        {
            BtnSec_Click(sender, e);
        }
    }
}

