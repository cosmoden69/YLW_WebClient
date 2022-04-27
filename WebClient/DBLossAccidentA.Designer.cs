namespace YLW_WebClient.CAA
{
    partial class DBLossAccidentA
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
            this.dtiCureFrDt = new YLW_WebClient.CustomDateTimeInput();
            this.txtCureCnts = new YLW_WebClient.CAA.RichTextBox();
            this.txtVstHosp = new YLW_WebClient.CAA.RichTextBox();
            this.pan_hide_00 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.lblno00_01 = new System.Windows.Forms.Label();
            this.lblno00_03 = new System.Windows.Forms.Label();
            this.lblno00_04 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureFrDt)).BeginInit();
            this.pan_hide_00.SuspendLayout();
            this.panelEx18.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtiCureFrDt
            // 
            // 
            // 
            // 
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
            this.dtiCureFrDt.TabIndex = 0;
            this.dtiCureFrDt.ValueChanged += new System.EventHandler(this.Date_Change);
            // 
            // txtCureCnts
            // 
            this.txtCureCnts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCureCnts.BackColor = System.Drawing.Color.White;
            this.txtCureCnts.bShowMenu = true;
            this.txtCureCnts.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCureCnts.Location = new System.Drawing.Point(112, 0);
            this.txtCureCnts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCureCnts.MaxInputLength = 0;
            this.txtCureCnts.MinHeight = 23;
            this.txtCureCnts.Name = "txtCureCnts";
            this.txtCureCnts.Size = new System.Drawing.Size(398, 23);
            this.txtCureCnts.TabIndex = 1;
            // 
            // txtVstHosp
            // 
            this.txtVstHosp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVstHosp.BackColor = System.Drawing.Color.White;
            this.txtVstHosp.bShowMenu = false;
            this.txtVstHosp.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtVstHosp.Location = new System.Drawing.Point(508, 0);
            this.txtVstHosp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVstHosp.MaxInputLength = 0;
            this.txtVstHosp.MinHeight = 23;
            this.txtVstHosp.Name = "txtVstHosp";
            this.txtVstHosp.Size = new System.Drawing.Size(184, 23);
            this.txtVstHosp.TabIndex = 2;
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.dtiCureFrDt);
            this.pan_hide_00.Controls.Add(this.txtCureCnts);
            this.pan_hide_00.Controls.Add(this.txtVstHosp);
            this.pan_hide_00.Controls.Add(this.panelEx18);
            this.pan_hide_00.Controls.Add(this.lblno00_01);
            this.pan_hide_00.Controls.Add(this.lblno00_03);
            this.pan_hide_00.Controls.Add(this.lblno00_04);
            this.pan_hide_00.Location = new System.Drawing.Point(1, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(724, 23);
            this.pan_hide_00.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pan_hide_00.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.pan_hide_00.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pan_hide_00.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pan_hide_00.Style.GradientAngle = 90;
            this.pan_hide_00.TabIndex = 10196;
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Controls.Add(this.btnDel);
            this.panelEx18.Location = new System.Drawing.Point(691, 0);
            this.panelEx18.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(33, 23);
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
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
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
            this.lblno00_01.Size = new System.Drawing.Size(113, 23);
            this.lblno00_01.TabIndex = 10196;
            this.lblno00_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_01.Visible = false;
            // 
            // lblno00_03
            // 
            this.lblno00_03.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_03.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_03.Location = new System.Drawing.Point(112, 0);
            this.lblno00_03.Name = "lblno00_03";
            this.lblno00_03.Size = new System.Drawing.Size(398, 23);
            this.lblno00_03.TabIndex = 10196;
            this.lblno00_03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_03.Visible = false;
            // 
            // lblno00_04
            // 
            this.lblno00_04.BackColor = System.Drawing.Color.LightGreen;
            this.lblno00_04.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblno00_04.Location = new System.Drawing.Point(508, 0);
            this.lblno00_04.Name = "lblno00_04";
            this.lblno00_04.Size = new System.Drawing.Size(184, 23);
            this.lblno00_04.TabIndex = 10196;
            this.lblno00_04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblno00_04.Visible = false;
            // 
            // DBLossAccidentA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DBLossAccidentA";
            this.Size = new System.Drawing.Size(725, 23);
            ((System.ComponentModel.ISupportInitialize)(this.dtiCureFrDt)).EndInit();
            this.pan_hide_00.ResumeLayout(false);
            this.panelEx18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx pan_hide_00;
        private CustomDateTimeInput dtiCureFrDt;
        private YLW_WebClient.CAA.RichTextBox txtCureCnts;
        private YLW_WebClient.CAA.RichTextBox txtVstHosp;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private System.Windows.Forms.Label lblno00_01;
        private System.Windows.Forms.Label lblno00_03;
        private System.Windows.Forms.Label lblno00_04;
    }
}
