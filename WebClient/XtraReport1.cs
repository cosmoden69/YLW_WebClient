using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace YLW_WebClient.CAA
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1(object lst)
        {
            InitializeComponent();
            //SetData(pds);
            this.DataSource = lst;
        }

        private void SetData(DataSet pds)
        {
            DataTable dt = pds.Tables[0];
            //for (int ii = 0; ii < dt.Rows.Count; ii++)
            {
                //picImage1.Visible = true;
                //Image img = YLWService.Utils.stringToImage(dt.Rows[ii]["FileBase641"] + "");
                //picImage1.Image = img;
                //picImage1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;


                //picImage2.Visible = true;
                //img = YLWService.Utils.stringToImage(dt.Rows[ii]["FileBase642"] + "");
                //picImage2.Image = img;
                //picImage2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            }
        }
    }
}
