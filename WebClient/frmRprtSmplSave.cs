using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLWService;

namespace YLW_WebClient
{
    public partial class frmRprtSmplSave : Form
    {
        ReportParam param = null;
        bool SmplAuth = false;
        DataSet pds = null;

        public frmRprtSmplSave(ReportParam p, bool auth, DataSet s)
        {
            this.param = p;
            this.SmplAuth = auth;
            this.pds = s;

            InitializeComponent();

            this.btnSave.Click += BtnSave_Click;
            this.btnCancel.Click += BtnCancel_Click;

            this.btnSave.Enabled = false;
            if (this.SmplAuth) this.btnSave.Enabled = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtRprtSmplNm.Text.Trim() == "")
            {
                MessageBox.Show("보고서 사례명을 입력하세요");
                return;
            }
            if (RprtSmpl_Save())
            {
                txtRprtSmplNm.Text = "";
                this.Close();
            }
        }

        private bool RprtSmpl_Save()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCSSmpl";
                security.methodId = "Save";
                security.companySeq = param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = param.UserID;

                DataSet ds = pds;
                DataTable dt = ds.Tables["DataBlock1"];
                if (!dt.Columns.Contains("DeptGrpCd")) dt.Columns.Add("DeptGrpCd");
                if (!dt.Columns.Contains("RprtSmplSeq")) dt.Columns.Add("RprtSmplSeq");
                if (!dt.Columns.Contains("RprtSmplNm")) dt.Columns.Add("RprtSmplNm");

                DataRow dr = dt.Rows[0];
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["DcmgDocNo"] = "";
                dr["DeptGrpCd"] = "1";   //인보험
                dr["RprtSmplSeq"] = "";
                dr["RprtSmplNm"] = txtRprtSmplNm.Text.Trim();

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("보고서 사례가 저장되지 않았습니다");
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
                MessageBox.Show("보고서 사례 저장 완료");
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
    }
}
