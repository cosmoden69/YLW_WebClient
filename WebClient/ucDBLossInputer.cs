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
    public partial class ucDBLossInputer : UserControl, ISmplInputer
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool readOnlyMode = false;

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        public ucDBLossInputer()
        {
            InitializeComponent();

            Utils.DoubleBuffered(dgv_Prg, true);
            this.dgv_Prg.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

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
                userno1.SetFocus();
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

                strSql = "";
                strSql += " SELECT b.MinorSeq, b.MinorName ";
                strSql += " FROM   _TDAUMajor a WITH(NOLOCK) ";
                strSql += "        JOIN _TDAUMinor b WITH(NOLOCK) ON b.CompanySeq = A.CompanySeq AND b.MajorSeq = a.MajorSeq ";
                strSql += " WHERE  a.MajorName = 'ADJ_보험유지구분' ";
                strSql += " ORDER By b.MinorSort ";
                strSql += " FOR JSON PATH ";
                DataTable dt1 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                dt1.TableName = "ADJ_보험유지구분";
                userno1.Init_Set(dt1);
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
                if (dgv_Othr.CurrentCell is DataGridViewTextBoxCell && dgv_Othr.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_Prg.CurrentCell is DataGridViewTextBoxCell && dgv_Prg.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
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
            userno1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLossAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLossPan61.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan41.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLossAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan21.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLossPan61.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLossPan41.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            S2_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            S2_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            S2_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            S2_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            this.dgv_Prg.CellBeginEdit += Grid_CellBeginEdit;
            this.dgv_Prg.CellEndEdit += Grid_CellEndEdit;
            dgv_Othr.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Prg.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Othr.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Prg.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Othr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Prg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Othr.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Prg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Othr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Prg.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Othr.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Prg.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Othr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Prg.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Othr.DataError += Grid_DataError;
            dgv_Prg.DataError += Grid_DataError;

            dbLossAccident1.Userno1 = userno1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            if (readOnlyMode)
            {
                //ucDBLossPan11.SetReadOnlyMode(rdonly);

                userno1.SetReadOnlyMode(rdonly);
                dbLossAccident1.SetReadOnlyMode(rdonly);
                ucDBLossPan21.SetReadOnlyMode(rdonly);
                ucDBLossPan31.SetReadOnlyMode(rdonly);
                dbLossPan61.SetReadOnlyMode(rdonly);
                ucDBLossPan41.SetReadOnlyMode(rdonly);
                S2_LongCnts1.rtbDoc.ReadOnly = rdonly;
                S2_LongCnts2.rtbDoc.ReadOnly = rdonly;
            }
        }

        private void ClearAll()
        {
            //수정금지
            ucDBLossPan11.SetReadOnlyMode(true);

            userno1.Clear();
            dbLossAccident1.Clear();
            ucDBLossPan11.Clear();
            ucDBLossPan21.Clear();
            ucDBLossPan31.Clear();
            dbLossPan61.Clear();
            ucDBLossPan41.Clear();

            S2_LongCnts1.rtbDoc.Text = string.Empty;  // 청구내용
            S2_LongCnts2.rtbDoc.Text = string.Empty;  // 민원예방활동

            dgv_Othr.Rows.Clear();
            dgv_Othr.Height = 68;
            dgv_Prg.Rows.Clear();
            dgv_Prg.Height = 68;
        }

        private void XmlData_Read(DataSet yds)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearAll();

                string xml = yds.GetXml();

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersDBLoss.xsd";
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
                    ucDBLossPan11.Insured = drow["Insured"] + "";
                    ucDBLossPan11.AcdtNo = drow["AcdtNo"] + "";
                    ucDBLossPan11.SurvAsgnEmpName = drow["SurvAsgnEmpName"] + "";
                    ucDBLossPan11.IsrdRegno = drow["IsrdRegno1"] + "-" + drow["IsrdRegno2"];
                    ucDBLossPan11.InsurChrg = drow["InsurChrg"] + "";

                    custName = drow["InsurCo"] + "";
                }

                // 계약사항
                DataTable dt3 = pds.Tables["DataBlock3"];
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DataRow drow = dt3.Rows[i];
                        userno1.AddRow(drow);
                    }
                }
                if (!readOnlyMode)
                {
                    userno1.AddEmptyRow();
                }
                userno1.Sort();

                DataTable dt4 = pds.Tables["DataBlock4"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    DataRow drow = dt4.Rows[0];
                    ucDBLossPan21.AcdtDt = drow["AcdtDt"] + "";
                    ucDBLossPan21.ShrtCnts1 = drow["ShrtCnts1"] + "";
                    ucDBLossPan21.LongCnts1 = Utils.ConvertToRtf(drow["LongCnts1"]);
                    ucDBLossPan21.IsrdJobGrad = drow["IsrdJobGrad"] + "";
                    ucDBLossPan21.IsrdJob = drow["IsrdJob"] + "";
                    ucDBLossPan21.IsrdJobGradDmnd = drow["IsrdJobGradDmnd"] + "";
                    ucDBLossPan21.IsrdJobDmnd = drow["IsrdJobDmnd"] + "";
                }

                // 청구내용 및 조사결과
                DataTable dt5 = pds.Tables["DataBlock5"];
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    DataRow drow = dt5.Rows[0];
                    S2_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S2_LongCnts1"]);  // 청구내용
                    S2_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S2_LongCnts2"]);  // 조사자 의견 및 조사결과

                    ucDBLossPan31.S3_LongCnts1 = Utils.ConvertToRtf(drow["S3_LongCnts1"]);  // 청구내용
                    ucDBLossPan31.S3_ShrtCnts1 = drow["S3_ShrtCnts1"] + "";               // 조사자확인
                    ucDBLossPan31.S3_LongCnts2 = Utils.ConvertToRtf(drow["S3_LongCnts2"]);  // 조사내용
                }

                // 주요쟁점사항
                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        DataRow drow = dt6.Rows[i];
                        dbLossPan61.AddRow(drow);
                    }
                }
                if (!readOnlyMode)
                {
                    dbLossPan61.AddEmptyRow();
                }
                dbLossPan61.Sort();

                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DataRow drow = dt7.Rows[i];
                        DBLossAccidentA itm = dbLossAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!readOnlyMode)
                {
                    dbLossAccident1.AddEmptyRow();
                }
                SortAccident1();

                dgv_Othr.AllowUserToAddRows = false;
                // 타사 가입 사항
                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DataRow drow = dt8.Rows[i];
                        dgv_Othr.Rows.Add();
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCoNm"].Value = drow["OthInsurCoNm"];          // 보험회사
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurPrdt"].Value = drow["OthInsurPrdt"];          // 담보내용
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtDt"]);                // 보험
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtExprDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtExprDt"]);         // 기간
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurSurvOpni"].Value = drow["OthInsurSurvOpni"];    // 청구내용 및 결과
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCtrtSeq"].Value = drow["OthInsurCtrtSeq"];      // 순번
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_Othr.AllowUserToAddRows = true;
                }

                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    DataRow drow = dt9.Rows[0];
                    ucDBLossPan41.S5_LongCnts1 = Utils.ConvertToRtf(drow["S5_LongCnts1"]);
                    ucDBLossPan41.S5_LongCnts2 = Utils.ConvertToRtf(drow["S5_LongCnts2"]);
                    ucDBLossPan41.S5_LongCnts3 = Utils.ConvertToRtf(drow["S5_LongCnts3"]);
                    ucDBLossPan41.S5_ShrtCnts1 = drow["S5_ShrtCnts1"] + "";
                }

                dgv_Prg.AllowUserToAddRows = false;
                // 사고조사처리과정
                DataTable dt10 = pds.Tables["DataBlock10"];
                if (dt10 != null && dt10.Rows.Count > 0)
                {
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DataRow drow = dt10.Rows[i];
                        dgv_Prg.Rows.Add();
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtDt"].Value = Utils.ConvertToDateTime(drow["PrgMgtDt"]); // 진행일시
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtHed"].Value = drow["PrgMgtHed"];          // 주요내용
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);   // 진행내용
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetObj"].Value = drow["MetObj"];                // 면담자
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetObjRels"].Value = drow["MetObjRels"];         // 관계
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetMthd"].Value = drow["MetMthd"];              // 접촉방법
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtSeq"].Value = drow["PrgMgtSeq"];           // 순번
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_Prg.AllowUserToAddRows = true;
                }
                dgv_Prg.AutoResizeRows();
                GridHeightResize(dgv_Prg);
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

        private int GetScrollToColumnIndex(DataGridView grd, int currIndex)
        {
            int sumWid = grd.RowHeadersWidth;
            for (int ii = 0; ii <= currIndex; ii++)
            {
                sumWid += grd.Columns[ii].Width;
            }
            if (grd.Width < sumWid) return currIndex;
            return grd.FirstDisplayedScrollingColumnIndex;
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
                grd.FirstDisplayedScrollingColumnIndex = GetScrollToColumnIndex(grd, e.ColumnIndex);
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

        private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                DataGridViewCell cel = grd.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (readOnlyMode)
                {
                    if (cel is DataGridViewComboBoxCell) e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                grd.AutoResizeRow(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
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
            grd.Height = height + 45;
        }

        public void XmlData_Save(bool bNoMessage = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersDBLoss";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersDBLoss.xsd";
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
            dr["Insured"] = ucDBLossPan11.Insured;
            dr["AcdtNo"] = ucDBLossPan11.AcdtNo;
            dr["SurvAsgnEmpName"] = ucDBLossPan11.SurvAsgnEmpName;
            dr["IsrdRegno1"] = Utils.GetP(ucDBLossPan11.IsrdRegno, "-", 1);
            dr["IsrdRegno2"] = Utils.GetP(ucDBLossPan11.IsrdRegno, "-", 2);
            dr["InsurChrg"] = ucDBLossPan11.InsurChrg;
            dr["InsurCo"] = custName;

            // 계약사항
            dt = ds.Tables["DataBlock3"];
            for (int i = 0; i < userno1.Rows.Count; i++)
            {
                if (userno1.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = userno1.Rows[i].InsurPrdt;             // 상품명
                dr["InsurNo"] = userno1.Rows[i].InsurNo;                 // 증권번호
                dr["CtrtDt"] = userno1.Rows[i].CtrtDt;                   // 보험시기
                dr["CtrtExprDt"] = userno1.Rows[i].CtrtExprDt;            // 보험종기
                dr["Insurant"] = userno1.Rows[i].Insurant;               // 계약자명
                dr["CtrtStts"] = userno1.Rows[i].CtrtStts;               // 계약상태
                dr["CtrtSttsDt"] = userno1.Rows[i].CtrtSttsDt;           // 계약상태일자
                dr["IsrtRegno1"] = userno1.Rows[i].IsrtRegno1;
                dr["IsrtRegno2"] = userno1.Rows[i].IsrtRegno2;
                dr["IsrtTel"] = userno1.Rows[i].IsrtTel;
                dr["Insured"] = userno1.Rows[i].Insured;
                dr["IsrdRegno1"] = userno1.Rows[i].IsrdRegno1;
                dr["IsrdRegno2"] = userno1.Rows[i].IsrdRegno2;
                dr["IsrdTel"] = userno1.Rows[i].IsrdTel;
                if (userno1.Rows[i].IsrdAddressSeq == "") dr["IsrdAddressSeq"] = DBNull.Value;
                else dr["IsrdAddressSeq"] = userno1.Rows[i].IsrdAddressSeq;
                dr["IsrdAddressName"] = userno1.Rows[i].IsrdAddressName;
                dr["IsrdJob"] = userno1.Rows[i].IsrdJob;
                dr["IsrdJobGrad"] = userno1.Rows[i].IsrdJobGrad;
                dr["IsrdJobDmnd"] = userno1.Rows[i].IsrdJobDmnd;
                dr["IsrdJobGradDmnd"] = userno1.Rows[i].IsrdJobGradDmnd;
                dr["IsrdJobNow"] = userno1.Rows[i].IsrdJobNow;
                dr["IsrdJobGradNow"] = userno1.Rows[i].IsrdJobGradNow;
                dr["Bnfc"] = userno1.Rows[i].Bnfc;
            }

            // 손해사항
            dt = ds.Tables["DataBlock4"];
            dr = dt.Rows.Add();
            dr["AcdtDt"] = ucDBLossPan21.AcdtDt;                    // 사고일자
            dr["ShrtCnts1"] = ucDBLossPan21.ShrtCnts1;               // 사고유형
            dr["LongCnts1"] = ucDBLossPan21.LongCnts1;               // 진단명
            dr["IsrdJobGrad"] = ucDBLossPan21.IsrdJobGrad;            // 가입시 직업급수
            dr["IsrdJob"] = ucDBLossPan21.IsrdJob;                   // 가입시 직업명
            dr["IsrdJobGradDmnd"] = ucDBLossPan21.IsrdJobGradDmnd;     // 사고시 직업급수
            dr["IsrdJobDmnd"] = ucDBLossPan21.IsrdJobDmnd;            // 사고시 직업명

            // 청구내용 및 조사결과
            dt = ds.Tables["DataBlock5"];
            dr = dt.Rows.Add();
            dr["S2_LongCnts1"] = S2_LongCnts1.rtbDoc.Rtf;  // 청구내용
            dr["S2_LongCnts2"] = S2_LongCnts2.rtbDoc.Rtf;  // 민원예방활동                 
            dr["S3_LongCnts1"] = ucDBLossPan31.S3_LongCnts1;  // 청구내용
            dr["S3_ShrtCnts1"] = ucDBLossPan31.S3_ShrtCnts1;  // 조사자확인
            dr["S3_LongCnts2"] = ucDBLossPan31.S3_LongCnts2;  // 조사내용

            // 주요쟁점사항
            dt = ds.Tables["DataBlock6"];
            for (int i = 0; i < dbLossPan61.Rows.Count; i++)
            {
                if (dbLossPan61.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["ShrtCnts1"] = dbLossPan61.Rows[i].ShrtCnts1;             // 조사쟁점 대(L1)
                dr["ShrtCnts2"] = dbLossPan61.Rows[i].ShrtCnts2;             // 조사쟁점 대(L2)
                dr["ShrtCnts3"] = dbLossPan61.Rows[i].ShrtCnts3;             // 조사자 확인
                dr["LongCnts1"] = dbLossPan61.Rows[i].LongCnts1;             // 조사쟁점 대(L1)
                dr["LongCnts2"] = dbLossPan61.Rows[i].LongCnts2;             // 조사내용
            }

            // 조사자 일자별 확인사항
            dt = ds.Tables["DataBlock7"];
            for (int i = 0; i < dbLossAccident1.Rows.Count; i++)
            {
                if (dbLossAccident1.Rows[i].IsNewRow) continue;
                if (dbLossAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = dbLossAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = dbLossAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureCnts"] = dbLossAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = dbLossAccident1.Rows[i].VstHosp;                // 의료기관
            }

            // 타사가입사항
            dt = ds.Tables["DataBlock8"];
            for (int i = 0; i < dgv_Othr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInsurCoNm"] = dgv_Othr.Rows[i].Cells["othrOthInsurCoNm"].Value;
                dr["OthInsurPrdt"] = dgv_Othr.Rows[i].Cells["othrOthInsurPrdt"].Value;
                dr["OthCtrtDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtDt"].Value, "yyyyMMdd");
                dr["OthCtrtExprDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtExprDt"].Value, "yyyyMMdd");
                dr["OthInsurSurvOpni"] = dgv_Othr.Rows[i].Cells["othrOthInsurSurvOpni"].Value;
                dr["OthInsurCtrtSeq"] = Utils.ToInt(dgv_Othr.Rows[i].Cells["othrOthInsurCtrtSeq"].Value);
            }

            dt = ds.Tables["DataBlock9"];
            dr = dt.Rows.Add();

            dr["S5_LongCnts1"] = ucDBLossPan41.S5_LongCnts1;
            dr["S5_LongCnts2"] = ucDBLossPan41.S5_LongCnts2;
            dr["S5_LongCnts3"] = ucDBLossPan41.S5_LongCnts3;
            dr["S5_ShrtCnts1"] = ucDBLossPan41.S5_ShrtCnts1;

            // 사고조사처리과정
            dt = ds.Tables["DataBlock10"];
            for (int i = 0; i < dgv_Prg.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["PrgMgtDt"] = Utils.DateFormat(dgv_Prg.Rows[i].Cells["prgPrgMgtDt"].Value, "yyyyMMdd");
                dr["PrgMgtHed"] = dgv_Prg.Rows[i].Cells["prgPrgMgtHed"].Value;
                dr["SurvGuideCnts"] = dgv_Prg.Rows[i].Cells["prgSurvGuideCnts"].Value;
                dr["MetObj"] = dgv_Prg.Rows[i].Cells["prgMetObj"].Value;
                dr["MetObjRels"] = dgv_Prg.Rows[i].Cells["prgMetObjRels"].Value;
                dr["MetMthd"] = dgv_Prg.Rows[i].Cells["prgMetMthd"].Value;
                dr["PrgMgtSeq"] = Utils.ToInt(dgv_Prg.Rows[i].Cells["prgPrgMgtSeq"].Value);
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
                for (int ii = dbLossAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (dbLossAccident1.Rows[ii].IsNewRow) continue;
                    if (dbLossAccident1.Rows[ii].Gubun == "1") dbLossAccident1.RemoveRow(dbLossAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < userno1.Rows.Count; ii++)
                {
                    if (userno1.Rows[ii].IsNewRow) continue;
                    DBLossAccidentA itm = dbLossAccident1.AddRow("", "계약일", userno1.Rows[ii].CtrtDt, userno1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                dbLossAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < dbLossAccident1.Rows.Count; ii++)
                {
                    if (dbLossAccident1.Rows[ii].IsNewRow) continue;
                    if (dbLossAccident1.Rows[ii].Gubun == "1") continue;
                    dbLossAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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