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
    public partial class rpt_GuestHighBalance : Form
    {
        public rpt_GuestHighBalance()
        {
            InitializeComponent();
        }

        private void rpt_GuestHighBalance_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            lbl_gballimit.Text = db.get_guestballimit().ToString("#0,000.00");
            disp_hibalguest();

            btn_print.Hide();
        }

        private void disp_hibalguest()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            GlobalMethod gm = new GlobalMethod();

            dt = db.get_hibalguestlist();

            try 
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgv_row = (DataGridViewRow)dgv_guestlist.Rows[0].Clone();

                    dgv_row.Cells[0].Value = row[0].ToString();
                    dgv_row.Cells[1].Value = Convert.ToDateTime(row[1].ToString()).ToString("MM/dd/yyyy");
                    dgv_row.Cells[2].Value = Convert.ToDateTime(row[2].ToString()).ToString("MM/dd/yyyy");
                    dgv_row.Cells[3].Value = row[3].ToString();
                    dgv_row.Cells[4].Value = gm.get_amount_negbracket(Convert.ToDouble(row[4].ToString()));
                    dgv_row.Cells[5].Value = gm.get_amount_negbracket(Convert.ToDouble(row[5].ToString()));
                    dgv_row.Cells[6].Value = gm.get_amount_negbracket(Convert.ToDouble(row[6].ToString()));
                    dgv_row.Cells[7].Value = row[7].ToString();

                    dgv_guestlist.Rows.Add(dgv_row);
                }
            }
            catch { }
            

            //dgv_guestlist.DataSource = db.get_hibalguestlist();
        }

        public void disp_chgfil(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double bal_amnt = 0.00;
            Double runbal = 0.00;
            DataTable dt = new DataTable();
            dt = db.get_guest_chargefil(reg_num, false);

            dgv_gfolio.Rows.Clear();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgv_row = (DataGridViewRow)dgv_gfolio.Rows[0].Clone();

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

                    dgv_gfolio.Rows.Add(dgv_row);
                }
            }
            catch (Exception) { }

            bal_amnt = db.get_guest_charges_total(reg_num, false);

            lbl_balanceamnt.Text = bal_amnt.ToString("#0,000.00");
        }

        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String reg_num = dgv_guestlist[7, e.RowIndex].Value.ToString();

                clr_frm();

                disp_guestinfo(reg_num);
                disp_chgfil(reg_num);

                //rom_code = lbl_rm.Text;
                //rom_rate = lbl_grossrate.Text;
            }
            catch (Exception) { }
        }

        private void clr_frm()
        {
            int length = 0;

            try
            {
                /*lbl_gfolio.Text = "";
                lbl_arrdate.Text = "";
                lbl_depdate.Text = "";
                lbl_company.Text = "";
                lbl_username.Text = "";
                lbl_rm.Text = "";
                lbl_grossrate.Text = "";
                lbl_noofnights.Text = "";
                lbl_pax.Text = "";

                rtxt_remarks.Text = "";
                rtxt_gname.Text = "";

                length = dgv_gfolio.Rows.Count;
                */
                dgv_gfolio.Rows.Clear();
            }
            catch (Exception er) { }
        }

        private void disp_guestinfo(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            Double db_amt = 0.00;
            DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));
            dt = db.get_guest_curchkin_selected(reg_num);

            foreach (DataRow row in dt.Rows)
            {
                lbl_gfolio.Text = row["reg_num"].ToString();
                lbl_arrdate.Text = Convert.ToDateTime(row["arr_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_depdate.Text = Convert.ToDateTime(row["dep_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_company.Text = "";
                lbl_chckinby.Text = row["user_id"].ToString() + " " + Convert.ToDateTime(row["t_date"].ToString()).ToString("MM/dd/yyyy") + " " + row["t_time"].ToString();
                lbl_rm.Text = row["rom_code"].ToString();
                lbl_grossrate.Text = Convert.ToDouble(db.get_roomrateamt(row["rate_code"].ToString(), row["typ_code"].ToString(), int.Parse(row["occ_type"].ToString())).ToString()).ToString("0.00");
                lbl_rate.Text = Math.Round(Convert.ToDecimal((Convert.ToDouble(row["rom_rate"].ToString()) + Convert.ToDouble(row["govt_tax"].ToString()) + Convert.ToDouble(row["serv_chg"].ToString()))), 1).ToString("0.00");
                lbl_noofnights.Text = "";
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

        private void btn_rpt_Click(object sender, EventArgs e)
        {
            Report rpt = new Report("", "");

            rpt.Show();
            rpt.print_highbal_rptlist();
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

        private void dgv_gfolio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void panel35_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
