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
    public partial class m_charge : Form
    {
        Boolean isnew = false, isupdaterun = false;
        thisDatabase db = new thisDatabase();
        String old_code = "";
        public m_charge()
        {

            InitializeComponent();

            GlobalClass gc = new GlobalClass();

            gc.load_costcenter(cbo_costcenter);
            gc.load_account_title(cbo_at_code);
            dgv_list.SelectionChanged += dgv_list_SelectionChanged;
            disp_dgvlist();
            /*
            cbo_sc.SelectedIndex = 0;
            cbo_tax.SelectedIndex = 0;
            cbo_hsk.SelectedIndex = 0;
            cbo_deposit.SelectedIndex = 0;
            cbo_posting.SelectedIndex = 0;
            cbo_utitility.SelectedIndex = 0;*/
        }

        void dgv_list_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void m_brand_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_desc.Text = "";
            txt_chgnum.Text = "00000001";
            txt_price.Text = "0.00";
            cbo_chgtype.SelectedIndex = -1;
            //cbo_hsk.SelectedIndex = -1;
            //cbo_deposit.SelectedIndex = -1;
            //cbo_tax.SelectedIndex = -1;
            //cbo_sc.SelectedIndex = -1;
            cbo_at_code.SelectedIndex = -1;
            cbo_costcenter.SelectedIndex = -1;
            cbo_subcostcenter.SelectedIndex = -1;
            cbo_chgclass.SelectedIndex = -1;

            textBox1.Text = "0.00";
        }

        private void goto_tbcntrl_info()
        {
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();
        }

        private void goto_tbcntrl_list()
        {
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
            txt_desc.Enabled = flag;
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["chg_code", dgv_list.CurrentRow.Index].Value.ToString()) == false)
            {
                isnew = false;
                tpg_info_enable(true);
                frm_clear();
                disp_info(dgv_list["chg_code", dgv_list.CurrentRow.Index].Value.ToString());
                goto_tbcntrl_info();
                old_code = dgv_list["chg_code", dgv_list.CurrentRow.Index].Value.ToString();
            }
            else
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This section has been disabled");
            thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (db.UpdateOnTable("charge", "cancel='Y'", "chg_code='" + dgv_list["chg_code", r].Value.ToString() + "'"))
                {
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                }
                else
                {
                    MessageBox.Show("Failed on deleting.");
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(2005);
            rpt.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String code, desc, chg_num, at_code, sl_code, cc_code, chg_type, scc_code, fcharge, price, sc_rep, vat_incl, ishskp, ismisc, isdeposit, paytype, chg_class, com;

            String col = "", val = "", add_col = "", add_val = "";
            String water_min = "0.00", water_min_charge = "0.00", water_excess_charge = "0.00", electricity_min = "0.00", electricity_min_charge = "0.00", electricity_excess_charge = "0.00", e_january = "0.00", e_february = "0.00", e_march = "0.00", e_april = "0.00", e_may = "0.00", e_june = "0.00", e_july = "0.00", e_august = "0.00", e_september = "0.00", e_october = "0.00", e_november = "0.00", e_december = "0.00", franchise_tax = "0.00", utility = "0.00";
            Double com1 = 0.00;
            try
            {
                com1 = Convert.ToDouble(textBox1.Text.ToString());
            }
            catch { }
            if (String.IsNullOrEmpty(txt_desc.Text) || String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                chg_class = (cbo_chgclass.SelectedValue ?? "").ToString();

                code = txt_code.Text;
                desc = txt_desc.Text;
                chg_num = txt_chgnum.Text;
                chg_type = "C";
                fcharge = "N";
                price = txt_price.Text;
                sc_rep = "N";
                vat_incl = "N";
                ishskp = "FALSE";
                ismisc = "FALSE";
                isdeposit = "FALSE";
                utility = "";

                water_min = txt_water_min.Text;
                water_min_charge = txt_water_min_charge.Text;
                water_excess_charge = txt_water_excess_charge.Text;
                electricity_min = txt_electricity_min.Text;
                electricity_min_charge = txt_electricity_min_charge.Text;
                electricity_excess_charge = txt_electricity_excess_charge.Text;
                e_january = txt_e_january.Text; 
                e_february = txt_e_february.Text;
                e_march = txt_e_march.Text; 
                e_april = txt_e_april.Text; 
                e_may = txt_e_may.Text; 
                e_june = txt_e_june.Text; 
                e_july = txt_e_july.Text; 
                e_august = txt_e_august.Text; 
                e_september = txt_e_september.Text; 
                e_october = txt_e_october.Text; 
                e_november = txt_e_november.Text; 
                e_december = txt_e_december.Text;
                franchise_tax = txt_franchise_tax.Text;
                at_code = "";
                sl_code = "";
                cc_code = "";
                scc_code = "";


                if (cbo_chgtype.SelectedIndex == 1)
                    chg_type = "P";

                if (chg_class == "DPST")
                    isdeposit = "TRUE";
                if (chg_class == "HKEEP")
                    ishskp = "TRUE";
                if (chg_class == "WATER" || chg_class == "ELEC")
                    utility = cbo_chgclass.Text.ToUpper();
                
                if (chk_tax.Checked)
                    vat_incl = "Y";

                if (chk_sc.Checked)
                    sc_rep = "Y";

                at_code = (cbo_at_code.SelectedValue??"").ToString();
                cc_code = (cbo_costcenter.SelectedValue??"").ToString();
                scc_code = (cbo_subcostcenter.SelectedValue??"").ToString();

                if (isnew)
                {
                    col = "chg_code, chg_desc, chg_num, at_code, sl_code, cc_code, chg_type, scc_code, fcharge, price, sc_rep, vat_incl, ishskp, ismisc, isdeposit, water_min, water_min_charge, water_excess_charge, electricity_min, electricity_min_charge, electricity_excess_charge, e_january, e_february, e_march, e_april, e_may, e_june, e_july, e_august, e_september, e_october, e_november, e_december, franchise_tax, utility, chg_class, com, ifree";
                    val = "'" + code + "', '" + desc + "', '" + chg_num + "', '" + at_code + "', '" + sl_code + "', '" + cc_code + "', '" + chg_type + "', '" + scc_code + "', '" + fcharge + "', '" + price + "', '" + sc_rep + "', '" + vat_incl + "', '" + ishskp + "', '" + ismisc + "', '" + isdeposit + "', '" + water_min + "', '" + water_min_charge + "', '" + water_excess_charge + "', '" + electricity_min + "', '" + electricity_min_charge + "', '" + electricity_excess_charge + "', '" + e_january + "', '" + e_february + "', '" + e_march + "', '" + e_april + "', '" + e_may + "', '" + e_june + "', '" + e_july + "', '" + e_august + "', '" + e_september + "', '" + e_october + "', '" + e_november + "', '" + e_december + "', '" + franchise_tax + "', '" + utility + "', '" + chg_class + "', '" + com1 + "', '" + checkBox1.Checked + "'";

                    if (db.InsertOnTable("charge", col, val))
                    {
                        success = true;
                        try { //db.set_pkm99("chg_code", db.get_nextincrementlimitchar(code, 3));
                        }
                        catch { } 
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("charge", "chg_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }

                        disp_dgvlist();
                        success = true;

                        //MessageBox.Show("Saved!");
                    
                }
                else
                {
                    col = "chg_code='" + code + "', chg_desc='" + desc + "', chg_num='" + chg_num + "', at_code='" + at_code + "', sl_code='" + sl_code + "', cc_code='" + cc_code + "', chg_type='" + chg_type + "', scc_code='" + scc_code + "', fcharge='" + fcharge + "', price='" + price + "', sc_rep='" + sc_rep + "', vat_incl='" + vat_incl + "', ishskp='" + ishskp + "', ismisc='" + ismisc + "', isdeposit='" + isdeposit + "', water_min='" + water_min + "', water_min_charge='" + water_min_charge + "', water_excess_charge='" + water_excess_charge + "', electricity_min='" + electricity_min + "', electricity_min_charge='" + electricity_min_charge + "', electricity_excess_charge='" + electricity_excess_charge + "', e_january='" + e_january + "', e_february='" + e_february + "', e_march='" + e_march + "', e_april='" + e_april + "', e_may='" + e_may + "', e_june='" + e_june + "', e_july='" + e_july + "', e_august='" + e_august + "', e_september='" + e_september + "', e_october='" + e_october + "', e_november='" + e_november + "', e_december='" + e_december + "', franchise_tax='" + franchise_tax + "', utility='" + utility + "', chg_class='" + chg_class + "', com='" + com1 + "', ifree='" + checkBox1.Checked + "'";
                     
                    if (db.UpdateOnTable("charge", col, "chg_code='" + old_code + "'"))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }

                   
                    try
                    {
                        success = true;
                        //MessageBox.Show("Saved!");
                    }

                    catch
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                old_code = "";

                if (success)
                {
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
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

        private void disp_dgvlist()
        {

            DataTable dt = db.QueryBySQLCode("SELECT chg_code, chg_desc, chg_num, at_code, chg_type, price, scc_code, fcharge, vat_incl, transferred from rssys.charge ORDER BY chg_code ASC");
            

            clear_dgv();

            try
            {
                dgv_list.DataSource = dt;
            }
            catch (Exception) { }
            /*
            externalDatabase.ga_hotelEntities all = new externalDatabase.ga_hotelEntities(thisDatabase.local);

            var temp = all.charges.OrderBy(p => p.chg_code).ToList();

            if (temp != null)
            {
                dgv_list.AutoGenerateColumns = false;
                dgv_list.DataSource = temp;
            }*/

        }

        private void disp_info(String chg_code)
        {
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.charge WHERE chg_code='"+chg_code+"'");
                isupdaterun = true;
                String charge_type = "";
                String hsk = "";
                String deposit ="";
                String sc ="";
                String vat = "";
                if(dt.Rows.Count > 0)
                {
                    for(int i=0 ; i<dt.Rows.Count;i++)
                    {
                    
                        txt_code.Text = dt.Rows[0]["chg_code"].ToString();
                        txt_desc.Text = dt.Rows[0]["chg_desc"].ToString();
                        txt_price.Text = dt.Rows[0]["price"].ToString();
                        txt_chgnum.Text = dt.Rows[0]["chg_num"].ToString();
                        if(dt.Rows[0]["chg_type"].ToString()=="C")
                        {
                            charge_type = "Charge";

                        }
                        else{
                            charge_type = "Payment";
                        }
                        
                        if((bool)dt.Rows[0]["ishskp"])
                            {
                            
                        hsk="YES";
                        }
                        else{
                            hsk="NO";
                        }
                         if((bool)dt.Rows[0]["isdeposit"])
                            {
                        deposit="YES";
                        }
                        else{
                            deposit="NO";
                        }


                        cbo_chgtype.Text = charge_type;
                        //cbo_hsk.Text = hsk;
                        //cbo_deposit.Text = deposit;

                        chk_tax.Checked = (dt.Rows[0]["vat_incl"].ToString() == "Y");
                        chk_sc.Checked = (dt.Rows[0]["sc_rep"].ToString() == "Y");

                        cbo_at_code.SelectedValue = dt.Rows[0]["at_code"].ToString();
                        cbo_costcenter.SelectedValue = dt.Rows[0]["cc_code"].ToString();
                        cbo_subcostcenter.SelectedValue = dt.Rows[0]["scc_code"].ToString();

                        //cbo_utitility.Text = dt.Rows[0]["utility"].ToString();
                        txt_water_min.Text = dt.Rows[0]["water_min"].ToString();
                        txt_water_min_charge.Text = dt.Rows[0]["water_min_charge"].ToString();
                        txt_water_excess_charge.Text = dt.Rows[0]["water_excess_charge"].ToString();
                        txt_electricity_min.Text = dt.Rows[0]["electricity_min"].ToString();
                        txt_electricity_min_charge.Text = dt.Rows[0]["electricity_min_charge"].ToString();
                        txt_electricity_excess_charge.Text = dt.Rows[0]["electricity_excess_charge"].ToString();
                        txt_e_january.Text = dt.Rows[0]["e_january"].ToString();
                        txt_e_february.Text = dt.Rows[0]["e_february"].ToString();
                        txt_e_march.Text = dt.Rows[0]["e_march"].ToString();
                        txt_e_april.Text = dt.Rows[0]["e_april"].ToString();
                        txt_e_may.Text = dt.Rows[0]["e_may"].ToString();
                        txt_e_june.Text = dt.Rows[0]["e_june"].ToString();
                        txt_e_august.Text = dt.Rows[0]["e_august"].ToString();
                        txt_e_july.Text = dt.Rows[0]["e_july"].ToString();
                        txt_e_september.Text = dt.Rows[0]["e_september"].ToString();
                        txt_e_october.Text = dt.Rows[0]["e_october"].ToString();
                        txt_e_november.Text = dt.Rows[0]["e_november"].ToString();
                        txt_e_december.Text = dt.Rows[0]["e_december"].ToString();
                        txt_franchise_tax.Text = dt.Rows[0]["franchise_tax"].ToString();

                        cbo_chgclass.SelectedValue = dt.Rows[0]["chg_class"].ToString();

                        textBox1.Text = dt.Rows[0]["com"].ToString();
                        checkBox1.Checked = Convert.ToBoolean(dt.Rows[0]["ifree"].ToString());
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
            isupdaterun = false;
        }

        private void cbo_costcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_costcenter.SelectedIndex != -1)
            {
                GlobalClass gc = new GlobalClass();
                String cc = cbo_costcenter.SelectedValue.ToString();

                gc.load_subcostcenter(cbo_subcostcenter, cc);
            }
            else
            {
                cbo_subcostcenter.SelectedIndex = -1;
            }
        }

        private void cbo_at_code_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_utitility_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbo_chgtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_chgtype.SelectedIndex != -1)
            {
                load_chgclass();
                if (!isupdaterun)
                {
                    cbo_chgclass.DroppedDown = true;
                }
            }
        }


        private void load_chgclass()
        {
            cbo_chgclass.DataSource = db.QueryBySQLCode("SELECT * FROM rssys.chgclass WHERE COALESCE(cancel,'')<>'Y' AND (cc_type='" + cbo_chgtype.Text + "' OR LOWER(cc_code)='other') ORDER BY cc_desc");
            cbo_chgclass.DisplayMember = "cc_desc";
            cbo_chgclass.ValueMember = "cc_code";
            cbo_chgclass.SelectedIndex = -1;
        }

        private void cbo_chgclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String isUtl = (cbo_chgclass.SelectedValue ?? "").ToString();

                if (isUtl == "ELEC")
                {
                    grp_utility.Enabled = true;
                    groupBox1.Enabled = false;
                }
                else if (isUtl == "WATER")
                {
                    grp_utility.Enabled = false;
                    groupBox1.Enabled = true;
                }
                else
                {
                    grp_utility.Enabled = false;
                    groupBox1.Enabled = false;
                }
            }
            catch { }
            
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }
        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "chg_desc";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "chg_code";
                }
                else
                {
                    typname = "chg_desc";
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();
                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        private void txt_code_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_code_MouseHover(object sender, EventArgs e)
        {
            old_code = ((old_code != "") ? txt_code.Text.ToString() : old_code);
        }
    }
}
