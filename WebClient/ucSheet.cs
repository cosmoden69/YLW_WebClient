using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using YLWService;

namespace YLW_WebClient
{
    public partial class ucSheet : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSheet()
        {
            InitializeComponent();
        }

        public void AddDocument(string cap, string key, System.Windows.Forms.Control obj)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument doc = this.documentManager1.View.AddDocument(obj);
            doc.Caption = cap;
            doc.Tag = key;
            this.documentManager1.View.ActivateDocument(obj);
        }

        public void RemoveDocument(System.Windows.Forms.Control obj)
        {
            obj.Dispose();
            this.documentManager1.View.RemoveDocument(obj);
        }

        public void RemoveDocument(DevExpress.XtraBars.Docking2010.Views.BaseDocument doc)
        {
            doc.Control.Dispose();
            this.documentManager1.View.Documents.Remove(doc);
        }

        public DevExpress.XtraBars.Docking2010.Views.BaseDocument FindDocument(string cap, string key)
        {
            return this.documentManager1.View.Documents.FindFirst(x => x.Caption == cap && Utils.ConvertToString(x.Tag) == key);
        }

        public DevExpress.XtraBars.Docking2010.Views.BaseDocument ActiveDocument()
        {
            return this.documentManager1.View.ActiveDocument;
        }

        public void ActivateDocument(DevExpress.XtraBars.Docking2010.Views.BaseDocument doc)
        {
            this.documentManager1.View.ActivateDocument(doc.Control);
        }

        public void Clear()
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument[] docs = this.documentManager1.View.Documents.ToArray();
            foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument doc in docs)
            {
                doc.Control.Dispose();
                this.documentManager1.View.Documents.Remove(doc);
            }
        }

        //출력전체
        public void PrintAll(string printname)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument[] docs = this.documentManager1.View.Documents.ToArray();
            foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument doc in docs)
            {
                if (doc.Control is IViewerSheet) (doc.Control as IViewerSheet).Print(printname);
            }
        }
        //출력 해당페이지
        public void PrintSingle(string printname)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument doc = ActiveDocument();
            if (doc.Control is IViewerSheet) (doc.Control as IViewerSheet).Print(printname);
        }

        //출력 미리보기 전체
        public void PrintPreViewAll(string printname)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument[] docs = this.documentManager1.View.Documents.ToArray();
            foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument doc in docs)
            {
                if (doc.Control is IViewerSheet) (doc.Control as IViewerSheet).PrintPreview(printname);
            }
        }

        //출력 미리보기 해당페이지
        public void PrintPreViewSingle(string printname)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument doc = ActiveDocument();
            if (doc.Control is IViewerSheet) (doc.Control as IViewerSheet).PrintPreview(printname);
        }
    }
}
