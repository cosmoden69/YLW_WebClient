using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLW_WebClient.Painter.PaintControls;
using YLW_WebClient.Painter.Controller;
using YLWService;

namespace YLW_WebClient.Painter
{
    public partial class ToolBar : UserControl
    {
        public PenStyle PenStyle
        {
            get
            {
                try
                {
                    if (chkBorderLine.Checked) return PenStyle.Line;
                    else if (chkBorderDash.Checked) return PenStyle.Dash;
                    else return PenStyle.None;
                }
                catch
                {
                    return PenStyle.Line;
                }
            }
        }

        public FillStyle FillStyle
        {
            get
            {
                try
                {
                    if (chkFillSingle.Checked) return FillStyle.Fill;
                    else return FillStyle.None;
                }
                catch
                {
                    return FillStyle.None;
                }
            }
        }

        public int LineSize
        {
            get
            {
                try
                {
                    object obj = cboLineSize.EditValue;
                    return Utils.ConvertToInt(obj);
                }
                catch
                {
                    return 1;
                }
            }
        }

        public Color BorderColor { get { return edtBorderColor.Color; } }

        public Color FillColor { get { return edtFillColor.Color; } }

        private bool bEvent = false;

        public ToolBar()
        {
            InitializeComponent();

            this.btnClear.Click += BtnClear_Click;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            this.btnUndo.Click += BtnUndo_Click;
            this.btnRedo.Click += BtnRedo_Click;
            this.chkSelect.CheckedChanged += chkSelect_CheckedChanged;
            this.chkPencil.CheckedChanged += chkSelect_CheckedChanged;
            this.chkEraser.CheckedChanged += chkSelect_CheckedChanged;
            this.chkFill.CheckedChanged += chkSelect_CheckedChanged;
            this.chkText.CheckedChanged += chkSelect_CheckedChanged;
            this.chkLine.CheckedChanged += chkSelect_CheckedChanged;
            this.chkBox.CheckedChanged += chkSelect_CheckedChanged;
            this.chkCircle.CheckedChanged += chkSelect_CheckedChanged;
            this.chkArrow.CheckedChanged += chkSelect_CheckedChanged;
            this.chkBrokenLine.CheckedChanged += chkSelect_CheckedChanged;
            this.chkStar.CheckedChanged += chkSelect_CheckedChanged;

            this.chkBorderNone.CheckedChanged += ChkBorder_CheckedChanged;
            this.chkBorderLine.CheckedChanged += ChkBorder_CheckedChanged;
            this.chkBorderDash.CheckedChanged += ChkBorder_CheckedChanged;
            this.chkBorderCBlack.Click += ChkBorderColor_Clicked;
            this.chkBorderCRed.Click += ChkBorderColor_Clicked;
            this.chkBorderCYellow.Click += ChkBorderColor_Clicked;
            this.chkBorderCBlue.Click += ChkBorderColor_Clicked;
            this.chkBorderCGreen.Click += ChkBorderColor_Clicked;
            this.chkBorderCOrange.Click += ChkBorderColor_Clicked;
            this.chkBorderCWhite.Click += ChkBorderColor_Clicked;
            this.chkFillNone.CheckedChanged += ChkFill_CheckedChanged;
            this.chkFillSingle.CheckedChanged += ChkFill_CheckedChanged;
            this.chkFillCBlack.Click += ChkFillColor_Clicked;
            this.chkFillCRed.Click += ChkFillColor_Clicked;
            this.chkFillCYellow.Click += ChkFillColor_Clicked;
            this.chkFillCBlue.Click += ChkFillColor_Clicked;
            this.chkFillCGreen.Click += ChkFillColor_Clicked;
            this.chkFillCOrange.Click += ChkFillColor_Clicked;
            this.chkFillCWhite.Click += ChkFillColor_Clicked;
            this.chkBorderW1.Click += ChkBorderWidth_Clicked;
            this.chkBorderW2.Click += ChkBorderWidth_Clicked;
            this.chkBorderW3.Click += ChkBorderWidth_Clicked;
            this.chkBorderW5.Click += ChkBorderWidth_Clicked;
            this.chkBorderW7.Click += ChkBorderWidth_Clicked;
            this.chkBorderW9.Click += ChkBorderWidth_Clicked;
            this.chkBorderW13.Click += ChkBorderWidth_Clicked;
            this.chkBorderW15.Click += ChkBorderWidth_Clicked;
            this.chkBorderW20.Click += ChkBorderWidth_Clicked;
            this.chkBorderW25.Click += ChkBorderWidth_Clicked;
            this.cboLineSize.SelectedIndexChanged += CboLineSize_SelectedIndexChanged;
            this.edtBorderColor.EditValueChanged += EdtBorderColor_EditValueChanged;
            this.edtFillColor.EditValueChanged += EdtFillColor_EditValueChanged;

            this.chkBorderLine.Checked = true;
            this.chkFillNone.Checked = true;
            this.cboLineSize.SelectedIndex = 0;
            this.edtBorderColor.Color = Color.Black;
            this.edtFillColor.Color = Color.White;

            this.bEvent = true;
        }

