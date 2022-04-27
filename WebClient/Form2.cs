using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Service svc = new Service();
            string value = "{\"ReportType\":\"\",\"ReportName\":\"FrmAdjSLAcdtImgGoods\",\"AcptMgmtSeq\":\"410017\",\"ReSurvAsgnNo\":\"1\",\"CompanySeq\":\"1\",\"UserID\":\"test21@metrosoft.co.kr\"}";
            svc.OpenAcdtPictGoods(value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap img = null;
            try
            {
                using (FileStream fs = new FileStream("C:\\Users\\ksm\\Documents\\제목 없음.png", FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        var ms = new MemoryStream(reader.ReadBytes((int)fs.Length));
                        img = new Bitmap(ms);
                    }
                }
                if (img == null) return;
                YLW_WebClient.Painter.ImageEditView.ShowPreview(null, null);
                YLW_WebClient.Painter.ImageEditView.Current.LoadDocument(null, img);
                Form frm = YLW_WebClient.Painter.ImageEditView.Current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
