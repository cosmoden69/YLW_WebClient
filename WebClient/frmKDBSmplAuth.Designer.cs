namespace YLW_WebClient
{
    partial class frmKDBSmplAuth
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
            this.grd = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRowDel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtCode_id = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grd
            // 
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 56);
            this.grd.Name = "grd";
            this.grd.RowTemplate.Height = 23;
            this.grd.Size = new System.Drawing.Size(644, 443);
            this.grd.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRowDel);
            this.panel2.Controls.Add(this.btnQuery);
            this.panel2.Controls.Add(this.txtCode_id);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 56);
            this.panel2.TabIndex = 14;
            // 
            // btnRowDel
            // 
            this.btnRowDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowDel.Location = new System.Drawing.Point(92, 13);
            this.btnRowDel.Name = "btnRowDel";
            this.btnRowDel.Size = new System.Drawing.Size(74, 30);
            this.btnRowDel.TabIndex = 4;
            this.btnRowDel.Text = "행삭제";
            this.btnRowDel.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(12, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(74, 30);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "다시조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // txtCode_id
            // 
            this.txtCode_id.Location = new System.Drawing.Point(508, 13);
            this.txtCode_id.Name = "txtCode_id";
            this.txtCode_id.Size = new System.Drawing.Size(111, 21);
            this.txtCode_id.TabIndex = 5;
            this.txtCode_id.Text = "USERCD_AUTH";
            this.txtCode_id.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(172, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmKDBSmplAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 499);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.panel2);
            this.Name = "frmKDBSmplAuth";
            this.ShowInTaskbar = false;
            this.Text = "KDB";
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRowDel;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtCode_id;
        private System.Windows.Forms.Button btnSave;
    }
}