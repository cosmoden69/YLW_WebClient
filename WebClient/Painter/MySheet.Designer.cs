namespace YLW_WebClient.Painter
{
    partial class MySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySheet));
            this.pictureBox_ReSize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ReSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_ReSize
            // 
            this.pictureBox_ReSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_ReSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox_ReSize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBox_ReSize.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_ReSize.Image")));
            this.pictureBox_ReSize.Location = new System.Drawing.Point(388, 261);
            this.pictureBox_ReSize.Name = "pictureBox_ReSize";
            this.pictureBox_ReSize.Size = new System.Drawing.Size(17, 17);
            this.pictureBox_ReSize.TabIndex = 1;
            this.pictureBox_ReSize.TabStop = false;
            // 
            // Sheet
            // 
            this.Controls.Add(this.pictureBox_ReSize);
            this.Name = "Sheet";
            this.Size = new System.Drawing.Size(405, 278);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ReSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_ReSize;
    }
}
