namespace YLW_WebClient.CAA
{
    partial class ucHyundaiPan1
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
            this.txtAcdtCnts = new YLW_WebClient.CAA.RichTextBox();
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
            this.panel1.Controls.Add(this.txtAcdtCnts);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 115);
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
            this.panelEx3.Location = new System.Drawing.Point(1, 22);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(68, 23);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx3.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10186;
            this.panelEx3.Text = "사고내용";
            // 
            // txtAcdtCnts
            // 
            this.txtAcdtCnts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcdtCnts.BackColor = System.Drawing.Color.White;
            this.txtAcdtCnts.bShowMenu = true;
            this.txtAcdtCnts.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAcdtCnts.Location = new System.Drawing.Point(0, 45);
            this.txtAcdtCnts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAcdtCnts.MaxInputLength = 0;
            this.txtAcdtCnts.MinHeight = 69;
            this.txtAcdtCnts.Name = "txtAcdtCnts";
            this.txtAcdtCnts.Size = new System.Drawing.Size(726, 69);
            this.txtAcdtCnts.TabIndex = 3;
            // 
            // ucHyundaiPan1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucHyundaiPan1";
            this.Size = new System.Drawing.Size(726, 115);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtAcdtDt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private CustomDateTimeInput dtAcdtDt;
        private YLW_WebClient.CustomMaskedTextBoxAdv txtAcdtTm;
        private YLW_WebClient.CustomTextBox txtAcdtAddressName;
        private RichTextBox txtAcdtCnts;
    }
}
