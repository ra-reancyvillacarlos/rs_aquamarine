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
    public partial class m_membership : Form
    {
        //dbSales db;
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean iscallbackfrm = false;
        s_RepairOrder frm_ro = null;
        s_Sales frm_sales = null;
        String _custcode = "";

        public m_membership()
        {
            InitializeComponent();

            gc.load_customer(cbo_customer);
            cbo_cardtype.SelectedIndexChanged += cbo_cardtype_SelectedIndexChanged;

            disp_list();
        }

        public m_membership(s_Sales frm, Boolean iscallback)
        {
            InitializeComponent();

            try
            {
                frm_sales = frm;
                iscallbackfrm = iscallback;

                gc.load_customer(cbo_customer);
                cbo_cardtype.SelectedIndexChanged += cbo_cardtype_SelectedIndexChanged;

                if (frm_sales != null)
                {
                    _custcode = frm_sales.cbo_customer.SelectedValue.ToString();
                    cbo_customer.SelectedValue = _custcode;
                }

                disp_list();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }


        public m_membership(s_RepairOrder frm, Boolean iscallback)
        {
            InitializeComponent();
            

            try
            {
                frm_ro = frm;
                iscallbackfrm = iscallback;

                gc.load_customer(cbo_customer);
                cbo_cardtype.SelectedIndexChanged += cbo_cardtype_SelectedIndexChanged;

                if (frm_ro != null)
                {
                    _custcode = frm_ro.cbo_customer.SelectedValue.ToString();
                    cbo_customer.SelectedValue = frm_ro.cbo_customer.SelectedValue.ToString();
                }


                disp_list();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void cbo_cardtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_cardtype.SelectedIndex = 0;
        }

        private void m_membership_Load(object sender, EventArgs e)
        {
            cbo_cardtype.SelectedIndex = 0;
        }

        private void tpg_info_Click(object sender, EventArgs e)
        {

        }
        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            cbo_cardtype.SelectedIndex = -1;
            txt_orno.Text = "";
            txt_points.Text = "0.00";
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
            thisDatabase db = new thisDatabase();
            String WHERE = "";
           

            if (String.IsNullOrEmpty(_custcode) == false)
            {
                WHERE = " WHERE debt_code = " + db.str_E(_custcode);
            }
            String query = "SELECT ec.cardno, ec.cardtype, to_char(ec.date_reg, 'Month dd, yyyy') AS date_reg, to_char(ec.date_expire, 'Month dd, yyyy') AS date_expire, ec.points, ec.or_reference, d.d_name, ec.debt_code  FROM rssys.ecard ec LEFT JOIN rssys.m06 d ON d.d_code=ec.debt_code " + WHERE + " ORDER BY ec.date_reg ASC";

            DataTable dt = db.QueryBySQLCode("SELECT ec.cardno, ec.cardtype, to_char(ec.date_reg, 'Month dd, yyyy') AS date_reg, to_char(ec.date_expire, 'Month dd, yyyy') AS date_expire, ec.points, ec.or_reference, d.d_name, ec.debt_code  FROM rssys.ecard ec LEFT JOIN rssys.m06 d ON d.d_code=ec.debt_code " + WHERE + " ORDER BY ec.date_reg ASC");
            //db.QueryOnTableWithParams("eCard", "*", WHERE, "ORDER BY cardno ASC");
            //dt = db.QueryBySQLCode("select * from rssys.ecard");

            //thisDatabase db = new thisDatabase();
            //DataTable dt = ;

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["d_code"].Value = dt.Rows[r]["cardno"].ToString();
                    row.Cells["d_name"].Value = dt.Rows[r]["d_name"].ToString();
                    row.Cells["d_cardtype"].Value = dt.Rows[r]["cardtype"].ToString();
                    row.Cells["d_reg"].Value = dt.Rows[r]["date_reg"].ToString();
                    row.Cells["d_expire"].Value = dt.Rows[r]["date_expire"].ToString();
                    row.Cells["d_points"].Value = dt.Rows[r]["points"].ToString();
                    row.Cells["d_or"].Value = dt.Rows[r]["or_reference"].ToString();
                    row.Cells["d_debt"].Value = dt.Rows[r]["debt_code"].ToString();
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
                dtp_registration.Value = DateTime.Parse(dgv_list["d_reg", r].Value.ToString());
                dtp_expiration.Value = DateTime.Parse(dgv_list["d_expire", r].Value.ToString());
                txt_points.Text = dgv_list["d_points", r].Value.ToString();
                txt_orno.Text = dgv_list["d_or", r].Value.ToString();

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            try
            {
                isnew = true;
                tpg_info_enable(true);
                frm_clear();
                goto_tbcntrl_info();

                if (frm_ro != null)
                {
                    cbo_customer.SelectedValue = _custcode;

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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
            MessageBox.Show("Currently Disabled");
            //thisDatabase db = new thisDatabase();
            //int r;

            //if (dgv_list.Rows.Count > 1)
            //{
            //    r = dgv_list.CurrentRow.Index;

            //    if (db.UpdateOnTable("brand", "cancel='Y'", "brd_code='" + dgv_list["ID", r].Value.ToString() + "'"))
            //    {
            //        disp_list();
            //        goto_tbcntrl_list();
            //        tpg_info_enable(false);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed on deleting.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No rows selected.");
            //}
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, card_type, or_ref, debt_code, pntstr = "";
            DateTime date_reg, date_expire;
            int points = 0;
            String col = "", val = "", add_col = "", add_val = "";


            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the cusomer.");
            }
            else
            {
                code = txt_code.Text;
                card_type = "D";
                date_reg = dtp_registration.Value.Date;
                date_expire = dtp_expiration.Value.Date;
                or_ref = txt_orno.Text;
                debt_code = cbo_customer.SelectedValue.ToString();
                pntstr = txt_points.Text;

                if (String.IsNullOrEmpty(pntstr) == false)
                {
                    points = gm.toInt(pntstr);
                }

                if (isnew)
                {
                    try
                    {
                        col = "cardno,cardtype,date_reg,date_expire,points,or_reference,debt_code";
                        val = "'" + code + "','D','" + date_reg + "','" + date_expire + "','" + points.ToString("0.00") + "','" + or_ref + "','" + cbo_customer.SelectedValue.ToString() + "'";

                        if (db.InsertOnTable("ecard", col, val))
                        {
                            success = true;
                            //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                        }
                        else
                        {
                            success = false;
                            db.DeleteOnTable("ecard", "cardno='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
                else
                {
                    col = "date_reg='" + date_reg + "',date_expire='" + date_expire + "',points='" + points.ToString("0.00") + "',or_reference='" + txt_orno.Text + "',debt_code = '" + debt_code + "'";

                    if (db.UpdateOnTable("ecard", col, "cardno='" + code + "'"))
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

        private void btn_back_Click_1(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void txt_points_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void disp_itemlist()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            String mcardid = txt_code.Text;

            try
            {
                dgv_itemlist.Rows.Clear();
            }
            catch { }

            try
            {
                if (String.IsNullOrEmpty(mcardid) == false)
                {
                    dgv_itemlist.DataSource = db.QueryBySQLCode("SELECT  o.out_desc, oh.ord_code, oh.ord_date, oh.reference, oh.car_plate, oh.car_vin_num, oh.car_modelFROM rssys.orhdr oh LEFT JOIN rssys.outlet o ON o.out_code=oh.out_code WHERE mcardid=" + db.str_E(mcardid) + " ORDER BY oh.ord_date, oh.t_time;");
                }
            }
            catch { }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4018);
            rpt.ShowDialog();
        }

        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            int r = -1;
            try { r = dgv_list.CurrentRow.Index; }
            catch { }

            if (r != -1)
            {
                if (!String.IsNullOrEmpty(dgv_list["d_code", r].Value.ToString()))
                {
                    String ecode = dgv_list["d_code", r].Value.ToString();

                    if (frm_ro != null)
                    {
                        frm_ro.txt_servex.Text = ecode;
                    }

                    if (frm_sales != null)
                    {
                        frm_sales.txt_servex.Text = ecode;
                    }

                    this.Close();
                }
            }
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

        private void dgv_itemlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_itemlist();
        }
    }
}
