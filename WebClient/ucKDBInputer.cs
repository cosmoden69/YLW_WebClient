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
    public partial class ucKDBInputer : UserControl
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
        private int parent_id3 { get; set; } = 0;

        public ucKDBInputer()
        {
            InitializeComponent();

            Utils.DoubleBuffered(SS1, true);
            Utils.DoubleBuffered(SS4, true);
            Utils.DoubleBuffered(SS5, true);
            Utils.DoubleBuffered(SS6, true);
            Utils.DoubleBuffered(SS7, true);
            Utils.DoubleBuffered(SS8, true);
            Utils.DoubleBuffered(SS9, true);
            Utils.DoubleBuffered(SS10, true);
            Utils.DoubleBuffered(SS12, true);
            Utils.DoubleBuffered(SS13, true);
            Utils.DoubleBuffered(SS13_2, true);
            Utils.DoubleBuffered(SS15, true);
            Utils.DoubleBuffered(SS16, true);

            this.rtfrqstCtnt.bShowMenu = false;
            this.rtfrfMtrCtnt.bShowMenu = false;
            this.rtfinvsgOpiCtnt.bShowMenu = false;
            this.rtfrltRuleCtnt.bShowMenu = false;
            this.rtfvrfcCtnt.bShowMenu = false;
            this.rtfrqstCtnt.MaxInputLength = 4000;
            this.rtfrfMtrCtnt.MaxInputLength = 4000;
            this.rtfinvsgOpiCtnt.MaxInputLength = 4000;
            this.rtfvrfcCtnt.MaxInputLength = 40000;
            this.rtfrltRuleCtnt.MaxInputLength = 4000;
            this.npayCtnt4.MaxInputLength = 200;
            this.mediCntxtVrfcCtnt5.MaxInputLength = 200;
            this.mediCntxtDtlCtnt5.MaxInputLength = 3000;
            this.intvwrDvsnNote7.MaxInputLength = 100;
            this.intvwCtnt7.MaxInputLength = 1000;
            this.chtInputCtnt9.MaxInputLength = 2000;
            this.mhCvappCtnt10.MaxInputLength = 200;
            this.SurvGuideCnts16.MaxInputLength = 120;
            this.SS1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS4.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS5.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS6.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS7.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS8.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS9.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS10.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS12.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS13.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS13_2.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS15.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.SS16.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

            this.Load += new System.EventHandler(this.Main_Load);
            this.btnPDFAuth.Click += BtnPDFAuth_Click;
            this.btnPDF1.Click += BtnPDF1_Click;
            this.btnPDF1Up.Click += BtnPDF1Up_Click;
            this.btnPDF2.Click += BtnPDF2_Click;
            this.btnPDF2Up.Click += BtnPDF2Up_Click;
            this.btnSS4Del.Click += BtnSS4Del_Click;
            this.btnSS5Del.Click += BtnSS5Del_Click;
            this.btnSS6Del.Click += BtnSS6Del_Click;
            this.btnSS7Del.Click += BtnSS7Del_Click;
            this.btnSS8Del.Click += BtnSS8Del_Click;
            this.btnSS9Del.Click += BtnSS9Del_Click;
            this.btnSS10Del.Click += BtnSS10Del_Click;
            this.btnSS12Del.Click += BtnSS12Del_Click;
            this.btnSS13Del.Click += BtnSS13Del_Click;
            this.btnSS13_2Del.Click += BtnSS13_2Del_Click;
            this.btnSS15Del.Click += BtnSS15Del_Click;
            this.btnSS15Add.Click += BtnSS15Add_Click;
            this.btnSS16Del.Click += BtnSS16Del_Click;
            this.SS1.RowPostPaint += Grid_RowPostPaint;
            this.SS4.RowPostPaint += Grid_RowPostPaint;
            this.SS5.RowPostPaint += Grid_RowPostPaint;
            this.SS6.RowPostPaint += Grid_RowPostPaint;
            this.SS7.RowPostPaint += Grid_RowPostPaint;
            this.SS8.RowPostPaint += Grid_RowPostPaint;
            this.SS9.RowPostPaint += Grid_RowPostPaint;
            this.SS10.RowPostPaint += Grid_RowPostPaint;
            this.SS12.RowPostPaint += Grid_RowPostPaint;
            this.SS13.RowPostPaint += Grid_RowPostPaint;
            this.SS13_2.RowPostPaint += Grid_RowPostPaint;
            this.SS15.RowPostPaint += Grid_RowPostPaint;
            this.SS16.RowPostPaint += Grid_RowPostPaint;
            this.SS1.CellBeginEdit += Grid_CellBeginEdit;
            this.SS4.CellBeginEdit += Grid_CellBeginEdit;
            this.SS5.CellBeginEdit += Grid_CellBeginEdit;
            this.SS6.CellBeginEdit += Grid_CellBeginEdit;
            this.SS7.CellBeginEdit += Grid_CellBeginEdit;
            this.SS8.CellBeginEdit += Grid_CellBeginEdit;
            this.SS9.CellBeginEdit += Grid_CellBeginEdit;
            this.SS10.CellBeginEdit += Grid_CellBeginEdit;
            this.SS12.CellBeginEdit += Grid_CellBeginEdit;
            this.SS13.CellBeginEdit += Grid_CellBeginEdit;
            this.SS13_2.CellBeginEdit += Grid_CellBeginEdit;
            this.SS15.CellBeginEdit += Grid_CellBeginEdit;
            this.SS16.CellBeginEdit += Grid_CellBeginEdit;
            this.SS1.CellEndEdit += Grid_CellEndEdit;
            this.SS4.CellEndEdit += Grid_CellEndEdit;
            this.SS5.CellEndEdit += Grid_CellEndEdit;
            this.SS6.CellEndEdit += Grid_CellEndEdit;
            this.SS7.CellEndEdit += Grid_CellEndEdit;
            this.SS8.CellEndEdit += Grid_CellEndEdit;
            this.SS9.CellEndEdit += Grid_CellEndEdit;
            this.SS10.CellEndEdit += Grid_CellEndEdit;
            this.SS12.CellEndEdit += Grid_CellEndEdit;
            this.SS13.CellEndEdit += Grid_CellEndEdit;
            this.SS13_2.CellEndEdit += Grid_CellEndEdit;
            this.SS15.CellEndEdit += Grid_CellEndEdit;
            this.SS16.CellEndEdit += Grid_CellEndEdit;
            this.SS1.CellLeave += Grid_CellLeave;
            this.SS4.CellLeave += Grid_CellLeave;
            this.SS5.CellLeave += Grid_CellLeave;
            this.SS6.CellLeave += Grid_CellLeave;
            this.SS7.CellLeave += Grid_CellLeave;
            this.SS8.CellLeave += Grid_CellLeave;
            this.SS9.CellLeave += Grid_CellLeave;
            this.SS10.CellLeave += Grid_CellLeave;
            this.SS12.CellLeave += Grid_CellLeave;
            this.SS13.CellLeave += Grid_CellLeave;
            this.SS13_2.CellLeave += Grid_CellLeave;
            this.SS15.CellLeave += Grid_CellLeave;
            this.SS16.CellLeave += Grid_CellLeave;
            this.SS1.CellEnter += Grid_CellEnter;
            this.SS4.CellEnter += Grid_CellEnter;
            this.SS5.CellEnter += Grid_CellEnter;
            this.SS6.CellEnter += Grid_CellEnter;
            this.SS7.CellEnter += Grid_CellEnter;
            this.SS8.CellEnter += Grid_CellEnter;
            this.SS9.CellEnter += Grid_CellEnter;
            this.SS10.CellEnter += Grid_CellEnter;
            this.SS12.CellEnter += Grid_CellEnter;
            this.SS13.CellEnter += Grid_CellEnter;
            this.SS13_2.CellEnter += Grid_CellEnter;
            this.SS15.CellEnter += Grid_CellEnter;
            this.SS16.CellEnter += Grid_CellEnter;
            this.SS1.DataError += Grid_DataError;
            this.SS4.DataError += Grid_DataError;
            this.SS5.DataError += Grid_DataError;
            this.SS6.DataError += Grid_DataError;
            this.SS7.DataError += Grid_DataError;
            this.SS8.DataError += Grid_DataError;
            this.SS9.DataError += Grid_DataError;
            this.SS10.DataError += Grid_DataError;
            this.SS12.DataError += Grid_DataError;
            this.SS13.DataError += Grid_DataError;
            this.SS13_2.DataError += Grid_DataError;
            this.SS15.DataError += Grid_DataError;
            this.SS16.DataError += Grid_DataError;
            this.SS1.CellValueChanged += Grid_CellValueChanged;
            this.SS4.CellValueChanged += Grid_CellValueChanged;
            this.SS5.CellValueChanged += Grid_CellValueChanged;
            this.SS6.CellValueChanged += Grid_CellValueChanged;
            this.SS7.CellValueChanged += Grid_CellValueChanged;
            this.SS8.CellValueChanged += Grid_CellValueChanged;
            this.SS9.CellValueChanged += Grid_CellValueChanged;
            this.SS10.CellValueChanged += Grid_CellValueChanged;
            this.SS12.CellValueChanged += Grid_CellValueChanged;
            this.SS13.CellValueChanged += Grid_CellValueChanged;
            this.SS13_2.CellValueChanged += Grid_CellValueChanged;
            this.SS15.CellValueChanged += Grid_CellValueChanged;
            this.SS16.CellValueChanged += Grid_CellValueChanged;
            this.SS1.EditingControlShowing += Grid_EditingControlShowing;
            this.SS4.EditingControlShowing += Grid_EditingControlShowing;
            this.SS5.EditingControlShowing += Grid_EditingControlShowing;
            this.SS6.EditingControlShowing += Grid_EditingControlShowing;
            this.SS7.EditingControlShowing += Grid_EditingControlShowing;
            this.SS8.EditingControlShowing += Grid_EditingControlShowing;
            this.SS9.EditingControlShowing += Grid_EditingControlShowing;
            this.SS10.EditingControlShowing += Grid_EditingControlShowing;
            this.SS12.EditingControlShowing += Grid_EditingControlShowing;
            this.SS13.EditingControlShowing += Grid_EditingControlShowing;
            this.SS13_2.EditingControlShowing += Grid_EditingControlShowing;
            this.SS15.EditingControlShowing += Grid_EditingControlShowing;
            this.SS16.EditingControlShowing += Grid_EditingControlShowing;
            this.SS1.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS4.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS5.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS6.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS7.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS8.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS9.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS10.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS12.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS13.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS13_2.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS15.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS16.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.SS1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS4.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS5.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS6.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS7.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS8.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS9.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS10.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS12.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS13.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS13_2.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS15.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS16.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowAdd);
            this.SS1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS4.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS5.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS6.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS7.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS8.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS9.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS10.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS12.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS13.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS13_2.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS15.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS16.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Grid_RowDel);
            this.SS13.CurrentCellChanged += SS13_CurrentCellChanged;
            this.SS15.CellContentClick += Grid_CellContentClick;
            this.SS16.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SS16_CellValueChanged);
            this.rtfrqstCtnt.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfrfMtrCtnt.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfinvsgOpiCtnt.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfrltRuleCtnt.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.rtfvrfcCtnt.rtbDoc.MouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid62.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid72.ContentsMouseWheel += new MouseEventHandler(Mouse_Wheel);
            this.grid62.InvsgDcdChanged += Grid62_InvsgDcdChanged;
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
            this.grid62.Width = width;
            this.panelEx29.Width = width;
            this.rtfrqstCtnt.Width = width;
            this.panelEx30.Width = width;
            this.rtfrfMtrCtnt.Width = width;
            this.panelEx32.Width = width;
            this.rtfinvsgOpiCtnt.Width = width;
            this.panelEx34.Width = width;
            this.rtfrltRuleCtnt.Width = width;
            this.panelEx28.Width = width;
            this.rtfvrfcCtnt.Width = width;
            this.panelEx37.Width = width;
            this.panelEx41.Width = width;
            this.SS4.Width = width;
            this.panelEx38.Width = width;
            this.SS5.Width = width;
            this.panelEx39.Width = width;
            this.SS6.Width = width;
            this.panelEx40.Width = width;
            this.SS7.Width = width;
            this.panelEx43.Width = width;
            this.SS8.Width = width;
            this.panelEx44.Width = width;
            this.SS9.Width = width;
            this.panelEx42.Width = width;
            this.SS10.Width = width;
            this.panelEx48.Width = width;
            this.grid72.Width = width;
            this.panelEx49.Width = width;
            this.SS12.Width = width;
            this.panelEx47.Width = width;
            this.SS13.Width = width;
            this.panelEx45.Width = width;
            this.SS13_2.Width = width;
            this.panelEx73.Width = width;
            this.SS15.Width = width;
            this.panelEx75.Width = width;
            this.panelEx76.Width = width;
            this.SS16.Width = width;
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
                grid62.SetFocus();
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
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS A WITH(NOLOCK) ";
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
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00084' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00084 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00084.TableName = "CD_PA00084";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00085' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00085 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00085.TableName = "CD_PA00085";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00086' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00086 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00086.TableName = "CD_PA00086";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00126' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00126 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00126.TableName = "CD_PA00126";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00145' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00145 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00145.TableName = "CD_PA00145";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00155' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00155 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00155.TableName = "CD_PA00155";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00156' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00156 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00156.TableName = "CD_PA00156";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00173' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00173 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00173.TableName = "CD_PA00173";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00174' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00174 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00174.TableName = "CD_PA00174";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00206' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00206 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00206.TableName = "CD_PA00206";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00238' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00238 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00238.TableName = "CD_PA00238";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00327' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00327 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00327.TableName = "CD_PA00327";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00329' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00329 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00329.TableName = "CD_PA00329";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00331' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00331 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00331.TableName = "CD_PA00331";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00332' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00332 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00332.TableName = "CD_PA00332";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00355' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00355 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00355.TableName = "CD_PA00355";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00492' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00492 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00492.TableName = "CD_PA00492";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_PA00589' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_PA00589 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_PA00589.TableName = "CD_PA00589";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_CN00086' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_CN00086 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_CN00086.TableName = "CD_CN00086";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_CN00142' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_CN00142 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_CN00142.TableName = "CD_CN00142";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'CD_CS00039' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable CD_CS00039 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                CD_CS00039.TableName = "CD_CS00039";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_INSJOBCD' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_INSJOBCD = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_INSJOBCD.TableName = "USERCD_INSJOBCD";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName, mnr.value_remark ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_SURVGB2' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_SURVGB2 = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_SURVGB2.TableName = "USERCD_SURVGB2";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'YESNO_CD' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable YESNO_CD = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                YESNO_CD.TableName = "YESNO_CD";

                strSql = "";
                strSql += " SELECT mnr.code AS MinorSeq, mnr.value_text AS MinorName ";
                strSql += " FROM   _TAdjSL_KDB_CODE_DTL AS mnr WITH(NOLOCK) ";
                strSql += " WHERE  mnr.CompanySeq = '" + Utils.ConvertToString(param.CompanySeq) + "' ";
                strSql += " AND    mnr.code_id = 'USERCD_FILETYPE' ";
                strSql += " ORDER BY mnr.sort_order ";
                strSql += " FOR JSON PATH ";
                DataTable USERCD_FILETYPE = YLWService.MTRServiceModule.GetMTRServiceDataTable(param.CompanySeq, strSql);
                USERCD_FILETYPE.TableName = "USERCD_FILETYPE";

                SetCombo(InsurStatCode1, CD_CN00142.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(invsgRcd4, CD_PA00126.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(invsgRstDtlcd4, CD_PA00145.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mediCntxtBzDcd5, CD_PA00329.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(rmdyTpcd5, CD_PA00355.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(otcoEntInscoCd6, CD_PA00492.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(clmRscd6, CD_PA00238.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(intvwPlceCd7, CD_PA00084.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(intvwrDcd7, CD_PA00327.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(cstTpcd8, CD_PA00589.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(chtTpcd9, CD_PA00331.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(chtInputTpcd9, CD_PA00332.Copy(), "MinorSeq", "MinorName", true);

                SetCombo(mhCvappDcd10, CD_PA00086.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mhTpcd10, CD_PA00174.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mhRscd10, CD_PA00173.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mhRltpDcd10, CD_PA00085.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mhRltDgrDcd10, CD_PA00155.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(mhGcd10, CD_PA00156.Copy(), "MinorSeq", "MinorName", true);

                SetCombo(sancYn13, YESNO_CD.Copy(), "MinorSeq", "MinorName", true);

                SetCombo(custContRelcd13_2, CD_CN00086.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(sensInfoAsntYn13_2, YESNO_CD.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(crinfiqAsntYn13_2, YESNO_CD.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(consgRfsYn13_2, YESNO_CD.Copy(), "MinorSeq", "MinorName", true);
                SetCombo(sndMcd13_2, CD_CS00039.Copy(), "MinorSeq", "MinorName", true);

                SetCombo(file_type15, USERCD_FILETYPE.Copy(), "MinorSeq", "MinorName", true);

                DataSet dsp6 = new DataSet();
                dsp6.Tables.Add(CD_PA00206.Copy());
                dsp6.Tables.Add(USERCD_SURVGB2.Copy());
                dsp6.Tables.Add(USERCD_INSJOBCD.Copy());
                grid62.Init_Set(dsp6);

                DataSet dsp7 = new DataSet();
                dsp7.Tables.Add(CD_PA00238.Copy());
                grid72.Init_Set(dsp7);
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
                else if (SS4.CurrentCell is DataGridViewTextBoxCell && SS4.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS5.CurrentCell is DataGridViewTextBoxCell && SS5.CurrentCell.IsInEditMode)
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
                else if (SS12.CurrentCell is DataGridViewTextBoxCell && SS12.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS13.CurrentCell is DataGridViewTextBoxCell && SS13.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS13_2.CurrentCell is DataGridViewTextBoxCell && SS13_2.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS15.CurrentCell is DataGridViewTextBoxCell && SS15.CurrentCell.IsInEditMode)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else if (SS16.CurrentCell is DataGridViewTextBoxCell && SS16.CurrentCell.IsInEditMode)
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

            // 조사자정보
            grid62.SetReadOnlyMode(rdonly);

            // 조사내용정보
            rtfrqstCtnt.rtbDoc.ReadOnly = rdonly;
            rtfrfMtrCtnt.rtbDoc.ReadOnly = rdonly;
            rtfinvsgOpiCtnt.rtbDoc.ReadOnly = rdonly;
            rtfrltRuleCtnt.rtbDoc.ReadOnly = rdonly;
            rtfvrfcCtnt.rtbDoc.ReadOnly = rdonly;

            // 지급불가안내
            grid72.SetReadOnlyMode(rdonly);

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

            // 조사자정보
            grid62.Clear();

            // 조사내용정보
            rtfrqstCtnt.rtbDoc.Text = "";
            rtfrfMtrCtnt.rtbDoc.Text = "";
            rtfinvsgOpiCtnt.rtbDoc.Text = "";
            rtfrltRuleCtnt.rtbDoc.Text = "";
            rtfvrfcCtnt.rtbDoc.Text = "";
            id3 = 0;
            parent_id3 = 0;

            SS1.Rows.Clear();
            SS1.Height = 76;

            SS4.Rows.Clear();
            SS4.Height = 76;

            SS5.Rows.Clear();
            SS5.Height = 76;

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

            // 지급불가안내
            grid72.Clear();

            SS12.Rows.Clear();
            SS12.Height = 76;

            SS13.Rows.Clear();
            SS13.Height = 76;

            SS13_2.Rows.Clear();
            SS13_2.Height = 76;

            SS15.Rows.Clear();
            SS15.Height = 76;

            SS16.Rows.Clear();
            SS16.Height = 76;

            ini_111.ValueObject = 0;      // 총 처리일수 
            ini_112.ValueObject = 0;      // 총 지연일수 ( 제외 )
            ini_113.ValueObject = 0;      // 총 귀책일수  

            nPrevLineIndex = -1;

            _bEvent = true;
        }

        private void ClearSS13_2()
        {
            _bEvent = false;

            SS13_2.Rows.Clear();
            SS13_2.Height = 76;

            _bEvent = true;
        }

        private void XmlData_Read(DataSet yds)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearAll();

                string xml = yds.GetXml();

                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptKDB.xsd";
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
                    grid11.AcdtNo = drow["acrcNo"] + "";
                    grid11.AcdtExamSerl = drow["acciUndwSeq"] + "";
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

                // 계약사항
                SS1.AllowUserToAddRows = false;
                DataTable SA17 = pds.Tables["DataBlock17"];
                if (SA17 != null && SA17.Rows.Count > 0)
                {
                    for (int i = 0; i < SA17.Rows.Count; i++)
                    {
                        DataRow drow = SA17.Rows[i];
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

                //// 조사자정보
                DataTable SA02 = pds.Tables["DataBlock2"];
                if (SA02 != null && SA02.Rows.Count > 0)
                {
                    DataRow drow = SA02.Rows[0];
                    grid62.invgrEno = drow["invgrEno"] + "";
                    //grid62.invsgDcd = drow["invsgDcd"] + "";
                    grid62.invsgDifcCd = drow["invsgDifcCd"] + "";
                    grid62.accipInsJobCd = drow["accipInsJobCd"] + "";
                    grid62.invsgDcd2 = drow["invsgDcd2"] + "";
                    grid62.id = Utils.ToInt(drow["id"]);
                }

                // 조사내용정보
                DataTable SA03 = pds.Tables["DataBlock3"];
                if (SA03 != null && SA03.Rows.Count > 0)
                {
                    DataRow drow = SA03.Rows[0];
                    rtfrqstCtnt.rtbDoc.Text = drow["rqstCtnt"] + "";           // 의뢰내용
                    rtfvrfcCtnt.rtbDoc.Text = drow["vrfcCtnt"] + "";           // 확인내용
                    rtfrltRuleCtnt.rtbDoc.Text = drow["rltRuleCtnt"] + "";     // 관련규정
                    rtfinvsgOpiCtnt.rtbDoc.Text = drow["invsgOpiCtnt"] + "";   // 조사의견
                    rtfrfMtrCtnt.rtbDoc.Text = drow["rfMtrCtnt"] + "";         // 참고사항
                    id3 = Utils.ToInt(drow["id"]);
                    parent_id3 = Utils.ToInt(drow["parent_id"]);
                }

                // 계약별계산결과내용
                SS4.AllowUserToAddRows = false;
                DataTable SA04 = pds.Tables["DataBlock4"];
                if (SA04 != null && SA04.Rows.Count > 0)
                {
                    for (int i = 0; i < SA04.Rows.Count; i++)
                    {
                        DataRow drow = SA04.Rows[i];
                        SS4.Rows.Add();
                        SS4.Rows[SS4.RowCount - 1].Cells["contNo4"].Value = drow["contNo"];
                        SS4.Rows[SS4.RowCount - 1].Cells["contclClmSeq4"].Value = drow["contclClmSeq"];
                        SS4.Rows[SS4.RowCount - 1].Cells["invsgRcd4"].Value = drow["invsgRcd"];
                        SS4.Rows[SS4.RowCount - 1].Cells["invsgRstDtlcd4"].Value = drow["invsgRstDtlcd"];
                        SS4.Rows[SS4.RowCount - 1].Cells["clmInsm4"].Value = Utils.ToDouble(drow["clmInsm"]);
                        SS4.Rows[SS4.RowCount - 1].Cells["payAmt4"].Value = Utils.ToDouble(drow["payAmt"]);
                        SS4.Rows[SS4.RowCount - 1].Cells["npayAmt4"].Value = Utils.ToDouble(drow["npayAmt"]);
                        SS4.Rows[SS4.RowCount - 1].Cells["npayCtnt4"].Value = Utils.ToMultiline(drow["npayCtnt"]);
                        SS4.Rows[SS4.RowCount - 1].Cells["id4"].Value = drow["id"];
                        SS4.Rows[SS4.RowCount - 1].Cells["parent_id4"].Value = drow["parent_id"];
                        SS4.Rows[SS4.RowCount - 1].Cells["A3_id4"].Value = drow["A3_id"];
                        SS4.Rows[SS4.RowCount - 1].Cells["WorkingTag4"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS4.AllowUserToAddRows = true;
                }
                SS4.AutoResizeRows();
                GridHeightResize(SS4);

                // 병력경위사항
                SS5.AllowUserToAddRows = false;
                DataTable SA05 = pds.Tables["DataBlock5"];
                if (SA05 != null && SA05.Rows.Count > 0)
                {
                    for (int i = 0; i < SA05.Rows.Count; i++)
                    {
                        DataRow drow = SA05.Rows[i];
                        SS5.Rows.Add();
                        SS5.Rows[SS5.RowCount - 1].Cells["mediCntxtBzDcd5"].Value = drow["mediCntxtBzDcd"];
                        SS5.Rows[SS5.RowCount - 1].Cells["rmdyStrtDt5"].Value = Utils.ConvertToDateTime(drow["rmdyStrtDt"]);
                        SS5.Rows[SS5.RowCount - 1].Cells["rmdyEndDt5"].Value = Utils.ConvertToDateTime(drow["rmdyEndDt"]);
                        SS5.Rows[SS5.RowCount - 1].Cells["mediCntxtVrfcCtnt5"].Value = Utils.ToMultiline(drow["mediCntxtVrfcCtnt"]);
                        SS5.Rows[SS5.RowCount - 1].Cells["mediCntxtVrfcInstNm5"].Value = drow["mediCntxtVrfcInstNm"];
                        SS5.Rows[SS5.RowCount - 1].Cells["mediCntxtDtlCtnt5"].Value = Utils.ToMultiline(drow["mediCntxtDtlCtnt"]);
                        SS5.Rows[SS5.RowCount - 1].Cells["mediCntxtDocNm5"].Value = drow["mediCntxtDocNm"];
                        SS5.Rows[SS5.RowCount - 1].Cells["rmdyTpcd5"].Value = drow["rmdyTpcd"];
                        SS5.Rows[SS5.RowCount - 1].Cells["fcausCd5"].Value = drow["fcausCd"];
                        SS5.Rows[SS5.RowCount - 1].Cells["id5"].Value = drow["id"];
                        SS5.Rows[SS5.RowCount - 1].Cells["parent_id5"].Value = drow["parent_id"];
                        SS5.Rows[SS5.RowCount - 1].Cells["WorkingTag5"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS5.AllowUserToAddRows = true;
                }
                SS5.AutoResizeRows();
                GridHeightResize(SS5);

                // 타사가입사항
                SS6.AllowUserToAddRows = false;
                DataTable SA06 = pds.Tables["DataBlock6"];
                if (SA06 != null && SA06.Rows.Count > 0)
                {
                    for (int i = 0; i < SA06.Rows.Count; i++)
                    {
                        DataRow drow = SA06.Rows[i];
                        SS6.Rows.Add();
                        SS6.Rows[SS6.RowCount - 1].Cells["otcoEntInscoCd6"].Value = drow["otcoEntInscoCd"];
                        SS6.Rows[SS6.RowCount - 1].Cells["otcoEntPrdnm6"].Value = drow["otcoEntPrdnm"];
                        SS6.Rows[SS6.RowCount - 1].Cells["clmRscd6"].Value = drow["clmRscd"];
                        SS6.Rows[SS6.RowCount - 1].Cells["octoClmAmt6"].Value = Utils.ToDouble(drow["octoClmAmt"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["octoPayAmt6"].Value = Utils.ToDouble(drow["octoPayAmt"]);
                        SS6.Rows[SS6.RowCount - 1].Cells["id6"].Value = drow["id"];
                        SS6.Rows[SS6.RowCount - 1].Cells["parent_id6"].Value = drow["parent_id"];
                        SS6.Rows[SS6.RowCount - 1].Cells["WorkingTag6"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS6.AllowUserToAddRows = true;
                }
                SS6.AutoResizeRows();
                GridHeightResize(SS6);

                // 조사면담내용
                SS7.AllowUserToAddRows = false;
                DataTable SA07 = pds.Tables["DataBlock7"];
                if (SA07 != null && SA07.Rows.Count > 0)
                {
                    for (int i = 0; i < SA07.Rows.Count; i++)
                    {
                        DataRow drow = SA07.Rows[i];
                        SS7.Rows.Add();
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwDt7"].Value = Utils.ConvertToDateTime(drow["intvwDt"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwHm7"].Value = drow["intvwHm"];
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwPlceCd7"].Value = drow["intvwPlceCd"];
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwrDcd7"].Value = drow["intvwrDcd"];
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwrDvsnNote7"].Value = Utils.ToMultiline(drow["intvwrDvsnNote"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["intvwCtnt7"].Value = Utils.ToMultiline(drow["intvwCtnt"]);
                        SS7.Rows[SS7.RowCount - 1].Cells["id7"].Value = drow["id"];
                        SS7.Rows[SS7.RowCount - 1].Cells["parent_id7"].Value = drow["parent_id"];
                        SS7.Rows[SS7.RowCount - 1].Cells["WorkingTag7"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS7.AllowUserToAddRows = true;
                }
                SS7.AutoResizeRows();
                GridHeightResize(SS7);

                // 조사수수료정보
                SS8.AllowUserToAddRows = false;
                DataTable SA08 = pds.Tables["DataBlock8"];
                if (SA08 != null && SA08.Rows.Count > 0)
                {
                    for (int i = 0; i < SA08.Rows.Count; i++)
                    {
                        DataRow drow = SA08.Rows[i];
                        SS8.Rows.Add();
                        SS8.Rows[SS8.RowCount - 1].Cells["acinvVstCmpyCnt8"].Value = Utils.ToInt(drow["acinvVstCmpyCnt"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["acinvGobyDys8"].Value = Utils.ToInt(drow["acinvGobyDys"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["cstTpcd8"].Value = drow["cstTpcd"];
                        SS8.Rows[SS8.RowCount - 1].Cells["cstUseDt8"].Value = Utils.ConvertToDateTime(drow["cstUseDt"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["cstAmt8"].Value = Utils.ToDouble(drow["cstAmt"]);
                        SS8.Rows[SS8.RowCount - 1].Cells["id8"].Value = drow["id"];
                        SS8.Rows[SS8.RowCount - 1].Cells["parent_id8"].Value = drow["parent_id"];
                        SS8.Rows[SS8.RowCount - 1].Cells["WorkingTag8"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS8.AllowUserToAddRows = true;
                }
                SS8.AutoResizeRows();
                GridHeightResize(SS8);

                // 차트열람정보
                SS9.AllowUserToAddRows = false;
                DataTable SA09 = pds.Tables["DataBlock9"];
                if (SA09 != null && SA09.Rows.Count > 0)
                {
                    for (int i = 0; i < SA09.Rows.Count; i++)
                    {
                        DataRow drow = SA09.Rows[i];
                        SS9.Rows.Add();
                        SS9.Rows[SS9.RowCount - 1].Cells["acciDt9"].Value = Utils.ConvertToDateTime(drow["acciDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["chtTpcd9"].Value = drow["chtTpcd"];
                        SS9.Rows[SS9.RowCount - 1].Cells["rmdyHospNo9"].Value = drow["rmdyHospNo"];
                        SS9.Rows[SS9.RowCount - 1].Cells["rmdyHospNm9"].Value = drow["rmdyHospNm"];
                        SS9.Rows[SS9.RowCount - 1].Cells["rmdyDrLcnNo9"].Value = drow["rmdyDrLcnNo"];
                        SS9.Rows[SS9.RowCount - 1].Cells["rmdyDrNm9"].Value = drow["rmdyDrNm"];
                        SS9.Rows[SS9.RowCount - 1].Cells["fstmdDt9"].Value = Utils.ConvertToDateTime(drow["fstmdDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["diagDt9"].Value = Utils.ConvertToDateTime(drow["diagDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["hspzDt9"].Value = Utils.ConvertToDateTime(drow["hspzDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["surgDt9"].Value = Utils.ConvertToDateTime(drow["surgDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["inspMsrmDt9"].Value = Utils.ConvertToDateTime(drow["inspMsrmDt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["fstmdHospNo9"].Value = drow["fstmdHospNo"];
                        SS9.Rows[SS9.RowCount - 1].Cells["diagNm9"].Value = drow["diagNm"];
                        SS9.Rows[SS9.RowCount - 1].Cells["surgNm9"].Value = drow["surgNm"];
                        SS9.Rows[SS9.RowCount - 1].Cells["chtInputTpcd9"].Value = drow["chtInputTpcd"];
                        SS9.Rows[SS9.RowCount - 1].Cells["chtInputCtnt9"].Value = Utils.ToMultiline(drow["chtInputCtnt"]);
                        SS9.Rows[SS9.RowCount - 1].Cells["id9"].Value = drow["id"];
                        SS9.Rows[SS9.RowCount - 1].Cells["parent_id9"].Value = drow["parent_id"];
                        SS9.Rows[SS9.RowCount - 1].Cells["WorkingTag9"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS9.AllowUserToAddRows = true;
                }
                SS9.AutoResizeRows();
                GridHeightResize(SS9);

                // 모랄해저드정보
                SS10.AllowUserToAddRows = false;
                DataTable SA10 = pds.Tables["DataBlock10"];
                if (SA10 != null && SA10.Rows.Count > 0)
                {
                    for (int i = 0; i < SA10.Rows.Count; i++)
                    {
                        DataRow drow = SA10.Rows[i];
                        SS10.Rows.Add();
                        SS10.Rows[SS10.RowCount - 1].Cells["mhCvappDcd10"].Value = drow["mhCvappDcd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhTpcd10"].Value = drow["mhTpcd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhRscd10"].Value = drow["mhRscd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhRltpDcd10"].Value = drow["mhRltpDcd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhRltDgrDcd10"].Value = drow["mhRltDgrDcd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhGcd10"].Value = drow["mhGcd"];
                        SS10.Rows[SS10.RowCount - 1].Cells["mhCvappCtnt10"].Value = Utils.ToMultiline(drow["mhCvappCtnt"]);
                        SS10.Rows[SS10.RowCount - 1].Cells["id10"].Value = drow["id"];
                        SS10.Rows[SS10.RowCount - 1].Cells["parent_id10"].Value = drow["parent_id"];
                        SS10.Rows[SS10.RowCount - 1].Cells["WorkingTag10"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS10.AllowUserToAddRows = true;
                }
                SS10.AutoResizeRows();
                GridHeightResize(SS10);

                //// 지급불가안내
                DataTable SA11 = pds.Tables["DataBlock11"];
                if (SA11 != null && SA11.Rows.Count > 0)
                {
                    DataRow drow = SA11.Rows[0];
                    grid72.clmRscd = drow["clmRscd"] + "";
                    grid72.payNotcCtnt = drow["payNotcCtnt"] + "";
                    grid72.rltRuleCtnt = drow["rltRuleCtnt"] + "";
                    grid72.id = Utils.ToInt(drow["id"]);
                    grid72.parent_id = Utils.ToInt(drow["parent_id"]);
                }

                // 지급불가 - 계약번호
                SS12.AllowUserToAddRows = false;
                DataTable SA12 = pds.Tables["DataBlock12"];
                if (SA12 != null && SA12.Rows.Count > 0)
                {
                    for (int i = 0; i < SA12.Rows.Count; i++)
                    {
                        DataRow drow = SA12.Rows[i];
                        SS12.Rows.Add();
                        SS12.Rows[SS12.RowCount - 1].Cells["contNo12"].Value = drow["contNo"];
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

                // 손해사정서교부사항
                SS13.AllowUserToAddRows = false;
                DataTable SA13 = pds.Tables["DataBlock13"];
                if (SA13 != null && SA13.Rows.Count > 0)
                {
                    for (int i = 0; i < SA13.Rows.Count; i++)
                    {
                        DataRow drow = SA13.Rows[i];
                        SS13.Rows.Add();
                        SS13.Rows[SS13.RowCount - 1].Cells["accipNm13"].Value = drow["accipNm"];
                        SS13.Rows[SS13.RowCount - 1].Cells["contNo13"].Value = drow["contNo"];
                        SS13.Rows[SS13.RowCount - 1].Cells["acrcDt13"].Value = Utils.ConvertToDateTime(drow["acrcDt"]);
                        SS13.Rows[SS13.RowCount - 1].Cells["InvgrNm13"].Value = drow["InvgrNm"];
                        SS13.Rows[SS13.RowCount - 1].Cells["invgrEno13"].Value = drow["invgrEno"];
                        SS13.Rows[SS13.RowCount - 1].Cells["sancYn13"].Value = drow["sancYn"];
                        SS13.Rows[SS13.RowCount - 1].Cells["sancDt13"].Value = Utils.ConvertToDateTime(drow["sancDt"]);
                        SS13.Rows[SS13.RowCount - 1].Cells["id13"].Value = drow["id"];
                        SS13.Rows[SS13.RowCount - 1].Cells["parent_id13"].Value = drow["parent_id"];
                        SS13.Rows[SS13.RowCount - 1].Cells["WorkingTag13"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                }
            }
                if (!ReadOnlyMode)
                {
                    SS13.AllowUserToAddRows = true;
                }
                SS13.AutoResizeRows();
                GridHeightResize(SS13);

                //// 손해사정서교부사항
                //SS13_2.AllowUserToAddRows = false;
                //DataTable SA14 = pds.Tables["DataBlock14"];
                //if (SA14 != null && SA14.Rows.Count > 0)
                //{
                //    for (int i = 0; i < SA14.Rows.Count; i++)
                //    {
                //        DataRow drow = SA14.Rows[i];
                //        SS13_2.Rows.Add();
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["custContRelcd13_2"].Value = drow["custContRelcd"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["custNm13_2"].Value = drow["custNm"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sensInfoAsntYn13_2"].Value = drow["sensInfoAsntYn"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["crinfiqAsntYn13_2"].Value = drow["crinfiqAsntYn"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["consgRfsYn13_2"].Value = drow["consgRfsYn"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["adjrNm13_2"].Value = drow["adjrNm"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["adjrEno13_2"].Value = drow["adjrEno"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sndMcd13_2"].Value = drow["sndMcd"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sndDt13_2"].Value = Utils.ConvertToDateTime(drow["sndDt"]);
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["id13_2"].Value = drow["id"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["parent_id13_2"].Value = drow["parent_id"];
                //        SS13_2.Rows[SS13_2.RowCount - 1].Cells["WorkingTag13_2"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                //    }
                //}
                //if (!ReadOnlyMode)
                //{
                //    SS13_2.AllowUserToAddRows = true;
                //}
                //SS13_2.AutoResizeRows();
                //GridHeightResize(SS13_2);

                // 보고서첨부파일
                SS15.AllowUserToAddRows = false;
                DataTable SA15 = pds.Tables["DataBlock15"];
                if (SA15 != null && SA15.Rows.Count > 0)
                {
                    for (int i = 0; i < SA15.Rows.Count; i++)
                    {
                        DataRow drow = SA15.Rows[i];
                        SS15.Rows.Add();
                        SS15.Rows[SS15.RowCount - 1].Cells["file_type15"].Value = drow["file_type"];
                        SS15.Rows[SS15.RowCount - 1].Cells["file_name15"].Value = drow["file_name"];
                        SS15.Rows[SS15.RowCount - 1].Cells["file_rmk15"].Value = drow["file_rmk"];
                        SS15.Rows[SS15.RowCount - 1].Cells["file_seq15"].Value = drow["file_seq"];
                        SS15.Rows[SS15.RowCount - 1].Cells["id15"].Value = drow["id"];
                        SS15.Rows[SS15.RowCount - 1].Cells["parent_id15"].Value = drow["parent_id"];
                        SS15.Rows[SS15.RowCount - 1].Cells["WorkingTag15"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS15.AllowUserToAddRows = true;
                }
                SS15.AutoResizeRows();
                GridHeightResize(SS15);

                SS16.AllowUserToAddRows = false;
                // 1. 처리과정 - 사고처리과정표 
                DataTable SA16 = pds.Tables["DataBlock16"];
                if (SA16 != null && SA16.Rows.Count > 0)
                {
                    for (int i = 0; i < SA16.Rows.Count; i++)
                    {
                        DataRow drow = SA16.Rows[i];

                        SS16.Rows.Add();

                        DateTime mydate = Utils.ConvertToDateTime(drow["PrgMgtDt"]);

                        SS16.Rows[SS16.RowCount - 1].Cells["PrgMgtDt16"].Value = mydate;                         // 일자
                        SS16.Rows[SS16.RowCount - 1].Cells["DayCnt16"].Value = drow["DayCnt"];                   // 일수
                        SS16.Rows[SS16.RowCount - 1].Cells["PrgMgtHed16"].Value = drow["PrgMgtHed"];             // 항목
                        SS16.Rows[SS16.RowCount - 1].Cells["SurvGuideCnts16"].Value = Utils.ToMultiline(drow["SurvGuideCnts"]);     // 내용
                        SS16.Rows[SS16.RowCount - 1].Cells["IsrdResnDlyDay16"].Value = drow["IsrdResnDlyDay"];   // 지연일수
                        SS16.Rows[SS16.RowCount - 1].Cells["keystr16"].Value = drow["keystr"];                   // keystr
                        SS16.Rows[SS16.RowCount - 1].Cells["WorkingTag16"].Value = (Utils.ConvertToString(drow["keystr"]) == "" ? "A" : "");

                        ini_111.ValueObject = Utils.ToInt(ini_111.Value) + Utils.ToInt(SS16.Rows[SS16.RowCount - 1].Cells["DayCnt16"].Value);
                        ini_112.ValueObject = Utils.ToInt(ini_112.Value) + Utils.ToInt(SS16.Rows[SS16.RowCount - 1].Cells["IsrdResnDlyDay16"].Value);
                        ini_113.ValueObject = Utils.ToInt(ini_111.Value) - Utils.ToInt(ini_112.Value);
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS16.AllowUserToAddRows = true;
                }
                SS16.AutoResizeRows();
                GridHeightResize(SS16);

                if (SS13.CurrentRow != null)
                {
                    DataGridViewRow dgrow = SS13.Rows[SS13.CurrentRow.Index];
                    if (dgrow != null)
                    {
                        Query_SS13_2(dgrow);
                    }
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

        private void Query_SS13_2(DataGridViewRow prow)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClearSS13_2();

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDB";
                security.methodId = "QuerySS12_2";
                security.companySeq = param.CompanySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock13");
                dt.Columns.Add("id");
                dt.Columns.Add("parent_id");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["id"] = prow.Cells["id13"].Value;
                dr["parent_id"] = prow.Cells["parent_id13"].Value;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }

                _bEvent = false;

                // 손해사정서교부사항
                SS13_2.AllowUserToAddRows = false;
                DataTable SA14 = yds.Tables["DataBlock14"];
                if (SA14 != null && SA14.Rows.Count > 0)
                {
                    for (int i = 0; i < SA14.Rows.Count; i++)
                    {
                        DataRow drow = SA14.Rows[i];
                        SS13_2.Rows.Add();
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["custContRelcd13_2"].Value = drow["custContRelcd"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["custNm13_2"].Value = drow["custNm"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sensInfoAsntYn13_2"].Value = drow["sensInfoAsntYn"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["crinfiqAsntYn13_2"].Value = drow["crinfiqAsntYn"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["consgRfsYn13_2"].Value = drow["consgRfsYn"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["adjrNm13_2"].Value = drow["adjrNm"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["adjrEno13_2"].Value = drow["adjrEno"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sndMcd13_2"].Value = drow["sndMcd"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["sndDt13_2"].Value = Utils.ConvertToDateTime(drow["sndDt"]);
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["id13_2"].Value = drow["id"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["parent_id13_2"].Value = drow["parent_id"];
                        SS13_2.Rows[SS13_2.RowCount - 1].Cells["WorkingTag13_2"].Value = (Utils.ToInt(drow["id"]) == 0 ? "A" : "");
                    }
                }
                if (!ReadOnlyMode)
                {
                    SS13_2.AllowUserToAddRows = true;
                }
                SS13_2.AutoResizeRows();
                GridHeightResize(SS13_2);

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

        private void Grid62_InvsgDcdChanged(string defaultFee, string TransFee, string OtherFee)
        {
            if (!_bEvent) return;
            for (int ii = 0; ii < SS8.Rows.Count; ii++)
            {
                if (SS8.Rows[ii].Cells["cstTpcd8"].Value + "" == "01")
                {
                    SS8.Rows[ii].Cells["cstAmt8"].Value = Utils.ToDouble(defaultFee) + Utils.ToDouble(TransFee) + Utils.ToDouble(OtherFee);
                    return;
                }
            }
            int idx = SS8.Rows.Add();
            SS8.Rows[idx].Cells["cstTpcd8"].Value = "01";
            SS8.Rows[idx].Cells["cstAmt8"].Value = Utils.ToDouble(defaultFee) + Utils.ToDouble(TransFee) + Utils.ToDouble(OtherFee);
            SS8.Rows[idx].Cells["id8"].Value = 0;
            SS8.Rows[idx].Cells["WorkingTag8"].Value = "A";


            //if (Utils.ToDouble(grid52.DefaultCost) == 0) grid52.DefaultCost = defaultFee;
            //if (Utils.ToDouble(grid52.TransCost) == 0) grid52.TransCost = TransFee;

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
                if (e.Control is CustomDataGridViewMaskedTextBoxEditingControl)
                    ((CustomDataGridViewMaskedTextBoxEditingControl)e.Control).ReadOnly = ReadOnlyMode;
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
            drow.Cells["file_name15"].Value = realFileName;
            drow.Cells["file_seq15"].Value = fileSeq;
            string workingTag = WorkingTagCell(drow).Value + "";
            if (workingTag == "")
            {
                WorkingTagCell(drow).Value = "U";
            }
            return true;
        }

        public string fnFileDownload(DataGridViewRow drow)
        {
            string fileName = drow.Cells["file_name15"].Value + "";
            string fileSeq = drow.Cells["file_seq15"].Value + "";

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
            string fileName = drow.Cells["file_name15"].Value + "";
            string fileSeq = drow.Cells["file_seq15"].Value + "";

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

        private void BtnSS15Add_Click(object sender, EventArgs e)
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
                        int rowIndex = SS15.Rows.Add();
                        DataGridViewRow dr = SS15.Rows[rowIndex];
                        fnFileUpload(dr, P_File);
                    }
                    SS15.Refresh();
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
                frmKDBSmplAuth f = new frmKDBSmplAuth(param);
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
            PDFFileShow("1", "안내사항");
        }

        private void BtnPDF1Up_Click(object sender, EventArgs e)
        {
            PDFFileUpload("1", "안내사항");
        }

        private void BtnPDF2_Click(object sender, EventArgs e)
        {
            PDFFileShow("2", "관련규정");
        }

        private void BtnPDF2Up_Click(object sender, EventArgs e)
        {
            PDFFileUpload("2", "관련규정");
        }

        private bool PDFFileShow(string fileTypeSeq, string fileTypeName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDBLink";
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
                security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDBLink";
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

        private void BtnSS16Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS16);
        }

        private void BtnSS15Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS15);
        }

        private void BtnSS13_2Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS13_2);
        }

        private void BtnSS13Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS13);
        }

        private void BtnSS12Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS12);
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

        private void BtnSS5Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS5);
        }

        private void BtnSS4Del_Click(object sender, EventArgs e)
        {
            GridRowRemove(SS4);
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
                                if (dgv == SS15)
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

        static int nPrevLineIndex = -1;
        private void SS13_CurrentCellChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            if (SS13.CurrentRow == null) return;
            if (nPrevLineIndex != SS13.CurrentRow.Index)
            {
                nPrevLineIndex = SS13.CurrentRow.Index;
                DataGridViewRow drow = SS13.Rows[SS13.CurrentRow.Index];
                if (drow != null)
                {
                    Query_SS13_2(drow);
                }
            }
        }

        private void SS16_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 4)
                {
                    try
                    {
                        if (e.ColumnIndex == 0)
                        {
                            for (int i = 0; i < SS16.RowCount - 1; i++)
                            {
                                string frdt = (i == 0 ? acptDt : Utils.DateFormat(SS16.Rows[i - 1].Cells[e.ColumnIndex].Value, "yyyyMMdd"));
                                string todt = Utils.DateFormat(SS16.Rows[i].Cells[e.ColumnIndex].Value, "yyyyMMdd");
                                SS16.Rows[i].Cells[e.ColumnIndex + 1].Value = uFunction.GetDelayDays(param.CompanySeq, frdt, todt);
                            }
                        }

                        int num_01 = 0;
                        int num_02 = 0;
                        for (int i = 0; i < SS16.RowCount - 1; i++)
                        {
                            if (SS16.Rows[i].Cells[1].Value == null) SS16.Rows[i].Cells[1].Value = 0;
                            if (SS16.Rows[i].Cells[4].Value == null) SS16.Rows[i].Cells[4].Value = 0;

                            num_01 = num_01 + Utils.ToInt(SS16.Rows[i].Cells[1].Value);
                            num_02 = num_02 + Utils.ToInt(SS16.Rows[i].Cells[4].Value);
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
                //GridHeightResize(dgv);
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
                dt.Columns.Add("acrcNo");
                dt.Columns.Add("acciUndwSeq");
                dt.Columns.Add("edi_id");
                dt.Columns.Add("parent_id");
                dt.Columns.Add("bis_code");
                DataRow dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["acrcNo"] = grid11.AcdtNo;
                dr["acciUndwSeq"] = grid11.AcdtExamSerl;
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
                    grid62.id = Utils.ToInt(drow["id"]);

                    // 조사내용정보
                    DataTable SA03 = yds.Tables["DataBlock3"];
                    drow = SA03.Rows[0];
                    id3 = Utils.ToInt(drow["id"]);
                    parent_id3 = Utils.ToInt(drow["parent_id"]);

                    // 계약별계산결과내용
                    DataTable SA04 = yds.Tables["DataBlock4"];
                    if (SA04 != null)
                    {
                        for (int ii = 0; ii < SA04.Rows.Count; ii++)
                        {
                            drow = SA04.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS4.Rows[idx].Cells["id4"].Value = drow["id"];
                            SS4.Rows[idx].Cells["parent_id4"].Value = drow["parent_id"];
                            SS4.Rows[idx].Cells["WorkingTag4"].Value = "";
                        }
                    }
                    SS4.Refresh();

                    // 병력경위사항
                    DataTable SA05 = yds.Tables["DataBlock5"];
                    if (SA05 != null)
                    {
                        for (int ii = 0; ii < SA05.Rows.Count; ii++)
                        {
                            drow = SA05.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS5.Rows[idx].Cells["id5"].Value = drow["id"];
                            SS5.Rows[idx].Cells["parent_id5"].Value = drow["parent_id"];
                            SS5.Rows[idx].Cells["WorkingTag5"].Value = "";
                        }
                    }
                    SS5.Refresh();

                    // 타사가입사항
                    DataTable SA06 = yds.Tables["DataBlock6"];
                    if (SA06 != null)
                    {
                        for (int ii = 0; ii < SA06.Rows.Count; ii++)
                        {
                            drow = SA06.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS6.Rows[idx].Cells["id6"].Value = drow["id"];
                            SS6.Rows[idx].Cells["parent_id6"].Value = drow["parent_id"];
                            SS6.Rows[idx].Cells["WorkingTag6"].Value = "";
                        }
                    }
                    SS6.Refresh();

                    // 조사면담내용
                    DataTable SA07 = yds.Tables["DataBlock7"];
                    if (SA07 != null)
                    {
                        for (int ii = 0; ii < SA07.Rows.Count; ii++)
                        {
                            drow = SA07.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS7.Rows[idx].Cells["id7"].Value = drow["id"];
                            SS7.Rows[idx].Cells["parent_id7"].Value = drow["parent_id"];
                            SS7.Rows[idx].Cells["WorkingTag7"].Value = "";
                        }
                    }
                    SS7.Refresh();

                    // 조사수수료정보
                    DataTable SA08 = yds.Tables["DataBlock8"];
                    if (SA08 != null)
                    {
                        for (int ii = 0; ii < SA08.Rows.Count; ii++)
                        {
                            drow = SA08.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS8.Rows[idx].Cells["id8"].Value = drow["id"];
                            SS8.Rows[idx].Cells["parent_id8"].Value = drow["parent_id"];
                            SS8.Rows[idx].Cells["WorkingTag8"].Value = "";
                        }
                    }
                    SS8.Refresh();

                    // 차트열람정보
                    DataTable SA09 = yds.Tables["DataBlock9"];
                    if (SA09 != null)
                    {
                        for (int ii = 0; ii < SA09.Rows.Count; ii++)
                        {
                            drow = SA09.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS9.Rows[idx].Cells["id9"].Value = drow["id"];
                            SS9.Rows[idx].Cells["parent_id9"].Value = drow["parent_id"];
                            SS9.Rows[idx].Cells["WorkingTag9"].Value = "";
                        }
                    }
                    SS9.Refresh();

                    // 모랄해저드정보
                    DataTable SA10 = yds.Tables["DataBlock10"];
                    if (SA10 != null)
                    {
                        for (int ii = 0; ii < SA10.Rows.Count; ii++)
                        {
                            drow = SA10.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS10.Rows[idx].Cells["id10"].Value = drow["id"];
                            SS10.Rows[idx].Cells["parent_id10"].Value = drow["parent_id"];
                            SS10.Rows[idx].Cells["WorkingTag10"].Value = "";
                        }
                    }
                    SS10.Refresh();

                    // 지급불가안내
                    DataTable SA11 = yds.Tables["DataBlock11"];
                    drow = SA11.Rows[0];
                    grid72.id = Utils.ToInt(drow["id"]);
                    grid72.parent_id = Utils.ToInt(drow["parent_id"]);

                    // 지급불가 - 계약번호
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

                    // 손해사정서교부사항
                    DataTable SA13 = yds.Tables["DataBlock13"];
                    if (SA13 != null)
                    {
                        for (int ii = 0; ii < SA13.Rows.Count; ii++)
                        {
                            drow = SA13.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS13.Rows[idx].Cells["id13"].Value = drow["id"];
                            SS13.Rows[idx].Cells["parent_id13"].Value = drow["parent_id"];
                            SS13.Rows[idx].Cells["WorkingTag13"].Value = "";
                        }
                    }
                    SS13.Refresh();

                    // 보고서첨부파일
                    DataTable SA15 = yds.Tables["DataBlock15"];
                    if (SA15 != null)
                    {
                        for (int ii = 0; ii < SA15.Rows.Count; ii++)
                        {
                            drow = SA15.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS15.Rows[idx].Cells["id15"].Value = drow["id"];
                            SS15.Rows[idx].Cells["parent_id15"].Value = drow["parent_id"];
                            SS15.Rows[idx].Cells["WorkingTag15"].Value = "";
                        }
                    }
                    SS15.Refresh();

                    // 처리과정표
                    DataTable SA16 = yds.Tables["DataBlock16"];
                    if (SA16 != null)
                    {
                        for (int ii = 0; ii < SA16.Rows.Count; ii++)
                        {
                            drow = SA16.Rows[ii];
                            int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                            SS16.Rows[idx].Cells["keystr16"].Value = drow["keystr"];
                            SS16.Rows[idx].Cells["WorkingTag16"].Value = "";
                        }
                    }
                    SS16.Refresh();

                    if (SS13.CurrentRow != null)
                    {
                        DataGridViewRow prow = SS13.Rows[SS13.CurrentRow.Index];
                        if (prow != null)
                        {
                            yds = null;
                            if (SaveData(GetSaveSS13_2(prow), out yds) != true) return;
                            // 손해사정서교부사항 - 대상자
                            DataTable SA14 = yds.Tables["DataBlock14"];
                            if (SA14 != null)
                            {
                                for (int ii = 0; ii < SA14.Rows.Count; ii++)
                                {
                                    drow = SA14.Rows[ii];
                                    int idx = Utils.ToInt(drow["IDX_NO"]) - 1;
                                    SS13_2.Rows[idx].Cells["id13_2"].Value = drow["id"];
                                    SS13_2.Rows[idx].Cells["parent_id13_2"].Value = drow["parent_id"];
                                    SS13_2.Rows[idx].Cells["WorkingTag13_2"].Value = "";
                                }
                            }
                            SS13_2.Refresh();
                        }
                    }

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
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDB";
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
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDB";
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
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptKDB.xsd";
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(s_CAA_XSD);

            // 조사보고서기본정보
            DataTable dt = ds.Tables["DataBlock1"];
            dt.Columns.Add("WorkingTag");
            DataRow dr = dt.Rows.Add();
            dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
            dr["acrcNo"] = grid11.AcdtNo;
            dr["acciUndwSeq"] = grid11.AcdtExamSerl;
            dr["SurvReqDt"] = grid11.SurvReqDt;
            dr["SurvCompDt"] = grid11.EndDate;
            dr["edi_id"] = grid11.edi_id;
            dr["parent_id"] = grid11.parent_id;
            dr["bis_code"] = grid11.bis_code;
            dr["WorkingTag"] = "A";

            // 조사자정보
            dt = ds.Tables["DataBlock2"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["invgrEno"] = grid62.invgrEno;
            dr["invsgDcd"] = grid62.invsgDcd;
            dr["invsgDifcCd"] = grid62.invsgDifcCd;
            dr["accipInsJobCd"] = grid62.accipInsJobCd;
            dr["invsgDcd2"] = grid62.invsgDcd2;
            dr["id"] = grid62.id;
            dr["WorkingTag"] = "A";

            // 조사내용정보
            dt = ds.Tables["DataBlock3"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["rqstCtnt"] = rtfrqstCtnt.rtbDoc.Text;          // 의뢰내용
            dr["vrfcCtnt"] = rtfvrfcCtnt.rtbDoc.Text;          // 확인내용
            dr["rltRuleCtnt"] = rtfrltRuleCtnt.rtbDoc.Text;    // 관련규정
            dr["invsgOpiCtnt"] = rtfinvsgOpiCtnt.rtbDoc.Text;  // 조사의견
            dr["rfMtrCtnt"] = rtfrfMtrCtnt.rtbDoc.Text;        // 참고사항
            dr["id"] = id3;
            dr["parent_id"] = parent_id3;
            dr["WorkingTag"] = "A";

            // 계약별계산결과내역
            GetSheetData(SS4, ds.Tables["DataBlock4"], "A;U");

            // 병력경위사항
            GetSheetData(SS5, ds.Tables["DataBlock5"], "A;U");

            // 타사가입사항
            GetSheetData(SS6, ds.Tables["DataBlock6"], "A;U");

            // 조사면담내용
            GetSheetData(SS7, ds.Tables["DataBlock7"], "A;U");

            // 조사수수료정보
            GetSheetData(SS8, ds.Tables["DataBlock8"], "A;U");

            // 차트열람정보
            GetSheetData(SS9, ds.Tables["DataBlock9"], "A;U");
            
            // 모럴해저드정보
            GetSheetData(SS10, ds.Tables["DataBlock10"], "A;U");

            // 지급불가안내
            dt = ds.Tables["DataBlock11"];
            dt.Columns.Add("WorkingTag");
            dr = dt.Rows.Add();
            dr["clmRscd"] = grid72.clmRscd;
            dr["payNotcCtnt"] = grid72.payNotcCtnt;
            dr["rltRuleCtnt"] = grid72.rltRuleCtnt;
            dr["id"] = grid72.id;
            dr["parent_id"] = grid72.parent_id;
            dr["WorkingTag"] = "A";

            // 지급불가 - 계약사항
            GetSheetData(SS12, ds.Tables["DataBlock12"], "A;U");

            // 손해사정서교부사항
            GetSheetData(SS13, ds.Tables["DataBlock13"], "A;U");

            // 보고서첨부파일
            GetSheetData(SS15, ds.Tables["DataBlock15"], "A;U");

            // 1. 처리과정 사고처리과정표
            GetSheetData(SS16, ds.Tables["DataBlock16"], "A;U;");   //'A' + 'U' + '' = 전체

            return ds;
        }

        private DataSet GetSaveSS13_2(DataGridViewRow prow)
        {
            string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptKDB.xsd";
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(s_CAA_XSD);

            // 조사보고서기본정보
            DataTable dt = ds.Tables["DataBlock1"];
            dt.Columns.Add("WorkingTag");
            DataRow dr = dt.Rows.Add();
            dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
            dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
            dr["acrcNo"] = grid11.AcdtNo;
            dr["acciUndwSeq"] = grid11.AcdtExamSerl;
            dr["SurvReqDt"] = grid11.SurvReqDt;
            dr["SurvCompDt"] = grid11.EndDate;
            dr["edi_id"] = grid11.edi_id;
            dr["parent_id"] = grid11.parent_id;
            dr["bis_code"] = grid11.bis_code;
            dr["WorkingTag"] = "A";

            if (prow.Cells["id13"].Value == null) return ds;  //손해사정서 교부사항이 없으면 대상자를 저장할 수 없음
            dt = ds.Tables["DataBlock13"];
            dr = dt.Rows.Add();
            dr["id"] = prow.Cells["id13"].Value;
            dr["parent_id"] = prow.Cells["parent_id13"].Value;

            // 손해사정서교부사항 - 대상자
            GetSheetData(SS13_2, ds.Tables["DataBlock14"], "A;U");

            return ds;
        }

        private void DeleteRows(DataGridView dgv)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string blockname = "";
                if (dgv == SS4) blockname = "DataBlock4";
                if (dgv == SS5) blockname = "DataBlock5";
                if (dgv == SS6) blockname = "DataBlock6";
                if (dgv == SS7) blockname = "DataBlock7";
                if (dgv == SS8) blockname = "DataBlock8";
                if (dgv == SS9) blockname = "DataBlock9";
                if (dgv == SS10) blockname = "DataBlock10";
                if (dgv == SS12) blockname = "DataBlock12";
                if (dgv == SS13) blockname = "DataBlock13";
                if (dgv == SS13_2) blockname = "DataBlock14";
                if (dgv == SS15) blockname = "DataBlock15";
                if (dgv == SS16) blockname = "DataBlock16";

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
                        if (dgv == SS15)
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
            security.serviceId = "Metro.Package.AdjSL.BisSurvRptKDB";
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
                string s_CAA_XSD = System.Windows.Forms.Application.StartupPath + @"\BisSurvRptKDB.xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(s_CAA_XSD);

                DataTable dt = ds.Tables["DataBlock1"];
                DataRow dr = dt.Rows.Add();
                dr["AcptMgmtSeq"] = param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = param.ReSurvAsgnNo;
                dr["acrcNo"] = grid11.AcdtNo;
                dr["acciUndwSeq"] = grid11.AcdtExamSerl;
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
                        else if (col is DataGridViewCustomMaskedTextBoxColumn)
                        {
                            dr[col.DataPropertyName] = Utils.ConvertToString(val).Replace(":", "");
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
