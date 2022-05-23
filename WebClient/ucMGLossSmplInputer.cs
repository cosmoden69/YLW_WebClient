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
    public partial class ucMGLossSmplInputer : UserControl, ISmplInputer
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool readOnlyMode = false;

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        public ucMGLossSmplInputer()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.Main_Load);
        }

        public bool LoadDocument(ReportParam p)
        {
            try
            {
                param = p;

                Init_Set();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Reload(DataSet pds)
        {
            try
            {
                flp_Panel_1.Focus();
                XmlData_Read(pds);
                flp_Panel_1.VerticalScroll.Value = 0;
                ucMGLossPan11.SetFocus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init_Set()
        {
            try
            { 
                string strSql = "";

                strSql = "";
                strSql += " SELECT B.AcptDt ";
                strSql += " FROM   _TAdjSLAcptList AS B WITH(NOLOCK) ";
                strSql += " WHERE  B.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    B.AcptMgmtSeq = '" + param.AcptMgmtSeq + "' ";
                strSql += " AND    B.ReSurvAsgnNo = '" + param.ReSurvAsgnNo + "' ";
                strSql += " FOR JSON PATH ";
                DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                acptDt = "";
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    acptDt = ds.Tables[0].Rows[0]["AcptDt"] + "";
                }

                strSql = "";
                strSql += " SELECT U.UserId, U.UserSeq, A.EmpSeq, A.EmpName, ISNULL(D.UmJdSeq,0) AS UmJdSeq, ISNULL(E.MinorName,'') AS MinorName, ISNULL(C.DeptSeq,0) AS DeptSeq, ISNULL(C.DeptName,'') AS DeptName ";
                strSql += " FROM   _TCAUser AS U WITH(NOLOCK) ";
                strSql += "        JOIN _TDAEmp AS A WITH(NOLOCK) ON A.CompanySeq = U.CompanySeq ";
                strSql += "                                      AND A.EmpSeq     = U.EmpSeq ";
                strSql += "        LEFT JOIN _TDADept AS C WITH(NOLOCK) ON C.CompanySeq = A.CompanySeq ";
                strSql += "                                            AND C.DeptSeq    = A.DeptSeq ";
                strSql += "        LEFT JOIN _THRAdmOrdEmp AS D WITH(NOLOCK) ON D.CompanySeq = A.CompanySeq ";
                strSql += "                                                 AND D.EmpSeq     = A.EmpSeq ";
                strSql += "                                                 AND D.IsLast     = 1 /* 가장 마지막 인사발령만 확인 */ ";
                strSql += "        LEFT JOIN _TDAUMinor AS E WITH(NOLOCK) ON E.CompanySeq = D.CompanySeq ";
                strSql += "                                              AND E.MinorSeq   = D.UMJdSeq  /* 직책 */ ";
                strSql += " WHERE  U.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    U.UserId     = '" + param.UserID + "' ";
                strSql += " FOR JSON PATH ";
                ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                this.SmplAuth = false;
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string umjdseq = ds.Tables[0].Rows[0]["UmJdSeq"] + "";
                    string deptseq = ds.Tables[0].Rows[0]["DeptSeq"] + "";
                    string deptnm = ds.Tables[0].Rows[0]["DeptName"] + "";
                    //사업부장이거나 지원부서 소속이면 사례저장 권한
                    //if (umjdseq == "3053004" || Utils.GetPos("16,19,40,55,61,63,120", ",", deptseq) > 0) this.SmplAuth = true;
                    if (umjdseq == "3053004" || deptnm.Contains("지원") ) this.SmplAuth = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetCombo(ComboBox cboObj, DataTable pdt, string strValueMember, string strDisplayMember, bool bAddnull)
        {
            try
            {
                if (cboObj.DataSource != null) cboObj.DataSource = null;
                cboObj.Items.Clear();

                if (pdt == null || pdt.Rows.Count < 1)
                {
                    return;
                }
                if (bAddnull)
                {
                    DataRow drTmp = pdt.NewRow();
                    drTmp[strValueMember] = DBNull.Value;
                    drTmp[strDisplayMember] = "";
                    pdt.Rows.InsertAt(drTmp, 0);
                }
                cboObj.DataSource = pdt;
                cboObj.ValueMember = strValueMember;
                cboObj.DisplayMember = strDisplayMember;
                cboObj.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                if (dgv_Etc.CurrentCell is DataGridViewTextBoxCell && dgv_Etc.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_file.CurrentCell is DataGridViewTextBoxCell && dgv_file.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else
                {
                    if (enter_ == "Y")
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                    else
                    {
                        SendKeys.Send("^{ENTER}");
                        return true;
                    }
                }
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.btnSort.Click += BtnSort_Click;
            ucMGLossPan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossContract1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossSmplAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossContract1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossSmplAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS101_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS101_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            dgv_file.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Etc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_file.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Etc.EditingControlShowing += Grid_EditingControlShowing;
            dgv_file.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Etc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_file.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Etc.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_file.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Etc.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_file.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Etc.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_file.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Etc.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_file.DataError += Grid_DataError;
            dgv_Etc.DataError += Grid_DataError;

            mgLossSmplAccident1.Userno1 = mgLossContract1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            if (readOnlyMode)
            {
                ucMGLossPan11.SetReadOnlyMode(rdonly);
                mgLossContract1.SetReadOnlyMode(rdonly);
                mgLossSmplAccident1.SetReadOnlyMode(rdonly);
                txtS101_LongCnts1.SetReadOnly(rdonly);
            }
        }

        private void ClearAll()
        {
            ucMGLossPan11.Clear();
            mgLossContract1.Clear();
            mgLossSmplAccident1.Clear();

            txtS101_LongCnts1.rtbDoc.Rtf = "";

            dgv_file.Rows.Clear();
            dgv_file.Height = 68;
            dgv_Etc.Rows.Clear();
            dgv_Etc.Height = 68;
        }

        private void XmlData_Read(DataSet yds)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearAll();

                string xml = yds.GetXml();

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMGLossSmpl.xsd";
                DataSet pds = new DataSet();
                pds.ReadXml(s_CAA_XSD);

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    pds.ReadXml(xmlReader);
                }

                DataTable dt2 = pds.Tables["DataBlock2"];
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataRow drow = dt2.Rows[0];
                    ucMGLossPan11.AcptDt = drow["AcptDt"] + "";
                    ucMGLossPan11.FldRptSbmsDt = drow["FldRptSbmsDt"] + "";
                    ucMGLossPan11.MidRptSbmsDt = drow["MidRptSbmsDt"] + "";
                    ucMGLossPan11.DlyRprtDt = drow["DlyRprtDt"] + "";
                    ucMGLossPan11.LasRptSbmsDt = drow["LasRptSbmsDt"] + "";

                    custName = drow["InsurCo"] + "";
                }

                DataTable dt3 = pds.Tables["DataBlock3"];
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    DataRow drow = dt3.Rows[0];
                    ucMGLossPan11.CmplPnt1 = drow["CmplPnt1"] + "";
                    ucMGLossPan11.CmplPnt2 = drow["CmplPnt2"] + "";
                    ucMGLossPan11.CmplPnt3 = drow["CmplPnt3"] + "";
                    ucMGLossPan11.CmplPnt4 = drow["CmplPnt4"] + "";
                    ucMGLossPan11.CmplPnt5 = drow["CmplPnt5"] + "";
                }

                // 계약사항
                DataTable dt4 = pds.Tables["DataBlock4"];
                DataTable dt5 = pds.Tables["DataBlock5"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DataRow drow = dt4.Rows[i];
                        string InsurNo = drow["InsurNo"] + "";
                        DataRow[] drs = dt5.Select("InsurNo = '" + InsurNo + "' ");
                        mgLossContract1.AddRow(drow, drs);
                    }
                }
                mgLossContract1.Sort();

                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        DataRow drow = dt6.Rows[i];
                        MGLossSmplAccidentA itm = mgLossSmplAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!readOnlyMode)
                {
                    mgLossSmplAccident1.AddEmptyRow();
                }
                SortAccident1();

                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    DataRow drow = dt7.Rows[0];
                    txtS101_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S101_LongCnts1"]);
                }

                dgv_file.AllowUserToAddRows = false;
                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DataRow drow = dt8.Rows[i];
                        dgv_file.Rows.Add();
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileSavSerl"].Value = drow["FileSavSerl"]; // 번호
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileSavDt"].Value = Utils.ConvertToDateTime(drow["FileSavDt"]);    // 일자
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileCnts"].Value = drow["FileCnts"];      // 첨부내용
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileCntsCn"].Value = drow["FileCntsCn"];  // 매수
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileRels"].Value = drow["FileRels"];      // 소재지                     
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileRmk"].Value = drow["FileRmk"];        // 비고                     
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_file.AllowUserToAddRows = true;
                }

                dgv_Etc.AllowUserToAddRows = false;
                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DataRow drow = dt9.Rows[i];
                        dgv_Etc.Rows.Add();
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcOthInfoSeq"].Value = drow["OthInfoSeq"];    // 순번
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcLongCnts1"].Value = drow["LongCnts1"];      // 소재지
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcLongCnts2"].Value = drow["LongCnts2"];      // 병원명
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts1"].Value = drow["ShrtCnts1"];      // 연락처
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts2"].Value = drow["ShrtCnts2"];      // 피보험자내원여부
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts3"].Value = drow["ShrtCnts3"];      // 비고
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcOthInfoGrp"].Value = drow["OthInfoGrp"];    // 그룹
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_Etc.AllowUserToAddRows = true;
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

        private void Mouse_Wheel(object sesender, MouseEventArgs e)
        {
            try
            {
                if (sesender is DevComponents.DotNetBar.Controls.DataGridViewX)
                {
                    DataGridViewCell currentCell = (sesender as DevComponents.DotNetBar.Controls.DataGridViewX).CurrentCell;
                    if (currentCell != null && currentCell.EditType != null)
                    {
                        if (currentCell.EditType.Name == "DataGridViewDateTimeInputEditingControl")
                        {
                            ((HandledMouseEventArgs)e).Handled = true;
                            return;
                        }
                    }
                }
                if ((e.Delta / 120) > 0)
                {
                    if (flp_Panel_1.VerticalScroll.Value > 80)
                        flp_Panel_1.VerticalScroll.Value = flp_Panel_1.VerticalScroll.Value - 80;
                    else
                        flp_Panel_1.VerticalScroll.Value = 0;
                }
                else
                {
                    flp_Panel_1.VerticalScroll.Value = flp_Panel_1.VerticalScroll.Value + 80;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        

        private void Text_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox t_box = (TextBox)sender;

            tpoint = 0;
            h_size = t_box.Height;
            enter_ = string.Empty;

            foreach (Control ctl in flp_Panel_1.Controls)
            {
                if (t_box.Name == ctl.Name)
                    break;

                tpoint = tpoint + ctl.Height;
            }

            flp_Panel_1.Focus();
        }

        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (readOnlyMode)
                {
                    if (e.Control is TextBox)
                        ((TextBox)e.Control).ReadOnly = true;
                    if (e.Control is ComboBox)
                        ((ComboBox)e.Control).Enabled = false;
                    if (e.Control is CheckBox)
                        ((CheckBox)e.Control).Enabled = false;
                    if (e.Control is DateTimePicker)
                        ((DateTimePicker)e.Control).Enabled = false;
                    if (e.Control is CustomDataGridViewTextBoxEditingControl)
                        ((CustomDataGridViewTextBoxEditingControl)e.Control).IsInputReadOnly = true;
                    if (e.Control is CustomDataGridViewDateTimeInputEditingControl)
                        ((CustomDataGridViewDateTimeInputEditingControl)e.Control).IsInputReadOnly = true;
                    if (e.Control is DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputEditingControl)
                        ((DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputEditingControl)e.Control).IsInputReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void Grid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                grd.CurrentCell.Style.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                DataGridView grd = (DataGridView)sender;
                if (grd.CurrentCell.ReadOnly)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                else
                {
                    grd.CurrentCell.Style.BackColor = Color.FromArgb(255, 255, 136);
                }
                if (grd.CurrentCell.EditType == typeof(CustomDataGridViewTextBoxEditingControl))
                {
                    grd.BeginEdit(false);
                    ((TextBox)grd.EditingControl).SelectionStart = ((TextBox)grd.EditingControl).TextLength;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                flp_Panel_1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_RowAdd(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                int row_num = e.RowIndex;
                dgv.AutoResizeRow(row_num, DataGridViewAutoSizeRowMode.AllCells);
                dgv.Height = dgv.Height + dgv.Rows[row_num].Height;
            }
            catch { }

        }

        private void Grid_RowDel(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                dgv.Height = dgv.Height - dgv.Rows[e.RowIndex].Height;
            }
            catch { }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (readOnlyMode) return;
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
                                dgv.Rows.Remove(dgv.Rows[e.RowIndex]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GridHeightResize(DataGridView grd)
        {
            int height = grd.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in grd.Rows)
            {
                height += dr.Height; // Row height.
            }
            grd.Height = height + 20;
        }

        public void XmlData_Save(bool bNoMessage = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersMGLossSmpl";
                security.methodId  = "Save";
                security.companySeq = param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = param.UserID;

                DataSet ds = GetSaveData();
                
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }
                foreach (DataTable dti in yds.Tables)
                {
                    if (!dti.Columns.Contains("Status")) continue;
                    if (!dti.Columns.Contains("Result")) continue;
                    if (dti.Rows.Count > 0 && Convert.ToInt32(dti.Rows[0]["Status"]) != 0)   //Status != 0 이면 저장안됨
                    {
                        MessageBox.Show(dti.Rows[0]["Result"] + "");
                        return;
                    }
                }
                if (!bNoMessage) MessageBox.Show("저장 완료");
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

        public DataSet GetSaveData()
        {
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMGLossSmpl.xsd";
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(s_CAA_XSD);

            DataTable dt = ds.Tables.Add("DataBlock1");

            dt.Columns.Add("AcptMgmtSeq");
            dt.Columns.Add("ReSurvAsgnNo");
            dt.Columns.Add("DcmgDocNo");

            dt.Clear();

            DataRow dr = dt.Rows.Add();
            dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
            dr["DcmgDocNo"] = "";

            dt = ds.Tables["DataBlock2"];
            dr = dt.Rows.Add();
            dr["AcptDt"] = ucMGLossPan11.AcptDt;
            dr["FldRptSbmsDt"] = ucMGLossPan11.FldRptSbmsDt;
            dr["MidRptSbmsDt"] = ucMGLossPan11.MidRptSbmsDt;
            dr["DlyRprtDt"] = ucMGLossPan11.DlyRprtDt;
            dr["LasRptSbmsDt"] = ucMGLossPan11.LasRptSbmsDt;
            dr["InsurCo"] = custName;

            dt = ds.Tables["DataBlock3"];
            dr = dt.Rows.Add();
            dr["CmplPnt1"] = ucMGLossPan11.CmplPnt1;                // 민원지수1
            dr["CmplPnt2"] = ucMGLossPan11.CmplPnt2;                // 민원지수2
            dr["CmplPnt3"] = ucMGLossPan11.CmplPnt3;                // 민원지수3
            dr["CmplPnt4"] = ucMGLossPan11.CmplPnt4;                // 민원지수4
            dr["CmplPnt5"] = ucMGLossPan11.CmplPnt5;                // 민원지수5

            // 계약사항
            dt = ds.Tables["DataBlock4"];
            DataTable dt5 = ds.Tables["DataBlock5"];
            for (int i = 0; i < mgLossContract1.Rows.Count; i++)
            {
                if (mgLossContract1.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = mgLossContract1.Rows[i].InsurPrdt;             // 보험종목
                dr["InsurNo"] = mgLossContract1.Rows[i].InsurNo;                 // 증권번호
                dr["CtrtDt"] = mgLossContract1.Rows[i].CtrtDt;                   // 보험시기
                dr["CtrtExprDt"] = mgLossContract1.Rows[i].CtrtExprDt;            // 보험종기
                dr["IsrdJob"] = mgLossContract1.Rows[i].IsrdJob;                 // 직업
                dr["Insurant"] = mgLossContract1.Rows[i].Insurant;               // 계약자명
                dr["Insured"] = mgLossContract1.Rows[i].Insured;                 // 피보험자
                dr["IsrdTel"] = mgLossContract1.Rows[i].IsrdTel;                 // 연락처
                dr["IsrdAddressName"] = mgLossContract1.Rows[i].IsrdAddressName;   // 주소

                dr["CtrtStts"] = mgLossContract1.Rows[i].CtrtStts;
                dr["CtrtSttsDt"] = mgLossContract1.Rows[i].CtrtSttsDt;
                dr["IsrtRegno1"] = mgLossContract1.Rows[i].IsrtRegno1;
                dr["IsrtRegno2"] = mgLossContract1.Rows[i].IsrtRegno2;
                dr["IsrtTel"] = mgLossContract1.Rows[i].IsrtTel;
                dr["IsrdRegno1"] = mgLossContract1.Rows[i].IsrdRegno1;
                dr["IsrdRegno2"] = mgLossContract1.Rows[i].IsrdRegno2;
                if (mgLossContract1.Rows[i].IsrdAddressSeq == "") dr["IsrdAddressSeq"] = DBNull.Value;
                else dr["IsrdAddressSeq"] = mgLossContract1.Rows[i].IsrdAddressSeq;
                dr["IsrdJobGrad"] = mgLossContract1.Rows[i].IsrdJobGrad;
                dr["IsrdJobDmnd"] = mgLossContract1.Rows[i].IsrdJobDmnd;
                dr["IsrdJobGradDmnd"] = mgLossContract1.Rows[i].IsrdJobGradDmnd;
                dr["IsrdJobNow"] = mgLossContract1.Rows[i].IsrdJobNow;
                dr["IsrdJobGradNow"] = mgLossContract1.Rows[i].IsrdJobGradNow;
                dr["Bnfc"] = mgLossContract1.Rows[i].Bnfc;
                miMGLossSmplContractBRows rows = mgLossContract1.Rows[i].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    if (rows[ii].IsNewRow) continue;
                    DataRow dr5 = dt5.Rows.Add();
                    dr5["CltrCnts"] = rows[ii].CltrCnts;                        // 담보내용
                    dr5["InsurRegsAmt"] = Utils.ToDecimal(rows[ii].InsurRegsAmt);  // 보험가입금액
                    dr5["InsurNo"] = dr["InsurNo"];                            // 증권번호
                }
            }

            // 손해액 범위 조사
            dt = ds.Tables["DataBlock6"];
            for (int i = 0; i < mgLossSmplAccident1.Rows.Count; i++)
            {
                if (mgLossSmplAccident1.Rows[i].IsNewRow) continue;
                if (mgLossSmplAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = mgLossSmplAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = mgLossSmplAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureCnts"] = mgLossSmplAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = mgLossSmplAccident1.Rows[i].VstHosp;                // 의료기관
            }

            dt = ds.Tables["DataBlock7"];
            dr = dt.Rows.Add();
            dr["S101_LongCnts1"] = txtS101_LongCnts1.rtbDoc.Rtf;

            // 별첨자료
            dt = ds.Tables["DataBlock8"];
            for (int i = 0; i < dgv_file.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["FileSavSerl"] = i + 1;
                dr["FileSavDt"] = Utils.DateFormat(dgv_file.Rows[i].Cells["fileFileSavDt"].Value, "yyyyMMdd");
                dr["FileCnts"] = dgv_file.Rows[i].Cells["fileFileCnts"].Value;
                dr["FileCntsCn"] = Utils.ToInt(dgv_file.Rows[i].Cells["fileFileCntsCn"].Value);
                dr["FileRels"] = dgv_file.Rows[i].Cells["fileFileRels"].Value;
                dr["FileRmk"] = dgv_file.Rows[i].Cells["fileFileRmk"].Value;
            }

            // 조사 일정 요약표
            dt = ds.Tables["DataBlock9"];
            for (int i = 0; i < dgv_Etc.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInfoSeq"] = Utils.ToInt(dgv_Etc.Rows[i].Cells["etcOthInfoSeq"].Value);
                dr["LongCnts1"] = dgv_Etc.Rows[i].Cells["etcLongCnts1"].Value;
                dr["LongCnts2"] = dgv_Etc.Rows[i].Cells["etcLongCnts2"].Value;
                dr["ShrtCnts1"] = dgv_Etc.Rows[i].Cells["etcShrtCnts1"].Value;
                dr["ShrtCnts2"] = dgv_Etc.Rows[i].Cells["etcShrtCnts2"].Value;
                dr["ShrtCnts3"] = dgv_Etc.Rows[i].Cells["etcShrtCnts3"].Value;
                dr["OthInfoGrp"] = dgv_Etc.Rows[i].Cells["etcOthInfoGrp"].Value;
            }

            return ds;
        }

        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            SortAccident1();
        }

        public void SortAccident1()
        {
            try
            {
                for (int ii = mgLossSmplAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (mgLossSmplAccident1.Rows[ii].IsNewRow) continue;
                    if (mgLossSmplAccident1.Rows[ii].Gubun == "1") mgLossSmplAccident1.RemoveRow(mgLossSmplAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < mgLossContract1.Rows.Count; ii++)
                {
                    if (mgLossContract1.Rows[ii].IsNewRow) continue;
                    MGLossSmplAccidentA itm = mgLossSmplAccident1.AddRow("", "계약일", mgLossContract1.Rows[ii].CtrtDt, mgLossContract1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                mgLossSmplAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < mgLossSmplAccident1.Rows.Count; ii++)
                {
                    if (mgLossSmplAccident1.Rows[ii].IsNewRow) continue;
                    if (mgLossSmplAccident1.Rows[ii].Gubun == "1") continue;
                    mgLossSmplAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Last Name column.
                int CompareResult = System.String.Compare(
                    Utils.DateFormat(DataGridViewRow1.Cells[0].Value, "yyyyMMdd"),
                    Utils.DateFormat(DataGridViewRow2.Cells[0].Value, "yyyyMMdd"));

                // If the Last Names are equal, sort based on the First Name.
                if (CompareResult == 0)
                {
                    CompareResult = Utils.ToInt(DataGridViewRow1.Cells[1].Value) - Utils.ToInt(DataGridViewRow2.Cells[1].Value);
                }
                return CompareResult * sortOrderModifier;
            }
        }
    }
}