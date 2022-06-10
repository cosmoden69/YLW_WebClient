using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

using YLWService;

namespace YLW_WebClient.CAA
{
    public partial class frmAttachFileView : Form
    {
        private Control currentObject;

        private static AttachFileParam _param = null;

        private static frmAttachFileView current = null;

        public static frmAttachFileView Current { get { return current; } }

        bool _bEvent = false;

        public frmAttachFileView()
        {
            InitializeComponent();

            this.btnSave.Click += BtnSave_Click;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnExit.Click += BtnExit_Click;
            this.btnPrint.Click += BtnPrint_Click;
            this.btnDownload.Click += BtnDownload_Click;
            this.dgvList.CellClick += DgvList_CellClick;
            this.dgvList.CellDoubleClick += DgList_CellDoubleClick;
            this.dgvList.RowPostPaint += DgvList_RowPostPaint;
            this.dgvList.DataError += DgvList_DataError;
            this.dgvList.CellValueChanged += DgvList_CellValueChanged;

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

                if (currentObject == pdf)
                {
                    pdf.Print();
                }
                else if (currentObject == pic)
                {
                    pic.Print();
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                currentObject = null;
                pic.Visible = false;
                pdf.Visible = false;
                dgvList.Rows.Clear();
                dgvList.Refresh();

                string fileSeq = txtFileSeq.Text;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet pds = null;
                try
                {
                    pds = YLWService.MTRServiceModule.CallMTRFileDownload(security, fileSeq, "", "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
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
                    if (!ydt.Columns.Contains("WorkingTag")) ydt.Columns.Add("WorkingTag");
                    for (int i = 0; i < ydt.Rows.Count; i++)
                    {
                        DataRow dr = ydt.Rows[i];
                        dr["WorkingTag"] = "";
                        int idx = dgvList.Rows.Add();
                        DataGridViewRow drow = dgvList.Rows[idx];
                        SetGridRow(drow, dr);
                    }
                    dgvList.Refresh();
                    DgvList_CellClick(dgvList, new DataGridViewCellEventArgs(0, 0));
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

        private bool SetGridRow(DataGridViewRow drow, DataRow dr)
        {
            try
            {
                _bEvent = false;

                string strImage = dr["FileBase64"].ToString();
                Image img = YLWService.Utils.stringToImage(strImage);
                drow.Cells["fileImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);   // 사진
                drow.Cells["AttachFileSeq"].Value = dr["AttachFileSeq"];
                drow.Cells["AttachFileNo"].Value = dr["AttachFileNo"];
                drow.Cells["FileName"].Value = dr["FileName"];
                drow.Cells["FilePathName"].Value = dr["RealFileName"];
                drow.Cells["FileExt"].Value = dr["FileExt"];
                drow.Cells["Remark"].Value = dr["Remark"];
                drow.Cells["FileBase64"].Value = dr["FileBase64"];
                drow.Cells["WorkingTag"].Value = (dr.Table.Columns.Contains("WorkingTag") ? dr["WorkingTag"] + "" : "");
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _bEvent = true;
            }
        }

        private bool SetGridRowUpdate(DataGridViewRow drow, DataRow dr)
        {
            try
            {
                _bEvent = false;

                drow.Cells["FileName"].Value = dr["FileName"];
                drow.Cells["Remark"].Value = dr["Remark"];
                drow.Cells["WorkingTag"].Value = (dr.Table.Columns.Contains("WorkingTag") ? dr["WorkingTag"] + "" : "");
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _bEvent = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (chkChanged() != true)
            {
                MessageBox.Show("수정된 내역이 없습니다");
                return;
            }
            if (chkValid() != true)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("AU"), out yds) == true)
                {
                    DataTable dt4 = yds.Tables["DataBlock4"];
                    for (int ii = 0; ii < dt4.Rows.Count; ii++)
                    {
                        dt4.Rows[ii]["WorkingTag"] = "";
                        int idx = Utils.ToInt(dt4.Rows[ii]["IDX_NO"]);
                        SetGridRowUpdate(dgvList.Rows[idx - 1], dt4.Rows[ii]);
                    }
                    dgvList.Refresh();
                    MessageBox.Show("저장 완료");
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

        private bool chkChanged()
        {
            try
            {
                for (int ii = 0; ii < dgvList.Rows.Count; ii++)
                {
                    DataGridViewRow dr = dgvList.Rows[ii];
                    string workingTag = dr.Cells["WorkingTag"].Value + "";
                    if (workingTag != "") return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool chkValid()
        {
            try
            {
                for (int ii = 0; ii < dgvList.Rows.Count; ii++)
                {
                    DataGridViewRow dr = dgvList.Rows[ii];
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }

        private bool SaveData(DataSet pds, out DataSet yds)
        {
            YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사

            yds = YLWService.MTRServiceModule.CallMTRFileUpdate(security, pds);
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

        private DataSet GetSaveData(string fg)
        {
            try
            {
                DataSet ds = new DataSet();

                DataTable dt1 = ds.Tables.Add("DataBlock1");
                dt1.Columns.Add("CompanySeq");
                dt1.Columns.Add("LanguageSeq");
                dt1.Columns.Add("LoginPgmSeq");
                dt1.Columns.Add("UserSeq");
                dt1.Columns.Add("AttachFileConstSeq");
                dt1.Columns.Add("AttachFileSeq");
                dt1.Columns.Add("AttachFileNo");
                dt1.Columns.Add("IsRepFile");
                dt1.Columns.Add("FileName");
                dt1.Columns.Add("Remark");
                dt1.Columns.Add("WorkingTag");
                dt1.Columns.Add("IDX_NO");
                dt1.Columns.Add("DataSeq");
                dt1.Clear();
                int seq = 1;
                for (int i = 0; i < dgvList.RowCount; i++)
                {
                    string workingTag = dgvList.Rows[i].Cells["WorkingTag"].Value + "";
                    if (fg == "AU")
                    {
                        if (workingTag != "A" && workingTag != "U") continue;
                    }
                    if (fg == "D")
                    {
                        if (workingTag != "D") continue;
                    }
                    DataRow dr = dt1.Rows.Add();
                    dr["CompanySeq"] = _param.CompanySeq.ToString();
                    dr["LanguageSeq"] = "1";
                    dr["LoginPgmSeq"] = "";
                    dr["UserSeq"] = "0";
                    dr["AttachFileConstSeq"] = "";
                    dr["AttachFileSeq"] = dgvList.Rows[i].Cells["AttachFileSeq"].Value;
                    dr["AttachFileNo"] = dgvList.Rows[i].Cells["AttachFileNo"].Value;
                    dr["IsRepFile"] = "";
                    dr["FileName"] = dgvList.Rows[i].Cells["FileName"].Value;
                    dr["Remark"] = dgvList.Rows[i].Cells["Remark"].Value;
                    dr["WorkingTag"] = dgvList.Rows[i].Cells["WorkingTag"].Value;
                    dr["IDX_NO"] = i + 1;
                    dr["Dataseq"] = seq;
                    seq++;
                }

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
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

                string file = GetFile(dgv.Rows[e.RowIndex]);
                if (file == "") return;  //파일없음
                if (Path.GetExtension(file).ToUpper() == ".PDF")
                {
                    pic.Visible = false;
                    pdf.Visible = true;
                    pdf.Dock = DockStyle.Fill;
                    pdf.LoadDocument(file);
                    currentObject = pdf;
                }
                else if (Path.GetExtension(file).ToUpper() == ".TIF" || Path.GetExtension(file).ToUpper() == ".TIFF")
                {
                    List<Image> images = GetAllPages(file);
                    pic.Visible = true;
                    pic.Dock = DockStyle.Fill;
                    pdf.Visible = false;
                    pic.SetImage(images);
                    currentObject = pic;
                }
                else
                {
                    List<Image> images = new List<Image>();
                    images.Add(Image.FromFile(file));
                    pic.Visible = true;
                    pic.Dock = DockStyle.Fill;
                    pdf.Visible = false;
                    pic.SetImage(images);
                    currentObject = pic;
                }
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

                string file = GetFile(dgv.Rows[e.RowIndex]);
                if (file != "") System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_bEvent) return;

            try
            {
                DataGridViewRow dr = dgvList.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "")
                {
                    dr.HeaderCell.Value = "U";
                    dr.Cells["WorkingTag"].Value = "U";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void DgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvList.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";

                //' Position text
                SizeF size = e.Graphics.MeasureString(workingTag, dgvList.Font);
                if (dgvList.RowHeadersWidth < Convert.ToInt32(size.Width + 20))
                {
                    dgvList.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
                }

                //' Use default system text brush
                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(workingTag, dgvList.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
            }
            catch { }
        }


        private void ImageView(DataGridViewRow drow)
        {
            try
            {
                Image img = YLWService.Utils.stringToImage(drow.Cells["FileBase64"].Value + "");
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

        private string GetFile(DataGridViewRow drow)
        {
            try
            {
                string ext = Utils.ConvertToString(drow.Cells["FileExt"].Value);
                string fileBase64 = Utils.ConvertToString(drow.Cells["FileBase64"].Value);
                if (fileBase64 == null || fileBase64 == "") return "";
                byte[] bytes_file = Convert.FromBase64String(fileBase64);
                string file = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), "." + ext));
                if (File.Exists(file)) File.Delete(file);
                FileStream fs = new FileStream(file, FileMode.Create);
                fs.Write(bytes_file, 0, bytes_file.Length);
                fs.Close();
                return file;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private List<Image> GetAllPages(string file)
        {
            List<Image> images = new List<Image>();
            Bitmap bitmap = (Bitmap)Image.FromFile(file);
            int count = bitmap.GetFrameCount(FrameDimension.Page);
            for (int idx = 0; idx < count; idx++)
            {
                // save each frame to a bytestream
                bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                MemoryStream byteStream = new MemoryStream();
                bitmap.Save(byteStream, ImageFormat.Tiff);

                // and then create a new Image from it
                images.Add(Image.FromStream(byteStream));
            }
            return images;
        }
    }

    public class ViewImage
    {
        public Image FileImage1 { get; set; }
        public string FileName1 { get; set; }
    }
}
