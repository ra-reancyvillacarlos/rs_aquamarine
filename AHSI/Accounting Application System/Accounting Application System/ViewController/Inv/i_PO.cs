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
    public partial class i_PO : Form
    {
        private z_enter_item _frm_ipr;
        dbInv db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        Boolean isTextItem = false;
        int lnno_last = 1;
        public String ok = "";
        String stk_trns_type = "PO";

        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_PO()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();

            gc.load_terms(cbo_payment);
            gc.load_branch(txt_notes);
            refresh_customerlist();

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true;
        }

        void i_PO_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void disp_list()
        {
            if (bgworker.IsBusy)
            {
                bgworker_cancel = true;
                try { bgworker.CancelAsync(); }
                catch { }
            }
            else
            {
                bgworker.RunWorkerAsync();
            }

            /*


            Double net_price = 0.0;
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
                String WHERE = "";

                if (!String.IsNullOrEmpty(textBox15.Text))
                {
                    WHERE = String.Format(" AND (purc_ord LIKE '%{0}%' OR supl_code LIKE '%{0}%' OR supl_name LIKE '%{0}%' OR reference LIKE '%{0}%' OR   pay_code LIKE '%{0}%' OR recipient LIKE '%{0}%' OR request_by LIKE '%{0}%' OR notes LIKE '%{0}%' OR pr_no LIKE '%{0}%' ) ", textBox15.Text);
                }

               // DataTable dt = db.QueryOnTableWithParams("purhdr", "*", "t_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'", " ORDER BY purc_ord ASC");
                DataTable dt = db.QueryBySQLCode("select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where p.t_date between '" + dateFrom + "' and '" + dateTo + "' " + WHERE + " ORDER BY purc_ord DESC");

                int i = 0;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                    row.Cells["dgvl_supl_name"].Value = dt.Rows[r]["supl_name"].ToString();
                    row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                    row.Cells["dgvl_delv_date"].Value = gm.toDateString(dt.Rows[r]["delv_date"].ToString(),"");
                    row.Cells["dgvl_rels_date"].Value = gm.toDateString(dt.Rows[r]["rels_date"].ToString(),"");
                    row.Cells["dgvl_printed"].Value = dt.Rows[r]["printed"].ToString();
                    row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                    row.Cells["dgvl_request_by"].Value = dt.Rows[r]["request_by"].ToString();
                    row.Cells["dgvl_payment"].Value = dt.Rows[r]["mp_desc"].ToString();
                    row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(),"");
                    row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                    row.Cells["dgvl_supl_code"].Value = dt.Rows[r]["supl_code"].ToString();
                    row.Cells["dgvl_pay_code"].Value = dt.Rows[r]["pay_code"].ToString();
                    row.Cells["dgvl_notes"].Value = dt.Rows[r]["notes"].ToString();
                    row.Cells["dgvl_notes2"].Value = dt.Rows[r]["notes2"].ToString();
                    row.Cells["dgvl_pr_no"].Value = dt.Rows[r]["pr_no"].ToString();


                    row.Cells["dgvl_finalized"].Value = dt.Rows[r]["finalized"].ToString();
                    row.Cells["dgvl_finalized_date"].Value = gm.toDateString(dt.Rows[r]["finalized_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl_finalized_time"].Value = dt.Rows[r]["finalized_time"].ToString();
                    row.Cells["dgvl_finalized_by"].Value = dt.Rows[r]["finalized_by"].ToString();
                    row.Cells["dgvl_closed"].Value = dt.Rows[r]["closed"].ToString();
                    row.Cells["dgvl_closed_date"].Value = gm.toDateString(dt.Rows[r]["closed_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl_closed_time"].Value = dt.Rows[r]["closed_time"].ToString();
                    row.Cells["dgvl_closed_by"].Value = dt.Rows[r]["closed_by"].ToString();
                    // row.Cells["dgvl_costunit"].Value = dt.Rows[r]["purc_unit"].ToString();
                    i++;
                }
                try 
                {
                    dgv_list.ClearSelection();
                    dgv_list.Rows[0].Selected = true;
                }
                catch { }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message); }*/
        }



        void cbo_costCenter_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            dtp_poDate.Enabled = true;
            isnew = true;
            goto_win2();
        }

        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "", canc = "";
            isnew = false;

            try
            {
                r = dgv_list.CurrentRow.Index;

                code = (dgv_list["dgvl_purc_ord", r].Value??"").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    try
                    {
                        canc = dgv_list["dgvl_cancel", r].Value.ToString();
                    }
                    catch { }

                    if (canc == "Y")
                    {
                        MessageBox.Show("PO# " + code + " is already cancelled. Can not be updated.");
                    }
                    else
                    {
                        try
                        {
                            txt_invoiceno.Text = code;

                            txt_reference.Text = dgv_list["dgvl_reference", r].Value.ToString();
                            txt_pr.Text = dgv_list["dgvl_pr_no", r].Value.ToString();
                            txt_supplierid.Text = dgv_list["dgvl_supl_code", r].Value.ToString();
                            cbo_suppliername.Text = dgv_list["dgvl_supl_name", r].Value.ToString();
                            cbo_payment.Text = dgv_list["dgvl_payment", r].Value.ToString();
                            dtp_poDate.Value = Convert.ToDateTime(dgv_list["dgvl_rels_date", r].Value.ToString());
                            dtp_deliveryDate.Value = Convert.ToDateTime(dgv_list["dgvl_delv_date", r].Value.ToString());
                            txt_notes.Text = dgv_list["dgvl_notes", r].Value.ToString();
                            txt_requested_by.Text = dgv_list["dgvl_request_by", r].Value.ToString();

                            txt_finalizedby.Text = dgv_list["dgvl_finalized_by", r].Value.ToString();
                            lbl_finalizedon.Text = dgv_list["dgvl_finalized_date", r].Value.ToString() + " " + dgv_list["dgvl_finalized_time", r].Value.ToString();
                            txt_approvedby.Text = dgv_list["dgvl_closed_by", r].Value.ToString();
                            lbl_approvedon.Text = dgv_list["dgvl_closed_date", r].Value.ToString() + " " + dgv_list["dgvl_closed_time", r].Value.ToString();
                        }
                        catch  {  }

                        disp_itemlist(code);
                        disp_total();
                        goto_win2();
                    }

                }
                else
                {
                    MessageBox.Show("No invoice selected.");
                }
            }
            catch
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void disp_itemlist(string code)
        {
            double net_price = 0.0;
            String qty = "", line_amount = "";
            try
            {
                DataTable dt = db.get_po_items(code);

                for (int i = 0; i < dt.Rows.Count; i++ )
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_qty", i].Value = qty = dt.Rows[i]["delv_qty"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["purc_unit"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[i]["discount"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = line_amount = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[i]["cnt_code"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[i]["scc_code"].ToString();
                    dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[i]["newprice"].ToString();

                    dgv_itemlist["dgvi_acct_code", i].Value = dt.Rows[i]["cht_code"].ToString();

                    //net_price = Convert.ToDouble(line_amount) / Convert.ToDouble(qty);
                    dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[i]["price"].ToString();

                    dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[i]["debt_code"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[i]["ln_vat"].ToString();

                    dgv_itemlist["dgvi_sccdesc", i].Value = dt.Rows[i]["scc_desc"].ToString();
                    dgv_itemlist["dgvi_ccdesc", i].Value = dt.Rows[i]["cc_desc"].ToString();
                    dgv_itemlist["dgvi_pr_code", i].Value = dt.Rows[i]["pr_code"].ToString();
                    dgv_itemlist["dgvi_pr_ln", i].Value = dt.Rows[i]["pr_ln"].ToString();

                    dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[i]["lot_num"].ToString();

                    if (isnew == false)
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["delv_qty"].ToString();
                    }
                    else
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    }
                }
            }
            catch { }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            String code = "", itemcode = "", itemqty = "0.00";
            int r = -1;

            if (dgv_list.Rows.Count > 1)
            {
                try
                {
                    r = dgv_list.CurrentRow.Index;

                    if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString().Equals("Y"))
                    {
                        MessageBox.Show("Invoice already cancelled."); //PO already cancelled.
                    }
                    else if ((dgv_list["dgvl_closed", r].Value ?? "").ToString().Equals("Y"))
                    {
                        MessageBox.Show("Invoice already finalized."); //PO already finalized.
                    }
                    else
                    {
                        code = dgv_list["dgvl_purc_ord", r].Value.ToString();

                        DialogResult  dialogResult = MessageBox.Show("Are you sure you want to cancel this P.O.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (db.UpdateOnTable("purhdr", "supl_name='CANCELLED' || '-' ||supl_name, cancel='Y'", "purc_ord='" + code + "'"))
                            {
                                db.DeleteOnTable("purlne", "purc_ord='" + code + "'");
                                disp_list();
                            }
                            else
                            {
                                MessageBox.Show("No invoice selected.");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("No invoice selected.");
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            String code, t_date;
            Report rpt = new Report();

            try
            {
                int r = dgv_list.CurrentRow.Index;

                code = this.get_dgv_list_value("dgvl_purc_ord", r);
                String supl = this.get_dgv_list_value("dgvl_supl_name", r);
                String supl_code = this.get_dgv_list_value("dgvl_supl_code", r);
                String payment = this.get_dgv_list_value("dgvl_payment", r);
                String ddate = this.get_dgv_list_value("dgvl_delv_date", r);
                String notes = this.get_dgv_list_value("dgvl_notes", r);
                String reference = this.get_dgv_list_value("dgvl_reference", r);
                String pr_no = this.get_dgv_list_value("dgvl_pr_no", r);
                String reqby = this.get_dgv_list_value("dgvl_request_by", r);
                String preparedby = this.get_dgv_list_value("dgvl_recipient", r);
                String finalizedby = this.get_dgv_list_value("dgvl_finalized_by", r);
                String approvedby = this.get_dgv_list_value("dgvl_closed_by", r);
                
                t_date = this.get_dgv_list_value("dgvl_t_date", r);

                if(String.IsNullOrEmpty(code) == false)
                {
                    if (db.UpdateOnTable("purhdr", "printed='Y'", "purc_ord='" + code + "'"))
                    {
                        rpt.print_purchaseorder(code, pr_no, supl, payment, ddate, notes, t_date, supl_code, reference, reqby, preparedby, finalizedby, approvedby);

                        rpt.Show();
                        disp_list();
                    }                    
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private String get_dgv_list_value(String col, int row)
        {
            String val = "";

            try
            {
                val = dgv_list[col, row].Value.ToString();
            }
            catch (Exception er)
            {

            }

            return val;
        }
       
        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            int lastrow = 0;
            isnew_item = true;
            isTextItem = false;
            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count-2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                    inc_lnno();
                }

                else
                {
                    if (dgv_itemlist.Rows.Count == 1)
                    {
                        lnno_last = 0;
                        inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_itemlist.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                        inc_lnno();
                    }
                }
            }
            catch { }

            _frm_ipr = new z_enter_item(this, isnew_item, lnno_last);
            _frm_ipr.ShowDialog();
        }

        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }

        public void set_dgv_itemlist(DataTable dt)
        {
            int i = 0;

            try
            {
                if (isnew_item)
                {
                    i = dgv_itemlist.Rows.Add();
                }
                else
                {
                    i = dgv_itemlist.CurrentRow.Index;
                }

                if (isTextItem)
                {

                    //dgvi_costunit
                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["dgvi_ln"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                    dgv_itemlist["dgvi_vatcode", i].Value = "";
                    //dgv_itemlist["dgvi_vattype", i].Value = db.get_vat_desc(dt.Rows[0]["vat_code"].ToString());
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                    dgv_itemlist["dgvi_notes", i].Value = dt.Rows[0]["dgvi_notes"].ToString();
                    //dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_discpct", i].Value = "";
                    dgv_itemlist["dgvi_discamnt", i].Value = "0.00";
                    dgv_itemlist["dgvi_netprice", i].Value = "0.00";
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value = "0.00";
                    dgv_itemlist["dgvi_newregsellprice", i].Value = "0.00";
                    dgv_itemlist["dgvi_lotnum", i].Value = "";
                    dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[0]["dgvi_cccode"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[0]["dgvi_scc_code"].ToString();
                    dgv_itemlist["dgvi_acct_code", i].Value = dt.Rows[0]["dgvi_acct_code"].ToString();

                    dgv_itemlist["dgvi_ccdesc", i].Value = "";
                    dgv_itemlist["dgvi_sccdesc", i].Value = "";
                    dgv_itemlist["dgvi_pr_code", i].Value = "";
                    dgv_itemlist["dgvi_pr_ln", i].Value = "";


                }
                else
                {
                    //dgvi_costunit
                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["part_no"].ToString();
                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["ln_num"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["item_desc"].ToString();
                    dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[0]["vat_code"].ToString();
                    //dgv_itemlist["dgvi_vattype", i].Value = db.get_vat_desc(dt.Rows[0]["vat_code"].ToString());
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["inv_qty"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[0]["unit_id"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[0]["unit"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[0]["price"].ToString();
                    //dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_discpct", i].Value = dt.Rows[0]["disc_pct"].ToString();
                    dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[0]["disc_amt"].ToString();
                    dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[0]["netprice"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[0]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[0]["ln_vat"].ToString();
                    dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[0]["newprice"].ToString();
                    dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[0]["lot_no"].ToString();
                    dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[0]["cc_code"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[0]["scc_code"].ToString();
                    dgv_itemlist["dgvi_ccdesc", i].Value = dt.Rows[0]["cc_desc"].ToString();
                    dgv_itemlist["dgvi_sccdesc", i].Value = dt.Rows[0]["scc_desc"].ToString();
                    dgv_itemlist["dgvi_acct_code", i].Value = dt.Rows[0]["acct_code"].ToString();

                    dgv_itemlist["dgvi_pr_code", i].Value = "";
                    dgv_itemlist["dgvi_pr_ln", i].Value = "";
                
                }

                if (isnew_item)
                {
                    inc_lnno();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Exception : " + ex.Message);
            }

            disp_total();
        }

        public void set_dgv_fromPO(String pr_code)
        {
            DataTable dt = null;
            String referencetext = "";
            int i = 0;

            try
            {
                if(dgv_itemlist.Rows.Count > 1)
                {
                    MessageBox.Show("Invalid action. One PO must only have one PR.");
                }
                else
                {
                    dt = db.get_purhace_request_items_forPOonly(pr_code);
                    
                    if (dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("All items of PR# " + pr_code + " already issued with PO.");
                    }
                    else
                    {
                        for (i = 0; dt.Rows.Count > i; i++)
                        {
                            inc_lnno();
                            i = dgv_itemlist.Rows.Add();

                            dgv_itemlist["dgvi_lnno", i].Value = lnno_last;
                            dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                            dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                            dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                            dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["quantity"].ToString();
                            dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                            dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["price"].ToString();
                            dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                            dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[i]["cnt_code"].ToString();

                            dgv_itemlist["dgvi_acct_code", i].Value = dt.Rows[i]["cht_code"].ToString();

                            dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[i]["cnt_code"].ToString();
                            dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[i]["scc_code"].ToString();

                            dgv_itemlist["dgvi_notes", i].Value = dt.Rows[i]["addl_desc"].ToString();
                            dgv_itemlist["dgvi_discpct", i].Value = "0.00";
                            dgv_itemlist["dgvi_discamnt", i].Value = "0.00";
                            dgv_itemlist["dgvi_netprice", i].Value = "0.00";
                            dgv_itemlist["dgvi_lnvat", i].Value = "0.00";
                            dgv_itemlist["dgvi_vatcode", i].Value = "N";
                            dgv_itemlist["dgvi_newregsellprice", i].Value = "0.00";
                            dgv_itemlist["dgvi_lotnum", i].Value = ""; 
                            dgv_itemlist["dgvi_sccdesc", i].Value = dt.Rows[i]["scc_desc"].ToString();
                            dgv_itemlist["dgvi_ccdesc", i].Value = dt.Rows[i]["cc_desc"].ToString();
                            dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["quantity"].ToString();
                            dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["purc_unit"].ToString();

                            dgv_itemlist["dgvi_pr_code", i].Value = dt.Rows[i]["pr_code"].ToString();

                            dgv_itemlist["dgvi_pr_ln", i].Value = dt.Rows[i]["ln_num"].ToString();

                            referencetext = dt.Rows[i]["pr_code"].ToString();
                            txt_requested_by.Text = get_requestedby(referencetext);
                        }

                    }
                    if (String.IsNullOrEmpty(txt_reference.Text))
                    {
                        txt_reference.Text = "";
                        txt_pr.Text = referencetext; 
                    }
                    else
                    {
                        txt_reference.Text = txt_reference.Text + " / " + referencetext;
                    }
                }                
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private String get_requestedby(String pr_code)
        {
            DataTable dt = db.QueryOnTableWithParams("prhdr", "request_by", "pr_code='" + pr_code + "'", "");
            String name = "";

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    name = dt.Rows[i][0].ToString();
                }
            }
            catch (Exception) { }

            return name;
        }

        public void disp_total()
        {
            Double total = 0.00;
            double total_vat = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamnt", i].Value.ToString());
                    //dgvi_lnvat
                    //dgvl_lnamnt_total
                }
            }
            catch (Exception) { }

            lbl_invoice_total.Text = gm.toAccountingFormat(total);
        }

        public String get_dgvi_lnno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

            return val;
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {

            int r = 0;
            int cur_index = 0;
            isnew_item = false;
            string item_unit = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;
                    if (dgv_itemlist["dgvi_itemcode", r].Value.ToString().ToLower().Equals("text-item"))
                    {
                        isTextItem = true;
                        z_enter_item_simple frm = new z_enter_item_simple(this, false, (int)double.Parse(dgv_itemlist["dgvi_lnno", r].Value.ToString()));
                        frm.ShowDialog();
                    }
                    else
                    {
                        //r = dgv_itemlist.CurrentRow.Index;
                        //cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());
                        isTextItem = false;
                        _frm_ipr = new z_enter_item(this, isnew_item, lnno_last, dgv_itemlist.CurrentRow.Index);
                        _frm_ipr.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    if (isnew == false)
                    {
                        code = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                        qty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();
                    }

                    dgv_itemlist.Rows.RemoveAt(r);

                    disp_total();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception er) { MessageBox.Show("No item selected.\n" + er.Message); }
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String code, supp_id, supp_name, reference, terms, rels_date, delv_date, notes, request_by,pr_no;
            String notes2 = "";
            String col = "", val = "";
            String notificationText = "", notifyadd = "";
            String table = "purhdr";
            String tableln = "purlne";
            int r;

            if (String.IsNullOrEmpty(cbo_suppliername.Text))
            {
                MessageBox.Show("Please select the supplier.");
            }
            else if (cbo_payment.SelectedIndex == -1) 
            {
                MessageBox.Show("Please select the payment term field.");
            }
            else if (String.IsNullOrEmpty(txt_reference.Text))
            {
                MessageBox.Show("Please type a reference.");
            }
            else if (dgv_itemlist.Rows.Count <= 1)
            {
                MessageBox.Show("There no items to purchase. Please entry items.");
            }
            else
            {
                code = txt_invoiceno.Text;
                reference = txt_reference.Text;
                rels_date = dtp_poDate.Value.ToString("yyyy-MM-dd");
                delv_date = dtp_deliveryDate.Value.ToString("yyyy-MM-dd");
                terms = cbo_payment.SelectedValue.ToString();
                supp_id = txt_supplierid.Text;
                supp_name = cbo_suppliername.Text;
                pr_no = txt_pr.Text;
                notes = txt_notes.Text;
                //notes2 = rtxt_notes.Text;
                request_by = txt_requested_by.Text;

                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("purc_ord");
                    col = "purc_ord, supl_code, supl_name, reference, rels_date, delv_date, pay_code, t_date, t_time, request_by, notes,pr_no,cancel, recipient";
                    val = "'" + code + "', " + db.str_E(supp_id) + ", " + db.str_E(supp_name) + ", " + db.str_E(reference) + ", '" + rels_date + "', '" + delv_date + "', '" + terms + "', '" + db.get_systemdate("") + "', '" + db.get_systemtime() + "', '" + request_by + "', " + db.str_E(notes) + ", '" + pr_no + "','N', '" + GlobalClass.username +"'" ;

                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE ORDER");
                            db.set_pkm99("purc_ord", db.get_nextincrementlimitchar(code, 8));
                            success = true;
                        }
                        else
                        {
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "purc_ord='" + code + "'");
                        db.DeleteOnTable(tableln, "purc_ord='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";
                    col = "purc_ord='" + code +
                        "', pr_no='" + pr_no + 
                        "', supl_code=" + db.str_E(supp_id) + 
                        ", supl_name=" + db.str_E(supp_name) + 
                        ", reference=" + db.str_E(reference) + 
                        ", rels_date='" + rels_date + 
                        "', delv_date='" + delv_date + 
                        "', pay_code='" + terms + 
                        "', t_date='" + db.get_systemdate("") + 
                        "', t_time='" + db.get_systemtime() + 
                        "', request_by='" + request_by +
                        "', recipient='" + GlobalClass.username +
                        "', notes2= " + db.str_E(notes2) +
                        ", notes= " + db.str_E(notes) + "";

                    if (db.UpdateOnTable(table, col, "purc_ord='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "purc_ord='" + code + "'");

                        notifyadd = add_items(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE ORDER");
                            success = true;
                        }
                        else
                        {
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
            }

            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            }
          
        }

        private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_vatcode = "", i_qty = "", i_costunitid = "", i_costprice = "", i_discamnt = "", i_lnamnt = "", i_newregsellprice = "", i_cccode = "", i_scc_code = "", i_newprice = "", i_pr_code = "", i_pr_ln = "", i_part_no = "", i_ln_vat = "00.00", i_lotnum="";
            String val2 = "";
            String col2 = "";
            String cht_code = "";
            String cnt_code = "", scc_code = "";
            col2 = "purc_ord, ln_num, item_code, item_desc, purc_unit, delv_qty, discount, price, ln_amnt, cnt_code,  scc_code, debt_code, ln_vat,newprice, pr_code, pr_ln,part_no,lot_num,cht_code";

            for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
            {
                i_lnno = dgv_itemlist["dgvi_lnno", r].Value.ToString();
                i_part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                i_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_qty", r].Value.ToString()).ToString();
                i_costprice = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_costprice", r].Value.ToString()).ToString("0.00");
                i_discamnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_discamnt", r].Value.ToString()).ToString();
                i_lnamnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamnt", r].Value.ToString()).ToString("0.00");
                i_cccode = dgv_itemlist["dgvi_cccode", r].Value.ToString();
                i_scc_code = dgv_itemlist["dgvi_scc_code", r].Value.ToString();
                cht_code = dgv_itemlist["dgvi_acct_code", r].Value.ToString();
                i_vatcode = dgv_itemlist["dgvi_vatcode", r].Value.ToString();
                i_costunitid = dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                i_newprice = dgv_itemlist["dgvi_newregsellprice", r].Value.ToString();

                i_pr_code = dgv_itemlist["dgvi_pr_code", r].Value.ToString();
                i_pr_ln = dgv_itemlist["dgvi_pr_ln", r].Value.ToString();

                i_lotnum = dgv_itemlist["dgvi_lotnum", r].Value.ToString();

                if (i_vatcode != "N")
                {
                    i_ln_vat = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnvat", r].Value.ToString()).ToString();
                }
                if (i_newregsellprice == "")
                {
                    i_newregsellprice = "0";
                }

                val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_costunitid + "', '" + gm.toNormalDoubleFormat(i_qty).ToString("0.00") + "', '" + gm.toNormalDoubleFormat(i_discamnt).ToString("0.00") + "', '" + gm.toNormalDoubleFormat(i_costprice).ToString("0.00") + "', '" + gm.toNormalDoubleFormat(i_lnamnt) + "', '" + i_cccode + "', '" + i_scc_code + "', '" + i_vatcode + "'," + "'" + i_ln_vat + "'," + "'" + gm.toNormalDoubleFormat(i_newprice).ToString("0.00") + "','" + i_pr_code + "','" + i_pr_ln + "','" + i_part_no + "','" + i_lotnum + "','" + cht_code + "'";

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    notificationText += Environment.NewLine + i_itemdesc;
                }
                else
                {
                    notificationText = null;
                }
            }

            return notificationText;
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
        }

        private void frm_clear()
        {
            try
            {
                txt_invoiceno.Text = "";
                txt_reference.Text = "";
                txt_notes.Text = "";
                txt_supplierid.Text = "";
                cbo_suppliername.Text = "";
                txt_suppliername.Text = "";
                txt_finalizedby.Text = "";
                txt_approvedby.Text = "";
                lbl_approvedon.Text = "";
                lbl_finalizedon.Text = "";
                cbo_payment.SelectedIndex = -1;
                txt_requested_by.Text = "";
                dtp_deliveryDate.Value = Convert.ToDateTime(db.get_systemdate(""));
                dtp_poDate.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_invoice_total.Text = "0.00";

                dgv_itemlist.Rows.Clear();
                txt_pr.Text = "";
            }
            catch (Exception) { }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            if (dgv_itemlist.Rows.Count > 1)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit without saving? You may loose some data.", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    goto_win1();
                    frm_clear();
                }
            }
            else
            {
                goto_win1();
                frm_clear();
            }
        }

        private void btn_PRitems_Click(object sender, EventArgs e)
        {           
            int lastrow = 0;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                    //inc_lnno();
                }

                else
                {
                    if (dgv_itemlist.Rows.Count == 1)
                    {
                        lnno_last = 0;
                       // inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_itemlist.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                        //inc_lnno();
                    }
                }
            }
            catch (Exception) { }

            z_add_item_from_PR pr = new z_add_item_from_PR(this, lnno_last);

            pr.ShowDialog();
        }


        public String get_dgvi_itemcode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_itemcode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_itemdesc(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_itemdesc", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_qty(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_qty", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_costunitid(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costunitid", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_costprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_discpct(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_discpct", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_discamnt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_discamnt", currow].Value.ToString();

            return val;
        }
        public String get_partno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_part_no", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_netprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_netprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnamnt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnamnt", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_vatcode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_vatcode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnvat(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnvat", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_newregsellprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_newregsellprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lotnum(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lotnum", currow].Value.ToString();

            return val;
        }

        public DateTime get_dgvi_expiry(int currow)
        {
            DateTime val;

            val = Convert.ToDateTime(dgv_itemlist["dgvi_expiry", currow].Value.ToString());

            return val;
        }

        public String get_dgvi_cccode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_cccode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_scc_code(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_scc_code", currow].Value.ToString();

            return val;
        }
                
        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
            /*Double net_price = 0.0;
            DataTable dt = new DataTable();
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

                // DataTable dt = db.QueryOnTableWithParams("purhdr", "*", "t_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'", " ORDER BY purc_ord ASC");
                if (comboBox1.Text == string.Empty)
                {
                    string ss = "select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where purc_ord LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR reference LIKE '" + textBox15.Text + "%' OR   pay_code LIKE '" + textBox15.Text + "%' OR recipient LIKE '" + textBox15.Text + "%' OR request_by LIKE '" + textBox15.Text + "%' OR notes LIKE '" + textBox15.Text + "%' OR pr_no LIKE '" + textBox15.Text + "%'  AND p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC";
                    dt = db.QueryBySQLCode("select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where purc_ord LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR reference LIKE '" + textBox15.Text + "%' OR   pay_code LIKE '" + textBox15.Text + "%' OR recipient LIKE '" + textBox15.Text + "%' OR request_by LIKE '" + textBox15.Text + "%' OR notes LIKE '" + textBox15.Text + "%' OR pr_no LIKE '" + textBox15.Text + "%'  AND p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC");
                }
                else if (comboBox1.Text == "clear filter")
                {
                    string ss = "select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC";
                    dt = db.QueryBySQLCode("select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC");

                }
                else {
                    string ss = "select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where " + comboBox1.Text + " LIKE '" + textBox15.Text + "%' p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC";
                    dt = db.QueryBySQLCode("select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where "+comboBox1.Text+" LIKE '"+textBox15.Text+"%' AND p.t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY purc_ord ASC");
                    
                }
                int i = 0;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                    row.Cells["dgvl_supl_name"].Value = dt.Rows[r]["supl_name"].ToString();
                    row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                    row.Cells["dgvl_delv_date"].Value = gm.toDateString(dt.Rows[r]["delv_date"].ToString(), "");
                    row.Cells["dgvl_rels_date"].Value = gm.toDateString(dt.Rows[r]["rels_date"].ToString(), "");
                    row.Cells["dgvl_printed"].Value = dt.Rows[r]["printed"].ToString();
                    row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                    row.Cells["dgvl_request_by"].Value = dt.Rows[r]["request_by"].ToString();
                    row.Cells["dgvl_payment"].Value = dt.Rows[r]["mp_desc"].ToString();
                    row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                    row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                    row.Cells["dgvl_supl_code"].Value = dt.Rows[r]["supl_code"].ToString();
                    row.Cells["dgvl_pay_code"].Value = dt.Rows[r]["pay_code"].ToString();
                    row.Cells["dgvl_notes"].Value = dt.Rows[r]["notes"].ToString();
                    row.Cells["dgvl_pr_no"].Value = dt.Rows[r]["pr_no"].ToString();
                    // row.Cells["dgvl_costunit"].Value = dt.Rows[r]["purc_unit"].ToString();
                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message); }*/
        }

        

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }


        public void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            disp_list();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            if (dtp_to.Value <= dtp_frm.Value)
            {
                MessageBox.Show("'TO' date must not be lesser or equal to 'FROM' date");
            }
            else
                disp_list();
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

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

            int rowIndex = -1;
            String typname = "dgvl_supl_name";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
                {
                    typname = "dgvl_purc_ord";
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    typname = "dgvl_supl_name";
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    typname = "dgvl_reference";
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    typname = "dgvl_pr_no";
                }

                dgv_list.ClearSelection();
                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue)).First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        private void btn_addtextitem_Click(object sender, EventArgs e)
        {
            int lastrow = 0;
            isnew_item = true;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                    inc_lnno();
                }

                else
                {
                    if (dgv_itemlist.Rows.Count == 1)
                    {
                        lnno_last = 0;
                        inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_itemlist.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                        inc_lnno();
                    }
                }
            }
            catch { }
            try
            {
                isnew_item = true;
                isTextItem = true;
                z_enter_item_simple frm = new z_enter_item_simple(this, true, lnno_last);
                frm.ShowDialog();
            }
            catch { }
        }

        private void btn_finalized_Click(object sender, EventArgs e)
        {
            verifiedClerk(GlobalClass.username, "F");
        }

        private void btn_approved_Click(object sender, EventArgs e)
        {
            verifiedClerk(GlobalClass.username, "A");
        }

        public void verifiedClerk(String user, String type)
        {
            DialogResult dialogResult;
            String code = "";
            int r = -1;
            String cdate = "", ctime = "", userid = "";
            String str_approved = "approved";

            if (String.IsNullOrEmpty(user) == false)
            {
                try
                {
                    if (dgv_list.Rows.Count > 1)
                    {
                        r = dgv_list.CurrentRow.Index;

                        if (type == "F")
                        {
                            str_approved = "finalized";
                        }

                        if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString().Equals("Y"))
                        {
                            MessageBox.Show("Invoice already cancelled."); //PO already cancelled.
                        }
                        else if (type == "A" && (dgv_list["dgvl_closed", r].Value ?? "").ToString().Equals("Y"))
                        {
                            MessageBox.Show("Invoice already approved."); //PO already finalized.
                        }
                        else if (type == "F" && (dgv_list["dgvl_finalized", r].Value ?? "").ToString().Equals("Y"))
                        {
                            MessageBox.Show("Invoice already finalized."); //PO already finalized.
                        }
                        else
                        {
                            code = dgv_list["dgvl_purc_ord", r].Value.ToString();

                            dialogResult = MessageBox.Show("Are you sure you want to " + str_approved + " this P.O.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {
                                i_PO_Password frm = new i_PO_Password(this);
                                frm.ShowDialog();

                                if (ok == "ok")
                                {
                                    cdate = db.get_systemdate("yyyy-MM-dd");
                                    ctime = db.get_systemtime();
                                    userid = GlobalClass.username;

                                    if (type == "A")
                                    {
                                        if (db.UpdateOnTable("purhdr", "closed='Y', closed_date='" + cdate + "', closed_time='" + ctime + "', closed_by='" + userid + "'", "purc_ord='" + code + "'"))
                                        {
                                            MessageBox.Show("The PO#" + code + " has been successfully " + str_approved + ".");
                                        }
                                    }
                                    else if (type == "F")
                                    {
                                        if (db.UpdateOnTable("purhdr", "finalized='Y', finalized_date='" + cdate + "', finalized_time='" + ctime + "', finalized_by='" + userid + "'", "purc_ord='" + code + "'"))
                                        {
                                            MessageBox.Show("The PO#" + code + " has been successfully " + str_approved + ".");
                                        }
                                    }

                                    disp_list();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No invoice selected.");
                    }
                }
                catch { MessageBox.Show("No invoice selected."); }
            }
        }
        
        private void btn_supplier_Click(object sender, EventArgs e)
        {
            m_suppliers frm = new m_suppliers (this, true);
            frm.ShowDialog();
        }

        private void cbo_suppliername_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_supplierid.Text = (cbo_suppliername.SelectedValue??"").ToString();
        }

        public void refresh_customerlist()
        {
            cbo_suppliername.DataSource = db.get_supplierlist_asc_name();
            cbo_suppliername.DisplayMember = "c_name";
            cbo_suppliername.ValueMember = "c_code";
            cbo_suppliername.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (bgworker.IsBusy)
            {
                bgworker_cancel = false;
                bgworker.CancelAsync();
            }
        }
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                dgv_list.Invoke(new Action(() =>
                {
                    dgv_list.Rows.Clear();
                }));
            }
            catch { }
            try
            {
                DataTable dt = get_dgv_list();
                
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    if (bgworker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    int i = -1;
                    DataGridViewRow row = new DataGridViewRow();
                    dgv_list.Invoke(new Action(() => {
                        i = dgv_list.Rows.Add();
                        row = dgv_list.Rows[i];
                    }));

                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                        row.Cells["dgvl_supl_name"].Value = dt.Rows[r]["supl_name"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                        row.Cells["dgvl_delv_date"].Value = gm.toDateString(dt.Rows[r]["delv_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_rels_date"].Value = gm.toDateString(dt.Rows[r]["rels_date"].ToString(), "");
                        row.Cells["dgvl_printed"].Value = dt.Rows[r]["printed"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                        row.Cells["dgvl_request_by"].Value = dt.Rows[r]["request_by"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_payment"].Value = dt.Rows[r]["mp_desc"].ToString(); row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                        row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgvl_supl_code"].Value = dt.Rows[r]["supl_code"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_pay_code"].Value = dt.Rows[r]["pay_code"].ToString();
                        row.Cells["dgvl_notes"].Value = dt.Rows[r]["notes"].ToString();
                        row.Cells["dgvl_notes2"].Value = dt.Rows[r]["notes2"].ToString();
                        row.Cells["dgvl_pr_no"].Value = dt.Rows[r]["pr_no"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        try 
                        {
                            row.Cells["dgvl_finalized"].Value = dt.Rows[r]["finalized"].ToString();
                            row.Cells["dgvl_finalized_date"].Value = gm.toDateString(dt.Rows[r]["finalized_date"].ToString(), "MM/dd/yyyy");
                            row.Cells["dgvl_finalized_time"].Value = dt.Rows[r]["finalized_time"].ToString();
                            row.Cells["dgvl_finalized_by"].Value = dt.Rows[r]["finalized_by"].ToString();
                        }
                        catch { }
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        try
                        {
                            row.Cells["dgvl_closed"].Value = dt.Rows[r]["closed"].ToString();
                            row.Cells["dgvl_closed_date"].Value = gm.toDateString(dt.Rows[r]["closed_date"].ToString(), "MM/dd/yyyy");
                            row.Cells["dgvl_closed_time"].Value = dt.Rows[r]["closed_time"].ToString();
                            row.Cells["dgvl_closed_by"].Value = dt.Rows[r]["closed_by"].ToString();
                        }
                        catch { }
                    }));
                }
                try
                {
                    dgv_list.ClearSelection();
                    dgv_list.Rows[0].Selected = true;
                }
                catch { }

            }
            catch (Exception er) {  }
        }


        private DataTable get_dgv_list()
        {
            DataTable dt = new DataTable();
            String WHERE = "";
            String ORDERBY = "ORDER BY purc_ord DESC";

            String search_text = textBox15.getText();
            int selected_index = comboBox1.getSelectedIndex();

            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR purc_ord LIKE '%" + search_text + "%')";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY supl_name, purc_ord  DESC";
                }
                else
                {
                    ORDERBY = "ORDER BY purc_ord DESC";
                }
            }
            else if (selected_index == 0)
            {
                WHERE = " AND purc_ord LIKE '%" + search_text + "%'";
            }
            else if (selected_index == 1)
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR supl_code LIKE '%" + search_text + "%')";
                ORDERBY = "ORDER BY supl_name, purc_ord  DESC";
            }
            else if (selected_index == 2)
            {
                WHERE = " AND reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY reference, purc_ord DESC";
            }
            else if (selected_index == 3)
            {
                WHERE = " AND pr_no LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY pr_no, purc_ord DESC";
            }

            dt = db.QueryBySQLCode("select p.*, m.* from rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code where p.t_date between " + "'" + dtp_frm.getStringDate() + "' AND '" + dtp_to.getStringDate() + "'  " + WHERE + " " + ORDERBY);
            
            return dt;
        }

        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bgworker_cancel)
            {
                bgworker.RunWorkerAsync();
                bgworker_cancel = false;
            }
        }
    }
}
