using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace YLW_WebClient.CAA
{
    public partial class RichTextBox : UserControl
    {
        private static CharacterMap _charMap = null;

        public event ContentsResizedEventHandler ContentsResized;
        public event MouseEventHandler ContentsMouseWheel;

        [DefaultValue(32767)]
        public int MaxInputLength { get; set; }
        public int MinWidth { get; set; } = -1;
        public int MinHeight { get; set; } = -1;
        public int NewWidth { get; set; }
        public int NewHeight { get; set; }

        public bool bShowMenu { get; set; } = true;
        public bool bFixWidth { get; set; } = true;
        public Color EnterBackColor { get; set; } = Color.FromArgb(255, 255, 136);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                SendKeys.Send("+~");
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        public RichTextBox()
        {   
            InitializeComponent();

            this.Resize += RichTextBox_Resize;
            this.Disposed += RichTextBox_Disposed;
            this.panDoc.MouseDown += PanDoc_MouseDown;
            this.rtbDoc.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.rtbDoc_ContentsResized);
            this.rtbDoc.Enter += new System.EventHandler(this.rtf_input_Enter);
            this.rtbDoc.Leave += new System.EventHandler(this.rtf_input_Leave);
            this.rtbDoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbDoc_MouseDown);
            this.rtbDoc.MouseWheel += RtbDoc_MouseWheel;
            this.rtbDoc.TextChanged += RtbDoc_TextChanged;
            this.rtbDoc.Validating += RtbDoc_Validating;
            this.rtbDoc.BorderStyle = BorderStyle.None;
            this.VisibleChanged += RichTextBox_VisibleChanged;
        }

        private void RichTextBox_VisibleChanged(object sender, EventArgs e)
        {
            try
            { 
                if (this.Visible == false)
                {
                    if (_charMap != null) _charMap.Dispose();
                }
            }
            catch { }
        }

        private void RichTextBox_Disposed(object sender, EventArgs e)
        {
            try
            { 
                if (_charMap != null) _charMap.Dispose();
            }
            catch { }
        }

        private void RichTextBox_Resize(object sender, EventArgs e)
        {
            try
            { 
                const int margin = 2;
                if (bFixWidth)
                {
                    rtbDoc.Width = this.Width - margin;
                }
            }
            catch { }
        }

        private void rtf_input_Enter(object sender, EventArgs e)
        {
            try
            { 
                if (!rtbDoc.ReadOnly) rtbDoc.BackColor = EnterBackColor;
                if (_charMap != null && _charMap.Visible)
                {
                    _charMap.SetRtbDoc(rtbDoc);
                    Point pos = this.PointToScreen(this.panDoc.Location);
                    _charMap.Location = new Point(pos.X + this.Width, (pos.Y < 0 ? 0 : pos.Y));
                }
            }
            catch { }
        }

        private void rtf_input_Leave(object sender, EventArgs e)
        {
            try
            { 
                rtbDoc.BackColor = Color.White;
            }
            catch { }
        }

        private void RtbDoc_Validating(object sender, CancelEventArgs e)
        {
            try
            { 
                string prevText = rtbDoc.Text;
                if (this.MaxInputLength > 0 && YLWService.Utils.HLen(prevText) > this.MaxInputLength)
                {
                    MessageBox.Show("입력가능 길이(" + this.MaxInputLength.ToString() + ") 초과입니다");
                    int pos = rtbDoc.SelectionStart;
                    rtbDoc.Text = YLWService.Utils.HMid(prevText, 1, this.MaxInputLength);
                    rtbDoc.SelectionStart = (pos < rtbDoc.Text.Length ? pos : rtbDoc.Text.Length);
                    rtbDoc.ScrollToCaret();
                    e.Cancel = true;
                }
            }
            catch { }
        }

        private void RtbDoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string prevText = rtbDoc.Text;
                //if (this.MaxInputLength > 0 && YLWService.Utils.HLen(prevText) > this.MaxInputLength)
                //{
                //    MessageBox.Show("입력가능 길이(" + this.MaxInputLength.ToString() + ") 초과입니다");
                //    int pos = rtbDoc.SelectionStart;
                //    rtbDoc.Text = YLWService.Utils.HMid(prevText, 1, this.MaxInputLength);
                //    rtbDoc.SelectionStart = (pos < rtbDoc.Text.Length ? pos : rtbDoc.Text.Length);
                //    rtbDoc.ScrollToCaret();
                //}
            }
            catch { }
        }

        private void RtbDoc_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            { 
                if (this.ContentsMouseWheel != null) this.ContentsMouseWheel(this, e);
            }
            catch { }
        }

        private void rtbDoc_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            try
            { 
                const int margin = 2;

                //rtbDoc.ClientSize = new Size(this.Width - margin, e.NewRectangle.Height);
                if (bFixWidth)
                {
                    rtbDoc.ClientSize = new Size(this.Width - margin, (MinHeight > e.NewRectangle.Height ? MinHeight : e.NewRectangle.Height));
                    Height = rtbDoc.ClientSize.Height + margin;
                    Height = (MinHeight > Height ? MinHeight : Height);
                    this.NewHeight = Height;
                    panDoc.Height = Height;
                    panDoc.Width = Width;
                }
                else
                {
                    rtbDoc.ClientSize = new Size((MinWidth > e.NewRectangle.Width + 20 ? MinWidth : e.NewRectangle.Width + 20), (MinHeight > e.NewRectangle.Height ? MinHeight : e.NewRectangle.Height));
                    Height = rtbDoc.ClientSize.Height + margin;
                    Width = rtbDoc.ClientSize.Width + margin;
                    this.NewHeight = Height;
                    this.NewWidth = Width;
                    panDoc.Height = Height;
                    panDoc.Width = Width;
                    if (_charMap != null && _charMap.Visible)
                    {
                        Point pos = this.PointToScreen(this.panDoc.Location);
                        _charMap.Location = new Point(pos.X + this.Width, (pos.Y < 0 ? 0 : pos.Y));
                    }

                }
                if (this.ContentsResized != null) this.ContentsResized(this, new ContentsResizedEventArgs(panDoc.ClientRectangle));
            }
            catch { }
        }

        public void SetContentsHeight(int hgt)
        {
            try
            { 
                Height = (MinHeight > hgt ? MinHeight : hgt);
                panDoc.Height = Height;
            }
            catch { }
        }

        public void SetReadOnly(bool rdOnly)
        {
            try
            { 
                rtbDoc.ReadOnly = rdOnly;
            }
            catch { }
        }

        private void rtbDoc_MouseDown(object sender, MouseEventArgs e)
        {
            try
            { 
                if (e.Button == MouseButtons.Right)
                {
                    if (bShowMenu)
                    {
                        ContextMainMenu.Displayed = false;
                        ContextMainMenu.PopupMenu(System.Windows.Forms.Control.MousePosition);
                    }
                }
            }
            catch { }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            { 
                ButtonItem btn = (ButtonItem)sender;

                switch (btn.Tag.ToString())
                {
                    case "1":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                            System.Drawing.FontStyle newFontStyle;
                            newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;
                            rtbDoc.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                        }
                        if (btn.Checked)
                            btn.Checked = false;
                        else
                            btn.Checked = true;
                        break;
                    case "2":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                            System.Drawing.FontStyle newFontStyle;
                            newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;
                            rtbDoc.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                       
                        }
                        if (btn.Checked)
                            btn.Checked = false;
                        else
                            btn.Checked = true;
                        break;
                    case "3":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                            System.Drawing.FontStyle newFontStyle;
                            newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;
                            rtbDoc.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                        }
                        if (btn.Checked)
                            btn.Checked = false;
                        else
                            btn.Checked = true;
                        break;
                    case "4":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            FontDialog1.Font = rtbDoc.SelectionFont;
                        }
                        else
                        {
                            FontDialog1.Font = null;
                        }
                        FontDialog1.ShowApply = true;
                        if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rtbDoc.SelectionFont = FontDialog1.Font;
                        }
                        break;
                    case "5":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            ColorDialog1.Color = rtbDoc.SelectionColor;
                        }
                        ColorDialog1.Color = rtbDoc.SelectionColor;
                        if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rtbDoc.SelectionColor = ColorDialog1.Color;
                        }
                        break;
                    case "6":
                        if (!(rtbDoc.SelectionFont == null))
                        {
                            ColorDialog1.Color = rtbDoc.SelectionBackColor;
                        }
                        ColorDialog1.Color = rtbDoc.SelectionBackColor;
                        if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rtbDoc.SelectionBackColor = ColorDialog1.Color;
                        }
                        break;
                    case "7":
                        if (rtbDoc.CanUndo)
                        {
                            rtbDoc.Undo();
                        }
                        break;
                    case "8":
                        if (rtbDoc.CanRedo)
                        {
                            rtbDoc.Redo();
                        }
                        break;
                    case "9":
                        if (_charMap == null || _charMap.IsDisposed) _charMap = new CharacterMap();
                        _charMap.SetRtbDoc(rtbDoc);
                        Point pos = this.PointToScreen(this.panDoc.Location);
                        _charMap.Location = new Point(pos.X + this.Width, (pos.Y < 0 ? 0 : pos.Y));
                        _charMap.Show();
                        if (!_charMap.Visible) _charMap.Visible = true;
                        break;
                }
                this.Invalidate();
            }
            catch { }
        }

        public void ShowCharMap()
        {
            try
            {
                if (_charMap == null || _charMap.IsDisposed) _charMap = new CharacterMap();
                _charMap.SetRtbDoc(rtbDoc);
                Point pos = this.PointToScreen(this.panDoc.Location);
                _charMap.Location = new Point(pos.X + this.Width, (pos.Y < 0 ? 0 : pos.Y));
                _charMap.Show();
                if (!_charMap.Visible) _charMap.Visible = true;
            }
            catch { }
        }

        private void Pan_Moon_1_Click(object sender, EventArgs e)
        {
            try
            { 
                PanelEx pan = (PanelEx)sender;
                rtbDoc.SelectedText = pan.Text;
                rtbDoc.Focus();

                Pan_MoonJa.Visible = false;

                const int margin = 2;

                Height = rtbDoc.ClientSize.Height + margin;
                panDoc.Height = Height;
            }
            catch { }
        }

        private void PanDoc_MouseDown(object sender, MouseEventArgs e)
        {
            try
            { 
                rtbDoc.Focus();
            }
            catch { }
        }
    }
}
