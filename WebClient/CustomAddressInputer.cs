using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YLW_WebClient
{
    public partial class CustomAddressInputer : UserControl
    {
        private bool bChange = false;
        public int AddressSeq { get; set; }

        public override string Text 
        {
            get { return this.txtAddr.Text; }
            set { this.txtAddr.Text = value; }
        }

        public CustomAddressInputer()
        {
            InitializeComponent();

            this.txtAddr.KeyDown += TxtAddr_KeyDown;
            this.txtAddr.TextChanged += TxtAddr_TextChanged;
            this.txtAddr.Leave += TxtAddr_Leave;
            this.txtAddr.Enter += TxtAddr_Enter;
            this.btnFind.Click += BtnFind_Click;
        }

        public void SetReadOnly(bool rdOnly)
        {
            txtAddr.ReadOnly = rdOnly;
            if (!rdOnly) txtAddr.FocusHighlightEnabled = true;
            else txtAddr.FocusHighlightEnabled = false;
        }

        private void TxtAddr_Enter(object sender, EventArgs e)
        {
            bChange = false;
        }

        private void TxtAddr_Leave(object sender, EventArgs e)
        {
            if (bChange)
            {
                btnFind.PerformClick();
            }
        }

        private void TxtAddr_TextChanged(object sender, EventArgs e)
        {
            bChange = true;
        }

        private void TxtAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            frmFindZip frm = new frmFindZip(AddressSeq, txtAddr.Text);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                this.AddressSeq = frm.AddressSeq;
                this.txtAddr.Text = frm.AddressText;
            }
        }
    }
}
