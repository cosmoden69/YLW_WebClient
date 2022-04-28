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
    public partial class ucHeungkukPan1 : UserControl
    {
        public delegate void HenkResizeEventHandler(object sender, ContentsResizedEventArgs e);

        public event MouseEventHandler ContentsMouseWheel;
        public event HenkResizeEventHandler HeungkukPan1Resize;

        public string ClntTdncEvat
        {
            get { return richTextBox1.rtbDoc.Rtf; }
            set { richTextBox1.rtbDoc.Rtf = value; }
        }
        public string UnusDtil
        {
            get { return richTextBox2.rtbDoc.Rtf; }
            set { richTextBox2.rtbDoc.Rtf = value; }
        }
        public string CmplPnt1
        {
            get
            {
                return GetPanelLine1();
            }
            set
            {
                if (value == "3") SetPanelLine1(panelExA1);
                else if (value == "2") SetPanelLine1(panelExB1);
                else if (value == "1") SetPanelLine1(panelExC1);
                else SetPanelLine1(null);
            }
        }
        public string CmplPnt2
        {
            get
            {
                return GetPanelLine2();
            }
            set
            {
                if (value == "3") SetPanelLine2(panelExA2);
                else if (value == "2") SetPanelLine2(panelExB2);
                else if (value == "1") SetPanelLine2(panelExC2);
                else SetPanelLine2(null);
            }
        }
        public string CmplPnt3
        {
            get
            {
                return GetPanelLine3();
            }
            set
            {
                if (value == "3") SetPanelLine3(panelExA3);
                else if (value == "2") SetPanelLine3(panelExB3);
                else if (value == "1") SetPanelLine3(panelExC3);
                else SetPanelLine3(null);
            }
        }
        public string CmplPnt4
        {
            get
            {
                return GetPanelLine4();
            }
            set
            {
                if (value == "3") SetPanelLine4(panelExA4);
                else if (value == "2") SetPanelLine4(panelExB4);
                else if (value == "1") SetPanelLine4(panelExC4);
                else SetPanelLine4(null);
            }
        }
        public string CmplPnt5
        {
            get
            {
                return GetPanelLine5();
            }
            set
            {
                if (value == "3") SetPanelLine5(panelExA5);
                else if (value == "2") SetPanelLine5(panelExB5);
                else if (value == "1") SetPanelLine5(panelExC5);
                else SetPanelLine5(null);
            }
        }
        private bool readOnlyMode = false;
        public ucHeungkukPan1()
        {
            InitializeComponent();

            this.richTextBox1.TextChanged += new System.EventHandler(this.Text_Change);
            this.richTextBox2.TextChanged += new System.EventHandler(this.Text_Change);
            this.richTextBox1.ContentsResized += Txt1_ContentsResized;
            this.richTextBox2.ContentsResized += Txt2_ContentsResized;
            this.richTextBox1.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.richTextBox2.ContentsMouseWheel += Txt_ContentsMouseWheel;
            this.panelExA1.Click += PanelExLine1_Click;
            this.panelExA2.Click += PanelExLine2_Click;
            this.panelExA3.Click += PanelExLine3_Click;
            this.panelExA4.Click += PanelExLine4_Click;
            this.panelExA5.Click += PanelExLine5_Click;
            this.panelExB1.Click += PanelExLine1_Click;
            this.panelExB2.Click += PanelExLine2_Click;
            this.panelExB3.Click += PanelExLine3_Click;
            this.panelExB4.Click += PanelExLine4_Click;
            this.panelExB5.Click += PanelExLine5_Click;
            this.panelExC1.Click += PanelExLine1_Click;
            this.panelExC2.Click += PanelExLine2_Click;
            this.panelExC3.Click += PanelExLine3_Click;
            this.panelExC4.Click += PanelExLine4_Click;
            this.panelExC5.Click += PanelExLine5_Click;

            this.txtSum.SetReadOnly(true);
        }

        private void PanelExLine1_Click(object sender, EventArgs e)
        {
            SetPanelLine1(sender as DevComponents.DotNetBar.PanelEx);
        }

        private void PanelExLine2_Click(object sender, EventArgs e)
        {
            SetPanelLine2(sender as DevComponents.DotNetBar.PanelEx);
        }

        private void PanelExLine3_Click(object sender, EventArgs e)
        {
            SetPanelLine3(sender as DevComponents.DotNetBar.PanelEx);
        }

        private void PanelExLine4_Click(object sender, EventArgs e)
        {
            SetPanelLine4(sender as DevComponents.DotNetBar.PanelEx);
        }

        private void PanelExLine5_Click(object sender, EventArgs e)
        {
            SetPanelLine5(sender as DevComponents.DotNetBar.PanelEx);
        }


        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            this.richTextBox1.rtbDoc.ReadOnly = rdonly;
            this.richTextBox2.rtbDoc.ReadOnly = rdonly;
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

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        private void Txt_ContentsMouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
        }

        private void Txt1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            int hgt = Math.Max(this.richTextBox2.NewHeight, e.NewRectangle.Height);
            this.richTextBox1.SetContentsHeight(hgt);
            this.richTextBox2.SetContentsHeight(hgt);
            this.panel2.Height = hgt + 23;
            this.Height = this.panel2.Height + this.panel3.Height;
            if (this.HeungkukPan1Resize != null) this.HeungkukPan1Resize(this, e);
        }

        private void Txt2_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            int hgt = Math.Max(this.richTextBox1.NewHeight, e.NewRectangle.Height);
            this.richTextBox1.SetContentsHeight(hgt);
            this.richTextBox2.SetContentsHeight(hgt);
            this.panel2.Height = hgt + 23;
            this.Height = this.panel2.Height + this.panel3.Height;
            if (this.HeungkukPan1Resize != null) this.HeungkukPan1Resize(this, e);
        }

        public void Clear()
        {
            this.richTextBox1.rtbDoc.Rtf = "";
            this.richTextBox2.rtbDoc.Rtf = "";
            this.txtSum.Text = "";
            SetPanelLine1(null);
            SetPanelLine2(null);
            SetPanelLine3(null);
            SetPanelLine4(null);
            SetPanelLine5(null);
        }

        public void SetFocus()
        {
            this.richTextBox1.Focus();
        }

        private void Text_Change(object sender, EventArgs e)
        {
            Control txt = (Control)sender;
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;
        }

        private void SetPanelLine1(DevComponents.DotNetBar.PanelEx p)
        {
            panelExA1.Text = "2";
            panelExB1.Text = "1";
            panelExC1.Text = "0";
            if (p != null)
            {
                if (this.readOnlyMode) return;
                if (p == panelExA1) p.Text = "②";
                else if (p == panelExB1) p.Text = "①";
                else if (p == panelExC1) p.Text = "ⓞ";
            }
            SetSumText();
        }

        private void SetPanelLine2(DevComponents.DotNetBar.PanelEx p)
        {
            panelExA2.Text = "2";
            panelExB2.Text = "1";
            panelExC2.Text = "0";
            if (p != null)
            {
                if (this.readOnlyMode) return;
                if (p == panelExA2) p.Text = "②";
                else if (p == panelExB2) p.Text = "①";
                else if (p == panelExC2) p.Text = "ⓞ";
            }
            SetSumText();
        }

        private void SetPanelLine3(DevComponents.DotNetBar.PanelEx p)
        {
            panelExA3.Text = "2";
            panelExB3.Text = "1";
            panelExC3.Text = "0";
            if (p != null)
            {
                if (this.readOnlyMode) return;
                if (p == panelExA3) p.Text = "②";
                else if (p == panelExB3) p.Text = "①";
                else if (p == panelExC3) p.Text = "ⓞ";
            }
            SetSumText();
        }

        private void SetPanelLine4(DevComponents.DotNetBar.PanelEx p)
        {
            panelExA4.Text = "2";
            panelExB4.Text = "1";
            panelExC4.Text = "0";
            if (p != null)
            {
                if (this.readOnlyMode) return;
                if (p == panelExA4) p.Text = "②";
                else if (p == panelExB4) p.Text = "①";
                else if (p == panelExC4) p.Text = "ⓞ";
            }
            SetSumText();
        }

        private void SetPanelLine5(DevComponents.DotNetBar.PanelEx p)
        {
            panelExA5.Text = "2";
            panelExB5.Text = "1";
            panelExC5.Text = "0";
            if (p != null)
            {
                if (this.readOnlyMode) return;
                if (p == panelExA5) p.Text = "②";
                else if (p == panelExB5) p.Text = "①";
                else if (p == panelExC5) p.Text = "ⓞ";
            }
            SetSumText();
        }

        private string GetPanelLine1()
        {
            if (panelExA1.Text == "②") return "2";
            else if (panelExB1.Text == "①") return "1";
            else if (panelExC1.Text == "ⓞ") return "0";
            else return "0";
        }

        private string GetPanelLine2()
        {
            if (panelExA2.Text == "②") return "2";
            else if (panelExB2.Text == "①") return "1";
            else if (panelExC2.Text == "ⓞ") return "0";
            else return "0";
        }

        private string GetPanelLine3()
        {
            if (panelExA3.Text == "②") return "2";
            else if (panelExB3.Text == "①") return "1";
            else if (panelExC3.Text == "ⓞ") return "0";
            else return "0";
        }

        private string GetPanelLine4()
        {
            if (panelExA4.Text == "②") return "2";
            else if (panelExB4.Text == "①") return "1";
            else if (panelExC4.Text == "ⓞ") return "0";
            else return "0";
        }

        private string GetPanelLine5()
        {
            if (panelExA5.Text == "②") return "2";
            else if (panelExB5.Text == "①") return "1";
            else if (panelExC5.Text == "ⓞ") return "0";
            else return "0";
        }

        private void SetSumText()
        {
            int val = 0;
            val += Utils.ToInt(GetPanelLine1());
            val += Utils.ToInt(GetPanelLine2());
            val += Utils.ToInt(GetPanelLine3());
            val += Utils.ToInt(GetPanelLine4());
            val += Utils.ToInt(GetPanelLine5());
            txtSum.Text = Utils.ConvertToString(val);
        }
    }
}
