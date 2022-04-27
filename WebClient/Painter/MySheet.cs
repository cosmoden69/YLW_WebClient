using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using YLW_WebClient.Painter.Controller;
using YLW_WebClient.Painter.PaintControls;

namespace YLW_WebClient.Painter
{
    [Serializable, ToolboxItem(true), Browsable(true)]
    public partial class MySheet : PictureBox
    {
        /// <summary>
        /// DrawObject를 그릴수 있는 DrawBox 사이즈를 수정 할 수 있는지 여부를 저장하는 변수
        /// </summary>
        private bool allowResize = false;

        internal bool _bLoading = false;
        internal bool _bKeyControl;
        internal bool _bKeyShift;
        internal BasePosition _basePosition;
        internal Point _currentMousePoint;

        private bool _bObjectCreateMode;
        private bool _bObjectSelectionMode;
        private bool _bMousePointerOnObject;
        private ObjectList _objectList;
        private MyTextBox _myText;
        private System.Windows.Forms.PictureBox _rtfBase;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image BaseImage { get; set; }

        public Image CanvasImage { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point CurrentMousePoint { get { return _currentMousePoint; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TransparentColor { get { return this.BackColor; } }

        UndoStack undoStack = new UndoStack();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UndoStack UndoStack
        {
            get
            {
                return undoStack;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ObjectCreateMode
        {
            get
            {
                return this._bObjectCreateMode;
            }
            set
            {
                this._bObjectCreateMode = value;
                this.OnObjectModeChanged();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ObjectSelectionMode
        {
            get
            {
                return this._bObjectSelectionMode;
            }
            set
            {
                this._bObjectSelectionMode = value;
                this.OnObjectModeChanged();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MousePointerOnObject
        {
            get
            {
                return this._bMousePointerOnObject;
            }
            set
            {
                this._bMousePointerOnObject = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectCreatorType ActiveObjectCreator
        {
            get
            {
                return MainController.Instance.CurrentCreator;
            }
            set
            {
                if (value != ObjectCreatorType.Selector)
                {
                    this.ObjectSelectionMode = false;
                }
                if ((value == ObjectCreatorType.None) || (value == ObjectCreatorType.Selector))
                {
                    this.ObjectCreateMode = false;
                }
                else
                {
                    this.ObjectCreateMode = true;
                }
                MainController.Instance.CurrentCreator = value;
                ObserverClass obs = new ObserverClass("MySheet");
                obs.Action = ObserverAction.ChangeCreator;
                obs.Name = ObserverName.MySheet;
                if (MainController.Instance.ToolBar != null) MainController.Instance.ToolBar.OnNext(obs);
                this.OnObjectModeChanged();
            }
        }

        private ObjectToolNone toolNone = null;
        private ObjectToolSelector toolSelector = null;
        private ObjectToolPencil toolPencil = null;
        private ObjectToolEraser toolEraser = null;
        private ObjectToolFill toolFill = null;
        private ObjectToolText toolText = null;
        private ObjectToolLine toolLine = null;
        private ObjectToolBox toolBox = null;
        private ObjectToolCircle toolCircle = null;
        private ObjectToolArrow toolArrow = null;
        private ObjectToolBrokenLine toolBrokenLine = null;
        private ObjectToolStar toolStar = null;
        private ObjectToolImage toolImage = null;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectToolBase ObjectTools
        {
            get
            {
                switch (MainController.Instance.CurrentCreator)
                {
                    case ObjectCreatorType.None:
                        return toolNone;
                    case ObjectCreatorType.Selector:
                        return toolSelector;
                    case ObjectCreatorType.Pencil:
                        return toolPencil;
                    case ObjectCreatorType.Eraser:
                        return toolEraser;
                    case ObjectCreatorType.Fill:
                        return toolFill;
                    case ObjectCreatorType.Text:
                        return toolText;
                    case ObjectCreatorType.Line:
                        return toolLine;
                    case ObjectCreatorType.Box:
                        return toolBox;
                    case ObjectCreatorType.Circle:
                        return toolCircle;
                    case ObjectCreatorType.Arrow:
                        return toolArrow;
                    case ObjectCreatorType.BrokenLine:
                        return toolBrokenLine;
                    case ObjectCreatorType.Star:
                        return toolStar;
                    case ObjectCreatorType.Image:
                        return toolImage;
                    default:
                        return toolNone;
                }
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectToolBrokenLine ToolBrokenLine { get { return toolBrokenLine; } }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolBar ToolBar { get { return MainController.Instance.ToolBar; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectList ObjectList
        {
            get
            {
                return this._objectList;
            }
            set
            {
                this._objectList = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MyTextBox MyText
        {
            get
            {
                return this._myText;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PictureBox RtfBase
        {
            get
            {
                return this._rtfBase;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle WorkingArea
        {
            get
            {
                return new Rectangle(this._basePosition.Position.X, this._basePosition.Position.Y, this.Width - this._basePosition.Position.X * 2, this.Height - this._basePosition.Position.Y * 2);
            }
        }

        public event EventHandler NeedContextMenu;
        public event EventHandler ObjectModeChanged;
        public event EventHandler ObjectSelected;
        public event ObjectEventArgs.ObjectEventHandler SaveButtonClicked;


        public MySheet()
        {
            InitializeComponent();

            toolNone = new ObjectToolNone(this);
            toolSelector = new ObjectToolSelector(this);
            toolPencil = new ObjectToolPencil(this);
            toolEraser = new ObjectToolEraser(this);
            toolFill = new ObjectToolFill(this);
            toolText = new ObjectToolText(this);
            toolLine = new ObjectToolLine(this);
            toolBox = new ObjectToolBox(this);
            toolCircle = new ObjectToolCircle(this);
            toolArrow = new ObjectToolArrow(this);
            toolBrokenLine = new ObjectToolBrokenLine(this);
            toolStar = new ObjectToolStar(this);
            toolImage = new ObjectToolImage(this);

            _myText = new MyTextBox();
            _myText.Visible = false;
            this.Controls.Add(_myText);

            _rtfBase = new System.Windows.Forms.PictureBox();
            _rtfBase.Visible = false;
            _rtfBase.BackColor = SystemColors.Control;
            _rtfBase.Paint += RtfBase_Paint;

            base.SuspendLayout();
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.UserMouse, true);
            base.UpdateStyles();
            base.ResumeLayout(false);

            TabStop = true;

            this.pictureBox_ReSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ReSize_MouseDown);
            this.pictureBox_ReSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ReSize_MouseMove);
            this.pictureBox_ReSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ReSize_MouseUp);

            this._basePosition = new BasePosition(0, 0);
            this._currentMousePoint = new Point(0, 0);
            this.ObjectList = new ObjectList(this);
            this.ActiveObjectCreator = ObjectCreatorType.None;

            this._bLoading = true;
        }

        /// <summary>
        /// 수신된 ObserverAction 에 따라서 처리 한다.
        /// </summary>
        public void OnNext(ObserverAction action)
        {
            try
            { 
                switch (action)
                {
                    case ObserverAction.New: this.NewPage(this.BaseImage); return;
                    case ObserverAction.FileLoad: this.FileLoad(); return;
                    case ObserverAction.FileSave: this.FileSave(); return;
                    case ObserverAction.Invalidate: this.Invalidate(); return;
                    case ObserverAction.Undo: this.Undo(); return;
                    case ObserverAction.Redo: this.Redo(); return;
                    case ObserverAction.ChangeCreator: this.ActiveObjectCreatorChange(); return;
                    case ObserverAction.ChangeProperty: this.ObjectsPropertyChange(); return;
                    case ObserverAction.ChangeFontStyle: this.ObjectsFontStyleChange(); return;
                }
            }
            catch { }
        }

        public void NewPage(Image img = null)
        {
            try
            { 
                this.MyText.UnSetObject();
                this.ObjectList.Clear();
                this.undoStack.ClearAll();
                if (img == null)
                {
                    img = new Bitmap(800, 600);
                    using (Graphics g = Graphics.FromImage(img)) { g.Clear(this.TransparentColor); }
                }
                this.Size = img.Size;
                this.BaseImage = img;
                this.Image = (Bitmap)BaseImage.Clone();
                this.CanvasImage = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.CanvasImage)) { g.Clear(this.TransparentColor); }
                this.Invalidate();
            }
            catch { }
        }

        public void FileLoad()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.Filter = "이미지 파일(.png)|*.png|모든 파일(*.*)|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!File.Exists(dlg.FileName))
                {
                    MessageBox.Show("해당 파일이 없습니다");
                    return;
                }
                Bitmap img = null;
                try
                {
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            var ms = new MemoryStream(reader.ReadBytes((int)fs.Length));
                            img = new Bitmap(ms);
                        }
                    }
                    this.NewPage(img);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void FileSave()
        {
            try
            { 
                if (this.SaveButtonClicked != null)
                {
                    this.ObjectList.UnselectAll();
                    Bitmap img = GetResultImage();
                    if (img == null) return;
                    this.SaveButtonClicked(this, new ObjectEventArgs(img));
                }
            }
            catch { }

            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //dlg.Filter = "이미지 파일(.png)|*.png|모든 파일(*.*)|*.*";
            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        string ext = Path.GetExtension(dlg.FileName);
            //        System.Drawing.Imaging.ImageFormat fmt = System.Drawing.Imaging.ImageFormat.Png;
            //        this.ObjectList.UnselectAll();
            //        Bitmap img = GetResultImage();
            //        using (FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate))
            //        {
            //            img.Save(fs, fmt);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        public Bitmap GetResultImage()
        {
            try
            { 
                Bitmap img = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(this.TransparentColor);
                    g.DrawImage(this.Image, 0, 0);
                    Bitmap img1 = new Bitmap(this.CanvasImage);
                    img1.MakeTransparent(this.TransparentColor);
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    g.DrawImage(img1, 0, 0);
                    g.Dispose();
                }
                return img;
            }
            catch
            {
                return null;
            }
        }

        public void Undo()
        {
            try
            { 
                if (this.undoStack.CanUndo)
                {
                    this.undoStack.Undo();
                    this.ReDrawObjects();
                }
            }
            catch { }
        }

        public void Redo()
        {
            try
            { 
                if (this.undoStack.CanRedo)
                {
                    this.undoStack.Redo();
                    this.ReDrawObjects();
                }
            }
            catch { }
        }

        #region Undo Redo
        public void InsertObject(ObjectBase d)
        {
            try
            { 
                undoStack.Push(new UndoableInsert(this, d));
                this.ObjectList.Add(d);
            }
            catch { }
        }

        public void InsertObjects(List<ObjectBase> lst)
        {
            try
            { 
                undoStack.Push(new UndoableInserts(this, lst));
                foreach (ObjectBase d in lst)
                {
                    this.ObjectList.Add(d);
                }
            }
            catch { }
        }

        public void DeleteObject(ObjectBase d)
        {
            try
            { 
                undoStack.Push(new UndoableDelete(this, d));
                this.ObjectList.Remove(d);
            }
            catch { }
        }

        public void DeleteObjects(List<ObjectBase> lst)
        {
            try
            { 
                undoStack.Push(new UndoableDeletes(this, lst));
                foreach (ObjectBase d in lst)
                {
                    this.ObjectList.Remove(d);
                }
            }
            catch { }
        }

        #endregion

        private void ActiveObjectCreatorChange()
        {
            this.ActiveObjectCreator = MainController.Instance.CurrentCreator;
            if (this.ObjectList.SelectionCount > 0)
            {
                this.ObjectList.UnselectAll();
                base.Invalidate(true);
            }
        }

        private void ObjectsPropertyChange()
        {
            try
            {
                for (int i = this.ObjectList.Selection.Count - 1; i >= 0; i--)
                {
                    ObjectBase base2 = this.ObjectList.Selection[i];
                    base2.SetProperties();
                }
                this.Invalidate();
            }
            catch { }
        }

        private void ObjectsFontStyleChange()
        {
        }

        public void CancelObjectAccessMode()
        {
            try
            {
                if (this.ObjectCreateMode || this.ObjectSelectionMode || this.MousePointerOnObject)
                {
                    this.ObjectCreateMode = false;
                    this.ObjectSelectionMode = false;
                    this.MousePointerOnObject = false;
                    this.Cursor = new MyCursor().ObjectDefaultCursor;
                }
                this.ActiveObjectCreator = ObjectCreatorType.None;
                if (this.ObjectList.SelectionCount > 0)
                {
                    this.ObjectList.UnselectAll();
                    base.Invalidate(true);
                }
            }
            catch { }
        }

        public void DrawObject(ObjectBase base2)
        {
            try
            {
                if (base2.Selected) return;
                if (base2.Drawn) return;
                Graphics g = Graphics.FromImage(this.CanvasImage);
                if (base2.IntersectsWith(this.ClientRectangle))
                {
                    base2.Draw(this, g);
                    base2.Drawn = true;
                }
                g.Dispose();
                this.Invalidate();
            }
            catch { }
        }


        public void DrawObjects(Graphics g, Rectangle clipRectangle)
        {
            try
            {
                for (int i = this.ObjectList.Selection.Count - 1; i >= 0; i--)
                {
                    ObjectBase base2 = this.ObjectList.Selection[i];
                    if (base2.Drawn) continue;
                    if (base2.IntersectsWith(clipRectangle))
                    {
                        base2.Draw(this, g);
                        if (base2.Selected)
                        {
                            base2.DrawTracker(g);
                        }
                    }
                }
            }
            catch { }
        }

        public void ReDrawObjects()
        {
            try
            {
                this.ObjectList.UnselectAll();
                this.ObjectList.UnDrawnAll();
                this.CanvasImage = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.CanvasImage))
                {
                    g.Clear(this.TransparentColor);
                    for (int i = this.ObjectList.Count - 1; i >= 0; i--)
                    {
                        ObjectBase base2 = this.ObjectList[i];
                        if (base2.Drawn) continue;
                        if (base2.IntersectsWith(this.ClientRectangle))
                        {
                            base2.Draw(this, g);
                            base2.Drawn = true;
                        }
                    }
                    g.Dispose();
                }
                this.Invalidate();
            }
            catch { }
        }

        private void DrawReversibleRect(Rectangle rect)
        {
            try
            { 
                rect.Location = base.PointToScreen(rect.Location);
                ControlPaint.DrawReversibleFrame(rect, Color.Navy, FrameStyle.Thick);
            }
            catch { }
        }

        public Point GetBasePosition()
        {
            return this._basePosition.Position;
        }

        public Point GetObjectBasePosition(Point point)
        {
            Point position = new Point(point.X, point.Y);
            return position;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Shift | Keys.Left):
                case (Keys.Shift | Keys.Up):
                case (Keys.Shift | Keys.Right):
                case (Keys.Shift | Keys.Down):
                case (Keys.Shift | Keys.Tab):
                case Keys.Home:
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Tab:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            { 
                if (e.Control && (e.KeyCode == Keys.C))
                {
                    this.ClipboardCopy();
                    base.Invalidate();
                    return;
                }
                else if (e.Control && (e.KeyCode == Keys.X))
                {
                    this.ClipboardCut();
                    base.Invalidate();
                    return;
                }
                else if (e.Control && (e.KeyCode == Keys.V))
                {
                    this.ClipboardPaste();
                    base.Invalidate();
                    return;
                }
                else if (e.Control && (e.KeyCode == Keys.Y))
                {
                    this.Redo();
                    return;
                }
                else if (e.Control && (e.KeyCode == Keys.Z))
                {
                    this.Undo();
                    return;
                }
                else if (((e.KeyCode == Keys.ControlKey) || (e.KeyCode == Keys.ShiftKey)) && (this.MousePointerOnObject || (this.ObjectList.SelectionCount > 0)))
                {
                    //    //this.ObjectTools.OnMouseMove(this, new MouseEventArgs(MouseButtons.None, 0, this._currentMousePoint.X, this._currentMousePoint.Y, 0));
                }
                else if ((((e.KeyCode != Keys.Up) && (e.KeyCode != Keys.Down)) && ((e.KeyCode != Keys.Left) && (e.KeyCode != Keys.Right))) && ((e.KeyCode != Keys.Prior) && (e.KeyCode != Keys.Next)))
                {
                    if (((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back)) && (this.ObjectList.SelectionCount > 0))
                    {
                        this.DeleteObjects(this.ObjectList.Selection);
                        base.Invalidate();
                        return;
                    }
                    this.CancelObjectAccessMode();
                    allowResize = false;
                    return;
                }
                else if ((this.ObjectList.SelectionCount > 0) && (((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)) || ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))))
                {
                    int dx = 0;
                    int dy = 0;
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            dx--;
                            break;

                        case Keys.Up:
                            dy--;
                            break;

                        case Keys.Right:
                            dx++;
                            break;

                        case Keys.Down:
                            dy++;
                            break;
                    }
                    if (e.Modifiers == Keys.Shift)
                    {
                        foreach (ObjectBase base2 in this.ObjectList.Selection)
                        {
                            base2.Resize(dx, dy);
                        }
                    }
                    else
                    {
                        foreach (ObjectBase base2 in this.ObjectList.Selection)
                        {
                            base2.Move(dx, dy);
                        }
                    }
                    base.Invalidate();
                    return;
                }
                base.OnKeyDown(e);
            }
            catch { }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            try
            { 
                this._bKeyControl = e.Control;
                this._bKeyShift = e.Shift;
                if (((e.KeyCode == Keys.ControlKey) || (e.KeyCode == Keys.ShiftKey)) && (this.MousePointerOnObject || (this.ObjectList.SelectionCount > 0)))
                {
                    this.ObjectTools.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, this._currentMousePoint.X, this._currentMousePoint.Y, 0));
                }
                base.OnKeyUp(e);
            }
            catch { }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            try
            { 
                if (this.MousePointerOnObject && (e.Button == MouseButtons.Left))
                {
                    if (this.ObjectList.ShowPropertiesDialog(this))
                    {
                        this.Refresh();
                    }
                }
                else if (!this.MousePointerOnObject && (e.Button == MouseButtons.Left))
                {
                    if (this.ObjectSelectionMode)
                    {
                        this.ObjectSelectionMode = false;
                    }
                    base.OnMouseDoubleClick(e);
                }
            }
            catch { }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            { 
                if (this._bLoading && this.ActiveObjectCreator == ObjectCreatorType.None)
                {
                    this.ActiveObjectCreator = ObjectCreatorType.Pencil;
                    this._bLoading = false;
                }
                if (((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) && (this.ObjectCreateMode || (this.ActiveObjectCreator == ObjectCreatorType.Selector)))
                {
                    this.ObjectTools.OnMouseDown(e);
                    if (e.Button == MouseButtons.Right)
                    {
                        this.OnMouseMove(e);
                        if (this.MousePointerOnObject)
                        {
                            this.OnNeedContextMenu();
                        }
                    }
                }
                else
                {
                    this.CancelObjectAccessMode();
                    base.OnMouseDown(e);
                }
            }
            catch { }
        }

        public void MouseMoveEventcall(MouseEventArgs e)
        {
            try
            { 
                this.OnMouseMove(e);
            }
            catch { }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                this._currentMousePoint.X = e.X;
                this._currentMousePoint.Y = e.Y;
                if ((!this.MousePointerOnObject && !this.ObjectCreateMode) && !this.ObjectSelectionMode)
                {
                    base.OnMouseMove(e);
                }
                this.ObjectTools.OnMouseMove(e);
                if (this.MousePointerOnObject && !this.ObjectCreateMode)
                {
                    this.ActiveObjectCreator = ObjectCreatorType.Selector;
                }
            }
            catch { }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            { 
                if ((e.Button == MouseButtons.Left) && (this.ActiveObjectCreator != ObjectCreatorType.None))
                {
                    this.ObjectTools.OnMouseUp(e);
                }
                else
                {
                    base.OnMouseUp(e);
                }
            }
            catch { }
        }

        public void OnNeedContextMenu()
        {
            try
            { 
                if (this.NeedContextMenu != null)
                {
                    this.NeedContextMenu(this, new EventArgs());
                }
            }
            catch { }
        }

        public void OnObjectModeChanged()
        {
            try
            { 
                if (this.ObjectModeChanged != null)
                {
                    this.ObjectModeChanged(this, EventArgs.Empty);
                }
            }
            catch { }
        }

        public void OnObjectSelected(ObjectBase obj)
        {
            try
            {
                if (this.ObjectSelected != null)
                {
                    this.ObjectSelected(obj, EventArgs.Empty);
                }
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if ((base.DesignMode))
                {
                    e.Graphics.Clear(Color.White);
                    e.Graphics.DrawString("MySheet\n\nVersion : " + Assembly.GetExecutingAssembly().GetName().Version + " ", new Font(this.Font.FontFamily, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), new PointF(10f, 10f));
                }
                else
                {
                    base.OnPaint(e);
                    if (this.ObjectCreateMode || this.MousePointerOnObject)
                    {
                        ////마우스 위치 표시
                        //Pen pen = new Pen(new SolidBrush(Color.LightGray));
                        //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        //Point p = new Point(this._currentMousePoint.X, this._currentMousePoint.Y);
                        //e.Graphics.DrawString("(" + p.X + "," + p.Y + ")", new Font("굴림체", 9), pen.Brush, p.X + 8, p.Y - 20);
                        //e.Graphics.DrawLine(pen, 0, p.Y, this.ClientRectangle.Width, p.Y);
                        //e.Graphics.DrawLine(pen, p.X, 0, p.X, this.ClientRectangle.Height);
                        ////마우스 위치 표시
                    }
                    //DrawTransparentBitmap(e.Graphics, this.CanvasImage, 0.5F);
                    Bitmap img = new Bitmap(this.CanvasImage);
                    Graphics g = Graphics.FromImage(img);
                    this.DrawObjects(g, this.ClientRectangle);
                    img.MakeTransparent(this.TransparentColor);
                    e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    e.Graphics.DrawImage(img, 0, 0);
                    if (!base.Enabled)
                    {
                        Region region = new Region(base.ClientRectangle);
                        e.Graphics.FillRegion(new HatchBrush(HatchStyle.Percent70, DefaultValues.MySheetColors.UnMoveableCellForeColor, Color.White), region);
                    }
                }
            }
            catch { }
        }

        #region 투명 비트맵 그리기 - DrawTransparentBitmap(graphics, image, transparency) 
        /// <summary> 
        /// 투명 비트맵 그리기 
        /// </summary> 
        /// <param name="graphics">그래픽스</param>
        /// <param name="image">이미지</param>
        /// <param name="transparency">투명도</param> 
        public void DrawTransparentBitmap(Graphics graphics, Image image, float transparency)
        {
            try
            {
                if (transparency <= 0f)
                {
                    return;
                }
                ImageAttributes imageAttributes = null;

                if (transparency < 1f)
                {
                    imageAttributes = new ImageAttributes();
                    float[][] colorMatrixElementArray =
                    {
                        new float[] {1, 0, 0, 0 , 0},
                        new float[] {0, 1, 0, 0 , 0},
                        new float[] {0, 0, 1, 0 , 0},
                        new float[] {0, 0, 0, transparency, 0},
                        new float[] {0, 0, 0, 0 , 1}
                    };
                    imageAttributes.SetColorMatrix(new ColorMatrix(colorMatrixElementArray));
                }
                Rectangle targetRectangle = new Rectangle(Point.Empty, Size);
                graphics.DrawImage(image, targetRectangle, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            catch { }
        }
        # endregion

        #region DrawBox 크기 조절 이벤트

        /// <summary>
        /// DrawBox 오른쪽 하단의 화살표를 마우스를 클릭했을 때 이벤트
        /// 클릭하면 DrawBox 의 사이즈를 조절 할 수 있도록 허락한다.
        /// </summary>
        private void pictureBox_ReSize_MouseDown(object sender, MouseEventArgs e)
        {
            allowResize = true;
        }

        /// <summary>
        /// 마우스의 위치에 따라서 DrawBox 의 사이즈를 조절한다.
        /// </summary>
        private void pictureBox_ReSize_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (allowResize)
                {
                    this.Height = pictureBox_ReSize.Top + e.Y;
                    this.Width = pictureBox_ReSize.Left + e.X;
                    if (this.Height < pictureBox_ReSize.Height) this.Height = pictureBox_ReSize.Height;
                    if (this.Width < pictureBox_ReSize.Width) this.Width = pictureBox_ReSize.Width;
                }
                this.Invalidate();
            }
            catch
            {
                allowResize = false;
            }
        }

        /// <summary>
        /// 마우스를 클릭을 해제하면 DrawBox 의 사이즈 조절이 끝난다.
        /// </summary>
        private void pictureBox_ReSize_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.Image = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.Image))
                {
                    g.Clear(Color.White);
                    g.DrawImage(this.BaseImage, 0, 0);
                }
                //this.Image = (Bitmap)this.BaseImage.Clone();
                this.ReDrawObjects();
                allowResize = false;
            }
            catch 
            {
                allowResize = false;
            }
        }

        #endregion

        #region Rtf 텍스트 그리기

        string _rtfString = "";
        Rectangle _rtfRect;
        public Bitmap GetRtfImage(string rtf, Rectangle rect)
        {
            try
            { 
                _rtfString = rtf;
                _rtfRect = rect;
                Bitmap img = GetResultImage();
                _rtfBase.Size = img.Size;
                _rtfBase.Image = img;
                //img.Save("C:\\rtfImage11.png", ImageFormat.Png);     //이미지확인 테스트용
                Rectangle rect1 = _rtfBase.ClientRectangle;
                Bitmap img1 = new Bitmap(rect1.Width, rect1.Height);
                using (Graphics g = Graphics.FromImage(img1)) { g.Clear(this.TransparentColor); }
                _rtfBase.DrawToBitmap(img1, rect1);
                //img1.Save("C:\\rtfImage21.png", ImageFormat.Png);     //이미지확인 테스트용
                return img1;
            }
            catch 
            {
                return null;
            }
        }

        private void RtfBase_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRtfText(_rtfString, _rtfRect);
            }
            catch { }
        }

