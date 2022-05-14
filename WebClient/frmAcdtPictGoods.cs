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

using YLW_WebClient.Painter;
using YLWService;

namespace YLW_WebClient
{
    public partial class frmAcdtPictGoods : Form
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

        private static ReportParam _param = null;

        private static frmAcdtPictGoods current = null;

        public static frmAcdtPictGoods Current { get { return current; } }

        private ImageEditView editView = null;

        DataTable CD_Goods;

        bool _bEvent = false;

        public frmAcdtPictGoods()
        {
            InitializeComponent();

            this.dgv_dbox.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.dgv_dbox.RowPostPaint += Dgv_dbox_RowPostPaint;
            this.dgv_dbox.DragDrop += Dgv_dbox_DragDrop;
            this.dgv_dbox.DragEnter += Dgv_dbox_DragEnter;
            this.dgv_dbox.DataError += Dgv_dbox_DataError;
            this.dgv_dbox.CellDoubleClick += Dgv_dbox_CellDoubleClick;
            this.dgv_dbox.CellContentClick += Dgv_dbox_CellContentClick;
            this.dgv_dbox.CellValueChanged += Dgv_dbox_CellValueChanged;
            this.dgv_dbox.CellValidating += Dgv_dbox_CellValidating;
            this.dgv_dbox.EditingControlShowing += Dgv_dbox_EditingControlShowing;
            this.cboAcdtPictFg.SelectedIndexChanged += CboAcdtPictFg_SelectedIndexChanged;
            this.btnFileA.Click += BtnFileA_Click;
            this.btnFileD.Click += BtnFileD_Click;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnClose.Click += BtnClose_Click;
            this.btnAcdtPictFgAply.Click += BtnAcdtPictFgAply_Click;
            this.btnSortOdrAply.Click += BtnSortOdrAply_Click;
            this.btnImageView.Click += BtnImageView_Click;
            this.Disposed += FrmAcdtPictGoods_Disposed;

            _bEvent = true;
        }

