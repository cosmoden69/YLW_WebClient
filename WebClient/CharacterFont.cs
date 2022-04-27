using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLWService;

namespace YLW_WebClient
{
    public partial class CharacterFont : Form
    {
        public Font FontStyle
        {
            get
            {
                try
                {
                    string fntname = Utils.ConvertToString(edtFontName.Text);
                    int fntsize = Utils.ToInt(edtFontSize.Text);
                    FontStyle fntstyle = System.Drawing.FontStyle.Regular;
                    if (chkFontBold.Checked) fntstyle |= System.Drawing.FontStyle.Bold;
                    if (chkFontItalic.Checked) fntstyle |= System.Drawing.FontStyle.Italic;
                    if (chkFontUnderline.Checked) fntstyle |= System.Drawing.FontStyle.Underline;
                    if (chkFontStrikeout.Checked) fntstyle |= System.Drawing.FontStyle.Strikeout;
                    Font fnt = new Font(fntname, fntsize, fntstyle);
                    return fnt;
                }
                catch
                {
                    return new Font("굴림체", 10);
                }
            }
            set
            {
                try
                {
                    this.bEvent = false;
                    Font fnt = value;
                    edtFontName.Text = fnt.Name;
                    edtFontSize.Text = Utils.ConvertToString(fnt.Size);
                    chkFontBold.Checked = (fnt.Bold & System.Drawing.FontStyle.Bold != 0 ? true : false);
                    chkFontItalic.Checked = (fnt.Italic & System.Drawing.FontStyle.Italic != 0 ? true : false);
                    chkFontUnderline.Checked = (fnt.Underline & System.Drawing.FontStyle.Underline != 0 ? true : false);
                    chkFontStrikeout.Checked = (fnt.Strikeout & System.Drawing.FontStyle.Strikeout != 0 ? true : false);
                }
                catch { }
                finally
                {
                    this.bEvent = true;
                }
            }
        }

        public string TextFontName
        {
            get
            {
                return Utils.ConvertToString(edtFontName.Text);
            }
            set
            {
                this.bEvent = false;
                edtFontName.Text = value;
                this.bEvent = true;
            }
        }

        public int TextFontSize
        {
            get
            {
                return Utils.ToInt(edtFontSize.Text);
            }
            set
            {
                this.bEvent = false;
                edtFontSize.Text = Utils.ConvertToString(value);
                this.bEvent = true;
            }
        }

        public bool TextFontBold
        {
            get
            {
                return chkFontBold.Checked;
            }
            set
            {
                this.bEvent = false;
                chkFontBold.Checked = value;
                this.bEvent = true;
            }
        }

        public bool TextFontItalic
        {
            get
            {
                return chkFontItalic.Checked;
            }
            set
            {
                this.bEvent = false;
                chkFontItalic.Checked = value;
                this.bEvent = true;
            }
        }

        public bool TextFontUnderline
        {
            get
            {
                return chkFontUnderline.Checked;
            }
            set
            {
                this.bEvent = false;
                chkFontUnderline.Checked = value;
                this.bEvent = true;
            }
        }

        public bool TextFontStrikeout
        {
            get
            {
                return chkFontStrikeout.Checked;
            }
            set
            {
                this.bEvent = false;
                chkFontStrikeout.Checked = value;
                this.bEvent = true;
            }
        }

        public Color FontForeColor
        {
            get
            {
                return edtFontForeColor.Color;
            }
            set
            {
                this.bEvent = false;
                edtFontForeColor.Color = value;
                this.bEvent = true;
            }
        }

        public Color FontBackColor
        {
            get
            {
                return edtFontBackColor.Color;
            }
            set
            {
                this.bEvent = false;
                edtFontBackColor.Color = value;
                this.bEvent = true;
            }
        }

        YLW_WebClient.CAA.RichTextBox rtb = null;

        private bool bEvent = false;
        private bool bSelecting = false;

