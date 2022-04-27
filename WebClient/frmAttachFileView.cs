using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class frmAttachFileView : Form
    {
        private static AttachFileParam _param = null;

        private static frmAttachFileView current = null;

        public static frmAttachFileView Current { get { return current; } }

        bool _bEvent = false;

        public frmAttachFileView()
        {
            InitializeComponent();

            this.btnQuery.Click += BtnQuery_Click;
            this.btnExit.Click += BtnExit_Click;
            this.btnPrint.Click += BtnPrint_Click;
            this.btnDownload.Click += BtnDownload_Click;
            this.dgvList.CellClick += DgvList_CellClick;
            this.dgvList.CellDoubleClick += DgList_CellDoubleClick;
            this.dgvImg.CellDoubleClick += DgList_CellDoubleClick;

            _bEvent = true;
        }

        public static bool ShowPreview(AttachFileParam p)
        {
            try
            {
                _param = p;

                if (current == null || current.IsDisposed)
                {
                    current = new frmAttachFileView();
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

                btnQuery.PerformClick();

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
            txtFileSeq.Text = _param.FileSeq;
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = Path.Combine(userRoot, "Downloads");
                for (int ii = 0; ii < dgvList.Rows.Count; ii++)
                {
                    fnFileDownload(downloadFolder, dgvList.Rows[ii]);
                }
                System.Diagnostics.Process.Start("explorer.exe", downloadFolder);
                //MessageBox.Show("다운로드 완료");
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

        public string fnFileDownload(string downloadFolder, DataGridViewRow drow)
        {
            string fileName = drow.Cells["FileName"].Value + "";
            string fileBase64 = drow.Cells["FileBase64"].Value + "";
            if (fileBase64 == "")
            {
                return "";
            }

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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Image img = null;
                string nm = "";

                var listModels = new List<ViewImage>();
                for (int ii = 0; ii < dgvImg.Rows.Count; ii++)
                {
                    img = (Image)dgvImg.Rows[ii].Cells["FileImage1"].Value;
                    nm = (string)dgvImg.Rows[ii].Cells["FileName1"].Value;
                    if (img != null)
                    {
                        listModels.Add(new ViewImage() { FileImage1 = img, FileName1 = nm });
                    }
                    img = (Image)dgvImg.Rows[ii].Cells["FileImage2"].Value;
                    nm = (string)dgvImg.Rows[ii].Cells["FileName2"].Value;
                    if (img != null)
                    {
                        listModels.Add(new ViewImage() { FileImage1 = img, FileName1 = nm });
                    }
                }
                XtraReport1 rpt1 = new XtraReport1(listModels);
                DevExpress.XtraReports.UI.ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rpt1);
                rpt.AutoShowParametersPanel = false;
                rpt.ShowPreviewDialog();
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                picPreview.Image = null;
                dgvList.Rows.Clear();
                dgvList.Refresh();
                dgvImg.Rows.Clear();
                dgvImg.Refresh();

                string fileSeq = txtFileSeq.Text;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet pds = null;
                try
                {
                    pds = YLWService.MTRServiceModule.CallMTRFileDownload(security, fileSeq, "", "");
                }
                catch (Exception ex) { return; }
                if (pds == null || pds.Tables.Count < 1)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }
                if (pds.Tables.Contains("ErrorMessage"))
                {
                    MessageBox.Show(pds.Tables["ErrorMessage"].Rows[0]["Message"] + "");
                    return;
                }
                DataTable ydt = pds.Tables[0];
                if (ydt != null && ydt.Rows.Count > 0)
                {
                    //목록
                    dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < ydt.Rows.Count; i++)
                    {
                        DataRow dr = ydt.Rows[i];
                        int idx = dgvList.Rows.Add();
                        DataGridViewRow drow = dgvList.Rows[idx];
                        string strImage = dr["FileBase64"].ToString();
                        Image img = YLWService.Utils.stringToImage(strImage);
                        drow.Cells["fileImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);   // 사진
                        drow.Cells["AttachFileSeq"].Value = dr["AttachFileSeq"];
                        drow.Cells["AttachFileNo"].Value = dr["AttachFileNo"];
                        drow.Cells["FileName"].Value = dr["FileName"];
                        drow.Cells["FilePathName"].Value = dr["RealFileName"];
                        drow.Cells["FileExt"].Value = dr["FileExt"];
                        drow.Cells["FileBase64"].Value = dr["FileBase64"];
                    }
                    dgvList.Refresh();
                    DgvList_CellClick(dgvList, new DataGridViewCellEventArgs(0, 0));

                    //미리보기
                    int colIdx = 0;
                    int rowidx = 0;
                    dgvImg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < ydt.Rows.Count; i++)
                    {
                        DataRow dr = ydt.Rows[i];
                        colIdx = i % 2 + 1;
                        if (colIdx == 1) rowidx = dgvImg.Rows.Add();
                        DataGridViewRow drow = dgvImg.Rows[rowidx];
                        string strImage = dr["FileBase64"].ToString();
                        Image img = YLWService.Utils.stringToImage(strImage);
                        drow.Cells["fileImage" + colIdx].Value = img;      // 사진
                        drow.Cells["AttachFileSeq" + colIdx].Value = dr["AttachFileSeq"];
                        drow.Cells["AttachFileNo" + colIdx].Value = dr["AttachFileNo"];
                        drow.Cells["FileName" + colIdx].Value = dr["FileName"];
                        drow.Cells["FilePathName" + colIdx].Value = dr["RealFileName"];
                        drow.Cells["FileExt" + colIdx].Value = dr["FileExt"];
                        drow.Cells["FileBase64" + colIdx].Value = dr["FileBase64"];
                    }
                    dgvImg.Refresh();
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

        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv == null) return;
                if (e.RowIndex < 0 || e.RowIndex >= dgv.RowCount) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;

                Image img = null;
                if (dgv == dgvList)
                {
                    img = YLWService.Utils.stringToImage(dgv.Rows[e.RowIndex].Cells["FileBase64"].Value + "");
                }
                else
                {
                    img = (Image)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                picPreview.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv == null) return;
                if (e.RowIndex < 0 || e.RowIndex >= dgv.RowCount) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.ColumnCount) return;

                Image img = null;
                if (dgv == dgvList)
                {
                    img = YLWService.Utils.stringToImage(dgv.Rows[e.RowIndex].Cells["FileBase64"].Value + "");
                }
                else
                {
                    img = (Image)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                ImageView(img);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImageView(Image img)
        {
            try
            {
                if (img == null) return;
                string file = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), ".jpg"));
                if (File.Exists(file)) File.Delete(file);
                FileStream fs = new FileStream(file, FileMode.CreateNew);
                img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }

    public class ViewImage
    {
        public Image FileImage1 { get; set; }
        public string FileName1 { get; set; }
    }
}
