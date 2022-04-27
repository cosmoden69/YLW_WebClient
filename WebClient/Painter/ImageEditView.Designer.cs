namespace YLW_WebClient.Painter
{
    partial class ImageEditView
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new YLW_WebClient.Painter.CustomPanel();
            this.sheet1 = new YLW_WebClient.Painter.MySheet();
            this.toolBar1 = new YLW_WebClient.Painter.ToolBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "DTF 파일 저장하기";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Title = "DTF 파일 불러오기";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sheet1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 620);
            this.panel1.TabIndex = 5;
            // 
            // sheet1
            // 
            this.sheet1.BackColor = System.Drawing.SystemColors.Control;
            this.sheet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheet1.CanvasImage = null;
            this.sheet1.Location = new System.Drawing.Point(3, 3);
            this.sheet1.Name = "sheet1";
            this.sheet1.Size = new System.Drawing.Size(800, 600);
            this.sheet1.TabIndex = 0;
            // 
            // toolBar1
            // 
            this.toolBar1.BackColor = System.Drawing.SystemColors.Control;
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(1058, 108);
            this.toolBar1.TabIndex = 0;
            // 
            // ImageEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 728);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.KeyPreview = true;
            this.Name = "ImageEditView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "사진보기";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolBar toolBar1;
        private CustomPanel panel1;
        private MySheet sheet1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

