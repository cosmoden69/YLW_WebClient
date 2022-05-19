using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    class Program
    {
        public static string G_UP_Path = @"C:\haesungHASP\";
        public static string G_WD_Path = @"C:\haesungHASP\L_Image\";
        public static string G_Create_ = @"C:\haesungHASP\CrtWord\";
        public static int GUserSize = 70;

        public static frmMain MainForm = null;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledException);

                MainForm = new frmMain();
                Application.Run(MainForm);
                //Application.Run(new Form2());   //테스트용
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }
}
