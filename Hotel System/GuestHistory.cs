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
    public partial class GuestHistory : Form
    {
        public GuestHistory()
        {
            InitializeComponent();
        }

        private void GuestHistory_Load(object sender, EventArgs e)
        {
            
            load_compcbo();
        }

        private void load_compcbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

                cbo_company.DataSource = dt;
                cbo_company.DisplayMember = "comp_name";
                cbo_company.ValueMember = "comp_code";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String reg_num = txt_folio.Text;
            String full_name = txt_gname.Text;
            String company = "";

            if (cbo_company.SelectedIndex > -1)
            {
                company = cbo_company.SelectedValue.ToString();
            }

            dgv_guestlist.DataSource = db.search_guesthist(reg_num, full_name, company);
            lbl_noofguestlist.Text = (dgv_guestlist.Rows.Count - 1).ToString();
        }

        private void dtp_inhousedt_ValueChanged(object sender, EventArgs e)
        {
            disp_inhouselist();
        }

        private void disp_guestlist()
        {
            thisDatabase db = new thisDatabase();            
        }

        private void disp_inhouselist()
        {
            thisDatabase db = new thisDatabase();

            dgv_guestlist.DataSource = db.get_inhouseguest_ondate(dtp_inhousedt.Value.ToString("yyyy-MM-dd"));
            lbl_noofguestlist.Text = (dgv_guestlist.Rows.Count-1).ToString();
        }

        private void btn_viewfolio_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_folio.CurrentRow.Index;
                String fol_code = dgv_folio.Rows[row].Cells[0].Value.ToString();
                disp_guestinfo(fol_code);
                disp_chghist(fol_code);

                tbcntrl.SelectedTab = tpg_2;
                tpg_2.Show();
            }
            catch (Exception) { }
        }

        private void disp_guestinfo(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            Double db_amt = 0.00;
            DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));
            dt = db.get_guest_hist_selected(reg_num);
            Double val = 0.00;

            foreach (DataRow row in dt.Rows)
            {
                lbl_gfolio.Text = row["reg_num"].ToString();
                lbl_arrdate.Text = Convert.ToDateTime(row["arr_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_depdate.Text = Convert.ToDateTime(row["dep_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_company.Text = "";
                lbl_username.Text = row["user_id"].ToString() + " " + Convert.ToDateTime(row["t_date"].ToString()).ToString("MM/dd/yyyy") + " " + row["t_time"].ToString();
                lbl_rm.Text = row["rom_code"].ToString();
                lbl_grossrate.Text = Convert.ToDouble(db.get_roomrateamt(row["rate_code"].ToString(), row["typ_code"].ToString(), int.Parse(row["occ_type"].ToString())).ToString()).ToString("0.00");
                lbl_rate.Text = Math.Round(Convert.ToDecimal((Convert.ToDouble(row["rom_rate"].ToString()) + Convert.ToDouble(row["govt_tax"].ToString()) + Convert.ToDouble(row["serv_chg"].ToString()))), 1).ToString("0.00");
                val = (Convert.ToDateTime(lbl_depdate.Text) - Convert.ToDateTime(lbl_arrdate.Text)).TotalDays;
                
                lbl_noofnights.Text = val.ToString();
                lbl_rmtype.Text = row["typ_code"].ToString();
                lbl_pax.Text = row["occ_type"].ToString();
                lbl_disc.Text = row["disc_pct"].ToString();
                rtxt_remarks.Text = row["remarks"].ToString() + " " + row["bill_info"].ToString() + " " + row["rm_features"].ToString();
                rtxt_gname.Text = row["full_name"].ToString();
                lbl_address.Text = row["address"].ToString();
                lbl_company.Text = row["company"].ToString();

                if (curdate >= Convert.ToDateTime(row["dep_date"].ToString()))
                {
                    //lbl_depdate.Font = new 
                }
            }
        }

        private void disp_chghist(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double bal_amnt = 0.00;
            Double runbal = 0.00;
            DataTable dt = new DataTable();
            dt = db.get_guest_chargehist(reg_num);

            dgv_gfolio_sel.Rows.Clear();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgv_row = (DataGridViewRow)dgv_gfolio_sel.Rows[0].Clone();

                    dgv_row.Cells[0].Value = Convert.ToDateTime(row[0].ToString()).ToString("MM/dd/yy");
                    dgv_row.Cells[1].Value = row[1].ToString();
                    dgv_row.Cells[2].Value = row[2].ToString();
                    dgv_row.Cells[3].Value = row[3].ToString();
                    dgv_row.Cells[4].Value = row[4].ToString();
                    dgv_row.Cells[5].Value = gm.get_amount_negbracket(Convert.ToDouble(row[5].ToString()));
                    
                    runbal = runbal + (Convert.ToDouble(row[5].ToString()));

                    dgv_row.Cells[6].Value = gm.get_amount_negbracket(runbal);                    
                    dgv_row.Cells[7].Value = row[6].ToString();
                    dgv_row.Cells[8].Value = row[7].ToString();
                    dgv_row.Cells[9].Value = row[8].ToString();
                    dgv_row.Cells[10].Value = row[9].ToString();
                    dgv_row.Cells[11].Value = row[10].ToString();
                    dgv_row.Cells[12].Value = row[11].ToString();
                    dgv_row.Cells[13].Value = row[12].ToString();

                    dgv_gfolio_sel.Rows.Add(dgv_row);
                }
            }
            catch (Exception) { }
            bal_amnt = db.get_guest_charges_total(reg_num, false);

            if (bal_amnt > 0)
            {
                lbl_balance_sel.Text = bal_amnt.ToString("0.00");
                lbl_deposit.Text = "0.00";
                lbl_deposit.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Regular);
                lbl_balance_sel.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Bold);
            }
            else
            {
                lbl_balance_sel.Text = "0.00";
                lbl_deposit.Text = Math.Abs(bal_amnt).ToString("0.00");
                lbl_deposit.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Bold);
                lbl_balance_sel.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Regular);
            }
        }

        private void dgv_guestlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (dgv_guestlist.Rows.Count > 0)
            {
                String acct_no = "";
                try { acct_no = dgv_guestlist.Rows[e.RowIndex].Cells[0].Value.ToString(); } catch { }
                try { rtxt_gname.Text = dgv_guestlist.Rows[e.RowIndex].Cells[1].Value.ToString(); } catch { }
                dgv_folio.DataSource = db.get_guest_foliohist(acct_no);
            }
        }

        private void btn_printbill_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                thisDatabase db = new thisDatabase();
                Report rpt = new Report("", "");
                double val = (Convert.ToDateTime(lbl_depdate.Text) - Convert.ToDateTime(lbl_arrdate.Text)).TotalDays;
                String noofnight = val.ToString();

                if (val == 0)
                {
                    noofnight = "1";
                }
                rpt.Show();
                rpt.printprev_gfhist(lbl_gfolio.Text, lbl_rm.Text, rtxt_gname.Text, lbl_rate.Text, lbl_pax.Text, lbl_rmtype.Text, lbl_address.Text + " | " + lbl_company.Text, lbl_arrdate.Text, lbl_arrtime.Text, lbl_depdate.Text, lbl_deptime.Text, noofnight, db.get_folio_totalcharges(lbl_gfolio.Text).ToString(), db.get_folio_totalpayment(lbl_gfolio.Text).ToString(), lbl_disc.Text, lbl_grossrate.Text);
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            tbcntrl.SelectedTab = tpg_1;
            tpg_1.Show();
        }

        private void dgv_guestlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_folio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

         
    }
}
