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
    public partial class z_enter_sales_item : Form
    {
        s_Sales frm_sales;
        s_Sales_Auto frm_salesauto;
        s_RepairOrder frm_repairorder;
        s_ServiceHistory frm_servicehistory;
        s_sub_items frm_subitems;
        s_GP_Computation frm_gp_compute;
        z_ItemSearch frm_itemsearch;
        z_clerkpassword frm_pclerkpass;
        z_approval frm_approval;
        s_Work_Shopload frm_work_shopload;
        GlobalClass gc;
        GlobalMethod gm;

        Boolean _isnew;
        String _tsrch;
        String _ttype; //D = description; C = code, Q = quantity
        dbVehicle dbv;
        dbSales db;
        Boolean seltbp = false;
        int lnno = 1, dgvrow=1;    
        private bool p1;
        private string p2;
        private int lnno_last;
        Double out_taxpct = 0.12;

        public z_enter_sales_item(s_Sales frmsales, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_sales = frmsales;
            init_load(isnew, tsrch, ttype, ln);

            try
            {
                out_taxpct = db.get_outlet_govt_pct(frm_sales.cbo_outlet.SelectedValue.ToString()) / 100;
            }
            catch { out_taxpct = 0.00; }
        }

        public z_enter_sales_item(s_RepairOrder frmrepairorder, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_repairorder = frmrepairorder;
            init_load(isnew, tsrch, ttype, ln);

            txt_t_date.Visible = true;
            cbo_clerk.Visible = true;

            try
            {
                out_taxpct = db.get_outlet_govt_pct(frm_repairorder.cbo_outlet.SelectedValue.ToString()) / 100;
            }
            catch { out_taxpct = 0.00; }
        }

        public z_enter_sales_item(s_ServiceHistory frmservicehistory, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_servicehistory = frmservicehistory;
            init_load(isnew, tsrch, ttype, ln);
        }
        public z_enter_sales_item(s_Work_Shopload frm, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_work_shopload = frm;
            init_load(isnew, tsrch, ttype, ln);
        }

        public z_enter_sales_item(s_Sales_Auto frmsalesauto, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_salesauto = frmsalesauto;
            init_load(isnew, tsrch, ttype, ln);
        }
        
        public z_enter_sales_item(s_GP_Computation frm, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_gp_compute = frm;
            init_load(isnew, tsrch, ttype, ln);
        }


        public z_enter_sales_item(s_sub_items frmsubitems, Boolean isnew, String tsrch, String ttype, int ln)
        {
            InitializeComponent();

            frm_subitems = frmsubitems;
            init_load(isnew, tsrch, ttype, ln);
        }
        
        private void init_load(Boolean isnew, String tsrch, String ttype, int ln)
        {
            db = new dbSales();
            dbv = new dbVehicle();
            DataTable dt = new DataTable();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            frm_pclerkpass = new z_clerkpassword(this);

            gc.load_userid(cbo_approvedby);
            cbo_approvedby.Hide();

            gc.load_saleunit(cbo_unit);
            gc.load_discount(cbo_disc_typ);
            _isnew = isnew;
            _tsrch = tsrch;
            _ttype = ttype;
            

            if (_isnew)
            {
                lbl_lnno.Text = ln.ToString();
                if (_ttype == "C")
                {
                    dt = db.get_itemForSaleEntry(tsrch);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            enter_salesitem(dt.Rows[0]["item_code"].ToString(), dt.Rows[0]["item_desc"].ToString(), dt.Rows[0]["sale_unit"].ToString(), dt.Rows[0]["sell_pric"].ToString(), dt.Rows[0]["part_no"].ToString(), dt.Rows[0]["item_grp"].ToString(), dt.Rows[0]["brd_code"].ToString());
                        }
                    }
                }
                else
                {
                    frm_itemsearch = new z_ItemSearch(this, _tsrch, _ttype);
                    frm_itemsearch.ShowDialog();
                }
                if (frm_repairorder != null)
                {
                    txt_t_date.Text = db.get_systemdate("");
                    cbo_clerk.Text = frm_repairorder.cbo_clerk.Text;
                }
            }
                //update item
            else
            {
                dgvrow = ln;
                load_fields_upd();
            }

            
        }

        private void z_enter_sales_item_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txt_qty;
            }
            catch (Exception er) { MessageBox.Show(er.Message);  }
        }

        private void load_fields_upd()
        {
            if (frm_sales != null && _isnew == false)
            {
                try { lbl_lnno.Text = frm_sales.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }catch{}
                try { txt_item_code.Text = frm_sales.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }catch{}
                try { txt_part_no.Text = frm_sales.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }catch{}
                try { txt_itemdesc.Text = frm_sales.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }catch{}
                try { txt_qty.Text = frm_sales.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }catch{}
                try { cbo_unit.SelectedValue = frm_sales.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }catch{}

                try { txt_sellprice.Text = frm_sales.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }catch{}
                try { txt_regprice.Text = frm_sales.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }catch{}
                try { txt_lnamt.Text = frm_sales.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }catch{}

                try { txt_disc_amt.Text = frm_sales.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }catch{}
                try { txt_disc_reason.Text = frm_sales.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }catch{}
                try { cbo_approvedby.SelectedValue = frm_sales.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }catch{}
                try { cbo_disc_typ.SelectedValue = frm_sales.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }catch{}

                try { txt_remarks.Text = frm_sales.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }catch { }
            }
            if (frm_salesauto != null && _isnew == false)
            {
                try { lbl_lnno.Text = frm_salesauto.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_item_code.Text = frm_salesauto.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_salesauto.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_salesauto.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_salesauto.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_salesauto.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_salesauto.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_salesauto.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_salesauto.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_salesauto.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_salesauto.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_salesauto.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_salesauto.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_salesauto.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }
                catch { }
            }
            if (frm_gp_compute != null && _isnew == false)
            {
                try { lbl_lnno.Text = frm_gp_compute.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_item_code.Text = frm_gp_compute.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_gp_compute.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_gp_compute.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_gp_compute.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_gp_compute.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_gp_compute.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_gp_compute.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_gp_compute.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_gp_compute.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_gp_compute.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_gp_compute.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_gp_compute.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_gp_compute.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }
                catch { }
            }
            if (frm_repairorder != null && _isnew == false)
            {
                try { lbl_lnno.Text = frm_repairorder.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_item_code.Text = frm_repairorder.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_repairorder.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_repairorder.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_repairorder.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_repairorder.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_repairorder.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_repairorder.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_repairorder.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_repairorder.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_repairorder.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_repairorder.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_repairorder.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_repairorder.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }
                catch { }

                try { txt_t_date.Text = frm_repairorder.dgv_itemlist["dgvli_t_date", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_clerk.Text = frm_repairorder.dgv_itemlist["dgvli_clerk", dgvrow].Value.ToString(); }
                catch { }
                
            }
            if (frm_servicehistory != null && _isnew == false)
            {
                txt_qty.ReadOnly = true;
                btn_save.Enabled = false;
                btn_search.Enabled = false;
                cbo_unit.Enabled = false;
                txt_remarks.ReadOnly = true;
                btn_discount.Enabled = false;
                
                try { lbl_lnno.Text = frm_servicehistory.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_item_code.Text = frm_servicehistory.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_servicehistory.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_servicehistory.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_servicehistory.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_servicehistory.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_servicehistory.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_servicehistory.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_servicehistory.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_servicehistory.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_servicehistory.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_servicehistory.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_servicehistory.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_servicehistory.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }
                catch { }

            }
            if (frm_work_shopload != null && _isnew == false)
            {
                txt_qty.ReadOnly = true;
                btn_save.Enabled = false;
                btn_search.Enabled = false;
                cbo_unit.Enabled = false;
                txt_remarks.ReadOnly = true;
                btn_discount.Enabled = false;

                try { lbl_lnno.Text = frm_work_shopload.dgv_itemlist["dgvli_lnno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_item_code.Text = frm_work_shopload.dgv_itemlist["dgvli_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_work_shopload.dgv_itemlist["dgvi_part_no", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_work_shopload.dgv_itemlist["dgvli_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_work_shopload.dgv_itemlist["dgvli_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_work_shopload.dgv_itemlist["dgvli_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_work_shopload.dgv_itemlist["dgvli_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_work_shopload.dgv_itemlist["dgvli_regprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_work_shopload.dgv_itemlist["dgvli_lnamt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_work_shopload.dgv_itemlist["dgvli_discamt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_work_shopload.dgv_itemlist["dgvli_discreason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_work_shopload.dgv_itemlist["dgvli_discuser", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_work_shopload.dgv_itemlist["dgvli_disccode", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_work_shopload.dgv_itemlist["dgvli_remarks", dgvrow].Value.ToString(); }
                catch { }

            }
            if (frm_subitems != null && _isnew == false)
            {
                try { lbl_lnno.Text = "###"; }
                catch { }
                try { txt_item_code.Text = frm_subitems.dgv_subitem["dgv_sub_item_itemcode", dgvrow].Value.ToString(); }
                catch { }
                try { txt_part_no.Text = frm_subitems.dgv_subitem["dgv_sub_item_partno", dgvrow].Value.ToString(); }
                catch { }
                try { txt_itemdesc.Text = frm_subitems.dgv_subitem["dgv_sub_item_itemdesc", dgvrow].Value.ToString(); }
                catch { }
                try { txt_qty.Text = frm_subitems.dgv_subitem["dgv_sub_item_qty", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_unit.SelectedValue = frm_subitems.dgv_subitem["dgv_sub_item_unitid", dgvrow].Value.ToString(); }
                catch { }

                try { txt_sellprice.Text = frm_subitems.dgv_subitem["dgv_sub_item_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_regprice.Text = frm_subitems.dgv_subitem["dgv_sub_item_sellprice", dgvrow].Value.ToString(); }
                catch { }
                try { txt_lnamt.Text = frm_subitems.dgv_subitem["dgv_sub_item_lnamnt", dgvrow].Value.ToString(); }
                catch { }

                try { txt_disc_amt.Text = frm_subitems.dgv_subitem["dgv_sub_item_disc_amt", dgvrow].Value.ToString(); }
                catch { }
                try { txt_disc_reason.Text = frm_subitems.dgv_subitem["dgv_sub_item_disc_reason", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_approvedby.SelectedValue = frm_subitems.dgv_subitem["dgv_sub_item_disc_user", dgvrow].Value.ToString(); }
                catch { }
                try { cbo_disc_typ.SelectedValue = frm_subitems.dgv_subitem["dgv_sub_item_disc_code", dgvrow].Value.ToString(); }
                catch { }

                try { txt_remarks.Text = frm_subitems.dgv_subitem["dgv_sub_item_notes", dgvrow].Value.ToString(); }
                catch { }
            }
        }

        private void _txt_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void _txt_WholeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void save_order()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("dgvli_lnno");
            dt.Columns.Add("dgvli_qty");

            dt.Columns.Add("dgvli_unit");
            dt.Columns.Add("dgvli_unitid");

            dt.Columns.Add("dgvli_itemcode");
            dt.Columns.Add("dgvi_part_no");
            dt.Columns.Add("dgvli_itemdesc");

            dt.Columns.Add("dgvli_regprice");
            dt.Columns.Add("dgvli_discamt");
            dt.Columns.Add("dgvli_sellprice");
            dt.Columns.Add("dgvli_lnamt");
            dt.Columns.Add("dgvli_taxamt");

            dt.Columns.Add("dgvli_car_plate");
            dt.Columns.Add("dgvli_car_plate_type");
            dt.Columns.Add("dgvli_car_model");
            dt.Columns.Add("dgvli_car_vin_num");
            dt.Columns.Add("dgvli_car_engine");

            dt.Columns.Add("dgvli_car_brand_desc");
            dt.Columns.Add("dgvli_car_brand_id");
            dt.Columns.Add("dgvi_car_color_desc");
            dt.Columns.Add("dgvli_car_color_id");
            dt.Columns.Add("dgvi_car_type_desc");
            dt.Columns.Add("dgvli_car_type_id");
            dt.Columns.Add("dgvi_dealer_name");
            dt.Columns.Add("dgvli_dealer_id");

            dt.Columns.Add("dgvli_car_date_released");
            dt.Columns.Add("dgvli_last_km_reading");
            dt.Columns.Add("dgvli_warrantyto");

            dt.Columns.Add("dgvli_discuser");
            dt.Columns.Add("dgvli_discreason");
            dt.Columns.Add("dgvli_disccode");

            dt.Columns.Add("dgvli_remarks");

            if(String.IsNullOrEmpty(cbo_unit.Text))
            {
                MessageBox.Show("Invalid unit");
            }
            else
            {
                if (gm.toNormalDoubleFormat(txt_qty.Text) <= 0)
                {
                    MessageBox.Show("Invalid quantity.");
                }
                else if (gm.toNormalDoubleFormat(txt_disc_amt.Text) < 0)
                {
                    MessageBox.Show("Invalid discount amount.");
                }
                else
                {
                    dt.Rows.Add();

                    dt.Rows[0]["dgvli_lnno"] = gm.toStr(lbl_lnno.Text);
                    dt.Rows[0]["dgvli_qty"] = txt_qty.Text;

                    dt.Rows[0]["dgvli_unit"] = cbo_unit.Text;
                    dt.Rows[0]["dgvli_unitid"] = cbo_unit.SelectedValue.ToString();

                    dt.Rows[0]["dgvli_itemcode"] = txt_item_code.Text;
                    dt.Rows[0]["dgvi_part_no"] = txt_part_no.Text;
                    dt.Rows[0]["dgvli_itemdesc"] = txt_itemdesc.Text;

                    dt.Rows[0]["dgvli_regprice"] = gm.toNormalDoubleFormat(txt_regprice.Text);
                    dt.Rows[0]["dgvli_discamt"] = gm.toNormalDoubleFormat(txt_disc_amt.Text);
                    dt.Rows[0]["dgvli_sellprice"] = gm.toNormalDoubleFormat(txt_sellprice.Text);
                    dt.Rows[0]["dgvli_lnamt"] = gm.toNormalDoubleFormat(txt_lnamt.Text);
                    dt.Rows[0]["dgvli_taxamt"] = gm.toNormalDoubleFormat(txt_lnamt.Text)*0.12;
                    
                    dt.Rows[0]["dgvli_remarks"] = txt_remarks.Text;

                    dt.Rows[0]["dgvli_car_plate"] = "";
                    dt.Rows[0]["dgvli_car_plate_type"] = "";
                    dt.Rows[0]["dgvli_car_model"] = "";
                    dt.Rows[0]["dgvli_car_vin_num"] = "";
                    dt.Rows[0]["dgvli_car_engine"] = "";

                    dt.Rows[0]["dgvli_car_brand_desc"] = "";
                    dt.Rows[0]["dgvli_car_brand_id"] = "";
                    dt.Rows[0]["dgvi_car_color_desc"] = "";
                    dt.Rows[0]["dgvli_car_color_id"] = "";
                    dt.Rows[0]["dgvi_car_type_desc"] = "";
                    dt.Rows[0]["dgvli_car_type_id"] = "";
                    dt.Rows[0]["dgvi_dealer_name"] = "";
                    dt.Rows[0]["dgvli_dealer_id"] = "";

                    dt.Rows[0]["dgvli_car_date_released"] = "";
                    dt.Rows[0]["dgvli_last_km_reading"] = "";
                    dt.Rows[0]["dgvli_warrantyto"] = "";

                    dt.Rows[0]["dgvli_disccode"] = "";
                    dt.Rows[0]["dgvli_discuser"] = "";
                    dt.Rows[0]["dgvli_discreason"] = "";

                    //discount
                    if (cbo_disc_typ.SelectedIndex > -1)
                    {
                        dt.Rows[0]["dgvli_disccode"] = cbo_disc_typ.SelectedValue.ToString();

                        if (cbo_approvedby.SelectedIndex > -1)
                        {
                            dt.Rows[0]["dgvli_discuser"] = cbo_approvedby.SelectedValue.ToString();
                        }
                        
                        dt.Rows[0]["dgvli_discreason"] = txt_disc_reason.Text;
                    }

                    if(frm_sales != null)
                    {
                        frm_sales.dgv_salesitem(dt, _isnew);
                    }
                    else if(frm_repairorder!=null)
                    {
                        frm_repairorder.dgv_salesitem(dt,_isnew);
                    }

                    else if (frm_salesauto != null)
                    {
                        frm_salesauto.dgv_salesitem(dt, _isnew);
                    }
                    else if (frm_gp_compute != null)
                    {
                        frm_gp_compute.dgv_salesitem(dt, _isnew);
                    }
                    else if (frm_subitems != null)
                    {
                        frm_subitems.dgv_salesitem(dt, _isnew);
                    }
                    this.Close();
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            frm_itemsearch = new z_ItemSearch(this, _tsrch, _ttype);
            frm_itemsearch.ShowDialog();
        }

        public void enter_salesitem(String code, String name, String unit, String regprice, String partno, String itmgrp, String brd)
        {
            //try
            //{
            String vin = "";

                txt_item_code.Text = code;
                txt_part_no.Text = partno;
                txt_itemdesc.Text = name;
                cbo_unit.SelectedValue = unit;
                txt_sellprice.Text = regprice;
                txt_regprice.Text = regprice;                
           // }
           // catch (Exception er) { MessageBox.Show(er.Message); }
        }

        public void enter_salesitem_upd(String code, String name, String unit, String qty, String disc_pct, String disc_amt, String regprice, String sellprice, String lnamt)
        {
            txt_item_code.Text = code;
            txt_itemdesc.Text = name;
            cbo_unit.SelectedValue = unit;
            txt_qty.Text = qty;
            txt_sellprice.Text = sellprice;
            txt_regprice.Text = regprice;
            txt_disc_amt.Text = disc_amt;
            txt_lnamt.Text = lnamt;
        }

        private void total_amount()
        {
            try
            {
                Double qty = gm.toNormalDoubleFormat(txt_qty.Text);
                Double price =  gm.toNormalDoubleFormat(txt_sellprice.Text);
                Double disc_amt = gm.toNormalDoubleFormat(txt_disc_amt.Text);
                Double ln_amt = ((qty * price) - disc_amt);

                //txt_sellprice.Text = gm.toAccountingFormat(price);
                //txt_disc_amt.Text = gm.toAccountingFormat(disc_amt);
                txt_lnamt.Text = gm.toAccountingFormat(ln_amt);
            }
            catch (Exception) { }
        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            save_order();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            total_amount();

            if (frm_repairorder != null)
            {
                if (frm_repairorder.txt_servex.Text != "" && frm_repairorder.txt_servex_disc_pct.Text == "10")
                {
                    String disccal = "";
                    cbo_disc_typ.SelectedValue = "002";
                    disccal = cbo_disc_typ.SelectedValue.ToString();
                    
                    String disc_code = "";
                    if (disccal=="002")
                    {
                        // cbo_disc_typ.Enabled = true;
                        Double discpct = 0.00, discval = 0.00, regprice = gm.toNormalDoubleFormat(txt_regprice.Text), sellingprice = gm.toNormalDoubleFormat(txt_sellprice.Text), qty = gm.toNormalDoubleFormat(txt_qty.Text);

                        if (cbo_disc_typ.SelectedIndex != -1)
                        {
                            disc_code = cbo_disc_typ.SelectedValue.ToString();
                            discpct = db.get_discpct(disc_code);
                            discval = ((sellingprice * qty) * (discpct / 100));
                            txt_disc_amt.Text = gm.toAccountingFormat(discval);
                            txt_disc_reason.Text = "SERVEX DISCOUNT";
                        }
                    }
                }

            }
            if (frm_sales != null)
            {
                if (frm_sales.txt_servex.Text != "" && frm_sales.txt_servex_disc_pct.Text == "10")
                {
                    String disccal = "";
                    cbo_disc_typ.SelectedValue = "002";
                    disccal = cbo_disc_typ.SelectedValue.ToString();

                    String disc_code = "";
                    if (disccal == "002")
                    {
                        // cbo_disc_typ.Enabled = true;
                        Double discpct = 0.00, discval = 0.00, regprice = gm.toNormalDoubleFormat(txt_regprice.Text), sellingprice = gm.toNormalDoubleFormat(txt_sellprice.Text), qty = gm.toNormalDoubleFormat(txt_qty.Text);

                        if (cbo_disc_typ.SelectedValue == disccal)
                        {
                            disc_code = cbo_disc_typ.SelectedValue.ToString();
                            discpct = db.get_discpct(disc_code);
                            discval = ((sellingprice * qty) * (discpct / 100));
                            txt_disc_amt.Text = gm.toAccountingFormat(discval);
                            txt_disc_reason.Text = "SERVEX DISCOUNT";
                        }
                    }
                }

            }
        }
        
        private void txt_disc_amt_TextChanged(object sender, EventArgs e)
        {
            total_amount();
        }
       
        private void txt_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                save_order();
            }
        }
        
        private void txt_disc_amt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                save_order();
            }
        }

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            txt_qty.Text = gm.toAccountingFormat(txt_qty.Text);
        }

        private void txt_disc_amt_Leave(object sender, EventArgs e)
        {
            txt_disc_amt.Text = gm.toAccountingFormat(txt_disc_amt.Text);
        }

        private void btn_discount_Click(object sender, EventArgs e)
        {
            if (btn_discount.Text == "Get Discount")
            {
                frm_pclerkpass.ShowDialog();
            }
            else
            {
                cbo_disc_typ.SelectedIndex = -1;
                cbo_approvedby.SelectedIndex = -1;
                txt_disc_amt.Text = "0.00";
                txt_disc_reason.Text = "";

                cbo_disc_typ.Enabled = false;
                cbo_approvedby.Enabled = false;
                txt_disc_amt.ReadOnly = true;
                txt_disc_reason.ReadOnly = true;
                btn_discount.Text = "Get Discount";

                lbl_discreason_txt.ForeColor = Color.Gray;
                lbl_disctyp_txt.ForeColor = Color.Gray;
                lbl_discamt_txt.ForeColor = Color.Gray;
                txt_disc_amt.ForeColor = Color.Gray;
                txt_disc_reason.ForeColor = Color.Gray;
            }
        }

        public void verifyUserIdForDiscount(String id)
        {
            cbo_disc_typ.Enabled = true;
            txt_disc_reason.Enabled = true;
            txt_disc_reason.ReadOnly = false;

            cbo_approvedby.SelectedValue = id;
            cbo_approvedby.Show();
            btn_discount.Text = "Cancel Discount";

            lbl_discreason_txt.ForeColor = Color.Black;
            lbl_disctyp_txt.ForeColor = Color.Black;
            lbl_discamt_txt.ForeColor = Color.Black;
            txt_disc_amt.ForeColor = Color.Black;
            txt_disc_reason.ForeColor = Color.Black;
        }

        private void cbo_disc_typ_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                String disc_code ="";
                Double discpct = 0.00, discval = 0.00, regprice = gm.toNormalDoubleFormat(txt_regprice.Text), sellingprice = gm.toNormalDoubleFormat(txt_sellprice.Text), qty = gm.toNormalDoubleFormat(txt_qty.Text);

                if(cbo_disc_typ.SelectedIndex != -1)
                {
                    disc_code =  cbo_disc_typ.SelectedValue.ToString();
                    discpct = db.get_discpct(disc_code);
                    discval = ((sellingprice * qty) * (discpct / 100));
                    txt_disc_amt.Text = gm.toAccountingFormat(discval);
                }
            }
            catch(Exception) {}
        }

        private void txt_item_code_TextChanged(object sender, EventArgs e)
        {
            lbl_part_no_title.Show();
            txt_part_no.Show();

            goto_win1();
        }

        private void goto_win1()
        {
            seltbp = true;

            tbcntrl_entry.SelectedTab = tpg_iteminfo;
            tpg_iteminfo.Show();

            seltbp = false;
        }

        private void tbcntrl_entry_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void txt_lnamt_TextChanged(object sender, EventArgs e)
        {
            compute_NET_and_VAT(false);
        }

        private void compute_NET_and_VAT(Boolean tax_manual)
        {
            Double lnamnt = gm.toNormalDoubleFormat(txt_lnamt.Text);
            Double netamnt = 0.00;
            Double taxamnt = 0.00;

            try
            {
                if(tax_manual)
                {
                    taxamnt = gm.toNormalDoubleFormat(txt_taxamnt.Text);
                    netamnt = lnamnt - taxamnt;
                }
                else
                {
                    lnamnt = gm.toNormalDoubleFormat(txt_lnamt.Text);
                    netamnt = lnamnt / (1 + out_taxpct);
                    taxamnt = lnamnt - netamnt;
                    txt_taxamnt.Text = gm.toAccountingFormat(taxamnt);
                }

                txt_lnamt.Text = gm.toAccountingFormat(lnamnt);
                txt_netamnt.Text = gm.toAccountingFormat(netamnt);
            }
            catch { }
        }

        private void txt_sellprice_TextChanged(object sender, EventArgs e)
        {
            total_amount();
        }

        private void cbo_disc_typ_SelectedIndexChanged(object sender, EventArgs e)
        {
            String disc_code = "";

            if(disc_code == "ZZZ")
            {
                txt_disc_amt.ReadOnly = false;
            }
            else
            {
                txt_disc_amt.ReadOnly = true;
            }
        }

        private void txt_taxamnt_Leave(object sender, EventArgs e)
        {
            txt_taxamnt.Text = gm.toAccountingFormat(txt_taxamnt.Text);
        }

        private void txt_taxamnt_TextChanged(object sender, EventArgs e)
        {
            compute_NET_and_VAT(true);
        }

        private void txt_taxamnt_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
