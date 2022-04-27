using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLW_WebClient.Painter.Controller;
using YLW_WebClient.Painter.PaintControls;

namespace YLW_WebClient.Painter
{
    public class MyTextBox : YLW_WebClient.CAA.RichTextBox
    {
        private static CharacterFont _charFont = null;

        private ObjectText baseObject;

        public MyTextBox()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            rtbDoc.WordWrap = false;
            bFixWidth = false;
            MinWidth = 21;
            EnterBackColor = Color.FromArgb(255, 255, 136);
            //EnterBackColor = SystemColors.ControlLightLight;

            this.ContentsResized += MyTextBox_ContentsResized;
            this.VisibleChanged += MyTextBox_VisibleChanged;
            this.Disposed += MyTextBox_Disposed;
            this.Enter += MyTextBox_Enter;
            this.rtbDoc.SelectionChanged += RtbDoc_SelectionChanged;
        }

        private void MyTextBox_Disposed(object sender, EventArgs e)
        {
            try
            { 
                if (_charFont != null) _charFont.Dispose();
            }
            catch { }
        }

        private void MyTextBox_VisibleChanged(object sender, EventArgs e)
        {
            try
            { 
                if (this.Visible == false)
                {
                    if (_charFont != null) _charFont.Dispose();
                }
            }
            catch { }
        }

        private void MyTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                if (_charFont == null || _charFont.IsDisposed) _charFont = new CharacterFont();
                _charFont.SetRtbDoc(this);
                Point pos = this.PointToScreen(new Point(0, 0));
                pos.Offset(-10, -(_charFont.Height + 2));
                _charFont.Location = new Point((pos.X < 0 ? 0 : pos.X), (pos.Y < 0 ? 0 : pos.Y));
                _charFont.Show();
                if (!_charFont.Visible) _charFont.Visible = true;
            }
            catch (Exception ex) { }
        }

        private void RtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _charFont.FontStyle = rtbDoc.SelectionFont;
                _charFont.FontForeColor = rtbDoc.SelectionColor;
                _charFont.FontBackColor = rtbDoc.SelectionBackColor;
            }
            catch (Exception ex) { }
        }

        private void MyTextBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            try
            { 
                MyTextBox txt = (MyTextBox)sender;
                const int x_margin = 2;
                const int y_margin = 2;
                Size size = new Size(txt.NewWidth + x_margin, txt.NewHeight + y_margin);
                if (this.baseObject != null)
                {
                    this.baseObject.SetObjectSize(size);
                }
            }
            catch { }
        }

        public void ObjectMoved()
        {
            try
            {
                if (_charFont != null && _charFont.Visible)
                {
                    Point pos = this.PointToScreen(new Point(0, 0));
                    pos.Offset(-10, -(_charFont.Height + 2));
                    _charFont.Location = new Point((pos.X < 0 ? 0 : pos.X), (pos.Y < 0 ? 0 : pos.Y));
                }
                this.Focus();
            }
            catch { }
        }

        public void SetObject(ObjectText obj, Point pos)
        {
            try
            { 
                this.baseObject = obj;
                //this.Size = new Size(100, 21);
                this.Location = new Point(pos.X + 2, pos.Y + 2);
                this.rtbDoc.Rtf = "";
                this.Visible = true;
                this.Focus();
            }
            catch { }
        }

        public void UnSetObject()
        {
            try
            {
                if (this.baseObject != null)
                {
                    this.baseObject.SetRtf(this.rtbDoc.Rtf);
                    this.baseObject.ObjectSheet.Refresh();
                    this.baseObject = null;
                }
                this.rtbDoc.Rtf = "";
                this.Visible = false;
            }
            catch { }
        }
    }
}