        public CharacterFont()
        {
            InitializeComponent();

            InstalledFontCollection inf = new InstalledFontCollection();
            foreach (FontFamily font in inf.Families)
            {
                this.edtFontName.Items.Add(font.Name); //filling the font name
            }

            this.edtFontName.Enter += EdtFontName_Enter;
            this.edtFontName.LostFocus += EdtFontName_LostFocus;
            this.edtFontName.SelectedIndexChanged += EdtFontName_SelectedIndexChanged;
            this.edtFontName.SelectionChangeCommitted += EdtFontName_SelectionChangeCommitted;
            this.edtFontName.KeyDown += EdtFontName_KeyDown;
            this.edtFontSize.Enter += EdtFontSize_Enter;
            this.edtFontSize.LostFocus += EdtFontSize_LostFocus;
            this.edtFontSize.SelectedIndexChanged += EdtFontSize_SelectedIndexChanged;
            this.edtFontSize.SelectionChangeCommitted += EdtFontSize_SelectionChangeCommitted;
            this.edtFontSize.KeyDown += EdtFontSize_KeyDown;
            this.chkFontBold.CheckedChanged += ChkFontBold_CheckedChanged;
            this.chkFontItalic.CheckedChanged += ChkFontItalic_CheckedChanged;
            this.chkFontUnderline.CheckedChanged += ChkFontUnderline_CheckedChanged;
            this.chkFontStrikeout.CheckedChanged += ChkFontStrikeout_CheckedChanged;
            this.chkFontCBlack.Click += ChkFontForeColor_Clicked;
            this.chkFontCRed.Click += ChkFontForeColor_Clicked;
            this.chkFontCYellow.Click += ChkFontForeColor_Clicked;
            this.chkFontCBlue.Click += ChkFontForeColor_Clicked;
            this.chkFontCGreen.Click += ChkFontForeColor_Clicked;
            this.chkFontCOrange.Click += ChkFontForeColor_Clicked;
            this.chkFontCWhite.Click += ChkFontForeColor_Clicked;
            this.chkFontBackCBlack.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCRed.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCYellow.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCBlue.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCGreen.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCOrange.Click += ChkFontBackColor_Clicked;
            this.chkFontBackCWhite.Click += ChkFontBackColor_Clicked;
            this.edtFontForeColor.EditValueChanged += EdtFontForeColor_EditValueChanged;
            this.edtFontBackColor.EditValueChanged += EdtFontBackColor_EditValueChanged;
            this.btnUndo.Click += BtnUndo_Click;
            this.btnRedo.Click += BtnRedo_Click;
            this.btnChar.Click += BtnChar_Click;

            this.edtFontName.Text = " 굴림체";
            this.edtFontSize.Text = "10";
            this.chkFontBold.Checked = false;
            this.chkFontItalic.Checked = false;
            this.chkFontUnderline.Checked = false;
            this.chkFontStrikeout.Checked = false;
            this.edtFontForeColor.Color = Color.Black;
            this.edtFontBackColor.Color = Color.Transparent;

            this.bEvent = true;
        }

        public void SetRtbDoc(YLW_WebClient.CAA.RichTextBox rtb)
        {
            this.rtb = rtb;
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            try
            { 
                this.rtb.rtbDoc.Redo();
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            try
            { 
                this.rtb.rtbDoc.Undo();
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void BtnChar_Click(object sender, EventArgs e)
        {
            try
            {
                rtb.ShowCharMap();
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void EdtFontName_Enter(object sender, EventArgs e)
        {
            bSelecting = true;
        }

        private void EdtFontName_LostFocus(object sender, EventArgs e)
        {
            try
            { 
                bSelecting = false;
                //rtb.rtbDoc.SelectionFont = this.FontStyle;
            }
            catch { }
        }

        private void EdtFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                if (bSelecting) return;
                bSelecting = false;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void EdtFontName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bSelecting = false;
        }

        private void EdtFontName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                if (e.KeyCode == Keys.Enter)
                {
                    rtb.rtbDoc.SelectionFont = this.FontStyle;
                    rtb.rtbDoc.Focus();
                }
            }
            catch { }
        }

        private void EdtFontSize_Enter(object sender, EventArgs e)
        {
            bSelecting = true;
        }

        private void EdtFontSize_LostFocus(object sender, EventArgs e)
        {
            try
            { 
                bSelecting = false;
                //rtb.rtbDoc.SelectionFont = this.FontStyle;
            }
            catch { }
        }

        private void EdtFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                if (bSelecting) return;
                bSelecting = false;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void EdtFontSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bSelecting = false;
        }

        private void EdtFontSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                if (e.KeyCode == Keys.Enter)
                {
                    rtb.rtbDoc.SelectionFont = this.FontStyle;
                    rtb.rtbDoc.Focus();
                }
            }
            catch { }
        }

        private void ChkFontStrikeout_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void ChkFontUnderline_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void ChkFontItalic_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void ChkFontBold_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                rtb.rtbDoc.SelectionFont = this.FontStyle;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void ChkFontForeColor_Clicked(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                Panel chk = (Panel)sender;
                if (chk == null) return;
                edtFontForeColor.Focus();
                edtFontForeColor.Color = chk.BackColor;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void ChkFontBackColor_Clicked(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                Panel chk = (Panel)sender;
                if (chk == null) return;
                edtFontBackColor.Focus();
                edtFontBackColor.Color = chk.BackColor;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void EdtFontForeColor_EditValueChanged(object sender, EventArgs e)
        {
            try
            { 
                if (!bEvent) return;
                rtb.rtbDoc.SelectionColor = this.FontForeColor;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }

        private void EdtFontBackColor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bEvent) return;
                rtb.rtbDoc.SelectionBackColor = this.FontBackColor;
                rtb.rtbDoc.Focus();
            }
            catch { }
        }
    }
}
