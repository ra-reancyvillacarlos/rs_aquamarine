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
    public partial class addGroup : Form
    {
        Boolean isnew = false;

        public addGroup()
        {
            InitializeComponent();
        }

        private void addGroup_Load(object sender, EventArgs e)
        {
            form_reset();

            load_comp();

            txt_code.Hide();

            cbo_srh_company.SelectedIndex = -1;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String sql;
            String schema = db.get_schema();
            String grpname = txt_srh_grp.Text;
            String contact = txt_srh_contact.Text;
            String comp = null;
            String WHERE = " WHERE ";

            if (cbo_srh_company.SelectedIndex != -1)
            {
                comp = cbo_srh_company.SelectedValue.ToString();
                WHERE = WHERE + "rg.comp_code='" + comp + "'";
            }
            if (String.IsNullOrEmpty(grpname) == false)
            {
                if (WHERE != " WHERE ")
                    WHERE = WHERE + " AND ";

                WHERE = WHERE + "rg.group ILIKE '%" + grpname + "%'";
            }
            if (String.IsNullOrEmpty(contact) == false)
            {
                if (WHERE != " WHERE ")
                    WHERE = WHERE + " AND ";

                WHERE = WHERE + "rg.contact ILIKE '%" + contact + "%'";
            }

            if (WHERE == " WHERE ")
                WHERE = "";

            sql = "SELECT rg.grp_code, rg.group, rg.contact, rg.tel_num, rg.email, rg.address, c.comp_name FROM " + schema + ".resgrp rg INNER JOIN " + schema + ".company c ON  rg.comp_code=c.comp_code" + WHERE;

            dgv_search.DataSource = db.QueryBySQLCode(sql);
        }

        private void load_comp()
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

            cbo_srh_company.DataSource = dt;
            cbo_srh_company.DisplayMember = "comp_name";
            cbo_srh_company.ValueMember = "comp_code";

            cbo_company.DataSource = dt;
            cbo_company.DisplayMember = "comp_name";
            cbo_company.ValueMember = "comp_code";
        }

        private void dgv_search_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // dgv_search[0, e.RowIndex].Value.ToString(); 'notep 
            try
            {
                if (dgv_search.SelectedRows.Count > 0)
                {
                    int row = dgv_search.CurrentCell.RowIndex;
                    String grpcode = dgv_search.Rows[row].Cells[0].Value.ToString().Trim();
                    String grpname = dgv_search.Rows[row].Cells[1].Value.ToString().Trim();
                    String contact = dgv_search.Rows[row].Cells[2].Value.ToString().Trim();
                    String tel = dgv_search.Rows[row].Cells[3].Value.ToString().Trim();
                    String email = dgv_search.Rows[row].Cells[4].Value.ToString().Trim();
                    String addr = dgv_search.Rows[row].Cells[5].Value.ToString().Trim();
                    String compname = dgv_search.Rows[row].Cells[6].Value.ToString().Trim();

                    txt_code.Text = grpcode;
                    txt_grpname.Text = grpname;
                    txt_contact.Text = contact;
                    txt_contactno.Text = tel;
                    txt_email.Text = email;
                    txt_address.Text = addr;
                    cbo_company.Text = compname;

                    form_toedit();
                }
            }
            catch (Exception)
            { }
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

            try
            {
                String pk = db.get_pk("grp_code");

                String value = "'" + pk + "','" + txt_grpname.Text + "','" + txt_contact.Text + "','" + txt_contactno.Text + "','" + txt_email.Text + "', '" + txt_address.Text + "', '" + cbo_company.SelectedValue.ToString() + "'";

                if (isnew == true)
                {

                    if (db.InsertOnTable("resgrp", "grp_code, \"group\", contact, tel_num, email, address, comp_code", value))
                    {
                        String msg = "New record added successfully.";

                        if (db.set_pkincrement("grp_code", pk) == false)
                        {
                            msg = msg + " But, theres a problem in primary key.";
                        }

                        MessageBox.Show(msg);
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
                    String colupd = "\"group\" ='" + txt_grpname.Text + "', contact='" + txt_contact.Text + "', tel_num='" + txt_contactno.Text + "', email='" + txt_email.Text + "', address='" + txt_address.Text + "', comp_code='" + cbo_company.SelectedValue.ToString() + "'";

                    if (db.UpdateOnTable("resgrp", colupd, "grp_code='" + txt_code.Text + "'"))
                    {
                        MessageBox.Show("Record updated successfully.");
                        //form_clear();
                        //form_reset();
                    }
                    else
                    {
                        MessageBox.Show("Record cannot update.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_clear();
            form_reset();
        }

        private void form_reset()
        {
            form_clear();

            pnl_guestinfo1.Enabled = false;

            btn_new.Enabled = true;
            btn_edit.Enabled = false;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void form_new()
        {
            pnl_guestinfo1.Enabled = true;

            btn_new.Enabled = false;
            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void form_toedit()
        {
            btn_new.Enabled = false;
            btn_edit.Enabled = true;
            pnl_guestinfo1.Enabled = true;

            form_setreadonly(true);
        }

        private void form_edit()
        {
            form_new();
            form_setreadonly(false);
        }

        private void form_clear()
        {
            txt_code.Text = "";
            txt_grpname.Text = "";
            txt_contact.Text = "";
            txt_email.Text = "";
            txt_contactno.Text = "";
            txt_address.Text = "";
            cbo_company.SelectedIndex = -1;
        }

        private void form_setreadonly(Boolean flag)
        {
            txt_code.ReadOnly = flag;
            txt_grpname.ReadOnly = flag;
            txt_contact.ReadOnly = flag;
            txt_email.ReadOnly = flag;
            txt_contactno.ReadOnly = flag;
            txt_address.ReadOnly = flag;

            if (flag == true)
            {
                cbo_company.Enabled = false;
            }
            else
            {
                cbo_company.Enabled = true;
            }
        }

        private void btn_save_all_Click(object sender, EventArgs e)
        {
            GlobalClass.gdt = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(String));
            dt.Columns.Add("name", typeof(String));
            dt.Columns.Add("contact", typeof(String));
            dt.Columns.Add("company", typeof(String));
            dt.Columns.Add("contactno", typeof(String));

            dt.Rows.Add(txt_code.Text, txt_grpname.Text, txt_contact.Text, cbo_company.SelectedValue.ToString(), txt_contactno.Text);

            GlobalClass.gdt = dt;

            this.Close();
        }

        private void btn_cancel_all_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_search_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
