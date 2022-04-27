namespace YLW_WebClient.CAA
{
    partial class frmHRFileDown
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
            this.chkAllEmp = new System.Windows.Forms.CheckBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chklinguisticFile = new System.Windows.Forms.CheckBox();
            this.chkLicenseFile = new System.Windows.Forms.CheckBox();
            this.chkCareerFile = new System.Windows.Forms.CheckBox();
            this.chkTranscriptFile = new System.Windows.Forms.CheckBox();
            this.chkGradeCertFile = new System.Windows.Forms.CheckBox();
            this.chkChildCareFile = new System.Windows.Forms.CheckBox();
            this.chkCarPayFile = new System.Windows.Forms.CheckBox();
            this.chkBnakBookFile = new System.Windows.Forms.CheckBox();
            this.chkRegIdFile = new System.Windows.Forms.CheckBox();
            this.txtEmp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDownloadAll = new System.Windows.Forms.Button();
            this.panDownFolder = new System.Windows.Forms.Panel();
            this.btnFolderOpen = new System.Windows.Forms.Button();
            this.btnFolderChange = new System.Windows.Forms.Button();
            this.txtDownPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panProcess = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvEmpList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.SEL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.deptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvFileList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.FileType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachFileSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachFileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePathName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileBase64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSingleFileBase64 = new System.Windows.Forms.TextBox();
            this.txtSingleFileName = new System.Windows.Forms.TextBox();
            this.btnSingleDown = new System.Windows.Forms.Button();
            this.btnSingleView = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panDownFolder.SuspendLayout();
            this.panProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAllEmp);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtEmp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDept);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDownloadAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1319, 96);
            this.panel1.TabIndex = 0;
            // 
            // chkAllEmp
            // 
            this.chkAllEmp.AutoSize = true;
            this.chkAllEmp.Location = new System.Drawing.Point(31, 62);
            this.chkAllEmp.Name = "chkAllEmp";
            this.chkAllEmp.Size = new System.Drawing.Size(57, 16);
            this.chkAllEmp.TabIndex = 21;
            this.chkAllEmp.Text = "전체↓";
            this.chkAllEmp.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(343, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(92, 29);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "사원명단 조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.chklinguisticFile);
            this.groupBox1.Controls.Add(this.chkLicenseFile);
            this.groupBox1.Controls.Add(this.chkCareerFile);
            this.groupBox1.Controls.Add(this.chkTranscriptFile);
            this.groupBox1.Controls.Add(this.chkGradeCertFile);
            this.groupBox1.Controls.Add(this.chkChildCareFile);
            this.groupBox1.Controls.Add(this.chkCarPayFile);
            this.groupBox1.Controls.Add(this.chkBnakBookFile);
            this.groupBox1.Controls.Add(this.chkRegIdFile);
            this.groupBox1.Location = new System.Drawing.Point(297, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(791, 47);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(15, 20);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 9;
            this.chkAll.Text = "전체";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // chklinguisticFile
            // 
            this.chklinguisticFile.AutoSize = true;
            this.chklinguisticFile.Location = new System.Drawing.Point(705, 20);
            this.chklinguisticFile.Name = "chklinguisticFile";
            this.chklinguisticFile.Size = new System.Drawing.Size(72, 16);
            this.chklinguisticFile.TabIndex = 18;
            this.chklinguisticFile.Text = "어학시험";
            this.chklinguisticFile.UseVisualStyleBackColor = true;
            // 
            // chkLicenseFile
            // 
            this.chkLicenseFile.AutoSize = true;
            this.chkLicenseFile.Location = new System.Drawing.Point(627, 20);
            this.chkLicenseFile.Name = "chkLicenseFile";
            this.chkLicenseFile.Size = new System.Drawing.Size(72, 16);
            this.chkLicenseFile.TabIndex = 17;
            this.chkLicenseFile.Text = "자격면허";
            this.chkLicenseFile.UseVisualStyleBackColor = true;
            // 
            // chkCareerFile
            // 
            this.chkCareerFile.AutoSize = true;
            this.chkCareerFile.Location = new System.Drawing.Point(573, 20);
            this.chkCareerFile.Name = "chkCareerFile";
            this.chkCareerFile.Size = new System.Drawing.Size(48, 16);
            this.chkCareerFile.TabIndex = 16;
            this.chkCareerFile.Text = "경력";
            this.chkCareerFile.UseVisualStyleBackColor = true;
            // 
            // chkTranscriptFile
            // 
            this.chkTranscriptFile.AutoSize = true;
            this.chkTranscriptFile.Location = new System.Drawing.Point(495, 20);
            this.chkTranscriptFile.Name = "chkTranscriptFile";
            this.chkTranscriptFile.Size = new System.Drawing.Size(72, 16);
            this.chkTranscriptFile.TabIndex = 15;
            this.chkTranscriptFile.Text = "성적증명";
            this.chkTranscriptFile.UseVisualStyleBackColor = true;
            // 
            // chkGradeCertFile
            // 
            this.chkGradeCertFile.AutoSize = true;
            this.chkGradeCertFile.Location = new System.Drawing.Point(417, 20);
            this.chkGradeCertFile.Name = "chkGradeCertFile";
            this.chkGradeCertFile.Size = new System.Drawing.Size(72, 16);
            this.chkGradeCertFile.TabIndex = 14;
            this.chkGradeCertFile.Text = "졸업증명";
            this.chkGradeCertFile.UseVisualStyleBackColor = true;
            // 
            // chkChildCareFile
            // 
            this.chkChildCareFile.AutoSize = true;
            this.chkChildCareFile.Location = new System.Drawing.Point(339, 20);
            this.chkChildCareFile.Name = "chkChildCareFile";
            this.chkChildCareFile.Size = new System.Drawing.Size(72, 16);
            this.chkChildCareFile.TabIndex = 13;
            this.chkChildCareFile.Text = "육아수당";
            this.chkChildCareFile.UseVisualStyleBackColor = true;
            // 
            // chkCarPayFile
            // 
            this.chkCarPayFile.AutoSize = true;
            this.chkCarPayFile.Location = new System.Drawing.Point(249, 20);
            this.chkCarPayFile.Name = "chkCarPayFile";
            this.chkCarPayFile.Size = new System.Drawing.Size(84, 16);
            this.chkCarPayFile.TabIndex = 12;
            this.chkCarPayFile.Text = "차량유지비";
            this.chkCarPayFile.UseVisualStyleBackColor = true;
            // 
            // chkBnakBookFile
            // 
            this.chkBnakBookFile.AutoSize = true;
            this.chkBnakBookFile.Location = new System.Drawing.Point(171, 20);
            this.chkBnakBookFile.Name = "chkBnakBookFile";
            this.chkBnakBookFile.Size = new System.Drawing.Size(72, 16);
            this.chkBnakBookFile.TabIndex = 11;
            this.chkBnakBookFile.Text = "통장사본";
            this.chkBnakBookFile.UseVisualStyleBackColor = true;
            // 
            // chkRegIdFile
            // 
            this.chkRegIdFile.AutoSize = true;
            this.chkRegIdFile.Location = new System.Drawing.Point(69, 20);
            this.chkRegIdFile.Name = "chkRegIdFile";
            this.chkRegIdFile.Size = new System.Drawing.Size(96, 16);
            this.chkRegIdFile.TabIndex = 10;
            this.chkRegIdFile.Text = "주민등록등본";
            this.chkRegIdFile.UseVisualStyleBackColor = true;
            // 
            // txtEmp
            // 
            this.txtEmp.BackColor = System.Drawing.Color.LightBlue;
            this.txtEmp.Location = new System.Drawing.Point(229, 15);
            this.txtEmp.Name = "txtEmp";
            this.txtEmp.Size = new System.Drawing.Size(100, 21);
            this.txtEmp.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "사 원";
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.Color.LightBlue;
            this.txtDept.Location = new System.Drawing.Point(59, 15);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(119, 21);
            this.txtDept.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "부 서";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1213, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 42);
            this.btnClose.TabIndex = 20;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.Location = new System.Drawing.Point(94, 55);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.Size = new System.Drawing.Size(174, 29);
            this.btnDownloadAll.TabIndex = 19;
            this.btnDownloadAll.Text = "파일 일괄 다운로드";
            this.btnDownloadAll.UseVisualStyleBackColor = true;
            // 
            // panDownFolder
            // 
            this.panDownFolder.Controls.Add(this.btnFolderOpen);
            this.panDownFolder.Controls.Add(this.btnFolderChange);
            this.panDownFolder.Controls.Add(this.txtDownPath);
            this.panDownFolder.Controls.Add(this.label1);
            this.panDownFolder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDownFolder.Location = new System.Drawing.Point(0, 760);
            this.panDownFolder.Name = "panDownFolder";
            this.panDownFolder.Size = new System.Drawing.Size(1319, 40);
            this.panDownFolder.TabIndex = 2;
            // 
            // btnFolderOpen
            // 
            this.btnFolderOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderOpen.Location = new System.Drawing.Point(1225, 7);
            this.btnFolderOpen.Name = "btnFolderOpen";
            this.btnFolderOpen.Size = new System.Drawing.Size(75, 26);
            this.btnFolderOpen.TabIndex = 3;
            this.btnFolderOpen.TabStop = false;
            this.btnFolderOpen.Text = "폴더 열기";
            this.btnFolderOpen.UseVisualStyleBackColor = true;
            // 
            // btnFolderChange
            // 
            this.btnFolderChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderChange.Location = new System.Drawing.Point(1147, 7);
            this.btnFolderChange.Name = "btnFolderChange";
            this.btnFolderChange.Size = new System.Drawing.Size(75, 26);
            this.btnFolderChange.TabIndex = 2;
            this.btnFolderChange.TabStop = false;
            this.btnFolderChange.Text = "폴더 변경";
            this.btnFolderChange.UseVisualStyleBackColor = true;
            // 
            // txtDownPath
            // 
            this.txtDownPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDownPath.Location = new System.Drawing.Point(103, 9);
            this.txtDownPath.Name = "txtDownPath";
            this.txtDownPath.ReadOnly = true;
            this.txtDownPath.Size = new System.Drawing.Size(1026, 21);
            this.txtDownPath.TabIndex = 1;
            this.txtDownPath.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "다운로드 경로";
            // 
            // panProcess
            // 
            this.panProcess.Controls.Add(this.txtProcess);
            this.panProcess.Controls.Add(this.pbProgress);
            this.panProcess.Controls.Add(this.label4);
            this.panProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panProcess.Location = new System.Drawing.Point(0, 720);
            this.panProcess.Name = "panProcess";
            this.panProcess.Size = new System.Drawing.Size(1319, 40);
            this.panProcess.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 96);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvEmpList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1319, 624);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvEmpList
            // 
            this.dgvEmpList.AllowDrop = true;
            this.dgvEmpList.AllowUserToAddRows = false;
            this.dgvEmpList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpList.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEmpList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmpList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SEL,
            this.deptName,
            this.DeptSeq,
            this.EmpName,
            this.EmpSeq});
            this.dgvEmpList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEmpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvEmpList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvEmpList.HighlightSelectedColumnHeaders = false;
            this.dgvEmpList.Location = new System.Drawing.Point(0, 0);
            this.dgvEmpList.Name = "dgvEmpList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEmpList.RowHeadersWidth = 25;
            this.dgvEmpList.RowTemplate.Height = 23;
            this.dgvEmpList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEmpList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpList.Size = new System.Drawing.Size(293, 624);
            this.dgvEmpList.TabIndex = 3;
            this.dgvEmpList.TabStop = false;
            this.dgvEmpList.Tag = "4";
            // 
            // SEL
            // 
            this.SEL.DataPropertyName = "SEL";
            this.SEL.FillWeight = 40F;
            this.SEL.HeaderText = "선택";
            this.SEL.Name = "SEL";
            this.SEL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SEL.Width = 40;
            // 
            // deptName
            // 
            this.deptName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.deptName.DataPropertyName = "DeptName";
            this.deptName.HeaderText = "부서";
            this.deptName.Name = "deptName";
            this.deptName.ReadOnly = true;
            // 
            // DeptSeq
            // 
            this.DeptSeq.DataPropertyName = "DeptSeq";
            this.DeptSeq.HeaderText = "부서코드";
            this.DeptSeq.Name = "DeptSeq";
            this.DeptSeq.ReadOnly = true;
            this.DeptSeq.Visible = false;
            // 
            // EmpName
            // 
            this.EmpName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmpName.DataPropertyName = "EmpName";
            this.EmpName.HeaderText = "사원";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            // 
            // EmpSeq
            // 
            this.EmpSeq.DataPropertyName = "EmpSeq";
            this.EmpSeq.HeaderText = "사원코드";
            this.EmpSeq.Name = "EmpSeq";
            this.EmpSeq.ReadOnly = true;
            this.EmpSeq.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvFileList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picPreview);
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(1022, 624);
            this.splitContainer2.SplitterDistance = 437;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvFileList
            // 
            this.dgvFileList.AllowDrop = true;
            this.dgvFileList.AllowUserToAddRows = false;
            this.dgvFileList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFileList.BackgroundColor = System.Drawing.Color.White;
            this.dgvFileList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFileList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvFileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileType,
            this.AttachFileSeq,
            this.AttachFileNo,
            this.FileName,
            this.FilePathName,
            this.FileExt,
            this.FileBase64});
            this.dgvFileList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileList.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFileList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFileList.HighlightSelectedColumnHeaders = false;
            this.dgvFileList.Location = new System.Drawing.Point(0, 0);
            this.dgvFileList.Name = "dgvFileList";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileList.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvFileList.RowHeadersWidth = 25;
            this.dgvFileList.RowTemplate.Height = 23;
            this.dgvFileList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFileList.Size = new System.Drawing.Size(437, 624);
            this.dgvFileList.TabIndex = 4;
            this.dgvFileList.TabStop = false;
            this.dgvFileList.Tag = "4";
            // 
            // FileType
            // 
            this.FileType.DataPropertyName = "FileType";
            this.FileType.HeaderText = "파일유형";
            this.FileType.Name = "FileType";
            this.FileType.ReadOnly = true;
            // 
            // AttachFileSeq
            // 
            this.AttachFileSeq.DataPropertyName = "AttachFileSeq";
            this.AttachFileSeq.HeaderText = "파일내부코드";
            this.AttachFileSeq.Name = "AttachFileSeq";
            this.AttachFileSeq.ReadOnly = true;
            this.AttachFileSeq.Visible = false;
            // 
            // AttachFileNo
            // 
            this.AttachFileNo.DataPropertyName = "AttachFileNo";
            this.AttachFileNo.HeaderText = "파일순번";
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
            this.FilePathName.HeaderText = "파일경로명";
            this.FilePathName.Name = "FilePathName";
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
            this.FileBase64.Visible = false;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.Window;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(0, 36);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(581, 588);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 36;
            this.picPreview.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSingleFileBase64);
            this.panel3.Controls.Add(this.txtSingleFileName);
            this.panel3.Controls.Add(this.btnSingleDown);
            this.panel3.Controls.Add(this.btnSingleView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(581, 36);
            this.panel3.TabIndex = 0;
            // 
            // txtSingleFileBase64
            // 
            this.txtSingleFileBase64.Location = new System.Drawing.Point(383, 7);
            this.txtSingleFileBase64.Name = "txtSingleFileBase64";
            this.txtSingleFileBase64.ReadOnly = true;
            this.txtSingleFileBase64.Size = new System.Drawing.Size(32, 21);
            this.txtSingleFileBase64.TabIndex = 3;
            this.txtSingleFileBase64.Visible = false;
            // 
            // txtSingleFileName
            // 
            this.txtSingleFileName.Location = new System.Drawing.Point(11, 7);
            this.txtSingleFileName.Name = "txtSingleFileName";
            this.txtSingleFileName.ReadOnly = true;
            this.txtSingleFileName.Size = new System.Drawing.Size(368, 21);
            this.txtSingleFileName.TabIndex = 2;
            this.txtSingleFileName.TabStop = false;
            // 
            // btnSingleDown
            // 
            this.btnSingleDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSingleDown.Location = new System.Drawing.Point(500, 6);
            this.btnSingleDown.Name = "btnSingleDown";
            this.btnSingleDown.Size = new System.Drawing.Size(75, 23);
            this.btnSingleDown.TabIndex = 1;
            this.btnSingleDown.TabStop = false;
            this.btnSingleDown.Text = "다운로드";
            this.btnSingleDown.UseVisualStyleBackColor = true;
            // 
            // btnSingleView
            // 
            this.btnSingleView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSingleView.Location = new System.Drawing.Point(422, 6);
            this.btnSingleView.Name = "btnSingleView";
            this.btnSingleView.Size = new System.Drawing.Size(75, 23);
            this.btnSingleView.TabIndex = 0;
            this.btnSingleView.TabStop = false;
            this.btnSingleView.Text = "뷰어 실행";
            this.btnSingleView.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "다운로드 진행";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(103, 8);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(421, 23);
            this.pbProgress.TabIndex = 1;
            // 
            // txtProcess
            // 
            this.txtProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcess.Location = new System.Drawing.Point(530, 10);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(770, 21);
            this.txtProcess.TabIndex = 2;
            // 
            // frmHRFileDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 800);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panProcess);
            this.Controls.Add(this.panDownFolder);
            this.Controls.Add(this.panel1);
            this.Name = "frmHRFileDown";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "첨부파일 조회";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panDownFolder.ResumeLayout(false);
            this.panDownFolder.PerformLayout();
            this.panProcess.ResumeLayout(false);
            this.panProcess.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpList)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panDownFolder;
        private System.Windows.Forms.Button btnFolderOpen;
        private System.Windows.Forms.Button btnFolderChange;
        private System.Windows.Forms.TextBox txtDownPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDownloadAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEmp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox chklinguisticFile;
        private System.Windows.Forms.CheckBox chkLicenseFile;
        private System.Windows.Forms.CheckBox chkCareerFile;
        private System.Windows.Forms.CheckBox chkTranscriptFile;
        private System.Windows.Forms.CheckBox chkGradeCertFile;
        private System.Windows.Forms.CheckBox chkChildCareFile;
        private System.Windows.Forms.CheckBox chkCarPayFile;
        private System.Windows.Forms.CheckBox chkBnakBookFile;
        private System.Windows.Forms.CheckBox chkRegIdFile;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkAllEmp;
        private System.Windows.Forms.Panel panProcess;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvEmpList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn deptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpSeq;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvFileList;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileType;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttachFileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePathName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileBase64;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSingleFileBase64;
        private System.Windows.Forms.TextBox txtSingleFileName;
        private System.Windows.Forms.Button btnSingleDown;
        private System.Windows.Forms.Button btnSingleView;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label label4;
    }
}