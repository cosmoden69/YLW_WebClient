namespace YLW_WebClient
{
    partial class frmReportView
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
            this.backstageViewControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ucSheet1 = new YLW_WebClient.ucSheet();
            ((System.ComponentModel.ISupportInitialize)(this.backstageViewControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // backstageViewControl1
            // 
            this.backstageViewControl1.Location = new System.Drawing.Point(256, 608);
            this.backstageViewControl1.Name = "backstageViewControl1";
            this.backstageViewControl1.Size = new System.Drawing.Size(8, 8);
            this.backstageViewControl1.TabIndex = 0;
            this.backstageViewControl1.Text = "backstageViewControl1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(921, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ucSheet1
            // 
            this.ucSheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSheet1.Location = new System.Drawing.Point(0, 24);
            this.ucSheet1.Name = "ucSheet1";
            this.ucSheet1.Size = new System.Drawing.Size(921, 858);
            this.ucSheet1.TabIndex = 2;
            // 
            // frmReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 882);
            this.Controls.Add(this.ucSheet1);
            this.Controls.Add(this.backstageViewControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmReportView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmReportView";
            ((System.ComponentModel.ISupportInitialize)(this.backstageViewControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ucSheet ucSheet1;
    }
}