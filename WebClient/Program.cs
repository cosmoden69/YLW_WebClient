using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
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
                MainForm = new frmMain();
                Application.Run(MainForm);
                //Application.Run(new Form2());   //테스트용
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
