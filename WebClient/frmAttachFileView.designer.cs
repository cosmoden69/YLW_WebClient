namespace YLW_WebClient.CAA
{
    partial class frmAttachFileView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtFileSeq = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabList = new System.Windows.Forms.TabPage();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.dgvList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.fileImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.AttachFileSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachFileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePathName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileBase64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPreview = new System.Windows.Forms.TabPage();
            this.dgvImg = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.fileImage1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.AttachFileSeq1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachFileNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePathName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileExt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileBase641 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileImage2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.AttachFileSeq2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachFileNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePathName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileExt2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileBase642 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tabPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.txtFileSeq);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 51);
            this.panel1.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(527, 8);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(72, 34);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "다운로드";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "파일내부코드";
            this.label1.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(605, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 34);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // txtFileSeq
            // 
            this.txtFileSeq.Location = new System.Drawing.Point(195, 16);
            this.txtFileSeq.Name = "txtFileSeq";
            this.txtFileSeq.Size = new System.Drawing.Size(55, 21);
            this.txtFileSeq.TabIndex = 2;
            this.txtFileSeq.Visible = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(35, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(62, 34);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(672, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 34);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "닫기";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabList);
            this.tabControl1.Controls.Add(this.tabPreview);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 823);
            this.tabControl1.TabIndex = 1;
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.picPreview);
            this.tabList.Controls.Add(this.dgvList);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(750, 797);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "목록";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(3, 278);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(744, 516);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 34;
            this.picPreview.TabStop = false;
            // 
            // dgvList
            // 
            this.dgvList.AllowDrop = true;
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileImage,
            this.AttachFileSeq,
            this.AttachFileNo,
            this.FileName,
            this.FilePathName,
            this.FileExt,
            this.FileBase64});
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvList.HighlightSelectedColumnHeaders = false;
            this.dgvList.Location = new System.Drawing.Point(3, 3);
            this.dgvList.Name = "dgvList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowHeadersWidth = 25;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(744, 275);
            this.dgvList.TabIndex = 33;
            this.dgvList.Tag = "4";
            // 
            // fileImage
            // 
            this.fileImage.DataPropertyName = "fileImage";
            this.fileImage.FillWeight = 60F;
            this.fileImage.HeaderText = "이미지";
            this.fileImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.fileImage.Name = "fileImage";
            this.fileImage.ReadOnly = true;
            this.fileImage.Width = 60;
            // 
            // AttachFileSeq
            // 
            this.AttachFileSeq.DataPropertyName = "AttachFileSeq";
            this.AttachFileSeq.HeaderText = "이미지내부코드";
            this.AttachFileSeq.Name = "AttachFileSeq";
            this.AttachFileSeq.ReadOnly = true;
            this.AttachFileSeq.Visible = false;
            // 
            // AttachFileNo
            // 
            this.AttachFileNo.DataPropertyName = "AttachFileNo";
            this.AttachFileNo.HeaderText = "이미지순번";
            this.AttachFileNo.Name = "AttachFileNo";
            this.AttachFileNo.ReadOnly = true;
            this.AttachFileNo.Visible = false;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "파일명";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // FilePathName
            // 
            this.FilePathName.DataPropertyName = "FilePathName";
            this.FilePathName.HeaderText = "파일경로";
            this.FilePathName.Name = "FilePathName";
            this.FilePathName.ReadOnly = true;
            this.FilePathName.Visible = false;
            // 
            // FileExt
            // 
            this.FileExt.DataPropertyName = "FileExt";
            this.FileExt.HeaderText = "확장자";
            this.FileExt.Name = "FileExt";
            this.FileExt.Visible = false;
            // 
            // FileBase64
            // 
            this.FileBase64.DataPropertyName = "FileBase64";
            this.FileBase64.HeaderText = "이미지문자열";
            this.FileBase64.Name = "FileBase64";
            this.FileBase64.ReadOnly = true;
            this.FileBase64.Visible = false;
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.dgvImg);
            this.tabPreview.Location = new System.Drawing.Point(4, 22);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreview.Size = new System.Drawing.Size(750, 797);
            this.tabPreview.TabIndex = 1;
            this.tabPreview.Text = "미리보기";
            this.tabPreview.UseVisualStyleBackColor = true;
            // 
            // dgvImg
            // 
            this.dgvImg.AllowDrop = true;
            this.dgvImg.AllowUserToAddRows = false;
            this.dgvImg.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvImg.BackgroundColor = System.Drawing.Color.White;
            this.dgvImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvImg.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvImg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileImage1,
            this.AttachFileSeq1,
            this.AttachFileNo1,
            this.FileName1,
            this.FilePathName1,
            this.FileExt1,
            this.FileBase641,
            this.fileImage2,
            this.AttachFileSeq2,
            this.AttachFileNo2,
            this.FileName2,
            this.FilePathName2,
            this.FileExt2,
            this.FileBase642});
            this.dgvImg.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImg.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvImg.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvImg.HighlightSelectedColumnHeaders = false;
            this.dgvImg.Location = new System.Drawing.Point(3, 3);
            this.dgvImg.Name = "dgvImg";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImg.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvImg.RowHeadersWidth = 25;
            this.dgvImg.RowTemplate.Height = 23;
            this.dgvImg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvImg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvImg.Size = new System.Drawing.Size(744, 791);
            this.dgvImg.TabIndex = 34;
            this.dgvImg.Tag = "4";
            // 
            // fileImage1
            // 
            this.fileImage1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileImage1.DataPropertyName = "fileImage1";
            this.fileImage1.HeaderText = "이미지1";
            this.fileImage1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.fileImage1.Name = "fileImage1";
            this.fileImage1.ReadOnly = true;
            // 
            // AttachFileSeq1
            // 
            this.AttachFileSeq1.DataPropertyName = "AttachFileSeq1";
            this.AttachFileSeq1.HeaderText = "이미지내부코드";
            this.AttachFileSeq1.Name = "AttachFileSeq1";
            this.AttachFileSeq1.ReadOnly = true;
            this.AttachFileSeq1.Visible = false;
            // 
            // AttachFileNo1
            // 
            this.AttachFileNo1.DataPropertyName = "AttachFileNo1";
            this.AttachFileNo1.HeaderText = "이미지순번";
            this.AttachFileNo1.Name = "AttachFileNo1";
            this.AttachFileNo1.ReadOnly = true;
            this.AttachFileNo1.Visible = false;
            // 
            // FileName1
            // 
            this.FileName1.DataPropertyName = "FileName1";
            this.FileName1.HeaderText = "파일명";
            this.FileName1.Name = "FileName1";
            this.FileName1.ReadOnly = true;
            this.FileName1.Visible = false;
            this.FileName1.Width = 532;
            // 
            // FilePathName1
            // 
            this.FilePathName1.DataPropertyName = "FilePathName1";
            this.FilePathName1.HeaderText = "파일경로";
            this.FilePathName1.Name = "FilePathName1";
            this.FilePathName1.ReadOnly = true;
            this.FilePathName1.Visible = false;
            // 
            // FileExt1
            // 
            this.FileExt1.DataPropertyName = "FileExt1";
            this.FileExt1.HeaderText = "확장자";
            this.FileExt1.Name = "FileExt1";
            this.FileExt1.ReadOnly = true;
            this.FileExt1.Visible = false;
            // 
            // FileBase641
            // 
            this.FileBase641.DataPropertyName = "FileBase641";
            this.FileBase641.HeaderText = "이미지문자열";
            this.FileBase641.Name = "FileBase641";
            this.FileBase641.ReadOnly = true;
            this.FileBase641.Visible = false;
            // 
            // fileImage2
            // 
            this.fileImage2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileImage2.DataPropertyName = "fileImage2";
            this.fileImage2.HeaderText = "이미지2";
            this.fileImage2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.fileImage2.Name = "fileImage2";
            this.fileImage2.ReadOnly = true;
            // 
            // AttachFileSeq2
            // 
            this.AttachFileSeq2.DataPropertyName = "AttachFileSeq2";
            this.AttachFileSeq2.HeaderText = "이미지내부코드";
            this.AttachFileSeq2.Name = "AttachFileSeq2";
            this.AttachFileSeq2.ReadOnly = true;
            this.AttachFileSeq2.Visible = false;
            // 
            // AttachFileNo2
            // 
            this.AttachFileNo2.DataPropertyName = "AttachFileNo2";
            this.AttachFileNo2.HeaderText = "이미지순번";
            this.AttachFileNo2.Name = "AttachFileNo2";
            this.AttachFileNo2.ReadOnly = true;
            this.AttachFileNo2.Visible = false;
            // 
            // FileName2
            // 
            this.FileName2.DataPropertyName = "FileName2";
            this.FileName2.HeaderText = "파일명";
            this.FileName2.Name = "FileName2";
            this.FileName2.ReadOnly = true;
            this.FileName2.Visible = false;
            // 
            // FilePathName2
            // 
            this.FilePathName2.DataPropertyName = "FilePathName2";
            this.FilePathName2.HeaderText = "파일경로";
            this.FilePathName2.Name = "FilePathName2";
            this.FilePathName2.ReadOnly = true;
            this.FilePathName2.Visible = false;
            // 
            // FileExt2
            // 
            this.FileExt2.DataPropertyName = "FileExt2";
            this.FileExt2.HeaderText = "확장자";
            this.FileExt2.Name = "FileExt2";
            this.FileExt2.ReadOnly = true;
            this.FileExt2.Visible = false;
            // 
            // FileBase642
            // 
            this.FileBase642.DataPropertyName = "FileBase642";
            this.FileBase642.HeaderText = "이미지문자열";
            this.FileBase642.Name = "FileBase642";
            this.FileBase642.ReadOnly = true;
            this.FileBase642.Visible = false;
            // 
            // frmAttachFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 874);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmAttachFileView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "첨부파일보기";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tabPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.TabPage tabPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvList;
        private System.Windows.Forms.TextBox txtFileSeq;
        private System.Windows.Forms.Button btnQuery;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvImg;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewImageColumn fileImage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileSeq1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePathName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileExt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileBase641;
        private System.Windows.Forms.DataGridViewImageColumn fileImage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileSeq2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePathName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileExt2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileBase642;
        private System.Windows.Forms.DataGridViewImageColumn fileImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePathName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileBase64;
        private System.Windows.Forms.Button btnDownload;
    }
}