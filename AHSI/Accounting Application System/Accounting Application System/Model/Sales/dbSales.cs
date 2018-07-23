using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Npgsql;

namespace Accounting_Application_System
{
    class dbSales : thisDatabase
    {
        GlobalClass gc;
        GlobalMethod gm;

        public dbSales()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
        }

        public DataTable get_disctbl()
        {
            return QueryOnTableWithParams("disctbl", "*", "", " ORDER BY disc_code ASC");
        }

        public Double get_discpct(String disc_code)
        {
            Double val = 0.00;

            try
            {
                DataTable dt = this.QueryBySQLCode("SELECT disc1 FROM rssys.disctbl WHERE disc_code='" + disc_code + "'");

                val = gm.toNormalDoubleFormat(dt.Rows[0]["disc1"].ToString());
            }
            catch(Exception) {}

            return val;
        }

        public DataTable get_sales_return(DateTime frm, DateTime to)
        {
            return this.QueryBySQLCode("SELECT o.*, w.* FROM " + schema + ".rethdr o LEFT JOIN  " + schema + ".whouse w ON w.whs_code=o.whs_code WHERE o.ret_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "'");
        }
        public DataTable get_soret_item_list(String ret_num)
        {
            return this.QueryBySQLCode("SELECT r.*, u.* FROM " + schema + ".retlne r LEFT JOIN " + schema + ".itmunit u ON r.unit=u.unit_id WHERE r.ret_num='" + ret_num + "' ORDER BY r.ln_num ASC");
        }

        public DataTable get_outlet_list()
        {
            return QueryBySQLCode("SELECT o.*, s.cc_code AS cc_code FROM " + this.schema + ".outlet o LEFT JOIN " + this.schema + ".subctr s ON o.scc_code=s.scc_code ORDER BY o.branch, o.out_code ASC;");
        }

        public DataTable get_outlet_list_by_branch(String branch, Boolean isPOS, Boolean isCarSale, Boolean isRepair)
        {
            String val = "";

            if (isPOS)
            {
                val = val + " AND o.ottyp='POS' ";
            }

            if (isCarSale)
            {
                val = val + " AND o.ottyp='CS' ";
            }

            if(isRepair)
            {
                val = val + " AND o.ottyp='R' ";
            }

            return QueryBySQLCode("SELECT o.*, s.cc_code AS cc_code, w.whs_desc FROM " + this.schema + ".outlet o LEFT JOIN " + this.schema + ".subctr s ON o.scc_code=s.scc_code LEFT JOIN " + this.schema + ".whouse w ON w.whs_code=o.whs_code WHERE o.branch='" + branch + "' " + val + ";");
        }

        public int get_noOfOutlet()
        {
            int r = 0;
            
            try
            {
                DataTable dt = QueryBySQLCode("SELECT COUNT(out_code) FROM " + this.schema + ".outlet ");

                r = gm.toInt(dt.Rows[0][0].ToString());
            }
            catch { }

            return r;
        }

        public String get_ord_code(String out_code)
        {
            String ord_code = "";

            try
            {
                DataTable dt = this.QueryOnTableWithParams("outlet", "ord_code", "out_code='" + out_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    ord_code = dt.Rows[0][0].ToString();
                }
            }
            catch { }

            return ord_code;
        }

        public Boolean set_ord_code_nextno(String ord_code, String out_code)
        {
            string newvalue = "";
            int len = ord_code.Length;
            var split = ord_code.ToCharArray();
            int num = 0;

            try
            {
                if (split[0].ToString().All(char.IsDigit) == false)
                {
                    char pre = split[0];
                    split[0] = '0';
                    String temp = ord_code.Substring(1);
                    num = Convert.ToInt32(temp);
                    num++;
                    len = len - 1;

                    while (len > 0)
                    {
                        newvalue += "0";
                        len--;
                    }

                    newvalue = pre.ToString() + num.ToString(newvalue);
                }
                else
                {
                    num = Convert.ToInt32(ord_code);
                    num++;


                    while (len > 0)
                    {
                        newvalue += "0";
                        len--;
                    }

                    newvalue = num.ToString(newvalue);
                }
            }
            catch (Exception er) { MessageBox.Show("Exception on get_nextincrementlimitchar " + er.Message ); }

            return this.UpdateOnTable("outlet", "ord_code='" + newvalue + "'", "out_code='"+out_code+"'");
        }

        public Double get_outlet_govt_pct(String out_code)
        {
            Double govt_pct = 0.00;

            try
            {
                DataTable dt = this.QueryOnTableWithParams("outlet", "govt_pct", "out_code='" + out_code + "'", "");
                govt_pct = gm.toNormalDoubleFormat(dt.Rows[0][0].ToString());               
            }
            catch { }

            return govt_pct;
        }

        public Boolean is_outlet_repair(String out_code)
        {
            Boolean flag = false;
                        
            try
            {
                DataTable dt = QueryOnTableWithParams("outlet", "*", "out_code='" + out_code + "' AND ottyp='R'", "");
                

                if (dt.Rows.Count > 0)
                    flag = true;
            }
            catch { }

            return flag;
        }

        public Boolean is_outlet_warranty(String out_code)
        {
            Boolean flag = false;

            try
            {
                DataTable dt = QueryOnTableWithParams("outlet", "*", "out_code='" + out_code + "' AND warranty='true'", "");


                if (dt.Rows.Count > 0)
                    flag = true;
            }
            catch { }

            return flag;
        }

        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
                dt = this.QueryBySQLCode("SELECT ol.*, u.*, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "' ORDER BY " + this.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }

        public String get_outlet_whs_code(String out_code)
        {
            DataTable dt;
            String whs_code = "";

            try
            {
                dt = this.QueryOnTableWithParams("outlet", "whs_code", "out_code='" + out_code + "'", "");

                whs_code = dt.Rows[0][0].ToString(); 
            }
            catch { }

            return whs_code;
        }
    }
}
