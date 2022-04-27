using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class ucDBLifePan4 : UserControl
    {
        public delegate void DBLfResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event DBLfResizeEventHandler DBLifePan4Resize;

        public string Title1
        {
            get { return txtTitle1.Text; }
            set { txtTitle1.Text = value; }
        }
        public string Title2
        {
            get { return txtTitle2.Text; }
            set { txtTitle2.Text = value; }
        }
        public string Title3
        {
            get { return txtTitle3.Text; }
            set { txtTitle3.Text = value; }
        }
        public string Title4
        {
            get { return txtTitle4.Text; }
            set { txtTitle4.Text = value; }
        }
        public string Title5
        {
            get { return txtTitle5.Text; }
            set { txtTitle5.Text = value; }
        }
        public string Title6
        {
            get { return txtTitle6.Text; }
            set { txtTitle6.Text = value; }
        }
        public string Title7
        {
            get { return txtTitle7.Text; }
            set { txtTitle7.Text = value; }
        }
        public string Title8
        {
            get { return txtTitle8.Text; }
            set { txtTitle8.Text = value; }
        }
        public string Check1
        {
            get
            {
                if (chk1Y.Checked) return "1";
                else if (chk1N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk1Y.Checked = true;
                else if (value == "2") chk1N.Checked = true;
            }
        }
        public string Check2
        {
            get
            {
                if (chk2Y.Checked) return "1";
                else if (chk2N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk2Y.Checked = true;
                else if (value == "2") chk2N.Checked = true;
            }
        }
        public string Check3
        {
            get
            {
                if (chk3Y.Checked) return "1";
                else if (chk3N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk3Y.Checked = true;
                else if (value == "2") chk3N.Checked = true;
            }
        }
        public string Check4
        {
            get
            {
                if (chk4Y.Checked) return "1";
                else if (chk4N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk4Y.Checked = true;
                else if (value == "2") chk4N.Checked = true;
            }
        }
        public string Check5
        {
            get
            {
                if (chk5Y.Checked) return "1";
                else if (chk5N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk5Y.Checked = true;
                else if (value == "2") chk5N.Checked = true;
            }
        }
        public string Check6
        {
            get
            {
                if (chk6Y.Checked) return "1";
                else if (chk6N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk6Y.Checked = true;
                else if (value == "2") chk6N.Checked = true;
            }
        }
        public string Check7
        {
            get
            {
                if (chk7Y.Checked) return "1";
                else if (chk7N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk7Y.Checked = true;
                else if (value == "2") chk7N.Checked = true;
            }
        }
        public string Check8
        {
            get
            {
                if (chk8Y.Checked) return "1";
                else if (chk8N.Checked) return "2";
                else return "";
            }
            set
            {
                if (value == "1") chk8Y.Checked = true;
                else if (value == "2") chk8N.Checked = true;
            }
        }

        private bool _bEvent = false;

        private bool readOnlyMode = false;
        public ucDBLifePan4()
        {
            InitializeComponent();

            this.chk1Y.Click += Chk1Y_Click;
            this.chk1N.Click += Chk1N_Click;
            this.chk2Y.Click += Chk2Y_Click;
            this.chk2N.Click += Chk2N_Click;
            this.chk3Y.Click += Chk3Y_Click;
            this.chk3N.Click += Chk3N_Click;
            this.chk4Y.Click += Chk4Y_Click;
            this.chk4N.Click += Chk4N_Click;
            this.chk5Y.Click += Chk5Y_Click;
            this.chk5N.Click += Chk5N_Click;
            this.chk6Y.Click += Chk6Y_Click;
            this.chk6N.Click += Chk6N_Click;
            this.chk7Y.Click += Chk7Y_Click;
            this.chk7N.Click += Chk7N_Click;
            this.chk8Y.Click += Chk8Y_Click;
            this.chk8N.Click += Chk8N_Click;

            _bEvent = true;
        }

        private void Chk1Y_Click(object sender, EventArgs e)
        {
            if (chk1Y.Checked) chk1N.Checked = false;
        }

        private void Chk1N_Click(object sender, EventArgs e)
        {
            if (chk1N.Checked) chk1Y.Checked = false;
        }

        private void Chk2Y_Click(object sender, EventArgs e)
        {
            if (chk2Y.Checked) chk2N.Checked = false;
        }

        private void Chk2N_Click(object sender, EventArgs e)
        {
            if (chk2N.Checked) chk2Y.Checked = false;
        }

        private void Chk3Y_Click(object sender, EventArgs e)
        {
            if (chk3Y.Checked) chk3N.Checked = false;
        }

        private void Chk3N_Click(object sender, EventArgs e)
        {
            if (chk3N.Checked) chk3Y.Checked = false;
        }

        private void Chk4Y_Click(object sender, EventArgs e)
        {
            if (chk4Y.Checked) chk4N.Checked = false;
        }

        private void Chk4N_Click(object sender, EventArgs e)
        {
            if (chk4N.Checked) chk4Y.Checked = false;
        }

        private void Chk5Y_Click(object sender, EventArgs e)
        {
            if (chk5Y.Checked) chk5N.Checked = false;
        }

        private void Chk5N_Click(object sender, EventArgs e)
        {
            if (chk5N.Checked) chk5Y.Checked = false;
        }

        private void Chk6Y_Click(object sender, EventArgs e)
        {
            if (chk6Y.Checked) chk6N.Checked = false;
        }

        private void Chk6N_Click(object sender, EventArgs e)
        {
            if (chk6N.Checked) chk6Y.Checked = false;
        }

        private void Chk7Y_Click(object sender, EventArgs e)
        {
            if (chk7Y.Checked) chk7N.Checked = false;
        }

        private void Chk7N_Click(object sender, EventArgs e)
        {
            if (chk7N.Checked) chk7Y.Checked = false;
        }

        private void Chk8Y_Click(object sender, EventArgs e)
        {
            if (chk8Y.Checked) chk8N.Checked = false;
        }

        private void Chk8N_Click(object sender, EventArgs e)
        {
            if (chk8N.Checked) chk8Y.Checked = false;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            txtTitle1.SetReadOnly(rdonly);
            txtTitle2.SetReadOnly(rdonly);
            txtTitle3.SetReadOnly(rdonly);
            txtTitle4.SetReadOnly(rdonly);
            txtTitle5.SetReadOnly(rdonly);
            txtTitle6.SetReadOnly(rdonly);
            txtTitle7.SetReadOnly(rdonly);
            txtTitle8.SetReadOnly(rdonly);
            chk1Y.Enabled = !rdonly;
            chk1N.Enabled = !rdonly;
            chk2Y.Enabled = !rdonly;
            chk2N.Enabled = !rdonly;
            chk3Y.Enabled = !rdonly;
            chk3N.Enabled = !rdonly;
            chk4Y.Enabled = !rdonly;
            chk4N.Enabled = !rdonly;
            chk5Y.Enabled = !rdonly;
            chk5N.Enabled = !rdonly;
            chk6Y.Enabled = !rdonly;
            chk6N.Enabled = !rdonly;
            chk7Y.Enabled = !rdonly;
            chk7N.Enabled = !rdonly;
            chk8Y.Enabled = !rdonly;
            chk8N.Enabled = !rdonly;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (this.DBLifePan4Resize != null) this.DBLifePan4Resize(this, e);
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }

        private void PriceChange(object sender, EventArgs e)
        {
            DevComponents.Editors.DoubleInput amt = (DevComponents.Editors.DoubleInput)sender;
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        public void Clear()
        {
            _bEvent = false;

            txtTitle1.Text = "고지 직접 이행후 자필서명";
            txtTitle2.Text = "약관 전달 여부";
            txtTitle3.Text = "면책 약관 주요내용 설명";
            txtTitle4.Text = "면책 판단 구비서류";
            txtTitle5.Text = "처리 적정성 여부";
            txtTitle6.Text = "상반되는 판례";
            txtTitle7.Text = "작성자 불이익 원칙 적용";
            txtTitle8.Text = "재검토 가능 요소";
            chk1Y.Checked = false;
            chk1N.Checked = false;
            chk2Y.Checked = false;
            chk2N.Checked = false;
            chk3Y.Checked = false;
            chk3N.Checked = false;
            chk4Y.Checked = false;
            chk4N.Checked = false;
            chk5Y.Checked = false;
            chk5N.Checked = false;
            chk6Y.Checked = false;
            chk6N.Checked = false;
            chk7Y.Checked = false;
            chk7N.Checked = false;
            chk8Y.Checked = false;
            chk8N.Checked = false;

            _bEvent = true;
        }

        public void SetFocus()
        {
            this.chk1Y.Focus();
        }

        public void SetRow(DataRow drow)
        {
            string rprtNo = drow["RprtNo"] + "";
            string rprtHed = drow["RprtHed"] + "";
            string rprtRevwRslt = drow["RprtRevwRslt"] + "";
            switch (rprtNo)
            {
                case "1":
                    Title1 = rprtHed;
                    Check1 = rprtRevwRslt;
                    break;
                case "2":
                    Title2 = rprtHed;
                    Check2 = rprtRevwRslt;
                    break;
                case "3":
                    Title3 = rprtHed;
                    Check3 = rprtRevwRslt;
                    break;
                case "4":
                    Title4 = rprtHed;
                    Check4 = rprtRevwRslt;
                    break;
                case "5":
                    Title5 = rprtHed;
                    Check5 = rprtRevwRslt;
                    break;
                case "6":
                    Title6 = rprtHed;
                    Check6 = rprtRevwRslt;
                    break;
                case "7":
                    Title7 = rprtHed;
                    Check7 = rprtRevwRslt;
                    break;
                case "8":
                    Title8 = rprtHed;
                    Check8 = rprtRevwRslt;
                    break;
                default:
                    break;
            }
        }

        public void GetRow(string rprtNo, DataRow drow)
        {
            drow["RprtNo"] = rprtNo;
            switch (rprtNo)
            {
                case "1":
                    drow["RprtHed"] = Title1;
                    drow["RprtRevwRslt"] = Check1;
                    break;
                case "2":
                    drow["RprtHed"] = Title2;
                    drow["RprtRevwRslt"] = Check2;
                    break;
                case "3":
                    drow["RprtHed"] = Title3;
                    drow["RprtRevwRslt"] = Check3;
                    break;
                case "4":
                    drow["RprtHed"] = Title4;
                    drow["RprtRevwRslt"] = Check4;
                    break;
                case "5":
                    drow["RprtHed"] = Title5;
                    drow["RprtRevwRslt"] = Check5;
                    break;
                case "6":
                    drow["RprtHed"] = Title6;
                    drow["RprtRevwRslt"] = Check6;
                    break;
                case "7":
                    drow["RprtHed"] = Title7;
                    drow["RprtRevwRslt"] = Check7;
                    break;
                case "8":
                    drow["RprtHed"] = Title8;
                    drow["RprtRevwRslt"] = Check8;
                    break;
                default:
                    break;
            }
        }
    }
}
