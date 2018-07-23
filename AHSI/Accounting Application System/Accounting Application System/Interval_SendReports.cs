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
    class Interval_SendReports
    {
        public readonly static Interval_SendReports GET = new Interval_SendReports();

        private GlobalClass gc = new GlobalClass();
        private GlobalMethod gm = new GlobalMethod();
        thisDatabase db = new thisDatabase();
        Timer _interval = new Timer();

        Boolean isBusy = false;
        Boolean isReset = false;

        public Interval_SendReports(){}

        public void init_load()
        {
            load_current_datetime();
        }


        private void t_LogBox_Load(object sender, EventArgs e)
        {
            load_current_datetime();
        }

        private void load_current_datetime()
        {
            _interval.Stop();
            _interval.Tick += new EventHandler(delegate(object sender, EventArgs e)
            {
                DateTime now = DateTime.Now;
                if (!isBusy){
                    isBusy = true;{ // if call back, well not called

                        String dnow = DateTime.Now.ToString("yyyy-MM-dd"); // for DAILY ONLY
                        DateTime setTimefrm = DateTime.Parse(dnow + " 07:00pm");// start
                        DateTime setTimeto = DateTime.Parse(dnow + " 07:35pm");// end

                        if (setTimefrm <= now && now <= setTimeto && isReset)
                        {
                            isReset = false;

                            load_reports(dnow, setTimefrm, setTimeto);

                        }
                        else if (setTimeto < now)
                        {
                            isReset = true;
                        }
                    }isBusy = false;//
                }
            });
            _interval.Interval = 100;
            _interval.Start();
        }

        public void load_reports(String dnow, DateTime dt_frm, DateTime dt_to)
        {
            //Start of Statement


            //**Cashier Reports GRAND TOTALs
            DataTable dt = db.QueryBySQLCode("SELECT o.ord_date, SUM(CASE WHEN ol.pay_code='101' AND COALESCE(ol.item_code,'')='' THEN (-1 * ol.ln_amnt) ELSE 0.00 END) AS cash, SUM(CASE WHEN ol.pay_code='102' AND COALESCE(ol.item_code,'')='' THEN (-1 * ol.ln_amnt) ELSE 0.00 END) AS dcard, SUM(CASE WHEN ol.pay_code='103' AND COALESCE(ol.item_code,'')='' THEN (-1 * ol.ln_amnt) ELSE 0.00 END) AS card, SUM(CASE WHEN ol.pay_code='114' AND COALESCE(ol.item_code,'')='' THEN (-1 * ol.ln_amnt) ELSE 0.00 END) AS check, SUM(CASE WHEN ol.pay_code NOT IN('101','102','103','114') THEN (-1 * ol.ln_amnt) ELSE 0.00 END) AS other, SUM(CASE WHEN COALESCE(ol.item_code,'')<>'' THEN ol.ln_amnt ELSE 0.00 END) AS sales, SUM(CASE WHEN COALESCE(ol.item_code,'')<>'' THEN ol.ln_tax ELSE 0.00 END) AS tax_amnt, SUM(CASE WHEN COALESCE(ol.item_code,'')<>'' THEN ol.disc_amt ELSE 0.00 END) AS disc_amnt FROM rssys.orhdr o LEFT JOIN rssys.orlne ol ON o.ord_code=ol.ord_code WHERE (o.ord_date BETWEEN '" + dnow + "' AND '" + dnow + "') GROUP BY o.ord_date");

            if (dt != null)
            {   //keys : cash dcard card price other sales tax_amnt disc_amnt
                if (dt.Rows.Count == 1)
                {
                    String grand_cash = dt.Rows[0]["cash"].ToString();
                    String grand_debit_card = dt.Rows[0]["dcard"].ToString();
                    String grand_credit_card = dt.Rows[0]["cash"].ToString();
                    String grand_check = dt.Rows[0]["check"].ToString();
                    String grand_other = dt.Rows[0]["other"].ToString();
                    String grand_sales = dt.Rows[0]["sales"].ToString();
                    String grand_tax_amnt = dt.Rows[0]["tax_amnt"].ToString();
                    String grand_disc_amnt = dt.Rows[0]["disc_amnt"].ToString();


                }
            }




            //End of Statement
        }

    }



}
