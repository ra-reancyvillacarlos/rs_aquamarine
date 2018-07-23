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
    public partial class m_assembleditem : Form
    {
        private z_enter_item_simple _frm_eis;
        DateTime dt_to;
        DateTime dt_frm;
        dbInv db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        public  int lnno_last = 0;
        String stk_trns_type = "I";
        String trns_type = "I";
        private string Cellsum;

        z_Notification notify;
        private Boolean isCopyBranch = false;

        public m_assembleditem()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();
            notify = new z_Notification();

            gc.load_branch(cbo_branch);
            gc.load_branch(cbo_branch_source);
            gc.load_branch(cbo_branch_import);
            gc.load_branch(cbo_branch_destination);
            gc.load_saleunit(cbo_purchunit);
            gc.load_saleunit(cbo_saleunit);
            //thisDatabase db2 = new thisDatabase();
            //String grp_id = "";
            //DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            //if (dt.Rows.Count > 0)
            //{
            //    grp_id = dt.Rows[0]["grp_id"].ToString();
            //}
            //DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0505' ORDER BY b.pla, b.mod_id");

            //if (dt2.Rows.Count > 0)
            //{
            //    String add = "", update = "", delete = "", print = "";
            //    add = dt2.Rows[0]["add"].ToString();
            //    update = dt2.Rows[0]["upd"].ToString();
            //    delete = dt2.Rows[0]["cancel"].ToString();
            //    print = dt2.Rows[0]["print"].ToString();

            //    if (add == "n")
            //    {
            //        btn_additem.Enabled = false;
            //    }
            //    if (update == "n")
            //    {
            //        btn_upditem.Enabled = false;
            //    }
            //    if (delete == "n")
            //    {
            //        btn_delitem.Enabled = false;
            //    }
            //    if (print == "n")
            //    {
            //        btn_print.Enabled = false;
            //    }

            //}
        }

        private void m_assembleditem_Load(object sender, EventArgs e)
        {
           // disp_itemlist();

            btn_state(false);           
        }

        private void btn_state(Boolean enable)
        {
            btn_itemadd.Enabled = enable;
            btn_itemupd.Enabled = enable;
            btn_itemremove.Enabled = enable;
            btn_mainsave.Enabled = enable;
            btn_print.Enabled = enable;
        }

        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                txt_itemdesc.Text = "";
                txt_regsell.Text = "0.00";
                txt_totalcost.Text = "0.00";
                cbo_saleunit.SelectedIndex = -1;
                cbo_purchunit.SelectedIndex = -1;

                dgv_itemlist.Rows.Clear();
            }
            catch (Exception) { }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            lnno_last = 0;
            goto_win2();

            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, txt_itemdesc.Text, "D");
            _frm_is.Show();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            //if(dgv_itemlist.Rows.Count>0 && String.IsNullOrEmpty(dgv_itemlist[]))
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void disp_itemlist(String code)
        {
            DataTable dt;
            clear_dgv();

            try
            {
                dt = db.QueryBySQLCode("SELECT i2.*, i.item_desc, i.ave_cost, i2.qty*i.ave_cost AS ln_amt, u.unit_shortcode FROM rssys.items2 i2 LEFT JOIN rssys.items i ON i2.item_code2=i.item_code LEFT JOIN rssys.itmunit u ON i2.sales_unit_id=u.unit_id WHERE i2.item_code='" + code + "'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["qty"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["ave_cost"].ToString();
                    dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amt"].ToString();

                    dgv_itemlist["dgvi_partno", i].Value = dt.Rows[i]["part_no"].ToString();

                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code2"].ToString();
                    dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[i]["sales_unit_id"].ToString();
                    dgv_itemlist["remarks", i].Value = dt.Rows[i]["remarks"].ToString();
                }
                
                disp_total();
            }
            catch (Exception) { }
        }

        public void disp_total()
        {
            Double total = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamt", i].Value.ToString());
                }
            }
            catch (Exception) { }

            txt_totalcost.Text = gm.toAccountingFormat(total);
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            z_enter_item_simple frm = new z_enter_item_simple(this, true, lnno_last);
            frm.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            z_enter_item_simple frm = new z_enter_item_simple(this, false, lnno_last);
            try
            {
                int r = dgv_itemlist.CurrentRow.Index;
                frm.update_item_index = r;
                frm.txt_qty.Text = dgv_itemlist["dgvi_qty", r].Value.ToString();//dgv_itemlist.SelectedRows[0].Cells[0].Value.ToString();
                frm.cbo_itemdesc.Text = dgv_itemlist["dgvi_itemdesc", r].Value.ToString(); //dgv_itemlist.SelectedRows[0].Cells[1].Value.ToString();
                frm.cbo_purchunit.SelectedValue = dgv_itemlist["dgvi_unitid", r].Value.ToString();//dgv_itemlist.SelectedRows[0].Cells[6].Value.ToString();
                frm.txt_costprice.Text = dgv_itemlist["dgvi_price", r].Value.ToString();//dgv_itemlist.SelectedRows[0].Cells[3].Value.ToString();
                frm.txt_lnamt.Text = dgv_itemlist["dgvi_lnamt", r].Value.ToString();//dgv_itemlist.SelectedRows[0].Cells[4].Value.ToString();
                frm.txt_itemcode.Text = dgv_itemlist["dgvi_itemcode", r].Value.ToString();//dgv_itemlist.SelectedRows[0].Cells[5].Value.ToString();
                frm.txt_partno.Text = dgv_itemlist["dgvi_partno", r].Value.ToString();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            frm.ShowDialog();
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
                       //dgv_delitem.Rows.Add(code, qty);
                    }

                    dgv_itemlist.Rows.RemoveAt(r);

                    disp_total();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String notificationText = "has been assembled the item of ";
            String code = "";
            try
            {
                if(isCopyBranch)
                {
                    if(String.IsNullOrEmpty(txt_prefix.Text) == true)
                    {
                        MessageBox.Show("Prefix Description is required");
                    }
                    else if(cbo_branch_destination.SelectedIndex == -1)
                    {
                        MessageBox.Show("Branch Destination is required");
                    }
                    else
                    {
                        //get loop items dgv_source
                        for (int s = 0; dgv_source.Rows.Count > s; s++ )
                        {
                            // insert to dgv_destination and save to db

                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(txt_code.Text))
                    {
                        MessageBox.Show("Pls select assembled item.");
                    }
                    else if (dgv_itemlist.Rows.Count <= 1)
                    {
                        MessageBox.Show("Please enter the items to assemble.");
                    }
                    else
                    {
                        code = txt_code.Text;
                        notificationText = notificationText + code + " to these items ";
                        notificationText = notificationText + add_items("items2", code);

                        success = true;

                        if (success)
                        {
                            btn_state(false);
                            tpg_info_enable(true);
                            frm_clear();
                            goto_win1();
                        }
                    }
                }                
            }
            catch (Exception) { MessageBox.Show("Error on inputs. Please check the entry."); }
        }

        private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String i_itemcode2 = "", i_qty = "", i_unitid = "", remarks = "", part_no = "" ;
            String val2 = "";
            String col2 = "item_code, item_code2, qty,sales_unit_id,remarks,part_no";

            try
            {
                db.DeleteOnTable("items2", "item_code='" + code + "'");

                for (int r = 0; r < dgv_itemlist.Rows.Count-1; r++)
                {
                    i_itemcode2 = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                    i_qty = dgv_itemlist["dgvi_qty", r].Value.ToString();
                    i_unitid = cbo_saleunit.SelectedValue.ToString();
                    remarks = dgv_itemlist["remarks", r].Value.ToString();
                    part_no = dgv_itemlist["dgvi_partno", r].Value.ToString();
                    val2 = "'" + code + "', '" + i_itemcode2 + "', '" + i_qty + "','"+i_unitid+"',"+ db.str_E(remarks) +"," + db.str_E(part_no) + "";
                                      
                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        notificationText += Environment.NewLine + i_qty + " " + i_unitid + " of " + i_itemcode2;
                    }
                    else
                    {
                        notificationText = null;
                    }
                }
            }
            catch (Exception error) { MessageBox.Show(error.Message); }            

            return notificationText;
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            btn_state(false);
            goto_win1();
            frm_clear();
        }

        private void btn_itemsave_Click(object sender, EventArgs e)
        {
            goto_win2();
        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            goto_win2();
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_1;
            tpg_option_1.Show();

        }

        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;

            isCopyBranch = false;
        }

        private void goto_copybranch()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_assembled_branch;
            tpg_assembled_branch.Show();
            seltbp = false;

            btn_mainsave.Text = "Copy and Save";
            isCopyBranch = true;
        }

        private void goto_import()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_import;
            tpg_import.Show();
            seltbp = false;

            btn_mainsave.Enabled = false;
            isCopyBranch = false;
            btnImport.Enabled = true;
        }

        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_searchitem_Click(object sender, EventArgs e)
        {
            isnew = true;
            lnno_last = 0;
            goto_win2();

           
            z_ItemSearch _frm_is;
            _frm_is = new z_ItemSearch(this, txt_itemdesc.Text, "D", 0, false);
            _frm_is.Show();
        }

        public void set_info(String itemcode, String tdesc)
        {
            btn_state(true);
            DataTable dt;
            String ave_cost = "", sell_pric = "";

            txt_code.Text = itemcode;
            txt_itemdesc.Text = tdesc;
            try
            {
                cbo_saleunit.SelectedValue = db.get_itemsellunitid(itemcode);
                cbo_purchunit.SelectedValue = db.get_itemsellunitid(itemcode);
                txt_regsell.Text = gm.toAccountingFormat(db.get_itemregsellprice(itemcode));
                txt_totalcost.Text = gm.toAccountingFormat(db.get_item_cost(itemcode));
            }
            catch { }

            try
            {
                dt = db.get_item_details(itemcode);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    ave_cost = dt.Rows[i]["ave_cost"].ToString();

                    if (String.IsNullOrEmpty(dt.Rows[i]["ave_cost"].ToString()) == false)
                    {
                        txt_totalcost.Text = gm.toNormalDoubleFormat(ave_cost).ToString("0.00");
                    }
                    else
                    {
                        txt_totalcost.Text = "0.00";
                    }

                    sell_pric = dt.Rows[i]["sell_pric"].ToString();

                    if (String.IsNullOrEmpty(dt.Rows[i]["sell_pric"].ToString()) == false)
                    {
                        txt_regsell.Text = gm.toNormalDoubleFormat(sell_pric).ToString("0.00");
                    }
                    else
                    {
                        txt_regsell.Text = "0.00";
                    }

                    cbo_purchunit.SelectedValue = dt.Rows[i]["purc_unit_id"].ToString();
                    cbo_saleunit.SelectedValue = dt.Rows[i]["sales_unit_id"].ToString();

                    cbo_branch.SelectedValue = dt.Rows[i]["branch"].ToString();
                }
            }
            catch (Exception er) 
            { 
                //MessageBox.Show(er.Message); 
            }

            disp_total();
        }

        private void tpg_info_enable(Boolean flag)
        {
            btn_searchitem.Enabled = flag;
        }
        private void clear_dgv()
        {
            try { dgv_itemlist.Rows.Clear(); }
            catch (Exception) { }
        }

        private void txt_code_TextChanged(object sender, EventArgs e)
        {
            disp_itemlist(txt_code.Text.ToString());

            //get_row_count();             
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            goto_copybranch();
        }

        private void cbo_branch_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgv_source.Rows.Clear();
            }
            catch { }
            
            try
            {
                DataTable dt = db.get_menulistByBranch(cbo_branch_source.SelectedValue.ToString());

                if (dt != null)
                {
                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        dgv_source.Rows.Add();

                        dgv_source["dgv1_name", r].Value = dt.Rows[r]["item_desc"].ToString();
                        dgv_source["dgv1_srp", r].Value = gm.toAccountingFormat(dt.Rows[r]["regular"].ToString());
                        dgv_source["dgv1_code", r].Value = dt.Rows[r]["item_code"].ToString();
                    } 

                    btn_mainsave.Enabled = true;
                }
            }
            catch {}
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtfilename.Text = openFileDialog1.FileName;
                //dgv_list.Visible = true;
                // bgWorker.RunWorkerAsync();
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
            String col = "", val = "", add_col = "", add_val = "";
            String code, ai_code="", name, ai_name = "", part_no = "", cartype = "", cat = "", brd = "", unit_cost = "", su = "", pu = "", typ = "", cost = "", regsell = "", sc = "", reorder = "", max = "", rack = "", stk_item = "Y", gp = "", reqLot = "", assembly = "N", ccode = "", fcp = "0.00", ave_cost = "0.00", qty = "0.00", clr = "", supl_code = "", supl_name = "", branch="";
            Boolean success = false;
            
            notificationText = "has added: ";
               
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
                cbo_branch_import.Invoke(new Action(() =>
                {
                    branch = cbo_branch_import.SelectedValue.ToString();
                }));

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
                    if (rCnt != 1)
                    {
                        //assembled item
                        ai_name =  gm.toStr((range.Cells[rCnt, 1] as Excel.Range).Value2);
                        regsell = gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2)).ToString("0.00");

                        //item or sub items
                        qty = gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2)).ToString("0.00");
                        part_no = gm.toStr((range.Cells[rCnt, 6] as Excel.Range).Value2);
                        name = gm.toStr((range.Cells[rCnt, 7] as Excel.Range).Value2).Trim();
                        cat = gm.toStr((range.Cells[rCnt, 8] as Excel.Range).Value2);
                        brd = gm.toStr((range.Cells[rCnt, 9] as Excel.Range).Value2);
                        reqLot = "Y";

                        su = "001";
                        pu = "001";
                        typ = "S";                       

                        ave_cost = gm.toNormalDoubleFormat(Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2)).ToString("0.00");
                        unit_cost = ave_cost;
                        fcp = ave_cost;

                        gp = (gm.toNormalDoubleFormat(regsell) - gm.toNormalDoubleFormat(unit_cost)).ToString("0.00");
                        gp = gm.toNormalDoubleFormat(gp).ToString("0.00");
                        reorder = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                        sc = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                        max = gm.toNormalDoubleFormat("0.00").ToString("0.00");

                        name = name.ToUpper();
                        part_no = name.ToUpper();

                        //Add item
                        
                        //if(db.QueryBySQLCode("SELECT 1 FROM s"))
                        /*
                         */
                        //assembled
                        if(String.IsNullOrEmpty(ai_name) == false)
                        {
                            ai_code = db.get_pk("item_code");
                            col = "item_code, item_desc, stk_item, item_class, unit_cost, sell_pric, gp, reorder, max_level, bin_loc, req_lot, brd_code, item_grp, sales_unit_id, purc_unit_id, sc_price, assembly, qty_onhand, qty_onhand_su, fcp, ave_cost, ccode";
                            val = "" + db.str_E(ai_code) + ", " + db.str_E(ai_name) + ", '" + stk_item + "', '" + typ + "', '" + unit_cost + "', '" + regsell + "', '" + gp + "', '" + reorder + "', '" + max + "', '" + rack + "', '" + reqLot + "', '" + brd + "', 'PMS', '014', '014', '" + sc + "', 'Y', '0.00','0.00','" + fcp + "','" + ave_cost + "', '" + ccode + "'";

                            if (db.InsertOnTable("items", col, val))
                            {
                                count++;
                                success = true;
                                db.set_pkm99("item_code", db.get_nextincrementlimitchar(ai_code, 12));
                            }
                            else
                            {
                                MessageBox.Show("Error on inserting at counter number " + rCnt.ToString());
                            }
                        }

                        if (String.IsNullOrEmpty(part_no) == false && db.isExists("items", "part_no= " + db.str_E(part_no) + " AND item_desc=" + db.str_E(name) ) == false)
                        {
                            code = db.get_pk("item_code");
                            col = "item_code, item_desc, part_no, stk_item, item_class, unit_cost, sell_pric, gp, reorder, max_level, bin_loc, req_lot, brd_code, item_grp, sales_unit_id, purc_unit_id, sc_price, assembly, qty_onhand, qty_onhand_su, fcp, ave_cost, ccode";
                            val = "" + db.str_E(code) + ", " + db.str_E(name) + ", " + db.str_E(part_no) + ", '" + stk_item + "', '" + typ + "', '" + unit_cost + "', '" + regsell + "', '" + gp + "', '" + reorder + "', '" + max + "', '" + rack + "', '" + reqLot + "', '" + brd + "', '" + cat + "', '" + su + "', '" + pu + "', '" + sc + "', '" + assembly + "', '0.00','0.00','" + fcp + "','" + ave_cost + "', '" + ccode + "'";

                            if (db.InsertOnTable("items", col, val))
                            {
                                count++;
                                success = true;
                                db.set_pkm99("item_code", db.get_nextincrementlimitchar(code, 12));
                            }
                            else
                            {
                                MessageBox.Show("Error on inserting at counter number " + rCnt.ToString());
                            }
                        }
                        else
                        {
                            code = db.get_item_code_by_part_no(part_no);
                        }

                        col = "item_code, item_code2, qty, sales_unit_id, branch, part_no";
                        val = "" + db.str_E(ai_code) + ", " + db.str_E(code) + ", " + db.str_E(qty) + ", '" + su + "', '" + branch + "', " + db.str_E(part_no) + "";

                        if (db.InsertOnTable("items2", col, val))
                        {
                            count++;
                            success = true;
                        }
                        else
                        {
                            MessageBox.Show("Error on inserting at counter number " + rCnt.ToString());
                        }

                        //success = true;

                        if (success == false)
                        {
                           // db.DeleteOnTable("items", "item_code='" + code + "'");
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

                btnImport.Invoke(new Action(() =>
                {
                    btnImport.Enabled = true;
                }));
                if (count >= rw)
                {
                    txt_status.Invoke(new Action(() =>
                    {
                        txt_status.Text = "Import completed";
                    }));
                }
                btnBrowse.Invoke(new Action(() =>
                {
                    btnBrowse.Enabled = true;
                }));
                inc_pbar(rw, rw);
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            txt_status.Text = "Please wait while importing to database.";
            btnBrowse.Enabled = false;
            bgWorker.RunWorkerAsync();
        }
        
        private void btn_import_Click(object sender, EventArgs e)
        {
            goto_import();
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            tpg_info_enable(true);
            frm_clear();
            goto_win2();
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

        private void btn_print_Click(object sender, EventArgs e)
        {
            String code = txt_code.Text;
            if (!String.IsNullOrEmpty(code))
            {
                String branch = cbo_branch.Text,
                       purch_unit = cbo_purchunit.Text,
                       sales_unit = cbo_saleunit.Text,
                       reg_price = gm.toNormalDoubleFormat(txt_regsell.Text).ToString("0.00"),
                       unit_price = gm.toNormalDoubleFormat(txt_totalcost.Text).ToString("0.00"),
                       item_desc = txt_itemdesc.Text;

                Report rpt = new Report();
                rpt.print_assembled_item(code, item_desc, purch_unit, sales_unit, gm.toAccountingFormat(reg_price), gm.toAccountingFormat(unit_price), branch);
                rpt.ShowDialog();
	
            }
            else {
                MessageBox.Show("Please select assembled item.");
            }

        }

        private void btn_print_list_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_assembled_item_list();
            rpt.ShowDialog();
        }
    }
}
