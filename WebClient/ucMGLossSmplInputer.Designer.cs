namespace YLW_WebClient.CAA
{
    partial class ucMGLossSmplInputer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnSort = new System.Windows.Forms.Button();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.flp_Panel_1 = new YLW_WebClient.ucFlowLayoutPanel();
            this.ucMGLossPan11 = new YLW_WebClient.CAA.ucMGLossPan1();
            this.mgLossContract1 = new YLW_WebClient.CAA.MGLossSmplContract();
            this.mgLossSmplAccident1 = new YLW_WebClient.CAA.MGLossSmplAccident();
            this.txtS101_LongCnts1 = new YLW_WebClient.CAA.RichTextBox();
            this.dgv_file = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgv_Etc = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.etcOthInfoSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcLongCnts1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcLongCnts2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcShrtCnts1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcShrtCnts2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcShrtCnts3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcOthInfoGrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etcDel = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.fileFileSavSerl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileFileSavDt = new YLW_WebClient.DataGridViewCustomDateTimeInputColumn();
            this.fileFileCnts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileFileCntsCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileFileRels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileFileRmk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.첨부삭제 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.panelEx2.SuspendLayout();
            this.flp_Panel_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_file)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Etc)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(3, 224);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(725, 19);
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10156;
            this.panelEx1.Text = " ▣계약 사항 ";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnSort);
            this.panelEx2.Location = new System.Drawing.Point(3, 282);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(725, 19);
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 10156;
            this.panelEx2.Text = " ▣조사 결과 ";
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSort.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSort.Location = new System.Drawing.Point(671, 0);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(50, 19);
            this.btnSort.TabIndex = 10204;
            this.btnSort.TabStop = false;
            this.btnSort.Text = "정렬";
            this.btnSort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSort.UseVisualStyleBackColor = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Location = new System.Drawing.Point(3, 340);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(725, 19);
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10156;
            this.panelEx3.Text = " ※종합 의견 ";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Location = new System.Drawing.Point(3, 396);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(725, 19);
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx4.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 10156;
            this.panelEx4.Text = " ▣첨부 자료 ";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Location = new System.Drawing.Point(3, 497);
            this.panelEx5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(725, 19);
            this.panelEx5.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx5.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 10156;
            this.panelEx5.Text = " ▣피보험자 활동지역 도보 탐문조사 내역서 ";
            // 
            // flp_Panel_1
            // 
            this.flp_Panel_1.AutoScroll = true;
            this.flp_Panel_1.AutoSize = true;
            this.flp_Panel_1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_Panel_1.BackColor = System.Drawing.Color.White;
            this.flp_Panel_1.Controls.Add(this.ucMGLossPan11);
            this.flp_Panel_1.Controls.Add(this.panelEx1);
            this.flp_Panel_1.Controls.Add(this.mgLossContract1);
            this.flp_Panel_1.Controls.Add(this.panelEx2);
            this.flp_Panel_1.Controls.Add(this.mgLossSmplAccident1);
            this.flp_Panel_1.Controls.Add(this.panelEx3);
            this.flp_Panel_1.Controls.Add(this.txtS101_LongCnts1);
            this.flp_Panel_1.Controls.Add(this.panelEx4);
            this.flp_Panel_1.Controls.Add(this.dgv_file);
            this.flp_Panel_1.Controls.Add(this.panelEx5);
            this.flp_Panel_1.Controls.Add(this.dgv_Etc);
            this.flp_Panel_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_Panel_1.Location = new System.Drawing.Point(0, 0);
            this.flp_Panel_1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flp_Panel_1.Name = "flp_Panel_1";
            this.flp_Panel_1.Size = new System.Drawing.Size(755, 772);
            this.flp_Panel_1.TabIndex = 1;
            // 
            // ucMGLossPan11
            // 
            this.ucMGLossPan11.AcptDt = "";
            this.ucMGLossPan11.BackColor = System.Drawing.Color.White;
            this.ucMGLossPan11.CmplPnt1 = "0";
            this.ucMGLossPan11.CmplPnt2 = "0";
            this.ucMGLossPan11.CmplPnt3 = "0";
            this.ucMGLossPan11.CmplPnt4 = "0";
            this.ucMGLossPan11.CmplPnt5 = "0";
            this.ucMGLossPan11.DlyRprtDt = "";
            this.ucMGLossPan11.FldRptSbmsDt = "";
            this.ucMGLossPan11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucMGLossPan11.LasRptSbmsDt = "";
            this.ucMGLossPan11.Location = new System.Drawing.Point(3, 4);
            this.ucMGLossPan11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucMGLossPan11.MidRptSbmsDt = "";
            this.ucMGLossPan11.Name = "ucMGLossPan11";
            this.ucMGLossPan11.Size = new System.Drawing.Size(726, 212);
            this.ucMGLossPan11.TabIndex = 0;
            // 
            // mgLossContract1
            // 
            this.mgLossContract1.BackColor = System.Drawing.Color.White;
            this.mgLossContract1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mgLossContract1.Location = new System.Drawing.Point(3, 251);
            this.mgLossContract1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mgLossContract1.Name = "mgLossContract1";
            this.mgLossContract1.Size = new System.Drawing.Size(725, 23);
            this.mgLossContract1.TabIndex = 1;
            // 
            // mgLossSmplAccident1
            // 
            this.mgLossSmplAccident1.BackColor = System.Drawing.Color.White;
            this.mgLossSmplAccident1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mgLossSmplAccident1.Location = new System.Drawing.Point(3, 309);
            this.mgLossSmplAccident1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mgLossSmplAccident1.Name = "mgLossSmplAccident1";
            this.mgLossSmplAccident1.Size = new System.Drawing.Size(725, 23);
            this.mgLossSmplAccident1.TabIndex = 2;
            this.mgLossSmplAccident1.Userno1 = null;
            // 
            // txtS101_LongCnts1
            // 
            this.txtS101_LongCnts1.BackColor = System.Drawing.Color.White;
            this.txtS101_LongCnts1.bShowMenu = true;
            this.txtS101_LongCnts1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS101_LongCnts1.Location = new System.Drawing.Point(3, 367);
            this.txtS101_LongCnts1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtS101_LongCnts1.MaxInputLength = 0;
            this.txtS101_LongCnts1.MinHeight = -1;
            this.txtS101_LongCnts1.Name = "txtS101_LongCnts1";
            this.txtS101_LongCnts1.NewHeight = 21;
            this.txtS101_LongCnts1.Size = new System.Drawing.Size(725, 21);
            this.txtS101_LongCnts1.TabIndex = 3;
            // 
            // dgv_file
            // 
            this.dgv_file.AllowUserToDeleteRows = false;
            this.dgv_file.AllowUserToResizeColumns = false;
            this.dgv_file.AllowUserToResizeRows = false;
            this.dgv_file.BackgroundColor = System.Drawing.Color.White;
            this.dgv_file.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_file.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_file.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_file.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_file.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileFileSavSerl,
            this.fileFileSavDt,
            this.fileFileCnts,
            this.fileFileCntsCn,
            this.fileFileRels,
            this.fileFileRmk,
            this.첨부삭제});
            this.dgv_file.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_file.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_file.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_file.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_file.HighlightSelectedColumnHeaders = false;
            this.dgv_file.Location = new System.Drawing.Point(3, 422);
            this.dgv_file.MultiSelect = false;
            this.dgv_file.Name = "dgv_file";
            this.dgv_file.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_file.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_file.RowHeadersVisible = false;
            this.dgv_file.RowHeadersWidth = 25;
            this.dgv_file.RowTemplate.Height = 23;
            this.dgv_file.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_file.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_file.Size = new System.Drawing.Size(725, 68);
            this.dgv_file.TabIndex = 4;
            // 
            // dgv_Etc
            // 
            this.dgv_Etc.AllowUserToDeleteRows = false;
            this.dgv_Etc.AllowUserToResizeColumns = false;
            this.dgv_Etc.AllowUserToResizeRows = false;
            this.dgv_Etc.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Etc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Etc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Etc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Etc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Etc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.etcOthInfoSeq,
            this.etcLongCnts1,
            this.etcLongCnts2,
            this.etcShrtCnts1,
            this.etcShrtCnts2,
            this.etcShrtCnts3,
            this.etcOthInfoGrp,
            this.etcDel});
            this.dgv_Etc.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Etc.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_Etc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_Etc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_Etc.HighlightSelectedColumnHeaders = false;
            this.dgv_Etc.Location = new System.Drawing.Point(3, 523);
            this.dgv_Etc.MultiSelect = false;
            this.dgv_Etc.Name = "dgv_Etc";
            this.dgv_Etc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Etc.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_Etc.RowHeadersVisible = false;
            this.dgv_Etc.RowHeadersWidth = 25;
            this.dgv_Etc.RowTemplate.Height = 23;
            this.dgv_Etc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_Etc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Etc.Size = new System.Drawing.Size(725, 68);
            this.dgv_Etc.TabIndex = 5;
            // 
            // etcOthInfoSeq
            // 
            this.etcOthInfoSeq.DataPropertyName = "OthInfoSeq";
            this.etcOthInfoSeq.FillWeight = 50F;
            this.etcOthInfoSeq.HeaderText = "순번";
            this.etcOthInfoSeq.Name = "etcOthInfoSeq";
            this.etcOthInfoSeq.Width = 50;
            // 
            // etcLongCnts1
            // 
            this.etcLongCnts1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.etcLongCnts1.DataPropertyName = "LongCnts1";
            this.etcLongCnts1.FillWeight = 200F;
            this.etcLongCnts1.HeaderText = "소 재 지";
            this.etcLongCnts1.Name = "etcLongCnts1";
            // 
            // etcLongCnts2
            // 
            this.etcLongCnts2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.etcLongCnts2.DataPropertyName = "LongCnts2";
            this.etcLongCnts2.FillWeight = 200F;
            this.etcLongCnts2.HeaderText = "병 원 명";
            this.etcLongCnts2.Name = "etcLongCnts2";
            // 
            // etcShrtCnts1
            // 
            this.etcShrtCnts1.DataPropertyName = "ShrtCnts1";
            this.etcShrtCnts1.HeaderText = "연 락 처";
            this.etcShrtCnts1.MinimumWidth = 100;
            this.etcShrtCnts1.Name = "etcShrtCnts1";
            this.etcShrtCnts1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.etcShrtCnts1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // etcShrtCnts2
            // 
            this.etcShrtCnts2.DataPropertyName = "ShrtCnts2";
            this.etcShrtCnts2.HeaderText = "피보험자내원여부";
            this.etcShrtCnts2.MinimumWidth = 110;
            this.etcShrtCnts2.Name = "etcShrtCnts2";
            this.etcShrtCnts2.Width = 110;
            // 
            // etcShrtCnts3
            // 
            this.etcShrtCnts3.DataPropertyName = "ShrtCnts3";
            this.etcShrtCnts3.HeaderText = "비  고";
            this.etcShrtCnts3.MinimumWidth = 100;
            this.etcShrtCnts3.Name = "etcShrtCnts3";
            // 
            // etcOthInfoGrp
            // 
            this.etcOthInfoGrp.DataPropertyName = "OthInfoGrp";
            this.etcOthInfoGrp.HeaderText = "그룹";
            this.etcOthInfoGrp.Name = "etcOthInfoGrp";
            this.etcOthInfoGrp.Visible = false;
            // 
            // etcDel
            // 
            this.etcDel.FillWeight = 34F;
            this.etcDel.HeaderText = "";
            this.etcDel.Name = "etcDel";
            this.etcDel.Text = "삭제";
            this.etcDel.UseColumnTextForButtonValue = true;
            this.etcDel.Width = 34;
            // 
            // fileFileSavSerl
            // 
            this.fileFileSavSerl.DataPropertyName = "FileSavSerl";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fileFileSavSerl.DefaultCellStyle = dataGridViewCellStyle2;
            this.fileFileSavSerl.FillWeight = 40F;
            this.fileFileSavSerl.HeaderText = "순서";
            this.fileFileSavSerl.Name = "fileFileSavSerl";
            this.fileFileSavSerl.ReadOnly = true;
            this.fileFileSavSerl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fileFileSavSerl.Width = 40;
            // 
            // fileFileSavDt
            // 
            // 
            // 
            // 
            this.fileFileSavDt.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.fileFileSavDt.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.fileFileSavDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fileFileSavDt.BackgroundStyle.TextColor = System.Drawing.Color.Black;
            this.fileFileSavDt.DataPropertyName = "FileSavDt";
            this.fileFileSavDt.HeaderText = "일  자";
            this.fileFileSavDt.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            // 
            // 
            // 
            this.fileFileSavDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fileFileSavDt.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.fileFileSavDt.MonthCalendar.BackgroundStyle.Class = "";
            this.fileFileSavDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.fileFileSavDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.fileFileSavDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fileFileSavDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 10, 1, 0, 0, 0, 0);
            this.fileFileSavDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.fileFileSavDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fileFileSavDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.fileFileSavDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fileFileSavDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.fileFileSavDt.Name = "fileFileSavDt";
            this.fileFileSavDt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // fileFileCnts
            // 
            this.fileFileCnts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileFileCnts.DataPropertyName = "FileCnts";
            this.fileFileCnts.FillWeight = 360F;
            this.fileFileCnts.HeaderText = "첨 부 내 용";
            this.fileFileCnts.Name = "fileFileCnts";
            this.fileFileCnts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fileFileCntsCn
            // 
            this.fileFileCntsCn.DataPropertyName = "FileCntsCn";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "#,###";
            dataGridViewCellStyle3.NullValue = "0";
            this.fileFileCntsCn.DefaultCellStyle = dataGridViewCellStyle3;
            this.fileFileCntsCn.FillWeight = 40F;
            this.fileFileCntsCn.HeaderText = "매수";
            this.fileFileCntsCn.Name = "fileFileCntsCn";
            this.fileFileCntsCn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fileFileCntsCn.Width = 40;
            // 
            // fileFileRels
            // 
            this.fileFileRels.DataPropertyName = "FileRels";
            this.fileFileRels.HeaderText = "소 재 지";
            this.fileFileRels.Name = "fileFileRels";
            // 
            // fileFileRmk
            // 
            this.fileFileRmk.DataPropertyName = "FileRmk";
            this.fileFileRmk.HeaderText = "비  고";
            this.fileFileRmk.Name = "fileFileRmk";
            this.fileFileRmk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 첨부삭제
            // 
            this.첨부삭제.FillWeight = 34F;
            this.첨부삭제.HeaderText = "";
            this.첨부삭제.Name = "첨부삭제";
            this.첨부삭제.Text = "삭제";
            this.첨부삭제.UseColumnTextForButtonValue = true;
            this.첨부삭제.Width = 34;
            // 
            // ucMGLossSmplInputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flp_Panel_1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucMGLossSmplInputer";
            this.Size = new System.Drawing.Size(755, 772);
            this.panelEx2.ResumeLayout(false);
            this.flp_Panel_1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_file)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Etc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;  // ▣계약 사항
        private DevComponents.DotNetBar.PanelEx panelEx2;  // ▣조사 결과
        private DevComponents.DotNetBar.PanelEx panelEx3;  // ※종합 의견
        private DevComponents.DotNetBar.PanelEx panelEx4;  // ▣첨부 자료
        private DevComponents.DotNetBar.PanelEx panelEx5;  // ▣피보험자 활동지역 도보 탐문조사 내역서
        private RichTextBox txtS101_LongCnts1;
        private ucFlowLayoutPanel flp_Panel_1;
        private ucMGLossPan1 ucMGLossPan11;
        private MGLossSmplContract mgLossContract1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_file;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_Etc;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcOthInfoSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcLongCnts1;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcLongCnts2;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcShrtCnts1;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcShrtCnts2;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcShrtCnts3;
        private System.Windows.Forms.DataGridViewTextBoxColumn etcOthInfoGrp;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn etcDel;
        private System.Windows.Forms.Button btnSort;
        private MGLossSmplAccident mgLossSmplAccident1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileSavSerl;
        private DataGridViewCustomDateTimeInputColumn fileFileSavDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileCnts;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileCntsCn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileRels;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileRmk;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn 첨부삭제;
    }
}

