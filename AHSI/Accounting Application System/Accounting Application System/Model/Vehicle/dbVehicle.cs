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
    class dbVehicle : thisDatabase
    {
        GlobalClass gc;
        GlobalMethod gm;

        public dbVehicle()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
        }

        public Boolean isItemGroup_Car(String item_grp)
        {
            Boolean flag = false;
            String val = "";
            
            try
            {
                DataTable dt = QueryBySQLCode("SELECT iscar FROM " + this.schema + ".itmgrp WHERE iscar='Y' AND item_grp='"+item_grp+"' ");

                val = dt.Rows[0][0].ToString();
                
                if(String.IsNullOrEmpty(val) == false)
                {
                    if (val == "Y")
                        flag = true;
                }
            }
            catch { }

            return flag;
        }

        public Boolean isItem_Car(String item_code)
        {
            Boolean flag = false;
            String val = "";

            try
            {
                DataTable dt = QueryBySQLCode("SELECT ig.iscar FROM rssys.items i LEFT JOIN rssys.itmgrp ig ON i.item_grp=ig.item_grp WHERE i.item_code='" + item_code + "' ");

                val = dt.Rows[0][0].ToString();

                if (String.IsNullOrEmpty(val) == false)
                {
                    if (val == "Y")
                        flag = true;
                }
            }
            catch { }

            return flag;
        }

        public String get_carcolor(String itemcode)
        {
            String val = "";
            try
            {
                DataTable dt = this.QueryOnTableWithParams("items", "brd_code", "item_code='" + itemcode + "'", "");

                if (dt.Rows.Count > 0)
                    val = dt.Rows[0][0].ToString();
            }
            catch (Exception) { }

            return val;
        }

        public String get_dealer_code(String vin)
        {
            String dcode = "";

            return dcode;
        }

        public String get_brand(String itemcode)
        {
            String val = "";

            try
            {
                DataTable dt = this.QueryOnTableWithParams("items", "brd_code", "item_code='" + itemcode + "'", "");

                if (dt.Rows.Count > 0)
                    val = dt.Rows[0][0].ToString();
            }
            catch (Exception) { }

            return val;
        }

        public String get_cartype(String itemcode)
        {
            String val = "";

            try
            {
                DataTable dt = this.QueryOnTableWithParams("items", "cartype", "item_code='" + itemcode + "'", "");

                if(dt.Rows.Count > 0)
                    val = dt.Rows[0][0].ToString();
            }
            catch (Exception) { }

            return val;
        }

        public String get_year_model(String vin)
        {
            String val = "";

            return val;
        }

        public String get_engine(String vin)
        {
            String val = "";

            return val;
        }

        public String get_plateno(String vin)
        {
            String val = "";

            return val;
        }

        public String get_platenotype(String vin)
        {
            String val = "";

            return val;
        }

        public String get_vinThruPlateNo(String plateno, String plateType)
        {
            String val = "";

            return val;
        }

        public String get_date_release(String vin)
        {
            String val = "";

            return val;
        }

        public String get_warrantend()
        {
            String val = "";

            return val;
        }

        public String get_lastKMReading()
        {
            String val = "";

            return val;
        }

        public DataTable get_dealerlist()
        {
            return this.QueryAllOnTable("company");
        }

        public DataTable get_vehiclelist()
        {
            return this.QueryAllOnTable("vehicle_info");
        }
    }
}
