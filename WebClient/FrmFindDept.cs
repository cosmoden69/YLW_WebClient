using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YLWService;
using YLWService.Extensions;

namespace YLW_WebClient.CAA
{
    public class FrmFindDept : Form
    {
        private Label label2;

        public List<FieldObject> ReturnFields
        {
            get;
            set;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(563, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 34);
            this.btnOK.TabIndex = 2;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtFind);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 55);
            this.panel1.TabIndex = 5;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(643, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 33);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(622, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "%";
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(104, 17);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(512, 21);
            this.txtFind.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "부서명";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(730, 42);
            this.panel2.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(640, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(730, 345);
            this.panel3.TabIndex = 6;
            // 
            // dgList
            // 
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgList.Location = new System.Drawing.Point(0, 0);
            this.dgList.Name = "dgList";
            this.dgList.RowTemplate.Height = 23;
            this.dgList.Size = new System.Drawing.Size(730, 345);
            this.dgList.TabIndex = 0;
            // 
            // FrmFindDept
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(730, 442);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmFindDept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부서찾기";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.ResumeLayout(false);

        }

        private Panel panel3;
        private DataGridView dgList;
        private Button btnQuery;
        private Label label3;
        private TextBox txtFind;
        private Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;

        #endregion

        public FrmFindDept(string kword)
        {
            InitializeComponent();

            this.dgList.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect; //.FullRowSelect;
            this.dgList.AllowUserToDeleteRows = false;
            this.dgList.AllowUserToAddRows = false;
            this.dgList.ReadOnly = true;

            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            this.dgList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellDoubleClick);
            this.Load += new System.EventHandler(this.Form_Load);

            this.txtFind.Text = kword;

            this.dgList.AddColumn("TEXTBOX", "DeptSeq", "부서코드", 100);
            this.dgList.AddColumn("TEXTBOX", "DeptName", "부서명", 200);
            this.dgList.AddColumn("TEXTBOX", "rowNum", "줄번호", 10, false);
        }

        public static DataRow QueryFindOne(string deptName)
        {
            try
            {
                string strSql = "";
                strSql += @" SELECT A.DeptName AS DeptName ";  /* 소속부서   */
                strSql += @"     ,A.DeptSeq AS DeptSeq ";    /* 소속부서(코드) */
                strSql += @"     ,ROW_NUMBER() OVER(ORDER BY A.DeptName) AS rowNum ";
                strSql += @" FROM _TDADept AS A WITH(NOLOCK) ";
                strSql += @" WHERE A.CompanySeq = @CompanySeq ";
                strSql += @" AND  (A.DeptName LIKE @Keyword OR CONVERT(NVARCHAR(MAX),A.DeptSeq) LIKE REPLACE(@Keyword, '%','')) ";
                strSql += @" AND   A.EndDate >= CONVERT(NCHAR(8), GETDATE(), 112) ";
                strSql += @" FOR JSON PATH ";

                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", YLWService.MTRServiceModule.SecurityJson.companySeq));
                lstPara.Add(new SqlParameter("@Keyword", deptName + "%"));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                strSql = strSql.Replace("\r\n", "");
                DataTable dt = MTRServiceModule.GetMTRServiceDataTable(YLWService.MTRServiceModule.SecurityJson.companySeq, strSql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    return dt.Rows[0];
                }
                return null;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }

        private void Query()
        {
            try
            {
                string strSql = "";
                strSql += @" SELECT A.DeptName AS DeptName ";  /* 소속부서   */
                strSql += @"     ,A.DeptSeq AS DeptSeq ";    /* 소속부서(코드) */
                strSql += @"     ,ROW_NUMBER() OVER(ORDER BY A.DeptName) AS rowNum ";
                strSql += @" FROM _TDADept AS A WITH(NOLOCK) ";
                strSql += @" WHERE A.CompanySeq = @CompanySeq ";
                strSql += @" AND  (A.DeptName LIKE @Keyword OR CONVERT(NVARCHAR(MAX),A.DeptSeq) LIKE REPLACE(@Keyword, '%','')) ";
                strSql += @" AND   A.EndDate >= CONVERT(NCHAR(8), GETDATE(), 112) ";
                strSql += @" FOR JSON PATH ";

                List<IDbDataParameter> lstPara = new List<IDbDataParameter>();
                lstPara.Clear();
                lstPara.Add(new SqlParameter("@CompanySeq", YLWService.MTRServiceModule.SecurityJson.companySeq));
                lstPara.Add(new SqlParameter("@Keyword", txtFind.Text + "%"));
                strSql = Utils.GetSQL(strSql, lstPara.ToArray());
                strSql = strSql.Replace("\r\n", "");
                DataTable dt = MTRServiceModule.GetMTRServiceDataTable(YLWService.MTRServiceModule.SecurityJson.companySeq, strSql);
                if (dt == null)
                {
                    DataTable dtr = (DataTable)this.dgList.DataSource;
                    if (dtr != null) dtr.Rows.Clear();
                    return;
                }
                this.dgList.DataSource = dt;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.dgList.CurrentRow != null)
            {
                this.ReturnFields = new List<FieldObject>();
                this.ReturnFields.Add(new FieldObject("DeptName", "", this.dgList["DeptName", this.dgList.CurrentRow.Index].Value));
                this.ReturnFields.Add(new FieldObject("DeptSeq", "", this.dgList["DeptSeq", this.dgList.CurrentRow.Index].Value));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.ReturnFields = null;
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ReturnFields = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        private void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (this.txtFind.Text != "") this.Query();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
            }
        }
    }
}
