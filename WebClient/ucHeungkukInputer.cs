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
    public partial class ucHeungkukInputer : UserControl, ISmplInputer
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool readOnlyMode = false;

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        public ucHeungkukInputer()
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
                ucHeungkukPan11.SetFocus();
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
            this.btnSort.Click += BtnSort_Click;
            heungkukContract1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            heungkukAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            heungkukAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan21.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHeungkukPan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS301_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS301_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS301_LongCnts3.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS401_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS501_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS501_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS301_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS301_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS301_LongCnts3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS401_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS501_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS501_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
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

            heungkukAccident1.Userno1 = heungkukContract1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            if (readOnlyMode)
            {
                heungkukContract1.SetReadOnlyMode(rdonly);
                heungkukAccident1.SetReadOnlyMode(rdonly);
                ucHeungkukPan11.SetReadOnlyMode(rdonly);
                ucHeungkukPan21.SetReadOnlyMode(rdonly);
                ucHeungkukPan31.SetReadOnlyMode(rdonly);
                txtS301_LongCnts1.rtbDoc.ReadOnly = rdonly;
                txtS301_LongCnts2.rtbDoc.ReadOnly = rdonly;
                txtS301_LongCnts3.rtbDoc.ReadOnly = rdonly;
                txtS401_ShrtCnts1.ReadOnly = rdonly;
                txtS401_ShrtCnts2.ReadOnly = rdonly;
                txtS401_LongCnts1.rtbDoc.ReadOnly = rdonly;
                txtS501_LongCnts1.rtbDoc.ReadOnly = rdonly;
                txtS501_LongCnts2.rtbDoc.ReadOnly = rdonly;
            }
        }

        private void ClearAll()
        {
            heungkukContract1.Clear();
            heungkukAccident1.Clear();
            ucHeungkukPan11.Clear();
            ucHeungkukPan21.Clear();
            ucHeungkukPan31.Clear();

            txtS301_LongCnts1.rtbDoc.Rtf = "";
            txtS301_LongCnts2.rtbDoc.Rtf = "";
            txtS301_LongCnts3.rtbDoc.Rtf = "";
            txtS401_ShrtCnts1.Text = "";
            txtS401_ShrtCnts2.Text = "";
            txtS401_LongCnts1.rtbDoc.Rtf = "";
            txtS501_LongCnts1.rtbDoc.Rtf = "";
            txtS501_LongCnts2.rtbDoc.Rtf = "";

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

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersHeungkuk.xsd";
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
                    custName = drow["InsurCo"] + "";
                }

                DataTable dt3 = pds.Tables["DataBlock3"];
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    DataRow drow = dt3.Rows[0];
                    ucHeungkukPan11.ClntTdncEvat = drow["ClntTdncEvat"] + "";
                    ucHeungkukPan11.UnusDtil = drow["UnusDtil"] + "";
                    ucHeungkukPan11.CmplPnt1 = drow["CmplPnt1"] + "";
                    ucHeungkukPan11.CmplPnt2 = drow["CmplPnt2"] + "";
                    ucHeungkukPan11.CmplPnt3 = drow["CmplPnt3"] + "";
                    ucHeungkukPan11.CmplPnt4 = drow["CmplPnt4"] + "";
                    ucHeungkukPan11.CmplPnt5 = drow["CmplPnt5"] + "";
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
                        heungkukContract1.AddRow(drow, drs);
                    }
                }
                heungkukContract1.Sort();

                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    DataRow drow = dt6.Rows[0];
                    ucHeungkukPan21.S101_LongCnts1 = Utils.ConvertToRtf(drow["S101_LongCnts1"]);
                    ucHeungkukPan21.S101_ShrtCnts1 = drow["S101_ShrtCnts1"] + "";
                    ucHeungkukPan21.S101_ShrtCnts2 = drow["S101_ShrtCnts2"] + "";
                    ucHeungkukPan21.S101_LongCnts2 = Utils.ConvertToRtf(drow["S101_LongCnts2"]);
                    ucHeungkukPan21.S102_ShrtCnts1 = drow["S102_ShrtCnts1"] + "";
                    ucHeungkukPan21.S102_ShrtCnts2 = drow["S102_ShrtCnts2"] + "";
                    ucHeungkukPan21.S102_LongCnts1 = Utils.ConvertToRtf(drow["S102_LongCnts1"]);
                    ucHeungkukPan21.S102_LongCnts2 = drow["S102_LongCnts2"] + "";
                    ucHeungkukPan21.S102_ShrtCnts3 = drow["S102_ShrtCnts3"] + "";
                    ucHeungkukPan21.S133_ShrtCnts1 = drow["S133_ShrtCnts1"] + "";
                    ucHeungkukPan21.S133_ShrtCnts2 = drow["S133_ShrtCnts2"] + "";

                    ucHeungkukPan31.S201_ShrtCnts1 = drow["S201_ShrtCnts1"] + "";
                    ucHeungkukPan31.S201_LongCnts1 = Utils.ConvertToRtf(drow["S201_LongCnts1"]);
                    ucHeungkukPan31.S201_LongCnts2 = Utils.ConvertToRtf(drow["S201_LongCnts2"]);
                }

                dgv_Othr.AllowUserToAddRows = false;
                // 타사 가입 사항
                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DataRow drow = dt7.Rows[i];
                        dgv_Othr.Rows.Add();
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCoNm"].Value = drow["OthInsurCoNm"];          // 보험회사
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurPrdt"].Value = drow["OthInsurPrdt"];       // 담보내용
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

                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    DataRow drow = dt8.Rows[0];
                    txtS301_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S301_LongCnts1"]);
                    txtS301_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S301_LongCnts2"]);
                    txtS301_LongCnts3.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S301_LongCnts3"]);
                    txtS401_ShrtCnts1.Text = drow["S401_ShrtCnts1"] + "";
                    txtS401_ShrtCnts2.Text = drow["S401_ShrtCnts2"] + "";
                    txtS401_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S401_LongCnts1"]);
                    txtS501_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S501_LongCnts1"]);
                    txtS501_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S501_LongCnts2"]);
                }

                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DataRow drow = dt9.Rows[i];
                        HeungkukAccidentA itm = heungkukAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureToDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!readOnlyMode)
                {
                    heungkukAccident1.AddEmptyRow();
                }
                SortAccident1();

                dgv_Prg.AllowUserToAddRows = false;
                DataTable dt10 = pds.Tables["DataBlock10"];
                if (dt10 != null && dt10.Rows.Count > 0)
                {
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DataRow drow = dt10.Rows[i];
                        dgv_Prg.Rows.Add();
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtDt"].Value = Utils.ConvertToDateTime(drow["PrgMgtDt"]); // 진행일시
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtHed"].Value = drow["PrgMgtHed"];           // 업무구분
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = drow["SurvGuideCnts"];    // 세부내용
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
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersHeungkuk";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersHeungkuk.xsd";
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
            dr["InsurCo"] = custName;

            dt = ds.Tables["DataBlock3"];
            dr = dt.Rows.Add();
            dr["ClntTdncEvat"] = ucHeungkukPan11.ClntTdncEvat;         // 고객성향평가
            dr["UnusDtil"] = ucHeungkukPan11.UnusDtil;                // 특이사항
            dr["CmplPnt1"] = ucHeungkukPan11.CmplPnt1;                // 민원지수1
            dr["CmplPnt2"] = ucHeungkukPan11.CmplPnt2;                // 민원지수2
            dr["CmplPnt3"] = ucHeungkukPan11.CmplPnt3;                // 민원지수3
            dr["CmplPnt4"] = ucHeungkukPan11.CmplPnt4;                // 민원지수4
            dr["CmplPnt5"] = ucHeungkukPan11.CmplPnt5;                // 민원지수5

            // 계약사항
            dt = ds.Tables["DataBlock4"];
            DataTable dt5 = ds.Tables["DataBlock5"];
            for (int i = 0; i < heungkukContract1.Rows.Count; i++)
            {
                if (heungkukContract1.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = heungkukContract1.Rows[i].InsurPrdt;             // 보험종목
                dr["CtrtDt"] = heungkukContract1.Rows[i].CtrtDt;                   // 보험시기
                dr["CtrtExprDt"] = heungkukContract1.Rows[i].CtrtExprDt;            // 보험종기
                dr["InsurNo"] = heungkukContract1.Rows[i].InsurNo;                 // 증권번호
                dr["Insurant"] = heungkukContract1.Rows[i].Insurant;               // 계약자명
                dr["Insured"] = heungkukContract1.Rows[i].Insured;                 // 피보험자
                dr["Bnfc"] = heungkukContract1.Rows[i].Bnfc;                      // 수익자
                dr["IsrdJob"] = heungkukContract1.Rows[i].IsrdJob;                 // 직업
                dr["IsrdJobDmnd"] = heungkukContract1.Rows[i].IsrdJobDmnd;          // 현재직무
                dr["CtrtStts"] = heungkukContract1.Rows[i].CtrtStts;
                dr["CtrtSttsDt"] = heungkukContract1.Rows[i].CtrtSttsDt;
                dr["IsrtRegno1"] = heungkukContract1.Rows[i].IsrtRegno1;
                dr["IsrtRegno2"] = heungkukContract1.Rows[i].IsrtRegno2;
                dr["IsrtTel"] = heungkukContract1.Rows[i].IsrtTel;
                dr["IsrdRegno1"] = heungkukContract1.Rows[i].IsrdRegno1;
                dr["IsrdRegno2"] = heungkukContract1.Rows[i].IsrdRegno2;
                dr["IsrdTel"] = heungkukContract1.Rows[i].IsrdTel;
                if (heungkukContract1.Rows[i].IsrdAddressSeq == "") dr["IsrdAddressSeq"] = DBNull.Value;
                else dr["IsrdAddressSeq"] = heungkukContract1.Rows[i].IsrdAddressSeq;
                dr["IsrdJobGrad"] = heungkukContract1.Rows[i].IsrdJobGrad;
                dr["IsrdJobGradDmnd"] = heungkukContract1.Rows[i].IsrdJobGradDmnd;
                dr["IsrdJobNow"] = heungkukContract1.Rows[i].IsrdJobNow;
                dr["IsrdJobGradNow"] = heungkukContract1.Rows[i].IsrdJobGradNow;
                miHeungkukContractBRows rows = heungkukContract1.Rows[i].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    if (rows[ii].IsNewRow) continue;
                    DataRow dr5 = dt5.Rows.Add();
                    dr5["CltrCnts"] = rows[ii].CltrCnts;                        // 담보내용
                    dr5["InsurRegsAmt"] = Utils.ToDecimal(rows[ii].InsurRegsAmt);  // 보험가입금액
                    dr5["InsurNo"] = dr["InsurNo"];                            // 증권번호
                }
            }

            dt = ds.Tables["DataBlock6"];
            dr = dt.Rows.Add();
            dr["S101_LongCnts1"] = ucHeungkukPan21.S101_LongCnts1;
            dr["S101_ShrtCnts1"] = ucHeungkukPan21.S101_ShrtCnts1;
            dr["S101_ShrtCnts2"] = ucHeungkukPan21.S101_ShrtCnts2;
            dr["S101_LongCnts2"] = ucHeungkukPan21.S101_LongCnts2;
            dr["S102_ShrtCnts1"] = ucHeungkukPan21.S102_ShrtCnts1;
            dr["S102_ShrtCnts2"] = ucHeungkukPan21.S102_ShrtCnts2;
            dr["S102_LongCnts1"] = ucHeungkukPan21.S102_LongCnts1;
            dr["S102_LongCnts2"] = ucHeungkukPan21.S102_LongCnts2;
            dr["S102_ShrtCnts3"] = ucHeungkukPan21.S102_ShrtCnts3;
            dr["S133_ShrtCnts1"] = ucHeungkukPan21.S133_ShrtCnts1;
            dr["S133_ShrtCnts2"] = ucHeungkukPan21.S133_ShrtCnts2;

            dr["S201_ShrtCnts1"] = ucHeungkukPan31.S201_ShrtCnts1;
            dr["S201_LongCnts1"] = ucHeungkukPan31.S201_LongCnts1;
            dr["S201_LongCnts2"] = ucHeungkukPan31.S201_LongCnts2;

            // 타사가입사항
            dt = ds.Tables["DataBlock7"];
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

            dt = ds.Tables["DataBlock8"];
            dr = dt.Rows.Add();
            dr["S301_LongCnts1"] = txtS301_LongCnts1.rtbDoc.Rtf;
            dr["S301_LongCnts2"] = txtS301_LongCnts2.rtbDoc.Rtf;
            dr["S301_LongCnts3"] = txtS301_LongCnts3.rtbDoc.Rtf;
            dr["S401_ShrtCnts1"] = txtS401_ShrtCnts1.Text;
            dr["S401_ShrtCnts2"] = txtS401_ShrtCnts2.Text;
            dr["S401_LongCnts1"] = txtS401_LongCnts1.rtbDoc.Rtf;
            dr["S501_LongCnts1"] = txtS501_LongCnts1.rtbDoc.Rtf;
            dr["S501_LongCnts2"] = txtS501_LongCnts2.rtbDoc.Rtf;

            // 손해액 범위 조사
            dt = ds.Tables["DataBlock9"];
            for (int i = 0; i < heungkukAccident1.Rows.Count; i++)
            {
                if (heungkukAccident1.Rows[i].IsNewRow) continue;
                if (heungkukAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = heungkukAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = heungkukAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureToDt"] = heungkukAccident1.Rows[i].CureToDt;              // 치료기간 To
                dr["CureCnts"] = heungkukAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = heungkukAccident1.Rows[i].VstHosp;                // 의료기관
            }

            // 조사 일정 요약표
            dt = ds.Tables["DataBlock10"];
            for (int i = 0; i < dgv_Prg.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["PrgMgtDt"] = Utils.DateFormat(dgv_Prg.Rows[i].Cells["prgPrgMgtDt"].Value, "yyyyMMdd");
                dr["PrgMgtHed"] = dgv_Prg.Rows[i].Cells["prgPrgMgtHed"].Value;
                dr["SurvGuideCnts"] = dgv_Prg.Rows[i].Cells["prgSurvGuideCnts"].Value;
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
                for (int ii = heungkukAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (heungkukAccident1.Rows[ii].IsNewRow) continue;
                    if (heungkukAccident1.Rows[ii].Gubun == "1") heungkukAccident1.RemoveRow(heungkukAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < heungkukContract1.Rows.Count; ii++)
                {
                    if (heungkukContract1.Rows[ii].IsNewRow) continue;
                    HeungkukAccidentA itm = heungkukAccident1.AddRow("", "계약일", heungkukContract1.Rows[ii].CtrtDt, "", heungkukContract1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                heungkukAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < heungkukAccident1.Rows.Count; ii++)
                {
                    if (heungkukAccident1.Rows[ii].IsNewRow) continue;
                    if (heungkukAccident1.Rows[ii].Gubun == "1") continue;
                    heungkukAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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