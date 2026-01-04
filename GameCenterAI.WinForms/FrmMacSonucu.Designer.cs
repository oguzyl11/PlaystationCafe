namespace GameCenterAI.WinForms
{
    partial class FrmMacSonucu
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
            this._lblUye1 = new DevExpress.XtraEditors.LabelControl();
            this._lblUye2 = new DevExpress.XtraEditors.LabelControl();
            this._spinSkor1 = new DevExpress.XtraEditors.SpinEdit();
            this._spinSkor2 = new DevExpress.XtraEditors.SpinEdit();
            this._btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this._btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._spinSkor1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinSkor2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _lblUye1
            // 
            this._lblUye1.Location = new System.Drawing.Point(20, 20);
            this._lblUye1.Name = "_lblUye1";
            this._lblUye1.Size = new System.Drawing.Size(50, 13);
            this._lblUye1.TabIndex = 0;
            this._lblUye1.Text = "Oyuncu 1:";
            // 
            // _lblUye2
            // 
            this._lblUye2.Location = new System.Drawing.Point(20, 60);
            this._lblUye2.Name = "_lblUye2";
            this._lblUye2.Size = new System.Drawing.Size(50, 13);
            this._lblUye2.TabIndex = 1;
            this._lblUye2.Text = "Oyuncu 2:";
            // 
            // _spinSkor1
            // 
            this._spinSkor1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinSkor1.Location = new System.Drawing.Point(200, 17);
            this._spinSkor1.Name = "_spinSkor1";
            this._spinSkor1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinSkor1.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._spinSkor1.Properties.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinSkor1.Size = new System.Drawing.Size(100, 20);
            this._spinSkor1.TabIndex = 2;
            // 
            // _spinSkor2
            // 
            this._spinSkor2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinSkor2.Location = new System.Drawing.Point(200, 57);
            this._spinSkor2.Name = "_spinSkor2";
            this._spinSkor2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._spinSkor2.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._spinSkor2.Properties.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._spinSkor2.Size = new System.Drawing.Size(100, 20);
            this._spinSkor2.TabIndex = 3;
            // 
            // _btnKaydet
            // 
            this._btnKaydet.Location = new System.Drawing.Point(144, 100);
            this._btnKaydet.Name = "_btnKaydet";
            this._btnKaydet.Size = new System.Drawing.Size(75, 30);
            this._btnKaydet.TabIndex = 4;
            this._btnKaydet.Text = "Kaydet";
            this._btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // _btnIptal
            // 
            this._btnIptal.Location = new System.Drawing.Point(225, 100);
            this._btnIptal.Name = "_btnIptal";
            this._btnIptal.Size = new System.Drawing.Size(75, 30);
            this._btnIptal.TabIndex = 5;
            this._btnIptal.Text = "İptal";
            this._btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(150, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Skor:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(150, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Skor:";
            // 
            // FrmMacSonucu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 150);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this._btnIptal);
            this.Controls.Add(this._btnKaydet);
            this.Controls.Add(this._spinSkor2);
            this.Controls.Add(this._spinSkor1);
            this.Controls.Add(this._lblUye2);
            this.Controls.Add(this._lblUye1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMacSonucu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maç Sonucu";
            ((System.ComponentModel.ISupportInitialize)(this._spinSkor1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._spinSkor2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}

