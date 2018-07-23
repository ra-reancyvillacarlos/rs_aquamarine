using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class m_company : Form
    {
        dbVehicle db;
        s_RepairOrder _frm_ro = null;
        GlobalMethod gm;
        private s_RepairOrder s_RepairOrder;
        private bool p;
        Boolean iscallbackfrm = false;
        Boolean seltbp = false;
        Boolean isnew = false;


        public m_company()
        {
            InitializeComponent();
            db = new dbVehicle();
            disp_list();
        }
        public m_company(s_RepairOrder frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbVehicle();
            gm = new GlobalMethod();

            _frm_ro = frm;
            iscallbackfrm = iscallback;

            disp_list();
        }


        private void m_company_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);

        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_name.Text = "";
            rtxt_address.Text = "";
            rtxt_remark.Text = "";
            txt_person1.Text = "";
            txt_contact1.Text = "";
            txt_position1.Text = "";
            txt_website.Text = "";
            txt_phone.Text = "";
            txt_fax.Text = "";
        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
            txt_name.Enabled = flag;
        }

        private void clear_dgv()
        {
            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void disp_list()
        {
            DataTable dt = db.get_dealerlist();

            clear_dgv();

            try
            {

                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["d_code"].Value = dt.Rows[r]["comp_code"].ToString();
                    row.Cells["d_name"].Value = dt.Rows[r]["comp_name"].ToString();
                    row.Cells["d_addrs"].Value = dt.Rows[r]["address1"].ToString();
                    row.Cells["d_tel"].Value = dt.Rows[r]["tel_num"].ToString();
                    row.Cells["d_fax"].Value = dt.Rows[r]["fax_num"].ToString();
                    row.Cells["d_website"].Value = dt.Rows[r]["website"].ToString();
                    row.Cells["d_pers"].Value = dt.Rows[r]["cntc_pers1"].ToString();
                    row.Cells["d_pos"].Value = dt.Rows[r]["cntc_pos1"].ToString();
                    row.Cells["d_cntc_no"].Value = dt.Rows[r]["cntc_num1"].ToString();
                    row.Cells["remarks"].Value = dt.Rows[r]["remarks"].ToString();







                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["d_code", r].Value.ToString();
                txt_name.Text = dgv_list["d_name", r].Value.ToString();
                rtxt_address.Text = dgv_list["d_addrs", r].Value.ToString();
                txt_phone.Text = dgv_list["d_tel", r].Value.ToString();
                txt_fax.Text = dgv_list["d_fax", r].Value.ToString();
                txt_website.Text = dgv_list["d_website", r].Value.ToString();
                txt_position1.Text = dgv_list["d_pos", r].Value.ToString();
                txt_contact1.Text = dgv_list["d_cntc_no", r].Value.ToString();
                rtxt_remark.Text = dgv_list["remarks", r].Value.ToString();
                txt_person1.Text = dgv_list["d_pers", r].Value.ToString(); ;

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {

            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["d_code", dgv_list.CurrentRow.Index].Value.ToString()) == false)
            {
                isnew = false;
                tpg_info_enable(true);
                frm_clear();
                disp_info();
                goto_tbcntrl_info();

            }
            else
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            int r;
            String code = "";
            String name = "";
            DialogResult dialogResult;
            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["d_code", r].Value.ToString();
                name = dgv_list["d_name", r].Value.ToString();
                dialogResult = MessageBox.Show("Are you sure you company name " + name + "?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {

                    if (db.DeleteOnTable("company", "comp_code='" + code + "'"))
                    {
                        disp_list();
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                    }
                    else
                    {
                        MessageBox.Show("Failed on deleting.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4012);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String code, desc;
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (txt_code.Text.Length > 6)
            {
                MessageBox.Show("Company ID must be six characters long.");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_name.Text;

                if (isnew)
                {
                    code = db.get_pk("comp_code");
                    //code = txt_code.Text;//db.get_pk("brd_code");
                    col = "comp_code,comp_name,address1,tel_num,fax_num,website,cntc_pers1,cntc_pos1,cntc_num1,remarks,mp_code";
                    val = "'" + code + "','" + desc + "','" + rtxt_address.Text + "','" + txt_phone.Text + "','" + txt_fax.Text + "','" + txt_website.Text + "','" + txt_person1.Text + "','" + txt_position1.Text + "','" + txt_contact1.Text + "','" + rtxt_remark.Text + "','000'";

                    if (db.InsertOnTable("company", col, val))
                    {
                        success = true;
                        db.set_pkm99("comp_code", db.get_nextincrementlimitchar(code, code.Length));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("company", "comp_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "comp_name='" + desc + "',address1='" + rtxt_address.Text + "',tel_num='" + txt_phone.Text + "',fax_num='" + txt_fax.Text + "',website='" + txt_website.Text + "',cntc_pers1='" + txt_person1.Text + "',cntc_pos1='" + txt_position1.Text + "',cntc_num1='" + txt_contact1.Text + "',remarks='" + rtxt_remark.Text + "'";

                    if (db.UpdateOnTable("company", col, "comp_code='" + code + "'"))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void m_company_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            int r = 0;
            String dealercode = "", dealername = "";

            try
            {
                if (iscallbackfrm)
                {


                    if (_frm_ro != null)
                    {
                        r = dgv_list.CurrentRow.Index;

                        dealercode = dgv_list[0, r].Value.ToString();
                        dealername = dgv_list[1, r].Value.ToString();

                        _frm_ro.set_dealer_frm(dealercode, dealername);
                        this.Close();
                    }
                }
            }
            catch (Exception) { }
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
