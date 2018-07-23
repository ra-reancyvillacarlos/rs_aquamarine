using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting_Application_System
{
    public partial class SalesServiceLoan : Form
    {
        public SalesServiceLoan()
        {
            InitializeComponent();
            
            gc.load_branch(cbo_branch_service1);
            display_Chart1();
            display_Chart2();
            display_chart();
            displaydgv();

            gc.load_branch(cbo_branch_sales);
            display_Chart3();
            display_Chart4();
            chart1_Sales.ChartAreas["ChartArea1"].AxisY.ScrollBar.Size = 10;
            chart1_Sales.ChartAreas["ChartArea1"].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chart1_Sales.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;
            chart1_Sales.ChartAreas["ChartArea1"].AxisY.ScrollBar.Enabled = true;
        }

        private void SalesServiceLoan_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        string[] days = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", 
                                       "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                                       "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
 

        private void ServiceDashBoard_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }
        public void display_Chart1()
        {
            String year = "2017";
            String date = DateTime.Now.ToString("yyyy");
            Boolean check = false;
            date = gm.toDateValue(date).ToString("MMMM");

            DataTable dt = new DataTable();
            String lll = "SELECT DISTINCT to_char(r.ord_date, 'dd') as day,r.statuss, count(r.total_stat) as total_status FROM (SELECT  ord_date , ro.ro_stat_desc as statuss, (SELECT COUNT(status) as total_stat FROM rssys.orhdr WHERE to_char(ord_date, 'FMMonth') ='" + dtp_service1.Value.ToString("MMMM") + "' AND to_char(ord_date,'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "') from rssys.orhdr o left join rssys.ro_status ro ON o.status=ro.ro_stat_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_service1.Value.ToString("MMMM") + "' AND  to_char(ord_date,'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "') r  WHERE r.statuss = 'Released' OR r.statuss ='Pending' OR r.statuss ='Ready for Release' GROUP BY  r.ord_date,r.statuss ORDER BY day";

            dt = db.QueryBySQLCode("SELECT DISTINCT to_char(r.ord_date, 'dd') as day,r.statuss, count(r.total_stat) as total_status FROM (SELECT  ord_date , ro.ro_stat_desc as statuss, (SELECT COUNT(status) as total_stat FROM rssys.orhdr WHERE to_char(ord_date, 'FMMonth') ='" + dtp_service1.Value.ToString("MMMM") + "' AND to_char(ord_date,'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "') from rssys.orhdr o left join rssys.ro_status ro ON o.status=ro.ro_stat_code JOIN rssys.outlet ot ON o.out_code=ot.out_code WHERE  '" + (cbo_branch_service1.SelectedValue ?? "").ToString() + "' IN (ot.branch,'') AND  to_char(ord_date, 'FMMonth') = '" + dtp_service1.Value.ToString("MMMM") + "' AND  to_char(ord_date,'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "') r  WHERE r.statuss = 'Released' OR r.statuss ='Pending' OR r.statuss ='Ready for Release' OR r.statuss ='Check Up' OR r.statuss ='On Going Repair' OR r.statuss ='Drive Testing' GROUP BY  r.ord_date,r.statuss ORDER BY day");

            if (dt.Rows.Count > 0)
            {
                label10.Text = "";
                foreach (var seriess in chart1_service1.Series)
                {
                    seriess.Points.Clear();
                }
                String[] series = { "Pending", "Released", "Ready for Release", "Check Up", "On Going Repair", "Drive Testing", };
                for (int i = 0; i < days.Length; i++)
                {
                    int[] series_val = new int[series.Length];
                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                    {
                        if (days[i] == dt.Rows[x]["day"].ToString())
                        {
                            for (int y = 0; y < series.Length; y++)
                            {
                                if (dt.Rows[x]["statuss"].ToString().ToLower() == series[y].ToLower())
                                {
                                    series_val[y] = (int)Double.Parse(dt.Rows[x]["total_status"].ToString());
                                    dt.Rows.RemoveAt(x);
                                    break;
                                }
                            }
                        }
                    }
                    for (int y = 0; y < series.Length; y++)
                    {
                        if (series_val[y] != 0)
                        {
                            this.chart1_service1.Series[series[y]].Points.AddXY(days[i], series_val[y]);

                        }
                        else
                        {
                            this.chart1_service1.Series[series[y]].Points.AddXY(days[i], 0);
                        }
                    }
                }
            }
            else {
                label10.Text = "[ No Record to be Plotted on the Graph. ]";
                foreach (var seriess in chart1_service1.Series)
                {
                    seriess.Points.Clear();
                }
                String[] series = { "Pending", "Released", "Ready for Release" };
                for (int i = 0; i < days.Length; i++)
                {
                    int[] series_val = new int[series.Length];
                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                    {
                        if (days[i] == dt.Rows[x]["day"].ToString())
                        {
                            for (int y = 0; y < series.Length; y++)
                            {
                                if (dt.Rows[x]["statuss"].ToString().ToLower() == series[y].ToLower())
                                {
                                    series_val[y] = (int)Double.Parse(dt.Rows[x]["total_status"].ToString());
                                    dt.Rows.RemoveAt(x);
                                    break;
                                }
                            }
                        }
                    }
                    for (int y = 0; y < series.Length; y++)
                    {
                        if (series_val[y] != 0)
                        {
                            this.chart1_service1.Series[series[y]].Points.AddXY(days[i], 0);

                        }
                        else
                        {
                            this.chart1_service1.Series[series[y]].Points.AddXY(days[i], 0);
                        }
                    }
                }
            }

        }
        public void display_Chart2()
        {

            DataTable dt = new DataTable();
            String yow = "SELECT *, (SELECT COUNT(*)   FROM rssys.orhdr o WHERE o.status=rs.ro_stat_code AND ord_date ='" + dtp_service1.Value.ToString("yyyy-MM-dd") + "') AS total FROM rssys.ro_status rs";

            //dt = db.QueryBySQLCode("SELECT *, (SELECT COUNT(*)  FROM rssys.orhdr o WHERE o.status=rs.ro_stat_code AND 


            dt = db.QueryBySQLCode("SELECT  DISTINCT ro.ro_stat_desc,SUM(ord_amnt) AS total_sales, (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_service1.Value.ToString("MMMM") + "' AND to_char(ord_date, 'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "' )  as total , to_char(ord_date, 'FMMonth') as months, SUM((ord_amnt / (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_service1.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "' )) * 1.0 ) as percent from rssys.orhdr orh JOIN rssys.ro_status ro ON orh.status = ro.ro_stat_code JOIN rssys.outlet ot ON ot.out_code=orh.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_service1.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_service1.Value.ToString("yyyy") + "'  AND ord_amnt<>0 AND '" + (cbo_branch_service1.SelectedValue ?? "").ToString() + "' IN (ot.branch,'')  GROUP BY ro.ro_stat_desc,months,total");
            


            chart2_service1.DataSource = dt;
            chart2_service1.Series["Series1"].XValueMember = "ro_stat_desc";
            chart2_service1.Series["Series1"].YValueMembers = "percent";
            chart2_service1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            if (dt.Rows.Count > 0)
            {
                label6.Text = "";
                lbl_total_sales.Text = gm.toAccountingFormat(dt.Rows[0]["total"].ToString());
            }
            else {
                label6.Text = "[ No Service Amount to be Calculated. ]";
                lbl_total_sales.Text = "";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            display_Chart1();
            display_Chart2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbo_branch_service1.SelectedIndex = -1;
        }

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_Chart1();
            display_Chart2();
        }

        private void dtp_service1_ValueChanged(object sender, EventArgs e)
        {
            display_Chart1();
            display_Chart2();
        }

        private void cbo_branch_service1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_Chart1();
            display_Chart2();
        }

        private void btn_reset_service1_Click(object sender, EventArgs e)
        {
            cbo_branch_service1.SelectedIndex = -1;
        }//


      
        Boolean isOpen = true;

        public void display_Chart3()
        {
            String year = "2017";
            String date = DateTime.Now.ToString("yyyy");
            Boolean check = false;
            date = gm.toDateValue(date).ToString("MMMM");
            
            DataTable dt = new DataTable();
            String ooo = "SELECT DISTINCT to_char(r.ord_date, 'dd') as day,r.outlet, count(r.total_stat) as total_status FROM (SELECT  ord_date , ol.out_desc as outlet, (SELECT COUNT(out_code) as total_stat FROM rssys.orhdr WHERE to_char(ord_date, 'FMMonth') ='" + dtp_Sales.Value.ToString("MMMM") + "' AND to_char(ord_date,'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "') from rssys.orhdr o  LEFT JOIN  rssys.ro_status ro ON o.status=ro.ro_stat_code LEFT JOIN rssys.outlet ol ON ol.out_code=o.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "' AND  to_char(ord_date,'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "'AND  '" + (cbo_branch_sales.SelectedValue ?? "").ToString() + "' IN (ol.branch,'')) r GROUP BY  r.ord_date,r.outlet ORDER BY day"; 

            dt = db.QueryBySQLCode("SELECT DISTINCT to_char(r.ord_date, 'dd') as day,r.outlet, count(r.total_stat) as total_status FROM (SELECT  ord_date , ol.out_desc as outlet, (SELECT COUNT(out_code) as total_stat FROM rssys.orhdr WHERE to_char(ord_date, 'FMMonth') ='" + dtp_Sales.Value.ToString("MMMM") + "' AND to_char(ord_date,'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "') from rssys.orhdr o  LEFT JOIN  rssys.ro_status ro ON o.status=ro.ro_stat_code LEFT JOIN rssys.outlet ol ON ol.out_code=o.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "' AND  to_char(ord_date,'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "'AND  '"+(cbo_branch_sales.SelectedValue??"").ToString()+"' IN (ol.branch,'')) r GROUP BY  r.ord_date,r.outlet ORDER BY day");



            if (dt.Rows.Count > 0)
            {
                lbl_notif_sales.Text = "";
                foreach (var seriess in chart1_Sales.Series)
                {
                    seriess.Points.Clear();
                }
                String[] series = {   
                            "OVER THE COUNTER"
                            ,"BRAND NEW VEHICLE"
                            ,"BODY, REPAIR, AND PAINT"
                            ,"PREVENTIVE MAINTENANCE SERVICE"
                            ,"GENERAL JOB"
                            ,"INSURANCE"
                            ,"WARRANTY"
                            };
                for (int i = 0; i < days.Length; i++)
                {
                    int[] series_val = new int[series.Length];
                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                    {
                        if (days[i] == dt.Rows[x]["day"].ToString())
                        {
                            for (int y = 0; y < series.Length; y++)
                            {
                                if (dt.Rows[x]["outlet"].ToString().ToLower() == series[y].ToLower())
                                {
                                    series_val[y] = (int)Double.Parse(dt.Rows[x]["total_status"].ToString());
                                    dt.Rows.RemoveAt(x);
                                    break;
                                }
                            }
                        }
                    }
                    for (int y = 0; y < series.Length; y++)
                    {
                        if (series_val[y] != 0)
                        {
                            this.chart1_Sales.Series[series[y]].Points.AddXY(days[i], series_val[y]);

                        }
                        else
                        {
                            this.chart1_Sales.Series[series[y]].Points.AddXY(days[i], 0);
                        }
                    }
                }
            }
            else
            {
                lbl_notif_sales.Text = "[ No Records to be Plotted.]";
                foreach (var seriess in chart1_Sales.Series)
                {
                    seriess.Points.Clear();
                }
                String[] series = {   
                            "OVER THE COUNTER"
                            ,"BRAND NEW VEHICLE"
                            ,"BODY, REPAIR, AND PAINT"
                            ,"PREVENTIVE MAINTENANCE SERVICE"
                            ,"GENERAL JOB"
                            ,"INSURANCE"
                            ,"WARRANTY"
                            };
                for (int i = 0; i < days.Length; i++)
                {
                    int[] series_val = new int[series.Length];
                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                    {
                        if (days[i] == dt.Rows[x]["day"].ToString())
                        {
                            for (int y = 0; y < series.Length; y++)
                            {
                                if (dt.Rows[x]["outlet"].ToString().ToLower() == series[y].ToLower())
                                {
                                    series_val[y] = (int)Double.Parse(dt.Rows[x]["total_status"].ToString());
                                    dt.Rows.RemoveAt(x);
                                    break;
                                }
                            }
                        }
                    }
                    for (int y = 0; y < series.Length; y++)
                    {
                        if (series_val[y] != 0)
                        {
                            this.chart1_Sales.Series[series[y]].Points.AddXY(days[i], 0);

                        }
                        else
                        {
                            this.chart1_Sales.Series[series[y]].Points.AddXY(days[i], 0);
                        }
                    }
                }
            }
        }

        public void display_Chart4() {
            
            DataTable dt = new DataTable();
            //String yow = "SELECT *, (SELECT COUNT(*)   FROM rssys.orhdr o WHERE o.status=rs.ro_stat_code AND ord_date ='" + dtp_Sales.Value.ToString("yyyy-MM-dd") + "') AS total FROM rssys.ro_status rs";

            //dt = db.QueryBySQLCode("SELECT *, (SELECT COUNT(*)  FROM rssys.orhdr o WHERE o.status=rs.ro_stat_code AND ord_date ='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"') AS total FROM rssys.ro_status rs");
           //string ss = "SELECT  DISTINCT ot.out_desc,SUM(total_amnt) AS total_sales, (SELECT SUM(total_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "' AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )  as total , to_char(ord_date, 'FMMonth') as months, SUM((total_amnt / (SELECT SUM(total_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )) * 1.0 ) as percent from rssys.orhdr orh JOIN rssys.outlet ot ON orh.out_code = ot.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "'  AND total_amnt<>0 GROUP BY ot.out_desc,months,total";
            string yow = "SELECT  DISTINCT ot.out_desc,SUM(ord_amnt) AS total_sales, (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "' AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )  as total , to_char(ord_date, 'FMMonth') as months, SUM((ord_amnt / (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )) * 1.0 ) as percent from rssys.orhdr orh JOIN rssys.outlet ot ON orh.out_code = ot.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "'  AND ord_amnt<>0 GROUP BY ot.out_desc,months,total";

            dt = db.QueryBySQLCode("SELECT  DISTINCT ot.out_desc,SUM(ord_amnt) AS total_sales, (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "' AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )  as total , to_char(ord_date, 'FMMonth') as months, SUM((ord_amnt / (SELECT SUM(ord_amnt)  from rssys.orhdr WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' )) * 1.0 ) as percent from rssys.orhdr orh JOIN rssys.outlet ot ON orh.out_code = ot.out_code WHERE to_char(ord_date, 'FMMonth') = '" + dtp_Sales.Value.ToString("MMMM") + "'AND to_char(ord_date, 'YYYY') = '" + dtp_Sales.Value.ToString("yyyy") + "' AND  '" + (cbo_branch_sales.SelectedValue ?? "").ToString() + "' IN (ot.branch,'') AND ord_amnt<>0 GROUP BY ot.out_desc,months,total");
            
            

            chart2_sales.DataSource = dt;
            chart2_sales.Series["Series1"].XValueMember = "out_desc";
            chart2_sales.Series["Series1"].YValueMembers = "total_sales";
            chart2_sales.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            if (dt.Rows.Count > 0)
            {
                label9.Text  = "";
                lbl_total.Text = gm.toAccountingFormat(dt.Rows[0]["total"].ToString());
            }
            else {
                lbl_total.Text = "";
                label9.Text = "[ No Sales to be Calculated in this Month. ]";
            }
        }
        private void SalesDashBoard_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
 

       

        private void dtp_Sales_ValueChanged(object sender, EventArgs e)
        {
            display_Chart3();
            display_Chart4();
        }

        private void cbo_branch_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (isOpen)
            //{

            //    isOpen = false;
            //}
            //else
            //{
                display_Chart3();
                display_Chart4();
            //}
        }

        private void btn_reset_sales_Click(object sender, EventArgs e)
        {
            cbo_branch_sales.SelectedIndex = -1;
        }

        
             
           
      
        private void LoanStatusStat_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

        public void display_chart() {

            DataTable dt = new DataTable();
            Boolean contained  = false;


            dt = db.QueryBySQLCode("SELECT  DISTINCT a.credit_des,SUM(paid_amt) AS total_sales, (SELECT SUM(paid_amt)  from rssys.autoloandhr WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "' AND to_char(trnx_date, 'YYYY') = '" + dtp_loan.Value.ToString("yyyy") + "')  as total , to_char(trnx_date, 'FMMonth') as months, SUM((paid_amt / (SELECT SUM(paid_amt)  from rssys.autoloandhr WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "'AND to_char(trnx_date, 'YYYY') = '" + dtp_loan.Value.ToString("yyyy") + "' )) * 1.0 ) as percent from rssys.autoloandhr a JOIN rssys.decision d ON a.credit_des = d.decision_name WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "' AND to_char(trnx_date, 'YYYY') = '" + dtp_loan.Value.ToString("yyyy") + "' AND paid_amt<>0 GROUP BY a.credit_des,months,total");
            
            chart1_loan.DataSource = dt;
            chart1_loan.Series["Series1"].XValueMember = "credit_des";
            chart1_loan.Series["Series1"].YValueMembers = "percent";
            chart1_loan.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["total"].ToString() != "0")
                {
                    contained = true;
                }
            }
            if (!contained)
            {
                lbl_notif2.Text = "[ No Trasactions Done. ]";
            }
            else {
                lbl_notif2.Text = "";
            }
        }
        void displaydgv()
        {
            DataTable dt = new DataTable();
             
            dt = db.QueryBySQLCode("SELECT  DISTINCT a.credit_des,SUM(paid_amt) AS total_sales, (SELECT SUM(paid_amt)  from rssys.autoloandhr WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "' AND to_char(trnx_date, 'YYYY') = '"+dtp_loan.Value.ToString("yyyy")+"' )  as total , to_char(trnx_date, 'FMMonth') as months, SUM((paid_amt / (SELECT SUM(paid_amt)  from rssys.autoloandhr WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "' AND to_char(trnx_date, 'YYYY') = '" + dtp_loan.Value.ToString("yyyy") + "')) * 1.0 ) as percent from rssys.autoloandhr a JOIN rssys.decision d ON a.credit_des = d.decision_name WHERE to_char(trnx_date, 'FMMonth') = '" + dtp_loan.Value.ToString("MMMM") + "'  AND to_char(trnx_date, 'YYYY') ='" + dtp_loan.Value.ToString("yyyy") + "' AND paid_amt<>0 GROUP BY a.credit_des,months,total");

            dataGridView1.DataSource = dt;
        }
         

        private void dtp_loan_ValueChanged(object sender, EventArgs e)
        {
            display_chart();
            displaydgv();
        }

        private void chart2_sales_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Sales_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void cbo_autoloan_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_chart();
            displaydgv();
        }
    }
}
