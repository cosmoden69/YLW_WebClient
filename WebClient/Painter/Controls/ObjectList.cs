using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using YLW_WebClient.Painter.Controller;

namespace YLW_WebClient.Painter.PaintControls
{
    [Serializable]
    public class ObjectList
    {
        public MySheet _sheet;

        public List<ObjectBase> _ObjectBaseList = new List<ObjectBase>();

        public ObjectList(MySheet sheet)
        {
            _sheet = sheet;
        }

        public void Add(ObjectBase obj)
        {
            this._ObjectBaseList.Insert(0, obj);
        }

        public void Remove(ObjectBase obj)
        {
            this._ObjectBaseList.Remove(obj);
        }

        private bool ApplyProperties(ObjectProperties prop)
        {
            bool flag = false;
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                if (base2.Selected)
                {
                    if (prop.Color.HasValue && (base2.PenColor != prop.Color.Value))
                    {
                        base2.PenColor = prop.Color.Value;
                        flag = true;
                    }
                    if (prop.PenWidth.HasValue && (base2.PenWidth != prop.PenWidth.Value))
                    {
                        base2.PenWidth = prop.PenWidth.Value;
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public bool Clear()
        {
            bool flag = this._ObjectBaseList.Count > 0;
            this._ObjectBaseList.Clear();
            return flag;
        }

        public void DeleteLastAddedObject()
        {
            if (this._ObjectBaseList.Count > 0)
            {
                this._ObjectBaseList.RemoveAt(0);
            }
        }

        public bool DeleteSelection()
        {
            bool flag = false;
            for (int i = this._ObjectBaseList.Count - 1; i >= 0; i--)
            {
                if (this._ObjectBaseList[i].Selected)
                {
                    this._ObjectBaseList.RemoveAt(i);
                    flag = true;
                }
            }
            return flag;
        }

        public void Draw(MySheet sheet, Graphics g, Rectangle clipRectangle)
        {
            for (int i = this._ObjectBaseList.Count - 1; i >= 0; i--)
            {
                if (this._ObjectBaseList[i].ObjectSheet == null)
                {
                    this._ObjectBaseList[i].ObjectSheet = sheet;
                }
                ObjectBase base2 = this._ObjectBaseList[i];
                if (base2.IntersectsWith(clipRectangle))
                {
                    base2.Draw(sheet, g);
                    if (base2.Selected)
                    {
                        base2.DrawTracker(g);
                    }
                }
            }
        }

        private ObjectProperties GetProperties()
        {
            ObjectProperties properties = new ObjectProperties();
            bool flag = true;
            int argb = 0;
            int penWidth = 1;
            bool flag2 = true;
            bool flag3 = true;
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                if (flag)
                {
                    argb = base2.PenColor.ToArgb();
                    penWidth = base2.PenWidth;
                    flag = false;
                }
                else
                {
                    if (base2.PenColor.ToArgb() != argb)
                    {
                        flag2 = false;
                    }
                    if (base2.PenWidth != penWidth)
                    {
                        flag3 = false;
                    }
                }
            }
            if (flag2)
            {
                properties.Color = new Color?(Color.FromArgb(argb));
            }
            if (flag3)
            {
                properties.PenWidth = new int?(penWidth);
            }
            return properties;
        }

        public void Insert(int index, ObjectBase obj)
        {
            if ((index >= 0) && (index < this._ObjectBaseList.Count))
            {
                this._ObjectBaseList.Insert(index, obj);
            }
        }

        public bool MoveSelectionToBack()
        {
            int num2;
            List<ObjectBase> list = new List<ObjectBase>();
            for (num2 = this._ObjectBaseList.Count - 1; num2 >= 0; num2--)
            {
                if (this._ObjectBaseList[num2].Selected)
                {
                    list.Add(this._ObjectBaseList[num2]);
                    this._ObjectBaseList.RemoveAt(num2);
                }
            }
            int count = list.Count;
            for (num2 = count - 1; num2 >= 0; num2--)
            {
                this._ObjectBaseList.Add(list[num2]);
            }
            return (count > 0);
        }

        public bool MoveSelectionToFront()
        {
            int num2;
            List<ObjectBase> list = new List<ObjectBase>();
            for (num2 = this._ObjectBaseList.Count - 1; num2 >= 0; num2--)
            {
                if (this._ObjectBaseList[num2].Selected)
                {
                    list.Add(this._ObjectBaseList[num2]);
                    this._ObjectBaseList.RemoveAt(num2);
                }
            }
            int count = list.Count;
            for (num2 = 0; num2 < count; num2++)
            {
                this._ObjectBaseList.Insert(0, list[num2]);
            }
            return (count > 0);
        }

        public void RemoveAt(int index)
        {
            this._ObjectBaseList.RemoveAt(index);
        }

        public void Replace(int index, ObjectBase obj)
        {
            if ((index >= 0) && (index < this._ObjectBaseList.Count))
            {
                this._ObjectBaseList.RemoveAt(index);
                this._ObjectBaseList.Insert(index, obj);
            }
        }

        public void SelectAll()
        {
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                base2.Selected = true;
            }
        }

        public void SelectInRectangle(Rectangle rect)
        {
            this.UnselectAll();
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                if (base2.IntersectsWith(rect))
                {
                    base2.Selected = true;
                }
            }
        }

        public bool ShowPropertiesDialog(Control parent)
        {
            if (this.SelectionCount < 1)
            {
                return false;
            }
            ObjectProperties properties = this.GetProperties();
            //            ObjectFormat format = new ObjectFormat();
            //            format.Properties = properties;
            //            if (format.ShowDialog(parent) != DialogResult.OK)
            //            {
            //                return false;
            //            }
            //            this.ApplyProperties(format.Properties);
            return true;
        }

        public void UnselectAll()
        {
            _sheet.MyText.UnSetObject();
            _sheet.ToolBrokenLine.CloseTool();
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                base2.Selected = false;
            }
        }

        public void UnDrawnAll()
        {
            foreach (ObjectBase base2 in this._ObjectBaseList)
            {
                base2.Drawn = false;
            }
        }

        public int Count
        {
            get
            {
                return this._ObjectBaseList.Count;
            }
        }

        public ObjectBase this[int index]
        {
            get
            {
                if ((index >= 0) && (index < this._ObjectBaseList.Count))
                {
                    return this._ObjectBaseList[index];
                }
                return null;
            }
        }

        public List<ObjectBase> Selection
        {
            get
            {
                int num2;
                List<ObjectBase> list = new List<ObjectBase>();
                for (num2 = this._ObjectBaseList.Count - 1; num2 >= 0; num2--)
                {
                    if (this._ObjectBaseList[num2].Selected)
                    {
                        list.Add(this._ObjectBaseList[num2]);
                    }
                }
                return list;
            }
        }

        public int SelectionCount
        {
            get
            {
                int num = 0;
                using (IEnumerator<ObjectBase> enumerator = this._ObjectBaseList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ObjectBase current = enumerator.Current;
                        if (current.Selected) num++;
                    }
                }
                return num;
            }
        }
    }
}

