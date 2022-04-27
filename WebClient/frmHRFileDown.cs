using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class frmHRFileDown : Form
    {
        private static AttachFileParam _param = null;

        private static frmHRFileDown current = null;

        public static frmHRFileDown Current { get { return current; } }

        bool _bEvent = false;

        public frmHRFileDown()
        {
            InitializeComponent();

            this.txtDept.KeyDown += TxtDept_KeyDown;
            this.txtDept.TextChanged += TxtDept_TextChanged;
            this.txtDept.MouseDoubleClick += TxtDept_MouseDoubleClick;
            this.txtEmp.KeyDown += TxtEmp_KeyDown;
            this.txtEmp.TextChanged += TxtEmp_TextChanged;
            this.txtEmp.MouseDoubleClick += TxtEmp_MouseDoubleClick;
            this.chkAllEmp.CheckedChanged += ChkAllEmp_CheckedChanged;
            this.chkAll.CheckedChanged += ChkAll_CheckedChanged;
            this.chkRegIdFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkBnakBookFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkCarPayFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkChildCareFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkGradeCertFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkTranscriptFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkCareerFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chkLicenseFile.CheckedChanged += ChkFile_CheckedChanged;
            this.chklinguisticFile.CheckedChanged += ChkFile_CheckedChanged;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnDownloadAll.Click += BtnDownloadAll_Click;
            this.btnClose.Click += BtnClose_Click;
            this.btnFolderChange.Click += BtnFolderChange_Click;
            this.btnFolderOpen.Click += BtnFolderOpen_Click;
            this.btnSingleView.Click += BtnSingleView_Click;
            this.btnSingleDown.Click += BtnSingleDown_Click;
            this.dgvEmpList.CellClick += DgvEmpList_CellClick;
            this.dgvFileList.CellClick += DgvFileList_CellClick;

            _bEvent = true;
        }

        public static bool ShowPreview(AttachFileParam p)
        {
            try
            {
                _param = p;

                if (current == null || current.IsDisposed)
                {
                    current = new frmHRFileDown();
                    current.Top = 0;
                    current.Left = (Screen.PrimaryScreen.WorkingArea.Width - current.Width) / 2;
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

        public bool LoadDocument()
        {
            try
            {
                _bEvent = false;
                SetInit();
                _bEvent = true;

                chkAll.CheckState = CheckState.Checked;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return false;
            }
        }

        private void SetInit()
        {
            try
            {
                this.panProcess.Visible = false;
                this.panDownFolder.Visible = true;

                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = Path.Combine(userRoot, "Downloads");
                txtDownPath.Text = YLWService.Utils.GetSetting("HaesungHASP", "FileDownload", "Path", downloadFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtEmp_TextChanged(object sender, EventArgs e)
        {
            txtEmp.Tag = "";
        }

        private void TxtDept_TextChanged(object sender, EventArgs e)
        {
            txtDept.Tag = "";
        }

        private void TxtEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DataRow dr = FrmFindEmp.QueryFindOne(txtEmp.Text);
            if (dr != null)
            {
                _bEvent = false;
                txtEmp.Text = dr["EmpName"] + "";
                txtEmp.Tag = dr["EmpSeq"] + "";
                txtDept.Text = dr["DeptName"] + "";
                txtDept.Tag = dr["DeptSeq"] + "";
                _bEvent = true;
                this.btnQuery.PerformClick();
            }
            else
            {
                TxtEmp_MouseDoubleClick(txtEmp, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
            }
        }

        private void TxtDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DataRow dr = FrmFindDept.QueryFindOne(txtDept.Text);
            if (dr != null)
            {
                _bEvent = false;
                txtDept.Text = dr["DeptName"] + "";
                txtDept.Tag = dr["DeptSeq"] + "";
                _bEvent = true;
                this.btnQuery.PerformClick();
            }
            else
            {
                TxtDept_MouseDoubleClick(txtDept, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
            }
        }

        private void TxtEmp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string dept = "0";  //txtDept.Tag + "";
            FrmFindEmp f = new FrmFindEmp(dept, txtEmp.Text);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                _bEvent = false;
                txtEmp.Text = f.ReturnFields.Find(x => x.FieldCode == "EmpName").FieldValue.ToString();
                txtEmp.Tag = f.ReturnFields.Find(x => x.FieldCode == "EmpSeq").FieldValue.ToString();
                txtDept.Text = f.ReturnFields.Find(x => x.FieldCode == "DeptName").FieldValue.ToString();
                txtDept.Tag = f.ReturnFields.Find(x => x.FieldCode == "DeptSeq").FieldValue.ToString();
                _bEvent = true;
                this.btnQuery.PerformClick();
            }
        }

        private void TxtDept_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmFindDept f = new FrmFindDept(txtDept.Text);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                _bEvent = false;
                txtDept.Text = f.ReturnFields.Find(x => x.FieldCode == "DeptName").FieldValue.ToString();
                txtDept.Tag = f.ReturnFields.Find(x => x.FieldCode == "DeptSeq").FieldValue.ToString();
                _bEvent = true;
                this.btnQuery.PerformClick();
            }
        }

        private void ChkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            for (int ii = 0; ii < dgvEmpList.Rows.Count; ii++)
            {
                dgvEmpList.Rows[ii].Cells["SEL"].Value = chkAllEmp.Checked;
            }
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            _bEvent = false;
            this.chkRegIdFile.Checked = chkAll.Checked;
            this.chkBnakBookFile.Checked = chkAll.Checked;
            this.chkCarPayFile.Checked = chkAll.Checked;
            this.chkChildCareFile.Checked = chkAll.Checked;
            this.chkGradeCertFile.Checked = chkAll.Checked;
            this.chkTranscriptFile.Checked = chkAll.Checked;
            this.chkCareerFile.Checked = chkAll.Checked;
            this.chkLicenseFile.Checked = chkAll.Checked;
            this.chklinguisticFile.Checked = chkAll.Checked;
            _bEvent = true;
            ChkFile_CheckedChanged(chkAll, new EventArgs());
        }

        private void ChkFile_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bEvent) return;
            if (dgvEmpList.CurrentCell == null) return;
            DgvEmpList_CellClick(dgvEmpList, new DataGridViewCellEventArgs(1, dgvEmpList.CurrentCell.RowIndex));
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjHR.BisEmpAttachMng";
                security.methodId = "QueryList";
                security.companySeq = _param.CompanySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("DeptSeq");
                dt.Columns.Add("EmpSeq");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["DeptSeq"] = txtDept.Tag;
                dr["EmpSeq"] = txtEmp.Tag;

                dgvEmpList.Rows.Clear();
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null || yds.Tables.Count < 1)
                {
                    //MessageBox.Show("데이타가 없습니다");
                    return;
                }
                DataTable ydt = yds.Tables[0];
                if (ydt != null && ydt.Rows.Count > 0)
                {
                    for (int i = 0; i < ydt.Rows.Count; i++)
                    {
                        DataRow ydr = ydt.Rows[i];
                        int idx = dgvEmpList.Rows.Add();
                        DataGridViewRow drow = dgvEmpList.Rows[idx];
                        drow.Cells["SEL"].Value = Utils.ToBool(ydr["SEL"]);
                        drow.Cells["DeptName"].Value = ydr["DeptName"];
                        drow.Cells["DeptSeq"].Value = ydr["DeptSeq"];
                        drow.Cells["EmpName"].Value = ydr["EmpName"];
                        drow.Cells["EmpSeq"].Value = ydr["EmpSeq"];
                    }
                    dgvEmpList.Refresh();
                    DgvEmpList_CellClick(dgvEmpList, new DataGridViewCellEventArgs(1, 0));
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnFolderOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", txtDownPath.Text);
        }

        private void BtnFolderChange_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = txtDownPath.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtDownPath.Text = dlg.SelectedPath;
                YLWService.Utils.SaveSetting("HaesungHASP", "FileDownload", "Path", txtDownPath.Text);
            }
        }

        private void DgvEmpList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgv.RowCount) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;
            if (e.ColumnIndex == dgv.Columns["SEL"].Index) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvFileList.Rows.Clear();
                picPreview.Image = null;
                txtSingleFileName.Text = "";
                txtSingleFileBase64.Text = "";
                DataTable ydt = GetFileList(dgv.Rows[e.RowIndex].Cells["EmpSeq"].Value + "");
                if (ydt == null) return;
                for (int i = 0; i < ydt.Rows.Count; i++)
                {
                    DataRow ydr = ydt.Rows[i];
                    int idx = dgvFileList.Rows.Add();
                    DataGridViewRow drow = dgvFileList.Rows[idx];
                    drow.Cells["FileType"].Value = ydr["FileType"];
                    drow.Cells["AttachFileSeq"].Value = ydr["FileSeq"];
                    drow.Cells["AttachFileNo"].Value = ydr["AttachFileNo"];
                    drow.Cells["FileName"].Value = ydr["FileName"];
                    drow.Cells["FileExt"].Value = ydr["FileExt"];
                }
                dgvFileList.Refresh();
                DgvFileList_CellClick(dgvFileList, new DataGridViewCellEventArgs(0, 0));
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

        private void DgvFileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgv.RowCount) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string attachFileSeq = dgv.Rows[e.RowIndex].Cells["AttachFileSeq"].Value + "";
                string attachFileNo = dgv.Rows[e.RowIndex].Cells["AttachFileNo"].Value + "";
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet yds = YLWService.MTRServiceModule.CallMTRFileDownload(security, attachFileSeq, attachFileNo, "");
                if (yds == null || yds.Tables.Count < 1) return;
                DataTable ydt = yds.Tables[0];
                if (ydt.TableName == "ErrorMessage") return;
                if (ydt.Rows.Count < 1) return;
                string fileBase64 = ydt.Rows[0]["FileBase64"] + "";
                Image img = YLWService.Utils.stringToImage(fileBase64);
                picPreview.Image = img;
                txtSingleFileName.Text = dgv.Rows[e.RowIndex].Cells["FileName"].Value + "";
                txtSingleFileBase64.Text = fileBase64;
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

        private void BtnSingleView_Click(object sender, EventArgs e)
        {
            try
            {
                if (picPreview.Image == null) return;
                string file = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), ".jpg"));
                if (File.Exists(file)) File.Delete(file);
                FileStream fs = new FileStream(file, FileMode.CreateNew);
                picPreview.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnSingleDown_Click(object sender, EventArgs e)
        {
            try
            {
                fnFileDownload(txtDownPath.Text, txtSingleFileName.Text, txtSingleFileBase64.Text);
                System.Diagnostics.Process.Start("explorer.exe", txtDownPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnDownloadAll_Click(object sender, EventArgs e)
        {
            string tempPath = Path.GetTempPath();
            string tempDownPath = Path.Combine(tempPath, Path.GetRandomFileName());
            Directory.CreateDirectory(tempDownPath);

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.panProcess.Visible = true;
                this.panDownFolder.Visible = false;

                var checkedRows = from DataGridViewRow r in dgvEmpList.Rows where Utils.ToBool(r.Cells["SEL"].Value) == true select r;

                int maxcnt = checkedRows.Count();
                this.pbProgress.Minimum = 0;
                this.pbProgress.Maximum = maxcnt;
                int cnt = 0;
                foreach (var row in checkedRows)
                {
                    string empName = row.Cells["EmpName"].Value + "";
                    string empSeq = row.Cells["EmpSeq"].Value + "";
                    this.pbProgress.Value = ++cnt;
                    //this.pbProgress.PerformStep();
                    string msg = maxcnt.ToString() + "건중 " + cnt.ToString() + "번째 - 사원명 : " + empName + " ";
                    this.txtProcess.Text = msg;
                    Application.DoEvents();
                    FileDownload(tempPath, tempDownPath, empName, GetFileList(empSeq), msg);
                }
                System.Diagnostics.Process.Start("explorer.exe", txtDownPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Directory.Delete(tempDownPath);
                this.panProcess.Visible = false;
                this.panDownFolder.Visible = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private DataTable GetFileList(string empSeq)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            security.serviceId = "Metro.Package.AdjHR.BisEmpAttachMng";
            security.methodId = "QueryFile";
            security.companySeq = _param.CompanySeq;

            DataSet ds = new DataSet("ROOT");
            DataTable dt = ds.Tables.Add("DataBlock1");
            dt.Columns.Add("EmpSeq");
            dt.Columns.Add("FileSelect");
            dt.Clear();
            DataRow dr = dt.Rows.Add();
            dr["EmpSeq"] = empSeq;
            string fileSelect = "";
            fileSelect = Utils.SetP(fileSelect, ";", 1, chkRegIdFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 2, chkBnakBookFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 3, chkCarPayFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 4, chkChildCareFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 5, chkGradeCertFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 6, chkTranscriptFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 7, chkCareerFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 8, chkLicenseFile.Checked ? "1" : "0");
            fileSelect = Utils.SetP(fileSelect, ";", 9, chklinguisticFile.Checked ? "1" : "0");
            dr["FileSelect"] = fileSelect;

            DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
            if (yds == null || yds.Tables.Count < 1) return null;
            return yds.Tables[0];
        }

        private DataTable GetFileBase64(string attachFileSeq)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
            DataSet yds = YLWService.MTRServiceModule.CallMTRFileDownload(security, attachFileSeq, "", "");
            if (yds == null || yds.Tables.Count < 1) return null;
            return yds.Tables[0];
        }

        private void FileDownload(string tempPath, string tempDownPath, string empName, DataTable pdt, string msg)
        {
            if (pdt == null || pdt.Rows.Count < 1) return;
            Utils.ClearFolder(tempDownPath);
            for (int i = 0; i < pdt.Rows.Count; i++)
            {
                string attachFileSeq = pdt.Rows[i]["FileSeq"] + "";
                string attachFileNo = pdt.Rows[i]["AttachFileNo"] + "";
                string fileName = pdt.Rows[i]["FileName"] + "";
                // 진행정보
                this.txtProcess.Text = msg + "  파일명(" + (i + 1) + "/" + pdt.Rows.Count + ") : " + fileName + " ";
                Application.DoEvents();
                // 진행정보
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet yds = YLWService.MTRServiceModule.CallMTRFileDownload(security, attachFileSeq, attachFileNo, "");
                if (yds == null || yds.Tables.Count < 1) continue;
                DataTable ydt = yds.Tables[0];
                if (ydt.TableName == "ErrorMessage") continue;
                if (ydt.Rows.Count < 1) continue;
                string fileBase64 = ydt.Rows[0]["FileBase64"] + "";
                fnFileDownload(tempDownPath, fileName, fileBase64);
            }
            string zipFile = Path.Combine(tempPath, empName + ".zip");
            Utils.ZipAddDirectory(zipFile, tempDownPath);
            FileMoveTo(zipFile, txtDownPath.Text);
            Utils.ClearFolder(tempDownPath);
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private string fnFileDownload(string downloadFolder, string fileName, string fileBase64)
        {
            if (fileBase64 == "")
            {
                return "";
            }

            string file = fnGetFileName(downloadFolder, fileName);
            byte[] bytes_file = Convert.FromBase64String(fileBase64);
            FileStream fs = new FileStream(file, FileMode.Create);
            fs.Write(bytes_file, 0, bytes_file.Length);
            fs.Close();
            return file;
        }

        private string fnGetFileName(string downloadFolder, string fileName)
        {
            if (!Directory.Exists(downloadFolder)) Directory.CreateDirectory(downloadFolder);
            string file = Path.Combine(downloadFolder, fileName);
            int fileCount = 0;
            while (File.Exists(file))
            {
                fileCount++;
                file = Path.GetFileNameWithoutExtension(fileName) + "(" + fileCount.ToString() + ")" + Path.GetExtension(fileName);
                file = Path.Combine(downloadFolder, file);
            }
            return file;
        }

        private bool FileMoveTo(string sourceFile, string destPath)
        {
            try
            {
                string filename = Path.GetFileName(sourceFile);
                string destFile = fnGetFileName(destPath, filename);
                File.Move(sourceFile, destFile);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
