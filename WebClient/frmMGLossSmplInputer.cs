using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class frmMGLossSmplInputer : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private static frmMGLossSmplInputer current = null;

        public static frmMGLossSmplInputer Current { get { return current; } }

        private FrmRptHistoryList RpthList = null;

        public frmMGLossSmplInputer()
        {
            InitializeComponent();

            btn_SmplQry.Visible = false;
            btn_SmplSave.Visible = false;

            this.Disposed += frmMGLossSmplInputer_Disposed;
        }

        public static bool ShowPreview()
        {
            try
            {
                if (current == null || current.IsDisposed)
                {
                    current = new frmMGLossSmplInputer();
                    current.Top = 0;
                    current.Left = 0;
                    current.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                current.Close();
                return false;
            }
        }

        public bool LoadDocument(ReportParam p)
        {
            try
            {
                if (RpthList != null) RpthList.Close();

                this.ucInputer1.LoadDocument(p);
                this.btn_Find.PerformClick();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.G_Create_))
                Directory.CreateDirectory(Program.G_Create_);

            if (!Directory.Exists(Program.G_WD_Path))
                Directory.CreateDirectory(Program.G_WD_Path);
            
            ScreenDetect();            
        }

        private void frmMGLossSmplInputer_Disposed(object sender, EventArgs e)
        {
            if (RpthList != null) RpthList.Close();
        }

        private void ScreenDetect()
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Left, 0);
            Size = new Size(Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            ucInputer1.SortAccident1();
            ucInputer1.XmlData_Save();
        }

        private void Btn_HSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ucInputer1.SortAccident1();
                ucInputer1.XmlData_Save(true);

                ReportParam p = new ReportParam();
                p.AcptMgmtSeq = this.ucInputer1.param.AcptMgmtSeq;
                p.ReSurvAsgnNo = this.ucInputer1.param.ReSurvAsgnNo;
                p.ReportName = this.ucInputer1.param.ReportName;
                p.ReportType = this.ucInputer1.param.ReportType;
                p.BizNo = this.ucInputer1.param.BizNo;
                p.CompanySeq = this.ucInputer1.param.CompanySeq;
                p.UserID = this.ucInputer1.param.UserID;

                string streamdata = Utils.ClassToJsonstring(p);
                DataSet yds = YLWService.MTRServiceModule.CallMTRSaveReportHistory(streamdata);
                if (yds == null)
                {
                    MessageBox.Show("보고서가 저장되지 않았습니다");
                    return;
                }
                foreach (DataTable dti in yds.Tables)
                {
                    if (!dti.Columns.Contains("Status")) continue;
                    if (dti.Rows.Count > 0 && Convert.ToString(dti.Rows[0]["Status"]) == "ERR")
                    {
                        if (!dti.Columns.Contains("Message")) continue;
                        MessageBox.Show(dti.Rows[0]["Message"] + "");
                        return;
                    }
                    if (dti.Rows.Count > 0 && Convert.ToInt32(dti.Rows[0]["Status"]) != 0)   //Status != 0 이면 저장안됨
                    {
                        if (!dti.Columns.Contains("Result")) continue;
                        MessageBox.Show(dti.Rows[0]["Result"] + "");
                        return;
                    }
                }
                MessageBox.Show("보고서 저장 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Btn_HQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FrmRptHistoryList.ShowPreview(this);
                FrmRptHistoryList.Current.LoadDocument(ucInputer1.param);
                RpthList = FrmRptHistoryList.Current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersMGLossSmpl";
            security.methodId = "Query";
            security.companySeq = ucInputer1.param.CompanySeq;

            DataSet ds = new DataSet("ROOT");

            DataTable dt = ds.Tables.Add("DataBlock1");

            dt.Columns.Add("AcptMgmtSeq");
            dt.Columns.Add("ReSurvAsgnNo");
            dt.Columns.Add("DcmgDocNo");

            dt.Clear();
            DataRow dr = dt.Rows.Add();

            dr["AcptMgmtSeq"] = ucInputer1.param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = ucInputer1.param.ReSurvAsgnNo;
            dr["DcmgDocNo"] = "";

            DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
            if (yds == null)
            {
                MessageBox.Show("데이타가 없습니다");
                return;
            }

            ucInputer1.Reload(yds);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_SmplQry_Click(object sender, EventArgs e)
        {
            try
            { 
                frmRprtSmplList frm = new frmRprtSmplList(ucInputer1.param, ucInputer1.SmplAuth);
                Point pos = this.Panel.PointToScreen(this.Panel.Location);
                pos.Y += this.Panel.Height + 5;
                frm.Location = pos;
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_SmplSave_Click(object sender, EventArgs e)
        {
            try
            { 
                DataSet ds = ucInputer1.GetSaveData();
                frmRprtSmplSave frm = new frmRprtSmplSave(ucInputer1.param, ucInputer1.SmplAuth, ds);
                Point pos = this.Panel.PointToScreen(this.Panel.Location);
                pos.Y += this.Panel.Height + 5;
                frm.Location = pos;
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}