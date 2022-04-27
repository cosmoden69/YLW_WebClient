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
using YLWService.Extensions;

namespace YLW_WebClient.CAA
{
    public partial class ucLinaInputer : UserControl
    {
        public ReportParam param = null;
        public bool SmplAuth = false;

        bool _bEvent = false;
        private bool _sendableMode = true;
        private bool _printableMode = true;
        private bool _readOnlyMode = false;

        public bool SendableMode
        {
            get { return _sendableMode; }
            set { _sendableMode = value; }
        }

        public bool PrintableMode
        {
            get { return _printableMode; }
            set { _printableMode = value; }
        }

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
        private int tpoint = 0;
        private int h_size = 0;

        private int id3 { get; set; } = 0;

        public ucLinaInputer()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Utils.DoubleBuffered(SS1, true);
            Utils.DoubleBuffered(SS6, true);
            Utils.DoubleBuffered(SS7, true);
            Utils.DoubleBuffered(SS8, true);
            Utils.DoubleBuffered(SS9, true);
            Utils.DoubleBuffered(SS10, true);
            Utils.DoubleBuffered(SS11, true);
            Utils.DoubleBuffered(SS12, true);
            Utils.DoubleBuffered(SS14, true);

            this.rtfSurvDangerCnts.bShowMenu = false;
            this.rtfSurvResultCnts.bShowMenu = false;
            this.rtfReviewOpinion.bShowMenu = false;
            this.rtfSearchMatters.bShowMenu = false;
            this.rtfSurvDtlCnts.bShowMenu = false;
            this.rtfGuideCnts.bShowMenu = false;
            this.rtfSurvDangerCnts.MaxInputLength = 3000;
            this.rtfSurvResultCnts.MaxInputLength = 3000;
            this.rtfReviewOpinion.MaxInputLength = 3000;
            this.rtfSearchMatters.MaxInputLength = 3000;
            this.rtfSurvDtlCnts.MaxInputLength = 40000;
            this.rtfGuideCnts.MaxInputLength = 3000;
            this.MedHistConfCnts6.MaxInputLength = 200;
            this.MedHistDtlCnts6.MaxInputLength = 3000;
            this.OthInsurRmk7.MaxInputLength = 100;
            this.CmplGuideTgtCnts8.MaxInputLength = 100;
            this.CmplGuideCnts8.MaxInputLength = 3000;
            this.CmplGuiderRefCnts8.MaxInputLength = 2000;
            this.FocusedIssue9.MaxInputLength = 100;
            this.FocusedIssue10.MaxInputLength = 100;
            this.SurvGuideCnts14.MaxInputLength = 120;
            this.SS1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS6.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS7.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS8.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS9.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS10.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS11.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS12.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS14.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

            this.Load += new System.EventHandler(this.Main_Load);
            this.btnPDFAuth.Click += BtnPDFAuth_Click;
            this.btnPDF1.Click += BtnPDF1_Click;
            this.btnPDF1Up.Click += BtnPDF1Up_Click;
            this.btnPDF2.Click += BtnPDF2_Click;
            this.btnPDF2Up.Click += BtnPDF2Up_Click;
            this.btnSS6Del.Click += BtnSS6Del_Click;
            this.btnSS7Del.Click += BtnSS7Del_Click;
            this.btnSS8Del.Click += BtnSS8Del_Click;
            this.btnSS9Del.Click += BtnSS9Del_Click;
            this.btnSS10Del.Click += BtnSS10Del_Click;
            this.btnSS11Del.Click += BtnSS11Del_Click;
            this.btnSS12Del.Click += BtnSS12Del_Click;
            this.btnSS12Add.Click += BtnSS12Add_Click;
            this.btnSS14Del.Click += BtnSS14Del_Click;
            this.SS1.RowPostPaint += Grid_RowPostPaint;
            this.SS6.RowPostPaint += Grid_RowPostPaint;
            this.SS7.RowPostPaint += Grid_RowPostPaint;
            this.SS8.RowPostPaint += Grid_RowPostPaint;
            this.SS9.RowPostPaint += Grid_RowPostPaint;
            this.SS10.RowPostPaint += Grid_RowPostPaint;
            this.SS11.RowPostPaint += Grid_RowPostPaint;
            this.SS12.RowPostPaint += Grid_RowPostPaint;
            this.SS14.RowPostPaint += Grid_RowPostPaint;
            this.SS1.CellBeginEdit += Grid_CellBeginEdit;
            this.SS6.CellBeginEdit += Grid_CellBeginEdit;
            this.SS7.CellBeginEdit += Grid_CellBeginEdit;
            this.SS8.CellBeginEdit += Grid_CellBeginEdit;
            this.SS9.CellBeginEdit += Grid_CellBeginEdit;
            this.SS10.CellBeginEdit += Grid_CellBeginEdit;
            this.SS11.CellBeginEdit += Grid_CellBeginEdit;
            this.SS12.CellBeginEdit += Grid_CellBeginEdit;
            this.SS14.CellBeginEdit += Grid_CellBeginEdit;
            this.SS1.CellEndEdit += Grid_CellEndEdit;
            this.SS6.CellEndEdit += Grid_CellEndEdit;
            this.SS7.CellEndEdit += Grid_CellEndEdit;
            this.SS8.CellEndEdit += Grid_CellEndEdit;
            this.SS9.CellEndEdit += Grid_CellEndEdit;
            this.SS10.CellEndEdit += Grid_CellEndEdit;
            this.SS11.CellEndEdit += Grid_CellEndEdit;
            this.SS12.CellEndEdit += Grid_CellEndEdit;
            this.SS14.CellEndEdit += Grid_CellEndEdit;
            this.SS1.CellLeave += Grid_CellLeave;
            this.SS6.CellLeave += Grid_CellLeave;
            this.SS7.CellLeave += Grid_CellLeave;
            this.SS8.CellLeave += Grid_CellLeave;
            this.SS9.CellLeave += Grid_CellLeave;
            this.SS10.CellLeave += Grid_CellLeave;
            this.SS11.CellLeave += Grid_CellLeave;
            this.SS12.CellLeave += Grid_CellLeave;
            this.SS14.CellLeave += Grid_CellLeave;
            this.SS1.CellEnter += Grid_CellEnter;
            this.SS6.CellEnter += Grid_CellEnter;
            this.SS7.CellEnter += Grid_CellEnter;
            this.SS8.CellEnter += Grid_CellEnter;
            this.SS9.CellEnter += Grid_CellEnter;
            this.SS10.CellEnter += Grid_CellEnter;
            this.SS11.CellEnter += Grid_CellEnter;
            this.SS12.CellEnter += Grid_CellEnter;
            this.SS14.CellEnter += Grid_CellEnter;
            this.SS1.DataError += Grid_DataError;
            this.SS6.DataError += Grid_DataError;
            this.SS7.DataError += Grid_DataError;
            this.SS8.DataError += Grid_DataError;
            this.SS9.DataError += Grid_DataError;
            this.SS10.DataError += Grid_DataError;
            this.SS11.DataError += Grid_DataError;
            this.SS12.DataError += Grid_DataError;
            this.SS14.DataError += Grid_DataError;
            this.SS1.CellValueChanged += Grid_CellValueChanged;
            this.SS6.CellValueChanged += Grid_CellValueChanged;
            this.SS7.CellValueChanged += Grid_CellValueChanged;
            this.SS8.CellValueChanged += Grid_CellValueChanged;
            this.SS9.CellValueChanged += Grid_CellValueChanged;
            this.SS10.CellValueChanged += Grid_CellValueChanged;
            this.SS11.CellValueChanged += Grid_CellValueChanged;
            this.SS12.CellValueChanged += Grid_CellValueChanged;
            this.SS14.CellValueChanged += Grid_CellValueChanged;
            this.SS1.EditingControlShowing += Grid_EditingControlShowing;
            this.SS6.EditingControlShowing += Grid_EditingControlShowing;
            this.SS7.EditingControlShowing += Grid_EditingControlShowing;
            this.SS8.EditingControlShowing += Grid_EditingControlShowing;
            this.SS9.EditingControlShowing += Grid_EditingControlShowing;
            this.SS10.EditingControlShowing += Grid_EditingControlShowing;
            this.SS11.EditingControlShowing += Grid_EditingControlShowing;
            this.SS12.EditingControlShowing += Grid_EditingControlShowing;
            this.SS14.EditingControlShowing += Grid_EditingControlShowing;
            this.SS1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS6.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS7.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS8.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS9.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS10.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS11.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS12.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS14.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS6.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS7.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS8.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS9.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS10.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS11.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS12.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS14.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS6.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS7.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS8.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS9.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS10.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS11.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS12.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS14.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS12.CellContentClick += Grid_CellContentClick;
            this.SS14.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SS14_CellValueChanged);
            this.rtfSurvDangerCnts.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfSurvResultCnts.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfReviewOpinion.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfSearchMatters.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfSurvDtlCnts.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfGuideCnts.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid22.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid42.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid52.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid22.AcdtSurvDtlCodeChanged += Grid22_AcdtSurvDtlCodeChanged;
            this.flp_Panel_1.Resize += Flp_Panel_1_Resize;

