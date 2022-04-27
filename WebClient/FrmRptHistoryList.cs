using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YLWService;
using YLWService.Extensions;

namespace YLW_WebClient
{
    public partial class FrmRptHistoryList : Form
    {
        ReportParam param = null;

        private static FrmRptHistoryList current = null;

        public static FrmRptHistoryList Current { get { return current; } }

        public FrmRptHistoryList()
        {
            InitializeComponent();

            this.btnView.Click += BtnView_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.dgv.CellDoubleClick += Dgv_CellDoubleClick;

            this.dgv.AutoGenerateColumns = false;
            this.dgv.AddColumn("TEXTBOX", "AcptMgmtSeq", "수임내부코드", 10, false);
            this.dgv.AddColumn("TEXTBOX", "ReSurvAsgnNo", "재조사순번", 10, false);
            this.dgv.AddColumn("TEXTBOX", "ReportType", "보고서구분", 10, false);
            this.dgv.AddColumn("TEXTBOX", "Seq", "순번", 10, false);
            this.dgv.AddColumn("TEXTBOX", "FileName", "파일명", 10, false);
            this.dgv.AddColumn("TEXTBOX", "FileSeq", "파일순번", 10, false);
            this.dgv.AddColumn("TEXTBOX", "SysDt", "수정일시", 200, true, true);
            this.dgv.AddColumn("TEXTBOX", "UserSeq", "수정자사번", 10, false);
            this.dgv.AddColumn("TEXTBOX", "UserName", "수정자", 150, true, true);
            this.dgv.AllowUserToAddRows = false;
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static bool ShowPreview(Form parent)
        {
            try
            {
                if (current == null || current.IsDisposed)
                {
                    current = new FrmRptHistoryList();
                    Point pos = parent.Location;
                    pos.X += parent.ClientSize.Width + 3;
                    current.Location = pos;
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
                this.param = p;
                GetList();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void GetList()
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCS";
                security.methodId = "ReportHistoryList";
                security.companySeq = param.CompanySeq;

                DataSet ds = new DataSet("ROOT");

                DataTable dt10 = ds.Tables.Add("DataBlock10");
                dt10.Columns.Add("AcptMgmtSeq");
                dt10.Columns.Add("ReSurvAsgnNo");
                dt10.Columns.Add("ReportType");

                dt10.Clear();
                DataRow dr1 = dt10.Rows.Add();
                dr1["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr1["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr1["ReportType"] = param.ReportType;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null || yds.Tables.Count < 1)
                {
                    DataTable dtr = (DataTable)this.dgv.DataSource;
                    if (dtr != null) dtr.Rows.Clear();
                    return;
                }
                this.dgv.DataSource = yds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv.CurrentCell == null) return;
                Dgv_CellDoubleClick(this.dgv, new DataGridViewCellEventArgs(this.dgv.CurrentCell.ColumnIndex, this.dgv.CurrentCell.RowIndex));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;

                ReportParam p = new ReportParam();
                p.CompanySeq = param.CompanySeq;
                p.AcptMgmtSeq = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["AcptMgmtSeq"].Value);
                p.ReSurvAsgnNo = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["ReSurvAsgnNo"].Value);
                p.ReportType = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["ReportType"].Value);
                p.Seq = Utils.ToInt(this.dgv.Rows[e.RowIndex].Cells["Seq"].Value);

                string streamdata = Utils.ClassToJsonstring(p);
                ReportData response = YLWService.MTRServiceModule.CallMTRGetSaveRptHistoryPost(streamdata);
                YLWService.Response rsp = response.Response;
                if (rsp.Result != 1)
                {
                    throw new Exception(rsp.Message);
                }
                string caption = @"수정일시 : " + Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["SysDt"].Value);
                string filename = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["FileName"].Value);
                string fileseq = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["FileSeq"].Value);
                string rptname = response.ReportName;
                string rpttext = response.ReportText;
                byte[] rptbyte = Convert.FromBase64String(rpttext);

                MemoryStream stream = new MemoryStream(rptbyte);
                string file = Program.G_Create_ + "조사보고서이력(" + p.Seq + ").docx";
                if (File.Exists(file)) File.Delete(file);
                using (FileStream fs = new FileStream(file, FileMode.CreateNew))
                {
                    stream.CopyTo(fs);
                }
                stream.Dispose();

                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
                procInfo.UseShellExecute = true;
                procInfo.FileName = Program.G_UP_Path + "PDFViewer.exe";
                procInfo.Arguments = file + " " + $"\"{caption}\"";
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procInfo;
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
