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
    public partial class z_Calendar2 : Form
    {

        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        DateTime dt_current;
        Timer _interval = new Timer();

        ToolTip PopUptt = new ToolTip();
        Boolean isOpen = true;
        
        public z_Calendar2()
        {
            InitializeComponent();
            init_load();
            //display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }
        public z_Calendar2(String option)
        {
            InitializeComponent();
            init_load();
            cbo_option.Text = option;

            isOpen = false;
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }
        public z_Calendar2(String branchid, String option)
        {
            InitializeComponent();
            init_load();
            cbo_option.Text = option;
            cbo_branch.SelectedValue = branchid;

            isOpen = false;
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }

        public DateTime getDateTimeNow() {
            //return DateTime.Parse(db.get_systemdate("") + " " + db.get_systemtime());
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"));
        }

        public void init_load() {
            gc.load_branch(cbo_branch);

            PopUptt.ToolTipTitle = "";
            PopUptt.IsBalloon = true;
            PopUptt.ShowAlways = true;

            cbo_branch.SelectedIndex = -1;
            dt_current = getDateTimeNow();
            
            //SELECT AFTER_NOW('2017-05-01','2017-06-07','1 week');
            db.QueryBySQLCode("CREATE OR REPLACE FUNCTION AFTER_NOW(dt_date timestamp,date_now timestamp,added char(10)) RETURNS DATE AS $$ BEGIN WHILE dt_date <= date_now LOOP dt_date = (dt_date + (added)::interval); END LOOP; RETURN dt_date; END; $$ LANGUAGE plpgsql;");
            db.QueryBySQLCode("CREATE OR REPLACE FUNCTION AFTER_NOW(dt_date timestamp,dt_date_end timestamp,date_now timestamp,added char(10)) RETURNS DATE AS $$ DECLARE t_date timestamp = dt_date; BEGIN  WHILE t_date < date_now LOOP CASE  WHEN t_date >= dt_date_end AND date_now >= dt_date_end THEN RETURN t_date;  ELSE t_date = (t_date + (added)::interval);  END CASE;  END LOOP;  IF DATE(t_date) = DATE(date_now)THEN IF((t_date)::time < (date_now)::time) THEN t_date = (t_date + ('-'||added)::interval); END IF; END IF; RETURN t_date;  END;  $$ LANGUAGE plpgsql;");
            //

            load_current_datetime();
        }


        private void z_Calendar2_Load(object sender, EventArgs e){
            this.WindowState = FormWindowState.Maximized;
        }
        private void z_Calendar2_FormClosing(object sender, FormClosingEventArgs e)
        {
            _interval.Stop();
        }

        public void display_Calendar(String year, String month) {
            DateTime dt_start = DateTime.Parse(getDateTimeNow().ToString("yyyy-MM-01"));
            try { dt_start = DateTime.Parse(year + "-" + month + "-1"); }
            catch { }

            dt_current = DateTime.Parse(dt_start.ToString("yyyy-MM-01"));
            
            //start
            DateTime dt_end = dt_start.AddMonths(1).AddDays(-1);
            Boolean inCurrMon = false;
            if (getDateTimeNow().ToString("yyyy-MM-01") == dt_start.ToString("yyyy-MM-01"))
            {
                inCurrMon=true;
            }

            int firstDay = ((int)dt_start.DayOfWeek % 7) + 1;
            int start = 1;
            int end = (int)Double.Parse(dt_end.ToString("dd"));
            int prv = (int)Double.Parse(dt_start.AddDays(-1).ToString("dd"));
            int next = (int)Double.Parse(dt_end.AddDays(1).ToString("dd"));

            int now = (int)Double.Parse(getDateTimeNow().ToString("dd"));

            clear_btn_opt();
            load_dateMonthNotif(dt_start.AddDays(-1 * firstDay + 1).ToString("yyyy-MM-dd"), dt_end.AddDays(42 - (firstDay + end + 1)).ToString("yyyy-MM-dd"));
            
            int cnt = 1;
            do{

                String disp_num = cnt.ToString();
                Label lbl_D = getControl("lbl_d" + cnt) as Label;
                Boolean inRangeDay = false;
                
                if (cnt < firstDay){
                    disp_num = (prv - firstDay + cnt + 1).ToString();
                }
                else if (firstDay <= cnt && cnt < (firstDay + end)){
                    disp_num = (cnt - firstDay + 1).ToString();
                    inRangeDay = true;
                }
                else{
                    disp_num = (cnt - (firstDay + end) + 1).ToString();
                }

                lbl_D.Text = "00".Substring(disp_num.Length) + disp_num;
                lbl_D.ForeColor = Color.Black;
                if (now == (int)Double.Parse(disp_num) && inRangeDay && inCurrMon){
                    lbl_D.BackColor = Color.Yellow;
                }
                else {
                    if (inRangeDay){
                        lbl_D.BackColor = Color.White;
                    }
                    else {
                        lbl_D.BackColor = Color.DimGray; //DimGray
                        lbl_D.ForeColor = Color.White; //DarkGray
                    }
                }

                cnt++;
            }while (cnt<=42);

            lbl_monyear.Text = dt_start.ToString("MMMM / yyyy");
            disp_images();
        }
        private void load_current_datetime()
        {
            _interval.Stop();
            _interval.Tick += new EventHandler(delegate(object sender, EventArgs e)
            {
                DateTime dt_start = DateTime.Parse(getDateTimeNow().ToString("yyyy-MM-01"));
                DateTime dt_end = dt_start.AddMonths(1).AddDays(-1);
                int firstDay = ((int)dt_start.DayOfWeek % 7) + 1;
                int end = (int)Double.Parse(dt_end.ToString("dd"));

                dt_start = DateTime.Parse(dt_start.AddDays(-1 * firstDay + 1).ToString("yyyy-MM-dd"));
                dt_end = DateTime.Parse(dt_end.AddDays(42 - (firstDay + end + 1)).ToString("yyyy-MM-dd"));
                
                for (int i = 0; i < dgv_datedetails.Rows.Count-1; i++)
                {
                    try {

                        DateTime dt_date = DateTime.Parse(dgv_datedetails["dgvl_date", i].Value.ToString());
                        if (!(dt_start <= dt_date && dt_date <= dt_end))
                        {
                            continue;
                        }
                        else if (!String.IsNullOrEmpty((dgv_datedetails["dgvl_reuse", i].Value ?? "").ToString()))
                        {
                            continue;
                        }
                        String stat = dgv_datedetails["dgvl_msg", i].Value.ToString();
                        String opt = dgv_datedetails["dgvl_opt", i].Value.ToString();
                        if (opt == "1")
                        {
                            if (stat != "Once")
                            {
                                DateTime dt_date_end = DateTime.Parse(dgv_datedetails["dgvl_date_end", i].Value.ToString());
                                DateTime dt_now = getDateTimeNow();

                                if (dt_date <= dt_now)
                                {
                                    while (dt_date < dt_now)// 
                                    {
                                        if (dt_date >= dt_date_end && dt_now >= dt_date_end) break;

                                        if (stat == "Daily")
                                        {
                                            dt_date = dt_date.AddDays(1);
                                        }
                                        else if (stat == "Weekly")
                                        {
                                            dt_date = dt_date.AddDays(7);
                                        }
                                        else if (stat == "Monthly")
                                        {
                                            dt_date = dt_date.AddMonths(1);
                                        }
                                    }
                                    if (dt_date.ToString("yyyy-MM-dd") == dt_now.ToString("yyyy-MM-dd"))
                                    {
                                        if (dt_date.Ticks < dt_now.Ticks)
                                        {
                                            if (stat == "Daily")
                                            {
                                                dt_date = dt_date.AddDays(-1);
                                            }
                                            else if (stat == "Weekly")
                                            {
                                                dt_date = dt_date.AddDays(-7);
                                            }
                                            else if (stat == "Monthly")
                                            {
                                                dt_date = dt_date.AddMonths(-1);
                                            }
                                        }
                                    }
                                    int indx = -1;
                                    for (int j = i + 1; j < dgv_datedetails.Rows.Count; j++)
                                    {
                                        if (String.IsNullOrEmpty((dgv_datedetails["dgvl_reuse", j].Value ?? "").ToString()))
                                            continue;

                                        if (dgv_datedetails["dgvl_date", i].Value == dgv_datedetails["dgvl_reuse", j].Value)
                                        {
                                            if (dt_date > DateTime.Parse(dgv_datedetails["dgvl_date", j].Value.ToString()))
                                            {
                                                indx = j;
                                                break;
                                            }
                                        }
                                    }
                                    if (indx != -1)
                                    {
                                        int r = dgv_datedetails.Rows.Add();
                                        dgv_datedetails["dgvl_date", r].Value = dt_date.ToString("yyy-MM-dd hh:mm tt");
                                        dgv_datedetails["dgvl_date_end", r].Value = dgv_datedetails["dgvl_date_end", i].Value;
                                        dgv_datedetails["dgvl_opt", r].Value = dgv_datedetails["dgvl_opt", i].Value;
                                        dgv_datedetails["dgvl_msg", r].Value = dgv_datedetails["dgvl_msg", i].Value;
                                        dgv_datedetails["dgvl_cnt", r].Value = dgv_datedetails["dgvl_cnt", i].Value;
                                        dgv_datedetails["dgvl_reuse", r].Value = dgv_datedetails["dgvl_reuse", i].Value;

                                        dgv_datedetails["dgvl_cnt", indx].Value = "0";
                                    }
                                }
                            }
                        }
                    
                    }
                    catch { }
                }
                // alert;
                disp_images();



            });
            _interval.Interval = 500; //100 = 1second
            _interval.Start();
        }



        public void clear_btn_opt()
        {
            for (int i = 1; i <= 42; i++)
            {
                (getControl("btn_opt1" + i) as PictureBox).Hide();
                (getControl("btn_opt2" + i) as PictureBox).Hide();
                (getControl("btn_opt3" + i) as PictureBox).Hide();
                (getControl("btn_opt4" + i) as PictureBox).Hide();
            }
        }
        public void load_dateMonthNotif(String dt_frm, String dt_to)
        {
            DataTable dt = null;
            try
            {
                String dt_now = getDateTimeNow().ToString("yyyy-MM-dd hh:mm tt");
                String WHERE = "";
                if (cbo_branch.SelectedIndex != -1) {
                    WHERE = " branch='" + cbo_branch.SelectedValue.ToString() + "'";
                }

                dt = db.QueryBySQLCode("(SELECT DISTINCT to_char(date_to_remind,'yyyy-MM-dd')||' '||time_to_remind as dt_date,CASE WHEN COALESCE(repeat_reminder,'Once') IN ('Daily','Weekly','Monthly') THEN repeat_reminder ELSE 'Once' END AS status, count(taskid) as cnt, 1 as option, to_char(date_to_remind_to,'yyyy-MM-dd')||' '||time_to_remind AS dt_date_end  FROM rssys.taskhdr WHERE (date_to_remind BETWEEN '" + dt_frm + "' AND '" + dt_to + "' OR (AFTER_NOW((date_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 day') BETWEEN '" + dt_frm + "' AND '" + dt_to + "')  OR (AFTER_NOW((date_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 week') BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (AFTER_NOW((date_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 month') BETWEEN '" + dt_frm + "' AND '" + dt_to + "')) AND user_id='" + GlobalClass.username + "' GROUP BY dt_date, dt_date_end, status UNION ALL SELECT DISTINCT to_char(o.t_date,'yyyy-MM-dd') as dt_date, COALESCE(ro.ro_stat_desc,'Other') AS status, count(o.ord_code) as cnt, 2 as option,'' AS dt_date_end FROM rssys.orhdr o LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.status LEFT JOIN  rssys.whouse w ON w.whs_code=o.loc LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code WHERE o.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' AND COALESCE(o.promise_time,'')<>'' AND out_code in (SELECT out_code FROM rssys.outlet " + (WHERE == "" ? "" : "WHERE " + WHERE) + ") GROUP BY dt_date,ro.ro_stat_desc UNION ALL SELECT DISTINCT to_char(o.t_date,'yyyy-MM-dd') as dt_date, COALESCE(ro.ro_stat_desc,'Other') AS status, count(o.ord_code) as cnt, 3 as option,'' AS dt_date_end FROM rssys.orhdr o LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE o.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' AND o.out_code in (SELECT out_code FROM rssys.outlet WHERE ottyp='CS' " + (WHERE == "" ? "" : "AND " + WHERE) + ") GROUP BY  dt_date,ro.ro_stat_desc UNION ALL SELECT DISTINCT to_char(o.t_date,'yyyy-MM-dd') as dt_date, COALESCE(ro.ro_stat_desc,'Other') AS status, count(o.ord_code) as cnt, 4 as option,'' AS dt_date_end FROM rssys.orhdr o LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE o.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' AND o.out_code in (SELECT out_code FROM rssys.outlet WHERE ottyp='POS' " + (WHERE == "" ? "" : "AND " + WHERE) + ") GROUP BY  dt_date,ro.ro_stat_desc) ORDER BY dt_date ASC"); // option3 
            }
            catch { }
            if (dt != null) {
                dgv_datedetails.Rows.Clear();
                //String[] repeat_reminder = { "Daily", "Weekly", "Monthly", "Once" };
                for (int i = 0; i < dt.Rows.Count; i++) {
                    int r = dgv_datedetails.Rows.Add();

                    dgv_datedetails["dgvl_date", r].Value = dt.Rows[i]["dt_date"].ToString();
                    dgv_datedetails["dgvl_date_end", r].Value = dt.Rows[i]["dt_date_end"].ToString();
                    dgv_datedetails["dgvl_opt", r].Value = dt.Rows[i]["option"].ToString();
                    dgv_datedetails["dgvl_msg", r].Value = dt.Rows[i]["status"].ToString();
                    dgv_datedetails["dgvl_cnt", r].Value = dt.Rows[i]["cnt"].ToString();

                    //if(dgv_datedetails["dgvl_msg", r].Value)
                    if (dt.Rows[i]["option"].ToString() == "1") {
                        if (dt.Rows[i]["status"].ToString() != "Once")
                        {
                            DateTime dt_date = DateTime.Parse(dt.Rows[i]["dt_date"].ToString());
                            DateTime dt_date_end = DateTime.Parse(dt.Rows[i]["dt_date_end"].ToString());
                            DateTime dt_now = getDateTimeNow();

                            if (dt_date <= dt_now)
                            {
                                while (dt_date < dt_now)// 
                                {
                                    if (dt_date >= dt_date_end && dt_now >= dt_date_end) break;

                                    if (dt.Rows[i]["status"].ToString() == "Daily"){
                                        dt_date = dt_date.AddDays(1);
                                    }
                                    else if (dt.Rows[i]["status"].ToString() == "Weekly"){
                                        dt_date = dt_date.AddDays(7);
                                    }
                                    else if (dt.Rows[i]["status"].ToString() == "Monthly"){
                                        dt_date = dt_date.AddMonths(1);
                                    }
                                }
                                if (dt_date.ToString("yyyy-MM-dd") == dt_now.ToString("yyyy-MM-dd")) {
                                    if (dt_date.Ticks < dt_now.Ticks)
                                    {
                                        if (dt.Rows[i]["status"].ToString() == "Daily"){
                                            dt_date = dt_date.AddDays(-1);
                                        }
                                        else if (dt.Rows[i]["status"].ToString() == "Weekly"){
                                            dt_date = dt_date.AddDays(-7);
                                        }
                                        else if (dt.Rows[i]["status"].ToString() == "Monthly"){
                                            dt_date = dt_date.AddMonths(-1);
                                        }
                                    }
                                }
                                r = dgv_datedetails.Rows.Add();
                                dgv_datedetails["dgvl_date", r].Value = dt_date.ToString("yyy-MM-dd hh:mm tt");
                                dgv_datedetails["dgvl_date_end", r].Value = dt.Rows[i]["dt_date_end"].ToString();
                                dgv_datedetails["dgvl_opt", r].Value = dt.Rows[i]["option"].ToString();
                                dgv_datedetails["dgvl_msg", r].Value = dt.Rows[i]["status"].ToString();
                                dgv_datedetails["dgvl_cnt", r].Value = dt.Rows[i]["cnt"].ToString();
                                dgv_datedetails["dgvl_reuse", r].Value = dgv_datedetails["dgvl_date", r - 1].Value;
                            }
                        }
                    }
                }
            }
        }

        private Control getControl(String id) {
            try{ return (this.Controls.Find(id, true).FirstOrDefault()); }
            catch { }
            return null;
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            dt_current = dt_current.AddMonths(-1);
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }
        private void btn_prevyear_Click(object sender, EventArgs e)
        {
            dt_current = dt_current.AddYears(-1);
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            dt_current = dt_current.AddMonths(1);
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }
        private void btn_nextyear_Click(object sender, EventArgs e)
        {
            dt_current = dt_current.AddYears(1);
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }

        private void lbl_d_TextChanged(object sender, EventArgs e){}


        private String getDateFromLblIndex(int indx) {
            Label lbl_D = getControl("lbl_d" + indx) as Label;
            DateTime dt_end = dt_current.AddMonths(1).AddDays(-1);
            int firstDay = ((int)dt_current.DayOfWeek % 7) + 1;
            int end = (int)Double.Parse(dt_end.ToString("dd"));
            int prv = (int)Double.Parse(dt_current.AddDays(-1).ToString("dd"));
            int next = (int)Double.Parse(dt_end.AddDays(1).ToString("dd"));
           
            if (indx < firstDay){
                return dt_current.AddDays(-1).ToString("yyyy-MM-") + lbl_D.Text;
            }
            else if (firstDay <= indx && indx < (firstDay + end)){
                return dt_current.ToString("yyyy-MM-") + lbl_D.Text;
            }
            else{
                return dt_end.AddDays(1).ToString("yyyy-MM-") + lbl_D.Text;
            }
        }
        private void cbo_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_images();
        }
        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isOpen) return;
            display_Calendar(dt_current.ToString("yyyy"), dt_current.ToString("MM"));
        }


        private void disp_images() {
            DateTime dt_end = dt_current.AddMonths(1).AddDays(-1);
            int firstDay = ((int)dt_current.DayOfWeek % 7) + 1;
            int end = (int)Double.Parse(dt_end.ToString("dd"));
            int prv = (int)Double.Parse(dt_current.AddDays(-1).ToString("dd"));
            int next = (int)Double.Parse(dt_end.AddDays(1).ToString("dd"));
            String cbo_text = cbo_option.Text.ToLower();

            for (int i = dgv_datedetails.Rows.Count - 1; i >= 0 ; i--){
                try{
                    DateTime dt = DateTime.Parse(dgv_datedetails["dgvl_date", i].Value.ToString());
                    int day = (int)Double.Parse(dt.ToString("dd"));
                    int indx = 0;
                    if (dt < dt_current){
                        indx = (firstDay - 1) - prv - day;
                    }
                    else if (dt > dt_end){
                        indx = firstDay + end + day - 1;
                    }
                    else{
                        indx = firstDay + day - 1;
                    }
                    if (indx > 0){
                        int opt_n = 0;
                        try{ opt_n = (int)Double.Parse(dgv_datedetails["dgvl_opt", i].Value.ToString()); }
                        catch{}

                        if (opt_n != 0)
                        {
                            String opt = "btn_opt" + opt_n;
                            PictureBox pb = getControl(opt + indx) as PictureBox;
                            if (dgv_datedetails["dgvl_cnt", i].Value.ToString() == "0")
                            {
                                pb.Visible = false;
                                dgv_datedetails.Rows.RemoveAt(i);
                                continue;
                            }
                            else
                            {
                                if (cbo_text == "all")
                                {
                                    pb.Visible = true;
                                }
                                else
                                {
                                    if (opt == "btn_opt1" && cbo_text == "reminder")
                                        pb.Visible = true;
                                    else if (opt == "btn_opt2" && cbo_text == "auto sales")
                                        pb.Visible = true;
                                    else if (opt == "btn_opt3" && cbo_text == "service status")
                                        pb.Visible = true;
                                    else if (opt == "btn_opt4" && cbo_text == "over the counter")
                                        pb.Visible = true;
                                    else    pb.Visible = false;
                                }
                                if (pb.Visible && pb.Image == null)
                                {
                                    if (opt == "btn_opt1")
                                        pb.Image = Properties.Resources.copy___26;
                                    else if (opt == "btn_opt2")
                                        pb.Image = Properties.Resources.car_26;
                                    else if (opt == "btn_opt3")
                                        pb.Image = Properties.Resources.car_technician_icon_28___36;
                                    else if (opt == "btn_opt4")
                                        pb.Image = Properties.Resources.cashbox;
                                }

                            }
                        }
                    }
                }catch { }
            }
        }

        private void btn_opt1_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int indx = (int)Double.Parse(pb.Name.Replace("btn_opt1", ""));
            String date = getDateFromLblIndex(indx);
            String[] dayly = {"Once","Daily","Weekly","Monthly"};
            int[] cny_dayly = new int[dayly.Length];
            String msg = "Reminder`s: \n";
            for (int i = 0; i < dgv_datedetails.Rows.Count - 1; i++) {
                if (DateTime.Parse(dgv_datedetails["dgvl_date", i].Value.ToString()).ToString("yyyy-MM-dd") == date)
                {
                    if (dgv_datedetails["dgvl_opt", i].Value.ToString() == "1"){
                        indx = Array.IndexOf(dayly, dgv_datedetails["dgvl_msg", i].Value.ToString());
                        cny_dayly[indx] = (int)Double.Parse(dgv_datedetails["dgvl_cnt", i].Value.ToString()) + cny_dayly[indx];
                    }
                }
            }
            foreach(String ly in dayly){
                if (cny_dayly[Array.IndexOf(dayly, ly)] != 0) {
                    msg += "(" + cny_dayly[Array.IndexOf(dayly, ly)] + ")" + ly + "\n";
                }
            }
            PopUptt.SetToolTip(pb, msg);
        }

        private void btn_opt2_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int indx = (int)Double.Parse(pb.Name.Replace("btn_opt2", ""));
            String date = getDateFromLblIndex(indx);
            String msg = "Auto Sales: \n";
            for (int i = 0; i < dgv_datedetails.Rows.Count - 1; i++)
            {
                if (dgv_datedetails["dgvl_date", i].Value.ToString() == date)
                {
                    if (dgv_datedetails["dgvl_opt", i].Value.ToString() == "2")
                    {
                        msg += "(" + dgv_datedetails["dgvl_cnt", i].Value.ToString() + ")" + dgv_datedetails["dgvl_msg", i].Value.ToString()+"\n";
                    }
                }
            }
            PopUptt.SetToolTip(pb, msg);
        }
        private void btn_opt3_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int indx = (int)Double.Parse(pb.Name.Replace("btn_opt3", ""));
            String date = getDateFromLblIndex(indx);
            String msg = "Services: \n";
            for (int i = 0; i < dgv_datedetails.Rows.Count - 1; i++)
            {
                if (dgv_datedetails["dgvl_date", i].Value.ToString() == date)
                {
                    if (dgv_datedetails["dgvl_opt", i].Value.ToString() == "3")
                    {
                        msg += "(" + dgv_datedetails["dgvl_cnt", i].Value.ToString() + ")" + dgv_datedetails["dgvl_msg", i].Value.ToString() + "\n";
                    }
                }
            }
            PopUptt.SetToolTip(pb, msg);
        }
        private void btn_opt4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int indx = (int)Double.Parse(pb.Name.Replace("btn_opt4", ""));
            String date = getDateFromLblIndex(indx);
            String msg = "Over the Counter: \n";
            for (int i = 0; i < dgv_datedetails.Rows.Count - 1; i++)
            {
                if (dgv_datedetails["dgvl_date", i].Value.ToString() == date)
                {
                    if (dgv_datedetails["dgvl_opt", i].Value.ToString() == "4")
                    {
                        msg = "(" + dgv_datedetails["dgvl_cnt", i].Value.ToString() + ")Over the Counter's";
                    }
                }
            }
            PopUptt.SetToolTip(pb, msg);
        }

        private void btn_opt1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            String date = getDateFromLblIndex((int)Double.Parse(pb.Name.Replace("btn_opt1","")));
            disp_list(date, 1, cbo_branch);
        }
        private void btn_opt2_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            String date = getDateFromLblIndex((int)Double.Parse(pb.Name.Replace("btn_opt2", "")));
            disp_list(date, 2, cbo_branch);
        }
        private void btn_opt3_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            String date = getDateFromLblIndex((int)Double.Parse(pb.Name.Replace("btn_opt3", "")));
            disp_list(date, 3, cbo_branch);
        }
        private void btn_opt4_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            String date = getDateFromLblIndex((int)Double.Parse(pb.Name.Replace("btn_opt4", "")));
            disp_list(date, 4, cbo_branch);
        }

        private void lbl_d_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            String date = getDateFromLblIndex((int)Double.Parse(lbl.Name.Replace("lbl_d", "")));
            if (getDateTimeNow().ToString("yyyy-MM-dd") == date){
                PopUptt.SetToolTip(lbl, "Now.");
            }
        }


        //
        DateTime dt_date;
        String WHERE = "",title = "All";
        int opt;
        public void disp_list(String fdate, int option, ComboBox branch)
        {
            dt_date = DateTime.Parse(fdate);
            opt = option;

            if (option == 1)
            {
                title = GlobalClass.user_fullname;
            }
            else if (branch.SelectedIndex != -1)
            {
                WHERE = " branch='" + branch.SelectedValue.ToString() + "'";
                title = branch.Text;
            }

            _init_load();
        }

        public void _init_load()
        {
            if (opt == 1){
                tpgi_item.Text = "Reminder - " + title + "(" + dt_date.ToString("yyyy-MM-dd") + ")";
                disp_list_tasks();
            }
            else if (opt == 2){
                tpgi_item.Text = "Auto Sales - " + title + "(" + dt_date.ToString("yyyy-MM-dd") + ")";
                disp_list_sales();
            }
            else if (opt == 3){
                tpgi_item.Text = "Auto Service - " + title + "(" + dt_date.ToString("yyyy-MM-dd") + ")";
                disp_list_repairorder();
            }
            else if (opt == 4)
            {
                tpgi_item.Text = "Over the Counter - " + title + "(" + dt_date.ToString("yyyy-MM-dd") + ")";
                disp_list_overthecounter();
            }
        }

        private void disp_list_sales()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dgv_list.DataSource = db.QueryBySQLCode("SELECT o.ord_code AS \"Trans Code\", COALESCE(ro.ro_stat_desc,'--') AS \"RO Status\", o.customer AS \"Customer\", v.vin_desc AS \"Car Variant\", v.plate_no AS \"Plate No/Cond No\", o.out_code AS \"Outlet\",  o.ord_date AS \"RO Date\", o.t_date AS \"Trans Date\", o.pending AS \"Pending\", o.cancel AS \"Cancel\", o.ord_amnt AS \"Ord Amount\",  o.disc_amnt AS \"Disc Amount\", o.total_amnt AS \"Total Amount\", o.net_amnt AS \"Net Amount\", o.tax_amnt AS \"Tax Amount\", o.payment AS \"Payment\", o.amnt_due AS \"Amount Due\", rm.rep_name AS \"Cashier\", o.pay_code AS \"Pay Code\", o.debt_code AS \"Cust ID\", o.loc AS \"Stock Location\", o.user_id AS \"User ID\", o.rep_code AS \"Rep ID\" FROM rssys.orhdr o LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.status LEFT JOIN  rssys.whouse w ON w.whs_code=o.loc LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.vehicle_info v ON v.vin_no=COALESCE(o.car_vin_num,o.vehicle)  LEFT JOIN rssys.repmst rm ON rm.rep_code=o.rep_code WHERE (o.t_date BETWEEN '" + dt_date.ToString("yyyy-MM-dd") + "' AND '" + dt_date.ToString("yyyy-MM-dd") + "') AND COALESCE(o.promise_time,'')<>'' AND out_code in (SELECT out_code FROM rssys.outlet " + (WHERE == "" ? "" : "WHERE " + WHERE) + ") ORDER BY o.t_date, o.t_time");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void disp_list_repairorder()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dgv_list.DataSource = db.QueryBySQLCode("SELECT o.ord_code AS \"Trans Code\", COALESCE(ro.ro_stat_desc,'--') AS \"RO Status\", m.d_name  AS \"Customer\", o.ord_date AS \"Ord Date\", o.t_date AS \"Trans Date\", o.t_time AS \"Trans Time\", o.out_code AS \"Outlet\", o.pending AS \"Pending\", o.cancel AS \"Cancel\", o.ord_amnt AS \"Ord Amount\",  o.disc_amnt AS \"Disc Amount\", o.total_amnt AS \"Total Amount\", o.net_amnt AS \"Net Amount\", o.tax_amnt AS \"Tax Amount\", o.payment AS \"Payment\", o.amnt_due AS \"Amount Due\", o.user_id AS \"User ID\", o.loc AS \"Stock Location\", o.user_id2 AS \"User ID\", o.rep_code AS \"Rep ID\", o.promise_date AS \"Promise Date\", o.promise_time AS \"Time\", rm.rep_name AS \"Cashier\", o.ord_amnt AS \"Ord Amount\", o.pay_code AS \"Pay Code\", o.debt_code AS \"Cust ID\" FROM rssys.orhdr o LEFT JOIN  rssys.whouse w ON w.whs_code=o.loc  LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.rorder_status LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.repmst rm ON rm.rep_code=o.rep_code WHERE (o.t_date BETWEEN '" + dt_date.ToString("yyyy-MM-dd") + "' AND '" + dt_date.ToString("yyyy-MM-dd") + "') AND o.out_code in (SELECT out_code FROM rssys.outlet WHERE ottyp='CS' " + (WHERE == "" ? "" : "AND " + WHERE) + ") ORDER BY o.ord_code");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void disp_list_overthecounter()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dgv_list.DataSource = db.QueryBySQLCode("SELECT o.ord_code AS \"Trans Code\", o.customer AS \"Customer\", o.out_code AS \"Outlet\", o.cancel AS \"Cancel\", o.ord_date AS \"Ord Date\", o.ord_amnt AS \"Ord Amount\",  o.disc_amnt AS \"Disc Amount\", o.total_amnt AS \"Total Amount\", o.net_amnt AS \"Net Amount\", o.tax_amnt AS \"Tax Amount\", o.payment AS \"Payment\", o.amnt_due AS \"Amount Due\", w.whs_desc AS \"Stock Location\", o.pay_code AS \"Pay Code\", rm.rep_name AS \"Cashier\", o.agentid AS \"Agent ID\", o.market_segment_id AS \"Market ID\",o.rep_code AS \"Rep ID\", o.mcardid AS \"Servex No\", o.reference AS \"Reference\" FROM rssys.orhdr o LEFT JOIN  rssys.whouse w ON w.whs_code=o.loc  LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.rorder_status LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.repmst rm ON rm.rep_code=o.rep_code WHERE (o.t_date BETWEEN '" + dt_date.ToString("yyyy-MM-dd") + "' AND '" + dt_date.ToString("yyyy-MM-dd") + "') AND o.out_code in (SELECT out_code FROM rssys.outlet WHERE ottyp='POS' " + (WHERE == "" ? "" : "AND " + WHERE) + ") ORDER BY o.ord_code");

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        
        private void disp_list_tasks()
        {
            String dt_now = getDateTimeNow().ToString("yyyy-MM-dd hh:mm tt");
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                
                dgv_list.DataSource = db.QueryBySQLCode("SELECT taskid AS \"Task No\", task_desc AS \"Task Desc\", COALESCE(repeat_reminder,'Once') AS \"Repeat Reminder\",date_task AS \"Task Date No\", date_to_remind AS \"Date to Remind From\", date_to_remind_to AS \"To\", time_to_remind AS \"Time to Remind\", user_id AS \"User ID\", t_date AS \"Trans Date\", t_time AS \"Trans Time\", client_id AS \"Client ID\", client_name AS \"Client Name\", priority_no AS \"Priority No\" FROM rssys.taskhdr WHERE (date_to_remind BETWEEN '" + dt_date.ToString("yyyy-MM-dd") + "' AND '" + dt_date.ToString("yyyy-MM-dd") + "' OR (AFTER_NOW((date_to_remind||' '||time_to_remind)::timestamp,(date_to_remind_to||' '||time_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 day')='" + dt_date.ToString("yyyy-MM-dd") + "' AND COALESCE(repeat_reminder,'Once')='Daily') OR (AFTER_NOW((date_to_remind||' '||time_to_remind)::timestamp,(date_to_remind_to||' '||time_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 week')='" + dt_date.ToString("yyyy-MM-dd") + "' AND COALESCE(repeat_reminder,'Once')='Weekly') OR (AFTER_NOW((date_to_remind||' '||time_to_remind)::timestamp,(date_to_remind_to||' '||time_to_remind)::timestamp,('" + dt_now + "')::timestamp,'1 month')='" + dt_date.ToString("yyyy-MM-dd") + "' AND COALESCE(repeat_reminder,'Once')='Monthly')) AND user_id='" + GlobalClass.username + "'  ORDER BY date_to_remind, time_to_remind");
                //AND '" + dt_now + "'>=date_to_remind_to
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }


    }
}