        /// <summary>
        /// 수신된 ObserverClass 와 Action 에 따라서 처리한다.
        /// </summary>
        public void OnNext(ObserverClass observer)
        {
            if (observer.Name == ObserverName.MySheet)
            {
                switch (observer.Action)
                {
                    case ObserverAction.ChangeCreator:
                        bEvent = false;
                        UnSelectAllExcept(null);
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Selector) chkSelect.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Pencil) chkPencil.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Eraser) chkEraser.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Fill) chkFill.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Text) chkText.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Line) chkLine.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Box) chkBox.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Circle) chkCircle.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Arrow) chkArrow.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.BrokenLine) chkBrokenLine.Checked = true;
                        if (MainController.Instance.CurrentCreator == ObjectCreatorType.Star) chkStar.Checked = true;
                        bEvent = true;
                        return;
                }
            }
        }

        private void UnSelectAllExcept(DevExpress.XtraEditors.CheckButton chk)
        {
            if (chk != chkSelect) chkSelect.Checked = false;
            if (chk != chkPencil) chkPencil.Checked = false;
            if (chk != chkEraser) chkEraser.Checked = false;
            if (chk != chkFill) chkFill.Checked = false;
            if (chk != chkText) chkText.Checked = false;
            if (chk != chkLine) chkLine.Checked = false;
            if (chk != chkBox) chkBox.Checked = false;
            if (chk != chkCircle) chkCircle.Checked = false;
            if (chk != chkArrow) chkArrow.Checked = false;
            if (chk != chkBrokenLine) chkBrokenLine.Checked = false;
            if (chk != chkStar) chkStar.Checked = false;
        }

        private void UnSelectAllBorderExcept(DevExpress.XtraEditors.CheckButton chk)
        {
            if (chk != chkBorderNone) chkBorderNone.Checked = false;
            if (chk != chkBorderLine) chkBorderLine.Checked = false;
            if (chk != chkBorderDash) chkBorderDash.Checked = false;
        }

        private void UnSelectAllFillExcept(DevExpress.XtraEditors.CheckButton chk)
        {
            if (chk != chkFillNone) chkFillNone.Checked = false;
            if (chk != chkFillSingle) chkFillSingle.Checked = false;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.New);
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.Undo);
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.Redo);
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            DevExpress.XtraEditors.CheckButton chk = (DevExpress.XtraEditors.CheckButton)sender;
            if (chk == null) return;
            UnSelectAllExcept(chk);
            MainController.Instance.CurrentCreator = ObjectCreatorType.None;
            if (chk == chkSelect) MainController.Instance.CurrentCreator = ObjectCreatorType.Selector;
            if (chk == chkPencil) MainController.Instance.CurrentCreator = ObjectCreatorType.Pencil;
            if (chk == chkEraser) MainController.Instance.CurrentCreator = ObjectCreatorType.Eraser;
            if (chk == chkFill) MainController.Instance.CurrentCreator = ObjectCreatorType.Fill;
            if (chk == chkText) MainController.Instance.CurrentCreator = ObjectCreatorType.Text;
            if (chk == chkLine) MainController.Instance.CurrentCreator = ObjectCreatorType.Line;
            if (chk == chkBox) MainController.Instance.CurrentCreator = ObjectCreatorType.Box;
            if (chk == chkCircle) MainController.Instance.CurrentCreator = ObjectCreatorType.Circle;
            if (chk == chkArrow) MainController.Instance.CurrentCreator = ObjectCreatorType.Arrow;
            if (chk == this.chkBrokenLine) MainController.Instance.CurrentCreator = ObjectCreatorType.BrokenLine;
            if (chk == this.chkStar) MainController.Instance.CurrentCreator = ObjectCreatorType.Star;
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeCreator);
        }

        private void ChkBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            DevExpress.XtraEditors.CheckButton chk = (DevExpress.XtraEditors.CheckButton)sender;
            if (chk == null) return;
            UnSelectAllBorderExcept(chk);
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeProperty);
        }

        private void ChkBorderColor_Clicked(object sender, EventArgs e)
        {
            if (!bEvent) return;
            Panel chk = (Panel)sender;
            if (chk == null) return;
            edtBorderColor.Color = chk.BackColor;
        }

        private void ChkFill_CheckedChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            DevExpress.XtraEditors.CheckButton chk = (DevExpress.XtraEditors.CheckButton)sender;
            if (chk == null) return;
            UnSelectAllFillExcept(chk);
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeProperty);
        }

        private void ChkFillColor_Clicked(object sender, EventArgs e)
        {
            if (!bEvent) return;
            Panel chk = (Panel)sender;
            if (chk == null) return;
            edtFillColor.Color = chk.BackColor;
        }

        private void ChkBorderWidth_Clicked(object sender, EventArgs e)
        {
            if (!bEvent) return;
            Label chk = (Label)sender;
            if (chk == null) return;
            cboLineSize.EditValue = chk.Text;
        }

        private void CboLineSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeProperty);
        }

        private void EdtBorderColor_EditValueChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeProperty);
        }

        private void EdtFillColor_EditValueChanged(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.ChangeProperty);
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.FileLoad);
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.FileSave);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (!bEvent) return;
            MainController.Instance.Sheet?.OnNext(ObserverAction.SaveAs);
        }
    }
}
