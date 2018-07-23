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
    public partial class z_enter_item : Form
    {
        i_PO _frm_po = null;
        i_DirectPurchase _frm_dp = null;
        i_ReceivingPurchase _frm_rp = null;
        GlobalMethod gm;
        GlobalClass gc;
        private Boolean isnew = false, fromDP = false;
        private Boolean islot_required = false;
        private int lnno;
        private int currow = 0;
        Double ln_amnt = 0.00, vat_amnt = 0.00;

        #region Direct Purchase
        //is_new: is new item
        public z_enter_item(i_DirectPurchase frm_dp, Boolean is_new, int i)
        {
            _frm_dp = frm_dp;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);

            txt_costprice.TextChanged +=txt_costprice_TextChanged;
            txt_qty.TextChanged +=txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged+=txt_disc_amt_TextChanged;
            txt_disc_pct.TextChanged +=txt_disc_pct_TextChanged;

            load_fields_upd();

            cbo_vat.SelectedIndexChanged +=cbo_vat_SelectedIndexChanged;
            fromDP = true;
        }

        void txt_netprice_TextChanged(object sender, EventArgs e)
        {
            
        }

        //use for update
        public z_enter_item(i_DirectPurchase frm_dp, Boolean is_new, int i, int cur_row)
        {
            _frm_dp = frm_dp;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            currow = cur_row;
            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);

            txt_costprice.TextChanged += txt_costprice_TextChanged;
            txt_qty.TextChanged += txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged += txt_disc_amt_TextChanged;
            txt_disc_pct.TextChanged += txt_disc_pct_TextChanged;

            load_fields_upd();

            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;
            fromDP = true;
        }

        #endregion

        #region Receiving Purchase
        //is_new: is new item
        public z_enter_item(i_ReceivingPurchase frm_dp, Boolean is_new, int i)
        {
            _frm_rp = frm_dp;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_acctid);

            txt_costprice.TextChanged += txt_costprice_TextChanged;
            txt_qty.TextChanged += txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged += txt_disc_amt_TextChanged;
            txt_disc_pct.TextChanged += txt_disc_pct_TextChanged;

            cbo_acctid.Visible = false;
            lbl_acctid.Visible = false;
            load_fields_upd();
            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;
            fromDP = false;
        }


        //use for update
        public z_enter_item(i_ReceivingPurchase frm_dp, Boolean is_new, int i, int cur_row)
        {
            _frm_rp = frm_dp;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            currow = cur_row;
            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);
            gc.load_account_title(cbo_acctid);

            txt_costprice.TextChanged += txt_costprice_TextChanged;
            txt_qty.TextChanged += txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged += txt_disc_amt_TextChanged;
            txt_disc_pct.TextChanged += txt_disc_pct_TextChanged;
            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;

            cbo_acctid.Visible = false;
            lbl_acctid.Visible = false;
            load_fields_upd();
            fromDP = false;

        }
        #endregion

        #region Purchase Order
        //is_new: is new item
        public z_enter_item(i_PO frm_po, Boolean is_new, int i)
        {
            _frm_po = frm_po;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);
            gc.load_subcostcenter(cbo_subcostcenter, "");

            txt_costprice.TextChanged += txt_costprice_TextChanged;
            txt_qty.TextChanged += txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged += txt_disc_amt_TextChanged;
            txt_disc_pct.TextChanged += txt_disc_pct_TextChanged;
                
                if(isnew == false)
                    load_fields_upd();

            cbo_acctid.Visible = false;
            lbl_acctid.Visible = false;
            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;
            fromDP = true;
        }
        
        //use for update
        public z_enter_item(i_PO frm_po, Boolean is_new, int i, int cur_row)
        {
            _frm_po = frm_po;
            gc = new GlobalClass();
            gm = new GlobalMethod();

            InitializeComponent();

            isnew = is_new;
            lnno = i;
            currow = cur_row;
            //if (isnew)
            //{
                //currow = cur_row - 1;
            //}

            gc.load_vat(cbo_vat);
            gc.load_costcenter(cbo_costcenter);
            gc.load_saleunit(cbo_purchunit);
            gc.load_subcostcenter(cbo_subcostcenter,"");

            txt_costprice.TextChanged += txt_costprice_TextChanged;
            txt_qty.TextChanged += txt_qty_TextChanged;
            //txt_netprice.TextChanged += txt_netprice_TextChanged;
            txt_disc_amt.TextChanged += txt_disc_amt_TextChanged;
           // cbo_purchunit.SelectedValue = item_unit;
            txt_disc_pct.TextChanged += txt_disc_pct_TextChanged;
            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;
            fromDP = true;
        }

        #endregion

        private void z_enter_item_Load(object sender, EventArgs e)
        {
            txt_lnno.Text = lnno.ToString();
           

            load_fields_upd();
            txt_disc_pct.Hide();
            lbl_discpct_txt.Hide();
           
           
        }

        private void load_fields_upd()
        {
            thisDatabase db = new thisDatabase();
            String itemcode = "";
            try
            {
                //update items -- isnew means isnew_item which is false because  items are already inserted . it only needs for update.
                if (isnew == false && _frm_dp!=null)
                {
                    txt_lnno.Text = _frm_dp.get_dgvi_lnno(currow);
                    txt_partno.Text = _frm_dp.get_dgvi_partno(currow);
                    itemcode = _frm_dp.get_dgvi_itemcode(currow);
                    txt_itemcode.Text = itemcode;
                    txt_itemdesc.Text = _frm_dp.get_dgvi_itemdesc(currow);
                    txt_qty.Text = _frm_dp.get_dgvi_qty(currow);
                    txt_costprice.Text = _frm_dp.get_dgvi_costprice(currow);
                    txt_disc_pct.Text = _frm_dp.get_dgvi_discpct(currow);
                    txt_disc_amt.Text = _frm_dp.get_dgvi_discamnt(currow);
                    txt_netprice.Text = _frm_dp.get_dgvi_netprice(currow);
                    txt_lnamt.Text = _frm_dp.get_dgvi_lnamnt(currow);
                    txt_vatamt.Text = _frm_dp.get_dgvi_lnvat(currow);
                    txt_newregsellprice.Text = _frm_dp.get_dgvi_newregsellprice(currow);
                    txt_lotnumber.Text = _frm_dp.get_dgvi_lotnum(currow);

                    if (itemcode == "TEXT-ITEM")
                    {
                        cbo_acctid.Visible = true;
                        lbl_acctid.Visible = true;
                    }
                    

                    cbo_purchunit.SelectedValue = _frm_dp.get_dgvi_costunitid(currow);
                    cbo_vat.SelectedValue = _frm_dp.get_dgvi_vatcode(currow);
                    cbo_costcenter.SelectedValue = _frm_dp.get_dgvi_cccode(currow);
                    cbo_subcostcenter.SelectedValue = _frm_dp.get_dgvi_scc_code(currow);

                    lbl_regsellprice.Text = db.get_itemregsellprice(itemcode).ToString("0.00");
                    lbl_sellunit.Text = db.get_itemsellunitid(itemcode);
                }
                
                if (isnew == false && _frm_po != null)
                {
                    txt_lnno.Text = _frm_po.get_dgvi_lnno(currow);
                    itemcode = _frm_po.get_dgvi_itemcode(currow);
                    txt_itemcode.Text = itemcode;
                    txt_itemdesc.Text = _frm_po.get_dgvi_itemdesc(currow);
                    txt_qty.Text = _frm_po.get_dgvi_qty(currow);
                    txt_costprice.Text = _frm_po.get_dgvi_costprice(currow);
                    //txt_lotnumber.Text = _frm_dp.get_dgvi_lotnum(currow);

                    //dtp_expiry.Value = _frm_po.get_dgvi_expiry(currow);
                    txt_partno.Text = _frm_po.get_partno(currow);
                    cbo_purchunit.SelectedValue = _frm_po.get_dgvi_costunitid(currow);
                    cbo_vat.SelectedValue = _frm_po.get_dgvi_vatcode(currow);
                    if (cbo_vat.SelectedValue == null)
                        cbo_vat.SelectedValue = "E";
                    cbo_costcenter.SelectedValue = _frm_po.get_dgvi_cccode(currow);
                    if (cbo_costcenter.SelectedValue == null)
                        cbo_costcenter.SelectedValue = "001";
                    cbo_subcostcenter.SelectedValue = _frm_po.get_dgvi_scc_code(currow);
                    if (cbo_subcostcenter.SelectedValue == null)
                        cbo_subcostcenter.SelectedValue = "001";
                    lbl_regsellprice.Text = db.get_itemregsellprice(itemcode).ToString("0.00");
                    lbl_sellunit.Text = db.get_itemsellunitid(itemcode);

                    //txt_disc_pct.Text = _frm_po.get_dgvi_discpct(currow);
                   // txt_disc_amt.Text = _frm_po.get_dgvi_discamnt(currow);
                   // txt_netprice.Text = _frm_po.get_dgvi_netprice(currow);
                    txt_lnamt.Text = _frm_po.get_dgvi_lnamnt(currow);
                    txt_vatamt.Text = _frm_po.get_dgvi_lnvat(currow);
                    txt_newregsellprice.Text = _frm_po.get_dgvi_newregsellprice(currow);
                    txt_lotnumber.Text = _frm_po.get_dgvi_lotnum(currow);

                    
                }

                if (isnew == false && _frm_rp != null)
                {
                    txt_lnno.Text = _frm_rp.get_dgvi_lnno(currow);
                    txt_partno.Text = _frm_rp.get_dgvi_partno(currow);
                    itemcode = _frm_rp.get_dgvi_itemcode(currow);
                    txt_itemcode.Text = itemcode;
                    txt_itemdesc.Text = _frm_rp.get_dgvi_itemdesc(currow);
                    txt_qty.Text = _frm_rp.get_dgvi_qty(currow);
                    txt_costprice.Text = _frm_rp.get_dgvi_costprice(currow);
                   // txt_disc_pct.Text = gc.toNormalDoubleFormat(_frm_rp.get_dgvi_discpct(currow)).ToString("0.00");
                    txt_disc_amt.Text = _frm_rp.get_dgvi_discamnt(currow);
                    txt_netprice.Text = _frm_rp.get_dgvi_netprice(currow);
                    txt_lnamt.Text = _frm_rp.get_dgvi_lnamnt(currow);
                    txt_vatamt.Text = _frm_rp.get_dgvi_lnvat(currow);
                    txt_newregsellprice.Text = _frm_rp.get_dgvi_newregsellprice(currow);
                    txt_lotnumber.Text = _frm_rp.get_dgvi_lotnum(currow);

                    //dtp_expiry.Value = _frm_rp.get_dgvi_expiry(currow);

                    cbo_purchunit.SelectedValue = _frm_rp.get_dgvi_costunitid(currow);
                    cbo_vat.SelectedValue = _frm_rp.get_dgvi_vatcode(currow);
                    cbo_costcenter.SelectedValue = _frm_rp.get_dgvi_cccode(currow);
                    cbo_subcostcenter.SelectedValue = _frm_rp.get_dgvi_scc_code(currow);
                    cbo_acctid.SelectedValue = _frm_rp.get_dgvi_acct_code(currow); ; 
                    lbl_regsellprice.Text = db.get_itemregsellprice(itemcode).ToString("0.00");
                    lbl_sellunit.Text = db.get_itemsellunitid(itemcode);

                    if (itemcode == "TEXT-ITEM")
                    {
                        cbo_acctid.Visible = true;
                        lbl_acctid.Visible = true;
                    }
                    

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
         
            DataTable dt = new DataTable();
            Boolean proceed = true;

            dt.Columns.Add("ln_num");
            dt.Columns.Add("item_code");
            dt.Columns.Add("item_desc");
            dt.Columns.Add("vat_code");
            dt.Columns.Add("inv_qty");
            dt.Columns.Add("unit");
            dt.Columns.Add("unit_id");
            dt.Columns.Add("price");
            dt.Columns.Add("disc_pct");
            dt.Columns.Add("disc_amt");
            dt.Columns.Add("netprice");
            dt.Columns.Add("ln_amnt");
            dt.Columns.Add("ln_vat");
            dt.Columns.Add("lot_no");
            dt.Columns.Add("expiry");
            dt.Columns.Add("cc_code");
            dt.Columns.Add("scc_code");
            dt.Columns.Add("newprice");
            dt.Columns.Add("scc_desc");
            dt.Columns.Add("cc_desc");
            dt.Columns.Add("part_no");
            dt.Columns.Add("acct_code");

            if (String.IsNullOrEmpty(txt_itemcode.Text))
            {
                proceed = false;
                MessageBox.Show("Item/Product Description is required.");
            }
            else if (String.IsNullOrEmpty(cbo_vat.Text))
            {
                proceed = false;
                MessageBox.Show("Vat Type is required.");
                cbo_vat.DroppedDown = true;
            }
            else if (gm.toNormalDoubleFormat(txt_qty.Text) <= 0.000)
            {
                proceed = false;
                MessageBox.Show("Invalid Quantity.");
            }
            else if (gm.toNormalDoubleFormat(txt_costprice.Text) <= 0.00)
            {
                proceed = false;
                MessageBox.Show("Invalid Cost Price.");
            }
            else if (String.IsNullOrEmpty(cbo_purchunit.Text))
            {
                MessageBox.Show("Purchase Unit is required.");
                cbo_purchunit.DroppedDown = true;
                proceed = false;
            }
            else if (String.IsNullOrEmpty(cbo_costcenter.Text))
            {
                MessageBox.Show("Cost Center is required.");
                cbo_costcenter.DroppedDown = true;
                proceed = false;
            }
            else if(String.IsNullOrEmpty(cbo_subcostcenter.Text))
            {
                MessageBox.Show("Project / Sub Cost Center is required.");
                cbo_subcostcenter.DroppedDown = true;
                proceed = false;
            }
           

            if (String.IsNullOrEmpty(txt_newregsellprice.Text) == false && gm.toNormalDoubleFormat(txt_newregsellprice.Text) != 0 && gm.toNormalDoubleFormat(txt_newregsellprice.Text) < gm.toNormalDoubleFormat(lbl_regsellprice.Text))
            {
                DialogResult dialogResult = MessageBox.Show("New selling price is lower than the Previous selling price. Are you sure you want to continue?", "Are you sure?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    proceed = false;
                }
            }

            if (proceed)
            {
                dt.Rows.Add();
              
                dt.Rows[0]["ln_num"] = txt_lnno.Text;
                dt.Rows[0]["item_code"] = txt_itemcode.Text;
                dt.Rows[0]["item_desc"] = txt_itemdesc.Text;
                dt.Rows[0]["vat_code"] = (cbo_vat.SelectedValue??"").ToString();// +" " + cbo_vat.Text.ToString();
                //dt.Rows[0]["dgvi_vattype"] = cbo_vat.Text.ToString();
                dt.Rows[0]["inv_qty"] = txt_qty.Text;
                dt.Rows[0]["unit"] = cbo_purchunit.Text; //unit description
                dt.Rows[0]["unit_id"] = (cbo_purchunit.SelectedValue??"").ToString(); //unitid
                dt.Rows[0]["price"] = txt_costprice.Text;
                dt.Rows[0]["disc_pct"] = txt_disc_pct.Text;
                dt.Rows[0]["disc_amt"] = txt_disc_amt.Text;
                dt.Rows[0]["netprice"] = txt_netprice.Text;
                dt.Rows[0]["ln_amnt"] = txt_lnamt.Text;
                dt.Rows[0]["ln_vat"] = txt_vatamt.Text;
                //dt.Rows[0]["dgvi_newregsellprice"] = txt_newregsellprice.Text;
                dt.Rows[0]["lot_no"] = txt_lotnumber.Text;

                dt.Rows[0]["acct_code"] = (cbo_acctid.SelectedValue??"").ToString();

                dt.Rows[0]["cc_code"] = (cbo_costcenter.SelectedValue??"").ToString();
                dt.Rows[0]["scc_code"] = (cbo_subcostcenter.SelectedValue??"").ToString();
                dt.Rows[0]["cc_desc"] = cbo_costcenter.Text;
                dt.Rows[0]["scc_desc"] = cbo_subcostcenter.Text;
                dt.Rows[0]["part_no"] = txt_partno.Text;
                if (String.IsNullOrEmpty(txt_newregsellprice.Text))
                    dt.Rows[0]["newprice"] = "00.00";
                else
                    dt.Rows[0]["newprice"] = txt_newregsellprice.Text;

                if(_frm_po != null)
                {
                    _frm_po.set_dgv_itemlist(dt);
                }
                else if (_frm_dp != null)
                {
                    _frm_dp.set_dgv_itemlist(dt);
                }
                else
                {
                    _frm_rp.set_dgv_itemlist(dt);
                }

                this.Close();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, txt_itemdesc.Text, "D");
            _frm_is.Show();
        }

        public void enter_item(String itemcode, String tdesc)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt;
            txt_itemcode.Text = itemcode;
            txt_itemdesc.Text = tdesc;

            try
            {
                dt = db.get_item_details(itemcode);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    txt_partno.Text = dt.Rows[i]["part_no"].ToString();
                    lbl_regsellprice.Text = dt.Rows[i]["sell_pric"].ToString();
                    lbl_sellunit.Text = db.get_item_unit_desc(dt.Rows[i]["sales_unit_id"].ToString());
                    cbo_purchunit.SelectedValue = dt.Rows[i]["purc_unit_id"].ToString();
                    

                    if (dt.Rows[i]["req_lot"].ToString() == "Y")
                    {
                        islot_required = true;
                    }
                    else
                    {
                        islot_required = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void txt_qty_Click(object sender, EventArgs e)
        {
            if (txt_qty.Text == "0.000")
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

        private void txt_disc_pct_Click(object sender, EventArgs e)
        {
            if (txt_disc_pct.Text == "0.00")
            {
                txt_disc_pct.Text = "";
            }
        }

        private void txt_disc_amt_Click(object sender, EventArgs e)
        {
            if (txt_disc_amt.Text == "0.00")
            {
                txt_disc_amt.Text = "";
            }
        }

        private void txt_newregsellprice_Click(object sender, EventArgs e)
        {
            if (txt_newregsellprice.Text == "0.00")
            {
                txt_newregsellprice.Text = "";
            }
        }

        private void disp_amnt_results()
        {
            Double total_costamnt = 0.00;
            Double net_amnt = 0.00, qty = 0.000, costprice = 0.00, disc_pct = 0.00, disc_amnt = 0.00;
            ln_amnt = 0.00; vat_amnt = 0.00;
            string qtyy = txt_qty.Text;
            
            try
            {
                qty = Convert.ToDouble(txt_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_costprice.Text);
                disc_amnt = gm.toNormalDoubleFormat(txt_disc_amt.Text);
                disc_pct = gm.toNormalDoubleFormat(txt_disc_pct.Text);

                total_costamnt = (qty * costprice);

                ln_amnt = total_costamnt - (total_costamnt * (disc_pct / 100)) - disc_amnt;
                net_amnt = ln_amnt / qty;
            }
            catch (Exception) { }

            txt_lnamt.Text = gm.toAccountingFormat(ln_amnt);
            txt_netprice.Text = gm.toAccountingFormat(net_amnt);

            disp_vat();
        }

        private void disp_vat()
        {
            thisDatabase db = new thisDatabase();
            Double total = ln_amnt; //gm.toNormalDoubleFormat(txt_qty.Text) * gm.toNormalDoubleFormat(txt_costprice.Text);
            Double lnvat = vat_amnt;
            Double vatpct = 0.00;
            Double net = 0.00;

            try
            {
                vatpct = 1 + (db.get_vat_pct(cbo_vat.SelectedValue.ToString()) / 100);

                if (cbo_vat.SelectedValue.ToString() == "E")
                {

                    net = total;
                    lnvat = total - (total / vatpct);

                    total = net + lnvat;
                }
                else if (cbo_vat.SelectedValue.ToString() == "I")
                {
                    net = total / vatpct;
                    lnvat = total - net;
                }
                else
                {
                    net = total / vatpct;
                    lnvat = 0;
                    total = net;
                }

                txt_lnamt.Text = gm.toAccountingFormat(total);
                txt_vatamt.Text = gm.toAccountingFormat(lnvat);
                txt_netprice.Text = gm.toAccountingFormat(net);
            }
            catch (Exception) { }
        }

        #region Texts

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void txt_costprice_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void txt_disc_pct_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void txt_disc_amt_TextChanged(object sender, EventArgs e)
        {
            disp_amnt_results();
        }

        private void cbo_vat_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_vat();
        }

        #endregion

        private void cbo_costcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalClass gc = new GlobalClass();

                if(cbo_costcenter.SelectedIndex > -1)
                {
                    gc.load_subcostcenter(cbo_subcostcenter, cbo_costcenter.SelectedValue.ToString());
                }
            }
            catch(Exception)
            { }
        }

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormatForQty(txt_qty.Text);
        }

        private void txt_costprice_Leave(object sender, EventArgs e)
        {
            txt_costprice.Text = gm.toAccountingFormat(txt_costprice.Text);
        }

        private void txt_disc_amt_Leave(object sender, EventArgs e)
        {
            txt_disc_amt.Text = gm.toAccountingFormat(txt_disc_amt.Text);
        }

        private void txt_newregsellprice_Leave(object sender, EventArgs e)
        {
            txt_newregsellprice.Text = gm.toAccountingFormat(txt_newregsellprice.Text);
        }


    }
}
