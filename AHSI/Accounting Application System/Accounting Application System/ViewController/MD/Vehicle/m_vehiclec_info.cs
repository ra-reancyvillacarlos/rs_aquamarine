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
    public partial class m_vehiclec_info : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        GlobalClass gc;
        GlobalMethod gm;
        dbInv db;
        dbVehicle db_v;

        //forms
        auto_loanapplication frm_auto_loan;
        s_RepairOrder frm_ro;
        s_Sales_Auto frm_sales_auto;
        s_Release_Deliver_Unit frm_reldel_unit;
        s_GP_Computation frm_gm_compute;
        s_Job_Quotation frm_jq;

        Boolean iscallbackfrm = false;
        String _custcode = "";
        String primaryKey; // use in UPDATING

        public m_vehiclec_info()
        {
            init_load();

            disp_list();
        }

        private void init_load()
        {
            InitializeComponent();
            gm = new GlobalMethod();
            gc = new GlobalClass();
            db = new dbInv();

            gc.load_generic(cbo_generic);
            gc.load_company_acct(cbo_dealer);
            gc.load_brand(cbo_brand);
            gc.load_cartype(cbo_ctype);
            gc.load_color(cbo_color);
            gc.load_insurance(cbo_insurance);
            gc.load_customer(cbo_owner);
        }


        public m_vehiclec_info(auto_loanapplication frm, Boolean iscallback)
        {
            init_load();

            frm_auto_loan = frm;
            iscallbackfrm = iscallback;

            disp_list();         
        }


        public m_vehiclec_info(s_Sales_Auto frm, Boolean iscallback)
        {
            init_load();

            frm_sales_auto = frm;
            iscallbackfrm = iscallback;

            try
            {
                if (frm_sales_auto != null)
                {
                    _custcode = frm_sales_auto.cbo_customer.SelectedValue.ToString();
                    cbo_owner.SelectedValue = frm_sales_auto.cbo_customer.SelectedValue.ToString();
                }
            }
            catch (Exception er) { }

            disp_list();
        }
        public m_vehiclec_info(s_Job_Quotation frm, Boolean iscallback)
        {
            init_load();

            frm_jq = frm;
            iscallbackfrm = iscallback;

            try
            {
                _custcode = frm_jq.cbo_customer.SelectedValue.ToString();
                cbo_owner.SelectedValue = frm_jq.cbo_customer.SelectedValue.ToString();
            }
            catch (Exception er) { }

            disp_list();
        }
        public m_vehiclec_info(s_GP_Computation frm, Boolean iscallback)
        {
            init_load();

            frm_gm_compute = frm;
            iscallbackfrm = iscallback;

            try
            {
                if (frm_gm_compute != null)
                {
                    _custcode = frm_gm_compute.cbo_customer.SelectedValue.ToString();
                    cbo_owner.SelectedValue = frm_gm_compute.cbo_customer.SelectedValue.ToString();
                }
            }
            catch (Exception er) { }

            disp_list();
        }
        public m_vehiclec_info(s_Release_Deliver_Unit frm, Boolean iscallback)
        {
            init_load();

            frm_reldel_unit = frm;
            iscallbackfrm = iscallback;

            try
            {
                if (frm_reldel_unit != null)
                {
                    _custcode = frm_reldel_unit.cbo_customer.SelectedValue.ToString();
                    cbo_owner.SelectedValue = frm_reldel_unit.cbo_customer.SelectedValue.ToString();
                }
            }
            catch (Exception er) { }

            disp_list();
        }

        public m_vehiclec_info(s_RepairOrder frm, Boolean iscallback)
        {
            init_load();

            frm_ro = frm;
            iscallbackfrm = iscallback;

            try
            {
                if (frm_ro != null)
                {
                    dgv_list.Columns["d_owner"].Visible = false;
                    _custcode = frm_ro.cbo_customer.SelectedValue.ToString();
                    cbo_owner.SelectedValue = frm_ro.cbo_customer.SelectedValue.ToString();
                }
            }
            catch (Exception er) {  }

            disp_list();
        }

        public m_vehiclec_info(String item_code,String brand, String color, String type, String owner, String po_no, String si_no)
        {
            init_load();

            try { cbo_brand.SelectedValue = brand; }
            catch (Exception) { }

            try { cbo_ctype.SelectedValue = type; }
            catch (Exception) { }

            try { cbo_color.SelectedValue = color; }
            catch (Exception) { }

            try { cbo_insurance.SelectedValue = owner; }
            catch (Exception) { }

            try { cbo_owner.SelectedValue = owner; }
            catch (Exception) { }

            disp_list();
        }

        private void m_vehiclec_info_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);

            if (iscallbackfrm)
            {
                lbl_filter_header.Show();
                dgv_list.Dock = DockStyle.Fill;
            }
            else
            {
                lbl_filter_header.Hide();
            }
        }

        private void frm_clear()
        {
            txt_vin.Text = "";
            txt_model.Text = "";
            txt_engine.Text = "";
            txt_plateno.Text = "";
            txt_vehicle_desc.Text = "";
            cbo_color.SelectedIndex = -1; 
            cbo_ctype.SelectedIndex = -1;
            cbo_brand.SelectedIndex = -1;
            cbo_dealer.SelectedIndex = -1;
            cbo_insurance.SelectedIndex = -1;
            cbo_owner.SelectedIndex = -1;
            cbo_generic.SelectedIndex = -1;
        }
       
        private void goto_tbcntrl_info()
        {
            seltbp = true ;
            tbcntrl_main.SelectedTab = tpg_info;
            tpg_info.Show();
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
           seltbp = false;
        }
        // private void goto_win1()
        //{
        //    seltbp = true;
        //    tbcntrl_left.SelectedTab = tpg_option;
        //    tpg_option.Show();

        //    seltbp = false;
        //}

        // private void goto_win2()
        // {
        //     seltbp = true;
        //     tbcntrl_left.SelectedTab = tpg_option_2;
        //     tpg_option_2.Show();

        //     tbcntrl_main.SelectedTab = tpg_right_entry;
        //     tpg_right_entry.Show();
        //     seltbp = false;
        // }
        private void tpg_info_enable(Boolean flag)
        {
            txt_vin.Enabled = flag;
            
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            try
            {
                isnew = true;
                primaryKey = null;
                txt_vehicle_desc.Enabled = true;
                tpg_info_enable(true);
                frm_clear();
                goto_tbcntrl_info();

                if (frm_ro != null || frm_sales_auto != null || frm_reldel_unit != null || frm_gm_compute != null || frm_jq != null)
                {
                    cbo_owner.SelectedValue = _custcode;
                }
            }
            catch{ }
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["d_code", r].Value.ToString()) == false)
                {
                    isnew = false;
                    primaryKey = dgv_list["d_vin_no", r].Value.ToString() + "." + (dgv_list["d_engine_no", r].Value??"").ToString();
                    tpg_info_enable(true); 
                    frm_clear();
                    disp_info();
                    goto_tbcntrl_info();
                }
            }
            catch { MessageBox.Show("No rows selected"); }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Disabled.");

            //thisDatabase db = new thisDatabase();
            //int r;

            //if (dgv_list.Rows.Count > 1)
            //{
            //    r = dgv_list.CurrentRow.Index;

            //    if (db.UpdateOnTable("vehicle_info", "cancel='Y'", "item_code='" + dgv_list["d_code", r].Value.ToString() + "'"))
            //    {
            //        disp_list();
            //        goto_tbcntrl_list();
            //        tpg_info_enable(false);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed on deleting.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No rows selected.");
            //} 
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4016);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, name, desc,engine;
            DateTime warranty_to;
            
            String col = "", val = "";
            String table = "vehicle_info";

            int r = -1;

            if (String.IsNullOrEmpty(txt_vin.Text) || String.IsNullOrEmpty(txt_vehicle_desc.Text)
                || String.IsNullOrEmpty(txt_engine.Text) || String.IsNullOrEmpty(txt_model.Text))
            {
                MessageBox.Show("Please enter the required fields.");
            }
            else if (cbo_brand.SelectedIndex == -1)
            {
                MessageBox.Show("Please select brand fields.");
                cbo_brand.DroppedDown = true;
            }
            /*else if (cbo_dealer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select repair fields");
                cbo_dealer.DroppedDown = true;
            }
            else if (cbo_insurance.SelectedIndex == -1)
            {
                MessageBox.Show("Please select insurance fields");
                cbo_insurance.DroppedDown = true;
            }
            else if (cbo_owner.SelectedIndex == -1)
            {
                MessageBox.Show("Please select owner fields");
                cbo_owner.DroppedDown = true;
            }*/
            else
            {

                name = txt_vin.Text; 
                desc = txt_vehicle_desc.Text;
                engine = txt_engine.Text;

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.vehicle_info WHERE vin_no=" + db.str_E(name) + " AND engine_no=" + db.str_E(engine) + "");
                if (dt.Rows.Count != 0 && (isnew || primaryKey != name + "." + engine))
                {
                    success = false;
                    MessageBox.Show("Saving Failed: Vin No & Engine No Already Existed."); 
                }
                else 
                {
                    String owner = cbo_owner.SelectedIndex == -1 ? "" : cbo_owner.SelectedValue.ToString();
                    String insurance = cbo_insurance.SelectedIndex == -1 ? "" : cbo_insurance.SelectedValue.ToString();
                    String brand = cbo_brand.SelectedIndex == -1 ? "" : cbo_brand.SelectedValue.ToString();
                    String dealer = cbo_dealer.SelectedIndex == -1 ? "" : cbo_dealer.SelectedValue.ToString();
                    String generic = cbo_generic.SelectedIndex == -1 ? "" : cbo_generic.SelectedValue.ToString();

                    code = txt_vehicle_desc.Text; // is not a code, its VIN DESC
                    warranty_to = dtp_release_date.Value.Date;
                    warranty_to.AddYears(3);

                    if (isnew)
                    {
                        //code = db.get_pk("or_code");
                        //cbo_itemcode.Enabled = true;
                        col = "vin_desc,color,cartype,brand,year_model,vin_no,engine_no,plate_no,date_release,dealer,owner,warranty_to,insurance,generic";
                        val = "'" + code + "','" + cbo_color.Text + "','" + cbo_ctype.Text + "','" + brand + "','" + txt_model.Text + "','" + name + "','" + engine + "','" + txt_plateno.Text + "','" + dtp_release_date.Value.ToString("yyyy-MM-dd") + "','" + dealer + "','" + owner + "','" + warranty_to.Date.ToString("yyyy-MM-dd") + "','" + insurance + "','" + generic + "'";

                        if (db.InsertOnTable(table, col, val))
                        {
                            success = true;
                            //db.set_pkm99(table, db.get_nextincrementlimitchar(code, 8));
                        }
                        else // not used
                        {
                            success = false;
                            //db.DeleteOnTable(table, "vehicle_info='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }                        
                    }
                    else
                    {
                        r = dgv_list.CurrentRow.Index;

                        col = "vin_desc='" + code + "',brand='" + brand + "',cartype='" + cbo_ctype.Text + "',color='" + cbo_color.Text + "'" + ",year_model='" + txt_model.Text + "',vin_no ='" + name + "',engine_no='" + engine + "',plate_no='" + txt_plateno.Text + "',date_release='" + dtp_release_date.Value.ToString("yyyy-MM-dd") + "',warranty_to='" + warranty_to.Date.ToString("yyyy-MM-dd") + "',dealer= '" + dealer + "',owner='" + owner + "',insurance='" + insurance + "',generic='" + generic + "'";

                        try
                        {
                            if (db.UpdateOnTable(table, col, "vin_no||'.'||COALESCE(engine_no,'')='" + primaryKey + "'"))
                            {
                                success = true;
                            }
                            else
                            {
                                success = false;
                                MessageBox.Show("Failed on saving.");
                            }
                        }
                        catch (Exception err) { MessageBox.Show(err.Message); }

                    }
                }

                if (success)
                {
                    if (!iscallbackfrm)
                    {
                        disp_list();
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                        frm_clear();
                        if (r != -1) // para sa update rani
                        {
                            dgv_list.ClearSelection();
                            dgv_list.Rows[r].Selected = true;
                        }
                    }
                    else {
                        if (frm_auto_loan != null)
                        {
                            frm_auto_loan.set_vehicle_frm(name, desc);
                        }
                        else if (frm_sales_auto != null)
                        {
                            frm_sales_auto.set_vehicle_frm(name, desc);
                        }
                        else if (frm_jq != null)
                        {
                            frm_jq.set_vehicle_frm(name, desc);
                        }
                        else if (frm_gm_compute != null)
                        {
                            frm_gm_compute.set_vehicle_frm(name, desc);
                        }
                        else if (frm_reldel_unit != null)
                        {
                            frm_reldel_unit.set_vehicle_frm(name, desc);
                        }
                        else if (frm_ro != null)
                        {
                            frm_ro.set_vehicle_frm(name, desc);
                        }
                        this.Close();
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
        }

        private void clear_dgv()
        {
            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void disp_list()
        {
            thisDatabase db = new thisDatabase();
            String WHERE = "";
            db_v = new dbVehicle();

            if(String.IsNullOrEmpty(_custcode) == false)
            {
                WHERE = " WHERE v.owner=" + db.str_E(_custcode);
            }

            DataTable dt = db.QueryBySQLCode("SELECT v.vin_desc, v.color, v.year_model, v.vin_no, v.engine_no, v.plate_no, v.dealer, to_char(v.date_release, 'MM/dd/yyyy') AS date_release, to_char(v.warranty_to, 'MM/dd/yyyy') AS warranty_to, v.last_km_reading, v.owner, v.cartype, v.brand, v.insurance, v.generic, b.brd_name AS _brand, m.d_name AS _owner, i.d_name AS _insurance, d.comp_name AS _dealer , g.gen_name AS _generic FROM rssys.vehicle_info AS v LEFT JOIN rssys.m06 AS m ON m.d_code=v.owner LEFT JOIN rssys.m06 AS i ON i.d_code=v.insurance LEFT JOIN rssys.brand AS b ON b.brd_code=v.brand LEFT JOIN rssys.company AS d ON d.comp_code=v.dealer LEFT JOIN rssys.generic AS g ON g.gen_code=v.generic" + WHERE);
           
            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["d_Code"].Value = dt.Rows[r]["vin_desc"].ToString();
                    row.Cells["d_color"].Value = dt.Rows[r]["color"].ToString();
                    row.Cells["d_year_model"].Value = dt.Rows[r]["year_model"].ToString();
                    row.Cells["d_vin_no"].Value = dt.Rows[r]["vin_no"].ToString();
                    row.Cells["d_engine_no"].Value = dt.Rows[r]["engine_no"].ToString();
                    row.Cells["d_plate_no"].Value = dt.Rows[r]["plate_no"].ToString();
                    row.Cells["d_date_released"].Value = dt.Rows[r]["date_release"].ToString();
                    row.Cells["d_warranty"].Value = dt.Rows[r]["warranty_to"].ToString();
                    row.Cells["d_ctype"].Value = dt.Rows[r]["cartype"].ToString();

                    row.Cells["d_brand"].Value = dt.Rows[r]["_brand"].ToString();
                    row.Cells["d_owner"].Value = dt.Rows[r]["_owner"].ToString();
                    row.Cells["d_insurance"].Value = dt.Rows[r]["_insurance"].ToString();
                    row.Cells["d_dealer"].Value = dt.Rows[r]["_dealer"].ToString();
                    row.Cells["d_generic"].Value = dt.Rows[r]["_generic"].ToString();

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_vehicle_desc.Text = dgv_list["d_Code", r].Value.ToString();
                
                txt_vin.Text = dgv_list["d_vin_no", r].Value.ToString();
                txt_model.Text = dgv_list["d_year_model", r].Value.ToString();
                txt_engine.Text = dgv_list["d_engine_no", r].Value.ToString();
                txt_plateno.Text = dgv_list["d_plate_no", r].Value.ToString();
                cbo_dealer.Text = dgv_list["d_dealer", r].Value.ToString();
                cbo_color.Text = dgv_list["d_color", r].Value.ToString();
                cbo_ctype.Text = dgv_list["d_ctype", r].Value.ToString();
                dtp_release_date.Value = DateTime.Parse(dgv_list["d_date_released", r].Value.ToString());

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.vehicle_info WHERE vin_no||'.'||COALESCE(engine_no,'')='" + primaryKey + "' LIMIT 1");

                cbo_brand.SelectedValue = dt.Rows[0]["brand"].ToString();
                cbo_owner.SelectedValue = dt.Rows[0]["owner"].ToString();
                cbo_insurance.SelectedValue = dt.Rows[0]["insurance"].ToString();
                cbo_dealer.SelectedValue = dt.Rows[0]["dealer"].ToString();
                cbo_generic.SelectedValue = dt.Rows[0]["generic"].ToString();

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;

        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;

        }
		
		
        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            int r = 0;
            String vin_no = "", vin_desc = "";

            try
            {
                if (iscallbackfrm)
                {
                    r = dgv_list.CurrentRow.Index;

                    if (!String.IsNullOrEmpty(dgv_list["d_vin_no", r].Value.ToString())) {

                        vin_no = dgv_list["d_vin_no", r].Value.ToString();
                        vin_desc = dgv_list["d_Code", r].Value.ToString();

                        if (frm_auto_loan != null)
                        {
                            frm_auto_loan.set_vehicle_frm(vin_no, vin_desc);
                        }
                        else if (frm_sales_auto != null)
                        {
                            frm_sales_auto.set_vehicle_frm(vin_no, vin_desc);
                        }
                        else if (frm_jq != null)
                        {
                            frm_jq.set_vehicle_frm(vin_no, vin_desc);
                        }
                        else if (frm_gm_compute != null)
                        {
                            frm_gm_compute.set_vehicle_frm(vin_no, vin_desc);
                        }
                        else if (frm_reldel_unit != null)
                        {
                            frm_reldel_unit.set_vehicle_frm(vin_no, vin_desc);
                        }
                        else if (frm_ro != null)
                        {
                            frm_ro.set_vehicle_frm(vin_no, vin_desc);

                        }

                        this.Close();
                    }
                }
            }
            catch (Exception) { }
        }


        private void btn_clr_generic_Click(object sender, EventArgs e)
        {
            try { cbo_generic.SelectedIndex = -1; }
            catch { }
        }

        private void btn_clr_dealer_Click(object sender, EventArgs e)
        {
            try { cbo_dealer.SelectedIndex = -1; }
            catch { }
        }

        private void btn_clr_insurance_Click(object sender, EventArgs e)
        {
            try { cbo_insurance.SelectedIndex = -1; }
            catch { }
        }

        private void btn_clr_owner_Click(object sender, EventArgs e)
        {
            try { cbo_owner.SelectedIndex = -1; }
            catch { }
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

        private void disp_itemlist()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            String WHERE = "";
            String txt_vin = "";
            
            try
            {
                dgv_itemlist.Rows.Clear();
            }
            catch { }

            try
            {
                if (String.IsNullOrEmpty(txt_vin) == false)
                {
                    dgv_itemlist.DataSource = db.QueryBySQLCode("SELECT  o.out_desc, oh.* FROM rssys.orhdr oh LEFT JOIN rssys.outlet o ON o.out_code=oh.out_code WHERE car_vin_num=" + db.str_E(txt_vin) + " ORDER BY oh.ord_date, oh.t_time;");
                }
            }
            catch { }

        }
    }
}
