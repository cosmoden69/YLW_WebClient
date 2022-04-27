namespace YLW_WebClient.CAA
{
    partial class HeungkukContractB
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
            this.pan_hide_00 = new System.Windows.Forms.Panel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtCltrCnts = new YLW_WebClient.CustomTextBox();
            this.iniInsurRegsAmt = new YLW_WebClient.CustomDoubleInput();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.btn_no00_01 = new DevComponents.DotNetBar.ButtonX();
            this.pan_hide_00.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iniInsurRegsAmt)).BeginInit();
            this.panelEx18.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.panelEx1);
            this.pan_hide_00.Controls.Add(this.txtCltrCnts);
            this.pan_hide_00.Controls.Add(this.iniInsurRegsAmt);
            this.pan_hide_00.Controls.Add(this.panelEx18);
            this.pan_hide_00.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_hide_00.Location = new System.Drawing.Point(0, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(726, 45);
            this.pan_hide_00.TabIndex = 10196;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(150, 45);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10194;
            this.panelEx1.Text = "청구담보";
            // 
            // txtCltrCnts
            // 
            this.txtCltrCnts.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCltrCnts.Border.Class = "TextBoxBorder";
            this.txtCltrCnts.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCltrCnts.FocusHighlightEnabled = true;
            this.txtCltrCnts.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtCltrCnts.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtCltrCnts.Location = new System.Drawing.Point(149, 0);
            this.txtCltrCnts.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCltrCnts.MinimumSize = new System.Drawing.Size(0, 45);
            this.txtCltrCnts.Name = "txtCltrCnts";
            this.txtCltrCnts.Size = new System.Drawing.Size(384, 45);
            this.txtCltrCnts.TabIndex = 0;
            this.txtCltrCnts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iniInsurRegsAmt
            // 
            // 
            // 
            // 
            this.iniInsurRegsAmt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniInsurRegsAmt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniInsurRegsAmt.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniInsurRegsAmt.DisplayFormat = "#,###";
            this.iniInsurRegsAmt.FocusHighlightEnabled = true;
            this.iniInsurRegsAmt.ForeColor = System.Drawing.Color.SteelBlue;
            this.iniInsurRegsAmt.Increment = 0D;
            this.iniInsurRegsAmt.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Center;
            this.iniInsurRegsAmt.Location = new System.Drawing.Point(532, 0);
            this.iniInsurRegsAmt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iniInsurRegsAmt.MinimumSize = new System.Drawing.Size(0, 45);
            this.iniInsurRegsAmt.Name = "iniInsurRegsAmt";
            this.iniInsurRegsAmt.Size = new System.Drawing.Size(150, 45);
            this.iniInsurRegsAmt.TabIndex = 1;
            this.iniInsurRegsAmt.ValueChanged += new System.EventHandler(this.PriceChange);
            this.iniInsurRegsAmt.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Right;
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Controls.Add(this.btn_no00_01);
            this.panelEx18.Location = new System.Drawing.Point(681, 0);
            this.panelEx18.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(44, 45);
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
            this.btn_no00_01.Location = new System.Drawing.Point(2, 15);
            this.btn_no00_01.Name = "btn_no00_01";
            this.btn_no00_01.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_no00_01.Size = new System.Drawing.Size(40, 19);
            this.btn_no00_01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_no00_01.TabIndex = 10195;
            this.btn_no00_01.TabStop = false;
            this.btn_no00_01.Text = "삭제";
            this.btn_no00_01.Click += new System.EventHandler(this.Button_Click);
            // 
            // HeungkukContractB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HeungkukContractB";
            this.Size = new System.Drawing.Size(726, 45);
            this.pan_hide_00.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iniInsurRegsAmt)).EndInit();
            this.panelEx18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pan_hide_00;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private YLW_WebClient.CustomTextBox txtCltrCnts;
        private YLW_WebClient.CustomDoubleInput iniInsurRegsAmt;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private DevComponents.DotNetBar.ButtonX btn_no00_01;
    }
}