        #endregion

        #region Clipboard

        public Bitmap ApplyScizzors(Bitmap bmpSource, GraphicsPath pScizzor, bool cut = false)
        {
            try
            {
                GraphicsPath graphicsPath = new GraphicsPath();   // specified Graphicspath          
                graphicsPath.AddPath(pScizzor, true);      // add the Polygon
                var rectCutout = graphicsPath.GetBounds();        // find rectangular range           
                Matrix m = new Matrix();
                m.Translate(-rectCutout.Left, -rectCutout.Top);   // translate clip to (0,0)
                graphicsPath.Transform(m);
                Bitmap bmpCutout = new Bitmap((int)(rectCutout.Width), (int)(rectCutout.Height));  // target
                Graphics graphicsCutout = Graphics.FromImage(bmpCutout);
                graphicsCutout.Clip = new Region(graphicsPath);
                graphicsCutout.DrawImage(bmpSource, (int)(-rectCutout.Left), (int)(-rectCutout.Top)); // draw
                if (cut)
                {
                    using (Graphics g = Graphics.FromImage(this.CanvasImage))
                    {
                        g.SetClip(this.WorkingArea);
                        //g.FillPolygon(new SolidBrush(this.TransparentColor), pScizzor);
                        g.FillPath(new SolidBrush(Color.White), pScizzor);
                        g.Dispose();
                    }
                }
                graphicsPath.Dispose();
                graphicsCutout.Dispose();
                return bmpCutout;
            }
            catch
            {
                return null;
            }
        }

