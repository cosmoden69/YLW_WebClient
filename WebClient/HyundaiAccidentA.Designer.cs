namespace YLW_WebClient.CAA
{
    partial class HyundaiAccidentA
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtGubunName = new YLW_WebClient.CustomTextBox();
            this.dtiCureFrDt = new YLW_WebClient.CustomDateTimeInput();
            this.dtiCureToDt = new YLW_WebClient.CustomDateTimeInput();
            this.iniOutHospDay = new YLW_WebClient.CustomIntegerInput();
            this.iniInHospDay = new YLW_WebClient.CustomIntegerInput();
            this.txtCureCnts = new YLW_WebClient.CAA.RichTextBox();
            this.txtTestNmRslt = new YLW_WebClient.CustomTextBox();
            this.txtVstHosp = new YLW_WebClient.CustomTextBox();
            this.txtBfGivCnts = new YLW_WebClient.CustomTextBox();
            this.txtPrvSrc = new YLW_WebClient.CustomTextBox();
            this.pan_hide_00 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx15 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx16 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx17 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.lblno00_01 = new System.Windows.Forms.Label();
            this.lblno00_02 = new System.Windows.Forms.Label();
            this.lblno00_04 = new System.Windows.Forms.Label();
            this.lblno00_05 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureFrDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureToDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniOutHospDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniInHospDay)).BeginInit();
            this.pan_hide_00.SuspendLayout();
            this.panelEx15.SuspendLayout();
            this.panelEx16.SuspendLayout();
            this.panelEx17.SuspendLayout();
            this.panelEx18.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGubunName
            // 
            this.txtGubunName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGubunName.Border.Class = "TextBoxBorder";
            this.txtGubunName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGubunName.FocusHighlightEnabled = true;
            this.txtGubunName.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtGubunName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtGubunName.Location = new System.Drawing.Point(0, 0);
            this.txtGubunName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGubunName.Multiline = true;
            this.txtGubunName.Name = "txtGubunName";
            this.txtGubunName.Size = new System.Drawing.Size(48, 45);
            this.txtGubunName.TabIndex = 0;
            this.txtGubunName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGubunName.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // dtiCureFrDt
            // 
            // 
            // 
            // 
            this.dtiCureFrDt.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureFrDt.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureFrDt.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureFrDt.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureFrDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiCureFrDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureFrDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiCureFrDt.ButtonDropDown.Visible = true;
            this.dtiCureFrDt.FocusHighlightEnabled = true;
            this.dtiCureFrDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtiCureFrDt.IsPopupCalendarOpen = false;
            this.dtiCureFrDt.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dtiCureFrDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiCureFrDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtiCureFrDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtiCureFrDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtiCureFrDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureFrDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtiCureFrDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiCureFrDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiCureFrDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtiCureFrDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureFrDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiCureFrDt.Name = "dtiCureFrDt";
            this.dtiCureFrDt.Size = new System.Drawing.Size(113, 23);
            this.dtiCureFrDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiCureFrDt.TabIndex = 1;
            this.dtiCureFrDt.ValueChanged += new System.EventHandler(this.Date_Change);
            // 
            // dtiCureToDt
            // 
            // 
            // 
            // 
            this.dtiCureToDt.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureToDt.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureToDt.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureToDt.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.dtiCureToDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiCureToDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureToDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiCureToDt.ButtonDropDown.Visible = true;
            this.dtiCureToDt.FocusHighlightEnabled = true;
            this.dtiCureToDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtiCureToDt.IsPopupCalendarOpen = false;
            this.dtiCureToDt.Location = new System.Drawing.Point(0, 22);
            // 
            // 
            // 
            this.dtiCureToDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiCureToDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtiCureToDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtiCureToDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtiCureToDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureToDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtiCureToDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiCureToDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiCureToDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtiCureToDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiCureToDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiCureToDt.Name = "dtiCureToDt";
            this.dtiCureToDt.Size = new System.Drawing.Size(113, 23);
            this.dtiCureToDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiCureToDt.TabIndex = 2;
            this.dtiCureToDt.ValueChanged += new System.EventHandler(this.Date_Change);
            // 
            // iniOutHospDay
            // 
            // 
            // 
            // 
            this.iniOutHospDay.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniOutHospDay.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniOutHospDay.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniOutHospDay.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniOutHospDay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniOutHospDay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniOutHospDay.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniOutHospDay.FocusHighlightEnabled = true;
            this.iniOutHospDay.ForeColor = System.Drawing.Color.SteelBlue;
            this.iniOutHospDay.Location = new System.Drawing.Point(0, 0);
            this.iniOutHospDay.Name = "iniOutHospDay";
            this.iniOutHospDay.Size = new System.Drawing.Size(40, 23);
            this.iniOutHospDay.TabIndex = 3;
            // 
            // iniInHospDay
            // 
            // 
            // 
            // 
            this.iniInHospDay.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniInHospDay.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniInHospDay.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniInHospDay.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.iniInHospDay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniInHospDay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniInHospDay.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniInHospDay.FocusHighlightEnabled = true;
            this.iniInHospDay.ForeColor = System.Drawing.Color.SteelBlue;
            this.iniInHospDay.Location = new System.Drawing.Point(0, 22);
            this.iniInHospDay.Name = "iniInHospDay";
            this.iniInHospDay.Size = new System.Drawing.Size(40, 23);
            this.iniInHospDay.TabIndex = 4;
            // 
            // txtCureCnts
            // 
            this.txtCureCnts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCureCnts.BackColor = System.Drawing.Color.White;
            this.txtCureCnts.bShowMenu = true;
            this.txtCureCnts.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCureCnts.Location = new System.Drawing.Point(198, 0);
            this.txtCureCnts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCureCnts.MaxInputLength = 0;
            this.txtCureCnts.MinHeight = 45;
            this.txtCureCnts.Name = "txtCureCnts";
            this.txtCureCnts.NewHeight = 47;
            this.txtCureCnts.Size = new System.Drawing.Size(280, 47);
            this.txtCureCnts.TabIndex = 5;
            // 
            // txtTestNmRslt
            // 
            this.txtTestNmRslt.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTestNmRslt.Border.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtTestNmRslt.Border.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtTestNmRslt.Border.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtTestNmRslt.Border.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtTestNmRslt.Border.Class = "TextBoxBorder";
            this.txtTestNmRslt.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTestNmRslt.FocusHighlightEnabled = true;
            this.txtTestNmRslt.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtTestNmRslt.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtTestNmRslt.Location = new System.Drawing.Point(0, 0);
            this.txtTestNmRslt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTestNmRslt.Multiline = true;
            this.txtTestNmRslt.Name = "txtTestNmRslt";
            this.txtTestNmRslt.Size = new System.Drawing.Size(110, 23);
            this.txtTestNmRslt.TabIndex = 6;
            this.txtTestNmRslt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTestNmRslt.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // txtVstHosp
            // 
            this.txtVstHosp.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtVstHosp.Border.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtVstHosp.Border.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtVstHosp.Border.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtVstHosp.Border.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.txtVstHosp.Border.Class = "TextBoxBorder";
            this.txtVstHosp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtVstHosp.FocusHighlightEnabled = true;
            this.txtVstHosp.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtVstHosp.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtVstHosp.Location = new System.Drawing.Point(0, 22);
            this.txtVstHosp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtVstHosp.Multiline = true;
            this.txtVstHosp.Name = "txtVstHosp";
            this.txtVstHosp.Size = new System.Drawing.Size(110, 23);
            this.txtVstHosp.TabIndex = 7;
            this.txtVstHosp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVstHosp.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // txtBfGivCnts
            // 
            this.txtBfGivCnts.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtBfGivCnts.Border.Class = "TextBoxBorder";
            this.txtBfGivCnts.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBfGivCnts.FocusHighlightEnabled = true;
            this.txtBfGivCnts.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtBfGivCnts.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtBfGivCnts.Location = new System.Drawing.Point(586, 0);
            this.txtBfGivCnts.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtBfGivCnts.Multiline = true;
            this.txtBfGivCnts.Name = "txtBfGivCnts";
            this.txtBfGivCnts.Size = new System.Drawing.Size(45, 45);
            this.txtBfGivCnts.TabIndex = 8;
            this.txtBfGivCnts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBfGivCnts.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // txtPrvSrc
            // 
            this.txtPrvSrc.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPrvSrc.Border.Class = "TextBoxBorder";
            this.txtPrvSrc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPrvSrc.FocusHighlightEnabled = true;
            this.txtPrvSrc.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtPrvSrc.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtPrvSrc.Location = new System.Drawing.Point(630, 0);
            this.txtPrvSrc.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtPrvSrc.Multiline = true;
            this.txtPrvSrc.Name = "txtPrvSrc";
            this.txtPrvSrc.Size = new System.Drawing.Size(62, 45);
            this.txtPrvSrc.TabIndex = 9;
            this.txtPrvSrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrvSrc.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.Controls.Add(this.txtCureCnts);
            this.pan_hide_00.Controls.Add(this.txtGubunName);
            this.pan_hide_00.Controls.Add(this.panelEx15);
            this.pan_hide_00.Controls.Add(this.panelEx16);
            this.pan_hide_00.Controls.Add(this.panelEx17);
            this.pan_hide_00.Controls.Add(this.txtBfGivCnts);
            this.pan_hide_00.Controls.Add(this.txtPrvSrc);
            this.pan_hide_00.Controls.Add(this.panelEx18);
            this.pan_hide_00.Controls.Add(this.lblno00_01);
            this.pan_hide_00.Controls.Add(this.lblno00_02);
            this.pan_hide_00.Controls.Add(this.lblno00_04);
            this.pan_hide_00.Controls.Add(this.lblno00_05);
            this.pan_hide_00.Location = new System.Drawing.Point(1, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(724, 45);
            this.pan_hide_00.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pan_hide_00.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.pan_hide_00.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pan_hide_00.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pan_hide_00.Style.GradientAngle = 90;
            this.pan_hide_00.TabIndex = 10196;
            // 
            // panelEx15
            // 
            this.panelEx15.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx15.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx15.Controls.Add(this.dtiCureFrDt);
            this.panelEx15.Controls.Add(this.dtiCureToDt);
            this.panelEx15.Location = new System.Drawing.Point(47, 0);
            this.panelEx15.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx15.Name = "panelEx15";
            this.panelEx15.Size = new System.Drawing.Size(113, 45);
            this.panelEx15.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx15.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx15.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx15.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx15.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx15.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx15.Style.GradientAngle = 90;
            this.panelEx15.TabIndex = 1;
            // 
            // panelEx16
            // 
            this.panelEx16.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx16.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx16.Controls.Add(this.iniOutHospDay);
            this.panelEx16.Controls.Add(this.iniInHospDay);
            this.panelEx16.Location = new System.Drawing.Point(159, 0);
            this.panelEx16.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx16.Name = "panelEx16";
            this.panelEx16.Size = new System.Drawing.Size(40, 45);
            this.panelEx16.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx16.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx16.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx16.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx16.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx16.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx16.Style.GradientAngle = 90;
            this.panelEx16.TabIndex = 3;
            // 
            // panelEx17
            // 
            this.panelEx17.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx17.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx17.Controls.Add(this.txtTestNmRslt);
            this.panelEx17.Controls.Add(this.txtVstHosp);
            this.panelEx17.Location = new System.Drawing.Point(477, 0);
            this.panelEx17.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx17.Name = "panelEx16";
            this.panelEx17.Size = new System.Drawing.Size(110, 45);
            this.panelEx17.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx17.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx17.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx17.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx17.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx17.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx17.Style.GradientAngle = 90;
            this.panelEx17.TabIndex = 6;
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Controls.Add(this.btnDel);
            this.panelEx18.Location = new System.Drawing.Point(691, 0);
            this.panelEx18.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(33, 45);
            this.panelEx18.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx18.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx18.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx18.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx18.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx18.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx18.Style.GradientAngle = 90;
            this.panelEx18.TabIndex = 10195;
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta;
            this.btnDel.ForeColor = System.Drawing.Color.Crimson;
            this.btnDel.Location = new System.Drawing.Point(2, 2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnDel.Size = new System.Drawing.Size(30, 19);
            this.btnDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDel.TabIndex = 10195;
            this.btnDel.TabStop = false;
            this.btnDel.Text = "삭제";
            this.btnDel.Click += new System.EventHandler(this.Button_Click);
            // 
            // lblno00_01
            // 
            this.lblno00_01.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_01.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_01.Location = new System.Drawing.Point(0, 0);
            this.lblno00_01.Name = "lblno00_01";
            this.lblno00_01.Size = new System.Drawing.Size(48, 45);
            this.lblno00_01.TabIndex = 10196;
            this.lblno00_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_01.Visible = false;
            // 
            // lblno00_02
            // 
            this.lblno00_02.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_02.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_02.Location = new System.Drawing.Point(47, 0);
            this.lblno00_02.Name = "lblno00_02";
            this.lblno00_02.Size = new System.Drawing.Size(113, 45);
            this.lblno00_02.TabIndex = 10196;
            this.lblno00_02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_02.Visible = false;
            // 
            // lblno00_04
            // 
            this.lblno00_04.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_04.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_04.Location = new System.Drawing.Point(160, 0);
            this.lblno00_04.Name = "lblno00_04";
            this.lblno00_04.Size = new System.Drawing.Size(403, 45);
            this.lblno00_04.TabIndex = 10196;
            this.lblno00_04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_04.Visible = false;
            // 
            // lblno00_05
            // 
            this.lblno00_05.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_05.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_05.Location = new System.Drawing.Point(563, 0);
            this.lblno00_05.Name = "lblno00_05";
            this.lblno00_05.Size = new System.Drawing.Size(160, 45);
            this.lblno00_05.TabIndex = 10196;
            this.lblno00_05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_05.Visible = false;
            // 
            // HyundaiAccidentA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HyundaiAccidentA";
            this.Size = new System.Drawing.Size(725, 45);
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureFrDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureToDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniOutHospDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniInHospDay)).EndInit();
            this.pan_hide_00.ResumeLayout(false);
            this.panelEx15.ResumeLayout(false);
            this.panelEx16.ResumeLayout(false);
            this.panelEx17.ResumeLayout(false);
            this.panelEx18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx pan_hide_00;
        private YLW_WebClient.CustomTextBox txtGubunName;
        private CustomDateTimeInput dtiCureFrDt;
        private CustomDateTimeInput dtiCureToDt;
        private CustomIntegerInput iniOutHospDay;
        private CustomIntegerInput iniInHospDay;
        private YLW_WebClient.CAA.RichTextBox txtCureCnts;
        private YLW_WebClient.CustomTextBox txtTestNmRslt;
        private YLW_WebClient.CustomTextBox txtVstHosp;
        private YLW_WebClient.CustomTextBox txtBfGivCnts;
        private YLW_WebClient.CustomTextBox txtPrvSrc;
        private DevComponents.DotNetBar.PanelEx panelEx15;
        private DevComponents.DotNetBar.PanelEx panelEx16;
        private DevComponents.DotNetBar.PanelEx panelEx17;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private System.Windows.Forms.Label lblno00_01;
        private System.Windows.Forms.Label lblno00_02;
        private System.Windows.Forms.Label lblno00_04;
        private System.Windows.Forms.Label lblno00_05;
    }
}
