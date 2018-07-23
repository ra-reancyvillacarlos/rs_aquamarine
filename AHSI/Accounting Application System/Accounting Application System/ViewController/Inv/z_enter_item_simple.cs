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
    public partial class z_enter_item_simple : Form
    {
        i_StockAdjustment _frm_sa;
        i_StockIssuance _frm_si;
        i_StockTransfer _frm_st;
        i_purchaseRequest _frm_pr;
        i_PO _frm_po;
        m_assembleditem _frm_ai;
        s_sub_items _frm_subi;
        s_RepairOrder _frm_ro;
        //s_Appointment _frm_ap;
        s_Job_Quotation frm_job_quotation;
        dbSales db;
        GlobalClass gc;
        GlobalMethod gm;
        //Load_Service ls;
        Boolean isnew = true, istextitem = false;
        String item_code = "", item_desc = "", ord_qty, unit_id, unit_desc, price, ln_amnt, part_no;
        int lnno = 0;
        int dgvrow = -1;
        String whatForm = "";
        Boolean thesameItem = false;
        public int update_item_index = 0;
        Boolean is_receiving = false;

        //if is_new == true, then lineno_or_rowindex is lineno; otherwise rowindex
        public z_enter_item_simple(i_PO frm_po, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            _frm_po = frm_po;
            isnew = is_new;
            whatForm = "O";
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex + 1;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }

            txt_lnno.Text = lnno.ToString();
            lbl_costprice.Text = "Regular Price";
            txt_costprice.ReadOnly = false;
            txt_itemcode.Text = "TEXT-ITEM";
            txt_partno.Text = "TEXT-ITEM";
            cbo_itemdesc.BackColor = Color.White;
            btn_search.Visible = false;
            txt_remark.Visible = false;
            label5.Visible = false;

            label6.Show();
            lbl_costcenter.Show();
            lbl_subcostcenter.Show();
            cbo_cost_center.Show();
            cbo_sub_cost_center.Show();
            cbo_account_link.Show();
        }

        public z_enter_item_simple(i_purchaseRequest frm_pr, Boolean is_new, int lineno_or_rowindex, Boolean textitem)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            istextitem = textitem;
            _frm_pr = frm_pr;
            isnew = is_new;
            whatForm = "R";

            if (istextitem)
            {
                txt_lnno.Text = lnno.ToString();
                lbl_costprice.Text = "Regular Price";
                txt_costprice.ReadOnly = false;
                txt_itemcode.Text = "TEXT-ITEM";
                txt_partno.Text = "TEXT-ITEM";
                cbo_itemdesc.BackColor = Color.White;
                cbo_itemdesc.Enabled = true;
                btn_search.Visible = false;

                label6.Show();
                lbl_costcenter.Show();
                lbl_subcostcenter.Show();
                cbo_cost_center.Show();
                cbo_sub_cost_center.Show();
                cbo_account_link.Show();
                this.BackColor = Color.DarkGoldenrod;
                cbo_purchunit.SelectedValue = "001";
                cbo_account_link.SelectedValue = "1104000-03";

                cbo_cost_center.SelectedValue = _frm_pr.cbo_costCenter.SelectedValue;
                cbo_sub_cost_center.SelectedValue = _frm_pr.cbo_subcostcenter.SelectedValue;
            }
            else
            {
                cbo_itemdesc.Enabled = false;
            }

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }
        public z_enter_item_simple(s_Job_Quotation frm, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            //lbl_costprice.Hide();
           // lbl_lnamt.Hide();
            //txt_costprice.Hide();
            //txt_lnamt.Hide();

            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            frm_job_quotation = frm;
            isnew = is_new;
            whatForm = "JQ";

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }
        public z_enter_item_simple(i_StockAdjustment frm_stkadj, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_saleunit(cbo_purchunit);

            _frm_sa = frm_stkadj;
            isnew = is_new;
            whatForm = "A";

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }

        //if is_new == true, then lineno_or_rowindex is lineno; otherwise rowindex
        public z_enter_item_simple(i_StockIssuance frm_stkiss, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            _frm_si = frm_stkiss;
            isnew = is_new;
            whatForm = "I";

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }

            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");
        }


        //if is_new == true, then lineno_or_rowindex is lineno; otherwise rowindex
        public z_enter_item_simple(i_StockTransfer frm_stktra, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            
            _frm_st = frm_stktra;
            isnew = is_new;
            whatForm = "T";
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            lbl_transpo_amnt.Visible = true;
            txt_transpo_amnt.Visible = true;

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }
        //if is_new == true, then lineno_or_rowindex is lineno; otherwise rowindex
        public z_enter_item_simple(s_RepairOrder frm_ro, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            //ls = new Load_Service();

            _frm_ro = frm_ro;
            isnew = is_new;
            whatForm = "RO";

            //ls.load_text_item(cbo_itemdesc);
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);

            cbo_itemdesc.DataSource = db.QueryOnTableWithParams("orlabor", "labor_desc", "", "ORDER BY labor_desc");
            cbo_itemdesc.DisplayMember = "labor_desc";
            cbo_itemdesc.ValueMember = "labor_desc";
            cbo_itemdesc.SelectedIndex = -1;

            cbo_discby.DataSource = db.QueryOnTableWithParams("x08", "uid, opr_name", "", "ORDER BY opr_name");
            cbo_discby.DisplayMember = "opr_name";
            cbo_discby.ValueMember = "uid";
            cbo_discby.SelectedIndex = -1;

            //gc.load_costcenter(cbo_cost_center);
            //gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }

            txt_lnno.Text = lnno.ToString();
            lbl_costprice.Text = "Regular Price";
            txt_costprice.ReadOnly = false;
            txt_itemcode.Text = "TEXT-ITEM";
            txt_partno.Text = "TEXT-ITEM";
            cbo_itemdesc.BackColor = Color.White;
            cbo_account_link.SelectedValue = "45200-01";
            cbo_purchunit.SelectedValue = "001";

            btn_search.Visible = false;
            txt_remark.Visible = false;
            label5.Visible = false;

            label6.Show();
            cbo_account_link.Show();

            label7.Show();
            label8.Show();
            txt_discamnt.Show();
            txt_discreason.Show();
            cbo_discby.Show();
        }
        /*
        public z_enter_item_simple(s_Appointment frm_ap, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            ls = new Load_Service();

            _frm_ap = frm_ap;
            isnew = is_new;
            whatForm = "AP";

            //ls.load_text_item(cbo_itemdesc);
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);

            cbo_itemdesc.DataSource = db.QueryOnTableWithParams("orlabor", "labor_desc", "", "ORDER BY labor_desc");
            cbo_itemdesc.DisplayMember = "labor_desc";
            cbo_itemdesc.ValueMember = "labor_desc";
            cbo_itemdesc.SelectedIndex = -1;

            cbo_discby.DataSource = db.QueryOnTableWithParams("x08", "uid, opr_name", "", "ORDER BY opr_name");
            cbo_discby.DisplayMember = "opr_name";
            cbo_discby.ValueMember = "uid";
            cbo_discby.SelectedIndex = -1;
            //gc.load_costcenter(cbo_cost_center);
            //gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }

            txt_lnno.Text = lnno.ToString();
            lbl_costprice.Text = "Regular Price";
            txt_costprice.ReadOnly = false;
            txt_itemcode.Text = "TEXT-ITEM";
            txt_partno.Text = "TEXT-ITEM";
            cbo_itemdesc.BackColor = Color.White;
            cbo_account_link.SelectedValue = "45200-01";
            cbo_purchunit.SelectedValue = "001";

            btn_search.Visible = false;
            txt_remark.Visible = false;
            label5.Visible = false;

            label6.Show();
            cbo_account_link.Show();

            label7.Show();
            label8.Show();
            txt_discamnt.Show();
            txt_discreason.Show();
            cbo_discby.Show();
        }*/
        public z_enter_item_simple(i_StockTransfer frm_stktra, Boolean is_new, int lineno_or_rowindex, Boolean is_reicive)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            this.is_receiving = is_reicive;
            _frm_st = frm_stktra;

            lbl_transpo_amnt.Visible = true;
            txt_transpo_amnt.Visible = true;

            if (this.is_receiving)
            {
                txt_partno.Enabled = false;
                txt_itemcode.Enabled = false;
                btn_search.Enabled = false;
                txt_qty.Enabled = false;
                cbo_purchunit.Enabled = false;
                txt_costprice.Enabled = false;
                txt_lnamt.Enabled = false;
                txt_remark.Enabled = false;
            }

            isnew = is_new;
            whatForm = "T";
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }

        public z_enter_item_simple(m_assembleditem frm, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            _frm_ai = frm;
            isnew = is_new;
            whatForm = "AI";
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            gc.load_costcenter(cbo_cost_center);
            gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }
        }

        public z_enter_item_simple(s_sub_items frm, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            //ls = new Load_Service();

            _frm_subi = frm;
            isnew = is_new;
            whatForm = "SI";

            //ls.load_text_item(cbo_itemdesc);
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_account_link);
            //gc.load_costcenter(cbo_cost_center);
            //gc.load_subcostcenter(cbo_sub_cost_center, "");

            if (is_new)
            {
                lnno = lineno_or_rowindex + 1;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
                load_fields_upd();
            }

            txt_lnno.Text = lnno.ToString();
            lbl_costprice.Text = "Regular Price";
            txt_costprice.ReadOnly = false;
            txt_itemcode.Text = "TEXT-ITEM";
            txt_partno.Text = "TEXT-ITEM";
            cbo_itemdesc.BackColor = Color.White;
            cbo_account_link.SelectedValue = "45200-01";
            cbo_purchunit.SelectedValue = "001";

            btn_search.Visible = false;
            txt_remark.Visible = false;
            label5.Visible = false;

            label6.Show();
            lbl_costcenter.Show();
            lbl_subcostcenter.Show();
            lbl_costcenter.Hide();
            lbl_subcostcenter.Hide();
            cbo_cost_center.Hide();
            cbo_sub_cost_center.Hide();
            cbo_account_link.Show();
        }

        private void z_enter_item_simple_Load(object sender, EventArgs e)
        {
            if (this.is_receiving == false)
            {
                lbl_rv.Visible = false;
                txt_receive_qty.Visible = false;
            }
            txt_lnno.Text = lnno.ToString();
            load_fields_upd();
            txt_qty.TextChanged +=txt_qty_TextChanged;
            txt_costprice.TextChanged +=txt_costprice_TextChanged;
        }

        private void load_fields_upd()
        {
            if (_frm_sa != null && isnew == false)
            {
                txt_lnno.Text = _frm_sa.get_dgvi_ln(dgvrow);
                txt_itemcode.Text = _frm_sa.get_dgvi_itemcode(dgvrow);
                cbo_itemdesc.Text = _frm_sa.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = _frm_sa.get_dgvi_qty(dgvrow);
                txt_costprice.Text = _frm_sa.get_dgvi_price(dgvrow);
                txt_lnamt.Text = _frm_sa.get_dgvi_lnamt(dgvrow);
                txt_partno.Text = _frm_sa.get_partno(dgvrow);
                txt_remark.Text = _frm_sa.get_dgvi_remark(dgvrow);
               
                cbo_purchunit.SelectedValue = _frm_sa.get_dgvi_unitid(dgvrow);
            }
            else if (_frm_si != null && isnew == false)
            {
                txt_lnno.Text = _frm_si.get_dgvi_ln(dgvrow);
                txt_itemcode.Text = _frm_si.get_dgvi_itemcode(dgvrow);
                cbo_itemdesc.Text = _frm_si.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = _frm_si.get_dgvi_qty(dgvrow);
                txt_costprice.Text = _frm_si.get_dgvi_price(dgvrow);
                txt_lnamt.Text = _frm_si.get_dgvi_lnamt(dgvrow);
                txt_partno.Text = _frm_si.get_dgvi_part_no(dgvrow);
                txt_remark.Text = _frm_si.get_dgvi_remark(dgvrow);

                cbo_purchunit.SelectedValue = _frm_si.get_dgvi_unitid(dgvrow);
            }
            else if (_frm_st != null && isnew == false)
            {
                txt_lnno.Text = _frm_st.get_dgvi_ln(dgvrow);
                txt_partno.Text = _frm_st.get_partno(dgvrow);
                txt_itemcode.Text = _frm_st.get_dgvi_itemcode(dgvrow);
                cbo_itemdesc.Text = _frm_st.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = _frm_st.get_dgvi_qty(dgvrow);
                txt_costprice.Text = _frm_st.get_dgvi_price(dgvrow);
                txt_lnamt.Text = _frm_st.get_dgvi_lnamt(dgvrow);
                txt_remark.Text = _frm_st.get_dgvi_notes(dgvrow);
                txt_transpo_amnt.Text = _frm_st.get_dgvi_transpo(dgvrow);
                

                if(is_receiving)
                {
                    txt_receive_qty.Text = _frm_st.get_dgvi_recqty(dgvrow);
                }
                cbo_purchunit.SelectedValue = _frm_st.get_dgvi_unitid(dgvrow);
            }
            else if (_frm_pr != null && isnew == false)
            {
                txt_partno.Text = _frm_pr.get_dgvi_part_no(dgvrow);
                txt_lnno.Text = _frm_pr.get_dgvi_ln(dgvrow);
                txt_itemcode.Text = _frm_pr.get_dgvi_itemcode(dgvrow);
                cbo_itemdesc.Text = _frm_pr.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = _frm_pr.get_dgvi_qty(dgvrow);
                txt_costprice.Text = _frm_pr.get_dgvi_price(dgvrow);
                txt_lnamt.Text = _frm_pr.get_dgvi_lnamt(dgvrow);
                txt_remark.Text = _frm_pr.get_dgvi_notes(dgvrow); 
                cbo_purchunit.SelectedValue = _frm_pr.get_dgvi_unitid(dgvrow);
            }
            else if (frm_job_quotation != null && isnew == false)
            {
                txt_partno.Text = frm_job_quotation.get_dgvi_part_no(dgvrow);
                txt_lnno.Text = frm_job_quotation.get_dgvi_ln(dgvrow);
                txt_itemcode.Text = frm_job_quotation.get_dgvi_itemcode(dgvrow);
                cbo_itemdesc.Text = frm_job_quotation.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = frm_job_quotation.get_dgvi_qty(dgvrow);
                txt_costprice.Text = frm_job_quotation.get_dgvi_price(dgvrow);
                txt_lnamt.Text = frm_job_quotation.get_dgvi_lnamt(dgvrow);
                txt_remark.Text = frm_job_quotation.get_dgvi_notes(dgvrow);
                cbo_purchunit.SelectedValue = frm_job_quotation.get_dgvi_unitid(dgvrow);
            }
            else if (_frm_subi != null && isnew == false)
            {
                txt_lnno.Text = dgvrow.ToString();
                txt_itemcode.Text = item_code.ToString();
                cbo_itemdesc.Text = item_desc.ToString();
                txt_qty.Text = ord_qty.ToString();
                txt_costprice.Text = price.ToString();
                txt_lnamt.Text = ln_amnt.ToString();
                txt_partno.Text = part_no.ToString();
                cbo_purchunit.SelectedValue = unit_id.ToString();
            }
            else if (_frm_ro != null && isnew == false)
            {
                int r = _frm_ro.dgv_itemlist.CurrentRow.Index ;

                txt_partno.Text = _frm_ro.dgv_itemlist["dgvi_part_no", r].Value.ToString();
                txt_lnno.Text = _frm_ro.dgv_itemlist["dgvli_lnno", r].Value.ToString();
                txt_itemcode.Text = _frm_ro.dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                cbo_itemdesc.Text = _frm_ro.dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                txt_qty.Text = _frm_ro.dgv_itemlist["dgvli_qty", r].Value.ToString();
                txt_costprice.Text = _frm_ro.dgv_itemlist["dgvli_sellprice", r].Value.ToString();
                txt_lnamt.Text = _frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString();
                txt_remark.Text = _frm_ro.dgv_itemlist["dgvli_remarks", r].Value.ToString();
                cbo_purchunit.SelectedValue = _frm_ro.dgv_itemlist["dgvli_unitid", r].Value.ToString();

                txt_discamnt.Text = (_frm_ro.dgv_itemlist["dgvli_discamt", r].Value??"").ToString();
                txt_discreason.Text = (_frm_ro.dgv_itemlist["dgvli_discreason", r].Value??"").ToString();
                cbo_discby.SelectedValue = (_frm_ro.dgv_itemlist["dgvli_discuser", r].Value??"").ToString();
                
            }
            /*else if (_frm_ap != null && isnew == false)
            {
                int r = _frm_ap.dgv_itemlist.CurrentRow.Index;

                txt_partno.Text = _frm_ap.dgv_itemlist["dgvi_part_no", r].Value.ToString();
                txt_lnno.Text = _frm_ap.dgv_itemlist["dgvli_lnno", r].Value.ToString();
                txt_itemcode.Text = _frm_ap.dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                cbo_itemdesc.Text = _frm_ap.dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                txt_qty.Text = _frm_ap.dgv_itemlist["dgvli_qty", r].Value.ToString();
                txt_costprice.Text = _frm_ap.dgv_itemlist["dgvli_sellprice", r].Value.ToString();
                txt_lnamt.Text = _frm_ap.dgv_itemlist["dgvli_lnamt", r].Value.ToString();
                txt_remark.Text = _frm_ap.dgv_itemlist["dgvli_remarks", r].Value.ToString();
                cbo_purchunit.SelectedValue = _frm_ap.dgv_itemlist["dgvli_unitid", r].Value.ToString();

                txt_discamnt.Text = (_frm_ap.dgv_itemlist["dgvli_discamt", r].Value??"").ToString();
                txt_discreason.Text = (_frm_ap.dgv_itemlist["dgvli_discreason", r].Value??"").ToString();
                cbo_discby.SelectedValue = (_frm_ap.dgv_itemlist["dgvli_discuser", r].Value??"").ToString();
                
            }*/
            else if (_frm_po != null && isnew == false)
            {
                int r = _frm_po.dgv_itemlist.CurrentRow.Index;

                txt_partno.Text = _frm_po.dgv_itemlist["dgvi_part_no", r].Value.ToString();
                txt_lnno.Text = _frm_po.dgv_itemlist["dgvi_lnno", r].Value.ToString();
                txt_itemcode.Text = _frm_po.dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                cbo_itemdesc.Text = _frm_po.dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                txt_qty.Text = _frm_po.dgv_itemlist["dgvi_qty", r].Value.ToString();
                txt_costprice.Text = _frm_po.dgv_itemlist["dgvi_costprice", r].Value.ToString();
                txt_lnamt.Text = _frm_po.dgv_itemlist["dgvi_lnamnt", r].Value.ToString();
                txt_remark.Text = (_frm_po.dgv_itemlist["dgvi_notes", r].Value??"").ToString();
                cbo_purchunit.SelectedValue = _frm_po.dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                cbo_cost_center.SelectedValue = _frm_po.dgv_itemlist["dgvi_cccode", r].Value.ToString();
                cbo_sub_cost_center.SelectedValue = _frm_po.dgv_itemlist["dgvi_scc_code", r].Value.ToString();
                cbo_account_link.SelectedValue = _frm_po.dgv_itemlist["dgvi_acct_code", r].Value.ToString();


            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, cbo_itemdesc.Text, "D");
            _frm_is.Show();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("dgvi_ln");
            dt.Columns.Add("dgvi_itemcode");
            dt.Columns.Add("dgvi_itemdesc");
            dt.Columns.Add("dgvi_unitdesc");
            dt.Columns.Add("dgvi_unitid");
            dt.Columns.Add("dgvi_qty");
            dt.Columns.Add("dgvi_price");
            dt.Columns.Add("dgvi_lnamt");
            dt.Columns.Add("dgvi_notes");
            dt.Columns.Add("dgvi_part_no");
            dt.Columns.Add("dgvi_transpo");
            dt.Columns.Add("dgvi_acct_code");
            dt.Columns.Add("dgvi_cccode");
            dt.Columns.Add("dgvi_scc_code");
            dt.Columns.Add("dgvi_discamnt");
            dt.Columns.Add("dgvi_discby");
            dt.Columns.Add("dgvi_discreason");

            if (whatForm == "RO")
            {
                dt.Columns.Add("dgvi_at_code");
            }
            

            if (is_receiving)
            {
                dt.Columns.Add("dgvi_received_qty");
            }

            //try
            //{
            if (String.IsNullOrEmpty(txt_qty.Text) || gm.toNormalDoubleFormat(txt_qty.Text) == 0.00)
            {
                MessageBox.Show("Quantity should not be equal to zero.");
            }
            else if (cbo_purchunit.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the unit.");
                cbo_purchunit.DroppedDown = true;
            }
            else if (gm.toNormalDoubleFormat(txt_discamnt.Text) > 0 && cbo_discby.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the discount by");
                cbo_discby.DroppedDown = true;
            }
            else if (gm.toNormalDoubleFormat(txt_discamnt.Text) > 0 && String.IsNullOrEmpty(txt_discreason.Text))
            {
                MessageBox.Show("Please input discount reason.");
            }
            else
            {
                dt.Rows.Add();

                dt.Rows[0]["dgvi_ln"] = txt_lnno.Text;
                dt.Rows[0]["dgvi_itemcode"] = txt_itemcode.Text;
                dt.Rows[0]["dgvi_itemdesc"] = cbo_itemdesc.Text;
                dt.Rows[0]["dgvi_unitdesc"] = cbo_purchunit.Text;
                dt.Rows[0]["dgvi_unitid"] = (cbo_purchunit.SelectedValue ?? "").ToString();
                dt.Rows[0]["dgvi_qty"] = gm.toAccountingFormat(txt_qty.Text);
                dt.Rows[0]["dgvi_price"] = gm.toAccountingFormat(txt_costprice.Text);
                dt.Rows[0]["dgvi_transpo"] = gm.toAccountingFormat(txt_transpo_amnt.Text);
                dt.Rows[0]["dgvi_lnamt"] = gm.toAccountingFormat(txt_lnamt.Text);
                dt.Rows[0]["dgvi_notes"] = txt_remark.Text;
                dt.Rows[0]["dgvi_part_no"] = txt_partno.Text;
                dt.Rows[0]["dgvi_acct_code"] = (cbo_account_link.SelectedValue ?? "").ToString();
                dt.Rows[0]["dgvi_cccode"] = (cbo_cost_center.SelectedValue ?? "").ToString();
                dt.Rows[0]["dgvi_scc_code"] = (cbo_sub_cost_center.SelectedValue ?? "").ToString();
                dt.Rows[0]["dgvi_discamnt"] = txt_discamnt.Text;
                dt.Rows[0]["dgvi_discby"] = (cbo_discby.SelectedValue ?? "").ToString();
                dt.Rows[0]["dgvi_discreason"] = txt_discreason.Text;

                if (whatForm == "RO")
                {
                    String at_code = "";

                    if (cbo_account_link.SelectedIndex != -1)
                        at_code = cbo_account_link.SelectedValue.ToString();

                    dt.Rows[0]["dgvi_at_code"] = at_code;
                }
            

                if (is_receiving)
                {
                    dt.Rows[0]["dgvi_received_qty"] = gm.toAccountingFormat(txt_receive_qty.Text);
                }

                if (whatForm == "A")
                {

                    _frm_sa.set_dgv_itemlist(dt);
                }
                else if (whatForm == "I")
                {
                    _frm_si.set_dgv_itemlist(dt);
                }
                else if (whatForm == "T")
                {
                    _frm_st.set_dgv_itemlist(dt);
                }
                //Purchase Request
                else if (whatForm == "R")
                {
                    _frm_pr.set_dgv_itemlist(dt);
                }
                else if (whatForm == "JQ")
                {
                    frm_job_quotation.set_dgv_itemlist(dt);
                }
                //Purchase Order
                else if (whatForm == "O")
                {
                    _frm_po.set_dgv_itemlist(dt);
                }
                //Assembled Item
                else if (whatForm == "AI")
                {
                    if (isnew)
                    {
                        int i = 0;

                        i = _frm_ai.dgv_itemlist.Rows.Add();
                        DataGridViewRow row = _frm_ai.dgv_itemlist.Rows[i];
                        row.Cells["dgvi_qty"].Value = gm.toAccountingFormat(txt_qty.Text);
                        row.Cells["dgvi_itemdesc"].Value = cbo_itemdesc.Text;
                        row.Cells["dgvi_partno"].Value = txt_partno.Text;
                        row.Cells["dgvi_price"].Value = gm.toAccountingFormat(txt_costprice.Text);
                        row.Cells["dgvi_unitdesc"].Value = cbo_purchunit.Text;
                        row.Cells["dgvi_itemcode"].Value = txt_itemcode.Text;
                        row.Cells["dgvi_lnamt"].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        row.Cells["remarks"].Value = txt_remark.Text;
                        row.Cells["dgvi_unitid"].Value = cbo_purchunit.SelectedValue.ToString();

                        /*

                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[0].Value = gm.toAccountingFormat(txt_qty.Text);
                        
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[1].Value = txt_itemdesc.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[2].Value = _frm_ai.cbo_saleunit.Text;
                        // _frm_ai.dgv_itemlist.Rows[lnno].Cells[2].Value = cbo_purchunit.SelectedValue.ToString();
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[3].Value = gm.toAccountingFormat(txt_costprice.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[4].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[5].Value = txt_itemcode.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[6].Value = cbo_purchunit.Text;
                         _frm_ai.dgv_itemlist.Rows[lnno].Cells[7].Value = txt_remark.Text;
                        */
                        _frm_ai.lnno_last = lnno + 1;

                        _frm_ai.disp_total();
                        i++;
                    }
                    else
                    {

                        _frm_ai.dgv_itemlist.Rows.RemoveAt(update_item_index);

                        int i = 0;

                        i = _frm_ai.dgv_itemlist.Rows.Add();
                        DataGridViewRow row = _frm_ai.dgv_itemlist.Rows[i];
                        row.Cells["dgvi_qty"].Value = gm.toAccountingFormat(txt_qty.Text);
                        row.Cells["dgvi_itemdesc"].Value = cbo_itemdesc.Text;
                        row.Cells["dgvi_partno"].Value = txt_partno.Text;
                        row.Cells["dgvi_price"].Value = gm.toAccountingFormat(txt_costprice.Text);
                        row.Cells["dgvi_unitdesc"].Value = cbo_purchunit.Text;
                        row.Cells["dgvi_itemcode"].Value = txt_itemcode.Text;
                        row.Cells["dgvi_lnamt"].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        row.Cells["remarks"].Value = txt_remark.Text;
                        row.Cells["dgvi_unitid"].Value = cbo_purchunit.SelectedValue.ToString();
                        _frm_ai.disp_total();
                        update_item_index = 0;
                        /*
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[0].Value = gm.toAccountingFormat(txt_qty.Text);

                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[1].Value = txt_itemdesc.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[2].Value = _frm_ai.cbo_saleunit.Text;
                        
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[3].Value = gm.toAccountingFormat(txt_costprice.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[4].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[5].Value = txt_itemcode.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[6].Value = cbo_purchunit.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[7].Value = txt_remark.Text;
                        */
                    }
                    // _frm_ai.set_dgv_itemlist(dt);
                }
                else if (whatForm == "SI")
                {
                    if (isnew)
                    {
                        int i = 0;

                        i = _frm_subi.dgv_subitem.Rows.Add();
                        DataGridViewRow row = _frm_subi.dgv_subitem.Rows[i];
                        row.Cells["dgv_sub_item_qty"].Value = gm.toAccountingFormat(txt_qty.Text);
                        row.Cells["dgv_sub_item_itemdesc"].Value = cbo_itemdesc.Text;
                        row.Cells["dgv_sub_item_partno"].Value = txt_partno.Text;
                        row.Cells["dgv_sub_item_costprice"].Value = gm.toAccountingFormat(txt_costprice.Text);
                        row.Cells["dgv_sub_item_unitdesc"].Value = cbo_purchunit.Text;
                        row.Cells["dgv_sub_item_itemcode"].Value = txt_itemcode.Text;
                        row.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        //row.Cells["remarks"].Value = txt_remark.Text;
                        row.Cells["dgv_sub_item_unitid"].Value = cbo_purchunit.SelectedValue.ToString();

                        //_frm_ai.lnno_last = lnno + 1;

                        //_frm_ai.disp_total();
                        i++;
                    }

                    else
                    {

                        _frm_subi.dgv_subitem.Rows.RemoveAt(update_item_index);

                        int x = 0;

                        x = _frm_subi.dgv_subitem.Rows.Add();
                        DataGridViewRow rows = _frm_subi.dgv_subitem.Rows[x];
                        rows.Cells["dgv_sub_item_qty"].Value = gm.toAccountingFormat(txt_qty.Text);
                        rows.Cells["dgv_sub_item_itemdesc"].Value = cbo_itemdesc.Text;
                        rows.Cells["dgv_sub_item_partno"].Value = txt_partno.Text;
                        rows.Cells["dgv_sub_item_costprice"].Value = gm.toAccountingFormat(txt_costprice.Text);
                        rows.Cells["dgv_sub_item_unitdesc"].Value = cbo_purchunit.Text;
                        rows.Cells["dgv_sub_item_itemcode"].Value = txt_itemcode.Text;
                        rows.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        // rows.Cells["remarks"].Value = txt_remark.Text;
                        rows.Cells["dgv_sub_item_unitid"].Value = cbo_purchunit.SelectedValue.ToString();
                        //_frm_subi.disp_total();
                        update_item_index = 0;
                        /*
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[0].Value = gm.toAccountingFormat(txt_qty.Text);

                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[1].Value = txt_itemdesc.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[2].Value = _frm_ai.cbo_saleunit.Text;
                        
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[3].Value = gm.toAccountingFormat(txt_costprice.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[4].Value = gm.toAccountingFormat(txt_lnamt.Text);
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[5].Value = txt_itemcode.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[6].Value = cbo_purchunit.Text;
                        _frm_ai.dgv_itemlist.Rows[lnno].Cells[7].Value = txt_remark.Text;
                        */
                    }
                }
                else if (whatForm == "RO")
                {

                    int r = 0;

                    if (isnew)
                    {
                        _frm_ro.dgv_itemlist.Rows.Add();
                        r = _frm_ro.dgv_itemlist.Rows.Count - 1;

                        _frm_ro.set_last_lnno(lnno);
                    }
                    else
                    {
                        r = _frm_ro.dgv_itemlist.CurrentRow.Index;
                        lnno = Convert.ToInt32(gm.toNormalDoubleFormat(txt_lnno.Text));
                    }

                    _frm_ro.dgv_itemlist["dgvli_lnno", r].Value = lnno;
                    _frm_ro.dgv_itemlist["dgvi_part_no", r].Value = dt.Rows[0]["dgvi_part_no"];
                    _frm_ro.dgv_itemlist["dgvli_itemcode", r].Value = dt.Rows[0]["dgvi_itemcode"];
                    _frm_ro.dgv_itemlist["dgvli_itemdesc", r].Value = dt.Rows[0]["dgvi_itemdesc"];
                    _frm_ro.dgv_itemlist["dgvli_qty", r].Value = dt.Rows[0]["dgvi_qty"];
                    _frm_ro.dgv_itemlist["dgvli_regprice", r].Value = dt.Rows[0]["dgvi_price"];
                    _frm_ro.dgv_itemlist["dgvli_sellprice", r].Value = dt.Rows[0]["dgvi_price"];
                    _frm_ro.dgv_itemlist["dgvli_lnamt", r].Value = dt.Rows[0]["dgvi_lnamt"];
                    _frm_ro.dgv_itemlist["dgvli_remarks", r].Value = dt.Rows[0]["dgvi_notes"];
                    _frm_ro.dgv_itemlist["dgvli_unit", r].Value = dt.Rows[0]["dgvi_unitdesc"];
                    _frm_ro.dgv_itemlist["dgvli_unitid", r].Value = dt.Rows[0]["dgvi_unitid"];
                    _frm_ro.dgv_itemlist["dgvli_at_code", r].Value = dt.Rows[0]["dgvi_at_code"];

                    Double lnamt = gm.toNormalDoubleFormat(dt.Rows[0]["dgvi_lnamt"].ToString());
                    Double net = lnamt / 1.12;
                    Double vat = lnamt - net;

                    _frm_ro.dgv_itemlist["dgvli_net", r].Value = net.ToString("0.00");
                    _frm_ro.dgv_itemlist["dgvli_taxamt", r].Value = vat.ToString("0.00");
                    
                    _frm_ro.dgv_itemlist["dgvli_discamt", r].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["dgvi_discamnt"].ToString()));
                    _frm_ro.dgv_itemlist["dgvli_discreason", r].Value = dt.Rows[0]["dgvi_discreason"];
                    _frm_ro.dgv_itemlist["dgvli_discuser", r].Value = dt.Rows[0]["dgvi_discby"];
                   

                    _frm_ro.dgv_itemlist["dgvli_t_date", r].Value = db.get_systemdate("");
                    _frm_ro.dgv_itemlist["dgvli_t_time", r].Value = db.get_systemtime();
                    //_frm_ro.dgv_itemlist["dgvli_clerk", r].Value = _frm_ro.txt_cro_name.Text;
                    //_frm_ro.dgv_itemlist["dgvli_clerkid", r].Value = _frm_ro.txt_clerk_id.Text;

                    //    
                    _frm_ro.total_amountdue();

                }
                /*else if (whatForm == "AP")
                {

                    int r = 0;

                    if (isnew)
                    {
                        _frm_ap.dgv_itemlist.Rows.Add();
                        r = _frm_ap.dgv_itemlist.Rows.Count - 1;

                        _frm_ap.set_last_lnno(lnno);
                    }
                    else
                    {
                        r = _frm_ap.dgv_itemlist.CurrentRow.Index;
                        lnno = Convert.ToInt32(gm.toNormalDoubleFormat(txt_lnno.Text));
                    }

                    _frm_ap.dgv_itemlist["dgvli_lnno", r].Value = lnno;
                    _frm_ap.dgv_itemlist["dgvi_part_no", r].Value = dt.Rows[0]["dgvi_part_no"];
                    _frm_ap.dgv_itemlist["dgvli_itemcode", r].Value = dt.Rows[0]["dgvi_itemcode"];
                    _frm_ap.dgv_itemlist["dgvli_itemdesc", r].Value = dt.Rows[0]["dgvi_itemdesc"];
                    _frm_ap.dgv_itemlist["dgvli_qty", r].Value = dt.Rows[0]["dgvi_qty"];
                    _frm_ap.dgv_itemlist["dgvli_regprice", r].Value = dt.Rows[0]["dgvi_price"];
                    _frm_ap.dgv_itemlist["dgvli_sellprice", r].Value = dt.Rows[0]["dgvi_price"];
                    _frm_ap.dgv_itemlist["dgvli_lnamt", r].Value = dt.Rows[0]["dgvi_lnamt"];
                    _frm_ap.dgv_itemlist["dgvli_remarks", r].Value = dt.Rows[0]["dgvi_notes"];
                    _frm_ap.dgv_itemlist["dgvli_unit", r].Value = dt.Rows[0]["dgvi_unitdesc"];
                    _frm_ap.dgv_itemlist["dgvli_unitid", r].Value = dt.Rows[0]["dgvi_unitid"];

                    Double lnamt = gm.toNormalDoubleFormat(dt.Rows[0]["dgvi_lnamt"].ToString());
                    Double net = lnamt / 1.12;
                    Double vat = lnamt - net;

                    _frm_ap.dgv_itemlist["dgvli_net", r].Value = net.ToString("0.00");
                    _frm_ap.dgv_itemlist["dgvli_taxamt", r].Value = vat.ToString("0.00");

                    _frm_ap.dgv_itemlist["dgvli_discamt", r].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["dgvi_discamnt"].ToString()));
                    _frm_ap.dgv_itemlist["dgvli_discreason", r].Value = dt.Rows[0]["dgvi_discreason"];
                    _frm_ap.dgv_itemlist["dgvli_discuser", r].Value = dt.Rows[0]["dgvi_discby"];
                   

                    _frm_ap.dgv_itemlist["dgvli_t_date", r].Value = db.get_systemdate("");
                    _frm_ap.dgv_itemlist["dgvli_t_time", r].Value = db.get_systemtime();
                    _frm_ap.dgv_itemlist["dgvli_clerk", r].Value = _frm_ap.txt_cro_name.Text;
                    _frm_ap.dgv_itemlist["dgvli_clerkid", r].Value = _frm_ap.txt_cro_id.Text;

                    //    
                    _frm_ap.total_amountdue();

                }*/
                else
                {
                    _frm_st.set_dgv_itemlist(dt);
                }

                this.Close();
            }
            //}
            //catch (Exception er) { MessageBox.Show("Invalid Entry. " + er.Message); }
        }

        public void enter_item(String itemcode, String tdesc)
        {
            DataTable dt;
            String ave_cost = "", su ="";

            txt_itemcode.Text = itemcode;
            cbo_itemdesc.Text = tdesc;
           

            try
            {
                dt = db.get_item_details(itemcode);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    ave_cost = dt.Rows[i]["unit_cost"].ToString();
                    su = dt.Rows[i]["sales_unit_id"].ToString();
                    txt_partno.Text = dt.Rows[i]["part_no"].ToString(); 
                    txt_costprice.Text = gm.toNormalDoubleFormat(ave_cost).ToString("0.00");
                    cbo_purchunit.SelectedValue = dt.Rows[i]["purc_unit_id"].ToString();
                }

                this.ActiveControl = txt_qty;
            }
            catch (Exception) { }
        }

        private void disp_amnt_results()
        {
            Double ln_amnt = 0.00;
            Double qty = 0.00, costprice = 0.00, transpo = 0.00, discval = 0.00;

            try
            {
                qty = gm.toNormalDoubleFormat(txt_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_costprice.Text);
                transpo = gm.toNormalDoubleFormat(txt_transpo_amnt.Text);
                discval = gm.toNormalDoubleFormat(txt_discamnt.Text);

                ln_amnt = ((qty * costprice) + (qty * transpo) - discval);
            }
            catch (Exception) { }

            txt_lnamt.Text = gm.toAccountingFormat(ln_amnt);
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void txt_costprice_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void txt_qty_Click(object sender, EventArgs e)
        {
            if (txt_qty.Text == "0.00")
            {
                txt_qty.Text = "";
            }
        }

        private void txt_costprice_Click(object sender, EventArgs e)
        {
            if (txt_costprice.Text == "0.00")
            {
                txt_costprice.Text = "";
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_itemcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormat(txt_qty.Text);
        }

        private void txt_costprice_Leave(object sender, EventArgs e)
        {
            txt_costprice.Text = gm.toAccountingFormat(txt_costprice.Text);
        }

        private void cbo_purchunit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_receive_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormat(txt_qty.Text);
        }

        private void txt_receive_qty_TextChanged(object sender, EventArgs e)
        {
            compute_lnamt();
        }

        private void txt_transpo_amnt_TextChanged(object sender, EventArgs e)
        {
            if (is_receiving)
            {
                compute_lnamt();
            }
            else {
                disp_amnt_results();
            }
        }

        private void compute_lnamt()
        {
            Double ln_amnt = 0.00, qty = 0.00, costprice = 0.00, transpo = 0.00, discval = 0.00;

            try
            {
                qty = gm.toNormalDoubleFormat(txt_receive_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_costprice.Text);
                transpo = gm.toNormalDoubleFormat(txt_transpo_amnt.Text);
                discval = gm.toNormalDoubleFormat(txt_discamnt.Text);

                ln_amnt = ((qty * costprice) + (qty * transpo) - discval);
            }
            catch (Exception) { }

            txt_lnamt.Text = gm.toAccountingFormat(ln_amnt);
        }

        private void cbo_cost_center_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalClass gc = new GlobalClass();

                if (cbo_cost_center.SelectedIndex > -1)
                {
                    gc.load_subcostcenter(cbo_sub_cost_center, cbo_cost_center.SelectedValue.ToString());
                }
            }
            catch (Exception)
            { }
        }

        private void txt_discamnt_TextChanged(object sender, EventArgs e)
        {
            if (is_receiving)
            {
                compute_lnamt();
            }
            else
            {
                disp_amnt_results();
            }
        }
    }
}
