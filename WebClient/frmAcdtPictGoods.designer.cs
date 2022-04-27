namespace YLW_WebClient
{
    partial class frmAcdtPictGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcdtPictGoods));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImageView = new System.Windows.Forms.Button();
            this.cboAcdtPictFgChg = new System.Windows.Forms.ComboBox();
            this.btnSortOdrAply = new System.Windows.Forms.Button();
            this.btnAcdtPictFgAply = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAcdtPictFg = new System.Windows.Forms.ComboBox();
            this.btnFileD = new System.Windows.Forms.Button();
            this.btnFileA = new System.Windows.Forms.Button();
            this.panImage = new System.Windows.Forms.Panel();
            this.dgv_dbox = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.imgAcdtPictImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.AcdtPictSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictFgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictFg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjSeq1 = new YLW_WebClient.DataGridViewCustomComboBoxColumn();
            this.AcdtPictSerl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjSymb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictCnts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictRmk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictDt = new YLW_WebClient.DataGridViewCustomDateTimeInputColumn();
            this.AcdtPictFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcdtPictImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dbox_Del = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.WorkingTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dbox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnImageView);
            this.panel1.Controls.Add(this.cboAcdtPictFgChg);
            this.panel1.Controls.Add(this.btnSortOdrAply);
            this.panel1.Controls.Add(this.btnAcdtPictFgAply);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboAcdtPictFg);
            this.panel1.Controls.Add(this.btnFileD);
            this.panel1.Controls.Add(this.btnFileA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1175, 87);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1101, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 50);
            this.btnClose.TabIndex = 24;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnImageView
            // 
            this.btnImageView.Image = ((System.Drawing.Image)(resources.GetObject("btnImageView.Image")));
            this.btnImageView.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImageView.Location = new System.Drawing.Point(327, 11);
            this.btnImageView.Name = "btnImageView";
            this.btnImageView.Size = new System.Drawing.Size(85, 29);
            this.btnImageView.TabIndex = 23;
            this.btnImageView.TabStop = false;
            this.btnImageView.Text = "사진 보기";
            this.btnImageView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImageView.UseVisualStyleBackColor = true;
            // 
            // cboAcdtPictFgChg
            // 
            this.cboAcdtPictFgChg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAcdtPictFgChg.FormattingEnabled = true;
            this.cboAcdtPictFgChg.Location = new System.Drawing.Point(191, 52);
            this.cboAcdtPictFgChg.Name = "cboAcdtPictFgChg";
            this.cboAcdtPictFgChg.Size = new System.Drawing.Size(131, 20);
            this.cboAcdtPictFgChg.TabIndex = 18;
            this.cboAcdtPictFgChg.TabStop = false;
            this.cboAcdtPictFgChg.Visible = false;
            // 
            // btnSortOdrAply
            // 
            this.btnSortOdrAply.ForeColor = System.Drawing.Color.Silver;
            this.btnSortOdrAply.Location = new System.Drawing.Point(533, 14);
            this.btnSortOdrAply.Name = "btnSortOdrAply";
            this.btnSortOdrAply.Size = new System.Drawing.Size(87, 22);
            this.btnSortOdrAply.TabIndex = 17;
            this.btnSortOdrAply.Text = "사진재정렬 ▼";
            this.btnSortOdrAply.UseVisualStyleBackColor = true;
            this.btnSortOdrAply.Visible = false;
            // 
            // btnAcdtPictFgAply
            // 
            this.btnAcdtPictFgAply.Location = new System.Drawing.Point(327, 48);
            this.btnAcdtPictFgAply.Name = "btnAcdtPictFgAply";
            this.btnAcdtPictFgAply.Size = new System.Drawing.Size(104, 24);
            this.btnAcdtPictFgAply.TabIndex = 15;
            this.btnAcdtPictFgAply.TabStop = false;
            this.btnAcdtPictFgAply.Text = "선택행에 반영 ▼";
            this.btnAcdtPictFgAply.UseVisualStyleBackColor = true;
            this.btnAcdtPictFgAply.Visible = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.Location = new System.Drawing.Point(236, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(85, 29);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.TabStop = false;
            this.btnQuery.Text = "다시 조회";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(984, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 50);
            this.btnSave.TabIndex = 13;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "수정내용 저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "사진구분";
            // 
            // cboAcdtPictFg
            // 
            this.cboAcdtPictFg.FormattingEnabled = true;
            this.cboAcdtPictFg.Location = new System.Drawing.Point(78, 15);
            this.cboAcdtPictFg.Name = "cboAcdtPictFg";
            this.cboAcdtPictFg.Size = new System.Drawing.Size(148, 20);
            this.cboAcdtPictFg.TabIndex = 2;
            this.cboAcdtPictFg.TabStop = false;
            // 
            // btnFileD
            // 
            this.btnFileD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileD.Location = new System.Drawing.Point(83, 48);
            this.btnFileD.Name = "btnFileD";
            this.btnFileD.Size = new System.Drawing.Size(90, 27);
            this.btnFileD.TabIndex = 1;
            this.btnFileD.TabStop = false;
            this.btnFileD.Text = "선택행 삭제";
            this.btnFileD.UseVisualStyleBackColor = true;
            // 
            // btnFileA
            // 
            this.btnFileA.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileA.Location = new System.Drawing.Point(12, 48);
            this.btnFileA.Name = "btnFileA";
            this.btnFileA.Size = new System.Drawing.Size(67, 27);
            this.btnFileA.TabIndex = 0;
            this.btnFileA.TabStop = false;
            this.btnFileA.Text = "사진 추가";
            this.btnFileA.UseVisualStyleBackColor = true;
            // 
            // panImage
            // 
            this.panImage.Controls.Add(this.dgv_dbox);
            this.panImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panImage.Location = new System.Drawing.Point(0, 87);
            this.panImage.Name = "panImage";
            this.panImage.Size = new System.Drawing.Size(1175, 600);
            this.panImage.TabIndex = 32;
            // 
            // dgv_dbox
            // 
            this.dgv_dbox.AllowDrop = true;
            this.dgv_dbox.AllowUserToAddRows = false;
            this.dgv_dbox.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dbox.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_dbox.BackgroundColor = System.Drawing.Color.White;
            this.dgv_dbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_dbox.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dbox.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_dbox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgAcdtPictImage,
            this.AcdtPictSeq,
            this.AcdtPictFgName,
            this.AcdtPictFg,
            this.ObjSeq1,
            this.AcdtPictSerl,
            this.ObjSymb,
            this.AcdtPictCnts,
            this.AcdtPictRmk,
            this.AcdtPictDt,
            this.AcdtPictFile,
            this.AcdtPictImage,
            this.Dbox_Del,
            this.WorkingTag});
            this.dgv_dbox.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dbox.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_dbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_dbox.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_dbox.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_dbox.HighlightSelectedColumnHeaders = false;
            this.dgv_dbox.Location = new System.Drawing.Point(0, 0);
            this.dgv_dbox.Name = "dgv_dbox";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dbox.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_dbox.RowHeadersWidth = 25;
            this.dgv_dbox.RowTemplate.Height = 23;
            this.dgv_dbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_dbox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_dbox.Size = new System.Drawing.Size(1175, 600);
            this.dgv_dbox.TabIndex = 32;
            this.dgv_dbox.Tag = "4";
            // 
            // imgAcdtPictImage
            // 
            this.imgAcdtPictImage.DataPropertyName = "imgAcdtPictImage";
            this.imgAcdtPictImage.FillWeight = 120F;
            this.imgAcdtPictImage.Frozen = true;
            this.imgAcdtPictImage.HeaderText = "사진";
            this.imgAcdtPictImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.imgAcdtPictImage.Name = "imgAcdtPictImage";
            this.imgAcdtPictImage.ReadOnly = true;
            this.imgAcdtPictImage.Width = 120;
            // 
            // AcdtPictSeq
            // 
            this.AcdtPictSeq.DataPropertyName = "AcdtPictSeq";
            this.AcdtPictSeq.HeaderText = "사진내부코드";
            this.AcdtPictSeq.Name = "AcdtPictSeq";
            this.AcdtPictSeq.Visible = false;
            // 
            // AcdtPictFgName
            // 
            this.AcdtPictFgName.DataPropertyName = "AcdtPictFgName";
            this.AcdtPictFgName.HeaderText = "사진구분";
            this.AcdtPictFgName.MinimumWidth = 130;
            this.AcdtPictFgName.Name = "AcdtPictFgName";
            this.AcdtPictFgName.ReadOnly = true;
            this.AcdtPictFgName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AcdtPictFgName.Width = 130;
            // 
            // AcdtPictFg
            // 
            this.AcdtPictFg.DataPropertyName = "AcdtPictFg";
            this.AcdtPictFg.HeaderText = "사진구분내부코드";
            this.AcdtPictFg.Name = "AcdtPictFg";
            this.AcdtPictFg.ReadOnly = true;
            this.AcdtPictFg.Visible = false;
            // 
            // ObjSeq1
            // 
            this.ObjSeq1.DataPropertyName = "ObjSeq";
            this.ObjSeq1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ObjSeq1.DisplayStyleForCurrentCellOnly = true;
            this.ObjSeq1.HeaderText = "목적물명";
            this.ObjSeq1.Name = "ObjSeq1";
            this.ObjSeq1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // AcdtPictSerl
            // 
            this.AcdtPictSerl.DataPropertyName = "AcdtPictSerl";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AcdtPictSerl.DefaultCellStyle = dataGridViewCellStyle3;
            this.AcdtPictSerl.HeaderText = "순서";
            this.AcdtPictSerl.Name = "AcdtPictSerl";
            this.AcdtPictSerl.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AcdtPictSerl.Width = 50;
            // 
            // ObjSymb
            // 
            this.ObjSymb.DataPropertyName = "ObjSymb";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ObjSymb.DefaultCellStyle = dataGridViewCellStyle4;
            this.ObjSymb.HeaderText = "부호";
            this.ObjSymb.MaxInputLength = 5;
            this.ObjSymb.Name = "ObjSymb";
            this.ObjSymb.Width = 50;
            // 
            // AcdtPictCnts
            // 
            this.AcdtPictCnts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AcdtPictCnts.DataPropertyName = "AcdtPictCnts";
            this.AcdtPictCnts.HeaderText = "사진설명";
            this.AcdtPictCnts.Name = "AcdtPictCnts";
            // 
            // AcdtPictRmk
            // 
            this.AcdtPictRmk.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AcdtPictRmk.DataPropertyName = "AcdtPictRmk";
            this.AcdtPictRmk.HeaderText = "비고";
            this.AcdtPictRmk.Name = "AcdtPictRmk";
            // 
            // AcdtPictDt
            // 
            // 
            // 
            // 
            this.AcdtPictDt.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.AcdtPictDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AcdtPictDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.AcdtPictDt.ButtonDropDown.Visible = true;
            this.AcdtPictDt.DataPropertyName = "AcdtPictDt";
            this.AcdtPictDt.HeaderText = "첨부일자";
            this.AcdtPictDt.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.AcdtPictDt.MinimumWidth = 120;
            // 
            // 
            // 
            this.AcdtPictDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.AcdtPictDt.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.AcdtPictDt.MonthCalendar.BackgroundStyle.Class = "";
            this.AcdtPictDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.AcdtPictDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.AcdtPictDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AcdtPictDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 4, 1, 0, 0, 0, 0);
            this.AcdtPictDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.AcdtPictDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.AcdtPictDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.AcdtPictDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AcdtPictDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.AcdtPictDt.Name = "AcdtPictDt";
            this.AcdtPictDt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AcdtPictDt.Width = 120;
            // 
            // AcdtPictFile
            // 
            this.AcdtPictFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AcdtPictFile.DataPropertyName = "AcdtPictFile";
            this.AcdtPictFile.HeaderText = "파일명";
            this.AcdtPictFile.Name = "AcdtPictFile";
            this.AcdtPictFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // AcdtPictImage
            // 
            this.AcdtPictImage.DataPropertyName = "AcdtPictImage";
            this.AcdtPictImage.HeaderText = "이미지";
            this.AcdtPictImage.Name = "AcdtPictImage";
            this.AcdtPictImage.Visible = false;
            // 
            // Dbox_Del
            // 
            this.Dbox_Del.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.Dbox_Del.FillWeight = 33F;
            this.Dbox_Del.HeaderText = "";
            this.Dbox_Del.Name = "Dbox_Del";
            this.Dbox_Del.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Dbox_Del.Text = "삭제";
            this.Dbox_Del.UseColumnTextForButtonValue = true;
            this.Dbox_Del.Width = 33;
            // 
            // WorkingTag
            // 
            this.WorkingTag.DataPropertyName = "WorkingTag";
            this.WorkingTag.HeaderText = "WorkingTag";
            this.WorkingTag.Name = "WorkingTag";
            this.WorkingTag.Visible = false;
            // 
            // frmAcdtPictGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1175, 687);
            this.Controls.Add(this.panImage);
            this.Controls.Add(this.panel1);
            this.Name = "frmAcdtPictGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "사고조사사진등록";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFileA;
        private System.Windows.Forms.Button btnFileD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAcdtPictFg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panImage;
        private System.Windows.Forms.Button btnAcdtPictFgAply;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_dbox;
        private System.Windows.Forms.Button btnSortOdrAply;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cboAcdtPictFgChg;
        private System.Windows.Forms.Button btnImageView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewImageColumn imgAcdtPictImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictFgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictFg;
        private DataGridViewCustomComboBoxColumn ObjSeq1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictSerl;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjSymb;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictCnts;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictRmk;
        private DataGridViewCustomDateTimeInputColumn AcdtPictDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcdtPictImage;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn Dbox_Del;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkingTag;
    }
}