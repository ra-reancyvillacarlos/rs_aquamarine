using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class DiscountTypes : Form
    {
        Boolean isnew = false;

        public DiscountTypes()
        {
            InitializeComponent();
        }

        private void DiscountTypes_Load(object sender, EventArgs e)
        {
            form_reset();
            dgv_list.DataSource = get_list();
        }

        private void form_reset()
        {
            txt_code.Enabled = false;
            txt_desc.Enabled = false;
            txt_perc.Enabled = false;
            chk_senior.Enabled = false;

            btn_new.Enabled = true;
            btn_edit.Enabled = false;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void form_new()
        {
            txt_code.Enabled = true;
            txt_desc.Enabled = true;
            txt_perc.Enabled = true;
            chk_senior.Enabled = true;
            txt_code.ReadOnly = false;

            btn_new.Enabled = false;
            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void form_toedit()
        {
            form_reset();
            btn_new.Enabled = false;
            btn_edit.Enabled = true;
            
            form_setreadonly(true);
        }

        private void form_edit()
        {
            form_new();
            txt_code.ReadOnly = true;
            form_setreadonly(false);
        }

        private void form_clear()
        {
            txt_code.Text = "";
            txt_desc.Text = "";
            txt_perc.Text = "";
            chk_senior.Checked = false;
        }

        private void form_setreadonly(Boolean flag)
        {
            txt_desc.ReadOnly = flag;
            txt_perc.ReadOnly = flag;

            if (flag == true)
            {
                chk_senior.Enabled = false;
            }
            else
            {
                chk_senior.Enabled = true;
            }
        }

        private DataTable get_list()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("disctbl", "disc_code, disc_desc, disc1, sen_disc", "", "ORDER BY disc_code ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            form_new();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_edit();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String issenior = "N";

            if (chk_senior.Checked == true)
                issenior = "Y";

            if (isnew == true)
            {
                String value = "'" + txt_code.Text + "','" + txt_desc.Text + "','" + txt_perc.Text + "','" + issenior + "'";

                if(db.InsertOnTable("disctbl", "disc_code, disc_desc, disc1, sen_disc", value))
                {
                    MessageBox.Show("New record added successfully.");
                    
                    form_clear();
                    form_reset();
                }
                else
                {
                    MessageBox.Show("Duplicate entry.");
                }
            }
            else
            {
                String value = "disc_desc='" + txt_desc.Text + "', disc1='" + txt_perc.Text + "', sen_disc='" + issenior + "'";

                if(db.UpdateOnTable("disctbl", value, "disc_code='" + txt_code.Text + "'"))
                {
                    MessageBox.Show("Record updated successfully.");
                    form_clear();
                    form_reset();
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_clear();
            form_reset();
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_toedit();

            int row = dgv_list.CurrentCell.RowIndex;

            String dis_code = dgv_list.Rows[row].Cells[0].Value.ToString().Trim();
            String disc_desc = dgv_list.Rows[row].Cells[1].Value.ToString().Trim();
            String disc1 = dgv_list.Rows[row].Cells[2].Value.ToString().Trim();
            String sen_dis = dgv_list.Rows[row].Cells[3].Value.ToString().Trim();

            txt_code.Text = dis_code;
            txt_desc.Text = disc_desc;
            txt_perc.Text = disc1;

            if (sen_dis == "Y")
                chk_senior.Checked = true;
            else
                chk_senior.Checked = false;
        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = Color.Brown;
            e.CellStyle.SelectionBackColor = Color.GreenYellow;

            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);

                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }
    }
}
