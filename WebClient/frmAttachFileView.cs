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

            this.dgvList.RowPostPaint += DgvList_RowPostPaint;
            this.dgvList.DragDrop += DgvList_DragDrop;
            this.dgvList.DragEnter += DgvList_DragEnter;
            this.dgvList.DataError += DgvList_DataError;
            this.dgvList.CellClick += DgvList_CellClick;
            this.dgvList.CellDoubleClick += DgList_CellDoubleClick;
            this.dgvList.CellContentClick += DgvList_CellContentClick;
            this.dgvList.CellValueChanged += DgvList_CellValueChanged;
            this.btnFileA.Click += BtnFileA_Click;
            this.btnFileD.Click += BtnFileD_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnExit.Click += BtnExit_Click;
            this.btnPrint.Click += BtnPrint_Click;
            this.btnDownload.Click += BtnDownload_Click;

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
            txtAcptMgmtSeq.Text = _param.AcptMgmtSeq;
            txtFileSeq.Text = _param.FileSeq;
            if (_param.ReadOnlyFg == "1")
            {
                btnFileA.Visible = false;
                btnFileD.Visible = false;
                DeleteRow.Visible = false;
            }
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
                //새로 화면에 들어가능 경우가 있으므로 무조건 조회되도록
                //if (chkChanged() == true)
                //{
                //    if (MessageBox.Show("수정된 내역이 있습니다. 새로 조회하시겠습니까", "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                //}

                Cursor.Current = Cursors.WaitCursor;

                currentObject = null;
                pic.Visible = false;
                pdf.Visible = false;
                dgvList.Rows.Clear();
                dgvList.Refresh();

                string fileSeq = _param.FileSeq;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                DataSet pds = null;
                try
                {
                    pds = YLWService.MTRServiceModule.CallMTRFileDownload(security, fileSeq, "", "");
                }
                catch (Exception ex)
                {
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
                drow.Cells["AttachFileConstSeq"].Value = dr["AttachFileConstSeq"];
                drow.Cells["AttachFileSeq"].Value = dr["AttachFileSeq"];
                drow.Cells["AttachFileNo"].Value = dr["AttachFileNo"];
                drow.Cells["FileName"].Value = dr["FileName"];
                drow.Cells["FilePathName"].Value = dr["RealFileName"];
                drow.Cells["FileExt"].Value = dr["FileExt"];
                drow.Cells["FileSize"].Value = dr["FileSize"];
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

        private void DeleteRows()
        {
            try
            {
                _bEvent = false;
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("D"), out yds) == true)
                {
                    DataTable dt4 = yds.Tables["DataBlock4"];
                    if (dt4 == null) return;
                    int idx = 0;
                    for (int ii = dt4.Rows.Count - 1; ii >= 0; ii--)
                    {
                        idx = Utils.ToInt(dt4.Rows[ii]["IDX_NO"]);
                        dgvList.Rows.RemoveAt(idx - 1);
                    }
                    dgvList.Refresh();
                    idx = (idx > 0 ? idx - 1 : 0);
                    DgvList_CellClick(dgvList, new DataGridViewCellEventArgs(0, idx));
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
                _bEvent = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private int AddRows(string P_File)
        {
            try
            {
                _bEvent = false;
                Cursor.Current = Cursors.WaitCursor;

                //if (readOnlyMode) return;
                FileInfo F_Info = new FileInfo(P_File);

                int rowIndex = dgvList.Rows.Add();
                DataGridViewRow dr = dgvList.Rows[rowIndex];

                string strImage = ToBase64String(F_Info);
                Image img = YLWService.Utils.stringToImage(strImage);
                dr.Cells["fileImage"].Value = img?.GetThumbnailImage(120, 80, null, IntPtr.Zero);   // 사진
                dr.Cells["AttachFileConstSeq"].Value = _param.FileConstSeq;
                dr.Cells["AttachFileSeq"].Value = _param.FileSeq;
                dr.Cells["AttachFileNo"].Value = "";
                dr.Cells["FileName"].Value = F_Info.Name;
                dr.Cells["FilePathName"].Value = F_Info.Name;
                dr.Cells["FileExt"].Value = F_Info.Extension.Replace(".", "");
                dr.Cells["FileSize"].Value = YLWService.Utils.ToInt(F_Info.Length);
                dr.Cells["Remark"].Value = "";
                dr.Cells["FileBase64"].Value = strImage;

                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "")
                {
                    dr.HeaderCell.Value = "A";
                    dr.Cells["WorkingTag"].Value = "A";
                }
                return rowIndex;
            }
            catch
            {
                return 0;
            }
            finally
            {
                _bEvent = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnFileA_Click(object sender, EventArgs e)
        {
            //if (readOnlyMode) return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff;*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int rowidx = 0;
                    foreach (string P_File in openFileDialog.FileNames)
                    {
                        rowidx = AddRows(P_File);
                    }
                    dgvList.Refresh();
                    DgvList_CellClick(dgvList, new DataGridViewCellEventArgs(0, rowidx));
                }
            }
            dgvList.Focus();
        }

        private void BtnFileD_Click(object sender, EventArgs e)
        {
            //if (readOnlyMode) return;
            if (dgvList.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("선택된 " + dgvList.SelectedRows.Count.ToString() + "개 파일을 정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvList.SelectedRows)
                    {
                        if (row.Cells["WorkingTag"].Value + "" == "A")
                        {
                            dgvList.Rows.Remove(row);
                        }
                        else
                        {
                            row.Cells["WorkingTag"].Value = "D";
                        }
                    }
                    DeleteRows();
                }
            }
            dgvList.Focus();
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
                    DataTable dt3 = yds.Tables["DataBlock3"];
                    if (Utils.ToInt(_param.FileSeq) == 0)
                    {
                        string fileSeq = dt3.Rows[0]["AttachFileSeq"].ToString();
                        if (!SaveFileSeq(fileSeq))  //파일내부코드 업데이트
                        {
                            return;
                        }
                        _param.FileSeq = fileSeq;
                        txtFileSeq.Text = _param.FileSeq;
                    }
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

        private bool SaveFileSeq(string fileSeq)
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisCSAttachFileMan";
                security.methodId = "Save";
                security.companySeq = _param.CompanySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = _param.UserID;

                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("PgmName");
                dt.Columns.Add("FileSeqName");
                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");
                dt.Columns.Add("KeyStr");
                dt.Columns.Add("ReadOnlyFg");
                dt.Columns.Add("FileConstSeq");
                dt.Columns.Add("FileSeq");
                dt.Columns.Add("UserID");
                dt.Clear();

                DataRow dr = dt.Rows.Add();
                dr["PgmName"] = _param.PgmName;
                dr["FileSeqName"] = _param.FileSeqName;
                dr["AcptMgmtSeq"] = _param.AcptMgmtSeq;
                dr["ReSurvAsgnNo"] = _param.ReSurvAsgnNo;
                dr["KeyStr"] = _param.KeyStr;
                dr["ReadOnlyFg"] = _param.ReadOnlyFg;
                dr["FileConstSeq"] = _param.FileConstSeq;
                dr["FileSeq"] = fileSeq;
                dr["UserID"] = _param.UserID;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
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
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
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
                DataRow dr1 = dt1.Rows.Add();
                dr1["CompanySeq"] = _param.CompanySeq.ToString();
                dr1["LanguageSeq"] = "1";
                dr1["LoginPgmSeq"] = "";
                dr1["UserSeq"] = "0";
                dr1["AttachFileConstSeq"] = _param.FileConstSeq;
                dr1["AttachFileSeq"] = _param.FileSeq;

                DataTable dt2 = ds.Tables.Add("DataBlock2");
                dt2.Columns.Add("AttachFileNo");
                dt2.Columns.Add("IsRepFile");
                dt2.Columns.Add("FilePath");
                dt2.Columns.Add("FileName");
                dt2.Columns.Add("FileExt");
                dt2.Columns.Add("FileSize");
                dt2.Columns.Add("Remark");
                dt2.Columns.Add("FileBase64");
                dt2.Columns.Add("WorkingTag");
                dt2.Columns.Add("IDX_NO");
                dt2.Columns.Add("DataSeq");
                dt2.Clear();
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

                    DataRow dr2 = dt2.Rows.Add();
                    dr2["AttachFileNo"] = dgvList.Rows[i].Cells["AttachFileNo"].Value;
                    dr2["IsRepFile"] = "";
                    dr2["FilePath"] = "";
                    dr2["FileName"] = dgvList.Rows[i].Cells["FileName"].Value;
                    dr2["FileExt"] = dgvList.Rows[i].Cells["FileExt"].Value;
                    dr2["FileSize"] = dgvList.Rows[i].Cells["FileSize"].Value;
                    dr2["Remark"] = dgvList.Rows[i].Cells["Remark"].Value;
                    dr2["FileBase64"] = dgvList.Rows[i].Cells["FileBase64"].Value;
                    dr2["WorkingTag"] = dgvList.Rows[i].Cells["WorkingTag"].Value;
                    dr2["IDX_NO"] = i + 1;
                    dr2["Dataseq"] = seq;
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

        private void DgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (readOnlyMode) return;
                DataGridView dgv = (DataGridView)sender;

                if (e.RowIndex > -1)
                {
                    if (!dgv.Rows[e.RowIndex].IsNewRow)
                    {
                        if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                        {
                            if ((dgv.Columns[e.ColumnIndex] as DataGridViewButtonColumn).Text == "") return;
                            if (MessageBox.Show("정말 삭제 하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                            {
                                if (dgv.Rows[e.RowIndex].Cells["WorkingTag"].Value + "" == "A")
                                {
                                    dgv.Rows.Remove(dgv.Rows[e.RowIndex]);
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["WorkingTag"].Value = "D";
                                    DeleteRows();
                                }
                            }
                        }
                    }
                }
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

        private void DgvList_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                //if (readOnlyMode) return;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DgvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void DgvList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //if (readOnlyMode) return;
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);

                int rowidx = 0;
                foreach (string P_File in FileList)
                {
                    rowidx = AddRows(P_File);
                }
                dgvList.Refresh();
                DgvList_CellClick(dgvList, new DataGridViewCellEventArgs(0, rowidx));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private string ToBase64String(FileInfo F_Info)
        {
            try
            {
                byte[] rptbyte = (byte[])MetroSoft.HIS.cFile.ReadBinaryFile(F_Info.FullName);
                string fileBase64 = Convert.ToBase64String(rptbyte);
                return fileBase64;
            }
            catch
            {
                return "";
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
