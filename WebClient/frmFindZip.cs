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

namespace YLW_WebClient
{
    public partial class frmFindZip : Form
    {
        public int AddressSeq { get; set; } = 0;
        public string AddressText { get; set; } = "";
        public ClsAddress Address { get; set; }

        public frmFindZip(int seq, string txt)
        {
            InitializeComponent();

            this.AddressSeq = seq;
            this.AddressText = txt;
            this.txtFind.Text = txt;

            this.Load += FrmFindZip_Load;
            webBrowser1.DocumentTitleChanged += new EventHandler(webBrowser1_DocumentTitleChanged);
            txtFind.KeyDown += TxtFind_KeyDown;
            btnFind.Click += BtnFind_Click;
            btnApply.Click += BtnApply_Click;
        }

        private void FrmFindZip_Load(object sender, EventArgs e)
        {
            try
            {
                if (AddressSeq == 0)
                {
                    ClearAll();
                    btnFind.PerformClick();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Genuine.Standard.COM.BisCOMAddress";
                security.methodId = "Query";
                security.companySeq = security.companySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("AddressSeq");
                dt.Columns.Add("SMAddressTypeSeq");
                dt.Columns.Add("Address");

                dt.Clear();
                DataRow dr = dt.Rows.Add();
                dr["AddressSeq"] = this.AddressSeq;
                dr["SMAddressTypeSeq"] = 0;
                dr["Address"] = this.AddressText;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null || yds.Tables.Count < 1)
                {
                    ClearAll();
                    btnFind.PerformClick();
                    return;
                }
                for (int ii = 0; ii < yds.Tables[0].Rows.Count; ii++)
                {
                    DataRow drow = yds.Tables[0].Rows[ii];
                    if (drow["SMAddressTypeSeq"] + "" == "500052001")  //지번주소
                    {
                        txtZipcd.Text = GetData(drow, "ZipCode") + "";
                        txtjibunAddress.Text = GetData(drow, "AddressBasic") + "";
                        txtAddressDetail.Text = GetData(drow, "AddressDetail") + "";
                    }
                    else if (drow["SMAddressTypeSeq"] + "" == "500052002")  //도로명주소
                    {
                        txtZipcd.Text = GetData(drow, "ZipCode") + "";
                        txtroadAddress.Text = GetData(drow, "AddressBasic") + "";
                        txtAddressDetail.Text = GetData(drow, "AddressDetail") + "";
                    }
                    else if (drow["SMAddressTypeSeq"] + "" == "500052003")  //영문주소
                    {
                        txtroadAddressEnglish.Text = GetData(drow, "AddressBasic") + "";
                        txtAddressDetailEng.Text = GetData(drow, "AddressDetail") + "";
                    }
                }
                btnFind.PerformClick();
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

        private void BtnFind_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(@"http://www.metrosoft.co.kr/jusosearch/daumjusosearch.asp?q=" + txtFind.Text, false);  //  검색어 전달...
            //IIS설정 못함
            //webBrowser1.Navigate(@"http://20.194.52.25:8100/haesungHASP/jusosearch/daumjusosearch.asp?q=" + txtFind.Text, false);  //  검색어 전달...
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Genuine.Standard.COM.BisCOMAddress";
                security.methodId = "Save";
                security.companySeq = security.companySeq;
                security.certId = security.certId + "_1";  // securityType = 1 --> ylwhnpsoftgw_1
                security.securityType = 1;
                security.userId = security.userId;

                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("DataBlock1");
                dt.Columns.Add("WorkingTag");
                dt.Columns.Add("IDX_NO");
                dt.Columns.Add("DataSeq");
                dt.Columns.Add("AddressSeq");
                dt.Columns.Add("SMAddressTypeSeq");
                dt.Columns.Add("ZipCode");
                dt.Columns.Add("AddressBasic");
                dt.Columns.Add("AddressDetail");
                dt.Columns.Add("AddressDetail2");
                dt.Columns.Add("UMCountrySeq");
                dt.Columns.Add("City");
                dt.Columns.Add("Region");
                dt.Clear();

                string workingTag = "A";
                if (this.AddressSeq != 0) workingTag = "U";
                DataRow dr = dt.Rows.Add();
                dr["WorkingTag"] = workingTag;
                dr["IDX_NO"] = 1;
                dr["DataSeq"] = 1;
                dr["AddressSeq"] = this.AddressSeq;
                dr["SMAddressTypeSeq"] = "500052001";  //지번주소
                dr["ZipCode"] = txtZipcd.Text;
                dr["AddressBasic"] = txtjibunAddress.Text;
                dr["AddressDetail"] = txtAddressDetail.Text;
                dr["AddressDetail2"] = "";
                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타없음");
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
                this.AddressSeq = Utils.ToInt(yds.Tables[0].Rows[0]["AddressSeq"]);
                this.AddressText = txtroadAddress.Text + " " + txtAddressDetail.Text;

                dt.Rows.Clear();
                workingTag = "U";
                dr = dt.Rows.Add();
                dr["WorkingTag"] = workingTag;
                dr["IDX_NO"] = 1;
                dr["DataSeq"] = 1;
                dr["AddressSeq"] = this.AddressSeq;
                dr["SMAddressTypeSeq"] = "500052002";  //도로명주소
                dr["ZipCode"] = txtZipcd.Text;
                dr["AddressBasic"] = txtroadAddress.Text;
                dr["AddressDetail"] = txtAddressDetail.Text;
                dr["AddressDetail2"] = "";
                dr = dt.Rows.Add();
                dr["WorkingTag"] = workingTag;
                dr["IDX_NO"] = 2;
                dr["DataSeq"] = 2;
                dr["AddressSeq"] = this.AddressSeq;
                dr["SMAddressTypeSeq"] = "500052003";  //영문주소
                dr["ZipCode"] = txtZipcd.Text;
                dr["AddressBasic"] = txtroadAddressEnglish.Text;
                dr["AddressDetail"] = txtAddressDetailEng.Text;
                dr["AddressDetail2"] = "";

                yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds == null)
                {
                    MessageBox.Show("데이타없음");
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

            this.Address = new ClsAddress();
            this.Address.Zipcd = txtZipcd.Text;
            this.Address.RoadAddress = txtroadAddress.Text;
            this.Address.JibunAddress = txtjibunAddress.Text;
            this.Address.RoadAddressEnglish = txtroadAddressEnglish.Text;
            this.Address.AddressDetail = txtAddressDetail.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TxtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            btnFind.PerformClick();
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            if (webBrowser1.DocumentTitle == "선택")
            {
                //document.getElementById('zipcode').value = data.zonecode; //5자리 새우편번호 사용
                //document.getElementById('addr1').value = fullRoadAddr; // 주소
                //document.getElementById('buildingCode').value = data.buildingCode; // 건물관리번호
                //document.getElementById('buildingName').value = data.buildingName; // 건물명
                //document.getElementById('sido').value = data.sido; // 시도
                //document.getElementById('sigungu').value = data.sigungu; // 시군구
                //document.getElementById('bcode').value = data.bcode; // 법정동/법정리 코드
                //document.getElementById('bname').value = data.bname; // 법정동/법정리 이름
                //document.getElementById('apartment').value = data.apartment; // 공동주택여부
                //document.getElementById('roadAddressEnglish').value = data.roadAddressEnglish; // 영문도로명주소
                //document.getElementById('jibunAddressEnglish').value = data.jibunAddressEnglish; // 영문지번주소
                //document.getElementById('jibunAddress').value = data.jibunAddress; // 지번주소               
                //document.getElementById('roadAddress').value = data.roadAddress; // 도로명주소
                txtZipcd.Text = webBrowser1.Document.GetElementById("zipcode").GetAttribute("value");
                txtroadAddress.Text = webBrowser1.Document.GetElementById("roadAddress").GetAttribute("value");
                txtjibunAddress.Text = webBrowser1.Document.GetElementById("jibunAddress").GetAttribute("value");
                txtroadAddressEnglish.Text = webBrowser1.Document.GetElementById("roadAddressEnglish").GetAttribute("value");
                if (txtjibunAddress.Text == "") txtjibunAddress.Text = txtroadAddress.Text;
                //txtAddr1.Text = webBrowser1.Document.GetElementById("addr1").GetAttribute("value");
                string dong = webBrowser1.Document.GetElementById("bname").GetAttribute("value");
                string buildingName = webBrowser1.Document.GetElementById("buildingName").GetAttribute("value");
                txtAddressDetail.Text = "(" + dong + (buildingName != "" ? ", " + buildingName : "") + ")";
                this.AddressText = txtroadAddress.Text + " " + txtAddressDetail.Text;
                txtAddressDetail.Focus();
            }
            else if (webBrowser1.DocumentTitle == "취소")
            {
            }
        }

        private void ClearAll()
        {
            txtZipcd.Text = "";
            txtroadAddress.Text = "";
            txtjibunAddress.Text = "";
            txtroadAddressEnglish.Text = "";
            txtAddressDetail.Text = "";
            txtAddressDetailEng.Text = "";
            this.AddressText = txtroadAddress.Text + " " + txtAddressDetail.Text;
        }

        private object GetData(DataRow dr, string fld)
        {
            if (dr == null || !dr.Table.Columns.Contains(fld)) return "";
            return dr[fld];
        }
    }
}
