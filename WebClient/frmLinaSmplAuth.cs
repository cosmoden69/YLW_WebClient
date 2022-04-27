using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLWService;
using YLWService.Extensions;

namespace YLW_WebClient
{
    public partial class frmLinaSmplAuth : Form
    {
        ReportParam param;

        bool _bEvent = false;

        public frmLinaSmplAuth(ReportParam p)
        {
            this.param = p;

            InitializeComponent();

            this.Load += FrmSmplAuthcs_Load;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnRowDel.Click += BtnRowDel_Click;
            this.btnSave.Click += BtnSave_Click;
            this.grd.RowPostPaint += Grd_RowPostPaint;
            this.grd.DataError += Grd_DataError;
            this.grd.CellContentClick += Grd_CellContentClick;
            this.grd.CellValueChanged += Grd_CellValueChanged;

            _bEvent = true;
        }

        private void FrmSmplAuthcs_Load(object sender, EventArgs e)
        {
            Utils.DoubleBuffered(grd, true);

            grd.AutoGenerateColumns = false;
            grd.AddColumn("TEXTBOX", "code_id", "코드구분", 100, false, true);
            grd.AddColumn("TEXTBOX", "code", "코드", 60);
            grd.AddColumn("TEXTBOX", "value_text", "사용자ID", 200);
            grd.AddColumn("TEXTBOX", "value_remark", "비고", 200);
            grd.AddColumn("TEXTBOX", "sort_order", "정렬순서", 60, false, true);
            grd.AddColumn("TEXTBOX", "id", "내부코드", 60, false, true);
            grd.AddColumn("BUTTON", "delete", "삭제", 40);
            grd.AddColumn("TEXTBOX", "WorkingTag", "WorkingTag", 100, false, true);
            grd.Columns["value_remark"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd.AllowUserToAddRows = true;
            grd.EditMode = DataGridViewEditMode.EditOnEnter;

            btnQuery.PerformClick();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkChanged() == true)
                {
                    if (MessageBox.Show("수정된 내역이 있습니다. 새로 조회하시겠습니까", "확인", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDICdMngLina";
                security.methodId = "QueryDetail";
                security.companySeq = param.CompanySeq;

                DataSet ds = new DataSet("ROOT");

                DataTable dt = ds.Tables.Add("DataBlock2");
                dt.Columns.Add("code_id");
                dt.Columns.Add("code_name");
                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["code_id"] = txtCode_id.Text;

                grd.Rows.Clear();
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타가 없습니다");
                    return;
                }

                // USERCD_AUTH
                DataTable SA16 = yds.Tables["DataBlock3"];
                if (SA16 != null && SA16.Rows.Count > 0)
                {
                    for (int i = 0; i < SA16.Rows.Count; i++)
                    {
                        int idx = grd.Rows.Add();
                        SetGridRow(grd.Rows[idx], SA16.Rows[i]);
                        grd.AutoResizeRow(idx, DataGridViewAutoSizeRowMode.AllCells);
                    }
                    grd.Refresh();
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("AU"), out yds) == true)
                {
                    DataTable SA16 = yds.Tables["DataBlock3"];
                    for (int ii = 0; ii < SA16.Rows.Count; ii++)
                    {
                        SA16.Rows[ii]["WorkingTag"] = "Q";
                        int idx = Utils.ToInt(SA16.Rows[ii]["IDX_NO"]);
                        SetGridRow(grd.Rows[idx - 1], SA16.Rows[ii]);
                        grd.AutoResizeRow(idx, DataGridViewAutoSizeRowMode.AllCells);
                    }
                    grd.Refresh();
                    MessageBox.Show("저장 완료");

                    btnQuery.PerformClick();
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

        private void BtnRowDel_Click(object sender, EventArgs e)
        {
            //if (readOnlyMode) return;
            DataGridView dgv = grd;

            if (dgv.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("선택된 " + dgv.SelectedRows.Count.ToString() + "개 행을 삭제하시겠습니까 ?", "확인", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        if (row.Cells["WorkingTag"].Value + "" == "A")
                        {
                            dgv.Rows.Remove(row);
                        }
                        else
                        {
                            row.Cells["WorkingTag"].Value = "D";
                        }
                    }
                    DeleteRows();
                }
            }
        }

        private bool SetGridRow(DataGridViewRow drow, DataRow dr)
        {
            try
            {
                _bEvent = false;

                drow.Cells["code_id"].Value = dr["code_id"];
                drow.Cells["code"].Value = dr["code"];
                drow.Cells["value_text"].Value = dr["value_text"];
                drow.Cells["value_remark"].Value = dr["value_remark"];
                drow.Cells["sort_order"].Value = dr["sort_order"];
                drow.Cells["id"].Value = dr["id"];
                drow.Cells["WorkingTag"].Value = (dr.Table.Columns.Contains("WorkingTag") ? dr["WorkingTag"] + "" : "Q");
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
                Cursor.Current = Cursors.WaitCursor;

                DataSet yds = null;
                if (SaveData(GetSaveData("D"), out yds) == true)
                {
                    DataTable SA16 = yds.Tables["DataBlock3"];
                    if (SA16 == null) return;
                    for (int ii = SA16.Rows.Count - 1; ii >= 0; ii--)
                    {
                        int idx = Utils.ToInt(SA16.Rows[ii]["IDX_NO"]);
                        grd.Rows.RemoveAt(idx - 1);
                    }
                    grd.Refresh();
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

        private bool SaveData(DataSet pds, out DataSet yds)
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisAdjSLEDICdMngLina";
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet GetSaveData(string fg)
        {
            try
            {
                DataSet ds = new DataSet();

                DataTable dt2 = ds.Tables.Add("DataBlock2");
                dt2.Columns.Add("code_id");
                dt2.Columns.Add("code_name");
                dt2.Clear();
                DataRow dr = dt2.Rows.Add();
                dr["code_id"] = txtCode_id.Text;

                DataTable dt3 = ds.Tables.Add("DataBlock3");
                dt3.Columns.Add("code_id");
                dt3.Columns.Add("code");
                dt3.Columns.Add("value_text");
                dt3.Columns.Add("value_remark");
                dt3.Columns.Add("sort_order");
                dt3.Columns.Add("id");
                dt3.Columns.Add("WorkingTag");
                dt3.Columns.Add("IDX_NO");
                dt3.Columns.Add("DataSeq");
                dt3.Clear();

                int seq = 1;
                for (int i = 0; i < grd.RowCount; i++)
                {
                    string workingTag = grd.Rows[i].Cells["WorkingTag"].Value + "";
                    if (fg == "AU")
                    {
                        if (workingTag != "A" && workingTag != "U") continue;
                    }
                    if (fg == "D")
                    {
                        if (workingTag != "D") continue;
                    }
                    dr = dt3.Rows.Add();
                    dr["code_id"] = grd.Rows[i].Cells["code_id"].Value;
                    dr["code"] = grd.Rows[i].Cells["code"].Value;
                    dr["value_text"] = grd.Rows[i].Cells["value_text"].Value;
                    dr["value_remark"] = grd.Rows[i].Cells["value_remark"].Value;
                    dr["sort_order"] = grd.Rows[i].Cells["sort_order"].Value;
                    dr["id"] = grd.Rows[i].Cells["id"].Value;
                    dr["WorkingTag"] = grd.Rows[i].Cells["WorkingTag"].Value;
                    dr["IDX_NO"] = i + 1;
                    dr["Dataseq"] = seq;
                    seq++;
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool chkChanged()
        {
            try
            {
                for (int ii = 0; ii < grd.Rows.Count; ii++)
                {
                    DataGridViewRow dr = grd.Rows[ii];
                    string workingTag = dr.Cells["WorkingTag"].Value + "";
                    if (workingTag != "" && workingTag != "Q") return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool chkValid()
        {
            try
            {
                for (int ii = 0; ii < grd.Rows.Count; ii++)
                {
                    DataGridViewRow dr = grd.Rows[ii];
                    //if (dr.Cells["ObjSeq1"].Value + "" == "")
                    //{
                    //    MessageBox.Show((ii + 1).ToString() + "번 줄에 목적물명을 입력해야 됩니다");
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Grd_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewRow dr = grd.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "" || workingTag == "Q") return;

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

        private void Grd_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void Grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void Grd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_bEvent) return;
            if (e.ColumnIndex == grd.Columns["WorkingTag"].Index) return;

            try
            {
                DataGridViewRow dr = grd.Rows[e.RowIndex];
                string workingTag = dr.Cells["WorkingTag"].Value + "";
                if (workingTag == "A") return;
                if (workingTag == "")
                {
                    //dr.HeaderCell.Value = "A";
                    dr.Cells["WorkingTag"].Value = "A";
                }
                else
                {
                    //dr.HeaderCell.Value = "U";
                    dr.Cells["WorkingTag"].Value = "U";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
