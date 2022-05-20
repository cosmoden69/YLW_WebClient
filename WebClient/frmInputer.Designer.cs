namespace YLW_WebClient.CAA
{
    partial class frmInputer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInputer));
            this.Panel = new System.Windows.Forms.Panel();
            this.btn_SmplQry = new DevComponents.DotNetBar.ButtonX();
            this.btn_SmplSave = new DevComponents.DotNetBar.ButtonX();
            this.btn_Find = new DevComponents.DotNetBar.ButtonX();
            this.btn_Exit = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btn_Save1 = new DevComponents.DotNetBar.ButtonX();
            this.btn_HSave = new DevComponents.DotNetBar.ButtonX();
            this.btn_HQuery = new DevComponents.DotNetBar.ButtonX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panLast = new System.Windows.Forms.Panel();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.btn_Save2 = new DevComponents.DotNetBar.ButtonX();
            this.ucInputer1 = new YLW_WebClient.CAA.ucInputer();
            this.Panel.SuspendLayout();
            this.panLast.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.BackColor = System.Drawing.Color.White;
            this.Panel.Controls.Add(this.btn_SmplQry);
            this.Panel.Controls.Add(this.btn_SmplSave);
            this.Panel.Controls.Add(this.btn_Find);
            this.Panel.Controls.Add(this.btn_Exit);
            this.Panel.Controls.Add(this.panelEx3);
            this.Panel.Controls.Add(this.btn_Save1);
            this.Panel.Controls.Add(this.btn_HSave);
            this.Panel.Controls.Add(this.btn_HQuery);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(758, 47);
            this.Panel.TabIndex = 0;
            // 
            // btn_SmplQry
            // 
            this.btn_SmplQry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_SmplQry.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_SmplQry.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_SmplQry.Location = new System.Drawing.Point(230, 3);
            this.btn_SmplQry.Name = "btn_SmplQry";
            this.btn_SmplQry.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_SmplQry.Size = new System.Drawing.Size(70, 34);
            this.btn_SmplQry.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_SmplQry.TabIndex = 10161;
            this.btn_SmplQry.TabStop = false;
            this.btn_SmplQry.Text = "보고서\n사례조회";
            this.btn_SmplQry.Click += new System.EventHandler(this.btn_SmplQry_Click);
            // 
            // btn_SmplSave
            // 
            this.btn_SmplSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_SmplSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_SmplSave.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_SmplSave.Location = new System.Drawing.Point(150, 3);
            this.btn_SmplSave.Name = "btn_SmplSave";
            this.btn_SmplSave.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_SmplSave.Size = new System.Drawing.Size(70, 34);
            this.btn_SmplSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_SmplSave.TabIndex = 10161;
            this.btn_SmplSave.TabStop = false;
            this.btn_SmplSave.Text = "보고서\n사례저장";
            this.btn_SmplSave.Click += new System.EventHandler(this.btn_SmplSave_Click);
            // 
            // btn_Find
            // 
            this.btn_Find.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Find.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Find.Image = ((System.Drawing.Image)(resources.GetObject("btn_Find.Image")));
            this.btn_Find.Location = new System.Drawing.Point(10, 3);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_Find.Size = new System.Drawing.Size(80, 34);
            this.btn_Find.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Find.TabIndex = 10161;
            this.btn_Find.TabStop = false;
            this.btn_Find.Text = "다시 조회";
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Exit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exit.Image")));
            this.btn_Exit.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_Exit.Location = new System.Drawing.Point(650, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_Exit.Size = new System.Drawing.Size(80, 34);
            this.btn_Exit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Exit.TabIndex = 10161;
            this.btn_Exit.TabStop = false;
            this.btn_Exit.Text = "화면 닫기";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Location = new System.Drawing.Point(8, 38);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(726, 7);
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.Etched;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10157;
            // 
            // btn_Save1
            // 
            this.btn_Save1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Save1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            //this.btn_Save1.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save1.Image")));
            this.btn_Save1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_Save1.Location = new System.Drawing.Point(399, 3);
            this.btn_Save1.Name = "btn_Save1";
            this.btn_Save1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_Save1.Size = new System.Drawing.Size(70, 34);
            this.btn_Save1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Save1.TabIndex = 25;
            this.btn_Save1.TabStop = false;
            this.btn_Save1.Text = "저 장";
            this.btn_Save1.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btn_HSave
            // 
            this.btn_HSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_HSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            //this.btn_HSave.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save1.Image")));
            this.btn_HSave.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_HSave.Location = new System.Drawing.Point(478, 3);
            this.btn_HSave.Name = "btn_HSave";
            this.btn_HSave.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_HSave.Size = new System.Drawing.Size(72, 34);
            this.btn_HSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_HSave.TabIndex = 24;
            this.btn_HSave.TabStop = false;
            this.btn_HSave.Text = "보고서 생성\n및 업로드";
            this.btn_HSave.Click += new System.EventHandler(this.Btn_HSave_Click);
            // 
            // btn_HQuery
            // 
            this.btn_HQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_HQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            //this.btn_HQuery.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save1.Image")));
            this.btn_HQuery.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_HQuery.Location = new System.Drawing.Point(560, 3);
            this.btn_HQuery.Name = "btn_HQuery";
            this.btn_HQuery.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btn_HQuery.Size = new System.Drawing.Size(70, 34);
            this.btn_HQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_HQuery.TabIndex = 23;
            this.btn_HQuery.TabStop = false;
            this.btn_HQuery.Text = "보고서\n이력 조회";
            this.btn_HQuery.Click += new System.EventHandler(this.Btn_HQuery_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 827);
            this.panel2.TabIndex = 963;
            // 
            // panLast
            // 
            this.panLast.Controls.Add(this.panelEx4);
            this.panLast.Controls.Add(this.btn_Save2);
            this.panLast.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panLast.Location = new System.Drawing.Point(5, 824);
            this.panLast.Name = "panLast";
            this.panLast.Size = new System.Drawing.Size(753, 50);
            this.panLast.TabIndex = 10197;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(725, 7);
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx4.Style.BackColor2.Color = System.Drawing.Color.White;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.Etched;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 10159;
            // 
            // btn_Save2
            // 
            this.btn_Save2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Save2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Save2.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save2.Image")));
            this.btn_Save2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_Save2.Location = new System.Drawing.Point(574, 6);
            this.btn_Save2.Name = "btn_Save2";
            this.btn_Save2.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(0, 0, 20, 20);
            this.btn_Save2.Size = new System.Drawing.Size(150, 42);
            this.btn_Save2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Save2.TabIndex = 26;
            this.btn_Save2.TabStop = false;
            this.btn_Save2.Text = "보고서 저장";
            this.btn_Save2.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // ucInputer1
            // 
            this.ucInputer1.BackColor = System.Drawing.Color.White;
            this.ucInputer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInputer1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucInputer1.Location = new System.Drawing.Point(5, 47);
            this.ucInputer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucInputer1.Name = "ucInputer1";
            this.ucInputer1.Size = new System.Drawing.Size(753, 777);
            this.ucInputer1.TabIndex = 10198;
            // 
            // frmInputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(758, 874);
            this.Controls.Add(this.ucInputer1);
            this.Controls.Add(this.panLast);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmInputer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "보고서 작성";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Panel.ResumeLayout(false);
            this.panLast.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX btn_Save1;
        private DevComponents.DotNetBar.ButtonX btn_HSave;
        private DevComponents.DotNetBar.ButtonX btn_HQuery;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btn_SmplQry;
        private DevComponents.DotNetBar.ButtonX btn_SmplSave;
        private DevComponents.DotNetBar.ButtonX btn_Find;
        private DevComponents.DotNetBar.ButtonX btn_Exit;
        private System.Windows.Forms.Panel panLast;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btn_Save2;
        private ucInputer ucInputer1;
    }
}

