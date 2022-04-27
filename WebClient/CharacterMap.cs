using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;

namespace YLW_WebClient.CAA
{
    public partial class CharacterMap : Form
    {
        ExtendedRichTextBox.RichTextBoxPrintCtrl rtbDoc = null;

        public CharacterMap()
        {
            InitializeComponent();

            this.KeyDown += CharacterMap_KeyDown;
        }

        public void SetRtbDoc(ExtendedRichTextBox.RichTextBoxPrintCtrl rtb)
        {
            this.rtbDoc = rtb;
        }

        private void CharacterMap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Pan_Moon_1_Click(object sender, EventArgs e)
        {
            PanelEx pan = (PanelEx)sender;
            rtbDoc.SelectedText = pan.Text;
            rtbDoc.Focus();
        }
    }
}
