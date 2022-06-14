using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLW_WebClient.CAA;

namespace YLW_WebClient
{
    public partial class ucImageViewer : UserControl
    {
        private List<Image> _images = null;
        private int _currentImageIndex = -1;

        public ucImageViewer()
        {
            InitializeComponent();

            btnFirst.Click += BtnFirst_Click;
            btnPrev.Click += BtnPrev_Click;
            btnNext.Click += BtnNext_Click;
            btnLast.Click += BtnLast_Click;
            btnZoomIn.Click += BtnZoomIn_Click;
            btnZoomOut.Click += BtnZoomOut_Click;
        }

        public void SetImage(List<Image> img)
        {
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pic.Properties.ZoomPercent = 100;
            _images = img;
            SetPicture(0);
        }

        public List<Image> GetImage()
        {
            return _images;
        }

        public void Print()
        {
            var listModels = new List<ViewImage>();
            for (int ii = 0; ii < _images.Count; ii++)
            {
                listModels.Add(new ViewImage() { FileImage1 = _images[ii], FileName1 = "Page #" + (ii + 1) });
            }

            XtraReport1 rpt1 = new XtraReport1(listModels);
            DevExpress.XtraReports.UI.ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rpt1);
            rpt.AutoShowParametersPanel = false;
            rpt.ShowPreviewDialog();
        }

        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pic.Properties.ZoomPercent -= 6;
        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pic.Properties.ZoomPercent += 6;
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            SetPicture(_images.Count - 1);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            SetPicture(_currentImageIndex + 1);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            SetPicture(_currentImageIndex - 1);
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            SetPicture(0);
        }

        private void SetPicture(int idx)
        {
            if (_images == null || _images.Count < 1)
            {
                _currentImageIndex = -1;
                lblPageCount.Text = "/";
                pic.Image = null;
                return;
            }
            if (idx < 0) idx = 0;
            if (idx >= _images.Count) idx = _images.Count - 1;
            _currentImageIndex = idx;
            lblPageCount.Text = (_currentImageIndex + 1) + "/" + _images.Count;
            pic.Image = _images[_currentImageIndex];
        }
    }
}
