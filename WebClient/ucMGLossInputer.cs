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
    public partial class ucMGLossInputer : UserControl
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool readOnlyMode = false;

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        public ucMGLossInputer()
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
                if (dgv_Othr.CurrentCell is DataGridViewTextBoxCell && dgv_Othr.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_file.CurrentCell is DataGridViewTextBoxCell && dgv_file.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_Prg.CurrentCell is DataGridViewTextBoxCell && dgv_Prg.CurrentCell.IsInEditMode)
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
            mgLossAccident1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan21.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan31.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan41.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan51.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan61.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan71.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan81.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan11.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossContract1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            mgLossAccident1.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan21.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan31.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan41.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan51.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan61.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan71.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            ucMGLossPan81.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS321_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS321_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS321_LongCnts3.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS322_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS322_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS351_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS351_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS351_LongCnts3.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS401_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS401_LongCnts2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS401_LongCnts3.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS402_LongCnts1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            txtS321_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS321_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS321_LongCnts3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS322_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS322_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS351_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS351_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS351_LongCnts3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS401_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS401_LongCnts2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS401_LongCnts3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            txtS402_LongCnts1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Text_MouseClick);
            dgv_Othr.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_file.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Prg.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_Othr.EditingControlShowing += Grid_EditingControlShowing;
            dgv_file.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Prg.EditingControlShowing += Grid_EditingControlShowing;
            dgv_Othr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_file.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Prg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            dgv_Othr.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_file.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Prg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            dgv_Othr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_file.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Prg.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            dgv_Othr.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_file.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Prg.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            dgv_Othr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_file.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Prg.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            dgv_Othr.DataError += Grid_DataError;
            dgv_file.DataError += Grid_DataError;
            dgv_Prg.DataError += Grid_DataError;

            mgLossAccident1.Userno1 = mgLossContract1;
        }
        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            if (readOnlyMode)
            {
                ucMGLossPan11.SetReadOnlyMode(rdonly);
                mgLossContract1.SetReadOnlyMode(rdonly);
                mgLossAccident1.SetReadOnlyMode(rdonly);
                ucMGLossPan21.SetReadOnlyMode(rdonly);
                ucMGLossPan31.SetReadOnlyMode(rdonly);
                ucMGLossPan41.SetReadOnlyMode(rdonly);
                ucMGLossPan51.SetReadOnlyMode(rdonly);
                ucMGLossPan61.SetReadOnlyMode(rdonly);
                ucMGLossPan71.SetReadOnlyMode(rdonly);
                ucMGLossPan81.SetReadOnlyMode(rdonly);
                txtS321_LongCnts1.SetReadOnly(rdonly);
                txtS321_LongCnts2.SetReadOnly(rdonly);
                txtS321_LongCnts3.SetReadOnly(rdonly);
                txtS322_LongCnts1.SetReadOnly(rdonly);
                txtS322_LongCnts2.SetReadOnly(rdonly);
                txtS351_LongCnts1.SetReadOnly(rdonly);
                txtS351_LongCnts2.SetReadOnly(rdonly);
                txtS351_LongCnts3.SetReadOnly(rdonly);
                txtS401_LongCnts1.SetReadOnly(rdonly);
                txtS401_LongCnts2.SetReadOnly(rdonly);
                txtS401_LongCnts3.SetReadOnly(rdonly);
                txtS402_LongCnts1.SetReadOnly(rdonly);
            }
        }

        private void ClearAll()
        {
            ucMGLossPan11.Clear();
            mgLossContract1.Clear();
            mgLossAccident1.Clear();
            ucMGLossPan21.Clear();
            ucMGLossPan31.Clear();
            ucMGLossPan41.Clear();
            ucMGLossPan51.Clear();
            ucMGLossPan61.Clear();
            ucMGLossPan71.Clear();
            ucMGLossPan81.Clear();

            txtS321_LongCnts1.rtbDoc.Rtf = "";
            txtS321_LongCnts2.rtbDoc.Rtf = "";
            txtS321_LongCnts3.rtbDoc.Rtf = "";
            txtS322_LongCnts1.rtbDoc.Rtf = "";
            txtS322_LongCnts2.rtbDoc.Rtf = "";
            txtS351_LongCnts1.rtbDoc.Rtf = "";
            txtS351_LongCnts2.rtbDoc.Rtf = "";
            txtS351_LongCnts3.rtbDoc.Rtf = "";
            txtS401_LongCnts1.rtbDoc.Rtf = "";
            txtS401_LongCnts2.rtbDoc.Rtf = "";
            txtS401_LongCnts3.rtbDoc.Rtf = "";
            txtS402_LongCnts1.rtbDoc.Rtf = "";

            dgv_Othr.Rows.Clear();
            dgv_Othr.Height = 68;
            dgv_file.Rows.Clear();
            dgv_file.Height = 68;
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

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMGLoss.xsd";
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

                DataTable dt4 = pds.Tables["DataBlock4"];
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    DataRow drow = dt4.Rows[0];
                    ucMGLossPan21.AcdtDt = drow["AcdtDt"] + "";
                    ucMGLossPan21.AcdtTm = drow["AcdtTm"] + "";
                    ucMGLossPan21.AcdtCaus = drow["AcdtCaus"] + "";
                    ucMGLossPan21.AcdtAddressName = drow["AcdtAddressName"] + "";
                    ucMGLossPan21.S111_LongCnts1 = drow["S111_LongCnts1"] + "";
                    ucMGLossPan21.S111_LongCnts2 = drow["S111_LongCnts2"] + "";
                    ucMGLossPan21.S111_ShrtCnts1 = drow["S111_ShrtCnts1"] + "";
                    ucMGLossPan21.S111_ShrtCnts2 = drow["S111_ShrtCnts2"] + "";
                    ucMGLossPan21.S111_ShrtCnts3 = drow["S111_ShrtCnts3"] + "";
                }

                // 계약사항
                DataTable dt5 = pds.Tables["DataBlock5"];
                DataTable dt6 = pds.Tables["DataBlock6"];
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DataRow drow = dt5.Rows[i];
                        string InsurNo = drow["InsurNo"] + "";
                        DataRow[] drs = dt6.Select("InsurNo = '" + InsurNo + "' ");
                        mgLossContract1.AddRow(drow, drs);
                    }
                }
                mgLossContract1.Sort();

                DataTable dt7 = pds.Tables["DataBlock7"];
                if (dt7 != null && dt7.Rows.Count > 0)
                {
                    DataRow drow = dt7.Rows[0];
                    ucMGLossPan31.S131_LongCnts1 = drow["S131_LongCnts1"] + "";
                    ucMGLossPan31.S131_LongCnts2 = drow["S131_LongCnts2"] + "";
                    ucMGLossPan31.S131_ShrtCnts1 = drow["S131_ShrtCnts1"] + "";
                    ucMGLossPan31.S131_ShrtCnts2 = drow["S131_ShrtCnts2"] + "";
                    ucMGLossPan31.IsrdJob = drow["IsrdJob"] + "";
                    ucMGLossPan31.IsrdJobDmnd = drow["IsrdJobDmnd"] + "";
                    ucMGLossPan31.IsrdJobNow = drow["IsrdJobNow"] + "";
                    ucMGLossPan31.S131_LongCnts3 = drow["S131_LongCnts3"] + "";
                }

                DataTable dt8 = pds.Tables["DataBlock8"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    DataRow drow = dt8.Rows[0];
                    ucMGLossPan41.S211_LongCnts1 = drow["S211_LongCnts1"] + "";
                    ucMGLossPan41.S211_LongCnts2 = drow["S211_LongCnts2"] + "";
                    ucMGLossPan41.S211_LongCnts3 = drow["S211_LongCnts3"] + "";
                    ucMGLossPan41.S212_LongCnts1 = drow["S212_LongCnts1"] + "";
                    ucMGLossPan41.S212_LongCnts2 = drow["S212_LongCnts2"] + "";
                    ucMGLossPan41.S212_LongCnts3 = drow["S212_LongCnts3"] + "";
                    ucMGLossPan51.S221_LongCnts1 = drow["S221_LongCnts1"] + "";
                    ucMGLossPan51.S221_LongCnts2 = drow["S221_LongCnts2"] + "";
                }

                DataTable dt9 = pds.Tables["DataBlock9"];
                if (dt9 != null && dt9.Rows.Count > 0)
                {
                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DataRow drow = dt9.Rows[i];
                        MGLossAccidentA itm = mgLossAccident1.AddRow(drow["CureSeq"], drow["Gubun"], drow["CureFrDt"], drow["CureToDt"], drow["CureCnts"], drow["VstHosp"]);
                        itm.Gubun = "2";
                    }
                }
                if (!readOnlyMode)
                {
                    mgLossAccident1.AddEmptyRow();
                }
                SortAccident1();

                dgv_Othr.AllowUserToAddRows = false;
                // 타사 가입 사항
                DataTable dt10 = pds.Tables["DataBlock10"];
                if (dt10 != null && dt10.Rows.Count > 0)
                {
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DataRow drow = dt10.Rows[i];
                        dgv_Othr.Rows.Add();
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCoNm"].Value = drow["OthInsurCoNm"];           // 보험회사
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthCtrtDt"].Value = Utils.ConvertToDateTime(drow["OthCtrtDt"]); // 가입일
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurPrdt"].Value = drow["OthInsurPrdt"];           // 상품명
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurSurvOpni"].Value = drow["OthInsurSurvOpni"];    // 처리내용
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrInsurDmndAmt"].Value = drow["InsurDmndAmt"];           // 청구금
                        dgv_Othr.Rows[dgv_Othr.RowCount - 1].Cells["othrOthInsurCtrtSeq"].Value = drow["OthInsurCtrtSeq"];      // 순번
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_Othr.AllowUserToAddRows = true;
                }

                DataTable dt11 = pds.Tables["DataBlock11"];
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drow = dt11.Rows[0];
                    txtS321_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S321_LongCnts1"]);
                    txtS321_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S321_LongCnts2"]);
                    txtS321_LongCnts3.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S321_LongCnts3"]);
                    txtS322_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S322_LongCnts1"]);
                    txtS322_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S322_LongCnts2"]);

                    ucMGLossPan61.S331_LongCnts1 = drow["S331_LongCnts1"] + "";
                    ucMGLossPan61.S331_LongCnts2 = drow["S331_LongCnts2"] + "";
                    ucMGLossPan61.S331_LongCnts3 = drow["S331_LongCnts3"] + "";
                    ucMGLossPan61.S332_LongCnts1 = drow["S332_LongCnts1"] + "";
                    ucMGLossPan61.S332_LongCnts2 = drow["S332_LongCnts2"] + "";

                    ucMGLossPan71.S341_LongCnts1 = drow["S341_LongCnts1"] + "";
                    ucMGLossPan71.S341_LongCnts2 = drow["S341_LongCnts2"] + "";
                    ucMGLossPan71.S341_LongCnts3 = drow["S341_LongCnts3"] + "";

                    txtS351_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S351_LongCnts1"]);
                    txtS351_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S351_LongCnts2"]);
                    txtS351_LongCnts3.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S351_LongCnts3"]);
                    txtS401_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S401_LongCnts1"]);
                    txtS401_LongCnts2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S401_LongCnts2"]);
                    txtS401_LongCnts3.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S401_LongCnts3"]);
                    txtS402_LongCnts1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["S402_LongCnts1"]);
                }

                dgv_file.AllowUserToAddRows = false;
                DataTable dt12 = pds.Tables["DataBlock12"];
                if (dt12 != null && dt12.Rows.Count > 0)
                {
                    for (int i = 0; i < dt12.Rows.Count; i++)
                    {
                        DataRow drow = dt12.Rows[i];
                        dgv_file.Rows.Add();
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileSavSerl"].Value = drow["FileSavSerl"]; // 번호
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileCnts"].Value = drow["FileCnts"];    // 첨부내용
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileRels"].Value = drow["FileRels"];    // 매수
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells["fileFileRmk"].Value = drow["FileRmk"];      // 비고                     
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_file.AllowUserToAddRows = true;
                }

                DataTable dt13 = pds.Tables["DataBlock13"];
                if (dt13 != null && dt13.Rows.Count > 0)
                {
                    DataRow drow = dt13.Rows[0];
                    ucMGLossPan81.InsurPrdt = drow["InsurPrdt"] + "";
                    ucMGLossPan81.AcdtDt = drow["AcdtDt"] + "";
                    ucMGLossPan81.AcdtTm = drow["AcdtTm"] + "";
                    ucMGLossPan81.Insured = drow["Insured"] + "";
                    ucMGLossPan81.InsurChrg = drow["InsurChrg"] + "";
                    ucMGLossPan81.SurvAsgnEmp = drow["SurvAsgnEmp"] + "";
                    ucMGLossPan81.LasRptSbmsDt = drow["LasRptSbmsDt"] + "";
                }

                dgv_Prg.AllowUserToAddRows = false;
                DataTable dt14 = pds.Tables["DataBlock14"];
                if (dt14 != null && dt14.Rows.Count > 0)
                {
                    for (int i = 0; i < dt14.Rows.Count; i++)
                    {
                        DataRow drow = dt14.Rows[i];
                        dgv_Prg.Rows.Add();
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtDt"].Value = Utils.ConvertToDateTime(drow["PrgMgtDt"]); // 진행일시
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtHed"].Value = drow["PrgMgtHed"];           // 항목
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgMetMthd"].Value = drow["MetMthd"];                 // 소재지
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgSurvGuideCnts"].Value = drow["SurvGuideCnts"];    // 처리내용
                        dgv_Prg.Rows[dgv_Prg.RowCount - 1].Cells["prgPrgMgtSeq"].Value = drow["PrgMgtSeq"];           // 순번
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_Prg.AllowUserToAddRows = true;
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
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersMGLoss";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersMGLoss.xsd";
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

            dt = ds.Tables["DataBlock3"];
            dr = dt.Rows.Add();
            dr["CmplPnt1"] = ucMGLossPan11.CmplPnt1;                // 민원지수1
            dr["CmplPnt2"] = ucMGLossPan11.CmplPnt2;                // 민원지수2
            dr["CmplPnt3"] = ucMGLossPan11.CmplPnt3;                // 민원지수3
            dr["CmplPnt4"] = ucMGLossPan11.CmplPnt4;                // 민원지수4
            dr["CmplPnt5"] = ucMGLossPan11.CmplPnt5;                // 민원지수5

            dt = ds.Tables["DataBlock4"];
            dr = dt.Rows.Add();
            dr["AcdtDt"] = ucMGLossPan21.AcdtDt;
            dr["AcdtTm"] = ucMGLossPan21.AcdtTm;
            dr["AcdtCaus"] = ucMGLossPan21.AcdtCaus;
            dr["AcdtAddressName"] = ucMGLossPan21.AcdtAddressName;
            dr["S111_LongCnts1"] = ucMGLossPan21.S111_LongCnts1;
            dr["S111_LongCnts2"] = ucMGLossPan21.S111_LongCnts2;
            dr["S111_ShrtCnts1"] = ucMGLossPan21.S111_ShrtCnts1;
            dr["S111_ShrtCnts2"] = ucMGLossPan21.S111_ShrtCnts2;
            dr["S111_ShrtCnts3"] = ucMGLossPan21.S111_ShrtCnts3;

            // 계약사항
            dt = ds.Tables["DataBlock5"];
            DataTable dt6 = ds.Tables["DataBlock6"];
            for (int i = 0; i < mgLossContract1.Rows.Count; i++)
            {
                if (mgLossContract1.Rows[i].IsNewRow) continue;
                dr = dt.Rows.Add();
                dr["InsurPrdt"] = mgLossContract1.Rows[i].InsurPrdt;             // 보험종목
                dr["InsurNo"] = mgLossContract1.Rows[i].InsurNo;                 // 증권번호
                dr["CtrtDt"] = mgLossContract1.Rows[i].CtrtDt;                   // 보험시기
                dr["CtrtExprDt"] = mgLossContract1.Rows[i].CtrtExprDt;            // 보험종기
                dr["Bnfc"] = mgLossContract1.Rows[i].Bnfc;                       // 수익자
                dr["Insurant"] = mgLossContract1.Rows[i].Insurant;               // 계약자명
                dr["Insured"] = mgLossContract1.Rows[i].Insured;                 // 피보험자
                dr["IsrdTel"] = mgLossContract1.Rows[i].IsrdTel;                 // 연락처
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
                dr["IsrdJob"] = mgLossContract1.Rows[i].IsrdJob;
                miMGLossContractBRows rows = mgLossContract1.Rows[i].Rows;
                for (int ii = 0; ii < rows.Count; ii++)
                {
                    if (rows[ii].IsNewRow) continue;
                    DataRow dr6 = dt6.Rows.Add();
                    dr6["CltrCnts"] = rows[ii].CltrCnts;                        // 담보내용
                    dr6["InsurRegsAmt"] = Utils.ToDecimal(rows[ii].InsurRegsAmt);  // 보험가입금액
                    dr6["InsurNo"] = dr["InsurNo"];                            // 증권번호
                }
            }

            dt = ds.Tables["DataBlock7"];
            dr = dt.Rows.Add();
            dr["S131_LongCnts1"] = ucMGLossPan31.S131_LongCnts1;
            dr["S131_LongCnts2"] = ucMGLossPan31.S131_LongCnts2;
            dr["S131_ShrtCnts1"] = ucMGLossPan31.S131_ShrtCnts1;
            dr["S131_ShrtCnts2"] = ucMGLossPan31.S131_ShrtCnts2;
            dr["IsrdJob"] = ucMGLossPan31.IsrdJob;
            dr["IsrdJobDmnd"] = ucMGLossPan31.IsrdJobDmnd;
            dr["IsrdJobNow"] = ucMGLossPan31.IsrdJobNow;
            dr["S131_LongCnts3"] = ucMGLossPan31.S131_LongCnts3;

            dt = ds.Tables["DataBlock8"];
            dr = dt.Rows.Add();
            dr["S211_LongCnts1"] = ucMGLossPan41.S211_LongCnts1;
            dr["S211_LongCnts2"] = ucMGLossPan41.S211_LongCnts2;
            dr["S211_LongCnts3"] = ucMGLossPan41.S211_LongCnts3;
            dr["S212_LongCnts1"] = ucMGLossPan41.S212_LongCnts1;
            dr["S212_LongCnts2"] = ucMGLossPan41.S212_LongCnts2;
            dr["S212_LongCnts3"] = ucMGLossPan41.S212_LongCnts3;
            dr["S221_LongCnts1"] = ucMGLossPan51.S221_LongCnts1;
            dr["S221_LongCnts2"] = ucMGLossPan51.S221_LongCnts2;

            // 손해액 범위 조사
            dt = ds.Tables["DataBlock9"];
            for (int i = 0; i < mgLossAccident1.Rows.Count; i++)
            {
                if (mgLossAccident1.Rows[i].IsNewRow) continue;
                if (mgLossAccident1.Rows[i].Gubun == "1") continue;
                dr = dt.Rows.Add();
                dr["Gubun"] = mgLossAccident1.Rows[i].Gubun;                   // 구분
                dr["CureFrDt"] = mgLossAccident1.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureToDt"] = mgLossAccident1.Rows[i].CureToDt;              // 치료기간 To
                dr["CureCnts"] = mgLossAccident1.Rows[i].CureCnts;              // 치료내용
                dr["VstHosp"] = mgLossAccident1.Rows[i].VstHosp;                // 의료기관
            }

            // 타사가입사항
            dt = ds.Tables["DataBlock10"];
            for (int i = 0; i < dgv_Othr.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["OthInsurCoNm"] = dgv_Othr.Rows[i].Cells["othrOthInsurCoNm"].Value;
                dr["OthCtrtDt"] = Utils.DateFormat(dgv_Othr.Rows[i].Cells["othrOthCtrtDt"].Value, "yyyyMMdd");
                dr["OthInsurPrdt"] = dgv_Othr.Rows[i].Cells["othrOthInsurPrdt"].Value;
                dr["OthInsurSurvOpni"] = dgv_Othr.Rows[i].Cells["othrOthInsurSurvOpni"].Value;
                dr["InsurDmndAmt"] = Utils.ToDecimal(dgv_Othr.Rows[i].Cells["othrInsurDmndAmt"].Value);
                dr["OthInsurCtrtSeq"] = Utils.ToInt(dgv_Othr.Rows[i].Cells["othrOthInsurCtrtSeq"].Value);
            }

            dt = ds.Tables["DataBlock11"];
            dr = dt.Rows.Add();
            dr["S321_LongCnts1"] = txtS321_LongCnts1.rtbDoc.Rtf;
            dr["S321_LongCnts2"] = txtS321_LongCnts2.rtbDoc.Rtf;
            dr["S321_LongCnts3"] = txtS321_LongCnts3.rtbDoc.Rtf;
            dr["S322_LongCnts1"] = txtS322_LongCnts1.rtbDoc.Rtf;
            dr["S322_LongCnts2"] = txtS322_LongCnts2.rtbDoc.Rtf;
            dr["S331_LongCnts1"] = ucMGLossPan61.S331_LongCnts1;
            dr["S331_LongCnts2"] = ucMGLossPan61.S331_LongCnts2;
            dr["S331_LongCnts3"] = ucMGLossPan61.S331_LongCnts3;
            dr["S332_LongCnts1"] = ucMGLossPan61.S332_LongCnts1;
            dr["S332_LongCnts2"] = ucMGLossPan61.S332_LongCnts2;
            dr["S341_LongCnts1"] = ucMGLossPan71.S341_LongCnts1;
            dr["S341_LongCnts2"] = ucMGLossPan71.S341_LongCnts2;
            dr["S341_LongCnts3"] = ucMGLossPan71.S341_LongCnts3;
            dr["S351_LongCnts1"] = txtS351_LongCnts1.rtbDoc.Rtf;
            dr["S351_LongCnts2"] = txtS351_LongCnts2.rtbDoc.Rtf;
            dr["S351_LongCnts3"] = txtS351_LongCnts3.rtbDoc.Rtf;
            dr["S401_LongCnts1"] = txtS401_LongCnts1.rtbDoc.Rtf;
            dr["S401_LongCnts2"] = txtS401_LongCnts2.rtbDoc.Rtf;
            dr["S401_LongCnts3"] = txtS401_LongCnts3.rtbDoc.Rtf;
            dr["S402_LongCnts1"] = txtS402_LongCnts1.rtbDoc.Rtf;

            // 별첨자료
            dt = ds.Tables["DataBlock12"];
            for (int i = 0; i < dgv_file.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["FileSavSerl"] = i + 1;
                dr["FileCnts"] = dgv_file.Rows[i].Cells["fileFileCnts"].Value;
                dr["FileRels"] = dgv_file.Rows[i].Cells["fileFileRels"].Value;
                //dr["FileCntsCn"] = Utils.ToInt(dgv_file.Rows[i].Cells["FileCntsCn"].Value);
                dr["FileRmk"] = dgv_file.Rows[i].Cells["fileFileRmk"].Value;
            }

            dt = ds.Tables["DataBlock13"];
            dr = dt.Rows.Add();
            dr["InsurPrdt"] = ucMGLossPan81.InsurPrdt;
            dr["AcdtDt"] = ucMGLossPan81.AcdtDt;
            dr["AcdtTm"] = ucMGLossPan81.AcdtTm;
            dr["Insured"] = ucMGLossPan81.Insured;
            dr["InsurChrg"] = ucMGLossPan81.InsurChrg;
            dr["SurvAsgnEmp"] = ucMGLossPan81.SurvAsgnEmp;
            dr["LasRptSbmsDt"] = ucMGLossPan81.LasRptSbmsDt;

            // 조사 일정 요약표
            dt = ds.Tables["DataBlock14"];
            for (int i = 0; i < dgv_Prg.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();
                dr["PrgMgtDt"] = Utils.DateFormat(dgv_Prg.Rows[i].Cells["prgPrgMgtDt"].Value, "yyyyMMdd");
                dr["PrgMgtHed"] = dgv_Prg.Rows[i].Cells["prgPrgMgtHed"].Value;
                dr["MetMthd"] = dgv_Prg.Rows[i].Cells["prgMetMthd"].Value;
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
                for (int ii = mgLossAccident1.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (mgLossAccident1.Rows[ii].IsNewRow) continue;
                    if (mgLossAccident1.Rows[ii].Gubun == "1") mgLossAccident1.RemoveRow(mgLossAccident1.Rows[ii]);
                }
                for (int ii = 0; ii < mgLossContract1.Rows.Count; ii++)
                {
                    if (mgLossContract1.Rows[ii].IsNewRow) continue;
                    MGLossAccidentA itm = mgLossAccident1.AddRow("", "계약일", mgLossContract1.Rows[ii].CtrtDt, "", mgLossContract1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                mgLossAccident1.Sort();
                int rownum = 1;
                for (int ii = 0; ii < mgLossAccident1.Rows.Count; ii++)
                {
                    if (mgLossAccident1.Rows[ii].IsNewRow) continue;
                    if (mgLossAccident1.Rows[ii].Gubun == "1") continue;
                    mgLossAccident1.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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