using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Microsoft.Win32;

namespace YLW_WebClient
{
    public partial class frmMain : Form
    {
        string INIfileName = Application.StartupPath + "\\" + "AutoLauncher.INI";
        string AUTOUPDATER_URL = "";
        string UPDATELISTFiLE = "updatinglist.xml";
        string LastUpdateFileName = "";
        string APPLICATION_NAME = "YLWService.Launcher";
        string APPLICATION_PATH = Application.StartupPath + "\\" + "YLWService.Launcher.exe";
        //private RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        private RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", true);
        int UpdateInterval = 60000;

        WebServiceHost server = null;

        public frmMain()
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            SetAutoStartForce(APPLICATION_NAME, APPLICATION_PATH);  //자동실행 강제설정

            this.chkAutoRun.Checked = IsAutoStartApplication(APPLICATION_NAME);
            this.chkAutoRun.CheckedChanged += ChkAutoRun_CheckedChanged;

            this.Load += FrmMain_Load;
            this.Shown += FrmMain_Shown;
            this.FormClosing += FrmMain_FormClosing;
            this.FormClosed += FrmMain_FormClosed;
            this.notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
        }

        public static bool Is64BitMode() { return System.Runtime.InteropServices.Marshal.SizeOf(typeof(IntPtr)) == 8; }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            StartService();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!this.chkAutoRun.Checked) this.chkAutoRun.Checked = true;
            LastUpdateFileName = MetroSoft.HIS.cFile.GetIniValue("LAUNCHER", "LastUpdateFileName", INIfileName);
            AUTOUPDATER_URL = MetroSoft.HIS.cFile.GetIniValue("LAUNCHER", "AUTOUPDATER_URL", INIfileName);
            string tmp = MetroSoft.HIS.cFile.GetIniValue("LAUNCHER", "UpdateInterval", INIfileName);
            if (tmp != "") UpdateInterval = YLWService.Utils.ToInt(tmp);

            lblLastUpdate.Text = LastUpdateFileName;

            timer1.Interval = UpdateInterval;
            timer1.Enabled = true;
            timer1.Tick += Timer1_Tick;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Visible = false;
            CloseService();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!IsInternetConnected())
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "인터넷에 연결되지 않았습니다");
                return;
            }
            if (UpdateCheck() == false)
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "새 업데이트가 없습니다");
                return;
            }
            if (IsFormShowing() == true)
            {
                string msg = "새 업데이트가 있습니다\r\n업데이트를 실행하기 위해서 프로그램을 종료하시겠습니까?";
                if (MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    msg, "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            }
            if (!File.Exists(APPLICATION_PATH))
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "파일이 없습니다\r\n" + APPLICATION_PATH);
                return;
            }
            UpdateMe();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (UpdateCheck() == false) return;
            if (IsFormShowing() == true) return;
            UpdateMe();
        }

        public bool UpdateCheck()
        {
            try
            {
                if (!IsInternetConnected()) return false;
                var version = Path.GetFileNameWithoutExtension(LastUpdateFileName);
                //var request = HttpWebRequest.CreateHttp(AUTOUPDATER_URL);
                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                //{
                //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                //    {
                //        string html = reader.ReadToEnd();
                //        Regex regex = new Regex("<A HREF=\".*?.ZIP\">(?<name>.*?)</A>", RegexOptions.IgnoreCase);
                //        MatchCollection matches = regex.Matches(html);
                //        if (matches.Count > 0)
                //        {
                //            foreach (Match match in matches)
                //            {
                //                if (match.Groups["name"].Value.CompareTo(version) > 0) return true;
                //            }
                //        }
                //    }
                //}
                if (AUTOUPDATER_URL.EndsWith("/") == false) AUTOUPDATER_URL += "/";
                string listText = GetUpdatingText(AUTOUPDATER_URL + UPDATELISTFiLE);

                UpdateListDataSet updateList = new UpdateListDataSet();
                StringReader reader = new StringReader(listText);
                updateList.ReadXml(reader);
                reader.Close();

                List<RemoteFile> remoteFiles = new List<RemoteFile>();

                foreach (UpdateListDataSet.FileRow file in updateList.File)
                {
                    RemoteFile remoteFile = new RemoteFile();
                    remoteFile.Uri = file.Path.Replace('\\', '/');
                    remoteFile.LocalPath = file.Path;
                    remoteFile.ContentLength = file.Length;
                    remoteFile.LastModified = file.LastWriteTime;

                    if (IsUpdatable(remoteFile)) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string GetUpdatingText(string listFileUri)
        {
            WebClient webClient = new WebClient();

            Stream stream = webClient.OpenRead(listFileUri);
            StreamReader reader = new StreamReader(stream);

            string text = reader.ReadToEnd();

            stream.Close();

            return text;
        }

        private bool IsUpdatable(RemoteFile remoteFile)
        {
            //// 로컬에 없는 파일이라면 다운로드한다.
            //if (File.Exists(remoteFile.LocalPath) == false)
            //    return true;

            //// 파일의 수정날짜가 다르다면 다운로드한다.
            //if (File.GetLastWriteTime(remoteFile.LocalPath) != remoteFile.LastModified)
            //    return true;

            //서버 파일의 이름이 최종 업데이트된 파일 이후면 true
            string filename = Path.GetFileName(remoteFile.LocalPath);
            if (filename.CompareTo(LastUpdateFileName) > 0) return true;

            // 로컬에 존재하고, 파일의 수정날짜가 같다면 다운로드하지 않는다.
            return false;
        }

        public bool IsFormShowing()
        {
            if ((frmReportView.Current != null && frmReportView.Current.Visible)
                || (frmReportView2.Current != null && frmReportView2.Current.Visible)
                || (CAA.frmInputer.Current != null && CAA.frmInputer.Current.Visible)) return true;
            return false;
        }

        public void UpdateMe()
        {
            try
            {
                if (!File.Exists(APPLICATION_PATH)) return;
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(APPLICATION_PATH, "silent");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAutoRun.Checked)
            {
                SetAutoStartApplication(APPLICATION_NAME, APPLICATION_PATH);
            }
            else
            {
                ResetAutoStartApplication(APPLICATION_NAME);
            }
        }

        private void StartService()
        {
            try
            {
                // 서버 인스턴스 생성
                server = new WebServiceHost(typeof(Service));
                server.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();
                // EndPoint 설정
                WebHttpBinding binding = new WebHttpBinding();
                binding.MaxReceivedMessageSize = 2147483647;
                binding.MaxBufferSize = 2147483647;
                binding.MaxBufferPoolSize = 2147483647;

                server.AddServiceEndpoint(typeof(IService), binding, "http://localhost:8080");
                foreach (ServiceEndpoint EP in server.Description.Endpoints)
                    EP.Behaviors.Add(new BehaviorAttribute());
                // 서버 기동
                server.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseService()
        {
            if (server != null)
            {
                server.Close();
                server = null;
            }
        }

        private void tsMenuClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료하면 보고서 조회가 안됩니다.\r\n종료하시겠습니까?", "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            notifyIcon1.Visible = false;
            Application.ExitThread();
            Application.Exit();
        }

        private void tsMenuOpen_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Activate();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            tsMenuOpen_Click(tsMenuOpen, new EventArgs());
        }

        public bool IsInternetConnected()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

            try
            {
                // Check NCSI test link
                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    return false;
                }

                // Check NCSI DNS IP
                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        //////////////////////////////////////////////////////////////////////////////// Function
        #region 자동 실행 애플리케이션 설정하기 - SetAutoStartApplication(applicationName, applicationFilePath)
        /// <summary>
        /// 자동 실행 애플리케이션 설정하기
        /// </summary>
        /// <param name="applicationName">애플리케이션명</param>
        /// <param name="applicationFilePath">애플리케이션 파일 경로</param>

        public void SetAutoStartForce(string applicationName, string applicationFilePath)
        {
            RegistryKey rkey;
            try
            {
                rkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", true);
                rkey.DeleteValue(applicationName, false);
                rkey.SetValue(applicationName, applicationFilePath);
            }
            catch { }
        }

        public void SetAutoStartApplication(string applicationName, string applicationFilePath)
        {
            if (registryKey.GetValue(applicationName) == null)
            {
                registryKey.SetValue(applicationName, applicationFilePath);
            }
        }

        #endregion

        #region 자동 실행 애플리케이션 설정하기 - SetAutoStartApplication(applicationName)
        /// <summary>
        /// 자동 실행 애플리케이션 설정하기
        /// </summary>
        /// <param name="applicationName">애플리케이션명</param>
        public void SetAutoStartApplication(string applicationName)
        {
            SetAutoStartApplication(applicationName, Application.ExecutablePath);
        }
        #endregion

        #region 자동 실행 애플리케이션 설정 해제하기 - ResetAutoStartApplication(applicationName)
        /// <summary>
        /// 자동 실행 애플리케이션 설정 해제하기
        /// </summary>
        /// <param name="applicationName">애플리케이션명</param>
        public void ResetAutoStartApplication(string applicationName)
        {
            if (registryKey.GetValue(applicationName) != null)
            {
                registryKey.DeleteValue(applicationName, false);
            }
        }
        #endregion

        #region 자동 실행 애플리케이션 여부 구하기 - IsAutoStartApplication(applicationName)
        /// <summary>
        /// 자동 실행 애플리케이션 여부 구하기
        /// </summary>
        /// <param name="applicationName">애플리케이션명</param>
        /// <returns>자동 실행 애플리케이션 여부</returns>
        public bool IsAutoStartApplication(string applicationName)
        {
            return registryKey.GetValue(applicationName) != null;
        }
        #endregion
    }
}
