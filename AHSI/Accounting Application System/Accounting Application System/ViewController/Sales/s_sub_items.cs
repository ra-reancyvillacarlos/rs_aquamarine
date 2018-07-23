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
    public partial class s_sub_items : Form
    {
        private dbSales db;
        private String code, ord_code = "";
        private s_RepairOrder frm_ro;
        private s_ServiceHistory frm_sh;
        private z_clerkpassword frm_pclerkpass;
        private s_Sales frm_sales;
        private z_payment frm_payment;
        private z_enter_sales_item frm_si;
        private z_enter_item_simple frm_ssi;
        private z_so_list frm_solist;
        private m_customers frm_cust;
        private m_company frm_dealer;
        private s_Work_Shopload frm_work_shopload;
        private int ln = 1;
        private Boolean isverified = false, isWin2Active = false;
        Boolean seltbp;
        private Boolean isnew = false;
        private Boolean isnew_item = true;
        String sub_col2 = "";
        String sub_val2 = "";
        private GlobalClass gc;
        private GlobalMethod gm;

        private int count;
        int lnno_last = 1;
        String stk_trns_type = "RO";
        Boolean isRepair = false, isCashier = false;
        private int p;
        Boolean exist = false;
        String lnno = "1";
        Boolean isnew_parentfrm = true;
        String checker1 = "0";
        String checker2 = "0";
        String discount = "";
        String out_code = "";

        public s_sub_items(s_RepairOrder frm, String code, String ord_code, String ln, Boolean isnewparent,String out_code)
        {
            InitializeComponent();
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            // TODO: Complete member initialization
            this.code = code;
            this.ord_code = ord_code;
            this.out_code = out_code;
            lnno = ln;
            frm_ro = frm;
            isnew_parentfrm = isnewparent;
            disp_list();
            disp_list_frm();
            disp_totals();
            discount = frm_ro.dgv_itemlist["dgvli_discamt", frm_ro.dgv_itemlist.CurrentRow.Index].Value.ToString();
            
        }

        public s_sub_items(s_ServiceHistory frm, String code, String ord_code, String ln, Boolean isnewparent)
        {
            InitializeComponent();
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            // TODO: Complete member initialization
            this.code = code;
            this.ord_code = ord_code;
            lnno = ln;
            frm_sh = frm;
            isnew_parentfrm = isnewparent;
            disp_list();
            disp_list_frm();
            disp_totals();
            //dgv_subitem[""]
        }

        public s_sub_items(s_Work_Shopload frm, String code, String ord_code, String ln, Boolean isnewparent)
        {
            InitializeComponent();
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            // TODO: Complete member initialization
            this.code = code;
            this.ord_code = ord_code;
            lnno = ln;
            frm_work_shopload = frm;
            isnew_parentfrm = isnewparent;
            disp_list();
            disp_list_frm();
            disp_totals();
            //dgv_subitem[""]
        }

        public s_sub_items(s_Sales frm, String code, String ord_code, String ln)
        {
            InitializeComponent();
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            // TODO: Complete member initialization
            this.code = code;
            this.ord_code = ord_code;
            disp_list();
            lnno = ln;
            frm_sales = frm;
        }


        private void s_sub_items_Load(object sender, EventArgs e)
        {

        }

        public DataTable get_subitem(String code, String ord_code,String ln)
        {
            DataTable checkerdt;

            // new = false
            checkerdt = db.QueryBySQLCode("SELECT * FROM rssys.orlne2 WHERE ord_code ='" + ord_code + "' AND item_code_ol='" + code + "'");

            if (checkerdt.Rows.Count > 0)
            {
                this.exist = true;
                return db.QueryBySQLCode("SELECT DISTINCT o.ord_code, o.item_code_ol, o.item_code, o.part_no, o.item_desc, o.unitid, o.qty, o.cost_pric, o.lnamnt AS lnamnt, o.ln_num ,o.disc_amt, o.sellprice, o.in_tax, o.disc_code, o.disc_user, o.disc_reason, o.notes ,ui.unit_shortcode FROM rssys.orlne2 o LEFT JOIN rssys.itmunit ui ON ui.unit_id=o.unitid WHERE o.ord_code='" + ord_code + "' AND o.item_code_ol='" + code + "' AND o.ln_num='" + ln + "'");
            }

            this.exist = false;
            return db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.sell_pric AS lnamt, i.sell_pric FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + code + "'");
            // 
        }

        private void disp_list()
        {
            int inx = 0;
            String itemdesc = "";
            String _ln = "";
            txt_menu_item.Text = code;

            if (frm_ro != null)
            {
                inx = frm_ro.dgv_itemlist.CurrentRow.Index;
                itemdesc = frm_ro.dgv_itemlist["dgvli_itemdesc", inx].Value.ToString();
                _ln = frm_ro.dgv_itemlist["dgvli_lnno", inx].Value.ToString();
            }
            if (frm_sh != null)
            {
                 inx = frm_sh.dgv_itemlist.CurrentRow.Index;
                 itemdesc = frm_sh.dgv_itemlist["dgvli_itemdesc", inx].Value.ToString();
                 _ln = frm_sh.dgv_itemlist["dgvli_lnno", inx].Value.ToString();
                 btn_addsubitem.Enabled = false;
                 btn_updsubitem.Enabled = false;
                 btn_delete.Enabled = false;
                 btn_save.Enabled = false;

            }
            if (frm_work_shopload != null)
            {
                inx = frm_work_shopload.dgv_itemlist.CurrentRow.Index;
                itemdesc = frm_work_shopload.dgv_itemlist["dgvli_itemdesc", inx].Value.ToString();
                _ln = frm_work_shopload.dgv_itemlist["dgvli_lnno", inx].Value.ToString();
                btn_addsubitem.Enabled = false;
                btn_updsubitem.Enabled = false;
                btn_delete.Enabled = false;
                btn_save.Enabled = false;

            }

            txt_menu_item_name.Text = itemdesc;

            if (!itemdesc.Contains("CUSTOMIZED-"))
            {
                DataTable dt = get_subitem(code, ord_code, _ln);

                
                if (dt.Rows.Count > 0)
                {
                    txt_rowCount.Text = dt.Rows.Count.ToString();

                    try
                    {
                        if (!exist)
                        {
                            for (int r = 0; dt.Rows.Count > r; r++)
                            {
                                int i = dgv_subitem.Rows.Add();
                                DataGridViewRow row = dgv_subitem.Rows[i];

                                row.Cells["dgv_sub_item_partno"].Value = dt.Rows[r]["part_no"].ToString();
                                row.Cells["dgv_sub_item_itemdesc"].Value = dt.Rows[r]["item_desc"].ToString();
                                row.Cells["dgv_sub_item_qty"].Value = dt.Rows[r]["qty"].ToString();
                                row.Cells["dgv_sub_item_unitdesc"].Value = dt.Rows[r]["unit_shortcode"].ToString();
                                row.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(dt.Rows[r]["lnamt"].ToString());
                                row.Cells["dgv_sub_item_unitid"].Value = dt.Rows[r]["sales_unit_id"].ToString();
                                row.Cells["dgv_sub_item_itemcode"].Value = dt.Rows[r]["item_code2"].ToString();
                               
                                row.Cells["dgv_sub_item_disc_amt"].Value = "0.00";
                                row.Cells["dgv_sub_item_sellprice"].Value = gm.toAccountingFormat(dt.Rows[r]["sell_pric"].ToString());

                                row.Cells["dgv_sub_item_net"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_lnamnt"].Value.ToString()) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00"));
                                row.Cells["dgv_sub_item_in_tax"].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_lnamnt"].Value.ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_net"].Value.ToString()));
                            }
                        }
                        else
                        {
                            for (int r = 0; dt.Rows.Count > r; r++)
                            {
                                int i = dgv_subitem.Rows.Add();
                                DataGridViewRow row = dgv_subitem.Rows[i];

                                row.Cells["dgv_sub_item_qty"].Value = dt.Rows[r]["qty"].ToString();
                                row.Cells["dgv_sub_item_unitdesc"].Value = dt.Rows[r]["unit_shortcode"].ToString();
                                row.Cells["dgv_sub_item_unitid"].Value = dt.Rows[r]["unitid"].ToString();
                                row.Cells["dgv_sub_item_itemcode"].Value = dt.Rows[r]["item_code"].ToString();
                                row.Cells["dgv_sub_item_itemdesc"].Value = dt.Rows[r]["item_desc"].ToString();
                                row.Cells["dgv_sub_item_partno"].Value = dt.Rows[r]["part_no"].ToString();
                                row.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(dt.Rows[r]["lnamnt"].ToString());

                                row.Cells["dgv_sub_item_net"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_lnamnt"].Value.ToString()) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00"));

                                row.Cells["dgv_sub_item_disc_amt"].Value = dt.Rows[r]["disc_amt"].ToString();
                                row.Cells["dgv_sub_item_sellprice"].Value = gm.toAccountingFormat(dt.Rows[r]["sellprice"].ToString());

                                row.Cells["dgv_sub_item_in_tax"].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_lnamnt"].Value.ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_net"].Value.ToString()));

                                row.Cells["dgv_sub_item_disc_code"].Value = dt.Rows[r]["disc_code"].ToString();
                                row.Cells["dgv_sub_item_disc_user"].Value = dt.Rows[r]["disc_user"].ToString();
                                row.Cells["dgv_sub_item_disc_reason"].Value = dt.Rows[r]["disc_reason"].ToString();
                                row.Cells["dgv_sub_item_notes"].Value = dt.Rows[r]["notes"].ToString();
                            }
                        }
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                    this.Show();
                }
                else
                {
                    this.Hide();
                }
            }
            else 
            {
                this.Show();
            }
            disp_totals();

        }

        private void disp_list_frm(){
            try
            {
                if (frm_ro != null)
                {
                    for (int r = 0; r < frm_ro.dgv_subitem.Rows.Count; r++) {

                        if (frm_ro.dgv_subitem["dgv_sub_item_ln",r].Value.ToString() == lnno)
                        {
                            String itemcode_frm_ro = (frm_ro.dgv_subitem["dgv_sub_item_itemcode", r].Value ?? "").ToString();
                            Boolean isExist = false;

                            for (int r2 = 0; r2 < dgv_subitem.Rows.Count; r2++)
                            {
                                String _itemcode = (dgv_subitem["dgv_sub_item_itemcode", r2].Value ?? "").ToString();
                                
                                if (itemcode_frm_ro == _itemcode) 
                                {
                                    isExist = true;
                                }
                            }

                            if (!isExist)
                            {

                                int i = dgv_subitem.Rows.Add();
                                DataGridViewRow row = frm_ro.dgv_subitem.Rows[r];

                                dgv_subitem["dgv_sub_item_qty", i].Value = row.Cells["dgv_sub_item_qty"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitdesc", i].Value = row.Cells["dgv_sub_item_unitdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitid", i].Value = row.Cells["dgv_sub_item_unitid"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemcode", i].Value = row.Cells["dgv_sub_item_itemcode"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemdesc", i].Value = row.Cells["dgv_sub_item_itemdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_partno", i].Value = row.Cells["dgv_sub_item_partno"].Value.ToString();
                                //dgv_subitem["dgv_sub_item_costprice", i].Value = row.Cells["dgv_sub_item_costprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_lnamnt", i].Value = row.Cells["dgv_sub_item_lnamnt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_amt", i].Value = row.Cells["dgv_sub_item_disc_amt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_sellprice", i].Value = row.Cells["dgv_sub_item_sellprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_in_tax", i].Value = row.Cells["dgv_sub_item_in_tax"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_code", i].Value = row.Cells["dgv_sub_item_disc_code"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_user", i].Value = row.Cells["dgv_sub_item_disc_user"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_reason", i].Value = row.Cells["dgv_sub_item_disc_reason"].Value.ToString();
                                dgv_subitem["dgv_sub_item_notes", i].Value = row.Cells["dgv_sub_item_notes"].Value.ToString();
                                dgv_subitem["dgv_sub_item_net", i].Value = row.Cells["dgv_sub_item_net"].Value.ToString();
                                 
                            }
                        }
                    }
                }
                if (frm_sh != null)
                {
                    // dgv_subitem.Rows[i]; dt.Rows.Count
                    for (int r = 0; r < frm_sh.dgv_subitem.Rows.Count; r++)
                    {

                        if (frm_sh.dgv_subitem["dgv_sub_item_ln", r].Value.ToString() == lnno)
                        {
                            String itemcode_frm_ro = (frm_sh.dgv_subitem["dgv_sub_item_itemcode", r].Value ?? "").ToString();
                            Boolean isExist = false;
                            for (int r2 = 0; r2 < dgv_subitem.Rows.Count; r2++)
                            {
                                String _itemcode = (dgv_subitem["dgv_sub_item_itemcode", r2].Value ?? "").ToString();
                                if (itemcode_frm_ro == _itemcode)
                                {
                                    isExist = true;
                                }
                            }

                            if (!isExist)
                            {

                                int i = dgv_subitem.Rows.Add();
                                DataGridViewRow row = frm_sh.dgv_subitem.Rows[r];

                                dgv_subitem["dgv_sub_item_qty", i].Value = row.Cells["dgv_sub_item_qty"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitdesc", i].Value = row.Cells["dgv_sub_item_unitdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitid", i].Value = row.Cells["dgv_sub_item_unitid"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemcode", i].Value = row.Cells["dgv_sub_item_itemcode"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemdesc", i].Value = row.Cells["dgv_sub_item_itemdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_partno", i].Value = row.Cells["dgv_sub_item_partno"].Value.ToString();
                                //dgv_subitem["dgv_sub_item_costprice", i].Value = row.Cells["dgv_sub_item_costprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_lnamnt", i].Value = row.Cells["dgv_sub_item_lnamnt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_amt", i].Value = row.Cells["dgv_sub_item_disc_amt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_sellprice", i].Value = row.Cells["dgv_sub_item_sellprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_in_tax", i].Value = row.Cells["dgv_sub_item_in_tax"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_code", i].Value = row.Cells["dgv_sub_item_disc_code"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_user", i].Value = row.Cells["dgv_sub_item_disc_user"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_reason", i].Value = row.Cells["dgv_sub_item_disc_reason"].Value.ToString();
                                dgv_subitem["dgv_sub_item_notes", i].Value = row.Cells["dgv_sub_item_notes"].Value.ToString();
                                dgv_subitem["dgv_sub_item_net", i].Value = row.Cells["dgv_sub_item_net"].Value.ToString();

                            }
                        }
                    }
                }
                if (frm_work_shopload != null)
                {
                    // dgv_subitem.Rows[i]; dt.Rows.Count
                    for (int r = 0; r < frm_work_shopload.dgv_subitem.Rows.Count; r++)
                    {

                        if (frm_work_shopload.dgv_subitem["dgv_sub_item_ln", r].Value.ToString() == lnno)
                        {
                            String itemcode_frm_ro = (frm_work_shopload.dgv_subitem["dgv_sub_item_itemcode", r].Value ?? "").ToString();
                            Boolean isExist = false;
                            for (int r2 = 0; r2 < dgv_subitem.Rows.Count; r2++)
                            {
                                String _itemcode = (dgv_subitem["dgv_sub_item_itemcode", r2].Value ?? "").ToString();
                                if (itemcode_frm_ro == _itemcode)
                                {
                                    isExist = true;
                                }
                            }

                            if (!isExist)
                            {

                                int i = dgv_subitem.Rows.Add();
                                DataGridViewRow row = frm_work_shopload.dgv_subitem.Rows[r];

                                dgv_subitem["dgv_sub_item_qty", i].Value = row.Cells["dgv_sub_item_qty"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitdesc", i].Value = row.Cells["dgv_sub_item_unitdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_unitid", i].Value = row.Cells["dgv_sub_item_unitid"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemcode", i].Value = row.Cells["dgv_sub_item_itemcode"].Value.ToString();
                                dgv_subitem["dgv_sub_item_itemdesc", i].Value = row.Cells["dgv_sub_item_itemdesc"].Value.ToString();
                                dgv_subitem["dgv_sub_item_partno", i].Value = row.Cells["dgv_sub_item_partno"].Value.ToString();
                                //dgv_subitem["dgv_sub_item_costprice", i].Value = row.Cells["dgv_sub_item_costprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_lnamnt", i].Value = row.Cells["dgv_sub_item_lnamnt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_amt", i].Value = row.Cells["dgv_sub_item_disc_amt"].Value.ToString();
                                dgv_subitem["dgv_sub_item_sellprice", i].Value = row.Cells["dgv_sub_item_sellprice"].Value.ToString();
                                dgv_subitem["dgv_sub_item_in_tax", i].Value = row.Cells["dgv_sub_item_in_tax"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_code", i].Value = row.Cells["dgv_sub_item_disc_code"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_user", i].Value = row.Cells["dgv_sub_item_disc_user"].Value.ToString();
                                dgv_subitem["dgv_sub_item_disc_reason", i].Value = row.Cells["dgv_sub_item_disc_reason"].Value.ToString();
                                dgv_subitem["dgv_sub_item_notes", i].Value = row.Cells["dgv_sub_item_notes"].Value.ToString();
                                dgv_subitem["dgv_sub_item_net", i].Value = row.Cells["dgv_sub_item_net"].Value.ToString();

                            }
                        }
                    }
                }
            }
            catch { }
        
        
        }

        private void disp_totals(){

            try
            {
                double ttax = 0, tdisc = 0, tline = 0, treg = 0,tsell = 0,net=0;
                for (int r = 0; r < dgv_subitem.Rows.Count; r++)
                {
                    ttax += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_in_tax", r].Value ?? "0").ToString());
                    tdisc += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_disc_amt", r].Value ?? "0").ToString());
                    tline += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_lnamnt", r].Value ?? "0").ToString());
                    //treg += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_costprice", r].Value ?? "0").ToString());
                    tsell += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_sellprice", r].Value ?? "0").ToString());
                    net += gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_net", r].Value ?? "0").ToString());
                }
                txt_ttax.Text = gm.toAccountingFormat(ttax);
                txt_tdisc.Text = gm.toAccountingFormat(tdisc);
                txt_tline.Text = gm.toAccountingFormat(tline);
               // txt_treg.Text = gm.toAccountingFormat(treg); 
                txt_vat.Text = gm.toAccountingFormat(net);
            }
            catch { }
        }

        private void del_item()
        {
            try
            {
                if (dgv_subitem.Rows.Count == 1)
                {
                    MessageBox.Show("No item selected.");
                }
                else
                {
                    int i;
                    DialogResult dialogResult = MessageBox.Show("Confirm", "Are you sure you want to remove this order?", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        i = dgv_subitem.CurrentRow.Index;
                        dgv_subitem.Rows.RemoveAt(i);
                        disp_totals();
                        //total_amountdue();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }
        private void btn_addsubitem_Click(object sender, EventArgs e)
        {
            add_item("");
            
        }
        private void add_item(String typ)
        {
            int lastrow = 0;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_subitem.Rows.Count;
                    lnno_last = int.Parse(dgv_subitem["dgvli_lnno", lastrow].Value.ToString());
                    inc_lnno();
                }
                else
                {
                    if (dgv_subitem.Rows.Count == 0)
                    {
                        lnno_last = 0;
                        inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_subitem.Rows.Count;
                        lnno_last = int.Parse(dgv_subitem["dgvli_lnno", lastrow].Value.ToString());
                        inc_lnno();
                    }
                }
            }
            catch { }

            //frm_ssi = new z_enter_item_simple(this, true, 0,"","","","","","","","",0);
            //frm_ssi.ShowDialog();

            //notes
            frm_si = new z_enter_sales_item(this, true, "", ""/*typ*/, lnno_last);
            frm_si.ShowDialog();
            checker1 = "1";
        }
        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }


        private void btn_updsubitem_Click(object sender, EventArgs e)
        {
            
            upd_item();
            checker1 = "1";
        }
        public void upd_item()
        {
            //int r = 0;
            //String itemcode = "", itemdesc = "", unitid = "", qty = "", amount = "", price = "" ;
            // ln_num, ord_line, item_code="", item_desc="", unit_id="", unit_desc="", ln_amnt="", ord_qty="", price="", ln_tax="", rep_code, reference, trnx_date, t_time, fcp = "0.00", part_no = "";
            try
            {
                if (dgv_subitem.Rows.Count > 0)
                {

                    frm_si = new z_enter_sales_item(this, false, "", ""/*typ*/, dgv_subitem.CurrentRow.Index /*lnno_last*/);
                    frm_si.ShowDialog();

                    /*r = dgv_subitem.CurrentRow.Index;
                   
                   try
                   {
                       ln_num = lnno;
                       item_code = dgv_subitem["dgv_sub_item_itemcode", r].Value.ToString();
                       item_desc = dgv_subitem["dgv_sub_item_itemdesc", r].Value.ToString();
                       ord_qty = dgv_subitem["dgv_sub_item_qty", r].Value.ToString();
                       //ord_qty = ord_qty.Replace(",", "");
                       unit_id = dgv_subitem["dgv_sub_item_unitid", r].Value.ToString();
                       unit_desc = dgv_subitem["dgv_sub_item_unitdesc", r].Value.ToString();
                       price = dgv_subitem["dgv_sub_item_costprice", r].Value.ToString();
                      // price = price.Replace(",", "");
                       ln_amnt = dgv_subitem["dgv_sub_item_lnamnt", r].Value.ToString();
                       ln_amnt = ln_amnt.Replace(",", "");
                       part_no = dgv_subitem["dgv_sub_item_partno", r].Value.ToString();

                        
                   }
                   catch (Exception er) { MessageBox.Show(er.Message); }
                   MessageBox.Show(item_code.ToString()+" "+item_desc.ToString());
                   frm_ssi = new z_enter_item_simple(this, false, 0, item_code, item_desc, ord_qty, unit_id, unit_desc, price, ln_amnt, part_no,r);
                   frm_ssi.ShowDialog();
                   */

                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }
        public void dgv_salesitem(DataTable dt, Boolean isnewitem)
        {
            int i = 0;

            try
            {
                if (isnewitem)
                {
                    i = dgv_subitem.Rows.Add();
                }
                else
                {
                    i = dgv_subitem.CurrentRow.Index;
                }

                dgv_subitem["dgv_sub_item_qty", i].Value = dt.Rows[0]["dgvli_qty"].ToString();

                dgv_subitem["dgv_sub_item_unitdesc", i].Value = dt.Rows[0]["dgvli_unit"].ToString();
                dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[0]["dgvli_unitid"].ToString();

                dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[0]["dgvli_itemcode"].ToString();
                dgv_subitem["dgv_sub_item_partno", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                dgv_subitem["dgv_sub_item_itemdesc", i].Value = dt.Rows[0]["dgvli_itemdesc"].ToString();

                //dgv_subitem["dgv_sub_item_costprice", i].Value = dt.Rows[0]["dgvli_regprice"].ToString();
                dgv_subitem["dgv_sub_item_lnamnt", i].Value = dt.Rows[0]["dgvli_lnamt"].ToString();

                dgv_subitem["dgv_sub_item_disc_amt", i].Value = dt.Rows[0]["dgvli_discamt"].ToString();
                dgv_subitem["dgv_sub_item_sellprice", i].Value = dt.Rows[0]["dgvli_sellprice"].ToString();
                dgv_subitem["dgv_sub_item_in_tax", i].Value = dt.Rows[0]["dgvli_taxamt"].ToString();

                dgv_subitem["dgv_sub_item_disc_code", i].Value = dt.Rows[0]["dgvli_disccode"].ToString();
                dgv_subitem["dgv_sub_item_disc_user", i].Value = dt.Rows[0]["dgvli_discuser"].ToString();
                dgv_subitem["dgv_sub_item_disc_reason", i].Value = dt.Rows[0]["dgvli_discreason"].ToString();

                dgv_subitem["dgv_sub_item_notes", i].Value = dt.Rows[0]["dgvli_remarks"].ToString();
                dgv_subitem["dgv_sub_item_net", i].Value = (gm.toNormalDoubleFormat(dt.Rows[0]["dgvli_lnamt"].ToString()) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00");
                //dgv_subitem["dgv_sub_item_lnamnt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                //dgv_subitem["dgv_sub_item_costprice", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                //dgv_subitem["dgv_sub_item_lnamnt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();





                if (isnew_item)
                {
                    inc_lnno();
                }
                disp_totals();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
        public void mainsave()
        {
            




        }
        public void add_items()
        {
            String notificationText = null;
            String ln_num, ord_line, item_code, item_desc, unit_id, unit_desc, ln_amnt, ord_qty, price, ln_tax, rep_code, reference, trnx_date, t_time, fcp = "0.00", part_no = "";
            String amt = "";
            String val2 = "";
            String col2 = "ord_code, ln_num, item_code,item_code_ol, item_desc, unitid, qty, cost_pric, lnamnt, part_no";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";
            DataTable dt;
            try
            {
                for (int r = 0; r < dgv_subitem.Rows.Count; r++)
                {
                    ln_num = lnno;
                    item_code = dgv_subitem["dgv_sub_item_itemcode", r].Value.ToString();
                    item_desc = dgv_subitem["dgv_sub_item_itemdesc", r].Value.ToString();
                    ord_qty = gc.toNormalDoubleFormat(dgv_subitem["dgv_sub_item_qty", r].Value.ToString()).ToString("0.00");
                    unit_id = dgv_subitem["dgv_sub_item_unitid", r].Value.ToString();
                    unit_desc = dgv_subitem["dgv_sub_item_unitdesc", r].Value.ToString();
                    price = gc.toNormalDoubleFormat(dgv_subitem["dgv_sub_item_sellprice", r].Value.ToString()).ToString("0.00");
                    ln_amnt = gc.toNormalDoubleFormat(dgv_subitem["dgv_sub_item_lnamnt", r].Value.ToString()).ToString();
                    part_no = dgv_subitem["dgv_sub_item_partno", r].Value.ToString();

                    val2 = "'" + ord_code + "', '" + ln_num + "', '" + item_code + "', '" + code + "', " + db.str_E(item_desc) + ", '" + unit_id + "', '" + ord_qty + "', '" + price + "', '" + ln_amnt + "', '" + part_no + "'";

                    dt = db.QueryBySQLCode("SELECT * FROM rssys.orlne2 WHERE ord_code ='" + ord_code + "' AND item_code='" + item_code + "'");
                    if (dt.Rows.Count > 0)
                    {
                        //db.DeleteOnTable("orlne2", "ord_code='" + ord_code + "' AND item_code='" + item_code + "' AND item_code_ol='" + code + "'");
                        //db.InsertOnTable("orlne2", col2, val2);
                        frm_ro.add_sub_items(col2,val2, ord_code , item_code,code);
                    }
                    else
                    {
                        //db.InsertOnTable("orlne2", col2, val2);
                        frm_ro.add_sub_items(col2, val2, ord_code, item_code,code);

                    }
                    

                }
            }
            catch { }



        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            checker2 = "1";

            if (dgv_subitem.Rows.Count == 0)
            {
                MessageBox.Show("Invalid to save. Empty sub item(s).");
            }
            else {
                DataTable dt = new DataTable();

                dt.Columns.Add("dgv_sub_item_ln");
                dt.Columns.Add("dgv_sub_item_itemdesc");
                dt.Columns.Add("dgv_sub_item_qty");
                dt.Columns.Add("dgv_sub_item_partno");
                dt.Columns.Add("dgv_sub_item_unitdesc");
                //dt.Columns.Add("dgv_sub_item_costprice");
                dt.Columns.Add("dgv_sub_item_lnamnt");
                dt.Columns.Add("dgv_sub_item_unitid");
                dt.Columns.Add("dgv_sub_item_itemcode");

                dt.Columns.Add("dgv_sub_item_disc_amt");
                dt.Columns.Add("dgv_sub_item_sellprice");
                dt.Columns.Add("dgv_sub_item_in_tax");
                dt.Columns.Add("dgv_sub_item_disc_code");
                dt.Columns.Add("dgv_sub_item_disc_user");
                dt.Columns.Add("dgv_sub_item_disc_reason");
                dt.Columns.Add("dgv_sub_item_notes");


                try
                {
                    for (int i = 0; i < dgv_subitem.Rows.Count; i++)
                    {
                        dt.Rows.Add();

                        dt.Rows[i]["dgv_sub_item_ln"] = lnno;//

                        dt.Rows[i]["dgv_sub_item_qty"] = dgv_subitem["dgv_sub_item_qty", i].Value.ToString();//
                        dt.Rows[i]["dgv_sub_item_unitdesc"] = dgv_subitem["dgv_sub_item_unitdesc", i].Value.ToString();
                        dt.Rows[i]["dgv_sub_item_unitid"] = dgv_subitem["dgv_sub_item_unitid", i].Value.ToString();//
                        dt.Rows[i]["dgv_sub_item_itemcode"] = dgv_subitem["dgv_sub_item_itemcode", i].Value.ToString();//
                        dt.Rows[i]["dgv_sub_item_itemdesc"] = dgv_subitem["dgv_sub_item_itemdesc", i].Value.ToString();//
                        dt.Rows[i]["dgv_sub_item_partno"] = dgv_subitem["dgv_sub_item_partno", i].Value.ToString();

                        //dt.Rows[i]["dgv_sub_item_costprice"] = dgv_subitem["dgv_sub_item_costprice", i].Value.ToString();//
                        dt.Rows[i]["dgv_sub_item_lnamnt"] = dgv_subitem["dgv_sub_item_lnamnt", i].Value.ToString();//

                        dt.Rows[i]["dgv_sub_item_disc_amt"] = (dgv_subitem["dgv_sub_item_disc_amt", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_sellprice"] = (dgv_subitem["dgv_sub_item_sellprice", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_in_tax"] = (dgv_subitem["dgv_sub_item_in_tax", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_disc_code"] = (dgv_subitem["dgv_sub_item_disc_code", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_disc_user"] = (dgv_subitem["dgv_sub_item_disc_user", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_disc_reason"] = (dgv_subitem["dgv_sub_item_disc_reason", i].Value ?? "").ToString();//
                        dt.Rows[i]["dgv_sub_item_notes"] = (dgv_subitem["dgv_sub_item_notes", i].Value ?? "").ToString();//
                    }
                }
                catch (Exception er)
                { MessageBox.Show(er.Message); }

                if (frm_ro != null)
                {
                    int r = frm_ro.dgv_itemlist.CurrentRow.Index;
                    int qty = (int)gm.toNormalDoubleFormat((frm_ro.dgv_itemlist["dgvli_qty", r].Value ?? "0").ToString());
                    String itemdesc = frm_ro.dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                    if (!itemdesc.Contains("CUSTOMIZED-"))
                    {
                        frm_ro.dgv_itemlist["dgvli_itemdesc", r].Value = "CUSTOMIZED-" + itemdesc;
                    }

                    frm_ro.dgv_itemlist["dgvli_lnamt", r].Value = gm.toAccountingFormat((qty * gm.toNormalDoubleFormat(txt_tline.Text)));

                    frm_ro.dgv_itemlist["dgvli_taxamt", r].Value = gm.toAccountingFormat((qty * gm.toNormalDoubleFormat(txt_ttax.Text)));
                    frm_ro.dgv_itemlist["dgvli_discamt", r].Value = gm.toAccountingFormat((qty * gm.toNormalDoubleFormat(txt_tdisc.Text)));
                    frm_ro.dgv_itemlist["dgvli_net", r].Value = gm.toAccountingFormat((qty * gm.toNormalDoubleFormat(txt_vat.Text)));
                    frm_ro.dgv_itemlist["dgvli_disccode", r].Value = "";
                    frm_ro.dgv_itemlist["dgvli_discuser", r].Value = "";
                    frm_ro.dgv_itemlist["dgvli_discreason", r].Value = "";
                    
                    frm_ro.total_amountdue();
                    frm_ro.dgv_sales_subitem(dt, lnno, true);

                    /*
                    if (checker1 == "1" && checker2 == "1")
                    {
                        if (checker1 == "1" && checker2 == "1")
                        {
                            frm_ro.dgv_itemlist["dgvli_lnamt", r].Value = gm.toAccountingFormat(txt_tline.Text);
                            frm_ro.dgv_itemlist["dgvli_discamt", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) * 0.10).ToString("0.00");

                            frm_ro.dgv_itemlist["dgvli_net", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00");

                            frm_ro.dgv_itemlist["dgvli_taxamt", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) - gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_net", r].Value.ToString())).ToString("0.00");

                            //dgvli_net
                            //dgvli_taxamt
                        }
                        else if (checker2 == "1" && checker1 == "0")
                        {
                            //frm_ro.dgv_itemlist["dgvli_lnamt", r].Value = gm.toAccountingFormat(txt_tline.Text);
                            frm_ro.dgv_itemlist["dgvli_discamt", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) * 0.10).ToString("0.00");

                            frm_ro.dgv_itemlist["dgvli_net", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00");

                            frm_ro.dgv_itemlist["dgvli_taxamt", r].Value = (gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_lnamt", r].Value.ToString()) - gm.toNormalDoubleFormat(frm_ro.dgv_itemlist["dgvli_net", r].Value.ToString())).ToString("0.00");
                        }
                        else
                        {
                            frm_ro.dgv_itemlist["dgvli_discamt", r].Value = discount;
                        }
                    }*/
                }
                add_items();
                this.Close();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            checker1 = "1";
            del_item();
        }

        public String get_item_ln(int currow)
        {
            String val = "";

            val = lnno;

            return val;
        }

        public String get_item_code(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_itemcode", currow].Value.ToString();

            return val;
        }

        public String get_item_desc(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_itemdesc", currow].Value.ToString();

            return val;
        }

        public String get_unit_id(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_unitid", currow].Value.ToString();

            return val;
        }

        public String get_item_qty(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_qty", currow].Value.ToString();

            return val;
        }

        public String get_cost_price(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_costprice", currow].Value.ToString();

            if (val == null)
                val = "0";

            return val;
        }

        public String get_item_lnamt(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_lnamnt", currow].Value.ToString();

            return val;
        }
        public String get_item_partno(int currow)
        {
            String val = "";

            val = dgv_subitem["dgv_sub_item_partno", currow].Value.ToString();

            return val;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
