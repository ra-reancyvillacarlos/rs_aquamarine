using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Accounting_Application_System
{
    public partial class m_item : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        thisDatabase db;
        GlobalMethod gm;
        Boolean is_import = false;
        String stk_trns_type = "A";
        String trns_type = "A";
        GlobalClass gc;
        z_Notification notify = new z_Notification();
        
        public m_item()
        {
            InitializeComponent();
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            gc.load_item_asc_desc(cbo_name);

            // for stock adjustment
            gc.load_account_title(cbo_contraacct);
            gc.load_costcenter(cbo_costcenter);
            gc.load_stocklocation(cbo_stocklocation);

            gc.load_company_acct(cbo_dealer);
            gc.load_category(cbo_itmgrp);
            gc.load_brand(cbo_brand);
            gc.load_saleunit(cbo_saleunit);
            gc.load_saleunit(cbo_purchunit);
            gc.load_color(cbo_color);
            gc.load_generic(cbo_generic);

            txt_code.Text = "";
            txt_desc.Text = "";
            cbo_generic.SelectedIndex = 0;
            cbo_contraacct.SelectedIndex = -1;
            cbo_stocklocation.SelectedIndex = -1;
            cbo_costcenter.SelectedIndex = -1;
            dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));

            try
            {
                cbo_branch.SelectedValue = GlobalClass.branch;
            }
            catch { }
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0504' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_additem.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upditem.Enabled = false;
                }
                if (delete == "n")
                {
                    //btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
        }

        private void m_item_Load(object sender, EventArgs e)
        {
            cbo_type.SelectedIndex = 0;
            tpg_info_enable(false);

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_part_no.Text = "";
            txt_cost.Text = "0.00";
            txt_regsell.Text = "1.00";
            txt_sc.Text = "0.00";
            //txt_costcode.Text = "";

            txt_reorderlvl.Text = "0";
            txt_maxlvl.Text = "0";
            txt_rack.Text = "";

            cbo_generic.SelectedIndex = 0;
            cbo_type.SelectedIndex = 0;

            cbo_name.Text = "";
            cbo_name.SelectedIndex = -1;
            cbo_brand.SelectedIndex = -1;
            cbo_itmgrp.SelectedIndex = -1;
            cbo_saleunit.SelectedIndex = -1;
            cbo_type.SelectedIndex = 0;
            txt_interestpct.Text = "10";

            chk_lot.Checked = false;
        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_opt_1.Show();

            tbcntrl_main.SelectedTab = tpg_info;
            tpg_info.Show();
            seltbp = false;
        }
        private void goto_tbcntrl_import()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();
            tbcntrl_main.SelectedTab = import;
            import.Show();
            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            //txt_code.Enabled = flag;
            //txt_part_no.Enabled = flag;
            txt_cost.Enabled = flag;
            txt_regsell.Enabled = flag;
            txt_sc.Enabled = flag;
            txt_reorderlvl.Enabled = flag;
            txt_maxlvl.Enabled = flag;
            txt_rack.Enabled = flag;
            //txt_costcode.Enabled = flag;

            cbo_name.Enabled = flag;
            cbo_itmgrp.Enabled = flag;
            cbo_brand.Enabled = flag;
            cbo_saleunit.Enabled = flag;
            cbo_type.Enabled = flag;
            cbo_generic.Enabled = flag;
            cbo_branch.Enabled = flag;
            
            chk_lot.Enabled = flag;
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            btnImport.Enabled = false;
            tpg_info_enable(true);
            frm_clear();
            cbo_branch.SelectedValue = GlobalClass.branch;
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;
            //tpg_info_enable(true);
            frm_clear();
            disp_list();
            btnImport.Enabled = false;
            goto_tbcntrl_info();
            tpg_info_enable(true);
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("M001");
            rpt.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            z_ItemSearch i = new z_ItemSearch();
            i.ShowDialog();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String code, name, part_no = "", cartype = "", cat, brd, su, pu, typ, cost, regsell, sc, reorder, max, rack, stk_item = "Y", gp, reqLot = "", assembly = "N", ccode = "", fcp = "0.00", ave_cost = "0.00", clr = "", gen_code = "";
            String col = "", val = "", add_col = "", add_val = "", menu_branch="";

            try
            {
                if (String.IsNullOrEmpty(cbo_name.Text) || String.IsNullOrEmpty(txt_regsell.Text) || cbo_itmgrp.SelectedIndex == -1 || cbo_saleunit.SelectedIndex == -1 || cbo_purchunit.SelectedIndex == -1 || cbo_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls enter the required fields.");
                }
                else if (cbo_itmgrp.SelectedValue.ToString() != "MENU" && cbo_generic.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter the generic name for this item.");
                    cbo_generic.DroppedDown = true;
                }
                else if ((cbo_itmgrp.SelectedValue.ToString() == "NV" || cbo_itmgrp.SelectedValue.ToString() == "UV" ) && cbo_color.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter the car color value for this category.");
                    cbo_color.DroppedDown = true;
                }
                else if (cbo_itmgrp.SelectedValue.ToString() == "MENU" && cbo_branch.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter the branch for the Menu Category.");
                    cbo_branch.DroppedDown = true;
                } 
                else
                { 
                    //cbo_dealer cbo_color txt_model txt_vin txt_engine
                        
                    if (cbo_generic.SelectedIndex != -1)
                    {
                        gen_code = cbo_generic.SelectedValue.ToString();
                    }

                    if (cbo_branch.SelectedIndex != -1)
                    {
                        menu_branch = cbo_branch.SelectedValue.ToString();
                    }
                    else
                    {
                        menu_branch = "001";
                    }

                    code = txt_code.Text;
                    name = cbo_name.Text;
                    part_no = txt_part_no.Text;
                    cat = cbo_itmgrp.SelectedValue.ToString();
                    brd = cbo_brand.SelectedValue.ToString();
                    su = cbo_saleunit.SelectedValue.ToString();
                    pu = cbo_purchunit.SelectedValue.ToString();
                    typ = get_cbotypevalue(); // cbo_type.SelectedValue.ToString();
                    cost = gm.toNormalDoubleFormat(txt_cost.Text).ToString("0.00");
                    ccode = "";//txt_costcode.Text;
                    regsell = gm.toNormalDoubleFormat(txt_regsell.Text).ToString("0.00");
                    sc = txt_sc.Text;
                    reorder = txt_reorderlvl.Text;
                    max = gm.toNormalDoubleFormat(txt_maxlvl.Text).ToString("0.00");
                    rack = txt_rack.Text;

                    String interestpct = gm.toNormalDoubleFormat(txt_interestpct.Text).ToString("0.00");
                    
                    if (cat == "00002")
                    {
                        clr = cbo_color.SelectedValue.ToString();
                    }

                    fcp = gm.toNormalDoubleFormat(txt_cost.Text).ToString("0.00");
                    ave_cost = gm.toNormalDoubleFormat(txt_cost.Text).ToString("0.00");
                    String yearmodel = txt_model.Text;
                    if (chk_lot.Checked)
                    {
                        reqLot = "Y";
                    }
                    else
                    {
                        reqLot = "";
                    }

                    gp = (Convert.ToDouble(regsell) - Convert.ToDouble(cost)).ToString("0.00");


                    if (isnew)
                    {
                        code = db.get_pk("item_code");

                        col = "item_code, item_desc, part_no, stk_item, item_class, unit_cost, sell_pric, gp, reorder, max_level, bin_loc, req_lot, brd_code, item_grp, sales_unit_id, purc_unit_id, sc_price, assembly, qty_onhand, qty_onhand_su, fcp, ave_cost, ccode, color_id, gen_code, branch,interestpct,yearmodel";
                        val = "'" + code + "', " + db.str_E(name) + ", '" + part_no + "', '" + stk_item + "', '" + typ + "', '" + cost + "', '" + regsell + "', '" + gp + "', '" + reorder + "', '" + max + "', '" + rack + "', '" + reqLot + "', '" + brd + "', '" + cat + "', '" + su + "', '" + pu + "', '" + sc + "', '" + assembly + "', '0.00','0.00','" + fcp + "','" + ave_cost + "', '" + ccode + "', '" + clr + "', '" + gen_code + "', '" + menu_branch + "','" + interestpct + "','" + yearmodel + "'"; 
                        
                        if (db.InsertOnTable("items", col, val))
                        {
                            success = true;
                            db.set_pkm99("item_code", db.get_nextincrementlimitchar(code, 10));
                        }

                        if (success == false)
                        {
                            db.DeleteOnTable("items", "item_code='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        fcp = db.get_item_fcp_compute(code).ToString("0.00");
                        ave_cost = db.get_item_ave_cost_compute(code).ToString("0.00");

                        col = "item_desc=" + db.str_E(name) + ", part_no='" + part_no + "', stk_item='" + stk_item + "', item_class='" + typ + "', unit_cost='" + cost + "', sell_pric='" + regsell + "', gp='" + gp + "', reorder='" + reorder + "', max_level='" + max + "', bin_loc='" + rack + "', req_lot='" + reqLot + "', brd_code='" + brd + "', item_grp='" + cat + "', sales_unit_id='" + su + "', purc_unit_id='" + pu + "', sc_price='" + sc + "', ccode='" + ccode + "', fcp='" + fcp + "', ave_cost='" + ave_cost + "', color_id='" + clr + "', gen_code='" + gen_code + "', branch='" + menu_branch + "', interestpct='" + interestpct + "',yearmodel='" + yearmodel + "'";

                        if (db.UpdateOnTable("items", col, "item_code='" + code + "'"))
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }

                    if (success)
                    {
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                        frm_clear();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Error on inputs. Please check the entry."); }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = true;
            btn_save.Enabled = true;
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
        }

        private void disp_info()
        {

        }

        private void disp_list()
        {
            z_ItemSearch frm_is;
            frm_is = new z_ItemSearch();
            frm_is.set_frm_m_item(this);
            frm_is.ShowDialog();
        }

        private void disp_purch_history(String itemcode)
        {
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.stkcrd WHERE item_code='" + itemcode + "' ORDER BY trnx_date DESC LIMIT 5");

                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    dgv_purchistory.Rows.Add();

                    dgv_purchistory["dgvi_trnxdate", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_trnx_type", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_supname", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_reference", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_costamt", r].Value = dt.Rows[r]["price"].ToString();
                    dgv_purchistory["dgvi_partno", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_userid", r].Value = dt.Rows[r][""].ToString();
                    dgv_purchistory["dgvi_supl_code", r].Value = dt.Rows[r][""].ToString();
                }
            }
            catch { }
        }

        private String get_cbotypevalue()
        {
            String cbotxt = "";

            cbotxt = cbo_type.Text;

            if (cbotxt == "Sale")
            {
                return "S";
            }
            else if (cbotxt == "Internal")
            {
                return "I";
            }
            else
            {
                return "N";
            }
        }

        private String set_cbotypevalue(String typeval)
        {
            if (typeval == "S")
            {
                return "Sale";
            }
            else if (typeval == "I")
            {
                return "Internal";
            }
            else
            {
                return "None";
            }
        }

        public void enter_item(String itemcode)
        {
            DataTable dt = db.get_item_details(itemcode.Trim());

            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > 0; i++)
                    {
                        txt_code.Text = dt.Rows[i]["item_code"].ToString();
                        cbo_name.Text = dt.Rows[i]["item_desc"].ToString();
                        cbo_itmgrp.SelectedValue = dt.Rows[i]["item_grp"].ToString();
                        cbo_brand.SelectedValue = dt.Rows[i]["brd_code"].ToString();
                        cbo_saleunit.SelectedValue = dt.Rows[i]["sales_unit_id"].ToString();
                        cbo_purchunit.SelectedValue = dt.Rows[i]["purc_unit_id"].ToString();
                        cbo_type.SelectedValue = set_cbotypevalue(dt.Rows[i]["item_class"].ToString());

                        txt_cost.Text = gm.toAccountingFormat(dt.Rows[i]["unit_cost"].ToString());
                        txt_regsell.Text = gm.toAccountingFormat(dt.Rows[i]["sell_pric"].ToString());
                        txt_sc.Text = gm.toAccountingFormat(dt.Rows[i]["sc_price"].ToString());
                        txt_reorderlvl.Text = gm.toAccountingFormat(dt.Rows[i]["reorder"].ToString());
                        txt_maxlvl.Text = gm.toAccountingFormat(dt.Rows[i]["max_level"].ToString());
                        txt_rack.Text = gm.toAccountingFormat(dt.Rows[i]["bin_loc"].ToString());
                        txt_interestpct.Text = gm.toAccountingFormat(dt.Rows[i]["interestpct"].ToString());
                        txt_model.Text = dt.Rows[i]["yearmodel"].ToString();
                        
                        
                        try
                        {
                            txt_part_no.Text = dt.Rows[i]["part_no"].ToString();
                        }
                        catch { }

                        try
                        {
                            cbo_generic.SelectedValue = dt.Rows[i]["gen_code"].ToString();
                        }
                        catch (Exception) { }

                        try
                        {
                            cbo_color.SelectedValue = dt.Rows[i]["color_id"].ToString();
                        }
                        catch (Exception) { }

                        if (dt.Rows[i]["req_lot"].ToString() == "Y")
                        {
                            chk_lot.Checked = true;
                        }
                        else
                        {
                            chk_lot.Checked = false;
                        }

                        //compute_dgv_amortlist();
                    }
                }
            }
            catch (Exception) {  }
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            pbar.Value = 0;
            btn_save.Enabled = false;
            txt_status.Text = "";
            goto_tbcntrl_import();
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (seltbp == false)
                e.Cancel = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtfilename.Text = openFileDialog1.FileName;
                //dgv_list.Visible = true;
                // bgWorker.RunWorkerAsync();
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            String str = "";
            int rCnt = 0;
            int cCnt = 0;
            int rw = 0;
            int cl = 0;
            String filename = "";
            int i = 0;

            String notificationText = "";

            Boolean isnew = true;
            String notifyadd = null;
            String table = "rechdr";
            String tableln = "reclne", rec_num = "";
            String col = "", val = "", add_col = "", add_val = "";
            String code, name, part_no = "", cartype = "", cat = "", brd = "", unit_cost = "", su = "", pu = "", typ = "", cost = "", regsell = "", sc = "", reorder = "", max = "", rack = "", stk_item = "Y", gp = "", reqLot = "", assembly = "N", ccode = "", fcp = "0.00", ave_cost = "0.00", qty = "0.00", clr = "", supl_code = "", supl_name = "";
            Boolean success = false;
            Double dt_double = 0;
            String at_code = "", loc = "", cc_code = "", dt_trnx = "", reference = "", stk_ref = "", stk_po_so = "";
            String branch = "", color = "", color_id = "", csr_no = "", csr_series = "", yearmodel = "", invoice_date = "", gen_code="";

            String gcolor_id = db.get_colval("color", "id", "id ~ E'^\\\\d+$'");
            if (String.IsNullOrEmpty(gcolor_id)) gcolor_id = "001";

            DateTime dt_temp = DateTime.Parse(db.get_systemdate(""));
            cbo_contraacct.Invoke(new Action(() =>
            {
                at_code = cbo_contraacct.SelectedValue.ToString();
            }));
            cbo_stocklocation.Invoke(new Action(() =>
            {
                loc = cbo_stocklocation.SelectedValue.ToString();
            }));
            cbo_costcenter.Invoke(new Action(() =>
            {
                cc_code = cbo_costcenter.SelectedValue.ToString();
            }));
            dtp_trnxdt.Invoke(new Action(() =>
            {
                dt_trnx = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
            }));
            txt_desc.Invoke(new Action(() =>
            {
                reference = txt_desc.Text;
            }));

            rec_num = db.get_pk("adj_num");
            stk_ref = stk_trns_type + "#" + rec_num;

            if (isnew)
            {
                notificationText = "has added: ";

                col = "rec_num, _reference, trnx_date, recipient, t_date, t_time, trn_type,whs_code, cancel, branch";
                val = "'" + rec_num + "', '" + reference + "', '" + dt_trnx + "', '" + GlobalClass.username + "', '"
                    + db.get_systemdate("") + "', '" + db.get_systemtime() + "', '" + trns_type + "', '" + loc + "', 'N', '" + GlobalClass.branch + "'";

                if (db.InsertOnTable(table, col, val))
                {
                    db.set_pkm99("adj_num", db.get_nextincrementlimitchar(rec_num, 8));

                    try
                    {
                        filename = openFileDialog1.FileName;
                        xlApp = new Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(@filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        range = xlWorkSheet.UsedRange;
                        rw = range.Rows.Count;
                        cl = range.Columns.Count;
                        if (rw > 0)
                        {
                            btnImport.Invoke(new Action(() =>
                            {
                                btnImport.Enabled = false;
                            }));
                        }

                        int xcol = 1;
                        int count = 0;
                        pbar.Invoke(new Action(() =>
                        {
                            pbar.Maximum = rw;
                        }));
                        lbl_minimum.Invoke(new Action(() =>
                        {
                            lbl_minimum.Text = pbar.Minimum.ToString();
                        }));
                        label33.Invoke(new Action(() =>
                        {
                            label33.Text = pbar.Maximum.ToString();
                        }));
                        for (rCnt = 1; rCnt <= rw; rCnt++)
                        {
                            try
                            {
                                name = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2).Trim().ToLower();
                                if (String.IsNullOrEmpty(name) || name.IndexOf("item desc") >= 0 || name.IndexOf("item desc") >= 0 || (name.IndexOf("item") >= 0 && name.IndexOf("description") >= 0) || name.IndexOf("item_desc") >= 0)
                                {
                                    success = false;
                                }
                                else
                                {
                                    success = true;
                                }
                            }
                            catch { success = false; }


                            if (rCnt != 1 && success == true)
                            {
                                part_no = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                                name = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2).Trim();
                                cat = Convert.ToString((range.Cells[rCnt, 3] as Excel.Range).Value2);
                                brd = Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2);
                                su = Convert.ToString((range.Cells[rCnt, 6] as Excel.Range).Value2);
                                pu = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
                                reqLot = Convert.ToString((range.Cells[rCnt, 9] as Excel.Range).Value2);

                                typ = Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2);
                                qty = Convert.ToString((range.Cells[rCnt, 5] as Excel.Range).Value2);
                                supl_name = "";//Convert.ToString((range.Cells[rCnt, 13] as Excel.Range).Value2);

                                qty = gm.toNormalDoubleFormat(qty).ToString();

                                if (cat.Trim() == "P")
                                {
                                    reqLot = "Y";

                                    if (String.IsNullOrEmpty(su))
                                    {
                                        su = "001";
                                    }
                                    if (String.IsNullOrEmpty(pu))
                                    {
                                        pu = "001";
                                    }
                                }                           

                                if (String.IsNullOrEmpty(typ))
                                {
                                    typ = "S";
                                }

                                if (String.IsNullOrEmpty(reqLot))
                                {
                                    reqLot = "Y";
                                }

                                ave_cost = gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2)).ToString("0.00");
                                unit_cost = ave_cost;//gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2)).ToString("0.00");
                                regsell = gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 11] as Excel.Range).Value2)).ToString("0.00");
                                rack = Convert.ToString((range.Cells[rCnt, 12] as Excel.Range).Value2);

                                fcp = ave_cost;
                                gp = (Convert.ToDouble(regsell) - Convert.ToDouble(unit_cost)).ToString("0.00");
                                gp = gm.toNormalDoubleFormat(gp).ToString("0.00");
                                reorder = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                                sc = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                                max = gm.toNormalDoubleFormat("0.00").ToString("0.00");

                                branch = Convert.ToString((range.Cells[rCnt, 16] as Excel.Range).Value2);
                                color = Convert.ToString((range.Cells[rCnt, 17] as Excel.Range).Value2).ToUpper();
                                yearmodel = Convert.ToString((range.Cells[rCnt, 18] as Excel.Range).Value2);
                                csr_no = Convert.ToString((range.Cells[rCnt, 19] as Excel.Range).Value2);
                                csr_series = Convert.ToString((range.Cells[rCnt, 20] as Excel.Range).Value2);
                                try { dt_double = Convert.ToDouble((range.Cells[rCnt, 21] as Excel.Range).Value2); } catch { dt_double = 0; }
                                gen_code = Convert.ToString((range.Cells[rCnt, 22] as Excel.Range).Value2);
                                invoice_date = dt_temp.ToString("yyyy-MM-dd");

                                try { if (dt_double != 0) { invoice_date = DateTime.FromOADate(dt_double).ToString("yyyy-MM-dd"); } }
                                catch { }

                                if (!String.IsNullOrEmpty(color))
                                {
                                    color_id = db.get_colval("color", "id", "color_desc=" + db.str_E(color) + "");
                                    if (String.IsNullOrEmpty(color_id))
                                    {
                                        gcolor_id = gcolor_id.Replace(Convert.ToInt32(gcolor_id).ToString(), (Convert.ToInt32(gcolor_id) + 1).ToString());

                                        col = "id, color_desc";
                                        val = "'" + gcolor_id + "', '" + color + "'";
                                        if (db.InsertOnTable("color", col, val))
                                        {
                                            color_id = gcolor_id;
                                        }
                                    }
                                }

                                code = db.get_pk("item_code");
                                col = "item_code, item_desc, part_no, stk_item, item_class, unit_cost, sell_pric, gp, reorder, max_level, bin_loc, req_lot, brd_code, item_grp, sales_unit_id, purc_unit_id, sc_price, assembly, qty_onhand, qty_onhand_su, fcp, ave_cost, ccode, branch, yearmodel, color_id, csr_no, csr_series, invoice_date, gen_code";
                                val = "" + db.str_E(code) + ", " + db.str_E(name) + ", " + db.str_E(part_no) + ", '" + stk_item + "', '" + typ + "', '" + unit_cost + "', '" + regsell + "', '" + gp + "', '" + reorder + "', '" + max + "', '" + rack + "', '" + reqLot + "', '" + brd + "', '" + cat + "', '" + su + "', '" + pu + "', '" + sc + "', '" + assembly + "', '0.00','0.00','" + fcp + "','" + ave_cost + "', '" + ccode + "','" + branch + "', '" + yearmodel + "', '" + color_id + "', " + db.str_E(csr_no) + ", " + db.str_E(csr_series) + ", '" + invoice_date + "', '" + gen_code + "'";

                                //if(db.QueryBySQLCode("SELECT 1 FROM s"))
                                success = false;
                                if (db.InsertOnTable("items", col, val))
                                {
                                    count++;
                                    success = true;
                                    db.set_pkm99("item_code", db.get_nextincrementlimitchar(code, 12));
                                    //DATABASE NOTE : ln_num length 3 change to 8
                                    add_items(tableln, rec_num, code, rCnt - 1, name, su, qty, regsell, fcp, dt_trnx, stk_ref, stk_po_so, loc, at_code, cc_code, supl_code, supl_name);
                                }
                                else
                                {
                                    MessageBox.Show("Error on inserting at counter number " + rCnt.ToString());
                                }

                                if (success == false)
                                {
                                    db.DeleteOnTable("items", "item_code='" + code + "'");
                                    MessageBox.Show("Failed on saving.");
                                }

                                if (success)
                                {
                                    lbl_minimum.Invoke(new Action(() =>
                                    {
                                        lbl_minimum.Text = count.ToString();
                                    }));
                                    inc_pbar(count, rw);

                                }
                            }
                        }

                        MessageBox.Show("Number of rows inserted : " + (count));

                        btn_import.Invoke(new Action(() =>
                        {
                            btn_import.Enabled = true;
                        }));
                        pbar.Invoke(new Action(() =>
                        {
                            pbar.Value = 0;
                        }));
                        lbl_minimum.Invoke(new Action(() =>
                        {
                            lbl_minimum.Text = "0";
                        }));
                        label33.Invoke(new Action(() =>
                        {
                            label33.Text = "0";
                        }));
                        btnBrowse.Invoke(new Action(() =>
                        {
                            btnBrowse.Enabled = true;
                        }));
                        xlWorkBook.Close(true, null, null);
                        xlApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);

                        db.set_pkm99("rec_num", db.get_nextincrementlimitchar(rec_num, 8));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
        }

        private String add_items(String tableln, String rec_num, String code, int lnno, String item_desc, String unit, String i_qty, String regsell, String fcp, String dt_trnx, String stk_ref, String stk_po_so, String loc, String at_code, String cc_code, String supcode, String supname)
        {
            String notificationText = null;
            String col2 = "rec_num, ln_num, item_code, item_desc, unit, recv_qty, price, ln_amnt, cht_code, cnt_code";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";
            String val2 = "";

            val2 = "'" + rec_num + "', '" + lnno + "', " + db.str_E(code) + ", " + db.str_E(item_desc) + ", '" + unit + "','" + i_qty + "', '" + regsell + "', '0.00', '" + at_code + "', '" + cc_code + "'";

            if (db.InsertOnTable(tableln, col2, val2))
            {
                stk_qty_in = "0.00";
                stk_qty_out = "0.00";

                if (gm.toNormalDoubleFormat(i_qty) < 0)
                    stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                else
                    stk_qty_in = i_qty;

                db.save_to_stkcard(code, item_desc, unit, dt_trnx, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, fcp, regsell, loc, supcode, supname, stk_trns_type, at_code, cc_code, "");

                db.upd_item_quantity_onhand(code, Convert.ToDouble(i_qty), stk_trns_type);
                notificationText += Environment.NewLine + item_desc;
            }
            else
            {
                notificationText = null;
            }
            return "";
        }
        private void inc_pbar(int i, int rw)
        {
            try
            {
                if (pbar.Value <= rw)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = i;
                    }));
                }
                else
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = rw;
                    }));

                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = true;
                    }));
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnImport_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Please select the description field.");
            }
            else if (cbo_contraacct.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the contra account field.");
            }
            else if (cbo_stocklocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select stock location field.");
            }
            else if (cbo_costcenter.SelectedIndex == -1)
            {
                MessageBox.Show("Please select cost center field.");
            }
            else
            {
                txt_status.Text = "Please wait while importing to database.";
                btnBrowse.Enabled = false;
                bgWorker.RunWorkerAsync();
            }
        }

        private void tpg_opt_1_Click(object sender, EventArgs e)
        {

        }

        private void import_Click(object sender, EventArgs e)
        {

        }

        private void cbo_itmgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String itm_grp = cbo_itmgrp.SelectedValue.ToString();

                if (itm_grp == "NV" | itm_grp == "UV")
                {
                    grp_carcat.Enabled = true;
                    cbo_color.Enabled = true;
                    cbo_dealer.Enabled = true;
                    txt_vin.Enabled = true;
                    txt_model.Enabled = true;
                    txt_model.Enabled = true;

                    grp_amort.Enabled = true;
                    dgv_amortlist.Enabled = true;

                    chk_lot.Checked = true;

                    dgv_amortlist.EnableHeadersVisualStyles = false;
                    dgv_amortlist.ColumnHeadersDefaultCellStyle.BackColor = Color.GhostWhite;
                    dgv_amortlist.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                    if(gm.toNormalDoubleFormat(txt_interestpct.Text) != 10)
                    {
                        txt_interestpct.Text = "10.00";
                    }

                }
                else
                {
                    grp_carcat.Enabled = false;
                    cbo_color.Enabled = false;
                    cbo_color.SelectedIndex = -1;
                    cbo_dealer.Enabled = false;
                    cbo_dealer.SelectedIndex = -1;

                    txt_vin.Enabled = false;
                    txt_model.Enabled = false;
                    txt_model.Enabled = false;

                    grp_amort.Enabled = false;
                    dgv_amortlist.Enabled = false;

                    chk_lot.Checked = false;

                    dgv_amortlist.EnableHeadersVisualStyles = false;
                    dgv_amortlist.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                    dgv_amortlist.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                }
            }
            catch { }
        }

        private void chk_lot_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_lot.Checked == true)
                {
                    txt_part_no.Enabled = true;
                    cbo_name.DroppedDown = true;
                }
                else
                {
                    txt_part_no.Enabled = false;
                    cbo_name.DroppedDown = false;

                   // txt_part_no.Text = "";
                }
            }
            catch { cbo_name.DroppedDown = false; }
        }

        private void tbcntrl_option_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_generic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_amortlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_purchistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txt_regsell_TextChanged_1(object sender, EventArgs e)
        {
            compute_dgv_amortlist();
        }
        private void txt_interestpct_TextChanged(object sender, EventArgs e)
        {
            compute_dgv_amortlist();
        }

        public void compute_dgv_amortlist() 
        {
            String itm_grp = "";

            if(cbo_itmgrp.SelectedIndex != -1)
            {
                itm_grp = cbo_itmgrp.SelectedValue.ToString();
            }

            if (itm_grp == "NV" | itm_grp == "UV")
            {
                //dgv_amortlist
                Double srp = 0;
                try { srp = gm.toNormalDoubleFormat(txt_regsell.Text); }
                catch { }

                Double interest = 10;
                try { interest = gm.toNormalDoubleFormat(txt_interestpct.Text); }
                catch { }

                try
                {
                    try { dgv_amortlist.Rows.Clear(); }
                    catch { }

                    if (srp == 0) { return; }

                    DataTable dt = db.QueryOnTableWithParams("downpayment", "dpcode, dppct, dpdesc ", "", "ORDER BY dpdesc");

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int r = dgv_amortlist.Rows.Add();

                            Double disc_pct = Double.Parse(dt.Rows[i]["dppct"].ToString()) / 100.0;

                            Double dpamt = (disc_pct * srp);

                            dgv_amortlist["dgvdp_dpdesc", r].Value = dt.Rows[i]["dpdesc"].ToString();
                            dgv_amortlist["dgvdp_dpamt", r].Value = gm.toAccountingFormat(dpamt);

                            Double rdpamt = (srp - dpamt);

                            Double _interest = 0;

                            _interest = ((interest / 100.0) * 1) + 1;
                            dgv_amortlist["dgvdp_12mo", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 12));
                            _interest = ((interest / 100.0) * 2) + 1;
                            dgv_amortlist["dgvdp_24mo", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 24));
                            _interest = ((interest / 100.0) * 3) + 1;
                            dgv_amortlist["dgvdp_36mo", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 36));
                            _interest = ((interest / 100.0) * 4) + 1;
                            dgv_amortlist["dgvdp_48mo", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 48));
                            _interest = ((interest / 100.0) * 5) + 1;
                            dgv_amortlist["dgvdp_60mo", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 60));
                        }
                    }
                }
                catch { }
            }
        }
        
        private void txt_EnterTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toNormalDoubleFormat(txt_num.Text).ToString("0.00");
                txt_num.Select(txt_num.Text.Length, 0);
            }
        }
        private void txt_LeaveTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toAccountingFormat(txt_num.Text);
                txt_num.Select(0, 0);
            }
        }

        private void txt_model_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }
    }
}
