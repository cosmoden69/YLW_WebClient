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
    public partial class ucHyundaiInputer : UserControl
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool _readOnlyMode = false;
        public bool ReadOnlyMode
        {
            get
            {
                return _readOnlyMode;
            }
            set
            {
                _readOnlyMode = value;
                SetReadOnlyMode(_readOnlyMode);
            }
        }

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        public ucHyundaiInputer()
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
                if (dgv_Cltr.CurrentCell is DataGridViewTextBoxCell && dgv_Cltr.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
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
            userno1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            hyundaiAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan41.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan51.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            hyundaiAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan21.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan41.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucHyundaiPan51.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.dgv_Cltr.CellBeginEdit += Grid_CellBeginEdit;
            this.dgv_Cltr.CellEndEdit += Grid_CellEndEdit;
            dgv_Cltr.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Prg.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Cltr.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Prg.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Cltr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Prg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Cltr.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Prg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Cltr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Prg.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Cltr.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Prg.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Cltr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Prg.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Cltr.DataError += Grid_DataError;
            dgv_Prg.DataError += Grid_DataError;

            hyundaiAccident1.Userno1 = userno1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.ReadOnlyMode = rdonly;
            if (ReadOnlyMode)
            {
                userno1.SetReadOnlyMode(rdonly);
                hyundaiAccident1.SetReadOnlyMode(rdonly);
                ucHyundaiPan11.SetReadOnlyMode(rdonly);
                ucHyundaiPan21.SetReadOnlyMode(rdonly);
                ucHyundaiPan31.SetReadOnlyMode(rdonly);
                ucHyundaiPan41.SetReadOnlyMode(rdonly);
                ucHyundaiPan51.SetReadOnlyMode(rdonly);
            }
        }

        private void ClearAll()
        {
            userno1.Clear();
            hyundaiAccident1.Clear();
            ucHyundaiPan11.Clear();
            ucHyundaiPan21.Clear();
            ucHyundaiPan31.Clear();
            ucHyundaiPan41.Clear();
            ucHyundaiPan51.Clear();

            dgv_Cltr.Rows.Clear();
            dgv_Cltr.Height = 68;
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

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersHyundai.xsd";
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
                    ucHyundaiPan11.AcdtDt = drow["AcdtDt"] + "";
                    ucHyundaiPan11.AcdtTm = drow["AcdtTm"] + "";
                    ucHyundaiPan11.AcdtAddressName = drow["AcdtAddressName"] + "";
                    ucHyundaiPan11.AcdtCnts = drow["AcdtCnts"] + "";
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
                if (!ReadOnlyMode)
                {
                    userno1.AddEmptyRow();
                }
                userno1.Sort();

                dgv_Cltr.AllowUserToAddRows = false;
                // 담보내용
                DataTable dt4 = pds.Tables["DataBlock4"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DataRow drow = dt4.Rows[i];
                        dgv_Cltr.Rows.Add();
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrCltrCnts"].Value = Utils.ToMultiline(drow["CltrCnts"]);         // 담보내용
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrInsurRegsAmt"].Value = drow["InsurRegsAmt"];  // 보험가입금액
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrInsurDmndAmt"].Value = drow["InsurDmndAmt"];  // 가입금액
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrCmnt"].Value = drow["Cmnt"];                // 비고
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrInsurNo"].Value = drow["InsurNo"];           // 증권번호
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrSubSeq"].Value = drow["SubSeq"];             // 순번
                    }
                }
                if (!ReadOnlyMode)
                {
                    dgv_Cltr.AllowUserToAddRows = true;
                }
                dgv_Cltr.AutoResizeRows();
                GridHeightResize(dgv_Cltr);

                DataTable dt5 = pds.Tables["DataBlock5"];
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    DataRow drow = dt5.Rows[0];
                    ucHyundaiPan21.S131_LongCnts1 = Utils.ConvertToRtf(drow["S131_LongCnts1"]);
                    ucHyundaiPan21.S131_ShrtCnts1 = drow["S131_ShrtCnts1"] + "";
                    ucHyundaiPan21.S131_LongCnts2 = Utils.ConvertToRtf(drow["S131_LongCnts2"]);
                    ucHyundaiPan21.S131_ShrtCnts2 = drow["S131_ShrtCnts2"] + "";
                    ucHyundaiPan21.S131_Amt1 = (Utils.ToDecimal(drow["S131_Amt1"]) == 0 ? "" : drow["S131_Amt1"] + "");
                    ucHyundaiPan21.S132_LongCnts1 = Utils.ConvertToRtf(drow["S132_LongCnts1"]);
                    ucHyundaiPan21.S132_ShrtCnts1 = drow["S132_ShrtCnts1"] + "";
                    ucHyundaiPan21.S132_LongCnts2 = Utils.ConvertToRtf(drow["S132_LongCnts2"]);
                    ucHyundaiPan21.S132_ShrtCnts2 = drow["S132_ShrtCnts2"] + "";
                    ucHyundaiPan21.S132_Amt1 = (Utils.ToDecimal(drow["S132_Amt1"]) == 0 ? "" : drow["S132_Amt1"] + "");
                    ucHyundaiPan21.S133_LongCnts1 = Utils.ConvertToRtf(drow["S133_LongCnts1"]);
                    ucHyundaiPan21.S133_ShrtCnts1 = drow["S133_ShrtCnts1"] + "";
                    ucHyundaiPan21.S133_LongCnts2 = Utils.ConvertToRtf(drow["S133_LongCnts2"]);
                    ucHyundaiPan21.S133_ShrtCnts2 = drow["S133_ShrtCnts2"] + "";
                    ucHyundaiPan21.S133_Amt1 = (Utils.ToDecimal(drow["S133_Amt1"]) == 0 ? "" : drow["S133_Amt1"] + ""); ;
                }

                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    DataRow drow = dt6.Rows[0];
                    ucHyundaiPan31.LongCnts1 = Utils.ConvertToRtf(drow["S141_LongCnts1"]);

                    ucHyundaiPan41.S201_LongCnts1 = Utils.ConvertToRtf(drow["S201_LongCnts1"]);
                    ucHyundaiPan41.S201_LongCnts2 = Utils.ConvertToRtf(drow["S201_LongCnts2"]);
                    ucHyundaiPan41.S202_LongCnts1 = Utils.ConvertToRtf(drow["S202_LongCnts1"]);
                    ucHyundaiPan41.S202_LongCnts2 = Utils.ConvertToRtf(drow["S202_LongCnts2"]);
                    ucHyundaiPan41.S203_LongCnts1 = Utils.ConvertToRtf(drow["S203_LongCnts1"]);
                    ucHyundaiPan41.S203_LongCnts2 = Utils.ConvertToRtf(drow["S203_LongCnts2"]);
                    ucHyundaiPan41.S204_LongCnts1 = Utils.ConvertToRtf(drow["S204_LongCnts1"]);
                    ucHyundaiPan41.S204_LongCnts2 = Utils.ConvertToRtf(drow["S204_LongCnts2"]);
                    ucHyundaiPan41.S205_LongCnts1 = Utils.ConvertToRtf(drow["S205_LongCnts1"]);
                    ucHyundaiPan41.S205_LongCnts2 = Utils.ConvertToRtf(drow["S205_LongCnts2"]);
                    ucHyundaiPan41.S206_LongCnts1 = Utils.ConvertToRtf(drow["S206_LongCnts1"]);
                    ucHyundaiPan41.S206_LongCnts2 = Utils.ConvertToRtf(drow["S206_LongCnts2"]);
                    ucHyundaiPan41.S207_LongCnts1 = Utils.ConvertToRtf(drow["S207_LongCnts1"]);
                    ucHyundaiPan41.S207_LongCnts2 = Utils.ConvertToRtf(drow["S207_LongCnts2"]);

                    ucHyundaiPan51.S301_LongCnts1 = Utils.ConvertToRtf(drow["S301_LongCnts1"]);
                    ucHyundaiPan51.S301_LongCnts2 = Utils.ConvertToRtf(drow["S301_LongCnts2"]);
                }

                // 조사자 일자별 확인사항
                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DataRow drow = dt7.Rows[i];
                        HyundaiAccidentA itm = hyundaiAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureToDt"], drow["OutHospDay"], drow["InHospDay"], drow["CureCnts"], drow["TestNmRslt"], Utils.ToMultiline(drow["VstHosp"]), drow["BfGivCnts"], drow["PrvSrc"]);
                        itm.Gubun = "2";
                    }
                }
                if (!ReadOnlyMode)
                {
                    hyundaiAccident1.AddEmptyRow();
                }
                SortAccident1();

                dgv_Prg.AllowUserToAddRows = false;
                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DataRow drow = dt8.Rows[i];
                        dgv_Prg.Rows.Add();
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtDt"].Value = Utils.ConvertToDateTime(drow["PrgMgtDt"]); // 진행일시
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtHed"].Value = drow["PrgMgtHed"];           // 업무구분
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);    // 세부내용
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetMthd"].Value = drow["MetMthd"];               // 비고
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtSeq"].Value = drow["PrgMgtSeq"];           // 순번
                    }
                }
                if (!ReadOnlyMode)
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
                if (ReadOnlyMode)
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
                if (ReadOnlyMode)
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
                if (ReadOnlyMode) return;
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
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersHyundai";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersHyundai.xsd";
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
            dr["AcdtDt"] = ucHyundaiPan11.AcdtDt;                    // 사고일자
            dr["AcdtTm"] = ucHyundaiPan11.AcdtTm;                    // 사고시간
            dr["AcdtCnts"] = ucHyundaiPan11.AcdtCnts;                // 사고내용

            // 계약사항
            dt = ds.Tables["DataBlock3"];
            for (int i = 0; i < userno1.Rows.Count; i++)
            {
                if (userno1.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = userno1.Rows[i].InsurPrdt;             // 보험종목
                dr["InsurNo"] = userno1.Rows[i].InsurNo;                 // 증권번호
                dr["Insurant"] = userno1.Rows[i].Insurant;               // 계약자명
                dr["Insured"] = userno1.Rows[i].Insured;                 // 피보험자
                dr["Bnfc"] = userno1.Rows[i].Bnfc;                      // 수익자
                dr["CtrtDt"] = userno1.Rows[i].CtrtDt;                   // 보험시기
                dr["CtrtExprDt"] = userno1.Rows[i].CtrtExprDt;            // 보험종기
                dr["IsrdJob"] = userno1.Rows[i].IsrdJob;                 // 직업
                dr["CtrtStts"] = userno1.Rows[i].CtrtStts;
                dr["CtrtSttsDt"] = userno1.Rows[i].CtrtSttsDt;
                dr["IsrtRegno1"] = userno1.Rows[i].IsrtRegno1;
                dr["IsrtRegno2"] = userno1.Rows[i].IsrtRegno2;
                dr["IsrtTel"] = userno1.Rows[i].IsrtTel;
                dr["IsrdRegno1"] = userno1.Rows[i].IsrdRegno1;
                dr["IsrdRegno2"] = userno1.Rows[i].IsrdRegno2;
                dr["IsrdTel"] = userno1.Rows[i].IsrdTel;
                if (userno1.Rows[i].IsrdAddressSeq == "") dr["IsrdAddressSeq"] = DBNull.Value;
                else dr["IsrdAddressSeq"] = userno1.Rows[i].IsrdAddressSeq;
                dr["IsrdJobGrad"] = userno1.Rows[i].IsrdJobGrad;
                dr["IsrdJobDmnd"] = userno1.Rows[i].IsrdJobDmnd;
                dr["IsrdJobGradDmnd"] = userno1.Rows[i].IsrdJobGradDmnd;
                dr["IsrdJobNow"] = userno1.Rows[i].IsrdJobNow;
                dr["IsrdJobGradNow"] = userno1.Rows[i].IsrdJobGradNow;
            }

            // 담보내용
            dt = ds.Tables["DataBlock4"];
            for (int i = 0; i < dgv_Cltr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["CltrCnts"] = dgv_Cltr.Rows[i].Cells["cltrCltrCnts"].Value;
                dr["InsurRegsAmt"] = Utils.ToDecimal(dgv_Cltr.Rows[i].Cells["cltrInsurRegsAmt"].Value);
                dr["InsurDmndAmt"] = Utils.ToDecimal(dgv_Cltr.Rows[i].Cells["cltrInsurDmndAmt"].Value);
                dr["Cmnt"] = dgv_Cltr.Rows[i].Cells["cltrCmnt"].Value;
                dr["InsurNo"] = dgv_Cltr.Rows[i].Cells["cltrInsurNo"].Value;
                dr["SubSeq"] = Utils.ToInt(dgv_Cltr.Rows[i].Cells["cltrSubSeq"].Value);
            }

            dt = ds.Tables["DataBlock5"];
            dr = dt.Rows.Add();
            dr["S131_LongCnts1"] = ucHyundaiPan21.S131_LongCnts1;
            dr["S131_ShrtCnts1"] = ucHyundaiPan21.S131_ShrtCnts1;
            dr["S131_LongCnts2"] = ucHyundaiPan21.S131_LongCnts2;
            dr["S131_ShrtCnts2"] = ucHyundaiPan21.S131_ShrtCnts2;
            dr["S131_Amt1"] = Utils.ToDecimal(ucHyundaiPan21.S131_Amt1);
            dr["S132_LongCnts1"] = ucHyundaiPan21.S132_LongCnts1;
            dr["S132_ShrtCnts1"] = ucHyundaiPan21.S132_ShrtCnts1;
            dr["S132_LongCnts2"] = ucHyundaiPan21.S132_LongCnts2;
            dr["S132_ShrtCnts2"] = ucHyundaiPan21.S132_ShrtCnts2;
            dr["S132_Amt1"] = Utils.ToDecimal(ucHyundaiPan21.S132_Amt1);
            dr["S133_LongCnts1"] = ucHyundaiPan21.S133_LongCnts1;
            dr["S133_ShrtCnts1"] = ucHyundaiPan21.S133_ShrtCnts1;
            dr["S133_LongCnts2"] = ucHyundaiPan21.S133_LongCnts2;
            dr["S133_ShrtCnts2"] = ucHyundaiPan21.S133_ShrtCnts2;
            dr["S133_Amt1"] = Utils.ToDecimal(ucHyundaiPan21.S133_Amt1);

            dt = ds.Tables["DataBlock6"];
            dr = dt.Rows.Add();
            dr["S141_LongCnts1"] = ucHyundaiPan31.LongCnts1;
            dr["S141_LongCnts2"] = "";

            dr["S201_LongCnts1"] = ucHyundaiPan41.S201_LongCnts1;
            dr["S201_LongCnts2"] = ucHyundaiPan41.S201_LongCnts2;
            dr["S202_LongCnts1"] = ucHyundaiPan41.S202_LongCnts1;
            dr["S202_LongCnts2"] = ucHyundaiPan41.S202_LongCnts2;
            dr["S203_LongCnts1"] = ucHyundaiPan41.S203_LongCnts1;
            dr["S203_LongCnts2"] = ucHyundaiPan41.S203_LongCnts2;
            dr["S204_LongCnts1"] = ucHyundaiPan41.S204_LongCnts1;
            dr["S204_LongCnts2"] = ucHyundaiPan41.S204_LongCnts2;
            dr["S205_LongCnts1"] = ucHyundaiPan41.S205_LongCnts1;
            dr["S205_LongCnts2"] = ucHyundaiPan41.S205_LongCnts2;
            dr["S206_LongCnts1"] = ucHyundaiPan41.S206_LongCnts1;
            dr["S206_LongCnts2"] = ucHyundaiPan41.S206_LongCnts2;
            dr["S207_LongCnts1"] = ucHyundaiPan41.S207_LongCnts1;
            dr["S207_LongCnts2"] = ucHyundaiPan41.S207_LongCnts2;

            dr["S301_LongCnts1"] = ucHyundaiPan51.S301_LongCnts1;
            dr["S301_LongCnts2"] = ucHyundaiPan51.S301_LongCnts2;

            // 손해액 범위 조사
            dt = ds.Tables["DataBlock7"];
            for (int i = 0; i < hyundaiAccident1.Rows.Count; i++)
            {
                if (hyundaiAccident1.Rows[i].IsNewRow) continue;
                if (hyundaiAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = hyundaiAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = hyundaiAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureToDt"] = hyundaiAccident1.Rows[i].CureToDt;              // 치료기간 To
                dr["OutHospDay"] = Utils.ToInt(hyundaiAccident1.Rows[i].OutHospDay);          // 외래일수
                dr["InHospDay"] = Utils.ToInt(hyundaiAccident1.Rows[i].InHospDay);            // 입원일수
                dr["CureCnts"] = hyundaiAccident1.Rows[i].CureCnts;              // 치료내용
                dr["TestNmRslt"] = hyundaiAccident1.Rows[i].TestNmRslt;          // 검사내용
                dr["VstHosp"] = hyundaiAccident1.Rows[i].VstHosp;                // 의료기관
                dr["BfGivCnts"] = hyundaiAccident1.Rows[i].BfGivCnts;            // 기지급여부
                dr["PrvSrc"] = hyundaiAccident1.Rows[i].PrvSrc;                  // 입증자료
            }

            // 조사 일정 요약표
            dt = ds.Tables["DataBlock8"];
            for (int i = 0; i < dgv_Prg.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["PrgMgtDt"] = Utils.DateFormat(dgv_Prg.Rows[i].Cells["prgPrgMgtDt"].Value, "yyyyMMdd");
                dr["PrgMgtHed"] = dgv_Prg.Rows[i].Cells["prgPrgMgtHed"].Value;
                dr["SurvGuideCnts"] = dgv_Prg.Rows[i].Cells["prgSurvGuideCnts"].Value;
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
                for (int ii = hyundaiAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (hyundaiAccident1.Rows[ii].IsNewRow) continue;
                    if (hyundaiAccident1.Rows[ii].Gubun == "1") hyundaiAccident1.RemoveRow(hyundaiAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < userno1.Rows.Count; ii++)
                {
                    if (userno1.Rows[ii].IsNewRow) continue;
                    HyundaiAccidentA itm = hyundaiAccident1.AddRow("", "계약일", userno1.Rows[ii].CtrtDt, "", "", "", userno1.Rows[ii].InsurPrdt, "", custName, "", "");
                    itm.Gubun = "1";    // 구분
                }
                hyundaiAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < hyundaiAccident1.Rows.Count; ii++)
                {
                    if (hyundaiAccident1.Rows[ii].IsNewRow) continue;
                    if (hyundaiAccident1.Rows[ii].Gubun == "1") continue;
                    hyundaiAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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