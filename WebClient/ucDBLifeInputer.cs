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
    public partial class ucDBLifeInputer : UserControl
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

        public ucDBLifeInputer()
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
                ucDBLifePan11.SetFocus();
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
                    if (umjdseq == "3053004" || deptnm.Contains("지원")) this.SmplAuth = true;
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
            this.btnSort.Click += BtnSort_Click;
            userno1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLifeAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan41.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifeInvoice1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifeInvoice2.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            DBLifeDetail11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dbLifeAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan21.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifePan41.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifeInvoice1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucDBLifeInvoice2.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            DBLifeDetail11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            S201_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            S301_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            S401_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            S201_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            S301_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            S401_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
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

            dbLifeAccident1.Userno1 = userno1;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.ReadOnlyMode = rdonly;
            if (ReadOnlyMode)
            {
                userno1.SetReadOnlyMode(rdonly);
                dbLifeAccident1.SetReadOnlyMode(rdonly);
                ucDBLifePan11.SetReadOnlyMode(rdonly);
                ucDBLifePan21.SetReadOnlyMode(rdonly);
                ucDBLifePan31.SetReadOnlyMode(rdonly);
                ucDBLifePan41.SetReadOnlyMode(rdonly);
                ucDBLifeInvoice1.SetReadOnlyMode(rdonly);
                ucDBLifeInvoice2.SetReadOnlyMode(rdonly);
                DBLifeDetail11.SetReadOnlyMode(rdonly);
                S201_LongCnts1.rtbDoc.ReadOnly = rdonly;
                S301_LongCnts1.rtbDoc.ReadOnly = rdonly;
                S401_LongCnts1.rtbDoc.ReadOnly = rdonly;
            }
        }

        private void ClearAll()
        {
            userno1.Clear();
            dbLifeAccident1.Clear();
            ucDBLifePan11.Clear();
            ucDBLifePan21.Clear();
            ucDBLifePan31.Clear();
            ucDBLifePan41.Clear();

            ucDBLifeInvoice1.Clear();
            ucDBLifeInvoice2.Clear();

            DBLifeDetail11.Clear();

            S201_LongCnts1.rtbDoc.Rtf = "";
            S301_LongCnts1.rtbDoc.Rtf = "";
            S401_LongCnts1.rtbDoc.Rtf = "";

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

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersDBLife.xsd";
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
                    ucDBLifePan11.AcptDt = drow["AcptDt"] + "";
                    ucDBLifePan11.AcptMgmtNo = drow["AcptMgmtNo"] + "";
                    ucDBLifePan11.InsurPrdt = drow["InsurPrdt"] + "";
                    ucDBLifePan11.InsurNo = drow["InsurNo"] + "";
                    ucDBLifePan11.InsurNoOld = drow["InsurNoOld"] + "";
                    ucDBLifePan11.Insured = drow["Insured"] + "";
                    ucDBLifePan11.IsrdRegno1 = drow["IsrdRegno1"] + "";
                    ucDBLifePan11.IsrdRegno2 = drow["IsrdRegno2"] + "";
                    ucDBLifePan11.IsrdJob = drow["IsrdJob"] + "";
                    ucDBLifePan11.IsrdJobDmnd = drow["IsrdJobDmnd"] + "";
                    ucDBLifePan11.IsrdTel = drow["IsrdTel"] + "";
                    ucDBLifePan11.IsrdAddressSeq = drow["IsrdAddressSeq"] + "";
                    ucDBLifePan11.IsrdAddressName = drow["IsrdAddressName"] + "";

                    ucDBLifePan21.SurvAsgnEmpName = drow["SurvAsgnEmpName"] + "";
                    ucDBLifePan21.InsurChrg = drow["InsurChrg"] + "";
                    ucDBLifePan21.SurvReqDt = drow["SurvReqDt"] + "";
                    ucDBLifePan21.AcptDt = drow["AcptDt"] + "";
                    ucDBLifePan21.CclsDt = drow["CclsDt"] + "";
                    ucDBLifePan21.LasRptSbmsDt = drow["LasRptSbmsDt"] + "";
                    ucDBLifePan21.PayDt = "";
                    ucDBLifePan21.CmplExptFg = drow["CmplExptFg"] + "";

                    ucDBLifePan31.SurvAsgnEmpName = drow["SurvAsgnEmpName"] + "";
                    ucDBLifePan31.SurvAsgnEmpHP = drow["SurvAsgnEmpHP"] + "";
                    ucDBLifePan31.SurvAsgnTeamLeadName = drow["SurvAsgnTeamLeadName"] + "";
                    ucDBLifePan31.SurvAsgnTeamLeadOP = drow["SurvAsgnTeamLeadOP"] + "";
                    ucDBLifePan31.ChrgAdjuster = drow["ChrgAdjuster"] + "";
                    ucDBLifePan31.SurvAsgnTeamName = drow["SurvAsgnTeamName"] + "";
                    ucDBLifePan31.LeadAdjuster = drow["LeadAdjuster"] + "";
                    ucDBLifePan31.SurvAsgnEmpRank = drow["SurvAsgnEmpRank"] + "";

                    custName = drow["InsurCo"] + "";
                }

                // 계약사항
                DataTable dt10 = pds.Tables["DataBlock10"];
                if (dt10 != null && dt10.Rows.Count > 0)
                {
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DataRow drow = dt10.Rows[i];
                        userno1.AddRow(drow);
                    }
                }
                if (!ReadOnlyMode)
                {
                    userno1.AddEmptyRow();
                }
                userno1.Sort();

                DataTable dt3 = pds.Tables["DataBlock3"];
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DataRow drow = dt3.Rows[i];
                        DBLifeAccidentA itm = dbLifeAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureToDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!ReadOnlyMode)
                {
                    dbLifeAccident1.AddEmptyRow();
                }
                SortAccident1();

                DataTable dt4 = pds.Tables["DataBlock4"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    DataRow drow = dt4.Rows[0];
                    ucDBLifePan11.LongCnts1 = drow["S101_LongCnts1"] + "";
                    S201_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S201_LongCnts1"]);
                    S301_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S301_LongCnts1"]);
                    S401_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S401_LongCnts1"]);
                }

                dgv_Othr.AllowUserToAddRows = false;
                // 타사 가입 사항
                DataTable dt5 = pds.Tables["DataBlock5"];
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DataRow drow = dt5.Rows[i];
                        dgv_Othr.Rows.Add();
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCoNm"].Value = drow["OthInsurCoNm"];                     // 보험회사
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtDt"]);    // 보험
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurPrdt"].Value = drow["OthInsurPrdt"];                     // 담보내용
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurSurvOpni"].Value = drow["OthInsurSurvOpni"];              // 청구내용 및 결과
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurRegsAmt"].Value = Utils.ToDecimal(drow["OthInsurRegsAmt"]); // 청구금액
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCtrtSeq"].Value = drow["OthInsurCtrtSeq"];                // 순번
                    }
                }
                if (!ReadOnlyMode)
                {
                    dgv_Othr.AllowUserToAddRows = true;
                }

                for (int i = 1; i <= 8; i++)
                {
                }
                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        DataRow drow = dt6.Rows[i];
                        ucDBLifePan41.SetRow(drow);
                    }
                }

                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    DataRow drow = dt7.Rows[0];
                    ucDBLifeInvoice1.InvcAmtCof = drow["InvcAmtCof"] + "";
                    ucDBLifeInvoice1.InvcAdjFeeCdNm = drow["InvcAdjFeeCdNm"] + "";
                    ucDBLifeInvoice1.InvcAdjFee = drow["InvcAdjFee"] + "";
                    ucDBLifeInvoice1.InvcDocuAmt = drow["InvcDocuAmt"] + "";
                    ucDBLifeInvoice1.InvcCsltReqAmt = drow["InvcCsltReqAmt"] + "";
                    ucDBLifeInvoice1.InvcTrspExps = drow["InvcTrspExps"] + "";
                    ucDBLifeInvoice1.InvcIctvAmt = drow["InvcIctvAmt"] + "";

                    ucDBLifeInvoice2.InvcAmtCof = drow["InvcAmtCof"] + "";
                    ucDBLifeInvoice2.InvcAdjFeeCdNm = drow["InvcAdjFeeCdNm"] + "";
                    ucDBLifeInvoice2.InvcAdjFee = drow["InvcAdjFee"] + "";
                    ucDBLifeInvoice2.InvcDocuAmt = drow["InvcDocuAmt"] + "";
                    ucDBLifeInvoice2.InvcCsltReqAmt = drow["InvcCsltReqAmt"] + "";
                    ucDBLifeInvoice2.InvcTrspExps = drow["InvcTrspExps"] + "";
                    ucDBLifeInvoice2.InvcIctvAmt = drow["InvcIctvAmt"] + "";
                }

                // 비용 세부항목 및 첨부 자료
                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DataRow drow = dt8.Rows[i];
                        DBLifeDetail11.AddRow(drow);
                    }
                }
                if (!ReadOnlyMode)
                {
                    DBLifeDetail11.AddEmptyRow();
                }
                DBLifeDetail11.Sort();

                dgv_Prg.AllowUserToAddRows = false;
                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DataRow drow = dt9.Rows[i];
                        dgv_Prg.Rows.Add();
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtDt"].Value = Utils.ConvertToDateTime(drow["PrgMgtDt"]); // 진행일시
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtHed"].Value = drow["PrgMgtHed"];           // 업무구분
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);    // 세부내용
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

        public bool Ins1Save()
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersDBLife";
                security.methodId = "Ins1Save";
                security.companySeq = param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = param.UserID;

                //저장할 데이타 가져오기
                DataSet ds = new DataSet();

                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");
                dt.Columns.Add("DcmgDocNo");
                dt.Columns.Add("ReportType");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["DcmgDocNo"] = "";
                dr["ReportType"] = param.ReportType;

                dt = ds.Tables.Add("DataBlock2");
                dt.Columns.Add("InsurPrdt");
                dt.Columns.Add("InsurNo");
                dt.Columns.Add("InsurNoOld");
                dt.Columns.Add("Insured");
                dt.Columns.Add("IsrdJob");
                dt.Columns.Add("IsrdJobDmnd");
                dt.Columns.Add("IsrdTel");
                dt.Columns.Add("IsrdAddressSeq");
                dt.Columns.Add("IsrdAddressName");
                dt.Columns.Add("IsrdRegno1");
                dt.Columns.Add("IsrdRegno2");
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = ucDBLifePan11.InsurPrdt;
                dr["InsurNo"] = ucDBLifePan11.InsurNo;
                dr["InsurNoOld"] = ucDBLifePan11.InsurNoOld;
                dr["Insured"] = ucDBLifePan11.Insured;
                dr["IsrdJob"] = ucDBLifePan11.IsrdJob;
                dr["IsrdJobDmnd"] = ucDBLifePan11.IsrdJobDmnd;
                dr["IsrdTel"] = ucDBLifePan11.IsrdTel;
                if (ucDBLifePan11.IsrdAddressSeq == "") dr["IsrdAddressSeq"] = DBNull.Value;
                else dr["IsrdAddressSeq"] = ucDBLifePan11.IsrdAddressSeq;
                dr["IsrdAddressName"] = ucDBLifePan11.IsrdAddressName;
                dr["IsrdRegno1"] = ucDBLifePan11.IsrdRegno1;
                dr["IsrdRegno2"] = ucDBLifePan11.IsrdRegno2;
                //저장할 데이타 가져오기

                //계약사항 저장
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
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
                ucDBLifePan11.InsurNoOld = ucDBLifePan11.InsurNo;

                //계약사항 새로 조회
                security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersDBLife";
                security.methodId = "InsQuery";
                security.companySeq = param.CompanySeq;

                ds = new DataSet("ROOT");
                dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");
                dt.Columns.Add("DcmgDocNo");
                dt.Columns.Add("ReportType");
                dt.Clear();
                dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["DcmgDocNo"] = "";
                dr["ReportType"] = param.ReportType;

                yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return true;
                }

                string xml = yds.GetXml();
                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersDBLife.xsd";
                DataSet pds = new DataSet();
                pds.ReadXml(s_CAA_XSD);

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    pds.ReadXml(xmlReader);
                }

                userno1.Clear();
                // 계약사항
                DataTable dt10 = pds.Tables["DataBlock10"];
                if (dt10 != null && dt10.Rows.Count > 0)
                {
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DataRow drow = dt10.Rows[i];
                        userno1.AddRow(drow);
                    }
                }
                if (!ReadOnlyMode)
                {
                    userno1.AddEmptyRow();
                }
                userno1.Sort();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void XmlData_Save(bool bNoMessage = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersDBLife";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersDBLife.xsd";
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(s_CAA_XSD);

            DataTable dt = ds.Tables.Add("DataBlock1");

            dt.Columns.Add("AcptMgmtSeq");
            dt.Columns.Add("ReSurvAsgnNo");
            dt.Columns.Add("DcmgDocNo");
            dt.Columns.Add("ReportType");

            dt.Clear();

            DataRow dr = dt.Rows.Add();
            dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
            dr["DcmgDocNo"] = "";
            dr["ReportType"] = param.ReportType;

            dt = ds.Tables["DataBlock2"];
            dr = dt.Rows.Add();
            dr["CmplExptFg"] = ucDBLifePan21.CmplExptFg;

            // 손해액 범위 조사
            dt = ds.Tables["DataBlock3"];
            for (int i = 0; i < dbLifeAccident1.Rows.Count; i++)
            {
                if (dbLifeAccident1.Rows[i].IsNewRow) continue;
                if (dbLifeAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = dbLifeAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = dbLifeAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureToDt"] = dbLifeAccident1.Rows[i].CureToDt;              // 치료기간 To
                dr["CureCnts"] = dbLifeAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = dbLifeAccident1.Rows[i].VstHosp;                // 의료기관
            }

            dt = ds.Tables["DataBlock4"];
            dr = dt.Rows.Add();
            dr["S101_LongCnts1"] = ucDBLifePan11.LongCnts1;
            dr["S201_LongCnts1"] = S201_LongCnts1.rtbDoc.Rtf;
            dr["S301_LongCnts1"] = S301_LongCnts1.rtbDoc.Rtf;
            dr["S401_LongCnts1"] = S401_LongCnts1.rtbDoc.Rtf;

            // 타사가입사항
            dt = ds.Tables["DataBlock5"];
            for (int i = 0; i < dgv_Othr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInsurCoNm"] = dgv_Othr.Rows[i].Cells["othrOthInsurCoNm"].Value;
                dr["OthCtrtDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtDt"].Value, "yyyyMMdd");
                dr["OthInsurPrdt"] = dgv_Othr.Rows[i].Cells["othrOthInsurPrdt"].Value;
                dr["OthInsurSurvOpni"] = dgv_Othr.Rows[i].Cells["othrOthInsurSurvOpni"].Value;
                dr["OthInsurRegsAmt"] = dgv_Othr.Rows[i].Cells["othrOthInsurRegsAmt"].Value;
                dr["OthInsurCtrtSeq"] = Utils.ToInt(dgv_Othr.Rows[i].Cells["othrOthInsurCtrtSeq"].Value);
            }

            dt = ds.Tables["DataBlock6"];
            for (int i = 1; i <= 8; i++)
            {
                dr = dt.Rows.Add();
                ucDBLifePan41.GetRow(Utils.ConvertToString(i), dr);
            }

            // 비용 세부항목 및 첨부 자료
            dt = ds.Tables["DataBlock8"];
            for (int i = 0; i < DBLifeDetail11.Rows.Count; i++)
            {
                if (DBLifeDetail11.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["ShrtCnts1"] = DBLifeDetail11.Rows[i].ShrtCnts1;             // 항목
                dr["Amt1"] = DBLifeDetail11.Rows[i].Amt1;                      // 비용
                dr["ShrtCnts2"] = DBLifeDetail11.Rows[i].ShrtCnts2;             // 방문처
                dr["ShrtCnts3"] = DBLifeDetail11.Rows[i].ShrtCnts3;             // 확인일자
                dr["LongCnts1"] = DBLifeDetail11.Rows[i].LongCnts1;             // 첨부자료
            }

            // 조사 일정 요약표
            dt = ds.Tables["DataBlock9"];
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
                for (int ii = dbLifeAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (dbLifeAccident1.Rows[ii].IsNewRow) continue;
                    if (dbLifeAccident1.Rows[ii].Gubun == "1") dbLifeAccident1.RemoveRow(dbLifeAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < userno1.Rows.Count; ii++)
                {
                    if (userno1.Rows[ii].IsNewRow) continue;
                    DBLifeAccidentA itm = dbLifeAccident1.AddRow("", "계약일", userno1.Rows[ii].CtrtDt, "", userno1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                dbLifeAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < dbLifeAccident1.Rows.Count; ii++)
                {
                    if (dbLifeAccident1.Rows[ii].IsNewRow) continue;
                    if (dbLifeAccident1.Rows[ii].Gubun == "1") continue;
                    dbLifeAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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