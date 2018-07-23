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
    public partial class z_enter_ret_sale_item : Form
    {
        s_SalesReturn _frm_sr;
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean isnew = true;
        String itemc = "";
        int lnno = 1;
        int dgvrow = -1;

        public z_enter_ret_sale_item(s_SalesReturn frm_sr, Boolean is_new,int i)
        {
            InitializeComponent();
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            _frm_sr = frm_sr;
            isnew = is_new;

            if (is_new)
            {
                lnno = i;
            }
            else
            {
                dgvrow = i;
                lnno = i;
            }

            txt_qty.ReadOnly = true;

            gc.load_saleunit(cbo_unit);
            gc.load_salesclerk(cbo_salesclerk);

            //cbo_salesclerk.Text = GlobalClass.username;

            
        }

        private void z_enter_ret_sale_item_Load(object sender, EventArgs e)
        {
            txt_lnno.Text = lnno.ToString();
            load_fields_upd();
        }

        private void load_fields_upd()
        {
            if (_frm_sr != null && isnew == false)
            {
                try
                {
                    txt_lnno.Text = _frm_sr.get_dgvi_ln(dgvrow);
                    txt_itemcode.Text = _frm_sr.get_dgvi_itemcode(dgvrow);
                    txt_itemdesc.Text = _frm_sr.get_dgvi_itemdesc(dgvrow);
                    txt_qty.Text = _frm_sr.get_dgvi_qty(dgvrow);
                    txt_price.Text = _frm_sr.get_dgvi_price(dgvrow);
                    txt_lnamt.Text = _frm_sr.get_dgvi_lnamt(dgvrow);

                    cbo_unit.SelectedValue = _frm_sr.get_dgvi_unitid(dgvrow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, txt_itemcode.Text, txt_itemdesc.Text);
            _frm_is.Show();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("dgvi_ln", typeof(String));
            dt.Columns.Add("dgvi_docno", typeof(String));
            dt.Columns.Add("dgvi_itemcode", typeof(String));
            dt.Columns.Add("dgvi_itemdesc", typeof(String));
            dt.Columns.Add("dgvi_unitdesc", typeof(String));
            dt.Columns.Add("dgvi_qty", typeof(String));
            dt.Columns.Add("dgvi_price", typeof(String));
            dt.Columns.Add("dgvi_lnamt", typeof(String));
            dt.Columns.Add("dgvi_unitid", typeof(String));

            dt.Columns.Add("dgvi_inv_num", typeof(String));
            dt.Columns.Add("dgvi_inv_lne", typeof(String));
            dt.Columns.Add("dgvi_prc_user", typeof(String));
            dt.Columns.Add("dgvi_prc_reason", typeof(String));
            dt.Columns.Add("dgvi_repcode", typeof(String));

            try
            {
                if (String.IsNullOrEmpty(txt_qty.Text) || gm.toNormalDoubleFormat(txt_qty.Text) == 0.00)
                {
                    MessageBox.Show("Quantity should not be equal to zero.");
                }
                else
                {
                    dt.Rows.Add();

                    dt.Rows[0]["dgvi_ln"] = txt_lnno.Text;
                    dt.Rows[0]["dgvi_docno"] = txt_referenceno.Text;
                    dt.Rows[0]["dgvi_itemcode"] = txt_itemcode.Text;
                    dt.Rows[0]["dgvi_itemdesc"] = txt_itemdesc.Text;
                    dt.Rows[0]["dgvi_unitdesc"] = cbo_unit.Text;
                    dt.Rows[0]["dgvi_unitid"] = cbo_unit.SelectedValue.ToString();
                    dt.Rows[0]["dgvi_qty"] = txt_qty.Text;
                    dt.Rows[0]["dgvi_price"] = txt_price.Text;
                    dt.Rows[0]["dgvi_lnamt"] = txt_lnamt.Text;

                    dt.Rows[0]["dgvi_inv_num"] = txt_invnum.Text;
                    dt.Rows[0]["dgvi_inv_lne"] = txt_invlne.Text;
                    dt.Rows[0]["dgvi_prc_user"] = txt_prcuser.Text;
                    dt.Rows[0]["dgvi_prc_reason"] = txt_prcreason.Text;

                    dt.Rows[0]["dgvi_repcode"] = GlobalClass.username;


                    _frm_sr.set_dgv_itemlist(dt);

                    this.Close();
                }
            }
            catch (Exception er) { MessageBox.Show("Invalid Entry." + er.Message); }

        }

        public void enter_item(String itemcode, String tdesc,String partno)
        {
            DataTable dt;
            String ave_cost = "";

            this.itemc = itemcode;

            txt_itemcode.Text = itemcode;
            txt_itemdesc.Text = tdesc;
            txt_partno.Text = partno;
            try
            {
                txt_qty.ReadOnly = false;

                disp_solistOfItem();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }
        public void set_unit(String unit)
        {
            cbo_unit.SelectedValue = unit;
        }
        public void set_reg_price(String regprice)
        {
            txt_price.Text = regprice;
        }
        public DataTable get_solistofitem(String itemcode) {
    
            return db.QueryBySQLCode("SELECT DISTINCT o.reference AS refdoc, ol.* FROM rssys.orlne ol LEFT JOIN rssys.orhdr o ON o.ord_code=ol.ord_code WHERE ol.item_code='"+itemcode+"' AND o.pending='N'" );
        }
        private void disp_solistOfItem()
        {
           //
            DataTable dt = get_solistofitem(itemc);
             try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dgv_list.DataSource = dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }


        }

        private void disp_amnt_results()
        {
            Double ln_amnt = 0.00;
            Double qty = 0.00, costprice = 0.00;

            try
            {
                qty = gm.toNormalDoubleFormat(txt_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_price.Text);

                ln_amnt = qty * costprice;
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
            if (txt_price.Text == "0.00")
            {
                txt_price.Text = "";
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = 0;
            String clerk_name = "";
            DataTable dt = null;
            if(dgv_list.Rows.Count > 0)
            {
                try
                {
                    r = dgv_list.CurrentRow.Index;
                    MessageBox.Show(dgv_list["dgvl_clerk", r].Value.ToString());

                    if (r > -1 )
                        cbo_salesclerk.SelectedValue = dgv_list["dgvl_clerk", r].Value.ToString();
                }
                catch { }
            try
            {
                r = dgv_list.CurrentRow.Index;

                if (r > -1)
                {
                    txt_referenceno.Text = dgv_list["dgvl_code", r].Value.ToString();
                    txt_qty.Text = dgv_list["dgvl_qty", r].Value.ToString();
                    txt_price.Text = dgv_list["dgvl_price", r].Value.ToString();
                    txt_lnamt.Text = dgv_list["dgvl_lnamt", r].Value.ToString();

                    txt_discamt.Text = gm.toNormalDoubleFormat(dgv_list["dgvl_discamt", r].Value.ToString()).ToString("0.00");
                    txt_discpct.Text = gm.toNormalDoubleFormat(dgv_list["dgvl_discpct", r].Value.ToString()).ToString("0.00");

                    txt_invnum.Text = dgv_list["dgvl_inv_num", r].Value.ToString();
                    txt_invlne.Text = dgv_list["dgvl_inv_lne", r].Value.ToString();
                    txt_prcuser.Text = dgv_list["dgvl_prc_user", r].Value.ToString();
                    txt_prcreason.Text = dgv_list["dgvl_prc_reason", r].Value.ToString();
                    txt_prcreason.Text = dgv_list["dgvl_unitid", r].Value.ToString();
                    cbo_unit.SelectedValue = dgv_list["dgvl_unitid", r].Value;

                    

                    dt = db.QueryBySQLCode("SELECT * FROM rssys.repmst WHERE rep_code ='" + dgv_list["dgvl_clerk", r].Value.ToString() + "'");
                    
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            clerk_name = dt.Rows[i]["rep_name"].ToString();
                           
                        }
                        
                    }//end of IF 
                }
            }
            catch (Exception) { }
            }
        }

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormat(txt_qty.Text);
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            double qty = Convert.ToDouble(gm.toNormalDoubleFormat(txt_qty.Text).ToString("0.00"));
            double price = Convert.ToDouble(gm.toNormalDoubleFormat(txt_price.Text).ToString("0.00"));
            double line_amt = qty * price;
            txt_lnamt.Text = line_amt.ToString("0.00");
        }

        private void txt_qty_TextChanged_1(object sender, EventArgs e)
        {
            double qty = Convert.ToDouble(gm.toNormalDoubleFormat(txt_qty.Text).ToString("0.00"));
            double price = Convert.ToDouble(gm.toNormalDoubleFormat(txt_price.Text).ToString("0.00"));
            double line_amt = qty * price;
            txt_lnamt.Text = line_amt.ToString("0.00");
        }

        private void txt_itemcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
