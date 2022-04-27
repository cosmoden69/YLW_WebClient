using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using DevComponents.DotNetBar;

namespace YLW_WebClient
{
    public class DataGridViewRichTextBoxColumn : DataGridViewColumn
    {
        public DataGridViewRichTextBoxColumn() : base(new DataGridViewRichTextBoxCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (!(value is DataGridViewRichTextBoxCell))
                    throw new InvalidCastException("CellTemplate must be a DataGridViewRichTextBoxCell");

                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewRichTextBoxCell : DataGridViewImageCell
    {
        private static readonly RichTextBox _editingControl = new RichTextBox();

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewRichTextBoxEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
            set
            {
                base.ValueType = value;
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(string);
            }
        }

        private static void SetRichTextBoxText(RichTextBox ctl, string text)
        {
            try
            {
                ctl.Rtf = text;
            }
            catch (ArgumentException)
            {
                ctl.Text = text;
            }
        }

        private Image GetRtfImage(int rowIndex, object value, bool selected)
        {
            Size cellSize = GetSize(rowIndex);

            if (cellSize.Width < 1 || cellSize.Height < 1)
                return null;

            RichTextBox ctl = null;

            if (ctl == null)
            {
                ctl = _editingControl;
                ctl.Size = GetSize(rowIndex);
                SetRichTextBoxText(ctl, Convert.ToString(value));
            }

            if (ctl != null)
            {
                // Print the content of RichTextBox to an image.
                Size imgSize = new Size(cellSize.Width - 1, cellSize.Height - 1);
                Image rtfImg = null;

                if (selected)
                {
                    // Selected cell state
                    ctl.BackColor = DataGridView.DefaultCellStyle.BackColor;
                    rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);
                }
                else
                {
                    ctl.BackColor = DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor;
                    rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);
                }

                return rtfImg;
            }

            return null;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            RichTextBox ctl = DataGridView.EditingControl as RichTextBox;
            if (ctl != null)
            {
                SetRichTextBoxText(ctl, Convert.ToString(initialFormattedValue));
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            return value;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, null, errorText, cellStyle, advancedBorderStyle, paintParts);

            Image img = GetRtfImage(rowIndex, value, base.Selected);
            if (img != null)
                graphics.DrawImage(img, cellBounds.Left, cellBounds.Top);
        }

        #region Handlers of edit events, copyied from DataGridViewTextBoxCell

        private byte flagsState;

        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            base.OnEnter(rowIndex, throughMouseClick);

