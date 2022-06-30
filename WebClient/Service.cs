using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DevExpress.XtraRichEdit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YLWService;

namespace YLW_WebClient
{
    // 구현 함수
    public class Service : IService
    {
        private static FrmRptHistoryList RptHistList = null;

        // /OpenDocx/{value}의 형식으로 접속되면 호출되어 처리한다.
        public string OpenDocx(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 생성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                ReportData response = YLWService.MTRServiceModule.CallMTRGetReportPost(value);
                YLWService.Response rsp = response.Response;
                if (rsp.Result != 1)
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, rsp.Message);
                    return rsp.Message;
                }
                string rptname = response.ReportName;
                string rpttext = response.ReportText;
                byte[] rptbyte = Convert.FromBase64String(rpttext);

                string strKey = Utils.Join(1, rptname);
                MemoryStream stream = new MemoryStream(rptbyte);
                if (para.ReportName == "RptAdjSLRptEDISSL")
                {
                    frmReportViewSSL.ShowPreview(para);
                    frmReportViewSSL.Current.LoadDocument(rptname, strKey, stream);
                    frm = frmReportViewSSL.Current;
                }
                else if (para.ReportName == "RptAdjSLRptViewInvoiceOut1" || para.ReportName == "RptAdjSLRptViewInvoiceOut2")
                {
                    frmReportViewInvoice.ShowPreview(para);
                    frmReportViewInvoice.Current.LoadDocument(rptname, strKey, stream);
                    frm = frmReportViewInvoice.Current;
                }
                else
                {
                    frmReportView2.ShowPreview(para);
                    frmReportView2.Current.LoadDocument(rptname, strKey, stream);
                    frm = frmReportView2.Current;
                }

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenInputer(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                if (para.BizNo == "1168103752")  //메리츠
                {
                    if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersMeritz";
                    if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersMeritzMid";
                    CAA.frmMeritzInputer.ShowPreview();
                    CAA.frmMeritzInputer.Current.LoadDocument(para);
                    frm = CAA.frmMeritzInputer.Current;
                }
                else if (para.BizNo == "1028132035")  //현대해상
                {
                    if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersHyundai";
                    if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersHyundaiMid";
                    CAA.frmHyundaiInputer.ShowPreview();
                    CAA.frmHyundaiInputer.Current.LoadDocument(para);
                    frm = CAA.frmHyundaiInputer.Current;
                }
                else if (para.BizNo == "1028107837")  //흥국화재
                {
                    if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersHeungkuk";
                    if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersHeungkukMid";
                    CAA.frmHeungkukInputer.ShowPreview();
                    CAA.frmHeungkukInputer.Current.LoadDocument(para);
                    frm = CAA.frmHeungkukInputer.Current;
                }
                else if (para.BizNo == "2028118152" || para.BizNo == "2028118158")  //DB생명
                {
                    if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersDBLife";
                    if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersDBLifeMid";
                    CAA.frmDBLifeInputer.ShowPreview();
                    CAA.frmDBLifeInputer.Current.LoadDocument(para);
                    frm = CAA.frmDBLifeInputer.Current;
                }
                else if (para.BizNo == "1048185673" || para.BizNo == "2018145593")  //DB손해
                {
                    if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersDBLoss";
                    if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersDBLossMid";
                    CAA.frmDBLossInputer.ShowPreview();
                    CAA.frmDBLossInputer.Current.LoadDocument(para);
                    frm = CAA.frmDBLossInputer.Current;
                }
                else if (para.BizNo == "2208852643")  //MG손해
                {
                    string strSql = "";
                    strSql += " SELECT SA01.InsurTyp ";
                    strSql += " FROM   _TAdjSLAcptList AS SA01 WITH(NOLOCK) ";
                    strSql += " WHERE  SA01.CompanySeq = '" + Utils.ConvertToString(para.CompanySeq) + "' ";
                    strSql += " AND    SA01.AcptMgmtSeq = '" + para.AcptMgmtSeq + "' ";
                    strSql += " FOR JSON PATH ";
                    DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(para.CompanySeq, strSql);
                    string insurTyp = "";
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        insurTyp = ds.Tables[0].Rows[0]["insurTyp"] + "";  //수임종류
                    }
                    if (insurTyp == "300193003")  //단순
                    {
                        if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersMGLossSmpl";
                        if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersMGLossSmplMid";
                        CAA.frmMGLossSmplInputer.ShowPreview();
                        CAA.frmMGLossSmplInputer.Current.LoadDocument(para);
                        frm = CAA.frmMGLossSmplInputer.Current;
                    }
                    else    //일반
                    {
                        if (para.ReportName == "RptAdjSLRptSurvRptPers") para.ReportName = "RptAdjSLRptSurvRptPersMGLoss";
                        if (para.ReportName == "RptAdjSLRptSurvRptPersMid") para.ReportName = "RptAdjSLRptSurvRptPersMGLossMid";
                        CAA.frmMGLossInputer.ShowPreview();
                        CAA.frmMGLossInputer.Current.LoadDocument(para);
                        frm = CAA.frmMGLossInputer.Current;
                    }
                }
                else
                {
                    CAA.frmInputer.ShowPreview();
                    CAA.frmInputer.Current.LoadDocument(para);
                    frm = CAA.frmInputer.Current;
                }

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenLinaInputer(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                CAA.frmLinaInputer.ShowPreview();
                CAA.frmLinaInputer.Current.LoadDocument(para);
                frm = CAA.frmLinaInputer.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenKDBInputer(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                CAA.frmKDBInputer.ShowPreview();
                CAA.frmKDBInputer.Current.LoadDocument(para);
                frm = CAA.frmKDBInputer.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenAcdtPictGoods(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 화면을 새로 조회하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                frmAcdtPictGoods.ShowPreview(para);
                frmAcdtPictGoods.Current.LoadDocument();
                frm = frmAcdtPictGoods.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenAttachFileView(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 화면을 새로 조회하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                AttachFileParam para = JsonConvert.DeserializeObject<AttachFileParam>(value, settings);

                CAA.frmAttachFileView.ShowPreview(para);
                CAA.frmAttachFileView.Current.LoadDocument();
                frm = CAA.frmAttachFileView.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenHRFileView(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 화면을 새로 조회하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                AttachFileParam para = JsonConvert.DeserializeObject<AttachFileParam>(value, settings);

                CAA.frmHRFileDown.ShowPreview(para);
                CAA.frmHRFileDown.Current.LoadDocument();
                frm = CAA.frmHRFileDown.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenSaveReport(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 조회하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                ReportData response = YLWService.MTRServiceModule.CallMTRGetSaveReportPost(value);
                YLWService.Response rsp = response.Response;
                if (rsp.Result != 1)
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, rsp.Message);
                    return rsp.Message;
                }
                string rptname = response.ReportName;
                string rpttext = response.ReportText;
                byte[] rptbyte = Convert.FromBase64String(rpttext);

                string strKey = Utils.Join(1, rptname);
                MemoryStream stream = new MemoryStream(rptbyte);
                frmReportView2.ShowPreview(para);
                frmReportView2.Current.LoadDocument(rptname, strKey, stream);
                frm = frmReportView2.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenPostReportData(ReportData response)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 조회하세요";
                UpdateCheck(addmsg);

                YLWService.Response rsp = response.Response;
                if (rsp.Result != 1)
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, rsp.Message);
                    return rsp.Message;
                }
                string rptname = response.ReportName;
                string rpttext = response.ReportText;
                byte[] rptbyte = Convert.FromBase64String(rpttext);

                string strKey = Utils.Join(1, rptname);
                ucDocxControl cntl = new ucDocxControl();
                MemoryStream stream = new MemoryStream(rptbyte);
                cntl.SetReport(stream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
                frmReportView.ShowPreview(cntl, rptname, strKey);
                frm = frmReportView.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenAcptUploadSSL(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                AttachFileParam para = JsonConvert.DeserializeObject<AttachFileParam>(value, settings);

                CAA.frmAcptUploadSSL.ShowPreview(para);
                frm = CAA.frmAcptUploadSSL.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenSendSSL(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                AttachFileParam para = JsonConvert.DeserializeObject<AttachFileParam>(value, settings);

                CAA.frmSendSSL.ShowPreview(para);
                frm = CAA.frmSendSSL.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenSendSSLImg(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                AttachFileParam para = JsonConvert.DeserializeObject<AttachFileParam>(value, settings);

                CAA.frmSendSSLImg.ShowPreview(para);
                frm = CAA.frmSendSSLImg.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        public string OpenRptHistList(string value)
        {
            Form frm = null;

            try
            {
                frmWait.SplashShow();
                Cursor.Current = Cursors.WaitCursor;

                string addmsg = "\r\n\r\n프로그램이 업데이트되면 보고서를 다시 작성하세요";
                UpdateCheck(addmsg);

                JsonSerializerSettings settings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                ReportParam para = JsonConvert.DeserializeObject<ReportParam>(value, settings);

                RptHistList = new FrmRptHistoryList();

                FrmRptHistoryList.ShowPreview(RptHistList);
                FrmRptHistoryList.Current.LoadDocument(para);
                RptHistList = FrmRptHistoryList.Current;

                return "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                frmWait.SplashClose(null);
                User32Helper.CloseWindow("localhost:8080/", 500, frm);
            }
        }

        private void UpdateCheck(string addmsg)
        {
            try
            {
                if (Program.MainForm.UpdateCheck() == false) return;
                string msg = "새 업데이트가 있습니다\r\n업데이트를 실행하기 위해서 프로그램을 종료하시겠습니까?";
                msg = msg + addmsg;
                if (MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    msg, "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                Program.MainForm.UpdateMe();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string Response()
        {
            string response = "";
            response += "<html>";
            response += "  <head>";
            response += "    <title>response</title>";
            response += "  </head>";
            response += "  <body>";
            response += "    <script language=javascript>window.close();</script>";
            response += "  </body>";
            response += "</html>";
            return response;
        }
    }

    public class User32Helper
    {
        internal class DesktopWindow
        {
            public IntPtr Handle { get; set; }
            public string Title { get; set; }
            public bool IsVisible { get; set; }
        }

        internal class CloseWindowParam
        {
            public string Title { get; set; }
            public int Timeout { get; set; }
            public IntPtr ActiveFormHandle { get; set; }
            public CloseWindowParam(string title, int timeout, IntPtr hdl)
            {
                this.Title = title;
                this.Timeout = timeout;
                this.ActiveFormHandle = hdl;
            }
        }

        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private static List<DesktopWindow> GetDesktopWindows()
        {
            var collection = new List<DesktopWindow>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var result = new StringBuilder(255);
                GetWindowText(hWnd, result, result.Capacity + 1);
                string title = result.ToString();

                var isVisible = !string.IsNullOrEmpty(title) && IsWindowVisible(hWnd);

                collection.Add(new DesktopWindow { Handle = hWnd, Title = title, IsVisible = isVisible });

                return true;
            };

            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return collection;
        }

        private static void CloseMe(object obj)
        {
            try
            {
                CloseWindowParam para = (CloseWindowParam)obj;
                Thread.Sleep(para.Timeout);
                List<DesktopWindow> lst = User32Helper.GetDesktopWindows();
                foreach (DesktopWindow w in lst)
                {
                    if (w.IsVisible && w.Title.ToLower().IndexOf(para.Title) >= 0)
                    {
                        SendMessage(w.Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                if (para.ActiveFormHandle != IntPtr.Zero)
                {
                    Utils.ActivateApp("WebClient");
                    Utils.BringToFront(para.ActiveFormHandle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CloseWindow(string title, int timeout)
        {
            CloseWindow(title, timeout, null);
        }

        public static void CloseWindow(string title, int timeout, Form frm)
        {
            try
            {
                IntPtr hdl = (frm == null ? IntPtr.Zero : frm.Handle);
                Thread thread = new Thread(new ParameterizedThreadStart(CloseMe));
                thread.IsBackground = true;
                thread.Start(new CloseWindowParam(title, timeout, hdl));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}