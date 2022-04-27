namespace YLW_WebClient.CAA
{
    partial class accidentA
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
            this.dti_no00_01 = new YLW_WebClient.CustomDateTimeInput();
            this.ini_no00_01 = new YLW_WebClient.CustomTextBox();
            this.txt_no00_01 = new YLW_WebClient.CAA.RichTextBox();
            this.txt_no00_02 = new YLW_WebClient.CustomTextBox();
            this.pan_hide_00 = new System.Windows.Forms.Panel();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.btn_no00_01 = new DevComponents.DotNetBar.ButtonX();
            this.lblno00_01 = new System.Windows.Forms.Label();
            this.lblno00_02 = new System.Windows.Forms.Label();
            this.lblno00_03 = new System.Windows.Forms.Label();
            this.lblno00_04 = new System.Windows.Forms.Label();
            this.lblno00_05 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dti_no00_01)).BeginInit();
            this.pan_hide_00.SuspendLayout();
            this.panelEx18.SuspendLayout();
            this.SuspendLayout();
            // 
            // dti_no00_01
            // 
            // 
            // 
            // 
            this.dti_no00_01.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dti_no00_01.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dti_no00_01.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dti_no00_01.ButtonDropDown.Visible = true;
            this.dti_no00_01.FocusHighlightEnabled = true;
            this.dti_no00_01.ForeColor = System.Drawing.Color.SteelBlue;
            this.dti_no00_01.IsPopupCalendarOpen = false;
            this.dti_no00_01.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dti_no00_01.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dti_no00_01.MonthCalendar.BackgroundStyle.Class = "";
            this.dti_no00_01.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.dti_no00_01.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dti_no00_01.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dti_no00_01.MonthCalendar.DisplayMonth = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dti_no00_01.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dti_no00_01.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dti_no00_01.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dti_no00_01.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dti_no00_01.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dti_no00_01.Name = "dti_no00_01";
            this.dti_no00_01.Size = new System.Drawing.Size(113, 23);
            this.dti_no00_01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dti_no00_01.TabIndex = 0;
            this.dti_no00_01.ValueChanged += new System.EventHandler(this.Date_Change);
            // 
            // ini_no00_01
            // 
            this.ini_no00_01.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.ini_no00_01.Border.Class = "TextBoxBorder";
            this.ini_no00_01.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ini_no00_01.FocusHighlightEnabled = true;
            this.ini_no00_01.ForeColor = System.Drawing.Color.SteelBlue;
            this.ini_no00_01.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.ini_no00_01.Location = new System.Drawing.Point(112, 0);
            this.ini_no00_01.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ini_no00_01.Name = "ini_no00_01";
            this.ini_no00_01.Size = new System.Drawing.Size(50, 23);
            this.ini_no00_01.TabIndex = 1;
            this.ini_no00_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ini_no00_01.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // txt_no00_01
            // 
            this.txt_no00_01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_no00_01.BackColor = System.Drawing.Color.White;
            this.txt_no00_01.bShowMenu = true;
            this.txt_no00_01.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_no00_01.Location = new System.Drawing.Point(161, 1);
            this.txt_no00_01.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_no00_01.MaxInputLength = 0;
            this.txt_no00_01.Name = "txt_no00_01";
            this.txt_no00_01.Size = new System.Drawing.Size(386, 21);
            this.txt_no00_01.TabIndex = 2;
            // 
            // txt_no00_02
            // 
            this.txt_no00_02.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txt_no00_02.Border.Class = "TextBoxBorder";
            this.txt_no00_02.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_no00_02.FocusHighlightEnabled = true;
            this.txt_no00_02.ForeColor = System.Drawing.Color.SteelBlue;
            this.txt_no00_02.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_no00_02.Location = new System.Drawing.Point(546, 0);
            this.txt_no00_02.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txt_no00_02.Name = "txt_no00_02";
            this.txt_no00_02.Size = new System.Drawing.Size(140, 23);
            this.txt_no00_02.TabIndex = 3;
            this.txt_no00_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_no00_02.TextChanged += new System.EventHandler(this.Text_Change);
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.dti_no00_01);
            this.pan_hide_00.Controls.Add(this.txt_no00_01);
            this.pan_hide_00.Controls.Add(this.txt_no00_02);
            this.pan_hide_00.Controls.Add(this.ini_no00_01);
            this.pan_hide_00.Controls.Add(this.panelEx18);
            this.pan_hide_00.Controls.Add(this.lblno00_01);
            this.pan_hide_00.Controls.Add(this.lblno00_02);
            this.pan_hide_00.Controls.Add(this.lblno00_03);
            this.pan_hide_00.Controls.Add(this.lblno00_04);
            this.pan_hide_00.Controls.Add(this.lblno00_05);
            this.pan_hide_00.Location = new System.Drawing.Point(1, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(723, 23);
            this.pan_hide_00.TabIndex = 10196;
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Controls.Add(this.btn_no00_01);
            this.panelEx18.Location = new System.Drawing.Point(685, 0);
            this.panelEx18.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(39, 23);
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
            this.btn_no00_01.Size = new System.Drawing.Size(35, 19);
            this.btn_no00_01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_no00_01.TabIndex = 10195;
            this.btn_no00_01.TabStop = false;
            this.btn_no00_01.Text = "삭제";
            this.btn_no00_01.Click += new System.EventHandler(this.Button_Click);
            // 
            // lblno00_01
            // 
            this.lblno00_01.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_01.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_01.Location = new System.Drawing.Point(0, 0);
            this.lblno00_01.Name = "lblno00_01";
            this.lblno00_01.Size = new System.Drawing.Size(113, 23);
            this.lblno00_01.TabIndex = 10196;
            this.lblno00_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_01.Visible = false;
            // 
            // lblno00_02
            // 
            this.lblno00_02.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_02.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_02.Location = new System.Drawing.Point(112, 0);
            this.lblno00_02.Name = "lblno00_02";
            this.lblno00_02.Size = new System.Drawing.Size(50, 23);
            this.lblno00_02.TabIndex = 10196;
            this.lblno00_02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_02.Visible = false;
            // 
            // lblno00_03
            // 
            this.lblno00_03.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_03.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_03.Location = new System.Drawing.Point(161, 0);
            this.lblno00_03.Name = "lblno00_03";
            this.lblno00_03.Size = new System.Drawing.Size(386, 23);
            this.lblno00_03.TabIndex = 10196;
            this.lblno00_03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_03.Visible = false;
            // 
            // lblno00_04
            // 
            this.lblno00_04.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_04.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_04.Location = new System.Drawing.Point(546, 0);
            this.lblno00_04.Name = "lblno00_04";
            this.lblno00_04.Size = new System.Drawing.Size(140, 23);
            this.lblno00_04.TabIndex = 10196;
            this.lblno00_04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_04.Visible = false;
            // 
            // lblno00_05
            // 
            this.lblno00_05.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_05.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_05.Location = new System.Drawing.Point(685, 0);
            this.lblno00_05.Name = "lblno00_05";
            this.lblno00_05.Size = new System.Drawing.Size(39, 23);
            this.lblno00_05.TabIndex = 10196;
            this.lblno00_05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_05.Visible = false;
            // 
            // accidentA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "accidentA";
            this.Size = new System.Drawing.Size(725, 23);
            ((System.ComponentModel.ISupportInitialize)(this.dti_no00_01)).EndInit();
            this.pan_hide_00.ResumeLayout(false);
            this.panelEx18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pan_hide_00;
        private CustomDateTimeInput dti_no00_01;
        private YLW_WebClient.CustomTextBox ini_no00_01;
        private YLW_WebClient.CAA.RichTextBox txt_no00_01;
        private YLW_WebClient.CustomTextBox txt_no00_02;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private DevComponents.DotNetBar.ButtonX btn_no00_01;
        private System.Windows.Forms.Label lblno00_01;
        private System.Windows.Forms.Label lblno00_02;
        private System.Windows.Forms.Label lblno00_03;
        private System.Windows.Forms.Label lblno00_04;
        private System.Windows.Forms.Label lblno00_05;
    }
}
