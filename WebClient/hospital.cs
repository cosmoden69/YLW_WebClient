using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Collections.Generic;


namespace YLW_WebClient.CAA
{
    public partial class hospital : UserControl
    {
        private bool change = true;
        public hospital()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
        private void hospital_Load(object sender, EventArgs e)
        {
            pan_hide_10.Visible = false;
            pan_hide_09.Visible = false;
            pan_hide_08.Visible = false;
            pan_hide_07.Visible = false;
            pan_hide_06.Visible = false;
            pan_hide_05.Visible = false;
            pan_hide_04.Visible = false;
            pan_hide_03.Visible = false;
            pan_hide_02.Visible = false;
            

            dgv_back_01.Rows.Add(10);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < dgv_back_01.ColumnCount; j++)
                    dgv_back_01.Rows[i].Cells[j].Value = "";

                if (i == 0)
                    dgv_back_01.Rows[i].Cells["C12"].Value = "Y";
                else
                    dgv_back_01.Rows[i].Cells["C12"].Value = "N";
            }
        }

        private void Text_Change(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            dgv_back_01.Rows[int.Parse(txt.Tag.ToString())].Cells[txt.TabIndex].Value = txt.Text;
            dgv_back_01.Rows[int.Parse(txt.Tag.ToString())].Cells["C12"].Value = "Y";

            if (txt.TabIndex == 10 && !(string.IsNullOrEmpty(txt.Text)))
                pan_show(int.Parse(txt.Tag.ToString()) + 1, true);
        }

        private void Date_Change(object sender, EventArgs e)
        {
            DevComponents.Editors.DateTimeAdv.DateTimeInput dat = (DevComponents.Editors.DateTimeAdv.DateTimeInput)sender;

            dgv_back_01.Rows[int.Parse(dat.Tag.ToString())].Cells[dat.TabIndex].Value = dat.Value;
            dgv_back_01.Rows[int.Parse(dat.Tag.ToString())].Cells["C12"].Value = "Y";

            if (dat.Name == "no01_01")
                no01_07.ValueObject = dat.Value;
            else if (dat.Name == "no02_01")
                no02_07.ValueObject = dat.Value;
            else if (dat.Name == "no03_01")
                no03_07.ValueObject = dat.Value;
            else if (dat.Name == "no04_01")
                no04_07.ValueObject = dat.Value;
            else if (dat.Name == "no05_01")
                no05_07.ValueObject = dat.Value;
            else if (dat.Name == "no06_01")
                no06_07.ValueObject = dat.Value;
            else if (dat.Name == "no07_01")
                no07_07.ValueObject = dat.Value;
            else if (dat.Name == "no08_01")
                no08_07.ValueObject = dat.Value;
            else if (dat.Name == "no09_01")
                no09_07.ValueObject = dat.Value;
            else if (dat.Name == "no10_01")
                no10_07.ValueObject = dat.Value;
            //else if (dat.Name == "no01_07")
            //    no01_01.ValueObject = dat.Value;
            //else if (dat.Name == "no02_07")
            //    no02_01.ValueObject = dat.Value;
            //else if (dat.Name == "no03_07")
            //    no03_01.ValueObject = dat.Value;
            //else if (dat.Name == "no04_07")
            //    no04_01.ValueObject = dat.Value;
            //else if (dat.Name == "no05_07")
            //    no05_01.ValueObject = dat.Value;
            //else if (dat.Name == "no06_07")
            //    no06_01.ValueObject = dat.Value;
            //else if (dat.Name == "no07_07")
            //    no07_01.ValueObject = dat.Value;
            //else if (dat.Name == "no08_07")
            //    no08_01.ValueObject = dat.Value;
            //else if (dat.Name == "no09_07")
            //    no09_01.ValueObject = dat.Value;
            //else if (dat.Name == "no10_07")
            //    no10_01.ValueObject = dat.Value;
        }

        private void pan_show(int pan_number, bool pan_bool)
        {
            if (change)
            {
                if      (pan_number == 1)
                {
                    if (!pan_hide_02.Visible)
                        Height = Height + 176;
                    pan_hide_02.Visible = pan_bool;
                }
                else if (pan_number == 2)
                {
                    if (!pan_hide_03.Visible)
                        Height = Height + 176;
                    pan_hide_03.Visible = pan_bool;
                }
                else if (pan_number == 3)
                {
                    if (!pan_hide_04.Visible)
                        Height = Height + 176;
                    pan_hide_04.Visible = pan_bool;
                }
                else if (pan_number == 4)
                {
                    if (!pan_hide_05.Visible)
                        Height = Height + 176;
                    pan_hide_05.Visible = pan_bool;
                }
                else if (pan_number == 5)
                {
                    if (!pan_hide_06.Visible)
                        Height = Height + 176;
                    pan_hide_06.Visible = pan_bool;
                }
                else if (pan_number == 6)
                {
                    if (!pan_hide_07.Visible)
                        Height = Height + 176;
                    pan_hide_07.Visible = pan_bool;
                }
                else if (pan_number == 7)
                {
                    if (!pan_hide_08.Visible)
                        Height = Height + 176;
                    pan_hide_08.Visible = pan_bool;
                }
                else if (pan_number == 8)
                {
                    if (!pan_hide_09.Visible)
                        Height = Height + 176;
                    pan_hide_09.Visible = pan_bool;
                }
                else if (pan_number == 9)
                {
                    if (!pan_hide_10.Visible)
                        Height = Height + 176;
                    pan_hide_10.Visible = pan_bool;
                }

                if (pan_number < 9)
                    dgv_back_01.Rows[pan_number].Cells["C12"].Value = "Y";
            }
        }
        

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonX btn = (ButtonX)sender;
            setpanel(int.Parse(btn.Tag.ToString()));
        }

        private void setpanel(int pan_number)
        {
            Height = Height - 176;

            change = false;

            dgv_back_01.Rows.Remove(dgv_back_01.Rows[pan_number]);

            dgv_back_01.Rows.Add(1);

            for (int i = 0; i < dgv_back_01.ColumnCount; i++)
            {
                dgv_back_01.Rows[dgv_back_01.RowCount - 1].Cells[i].Value = string.Empty;
            }

            dgv_back_01.Rows[dgv_back_01.RowCount - 1].Cells["C12"].Value = "N";

            for (int a = 1; a < dgv_back_01.RowCount; a++)
            {
                string yn = dgv_back_01.Rows[a].Cells["C12"].Value.ToString();

                if (a == 1)
                {
                    no02_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no02_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no02_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no02_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no02_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no02_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no02_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no02_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no02_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no02_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no02_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_02.Visible = false;
                }
                else if (a == 2)
                {
                    no03_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no03_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no03_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no03_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no03_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no03_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no03_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no03_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no03_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no03_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no03_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_03.Visible = false;
                }
                else if (a == 3)
                {
                    no04_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no04_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no04_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no04_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no04_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no04_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no04_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no04_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no04_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no04_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no04_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_04.Visible = false;
                }
                else if (a == 4)
                {
                    no05_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no05_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no05_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no05_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no05_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no05_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no05_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no05_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no05_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no05_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no05_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_05.Visible = false;
                }
                else if (a == 5)
                {
                    no06_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no06_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no06_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no06_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no06_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no06_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no06_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no06_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no06_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no06_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no06_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_06.Visible = false;
                }
                else if (a == 6)
                {
                    no07_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no07_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no07_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no07_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no07_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no07_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no07_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no07_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no07_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no07_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no07_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_07.Visible = false;
                }
                else if (a == 7)
                {
                    no08_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no08_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no08_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no08_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no08_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no08_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no08_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no08_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no08_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no08_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no08_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_08.Visible = false;
                }
                else if (a == 8)
                {
                    no09_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no09_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no09_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no09_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no09_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no09_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no09_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no09_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no09_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no09_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no09_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_09.Visible = false;
                }
                else if (a == 9)
                {
                    no10_01.Text = dgv_back_01.Rows[a].Cells["C01"].Value.ToString();
                    no10_02.Text = dgv_back_01.Rows[a].Cells["C02"].Value.ToString();
                    no10_03.Text = dgv_back_01.Rows[a].Cells["C03"].Value.ToString();
                    no10_04.Text = dgv_back_01.Rows[a].Cells["C04"].Value.ToString();
                    no10_05.Text = dgv_back_01.Rows[a].Cells["C05"].Value.ToString();
                    no10_06.Text = dgv_back_01.Rows[a].Cells["C06"].Value.ToString();
                    no10_07.Text = dgv_back_01.Rows[a].Cells["C07"].Value.ToString();
                    no10_08.Text = dgv_back_01.Rows[a].Cells["C08"].Value.ToString();
                    no10_09.Text = dgv_back_01.Rows[a].Cells["C09"].Value.ToString();
                    no10_10.Text = dgv_back_01.Rows[a].Cells["C10"].Value.ToString();
                    no10_11.Text = dgv_back_01.Rows[a].Cells["C11"].Value.ToString();

                    if (yn == "N") pan_hide_10.Visible = false;
                }

                dgv_back_01.Rows[a].Cells["C12"].Value = yn;
            }
            change = true;
        }

        public void PanelRefresh()
        {
            Height = 199;

            for (int i = 1; i < dgv_back_01.Rows.Count; i++)
            {
                string yn = dgv_back_01.Rows[i].Cells["C12"].Value.ToString();
                bool pan_bool = (yn == "N" ? false : true);

                if (i == 1)
                {
                    pan_hide_02.Visible = pan_bool;
                    if (pan_hide_02.Visible)
                        Height = Height + 176;
                }
                else if (i == 2)
                {
                    pan_hide_03.Visible = pan_bool;
                    if (pan_hide_03.Visible)
                        Height = Height + 176;
                }
                else if (i == 3)
                {
                    pan_hide_04.Visible = pan_bool;
                    if (pan_hide_04.Visible)
                        Height = Height + 176;
                }
                else if (i == 4)
                {
                    pan_hide_05.Visible = pan_bool;
                    if (pan_hide_05.Visible)
                        Height = Height + 176;
                }
                else if (i == 5)
                {
                    pan_hide_06.Visible = pan_bool;
                    if (pan_hide_06.Visible)
                        Height = Height + 176;
                }
                else if (i == 6)
                {
                    pan_hide_07.Visible = pan_bool;
                    if (pan_hide_07.Visible)
                        Height = Height + 176;
                }
                else if (i == 7)
                {
                    pan_hide_08.Visible = pan_bool;
                    if (pan_hide_08.Visible)
                        Height = Height + 176;
                }
                else if (i == 8)
                {
                    pan_hide_09.Visible = pan_bool;
                    if (pan_hide_09.Visible)
                        Height = Height + 176;
                }
                else if (i == 9)
                {
                    pan_hide_10.Visible = pan_bool;
                    if (pan_hide_10.Visible)
                        Height = Height + 176;
                }
            }
        }
    }
}
