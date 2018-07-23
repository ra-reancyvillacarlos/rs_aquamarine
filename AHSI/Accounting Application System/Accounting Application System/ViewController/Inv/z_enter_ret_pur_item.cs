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
    public partial class z_enter_ret_pur_item : Form
    {
        i_Purch_Orders _frm_pr;
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean isnew = true;
        int lnno = 1;
        int dgvrow = -1;
        Double ln_amnt = 0.00, vat_amnt = 0.00;

        //if is_new == true, then lineno_or_rowindex is lineno; otherwise rowindex
        public z_enter_ret_pur_item(i_Purch_Orders frm_pr, Boolean is_new, int lineno_or_rowindex)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            _frm_pr = frm_pr;
            isnew = is_new;

            if (is_new)
            {
                lnno = lineno_or_rowindex;
            }
            else
            {
                dgvrow = lineno_or_rowindex;
            }

            gc.load_saleunit(cbo_purchunit);
            gc.load_vat(cbo_vat);

            this.Load +=z_enter_ret_pur_item_Load;
            cbo_vat.SelectedIndexChanged += cbo_vat_SelectedIndexChanged;
            //btn_save.Click +=btn_save_Click;

        }


        void cbo_vat_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_withOrOut_vat();
        }

        private void z_enter_ret_pur_item_Load(object sender, EventArgs e)
        {
            txt_lnno.Text = lnno.ToString();
            load_fields_upd();
            txt_qty.TextChanged +=txt_qty_TextChanged;
            txt_costprice.TextChanged +=txt_costprice_TextChanged;
            
        }

        private void load_fields_upd()
        {
            if (_frm_pr != null && isnew == false)
            {
                txt_lnno.Text = _frm_pr.get_dgvi_ln(dgvrow);
                txt_itemcode.Text = _frm_pr.get_dgvi_itemcode(dgvrow);
                txt_itemdesc.Text = _frm_pr.get_dgvi_itemdesc(dgvrow);
                txt_qty.Text = _frm_pr.get_dgvi_qty(dgvrow);
                txt_costprice.Text = _frm_pr.get_dgvi_price(dgvrow);
                txt_lnamt.Text = _frm_pr.get_dgvi_lnamt(dgvrow);
                txt_partno.Text = _frm_pr.get_partno(dgvrow);
                cbo_vat.SelectedValue = _frm_pr.get_dgvi_vat(dgvrow);

                cbo_purchunit.SelectedValue = _frm_pr.get_dgvi_unitid(dgvrow);
                disp_withOrOut_vat();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, txt_itemcode.Text, txt_itemdesc.Text);
            _frm_is.Show();
        }

        List<enterRetPurItems> theList = new List<enterRetPurItems>();

        private void btn_save_Click(object sender, EventArgs e)
        {





            DataTable dt = new DataTable();
            enterRetPurItems listOfData = new enterRetPurItems();

            dt.Columns.Add("dgvi_ln", typeof(String));
            dt.Columns.Add("dgvi_itemcode", typeof(String));
            dt.Columns.Add("dgvi_itemdesc", typeof(String));
            dt.Columns.Add("dgvi_unitdesc", typeof(String));
            dt.Columns.Add("dgvi_qty", typeof(String));
            dt.Columns.Add("dgvi_price", typeof(String));
            dt.Columns.Add("dgvi_lnamt", typeof(String));
            dt.Columns.Add("dgvi_unitid", typeof(String));
            dt.Columns.Add("dgvi_part_no", typeof(String));
            dt.Columns.Add("dgvi_vat", typeof(String));

            try
            {
                if (String.IsNullOrEmpty(txt_qty.Text) || gm.toNormalDoubleFormat(txt_qty.Text) == 0.00)
                {
                    MessageBox.Show("Quantity should not be equal to zero.");
                }
                else if(cbo_purchunit.SelectedIndex==-1)
                {
                    MessageBox.Show("Please select the unit.");
                }
                else
                {
                    if (cbo_vat.SelectedIndex == -1)
                        cbo_vat.SelectedIndex = 0;

                    //Create List for this
                    dt.Rows.Add();

                    dt.Rows[0]["dgvi_ln"] = txt_lnno.Text;
                    dt.Rows[0]["dgvi_itemcode"] = txt_itemcode.Text;
                    dt.Rows[0]["dgvi_itemdesc"] = txt_itemdesc.Text;
                    dt.Rows[0]["dgvi_unitdesc"] = cbo_purchunit.Text;
                    dt.Rows[0]["dgvi_unitid"] = cbo_purchunit.SelectedValue.ToString();
                    dt.Rows[0]["dgvi_vat"] = cbo_vat.SelectedValue.ToString();
                    dt.Rows[0]["dgvi_qty"] = txt_qty.Text;
                    dt.Rows[0]["dgvi_price"] = txt_costprice.Text;
                    dt.Rows[0]["dgvi_lnamt"] = txt_lnamt.Text;
                    dt.Rows[0]["dgvi_part_no"] = txt_partno.Text;

                    //listOfData.llno = txt_lnno.Text;
                    //listOfData.itemcode = txt_itemcode.Text;
                    //listOfData.itemdesc = txt_itemdesc.Text;
                    //listOfData.purchUnit = cbo_purchunit.Text;
                    //listOfData.purchUnitSelectedValue = cbo_purchunit.SelectedValue.ToString();
                    //listOfData.qty = txt_qty.Text;
                    //listOfData.costprice = txt_costprice.Text;
                    //listOfData.lnamt = txt_lnamt.Text;

                    //theList.Add(listOfData);

                    _frm_pr.set_dgv_itemlist(dt);

                    this.Close();
                }

            }                                                                           
            catch (Exception er) { MessageBox.Show("Invalid Entry." + er.Message); }


        }

        public void enter_item(String itemcode, String tdesc)
        {
            DataTable dt;
            String ave_cost = "";

            txt_itemcode.Text = itemcode;
            txt_itemdesc.Text = tdesc;

            try
            {
                dt = db.get_item_details(itemcode);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    ave_cost = dt.Rows[i]["ave_cost"].ToString();
                    txt_partno.Text = dt.Rows[i]["part_no"].ToString();
                    if (String.IsNullOrEmpty(dt.Rows[i]["ave_cost"].ToString()) == false)
                    {
                        txt_costprice.Text = gm.toNormalDoubleFormat(ave_cost).ToString("0.00");
                    }
                    else
                    {
                        txt_costprice.Text = "0.00";
                    }
                    cbo_purchunit.SelectedValue = dt.Rows[i]["purc_unit_id"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_withOrOut_vat() {


            Double total = ln_amnt;
            Double lnvat = vat_amnt;
            Double vatpct = 0.00;


            Double qty = 0.00, costprice = 0.00;

            try
            {
                qty = gm.toNormalDoubleFormat(txt_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_costprice.Text);

                ln_amnt = qty * costprice;
            }
            catch (Exception) { }

            txt_lnamt.Text = gm.toAccountingFormat(ln_amnt);


            try
            {
                if (cbo_vat.SelectedValue.ToString() == "E")
                {
                    vatpct = (db.get_vat_pct(cbo_vat.SelectedValue.ToString()) / 100);
                    lnvat = total * vatpct;

                    total = total + lnvat;
                }
                else if (cbo_vat.SelectedValue.ToString() == "I")
                {
                    vatpct = (db.get_vat_pct(cbo_vat.SelectedValue.ToString()) / 100);
                    lnvat = total * vatpct;

                    total = total - lnvat;
                }

                else
                {
                    vatpct = 0;
                    lnvat = total * vatpct;
                    total = total - lnvat;
                }

                txt_lnamt.Text = gm.toAccountingFormat(total);
                textBox1.Text = gm.toAccountingFormat(lnvat);
            }
            catch (Exception) { }
        
        
        
        
        }

        private void disp_vat()
        {
            thisDatabase db = new thisDatabase();
            Double total = ln_amnt;
            Double lnvat = vat_amnt;
            Double vatpct = 0.00;
            disp_amnt_results();
            try
            {
                if (cbo_vat.SelectedValue.ToString() == "E")
                {
                    vatpct = (db.get_vat_pct(cbo_vat.SelectedValue.ToString()) / 100);
                    lnvat = total * vatpct;

                    total = total + lnvat;
                }
                else if (cbo_vat.SelectedValue.ToString() == "I")
                {
                    vatpct = (db.get_vat_pct(cbo_vat.SelectedValue.ToString()) / 100);
                    lnvat = total * vatpct;

                    total = total - lnvat;
                }

                else
                {
                    vatpct = 0;
                    lnvat = total * vatpct;
                    total = total - lnvat;
                }

                txt_lnamt.Text = gm.toAccountingFormat(total);
                textBox1.Text = gm.toAccountingFormat(lnvat);
            }
            catch (Exception) { }
        }

        private void disp_amnt_results()
        {
           // Double ln_amnt = 0.00;
            Double qty = 0.00, costprice = 0.00;

            try
            {
                qty = gm.toNormalDoubleFormat(txt_qty.Text);
                costprice = gm.toNormalDoubleFormat(txt_costprice.Text);

                ln_amnt = qty * costprice;
            }
            catch (Exception) { }

            txt_lnamt.Text = gm.toAccountingFormat(ln_amnt);
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {

            disp_withOrOut_vat();
        }

        private void txt_costprice_TextChanged(object sender, EventArgs e)
        {
            disp_withOrOut_vat();
            
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

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormat(txt_qty.Text);
        }

        private void txt_costprice_Leave(object sender, EventArgs e)
        {
            txt_costprice.Text = gm.toAccountingFormat(txt_costprice.Text);
        }
    }
}