        public static bool ShowPreview(ReportParam p)
        {
            try
            {
                _param = p;

                if (current == null || current.IsDisposed)
                {
                    current = new frmAcdtPictGoods();
                    current.Top = 0;
                    current.Left = (Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
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

        public bool LoadDocument()
        {
            try
            {
                if (editView != null) editView.Close();

                _bEvent = false;
                SetInit();
                _bEvent = true;

                btnQuery.PerformClick();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void FrmAcdtPictGoods_Disposed(object sender, EventArgs e)
        {
            if (editView != null) editView.Close();
        }

        private void SetInit()
        {
            try
            {
                string strSql = "";
                strSql += @" SELECT mnr.MinorSeq, mnr.MinorName ";
                strSql += @" FROM   _TDAUMinor mnr WITH(NOLOCK) ";
                strSql += @" WHERE  mnr.CompanySeq = @CompanySeq ";
                strSql += @" AND    mnr.MajorSeq   = @MajorSeq ";
                strSql += @" ORDER  BY mnr.MinorSort ";
                strSql += @" FOR JSON PATH ";
                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", _param.CompanySeq));
                lstPara.Add(new SqlParameter("@MajorSeq", "300092"));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                DataTable dt = YLWService.MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                //Utils.SetCombo(cboAcdtPictFgChg, dt.Copy(), "MinorSeq", "MinorName", false);
                Utils.SetCombo(cboAcdtPictFg, dt, "MinorSeq", "MinorName", true);

                ////목적물
                //strSql = "";
                //strSql += " SELECT CONVERT(VARCHAR,gds.ObjSeq) AS ObjSeq, gds.InsurObjDvs AS ObjName, gds.RprtSeq AS sortOrder ";
                //strSql += " FROM   _TAdjSLInsTargetGoods AS gds WITH(NOLOCK) ";
                //strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                //strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                //strSql += " ORDER BY sortOrder ";
                //strSql += " FOR JSON PATH ";
                //DataTable dt1 = YLWService.MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                //strSql += " UNION ALL ";
                ////피해물
                //strSql = "";
                //strSql += " SELECT CONVERT(VARCHAR,gds.DmobSeq) AS ObjSeq, gds.DmobNm AS ObjName,gds.DmobSortNo AS sortOrder ";
                //strSql += " FROM   _TADjSLDamObjSurv AS gds WITH(NOLOCK) ";
                //strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                //strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                //strSql += " ORDER BY sortOrder ";
                //strSql += " FOR JSON PATH ";
                //DataTable dt2 = YLWService.MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                //strSql += " UNION ALL ";
                ////피해자
                //strSql = "";
                //strSql += " SELECT CONVERT(VARCHAR,gds.VitmSubSeq) AS ObjSeq, gds.Vitm AS ObjName, gds.VitmSortNo AS sortOrder ";
                //strSql += " FROM   _TAdjSLVitmInfo AS gds WITH(NOLOCK) ";
                //strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                //strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                //strSql += " ORDER BY sortOrder ";
                //strSql += " FOR JSON PATH ";
                //DataTable dt3 = YLWService.MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                //strSql += " ORDER BY sortOrder ";
                //strSql += " FOR JSON PATH ";

                strSql = "";
                strSql += " SELECT DISTINCT t.ObjSeq, t.ObjName FROM ( ";
                //목적물
                strSql += " SELECT CONVERT(VARCHAR,gds.ObjSeq) AS ObjSeq, gds.InsurObjDvs AS ObjName, gds.RprtSeq AS sortOrder ";
                strSql += " FROM   _TAdjSLInsTargetGoods AS gds WITH(NOLOCK) ";
                strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                strSql += " UNION ALL ";
                //피해물
                strSql += " SELECT CONVERT(VARCHAR,gds.DmobSeq) AS ObjSeq, gds.DmobNm AS ObjName,gds.DmobSortNo AS sortOrder ";
                strSql += " FROM   _TADjSLDamObjSurv AS gds WITH(NOLOCK) ";
                strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                strSql += " UNION ALL ";
                //피해자
                strSql += " SELECT CONVERT(VARCHAR,gds.VitmSubSeq) AS ObjSeq, gds.Vitm AS ObjName, gds.VitmSortNo AS sortOrder ";
                strSql += " FROM   _TAdjSLVitmInfo AS gds WITH(NOLOCK) ";
                strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                strSql += " UNION ALL ";
                //저장데이타
                strSql += " SELECT CONVERT(VARCHAR,gds.ObjSeq) AS ObjSeq, gds.ObjNm AS ObjName, gds.AcdtPictSerl AS sortOrder ";
                strSql += " FROM   _TAdjSLAcdtImgList AS gds WITH(NOLOCK) ";
                strSql += " WHERE  gds.CompanySeq = '" + Utils.ConvertToString(_param.CompanySeq) + "' ";
                strSql += " AND    gds.AcptMgmtSeq = '" + Utils.ConvertToString(_param.AcptMgmtSeq) + "' ";
                //
                strSql += " ) AS t ";
                strSql += " FOR JSON PATH ";
                CD_Goods = YLWService.MTRServiceModule.GetMTRServiceDataTable(_param.CompanySeq, strSql);
                if (CD_Goods != null)
                {
                    CD_Goods.TableName = "CD_Goods";
                    SetCombo(ObjSeq1, CD_Goods.Copy(), "ObjSeq", "ObjName", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetCombo(DataGridViewComboBoxColumn cboObj, DataTable pdt, string strValueMember, string strDisplayMember, bool bAddnull)
        {
            try
            {
                cboObj.Items.Clear();
                if (pdt == null) return;
                if (bAddnull)
                {
                    cboObj.Items.Add("");
                }
                for (int i = 0; i < pdt.Rows.Count; i++)
                {
                    cboObj.Items.Add(pdt.Rows[i][strDisplayMember]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            if (chkChanged() == true)
            {
                if (MessageBox.Show("수정된 내역이 있습니다. 새로 조회하시겠습니까", "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAcdtImgLiGds";
                security.methodId = "Query";
                security.companySeq = _param.CompanySeq;

                DataSet ds = new DataSet("ROOT");

                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("AcdtPictFg");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFg, "MinorSeq");
                dr["AcptMgmtSeq"] = _param.AcptMgmtSeq;
                dr["AcdtPictFg"] = acdtPictFg;

                dgv_dbox.Rows.Clear();
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }

                // 사진 이미지               
                DataTable SA16 = yds.Tables["DataBlock2"];
                if (SA16 != null && SA16.Rows.Count > 0)
                {
                    for (int i = 0; i < SA16.Rows.Count; i++)
                    {
                        int idx = dgv_dbox.Rows.Add();
                        SetGridRow(dgv_dbox.Rows[idx], SA16.Rows[i]);
                        dgv_dbox.AutoResizeRow(idx, DataGridViewAutoSizeRowMode.AllCells);
                    }
                    dgv_dbox.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (chkChanged() != true)
            {
                MessageBox.Show("수정된 내역이 없습니다");
                return;
            }
            if (chkValid() != true)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("AU"), out yds) == true)
                {
                    DataTable SA16 = yds.Tables["DataBlock2"];
                    for (int ii = 0; ii < SA16.Rows.Count; ii++)
                    {
                        SA16.Rows[ii]["WorkingTag"] = "";
                        int idx = Utils.ToInt(SA16.Rows[ii]["IDX_NO"]);
                        SetGridRow(dgv_dbox.Rows[idx - 1], SA16.Rows[ii]);
                    }
                    dgv_dbox.Refresh();
                    MessageBox.Show("사진저장 완료");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboAcdtPictFg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            btnQuery.PerformClick();
        }

        private void DeleteRows()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("D"), out yds) == true)
                {
                    DataTable SA16 = yds.Tables["DataBlock2"];
                    if (SA16 == null) return;
                    for (int ii = SA16.Rows.Count - 1; ii >= 0; ii--)
                    {
                        int idx = Utils.ToInt(SA16.Rows[ii]["IDX_NO"]);
                        dgv_dbox.Rows.RemoveAt(idx - 1);
                    }
                    dgv_dbox.Refresh();
                    //MessageBox.Show("행삭제 완료");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool SaveData(DataSet pds, out DataSet yds)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisAcdtImgLiGds";
            security.methodId = "Save";
            security.companySeq = _param.CompanySeq;
            security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
            security.securityType = 1;
            security.userId = _param.UserID;

            yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, pds);
            if (yds == null)
            {
                MessageBox.Show("저장할 수 없습니다");
                return false;
            }
            foreach (DataTable dti in yds.Tables)
            {
                if (!dti.Columns.Contains("Status")) continue;
                if (!dti.Columns.Contains("Result")) continue;
                if (dti.Rows != null && dti.Columns.Contains("Status"))
                {
                    for (int ii = 0; ii < dti.Rows.Count; ii++)
                    {
                        if (Convert.ToInt32(dti.Rows[ii]["Status"]) != 0)   //Status != 0 이면 저장안됨
                        {
                            MessageBox.Show("저장안됨:" + dti.Rows[ii]["Result"]);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void BtnSortOdrAply_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_dbox.Sort(new RowComparer(System.Windows.Forms.SortOrder.Ascending));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAcdtPictFgAply_Click(object sender, EventArgs e)
        {
            try
            {
                string acdtPictFgName = cboAcdtPictFgChg.Text;
                string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFgChg, "MinorSeq");
                if (acdtPictFg == "")
                {
                    MessageBox.Show("사진구분을 선택해야 됩니다");
                    return;
                }

                foreach (DataGridViewRow dr in dgv_dbox.SelectedRows)
                {
                    dr.Cells["AcdtPictFgName"].Value = acdtPictFgName;
                    dr.Cells["AcdtPictFg"].Value = acdtPictFg;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnImageView_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dgv_dbox.CurrentRow;
                if (row == null) return;
                Image img = stringToImage(row.Cells["AcdtPictImage"].Value + "");
                ImageView(row, img);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool chkChanged()
        {
            try
            {
                for (int ii = 0; ii < dgv_dbox.Rows.Count; ii++)
                {
                    DataGridViewRow dr = dgv_dbox.Rows[ii];
                    string workingTag = dr.Cells["WorkingTag"].Value + "";
                    if (workingTag != "") return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool chkValid()
        {
            try
            {
                for (int ii = 0; ii < dgv_dbox.Rows.Count; ii++)
                {
                    DataGridViewRow dr = dgv_dbox.Rows[ii];
                    if (dr.Cells["AcdtPictFg"].Value + "" == "")
                    {
                        MessageBox.Show((ii + 1).ToString() + "번 줄에 사진구분을 선택해야 됩니다");
                        return false;
                    }
                    if (dr.Cells["ObjSeq1"].Value + "" == "")
                    {
                        MessageBox.Show((ii + 1).ToString() + "번 줄에 목적물명을 입력해야 됩니다");
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
        }

        private DataSet GetSaveData(string fg)
        {
            try
            {
                DataSet ds = new DataSet();

                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("AcptMgmtSeq");
                dt1.Columns.Add("AcdtPictFg");
                dt1.Clear();
                DataRow dr = dt1.Rows.Add();
                dr["AcptMgmtSeq"] = _param.AcptMgmtSeq;

                // 사진 이미지
                DataTable dt2 = ds.Tables.Add("DataBlock2");     // 사진
                dt2.Columns.Add("AcdtPictFgName");
                dt2.Columns.Add("AcdtPictSeq");
                dt2.Columns.Add("AcdtPictFg");
                dt2.Columns.Add("AcdtPictFile");
                dt2.Columns.Add("AcdtPictDt");
                dt2.Columns.Add("AcdtPictCnts");
                dt2.Columns.Add("ObjNm");
                dt2.Columns.Add("ObjSymb");
                dt2.Columns.Add("AcdtPictRmk");
                dt2.Columns.Add("AcdtPictSerl");
                dt2.Columns.Add("AcdtPictImage");
                dt2.Columns.Add("ObjSeq");
                dt2.Columns.Add("WorkingTag");
                dt2.Columns.Add("IDX_NO");
                dt2.Columns.Add("DataSeq");
                dt2.Clear();
                int seq = 1;
                for (int i = 0; i < dgv_dbox.RowCount; i++)
                {
                    string workingTag = dgv_dbox.Rows[i].Cells["WorkingTag"].Value + "";
                    if (fg == "AU")
                    {
                        if (workingTag != "A" && workingTag != "U") continue;
                    }
                    if (fg == "D")
                    {
                        if (workingTag != "D") continue;
                    }
                    dr = dt2.Rows.Add();
                    dr["AcdtPictFgName"] = dgv_dbox.Rows[i].Cells["AcdtPictFgName"].Value;
                    dr["AcdtPictSeq"] = dgv_dbox.Rows[i].Cells["AcdtPictSeq"].Value;
                    dr["AcdtPictFg"] = dgv_dbox.Rows[i].Cells["AcdtPictFg"].Value;
                    dr["AcdtPictFile"] = dgv_dbox.Rows[i].Cells["AcdtPictFile"].Value;
                    dr["AcdtPictDt"] = Utils.DateFormat(dgv_dbox.Rows[i].Cells["AcdtPictDt"].Value, "yyyyMMdd");
                    dr["AcdtPictCnts"] = dgv_dbox.Rows[i].Cells["AcdtPictCnts"].Value;
                    CustomDataGridViewComboBoxCell cel = dgv_dbox.Rows[i].Cells["ObjSeq1"] as CustomDataGridViewComboBoxCell;
                    dr["ObjNm"] = cel.EditedFormattedValue;  //dgv_dbox.Rows[i].Cells["ObjNm"].Value;
                    dr["ObjSymb"] = dgv_dbox.Rows[i].Cells["ObjSymb"].Value;
                    dr["AcdtPictRmk"] = dgv_dbox.Rows[i].Cells["AcdtPictRmk"].Value;
                    dr["AcdtPictSerl"] = dgv_dbox.Rows[i].Cells["AcdtPictSerl"].Value;
                    dr["AcdtPictImage"] = dgv_dbox.Rows[i].Cells["AcdtPictImage"].Value;
                    dr["ObjSeq"] = GetObjSeq(Utils.ConvertToString(dr["ObjNm"]));
                    //dr["ObjSeq"] = dgv_dbox.Rows[i].Cells["ObjSeq1"].Value;
                    dr["WorkingTag"] = dgv_dbox.Rows[i].Cells["WorkingTag"].Value;
                    dr["IDX_NO"] = i + 1;
                    dr["Dataseq"] = seq;
                    seq++;
                }

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private object GetObjSeq(string objnm)
        {
            if (CD_Goods != null && CD_Goods.Rows.Count > 0)
            {
                for (int i = 0; i < CD_Goods.Rows.Count; i++)
                {
                    if (Utils.ConvertToString(CD_Goods.Rows[i]["ObjName"]) == objnm)
                    {
                        return CD_Goods.Rows[i]["ObjSeq"];
                    }
                }
            }
            return "0";
        }

        private void dbox_file_add(string P_File)
        {
            try
            {
                _bEvent = false;

                //if (readOnlyMode) return;
                FileInfo F_Info = new FileInfo(P_File);

                Image img = ToImage(F_Info);
                int rowIndex = dgv_dbox.Rows.Add();
                DataGridViewRow dr = dgv_dbox.Rows[rowIndex];

                string acdtPictFgName = cboAcdtPictFg.Text;
                string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFg, "MinorSeq");
                dr.Cells["imgAcdtPictImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);
                dr.Cells["AcdtPictSeq"].Value = DBNull.Value;
                dr.Cells["AcdtPictFgName"].Value = acdtPictFgName;
                dr.Cells["AcdtPictFg"].Value = acdtPictFg;
                dr.Cells["ObjSeq1"].Value = DBNull.Value;
                //dr.Cells["ObjNm"].Value = Path.GetFileNameWithoutExtension(F_Info.Name);
                dr.Cells["AcdtPictDt"].Value = F_Info.CreationTime;
                dr.Cells["AcdtPictFile"].Value = F_Info.Name;
                dr.Cells["AcdtPictImage"].Value = ImageToString(img);

                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "")
                {
                    dr.HeaderCell.Value = "A";
                    dr.Cells["WorkingTag"].Value = "A";
                }

                dgv_dbox.AutoResizeRow(rowIndex, DataGridViewAutoSizeRowMode.AllCells);
            }
            catch { }
            finally
            {
                _bEvent = true;
            }
        }

        private void Dgv_dbox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != dgv_dbox.Columns["imgAcdtPictImage"].Index) return;
                DataGridViewRow row = dgv_dbox.Rows[e.RowIndex];
                if (row == null) return;
                Image img = stringToImage(row.Cells["AcdtPictImage"].Value + "");
                ImageView(row, img);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Dgv_dbox_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                //if (readOnlyMode) return;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Dgv_dbox_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void Dgv_dbox_DragDrop(object sender, DragEventArgs e)
        {
            string acdtPictFgName = cboAcdtPictFg.Text;
            string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFg, "MinorSeq");
            if (acdtPictFg == "")
            {
                MessageBox.Show("사진구분을 선택해야 됩니다");
                return;
            }

            try
            {
                //if (readOnlyMode) return;
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string P_File in FileList)
                {
                    dbox_file_add(P_File);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Dgv_dbox_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_bEvent) return;

            try
            {
                DataGridViewRow dr = dgv_dbox.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "")
                {
                    dr.HeaderCell.Value = "U";
                    dr.Cells["WorkingTag"].Value = "U";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Dgv_dbox_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgv_dbox.CurrentCell.ColumnIndex == ObjSeq1.Index)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        private void Dgv_dbox_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == ObjSeq1.Index)
            {
                var cell = this.dgv_dbox.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (cell != null && e.FormattedValue.ToString() != string.Empty && !cell.Items.Contains(e.FormattedValue))
                {
                    this.ObjSeq1.Items.Add(e.FormattedValue);
                    if (this.dgv_dbox.IsCurrentCellDirty)
                    {
                        this.dgv_dbox.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
                cell.Value = e.FormattedValue;
            }
        }

        private void Dgv_dbox_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgv_dbox.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";

                //' Position text
                SizeF size = e.Graphics.MeasureString(workingTag, dgv_dbox.Font);
                if (dgv_dbox.RowHeadersWidth < Convert.ToInt32(size.Width + 20))
                {
                    dgv_dbox.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
                }

                //' Use default system text brush
                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(workingTag, dgv_dbox.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
            }
            catch { }
        }

        private void Dgv_dbox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (readOnlyMode) return;
                DataGridView dgv = (DataGridView)sender;

                if (e.RowIndex > -1)
                {
                    if (!dgv.Rows[e.RowIndex].IsNewRow)
                    {
                        if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                        {
                            if ((dgv.Columns[e.ColumnIndex] as DataGridViewButtonColumn).Text == "") return;
                            if (MessageBox.Show("정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                            {
                                if (dgv.Rows[e.RowIndex].Cells["WorkingTag"].Value + "" == "A")
                                {
                                    dgv.Rows.Remove(dgv.Rows[e.RowIndex]);
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["WorkingTag"].Value = "D";
                                    DeleteRows();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFileA_Click(object sender, EventArgs e)
        {
            string acdtPictFgName = cboAcdtPictFg.Text;
            string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFg, "MinorSeq");
            if (acdtPictFg == "")
            {
                MessageBox.Show("사진구분을 선택해야 됩니다");
                return;
            }

            //if (readOnlyMode) return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string P_File in openFileDialog.FileNames)
                    {
                        dbox_file_add(P_File);
                    }

                }
            }
            panImage.Focus();
        }

        private void BtnFileD_Click(object sender, EventArgs e)
        {
            //if (readOnlyMode) return;
            if (dgv_dbox.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("선택된 " + dgv_dbox.SelectedRows.Count.ToString() + "개 파일을 정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgv_dbox.SelectedRows)
                    {
                        if (row.Cells["WorkingTag"].Value + "" == "A")
                        {
                            dgv_dbox.Rows.Remove(row);
                        }
                        else
                        {
                            row.Cells["WorkingTag"].Value = "D";
                        }
                    }
                    DeleteRows();
                }
            }
            panImage.Focus();
        }

        private bool SetGridRow(DataGridViewRow drow, DataRow dr)
        {
            try
            {
                _bEvent = false;

                string strImage = dr["AcdtPictImage"].ToString();
                Image img = stringToImage(strImage);
                drow.Cells["imgAcdtPictImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);   // 사진
                drow.Cells["AcdtPictSeq"].Value = dr["AcdtPictSeq"];
                drow.Cells["AcdtPictFgName"].Value = dr["AcdtPictFgName"];
                drow.Cells["AcdtPictFg"].Value = dr["AcdtPictFg"];
                //drow.Cells["ObjNm"].Value = dr["ObjNm"];
                //drow.Cells["ObjSeq1"].Value = dr["ObjSeq"];
                drow.Cells["ObjSeq1"].Value = dr["ObjNm"];
                drow.Cells["AcdtPictSerl"].Value = dr["AcdtPictSerl"];
                drow.Cells["ObjSymb"].Value = dr["ObjSymb"];
                drow.Cells["AcdtPictCnts"].Value = dr["AcdtPictCnts"];
                drow.Cells["AcdtPictRmk"].Value = dr["AcdtPictRmk"];
                drow.Cells["AcdtPictDt"].Value = Utils.ConvertToDateTime(dr["AcdtPictDt"]);
                drow.Cells["AcdtPictFile"].Value = dr["AcdtPictFile"];
                drow.Cells["AcdtPictImage"].Value = dr["AcdtPictImage"];
                drow.Cells["WorkingTag"].Value = (dr.Table.Columns.Contains("WorkingTag") ? dr["WorkingTag"] + "" : "");
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _bEvent = true;
            }
        }

        private Image stringToImage(string inputString)
        {
            try
            {
                if (inputString.Length % 4 != 0)  //이미지가 4의 배수이므로 잘못된 이미지인 경우 잘라낸다
                {
                    inputString = inputString.Substring(0, inputString.Length - (inputString.Length % 4));
                }
                byte[] imageBytes = Convert.FromBase64String(inputString);
                MemoryStream ms = new MemoryStream(imageBytes);
                Image image = Image.FromStream(ms, true, true);
                return image;
            }
            catch
            {
                return null;
            }
        }

        private Image ToImage(FileInfo F_Info)
        {
            try
            {
                byte[] imgData = System.IO.File.ReadAllBytes(F_Info.FullName);
                MemoryStream ms = new MemoryStream(imgData);
                Image img = Image.FromStream(ms);
                ms.Close();
                return img;
            }
            catch
            {
                return null;
            }

        }

        private string ImageToString(Image value, System.Drawing.Imaging.ImageFormat fmt = null)
        {
            try
            {
                if (value == null)
                    return "";
                else
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmp = new Bitmap(value);
                        if (fmt == null)
                        {
                            bmp.Save(ms, value.RawFormat);
                        }
                        else
                        {
                            bmp.Save(ms, fmt);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ImageView(DataGridViewRow row, Image img)
        {
            Form frm = null;

            try
            {
                if (img == null) return;

                //이하 윈도우 기본 연결프로그램으로 이미지 오픈
                //string file = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), ".jpg"));
                //if (File.Exists(file)) File.Delete(file);
                //FileStream fs = new FileStream(file, FileMode.CreateNew);
                //img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                //fs.Close();
                //System.Diagnostics.Process.Start(file);

                ImageEditView.ShowPreview(this, _param);
                ImageEditView.Current.LoadDocument(row, img);
                frm = ImageEditView.Current;
                editView = ImageEditView.Current;
                //Utils.SetForegroundWindow(ImageEditView.Current.Handle);
                Utils.BringToFront(ImageEditView.Current.Handle);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void SetChangeImage(DataGridViewRow row, Image img)
        {
            if (img == null) return;

            try
            {
                _bEvent = false;

                if (row == null)
                {
                    int rowIndex = dgv_dbox.Rows.Add();
                    DataGridViewRow dr = dgv_dbox.Rows[rowIndex];

                    string acdtPictFgName = cboAcdtPictFg.Text;
                    string acdtPictFg = Utils.GetComboSelectedValue(cboAcdtPictFg, "MinorSeq");
                    dr.Cells["imgAcdtPictImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);
                    dr.Cells["AcdtPictSeq"].Value = DBNull.Value;
                    dr.Cells["AcdtPictFgName"].Value = acdtPictFgName;
                    dr.Cells["AcdtPictFg"].Value = acdtPictFg;
                    dr.Cells["ObjSeq1"].Value = DBNull.Value;
                    //dr.Cells["ObjNm"].Value = Path.GetFileNameWithoutExtension(F_Info.Name);
                    dr.Cells["AcdtPictDt"].Value = DateTime.Now;
                    dr.Cells["AcdtPictFile"].Value = "";
                    dr.Cells["AcdtPictImage"].Value = ImageToString(img, System.Drawing.Imaging.ImageFormat.Bmp);

                    string workingTag = dr.Cells["WorkingTag"].Value + "";
                    if (workingTag == "")
                    {
                        dr.HeaderCell.Value = "A";
                        dr.Cells["WorkingTag"].Value = "A";
                    }
                    dgv_dbox.AutoResizeRow(rowIndex, DataGridViewAutoSizeRowMode.AllCells);
                }
                else
                {
                    row.Cells["imgAcdtPictImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);
                    row.Cells["AcdtPictImage"].Value = ImageToString(img, System.Drawing.Imaging.ImageFormat.Bmp);

                    string workingTag = row.Cells["WorkingTag"].Value + "";
                    if (workingTag == "")
                    {
                        row.HeaderCell.Value = "U";
                        row.Cells["WorkingTag"].Value = "U";
                    }
                }
            }
            catch { }
            finally
            {
                _bEvent = true;
            }
        }

        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(System.Windows.Forms.SortOrder sortOrder)
            {
                if (sortOrder == System.Windows.Forms.SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Last Name column.
                if (Utils.ToInt(DataGridViewRow2.Cells["AcdtPictSerl"].Value) == 0) return -1 * sortOrderModifier;
                if (Utils.ToInt(DataGridViewRow1.Cells["AcdtPictSerl"].Value) == 0) return 1 * sortOrderModifier;
                int CompareResult = Utils.ToInt(DataGridViewRow1.Cells["AcdtPictSerl"].Value) - Utils.ToInt(DataGridViewRow2.Cells["AcdtPictSerl"].Value);
                return CompareResult * sortOrderModifier;
            }
        }
    }
}
