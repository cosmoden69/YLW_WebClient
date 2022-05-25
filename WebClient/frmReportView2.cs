using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YLWService;

namespace YLW_WebClient
{
    public partial class frmReportView2 : Form
    {
        private static string _fileName = "";
        private static string _fileSeq = "";
        private static ReportParam _param = null;

        private static frmReportView2 current = null;

        public static frmReportView2 Current { get { return current; } }

        public frmReportView2()
        {
            InitializeComponent();

            this.btnRefresh.Click += BtnRefresh_Click;
            this.btnPrint.Click += BtnPrint_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnClose.Click += BtnClose_Click;
            this.FormClosing += FrmReportView2_FormClosing;
        }

        private void FrmReportView2_FormClosing(object sender, FormClosingEventArgs e)
        {
            winWordControl1.CloseControl();
            //this.Hide();
            //e.Cancel = true;
        }

        public static bool ShowPreview(ReportParam p)
        {
            try
            {
                _param = p;
                _fileSeq = "";

                if (!Directory.Exists(Program.G_Create_))
                    Directory.CreateDirectory(Program.G_Create_);

                if (!Directory.Exists(Program.G_WD_Path))
                    Directory.CreateDirectory(Program.G_WD_Path);

                if (current == null || current.IsDisposed)
                {
                    current = new frmReportView2();
                    current.Top = 0;
                    current.Left = Screen.PrimaryScreen.WorkingArea.Width - current.Width;
                    current.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    current.Show();
                }
                else if (!current.Visible)
                {
                    current.Visible = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                current.Close();
                return false;
            }
        }

        public bool LoadDocument(string caption, string key, Stream stream)
        {
            try
            {
                _fileName = caption;
                //string file = Program.G_Create_ + Guid.NewGuid().ToString() + ".docx";
                string file = Program.G_Create_ + "조사보고서.docx";
                winWordControl1.CloseControl();
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        winWordControl1.KillProcess("WINWORD");
                        File.Delete(file);
                    }
                }
                using (FileStream fs = new FileStream(file, FileMode.CreateNew))
                {
                    stream.CopyTo(fs);
                }
                switch (_param.ReportName)
                {
                    case "RptAdjSLRptSurvRptPers":            //인보험(종결)
                    case "RptAdjSLRptSurvRptPersMid":          //인보험(중간)
                    case "RptAdjSLRptSurvRptPersMeritz":       //인보험-메리츠(종결)
                    case "RptAdjSLRptSurvRptPersMeritzMid":     //인보험-메리츠(중간)
                    case "RptAdjSLRptSurvRptPersHyundai":       //인보험-현대해상(종결)
                    case "RptAdjSLRptSurvRptPersHyundaiMid":     //인보험-현대해상(중간)
                    case "RptAdjSLRptSurvRptPersHeungkuk":      //인보험-흥국화재(종결)
                    case "RptAdjSLRptSurvRptPersHeungkukMid":    //인보험-흥국화재(중간)
                    case "RptAdjSLRptSurvRptPersDBLife":        //인보험-DB생명(종결)
                    case "RptAdjSLRptSurvRptPersDBLifeMid":      //인보험-DB생명(중간)
                    case "RptAdjSLRptSurvRptPersDBLoss":         //인보험-DB손해(종결)
                    case "RptAdjSLRptSurvRptPersDBLossMid":      //인보험-DB손해(중간)
                    case "RptAdjSLRptSurvRptPersMGLossSmpl":     //인보험-MG손해단순(종결)
                    case "RptAdjSLRptSurvRptPersMGLossSmplMid":   //인보험-MG손해단순(중간)
                    case "RptAdjSLRptSurvRptPersMGLoss":         //인보험-MG손해일반(종결)
                    case "RptAdjSLRptSurvRptPersMGLossMid":      //인보험-MG손해일반(중간)
                    case "DlgAdjSLSurvRptLina":                //인보험(라이나)
                    case "SaveReportPers":                     //인보험(업로드)
                        btnSave.Visible = true;
                        winWordControl1.LoadDocument(file, true);
                        break;
                    default:
                        btnSave.Visible = true;
                        winWordControl1.LoadDocument(file, false);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (_fileSeq != "")
                {
                    //_param.ReportName = "SaveReportPers";  //인보험은 SaveReportPers
                    _param.ReportName = "SaveReport";  //업로드된 이후에는 업로드파일로 리프레시
                }
                string value = JsonConvert.SerializeObject(_param);
                ReportData response = YLWService.MTRServiceModule.CallMTRGetReportPost(value);
                YLWService.Response rsp = response.Response;
                if (rsp.Result != 1)
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, rsp.Message);
                    return;
                }
                string rptname = response.ReportName;
                string rpttext = response.ReportText;
                byte[] rptbyte = Convert.FromBase64String(rpttext);

