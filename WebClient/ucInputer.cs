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
    public partial class ucInputer : UserControl, ISmplInputer
    {
        public ReportParam param = null;
        public  bool SmplAuth = false;

        private bool readOnlyMode = false;

        private string acptDt = "";
        private string custName = "";

        private string enter_ = "Y";
        private int    tpoint = 0;
        private int    h_size = 0;

        private string SurvHed_A410 = string.Empty;
        private string SurvHed_A420 = string.Empty;
        private string SurvHed_B311 = string.Empty;
        private string SurvHed_B312 = string.Empty;
        private string SurvHed_B313 = string.Empty;
        private string SurvHed_B321 = string.Empty;
        private string SurvHed_B322 = string.Empty;
        private string SurvHed_B411 = string.Empty;
        private string SurvHed_B412 = string.Empty;
        private string SurvHed_B413 = string.Empty;
        private string SurvHed_B451 = string.Empty;
        private string SurvHed_B452 = string.Empty;

        public ucInputer()
        {
            InitializeComponent();

            this.btnSort.Click += BtnSort_Click;
            this.dgv_file.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            this.dgv_step.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            this.dgv_dbox.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellLeave);
            this.dgv_file.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.dgv_step.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.dgv_dbox.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.dgv_file.EditingControlShowing += Grid_EditingControlShowing;
            this.dgv_step.EditingControlShowing += Grid_EditingControlShowing;
            this.dgv_dbox.EditingControlShowing += Grid_EditingControlShowing;
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
                strSql += " SELECT mnr.MinorSeq, mnr.MinorName ";
                strSql += " FROM   _TXBMSMinor AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.MajorSeq = '1902' ";
                strSql += " AND    ISNULL(mnr.IsNoUse,'') <> '1' ";
                strSql += " ORDER BY mnr.MinorSort ";
                strSql += " FOR JSON PATH ";
                DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                SetCombo(cb_bank, ds.Tables[0], "MinorSeq", "MinorName", true);

                strSql = "";
                strSql += " SELECT B.AcptDt ";
                strSql += " FROM   _TAdjSLAcptList AS B WITH(NOLOCK) ";
                strSql += " WHERE  B.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    B.AcptMgmtSeq = '" + param.AcptMgmtSeq + "' ";
                strSql += " AND    B.ReSurvAsgnNo = '" + param.ReSurvAsgnNo + "' ";
                strSql += " FOR JSON PATH ";
                ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
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

        private int GetDelayDays(string frdt, string todt)
        {
            try
            {
                string strSql = "";
                strSql += " SELECT dbo._fnAdjGetSolarWDays('" + Utils.ConvertToString(param.CompanySeq) + "', '" + frdt + "', '" + todt + "') AS CNT ";
                strSql += " FOR JSON PATH ";
                DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Utils.ToInt(ds.Tables[0].Rows[0]["CNT"]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
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
                if (dgv_dbox.CurrentCell is DataGridViewTextBoxCell && dgv_dbox.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("+~");
                    return true;
                }
                else if (dgv_file.CurrentCell is DataGridViewTextBoxCell && dgv_file.CurrentCell.IsInEditMode)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
                else if (dgv_step.CurrentCell is DataGridViewTextBoxCell && dgv_step.CurrentCell.IsInEditMode)
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
            this.dti_2_1.MouseWheel += Dti_2_1_MouseWheel;
            dgv_file.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_step.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            dgv_dbox.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_1_3_1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_1_3_2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_1_4_1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_1_4_2.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_1_5_1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_3_1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_3_4.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_3_5.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_1.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_4.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_5.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_5.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_6.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            rtf_2_4_7.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            userno2.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
        }

        private void Dti_2_1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        public void SetReadOnlyMode(bool rdonly)
        {
            this.readOnlyMode = rdonly;
            if (readOnlyMode)
            {
                // 1. 계약사항
                userno1.SetReadOnlyMode(rdonly);

                // 2. 청구사항
                dti_2_1.IsInputReadOnly = true;
                txt_2_5.ReadOnly = true;
                txt_2_2.ReadOnly = true;
                txt_2_3.ReadOnly = true;
                txt_2_4.ReadOnly = true;

                // 3. 손해사정 내용
                rtf_1_3_1.rtbDoc.ReadOnly = true;
                rtf_1_3_2.rtbDoc.ReadOnly = true;

                // 4. 손해사정 의견
                rtf_1_4_1.rtbDoc.ReadOnly = true;
                rtf_1_4_2.rtbDoc.ReadOnly = true;

                // 5. 약관규정 및 관련법규
                rtf_1_5_1.rtbDoc.ReadOnly = true;

                userno2.SetReadOnlyMode(rdonly);

                // 3. 조사결과 - 확인사항 ( 첨부자료 )
                rtf_2_3_1.rtbDoc.ReadOnly = true;
                rtf_2_3_4.rtbDoc.ReadOnly = true;
                rtf_2_3_5.rtbDoc.ReadOnly = true;

                // 4. 세부 조사 내용 ( 피보험자 면담사항) ( 첨부자료 )
                rtf_2_4_1.rtbDoc.ReadOnly = true;
                rtf_2_4_4.rtbDoc.ReadOnly = true;

                // 4. 보험급 지급 ( 첨부자료 )
                cb_bank.Enabled = false;
                bank_01.ReadOnly = true;
                bank_02.ReadOnly = true;
                bank_03.ReadOnly = true;

                // 4. 타 보험사 확인 사항 ( 첨부자료 )
                rtf_2_4_5.rtbDoc.ReadOnly = true;

                // 4. 세부 조사 내용 – 민원예방활동 ( 첨부자료 )
                rtf_2_4_6.rtbDoc.ReadOnly = true;
                rtf_2_4_7.rtbDoc.ReadOnly = true;

                ini_111.IsInputReadOnly = true;
                ini_112.IsInputReadOnly = true;
                ini_113.IsInputReadOnly = true;

                // 2. 지급조사수수료
                ini_114.IsInputReadOnly = true;
                ini_115.IsInputReadOnly = true;
                ini_116.IsInputReadOnly = true;
                ini_117.IsInputReadOnly = true;
            }
        }

        private void ClearAll()
        {
            // 1. 계약사항
            userno1.Clear();

            // 2. 청구사항
            dti_2_1.ValueObject = null;            // 사고일자
            txt_2_5.Text        = string.Empty;    // 사고시간
            txt_2_2.Text        = string.Empty;    // 청구사유
            txt_2_3.Text        = string.Empty;    // 청구병원명
            txt_2_4.Text        = string.Empty;    // 진단명
                                                   
            // 3. 손해사정 내용
            rtf_1_3_1.rtbDoc.Text = string.Empty;  // 청구내용
            rtf_1_3_2.rtbDoc.Text = string.Empty;  // 확인내용 

            // 4. 손해사정 의견
            SurvHed_A410 = string.Empty;
            rtf_1_4_1.rtbDoc.Text = string.Empty;  // 보험금 지급 여부
            SurvHed_A420 = string.Empty;
            rtf_1_4_2.rtbDoc.Text = string.Empty;  // 계약전 알릴의무 위반여부

            // 5. 약관규정 및 관련법규
            rtf_1_5_1.rtbDoc.Text = string.Empty;  // 약관규정 및 관련법규


            // 2. 사고경위 ( 첨부자료 )
            // 5. 별첨자료
            userno2.Clear();

            // 3. 조사결과 - 확인사항 ( 첨부자료 )
            SurvHed_B311 = string.Empty;
            rtf_2_3_1.rtbDoc.Text = string.Empty;   // 청구 건 관련 청구병원 확인사항
            SurvHed_B312          = string.Empty;   
            SurvHed_B313          = string.Empty;   
            SurvHed_B321          = string.Empty;   
            rtf_2_3_4.rtbDoc.Text = string.Empty;   // 결론 - 보험금 지급여부
            SurvHed_B322          = string.Empty;   
            rtf_2_3_5.rtbDoc.Text = string.Empty;   // 결론 - 계약전 알릴의무 위반여부

            // 4. 세부 조사 내용 ( 피보험자 면담사항) ( 첨부자료 )
            SurvHed_B411          = string.Empty;
            rtf_2_4_1.rtbDoc.Text = string.Empty;   // 직업 및 생활환경 
            SurvHed_B412          = string.Empty;
            SurvHed_B413          = string.Empty;
            rtf_2_4_4.rtbDoc.Text = string.Empty;   // 모집경위서 주요내용

            // 4. 보험급 지급 ( 첨부자료 )
            cb_bank.Text = string.Empty;            // 지급은행명
            bank_01.Text = string.Empty;            // 계좌번호
            bank_02.Text = string.Empty;            // 예금주
            bank_03.Text = string.Empty;            // 피보험자와의 관계

            if (cb_bank.Items.Count > 0)
                cb_bank.SelectedIndex = 0;

            // 4. 타 보험사 확인 사항 ( 첨부자료 )
            rtf_2_4_5.rtbDoc.Text = string.Empty;   // 타 보험사 확인 사항

            // 4. 세부 조사 내용 – 민원예방활동 ( 첨부자료 )
            SurvHed_B451 = string.Empty;
            rtf_2_4_6.rtbDoc.Text = string.Empty;   // 조사과정 중 고객불만
            SurvHed_B452 = string.Empty;
            rtf_2_4_7.rtbDoc.Text = string.Empty;   // 불만사항에 대한 조치

            // 5. 별첨자료
            dgv_file.Rows.Clear();
            dgv_file.Height = 48;
            // 1. 처리과정 - 사고처리과정표 
            dgv_step.Rows.Clear();
            dgv_step.Height = 48;

            ini_111.ValueObject = 0;      // 총 처리일수 
            ini_112.ValueObject = 0;      // 총 지연일수 ( 제외 )
            ini_113.ValueObject = 0;      // 총 귀책일수  

            // 2. 지급조사수수료
            ini_114.ValueObject = 0.00;   // 수수료
            ini_115.ValueObject = 0.00;   // 교통비
            ini_116.ValueObject = 0.00;   // 제경비
            ini_117.ValueObject = 0.00;   // 합계

            // 사진 이미지               
            dgv_dbox.Rows.Clear();
        }

        private void XmlData_Read(DataSet yds)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearAll();

                string xml = yds.GetXml();

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersCS.xsd";
                DataSet pds = new DataSet();
                pds.ReadXml(s_CAA_XSD);

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    pds.ReadXml(xmlReader);
                }
                // 1. 계약사항
                DataTable SA01 = pds.Tables["DataBlock9"];
                if (SA01 != null && SA01.Rows.Count > 0)
                {
                    for (int i = 0; i < SA01.Rows.Count; i++)
                    {
                        DataRow drow = SA01.Rows[i];
                        userno1.AddRow(drow);
                    }
                }
                if (!readOnlyMode)
                {
                    userno1.AddEmptyRow();
                }
                userno1.Sort();

                // 2. 청구사항
                custName = "";
                DataTable SA02 = pds.Tables["DataBlock2"];
                if (SA02 != null && SA02.Rows.Count > 0)
                {
                    DataRow drow = SA02.Rows[0];

                    DateTime mydate = Utils.ConvertToDateTime(drow["AcdtDt"]);

                    dti_2_1.ValueObject = mydate;               // 사고일자
                    txt_2_5.Text = drow["AcdtTm"] + "";         // 사고시간
                    txt_2_2.Text = drow["InsurDmndResn"] + "";  // 청구사유
                    txt_2_3.Text = drow["MjrVstHosp"] + "";  // 청구병원명
                    txt_2_4.Text = drow["MjrDgnsNm"] + "";  // 진단명

                    if (drow["CustSeq"] + "" == "17")           // 교원공제  
                    {
                        pan_susuryo1.Visible = true;
                        pan_susuryo2.Visible = true;
                    }
                    custName = drow["CustName"] + "";
                }

                // 3. 손해사정 내용
                DataTable SA03 = pds.Tables["DataBlock3"];
                if (SA03 != null && SA03.Rows.Count > 0)
                {
                    DataRow drow = SA03.Rows[0];

                    rtf_1_3_1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_A310"]);  // 청구내용
                    rtf_1_3_2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_A320"]);  // 확인내용 
                }

                // 4. 손해사정 의견
                DataTable SA04 = pds.Tables["DataBlock3"];
                if (SA04 != null && SA04.Rows.Count > 0)
                {
                    DataRow drow = SA04.Rows[0];
                    SurvHed_A410 = drow["SurvHed_A410"] + "";
                    rtf_1_4_1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_A410"]);  // 보험금 지급 여부
                    SurvHed_A420 = drow["SurvHed_A420"] + "";
                    rtf_1_4_2.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_A420"]);  // 계약전 알릴의무 위반여부
                }

                // 5. 약관규정 및 관련법규
                DataTable SA05 = pds.Tables["DataBlock3"];
                if (SA05 != null && SA05.Rows.Count > 0)
                {
                    DataRow drow = SA05.Rows[0];

                    rtf_1_5_1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_A500"]);  // 약관규정 및 관련법규
                }

                // 2. 사고경위 ( 첨부자료 )
                DataTable SA06 = pds.Tables["DataBlock4"];
                if (SA06 != null && SA06.Rows.Count > 0)
                {
                    for (int i = 0; i < SA06.Rows.Count; i++)
                    {
                        DataRow drow = SA06.Rows[i];
                        accidentA itm = userno2.AddRow(drow["CureFrDt"], drow["CureSeq"], drow["CureCnts"], drow["ObjAgnc"]);
                        itm.Gubun = "2";
                    }
                }
                if (!readOnlyMode)
                {
                    userno2.AddEmptyRow();
                }
                SortUserno2();

                // 3. 조사결과 - 확인사항 ( 첨부자료 )
                DataTable SA07 = pds.Tables["DataBlock3"];
                if (SA07 != null && SA07.Rows.Count > 0)
                {
                    DataRow drow = SA07.Rows[0];
                    SurvHed_B311 = drow["SurvHed_B311"] + "";
                    rtf_2_3_1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B311"]);  // 청구 건 관련 청구병원 확인사항
                    SurvHed_B312 = drow["SurvHed_B312"] + "";
                    SurvHed_B313 = drow["SurvHed_B313"] + "";

                    SurvHed_B321 = drow["SurvHed_B321"] + "";
                    rtf_2_3_4.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B321"]);  // 결론 - 보험금 지급여부
                    SurvHed_B322 = drow["SurvHed_B322"] + "";
                    rtf_2_3_5.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B322"]);  // 결론 - 계약전 알릴의무 위반여부
                }

                // 4. 세부 조사 내용 ( 피보험자 면담사항) ( 첨부자료 )
                DataTable SA08 = pds.Tables["DataBlock3"];
                if (SA08 != null && SA08.Rows.Count > 0)
                {
                    DataRow drow = SA08.Rows[0];

                    SurvHed_B411 = drow["SurvHed_B411"] + "";
                    rtf_2_4_1.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B411"]);   // 직업 및 생활환경 
                    SurvHed_B412 = drow["SurvHed_B412"] + "";
                    SurvHed_B413 = drow["SurvHed_B413"] + "";

                    // 모집경위서 청구여부는 DataBlock2에 있음
                    rtf_2_4_4.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B422"]);   // 모집경위서 주요내용
                }

                // 4. 보험급 지급 ( 첨부자료 )
                DataTable SA09 = pds.Tables["DataBlock2"];
                if (SA09 != null && SA09.Rows.Count > 0)
                {
                    DataRow drow = SA09.Rows[0];

                    Utils.SetComboSelectedValue(cb_bank, drow["GivObjBankCd"] + "", "MinorSeq");  // 지급은행명
                    bank_01.Text = drow["GivObjAcntNum"] + "";  // 계좌번호
                    bank_02.Text = drow["GivAcntOwn"] + "";  // 예금주
                    bank_03.Text = drow["GivObjRels"] + "";  // 피보험자와의 관계
                }

                // 4. 타 보험사 확인 사항 ( 첨부자료 )
                DataTable SA10 = pds.Tables["DataBlock3"];
                if (SA10 != null && SA10.Rows.Count > 0)
                {
                    DataRow drow = SA10.Rows[0];

                    rtf_2_4_5.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B441"]);  // 타 보험사 확인 사항
                }

                // 4. 세부 조사 내용 – 민원예방활동 ( 첨부자료 )
                DataTable SA11 = pds.Tables["DataBlock3"];
                if (SA11 != null && SA11.Rows.Count > 0)
                {
                    DataRow drow = SA11.Rows[0];
                    SurvHed_B451 = drow["SurvHed_B451"] + "";
                    rtf_2_4_6.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B451"]);  // 조사과정 중 고객불만
                    SurvHed_B452 = drow["SurvHed_B452"] + "";
                    rtf_2_4_7.rtbDoc.Rtf = Utils.ConvertToRtf(drow["SurvCnts_B452"]);  // 불만사항에 대한 조치
                }

                dgv_file.AllowUserToAddRows = false;
                // 5. 별첨자료
                DataTable SA12 = pds.Tables["DataBlock5"];
                if (SA12 != null && SA12.Rows.Count > 0)
                {
                    for (int i = 0; i < SA12.Rows.Count; i++)
                    {
                        DataRow drow = SA12.Rows[i];

                        dgv_file.Rows.Add();
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells[0].Value = drow["FileSavSerl"]; // 번호
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells[1].Value = drow["FileCnts"];    // 첨부내용
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells[2].Value = drow["FileRels"];    // 확인처
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells[3].Value = drow["FileCntsCn"];  // 매수
                        dgv_file.Rows[dgv_file.RowCount - 1].Cells[4].Value = drow["FileRmk"];     // 비고                     
                    }
                }
                if (!readOnlyMode)
                { 
                    dgv_file.AllowUserToAddRows = true;
                }

                dgv_step.AllowUserToAddRows = false;
                // 1. 처리과정 - 사고처리과정표 
                DataTable SA13 = pds.Tables["DataBlock6"];
                if (SA13 != null && SA13.Rows.Count > 0)
                {
                    for (int i = 0; i < SA13.Rows.Count; i++)
                    {
                        DataRow drow = SA13.Rows[i];

                        dgv_step.Rows.Add();

                        DateTime mydate = Utils.ConvertToDateTime(drow["PrgMgtDt"]);

                        dgv_step.Rows[dgv_step.RowCount - 1].Cells[0].Value = mydate;                   // 일자
                        dgv_step.Rows[dgv_step.RowCount - 1].Cells[1].Value = drow["DayCnt"];           // 일수
                        dgv_step.Rows[dgv_step.RowCount - 1].Cells[2].Value = drow["PrgMgtHed"];        // 항목
                        dgv_step.Rows[dgv_step.RowCount - 1].Cells[3].Value = drow["SurvGuideCnts"];    // 내용
                        dgv_step.Rows[dgv_step.RowCount - 1].Cells[4].Value = drow["IsrdResnDlyDay"];   // 지연일수

                        ini_111.ValueObject = Utils.ToInt(ini_111.Value) + Utils.ToInt(dgv_step.Rows[dgv_step.RowCount - 1].Cells[1].Value);
                        ini_112.ValueObject = Utils.ToInt(ini_112.Value) + Utils.ToInt(dgv_step.Rows[dgv_step.RowCount - 1].Cells[4].Value);
                        ini_113.ValueObject = Utils.ToInt(ini_111.Value) - Utils.ToInt(ini_112.Value);
                    }
                }
                if (!readOnlyMode)
                {
                    dgv_step.AllowUserToAddRows = true;
                }

                // 2. 지급조사수수료
                DataTable SA15 = pds.Tables["DataBlock7"];
                if (SA15 != null && SA15.Rows.Count > 0)
                {
                    DataRow drow = SA15.Rows[0];

                    ini_114.ValueObject = drow["FeeTot"];         // 수수료
                    ini_115.ValueObject = drow["TrafficTot"];     // 교통비
                    ini_116.ValueObject = drow["ExpenseTot"];     // 제경비
                    ini_117.ValueObject = drow["FeeTotalSum"];    // 합계
                }

                // 사진 이미지               
                
                DataTable SA16 = pds.Tables["DataBlock8"];
                if (SA16 != null && SA16.Rows.Count > 0)
                {
                    for (int i = 0; i < SA16.Rows.Count; i++)
                    {
                        DataRow drow = SA16.Rows[i];

                        string strImage = drow["AcdtPictPath"].ToString();
                        Image img = Utils.stringToImage(strImage);
                        dgv_dbox.Rows.Add();
                        dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[0].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero); ;   // 사진
                        dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[1].Value = drow["AcdtPictCnts"];              // 정보    
                        dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[2].Value = strImage;                          // 원본
                        dgv_dbox.AutoResizeRow(dgv_dbox.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                    }
                }
                GridHeightResize(dgv_dbox);

                DataTable DB10 = pds.Tables["DataBlock10"];
                if (DB10 != null && DB10.Rows.Count > 0)
                {
                    ini_111.ValueObject = DB10.Rows[0]["D01_1"];
                    ini_112.ValueObject = DB10.Rows[0]["D01_2"];
                    ini_113.ValueObject = DB10.Rows[0]["D01_3"];
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
                if (grd.CurrentCell.ReadOnly && grd.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                if (grd.CurrentCell.ReadOnly)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                if (!grd.CurrentCell.ReadOnly)
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

        private void dgv_dbox_DragDrop(object sender, DragEventArgs e)
        {
            try
            { 
                if (readOnlyMode) return;
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string P_File in FileList)
                    dbox_file_add(P_File);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgv_dbox_DragEnter(object sender, DragEventArgs e)
        {
            try
            { 
                if (readOnlyMode) return;
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

        private void dbox_file_add(string P_File)
        {
            if (readOnlyMode) return;
            FileInfo F_Info = new FileInfo(P_File);

            try
            {
                byte[] imgData = System.IO.File.ReadAllBytes(F_Info.FullName);

                dgv_dbox.Rows.Add();

                MemoryStream ms = new MemoryStream(imgData);
                dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[0].Value = Image.FromStream(ms).GetThumbnailImage(120, 80, null, IntPtr.Zero);
                dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[1].Value = "";
                dgv_dbox.Rows[dgv_dbox.RowCount - 1].Cells[2].Value = Convert.ToBase64String(imgData);
                ms.Close();

                dgv_dbox.AutoResizeRow(dgv_dbox.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
            }
            catch {  }

            GridHeightResize(dgv_dbox);
        }

        private void BtnFileA_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (string.IsNullOrEmpty(Program.G_UP_Path))
                    openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                else
                    openFileDialog.InitialDirectory = Program.G_UP_Path;

                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string P_File in openFileDialog.FileNames)
                        dbox_file_add(P_File);

                }
            }
            panImage.Focus();
        }

        private void BtnFileD_Click(object sender, EventArgs e)
        {
            if (readOnlyMode) return;
            if (dgv_dbox.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("선택된 " + dgv_dbox.SelectedRows.Count.ToString() + "개 파일을 정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgv_dbox.SelectedRows)
                        dgv_dbox.Rows.Remove(row);
                    GridHeightResize(dgv_dbox);
                }
            }
            panImage.Focus();
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

            int row_num = e.RowIndex;

            if      (dgv.Tag.ToString() == "2")
                dgv.Rows[e.RowIndex].Cells[0].Value = dgv.RowCount;
            else if (dgv.Tag.ToString() == "3")
            {
                dgv.Rows[e.RowIndex].Cells[1].Value = 0;
                dgv.Rows[e.RowIndex].Cells[4].Value = 0;
            }
            else if (dgv.Tag.ToString() == "4")
            {
                BtnFileD.Enabled = true;
                row_num = e.RowIndex;
            }

            dgv.AutoResizeRow(row_num, DataGridViewAutoSizeRowMode.AllCells);

            dgv.Height = dgv.Height + dgv.Rows[row_num].Height;
        }

        private void Grid_RowDel(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                dgv.Height = dgv.Height - dgv.Rows[e.RowIndex].Height;
            }
            catch { }


            if (dgv.Tag.ToString() == "2")
            {
                for (int i = 0; i < dgv.RowCount - 1; i++)
                    dgv.Rows[i].Cells[0].Value = i + 1;
            }
            if (dgv.Tag.ToString() == "3")
            {
                try
                {
                    int num_01 = 0;
                    int num_02 = 0;

                    for (int i = 0; i < dgv.RowCount - 1; i++)
                    {
                        if (dgv.Rows[i].Cells[1].Value == null) dgv.Rows[i].Cells[1].Value = 0;
                        if (dgv.Rows[i].Cells[4].Value == null) dgv.Rows[i].Cells[4].Value = 0;

                        num_01 = num_01 + Utils.ToInt(dgv.Rows[i].Cells[1].Value);
                        num_02 = num_02 + Utils.ToInt(dgv.Rows[i].Cells[4].Value);
                    }
                    ini_111.Value = num_01;
                    ini_112.Value = num_02;
                    ini_113.Value = num_01 - num_02;
                }
                catch { }
            }
            if (dgv.Tag.ToString() == "4" && dgv.RowCount == 0)
                BtnFileD.Enabled = false;
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
                                if (dgv == dgv_dbox) GridHeightResize(dgv_dbox);
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
        private void dgv_dbox_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            { 
                dgv_dbox.AutoResizeRow(dgv_dbox.CurrentRow.Index, DataGridViewAutoSizeRowMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgv_dbox_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                dgv_dbox.AutoResizeRow(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
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

        private void GridEditSetting(DataGridView grd)
        {
            foreach (DataGridViewRow dr in grd.Rows)
            {
                if (dr.Cells[5].Value + "" == "1")
                {
                    dr.DefaultCellStyle.BackColor = Color.LightGreen;
                    dr.ReadOnly = true;
                }
                else
                {
                    dr.DefaultCellStyle.BackColor = SystemColors.Window;
                    dr.ReadOnly = false;
                }
            }
        }

        private void cb_bank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bank.SelectedIndex > 0)
            {
                bank_01.Enabled = true;
                bank_02.Enabled = true;
                bank_03.Enabled = true;

                bank_01.Focus();
            }
            else
            {
                bank_01.Enabled = false;
                bank_02.Enabled = false;
                bank_03.Enabled = false;

                flp_Panel_1.Focus();
            }
        }
        private void chk_9_1_CheckedChanged(object sender, EventArgs e)
        {
            flp_Panel_1.Focus();
        }

        private void dgv_step_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 4)
                {
                    try
                    {
                        if (e.ColumnIndex == 0)
                        {
                            for (int i = 0; i < dgv_step.RowCount - 1; i++)
                            {
                                string frdt = (i == 0 ? acptDt : Utils.DateFormat(dgv_step.Rows[i - 1].Cells[e.ColumnIndex].Value, "yyyyMMdd"));
                                string todt = Utils.DateFormat(dgv_step.Rows[i].Cells[e.ColumnIndex].Value, "yyyyMMdd");
                                dgv_step.Rows[i].Cells[e.ColumnIndex + 1].Value = GetDelayDays(frdt, todt);
                            }
                        }

                        int num_01 = 0;
                        int num_02 = 0;
                        for (int i = 0; i < dgv_step.RowCount - 1; i++)
                        {
                            if (dgv_step.Rows[i].Cells[1].Value == null) dgv_step.Rows[i].Cells[1].Value = 0;
                            if (dgv_step.Rows[i].Cells[4].Value == null) dgv_step.Rows[i].Cells[4].Value = 0;

                            num_01 = num_01 + Utils.ToInt(dgv_step.Rows[i].Cells[1].Value);
                            num_02 = num_02 + Utils.ToInt(dgv_step.Rows[i].Cells[4].Value);
                        }
                        ini_111.Value = num_01;
                        ini_112.Value = num_02;
                        ini_113.Value = num_01 - num_02;
                    }
                    catch { }
                }
            }
        }

        public void XmlData_Save(bool bNoMessage = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCS";
                security.methodId  = "Save";
                security.companySeq = param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = param.UserID;

                DataSet ds = GetSaveData();
                
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                //DataSet yds = YLWService.YLWDBService.CallYlwServiceCallPost(security, ds);
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisCclsRprtMngPersCS.xsd";
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

            dr["InsurValue"] = 0;   //Decimal Type 데이타가 없으면 에러남 (Error converting data type nvarchar to numeric)
            dr["AcdtDt"] = Utils.DateFormat(dti_2_1.Value, "yyyyMMdd");
            dr["AcdtTm"] = txt_2_5.Text;
            dr["InsurDmndResn"] = txt_2_2.Text;
            dr["MjrVstHosp"] = txt_2_3.Text;
            dr["MjrDgnsNm"] = txt_2_4.Text;

            dr["RaisResnFg"] = "";

            dr["GivObjBankCd"] = Utils.GetComboSelectedValue(cb_bank, "MinorSeq");
            dr["GivObjAcntNum"] = bank_01.Text;
            dr["GivAcntOwn"] = bank_02.Text;
            dr["GivObjRels"] = bank_03.Text;

            // RTF
            dt = ds.Tables["DataBlock3"];
            dr = dt.Rows.Add();
            // 3. 손해사정 내용
            dr["SurvCnts_A310"] = rtf_1_3_1.rtbDoc.Rtf;  // 청구내용
            dr["SurvCnts_A320"] = rtf_1_3_2.rtbDoc.Rtf;  // 확인내용                 
            // 4. 손해사정 의견           
            dr["SurvHed_A410"] = SurvHed_A410;
            dr["SurvCnts_A410"] = rtf_1_4_1.rtbDoc.Rtf;  // 보험금 지급 여부
            dr["SurvHed_A420"] = SurvHed_A420;
            dr["SurvCnts_A420"] = rtf_1_4_2.rtbDoc.Rtf;  // 계약전 알릴의무 위반여부                

            dr["SurvCnts_A500"] = rtf_1_5_1.rtbDoc.Rtf;  // 약관규정 및 관련법규                

            // 3. 조사결과 - 확인사항 ( 첨부자료 )          
            dr["SurvHed_B311"] = SurvHed_B311;
            dr["SurvCnts_B311"] = rtf_2_3_1.rtbDoc.Rtf;  // 청구 건 관련 청구병원 확인사항
            dr["SurvHed_B312"] = SurvHed_B312;
            dr["SurvCnts_B312"] = "";  // 계약 전 알릴 의무 관련 과거 병력 확인 사항
            dr["SurvHed_B313"] = SurvHed_B313;
            dr["SurvCnts_B313"] = "";  // 후유장해 적정성 확인 
            dr["SurvHed_B321"] = SurvHed_B321;
            dr["SurvCnts_B321"] = rtf_2_3_4.rtbDoc.Rtf;  // 결론 - 보험금 지급여부
            dr["SurvHed_B322"] = SurvHed_B322;
            dr["SurvCnts_B322"] = rtf_2_3_5.rtbDoc.Rtf;  // 결론 - 계약전 알릴의무 위반여부                

            // 4. 세부 조사 내용 ( 피보험자 면담사항) ( 첨부자료 )
            dr["SurvHed_B411"] = SurvHed_B411;
            dr["SurvCnts_B411"] = rtf_2_4_1.rtbDoc.Rtf;  // 직업 및 생활환경 
            dr["SurvHed_B412"] = SurvHed_B412;
            dr["SurvCnts_B412"] = "";  // 보험가입경위
            dr["SurvHed_B413"] = SurvHed_B413;
            dr["SurvCnts_B413"] = "";  // 가입 전 병력 및 주요사항 고지여부                                                                     
            dr["SurvCnts_B422"] = rtf_2_4_4.rtbDoc.Rtf;  // 모집경위서 주요내용                
            dr["SurvCnts_B441"] = rtf_2_4_5.rtbDoc.Rtf;  // 타 보험사 확인 사항                

            // 4. 세부 조사 내용 – 민원예방활동 ( 첨부자료 )
            dr["SurvHed_B451"] = SurvHed_B451;
            dr["SurvCnts_B451"] = rtf_2_4_6.rtbDoc.Rtf;  // 조사과정 중 고객불만
            dr["SurvHed_B452"] = SurvHed_B452;
            dr["SurvCnts_B452"] = rtf_2_4_7.rtbDoc.Rtf;  // 불만사항에 대한 조치

            object str = string.Empty;
            object amt = 0.00;

            // 사고경위
            dt = ds.Tables["DataBlock4"];     // 2. 사고경위                
            for (int i = 0; i < userno2.Rows.Count; i++)
            {
                if (userno2.Rows[i].IsNewRow) continue;
                if (userno2.Rows[i].Gubun == "1") continue;

                dr = dt.Rows.Add();

                dr["CureFrDt"] = userno2.Rows[i].CureFrDt;              // 치료기간 From
                dr["CureSeq"] = userno2.Rows[i].CureSeq;                // 순번
                dr["CureCnts"] = userno2.Rows[i].CureCnts;              // 치료내용
                dr["ObjAgnc"] = userno2.Rows[i].ObjAgnc;                // 의료기관
            }

            // 별첨자료
            dt = ds.Tables["DataBlock5"];     // 5. 별첨자료
            for (int i = 0; i < dgv_file.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();

                //dr["FileTypCd"]   = string.Empty;
                dr["FileSavSerl"] = i + 1;
                dr["FileCnts"] = dgv_file.Rows[i].Cells[1].Value;
                dr["FileRels"] = dgv_file.Rows[i].Cells[2].Value;
                dr["FileCntsCn"] = Utils.ToInt(dgv_file.Rows[i].Cells[3].Value);
                dr["FileRmk"] = dgv_file.Rows[i].Cells[4].Value;
            }

            // 1. 처리과정 사고처리과정표
            dt = ds.Tables["DataBlock6"];     //    처리과정
            for (int i = 0; i < dgv_step.RowCount - 1; i++)
            {
                dr = dt.Rows.Add();

                dr["PrgMgtDt"] = Utils.DateFormat(dgv_step.Rows[i].Cells[0].Value, "yyyyMMdd");
                dr["DayCnt"] = dgv_step.Rows[i].Cells[1].Value;
                dr["PrgMgtHed"] = dgv_step.Rows[i].Cells[2].Value;
                dr["SurvGuideCnts"] = dgv_step.Rows[i].Cells[3].Value;
                dr["IsrdResnDlyDay"] = dgv_step.Rows[i].Cells[4].Value;
            }

            // 2. 지급조사수수료
            dt = ds.Tables["DataBlock7"];
            dr = dt.Rows.Add();
            dr["FeeTot"] = ini_114.Value;
            dr["TrafficTot"] = ini_115.Value;
            dr["ExpenseTot"] = ini_116.Value;
            dr["FeeTotalSum"] = ini_117.Value;

            // 사진 이미지
            dt = ds.Tables["DataBlock8"];     // 사진
            for (int i = 0; i < dgv_dbox.RowCount; i++)
            {
                dr = dt.Rows.Add();
                dr["AcdtPictPath"] = dgv_dbox.Rows[i].Cells[2].Value;
                dr["AcdtPictCnts"] = dgv_dbox.Rows[i].Cells[1].Value;
                dr["AcdtPictSeq"] = (i + 1).ToString();
            }

            // 1. 계약사항
            dt = ds.Tables["DataBlock9"];     // 1. 계약사항                

            for (int i = 0; i < userno1.Rows.Count; i++)
            {
                if (userno1.Rows[i].IsNewRow) continue;

                dr = dt.Rows.Add();

                dr["InsurPrdt"] = userno1.Rows[i].InsurPrdt;          // 보험종목
                dr["InsurNo"] = userno1.Rows[i].InsurNo;              // 증권번호
                dr["CtrtDt"] = userno1.Rows[i].CtrtDt;                // 계약일자
                dr["Insurant"] = userno1.Rows[i].Insurant;            // 계약자
                dr["Insured"] = userno1.Rows[i].Insured;              // 피보험자
                dr["Bnfc"] = userno1.Rows[i].Bnfc;                    // 수익자
                dr["IsrdJob"] = userno1.Rows[i].IsrdJob;              // 직업
                dr["CltrCnts"] = userno1.Rows[i].CltrCnts;            // 담보내용
                dr["InsurValue"] = Utils.ToDecimal(userno1.Rows[i].InsurValue);        // 담보금액
                dr["CtrtExprDt"] = userno1.Rows[i].CtrtExprDt;
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

            // 2. 지급조사수수료
            dt = ds.Tables["DataBlock10"];
            dr = dt.Rows.Add();
            dr["D01_1"] = ini_111.Value;      // 총 처리일수 
            dr["D01_2"] = ini_112.Value;      // 총 지연일수 ( 제외 )
            dr["D01_3"] = ini_113.Value;      // 총 귀책일수  

            return ds;
        }

        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            SortUserno2();
        }

        public void SortUserno2()
        { 
            try
            {
                for (int ii = userno2.Rows.Count - 1; ii >= 0; ii--)
                {
                    if (userno2.Rows[ii].IsNewRow) continue;
                    if (userno2.Rows[ii].Gubun == "1") userno2.RemoveRow(userno2.Rows[ii]);
                }
                for (int ii = 0; ii < userno1.Rows.Count; ii++)
                {
                    if (userno1.Rows[ii].IsNewRow) continue;
                    accidentA itm = userno2.AddRow(userno1.Rows[ii].CtrtDt, "", userno1.Rows[ii].InsurPrdt, custName);
                    itm.Gubun = "1";    // 구분
                }
                userno2.Sort();
                int rownum = 1;
                for (int ii = 0; ii < userno2.Rows.Count; ii++)
                {
                    if (userno2.Rows[ii].IsNewRow) continue;
                    if (userno2.Rows[ii].Gubun == "1") continue;
                    userno2.Rows[ii].CureSeq = Utils.ConvertToString(rownum++);
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