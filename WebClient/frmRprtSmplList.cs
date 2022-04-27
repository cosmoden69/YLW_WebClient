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

namespace YLW_WebClient
{
    public partial class frmRprtSmplList : Form
    {
        ReportParam param = null;
        bool SmplAuth = false;
        DataTable dtSorceList = null;

        public frmRprtSmplList(ReportParam p, bool auth)
        {
            this.param = p;
            this.SmplAuth = auth;

            InitializeComponent();

            this.btnQuery.Click += BtnQuery_Click;
            this.btnDel.Click += BtnDel_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.dgv.CellDoubleClick += Dgv_CellDoubleClick;
            this.txtRprtSmplNm.TextChanged += TxtRprtSmplNm_TextChanged;
            this.Load += FrmRprtSmplList_Load;

            this.dgv.AutoGenerateColumns = false;
            this.dgv.AddColumn("TEXTBOX", "AcptMgmtSeq", "접수내부코드", 10, false);
            this.dgv.AddColumn("TEXTBOX", "ReSurvAsgnNo", "재조사순번", 10, false);
            this.dgv.AddColumn("TEXTBOX", "DeptGrpCd", "사업부구분", 10, false);
            this.dgv.AddColumn("TEXTBOX", "RprtSmplSeq", "보고서 사례 번호", 10, false);
            this.dgv.AddColumn("TEXTBOX", "RprtSmplNm", "보고서 사례명칭", 670, true, true);
            this.dgv.AllowUserToAddRows = false;

            this.btnDel.Enabled = false;
            if (this.SmplAuth) this.btnDel.Enabled = true;
        }

        private void FrmRprtSmplList_Load(object sender, EventArgs e)
        {
            try
            {
                GetSmplList();

                this.txtRprtSmplNm.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetSmplList()
        {
            try
            {
                string strSql = "";
                strSql += @" SELECT A.AcptMgmtSeq, A.ReSurvAsgnNo, A.DeptGrpCd, A.RprtSmplSeq, A.RprtSmplNm ";
                strSql += @" FROM   _TAdjSLRprtSmplHead AS A WITH(NOLOCK) ";
                strSql += @" WHERE  A.CompanySeq = @CompanySeq ";
                strSql += @" AND    A.DeptGrpCd  = @DeptGrpCd ";
                strSql += @" ORDER BY A.RprtSmplNm ";
                strSql += @" FOR JSON PATH ";

                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", param.CompanySeq));
                lstPara.Add(new SqlParameter("@DeptGrpCd", "1"));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                strSql = strSql.Replace("\r\n", "");
                DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                if (ds == null || ds.Tables.Count < 1)
                {
                    DataTable dtr = (DataTable)this.dgv.DataSource;
                    if (dtr != null) dtr.Rows.Clear();
                    return;
                }
                dtSorceList = ds.Tables[0];
                this.dgv.DataSource = dtSorceList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtRprtSmplNm_TextChanged(object sender, EventArgs e)
        {
            try
            { 
                dtSorceList.DefaultView.RowFilter = "RprtSmplNm LIKE '%" + txtRprtSmplNm.Text + "%'";
                DataView dv = new DataView(dtSorceList.DefaultView.ToTable());
                this.dgv.DataSource = dv;
                this.dgv.Refresh();
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

        private void BtnDel_Click(object sender, EventArgs e)
        {
            string smplnm = Utils.ConvertToString(this.dgv.CurrentRow.Cells["RprtSmplNm"].Value);
            if (MessageBox.Show("현재 선택된 보고서 사례[" + smplnm + "]를 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            string grpcd = Utils.ConvertToString(this.dgv.CurrentRow.Cells["DeptGrpCd"].Value);
            string smplseq = Utils.ConvertToString(this.dgv.CurrentRow.Cells["RprtSmplSeq"].Value);
            if (RprtSmpl_Delete(grpcd, smplseq))
            {
                GetSmplList();
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
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

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;
                string acptmgmtseq = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["AcptMgmtSeq"].Value);
                string resurvasgnno = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["ReSurvAsgnNo"].Value);
                string grpcd = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["DeptGrpCd"].Value);
                string smplseq = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["RprtSmplSeq"].Value);
                string smplnm = Utils.ConvertToString(this.dgv.Rows[e.RowIndex].Cells["RprtSmplNm"].Value);

                ReportParam p = new ReportParam();
                p.ReportName = smplnm;
                p.AcptMgmtSeq = acptmgmtseq;
                p.ReSurvAsgnNo = resurvasgnno;
                p.CompanySeq = param.CompanySeq;
                p.UserID = param.UserID;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCSSmpl";
                security.methodId = "Query";
                security.companySeq = p.CompanySeq;

                DataSet ds = new DataSet("ROOT");

                DataTable dt = ds.Tables.Add("DataBlock1");

                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");
                dt.Columns.Add("DcmgDocNo");
                dt.Columns.Add("DeptGrpCd");
                dt.Columns.Add("RprtSmplSeq");
                dt.Columns.Add("RprtSmplNm");

                dt.Clear();
                DataRow dr = dt.Rows.Add();

                dr["AcptMgmtSeq"] = acptmgmtseq;
                dr["ReSurvAsgnNo"] = resurvasgnno;
                dr["DcmgDocNo"] = "";
                dr["DeptGrpCd"] = grpcd;
                dr["RprtSmplSeq"] = smplseq;
                dr["RprtSmplNm"] = smplnm;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }

                CAA.frmSampleView frm = new CAA.frmSampleView(grpcd, smplseq, smplnm);
                frm.Show();
                frm.LoadDocument(p, yds);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool RprtSmpl_Delete(string grpcd, string smplseq)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCSSmpl";
                security.methodId = "Delete";
                security.companySeq = param.CompanySeq;

                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("DeptGrpCd");
                dt.Columns.Add("RprtSmplSeq");

                DataRow dr = dt.Rows.Add();
                dr["DeptGrpCd"] = grpcd;
                dr["RprtSmplSeq"] = smplseq;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("보고서 사례가 삭제되지 않았습니다");
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
                MessageBox.Show("보고서 사례 삭제 완료");
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