                string strKey = Utils.Join(1, rptname);
                MemoryStream stream = new MemoryStream(rptbyte);
                current.LoadDocument(rptname, strKey, stream);
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                winWordControl1.PrintDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string uploadfile = Program.G_Create_ + _fileName;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!winWordControl1.SaveAsFile(uploadfile))
                {
                    MessageBox.Show("보고서 업로드용 파일 생성 실패");
                    return;
                }

                string fileSeq = Utils.ConvertToString(GetFileSeq(_param));
                YLWService.YlwSecurityJson sec1 = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                sec1.companySeq = _param.CompanySeq;
                // File Info
                FileInfo finfo = new FileInfo(uploadfile);
                byte[] rptbyte = (byte[])MetroSoft.HIS.cFile.ReadBinaryFile(uploadfile);
                string fileBase64 = Convert.ToBase64String(rptbyte);
                // File Info
                //fileSeq = YLWService.MTRServiceModule.CallMTRFileuploadGetSeq(sec1, finfo, fileBase64, "47820005", fileSeq);  // 이부분에서 오류남. CallMTRFileuploadGetSeq -> FileuploadGetSeq
                //fileSeq = YLWService.YLWServiceModule.FileuploadGetSeq(sec1, finfo, fileBase64, "47820005", fileSeq);
                fileSeq = YLWService.MTRServiceModule.CallMTRFileuploadGetSeq(sec1, finfo, fileBase64, "47820005", fileSeq);  // WebYlwPlugin_MetroSoft -> 일반 POST API 로 변경
                if (fileSeq == "")
                {
                    MessageBox.Show("보고서 업로드 실패");
                    return;
                }
                _fileSeq = fileSeq;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCS";
                security.methodId = "ReportHistorySave";
                security.companySeq = _param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = _param.UserID;

                DataSet ds = new DataSet();
                DataTable dt10 = ds.Tables.Add("DataBlock10");
                dt10.Columns.Add("AcptMgmtSeq");
                dt10.Columns.Add("ReSurvAsgnNo");
                dt10.Columns.Add("ReportType");
                dt10.Columns.Add("Seq");
                dt10.Columns.Add("FileName");
                dt10.Columns.Add("FileSeq");

                dt10.Clear();
                DataRow dr1 = dt10.Rows.Add();
                dr1["AcptMgmtSeq"] = _param.AcptMgmtSeq;
                dr1["ReSurvAsgnNo"] = _param.ReSurvAsgnNo;
                dr1["ReportType"] = _param.ReportType;
                dr1["FileName"] = _fileName;
                dr1["FileSeq"] = _fileSeq;

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
                MessageBox.Show("보고서 업로드 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (File.Exists(uploadfile)) File.Delete(uploadfile);
                Cursor.Current = Cursors.Default;
            }
        }

        private int GetFileSeq(ReportParam p)
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCclsRprtMngPersCS";
                security.methodId = "ReportQuery";
                security.companySeq = p.CompanySeq;

                DataSet ds = new DataSet();
                DataTable dt10 = ds.Tables.Add("DataBlock10");
                dt10.Columns.Add("AcptMgmtSeq");
                dt10.Columns.Add("ReSurvAsgnNo");
                dt10.Columns.Add("ReportType");

                dt10.Clear();
                DataRow dr1 = dt10.Rows.Add();
                dr1["AcptMgmtSeq"] = p.AcptMgmtSeq;
                dr1["ReSurvAsgnNo"] = p.ReSurvAsgnNo;
                dr1["ReportType"] = p.ReportType;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null && !yds.Tables.Contains("DataBlock10")) return 0;
                if (yds.Tables["DataBlock10"].Rows.Count < 1) return 0;
                return Utils.ToInt(yds.Tables["DataBlock10"].Rows[0]["FileSeq"]);
            }
            catch
            {
                return 0;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
