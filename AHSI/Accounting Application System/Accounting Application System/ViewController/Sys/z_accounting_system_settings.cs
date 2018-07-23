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
    public partial class z_accounting_system_settings : Form
    {
        public z_accounting_system_settings()
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            
            gc.load_journal(cbo_pj);
            gc.load_journal(cbo_sj);
            gc.load_journal(cbo_gj);

            gc.load_account_title(cbo_salereturn);
            gc.load_account_title(cbo_checkOnHand);
            gc.load_account_title(cbo_merch_inv);
            gc.load_account_title(cbo_cashOnHand);
            gc.load_account_title(cbo_pos);
            gc.load_account_title(cbo_vat);
            gc.load_account_title(cbo_ivat);
            gc.load_account_title(cbo_unitbilling);


            gc.load_charge(cbo_rmchg_reg);
            gc.load_charge(cbo_rmchg_senior);
            gc.load_charge(cbo_dep_incidental);
            gc.load_charge(cbo_dep_security);
            gc.load_charge(cbo_transferbal);
            gc.load_charge(cbo_transferpay);
            gc.load_charge(cbo_finalize);
            gc.load_rooms(cbo_sroom);

            dispinfo();
        }

        private void z_accounting_system_settings_Load(object sender, EventArgs e)
        {
            //z_clerkpassword frm = new z_clerkpassword(true);

            //frm.ShowDialog();

            //if (frm.get_adminpasswordconfrimation() == false)
            //{
            //    MessageBox.Show("Invalid Password.\nSorry, you are not allowed to access this window.");
            //    this.Close();
            //}

            show();

        }

        private void show()
        {
            /*
            try
            {
                var emailInfo = all.emails.FirstOrDefault();

                if (emailInfo != null)
                {
                    txt_sender_email.Text = emailInfo.sender;
                    txt_sender_password.Text = emailInfo.password;
                    txt_smtp.Text = emailInfo.smtp;
                    txt_smtp_port.Text = emailInfo.port_number.ToString();
                    txt_receiver_1.Text = emailInfo.receiverEmail1;
                    txt_receiver_2.Text = emailInfo.receiverEmail2;
                    txt_time.Text = emailInfo.mailingTimeSchedule;
                    txt_mail_subj.Text = emailInfo.subject;
                    txt_mail_content.Text = emailInfo.content;
                }
            }
            catch (Exception er) { MessageBox.Show("Email Information might be empty. Please check the tab for email.\n"+er.Message); }     */       
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            thisDatabase db = new thisDatabase();
            String check_by = "", comp_president = "", pres_identity = "", pres_pd_issue = "";
            String pur_jrl = "", sal_jrl = "", gen_jrl = "", acct_cash = "", acct_stks = "", acct_sales = "", acct_vat = "", acct_sret = "", vat_rate = "", acct_chk = "", trnx_date = "", gt_pct = "", sc_pct = "", chg_trb = "", rom_chg = "", govt_chg = "", sc_chg = "", tot_room = "", resv_fol = "", rom_chg2 = "", chg_trp = "", dep_incidental = "", dep_security = "", hibal_d = "", hibal_m = "", hibal_w = "", longstay_d = "", longstay_m = "", longstay_w = "", time_autosend = "", fnlz_chg = "", acct_ivat = "", acct_billing = "", sroom = "";
            String day_of_posting = "";
            String col = "", val = "", add_col = "", add_val = "";
            String table = "m99";
            Boolean success = false;

            if (cbo_rmchg_reg.SelectedIndex == -1 || cbo_rmchg_senior.SelectedIndex == -1 || cbo_cashOnHand.SelectedIndex == -1 || cbo_checkOnHand.SelectedIndex == -1 || cbo_dep_incidental.SelectedIndex == -1 || cbo_dep_security.SelectedIndex == -1 || cbo_gj.SelectedIndex == -1 || cbo_merch_inv.SelectedIndex == -1 || cbo_pj.SelectedIndex == -1 || cbo_pos.SelectedIndex == -1 || cbo_salereturn.SelectedIndex == -1 || cbo_sj.SelectedIndex == -1 || cbo_transferbal.SelectedIndex == -1 || cbo_transferpay.SelectedIndex == -1 || cbo_vat.SelectedIndex == -1 || cbo_ivat.SelectedIndex == -1 || cbo_finalize.SelectedIndex == -1 || cbo_sroom.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                pur_jrl = cbo_pj.SelectedValue.ToString();
                sal_jrl = cbo_sj.SelectedValue.ToString();
                gen_jrl = cbo_gj.SelectedValue.ToString();
                acct_cash = cbo_cashOnHand.SelectedValue.ToString();
                acct_stks = cbo_merch_inv.SelectedValue.ToString();
                acct_sales = cbo_pos.SelectedValue.ToString();
                acct_vat = cbo_vat.SelectedValue.ToString();
                acct_sret = cbo_salereturn.SelectedValue.ToString();
                acct_chk = cbo_checkOnHand.SelectedValue.ToString();
                
                day_of_posting = txt_day_posting_sched.Text;
                check_by = txt_check_by.Text;
                comp_president = txt_president.Text;
                pres_identity = txt_president_id.Text;
                pres_pd_issue = txt_place_issue.Text;

                trnx_date = dtp_dt.Value.ToString("yyyy-MM-dd");

                gt_pct = txt_govt_tax.Text;
                sc_pct = txt_service_chg.Text;

                chg_trb = cbo_transferbal.SelectedValue.ToString();
                rom_chg = cbo_rmchg_reg.SelectedValue.ToString();
                tot_room = txt_totalroom.Text;
                resv_fol = txt_reservation_folio.Text;
                rom_chg2 = cbo_rmchg_senior.SelectedValue.ToString();
                chg_trp = cbo_transferpay.SelectedValue.ToString();
                dep_incidental = cbo_dep_incidental.SelectedValue.ToString();
                dep_security = cbo_dep_security.SelectedValue.ToString();
                fnlz_chg = cbo_finalize.SelectedValue.ToString();

                acct_billing = (cbo_unitbilling.SelectedValue ?? "").ToString();
                acct_ivat = cbo_ivat.SelectedValue.ToString();
                sroom = (cbo_sroom.SelectedValue ?? "").ToString();
                 
                hibal_d = txt_guestbal_daily.Text;
                hibal_m = txt_guestbal_monthly.Text;
                hibal_w = txt_guestbal_weekly.Text;
                longstay_d = txt_longstay_daily.Text;
                longstay_m = txt_longstay_monthly.Text;
                longstay_w = txt_longstay_weekly.Text;
                time_autosend = txt_time.Text;

                col = "pur_jrl='" + pur_jrl + "', sal_jrl='" + sal_jrl + "', gen_jrl='" + gen_jrl + "', acct_cash='" + acct_cash + "', acct_stks='" + acct_stks + "', acct_sales='" + acct_sales + "', acct_vat='" + acct_vat + "', acct_ivat='" + acct_ivat + "', acct_sret='" + acct_sret + "', acct_chk='" + acct_chk + "', trnx_date='" + trnx_date + "', gt_pct='" + gt_pct + "'" + ", sc_pct='" + sc_pct + "', chg_trb='" + chg_trb + "', rom_chg='" + rom_chg + "', tot_room='" + tot_room + "', resv_fol='" + resv_fol + "', rom_chg2='" + rom_chg2 + "', chg_trp='" + chg_trp + "', check_by='" + check_by + "', comp_president='" + comp_president + "', pres_identity='" + pres_identity + "', pres_pd_issue='" + pres_pd_issue + "', dep_incidental='" + dep_incidental + "', dep_security='" + dep_security + "', hibal_d='" + hibal_d + "', hibal_m='" + hibal_m + "', hibal_w='" + hibal_w + "', longstay_d='" + longstay_d + "', longstay_m='" + longstay_m + "', longstay_w='" + longstay_w + "', time_autosend='" + time_autosend + "', day_of_posting='" + day_of_posting + "',fnlz_chg='" + fnlz_chg + "',acct_billing='" + acct_billing + "', rom_spc='" + sroom + "'";

                if (db.UpdateOnTable(table, col, ""))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(txt_sender_email.Text) ||
                            string.IsNullOrEmpty(txt_sender_password.Text) ||
                            string.IsNullOrEmpty(txt_smtp.Text) ||
                            string.IsNullOrEmpty(txt_smtp_port.Text) ||
                            string.IsNullOrEmpty(txt_receiver_1.Text) ||
                            string.IsNullOrEmpty(txt_receiver_2.Text) ||
                            string.IsNullOrEmpty(txt_time.Text) ||
                            string.IsNullOrEmpty(txt_mail_subj.Text) ||
                            string.IsNullOrEmpty(txt_mail_content.Text))
                        {
                            MessageBox.Show("Please fill out the e-mail form properly");
                        }

                        else
                        {
                            String port = txt_smtp_port.Text,password = "", smtp = "", port_number = "", receiverEmail1 = "", receiverEmail2 = "", mailingTimeSchedule = "", subject = "", content="";
                                sender = txt_sender_email.Text;
                                password = txt_sender_password.Text;
                                smtp = txt_smtp.Text;
                                port_number = port;
                                receiverEmail1 = txt_receiver_1.Text;
                                receiverEmail2 = txt_receiver_2.Text;
                                mailingTimeSchedule = txt_time.Text;
                                subject = txt_mail_subj.Text;
                                content = txt_mail_content.Text;
                                String col2 = "email_sender='" + sender + "', e_sender_password='"+password+"'";

                                if (db.UpdateOnTable(table, col, ""))
                                {
                                    MessageBox.Show("Sender Email and Password Successfully Updated.");

                                }
                                else { 
                                   
                                }
                        }

                        success = true;
                        MessageBox.Show("Successfully saved.");
                        show();

                    }
                    catch(Exception er) { MessageBox.Show("Email not saved!\n" + er.Message); }


                    
                }
                else
                {
                    MessageBox.Show("Failed on saving.");
                }
            }

            if (success)
            {
                //tpg_info_enable(false);
                this.Close();
            } 
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tpg_info_enable(Boolean flag)
        {
            dtp_dt.Enabled = flag;
            
            cbo_rmchg_reg.Enabled = flag;
            cbo_rmchg_senior.Enabled = flag;
            cbo_transferbal.Enabled = flag;
            cbo_transferpay.Enabled = flag;
            cbo_dep_incidental.Enabled = flag;
            cbo_dep_security.Enabled = flag;

            txt_reservation_folio.Enabled = flag;
            txt_totalroom.Enabled = flag;

            txt_guestbal_daily.Enabled = flag;
            txt_guestbal_monthly.Enabled = flag;
            txt_guestbal_weekly.Enabled = flag;
            txt_longstay_daily.Enabled = flag;
            txt_longstay_monthly.Enabled = flag;
            txt_longstay_weekly.Enabled = flag;

            txt_govt_tax.Enabled = flag;
            txt_service_chg.Enabled = flag;

            //Accounting
            cbo_gj.Enabled = flag;
            cbo_pj.Enabled = flag;
            cbo_sj.Enabled = flag;
            cbo_cashOnHand.Enabled = flag;
            cbo_checkOnHand.Enabled = flag;
            cbo_pos.Enabled = flag;
            cbo_salereturn.Enabled = flag;
            cbo_merch_inv.Enabled = flag;
            cbo_vat.Enabled = flag;

            //Automatic Email
            txt_smtp.Enabled = flag;
            txt_smtp_port.Enabled = flag;
            txt_sender_email.Enabled = flag;
            txt_sender_password.Enabled = flag;

            txt_time.Enabled = flag;
            txt_receiver_1.Enabled = flag;
            txt_receiver_2.Enabled = flag;
            txt_reservation_folio.Enabled = flag;
            txt_mail_content.Enabled = flag;
            txt_mail_subj.Enabled = flag;
        }

        private void dispinfo()
        {
            thisDatabase db = new thisDatabase();
            GlobalClass gc = new GlobalClass();
            String hr = "", min = "";
            int ampm = 0;

            DataTable dt = db.QueryAllOnTable("m99");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    //General
                    dtp_dt.Value = Convert.ToDateTime(dt.Rows[0]["trnx_date"].ToString());

                    gc.set_cbo_selectedvalue(cbo_rmchg_reg, dt.Rows[0]["rom_chg"].ToString());
                    gc.set_cbo_selectedvalue(cbo_rmchg_senior, dt.Rows[0]["rom_chg2"].ToString());
                    gc.set_cbo_selectedvalue(cbo_transferbal, dt.Rows[0]["chg_trb"].ToString());
                    gc.set_cbo_selectedvalue(cbo_transferpay, dt.Rows[0]["chg_trp"].ToString());
                    gc.set_cbo_selectedvalue(cbo_dep_incidental, dt.Rows[0]["dep_incidental"].ToString());
                    gc.set_cbo_selectedvalue(cbo_dep_security, dt.Rows[0]["dep_security"].ToString());

                    txt_reservation_folio.Text = dt.Rows[0]["resv_fol"].ToString();
                    txt_totalroom.Text = dt.Rows[0]["tot_room"].ToString();

                    txt_guestbal_daily.Text = dt.Rows[0]["hibal_d"].ToString();
                    txt_guestbal_monthly.Text = dt.Rows[0]["hibal_m"].ToString();
                    txt_guestbal_weekly.Text = dt.Rows[0]["hibal_w"].ToString();
                    txt_longstay_daily.Text = dt.Rows[0]["longstay_d"].ToString();
                    txt_longstay_monthly.Text = dt.Rows[0]["longstay_m"].ToString();
                    txt_longstay_weekly.Text = dt.Rows[0]["longstay_w"].ToString();

                    txt_govt_tax.Text = dt.Rows[0]["gt_pct"].ToString();
                    txt_service_chg.Text = dt.Rows[0]["sc_pct"].ToString();

                    //Accounting
                    gc.set_cbo_selectedvalue(cbo_gj, dt.Rows[0]["gen_jrl"].ToString());
                    gc.set_cbo_selectedvalue(cbo_pj, dt.Rows[0]["pur_jrl"].ToString());
                    gc.set_cbo_selectedvalue(cbo_sj, dt.Rows[0]["sal_jrl"].ToString());
                    gc.set_cbo_selectedvalue(cbo_cashOnHand, dt.Rows[0]["acct_cash"].ToString());
                    gc.set_cbo_selectedvalue(cbo_checkOnHand, dt.Rows[0]["acct_chk"].ToString());
                    gc.set_cbo_selectedvalue(cbo_pos, dt.Rows[0]["acct_sales"].ToString());
                    gc.set_cbo_selectedvalue(cbo_salereturn, dt.Rows[0]["acct_sret"].ToString());
                    gc.set_cbo_selectedvalue(cbo_merch_inv, dt.Rows[0]["acct_stks"].ToString());
                    gc.set_cbo_selectedvalue(cbo_vat, dt.Rows[0]["acct_vat"].ToString());
                    gc.set_cbo_selectedvalue(cbo_finalize, dt.Rows[0]["fnlz_chg"].ToString());
                    gc.set_cbo_selectedvalue(cbo_unitbilling, dt.Rows[0]["acct_billing"].ToString());
                    gc.set_cbo_selectedvalue(cbo_ivat, dt.Rows[0]["acct_ivat"].ToString());
                    gc.set_cbo_selectedvalue(cbo_sroom, dt.Rows[0]["rom_spc"].ToString());

                    //Automatic Email
                    txt_president.Text = dt.Rows[0]["comp_president"].ToString();
                    txt_president_id.Text = dt.Rows[0]["pres_identity"].ToString();
                    txt_place_issue.Text = dt.Rows[0]["pres_pd_issue"].ToString();
                    txt_check_by.Text = dt.Rows[0]["check_by"].ToString();
                    txt_sender_email.Text = dt.Rows[0]["email_sender"].ToString();
                    txt_sender_password.Text = dt.Rows[0]["e_sender_password"].ToString();
                    txt_day_posting_sched.Text = dt.Rows[0]["day_of_posting"].ToString();
                    txt_smtp.Text = dt.Rows[0][""].ToString();
                    txt_smtp_port.Text = dt.Rows[0][""].ToString();
                    
                    txt_time.Text = hr;
                    //cbo_ampm.SelectedIndex = 0; //0=AM 1=PM
                    txt_receiver_1.Text = dt.Rows[0][""].ToString();
                    txt_receiver_2.Text = dt.Rows[0][""].ToString();
                    txt_reservation_folio.Text = dt.Rows[0][""].ToString();
                    txt_mail_content.Text = dt.Rows[0][""].ToString();
                    txt_mail_subj.Text = dt.Rows[0][""].ToString();
                   
                }
            }
            catch(Exception) {}
        }

        private void tbpg_general_Click(object sender, EventArgs e)
        {

        }
    }
}
