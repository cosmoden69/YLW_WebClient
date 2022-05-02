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
    public partial class frmSendSSL : Form
    {
        string ftpServerIP = "";
        string ftpUserID = "";
        string ftpPassword = "";
        string ftpEdiFolder = "";

        private static frmSendSSL current = null;
        public static frmSendSSL Current { get { return current; } }

        private static AttachFileParam _param = null;

        bool _bEvent = false;

        public frmSendSSL()
        {
            InitializeComponent();

            this.btnQuery.Click += BtnQuery_Click;
            this.btnEDISend.Click += BtnEDISend_Click;

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
                    current = new frmSendSSL();
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

        private void BtnEDISend_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvList.Rows.Count < 1) return;
                if (SendEDI())
                {
                    MessageBox.Show("전문파일 업로드 완료");
                    DataTable dtr = (DataTable)dgvList.DataSource;
                    if (dtr != null) dtr.Rows.Clear();
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
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                dtpFrDt.Value = month.AddMonths(-1);
                dtpToDt.Value = month.AddDays(-1);

                SetFtpInfo();

                this.dgvList.AutoGenerateColumns = false;
                this.dgvList.AddColumn("TEXTBOX", "dsgnNo", "설계번호", 100);
                this.dgvList.AddColumn("TEXTBOX", "contrNm", "계약자명", 100);
                this.dgvList.AddColumn("TEXTBOX", "trDt", "조사요청일자", 80);
                this.dgvList.AddColumn("TEXTBOX", "CclsDt", "종결일자", 80);
                this.dgvList.AddColumn("TEXTBOX", "AsgnEmpSeq", "조사자내부코드", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "AsgnEmpName", "조사자명", 100);
                this.dgvList.AddColumn("TEXTBOX", "req_id", "조사요청내부코드", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "rtn_id", "조사결과내부코드", 10, false);
                this.dgvList.AddColumn("TEXTBOX", "edi_id", "결과_edi_id", 10, false);

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
                            case "edi_folder":
                                ftpEdiFolder = dt.Rows[i]["value_text"] + "";
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
                string strSql = "";
                strSql += @" SELECT L.dsgnNo, L.contrNm, L.trDt, L.CclsDt, L.AsgnEmpSeq, emp.EmpName AS AsgnEmpName, L.id AS req_id, L.id AS rtn_id, L.send_edi_id AS edi_id ";
                strSql += @" FROM _TAdjSL_SSL_SUIT AS L WITH(NOLOCK) ";
                strSql += @" LEFT JOIN _TDAEmp AS emp WITH(NOLOCK) ON emp.CompanySeq = L.CompanySeq ";
                strSql += @"      AND emp.EmpSeq     = L.AsgnEmpSeq ";
                strSql += @" WHERE  L.CompanySeq = @CompanySeq ";
                strSql += @" AND    L.transOutDt = '' ";
                strSql += @" AND    L.CclsFg = '6' ";
                strSql += @" AND    L.trDt >= @frdt ";
                strSql += @" AND    L.trDt <= @todt ";
                strSql += @" FOR JSON PATH ";

                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", YLWService.MTRServiceModule.SecurityJson.companySeq));
                lstPara.Add(new SqlParameter("@frdt", dtpFrDt.Value.ToString("yyyy-MM-dd")));
                lstPara.Add(new SqlParameter("@todt", dtpToDt.Value.ToString("yyyy-MM-dd")));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                strSql = strSql.Replace("\r\n", "");
                DataTable dt = MTRServiceModule.GetMTRServiceDataTable(YLWService.MTRServiceModule.SecurityJson.companySeq, strSql);
                if (dt == null)
                {
                    DataTable dtr = (DataTable)this.dgvList.DataSource;
                    if (dtr != null) dtr.Rows.Clear();
                    return;
                }
                this.dgvList.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SendEDI()
        {
            string trDt = DateTime.Now.ToString("yyyy-MM-dd");
            string writefile = "";
            string filename = "";
            string ftpFilePath = "";
            int fileSeq = 0;

            try
            {
                //Commit용 데이타 블럭
                DataTable dtC = new DataTable("DataBlock1");
                dtC.Columns.Add("send_type");
                dtC.Columns.Add("success_fg");
                dtC.Columns.Add("cust_code");
                dtC.Columns.Add("file_name");
                dtC.Columns.Add("rtn_id");
                dtC.Columns.Add("edi_id");
                dtC.Columns.Add("trDt");
                dtC.Clear();

                for (int ii = 0; ii < dgvList.Rows.Count; ii++)
                {
                    string rtn_id = Utils.ConvertToString(this.dgvList.Rows[ii].Cells["rtn_id"].Value);
                    string edi_id = Utils.ConvertToString(this.dgvList.Rows[ii].Cells["edi_id"].Value);
                    if (ii % 200 == 0)
                    {
                        if (ii > 0)
                        {
                            ftpFilePath = ftpServerIP + "/" + ftpEdiFolder + "/" + filename;
                            Utils.FtpUpload(ftpUserID, ftpPassword, ftpFilePath, writefile);
                        }
                        fileSeq++;
                        filename = "R.JUHSRS." + trDt.Replace("-", "") + "." + Utils.PadLeft(fileSeq, 3, '0') + ".txt";
                        writefile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                    }
                    DataRow dr = dtC.Rows.Add();
                    if (WriteFileAppend(writefile, filename, rtn_id, edi_id, trDt, dr) < 1)
                    {
                        throw new Exception("전문파일 생성중 에러 발생");
                    }
                }
                ftpFilePath = ftpServerIP + "/" + ftpEdiFolder + "/" + filename;
                Utils.FtpUpload(ftpUserID, ftpPassword, ftpFilePath, writefile);

                if (!CommitAll(dtC)) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int WriteFileAppend(string writefile, string filename, string rtn_id, string edi_id, string trDt, DataRow pdr)
        {
            try
            {
                YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDITransSSL";
                security.methodId = "out";

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock1");

                dt.Columns.Add("companyseq");
                dt.Columns.Add("send_type");
                dt.Columns.Add("success_fg");
                dt.Columns.Add("cust_code");
                dt.Columns.Add("rtn_id");
                dt.Columns.Add("edi_id");
                dt.Columns.Add("trDt");

                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["companyseq"] = security.companySeq;
                dr["send_type"] = 1;
                dr["success_fg"] = 0;
                dr["cust_code"] = "SSL";
                dr["rtn_id"] = rtn_id;
                dr["edi_id"] = edi_id;
                dr["trDt"] = trDt;

                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null && yds.Tables.Count > 0)
                {
                    if (yds.Tables.Contains("ErrorMessage")) throw new Exception(yds.Tables["ErrorMessage"].Rows[0]["Message"].ToString());
                    DataTable dataBlock1 = yds.Tables["DataBlock1"];
                    if (dataBlock1 != null && dataBlock1.Rows.Count > 0)
                    {
                        FileStream fs = new FileStream(writefile, FileMode.Append, FileAccess.Write);
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("euc-kr")))
                        {
                            sw.Write(dataBlock1.Rows[0]["edi_text"]);
                            sw.Write("\r\n");
                            sw.Close();
                        }
                        fs.Close();

                        pdr["send_type"] = 1;
                        pdr["success_fg"] = 1;
                        pdr["cust_code"] = "SSL";
                        pdr["file_name"] = filename;
                        pdr["rtn_id"] = rtn_id;
                        pdr["edi_id"] = dataBlock1.Rows[0]["edi_id"];
                        pdr["trDt"] = trDt;
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CommitAll(DataTable dt)
        {
            try
            {
                YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDITransSSL";
                security.methodId = "commit";

                DataSet ds = new DataSet("ROOT");
                ds.Tables.Add(dt);

                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
