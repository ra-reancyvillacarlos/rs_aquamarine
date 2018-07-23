using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class selectsoa : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        newGuestBilling frmgbill;
        String reg_num, rmrttyp;
        Boolean isReady = false;

        public selectsoa()
        {
            InitializeComponent();
        }
        public selectsoa(newGuestBilling _frmgbill)
        {
            InitializeComponent();
            this.Text = "SOA Period";
            lbl_desc.Text = "Select period:";
            frmgbill = _frmgbill;

            load_soaperiod();
        }
        public selectsoa(newGuestBilling _frmgbill, String _reg_num, String _rmrttyp)
        {
            InitializeComponent();

            rmrttyp = _rmrttyp;
            frmgbill = _frmgbill;
            reg_num = _reg_num;

            if (rmrttyp == "M")
            {
                this.Text = "SOA Period";
                lbl_desc.Text = "Select period:";

                cbo_1.Show();
                cbo_inv_frm.Hide();
                cbo_inv_to.Hide();
                label1.Hide();
                load_soaperiod();

                button1.Enabled = true;
            }
            else
            {
                this.Text = "SOA Date";
                lbl_desc.Text = "Select charges:";

                cbo_1.Hide();
                cbo_inv_frm.Show();
                cbo_inv_to.Show();
                label1.Show();
                load_charges(cbo_inv_frm);
                load_charges(cbo_inv_to);

                button1.Enabled = false;
            }

            btn_print.Visible = false;
            btn_finalized.Visible = true;
            pbar.Visible = true;
            isReady = true;
        }
        private void selectprint_Load(object sender, EventArgs e)
        {
            
        }

        private void load_soaperiod()
        {
            cbo_1.DataSource = db.QueryBySQLCode("SELECT soa_desc, to_Char(soafrom,'yyyy/MM/dd')||'-'||to_Char(soato,'yyyy/MM/dd') AS soa_dt FROM rssys.soa_period WHERE COALESCE(closed,'')<>'Y' ORDER BY soafrom desc,soato desc");
            cbo_1.DisplayMember = "soa_desc";
            cbo_1.ValueMember = "soa_dt";
            try { cbo_1.SelectedIndex = 0; }
            catch { }
        }
        private void load_charges(ComboBox cbo)
        {
            DataTable dt = db.QueryBySQLCode("SELECT to_char(chg_date,'YYYY/MM/DD')||' '||cf.chg_code||'-'||cf.chg_num AS chg_desc FROM rssys.chgfil cf JOIN rssys.charge c ON (c.chg_code=cf.chg_code AND c.chg_type='C' AND c.isdeposit=FALSE) WHERE reg_num='" + reg_num + "' AND COALESCE(soa_code,'')='' ORDER BY chg_date, cf.chg_code, cf.chg_num");

            cbo.DataSource = dt;
            cbo.DisplayMember = "chg_desc";
            cbo.ValueMember = "chg_desc";
            try { cbo.SelectedIndex = 0; }
            catch { }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo_1.SelectedIndex != -1)
                {
                    if (frmgbill != null)
                    {
                        String[] splt = cbo_1.SelectedValue.ToString().Split('-');
                        frmgbill.print_soa(splt[0], splt[1]);
                    }
                }
                else
                {
                    MessageBox.Show("Select option first.");
                }
            }
            catch
            {
                MessageBox.Show("Select option first.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hotel_System.m_soaperiods frm = new Hotel_System.m_soaperiods();
            frm.ShowDialog();
        }

        private void btn_finalized_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean proceed = false;

                if (cbo_1.Visible)
                {
                    if (cbo_1.SelectedIndex != -1)
                    {
                        if (frmgbill != null)
                        {
                            proceed = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select option first.");
                    }
                }
                else
                {
                    proceed = true;
                }

                if (proceed)
                {
                    if (MessageBox.Show("Are you sure you want to finalize this billing?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bgworker.RunWorkerAsync();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Select option first.");
            }
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            reset_pbar();
            
            if (frmgbill != null)
            {
                String soa_period = "";
                Boolean hasEntry = false, success = false;
                DataTable dt_charge = new DataTable();

                if (cbo_1.Visible)
                {
                    soa_period = cbo_get_value(cbo_1);
                    String[] splt = soa_period.Split('-');

                    dt_charge = db.QueryBySQLCode("SELECT cf.chg_code, cf.chg_num, cf.chg_date AS t_date, cf.t_time, cf.user_id, c.chg_desc as description, cf.doc_type, cf.\"reference\", cf.amount FROM rssys.chgfil cf LEFT JOIN rssys.charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num ='" + reg_num + "' AND (cf.chg_date BETWEEN '" + DateTime.Parse(splt[0]).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(splt[1]).ToString("yyyy-MM-dd") + "') AND chg_type='C' AND c.isdeposit=FALSE AND COALESCE(soa,'')<>'Y' ORDER BY cf.chg_date desc, c.postcharge, c.utility, cf.t_time desc");
                }
                else
                {
                    dt_charge = db.QueryBySQLCode("SELECT cf.chg_code, cf.chg_num, cf.chg_date AS t_date, cf.t_time, cf.user_id, c.chg_desc as description, cf.doc_type, cf.\"reference\", cf.amount FROM rssys.chgfil cf LEFT JOIN rssys.charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num ='" + reg_num + "' AND (to_char(chg_date,'YYYY/MM/DD')||' '||cf.chg_code||'-'||cf.chg_num BETWEEN '" + cbo_get_value(cbo_inv_frm) + "' AND '" + cbo_get_value(cbo_inv_to) + "') AND chg_type='C' AND c.isdeposit=FALSE AND COALESCE(soa,'')<>'Y' ORDER BY cf.chg_date desc, c.postcharge, c.utility, cf.t_time desc");
                }

                inc_pbar(25);

                if(dt_charge!=null)
                {
                    if(dt_charge.Rows.Count!=0)
                    {
                        hasEntry = true;

                        String val, col;
                        String soa_code, soa_desc, tenant_no, tenant_name, rom_code;
                        Double total_charges = 0.00;

                        soa_code = db.get_pk("soa_code");
                        tenant_name = db.get_colval("gfolio", "full_name", "reg_num='" + reg_num + "'");
                        rom_code = db.get_colval("gfolio", "rom_code", "reg_num='" + reg_num + "'");
                        tenant_no = db.get_colval("gfolio", "acct_no", "reg_num='" + reg_num + "'");


                        if (cbo_1.Visible)
                        {
                            soa_desc = "The SOA period of " + soa_period + " has been finalized.\nThe generated SOA number is " + soa_code + ".";
                        }
                        else
                        {
                            soa_period = DateTime.Parse(db.get_colval("gfolio", "arr_date", "reg_num='" + reg_num + "'")).ToString("yyyy/MM/dd");
                            soa_desc = "The SOA date of " + soa_period + " has been finalized.\nThe generated SOA number is " + soa_code + ".";
                        }

                        col = "soa_code, debt_code, debt_name, soa_period, user_id, comments, due_date, t_date, t_time, branch";
                        val = "'" + soa_code + "', '" + tenant_no + "', '" + tenant_name + "', '" + soa_period + "', '" + GlobalClass.username + "', '" + soa_desc + "', '" + db.get_systemdate("") + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + GlobalClass.branch + "'";

                        //Finalized Unit Billing with Contract#" + reg_num + " by " + GlobalClass.username + "

                        if (db.InsertOnTable("soahdr", col, val))
                        {
                            inc_pbar(5);
                            String ln_num, invoice, reference, gfolio, chg_code, chg_date, chg_time, chg_num, istransferred, balance;
                            
                            String val2, col2;
                            col2 = "soa_code, ln_num, invoice, reference, amount, gfolio, chg_date, chg_time, chg_code, chg_num,istransferred";

                            try
                            {
                                for (int i = 0; i < dt_charge.Rows.Count; i++)
                                {
                                    inc_pbar(10);
                                    ln_num = (i + 1).ToString();
                                    invoice = soa_code;
                                    gfolio = reg_num;

                                    reference = dt_charge.Rows[i]["reference"].ToString();
                                    chg_date = DateTime.Parse(dt_charge.Rows[i]["t_date"].ToString()).ToString("yyyy-MM-dd");
                                    chg_time = dt_charge.Rows[i]["t_time"].ToString();
                                    chg_code = dt_charge.Rows[i]["chg_code"].ToString();
                                    chg_num = dt_charge.Rows[i]["chg_num"].ToString();
                                    istransferred = "Y";

                                    balance = gm.toNormalDoubleFormat(dt_charge.Rows[i]["amount"].ToString()).ToString("0.00");

                                    total_charges += gm.toNormalDoubleFormat(balance);

                                    val2 = "'" + soa_code + "', '" + ln_num + "', '" + invoice + "', $$" + reference + "$$, '" + balance + "', '" + gfolio + "', '" + chg_date + "', '" + chg_time + "', '" + chg_code + "', '" + chg_num + "', '" + istransferred + "'";

                                    if (db.InsertOnTable("soalne", col2, val2))
                                    {
                                        db.UpdateOnTable("chgfil", "soa='Y', soa_code='" + invoice + "'", "chg_num='" + chg_num + "' AND chg_code='" + chg_code + "' AND reg_num='" + gfolio + "' ");
                                    }
                                    else
                                    {

                                    }
                                }
                                success = true;
                            }
                            catch
                            {
                                success = false;
                            }

                            if (success)
                            {
                                db.set_pkm99("soa_code", db.get_nextincrementlimitchar(soa_code, 8));
                                inc_pbar(10);

                                chg_code = db.get_colval("m99", "fnlz_chg", "");
                                db.insert_charges(reg_num, tenant_name, chg_code, rom_code, soa_desc, total_charges, db.get_systemdate(""), "", "", "", "", "", 0.00, 0.00, 0.00, "", "", "", true);
                                db.UpdateOnTable("chgfil", "soa='Y', soa_code='" + soa_code + "'", "COALESCE(soa_code,'')='' AND chg_code='" + chg_code + "' AND reg_num='" + reg_num + "'");

                                inc_pbar100();
                                MessageBox.Show("Successfully finalized unit billing.");
                            }
                            else
                            {
                                db.DeleteOnTable("soalne", "soa_code='" + soa_code + "'");
                                db.DeleteOnTable("soahdr", "soa_code='" + soa_code + "'");
                                inc_pbar100();
                                MessageBox.Show("Failed to finalized unit billing.");
                            }
                        }
                    }
                }

                if (!hasEntry)
                {
                    inc_pbar100();
                    MessageBox.Show("Nothing to finalize select soa period.");
                }

                if (success)
                {
                    Invoke(new Action(() => {
                        frmgbill.disp_chgfil(reg_num);
                        this.Close();
                    }));
                }
            }
        }

        private String cbo_get_value(ComboBox cbo)
        {
            String val = "";
            cbo.Invoke(new Action(() => {
                val = cbo.SelectedValue.ToString();
            }));
            return val;
        }


        private void inc_pbar100()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value = 100;
                }));
            }
            catch (Exception) { }
        }
        private void inc_pbar(int i)
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }

        private void reset_pbar()
        {
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
        }

        private void cbo_inv_frm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            cbo_inv_to.SelectedValue = cbo_inv_frm.SelectedValue.ToString();
            cbo_inv_to.DroppedDown = true;
        }



    }
}
