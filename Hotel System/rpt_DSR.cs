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
    public partial class rpt_DSR : Form
    {
        private String shift_code = "";
        private String trnx_dt = "";
        private String dclerk = "";
        private int action = 0; //1=generate, 2=save, 3=cash report, 4=crd report, 5=cc report, 6= extra report, 7=finalized
        private Boolean has_final = false;
        String _schema;

        public rpt_DSR()
        {
            InitializeComponent();
        }

        private void rpt_DSR_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            _schema = db.get_schema();

            dtp_frm.Value = Convert.ToDateTime(Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM") + "-01");
            dtp_to.Value = Convert.ToDateTime(db.get_systemdate(""));
            dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));
            btn_save.Enabled = false;
            btn_cash.Enabled = false;
            btn_ccrd.Enabled = false;
            btn_extraitem.Enabled = false;
            btn_cc.Enabled = false;
            btn_finalized.Enabled = false;

            load_dsrhist();
            load_userid();
            gm.load_shift(cbo_shift);
            
            pnl_1.Hide();
            pnl_2.Hide();
        }

        private void load_dsrhist()
        {
            thisDatabase db = new thisDatabase();

            dgv_list.DataSource = db.get_dsrlist(dtp_frm.Value.ToString("yyyy-MM-dd"), dtp_to.Value.ToString("yyyy-MM-dd"));
        }

        private void load_userid()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_alluserid();
            cbo_cashier.DataSource = dt;
            cbo_cashier.DisplayMember = "uid";
            cbo_cashier.ValueMember = "uid";
            cbo_cashier.SelectedIndex = -1;
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            pnl_1.Show();
            pnl_2.Show();
            action = 1;

            if (get_cbo_index(cbo_cashier) > -1)
            {
                dclerk = get_cbo_value(cbo_cashier);
            }

            bgWorker.RunWorkerAsync();
        }

        private void btn_finalized_Click(object sender, EventArgs e)
        {
            pnl_1.Show();
            pnl_2.Show();
            action = 7;

            bgWorker.RunWorkerAsync();
        }

        private void btn_cash_Click(object sender, EventArgs e)
        {
            disp_cshrpt();
        }

        private void btn_ccrd_Click(object sender, EventArgs e)
        {
            disp_crdrpt();
        }

        private void btn_transfer1_Click(object sender, EventArgs e)
        {
            int r;

            if (dgv_cash.SelectedRows.Count > 0)
            {
                r = dgv_cash.CurrentRow.Index;

                if(String.IsNullOrEmpty(dgv_cash[6, r].Value.ToString()) == true)
                {
                    MessageBox.Show("Food and Beverage value is empty.");
                }
                else if (Convert.ToDouble(dgv_cash[6, r].Value.ToString()) == 0)
                {
                    MessageBox.Show("Food and Beverage amount is zero.");
                }
                else
                {
                    lbl_transf_frm_folio.Text = dgv_cash[0, r].Value.ToString();
                    lbl_transf_guest.Text = dgv_cash[1, r].Value.ToString();
                    lbl_transf_rom.Text = dgv_cash[2, r].Value.ToString();
                    lbl_transf_c_name.Text = "Cash Amt";
                    lbl_transf_c_amount.Text = dgv_cash[5, r].Value.ToString();
                    txt_transf_amt.Text = dgv_cash[6, r].Value.ToString();
                    lbl_trans_row_selected.Text = r.ToString();

                    dgv_rpt_transfer.DataSource = dgv_ccrd.DataSource;

                    tbcntrl_disp.SelectedTab = tpg_2;
                    tpg_2.Show();
                }
            }
        }

        private void btn_transfer2_Click(object sender, EventArgs e)
        {
            int r;

            if (dgv_cash.SelectedRows.Count > 0)
            {
                r = dgv_ccrd.CurrentRow.Index;

                if (String.IsNullOrEmpty(dgv_ccrd[9, r].Value.ToString()) == true)
                {
                    MessageBox.Show("Food and Beverage value is empty.");
                }
                else if (Convert.ToDouble(dgv_ccrd[9, r].Value.ToString()) == 0)
                {
                    MessageBox.Show("Food and Beverage should not be zero.");
                }
                else
                {
                    lbl_transf_frm_folio.Text = dgv_ccrd[0, r].Value.ToString();
                    lbl_transf_guest.Text = dgv_ccrd[1, r].Value.ToString();
                    lbl_transf_rom.Text = dgv_ccrd[2, r].Value.ToString();
                    lbl_transf_c_name.Text = "Card Amt";
                    lbl_transf_c_amount.Text = dgv_ccrd[8, r].Value.ToString();
                    txt_transf_amt.Text = dgv_ccrd[9, r].Value.ToString();
                    lbl_trans_row_selected.Text = r.ToString();

                    dgv_rpt_transfer.DataSource = dgv_cash.DataSource;

                    tbcntrl_disp.SelectedTab = tpg_2;
                    tpg_2.Show();
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goback();
        }

        private void frm_reset()
        {

        }

        private void goback()
        {
            tbcntrl_disp.SelectedTab = tpg_1;
            tpg_1.Show();
        }

        private void disp_cshrpt()
        {
            DataTable dt = new DataTable();
            Report rpt = new Report("", "");
            String dsr_csh = "";
            Double or_amnt = 0.00, amnt = 0.00, fb_amnt = 0.00;
           
            field_enable(false);

            dt.Columns.Add("reg_num", typeof(String));
            dt.Columns.Add("full_name", typeof(String));
            dt.Columns.Add("rom_code", typeof(String));
            dt.Columns.Add("reference", typeof(String));
            dt.Columns.Add("or_amnt", typeof(String));
            dt.Columns.Add("amount", typeof(String));
            dt.Columns.Add("food", typeof(String));

            lbl_csh_no.Invoke(new Action(() =>
            {
                dsr_csh = lbl_csh_no.Text;
            }));

            try
            {
                foreach (DataGridViewRow row in dgv_cash.Rows)
                {
                    dt.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString());

                    or_amnt += Convert.ToDouble(row.Cells[4].Value.ToString());
                    amnt += Convert.ToDouble(row.Cells[5].Value.ToString());
                    fb_amnt += Convert.ToDouble(row.Cells[6].Value.ToString());
                }
            }
            catch (Exception) { }

            rpt.Show();

            rpt.print_cashsales_rpt(dsr_csh, dclerk + " / " + cbo_shift.Text, trnx_dt, dt, or_amnt.ToString("0.00"), amnt.ToString("0.00"), fb_amnt.ToString("0.00"));

            field_enable(true);
        }

        private void disp_crdrpt()
        {
            Report rpt = new Report("", "");
            String dsr_crd = "";
            DataTable dt = new DataTable();
            Double total_or = 0.00, total_cc = 0.00, total_food = 0.00;

            field_enable(false);
            dt.Columns.Add("reg_num", typeof(String));
            dt.Columns.Add("full_name", typeof(String));
            dt.Columns.Add("rom_code", typeof(String));
            dt.Columns.Add("or_number", typeof(String));
            dt.Columns.Add("card_type", typeof(String));
            dt.Columns.Add("reference", typeof(String)); //CARD NO
            dt.Columns.Add("trace_no", typeof(String));
            dt.Columns.Add("or_amnt", typeof(String));
            dt.Columns.Add("amount", typeof(String));
            dt.Columns.Add("food", typeof(String));

            lbl_crd_no.Invoke(new Action(() =>
            {
                dsr_crd = lbl_crd_no.Text;
            }));

            try
            {
                foreach (DataGridViewRow row in dgv_ccrd.Rows)
                {
                    dt.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString());
                    total_or += Convert.ToDouble(row.Cells[7].Value.ToString());
                    total_cc += Convert.ToDouble(row.Cells[8].Value.ToString());
                    total_food += Convert.ToDouble(row.Cells[9].Value.ToString());
                }
            }
            catch (Exception) { }

            rpt.Show();

            rpt.print_cardsales_rpt(dsr_crd, dclerk + " / " + cbo_shift.Text, trnx_dt, dt, total_or.ToString("0.00"), total_cc.ToString("0.00"), total_food.ToString("0.00"));

            field_enable(true);
        }

        private void disp_ccrpt()
        {
            Report rpt = new Report("", "");
            String trnx_dt = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
            DataTable dt = new DataTable();

            field_enable(false);
            dt.Columns.Add("reference", typeof(String));
            dt.Columns.Add("reg_num", typeof(String));
            dt.Columns.Add("full_name", typeof(String));
            dt.Columns.Add("rom_code", typeof(String));
            dt.Columns.Add("amount", typeof(String));
            dt.Columns.Add("Status", typeof(String));

            try
            {
                foreach (DataGridViewRow row in dgv_ccissued.Rows)
                {
                    dt.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), "1-Summary of Cash Credit Issued");
                }
            }
            catch (Exception) { }

            try
            {
                foreach (DataGridViewRow row in dgv_ccapplied.Rows)
                {
                    dt.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), "2-Summary of Cash Credit Applied");
                }
            }
            catch (Exception) { }
            
            rpt.Show();

            rpt.print_ccsummary(dclerk + " / " + cbo_shift.Text, trnx_dt, trnx_dt, lbl_hotel_sales.Text, lbl_fb_sales.Text, lbl_extra_sales.Text, lbl_ccissued.Text, lbl_ccapplied.Text, lbl_totalsales.Text, lbl_actualcash.Text, lbl_difference.Text, dt);

            field_enable(true);
        }

        private void disp_extrarpt()
        {
            Report rpt = new Report("", "");
            String trnx_dt = dtp_trnxdt.Value.ToString("yyyy-MM-dd");

            field_enable(false);
            rpt.Show();
            rpt.print_extraitemsales(dclerk + " / " + cbo_shift.Text, trnx_dt, trnx_dt);
            field_enable(true);   
        }

        private void insert_to_cash(String dsr_no, String dsr_rpt_no, String cashier)
        {
            thisDatabase db = new thisDatabase();
            String reg_num, full_name, rom_code, or_reference, or_amnt, amount, fb_amount;

            try
            {
                db.DeleteOnTable("dsr_rpt", "dsr_rpt_no='"+dsr_rpt_no+"' AND dsr_no='"+dsr_no +"' AND cashier='"+ cashier+"' AND dsr_type='CSH'");

                foreach (DataGridViewRow row in dgv_cash.Rows)
                {
                    reg_num     = row.Cells[0].Value.ToString();
                    full_name   = row.Cells[1].Value.ToString();
                    rom_code    = row.Cells[2].Value.ToString();
                    or_reference= row.Cells[3].Value.ToString();
                    or_amnt     = row.Cells[4].Value.ToString();
                    amount      = row.Cells[5].Value.ToString();
                    fb_amount   = row.Cells[6].Value.ToString();

                    db.InsertOnTable("dsr_rpt", "dsr_rpt_no, dsr_no, dsr_type, cashier, reg_num, full_name, rom_code, or_reference, or_amnt, amount, fb_amnt", "'" + dsr_rpt_no + "', '" + dsr_no + "', 'CSH','" + cashier + "', '" + reg_num + "', '" + full_name + "', '" + rom_code + "', '" + or_reference + "', '" + or_amnt + "', '" + amount + "', '" + fb_amount + "'");
                }
            }
            catch (Exception) { }
        }

        private void insert_to_card(String dsr_no, String dsr_rpt_no, String cashier)
        {
            thisDatabase db = new thisDatabase();
            String reg_num, full_name, rom_code, or_reference, card_type, card_no, trace_no, or_amnt, card_amnt, fb_amnt;

            try
            {
                db.DeleteOnTable("dsr_rpt", "dsr_rpt_no='" + dsr_rpt_no + "' AND dsr_no='" + dsr_no + "' AND cashier='" + cashier+ "' AND dsr_type='CRD'");

                foreach (DataGridViewRow row in dgv_ccrd.Rows)
                {
                    reg_num = row.Cells[0].Value.ToString();
                    full_name = row.Cells[1].Value.ToString();
                    rom_code = row.Cells[2].Value.ToString();
                    or_reference = row.Cells[3].Value.ToString();
                    card_type = row.Cells[4].Value.ToString();
                    card_no = row.Cells[5].Value.ToString();
                    trace_no = row.Cells[6].Value.ToString();
                    or_amnt = row.Cells[7].Value.ToString();
                    card_amnt = row.Cells[8].Value.ToString();
                    fb_amnt = row.Cells[9].Value.ToString();

                    db.InsertOnTable("dsr_rpt", "dsr_rpt_no, dsr_no, dsr_type, cashier, reg_num, full_name, rom_code, or_reference, card_type, trace_no, card_no, or_amnt, amount, fb_amnt", "'" + dsr_no + "', '" + dsr_rpt_no + "', 'CRD', '" + cashier + "', '" + reg_num + "', '" + full_name + "', '" + rom_code + "', '" + or_reference + "', '" + card_type + "', '" + trace_no + "', '" + card_no + "', '" + or_amnt + "', '" + card_amnt + "', '" + fb_amnt + "'");
                }
            }
            catch (Exception) { }
        }

        private void insert_to_cc(String dsr_no, String dsr_rpt_no, String cashier)
        {
            thisDatabase db = new thisDatabase();
            String reg_num, full_name, rom_code, or_num, cc_num, chg_code, chg_desc, user_id, t_date, t_time, amount, resvpaymentof, cc_type;
            String val = "";

            db.DeleteOnTable("dsr_cc", "dsr_rpt_no='" + dsr_rpt_no + "' AND dsr_no='" + dsr_no + "' AND cashier='" + cashier+ "'");

            try
            {
                foreach (DataGridViewRow row in dgv_ccissued.Rows)
                {
                    cc_num = row.Cells[0].Value.ToString();
                    reg_num = row.Cells[1].Value.ToString();
                    full_name = row.Cells[2].Value.ToString();
                    rom_code = row.Cells[3].Value.ToString();
                    amount = row.Cells[4].Value.ToString();
                    user_id = row.Cells[5].Value.ToString();
                    t_date = row.Cells[6].Value.ToString();
                    t_time = row.Cells[7].Value.ToString();                    
                    resvpaymentof = row.Cells[8].Value.ToString();
                    cc_type = "i";

                    val = "'" + cashier + "', '" + reg_num + "', '" + full_name + "', '" + rom_code + "', '" + dsr_rpt_no + "', '" + dsr_no + "', '" + cc_num + "', '" + amount + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + resvpaymentof + "', '" + cc_type + "'";
                    db.InsertOnTable("dsr_cc", "cashier, reg_num, full_name, rom_code, dsr_rpt_no, dsr_no, cc_no, amount, user_id, t_date, t_time,  resvpaymentof, cc_type", val);
                }
            }
            catch (Exception) { }

            try
            {
                foreach (DataGridViewRow row in dgv_ccapplied.Rows)
                {
                    cc_num = row.Cells[0].Value.ToString();
                    reg_num = row.Cells[1].Value.ToString();
                    full_name = row.Cells[2].Value.ToString();
                    rom_code = row.Cells[3].Value.ToString();
                    amount = row.Cells[4].Value.ToString();
                    user_id = row.Cells[5].Value.ToString();
                    t_date = row.Cells[6].Value.ToString();
                    t_time = row.Cells[7].Value.ToString();
                    resvpaymentof = row.Cells[8].Value.ToString();
                    cc_type = "a";

                    val = "'" + cashier + "', '" + reg_num + "', '" + full_name + "', '" + rom_code + "', '" + dsr_rpt_no + "', '" + dsr_no + "', '" + cc_num + "', '" + amount + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + resvpaymentof + "', '" + cc_type + "'";
                    db.InsertOnTable("dsr_cc", "cashier, reg_num, full_name, rom_code, dsr_rpt_no, dsr_no, cc_no, amount, user_id, t_date, t_time,  resvpaymentof, cc_type", val);
                }
            }
            catch (Exception) { }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            action = 2;
            pnl_1.Show();
            pnl_2.Show();
            bgWorker.RunWorkerAsync();
        }

        private void btn_transfer_confirm_Click(object sender, EventArgs e)
        {
           // try
            //{
                int r;
                Double dbl;

                if (dgv_rpt_transfer.SelectedRows.Count > 0)
                {
                    r = dgv_rpt_transfer.CurrentRow.Index;

                    if (dgv_rpt_transfer[0, r].Value.ToString() != lbl_transf_frm_folio.Text)
                    {
                        MessageBox.Show("Folio to transfer must be the same.");
                    }
                    else
                    {
                        if (lbl_transf_c_name.Text == "Cash Amt")
                        {
                            if (String.IsNullOrEmpty(dgv_rpt_transfer[9, r].Value.ToString()))
                            {
                                dbl = 0.00;
                            }
                            else
                            {
                                dbl = Convert.ToDouble(dgv_rpt_transfer[9, r].Value.ToString());
                            }
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(dgv_rpt_transfer[6, r].Value.ToString()))
                            {
                                dbl = 0.00;
                            }
                            else
                            {
                                dbl = Convert.ToDouble(dgv_rpt_transfer[6, r].Value.ToString());
                            }
                        }

                        dbl = dbl + Convert.ToDouble(txt_transf_amt.Text);

                        if (lbl_transf_c_name.Text == "Cash Amt")
                        {
                            dgv_cash[6, int.Parse(lbl_trans_row_selected.Text)].Value = (Convert.ToDouble(dgv_cash[6, int.Parse(lbl_trans_row_selected.Text)].Value) - dbl).ToString("0.00");
                            dgv_ccrd[9, r].Value = (Convert.ToDouble(dgv_ccrd[9, r].Value) + dbl).ToString("0.00");
                        }
                        else
                        {
                            dgv_ccrd[9, int.Parse(lbl_trans_row_selected.Text)].Value = (Convert.ToDouble(dgv_ccrd[9, int.Parse(lbl_trans_row_selected.Text)].Value) - dbl).ToString("0.00");
                            dgv_cash[6, r].Value = (Convert.ToDouble(dgv_cash[6, r].Value) + dbl).ToString("0.00");
                        }

                        goback();
                    }
                }                
            //}
            //catch (Exception) { }
            
        }

        private String get_zero_abs_strval(String val)
        {
            if (String.IsNullOrEmpty(val))
            {
                val = "0.00";
            }
            else
            {
                val = Math.Abs(Convert.ToDouble(val)).ToString("0.00");
            }

            return val;
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                i = cbo.SelectedIndex;
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";

            cbo.Invoke(new Action(() =>
            {
                value = cbo.SelectedValue.ToString();
            }));

            return value;
        }

        private String get_cbo_text(ComboBox cbo)
        {
            String txt = "";

            cbo.Invoke(new Action(() =>
            {
                txt = cbo.Text.ToString();
            }));

            return txt;
        }

        private void inc_pbar1(int i)
        {
            pbar_1.Invoke(new Action(() =>
            {
                pbar_1.Value += i;
            }));
        }

        private void reset_pbar1()
        {
            pbar_1.Invoke(new Action(() =>
            {
                pbar_1.Value = 0;
            }));
        }

        private void inc_pbar2(int i)
        {
            pbar_2.Invoke(new Action(() =>
            {
                pbar_2.Value += i;
            }));
        }

        private void reset_pbar2()
        {
            pbar_2.Invoke(new Action(() =>
            {
                pbar_2.Value = 0;
            }));
        }

        private void pbar_panl_hide()
        {
            pnl_1.Invoke(new Action(() =>
            {
                pnl_1.Hide();
            }));

            pnl_2.Invoke(new Action(() =>
            {
                pnl_2.Hide();
            }));
        }

        private void field_enable(Boolean flag)
        {
            dtp_trnxdt.Invoke(new Action(() =>
            {
                dtp_trnxdt.Enabled = flag;
            }));

            cbo_cashier.Invoke(new Action(() =>
            {
                cbo_cashier.Enabled = flag;
            }));

            cbo_shift.Invoke(new Action(() =>
            {
                cbo_shift.Enabled = flag;
            }));

            btn_generate.Invoke(new Action(() =>
            {
                btn_generate.Enabled = flag;
            }));

            btn_save.Invoke(new Action(() =>
            {
                btn_save.Enabled = flag;
            }));

            btn_cash.Invoke(new Action(() =>
            {
                btn_cash.Enabled = flag;
            }));

            btn_ccrd.Invoke(new Action(() =>
            {
                btn_ccrd.Enabled = flag;
            }));

            if (has_final)
            {
                btn_finalized.Invoke(new Action(() =>
                {
                    btn_finalized.Enabled = flag;
                }));
            }

            btn_transfer1.Invoke(new Action(() =>
            {
                btn_transfer1.Enabled = flag;
            }));

            btn_transfer2.Invoke(new Action(() =>
            {
                btn_transfer2.Enabled = flag;
            }));

            btn_extraitem.Invoke(new Action(() =>
            {
                btn_extraitem.Enabled = flag;
            }));

            btn_cc.Invoke(new Action(() =>
            {
                btn_cc.Enabled = flag;
            }));
        }

        private Double get_fb_total()
        {
            Double total = 0.00;           

            return total;
        }

        private Double get_cci_total()
        {
            Double total = 0.00;
            Double amnt = 0.00;

            try
            {
                for (int i=0; i < dgv_ccissued.Rows.Count-1; i++)
                {
                    amnt = Math.Abs(Convert.ToDouble(dgv_ccissued.Rows[i].Cells[4].Value.ToString()));
                    total += amnt;

                    dgv_ccissued.Invoke(new Action(() =>
                    {
                        dgv_ccissued.Rows[i].Cells[4].Value = amnt.ToString("0.00");
                    }));
                }
            }
            catch (Exception) { }

            return total;
        }

        private Double get_cca_total()
        {
            Double total = 0.00;
            Double amnt = 0.00;
            
            try
            {
                for (int i=0; i < dgv_ccapplied.Rows.Count-1; i++)
                {
                    amnt = Math.Abs(Convert.ToDouble(dgv_ccapplied.Rows[i].Cells[4].Value.ToString()));
                    total += amnt;

                    dgv_ccapplied.Invoke(new Action(() =>
                    {
                        dgv_ccapplied.Rows[i].Cells[4].Value = amnt.ToString("0.00");
                    }));
                }
            }
            catch (Exception) { }

            return total;
        }

        //on current date to generate
        private void generate_cc(String clerk, String dt_frm, String dt_to, Double fb_total)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt_issued = new DataTable();
            DataTable dt_applied = new DataTable();
            String WHERE1 = "", WHERE2 = "";
            Double total_cci = 0.00, total_cca = 0.00;

            Double total_csh = 0.00;
            Double extra = db.get_extraitemsales_amnt(clerk, dt_frm, dt_to);
            Double total_cshsales = 0.00;
            Double actual_csh = Convert.ToDouble(txt_actualcash.Text);
            Double diff = 0.00;

            if (clerk != "")
            {
                WHERE1 = WHERE1 + " AND cf.user_id='" + clerk + "'";
                WHERE2 = WHERE2 + " AND gf.co_user = '" + clerk + "'";
            }

            dt_issued = db.QueryBySQLCode("SELECT cf.reference AS \"CC No\", cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", cf.rom_code AS Room, cf.amount AS Amount, cf.user_id AS \"User ID\", cf.chg_date AS \"Date\", cf.t_time AS Time, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) AS \"Resv.PaymentOf\" FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.doc_type='CC' AND cf.t_date >= '" + dt_frm + "' AND cf.t_date <= '" + dt_to + "'" + WHERE1 + " ORDER BY cf.reference ASC");

            dt_applied = db.QueryBySQLCode("SELECT cf.reference AS \"CC No\", cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", cf.rom_code AS Room, cf.amount AS Amount, cf.user_id AS \"User ID\", cf.chg_date AS \"Date\", cf.t_time AS Time, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) AS \"Resv.PaymentOf\" FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".gfhist gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.doc_type='CC' AND gf.co_date = '" + dt_to + "'" + WHERE2 + " ORDER BY  cf.reference ASC");

            dgv_ccissued.Invoke(new Action(() =>
            {
                dgv_ccissued.DataSource = dt_issued;
            }));

            dgv_ccapplied.Invoke(new Action(() =>
            {
                dgv_ccapplied.DataSource = dt_applied;
            }));

            total_cci = get_cci_total();
            total_cca = get_cca_total();

            lbl_totalcash.Invoke(new Action(() =>
            {
                total_csh = Convert.ToDouble(lbl_totalcash.Text);
            }));

            total_cshsales = (total_csh + fb_total + extra + total_cci) - total_cca;
            diff = actual_csh - total_cshsales;

            //display summary report
            lbl_hotel_sales.Invoke(new Action(() =>
            {
                lbl_hotel_sales.Text = total_csh.ToString("0.00");
            }));

            lbl_fb_sales.Invoke(new Action(() =>
            {
                lbl_fb_sales.Text = fb_total.ToString("0.00");
            }));

            lbl_extra_sales.Invoke(new Action(() =>
            {
                lbl_extra_sales.Text = extra.ToString("0.00");
            }));

            lbl_ccissued.Invoke(new Action(() =>
            {
                lbl_ccissued.Text = total_cci.ToString("0.00");
            }));

            lbl_ccapplied.Invoke(new Action(() =>
            {
                lbl_ccapplied.Text = total_cca.ToString("0.00");
            }));

            lbl_totalsales.Invoke(new Action(() =>
            {
                lbl_totalsales.Text = total_cshsales.ToString("0.00");
            }));

            lbl_actualcash.Invoke(new Action(() =>
            {
                lbl_actualcash.Text = actual_csh.ToString("0.00");
            }));

            lbl_difference.Invoke(new Action(() =>
            {
                lbl_difference.Text = diff.ToString("0.00");
            }));

            lbl_totalsales_header.Invoke(new Action(() =>
            {
                lbl_totalsales_header.Text = total_cshsales.ToString("0.00");
            }));
        }

        //generate cc on prevous date
        private void generate_cc_prevdate(String clerk, String dt_frm, String dt_to, Double fb_total)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt_issued = new DataTable();
            DataTable dt_applied = new DataTable();
            String WHERE1 = "", WHERE2 = "";
            Double total_cci = 0.00, total_cca = 0.00;

            Double total_csh = 0.00;
            Double extra = db.get_extraitemsales_amnt(clerk, dt_frm, dt_to);
            Double total_cshsales = 0.00;
            Double actual_csh = Convert.ToDouble(txt_actualcash.Text);
            Double diff = 0.00;

            if (clerk != "")
            {
                WHERE1 = WHERE1 + " AND cf.user_id='" + clerk + "'";
                WHERE2 = WHERE2 + " AND gf.co_user = '" + clerk + "'";
            }

            dt_issued = db.QueryBySQLCode("SELECT cf.reference AS \"CC No\", cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", cf.rom_code AS Room, cf.amount AS Amount, cf.user_id AS \"User ID\", cf.chg_date AS \"Date\", cf.t_time AS Time, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) AS \"Resv.PaymentOf\" FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".gfhist gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.doc_type='CC' AND cf.t_date >= '" + dt_frm + "' AND cf.t_date <= '" + dt_to + "'" + WHERE1 + " ORDER BY cf.reference ASC");

            dt_applied = db.QueryBySQLCode("SELECT cf.reference AS \"CC No\", cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", cf.rom_code AS Room, cf.amount AS Amount, cf.user_id AS \"User ID\", cf.chg_date AS \"Date\", cf.t_time AS Time, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) AS \"Resv.PaymentOf\" FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".gfhist gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.doc_type='CC' AND gf.co_date = '" + dt_to + "'" + WHERE2 + " ORDER BY  cf.reference ASC");

            dgv_ccissued.Invoke(new Action(() =>
            {
                dgv_ccissued.DataSource = dt_issued;
            }));

            dgv_ccapplied.Invoke(new Action(() =>
            {
                dgv_ccapplied.DataSource = dt_applied;
            }));

            total_cci = get_cci_total();
            total_cca = get_cca_total();

            lbl_totalcash.Invoke(new Action(() =>
            {
                total_csh = Convert.ToDouble(lbl_totalcash.Text);
            }));

            total_cshsales = (total_csh + fb_total + extra + total_cci) - total_cca;
            diff = actual_csh - total_cshsales;

            //display summary report
            lbl_hotel_sales.Invoke(new Action(() =>
            {
                lbl_hotel_sales.Text = total_csh.ToString("0.00");
            }));

            lbl_fb_sales.Invoke(new Action(() =>
            {
                lbl_fb_sales.Text = fb_total.ToString("0.00");
            }));

            lbl_extra_sales.Invoke(new Action(() =>
            {
                lbl_extra_sales.Text = extra.ToString("0.00");
            }));

            lbl_ccissued.Invoke(new Action(() =>
            {
                lbl_ccissued.Text = total_cci.ToString("0.00");
            }));

            lbl_ccapplied.Invoke(new Action(() =>
            {
                lbl_ccapplied.Text = total_cca.ToString("0.00");
            }));

            lbl_totalsales.Invoke(new Action(() =>
            {
                lbl_totalsales.Text = total_cshsales.ToString("0.00");
            }));

            lbl_actualcash.Invoke(new Action(() =>
            {
                lbl_actualcash.Text = actual_csh.ToString("0.00");
            }));

            lbl_difference.Invoke(new Action(() =>
            {
                lbl_difference.Text = diff.ToString("0.00");
            }));

            lbl_totalsales_header.Invoke(new Action(() =>
            {
                lbl_totalsales_header.Text = total_cshsales.ToString("0.00");
            }));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            String dt_frm = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
            String dt_to = dt_frm;
            String remark = "";
            String cashier = "";
            String prev_folio = "";
            Double total_cash = 0.00;
            Double total_card = 0.00;
            String dsr_no = "";
            Double total_fb = 0.00;
            Double total_sales = 0.00, total_actual = 0.00, total_extra = 0.00, total_cci = 0.00, total_cca = 0.00; 

            if (action == 1)
            {
                inc_pbar1(1);

                if (get_cbo_index(cbo_shift) == -1)
                {
                    MessageBox.Show("Time Shift must be selected.");
                }
                else
                {
                    inc_pbar1(5);
                    field_enable(false);

                    String shift_no = db.get_shift_no(get_cbo_text(cbo_shift));

                    if (shift_no == "03")
                    {
                        dtp_trnxdt.Invoke(new Action(() =>
                        {
                            dt_to = dtp_trnxdt.Value.AddDays(1).ToString("yyyy-MM-dd");
                        }));
                    }
                    if (get_cbo_index(cbo_cashier) > -1)
                    {
                        cashier = get_cbo_value(cbo_cashier);
                    }
                    dclerk = cashier;
                    shift_code = shift_no;

                    trnx_dt = dt_to;

                    dtp_trnxdt.Invoke(new Action(() =>
                    {
                        dsr_no = dtp_trnxdt.Value.ToString("MMddyy") + "-" + shift_no;
                    }));

                    lbl_dsr_no.Invoke(new Action(() =>
                    {
                        lbl_dsr_no.Text = dsr_no;
                    }));

                    lbl_csh_no.Invoke(new Action(() =>
                    {
                        lbl_csh_no.Text = "CSH" + dsr_no;
                    }));

                    lbl_crd_no.Invoke(new Action(() =>
                    {
                        lbl_crd_no.Text = "CRD" + dsr_no;
                    }));

                    dt = db.get_cash_or_rpt(dt_frm, dt_to, cashier);
                    dt2 = db.get_ccrd_or_rpt(dt_frm, dt_to, cashier);
                    Double amt = 0.00;

                    inc_pbar1(4);

                    if (dt.Rows.Count > 0)
                    {
                        int i = 80/dt.Rows.Count;

                        if (dt.Rows.Count > 80)
                        {
                            i = 1;
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            row[4] = get_zero_abs_strval(row[4].ToString());
                            row[5] = get_zero_abs_strval(row[5].ToString());
                            row[6] = get_zero_abs_strval(row[6].ToString());
                            row[7] = get_zero_abs_strval(row[7].ToString());

                            amt = Convert.ToDouble(row[5].ToString());
                            row[5] = (amt - Convert.ToDouble(row[6].ToString())).ToString("0.00");

                            if (prev_folio != "")
                            {
                                if (prev_folio == row[0].ToString())
                                {
                                    row[5] = "0.00";
                                    row[6] = "0.00";
                                }
                            }

                            total_fb += Convert.ToDouble(row[6]);
                            total_cash += Convert.ToDouble(row[5]);
                            prev_folio = row[0].ToString();

                            if (i < 80)
                                inc_pbar1(i);
                            else
                                reset_pbar1();
                        }
                    }

                    dt.Columns.RemoveAt(7);

                    if (dt2.Rows.Count > 0)
                    {
                        prev_folio = "";

                        inc_pbar2(5);

                        int i = 80 / dt2.Rows.Count;

                        if (dt2.Rows.Count > 80)
                        {
                            i = 1;
                        }

                        foreach (DataRow row2 in dt2.Rows)
                        {
                            row2[7] = get_zero_abs_strval(row2[7].ToString());
                            row2[8] = get_zero_abs_strval(row2[8].ToString());
                            row2[9] = get_zero_abs_strval(row2[9].ToString());

                            amt = Convert.ToDouble(row2[8].ToString());
                            row2[8] = (amt - Convert.ToDouble(row2[9].ToString())).ToString("0.00");

                            if (prev_folio != "")
                            {
                                if (prev_folio == row2[0].ToString())
                                {
                                    //row2[8] = "0.00";
                                    row2[9] = "0.00";
                                }
                            }
                            if (db.is_exists_regnumfromcash(row2[0].ToString(), dt_frm, dt_to, cashier))
                            {
                                row2[9] = "0.00";
                            }

                            total_fb += Convert.ToDouble(row2[9]);
                            total_card += Convert.ToDouble(row2[8]);
                            prev_folio = row2[0].ToString();

                            if (i < 80)
                                inc_pbar2(i);
                            else
                                reset_pbar2();
                        }
                    }

                    inc_pbar2(5);
                    lbl_totalcash.Invoke(new Action(() =>
                    {
                        lbl_totalcash.Text = total_cash.ToString("0.00");
                    }));

                    lbl_totalcc.Invoke(new Action(() =>
                    {
                        lbl_totalcc.Text = total_card.ToString("0.00");
                    }));

                    dgv_cash.Invoke(new Action(() =>
                    {
                        dgv_cash.DataSource = dt;
                    }));

                    dgv_ccrd.Invoke(new Action(() =>
                    {
                        dgv_ccrd.DataSource = dt2;
                    }));


                    if (Convert.ToDateTime(dt_frm).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                    {
                        generate_cc(cashier, dt_frm, dt_to, total_fb);
                    }
                    else
                    {
                        generate_cc_prevdate(cashier, dt_frm, dt_to, total_fb);
                    }

                    inc_pbar2(5);

                    reset_pbar1();
                    reset_pbar2();

                    pbar_panl_hide();

                    field_enable(true);

                    btn_cash.Invoke(new Action(() =>
                    {
                        btn_cash.Enabled = false;
                    }));

                    btn_ccrd.Invoke(new Action(() =>
                    {
                        btn_ccrd.Enabled = false;
                    }));

                    btn_extraitem.Invoke(new Action(() =>
                    {
                        btn_extraitem.Enabled = false;
                    }));

                    btn_cc.Invoke(new Action(() =>
                    {
                        btn_cc.Enabled = false;
                    }));
                }
            }
            else if (action == 2)
            {
                String tcsh = "", tcrd = "", dsr_csh = "", dsr_crd = "", dsr_fb = "", dsr_cci="", dsr_cca="", dsr_extra="", dsr_sales="", dsr_actual="";
                String dsr_no_cashier = "";
                field_enable(false);

                lbl_totalcash.Invoke(new Action(() =>
                {
                    tcsh = lbl_totalcash.Text;
                }));

                lbl_totalcc.Invoke(new Action(() =>
                {
                    tcrd = lbl_totalcc.Text;
                }));

                lbl_dsr_no.Invoke(new Action(() =>
                {
                    dsr_no = lbl_dsr_no.Text;
                }));

                lbl_csh_no.Invoke(new Action(() =>
                {
                    dsr_csh = lbl_csh_no.Text;
                }));

                lbl_crd_no.Invoke(new Action(() =>
                {
                    dsr_crd = lbl_crd_no.Text;
                }));

                ///
                lbl_fb_sales.Invoke(new Action(() =>
                {
                    dsr_fb = lbl_fb_sales.Text;
                }));

                lbl_extra_sales.Invoke(new Action(() =>
                {
                    dsr_extra = lbl_extra_sales.Text;
                }));

                lbl_ccissued.Invoke(new Action(() =>
                {
                    dsr_cci = lbl_ccissued.Text;
                }));

                lbl_ccapplied.Invoke(new Action(() =>
                {
                    dsr_cca = lbl_ccapplied.Text;
                }));

                lbl_totalsales.Invoke(new Action(() =>
                {
                    dsr_sales = lbl_totalsales.Text;
                }));

                lbl_actualcash.Invoke(new Action(() =>
                {
                    dsr_actual = lbl_actualcash.Text;
                }));

                rtxt_remark.Invoke(new Action(() =>
                {
                    remark = rtxt_remark.Text;
                }));

                total_cash = Convert.ToDouble(tcsh);
                total_card = Convert.ToDouble(tcrd);
                total_fb = Convert.ToDouble(dsr_fb);
                total_extra = Convert.ToDouble(dsr_extra);
                total_cci = Convert.ToDouble(dsr_cci);
                total_cca = Convert.ToDouble(dsr_cca);
                total_sales = Convert.ToDouble(dsr_sales);
                total_actual = Convert.ToDouble(dsr_actual);

                cashier = "ALL";                

                if (get_cbo_index(cbo_cashier) > -1)
                {
                    cashier = get_cbo_value(cbo_cashier);
                }

                dsr_no_cashier = dsr_no + "-" + cashier;

                if (db.InsertOnTable("dsr", "dsr_no_cashier, dsr_no, cashier, shift_no, dsr_dt, cash_amnt, crd_amnt, cci_amnt, cca_amnt, fb_sales, extra, sales, actual, generatedby, t_date_gen, t_time_gen, remark", "'" + dsr_no_cashier + "', '" + dsr_no + "', '" + cashier + "', '" + shift_code + "', '" + trnx_dt + "', '" + total_cash.ToString("0.00") + "', '" + total_card.ToString("0.00") + "', '" + total_cci.ToString("0.00") + "', '" + total_cca.ToString("0.00") + "', '" + total_fb.ToString("0.00") + "', '" + total_extra.ToString("0.00") + "', '" + total_sales.ToString("0.00") + "', '" + total_actual.ToString("0.00") + "', '" + GlobalClass.username + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + remark + "'") == false)
                {
                    db.UpdateOnTable("dsr", "dsr_no='" + dsr_no + "', cashier='" + cashier + "', shift_no='" + shift_code + "', dsr_dt='" + trnx_dt + "', cash_amnt='" + total_cash.ToString("0.00") + "', crd_amnt='" + total_card.ToString("0.00") + "', cci_amnt='" + total_cci.ToString("0.00") + "', cca_amnt='" + total_cca.ToString("0.00") + "', fb_sales='" + total_fb.ToString("0.00") + "', extra='" + total_extra.ToString("0.00") + "', sales='" + total_sales.ToString("0.00") + "', actual='" + total_actual.ToString("0.00") + "', generatedby='" + GlobalClass.username + "', t_date_gen='" + DateTime.Now.ToString("yyyy-MM-dd") + "', t_time_gen='" + DateTime.Now.ToString("HH:mm") + "', remark='" + remark + "'", "dsr_no_cashier='" + dsr_no_cashier + "'");
                }

                insert_to_cash(dsr_no, dsr_csh, cashier);
                insert_to_card(dsr_no, dsr_crd, cashier);
                insert_to_cc(dsr_no, dsr_crd, cashier);
                field_enable(true);
                pbar_panl_hide();
            }
            else if (action == 3)
            {
                
            }
            else if (action == 4)
            {
               
            }
            else if (action == 5)
            {
                
            }
            else if (action == 6)
            {       

            }
            else if (action == 7)
            {
                String thiscashier = "ALL";

                if (get_cbo_index(cbo_cashier) > -1)
                {
                    cashier = get_cbo_value(cbo_cashier);
                }

                db.UpdateOnTable("dsr", "finalizedby='" + GlobalClass.username + "' AND t_date_fin='" + db.get_systemdate("") + "' AND t_time_fin='" + DateTime.Now.ToString("HH:mm") + "'", "dsr_no='" + lbl_dsr_no.Text + "' AND cashier='" + thiscashier + "'");
            
            }
        }

        private void btn_cc_Click(object sender, EventArgs e)
        {
            disp_ccrpt();
        }

        private void btn_extraitem_Click(object sender, EventArgs e)
        {
            disp_extrarpt();
        }

        private void btn_summary_Click(object sender, EventArgs e)
        {
            tbcntrl_disp.SelectedTab = tpg_3;

            tpg_3.Show();
        }

        private void btn_back2_Click(object sender, EventArgs e)
        {
            goback();
        }

        private void txt_actualcash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lbl_actualcash.Text = txt_actualcash.Text;

                lbl_difference.Text = (Convert.ToDouble(txt_actualcash.Text) - Convert.ToDouble(lbl_totalsales.Text)).ToString("0.00");
            }
            catch (Exception) { }
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();

            try
            {

                if (dgv_list.Rows.Count > 0)
                {
                    int row = e.RowIndex;
                    String dsrno = dgv_list.Rows[row].Cells[0].Value.ToString();
                    String cashier = dgv_list.Rows[row].Cells[2].Value.ToString();

                    lbl_dsr_no.Text = dgv_list.Rows[e.RowIndex].Cells[0].Value.ToString();
                    
                    cbo_shift.SelectedValue = dgv_list.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //cashier
                    cbo_cashier.SelectedValue = dgv_list.Rows[e.RowIndex].Cells[2].Value.ToString();
                    
                    dtp_trnxdt.Value = Convert.ToDateTime(dgv_list.Rows[e.RowIndex].Cells[3].Value.ToString());
                    lbl_totalcash.Text = dgv_list.Rows[e.RowIndex].Cells[4].Value.ToString();
                    lbl_totalcc.Text = dgv_list.Rows[e.RowIndex].Cells[5].Value.ToString();
                    lbl_ccissued.Text = dgv_list.Rows[e.RowIndex].Cells[6].Value.ToString();
                    lbl_ccapplied.Text = dgv_list.Rows[e.RowIndex].Cells[7].Value.ToString();
                    lbl_fb_sales.Text = dgv_list.Rows[e.RowIndex].Cells[8].Value.ToString();
                    lbl_extra_sales.Text = dgv_list.Rows[e.RowIndex].Cells[9].Value.ToString();
                    lbl_totalsales.Text = dgv_list.Rows[e.RowIndex].Cells[10].Value.ToString();
                    lbl_totalsales_header.Text = dgv_list.Rows[e.RowIndex].Cells[10].Value.ToString();

                    txt_actualcash.Text = dgv_list.Rows[e.RowIndex].Cells[11].Value.ToString();
                    lbl_actualcash.Text = dgv_list.Rows[e.RowIndex].Cells[11].Value.ToString();
                    lbl_generatedby.Text = dgv_list.Rows[e.RowIndex].Cells[12].Value.ToString() + " " + dgv_list.Rows[e.RowIndex].Cells[13].Value.ToString();
                    lbl_finalizedby.Text = dgv_list.Rows[e.RowIndex].Cells[14].Value.ToString() + " " + dgv_list.Rows[e.RowIndex].Cells[15].Value.ToString();

                    dgv_cash.DataSource = db.get_dsr_cash_rpt(dsrno, cashier);
                    dgv_ccrd.DataSource = db.get_dsr_ccrd_rpt(dsrno, cashier);
                    dgv_ccissued.DataSource = db.get_dsr_cci_rpt(dsrno, cashier);
                    dgv_ccapplied.DataSource = db.get_dsr_cca_rpt(dsrno, cashier);

                    btn_save.Enabled = true;
                    btn_cash.Enabled = true;
                    btn_ccrd.Enabled = true;
                    btn_extraitem.Enabled = true;
                    btn_cc.Enabled = true;
                }
            }
            catch (Exception) { }
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            load_dsrhist();
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