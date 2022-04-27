namespace YLW_WebClient.CAA
{
    partial class ucMGLossPan2
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
            this.dtAcdtDt = new YLW_WebClient.CustomDateTimeInput();
            this.txtAcdtTm = new YLW_WebClient.CustomMaskedTextBoxAdv();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtAcdtAddressName = new YLW_WebClient.CustomTextBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txtAcdtCaus = new YLW_WebClient.CAA.RichTextBox();
            this.txtS111_LongCnts1 = new YLW_WebClient.CAA.RichTextBox();
            this.txtS111_LongCnts2 = new YLW_WebClient.CAA.RichTextBox();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx71 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx72 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx73 = new DevComponents.DotNetBar.PanelEx();
            this.txtS111_ShrtCnts1 = new YLW_WebClient.CustomTextBox();
            this.txtS111_ShrtCnts2 = new YLW_WebClient.CustomTextBox();
            this.txtS111_ShrtCnts3 = new YLW_WebClient.CustomTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtAcdtDt)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEx1);
            this.panel1.Controls.Add(this.dtAcdtDt);
            this.panel1.Controls.Add(this.txtAcdtTm);
            this.panel1.Controls.Add(this.panelEx2);
            this.panel1.Controls.Add(this.txtAcdtAddressName);
            this.panel1.Controls.Add(this.panelEx3);
            this.panel1.Controls.Add(this.txtAcdtCaus);
            this.panel1.Controls.Add(this.panelEx4);
            this.panel1.Controls.Add(this.txtS111_LongCnts1);
            this.panel1.Controls.Add(this.panelEx5);
            this.panel1.Controls.Add(this.txtS111_LongCnts2);
            this.panel1.Controls.Add(this.panelEx6);
            this.panel1.Controls.Add(this.panelEx71);
            this.panel1.Controls.Add(this.txtS111_ShrtCnts1);
            this.panel1.Controls.Add(this.panelEx72);
            this.panel1.Controls.Add(this.txtS111_ShrtCnts2);
            this.panel1.Controls.Add(this.panelEx73);
            this.panel1.Controls.Add(this.txtS111_ShrtCnts3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 352);
            this.panel1.TabIndex = 10197;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(70, 23);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10185;
            this.panelEx1.Text = "사고일시";
            // 
            // dtAcdtDt
            // 
            // 
            // 
            // 
            this.dtAcdtDt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtAcdtDt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcdtDt.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtAcdtDt.ButtonDropDown.Visible = true;
            this.dtAcdtDt.FocusHighlightEnabled = true;
            this.dtAcdtDt.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtAcdtDt.IsPopupCalendarOpen = false;
            this.dtAcdtDt.Location = new System.Drawing.Point(69, 0);
            // 
            // 
            // 
            this.dtAcdtDt.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtAcdtDt.MonthCalendar.BackgroundStyle.Class = "";
            this.dtAcdtDt.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtAcdtDt.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtAcdtDt.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcdtDt.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtAcdtDt.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtAcdtDt.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtAcdtDt.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtAcdtDt.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtAcdtDt.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtAcdtDt.Name = "dtAcdtDt";
            this.dtAcdtDt.Size = new System.Drawing.Size(116, 23);
            this.dtAcdtDt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtAcdtDt.TabIndex = 0;
            // 
            // txtAcdtTm
            // 
            this.txtAcdtTm.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtAcdtTm.BackgroundStyle.Class = "TextBoxBorder";
            this.txtAcdtTm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAcdtTm.FocusHighlightEnabled = true;
            this.txtAcdtTm.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAcdtTm.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtAcdtTm.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtAcdtTm.Location = new System.Drawing.Point(184, 0);
            this.txtAcdtTm.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAcdtTm.Mask = "90:00";
            this.txtAcdtTm.Name = "txtAcdtTm";
            this.txtAcdtTm.Size = new System.Drawing.Size(60, 23);
            this.txtAcdtTm.TabIndex = 1;
            this.txtAcdtTm.Text = "";
            this.txtAcdtTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAcdtTm.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Location = new System.Drawing.Point(243, 0);
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
            this.panelEx2.Text = "사고장소";
            // 
            // txtAcdtAddressName
            // 
            this.txtAcdtAddressName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtAcdtAddressName.Border.Class = "TextBoxBorder";
            this.txtAcdtAddressName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAcdtAddressName.FocusHighlightEnabled = true;
            this.txtAcdtAddressName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAcdtAddressName.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtAcdtAddressName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtAcdtAddressName.Location = new System.Drawing.Point(312, 0);
            this.txtAcdtAddressName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAcdtAddressName.Name = "txtAcdtAddressName";
            this.txtAcdtAddressName.Size = new System.Drawing.Size(414, 23);
            this.txtAcdtAddressName.TabIndex = 2;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Location = new System.Drawing.Point(1, 23);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(68, 23);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.None;
            this.panelEx3.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx3.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10186;
            this.panelEx3.Text = "사고 경위";
            // 
            // txtAcdtCaus
            // 
            this.txtAcdtCaus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcdtCaus.BackColor = System.Drawing.Color.White;
            this.txtAcdtCaus.bShowMenu = true;
            this.txtAcdtCaus.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAcdtCaus.Location = new System.Drawing.Point(0, 46);
            this.txtAcdtCaus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAcdtCaus.MaxInputLength = 0;
            this.txtAcdtCaus.MinHeight = 69;
            this.txtAcdtCaus.Name = "txtAcdtCaus";
            this.txtAcdtCaus.NewHeight = 71;
            this.txtAcdtCaus.Size = new System.Drawing.Size(726, 71);
            this.txtAcdtCaus.TabIndex = 3;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Location = new System.Drawing.Point(1, 117);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(68, 23);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx4.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.None;
            this.panelEx4.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx4.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 10186;
            this.panelEx4.Text = "청구 경위";
            // 
            // txtS111_LongCnts1
            // 
            this.txtS111_LongCnts1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtS111_LongCnts1.BackColor = System.Drawing.Color.White;
            this.txtS111_LongCnts1.bShowMenu = true;
            this.txtS111_LongCnts1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS111_LongCnts1.Location = new System.Drawing.Point(0, 140);
            this.txtS111_LongCnts1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtS111_LongCnts1.MaxInputLength = 0;
            this.txtS111_LongCnts1.MinHeight = 69;
            this.txtS111_LongCnts1.Name = "txtS111_LongCnts1";
            this.txtS111_LongCnts1.NewHeight = 71;
            this.txtS111_LongCnts1.Size = new System.Drawing.Size(726, 71);
            this.txtS111_LongCnts1.TabIndex = 4;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Location = new System.Drawing.Point(1, 211);
            this.panelEx5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(68, 23);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx5.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.None;
            this.panelEx5.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx5.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 10186;
            this.panelEx5.Text = "진단명";
            // 
            // txtS111_LongCnts2
            // 
            this.txtS111_LongCnts2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtS111_LongCnts2.BackColor = System.Drawing.Color.White;
            this.txtS111_LongCnts2.bShowMenu = true;
            this.txtS111_LongCnts2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS111_LongCnts2.Location = new System.Drawing.Point(0, 234);
            this.txtS111_LongCnts2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtS111_LongCnts2.MaxInputLength = 0;
            this.txtS111_LongCnts2.MinHeight = 69;
            this.txtS111_LongCnts2.Name = "txtS111_LongCnts2";
            this.txtS111_LongCnts2.NewHeight = 71;
            this.txtS111_LongCnts2.Size = new System.Drawing.Size(726, 71);
            this.txtS111_LongCnts2.TabIndex = 5;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Location = new System.Drawing.Point(1, 305);
            this.panelEx6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(68, 23);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx6.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.None;
            this.panelEx6.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx6.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 10186;
            this.panelEx6.Text = "면담 일시";
            // 
            // panelEx71
            // 
            this.panelEx71.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx71.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx71.Location = new System.Drawing.Point(1, 328);
            this.panelEx71.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx71.Name = "panelEx71";
            this.panelEx71.Size = new System.Drawing.Size(80, 23);
            this.panelEx71.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx71.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx71.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx71.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx71.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx71.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx71.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx71.Style.GradientAngle = 90;
            this.panelEx71.TabIndex = 10186;
            this.panelEx71.Text = "1차";
            // 
            // txtS111_ShrtCnts1
            // 
            this.txtS111_ShrtCnts1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtS111_ShrtCnts1.Border.Class = "TextBoxBorder";
            this.txtS111_ShrtCnts1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtS111_ShrtCnts1.FocusHighlightEnabled = true;
            this.txtS111_ShrtCnts1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS111_ShrtCnts1.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtS111_ShrtCnts1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtS111_ShrtCnts1.Location = new System.Drawing.Point(80, 328);
            this.txtS111_ShrtCnts1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtS111_ShrtCnts1.Name = "txtS111_ShrtCnts1";
            this.txtS111_ShrtCnts1.Size = new System.Drawing.Size(162, 23);
            this.txtS111_ShrtCnts1.TabIndex = 6;
            // 
            // panelEx72
            // 
            this.panelEx72.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx72.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx72.Location = new System.Drawing.Point(241, 328);
            this.panelEx72.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx72.Name = "panelEx72";
            this.panelEx72.Size = new System.Drawing.Size(80, 23);
            this.panelEx72.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx72.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx72.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx72.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx72.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx72.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx72.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx72.Style.GradientAngle = 90;
            this.panelEx72.TabIndex = 10186;
            this.panelEx72.Text = "2차";
            // 
            // txtS111_ShrtCnts2
            // 
            this.txtS111_ShrtCnts2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtS111_ShrtCnts2.Border.Class = "TextBoxBorder";
            this.txtS111_ShrtCnts2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtS111_ShrtCnts2.FocusHighlightEnabled = true;
            this.txtS111_ShrtCnts2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS111_ShrtCnts2.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtS111_ShrtCnts2.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtS111_ShrtCnts2.Location = new System.Drawing.Point(320, 328);
            this.txtS111_ShrtCnts2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtS111_ShrtCnts2.Name = "txtS111_ShrtCnts2";
            this.txtS111_ShrtCnts2.Size = new System.Drawing.Size(162, 23);
            this.txtS111_ShrtCnts2.TabIndex = 7;
            // 
            // panelEx73
            // 
            this.panelEx73.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx73.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx73.Location = new System.Drawing.Point(481, 328);
            this.panelEx73.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx73.Name = "panelEx73";
            this.panelEx73.Size = new System.Drawing.Size(80, 23);
            this.panelEx73.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx73.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx73.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx73.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx73.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx73.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx73.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx73.Style.GradientAngle = 90;
            this.panelEx73.TabIndex = 10186;
            this.panelEx73.Text = "3차";
            // 
            // txtS111_ShrtCnts3
            // 
            this.txtS111_ShrtCnts3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtS111_ShrtCnts3.Border.Class = "TextBoxBorder";
            this.txtS111_ShrtCnts3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtS111_ShrtCnts3.FocusHighlightEnabled = true;
            this.txtS111_ShrtCnts3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS111_ShrtCnts3.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtS111_ShrtCnts3.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtS111_ShrtCnts3.Location = new System.Drawing.Point(560, 328);
            this.txtS111_ShrtCnts3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtS111_ShrtCnts3.Name = "txtS111_ShrtCnts3";
            this.txtS111_ShrtCnts3.Size = new System.Drawing.Size(164, 23);
            this.txtS111_ShrtCnts3.TabIndex = 8;
            // 
            // ucMGLossPan2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucMGLossPan2";
            this.Size = new System.Drawing.Size(726, 352);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtAcdtDt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.PanelEx panelEx71;
        private DevComponents.DotNetBar.PanelEx panelEx72;
        private DevComponents.DotNetBar.PanelEx panelEx73;
        private CustomDateTimeInput dtAcdtDt;
        private YLW_WebClient.CustomMaskedTextBoxAdv txtAcdtTm;
        private YLW_WebClient.CustomTextBox txtAcdtAddressName;
        private RichTextBox txtAcdtCaus;
        private RichTextBox txtS111_LongCnts1;
        private RichTextBox txtS111_LongCnts2;
        private YLW_WebClient.CustomTextBox txtS111_ShrtCnts1;
        private YLW_WebClient.CustomTextBox txtS111_ShrtCnts2;
        private YLW_WebClient.CustomTextBox txtS111_ShrtCnts3;
    }
}
