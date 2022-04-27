namespace YLW_WebClient.CAA
{
    partial class DBLifeDetail1A
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
            this.txtShrtCnts1 = new YLW_WebClient.CustomTextBox();
            this.iniAmt1 = new YLW_WebClient.CustomDoubleInput();
            this.txtShrtCnts2 = new YLW_WebClient.CustomTextBox();
            this.dtiShrtCnts3 = new YLW_WebClient.CustomDateTimeInput();
            this.txtLongCnts1 = new YLW_WebClient.CAA.RichTextBox();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.btn_no00_01 = new DevComponents.DotNetBar.ButtonX();
            this.pan_hide_00 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.iniAmt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiShrtCnts3)).BeginInit();
            this.panelEx18.SuspendLayout();
            this.pan_hide_00.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShrtCnts1
            // 
            this.txtShrtCnts1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtShrtCnts1.Border.Class = "TextBoxBorder";
            this.txtShrtCnts1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtShrtCnts1.FocusHighlightEnabled = true;
            this.txtShrtCnts1.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtShrtCnts1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtShrtCnts1.Location = new System.Drawing.Point(0, 0);
            this.txtShrtCnts1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtShrtCnts1.Name = "txtShrtCnts1";
            this.txtShrtCnts1.Size = new System.Drawing.Size(120, 23);
            this.txtShrtCnts1.TabIndex = 0;
            this.txtShrtCnts1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShrtCnts1.Multiline = true;
            // 
            // iniAmt1
            // 
            // 
            // 
            // 
            this.iniAmt1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniAmt1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniAmt1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniAmt1.DisplayFormat = "#,###";
            this.iniAmt1.FocusHighlightEnabled = true;
            this.iniAmt1.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Center;
            this.iniAmt1.ForeColor = System.Drawing.Color.SteelBlue;
            this.iniAmt1.Increment = 0D;
            this.iniAmt1.Location = new System.Drawing.Point(119, 0);
            this.iniAmt1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iniAmt1.Name = "iniAmt1";
            this.iniAmt1.Size = new System.Drawing.Size(80, 23);
            this.iniAmt1.TabIndex = 1;
            this.iniAmt1.ValueChanged += new System.EventHandler(this.PriceChange);
            // 
            // txtShrtCnts2
            // 
            this.txtShrtCnts2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtShrtCnts2.Border.Class = "TextBoxBorder";
            this.txtShrtCnts2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtShrtCnts2.FocusHighlightEnabled = true;
            this.txtShrtCnts2.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtShrtCnts2.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtShrtCnts2.Location = new System.Drawing.Point(198, 0);
            this.txtShrtCnts2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtShrtCnts2.Name = "txtShrtCnts2";
            this.txtShrtCnts2.Size = new System.Drawing.Size(120, 23);
            this.txtShrtCnts2.TabIndex = 3;
            this.txtShrtCnts2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShrtCnts2.Multiline = true;
            // 
            // dtiShrtCnts3
            // 
            // 
            // 
            // 
            this.dtiShrtCnts3.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiShrtCnts3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiShrtCnts3.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiShrtCnts3.ButtonDropDown.Visible = true;
            this.dtiShrtCnts3.FocusHighlightEnabled = true;
            this.dtiShrtCnts3.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtiShrtCnts3.IsPopupCalendarOpen = false;
            this.dtiShrtCnts3.Location = new System.Drawing.Point(317, 0);
            // 
            // 
            // 
            this.dtiShrtCnts3.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiShrtCnts3.MonthCalendar.BackgroundStyle.Class = "";
            this.dtiShrtCnts3.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dtiShrtCnts3.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtiShrtCnts3.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiShrtCnts3.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtiShrtCnts3.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiShrtCnts3.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiShrtCnts3.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtiShrtCnts3.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiShrtCnts3.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiShrtCnts3.Name = "dtiShrtCnts3";
            this.dtiShrtCnts3.Size = new System.Drawing.Size(120, 23);
            this.dtiShrtCnts3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiShrtCnts3.TabIndex = 4;
            // 
            // txtLongCnts1
            // 
            this.txtLongCnts1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLongCnts1.BackColor = System.Drawing.Color.White;
            this.txtLongCnts1.bShowMenu = true;
            this.txtLongCnts1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLongCnts1.Location = new System.Drawing.Point(436, 0);
            this.txtLongCnts1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLongCnts1.MaxInputLength = 0;
            this.txtLongCnts1.Name = "txtLongCnts1";
            this.txtLongCnts1.Size = new System.Drawing.Size(250, 23);
            this.txtLongCnts1.TabIndex = 5;
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Controls.Add(this.btn_no00_01);
            this.panelEx18.Location = new System.Drawing.Point(685, 0);
            this.panelEx18.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(40, 23);
            this.panelEx18.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx18.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx18.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx18.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx18.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx18.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx18.Style.GradientAngle = 90;
            this.panelEx18.TabIndex = 10195;
            // 
            // btn_no00_01
            // 
            this.btn_no00_01.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_no00_01.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta;
            this.btn_no00_01.ForeColor = System.Drawing.Color.Crimson;
            this.btn_no00_01.Location = new System.Drawing.Point(2, 2);
            this.btn_no00_01.Name = "btn_no00_01";
            this.btn_no00_01.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_no00_01.Size = new System.Drawing.Size(36, 19);
            this.btn_no00_01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_no00_01.TabIndex = 10195;
            this.btn_no00_01.TabStop = false;
            this.btn_no00_01.Text = "삭제";
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.txtShrtCnts1);
            this.pan_hide_00.Controls.Add(this.iniAmt1);
            this.pan_hide_00.Controls.Add(this.txtShrtCnts2);
            this.pan_hide_00.Controls.Add(this.dtiShrtCnts3);
            this.pan_hide_00.Controls.Add(this.txtLongCnts1);
            this.pan_hide_00.Controls.Add(this.panelEx18);
            this.pan_hide_00.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_hide_00.Location = new System.Drawing.Point(0, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(725, 23);
            this.pan_hide_00.TabIndex = 10196;
            // 
            // DBLifeDetail1A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DBLifeDetail1A";
            this.Size = new System.Drawing.Size(725, 23);
            ((System.ComponentModel.ISupportInitialize)(this.dtiShrtCnts3)).EndInit();
            this.panelEx18.ResumeLayout(false);
            this.pan_hide_00.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pan_hide_00;
        private DevComponents.DotNetBar.ButtonX btn_no00_01;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private YLW_WebClient.CustomTextBox txtShrtCnts1;
        private YLW_WebClient.CustomDoubleInput iniAmt1;
        private YLW_WebClient.CustomTextBox txtShrtCnts2;
        private YLW_WebClient.CustomDateTimeInput dtiShrtCnts3;
        private YLW_WebClient.CAA.RichTextBox txtLongCnts1;
    }
}
