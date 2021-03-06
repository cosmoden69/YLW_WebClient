namespace YLW_WebClient.CAA
{
    partial class MGLossSmplContractBT
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
            this.iniSum = new YLW_WebClient.CustomDoubleInput();
            this.pan_hide_00.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iniSum)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.panelEx1);
            this.pan_hide_00.Controls.Add(this.iniSum);
            this.pan_hide_00.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_hide_00.Location = new System.Drawing.Point(0, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(725, 23);
            this.pan_hide_00.TabIndex = 10196;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(587, 23);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.WhiteSmoke;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelEx1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "합  계";
            // 
            // iniSum
            // 
            // 
            // 
            // 
            this.iniSum.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniSum.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniSum.DisplayFormat = "#,###";
            this.iniSum.FocusHighlightEnabled = true;
            this.iniSum.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Center;
            this.iniSum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.iniSum.ForeColor = System.Drawing.Color.SteelBlue;
            this.iniSum.Increment = 0D;
            this.iniSum.Location = new System.Drawing.Point(586, 0);
            this.iniSum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iniSum.Name = "iniSum";
            this.iniSum.Size = new System.Drawing.Size(139, 23);
            this.iniSum.TabIndex = 1;
            this.iniSum.TabStop = false;
            // 
            // MGLossSmplContractBT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MGLossSmplContractBT";
            this.Size = new System.Drawing.Size(725, 23);
            this.pan_hide_00.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iniSum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pan_hide_00;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private YLW_WebClient.CustomDoubleInput iniSum;
    }
}