            if ((base.DataGridView != null) && throughMouseClick)
            {
                this.flagsState = (byte)(this.flagsState | 1);
            }
        }

        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            base.OnLeave(rowIndex, throughMouseClick);

            if (base.DataGridView != null)
            {
                this.flagsState = (byte)(this.flagsState & -2);
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (base.DataGridView != null)
            {
                Point currentCellAddress = base.DataGridView.CurrentCellAddress;

                if (((currentCellAddress.X == e.ColumnIndex) && (currentCellAddress.Y == e.RowIndex)) && (e.Button == MouseButtons.Left))
                {
                    if ((this.flagsState & 1) != 0)
                    {
                        this.flagsState = (byte)(this.flagsState & -2);
                    }
                    else if (base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
                    {
                        base.DataGridView.BeginEdit(false);
                    }
                }
            }
        }

        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            return (((((char.IsLetterOrDigit((char)((ushort)e.KeyCode)) && ((e.KeyCode < Keys.F1) || (e.KeyCode > Keys.F24))) || ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.Divide))) || (((e.KeyCode >= Keys.OemSemicolon) && (e.KeyCode <= Keys.OemBackslash)) || ((e.KeyCode == Keys.Space) && !e.Shift))) && (!e.Alt && !e.Control)) || base.KeyEntersEditMode(e));
        }

        #endregion
    }

    public class DataGridViewRichTextBoxEditingControl : RichTextBox, IDataGridViewEditingControl
    {
        private DataGridView _dataGridView;
        private int _rowIndex;
        private bool _valueChanged;

        public bool bShowMenu { get; set; } = true;

        public bool IsInputReadOnly { get; set; } = false;

        public DataGridViewRichTextBoxEditingControl()
        {
            InitializeComponent();

            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.FromArgb(255, 255, 136);
            this.MouseDown += DataGridViewRichTextBoxEditingControl_MouseDown;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            this.Parent.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode & Keys.KeyCode)
            {
                case Keys.Enter:
                    if (IsInputReadOnly) return;
                    break;
                case Keys.Home:
                    break;
                case Keys.End:
                    break;
                case Keys.Back:
                case Keys.Delete:
                    if (IsInputReadOnly) return;
                    break;
                default:
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (IsInputReadOnly) return;
            base.OnTextChanged(e);

            _valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }

        protected override void OnContentsResized(ContentsResizedEventArgs e)
        {
            if (IsInputReadOnly) return;
            if (e.NewRectangle.Top < 0) return;
            const int margin = 2;

            ClientSize = new Size(this.Width, e.NewRectangle.Height);

            Height = ClientSize.Height + margin;

            DataGridView dgv = this.EditingControlDataGridView;
            dgv.CurrentCell.OwningRow.Height = Height;
            GridHeightResize();
        }

        private void GridHeightResize()
        {
            DataGridView dgv = this.EditingControlDataGridView;

            int height = dgv.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                height += dr.Height; // Row height.
            }
            dgv.Height = height + 45;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            Keys keys = keyData & Keys.KeyCode;
            if (keys == Keys.Return)
            {
                return this.Multiline;
            }

            return base.IsInputKey(keyData);
        }

        #region IDataGridViewEditingControl Members

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return _dataGridView;
            }
            set
            {
                _dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Rtf;
            }
            set
            {
                if (value is string)
                    this.Rtf = value as string;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return _valueChanged;
            }
            set
            {
                _valueChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Return:
                    return true;
                case Keys.Home:
                case Keys.End:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }

            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get { return this.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Rtf;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        private Image GetRtfImage(Rectangle newRect)
        {
            Size cellSize = newRect.Size;

            if (cellSize.Width < 1 || cellSize.Height < 1)
                return null;

            RichTextBox ctl = null;

            if (ctl != null)
            {
                // Print the content of RichTextBox to an image.
                Size imgSize = new Size(cellSize.Width - 1, cellSize.Height - 1);
                Image rtfImg = null;
                rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);
                return rtfImg;
            }

            return null;
        }

        private void DataGridViewRichTextBoxEditingControl_MouseDown(object sender, MouseEventArgs e)
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

        #endregion

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonItem btn = (ButtonItem)sender;
            RichTextBox rtbDoc = this;

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
                        ColorDialog1.Color = rtbDoc.ForeColor;
                    }
                    ColorDialog1.Color = rtbDoc.ForeColor;
                    if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        rtbDoc.SelectionColor = ColorDialog1.Color;
                    }
                    break;
                case "6":
                    if (rtbDoc.CanUndo)
                    {
                        rtbDoc.Undo();
                    }
                    break;
                case "7":
                    if (rtbDoc.CanRedo)
                    {
                        rtbDoc.Redo();
                    }
                    break;
                case "8":
                    if (Height < Pan_MoonJa.Height)
                        Height = Pan_MoonJa.Height;

                    Pan_MoonJa.Visible = true;
                    break;
            }
        }

        private void Pan_Moon_1_Click(object sender, EventArgs e)
        {
            PanelEx pan = (PanelEx)sender;
            RichTextBox rtbDoc = this;

            rtbDoc.SelectedText = pan.Text;
            rtbDoc.Focus();

            Pan_MoonJa.Visible = false;

            const int margin = 2;

            Height = rtbDoc.ClientSize.Height + margin;
        }

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.ContextMainMenu = new DevComponents.DotNetBar.ButtonItem();
            this.Btn_S_01 = new DevComponents.DotNetBar.ButtonItem();
            this.Btn_S_02 = new DevComponents.DotNetBar.ButtonItem();
            this.Btn_S_03 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.Btn_S_04 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.Btn_S_05 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.Btn_S_08 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.Btn_S_06 = new DevComponents.DotNetBar.ButtonItem();
            this.Btn_S_07 = new DevComponents.DotNetBar.ButtonItem();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panelEx57 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx58 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx56 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx59 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx55 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx60 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx54 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx61 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx53 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx62 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx52 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx49 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx77 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx37 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx76 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx38 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx75 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx39 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx74 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx40 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx73 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx41 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx72 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx42 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx71 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx43 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx70 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx44 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx69 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx45 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx68 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx46 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx67 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx47 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx66 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx36 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx65 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx20 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx64 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx21 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx92 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx22 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx91 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx23 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx90 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx24 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx89 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx25 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx88 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx26 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx87 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx27 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx86 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx28 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx85 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx29 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx84 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx30 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx83 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx31 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx82 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx32 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx81 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx19 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx107 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx16 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx106 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx17 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx105 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx15 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx104 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx14 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx103 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx13 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx102 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx12 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx78 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx11 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx101 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx10 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx79 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx80 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx35 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx33 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx18 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx48 = new DevComponents.DotNetBar.PanelEx();
            this.Pan_Moon_1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx34 = new DevComponents.DotNetBar.PanelEx();
            this.Pan_MoonJa = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            this.Pan_MoonJa.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.AntiAlias = true;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ContextMainMenu});
            this.contextMenuBar1.Location = new System.Drawing.Point(59, 88);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(146, 27);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.contextMenuBar1.TabIndex = 10219;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            this.contextMenuBar1.WrapItemsDock = true;
            // 
            // ContextMainMenu
            // 
            this.ContextMainMenu.AutoExpandOnClick = true;
            this.ContextMainMenu.Name = "ContextMainMenu";
            this.ContextMainMenu.PopupWidth = 150;
            this.ContextMainMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.Btn_S_01,
            this.Btn_S_02,
            this.Btn_S_03,
            this.labelItem1,
            this.Btn_S_04,
            this.labelItem2,
            this.Btn_S_05,
            this.labelItem3,
            this.Btn_S_08,
            this.labelItem4,
            this.Btn_S_06,
            this.Btn_S_07});
            this.ContextMainMenu.Text = "ContextMainMenu1";
            // 
            // Btn_S_01
            // 
            this.Btn_S_01.FontBold = true;
            this.Btn_S_01.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_01_Image));
            this.Btn_S_01.Name = "Btn_S_01";
            this.Btn_S_01.PopupWidth = 150;
            this.Btn_S_01.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.Btn_S_01.Tag = "1";
            this.Btn_S_01.Text = "굵    게";
            this.Btn_S_01.Click += new System.EventHandler(this.Button_Click);
            // 
            // Btn_S_02
            // 
            this.Btn_S_02.FontBold = true;
            this.Btn_S_02.FontUnderline = true;
            this.Btn_S_02.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_02_Image));
            this.Btn_S_02.Name = "Btn_S_02";
            this.Btn_S_02.PopupWidth = 150;
            this.Btn_S_02.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.Btn_S_02.Tag = "2";
            this.Btn_S_02.Text = "밑    줄";
            this.Btn_S_02.Click += new System.EventHandler(this.Button_Click);
            // 
            // Btn_S_03
            // 
            this.Btn_S_03.FontBold = true;
            this.Btn_S_03.FontItalic = true;
            this.Btn_S_03.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_03_Image));
            this.Btn_S_03.Name = "Btn_S_03";
            this.Btn_S_03.PopupWidth = 150;
            this.Btn_S_03.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.Btn_S_03.Tag = "3";
            this.Btn_S_03.Text = "기울림";
            this.Btn_S_03.Click += new System.EventHandler(this.Button_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem1.Font = new System.Drawing.Font("굴림", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.PaddingBottom = 1;
            this.labelItem1.PaddingLeft = 10;
            this.labelItem1.PaddingTop = 1;
            this.labelItem1.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            // 
            // Btn_S_04
            // 
            this.Btn_S_04.FontBold = true;
            this.Btn_S_04.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_04_Image));
            this.Btn_S_04.Name = "Btn_S_04";
            this.Btn_S_04.Tag = "4";
            this.Btn_S_04.Text = "글자체";
            this.Btn_S_04.Click += new System.EventHandler(this.Button_Click);
            // 
            // labelItem2
            // 
            this.labelItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem2.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem2.Font = new System.Drawing.Font("굴림", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingBottom = 1;
            this.labelItem2.PaddingLeft = 10;
            this.labelItem2.PaddingTop = 1;
            this.labelItem2.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            // 
            // Btn_S_05
            // 
            this.Btn_S_05.FontBold = true;
            this.Btn_S_05.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_05_Image));
            this.Btn_S_05.Name = "Btn_S_05";
            this.Btn_S_05.Tag = "5";
            this.Btn_S_05.Text = "글자색";
            this.Btn_S_05.Click += new System.EventHandler(this.Button_Click);
            // 
            // labelItem3
            // 
            this.labelItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem3.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem3.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem3.Font = new System.Drawing.Font("굴림", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.PaddingBottom = 1;
            this.labelItem3.PaddingLeft = 10;
            this.labelItem3.PaddingTop = 1;
            this.labelItem3.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            // 
            // Btn_S_08
            // 
            this.Btn_S_08.FontBold = true;
            this.Btn_S_08.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_08_Image));
            this.Btn_S_08.Name = "Btn_S_08";
            this.Btn_S_08.Tag = "8";
            this.Btn_S_08.Text = "문자표";
            this.Btn_S_08.Click += new System.EventHandler(this.Button_Click);
            // 
            // labelItem4
            // 
            this.labelItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem4.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem4.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem4.Font = new System.Drawing.Font("굴림", 2.25F);
            this.labelItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.PaddingBottom = 1;
            this.labelItem4.PaddingLeft = 10;
            this.labelItem4.PaddingTop = 1;
            this.labelItem4.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            // 
            // Btn_S_06
            // 
            this.Btn_S_06.FontBold = true;
            this.Btn_S_06.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_06_Image));
            this.Btn_S_06.Name = "Btn_S_06";
            this.Btn_S_06.Tag = "6";
            this.Btn_S_06.Text = "Undo .";
            this.Btn_S_06.Click += new System.EventHandler(this.Button_Click);
            // 
            // Btn_S_07
            // 
            this.Btn_S_07.FontBold = true;
            this.Btn_S_07.Image = ((System.Drawing.Image)(Properties.Resource1.Btn_S_07_Image));
            this.Btn_S_07.Name = "Btn_S_07";
            this.Btn_S_07.Tag = "7";
            this.Btn_S_07.Text = "Redo .";
            this.Btn_S_07.Click += new System.EventHandler(this.Button_Click);
            // 
            // panelEx57
            // 
            this.panelEx57.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx57.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx57.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx57.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx57.Location = new System.Drawing.Point(130, 75);
            this.panelEx57.Name = "panelEx57";
            this.panelEx57.Size = new System.Drawing.Size(27, 26);
            this.panelEx57.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx57.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx57.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx57.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx57.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx57.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx57.Style.GradientAngle = 90;
            this.panelEx57.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx57.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx57.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx57.TabIndex = 50;
            this.panelEx57.Text = "▽";
            this.panelEx57.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx58
            // 
            this.panelEx58.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx58.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx58.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx58.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx58.Location = new System.Drawing.Point(104, 75);
            this.panelEx58.Name = "panelEx58";
            this.panelEx58.Size = new System.Drawing.Size(27, 26);
            this.panelEx58.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx58.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx58.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx58.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx58.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx58.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx58.Style.GradientAngle = 90;
            this.panelEx58.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx58.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx58.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx58.TabIndex = 49;
            this.panelEx58.Text = "▼";
            this.panelEx58.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx56
            // 
            this.panelEx56.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx56.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx56.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx56.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx56.Location = new System.Drawing.Point(156, 75);
            this.panelEx56.Name = "panelEx56";
            this.panelEx56.Size = new System.Drawing.Size(27, 26);
            this.panelEx56.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx56.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx56.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx56.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx56.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx56.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx56.Style.GradientAngle = 90;
            this.panelEx56.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx56.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx56.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx56.TabIndex = 51;
            this.panelEx56.Text = "◀";
            this.panelEx56.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx59
            // 
            this.panelEx59.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx59.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx59.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx59.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx59.Location = new System.Drawing.Point(78, 75);
            this.panelEx59.Name = "panelEx59";
            this.panelEx59.Size = new System.Drawing.Size(27, 26);
            this.panelEx59.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx59.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx59.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx59.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx59.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx59.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx59.Style.GradientAngle = 90;
            this.panelEx59.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx59.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx59.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx59.TabIndex = 48;
            this.panelEx59.Text = "▷";
            this.panelEx59.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx55
            // 
            this.panelEx55.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx55.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx55.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx55.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx55.Location = new System.Drawing.Point(182, 75);
            this.panelEx55.Name = "panelEx55";
            this.panelEx55.Size = new System.Drawing.Size(27, 26);
            this.panelEx55.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx55.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx55.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx55.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx55.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx55.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx55.Style.GradientAngle = 90;
            this.panelEx55.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx55.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx55.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx55.TabIndex = 52;
            this.panelEx55.Text = "◁";
            this.panelEx55.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx60
            // 
            this.panelEx60.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx60.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx60.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx60.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx60.Location = new System.Drawing.Point(52, 75);
            this.panelEx60.Name = "panelEx60";
            this.panelEx60.Size = new System.Drawing.Size(27, 26);
            this.panelEx60.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx60.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx60.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx60.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx60.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx60.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx60.Style.GradientAngle = 90;
            this.panelEx60.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx60.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx60.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx60.TabIndex = 47;
            this.panelEx60.Text = "▶";
            this.panelEx60.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx54
            // 
            this.panelEx54.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx54.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx54.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx54.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx54.Location = new System.Drawing.Point(208, 75);
            this.panelEx54.Name = "panelEx54";
            this.panelEx54.Size = new System.Drawing.Size(27, 26);
            this.panelEx54.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx54.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx54.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx54.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx54.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx54.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx54.Style.GradientAngle = 90;
            this.panelEx54.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx54.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx54.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx54.TabIndex = 53;
            this.panelEx54.Text = "◆";
            this.panelEx54.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx61
            // 
            this.panelEx61.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx61.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx61.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx61.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx61.Location = new System.Drawing.Point(26, 75);
            this.panelEx61.Name = "panelEx61";
            this.panelEx61.Size = new System.Drawing.Size(27, 26);
            this.panelEx61.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx61.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx61.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx61.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx61.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx61.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx61.Style.GradientAngle = 90;
            this.panelEx61.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx61.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx61.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx61.TabIndex = 46;
            this.panelEx61.Text = "△";
            this.panelEx61.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx53
            // 
            this.panelEx53.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx53.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx53.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx53.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx53.Location = new System.Drawing.Point(234, 75);
            this.panelEx53.Name = "panelEx53";
            this.panelEx53.Size = new System.Drawing.Size(27, 26);
            this.panelEx53.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx53.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx53.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx53.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx53.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx53.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx53.Style.GradientAngle = 90;
            this.panelEx53.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx53.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx53.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx53.TabIndex = 54;
            this.panelEx53.Text = "◇";
            this.panelEx53.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx62
            // 
            this.panelEx62.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx62.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx62.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx62.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx62.Location = new System.Drawing.Point(0, 75);
            this.panelEx62.Name = "panelEx62";
            this.panelEx62.Size = new System.Drawing.Size(27, 26);
            this.panelEx62.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx62.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx62.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx62.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx62.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx62.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx62.Style.GradientAngle = 90;
            this.panelEx62.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx62.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx62.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx62.TabIndex = 45;
            this.panelEx62.Text = "▲";
            this.panelEx62.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx52
            // 
            this.panelEx52.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx52.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx52.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx52.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx52.Location = new System.Drawing.Point(260, 75);
            this.panelEx52.Name = "panelEx52";
            this.panelEx52.Size = new System.Drawing.Size(27, 26);
            this.panelEx52.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx52.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx52.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx52.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx52.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx52.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx52.Style.GradientAngle = 90;
            this.panelEx52.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx52.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx52.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx52.TabIndex = 55;
            this.panelEx52.Text = "◈";
            this.panelEx52.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx49
            // 
            this.panelEx49.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx49.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx49.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx49.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx49.Location = new System.Drawing.Point(286, 50);
            this.panelEx49.Name = "panelEx49";
            this.panelEx49.Size = new System.Drawing.Size(27, 26);
            this.panelEx49.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx49.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx49.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx49.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx49.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx49.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx49.Style.GradientAngle = 90;
            this.panelEx49.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx49.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx49.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx49.TabIndex = 41;
            this.panelEx49.Text = "≫";
            this.panelEx49.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx77
            // 
            this.panelEx77.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx77.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx77.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx77.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx77.Location = new System.Drawing.Point(0, 100);
            this.panelEx77.Name = "panelEx77";
            this.panelEx77.Size = new System.Drawing.Size(27, 26);
            this.panelEx77.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx77.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx77.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx77.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx77.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx77.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx77.Style.GradientAngle = 90;
            this.panelEx77.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx77.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx77.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx77.TabIndex = 60;
            this.panelEx77.Text = "○";
            this.panelEx77.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx37
            // 
            this.panelEx37.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx37.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx37.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx37.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx37.Location = new System.Drawing.Point(260, 50);
            this.panelEx37.Name = "panelEx37";
            this.panelEx37.Size = new System.Drawing.Size(27, 26);
            this.panelEx37.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx37.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx37.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx37.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx37.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx37.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx37.Style.GradientAngle = 90;
            this.panelEx37.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx37.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx37.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx37.TabIndex = 40;
            this.panelEx37.Text = "≪";
            this.panelEx37.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx76
            // 
            this.panelEx76.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx76.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx76.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx76.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx76.Location = new System.Drawing.Point(26, 100);
            this.panelEx76.Name = "panelEx76";
            this.panelEx76.Size = new System.Drawing.Size(27, 26);
            this.panelEx76.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx76.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx76.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx76.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx76.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx76.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx76.Style.GradientAngle = 90;
            this.panelEx76.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx76.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx76.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx76.TabIndex = 61;
            this.panelEx76.Text = "◎";
            this.panelEx76.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx38
            // 
            this.panelEx38.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx38.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx38.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx38.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx38.Location = new System.Drawing.Point(234, 50);
            this.panelEx38.Name = "panelEx38";
            this.panelEx38.Size = new System.Drawing.Size(27, 26);
            this.panelEx38.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx38.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx38.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx38.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx38.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx38.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx38.Style.GradientAngle = 90;
            this.panelEx38.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx38.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx38.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx38.TabIndex = 39;
            this.panelEx38.Text = "】";
            this.panelEx38.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx75
            // 
            this.panelEx75.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx75.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx75.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx75.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx75.Location = new System.Drawing.Point(52, 100);
            this.panelEx75.Name = "panelEx75";
            this.panelEx75.Size = new System.Drawing.Size(27, 26);
            this.panelEx75.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx75.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx75.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx75.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx75.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx75.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx75.Style.GradientAngle = 90;
            this.panelEx75.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx75.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx75.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx75.TabIndex = 62;
            this.panelEx75.Text = "●";
            this.panelEx75.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx39
            // 
            this.panelEx39.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx39.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx39.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx39.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx39.Location = new System.Drawing.Point(208, 50);
            this.panelEx39.Name = "panelEx39";
            this.panelEx39.Size = new System.Drawing.Size(27, 26);
            this.panelEx39.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx39.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx39.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx39.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx39.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx39.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx39.Style.GradientAngle = 90;
            this.panelEx39.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx39.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx39.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx39.TabIndex = 38;
            this.panelEx39.Text = "【";
            this.panelEx39.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx74
            // 
            this.panelEx74.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx74.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx74.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx74.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx74.Location = new System.Drawing.Point(312, 125);
            this.panelEx74.Name = "panelEx74";
            this.panelEx74.Size = new System.Drawing.Size(27, 26);
            this.panelEx74.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx74.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx74.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx74.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx74.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx74.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx74.Style.GradientAngle = 90;
            this.panelEx74.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx74.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx74.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx74.TabIndex = 63;
            this.panelEx74.Text = "◐";
            this.panelEx74.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx40
            // 
            this.panelEx40.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx40.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx40.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx40.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx40.Location = new System.Drawing.Point(182, 50);
            this.panelEx40.Name = "panelEx40";
            this.panelEx40.Size = new System.Drawing.Size(27, 26);
            this.panelEx40.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx40.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx40.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx40.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx40.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx40.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx40.Style.GradientAngle = 90;
            this.panelEx40.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx40.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx40.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx40.TabIndex = 37;
            this.panelEx40.Text = "』";
            this.panelEx40.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx73
            // 
            this.panelEx73.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx73.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx73.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx73.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx73.Location = new System.Drawing.Point(338, 125);
            this.panelEx73.Name = "panelEx73";
            this.panelEx73.Size = new System.Drawing.Size(27, 26);
            this.panelEx73.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx73.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx73.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx73.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx73.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx73.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx73.Style.GradientAngle = 90;
            this.panelEx73.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx73.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx73.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx73.TabIndex = 64;
            this.panelEx73.Text = "◑";
            this.panelEx73.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx41
            // 
            this.panelEx41.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx41.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx41.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx41.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx41.Location = new System.Drawing.Point(156, 50);
            this.panelEx41.Name = "panelEx41";
            this.panelEx41.Size = new System.Drawing.Size(27, 26);
            this.panelEx41.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx41.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx41.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx41.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx41.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx41.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx41.Style.GradientAngle = 90;
            this.panelEx41.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx41.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx41.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx41.TabIndex = 36;
            this.panelEx41.Text = "『";
            this.panelEx41.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx72
            // 
            this.panelEx72.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx72.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx72.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx72.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx72.Location = new System.Drawing.Point(78, 100);
            this.panelEx72.Name = "panelEx72";
            this.panelEx72.Size = new System.Drawing.Size(27, 26);
            this.panelEx72.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx72.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx72.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx72.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx72.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx72.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx72.Style.GradientAngle = 90;
            this.panelEx72.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx72.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx72.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx72.TabIndex = 65;
            this.panelEx72.Text = "▤";
            this.panelEx72.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx42
            // 
            this.panelEx42.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx42.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx42.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx42.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx42.Location = new System.Drawing.Point(130, 50);
            this.panelEx42.Name = "panelEx42";
            this.panelEx42.Size = new System.Drawing.Size(27, 26);
            this.panelEx42.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx42.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx42.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx42.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx42.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx42.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx42.Style.GradientAngle = 90;
            this.panelEx42.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx42.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx42.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx42.TabIndex = 35;
            this.panelEx42.Text = "」";
            this.panelEx42.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx71
            // 
            this.panelEx71.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx71.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx71.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx71.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx71.Location = new System.Drawing.Point(104, 100);
            this.panelEx71.Name = "panelEx71";
            this.panelEx71.Size = new System.Drawing.Size(27, 26);
            this.panelEx71.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx71.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx71.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx71.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx71.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx71.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx71.Style.GradientAngle = 90;
            this.panelEx71.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx71.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx71.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx71.TabIndex = 66;
            this.panelEx71.Text = "▥";
            this.panelEx71.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx43
            // 
            this.panelEx43.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx43.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx43.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx43.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx43.Location = new System.Drawing.Point(104, 50);
            this.panelEx43.Name = "panelEx43";
            this.panelEx43.Size = new System.Drawing.Size(27, 26);
            this.panelEx43.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx43.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx43.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx43.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx43.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx43.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx43.Style.GradientAngle = 90;
            this.panelEx43.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx43.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx43.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx43.TabIndex = 34;
            this.panelEx43.Text = "「";
            this.panelEx43.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx70
            // 
            this.panelEx70.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx70.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx70.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx70.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx70.Location = new System.Drawing.Point(130, 100);
            this.panelEx70.Name = "panelEx70";
            this.panelEx70.Size = new System.Drawing.Size(27, 26);
            this.panelEx70.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx70.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx70.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx70.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx70.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx70.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx70.Style.GradientAngle = 90;
            this.panelEx70.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx70.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx70.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx70.TabIndex = 67;
            this.panelEx70.Text = "▦";
            this.panelEx70.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx44
            // 
            this.panelEx44.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx44.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx44.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx44.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx44.Location = new System.Drawing.Point(78, 50);
            this.panelEx44.Name = "panelEx44";
            this.panelEx44.Size = new System.Drawing.Size(27, 26);
            this.panelEx44.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx44.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx44.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx44.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx44.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx44.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx44.Style.GradientAngle = 90;
            this.panelEx44.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx44.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx44.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx44.TabIndex = 33;
            this.panelEx44.Text = "》";
            this.panelEx44.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx69
            // 
            this.panelEx69.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx69.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx69.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx69.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx69.Location = new System.Drawing.Point(156, 100);
            this.panelEx69.Name = "panelEx69";
            this.panelEx69.Size = new System.Drawing.Size(27, 26);
            this.panelEx69.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx69.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx69.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx69.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx69.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx69.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx69.Style.GradientAngle = 90;
            this.panelEx69.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx69.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx69.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx69.TabIndex = 68;
            this.panelEx69.Text = "▧";
            this.panelEx69.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx45
            // 
            this.panelEx45.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx45.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx45.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx45.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx45.Location = new System.Drawing.Point(52, 50);
            this.panelEx45.Name = "panelEx45";
            this.panelEx45.Size = new System.Drawing.Size(27, 26);
            this.panelEx45.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx45.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx45.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx45.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx45.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx45.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx45.Style.GradientAngle = 90;
            this.panelEx45.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx45.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx45.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx45.TabIndex = 32;
            this.panelEx45.Text = "《";
            this.panelEx45.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx68
            // 
            this.panelEx68.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx68.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx68.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx68.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx68.Location = new System.Drawing.Point(182, 100);
            this.panelEx68.Name = "panelEx68";
            this.panelEx68.Size = new System.Drawing.Size(27, 26);
            this.panelEx68.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx68.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx68.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx68.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx68.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx68.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx68.Style.GradientAngle = 90;
            this.panelEx68.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx68.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx68.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx68.TabIndex = 69;
            this.panelEx68.Text = "▨";
            this.panelEx68.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx46
            // 
            this.panelEx46.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx46.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx46.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx46.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx46.Location = new System.Drawing.Point(26, 50);
            this.panelEx46.Name = "panelEx46";
            this.panelEx46.Size = new System.Drawing.Size(27, 26);
            this.panelEx46.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx46.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx46.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx46.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx46.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx46.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx46.Style.GradientAngle = 90;
            this.panelEx46.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx46.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx46.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx46.TabIndex = 31;
            this.panelEx46.Text = "〉";
            this.panelEx46.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx67
            // 
            this.panelEx67.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx67.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx67.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx67.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx67.Location = new System.Drawing.Point(208, 100);
            this.panelEx67.Name = "panelEx67";
            this.panelEx67.Size = new System.Drawing.Size(27, 26);
            this.panelEx67.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx67.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx67.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx67.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx67.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx67.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx67.Style.GradientAngle = 90;
            this.panelEx67.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx67.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx67.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx67.TabIndex = 70;
            this.panelEx67.Text = "▩";
            this.panelEx67.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx47
            // 
            this.panelEx47.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx47.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx47.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx47.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx47.Location = new System.Drawing.Point(0, 50);
            this.panelEx47.Name = "panelEx47";
            this.panelEx47.Size = new System.Drawing.Size(27, 26);
            this.panelEx47.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx47.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx47.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx47.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx47.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx47.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx47.Style.GradientAngle = 90;
            this.panelEx47.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx47.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx47.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx47.TabIndex = 30;
            this.panelEx47.Text = "〈";
            this.panelEx47.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx66
            // 
            this.panelEx66.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx66.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx66.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx66.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx66.Location = new System.Drawing.Point(234, 100);
            this.panelEx66.Name = "panelEx66";
            this.panelEx66.Size = new System.Drawing.Size(27, 26);
            this.panelEx66.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx66.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx66.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx66.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx66.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx66.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx66.Style.GradientAngle = 90;
            this.panelEx66.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx66.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx66.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx66.TabIndex = 71;
            this.panelEx66.Text = "■";
            this.panelEx66.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx36
            // 
            this.panelEx36.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx36.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx36.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx36.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx36.Location = new System.Drawing.Point(338, 25);
            this.panelEx36.Name = "panelEx36";
            this.panelEx36.Size = new System.Drawing.Size(27, 26);
            this.panelEx36.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx36.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx36.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx36.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx36.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx36.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx36.Style.GradientAngle = 90;
            this.panelEx36.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx36.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx36.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx36.TabIndex = 28;
            this.panelEx36.Text = "☜";
            this.panelEx36.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx65
            // 
            this.panelEx65.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx65.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx65.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx65.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx65.Location = new System.Drawing.Point(260, 100);
            this.panelEx65.Name = "panelEx65";
            this.panelEx65.Size = new System.Drawing.Size(27, 26);
            this.panelEx65.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx65.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx65.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx65.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx65.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx65.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx65.Style.GradientAngle = 90;
            this.panelEx65.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx65.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx65.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx65.TabIndex = 72;
            this.panelEx65.Text = "□";
            this.panelEx65.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx20
            // 
            this.panelEx20.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx20.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx20.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx20.Location = new System.Drawing.Point(312, 25);
            this.panelEx20.Name = "panelEx20";
            this.panelEx20.Size = new System.Drawing.Size(27, 26);
            this.panelEx20.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx20.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx20.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx20.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx20.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx20.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx20.Style.GradientAngle = 90;
            this.panelEx20.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx20.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx20.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx20.TabIndex = 27;
            this.panelEx20.Text = "☞";
            this.panelEx20.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx64
            // 
            this.panelEx64.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx64.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx64.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx64.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx64.Location = new System.Drawing.Point(286, 100);
            this.panelEx64.Name = "panelEx64";
            this.panelEx64.Size = new System.Drawing.Size(27, 26);
            this.panelEx64.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx64.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx64.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx64.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx64.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx64.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx64.Style.GradientAngle = 90;
            this.panelEx64.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx64.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx64.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx64.TabIndex = 73;
            this.panelEx64.Text = "▣";
            this.panelEx64.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx21
            // 
            this.panelEx21.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx21.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx21.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx21.Location = new System.Drawing.Point(286, 25);
            this.panelEx21.Name = "panelEx21";
            this.panelEx21.Size = new System.Drawing.Size(27, 26);
            this.panelEx21.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx21.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx21.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx21.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx21.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx21.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx21.Style.GradientAngle = 90;
            this.panelEx21.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx21.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx21.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx21.TabIndex = 26;
            this.panelEx21.Text = "⇔";
            this.panelEx21.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx92
            // 
            this.panelEx92.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx92.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx92.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx92.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx92.Location = new System.Drawing.Point(0, 125);
            this.panelEx92.Name = "panelEx92";
            this.panelEx92.Size = new System.Drawing.Size(27, 26);
            this.panelEx92.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx92.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx92.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx92.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx92.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx92.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx92.Style.GradientAngle = 90;
            this.panelEx92.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx92.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx92.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx92.TabIndex = 75;
            this.panelEx92.Text = "♠";
            this.panelEx92.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx22
            // 
            this.panelEx22.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx22.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx22.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx22.Location = new System.Drawing.Point(260, 25);
            this.panelEx22.Name = "panelEx22";
            this.panelEx22.Size = new System.Drawing.Size(27, 26);
            this.panelEx22.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx22.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx22.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx22.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx22.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx22.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx22.Style.GradientAngle = 90;
            this.panelEx22.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx22.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx22.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx22.TabIndex = 25;
            this.panelEx22.Text = "⇒";
            this.panelEx22.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx91
            // 
            this.panelEx91.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx91.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx91.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx91.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx91.Location = new System.Drawing.Point(26, 125);
            this.panelEx91.Name = "panelEx91";
            this.panelEx91.Size = new System.Drawing.Size(27, 26);
            this.panelEx91.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx91.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx91.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx91.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx91.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx91.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx91.Style.GradientAngle = 90;
            this.panelEx91.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx91.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx91.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx91.TabIndex = 76;
            this.panelEx91.Text = "♤";
            this.panelEx91.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx23
            // 
            this.panelEx23.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx23.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx23.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx23.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx23.Location = new System.Drawing.Point(234, 25);
            this.panelEx23.Name = "panelEx23";
            this.panelEx23.Size = new System.Drawing.Size(27, 26);
            this.panelEx23.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx23.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx23.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx23.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx23.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx23.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx23.Style.GradientAngle = 90;
            this.panelEx23.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx23.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx23.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx23.TabIndex = 24;
            this.panelEx23.Text = "↙";
            this.panelEx23.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx90
            // 
            this.panelEx90.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx90.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx90.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx90.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx90.Location = new System.Drawing.Point(52, 125);
            this.panelEx90.Name = "panelEx90";
            this.panelEx90.Size = new System.Drawing.Size(27, 26);
            this.panelEx90.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx90.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx90.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx90.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx90.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx90.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx90.Style.GradientAngle = 90;
            this.panelEx90.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx90.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx90.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx90.TabIndex = 77;
            this.panelEx90.Text = "♣";
            this.panelEx90.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx24
            // 
            this.panelEx24.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx24.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx24.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx24.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx24.Location = new System.Drawing.Point(208, 25);
            this.panelEx24.Name = "panelEx24";
            this.panelEx24.Size = new System.Drawing.Size(27, 26);
            this.panelEx24.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx24.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx24.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx24.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx24.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx24.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx24.Style.GradientAngle = 90;
            this.panelEx24.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx24.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx24.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx24.TabIndex = 23;
            this.panelEx24.Text = "↘";
            this.panelEx24.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx89
            // 
            this.panelEx89.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx89.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx89.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx89.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx89.Location = new System.Drawing.Point(78, 125);
            this.panelEx89.Name = "panelEx89";
            this.panelEx89.Size = new System.Drawing.Size(27, 26);
            this.panelEx89.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx89.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx89.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx89.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx89.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx89.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx89.Style.GradientAngle = 90;
            this.panelEx89.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx89.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx89.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx89.TabIndex = 78;
            this.panelEx89.Text = "♧";
            this.panelEx89.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx25
            // 
            this.panelEx25.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx25.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx25.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx25.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx25.Location = new System.Drawing.Point(182, 25);
            this.panelEx25.Name = "panelEx25";
            this.panelEx25.Size = new System.Drawing.Size(27, 26);
            this.panelEx25.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx25.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx25.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx25.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx25.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx25.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx25.Style.GradientAngle = 90;
            this.panelEx25.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx25.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx25.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx25.TabIndex = 22;
            this.panelEx25.Text = "↗";
            this.panelEx25.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx88
            // 
            this.panelEx88.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx88.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx88.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx88.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx88.Location = new System.Drawing.Point(104, 125);
            this.panelEx88.Name = "panelEx88";
            this.panelEx88.Size = new System.Drawing.Size(27, 26);
            this.panelEx88.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx88.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx88.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx88.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx88.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx88.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx88.Style.GradientAngle = 90;
            this.panelEx88.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx88.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx88.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx88.TabIndex = 79;
            this.panelEx88.Text = "♥";
            this.panelEx88.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx26
            // 
            this.panelEx26.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx26.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx26.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx26.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx26.Location = new System.Drawing.Point(156, 25);
            this.panelEx26.Name = "panelEx26";
            this.panelEx26.Size = new System.Drawing.Size(27, 26);
            this.panelEx26.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx26.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx26.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx26.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx26.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx26.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx26.Style.GradientAngle = 90;
            this.panelEx26.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx26.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx26.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx26.TabIndex = 21;
            this.panelEx26.Text = "↖";
            this.panelEx26.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx87
            // 
            this.panelEx87.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx87.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx87.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx87.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx87.Location = new System.Drawing.Point(130, 125);
            this.panelEx87.Name = "panelEx87";
            this.panelEx87.Size = new System.Drawing.Size(27, 26);
            this.panelEx87.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx87.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx87.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx87.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx87.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx87.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx87.Style.GradientAngle = 90;
            this.panelEx87.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx87.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx87.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx87.TabIndex = 80;
            this.panelEx87.Text = "♡";
            this.panelEx87.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx27
            // 
            this.panelEx27.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx27.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx27.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx27.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx27.Location = new System.Drawing.Point(130, 25);
            this.panelEx27.Name = "panelEx27";
            this.panelEx27.Size = new System.Drawing.Size(27, 26);
            this.panelEx27.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx27.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx27.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx27.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx27.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx27.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx27.Style.GradientAngle = 90;
            this.panelEx27.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx27.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx27.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx27.TabIndex = 20;
            this.panelEx27.Text = "↕";
            this.panelEx27.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx86
            // 
            this.panelEx86.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx86.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx86.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx86.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx86.Location = new System.Drawing.Point(156, 125);
            this.panelEx86.Name = "panelEx86";
            this.panelEx86.Size = new System.Drawing.Size(27, 26);
            this.panelEx86.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx86.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx86.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx86.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx86.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx86.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx86.Style.GradientAngle = 90;
            this.panelEx86.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx86.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx86.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx86.TabIndex = 81;
            this.panelEx86.Text = "★";
            this.panelEx86.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx28
            // 
            this.panelEx28.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx28.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx28.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx28.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx28.Location = new System.Drawing.Point(104, 25);
            this.panelEx28.Name = "panelEx28";
            this.panelEx28.Size = new System.Drawing.Size(27, 26);
            this.panelEx28.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx28.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx28.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx28.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx28.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx28.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx28.Style.GradientAngle = 90;
            this.panelEx28.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx28.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx28.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx28.TabIndex = 19;
            this.panelEx28.Text = "↔";
            this.panelEx28.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx85
            // 
            this.panelEx85.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx85.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx85.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx85.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx85.Location = new System.Drawing.Point(182, 125);
            this.panelEx85.Name = "panelEx85";
            this.panelEx85.Size = new System.Drawing.Size(27, 26);
            this.panelEx85.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx85.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx85.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx85.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx85.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx85.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx85.Style.GradientAngle = 90;
            this.panelEx85.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx85.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx85.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx85.TabIndex = 82;
            this.panelEx85.Text = "☆";
            this.panelEx85.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx29
            // 
            this.panelEx29.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx29.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx29.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx29.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx29.Location = new System.Drawing.Point(78, 25);
            this.panelEx29.Name = "panelEx29";
            this.panelEx29.Size = new System.Drawing.Size(27, 26);
            this.panelEx29.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx29.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx29.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx29.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx29.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx29.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx29.Style.GradientAngle = 90;
            this.panelEx29.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx29.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx29.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx29.TabIndex = 18;
            this.panelEx29.Text = "↓";
            this.panelEx29.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx84
            // 
            this.panelEx84.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx84.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx84.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx84.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx84.Location = new System.Drawing.Point(208, 125);
            this.panelEx84.Name = "panelEx84";
            this.panelEx84.Size = new System.Drawing.Size(27, 26);
            this.panelEx84.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx84.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx84.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx84.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx84.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx84.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx84.Style.GradientAngle = 90;
            this.panelEx84.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx84.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx84.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx84.TabIndex = 83;
            this.panelEx84.Text = "☎";
            this.panelEx84.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx30
            // 
            this.panelEx30.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx30.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx30.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx30.Location = new System.Drawing.Point(52, 25);
            this.panelEx30.Name = "panelEx30";
            this.panelEx30.Size = new System.Drawing.Size(27, 26);
            this.panelEx30.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx30.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx30.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx30.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx30.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx30.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx30.Style.GradientAngle = 90;
            this.panelEx30.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx30.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx30.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx30.TabIndex = 17;
            this.panelEx30.Text = "→";
            this.panelEx30.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx83
            // 
            this.panelEx83.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx83.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx83.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx83.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx83.Location = new System.Drawing.Point(234, 125);
            this.panelEx83.Name = "panelEx83";
            this.panelEx83.Size = new System.Drawing.Size(27, 26);
            this.panelEx83.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx83.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx83.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx83.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx83.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx83.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx83.Style.GradientAngle = 90;
            this.panelEx83.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx83.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx83.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx83.TabIndex = 84;
            this.panelEx83.Text = "☏";
            this.panelEx83.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx31
            // 
            this.panelEx31.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx31.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx31.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx31.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx31.Location = new System.Drawing.Point(26, 25);
            this.panelEx31.Name = "panelEx31";
            this.panelEx31.Size = new System.Drawing.Size(27, 26);
            this.panelEx31.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx31.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx31.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx31.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx31.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx31.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx31.Style.GradientAngle = 90;
            this.panelEx31.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx31.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx31.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx31.TabIndex = 16;
            this.panelEx31.Text = "↑";
            this.panelEx31.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx82
            // 
            this.panelEx82.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx82.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx82.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx82.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx82.Location = new System.Drawing.Point(260, 125);
            this.panelEx82.Name = "panelEx82";
            this.panelEx82.Size = new System.Drawing.Size(27, 26);
            this.panelEx82.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx82.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx82.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx82.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx82.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx82.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx82.Style.GradientAngle = 90;
            this.panelEx82.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx82.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx82.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx82.TabIndex = 85;
            this.panelEx82.Text = "♀";
            this.panelEx82.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx32
            // 
            this.panelEx32.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx32.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx32.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx32.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx32.Location = new System.Drawing.Point(0, 25);
            this.panelEx32.Name = "panelEx32";
            this.panelEx32.Size = new System.Drawing.Size(27, 26);
            this.panelEx32.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx32.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx32.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx32.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx32.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx32.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx32.Style.GradientAngle = 90;
            this.panelEx32.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx32.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx32.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx32.TabIndex = 15;
            this.panelEx32.Text = "←";
            this.panelEx32.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx81
            // 
            this.panelEx81.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx81.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx81.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx81.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx81.Location = new System.Drawing.Point(286, 125);
            this.panelEx81.Name = "panelEx81";
            this.panelEx81.Size = new System.Drawing.Size(27, 26);
            this.panelEx81.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx81.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx81.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx81.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx81.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx81.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx81.Style.GradientAngle = 90;
            this.panelEx81.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx81.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx81.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx81.TabIndex = 86;
            this.panelEx81.Text = "♂";
            this.panelEx81.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx19
            // 
            this.panelEx19.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx19.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx19.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx19.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx19.Location = new System.Drawing.Point(364, 0);
            this.panelEx19.Name = "panelEx19";
            this.panelEx19.Size = new System.Drawing.Size(27, 26);
            this.panelEx19.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx19.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx19.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx19.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx19.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx19.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx19.Style.GradientAngle = 90;
            this.panelEx19.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx19.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx19.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx19.TabIndex = 14;
            this.panelEx19.Text = "⑮";
            this.panelEx19.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx107
            // 
            this.panelEx107.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx107.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx107.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx107.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx107.Location = new System.Drawing.Point(0, 150);
            this.panelEx107.Name = "panelEx107";
            this.panelEx107.Size = new System.Drawing.Size(27, 26);
            this.panelEx107.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx107.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx107.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx107.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx107.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx107.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx107.Style.GradientAngle = 90;
            this.panelEx107.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx107.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx107.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx107.TabIndex = 90;
            this.panelEx107.Text = "·";
            this.panelEx107.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx16
            // 
            this.panelEx16.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx16.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx16.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx16.Location = new System.Drawing.Point(338, 0);
            this.panelEx16.Name = "panelEx16";
            this.panelEx16.Size = new System.Drawing.Size(27, 26);
            this.panelEx16.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx16.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx16.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx16.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx16.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx16.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx16.Style.GradientAngle = 90;
            this.panelEx16.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx16.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx16.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx16.TabIndex = 13;
            this.panelEx16.Text = "⑭";
            this.panelEx16.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx106
            // 
            this.panelEx106.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx106.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx106.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx106.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx106.Location = new System.Drawing.Point(26, 150);
            this.panelEx106.Name = "panelEx106";
            this.panelEx106.Size = new System.Drawing.Size(27, 26);
            this.panelEx106.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx106.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx106.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx106.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx106.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx106.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx106.Style.GradientAngle = 90;
            this.panelEx106.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx106.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx106.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx106.TabIndex = 91;
            this.panelEx106.Text = "¤";
            this.panelEx106.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx17
            // 
            this.panelEx17.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx17.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx17.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx17.Location = new System.Drawing.Point(312, 0);
            this.panelEx17.Name = "panelEx17";
            this.panelEx17.Size = new System.Drawing.Size(27, 26);
            this.panelEx17.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx17.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx17.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx17.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx17.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx17.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx17.Style.GradientAngle = 90;
            this.panelEx17.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx17.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx17.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx17.TabIndex = 12;
            this.panelEx17.Text = "⑬";
            this.panelEx17.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx105
            // 
            this.panelEx105.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx105.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx105.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx105.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx105.Location = new System.Drawing.Point(52, 150);
            this.panelEx105.Name = "panelEx105";
            this.panelEx105.Size = new System.Drawing.Size(27, 26);
            this.panelEx105.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx105.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx105.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx105.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx105.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx105.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx105.Style.GradientAngle = 90;
            this.panelEx105.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx105.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx105.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx105.TabIndex = 92;
            this.panelEx105.Text = "§";
            this.panelEx105.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx15
            // 
            this.panelEx15.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx15.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx15.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx15.Location = new System.Drawing.Point(286, 0);
            this.panelEx15.Name = "panelEx15";
            this.panelEx15.Size = new System.Drawing.Size(27, 26);
            this.panelEx15.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx15.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx15.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx15.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx15.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx15.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx15.Style.GradientAngle = 90;
            this.panelEx15.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx15.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx15.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx15.TabIndex = 11;
            this.panelEx15.Text = "⑫";
            this.panelEx15.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx104
            // 
            this.panelEx104.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx104.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx104.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx104.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx104.Location = new System.Drawing.Point(78, 150);
            this.panelEx104.Name = "panelEx104";
            this.panelEx104.Size = new System.Drawing.Size(27, 26);
            this.panelEx104.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx104.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx104.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx104.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx104.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx104.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx104.Style.GradientAngle = 90;
            this.panelEx104.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx104.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx104.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx104.TabIndex = 93;
            this.panelEx104.Text = "+";
            this.panelEx104.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx14
            // 
            this.panelEx14.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx14.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx14.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx14.Location = new System.Drawing.Point(260, 0);
            this.panelEx14.Name = "panelEx14";
            this.panelEx14.Size = new System.Drawing.Size(27, 26);
            this.panelEx14.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx14.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx14.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx14.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx14.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx14.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx14.Style.GradientAngle = 90;
            this.panelEx14.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx14.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx14.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx14.TabIndex = 10;
            this.panelEx14.Text = "⑪";
            this.panelEx14.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx103
            // 
            this.panelEx103.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx103.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx103.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx103.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx103.Location = new System.Drawing.Point(104, 150);
            this.panelEx103.Name = "panelEx103";
            this.panelEx103.Size = new System.Drawing.Size(27, 26);
            this.panelEx103.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx103.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx103.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx103.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx103.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx103.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx103.Style.GradientAngle = 90;
            this.panelEx103.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx103.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx103.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx103.TabIndex = 94;
            this.panelEx103.Text = "-";
            this.panelEx103.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx13
            // 
            this.panelEx13.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx13.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx13.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx13.Location = new System.Drawing.Point(234, 0);
            this.panelEx13.Name = "panelEx13";
            this.panelEx13.Size = new System.Drawing.Size(27, 26);
            this.panelEx13.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx13.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx13.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx13.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx13.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx13.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx13.Style.GradientAngle = 90;
            this.panelEx13.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx13.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx13.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx13.TabIndex = 9;
            this.panelEx13.Text = "⑩";
            this.panelEx13.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx102
            // 
            this.panelEx102.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx102.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx102.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx102.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx102.Location = new System.Drawing.Point(182, 150);
            this.panelEx102.Name = "panelEx102";
            this.panelEx102.Size = new System.Drawing.Size(27, 26);
            this.panelEx102.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx102.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx102.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx102.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx102.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx102.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx102.Style.GradientAngle = 90;
            this.panelEx102.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx102.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx102.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx102.TabIndex = 95;
            this.panelEx102.Text = "｛";
            this.panelEx102.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx12
            // 
            this.panelEx12.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx12.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx12.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx12.Location = new System.Drawing.Point(208, 0);
            this.panelEx12.Name = "panelEx12";
            this.panelEx12.Size = new System.Drawing.Size(27, 26);
            this.panelEx12.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx12.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx12.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx12.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx12.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx12.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx12.Style.GradientAngle = 90;
            this.panelEx12.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx12.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx12.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx12.TabIndex = 8;
            this.panelEx12.Text = "⑨";
            this.panelEx12.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx78
            // 
            this.panelEx78.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx78.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx78.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx78.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx78.Location = new System.Drawing.Point(286, 150);
            this.panelEx78.Name = "panelEx78";
            this.panelEx78.Size = new System.Drawing.Size(27, 26);
            this.panelEx78.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx78.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx78.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx78.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx78.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx78.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx78.Style.GradientAngle = 90;
            this.panelEx78.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx78.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx78.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx78.TabIndex = 89;
            this.panelEx78.Text = "%";
            this.panelEx78.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx11
            // 
            this.panelEx11.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx11.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx11.Location = new System.Drawing.Point(182, 0);
            this.panelEx11.Name = "panelEx11";
            this.panelEx11.Size = new System.Drawing.Size(27, 26);
            this.panelEx11.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx11.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx11.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx11.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx11.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx11.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx11.Style.GradientAngle = 90;
            this.panelEx11.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx11.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx11.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx11.TabIndex = 7;
            this.panelEx11.Text = "⑧";
            this.panelEx11.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx101
            // 
            this.panelEx101.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx101.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx101.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx101.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx101.Location = new System.Drawing.Point(208, 150);
            this.panelEx101.Name = "panelEx101";
            this.panelEx101.Size = new System.Drawing.Size(27, 26);
            this.panelEx101.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx101.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx101.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx101.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx101.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx101.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx101.Style.GradientAngle = 90;
            this.panelEx101.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx101.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx101.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx101.TabIndex = 96;
            this.panelEx101.Text = "｝";
            this.panelEx101.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx10
            // 
            this.panelEx10.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx10.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx10.Location = new System.Drawing.Point(156, 0);
            this.panelEx10.Name = "panelEx10";
            this.panelEx10.Size = new System.Drawing.Size(27, 26);
            this.panelEx10.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx10.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx10.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx10.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx10.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx10.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx10.Style.GradientAngle = 90;
            this.panelEx10.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx10.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx10.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx10.TabIndex = 6;
            this.panelEx10.Text = "⑦";
            this.panelEx10.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx79
            // 
            this.panelEx79.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx79.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx79.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx79.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx79.Location = new System.Drawing.Point(260, 150);
            this.panelEx79.Name = "panelEx79";
            this.panelEx79.Size = new System.Drawing.Size(27, 26);
            this.panelEx79.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx79.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx79.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx79.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx79.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx79.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx79.Style.GradientAngle = 90;
            this.panelEx79.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx79.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx79.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx79.TabIndex = 88;
            this.panelEx79.Text = "@";
            this.panelEx79.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx9.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx9.Location = new System.Drawing.Point(130, 0);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(27, 26);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx9.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx9.TabIndex = 5;
            this.panelEx9.Text = "⑥";
            this.panelEx9.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx80
            // 
            this.panelEx80.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx80.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx80.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx80.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx80.Location = new System.Drawing.Point(234, 150);
            this.panelEx80.Name = "panelEx80";
            this.panelEx80.Size = new System.Drawing.Size(27, 26);
            this.panelEx80.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx80.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx80.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx80.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx80.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx80.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx80.Style.GradientAngle = 90;
            this.panelEx80.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx80.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx80.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx80.TabIndex = 87;
            this.panelEx80.Text = "\\";
            this.panelEx80.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx8.Location = new System.Drawing.Point(104, 0);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(27, 26);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx8.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx8.TabIndex = 4;
            this.panelEx8.Text = "⑤";
            this.panelEx8.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx35
            // 
            this.panelEx35.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx35.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx35.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx35.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx35.Location = new System.Drawing.Point(364, 150);
            this.panelEx35.Name = "panelEx35";
            this.panelEx35.Size = new System.Drawing.Size(27, 26);
            this.panelEx35.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx35.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx35.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx35.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx35.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx35.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx35.Style.GradientAngle = 90;
            this.panelEx35.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx35.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx35.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx35.TabIndex = 97;
            this.panelEx35.Text = "˚";
            this.panelEx35.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx7.Location = new System.Drawing.Point(78, 0);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(27, 26);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx7.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx7.TabIndex = 3;
            this.panelEx7.Text = "④";
            this.panelEx7.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx33
            // 
            this.panelEx33.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx33.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx33.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx33.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx33.Location = new System.Drawing.Point(312, 150);
            this.panelEx33.Name = "panelEx33";
            this.panelEx33.Size = new System.Drawing.Size(27, 26);
            this.panelEx33.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx33.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx33.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx33.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx33.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx33.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx33.Style.GradientAngle = 90;
            this.panelEx33.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx33.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx33.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx33.TabIndex = 99;
            this.panelEx33.Text = "ˆ";
            this.panelEx33.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx6.Location = new System.Drawing.Point(52, 0);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(27, 26);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx6.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx6.TabIndex = 2;
            this.panelEx6.Text = "③";
            this.panelEx6.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx18
            // 
            this.panelEx18.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx18.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx18.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx18.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx18.Location = new System.Drawing.Point(338, 150);
            this.panelEx18.Name = "panelEx18";
            this.panelEx18.Size = new System.Drawing.Size(27, 26);
            this.panelEx18.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx18.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx18.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx18.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx18.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx18.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx18.Style.GradientAngle = 90;
            this.panelEx18.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx18.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx18.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx18.TabIndex = 100;
            this.panelEx18.Text = "ˇ";
            this.panelEx18.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx5.Location = new System.Drawing.Point(26, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(27, 26);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx5.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx5.TabIndex = 1;
            this.panelEx5.Text = "②";
            this.panelEx5.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx48
            // 
            this.panelEx48.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx48.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx48.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx48.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx48.Location = new System.Drawing.Point(130, 150);
            this.panelEx48.Name = "panelEx48";
            this.panelEx48.Size = new System.Drawing.Size(27, 26);
            this.panelEx48.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx48.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx48.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx48.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx48.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx48.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx48.Style.GradientAngle = 90;
            this.panelEx48.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx48.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx48.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx48.TabIndex = 101;
            this.panelEx48.Text = "×";
            this.panelEx48.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // Pan_Moon_1
            // 
            this.Pan_Moon_1.CanvasColor = System.Drawing.SystemColors.Control;
            this.Pan_Moon_1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Pan_Moon_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pan_Moon_1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Pan_Moon_1.Location = new System.Drawing.Point(0, 0);
            this.Pan_Moon_1.Name = "Pan_Moon_1";
            this.Pan_Moon_1.Size = new System.Drawing.Size(27, 26);
            this.Pan_Moon_1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Pan_Moon_1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Pan_Moon_1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Pan_Moon_1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Pan_Moon_1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.Pan_Moon_1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Pan_Moon_1.Style.GradientAngle = 90;
            this.Pan_Moon_1.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.Pan_Moon_1.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.Pan_Moon_1.StyleMouseOver.BackColor1.Color = System.Drawing.Color.White;
            this.Pan_Moon_1.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.Pan_Moon_1.TabIndex = 0;
            this.Pan_Moon_1.Text = "①";
            this.Pan_Moon_1.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // panelEx34
            // 
            this.panelEx34.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx34.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx34.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelEx34.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelEx34.Location = new System.Drawing.Point(156, 150);
            this.panelEx34.Name = "panelEx34";
            this.panelEx34.Size = new System.Drawing.Size(27, 26);
            this.panelEx34.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx34.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx34.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx34.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx34.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx34.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx34.Style.GradientAngle = 90;
            this.panelEx34.StyleMouseDown.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx34.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx34.StyleMouseOver.BackColor2.Color = System.Drawing.Color.DodgerBlue;
            this.panelEx34.TabIndex = 102;
            this.panelEx34.Text = "÷";
            this.panelEx34.Click += new System.EventHandler(this.Pan_Moon_1_Click);
            // 
            // Pan_MoonJa
            // 
            this.Pan_MoonJa.CanvasColor = System.Drawing.SystemColors.Control;
            this.Pan_MoonJa.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Pan_MoonJa.Controls.Add(this.panelEx34);
            this.Pan_MoonJa.Controls.Add(this.Pan_Moon_1);
            this.Pan_MoonJa.Controls.Add(this.panelEx48);
            this.Pan_MoonJa.Controls.Add(this.panelEx5);
            this.Pan_MoonJa.Controls.Add(this.panelEx18);
            this.Pan_MoonJa.Controls.Add(this.panelEx6);
            this.Pan_MoonJa.Controls.Add(this.panelEx33);
            this.Pan_MoonJa.Controls.Add(this.panelEx7);
            this.Pan_MoonJa.Controls.Add(this.panelEx35);
            this.Pan_MoonJa.Controls.Add(this.panelEx8);
            this.Pan_MoonJa.Controls.Add(this.panelEx80);
            this.Pan_MoonJa.Controls.Add(this.panelEx9);
            this.Pan_MoonJa.Controls.Add(this.panelEx79);
            this.Pan_MoonJa.Controls.Add(this.panelEx10);
            this.Pan_MoonJa.Controls.Add(this.panelEx101);
            this.Pan_MoonJa.Controls.Add(this.panelEx11);
            this.Pan_MoonJa.Controls.Add(this.panelEx78);
            this.Pan_MoonJa.Controls.Add(this.panelEx12);
            this.Pan_MoonJa.Controls.Add(this.panelEx102);
            this.Pan_MoonJa.Controls.Add(this.panelEx13);
            this.Pan_MoonJa.Controls.Add(this.panelEx103);
            this.Pan_MoonJa.Controls.Add(this.panelEx14);
            this.Pan_MoonJa.Controls.Add(this.panelEx104);
            this.Pan_MoonJa.Controls.Add(this.panelEx15);
            this.Pan_MoonJa.Controls.Add(this.panelEx105);
            this.Pan_MoonJa.Controls.Add(this.panelEx17);
            this.Pan_MoonJa.Controls.Add(this.panelEx106);
            this.Pan_MoonJa.Controls.Add(this.panelEx16);
            this.Pan_MoonJa.Controls.Add(this.panelEx107);
            this.Pan_MoonJa.Controls.Add(this.panelEx19);
            this.Pan_MoonJa.Controls.Add(this.panelEx81);
            this.Pan_MoonJa.Controls.Add(this.panelEx32);
            this.Pan_MoonJa.Controls.Add(this.panelEx82);
            this.Pan_MoonJa.Controls.Add(this.panelEx31);
            this.Pan_MoonJa.Controls.Add(this.panelEx83);
            this.Pan_MoonJa.Controls.Add(this.panelEx30);
            this.Pan_MoonJa.Controls.Add(this.panelEx84);
            this.Pan_MoonJa.Controls.Add(this.panelEx29);
            this.Pan_MoonJa.Controls.Add(this.panelEx85);
            this.Pan_MoonJa.Controls.Add(this.panelEx28);
            this.Pan_MoonJa.Controls.Add(this.panelEx86);
            this.Pan_MoonJa.Controls.Add(this.panelEx27);
            this.Pan_MoonJa.Controls.Add(this.panelEx87);
            this.Pan_MoonJa.Controls.Add(this.panelEx26);
            this.Pan_MoonJa.Controls.Add(this.panelEx88);
            this.Pan_MoonJa.Controls.Add(this.panelEx25);
            this.Pan_MoonJa.Controls.Add(this.panelEx89);
            this.Pan_MoonJa.Controls.Add(this.panelEx24);
            this.Pan_MoonJa.Controls.Add(this.panelEx90);
            this.Pan_MoonJa.Controls.Add(this.panelEx23);
            this.Pan_MoonJa.Controls.Add(this.panelEx91);
            this.Pan_MoonJa.Controls.Add(this.panelEx22);
            this.Pan_MoonJa.Controls.Add(this.panelEx92);
            this.Pan_MoonJa.Controls.Add(this.panelEx21);
            this.Pan_MoonJa.Controls.Add(this.panelEx64);
            this.Pan_MoonJa.Controls.Add(this.panelEx20);
            this.Pan_MoonJa.Controls.Add(this.panelEx65);
            this.Pan_MoonJa.Controls.Add(this.panelEx36);
            this.Pan_MoonJa.Controls.Add(this.panelEx66);
            this.Pan_MoonJa.Controls.Add(this.panelEx47);
            this.Pan_MoonJa.Controls.Add(this.panelEx67);
            this.Pan_MoonJa.Controls.Add(this.panelEx46);
            this.Pan_MoonJa.Controls.Add(this.panelEx68);
            this.Pan_MoonJa.Controls.Add(this.panelEx45);
            this.Pan_MoonJa.Controls.Add(this.panelEx69);
            this.Pan_MoonJa.Controls.Add(this.panelEx44);
            this.Pan_MoonJa.Controls.Add(this.panelEx70);
            this.Pan_MoonJa.Controls.Add(this.panelEx43);
            this.Pan_MoonJa.Controls.Add(this.panelEx71);
            this.Pan_MoonJa.Controls.Add(this.panelEx42);
            this.Pan_MoonJa.Controls.Add(this.panelEx72);
            this.Pan_MoonJa.Controls.Add(this.panelEx41);
            this.Pan_MoonJa.Controls.Add(this.panelEx73);
            this.Pan_MoonJa.Controls.Add(this.panelEx40);
            this.Pan_MoonJa.Controls.Add(this.panelEx74);
            this.Pan_MoonJa.Controls.Add(this.panelEx39);
            this.Pan_MoonJa.Controls.Add(this.panelEx75);
            this.Pan_MoonJa.Controls.Add(this.panelEx38);
            this.Pan_MoonJa.Controls.Add(this.panelEx76);
            this.Pan_MoonJa.Controls.Add(this.panelEx37);
            this.Pan_MoonJa.Controls.Add(this.panelEx77);
            this.Pan_MoonJa.Controls.Add(this.panelEx49);
            this.Pan_MoonJa.Controls.Add(this.panelEx52);
            this.Pan_MoonJa.Controls.Add(this.panelEx62);
            this.Pan_MoonJa.Controls.Add(this.panelEx53);
            this.Pan_MoonJa.Controls.Add(this.panelEx61);
            this.Pan_MoonJa.Controls.Add(this.panelEx54);
            this.Pan_MoonJa.Controls.Add(this.panelEx60);
            this.Pan_MoonJa.Controls.Add(this.panelEx55);
            this.Pan_MoonJa.Controls.Add(this.panelEx59);
            this.Pan_MoonJa.Controls.Add(this.panelEx56);
            this.Pan_MoonJa.Controls.Add(this.panelEx58);
            this.Pan_MoonJa.Controls.Add(this.panelEx57);
            this.Pan_MoonJa.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Pan_MoonJa.Location = new System.Drawing.Point(334, 0);
            this.Pan_MoonJa.Name = "Pan_MoonJa";
            this.Pan_MoonJa.Size = new System.Drawing.Size(391, 176);
            this.Pan_MoonJa.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Pan_MoonJa.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Pan_MoonJa.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Pan_MoonJa.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Pan_MoonJa.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.Pan_MoonJa.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Pan_MoonJa.Style.GradientAngle = 90;
            this.Pan_MoonJa.TabIndex = 10220;
            this.Pan_MoonJa.Visible = false;
            // 
            // RichTextBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Pan_MoonJa);
            this.Controls.Add(this.contextMenuBar1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            this.Pan_MoonJa.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem ContextMainMenu;
        private DevComponents.DotNetBar.ButtonItem Btn_S_01;
        private DevComponents.DotNetBar.ButtonItem Btn_S_02;
        private DevComponents.DotNetBar.ButtonItem Btn_S_03;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem Btn_S_04;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem Btn_S_05;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.ButtonItem Btn_S_06;
        private DevComponents.DotNetBar.ButtonItem Btn_S_07;
        internal System.Windows.Forms.FontDialog FontDialog1;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        private DevComponents.DotNetBar.ButtonItem Btn_S_08;
        private DevComponents.DotNetBar.PanelEx panelEx57;
        private DevComponents.DotNetBar.PanelEx panelEx58;
        private DevComponents.DotNetBar.PanelEx panelEx56;
        private DevComponents.DotNetBar.PanelEx panelEx59;
        private DevComponents.DotNetBar.PanelEx panelEx55;
        private DevComponents.DotNetBar.PanelEx panelEx60;
        private DevComponents.DotNetBar.PanelEx panelEx54;
        private DevComponents.DotNetBar.PanelEx panelEx61;
        private DevComponents.DotNetBar.PanelEx panelEx53;
        private DevComponents.DotNetBar.PanelEx panelEx62;
        private DevComponents.DotNetBar.PanelEx panelEx52;
        private DevComponents.DotNetBar.PanelEx panelEx49;
        private DevComponents.DotNetBar.PanelEx panelEx77;
        private DevComponents.DotNetBar.PanelEx panelEx37;
        private DevComponents.DotNetBar.PanelEx panelEx76;
        private DevComponents.DotNetBar.PanelEx panelEx38;
        private DevComponents.DotNetBar.PanelEx panelEx75;
        private DevComponents.DotNetBar.PanelEx panelEx39;
        private DevComponents.DotNetBar.PanelEx panelEx74;
        private DevComponents.DotNetBar.PanelEx panelEx40;
        private DevComponents.DotNetBar.PanelEx panelEx73;
        private DevComponents.DotNetBar.PanelEx panelEx41;
        private DevComponents.DotNetBar.PanelEx panelEx72;
        private DevComponents.DotNetBar.PanelEx panelEx42;
        private DevComponents.DotNetBar.PanelEx panelEx71;
        private DevComponents.DotNetBar.PanelEx panelEx43;
        private DevComponents.DotNetBar.PanelEx panelEx70;
        private DevComponents.DotNetBar.PanelEx panelEx44;
        private DevComponents.DotNetBar.PanelEx panelEx69;
        private DevComponents.DotNetBar.PanelEx panelEx45;
        private DevComponents.DotNetBar.PanelEx panelEx68;
        private DevComponents.DotNetBar.PanelEx panelEx46;
        private DevComponents.DotNetBar.PanelEx panelEx67;
        private DevComponents.DotNetBar.PanelEx panelEx47;
        private DevComponents.DotNetBar.PanelEx panelEx66;
        private DevComponents.DotNetBar.PanelEx panelEx36;
        private DevComponents.DotNetBar.PanelEx panelEx65;
        private DevComponents.DotNetBar.PanelEx panelEx20;
        private DevComponents.DotNetBar.PanelEx panelEx64;
        private DevComponents.DotNetBar.PanelEx panelEx21;
        private DevComponents.DotNetBar.PanelEx panelEx92;
        private DevComponents.DotNetBar.PanelEx panelEx22;
        private DevComponents.DotNetBar.PanelEx panelEx91;
        private DevComponents.DotNetBar.PanelEx panelEx23;
        private DevComponents.DotNetBar.PanelEx panelEx90;
        private DevComponents.DotNetBar.PanelEx panelEx24;
        private DevComponents.DotNetBar.PanelEx panelEx89;
        private DevComponents.DotNetBar.PanelEx panelEx25;
        private DevComponents.DotNetBar.PanelEx panelEx88;
        private DevComponents.DotNetBar.PanelEx panelEx26;
        private DevComponents.DotNetBar.PanelEx panelEx87;
        private DevComponents.DotNetBar.PanelEx panelEx27;
        private DevComponents.DotNetBar.PanelEx panelEx86;
        private DevComponents.DotNetBar.PanelEx panelEx28;
        private DevComponents.DotNetBar.PanelEx panelEx85;
        private DevComponents.DotNetBar.PanelEx panelEx29;
        private DevComponents.DotNetBar.PanelEx panelEx84;
        private DevComponents.DotNetBar.PanelEx panelEx30;
        private DevComponents.DotNetBar.PanelEx panelEx83;
        private DevComponents.DotNetBar.PanelEx panelEx31;
        private DevComponents.DotNetBar.PanelEx panelEx82;
        private DevComponents.DotNetBar.PanelEx panelEx32;
        private DevComponents.DotNetBar.PanelEx panelEx81;
        private DevComponents.DotNetBar.PanelEx panelEx19;
        private DevComponents.DotNetBar.PanelEx panelEx107;
        private DevComponents.DotNetBar.PanelEx panelEx16;
        private DevComponents.DotNetBar.PanelEx panelEx106;
        private DevComponents.DotNetBar.PanelEx panelEx17;
        private DevComponents.DotNetBar.PanelEx panelEx105;
        private DevComponents.DotNetBar.PanelEx panelEx15;
        private DevComponents.DotNetBar.PanelEx panelEx104;
        private DevComponents.DotNetBar.PanelEx panelEx14;
        private DevComponents.DotNetBar.PanelEx panelEx103;
        private DevComponents.DotNetBar.PanelEx panelEx13;
        private DevComponents.DotNetBar.PanelEx panelEx102;
        private DevComponents.DotNetBar.PanelEx panelEx12;
        private DevComponents.DotNetBar.PanelEx panelEx78;
        private DevComponents.DotNetBar.PanelEx panelEx11;
        private DevComponents.DotNetBar.PanelEx panelEx101;
        private DevComponents.DotNetBar.PanelEx panelEx10;
        private DevComponents.DotNetBar.PanelEx panelEx79;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private DevComponents.DotNetBar.PanelEx panelEx80;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private DevComponents.DotNetBar.PanelEx panelEx35;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.PanelEx panelEx33;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.PanelEx panelEx18;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx48;
        private DevComponents.DotNetBar.PanelEx Pan_Moon_1;
        private DevComponents.DotNetBar.PanelEx panelEx34;
        private DevComponents.DotNetBar.PanelEx Pan_MoonJa;
        private DevComponents.DotNetBar.LabelItem labelItem4;

        #endregion
    }

}