        private ObjectBase InsertImage(Image img, int left, int top)
        {
            try
            {
                ObjectImage box = new ObjectImage(this, left, top, img.Width, img.Height);
                box.SetImage(img);
                if (this.Width < left + img.Width) this.Width = left + img.Width;
                if (this.Height < top + img.Height) this.Height = top + img.Height;
                this.InsertObject(box);
                return box;
            }
            catch
            {
                return null;
            }
        }

        public void ClipboardCopy()
        {
            try
            {
                if (this.ObjectList.SelectionCount < 1) return;
                GraphicsPath path = this.ObjectList.Selection[0].GetGraphicPath();
                this.ObjectList.Remove(this.ObjectList.Selection[0]);
                if (path == null) return;
                this.ObjectList.UnselectAll();
                Bitmap img = GetResultImage();
                img = ApplyScizzors(img, path, false);
                path.Dispose();
                if (img == null) return;
                Clipboard.Clear();
                Clipboard.SetImage(img);
                ObjectBase box = InsertImage(img, 0, 0);
                box.Selected = true;
                box.DrawTrackers = true;
                this.MousePointerOnObject = false;
                this.ActiveObjectCreator = ObjectCreatorType.Selector;
            }
            catch { }
        }

        public void ClipboardCut()
        {
            try
            {
                if (this.ObjectList.SelectionCount < 1) return;
                GraphicsPath path = this.ObjectList.Selection[0].GetGraphicPath();
                this.ObjectList.Remove(this.ObjectList.Selection[0]);
                if (path == null) return;
                this.ObjectList.UnselectAll();
                Bitmap img = GetResultImage();
                img = ApplyScizzors(img, path, true);
                path.Dispose();
                if (img == null) return;
                Clipboard.Clear();
                Clipboard.SetImage(img);
                ObjectBase box = InsertImage(img, 0, 0);
                box.Selected = true;
                box.DrawTrackers = true;
                this.MousePointerOnObject = false;
                this.ActiveObjectCreator = ObjectCreatorType.Selector;
            }
            catch { }
        }

        public void ClipboardPaste()
        {
            try
            {
                this.ObjectList.UnselectAll();
                Image img = Clipboard.GetImage();
                if (img == null) return;
                ObjectBase box = InsertImage(img, 0, 0);
                box.Selected = true;
                box.DrawTrackers = true;
                this.MousePointerOnObject = false;
                this.ActiveObjectCreator = ObjectCreatorType.Selector;
            }
            catch { }
        }

        #endregion
    }
}
