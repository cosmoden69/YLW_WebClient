using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLWService;
using YLWService.Extensions;

namespace YLW_WebClient.CAA
{
    public partial class frmSSDFTPInfo : Form
    {
        private static AttachFileParam _param = null;

        bool _bEvent = false;

        string dept7 = "";

        public frmSSDFTPInfo(AttachFileParam p)
        {
            _param = p;

            InitializeComponent();

            btnSave.Click += BtnSave_Click;
            txtEmp.KeyDown += TxtEmp_KeyDown;
            txtEmp.MouseDoubleClick += TxtEmp_MouseDoubleClick;
            txtCnfEmp.KeyDown += TxtCnfEmp_KeyDown;
            txtCnfEmp.MouseDoubleClick += TxtCnfEmp_MouseDoubleClick;

            SetInit();

            _bEvent = true;
        }

        private void TxtEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow dr = FrmFindEmp.QueryFindOne(txtEmp.Text);
                if (dr != null)
                {
                    txtEmp.Text = dr["EmpName"] + "";
                    txtEmpSeq.Text = dr["EmpSeq"] + "";
                    return;
                }
                TxtEmp_MouseDoubleClick(txtEmp, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 1));
            }
        }

        private void TxtCnfEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow dr = FrmFindEmp.QueryFindOne(txtCnfEmp.Text);
                if (dr != null)
                {
                    txtCnfEmp.Text = dr["EmpName"] + "";
                    txtCnfEmpSeq.Text = dr["EmpSeq"] + "";
                    return;
                }
                TxtCnfEmp_MouseDoubleClick(txtCnfEmp, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 1));
            }
        }

        private void TxtEmp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string dept = "";
            FrmFindEmp f = new FrmFindEmp(dept, txtEmp.Text);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                _bEvent = false;
                txtEmp.Text = f.ReturnFields.Find(x => x.FieldCode == "EmpName").FieldValue.ToString();
                txtEmpSeq.Text = f.ReturnFields.Find(x => x.FieldCode == "EmpSeq").FieldValue.ToString();
                dept = f.ReturnFields.Find(x => x.FieldCode == "DeptSeq").FieldValue.ToString();
                _bEvent = true;
            }
        }

        private void TxtCnfEmp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string dept = "";
            FrmFindEmp f = new FrmFindEmp(dept, txtCnfEmp.Text);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                _bEvent = false;
                txtCnfEmp.Text = f.ReturnFields.Find(x => x.FieldCode == "EmpName").FieldValue.ToString();
                txtCnfEmpSeq.Text = f.ReturnFields.Find(x => x.FieldCode == "EmpSeq").FieldValue.ToString();
                dept = f.ReturnFields.Find(x => x.FieldCode == "DeptSeq").FieldValue.ToString();
                _bEvent = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                MessageBox.Show("저장 완료");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SetInit()
        {
            try
            {
                SetComboDept(cboDept);

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
                                txtFtpAddr.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "ftp_Id":
                                txtFtpId.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "ftp_Pwd":
                                txtFtpPwd.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "get_folder":
                                txtGetFolder.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "edi_folder":
                                txtEdiFolder.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "image_folder":
                                txtImgFolder.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "Ent_Emp":
                                txtEntEmp.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "Cat_Code":
                                txtCatCode.Text = dt.Rows[i]["value_text"] + "";
                                break;
                            case "acpt_dept":
                                Utils.SetComboSelectedValue(cboDept, dt.Rows[i]["value_text"] + "", "BeDeptSeq");
                                break;
                            case "acpt_emp":
                                txtEmpSeq.Text = dt.Rows[i]["value_text"] + "";
                                txtEmp.Text = GetEmpName(txtEmpSeq.Text);
                                break;
                            case "conf_emp":
                                txtCnfEmpSeq.Text = dt.Rows[i]["value_text"] + "";
                                txtCnfEmp.Text = GetEmpName(txtCnfEmpSeq.Text);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetComboDept(ComboBox cboObj)
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

        private bool Save()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDISSLFtp";
                security.methodId = "Save";
                security.companySeq = _param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = _param.UserID;

                DataSet ds = new DataSet("ROOT");
                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("code_id");
                dt1.Columns.Add("code");
                dt1.Columns.Add("value_text");

                dt1.Clear();
                DataRow dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "ftp_Addr";
                dr["value_text"] = txtFtpAddr.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "ftp_Id";
                dr["value_text"] = txtFtpId.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "ftp_Pwd";
                dr["value_text"] = txtFtpPwd.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "get_folder";
                dr["value_text"] = txtGetFolder.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "edi_folder";
                dr["value_text"] = txtEdiFolder.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "image_folder";
                dr["value_text"] = txtImgFolder.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "Ent_Emp";
                dr["value_text"] = txtEntEmp.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "Cat_Code";
                dr["value_text"] = txtCatCode.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "acpt_dept";
                dr["value_text"] = Utils.GetComboSelectedValue(cboDept, "BeDeptSeq");
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "acpt_emp";
                dr["value_text"] = txtEmpSeq.Text;
                dr = dt1.Rows.Add();
                dr["code_id"] = "USERCD_FTP";
                dr["code"] = "conf_emp";
                dr["value_text"] = txtCnfEmpSeq.Text;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return false;
                }
                foreach (DataTable dti in yds.Tables)
                {
                    if (!dti.Columns.Contains("Status")) continue;
                    if (!dti.Columns.Contains("Result")) continue;
                    if (dti.Rows.Count > 0 && Convert.ToInt32(dti.Rows[0]["Status"]) != 0)   //Status != 0 이면 저장안됨
                    {
                        MessageBox.Show(dti.Rows[0]["Result"] + "");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private string GetEmpName(string empSeq)
        {
            try
            {
                string strSql = "";
                strSql += @" SELECT A.EmpName AS EmpName ";
                strSql += @" FROM   _TDAEmp AS A WITH(NOLOCK) ";
                strSql += @" WHERE  A.CompanySeq = @CompanySeq ";
                strSql += @" AND    A.EmpSeq     = @EmpSeq ";
                strSql += @" FOR JSON PATH ";

                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", _param.CompanySeq));
                lstPara.Add(new SqlParameter("@EmpSeq", empSeq));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                strSql = strSql.Replace("\r\n", "");
                DataTable dt = MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["EmpName"] + "";
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