            _bEvent = true;
        }

        private void Flp_Panel_1_Resize(object sender, EventArgs e)
        {
            int width = flp_Panel_1.Width - 35;
            this.panelEx6.Width = width;
            this.grid11.Width = width;
            this.panelEx52.Width = width;
            this.SS1.Width = width;
            this.panelEx1.Width = width;
            this.grid22.Width = width;
            this.panelEx29.Width = width;
            this.rtfSurvDangerCnts.Width = width;
            this.panelEx30.Width = width;
            this.rtfSurvResultCnts.Width = width;
            this.panelEx32.Width = width;
            this.rtfReviewOpinion.Width = width;
            this.panelEx34.Width = width;
            this.rtfSearchMatters.Width = width;
            this.panelEx28.Width = width;
            this.rtfSurvDtlCnts.Width = width;
            this.panelEx50.Width = width;
            this.rtfGuideCnts.Width = width;
            this.panelEx13.Width = width;
            this.grid42.Width = width;
            this.panelEx14.Width = width;
            this.grid52.Width = width;
            this.panelEx37.Width = width;
            this.panelEx38.Width = width;
            this.SS6.Width = width;
            this.panelEx39.Width = width;
            this.SS7.Width = width;
            this.panelEx40.Width = width;
            this.SS8.Width = width;
            this.panelEx41.Width = width;
            this.SS9.Width = width;
            this.panelEx42.Width = width;
            this.SS10.Width = width;
            this.panelEx43.Width = width;
            this.SS11.Width = width;
            this.panelEx73.Width = width;
            this.SS12.Width = width;
            this.panelEx75.Width = width;
            this.panelEx76.Width = width;
            this.SS14.Width = width;
            this.panel8.Width = width;
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        public bool LoadDocument(ReportParam p)
        {
            try
            {
                param = p;

                _bEvent = false;
                Init_Set();
                _bEvent = true;

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
                grid22.SetFocus();
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
                strSql += " SELECT A.id, A.code, A.value_text, A.value_remark  ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS A WITH(NOLOCK) ";
                strSql += " WHERE  A.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    A.code_id    = 'USERCD_AUTH' ";
                strSql += " AND    A.value_text = '" + param.UserID + "' ";
                strSql += " FOR JSON PATH ";
                ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(param.CompanySeq, strSql);
                this.SmplAuth = false;
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string usr = ds.Tables[0].Rows[0]["value_text"] + "";
                    if (usr == param.UserID) this.SmplAuth = true;
                }

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_CN00082' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_CN00082 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_CN00082.TableName = "CD_CN00082";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_CN00086' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_CN00086 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_CN00086.TableName = "CD_CN00086";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00238' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00238 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00238.TableName = "CD_PA00238";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00329' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00329 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00329.TableName = "CD_PA00329";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00492' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00492 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00492.TableName = "CD_PA00492";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00558' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00558 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00558.TableName = "CD_AI00558";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00588' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00588 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00588.TableName = "CD_AI00588";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00589' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00589 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00589.TableName = "CD_AI00589";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00590' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00590 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00590.TableName = "CD_AI00590";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00591' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00591 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00591.TableName = "CD_AI00591";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00593' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00593 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00593.TableName = "CD_AI00593";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00594' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00594 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00594.TableName = "CD_AI00594";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_AI00595' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_AI00595 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_AI00595.TableName = "CD_AI00595";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_001' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_001 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_001.TableName = "USERCD_001";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_002' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_002 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_002.TableName = "USERCD_002";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_003' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_003 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_003.TableName = "USERCD_003";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_004' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_004 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_004.TableName = "USERCD_004";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_005' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_005 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_005.TableName = "USERCD_005";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName, mnr.value_remark ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_006' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_006 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_006.TableName = "USERCD_006";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_LINA_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'YESNO_CD' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable YESNO_CD = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                YESNO_CD.TableName = "YESNO_CD";

                SetCombo(InsurStatCode1, CD_CN00082.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(MedHistTaskCode6, CD_PA00329.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(OthInsurCompCode7, CD_PA00492.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(CmplPrvtCode8, CD_AI00588.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(CmplPrvtStageCode8, USERCD_002.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(ClaimRsnCode10, CD_PA00238.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(TargetYn11, YESNO_CD.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(CustRelCode11, CD_CN00086.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(DamRptConfYn11, USERCD_003.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(SendWayCode11, USERCD_004.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(file_type12, USERCD_005.Copy(), "MinorSeq", "MinorName", true);

                DataSet dsp2 = new DataSet();
                dsp2.Tables.Add(CD_AI00558.Copy());
                dsp2.Tables.Add(USERCD_006.Copy());
                dsp2.Tables.Add(YESNO_CD.Copy());
                grid22.Init_Set(dsp2);

                DataSet dsp4 = new DataSet();
                dsp4.Tables.Add(CD_AI00589.Copy());
                dsp4.Tables.Add(CD_AI00590.Copy());
                dsp4.Tables.Add(CD_AI00591.Copy());
                dsp4.Tables.Add(CD_AI00593.Copy());
                dsp4.Tables.Add(CD_AI00594.Copy());
                dsp4.Tables.Add(CD_AI00595.Copy());
                dsp4.Tables.Add(YESNO_CD.Copy());
                dsp4.Tables.Add(USERCD_001.Copy());
                grid42.Init_Set(dsp4);

                DataSet dsp5 = new DataSet();
                dsp5.Tables.Add(CD_AI00558.Copy());
                grid52.Init_Set(dsp5);
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

        public static void SetCombo(DataGridViewComboBoxColumn cboObj, DataTable pdt, string strValueMember, string strDisplayMember, bool bAddnull)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetCombo(DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn cboObj, DataTable pdt, string strValueMember, string strDisplayMember, bool bAddnull)
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
                if (SS1.CurrentCell is DataGridViewTextBoxCell && SS1.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS6.CurrentCell is DataGridViewTextBoxCell && SS6.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS7.CurrentCell is DataGridViewTextBoxCell && SS7.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS8.CurrentCell is DataGridViewTextBoxCell && SS8.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS9.CurrentCell is DataGridViewTextBoxCell && SS9.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS10.CurrentCell is DataGridViewTextBoxCell && SS10.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS11.CurrentCell is DataGridViewTextBoxCell && SS11.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS12.CurrentCell is DataGridViewTextBoxCell && SS12.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS14.CurrentCell is DataGridViewTextBoxCell && SS14.CurrentCell.IsInEditMode)
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

        public void SetReadOnlyMode(bool rdonly)
        {
            // 조사보고서기본정보
            grid11.SetReadOnlyMode(rdonly);

            // 조사위임변경정보
            grid22.SetReadOnlyMode(rdonly);

            // 조사내용정보
            rtfSurvDangerCnts.rtbDoc.ReadOnly = rdonly;
            rtfSurvResultCnts.rtbDoc.ReadOnly = rdonly;
            rtfReviewOpinion.rtbDoc.ReadOnly = rdonly;
            rtfSearchMatters.rtbDoc.ReadOnly = rdonly;
            rtfSurvDtlCnts.rtbDoc.ReadOnly = rdonly;
            rtfGuideCnts.rtbDoc.ReadOnly = rdonly;

            // 면책체크리스트정보
            grid42.SetReadOnlyMode(rdonly);

            // 조사수수료정보
            grid52.SetReadOnlyMode(rdonly);

            ini_111.IsInputReadOnly = rdonly;
            ini_112.IsInputReadOnly = rdonly;
            ini_113.IsInputReadOnly = rdonly;
        }

        private void ClearAll()
        {
            _bEvent = false;

            if (this.SmplAuth)
            {
                btnPDFAuth.Visible = true;
                btnPDF1Up.Visible = true;
                btnPDF2Up.Visible = true;
            }
            else
            {
                btnPDFAuth.Visible = false;
                btnPDF1Up.Visible = false;
                btnPDF2Up.Visible = false;
            }

            // 조사보고서기본정보
            grid11.Clear();

            // 조사위임변경정보
            grid22.Clear();

            // 조사내용정보
            rtfSurvDangerCnts.rtbDoc.Text = "";
            rtfSurvResultCnts.rtbDoc.Text = "";
            rtfReviewOpinion.rtbDoc.Text = "";
            rtfSearchMatters.rtbDoc.Text = "";
            rtfSurvDtlCnts.rtbDoc.Text = "";
            rtfGuideCnts.rtbDoc.Text = "";
            id3 = 0;

            // 면책체크리스트정보
            grid42.Clear();

            // 조사수수료정보
            grid52.Clear();

            SS1.Rows.Clear();
            SS1.Height = 76;

            SS6.Rows.Clear();
            SS6.Height = 76;

            SS7.Rows.Clear();
            SS7.Height = 76;

            SS8.Rows.Clear();
            SS8.Height = 76;

            SS9.Rows.Clear();
            SS9.Height = 76;

            SS10.Rows.Clear();
            SS10.Height = 76;

            SS11.Rows.Clear();
            SS11.Height = 76;

            SS12.Rows.Clear();
            SS12.Height = 76;

            SS14.Rows.Clear();
            SS14.Height = 76;

            ini_111.ValueObject = 0;      // 총 처리일수 
            ini_112.ValueObject = 0;      // 총 지연일수 ( 제외 )
            ini_113.ValueObject = 0;      // 총 귀책일수  

            _bEvent = true;
        }

        private void XmlData_Read(DataSet yds)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearAll();

                string xml = yds.GetXml();

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptLina.xsd";
                DataSet pds = new DataSet();
                pds.ReadXml(s_CAA_XSD);

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    pds.ReadXml(xmlReader);
                }

                _bEvent = false;

                // 조사보고서기본정보
                DataTable SA01 = pds.Tables["DataBlock1"];
                if (SA01 != null && SA01.Rows.Count > 0)
                {
                    DataRow drow = SA01.Rows[0];
                    grid11.AcdtNo = drow["AcdtNo"] + "";
                    grid11.AcdtExamSerl = drow["AcdtExamSerl"] + "";
                    grid11.AcdtPsnName = drow["AcdtPsnName"] + "";
                    grid11.Regno = drow["IsrdRegno1"] + "-" + drow["IsrdRegno2"];
                    grid11.InsurChrg = drow["InsurChrg"] + "";
                    grid11.SurvAcptDt = drow["SurvAcptDt"] + "";
                    grid11.SurvReqDt = drow["SurvReqDt"] + "";
                    grid11.DelayRprtDt = drow["DelayRprtDt"] + "";
                    grid11.EndDate = drow["SurvCompDt"] + "";
                    grid11.SolarWDays = drow["SolarWDays"] + "";
                    grid11.SurvComp = "해성";
                    grid11.SurvAsgnTeamLeadName = drow["SurvAsgnTeamLeadName"] + "";
                    grid11.SurvAsgnTeamLeadOP = drow["SurvAsgnTeamLeadOP"] + "";
                    grid11.SurvAsgnEmpName = drow["SurvAsgnEmpName"] + "";
                    grid11.SurvAsgnEmpOP = drow["SurvAsgnEmpOP"] + "";
                    grid11.edi_id = Utils.ToInt(drow["edi_id"]);
                    grid11.parent_id = Utils.ToInt(drow["parent_id"]);
                }

                this.ReadOnlyMode = false;
                if (grid11.parent_id == 0)
                {
                    this.SendableMode = false;
                    this.PrintableMode = false;
                }
                else
                {
                    this.SendableMode = true;
                    this.PrintableMode = true;
                }
                if (grid11.edi_id != 0)
                {
                    this.SendableMode = false;
                    this.ReadOnlyMode = true;
                }

                // 조사위임변경정보
                DataTable SA02 = pds.Tables["DataBlock2"];
                if (SA02 != null && SA02.Rows.Count > 0)
                {
                    DataRow drow = SA02.Rows[0];
                    grid22.OurAcdtSurvDtlCode = drow["OurAcdtSurvDtlCode"] + "";
                    grid22.CompAcdtSurvDtlCode = drow["CompAcdtSurvDtlCode"] + "";
                    grid22.CompAcdtSurvDtlCode2 = drow["CompAcdtSurvDtlCode2"] + "";
                    grid22.AccuseYn = drow["AccuseYn"] + "";
                    grid22.SurvDtlChgRsn = drow["SurvDtlChgRsn"] + "";
                    grid22.id = Utils.ToInt(drow["id"]);
                }

                // 조사내용정보
                DataTable SA03 = pds.Tables["DataBlock3"];
                if (SA03 != null && SA03.Rows.Count > 0)
                {
                    DataRow drow = SA03.Rows[0];
                    rtfSurvDangerCnts.rtbDoc.Text = drow["SurvDangerCnts"] + "";  // 조사위험내용
                    rtfSurvResultCnts.rtbDoc.Text = drow["SurvResultCnts"] + "";  // 조사결과내용
                    rtfReviewOpinion.rtbDoc.Text = drow["ReviewOpinion"] + "";    // 검토의견내용
                    rtfSearchMatters.rtbDoc.Text = drow["SearchMatters"] + "";    // 검색사항내용
                    rtfSurvDtlCnts.rtbDoc.Text = drow["SurvDtlCnts"] + "";        // 조사상세내용
                    rtfGuideCnts.rtbDoc.Text = drow["GuideCnts"] + "";            // 안내사항
                    id3 = Utils.ToInt(drow["id"]);
                }

                // 4. 손해사정 의견
                DataTable SA04 = pds.Tables["DataBlock4"];
                if (SA04 != null && SA04.Rows.Count > 0)
                {
                    DataRow drow = SA04.Rows[0];
                    grid42.TermsRelayCode = drow["TermsRelayCode"] + "";      //약관전달방법코드
                    grid42.TermsRelayCnts = drow["TermsRelayCnts"] + "";
                    grid42.HandSignCode = drow["HandSignCode"] + "";          //자필서명방법코드
                    grid42.HandSignCnts = drow["HandSignCnts"] + "";
                    grid42.ExTermsExplnCode = drow["ExTermsExplnCode"] + "";  //면책약관설명방법코드
                    grid42.ExTermsExplnCnts = drow["ExTermsExplnCnts"] + "";
                    grid42.ExTermsAplyCode = drow["ExTermsAplyCode"] + "";    //면책약관적용구분코드
                    grid42.ExTermsAplyCnts = drow["ExTermsAplyCnts"] + "";
                    grid42.ReqDocuAdeqCode = drow["ReqDocuAdeqCode"] + "";    //구비서류적정성구분코드
                    grid42.ReqDocuAdeqCnts = drow["ReqDocuAdeqCnts"] + "";
                    grid42.ExTermsUndstdLvlCode = drow["ExTermsUndstdLvlCode"] + "";    //면책약관이해도등급코드
                    grid42.ExTermsUndstdLvlCnts = drow["ExTermsUndstdLvlCnts"] + "";
                    grid42.WriterDisadvantageYn = drow["WriterDisadvantageYn"] + "";    //작성자불이익적용코드
                    grid42.WriterDisadvantageCnts = drow["WriterDisadvantageCnts"] + "";
                    grid42.CaseLawAdjReviewYn = drow["CaseLawAdjReviewYn"] + "";        //판례조정사례검토여부
                    grid42.CaseLawAdjReviewCnts = drow["CaseLawAdjReviewCnts"] + "";
                    grid42.ReviewPossibleYn = drow["ReviewPossibleYn"] + "";            //재검토가능여부
                    grid42.ReviewPossibleCnts = drow["ReviewPossibleCnts"] + "";
                    grid42.ExTermsTtlOpinionCode = drow["ExTermsTtlOpinionCode"] + "";  //면책종합의견구분코드
                    grid42.ExTermsTtlOpinionCnts = drow["ExTermsTtlOpinionCnts"] + "";
                    grid42.id = Utils.ToInt(drow["id"]);
                }

                // 조사수수료정보
                DataTable SA05 = pds.Tables["DataBlock5"];
                if (SA05 != null && SA05.Rows.Count > 0)
                {
                    DataRow drow = SA05.Rows[0];
                    grid52.AcdtSurvDtlCode = drow["AcdtSurvDtlCode"] + "";
                    grid52.AcdtSurvVstCnt = drow["AcdtSurvVstCnt"] + "";
                    grid52.AcdtSurvPassDay = drow["AcdtSurvPassDay"] + "";
                    grid52.DefaultCost = drow["DefaultCost"] + "";
                    grid52.TransCost = drow["TransCost"] + "";
                    grid52.DocuCost = drow["DocuCost"] + "";
                    grid52.AdviceCost = drow["AdviceCost"] + "";
                    grid52.OtherCost = drow["OtherCost"] + "";
                    grid52.id = Utils.ToInt(drow["id"]);
                }

                // 병력경위사항
                SS6.AllowUserToAddRows = false;
                DataTable SA06 = pds.Tables["DataBlock6"];
                if (SA06 != null && SA06.Rows.Count > 0)
                {
                    for (int i = 0; i < SA06.Rows.Count; i++)
                    {
                        DataRow drow = SA06.Rows[i];
                        SS6.Rows.Add();
                        SS6.Rows[SS6.RowCount - 1].Cells["MedHistTaskCode6"].Value = drow["MedHistTaskCode"];
                        SS6.Rows[SS6.RowCount - 1].Cells["TreatFrDt6"].Value = Utils.ConvertToDateTime(drow["TreatFrDt"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["TreatToDt6"].Value = Utils.ConvertToDateTime(drow["TreatToDt"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["MedHistConfCnts6"].Value = Utils.ToMultiline(drow["MedHistConfCnts"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["MedHistConfOrgName6"].Value = drow["MedHistConfOrgName"];
                        SS6.Rows[SS6.RowCount - 1].Cells["MedHistDtlCnts6"].Value = Utils.ToMultiline(drow["MedHistDtlCnts"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["id6"].Value = drow["id"];
                        SS6.Rows[SS6.RowCount - 1].Cells["WorkingTag6"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS6.AllowUserToAddRows = true;
                }
                SS6.AutoResizeRows();
                GridHeightResize(SS6);

                // 타사가입사항
                SS7.AllowUserToAddRows = false;
                DataTable SA07 = pds.Tables["DataBlock7"];
                if (SA07 != null && SA07.Rows.Count > 0)
                {
                    for (int i = 0; i < SA07.Rows.Count; i++)
                    {
                        DataRow drow = SA07.Rows[i];
                        SS7.Rows.Add();
                        SS7.Rows[SS7.RowCount - 1].Cells["OthInsurCompCode7"].Value = drow["OthInsurCompCode"];
                        SS7.Rows[SS7.RowCount - 1].Cells["OthInsurPrdName7"].Value = drow["OthInsurPrdName"];
                        SS7.Rows[SS7.RowCount - 1].Cells["ClaimRsnName7"].Value = drow["ClaimRsnName"];
                        SS7.Rows[SS7.RowCount - 1].Cells["OthClaimReqAmt7"].Value = Utils.ToDecimal(drow["OthClaimReqAmt"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["OthPayAmt7"].Value = Utils.ToDecimal(drow["OthPayAmt"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["OthInsurRmk7"].Value = Utils.ToMultiline(drow["OthInsurRmk"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["id7"].Value = drow["id"];
                        SS7.Rows[SS7.RowCount - 1].Cells["WorkingTag7"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS7.AllowUserToAddRows = true;
                }
                SS7.AutoResizeRows();
                GridHeightResize(SS7);

                // 민원안내사항
                SS8.AllowUserToAddRows = false;
                DataTable SA08 = pds.Tables["DataBlock8"];
                if (SA08 != null && SA08.Rows.Count > 0)
                {
                    for (int i = 0; i < SA08.Rows.Count; i++)
                    {
                        DataRow drow = SA08.Rows[i];
                        SS8.Rows.Add();
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplPrvtCode8"].Value = drow["CmplPrvtCode"];
                        SS8.Rows[SS8.RowCount - 1].Cells["GuideDt8"].Value = Utils.ConvertToDateTime(drow["GuideDt"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplGuidePassDay8"].Value = drow["CmplGuidePassDay"];
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplGuideTgtCnts8"].Value = Utils.ToMultiline(drow["CmplGuideTgtCnts"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplGuideCnts8"].Value = Utils.ToMultiline(drow["CmplGuideCnts"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplGuiderRefCnts8"].Value = Utils.ToMultiline(drow["CmplGuiderRefCnts"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["CmplPrvtStageCode8"].Value = drow["CmplPrvtStageCode"];
                        SS8.Rows[SS8.RowCount - 1].Cells["id8"].Value = drow["id"];
                        SS8.Rows[SS8.RowCount - 1].Cells["WorkingTag8"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS8.AllowUserToAddRows = true;
                }
                SS8.AutoResizeRows();
                GridHeightResize(SS8);

                // 계약별계산결과내역
                SS9.AllowUserToAddRows = false;
                DataTable SA09 = pds.Tables["DataBlock9"];
                if (SA09 != null && SA09.Rows.Count > 0)
                {
                    for (int i = 0; i < SA09.Rows.Count; i++)
                    {
                        DataRow drow = SA09.Rows[i];
                        SS9.Rows.Add();
                        SS9.Rows[SS9.RowCount - 1].Cells["ContractNo9"].Value = drow["ContractNo"];
                        SS9.Rows[SS9.RowCount - 1].Cells["ClaimSeqNo9"].Value = drow["ClaimSeqNo"];
                        SS9.Rows[SS9.RowCount - 1].Cells["PayAmt9"].Value = Utils.ToDecimal(drow["PayAmt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["NotPayAmt9"].Value = Utils.ToDecimal(drow["NotPayAmt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["FocusedIssue9"].Value = Utils.ToMultiline(drow["FocusedIssue"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["id9"].Value = drow["id"];
                        SS9.Rows[SS9.RowCount - 1].Cells["A3_id9"].Value = drow["A3_id"];
                        SS9.Rows[SS9.RowCount - 1].Cells["WorkingTag9"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS9.AllowUserToAddRows = true;
                }
                SS9.AutoResizeRows();
                GridHeightResize(SS9);

                // 보유계약별계산결과내역
                SS10.AllowUserToAddRows = false;
                DataTable SA10 = pds.Tables["DataBlock10"];
                if (SA10 != null && SA10.Rows.Count > 0)
                {
                    for (int i = 0; i < SA10.Rows.Count; i++)
                    {
                        DataRow drow = SA10.Rows[i];
                        SS10.Rows.Add();
                        SS10.Rows[SS10.RowCount - 1].Cells["ContractNo10"].Value = drow["ContractNo"];
                        SS10.Rows[SS10.RowCount - 1].Cells["ClaimRsnCode10"].Value = drow["ClaimRsnCode"];
                        SS10.Rows[SS10.RowCount - 1].Cells["PreRsnCode10"].Value = drow["PreRsnCode"];
                        SS10.Rows[SS10.RowCount - 1].Cells["PostRsnCode10"].Value = drow["PostRsnCode"];
                        SS10.Rows[SS10.RowCount - 1].Cells["RsnOccurDt10"].Value = Utils.ConvertToDateTime(drow["RsnOccurDt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["DischargeDt10"].Value = Utils.ConvertToDateTime(drow["DischargeDt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["ClaimReqAmt10"].Value = Utils.ToDecimal(drow["ClaimReqAmt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["PayAmt10"].Value = Utils.ToDecimal(drow["PayAmt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["NotPayAmt10"].Value = Utils.ToDecimal(drow["NotPayAmt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["FocusedIssue10"].Value = Utils.ToMultiline(drow["FocusedIssue"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["id10"].Value = drow["id"];
                        SS10.Rows[SS10.RowCount - 1].Cells["WorkingTag10"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS10.AllowUserToAddRows = true;
                }
                SS10.AutoResizeRows();
                GridHeightResize(SS10);

                // 손해사정서교부사항
                SS11.AllowUserToAddRows = false;
                DataTable SA11 = pds.Tables["DataBlock11"];
                if (SA11 != null && SA11.Rows.Count > 0)
                {
                    for (int i = 0; i < SA11.Rows.Count; i++)
                    {
                        DataRow drow = SA11.Rows[i];
                        SS11.Rows.Add();
                        SS11.Rows[SS11.RowCount - 1].Cells["TargetYn11"].Value = drow["TargetYn"];
                        SS11.Rows[SS11.RowCount - 1].Cells["CustRelCode11"].Value = drow["CustRelCode"];
                        SS11.Rows[SS11.RowCount - 1].Cells["CustNo11"].Value = drow["CustNo"];
                        SS11.Rows[SS11.RowCount - 1].Cells["CustName11"].Value = drow["CustName"];
                        SS11.Rows[SS11.RowCount - 1].Cells["DamRptConfYn11"].Value = drow["DamRptConfYn"];
                        SS11.Rows[SS11.RowCount - 1].Cells["SendWayCode11"].Value = drow["SendWayCode"];
                        SS11.Rows[SS11.RowCount - 1].Cells["SendDt11"].Value = Utils.ConvertToDateTime(drow["SendDt"]);
                        SS11.Rows[SS11.RowCount - 1].Cells["id11"].Value = drow["id"];
                        SS11.Rows[SS11.RowCount - 1].Cells["WorkingTag11"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    //SS11.AllowUserToAddRows = true;
                }
                SS11.AutoResizeRows();
                GridHeightResize(SS11);

                // 보고서첨부파일
                SS12.AllowUserToAddRows = false;
                DataTable SA12 = pds.Tables["DataBlock12"];
                if (SA12 != null && SA12.Rows.Count > 0)
                {
                    for (int i = 0; i < SA12.Rows.Count; i++)
                    {
                        DataRow drow = SA12.Rows[i];
                        SS12.Rows.Add();
                        SS12.Rows[SS12.RowCount - 1].Cells["file_type12"].Value = drow["file_type"];
                        SS12.Rows[SS12.RowCount - 1].Cells["file_name12"].Value = drow["file_name"];
                        SS12.Rows[SS12.RowCount - 1].Cells["file_rmk12"].Value = drow["file_rmk"];
                        SS12.Rows[SS12.RowCount - 1].Cells["file_seq12"].Value = drow["file_seq"];
                        SS12.Rows[SS12.RowCount - 1].Cells["id12"].Value = drow["id"];
                        SS12.Rows[SS12.RowCount - 1].Cells["parent_id12"].Value = drow["parent_id"];
                        SS12.Rows[SS12.RowCount - 1].Cells["WorkingTag12"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS12.AllowUserToAddRows = true;
                }
                SS12.AutoResizeRows();
                GridHeightResize(SS12);

                SS14.AllowUserToAddRows = false;
                // 1. 처리과정 - 사고처리과정표 
                DataTable SA14 = pds.Tables["DataBlock14"];
                if (SA14 != null && SA14.Rows.Count > 0)
                {
                    for (int i = 0; i < SA14.Rows.Count; i++)
                    {
                        DataRow drow = SA14.Rows[i];

                        SS14.Rows.Add();

                        DateTime mydate = Utils.ConvertToDateTime(drow["PrgMgtDt"]);

                        SS14.Rows[SS14.RowCount - 1].Cells["PrgMgtDt14"].Value = mydate;                         // 일자
                        SS14.Rows[SS14.RowCount - 1].Cells["DayCnt14"].Value = drow["DayCnt"];                   // 일수
                        SS14.Rows[SS14.RowCount - 1].Cells["PrgMgtHed14"].Value = drow["PrgMgtHed"];             // 항목
                        SS14.Rows[SS14.RowCount - 1].Cells["SurvGuideCnts14"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);     // 내용
                        SS14.Rows[SS14.RowCount - 1].Cells["IsrdResnDlyDay14"].Value = drow["IsrdResnDlyDay"];   // 지연일수
                        SS14.Rows[SS14.RowCount - 1].Cells["keystr14"].Value = drow["keystr"];                   // keystr
                        SS14.Rows[SS14.RowCount - 1].Cells["WorkingTag14"].Value = (Utils.ConvertToString(drow["keystr"]) == "" ? "A" : "");

                        ini_111.ValueObject = Utils.ToInt(ini_111.Value) + Utils.ToInt(SS14.Rows[SS14.RowCount - 1].Cells["DayCnt14"].Value);
                        ini_112.ValueObject = Utils.ToInt(ini_112.Value) + Utils.ToInt(SS14.Rows[SS14.RowCount - 1].Cells["IsrdResnDlyDay14"].Value);
                        ini_113.ValueObject = Utils.ToInt(ini_111.Value) - Utils.ToInt(ini_112.Value);
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS14.AllowUserToAddRows = true;
                }
                SS14.AutoResizeRows();
                GridHeightResize(SS14);

                // 계약사항
                SS1.AllowUserToAddRows = false;
                DataTable SA15 = pds.Tables["DataBlock15"];
                if (SA15 != null && SA15.Rows.Count > 0)
                {
                    for (int i = 0; i < SA15.Rows.Count; i++)
                    {
                        DataRow drow = SA15.Rows[i];
                        SS1.Rows.Add();
                        SS1.Rows[SS1.RowCount - 1].Cells["InsurName1"].Value = drow["InsurName"];
                        SS1.Rows[SS1.RowCount - 1].Cells["ContractNo1"].Value = drow["ContractNo"];
                        SS1.Rows[SS1.RowCount - 1].Cells["InsurStatCode1"].Value = drow["InsurStatCode"];
                        SS1.Rows[SS1.RowCount - 1].Cells["ContractDt1"].Value = Utils.ConvertToDateTime(drow["ContractDt"]);
                        SS1.Rows[SS1.RowCount - 1].Cells["EffectiveDt1"].Value = Utils.ConvertToDateTime(drow["EffectiveDt"]);
                        SS1.Rows[SS1.RowCount - 1].Cells["resurrectionDt1"].Value = Utils.ConvertToDateTime(drow["resurrectionDt"]);
                        //SS1.Rows[SS1.RowCount - 1].Cells["id1"].Value = drow["id"];
                        //SS1.Rows[SS1.RowCount - 1].Cells["WorkingTag1"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                SS1.AutoResizeRows();
                GridHeightResize(SS1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                _bEvent = true;
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

        private void Grid22_AcdtSurvDtlCodeChanged(string defaultFee, string TransFee)
        {
            if (!_bEvent) return;
            grid52.DefaultCost = defaultFee;
            grid52.TransCost = TransFee;

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
                if (e.Control is TextBox)
                    ((TextBox)e.Control).ReadOnly = ReadOnlyMode;
                if (e.Control is ComboBox)
                    ((ComboBox)e.Control).Enabled = !ReadOnlyMode;
                if (e.Control is CheckBox)
                    ((CheckBox)e.Control).Enabled = !ReadOnlyMode;
                if (e.Control is DateTimePicker)
                    ((DateTimePicker)e.Control).Enabled = !ReadOnlyMode;
                if (e.Control is CustomDataGridViewTextBoxEditingControl)
                    ((CustomDataGridViewTextBoxEditingControl)e.Control).IsInputReadOnly = ReadOnlyMode;
                if (e.Control is CustomDataGridViewDoubleInputEditingControl)
                    ((CustomDataGridViewDoubleInputEditingControl)e.Control).IsInputReadOnly = ReadOnlyMode;
                if (e.Control is CustomDataGridViewDateTimeInputEditingControl)
                    ((CustomDataGridViewDateTimeInputEditingControl)e.Control).IsInputReadOnly = ReadOnlyMode;
                if (e.Control is DevComponents.DotNetBar.Controls.DataGridViewDoubleInputEditingControl)
                    ((DevComponents.DotNetBar.Controls.DataGridViewDoubleInputEditingControl)e.Control).IsInputReadOnly = ReadOnlyMode;
                if (e.Control is DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputEditingControl)
                    ((DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputEditingControl)e.Control).IsInputReadOnly = ReadOnlyMode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (dgv.Columns[e.ColumnIndex].Name == "FileUpload")
                    {
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.RestoreDirectory = true;
                            openFileDialog.Multiselect = false;
                            openFileDialog.Filter = "TIF files|*.tif;*.tiff";
                            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                            fnFileUpload(dgv.Rows[e.RowIndex], openFileDialog.FileName);
                        }
                    }
                    else if (dgv.Columns[e.ColumnIndex].Name == "FileDownload")
                    {
                        string file = fnFileDownload(dgv.Rows[e.RowIndex]);
                        if (file != "")
                        {
                            string path = Path.GetDirectoryName(file);
                            Process.Start(path, file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool fnFileUpload(DataGridViewRow drow, string fileName)
        {
            string realFileName = Path.GetFileName(fileName);
            YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            // File Info
            FileInfo finfo = new FileInfo(fileName);
            byte[] rptbyte = (byte[])MetroSoft.HIS.cFile.ReadBinaryFile(fileName);
            string fileBase64 = Convert.ToBase64String(rptbyte);
            // File Info
            //string fileSeq = YLWService.MTRServiceModule.CallMTRFileuploadGetSeq(security, finfo, fileBase64, "47820004");  // 이부분에서 오류남. CallMTRFileuploadGetSeq -> FileuploadGetSeq
            //string fileSeq = YLWService.YLWServiceModule.FileuploadGetSeq(security, finfo, fileBase64, "47820004"); 
            string fileSeq = YLWService.MTRServiceModule.CallMTRFileuploadGetSeq(security, finfo, fileBase64, "47820004");  // WebYlwPlugin_MetroSoft -> 일반 POST API 로 변경
            if (fileSeq == "")
            {
                MessageBox.Show("FileUpload : file_seq not found");
                return false;
            }
            drow.Cells["file_name12"].Value = realFileName;
            drow.Cells["file_seq12"].Value = fileSeq;
            string workingTag = WorkingTagCell(drow).Value + "";
            if (workingTag == "")
            {
                WorkingTagCell(drow).Value = "U";
            }
            return true;
        }

        public string fnFileDownload(DataGridViewRow drow)
        {
            string fileName = drow.Cells["file_name12"].Value + "";
            string fileSeq = drow.Cells["file_seq12"].Value + "";

            YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            string fileBase64 = YLWService.MTRServiceModule.CallMTRFileDownloadBase64(security, fileSeq, "0", "0");
            if (fileBase64 == "")
            {
                MessageBox.Show("FileDownload : fileBase64 not found");
                return "";
            }

            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads");
            if (!Directory.Exists(downloadFolder)) Directory.CreateDirectory(downloadFolder);
            string file = Path.Combine(downloadFolder, fileName);
            int fileCount = 0;
            while (File.Exists(file))
            {
                fileCount++;
                file = Path.GetFileNameWithoutExtension(fileName) + "(" + fileCount.ToString() + ")" + Path.GetExtension(fileName);
                file = Path.Combine(downloadFolder, file);
            }
            byte[] bytes_file = Convert.FromBase64String(fileBase64);
            FileStream fs = new FileStream(file, FileMode.Create);
            fs.Write(bytes_file, 0, bytes_file.Length);
            fs.Close();
            return file;
        }

        public bool fnFileDelete(DataGridViewRow drow)
        {
            string fileName = drow.Cells["file_name12"].Value + "";
            string fileSeq = drow.Cells["file_seq12"].Value + "";

            YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            string dfg = YLWService.MTRServiceModule.CallMTRFileDelete(security, fileSeq, "1");
            if (dfg != "OK")
            {
                MessageBox.Show("FileDelete : fileDelete error");
                return false;
            }
            return true;
        }

        private void Grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                DataGridViewRow dr = grd.Rows[e.RowIndex];
                string workingTag = WorkingTagCell(dr).Value + "";

                //' Position text
                SizeF size = e.Graphics.MeasureString(workingTag, grd.Font);
                if (grd.RowHeadersWidth < Convert.ToInt32(size.Width + 20))
                {
                    grd.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
                }

                //' Use default system text brush
                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(workingTag, grd.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
            }
            catch { }
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
                if (!grd.CurrentCell.ReadOnly)
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

        private void BtnSS12Add_Click(object sender, EventArgs e)
        {
            if (ReadOnlyMode) return;

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Multiselect = true;
                    openFileDialog.Filter = "TIF files|*.tif;*.tiff|All files|*.*";
                    if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (string P_File in openFileDialog.FileNames)
                    {
                        int rowIndex = SS12.Rows.Add();
                        DataGridViewRow dr = SS12.Rows[rowIndex];
                        fnFileUpload(dr, P_File);
                    }
                    SS12.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnPDFAuth_Click(object sender, EventArgs e)
        {
            try
            {
                frmLinaSmplAuth f = new frmLinaSmplAuth(param);
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnPDF1_Click(object sender, EventArgs e)
        {
            PDFFileShow("1", "견본보고서");
        }

        private void BtnPDF1Up_Click(object sender, EventArgs e)
        {
            PDFFileUpload("1", "견본보고서");
        }

        private void BtnPDF2_Click(object sender, EventArgs e)
        {
            PDFFileShow("2", "유의사항");
        }

        private void BtnPDF2Up_Click(object sender, EventArgs e)
        {
            PDFFileUpload("2", "유의사항");
        }

        private bool PDFFileShow(string fileTypeSeq, string fileTypeName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisSurvRptLinaLink";
                security.methodId = "Query";
                security.companySeq = param.CompanySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("FileTypeSeq");
                DataRow dr = dt1.Rows.Add();
                dr["FileTypeSeq"] = fileTypeSeq;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show(fileTypeName + "이(가) 없습니다");
                    return false;
                }
                if (yds == null || !yds.Tables.Contains("DataBlock2"))
                {
                    MessageBox.Show(fileTypeName + " 파일이 없습니다");
                    return false;
                }
                DataTable ydt = yds.Tables["DataBlock2"];
                string fileName = ydt.Rows[0]["FileName"].ToString();
                string fileBase64 = ydt.Rows[0]["FileBase64"].ToString();

                string file = Path.Combine(Path.GetTempPath(), fileName);
                if (File.Exists(file)) File.Delete(file);
                byte[] bytes_file = Convert.FromBase64String(fileBase64);
                FileStream orgFile = new FileStream(file, FileMode.Create);
                orgFile.Write(bytes_file, 0, bytes_file.Length);
                orgFile.Close();
                System.Diagnostics.Process.Start(file);

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

        private bool PDFFileUpload(string fileTypeSeq, string fileTypeName)
        {
            try
            {
                FileInfo fInfo = null;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "All files|*.*";
                    if (openFileDialog.ShowDialog() != DialogResult.OK) return false;
                    fInfo = new FileInfo(openFileDialog.FileName);
                }
                if (fInfo == null) return false;
                FileStream fs = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);
                Byte[] bytBLOBData = new Byte[fs.Length];
                bytBLOBData = r.ReadBytes((int)fs.Length);
                r.Close();
                fs.Close();
                string fileBase64 = Convert.ToBase64String(bytBLOBData);

                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisSurvRptLinaLink";
                security.methodId = "Save";
                security.companySeq = param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = param.UserID;

                DataSet ds = new DataSet("ROOT");
                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("FileTypeSeq");
                DataRow dr = dt1.Rows.Add();
                dr["FileTypeSeq"] = fileTypeSeq;
                DataTable dt2 = ds.Tables.Add("DataBlock2");
                dt2.Columns.Add("FileTypeSeq");
                dt2.Columns.Add("FileName");
                dt2.Columns.Add("FileBase64");
                dt2.Columns.Add("WorkingTag");
                dr = dt2.Rows.Add();
                dr["FileTypeSeq"] = fileTypeSeq;
                dr["FileName"] = fInfo.Name;
                dr["FileBase64"] = fileBase64;
                dr["WorkingTag"] = "A";

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show(fileTypeName + "이(가) 저장되지 않았습니다");
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
                MessageBox.Show(fileTypeName + " 업로드 완료");
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

        private void BtnSS14Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS14);
        }

        private void BtnSS12Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS12);
        }

        private void BtnSS11Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS11);
        }

        private void BtnSS10Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS10);
        }

        private void BtnSS9Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS9);
        }

        private void BtnSS8Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS8);
        }

        private void BtnSS7Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS7);
        }

        private void BtnSS6Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS6);
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

        private void GridRowRemove(DataGridView dgv)
        {
            try
            {
                if (ReadOnlyMode) return;
                if (dgv.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("선택된 " + dgv.SelectedRows.Count.ToString() + "개 행을 정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (DataGridViewRow row in dgv.SelectedRows)
                        {
                            if (row.IsNewRow) continue;
                            if (WorkingTagCell(row).Value + "" == "A")
                            {
                                if (dgv == SS12)
                                {
                                    if (!fnFileDelete(row)) continue;
                                }
                                dgv.Rows.Remove(row);
                            }
                            else
                            {
                                WorkingTagCell(row).Value = "D";
                            }
                        }
                        DeleteRows(dgv);
                    }
                }
                dgv.Focus();
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
                if (ReadOnlyMode)
                {
                    DataGridViewCell cel = grd.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                WorkingTagCell(dgv.Rows[row_num]).Value = "A";
                dgv.AutoResizeRow(row_num, DataGridViewAutoSizeRowMode.AllCells);
                dgv.Height = dgv.Height + dgv.Rows[row_num].Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_RowDel(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            try
            {
                dgv.Height = dgv.Height - dgv.Rows[e.RowIndex].Height;
            }
            catch { }

            if (Utils.ConvertToString(dgv.Tag) == "3")
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
        }

        private void SS14_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 4)
                {
                    try
                    {
                        if (e.ColumnIndex == 0)
                        {
                            for (int i = 0; i < SS14.RowCount - 1; i++)
                            {
                                string frdt = (i == 0 ? acptDt : Utils.DateFormat(SS14.Rows[i - 1].Cells[e.ColumnIndex].Value, "yyyyMMdd"));
                                string todt = Utils.DateFormat(SS14.Rows[i].Cells[e.ColumnIndex].Value, "yyyyMMdd");
                                SS14.Rows[i].Cells[e.ColumnIndex + 1].Value = uFunction.GetDelayDays(param.CompanySeq, frdt, todt);
                            }
                        }

                        int num_01 = 0;
                        int num_02 = 0;
                        for (int i = 0; i < SS14.RowCount - 1; i++)
                        {
                            if (SS14.Rows[i].Cells[1].Value == null) SS14.Rows[i].Cells[1].Value = 0;
                            if (SS14.Rows[i].Cells[4].Value == null) SS14.Rows[i].Cells[4].Value = 0;

                            num_01 = num_01 + Utils.ToInt(SS14.Rows[i].Cells[1].Value);
                            num_02 = num_02 + Utils.ToInt(SS14.Rows[i].Cells[4].Value);
                        }
                        ini_111.Value = num_01;
                        ini_112.Value = num_02;
                        ini_113.Value = num_01 - num_02;
                    }
                    catch { }
                }
            }
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_bEvent) return;
            DataGridView dgv = (DataGridView)sender;

            try
            {
                DataGridViewRow dr = dgv.Rows[e.RowIndex];
                string workingTag = WorkingTagCell(dr).Value + "";
                if (workingTag == "")
                {
                    WorkingTagCell(dr).Value = "U";
                }
                GridHeightResize(dgv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public void Data_Send()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _bEvent = false;

                DataSet pds = new DataSet();

                // 조사보고서기본정보
                DataTable dt = pds.Tables.Add("DataBlock1");
                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");
                dt.Columns.Add("AcdtNo");
                dt.Columns.Add("AcdtExamSerl");
                dt.Columns.Add("edi_id");
                dt.Columns.Add("parent_id");
                dt.Columns.Add("bis_code");
                DataRow dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["AcdtNo"] = grid11.AcdtNo;
                dr["AcdtExamSerl"] = grid11.AcdtExamSerl;
                dr["edi_id"] = grid11.edi_id;
                dr["parent_id"] = grid11.parent_id;
                dr["bis_code"] = grid11.bis_code;

                DataSet yds = null;
                if (SendData(pds, out yds) == true)
                {
                    // 조사보고서기본정보
                    DataTable SA01 = yds.Tables["DataBlock1"];
                    DataRow drow = SA01.Rows[0];
                    grid11.edi_id = Utils.ToInt(drow["edi_id"]);
                    grid11.parent_id = Utils.ToInt(drow["parent_id"]);

                    this.ReadOnlyMode = false;
                    if (grid11.parent_id == 0)
                    {
                        this.SendableMode = false;
                        this.PrintableMode = false;
                    }
                    else
                    {
                        this.SendableMode = true;
                        this.PrintableMode = true;
                    }
                    if (grid11.edi_id != 0)
                    {
                        this.SendableMode = false;
                        this.ReadOnlyMode = true;
                    }

                    MessageBox.Show("전송 완료");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                _bEvent = true;
                Cursor.Current = Cursors.Default;
            }
        }

        public void XmlData_Save(bool bNoMessage = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _bEvent = false;

                DataSet yds = null;
                if (SaveData(GetSaveData(), out yds) == true)
                {
                    // 조사보고서기본정보
                    DataTable SA01 = yds.Tables["DataBlock1"];
                    DataRow drow = SA01.Rows[0];
                    grid11.edi_id = Utils.ToInt(drow["edi_id"]);
                    grid11.parent_id = Utils.ToInt(drow["parent_id"]);

                    if (grid11.parent_id == 0)
                    {
                        this.SendableMode = false;
                        this.PrintableMode = false;
                    }
                    else
                    {
                        this.SendableMode = true;
                        this.PrintableMode = true;
                    }

                    // 조사위임변경정보
                    DataTable SA02 = yds.Tables["DataBlock2"];
                    drow = SA02.Rows[0];
                    grid22.id = Utils.ToInt(drow["id"]);

                    // 조사내용정보
                    DataTable SA03 = yds.Tables["DataBlock3"];
                    drow = SA03.Rows[0];
                    id3 = Utils.ToInt(drow["id"]);

                    // 4. 손해사정 의견
                    DataTable SA04 = yds.Tables["DataBlock4"];
                    drow = SA04.Rows[0];
                    grid42.id = Utils.ToInt(drow["id"]);

                    // 조사수수료정보
                    DataTable SA05 = yds.Tables["DataBlock5"];
                    drow = SA05.Rows[0];
                    grid52.id = Utils.ToInt(drow["id"]);

                    // 병력경위사항
                    DataTable SA06 = yds.Tables["DataBlock6"];
                    if (SA06 != null)
                    {
                        for (int ii = 0; ii < SA06.Rows.Count; ii++)
                        {
                            drow = SA06.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS6.Rows[idx].Cells["id6"].Value = drow["id"];
                            SS6.Rows[idx].Cells["WorkingTag6"].Value = "";
                        }
                    }
                    SS6.Refresh();

                    // 타사가입사항
                    DataTable SA07 = yds.Tables["DataBlock7"];
                    if (SA07 != null)
                    {
                        for (int ii = 0; ii < SA07.Rows.Count; ii++)
                        {
                            drow = SA07.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS7.Rows[idx].Cells["id7"].Value = drow["id"];
                            SS7.Rows[idx].Cells["WorkingTag7"].Value = "";
                        }
                    }
                    SS7.Refresh();

                    // 민원안내사항
                    DataTable SA08 = yds.Tables["DataBlock8"];
                    if (SA08 != null)
                    {
                        for (int ii = 0; ii < SA08.Rows.Count; ii++)
                        {
                            drow = SA08.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS8.Rows[idx].Cells["id8"].Value = drow["id"];
                            SS8.Rows[idx].Cells["WorkingTag8"].Value = "";
                        }
                    }
                    SS8.Refresh();

                    // 계약별계산결과내역
                    DataTable SA09 = yds.Tables["DataBlock9"];
                    if (SA09 != null)
                    {
                        for (int ii = 0; ii < SA09.Rows.Count; ii++)
                        {
                            drow = SA09.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS9.Rows[idx].Cells["id9"].Value = drow["id"];
                            SS9.Rows[idx].Cells["WorkingTag9"].Value = "";
                        }
                    }
                    SS9.Refresh();

                    // 보유계약별계산결과내역
                    DataTable SA10 = yds.Tables["DataBlock10"];
                    if (SA10 != null)
                    {
                        for (int ii = 0; ii < SA10.Rows.Count; ii++)
                        {
                            drow = SA10.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS10.Rows[idx].Cells["id10"].Value = drow["id"];
                            SS10.Rows[idx].Cells["WorkingTag10"].Value = "";
                        }
                    }
                    SS10.Refresh();

                    // 손해사정서교부사항
                    DataTable SA11 = yds.Tables["DataBlock11"];
                    if (SA11 != null)
                    {
                        for (int ii = 0; ii < SA11.Rows.Count; ii++)
                        {
                            drow = SA11.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS11.Rows[idx].Cells["id11"].Value = drow["id"];
                            SS11.Rows[idx].Cells["WorkingTag11"].Value = "";
                        }
                    }
                    SS11.Refresh();

                    // 보고서첨부파일
                    DataTable SA12 = yds.Tables["DataBlock12"];
                    if (SA12 != null)
                    {
                        for (int ii = 0; ii < SA12.Rows.Count; ii++)
                        {
                            drow = SA12.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS12.Rows[idx].Cells["id12"].Value = drow["id"];
                            SS12.Rows[idx].Cells["parent_id12"].Value = drow["parent_id"];
                            SS12.Rows[idx].Cells["WorkingTag12"].Value = "";
                        }
                    }
                    SS12.Refresh();

                    // 처리과정표
                    DataTable SA14 = yds.Tables["DataBlock14"];
                    if (SA14 != null)
                    {
                        for (int ii = 0; ii < SA14.Rows.Count; ii++)
                        {
                            drow = SA14.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS14.Rows[idx].Cells["keystr14"].Value = drow["keystr"];
                            SS14.Rows[idx].Cells["WorkingTag14"].Value = "";
                        }
                    }
                    SS14.Refresh();

                    if (!bNoMessage) MessageBox.Show("저장 완료");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                _bEvent = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool SendData(DataSet pds, out DataSet yds)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptLina";
            security.methodId = "Send";
            security.companySeq = param.CompanySeq;
            security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
            security.securityType = 1;
            security.userId = param.UserID;

            yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, pds);
            if (yds == null)
            {
                MessageBox.Show("전송할 수 없습니다");
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
                            MessageBox.Show("전송안됨:" + dti.Rows[ii]["Result"]);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool SaveData(DataSet pds, out DataSet yds)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptLina";
            security.methodId = "Save";
            security.companySeq = param.CompanySeq;
            security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
            security.securityType = 1;
            security.userId = param.UserID;

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

        private DataSet GetSaveData()
        {
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptLina.xsd";
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(s_CAA_XSD);

            // 조사보고서기본정보
            DataTable dt = ds.Tables["DataBlock1"];
            dt.Columns.Add("WorkingTag");
            DataRow dr = dt.Rows.Add();
            dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
            dr["AcdtNo"] = grid11.AcdtNo;
            dr["AcdtExamSerl"] = grid11.AcdtExamSerl;
            dr["SurvReqDt"] = grid11.SurvReqDt;
            dr["SurvCompDt"] = grid11.EndDate;
            dr["edi_id"] = grid11.edi_id;
            dr["parent_id"] = grid11.parent_id;
            dr["bis_code"] = grid11.bis_code;
            dr["WorkingTag"] = "A";

            // 조사위임변경정보
            dt = ds.Tables["DataBlock2"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["OurAcdtSurvDtlCode"] = grid22.OurAcdtSurvDtlCode;
            dr["CompAcdtSurvDtlCode"] = grid22.CompAcdtSurvDtlCode;
            dr["CompAcdtSurvDtlCode2"] = grid22.CompAcdtSurvDtlCode2;
            dr["AccuseYn"] = grid22.AccuseYn;
            dr["SurvDtlChgRsn"] = grid22.SurvDtlChgRsn;
            dr["id"] = grid22.id;
            dr["WorkingTag"] = "A";

            // 조사내용정보
            dt = ds.Tables["DataBlock3"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["SurvDangerCnts"] = rtfSurvDangerCnts.rtbDoc.Text;  // 조사위험내용
            dr["SurvResultCnts"] = rtfSurvResultCnts.rtbDoc.Text;  // 조사결과내용
            dr["ReviewOpinion"] = rtfReviewOpinion.rtbDoc.Text;    // 검토의견내용
            dr["SearchMatters"] = rtfSearchMatters.rtbDoc.Text;    // 검색사항내용
            dr["SurvDtlCnts"] = rtfSurvDtlCnts.rtbDoc.Text;        // 조사상세내용
            dr["GuideCnts"] = rtfGuideCnts.rtbDoc.Text;            // 안내사항
            dr["id"] = id3;
            dr["WorkingTag"] = "A";

            // 4. 손해사정 의견
            dt = ds.Tables["DataBlock4"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["TermsRelayCode"] = grid42.TermsRelayCode;      //약관전달방법코드
            dr["TermsRelayCnts"] = grid42.TermsRelayCnts;
            dr["HandSignCode"] = grid42.HandSignCode;          //자필서명방법코드
            dr["HandSignCnts"] = grid42.HandSignCnts;
            dr["ExTermsExplnCode"] = grid42.ExTermsExplnCode;  //면책약관설명방법코드
            dr["ExTermsExplnCnts"] = grid42.ExTermsExplnCnts;
            dr["ExTermsAplyCode"] = grid42.ExTermsAplyCode;    //면책약관적용구분코드
            dr["ExTermsAplyCnts"] = grid42.ExTermsAplyCnts;
            dr["ReqDocuAdeqCode"] = grid42.ReqDocuAdeqCode;    //구비서류적정성구분코드
            dr["ReqDocuAdeqCnts"] = grid42.ReqDocuAdeqCnts;
            dr["ExTermsUndstdLvlCode"] = grid42.ExTermsUndstdLvlCode;    //면책약관이해도등급코드
            dr["ExTermsUndstdLvlCnts"] = grid42.ExTermsUndstdLvlCnts;
            dr["WriterDisadvantageYn"] = grid42.WriterDisadvantageYn;    //작성자불이익적용코드
            dr["WriterDisadvantageCnts"] = grid42.WriterDisadvantageCnts;
            dr["CaseLawAdjReviewYn"] = grid42.CaseLawAdjReviewYn;        //판례조정사례검토여부
            dr["CaseLawAdjReviewCnts"] = grid42.CaseLawAdjReviewCnts;
            dr["ReviewPossibleYn"] = grid42.ReviewPossibleYn;            //재검토가능여부
            dr["ReviewPossibleCnts"] = grid42.ReviewPossibleCnts;
            dr["ExTermsTtlOpinionCode"] = grid42.ExTermsTtlOpinionCode;  //면책종합의견구분코드
            dr["ExTermsTtlOpinionCnts"] = grid42.ExTermsTtlOpinionCnts;
            dr["id"] = grid42.id;
            dr["WorkingTag"] = "A";

            // 조사수수료정보
            dt = ds.Tables["DataBlock5"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["AcdtSurvDtlCode"] = grid52.AcdtSurvDtlCode;
            dr["AcdtSurvVstCnt"] = grid52.AcdtSurvVstCnt;
            dr["AcdtSurvPassDay"] = grid52.AcdtSurvPassDay;
            dr["DefaultCost"] = Utils.ToDecimal(grid52.DefaultCost);
            dr["TransCost"] = Utils.ToDecimal(grid52.TransCost);
            dr["DocuCost"] = Utils.ToDecimal(grid52.DocuCost);
            dr["AdviceCost"] = Utils.ToDecimal(grid52.AdviceCost);
            dr["OtherCost"] = Utils.ToDecimal(grid52.OtherCost);
            dr["id"] = grid52.id;
            dr["WorkingTag"] = "A";

            // 병력경위사항
            GetSheetData(SS6, ds.Tables["DataBlock6"], "A;U");

            // 타사가입사항
            GetSheetData(SS7, ds.Tables["DataBlock7"], "A;U");

            // 민원안내사항
            GetSheetData(SS8, ds.Tables["DataBlock8"], "A;U");

            // 계약별계산결과내역
            GetSheetData(SS9, ds.Tables["DataBlock9"], "A;U");

            // 보유계약별계산결과내역
            GetSheetData(SS10, ds.Tables["DataBlock10"], "A;U");

            // 손해사정서교부사항
            GetSheetData(SS11, ds.Tables["DataBlock11"], "A;U");

            // 보고서첨부파일
            GetSheetData(SS12, ds.Tables["DataBlock12"], "A;U");

            // 1. 처리과정 사고처리과정표
            GetSheetData(SS14, ds.Tables["DataBlock14"], "A;U;");   //'A' + 'U' + '' = 전체

            return ds;
        }

        private void DeleteRows(DataGridView dgv)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string blockname = "";
                if (dgv == SS6) blockname = "DataBlock6";
                if (dgv == SS7) blockname = "DataBlock7";
                if (dgv == SS8) blockname = "DataBlock8";
                if (dgv == SS9) blockname = "DataBlock9";
                if (dgv == SS10) blockname = "DataBlock10";
                if (dgv == SS11) blockname = "DataBlock11";
                if (dgv == SS12) blockname = "DataBlock12";
                if (dgv == SS14) blockname = "DataBlock14";

                DataSet yds = null;
                if (DeleteData(GetDeleteData(dgv, blockname), out yds) == true)
                {
                    DataTable SA01 = yds.Tables["DataBlock1"];
                    DataRow drow = SA01.Rows[0];
                    grid11.edi_id = Utils.ToInt(drow["edi_id"]);
                    grid11.parent_id = Utils.ToInt(drow["parent_id"]);

                    DataTable SA16 = yds.Tables[blockname];
                    if (SA16 == null) return;
                    for (int ii = SA16.Rows.Count - 1; ii >= 0; ii--)
                    {
                        int idx = Utils.ToInt(SA16.Rows[ii]["IDX_NO"]);
                        if (dgv == SS12)
                        {
                            if (!fnFileDelete(dgv.Rows[idx - 1])) continue;
                        }
                        dgv.Rows.RemoveAt(idx - 1);
                    }
                    dgv.Refresh();
                    GridHeightResize(dgv);
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

        private bool DeleteData(DataSet pds, out DataSet yds)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptLina";
            security.methodId = "SheetDel";
            security.companySeq = param.CompanySeq;
            security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
            security.securityType = 1;
            security.userId = param.UserID;

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

        private DataSet GetDeleteData(DataGridView dgv, string blockname)
        {
            try
            {
                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptLina.xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(s_CAA_XSD);

                DataTable dt = ds.Tables["DataBlock1"];
                DataRow dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["AcdtNo"] = grid11.AcdtNo;
                dr["AcdtExamSerl"] = grid11.AcdtExamSerl;
                dr["edi_id"] = grid11.edi_id;
                dr["parent_id"] = grid11.parent_id;
                dr["bis_code"] = grid11.bis_code;

                DataTable dt2 = ds.Tables[blockname];
                GetSheetData(dgv, dt2, "D");

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private bool GetSheetData(DataGridView dgv, DataTable pdt, string fg)
        {
            try
            {
                pdt.Columns.Add("WorkingTag");
                pdt.Columns.Add("IDX_NO");
                pdt.Columns.Add("DataSeq");
                int seq = 1;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv.Rows[i].IsNewRow) continue;
                    string workingTag = WorkingTagCell(dgv.Rows[i]).Value + "";
                    if (Utils.GetPos(fg, ";", workingTag) < 1) continue;
                    DataRow dr = pdt.Rows.Add();
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!pdt.Columns.Contains(col.DataPropertyName)) continue;
                        object val = dgv.Rows[i].Cells[col.Name].Value;
                        if (col is DataGridViewCustomDateTimeInputColumn)
                        {
                            dr[col.DataPropertyName] = (val == null || val + "" == "" || val + "" == "0001-01-01 오전 12:00:00" ? "" : Utils.DateFormat(val, "yyyyMMdd"));
                        }
                        else if (col is DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn)
                        {
                            dr[col.DataPropertyName] = Utils.ToDecimal(val);
                        }
                        else if (col is DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn)
                        {
                            dr[col.DataPropertyName] = Utils.ToInt(val);
                        }
                        else dr[col.DataPropertyName] = (val == null ? DBNull.Value : val);
                    }
                    dr["WorkingTag"] = workingTag;
                    dr["IDX_NO"] = i + 1;
                    dr["Dataseq"] = seq;
                    seq++;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private DataGridViewCell WorkingTagCell(DataGridViewRow drow)
        {
            return drow.Cells[drow.DataGridView.GetPropertyColumn("WorkingTag")?.Name];
        }
    }
}
