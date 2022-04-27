using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    public class ResizeRowManager
    {
        Control myParent = null;
        Panel myBase = null;
        Dictionary<int, ResizeRow> rows = new Dictionary<int, ResizeRow>();
        List<object> lcks = new List<object>();

        public ResizeRowManager(Control p, Panel b)
        {
            myParent = p;
            myBase = b;
        }

        public void LockSize(params object[] items)
        {
            foreach (object obj in items)
            {
                if (!lcks.Contains(obj)) lcks.Add(obj);
            }
        }

        public void AddControl(int rownum, params object[] items)
        {
            if (!rows.ContainsKey(rownum)) rows.Add(rownum, new ResizeRow());
            ResizeRow row = rows[rownum];
            row.Items.AddRange(items);
        }

        public void ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            //높이가 바뀐 컨트롤이 속한 ROW를 찾는다
            int si = rows.Count;
            for (int ii = 0; ii < rows.Count; ii++)
            {
                if (!rows[ii].Contains(sender)) continue;
                si = ii;
                break;
            }

            //해당 ROW의 최대 높이를 구한다
            int maxhgt = e.NewRectangle.Height;
            foreach (object obj in rows[si].Items)
            {
                if (obj == sender) continue;
                if (obj is YLW_WebClient.CAA.RichTextBox)
                {
                    maxhgt = Math.Max(maxhgt, (obj as YLW_WebClient.CAA.RichTextBox).NewHeight);
                }
            }

            myParent.SuspendLayout();
            int toppos = 0;
            foreach (object obj in rows[si].Items)
            {
                if (lcks.Contains(obj)) continue;
                if (obj is YLW_WebClient.CAA.RichTextBox)
                {
                    (obj as YLW_WebClient.CAA.RichTextBox).SetContentsHeight(maxhgt);
                }
                if (obj is YLW_WebClient.CustomDateTimeInput)
                {
                    (obj as YLW_WebClient.CustomDateTimeInput).MinimumSize = new Size(0, maxhgt);
                    (obj as Control).Height = maxhgt;
                }
                if (obj is YLW_WebClient.CustomTextBox)
                {
                    (obj as YLW_WebClient.CustomTextBox).MinimumSize = new Size(0, maxhgt);
                    (obj as Control).Height = maxhgt;
                }
                else
                {
                    (obj as Control).Height = maxhgt;
                }
                toppos = Math.Max(toppos, (obj as Control).Top);
            }
            toppos = toppos + maxhgt - 1;
            for (int ii = si + 1; ii < rows.Count; ii++)
            {
                maxhgt = 0;
                foreach (object obj in rows[ii].Items)
                {
                    (obj as Control).Top = toppos;
                    maxhgt = Math.Max(maxhgt, (obj as Control).Height);
                }
                toppos = toppos + maxhgt - 1;
            }
            myBase.Height = toppos + 1;
            myParent.Height = toppos + 1;
            myParent.ResumeLayout(false);
            myParent.PerformLayout();
        }
    }

    public class ResizeRow
    {
        public List<object> Items = new List<object>();

        public bool Contains(object obj)
        {
            return Items.Contains(obj);
        }
    }
}
