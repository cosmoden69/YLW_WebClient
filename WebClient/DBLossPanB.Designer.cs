namespace YLW_WebClient.CAA
{
    partial class DBLossPanB
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
            this.pnTitle1 = new DevComponents.DotNetBar.PanelEx();
            this.txtShrtCnts1 = new YLW_WebClient.CAA.RichTextBox();
            this.pan_hide_00 = new System.Windows.Forms.Panel();
            this.pan_hide_00.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTitle1
            // 
            this.pnTitle1.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnTitle1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnTitle1.Location = new System.Drawing.Point(0, 0);
            this.pnTitle1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnTitle1.Name = "panS3_Title1";
            this.pnTitle1.Size = new System.Drawing.Size(116, 46);
            this.pnTitle1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnTitle1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.pnTitle1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.pnTitle1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnTitle1.Style.BorderColor.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnTitle1.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pnTitle1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnTitle1.Style.GradientAngle = 90;
            this.pnTitle1.TabIndex = 10185;
            // 
            // txtShrtCnts1
            // 
            this.txtShrtCnts1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShrtCnts1.BackColor = System.Drawing.Color.White;
            this.txtShrtCnts1.bShowMenu = false;
            this.txtShrtCnts1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtShrtCnts1.Location = new System.Drawing.Point(115, 0);
            this.txtShrtCnts1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShrtCnts1.MaxInputLength = 0;
            this.txtShrtCnts1.MinHeight = 46;
            this.txtShrtCnts1.Name = "txtShrtCnts1";
            this.txtShrtCnts1.NewHeight = 46;
            this.txtShrtCnts1.Size = new System.Drawing.Size(610, 46);
            this.txtShrtCnts1.TabIndex = 0;
            // 
            // pan_hide_00
            // 
            this.pan_hide_00.BackColor = System.Drawing.Color.White;
            this.pan_hide_00.Controls.Add(this.pnTitle1);
            this.pan_hide_00.Controls.Add(this.txtShrtCnts1);
            this.pan_hide_00.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_hide_00.Location = new System.Drawing.Point(0, 0);
            this.pan_hide_00.Name = "pan_hide_00";
            this.pan_hide_00.Size = new System.Drawing.Size(725, 46);
            this.pan_hide_00.TabIndex = 10196;
            // 
            // DBLossPanB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pan_hide_00);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DBLossPanB";
            this.Size = new System.Drawing.Size(725, 46);
            this.pan_hide_00.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pan_hide_00;
        private DevComponents.DotNetBar.PanelEx pnTitle1;
        private YLW_WebClient.CAA.RichTextBox txtShrtCnts1;
    }
}
