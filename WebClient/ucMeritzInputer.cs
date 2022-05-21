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
    public partial class ucMeritzInputer : UserControl, ISmplInputer
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

        public ucMeritzInputer()
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
                if (dgv_Cltr.CurrentCell is DataGridViewTextBoxCell && dgv_Cltr.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (dgv_Etc.CurrentCell is DataGridViewTextBoxCell && dgv_Etc.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_Othr.CurrentCell is DataGridViewTextBoxCell && dgv_Othr.CurrentCell.IsInEditMode)
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
            meritzAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan41.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan51.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            meritzAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan41.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMeritzPan51.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            db6LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            db6LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            db6LongCnts3.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            db6LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            db6LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            db6LongCnts3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            this.dgv_Cltr.CellBeginEdit += Grid_CellBeginEdit;
            this.dgv_Cltr.CellEndEdit += Grid_CellEndEdit;
            dgv_Cltr.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Othr.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Prg.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Etc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Cltr.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Othr.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Prg.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Etc.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Cltr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Othr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Prg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Etc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Cltr.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Othr.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Prg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Etc.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Cltr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Othr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Prg.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Etc.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Cltr.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Othr.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Prg.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Etc.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Cltr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Othr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Prg.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Etc.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Cltr.DataError += Grid_DataError;
            dgv_Othr.DataError += Grid_DataError;
            dgv_Prg.DataError += Grid_DataError;
            dgv_Etc.DataError += Grid_DataError;

            meritzAccident1.Userno1 = userno1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.ReadOnlyMode = rdonly;
            if (ReadOnlyMode)
            {
                //ucMeritzPan11.SetReadOnlyMode(rdonly);

                userno1.SetReadOnlyMode(rdonly);
                meritzAccident1.SetReadOnlyMode(rdonly);
                ucMeritzPan21.SetReadOnlyMode(rdonly);
                ucMeritzPan31.SetReadOnlyMode(rdonly);
                ucMeritzPan41.SetReadOnlyMode(rdonly);
                ucMeritzPan51.SetReadOnlyMode(rdonly);
                db6LongCnts1.rtbDoc.ReadOnly = rdonly;
                db6LongCnts2.rtbDoc.ReadOnly = rdonly;
                db6LongCnts3.rtbDoc.ReadOnly = rdonly;
            }
        }

        private void ClearAll()
        {
            //수정금지
            ucMeritzPan11.SetReadOnlyMode(true);

            userno1.Clear();
            meritzAccident1.Clear();
            ucMeritzPan11.Clear();
            ucMeritzPan21.Clear();
            ucMeritzPan31.Clear();
            ucMeritzPan41.Clear();
            ucMeritzPan51.Clear();

            db6LongCnts1.rtbDoc.Text = string.Empty;  // 청구내용
            db6LongCnts2.rtbDoc.Text = string.Empty;  // 민원예방활동
            db6LongCnts3.rtbDoc.Text = string.Empty;  // 조사자의견 및 조사결과

            dgv_Cltr.Rows.Clear();
            dgv_Cltr.Height = 68;
            dgv_Othr.Rows.Clear();
            dgv_Othr.Height = 68;
            dgv_Prg.Rows.Clear();
            dgv_Prg.Height = 68;
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

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMeritz.xsd";
                DataSet pds = new DataSet();
                pds.ReadXml(s_CAA_XSD);

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    pds.ReadXml(xmlReader);
                }

                custName = "";
                DataTable dt2 = pds.Tables["DataBlock2"];
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataRow drow = dt2.Rows[0];
                    ucMeritzPan11.SurvReqDt = drow["SurvReqDt"] + "";
                    ucMeritzPan11.AcptDt = drow["AcptDt"] + "";
                    ucMeritzPan11.LasRptSbmsDt = drow["LasRptSbmsDt"] + "";
                    ucMeritzPan11.Insured = drow["Insured"] + "";
                    ucMeritzPan11.AcdtNo = drow["AcdtNo"] + "";
                    ucMeritzPan11.SurvAsgnEmpName = drow["SurvAsgnEmpName"] + "";
                    ucMeritzPan11.IsrdRegno = drow["IsrdRegno1"] + "-" + drow["IsrdRegno2"];
                    ucMeritzPan11.InsurChrg = drow["InsurChrg"] + "";
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

                DataTable dt4 = pds.Tables["DataBlock4"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    DataRow drow = dt4.Rows[0];
                    ucMeritzPan21.AcdtDt = drow["AcdtDt"] + "";
                    ucMeritzPan21.ShrtCnts1 = Utils.ToMultiline(drow["ShrtCnts1"]);
                    ucMeritzPan21.LongCnts1 = Utils.ConvertToRtf(drow["LongCnts1"]);
                    ucMeritzPan21.IsrdJobGrad = Utils.ToMultiline(drow["IsrdJobGrad"]);
                    ucMeritzPan21.IsrdJobGradDmnd = Utils.ToMultiline(drow["IsrdJobGradDmnd"]);
                }

                dgv_Cltr.AllowUserToAddRows = false;
                // 담보내용
                DataTable dt5 = pds.Tables["DataBlock5"];
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DataRow drow = dt5.Rows[i];
                        dgv_Cltr.Rows.Add();
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrCltrCnts"].Value = Utils.ToMultiline(drow["CltrCnts"]);  // 담보내용
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrInsurRegsAmt"].Value = drow["InsurRegsAmt"];  // 보험가입금액
                        dgv_Cltr.Rows[dgv_Cltr.RowCount - 1].Cells["cltrEstmLosAmt"].Value = drow["EstmLosAmt"];      // 추정손해액
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

                // 청구내용 및 조사결과
                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    DataRow drow = dt6.Rows[0];
                    db6LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["LongCnts1"]);  // 청구내용
                    db6LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["LongCnts2"]);  // 민원예방활동 
                    db6LongCnts3.rtbDoc.Rtf = Utils.ConvertToRtf(drow["LongCnts3"]);  // 조사자의견 및 조사결과 
                }

                // 조사자 일자별 확인사항
                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DataRow drow = dt7.Rows[i];
                        MeritzAccidentA itm = meritzAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!ReadOnlyMode)
                {
                    meritzAccident1.AddEmptyRow();
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
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCltrSpcCtrt"].Value = drow["OthCltrSpcCtrt"];       // 담보내용
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtDt"]);                // 보험
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtExprDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtExprDt"]);         // 기간
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurSurvOpni"].Value = drow["OthInsurSurvOpni"];    // 청구내용 및 결과
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCtrtSeq"].Value = drow["OthInsurCtrtSeq"];      // 순번
                    }
                }
                if (!ReadOnlyMode)
                {
                    dgv_Othr.AllowUserToAddRows = true;
                }

                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    DataRow drow = dt9.Rows[0];
                    ucMeritzPan31.S3_LongCnts1 = Utils.ConvertToRtf(drow["S3_LongCnts1"]);
                    ucMeritzPan31.S3_LongCnts2 = Utils.ConvertToRtf(drow["S3_LongCnts2"]);
                    ucMeritzPan31.S3_LongCnts3 = Utils.ConvertToRtf(drow["S3_LongCnts3"]);
                    ucMeritzPan31.S4_LongCnts1 = Utils.ConvertToRtf(drow["S4_LongCnts1"]);
                    ucMeritzPan31.S4_LongCnts2 = Utils.ConvertToRtf(drow["S4_LongCnts2"]);
                    ucMeritzPan31.S4_LongCnts3 = Utils.ConvertToRtf(drow["S4_LongCnts3"]);
                    ucMeritzPan31.S5_LongCnts1 = Utils.ConvertToRtf(drow["S5_LongCnts1"]);
                    ucMeritzPan31.S5_LongCnts2 = Utils.ConvertToRtf(drow["S5_LongCnts2"]);
                    ucMeritzPan31.S5_LongCnts3 = Utils.ConvertToRtf(drow["S5_LongCnts3"]);

                    ucMeritzPan41.S6_LongCnts1 = Utils.ConvertToRtf(drow["S6_LongCnts1"]);
                    ucMeritzPan41.S6_LongCnts2 = Utils.ConvertToRtf(drow["S6_LongCnts2"]);
                    ucMeritzPan41.S6_LongCnts3 = Utils.ConvertToRtf(drow["S6_LongCnts3"]);
                    ucMeritzPan41.S7_LongCnts1 = Utils.ConvertToRtf(drow["S7_LongCnts1"]);
                    ucMeritzPan41.S7_LongCnts2 = Utils.ConvertToRtf(drow["S7_LongCnts2"]);
                    //ucMeritzPan41.S7_LongCnts3 = Utils.ConvertToRtf(drow["S7_LongCnts3"]);

                    ucMeritzPan51.S8_LongCnts1 = Utils.ConvertToRtf(drow["S8_LongCnts1"]);
                    ucMeritzPan51.S8_LongCnts2 = Utils.ConvertToRtf(drow["S8_LongCnts2"]);
                    ucMeritzPan51.S8_LongCnts3 = Utils.ConvertToRtf(drow["S8_LongCnts3"]);
                    ucMeritzPan51.S9_LongCnts1 = Utils.ConvertToRtf(drow["S9_LongCnts1"]);
                    ucMeritzPan51.S9_LongCnts2 = Utils.ConvertToRtf(drow["S9_LongCnts2"]);
                    //ucMeritzPan51.S9_LongCnts3 = Utils.ConvertToRtf(drow["S9_LongCnts3"]);
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
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);   // 주요내용
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetObj"].Value = drow["MetObj"];                // 면담자
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetObjRels"].Value = drow["MetObjRels"];         // 관계
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetMthd"].Value = drow["MetMthd"];              // 접촉방법
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtSeq"].Value = drow["PrgMgtSeq"];           // 순번
                    }
                }
                if (!ReadOnlyMode)
                {
                    dgv_Prg.AllowUserToAddRows = true;
                }
                dgv_Prg.AutoResizeRows();
                GridHeightResize(dgv_Prg);

                dgv_Etc.AllowUserToAddRows = false;
                // 탐문리스트
                DataTable dt11 = pds.Tables["DataBlock11"];
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    for (int i = 0; i < dt11.Rows.Count; i++)
                    {
                        DataRow drow = dt11.Rows[i];
                        dgv_Etc.Rows.Add();
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcOthInfoSeq"].Value = drow["OthInfoSeq"];    // 순번
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcLongCnts1"].Value = drow["LongCnts1"];      // 병원명
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts1"].Value = drow["ShrtCnts1"];      // 연락처
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts2"].Value = drow["ShrtCnts2"];      // 피보험자내원여부
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcShrtCnts3"].Value = drow["ShrtCnts3"];      // 비고
                        dgv_Etc.Rows[dgv_Etc.RowCount - 1].Cells["etcOthInfoGrp"].Value = drow["OthInfoGrp"];    // 그룹
                    }
                }
                if (!ReadOnlyMode)
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
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersMeritz";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMeritz.xsd";
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
                dr["InsurKeepCd"] = userno1.Rows[i].InsurKeepCd;          // 계약상태
                dr["InsurKeepCdNm"] = userno1.Rows[i].InsurKeepCdNm;       // 계약상태명
                dr["CtrtStts"] = userno1.Rows[i].CtrtStts;
                dr["CtrtSttsDt"] = userno1.Rows[i].CtrtSttsDt;
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
            dr["AcdtDt"] = ucMeritzPan21.AcdtDt;                    // 사고일자
            dr["ShrtCnts1"] = ucMeritzPan21.ShrtCnts1;               // 사고유형
            dr["LongCnts1"] = ucMeritzPan21.LongCnts1;               // 진단명
            dr["IsrdJobGrad"] = ucMeritzPan21.IsrdJobGrad;            // 가입시 직업급수
            dr["IsrdJobGradDmnd"] = ucMeritzPan21.IsrdJobGradDmnd;     // 사고시 직업급수

            // 담보내용
            dt = ds.Tables["DataBlock5"];
            for (int i = 0; i < dgv_Cltr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["CltrCnts"] = dgv_Cltr.Rows[i].Cells["cltrCltrCnts"].Value;
                dr["InsurRegsAmt"] = Utils.ToDecimal(dgv_Cltr.Rows[i].Cells["cltrInsurRegsAmt"].Value);
                dr["EstmLosAmt"] = Utils.ToDecimal(dgv_Cltr.Rows[i].Cells["cltrEstmLosAmt"].Value);
                dr["InsurNo"] = dgv_Cltr.Rows[i].Cells["cltrInsurNo"].Value;
                dr["SubSeq"] = Utils.ToInt(dgv_Cltr.Rows[i].Cells["cltrSubSeq"].Value);
            }

            // 청구내용 및 조사결과
            dt = ds.Tables["DataBlock6"];
            dr = dt.Rows.Add();
            dr["LongCnts1"] = db6LongCnts1.rtbDoc.Rtf;  // 청구내용
            dr["LongCnts2"] = db6LongCnts2.rtbDoc.Rtf;  // 민원예방활동                 
            dr["LongCnts3"] = db6LongCnts3.rtbDoc.Rtf;  // 조사자의견 및 조사결과

            // 조사자 일자별 확인사항
            dt = ds.Tables["DataBlock7"];
            for (int i = 0; i < meritzAccident1.Rows.Count; i++)
            {
                if (meritzAccident1.Rows[i].IsNewRow) continue;
                if (meritzAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = meritzAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = meritzAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureCnts"] = meritzAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = meritzAccident1.Rows[i].VstHosp;                // 의료기관
            }

            // 타사가입사항
            dt = ds.Tables["DataBlock8"];
            for (int i = 0; i < dgv_Othr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInsurCoNm"] = dgv_Othr.Rows[i].Cells["othrOthInsurCoNm"].Value;
                dr["OthCltrSpcCtrt"] = dgv_Othr.Rows[i].Cells["othrOthCltrSpcCtrt"].Value;
                dr["OthCtrtDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtDt"].Value, "yyyyMMdd");
                dr["OthCtrtExprDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtExprDt"].Value, "yyyyMMdd");
                dr["OthInsurSurvOpni"] = dgv_Othr.Rows[i].Cells["othrOthInsurSurvOpni"].Value;
                dr["OthInsurCtrtSeq"] = Utils.ToInt(dgv_Othr.Rows[i].Cells["othrOthInsurCtrtSeq"].Value);
            }

            dt = ds.Tables["DataBlock9"];
            dr = dt.Rows.Add();
            dr["S3_LongCnts1"] = ucMeritzPan31.S3_LongCnts1;
            dr["S3_LongCnts2"] = ucMeritzPan31.S3_LongCnts2;
            dr["S3_LongCnts3"] = ucMeritzPan31.S3_LongCnts3;
            dr["S4_LongCnts1"] = ucMeritzPan31.S4_LongCnts1;
            dr["S4_LongCnts2"] = ucMeritzPan31.S4_LongCnts2;
            dr["S4_LongCnts3"] = ucMeritzPan31.S4_LongCnts3;
            dr["S5_LongCnts1"] = ucMeritzPan31.S5_LongCnts1;
            dr["S5_LongCnts2"] = ucMeritzPan31.S5_LongCnts2;
            dr["S5_LongCnts3"] = ucMeritzPan31.S5_LongCnts3;

            dr["S6_LongCnts1"] = ucMeritzPan41.S6_LongCnts1;
            dr["S6_LongCnts2"] = ucMeritzPan41.S6_LongCnts2;
            dr["S6_LongCnts3"] = ucMeritzPan41.S6_LongCnts3;
            dr["S7_LongCnts1"] = ucMeritzPan41.S7_LongCnts1;
            dr["S7_LongCnts2"] = ucMeritzPan41.S7_LongCnts2;
            //dr["S7_LongCnts3"] = ucMeritzPan41.S7_LongCnts3;

            dr["S8_LongCnts1"] = ucMeritzPan51.S8_LongCnts1;
            dr["S8_LongCnts2"] = ucMeritzPan51.S8_LongCnts2;
            dr["S8_LongCnts3"] = ucMeritzPan51.S8_LongCnts3;
            dr["S9_LongCnts1"] = ucMeritzPan51.S9_LongCnts1;
            dr["S9_LongCnts2"] = ucMeritzPan51.S9_LongCnts2;
            //dr["S9_LongCnts3"] = ucMeritzPan51.S9_LongCnts3;

            // 사고조사처리과정
            dt = ds.Tables["DataBlock10"];
            for (int i = 0; i < dgv_Prg.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["PrgMgtDt"] = Utils.DateFormat(dgv_Prg.Rows[i].Cells["prgPrgMgtDt"].Value, "yyyyMMdd");
                dr["SurvGuideCnts"] = dgv_Prg.Rows[i].Cells["prgSurvGuideCnts"].Value;
                dr["MetObj"] = dgv_Prg.Rows[i].Cells["prgMetObj"].Value;
                dr["MetObjRels"] = dgv_Prg.Rows[i].Cells["prgMetObjRels"].Value;
                dr["MetMthd"] = dgv_Prg.Rows[i].Cells["prgMetMthd"].Value;
                dr["PrgMgtSeq"] = Utils.ToInt(dgv_Prg.Rows[i].Cells["prgPrgMgtSeq"].Value);
            }

            // 탐문리스트
            dt = ds.Tables["DataBlock11"];
            for (int i = 0; i < dgv_Etc.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInfoSeq"] = Utils.ToInt(dgv_Etc.Rows[i].Cells["etcOthInfoSeq"].Value);
                dr["LongCnts1"] = dgv_Etc.Rows[i].Cells["etcLongCnts1"].Value;
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
                for (int ii = meritzAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (meritzAccident1.Rows[ii].IsNewRow) continue;
                    if (meritzAccident1.Rows[ii].Gubun == "1") meritzAccident1.RemoveRow(meritzAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < userno1.Rows.Count; ii++)
                {
                    if (userno1.Rows[ii].IsNewRow) continue;
                    MeritzAccidentA itm = meritzAccident1.AddRow("", "계약일", userno1.Rows[ii].CtrtDt, userno1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                meritzAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < meritzAccident1.Rows.Count; ii++)
                {
                    if (meritzAccident1.Rows[ii].IsNewRow) continue;
                    if (meritzAccident1.Rows[ii].Gubun == "1") continue;
                    meritzAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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