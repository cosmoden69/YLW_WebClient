namespace YLW_WebClient
{
    partial class CustomAddressInputer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomAddressInputer));
            this.txtAddr = new YLW_WebClient.CustomTextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAddr
            // 
            this.txtAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddr.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtAddr.Border.Class = "TextBoxBorder";
            this.txtAddr.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAddr.FocusHighlightEnabled = true;
            this.txtAddr.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtAddr.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtAddr.Location = new System.Drawing.Point(0, 0);
            this.txtAddr.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(543, 21);
            this.txtAddr.TabIndex = 0;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.Location = new System.Drawing.Point(543, -1);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(24, 20);
            this.btnFind.TabIndex = 1;
            this.btnFind.TabStop = false;
            this.btnFind.UseVisualStyleBackColor = true;
            // 
            // CustomAddressInputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.btnFind);
            this.Name = "CustomAddressInputer";
            this.Size = new System.Drawing.Size(566, 23);
            this.ResumeLayout(false);

        }

        #endregion
        private YLW_WebClient.CustomTextBox txtAddr;
        private System.Windows.Forms.Button btnFind;
    }
}
