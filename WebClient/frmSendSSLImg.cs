using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLWService;
using YLWService.Extensions;

namespace YLW_WebClient.CAA
{
    public partial class frmSendSSLImg : Form
    {
        string ftpServerIP = "";
        string ftpUserID = "";
        string ftpPassword = "";
        string ftpImgFolder = "";

        private static frmSendSSLImg current = null;
        public static frmSendSSLImg Current { get { return current; } }

        private static AttachFileParam _param = null;

        bool _bEvent = false;

        public frmSendSSLImg()
        {
            InitializeComponent();

            this.btnQuery.Click += BtnQuery_Click;
            this.dgvList.CellClick += DgvList_CellClick;

            SetInit();

            _bEvent = true;
        }

        public static bool ShowPreview(AttachFileParam p)
        {
            try
            {
                _param = p;

                if (current == null || current.IsDisposed)
                {
                    current = new frmSendSSLImg();
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }

                current.Query();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                current.Close();
                return false;
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Query();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv == null) return;
                if (e.RowIndex < 0 || e.RowIndex >= dgv.RowCount) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;
                if (dgv.Columns[e.ColumnIndex].Name == "Send")
                {
                    if (Utils.ToInt(dgv.Rows[e.RowIndex].Cells["SendFg"].Value) != 0)
                    {
                        MessageBox.Show("이미 전송처리되었습니다");
                        return;
                    }
                    SendImage(dgv.Rows[e.RowIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetInit()
        {
            try
            {
                SetFtpInfo();

                this.dgvList.AutoGenerateColumns = false;
                this.dgvList.AddColumn("TEXTBOX", "file_name", "파일명", 100);
                this.dgvList.AddColumn("TEXTBOX", "file_rmk", "비고", 100);
                this.dgvList.AddColumn("TEXTBOX", "file_seq", "file_seq", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "SendDtTm", "전송일자", 140);
                this.dgvList.AddColumn("TEXTBOX", "SendFg", "구분", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "SendFgNm", "구분", 60);
                this.dgvList.AddColumn("BUTTON", "Send", "전송", 60);
                this.dgvList.AddColumn("TEXTBOX", "TransOutDt", "TransOutDt", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "id", "id", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "parent_id", "parent_id", 10, false);
                this.dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                this.dgvList.Columns["file_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetFtpInfo()
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDISSLFtp";
                security.methodId = "Query";
                security.companySeq = _param.CompanySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("code_id");

                dt1.Clear();
                DataRow dr1 = dt1.Rows.Add();
                dr1["code_id"] = "USERCD_FTP";

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null || yds.Tables.Count > 0)
                {
                    DataTable dt = yds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["code"] + "")
                        {
                            case "ftp_Addr":
                                ftpServerIP = dt.Rows[i]["value_text"] + "";
                                break;
                            case "ftp_Id":
                                ftpUserID = dt.Rows[i]["value_text"] + "";
                                break;
                            case "ftp_Pwd":
                                ftpPassword = dt.Rows[i]["value_text"] + "";
                                break;
                            case "image_folder":
                                ftpImgFolder = dt.Rows[i]["value_text"] + "";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearScreen()
        {
            _bEvent = false;

            DataTable dtr = (DataTable)dgvList.DataSource;
            if (dtr != null) dtr.Rows.Clear();

            _bEvent = true;
        }

        private void Query()
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisEDISSLSpotImage";
                security.methodId = "Query";

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("req_id");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["req_id"] = _param.AcptMgmtSeq;

                DataTable dtr = (DataTable)dgvList.DataSource;
                if (dtr != null) dtr.Rows.Clear();
                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null && yds.Tables.Count > 0)
                {
                    if (yds.Tables.Contains("ErrorMessage")) throw new Exception(yds.Tables["ErrorMessage"].Rows[0]["Message"].ToString());
                    DataTable dataBlock2 = yds.Tables["DataBlock2"];
                    if (dataBlock2 != null && dataBlock2.Rows.Count > 0)
                    {
                        dgvList.DataSource = dataBlock2;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SendImage(DataGridViewRow drow)
        {
            string writefile = "";
            string ftpFilePath = "";

            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                writefile = GetSendFile(drow.Cells["file_seq"].Value + "");
                if (writefile == "") throw new Exception("이미지파일을 찾을 수 없습니다");

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisEDISSLSpotImage";
                security.methodId = "Send";

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock2");
                dt.Columns.Add("id");
                dt.Columns.Add("parent_id");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["id"] = drow.Cells["id"].Value;
                dr["parent_id"] = drow.Cells["parent_id"].Value;

                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null && yds.Tables.Count > 0)
                {
                    if (yds.Tables.Contains("ErrorMessage")) throw new Exception(yds.Tables["ErrorMessage"].Rows[0]["Message"].ToString());
                    DataTable dataBlock2 = yds.Tables["DataBlock2"];
                    if (dataBlock2 != null && dataBlock2.Rows.Count > 0)
                    {
                        string filename = dataBlock2.Rows[0]["file_name"] + "";
                        ftpFilePath = ftpServerIP + "/" + ftpImgFolder + "/" + filename;
                        Utils.FtpUpload(ftpUserID, ftpPassword, ftpFilePath, writefile);
                        drow.Cells["file_name"].Value =dataBlock2.Rows[0]["file_name"];
                        if (!CommitAll(drow)) return false;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                File.Delete(writefile);
                Cursor = Cursors.Default;
            }
        }

        private string GetSendFile(string fileSeq)
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet yds = YLWService.MTRServiceModule.CallMTRFileDownload(security, fileSeq, "", "");
                if (yds == null || yds.Tables.Count < 1) return "";
                DataTable ydt = yds.Tables[0];
                if (ydt.TableName == "ErrorMessage") return "";
                if (ydt.Rows.Count < 1) return "";
                string fileBase64 = ydt.Rows[0]["FileBase64"] + "";
                string writefile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                byte[] bytes_file = Convert.FromBase64String(fileBase64);
                FileStream fs = new FileStream(writefile, FileMode.Create);
                fs.Write(bytes_file, 0, bytes_file.Length);
                fs.Close();
                return writefile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CommitAll(DataGridViewRow drow)
        {
            try
            {
                YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisEDISSLSpotImage";
                security.methodId = "SendCommit";

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock2");
                dt.Columns.Add("file_name");
                dt.Columns.Add("id");
                dt.Columns.Add("parent_id");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["file_name"] = drow.Cells["file_name"].Value;
                dr["id"] = drow.Cells["id"].Value;
                dr["parent_id"] = drow.Cells["parent_id"].Value;

                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null && yds.Tables.Count > 0)
                {
                    if (yds.Tables.Contains("ErrorMessage")) throw new Exception(yds.Tables["ErrorMessage"].Rows[0]["Message"].ToString());
                    DataTable dataBlock2 = yds.Tables["DataBlock2"];
                    if (dataBlock2 != null && dataBlock2.Rows.Count > 0)
                    {
                        drow.Cells["SendDtTm"].Value = dataBlock2.Rows[0]["SendDtTm"];
                        drow.Cells["SendFg"].Value = dataBlock2.Rows[0]["SendFg"];
                        drow.Cells["SendFgNm"].Value = dataBlock2.Rows[0]["SendFgNm"];
                        drow.Cells["TransOutDt"].Value = dataBlock2.Rows[0]["TransOutDt"];
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
