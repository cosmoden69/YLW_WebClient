namespace YLW_WebClient.CAA
{
    partial class ucMeritzPan1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dtSurvReqDt = new YLW_WebClient.CustomDateTimeInput();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dtAcptDt = new YLW_WebClient.CustomDateTimeInput();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dtLasRptSbmsDt = new YLW_WebClient.CustomDateTimeInput();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelEx21 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx22 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx23 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx24 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx25 = new DevComponents.DotNetBar.PanelEx();
            this.txtInsured = new YLW_WebClient.CustomTextBox();
            this.txtAcdtNo = new YLW_WebClient.CustomTextBox();
            this.txtIsrdRegno = new YLW_WebClient.CustomTextBox();
            this.txtSurvAsgnEmpName = new YLW_WebClient.CustomTextBox();
            this.txtInsurChrg = new YLW_WebClient.CustomTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtSurvReqDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAcptDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLasRptSbmsDt)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEx1);
            this.panel1.Controls.Add(this.dtSurvReqDt);
            this.panel1.Controls.Add(this.panelEx2);
            this.panel1.Controls.Add(this.dtAcptDt);
            this.panel1.Controls.Add(this.panelEx3);
            this.panel1.Controls.Add(this.dtLasRptSbmsDt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 43);
            this.panel1.TabIndex = 10197;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(20, 10);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(70, 23);
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10185;
            this.panelEx1.Text = "사고접수일";
            // 
            // dtSurvReqDt
            // 
            // 
            // 
            // 
            this.dtSurvReqDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtSurvReqDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSurvReqDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtSurvReqDt.ButtonDropDown.Visible = true;
            this.dtSurvReqDt.FocusHighlightEnabled = true;
            this.dtSurvReqDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtSurvReqDt.IsPopupCalendarOpen = false;
            this.dtSurvReqDt.Location = new System.Drawing.Point(89, 10);
            // 
            // 
            // 
            this.dtSurvReqDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtSurvReqDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtSurvReqDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtSurvReqDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtSurvReqDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSurvReqDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtSurvReqDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtSurvReqDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtSurvReqDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtSurvReqDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSurvReqDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtSurvReqDt.Name = "dtSurvReqDt";
            this.dtSurvReqDt.Size = new System.Drawing.Size(116, 23);
            this.dtSurvReqDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtSurvReqDt.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Location = new System.Drawing.Point(250, 10);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(70, 23);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx2.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx2.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 10186;
            this.panelEx2.Text = "의뢰일자";
            // 
            // dtAcptDt
            // 
            // 
            // 
            // 
            this.dtAcptDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtAcptDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcptDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtAcptDt.ButtonDropDown.Visible = true;
            this.dtAcptDt.FocusHighlightEnabled = true;
            this.dtAcptDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtAcptDt.IsPopupCalendarOpen = false;
            this.dtAcptDt.Location = new System.Drawing.Point(319, 10);
            // 
            // 
            // 
            this.dtAcptDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtAcptDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtAcptDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtAcptDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtAcptDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcptDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtAcptDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtAcptDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtAcptDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtAcptDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcptDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtAcptDt.Name = "dtAcptDt";
            this.dtAcptDt.Size = new System.Drawing.Size(116, 23);
            this.dtAcptDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtAcptDt.TabIndex = 1;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Location = new System.Drawing.Point(500, 10);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(70, 23);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx3.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10186;
            this.panelEx3.Text = "보고일자";
            // 
            // dtLasRptSbmsDt
            // 
            // 
            // 
            // 
            this.dtLasRptSbmsDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtLasRptSbmsDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLasRptSbmsDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtLasRptSbmsDt.ButtonDropDown.Visible = true;
            this.dtLasRptSbmsDt.FocusHighlightEnabled = true;
            this.dtLasRptSbmsDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtLasRptSbmsDt.IsPopupCalendarOpen = false;
            this.dtLasRptSbmsDt.Location = new System.Drawing.Point(569, 10);
            // 
            // 
            // 
            this.dtLasRptSbmsDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLasRptSbmsDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtLasRptSbmsDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtLasRptSbmsDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtLasRptSbmsDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLasRptSbmsDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtLasRptSbmsDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtLasRptSbmsDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLasRptSbmsDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtLasRptSbmsDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLasRptSbmsDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtLasRptSbmsDt.Name = "dtLasRptSbmsDt";
            this.dtLasRptSbmsDt.Size = new System.Drawing.Size(116, 23);
            this.dtLasRptSbmsDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtLasRptSbmsDt.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelEx21);
            this.panel2.Controls.Add(this.panelEx22);
            this.panel2.Controls.Add(this.panelEx23);
            this.panel2.Controls.Add(this.panelEx24);
            this.panel2.Controls.Add(this.panelEx25);
            this.panel2.Controls.Add(this.txtInsured);
            this.panel2.Controls.Add(this.txtAcdtNo);
            this.panel2.Controls.Add(this.txtIsrdRegno);
            this.panel2.Controls.Add(this.txtSurvAsgnEmpName);
            this.panel2.Controls.Add(this.txtInsurChrg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(726, 46);
            this.panel2.TabIndex = 10199;
            // 
            // panelEx21
            // 
            this.panelEx21.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx21.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx21.Location = new System.Drawing.Point(0, 0);
            this.panelEx21.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx21.Name = "panelEx21";
            this.panelEx21.Size = new System.Drawing.Size(70, 23);
            this.panelEx21.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx21.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx21.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx21.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx21.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx21.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx21.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx21.Style.GradientAngle = 90;
            this.panelEx21.TabIndex = 10185;
            this.panelEx21.Text = "피보험자";
            // 
            // panelEx22
            // 
            this.panelEx22.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx22.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx22.Location = new System.Drawing.Point(220, 0);
            this.panelEx22.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx22.Name = "panelEx22";
            this.panelEx22.Size = new System.Drawing.Size(70, 23);
            this.panelEx22.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx22.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx22.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx22.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx22.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx22.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx22.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx22.Style.GradientAngle = 90;
            this.panelEx22.TabIndex = 10185;
            this.panelEx22.Text = "사고번호";
            // 
            // panelEx23
            // 
            this.panelEx23.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx23.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx23.Location = new System.Drawing.Point(500, 0);
            this.panelEx23.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx23.Name = "panelEx23";
            this.panelEx23.Size = new System.Drawing.Size(70, 23);
            this.panelEx23.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx23.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx23.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx23.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx23.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx23.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx23.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx23.Style.GradientAngle = 90;
            this.panelEx23.TabIndex = 10185;
            this.panelEx23.Text = "조사담당";
            // 
            // panelEx24
            // 
            this.panelEx24.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx24.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx24.Location = new System.Drawing.Point(0, 22);
            this.panelEx24.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx24.Name = "panelEx24";
            this.panelEx24.Size = new System.Drawing.Size(70, 23);
            this.panelEx24.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx24.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx24.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx24.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx24.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx24.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx24.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx24.Style.GradientAngle = 90;
            this.panelEx24.TabIndex = 10185;
            this.panelEx24.Text = "주민번호";
            // 
            // panelEx25
            // 
            this.panelEx25.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx25.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx25.Location = new System.Drawing.Point(500, 22);
            this.panelEx25.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx25.Name = "panelEx25";
            this.panelEx25.Size = new System.Drawing.Size(70, 23);
            this.panelEx25.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx25.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx25.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx25.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx25.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx25.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx25.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx25.Style.GradientAngle = 90;
            this.panelEx25.TabIndex = 10185;
            this.panelEx25.Text = "보상담당";
            // 
            // txtInsured
            // 
            this.txtInsured.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtInsured.Border.Class = "TextBoxBorder";
            this.txtInsured.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInsured.FocusHighlightEnabled = true;
            this.txtInsured.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtInsured.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtInsured.Location = new System.Drawing.Point(70, 0);
            this.txtInsured.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtInsured.Name = "txtInsured";
            this.txtInsured.Size = new System.Drawing.Size(151, 23);
            this.txtInsured.TabIndex = 3;
            // 
            // txtAcdtNo
            // 
            this.txtAcdtNo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtAcdtNo.Border.Class = "TextBoxBorder";
            this.txtAcdtNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAcdtNo.FocusHighlightEnabled = true;
            this.txtAcdtNo.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtAcdtNo.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtAcdtNo.Location = new System.Drawing.Point(289, 0);
            this.txtAcdtNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAcdtNo.Name = "txtAcdtNo";
            this.txtAcdtNo.Size = new System.Drawing.Size(212, 23);
            this.txtAcdtNo.TabIndex = 4;
            // 
            // txtSurvAsgnEmpName
            // 
            this.txtSurvAsgnEmpName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSurvAsgnEmpName.Border.Class = "TextBoxBorder";
            this.txtSurvAsgnEmpName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSurvAsgnEmpName.FocusHighlightEnabled = true;
            this.txtSurvAsgnEmpName.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtSurvAsgnEmpName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtSurvAsgnEmpName.Location = new System.Drawing.Point(569, 0);
            this.txtSurvAsgnEmpName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSurvAsgnEmpName.Name = "txtSurvAsgnEmpName";
            this.txtSurvAsgnEmpName.Size = new System.Drawing.Size(157, 23);
            this.txtSurvAsgnEmpName.TabIndex = 5;
            // 
            // txtIsrdRegno
            // 
            this.txtIsrdRegno.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtIsrdRegno.Border.Class = "TextBoxBorder";
            this.txtIsrdRegno.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtIsrdRegno.FocusHighlightEnabled = true;
            this.txtIsrdRegno.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtIsrdRegno.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtIsrdRegno.Location = new System.Drawing.Point(70, 22);
            this.txtIsrdRegno.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtIsrdRegno.Name = "txtIsrdRegno";
            this.txtIsrdRegno.Size = new System.Drawing.Size(431, 23);
            this.txtIsrdRegno.TabIndex = 6;
            // 
            // txtInsurChrg
            // 
            this.txtInsurChrg.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtInsurChrg.Border.Class = "TextBoxBorder";
            this.txtInsurChrg.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInsurChrg.FocusHighlightEnabled = true;
            this.txtInsurChrg.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtInsurChrg.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtInsurChrg.Location = new System.Drawing.Point(569, 22);
            this.txtInsurChrg.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtInsurChrg.Name = "txtInsurChrg";
            this.txtInsurChrg.Size = new System.Drawing.Size(157, 23);
            this.txtInsurChrg.TabIndex = 6;
            // 
            // ucMeritzPan1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucMeritzPan1";
            this.Size = new System.Drawing.Size(726, 89);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtSurvReqDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAcptDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLasRptSbmsDt)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private YLW_WebClient.CustomDateTimeInput dtSurvReqDt;
        private YLW_WebClient.CustomDateTimeInput dtAcptDt;
        private YLW_WebClient.CustomDateTimeInput dtLasRptSbmsDt;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.PanelEx panelEx21;
        private DevComponents.DotNetBar.PanelEx panelEx22;
        private DevComponents.DotNetBar.PanelEx panelEx23;
        private DevComponents.DotNetBar.PanelEx panelEx24;
        private DevComponents.DotNetBar.PanelEx panelEx25;
        private YLW_WebClient.CustomTextBox txtInsured;
        private YLW_WebClient.CustomTextBox txtAcdtNo;
        private YLW_WebClient.CustomTextBox txtIsrdRegno;
        private YLW_WebClient.CustomTextBox txtSurvAsgnEmpName;
        private YLW_WebClient.CustomTextBox txtInsurChrg;
    }
}
