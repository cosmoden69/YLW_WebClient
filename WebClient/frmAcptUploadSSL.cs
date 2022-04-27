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
    public partial class frmAcptUploadSSL : Form
    {
        string ftpServerIP = "";
        string ftpUserID = "";
        string ftpPassword = "";
        string ftpFolder = "";

        private static frmAcptUploadSSL current = null;
        public static frmAcptUploadSSL Current { get { return current; } }

        private static AttachFileParam _param = null;

        bool _bEvent = false;

        string dept7 = "";

        public frmAcptUploadSSL()
        {
            InitializeComponent();

            this.btnGet.Click += BtnGet_Click;
            this.btnConnect.Click += BtnConnect_Click;
            this.btnFtpInfo.Click += BtnFtpInfo_Click;
            this.cboDept.SelectedValueChanged += CboDept_SelectedValueChanged;

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
                    current = new frmAcptUploadSSL();
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }

                current.GetFileList();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                current.Close();
                return false;
            }
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                MessageBox.Show("다운로드할 파일을 선택하세요");
                return;
            }
            string filename = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["FILENAME"].Value + "";
            if (filename == "")
            {
                MessageBox.Show("다운로드 파일을 선택하세요");
                return;
            }
            string dept = Utils.GetComboSelectedValue(cboDept, "BeDeptSeq");
            if (dept == "")
            {
                MessageBox.Show("조사팀은 필수입니다");
                return;
            }

            try
            {
                string tempFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                string ftpFilePath = ftpServerIP + "/" + ftpFolder + "/" + filename;
                Utils.FtpDownload(ftpUserID, ftpPassword, ftpFilePath, tempFile);
                ReadFile(tempFile);

                for (int ii = 0; ii < dgvList.Rows.Count; ii++)
                {
                    dgvList.CurrentCell = dgvList.Rows[ii].Cells[0];
                    if (!ReadTxtFile(filename, dgvList.Rows[ii]))  //전문데이타 업로드
                    {
                        return;
                    }
                    Application.DoEvents();
                }
                for (int ii = dgvList.Rows.Count - 1; ii >= 0; ii--)
                {
                    dgvList.CurrentCell = dgvList.Rows[ii].Cells[0];
                    if (dgvList.Rows[ii].Cells["success_fg"].Value + "" == "OK")
                    {
                        dgvList.Rows.Remove(dgvList.Rows[ii]);
                    }
                    else if (dgvList.Rows[ii].Cells["success_fg"].Value + "" == "EXIST")
                    {
                        dgvList.Rows.Remove(dgvList.Rows[ii]);
                    }
                    Application.DoEvents();
                }
                if (dgvList.Rows.Count < 1)
                {
                    MessageBox.Show("접수 업로드 완료");
                    string rtn = Utils.FtpDelete(ftpUserID, ftpPassword, ftpFilePath);
                    if (rtn.StartsWith("250 DELE command successful.")) dgv.Rows.Remove(dgv.CurrentRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            { 
                GetFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFtpInfo_Click(object sender, EventArgs e)
        {
            try
            { 
                frmSSDFTPInfo info = new frmSSDFTPInfo(_param);
                if (info.ShowDialog(this) == DialogResult.OK)
                {
                    SetFtpInfo();
                    btnConnect.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CboDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            txtEmp.Text = "";
            txtEmpSeq.Text = "";
        }

        private void SetInit()
        {
            try
            {
                SetComboDept(cboDept);
                SetFtpInfo();

                this.dgv.AddColumn("TEXTBOX", "FILESIZE", "파일크기", 100);
                this.dgv.AddColumn("TEXTBOX", "FILENAME", "전문파일명", 400);
                this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                this.dgvList.AddColumn("TEXTBOX", "success_fg", "결과", 100);
                this.dgvList.AddColumn("TEXTBOX", "edi_text", "전문내용", 600);
                this.dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                this.dgvList.Columns["edi_text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                            case "get_folder":
                                ftpFolder = dt.Rows[i]["value_text"] + "";
                                break;
                            case "acpt_dept":
                                Utils.SetComboSelectedValue(cboDept, dt.Rows[i]["value_text"] + "", "BeDeptSeq");
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

        private void SetComboDept(ComboBox cboObj)
        {
            try
            {
                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                string strSql = "";

                strSql = "SELECT b.MinorName FROM _TDAUMinor b WHERE b.CompanySeq = @CompanySeq AND b.MajorSeq = '300163' AND b.MinorSeq = '300163002' FOR JSON PATH ";  //300163002:7(물)
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", _param.CompanySeq));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                DataTable dt7 = MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                dept7 = "";
                if (dt7 != null && dt7.Rows.Count > 0) dept7 = dt7.Rows[0]["MinorName"] + "";

                strSql = "";
                strSql += @" SELECT A.DeptName AS BeDeptName ";
                strSql += @"     ,A.BegDate AS BeBegDate ";
                strSql += @"     ,A.EndDate AS BeEndDate ";
                strSql += @"     ,A.Remark AS DeptRemark ";
                strSql += @"     ,A.DeptSeq AS BeDeptSeq ";
                strSql += @"     ,CASE WHEN A.EndDate >= CONVERT(NCHAR(8), GETDATE(), 112) THEN '1' ELSE '0' END AS IsUse  ";  /* 현재일을 기준으로 사용중인 부서인지 아닌지 판단 */
                strSql += @" FROM _TDADept AS A WITH(NOLOCK) ";
                strSql += @"      JOIN [dbo].[_fnOrgDeptHR](@CompanySeq, 1, @HeadDeptSeq, CONVERT(NCHAR(8), GETDATE(), 112)) AS hddept ON hddept.DeptSeq = A.DeptSeq ";
                strSql += @"      LEFT JOIN _THROrgDeptCCtr AS B WITH(NOLOCK) ON A.CompanySeq = B.CompanySeq AND A.DeptSeq = B.DeptSeq AND B.IsLast = '1' ";   /* 무조건 최종 활동센터를 표시한다. 2011.12.08 민형준 */
                strSql += @"      LEFT JOIN _TDACCtr AS C WITH(NOLOCK) ON A.CompanySeq = C.CompanySeq AND B.CCtrSeq = C.CCtrSeq ";
                strSql += @" WHERE A.CompanySeq = @CompanySeq ";
                strSql += @" AND   A.SMDeptType NOT IN(3051003, 3051004) ";  /* TFT제외, BPM부서 제외 */
                strSql += @" ";  /* AND CASE @DefQueryOption WHEN 0 THEN CONVERT(NVARCHAR(10), A.DeptSeq) ELSE A.DeptName END LIKE @Keyword */
                strSql += @" ORDER BY A.DeptName, A.DispSeq ";
                strSql += @" FOR JSON PATH ";
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", _param.CompanySeq));
                lstPara.Add(new SqlParameter("@HeadDeptSeq", dept7));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                DataTable dt = MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                Utils.SetCombo(cboObj, dt, "BeDeptSeq", "BeDeptName", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearScreen()
        {
            _bEvent = false;

            cboDept.SelectedIndex = -1;
            txtEmp.Text = "";
            txtEmpSeq.Text = "";
            dgv.Rows.Clear();
            DataTable dtr = (DataTable)dgvList.DataSource;
            if (dtr != null) dtr.Rows.Clear();

            _bEvent = true;
        }

        public void GetFileList()
        {
            FtpWebRequest reqFTP;

            string pattern = @"^(\d+-\d+-\d+\s+\d+:\d+(?:AM|PM))\s+(<DIR>|\d+)\s+(.+)$";
            Regex regex = new Regex(pattern);
            IFormatProvider culture = CultureInfo.GetCultureInfo("euc-kr");

            try
            {
                dgv.Rows.Clear();
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerIP + "/" + ftpFolder));
                reqFTP.KeepAlive = false;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    Match match = regex.Match(line);
                    string s = match.Groups[1].Value;
                    DateTime modified = DateTime.ParseExact(s, "MM-dd-yy  hh:mmtt", culture, DateTimeStyles.None);
                    s = match.Groups[2].Value;
                    if (s != "<DIR>")
                    {
                        long size = (s != "<DIR>") ? long.Parse(s) : 0;
                        string name = match.Groups[3].Value;
                        int rnum = dgv.Rows.Add();
                        dgv.Rows[rnum].Cells["FILESIZE"].Value = size;
                        dgv.Rows[rnum].Cells["FILENAME"].Value = name;
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReadFile(string fileName)
        {
            try
            {
                int counter = 0;
                string line = "";
                DataTable dt = new DataTable();
                dt.Columns.Add("edi_text");
                using (System.IO.StreamReader file = new System.IO.StreamReader(fileName, Encoding.GetEncoding("euc-kr")))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        DataRow dr = dt.Rows.Add();
                        dr["edi_text"] = line;
                        counter++;
                    }
                    file.Close();
                }
                dgvList.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                File.Delete(fileName);
            }
        }

        private bool ReadTxtFile(string filename, DataGridViewRow grow)
        {
            YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDITransSSL";
            security.methodId = "in";

            DataSet ds = new DataSet("ROOT");
            DataTable dt = ds.Tables.Add("DataBlock1");

            dt.Columns.Add("companyseq");
            dt.Columns.Add("send_type");
            dt.Columns.Add("success_fg");
            dt.Columns.Add("cust_code");
            dt.Columns.Add("trans_dtm");
            dt.Columns.Add("file_name");
            dt.Columns.Add("edi_text");
            dt.Columns.Add("CclsFg");
            dt.Columns.Add("AsgnTeamSeq");
            dt.Columns.Add("AsgnEmpSeq");

            try
            {
                string editext = grow.Cells["edi_text"].Value + "";
                string dept = Utils.GetComboSelectedValue(cboDept, "BeDeptSeq");
                string emp = txtEmpSeq.Text;
                string cclsfg = "0";
                if (dept != "") cclsfg = "1";
                if (emp != "") cclsfg = "2";

                dt.Clear();
                DataRow dr = dt.Rows.Add();

                dr["companyseq"] = security.companySeq;
                dr["send_type"] = 0;
                dr["success_fg"] = 0;
                dr["cust_code"] = "SSL";
                dr["file_name"] = filename;
                dr["edi_text"] = editext;
                dr["CclsFg"] = cclsfg;
                dr["AsgnTeamSeq"] = dept;
                dr["AsgnEmpSeq"] = emp;

                DataSet yds = MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null)
                {
                    DataTable dataBlock1 = yds.Tables["DataBlock1"];
                    if (dataBlock1 != null && dataBlock1.Rows.Count > 0)
                    {
                        if (dataBlock1.Rows[0]["success_fg"] + "" == "1")
                        {
                            string edi_id = dataBlock1.Rows[0]["edi_id"] + "";
                            grow.Cells["success_fg"].Value = "OK";
                        }
                        else if (dataBlock1.Rows[0]["success_fg"] + "" == "2")
                        {
                            //있는 설계번호임
                            grow.Cells["success_fg"].Value = "EXIST";
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                YLWService.LogWriter.WriteLog(ex.Message);
                return false;
            }
        }

    }
}
