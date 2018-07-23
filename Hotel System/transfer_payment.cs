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
    public partial class transfer_payment : Form
    {
        private String whole_amt = "0.00";
        private String chg_amt = "0.00";
        public Boolean isUpdated = false;
        GlobalMethod gm = new GlobalMethod();
        public transfer_payment()
        {
            InitializeComponent();
        }
        public transfer_payment(newGuestBilling frm, String gfolio, String gname, String rm, String dchg_amt, String dwhole_balance)
        {

            InitializeComponent();
            lbl_gfno_frm.Text = gfolio;
            lbl_guest_frm.Text = gname;
            lbl_rm_frm.Text = rm;
            txt_bal_amt.Text = dwhole_balance;
            chg_amt = dchg_amt;
            whole_amt = dwhole_balance;
            set_data(gfolio, gname, rm, dchg_amt, dwhole_balance);
        }
        private void transfer_payment_Load(object sender, EventArgs e)
        {
            disp_currently_guest("");
            cbo_option.SelectedIndex = 1;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_currently_guest(txt_search.Text);
        }

        private void btn_transfer_Click(object sender, EventArgs e)
        {
            if (lbl_gfno_to.Text == "")
            {
                MessageBox.Show("Pls select guest to transfer the balance.");
            }
            else if (gm.toNormalDoubleFormat(txt_bal_amt.Text) == 0.00)
            {
                MessageBox.Show("Make sure that your balance entry is not equal to Zero(0).");
            }
            else
            {
                //if (cbo_option.SelectedIndex == 0)
                //{
                    thisDatabase db = new thisDatabase();
                    String trans_chg = db.get_pk("chg_trp");
                    String refer_frm = "PAY TO " + lbl_rm_to.Text + "-" + lbl_guest_to.Text;
                    String refer_to = "PAY FRM " + lbl_rm_frm.Text + "-" + lbl_guest_frm.Text;
                    String nxt_val = db.get_chg_num_latest(trans_chg);
                    String val_frm = "'" + lbl_gfno_frm.Text + "', '" + trans_chg + "', '" + nxt_val + "', '" + lbl_rm_frm.Text + "', '" + refer_frm + "', '" + gm.toNormalDoubleFormat(txt_bal_amt.Text).ToString("0.00") + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + db.get_systemdate("") + "', '" + lbl_gfno_to.Text + "'";
                    String val_to = "";
                
                    if (db.InsertOnTable("chgfil", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, chg_date, tofr_fol", val_frm))
                    {
                        db.set_all_pk("charge", "chg_num", nxt_val, "chg_code='" + trans_chg + "'", nxt_val.Length);

                        nxt_val = db.get_chg_num_latest(trans_chg);

                        val_to = "'" + lbl_gfno_to.Text + "', '" + trans_chg + "', '" + nxt_val + "', '" + lbl_rm_to.Text + "', '" + refer_to + "', '-" + Convert.ToDouble(txt_bal_amt.Text).ToString("0.00") + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + db.get_systemdate("") + "', '" + lbl_gfno_frm.Text + "'";

                        db.InsertOnTable("chgfil", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, chg_date, tofr_fol", val_to);

                        db.set_all_pk("charge", "chg_num", nxt_val, "chg_code='" + trans_chg + "'", nxt_val.Length);
                        MessageBox.Show("Successfully transfer payment.");
                        isUpdated = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error on tranfering payment.");
                    }
               // }
                //else
                //{

                //}
            }
        }

        private void disp_currently_guest(String search)
        {
            thisDatabase db = new thisDatabase();

            dgv_search.DataSource = db.get_guest_currentlycheckin(search);
        }

        public void set_data(String gfolio, String gname, String rm, String dchg_amt, String dwhole_balance)
        {
           
        }

        private void dgv_search_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_search.Rows.Count > 0)
                {

                    if (lbl_gfno_frm.Text != dgv_search[5, e.RowIndex].Value.ToString())
                    {
                        lbl_rm_to.Text = dgv_search[0, e.RowIndex].Value.ToString();
                        lbl_guest_to.Text = dgv_search[2, e.RowIndex].Value.ToString();
                        lbl_gfno_to.Text = dgv_search[5, e.RowIndex].Value.ToString();
                    }
                }
            }
            catch (Exception) { }
        }

        private void cbo_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_option.SelectedIndex == 0)
            {
                txt_bal_amt.Text = whole_amt;
                cbo_type.SelectedIndex = 1;
            }
            else if (cbo_option.SelectedIndex == 1)
            {
                txt_bal_amt.ReadOnly = false;
                txt_bal_amt.Text = "0.00";
                cbo_type.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Invalid Option.");
                txt_bal_amt.Text = "0.00";
                cbo_type.SelectedIndex = 2;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
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

        private void dgv_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
