using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Npgsql;
using System.Text.RegularExpressions;
// Database

namespace Hotel_System
{
    public class thisDatabase
    {
        public static String driveloc = "\\\\RIGHTAPPS\\RightApps\\";//"\\\\RIGHTAPPS\\RightApps\\";
        public static String comp_folder = "Aquamarine";
        //public static String servers = System.IO.File.ReadAllText(driveloc + comp_folder + "\\Publish\\localDatabase.txt");
        public static String servers = "localhost";
        public static String lcl_db = "aquamarine";
        public static String svr_pass = "Rightech777"; 
        String schema = "rssys";//*/
        String chg_roomcharge = "RNT";

        NpgsqlConnection conn = new NpgsqlConnection("Server=" + servers + ";Port=5432;User Id=postgres;Password=" + svr_pass + ";Database=" + lcl_db + ";");

        public static String db_name
        {
            get { return lcl_db; }
            set { lcl_db = value; }
        }

        public void OpenConn()
        {
            try
            {
                conn.Open();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + " Unable to connect to the Server.");
            }
        }

        public void CloseConn()
        {
            try
            {
                conn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public String validate_login(String user, String pass)
        {
            try
            {
                String flag = null;

                this.OpenConn();

                String SQL = "Select uid FROM " + schema + ".X08 WHERE uid='" + user.ToUpper() + "' AND pwd='" + pass + "'";

                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                NpgsqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                    flag = dr[0].ToString();

                this.CloseConn();

                return flag;
            }
            catch (Exception)
            { }

            return null;
        }

        public Boolean InsertOnTable(String table, String column, String value)
        {
            Boolean flag = false;

            try
            {
                this.OpenConn();

                string SQL = "INSERT INTO " + this.schema + "." + table + "(" + column + ") VALUES (" + value + ")";
                //MessageBox.Show(SQL);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                this.CloseConn();

                flag = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            return flag;
        }

        public Boolean UpdateOnTable(String table, String col_upd, String cond)
        {
            Boolean flag = false;

            try
            {
                this.OpenConn();

                if (cond != "")
                {
                    cond = " WHERE " + cond + "";
                }

                string SQL = "UPDATE " + this.schema + "." + table + " SET " + col_upd + "" + cond + ";";
                //MessageBox.Show(SQL);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                this.CloseConn();

                flag = true;
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);

            }

            return flag;
        }

       public Boolean UpSertOnTable(String table, String col, String val, String col_upd, String cond)
        {
            Boolean flag = false;
            DataTable dt = new DataTable();

            try
            {
                if (String.IsNullOrEmpty(cond) == false)
                {
                    dt = QueryOnTableWithParams(table, "1", cond, "");

                    if (dt.Rows.Count > 0)
                    {
                        flag = UpdateOnTable(table, col_upd, cond);
                    }
                }
                                
                if(flag == false)
                {
                    flag = InsertOnTable(table, col, val);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            return flag;
        }

        public Boolean DeleteOnTable(String table, String cond)
        {
            Boolean flag = false;
            String WHERE = "";
            try
            {
                this.OpenConn();

                if (String.IsNullOrEmpty(cond) == false)
                {
                    WHERE = " WHERE " + cond;
                }

                string SQL = "DELETE FROM " + this.schema + "." + table + WHERE + ";";
                //MessageBox.Show(SQL);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                this.CloseConn();

                flag = true;
            }
            catch (Exception)
            { }

            return flag;
        }

        public DataTable QueryAllOnTable(string table)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                this.OpenConn();

                string SQL = "SELECT * FROM " + this.schema + "." + table + ";";
                //MessageBox.Show(SQL);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, conn);

                ds.Reset();

                da.Fill(ds);

                this.CloseConn();

                return ds.Tables[0];
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public DataTable QueryOnTableWithParams(string table, String param, String cond, String addcode)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                this.OpenConn();

                if (cond == null || cond == "")
                {
                    cond = "";
                }
                else
                {
                    cond = " WHERE " + cond;
                }

                string SQL = "SELECT " + param + " FROM " + this.schema + "." + table + "" + cond + " " + addcode;
                //MessageBox.Show(SQL);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, conn);

                ds.Reset();

                da.Fill(ds);

                this.CloseConn();

                return ds.Tables[0];
            }
            catch (Exception er)
            {
                MessageBox.Show(table + " : " + er.Message);
                return null;
            }
        }

        public DataTable QueryBySQLCode(String SQL)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                this.OpenConn();

                //MessageBox.Show(SQL);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, conn);
                ds.Reset();

                da.Fill(ds);

                this.CloseConn();

                return ds.Tables[0];
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public Boolean QueryBySQLCode_bool(String SQL)
        {
            Boolean flag = false;
            try
            {
                conn.Close();
                try
                {
                    conn.Open();
                }
                catch { }

                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                try
                {
                    conn.Close();
                }
                catch { }

                flag = true;
            }
            catch (Exception er)
            {

            }

            return flag;
        }
        public String QueryBySQLCodeRetStr(String SQL)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                DataTable dt_cur = QueryBySQLCode(SQL);

                return dt_cur.Rows[0][0].ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return "0.00";
            }
        }

        public String get_user_fullname(String username)
        {
            String val = "";

            DataTable dt = this.QueryOnTableWithParams("x08", "opr_name", "uid='" + username + "'", "");

            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                //tdate = Convert.ToDateTime(dt.Rows[i]["trnx_date"]).ToString("MMM dd, yyyy");
                val = dt.Rows[i]["opr_name"].ToString();
            }

            return val;
        }

        public String get_m99comp_name()
        {
            String val = "";

            DataTable dt = this.QueryOnTableWithParams("m99", "comp_name", "", "");

            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                //tdate = Convert.ToDateTime(dt.Rows[i]["trnx_date"]).ToString("MMM dd, yyyy");
                val = dt.Rows[i]["comp_name"].ToString();
            }

            return val;
        }

        public String get_m99comp_addr()
        {
            String val = "";

            DataTable dt = this.QueryOnTableWithParams("m99", "comp_addr", "", "");

            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                //tdate = Convert.ToDateTime(dt.Rows[i]["trnx_date"]).ToString("MMM dd, yyyy");
                val = dt.Rows[i]["comp_addr"].ToString();
            }

            return val;
        }

        public String get_systemdate(String format)
        {
            String tdate = "";
            DateTime ldt = DateTime.Now;
            DataTable dt = this.QueryOnTableWithParams("m99", "trnx_date", "", "");

            try
            {
                if (!String.IsNullOrEmpty(dt.Rows[0]["trnx_date"].ToString()))
                {
                    ldt = Convert.ToDateTime(dt.Rows[0]["trnx_date"].ToString());
                }
                if (String.IsNullOrEmpty(format))
                {
                    format = "yyyy-MM-dd";
                }

            }
            catch (Exception) { }

            //get localmachine date
            tdate = DateTime.Now.ToString(format);

            tdate = ldt.ToString(format);

            return tdate;
        }

        public String get_system_loc()
        {
            String system_loc = "";
            DateTime ldt;
            DataTable dt = this.QueryOnTableWithParams("m99", "system_loc", "", "");

            try
            {
                system_loc = dt.Rows[0]["system_loc"].ToString();
            }
            catch (Exception) { }

            return system_loc;
        }

        //get pk value that used for the table. input: m99col = column of m99
        public String get_pk(String m99col)
        {
            String pk = "";

            DataTable dt = this.QueryOnTableWithParams("m99", m99col, "", "");

            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                pk = dt.Rows[i][m99col].ToString();
            }

            return pk;
        }

        public Boolean set_all_pk(String table, String pkcol, String pk_val, String cond, int limit)
        {
            String newpk = get_nextincrementlimitchar(pk_val, limit);

            return this.UpdateOnTable(table, pkcol + "='" + newpk + "'", cond);
        }

        //set new pk to m99.. the next increment value for the next input
        public Boolean set_pkincrement(String m99col, String val)
        {
            string newvalue = string.Empty;
            int len = val.Length;
            string split = val.Substring(2, len - 2);
            int num = Convert.ToInt32(split);

            num++;
            newvalue = val.Substring(0, 2) + num.ToString("0000");

            return this.UpdateOnTable("m99", m99col + "='" + newvalue + "'", "");
        }

        public Boolean set_pkm99(String m99col, String val)
        {
            return this.UpdateOnTable("m99", m99col + "='" + val + "'", "");
        }

        // get next increment value with 6 characters.. 
        public String get_nextincrement(String val)
        {
            Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
            string newvalue = string.Empty;

            int len = val.Length;
            //string split = val.Substring(2, len - 2);
            int num = Convert.ToInt32(gm.getNumber(val));
            num++;
            //newvalue = val.Substring(0, 2) + num.ToString("0000");

            newvalue = num.ToString("000000");

            return newvalue;
        }

        //get next increment value with limit of characters.. min: 3 chars. max: 8chars
        public String get_nextincrementlimitchar(String val, int limit)
        {
            Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
            string newvalue = string.Empty;

            int len = val.Length;
            //string split = jid.Substring(2, len - 2);
            int num = Convert.ToInt32(gm.getNumber(val));
            num++;
            //newvalue = jid.Substring(0, 2) + num.ToString("0000");

            if (limit == 3)
                newvalue = num.ToString("000");
            else if (limit == 4)
                newvalue = num.ToString("0000");
            else if (limit == 5)
                newvalue = num.ToString("00000");
            else if (limit == 6)
                newvalue = num.ToString("000000");
            else if (limit == 7)
                newvalue = num.ToString("0000000");
            else if (limit == 8)
                newvalue = num.ToString("00000000");

            return newvalue;
        }

        //get the last value of key in a table.
        public String get_colval(String table, String col, String cond)
        {
            String pk = "";

            try
            {
                DataTable dt = this.QueryOnTableWithParams(table, col, cond, "ORDER BY " + col + " ASC");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {
                        pk = dt.Rows[i][col].ToString();
                    }
                }
                else
                {
                    pk = "000000";
                }
            }
            catch (Exception er) { MessageBox.Show("T:" + table + "  |Col: "+col + "   |Cond: "+cond+ "\n" + er.Message); }
            

            return pk;
        }

        public int get_cntrow(String table, String col, String cond)
        {
            DataTable dt = this.QueryOnTableWithParams(table, col, cond, "ORDER BY " + col + " ASC");

            return dt.Rows.Count;
        }

        public String get_schema()
        {
            return this.schema;
        }

        public DataTable get_allroomExpZ(String typ_code, String cur_rom_stat_Only, String cur_rom_stat_Except)
        {
            String WHERE = "";

            if (String.IsNullOrEmpty(typ_code) == false)
            {
                WHERE = WHERE + " AND r.typ_code = '" + typ_code + "'";
            }
            if (String.IsNullOrEmpty(cur_rom_stat_Only) == false)
            {
                WHERE = WHERE + " AND r.stat_code = '" + cur_rom_stat_Only + "'";
            }
            if (String.IsNullOrEmpty(cur_rom_stat_Except) == false)
            {
                WHERE = WHERE + " AND r.stat_code != '" + cur_rom_stat_Except + "'";
            }

            String SQL_allrooms = "SELECT r.rom_code, r.rom_desc, rt.typ_desc, r.stat_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".rtype rt ON rt.typ_code=r.typ_code  WHERE r.typ_code != 'Z'" + WHERE + "  ORDER BY r.rom_code ASC"; //AND  COALESCE(r.stat_code,'') in ('VC','')

            return QueryBySQLCode(SQL_allrooms);
        }

        public DataTable get_allroomWithZ(String typ_code, String cur_rom_stat_Only, String cur_rom_stat_Except)
        {
            String WHERE = "";

            if (String.IsNullOrEmpty(typ_code) == false)
            {
                WHERE = " WHERE r.typ_code = '" + typ_code + "'";
            }
            if (String.IsNullOrEmpty(cur_rom_stat_Only) == false)
            {
                if (WHERE == "")
                {
                    WHERE = " WHERE";
                }
                else
                {
                    WHERE = WHERE + " AND";
                }

                WHERE = WHERE + " r.stat_code = '" + cur_rom_stat_Only + "'";
            }
            if (String.IsNullOrEmpty(cur_rom_stat_Except) == false)
            {
                if (WHERE == "")
                {
                    WHERE = " WHERE";
                }
                else
                {
                    WHERE = WHERE + " AND";
                }

                WHERE = WHERE + " r.stat_code != '" + cur_rom_stat_Except + "'";
            }

            String SQL_allrooms = "SELECT r.rom_code, r.rom_desc, rt.typ_desc, r.stat_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".rtype rt ON rt.typ_code=r.typ_code " + WHERE + " ORDER BY r.rom_code ASC";

            return QueryBySQLCode(SQL_allrooms);
        }

        public DataTable get_roomtypExptZ()
        {
            //return this.QueryOnTableWithParams("rtype", "typ_code, typ_desc", "typ_code!='Z' AND typ_code!='DBL AND typ_code!='SGL''", "ORDER BY typ_code ASC;");
            //return this.QueryBySQLCode("SELECT DISTINCT rtyp.typ_code, rtyp.typ_desc, rr.double FROM " + schema + ".romrate rr INNER JOIN " + schema + ".rtype rtyp ON rr.typ_code=rtyp.typ_code WHERE rr.rate_code='004' ORDER BY rr.double ASC");

            return this.QueryBySQLCode("SELECT DISTINCT rtyp.typ_code, rtyp.typ_desc FROM " + schema + ".rtype rtyp");
        }

        public DataTable get_occupancyExptZ(String col, String str_dtin, String str_dtout, String typ_code)
        {
            String WHERE = "";

            if (!String.IsNullOrEmpty(typ_code) && typ_code != ".")
            {
                WHERE = " AND typ_code='" + typ_code + "'";
            }
            // Start Add By: Reancy 05/04/18 conflict on the query (not displaying required data)
            // String SQL_occ = "SELECT DISTINCT " + col + " FROM " + schema + ".gfolio WHERE typ_code != 'Z' AND (cancel IS NULL OR cancel='') AND (arr_date >= '" + str_dtin + "' OR dep_date >= '" + str_dtin + "') AND (arr_date <= '" + str_dtout + "' OR dep_date <= '" + str_dtout + "')  AND arr_date<>'" + str_dtout + "' AND dep_date <> '" + str_dtin + "'    " + WHERE;

            String SQL_occ = "SELECT DISTINCT " + col + " FROM " + schema + ".gfolio WHERE typ_code != 'Z' AND (cancel IS NULL OR cancel='') AND ((('" + str_dtin + "' BETWEEN arr_date AND dep_date) OR ('" + str_dtout + "' BETWEEN arr_date AND dep_date)) OR ((arr_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "') OR (dep_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "')))" + WHERE;
            //MessageBox.Show("Get Occupancy : " + SQL_occ);
            return this.QueryBySQLCode(SQL_occ);
        }

        public DataTable get_statCodeRoom(String stat_code)
        {
            String SQL_occ = "SELECT DISTINCT rom_code FROM " + schema + ".rooms WHERE stat_code = '" + stat_code + "'";
            //MessageBox.Show("Get Occupancy : " + SQL_occ);
            return this.QueryBySQLCode(SQL_occ);
        }

        public DataTable get_occupancyExptZandgfolioRoom(String col, String str_dtin, String str_dtout, String typ_code, String rom_code)
        {
            String WHERE = "";

            if (!String.IsNullOrEmpty(typ_code) && typ_code != ".")
            {
                WHERE = " AND typ_code='" + typ_code + "'";
            }
            
             String SQL_occ = "SELECT DISTINCT " + col + " FROM " + schema + ".gfolio WHERE typ_code != 'Z' AND rom_code != '" + rom_code + "' AND (cancel IS NULL OR cancel='') AND (arr_date >= '" + str_dtin + "' OR dep_date >= '" + str_dtin + "') AND (arr_date <= '" + str_dtout + "' OR dep_date <= '" + str_dtout + "')" + WHERE;
            //MessageBox.Show("Get Occupancy : " + SQL_occ);
            return this.QueryBySQLCode(SQL_occ);
        }

        public DataTable get_reservedroomExptZ(String str_dtin, String str_dtout, String typ_code)
        {
            String WHERE = "";

            if (!String.IsNullOrEmpty(typ_code) && typ_code != ".")
            {
                // modified by: Reancy 05 28 2018
                WHERE = " AND rf.typ_code='" + typ_code + "'";
            }

            //String SQL_reserved = "SELECT rom_code, res_code, full_name, arr_date, dep_date FROM " + schema + ".resfil WHERE arr_date >= '" + str_dtin + "' AND dep_date >= '" + str_dtin + "' ORDER BY rom_code ASC, arr_date ASC, dep_date ASC, full_name ASC"; //"SELECT DISTINCT r.rom_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".resfil rf ON r.rom_code=rf.rom_code WHERE (rf.arr_date >= '" + str_dtin + "' OR rf.dep_date >= '" + str_dtin + "') AND (rf.arr_date <= '" + str_dtout + "' OR rf.dep_date <= '" + str_dtout + "')"; 
            //rf.rom_code > '200' AND

            //updated by: Reancy 05 29 2018
            String SQL_reserved = "SELECT DISTINCT rf.rom_code, rf.res_code, rf.full_name, rf.arr_date, rf.dep_date, rf.arrived FROM " + schema + ".rooms r INNER JOIN " + schema + ".resfil rf ON r.rom_code=rf.rom_code WHERE  rf.typ_code!='Z' AND (rf.cancel IS NULL OR rf.cancel='') AND ((('" + str_dtin + "' BETWEEN arr_date AND dep_date) OR ('" + str_dtout + "' BETWEEN arr_date AND dep_date)) OR ((arr_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "') OR (dep_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "')))" + WHERE;
            //String SQL_reserved = "SELECT DISTINCT rf.rom_code, rf.res_code, rf.full_name, rf.arr_date, rf.dep_date FROM "+schema+".rooms r INNER JOIN "+schema+".resfil rf ON r.rom_code=rf.rom_code WHERE rf.rom_code > '200' AND rf.typ_code!='Z' AND rf.cancel IS NULL AND rf.arr_date >= '" + str_dtin + "' AND rf.dep_date <= '" + str_dtout + "'";

            return this.QueryBySQLCode(SQL_reserved);
        }

        //added by: Reancy 05-08-18
        public Boolean is_roomreservedByReancy(String rom_code, String date_set)
        {

            //String SQL_reserved = "SELECT rom_code, res_code, full_name, arr_date, dep_date FROM " + schema + ".resfil WHERE arr_date >= '" + str_dtin + "' AND dep_date >= '" + str_dtin + "' ORDER BY rom_code ASC, arr_date ASC, dep_date ASC, full_name ASC"; //"SELECT DISTINCT r.rom_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".resfil rf ON r.rom_code=rf.rom_code WHERE (rf.arr_date >= '" + str_dtin + "' OR rf.dep_date >= '" + str_dtin + "') AND (rf.arr_date <= '" + str_dtout + "' OR rf.dep_date <= '" + str_dtout + "')";

            //changed query by: Reancy 05-08-2018
            //String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND rf.arr_date= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND (rf.arrived IS NULL OR rf.arrived != 'Y')";
            String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND ('" + date_set + "' BETWEEN rf.arr_date AND rf.dep_date)";
            //String SQL_reserved = "SELECT DISTINCT rf.rom_code, rf.res_code, rf.full_name, rf.arr_date, rf.dep_date FROM "+schema+".rooms r INNER JOIN "+schema+".resfil rf ON r.rom_code=rf.rom_code WHERE rf.rom_code > '200' AND rf.typ_code!='Z' AND rf.cancel IS NULL AND rf.arr_date >= '" + str_dtin + "' AND rf.dep_date <= '" + str_dtout + "'";


            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode(SQL_reserved);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        public Boolean is_roomreserved(String rom_code)
        {

            //String SQL_reserved = "SELECT rom_code, res_code, full_name, arr_date, dep_date FROM " + schema + ".resfil WHERE arr_date >= '" + str_dtin + "' AND dep_date >= '" + str_dtin + "' ORDER BY rom_code ASC, arr_date ASC, dep_date ASC, full_name ASC"; //"SELECT DISTINCT r.rom_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".resfil rf ON r.rom_code=rf.rom_code WHERE (rf.arr_date >= '" + str_dtin + "' OR rf.dep_date >= '" + str_dtin + "') AND (rf.arr_date <= '" + str_dtout + "' OR rf.dep_date <= '" + str_dtout + "')";

            //changed query by: Reancy 05-08-2018
            //String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND rf.arr_date= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND (rf.arrived IS NULL OR rf.arrived != 'Y')";
            String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND ('" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN rf.arr_date AND rf.dep_date)";
            //String SQL_reserved = "SELECT DISTINCT rf.rom_code, rf.res_code, rf.full_name, rf.arr_date, rf.dep_date FROM "+schema+".rooms r INNER JOIN "+schema+".resfil rf ON r.rom_code=rf.rom_code WHERE rf.rom_code > '200' AND rf.typ_code!='Z' AND rf.cancel IS NULL AND rf.arr_date >= '" + str_dtin + "' AND rf.dep_date <= '" + str_dtout + "'";
            

            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode(SQL_reserved);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        // Start Add By: Roldan 04/20/18 check if date reserved

        public Boolean is_arr_roomreserved(String rom_code, String arr_date)
        {
            String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND '" + arr_date + "' BETWEEN rf.arr_date AND rf.dep_date";

            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode(SQL_reserved);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public Boolean is_dep_roomreserved(String rom_code, String dep_date)
        {
            String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND '" + dep_date + "' BETWEEN rf.arr_date AND rf.dep_date";

            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode(SQL_reserved);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        // End Add By: Roldan 04/20/18 check if date reserved

        public Boolean is_roomreservedBythisRescode(String rom_code, String res_code, String str_dtin, String str_dtout)
        {
            //String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND rf.res_code='" + res_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND rf.arr_date= '" + this.get_systemdate("") + "' AND (rf.arrived IS NULL OR rf.arrived != 'Y')";
            
            // Start Modify by: Roldan 03/19/18

            //changed by: Reancy 06-18-2018

            String SQL_reserved = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.rom_code = '" + rom_code + "' AND (rf.cancel IS NULL OR rf.cancel='') AND (rf.arrived IS NULL OR rf.arrived != 'Y') AND ((('" + str_dtin + "' BETWEEN arr_date AND dep_date) OR ('" + str_dtout + "' BETWEEN arr_date AND dep_date)) OR ((arr_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "') OR (dep_date BETWEEN '" + str_dtin + "' AND '" + str_dtout + "')))";
            String SQL_byRes = "SELECT rf.rom_code FROM " + schema + ".resfil rf WHERE rf.res_code = '" + res_code + "'";

            // End Modify by: Roldan 03/19/18

            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();

            dt = this.QueryBySQLCode(SQL_reserved);
            dtt = this.QueryBySQLCode(SQL_byRes);

            if (dt.Rows.Count > 0)
            {
                if (dtt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public Double get_roomrateamt(String rttyp_cod, String rmtyp, int occtyp)
        {
            DataTable dt = new DataTable();
            String col_occ = "single"; //default

            try
            {
                if (occtyp == 2)
                {
                    col_occ = "double";
                }
                else if (occtyp == 3)
                {
                    col_occ = "triple";
                }
                else if (occtyp == 4)
                {
                    col_occ = "quad";
                }

                dt = this.QueryOnTableWithParams("romrate", col_occ, "rate_code='" + rttyp_cod + "' AND typ_code='" + rmtyp + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            return 0.00;
        }

        public String get_ratetype_desc(String rtcode)
        {
            try
            {
                DataTable dt = this.QueryOnTableWithParams("ratetype", "rate_desc", "rate_code='" + rtcode + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }

            return "";
        }

        public String get_romratetype_code(String rtcode)
        {
            try
            {
                DataTable dt = this.QueryOnTableWithParams("romratetyp", "code", "code='" + rtcode + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }

            return null;
        }

        public String get_romtyp_code(String rm_desc)
        {
            try
            {
                DataTable dt = this.QueryOnTableWithParams("rtype", "typ_code", "typ_desc='" + rm_desc + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }

            return "";
        }

        public String get_romtyp_desc(String rm_code)
        {
            try
            {
                DataTable dt = this.QueryOnTableWithParams("rtype", "typ_desc", "typ_code='" + rm_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }

            return "";
        }

        public String get_romdesc(String rm_code)
        {
            try
            {
                DataTable dt = this.QueryOnTableWithParams("rooms", "rom_desc", "rom_code='" + rm_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }

            return "";
        }

        public Double get_m99tax_pct()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "gt_pct", "", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_m99sc_pct()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "sc_pct", "", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_guest_rmnetrate(String reg_num)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("gfolio", "rom_rate", "reg_num='" + reg_num + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_guest_rmtax(String reg_num)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("gfolio", "govt_tax", "reg_num='" + reg_num + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_guest_rmsc(String reg_num)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("gfolio", "serv_chg", "reg_num='" + reg_num + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_netrate(Double grossrate, Double disc_pct, Double disc_amt)
        {
            Double rate = 0.00, divisor = 0.00;

            rate = grossrate - (grossrate * (disc_pct / 100));

            rate = rate - disc_amt;

            if (has_sc(chg_roomcharge))
            {
                divisor += get_m99sc_pct() / 100;
            }
            if (has_vat(chg_roomcharge))
            {
                divisor += get_m99tax_pct() / 100;
            }

            divisor += 1;

            return (rate / divisor);
        }

        public Double get_tax(Double grossrate, Double disc_pct, Double disc_amt)
        {
            return (this.get_netrate(grossrate, disc_pct, disc_amt) * (get_m99tax_pct() / 100));
        }

        public Double get_svccharge(Double grossrate, Double disc_pct, Double disc_amt)
        {
            return (this.get_netrate(grossrate, disc_pct, disc_amt) * (get_m99sc_pct() / 100));
        }

        public Double get_discount(String disc_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("disctbl", "disc1", "disc_code='" + disc_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0]);
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Boolean issenior_disc(String disc_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("disctbl", "sen_disc", "disc_code='" + disc_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "Y")
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }

        public String get_disc_desc(String disc_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("disctbl", "disc_desc", "disc_code='" + disc_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public String get_chg_code_tax()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "govt_chg", "", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public String get_chg_code_tax_senior()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "govt_chg2", "", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public String get_chg_code_sc()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "sc_chg", "", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public String get_chg_code_sc_senior()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "sc_chg2", "", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public String get_addrcompmarket(String acct_no)
        {
            DataTable dt = new DataTable();
            String val = "";

            try
            {
                dt = this.QueryBySQLCode("SELECT g.address1, c.comp_name, g.mobile, g.tel_num, g.fax_num FROM " + schema + ".guest g LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE g.acct_no='" + acct_no + "'");

                foreach (DataRow row in dt.Rows)
                {
                    val = row["address1"].ToString() + " | " + row["comp_name"].ToString() + " | " + row["mobile"].ToString() + ", " + row["tel_num"].ToString() + ", " + row["fax_num"].ToString();
                }

            }
            catch (Exception) { }

            return val;
        }

        public Boolean is_roomcharge(String chg_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "rom_chg", "rom_chg='" + chg_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception) { }

            return false;
        }

        public Boolean is_roomcharge_senior(String chg_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("m99", "rom_chg2", "rom_chg2='" + chg_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception) { }

            return false;
        }

        public Boolean is_chg_deposit(String chg_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("charge", "1", "chg_code='" + chg_code + "' AND isdeposit='TRUE'", "");

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception) { }

            return false;
        }

        public Boolean has_brkfast(String rt_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("ratetype", "bfast", "rate_code='" + rt_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "Y")
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }

        public String get_roomchargetype(String chg_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("charge", "chg_type", "chg_code='" + chg_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        //Collection Status Report
        public DataTable get_collectionreport()
        {

            /*return QueryBySQLCode("SELECT sl.chg_date, (SELECT ROUND(SUM(sl1.amount)) FROM " + schema + ".soahdr s1 LEFT JOIN " + schema + ".soalne sl1 ON s1.soa_code=sl1.soa_code LEFT JOIN " + schema + ".tr02 t2_1 ON sl1.soa_code=t2_1.invoice WHERE sl1.chg_code != 'RNT' AND sl1.chg_date = sl.chg_date )AS \"Utilities\", (SELECT ROUND(SUM(sl1.amount)) FROM " + schema + ".soahdr s1 LEFT JOIN " + schema + ".soalne sl1 ON s1.soa_code=sl1.soa_code LEFT JOIN " + schema + ".tr02 t2_1 ON sl1.soa_code=t2_1.invoice WHERE sl1.chg_code = 'RNT' AND sl1.chg_date = sl.chg_date )As \"Rent\", ROUND(SUM(sl.amount)) as \"Total\", ROUND(SUM(t2.credit)) AS \"Collection\", ROUND((SUM(t2.credit)/sum(amount)) * 100) AS \"Collectionpct\", ROUND(SUM(amount)-sum(t2.credit)) AS \"Uncollected\", ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) AS \"Uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice GROUP BY sl.chg_date ORDER BY sl.chg_date");*/

            /*return QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice GROUP BY month ORDER BY month");*/

            return QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) = '2018' GROUP BY month ORDER BY month");
        }

        public DataTable get_collectionreport_utilities(int m)
        {
            DataTable dt_utilities = new DataTable();

            String val = "";

            dt_utilities = QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + "GROUP BY month ORDER BY month");

            String a = dt_utilities.Rows[0]["total"].ToString();

            foreach (DataRow row in dt_utilities.Rows)
            {
                val = row["total"].ToString();
            }


            //return dt_utilities.Rows[0]["total"].ToString();//val;

            return QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + "GROUP BY month ORDER BY month");

        }

        //Occupancy Rate
        public DataTable get_occupancy()
        {
            //return QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", ROUND(SUM(g.rom_rate)) as \"Total\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code GROUP BY month ORDER BY month");

            //return QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", extract(year from c.chg_date) as \"year\", ROUND(SUM(g.rom_rate)) as \"total\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code GROUP BY month, year ORDER BY month, year");

            return QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", extract(year from c.chg_date) as \"year\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) GROUP BY month, year, series1 ORDER BY month, year");
        }

        //Market by: Roldan 04/04/18
        public DataTable get_market()
        {
            /*
            return QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", to_char(gf.arr_date, 'MM/dd/yyyy') as \"date\", gf.rom_rate as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy') >= '01/01/18' AND to_char(gf.arr_date, 'MM/dd/yyyy') <= '01/31/18' ORDER BY gf.arr_date");
            */
            /*
            return QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", to_char(gf.arr_date, 'MM/dd/yyyy') as \"date\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')='08/01/2017' GROUP BY marketcode, market, gf.arr_date ORDER BY gf.arr_date");* */

            return QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", to_char(gf.arr_date, 'MM/dd/yyyy') as \"date\", gf.rom_rate as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')='08/01/2017' ORDER BY gf.arr_date");
        }



        //Guest Billing search room or guest name or guest folio
        public DataTable get_guest_currentlycheckin(String search)
        {
            search = ((search != "") ? " WHERE " + search + "" : "");
            return QueryBySQLCode("SELECT res_code, arr_date, full_name, acct_no, COALESCE(SPLIT_PART(rf.occ_type, ', ', 1), '0') AS adult, COALESCE(SPLIT_PART(rf.occ_type, ', ', 2), '0') AS kid, COALESCE(SPLIT_PART(rf.occ_type, ', ', 3), '0') AS inf, COALESCE(SPLIT_PART(rf.occ_type, ', ', 4), '0') AS ttlpax, hl.name, rom_code, trns, pck.package, pck1.activities, ent, (CASE WHEN cdr = TRUE THEN cdr ELSE FALSE END) AS cdr, prc.ttl AS price, rf.remarks, cp.cpttl AS com, trv_name, rf.seller, (wcd)::numeric(20,2) AS cpr, cp.remarks, r_date, cashier, (COALESCE(prc.ttl, 0.00) - (CASE WHEN (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) < 0 THEN ((COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00))*-1) ELSE (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) END))::numeric(20,2) AS net_income, user_id AS user_id1, (CASE WHEN (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) < 0 THEN ((COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00))*-1) ELSE (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) END)::numeric(20,2) AS comttl FROM rssys.gfolio rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS package FROM (SELECT DISTINCT res_code, reg_num, chg_code FROM rssys.chgfil) cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'PCK%' GROUP BY cf.reg_num, cf.res_code) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS activities FROM rssys.chgfil cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'ACT%' GROUP BY cf.reg_num, cf.res_code) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ LEFT JOIN (SELECT STRING_AGG(chg_desc, ', ') AS trns, res_code AS rg_code FROM rssys.chgfil INNER JOIN rssys.charge ON charge.chg_code = chgfil.chg_code WHERE UPPER(charge.chg_code) LIKE 'TRNS%' GROUP BY rg_code) trn ON trn.rg_code = rf.res_code LEFT JOIN (SELECT STRING_AGG(chg_desc, ', ') AS ent, res_code AS rg_code FROM rssys.chgfil INNER JOIN rssys.charge ON charge.chg_code = chgfil.chg_code WHERE UPPER(charge.chg_code) LIKE 'ADTL%' AND UPPER(charge.chg_desc) LIKE 'ENTRANCE%' GROUP BY rg_code) ent ON ent.rg_code = rf.res_code LEFT JOIN (SELECT TRUE AS cdr, res_code AS rg_code FROM rssys.chgfil INNER JOIN rssys.charge ON charge.chg_code = chgfil.chg_code WHERE UPPER(charge.chg_code) LIKE 'ADTL%' AND UPPER(charge.chg_desc) LIKE 'CAMERA%' GROUP BY rg_code) cdr ON cdr.rg_code = rf.res_code LEFT JOIN (SELECT trv_name, SUM(price * (c.com * 0.01)) AS wcd, SUM(cpttl) AS cpttl, ttl, c.com AS cpr, COALESCE(tr.com, 0.00) AS com, COALESCE(remarks, 'NOT RELEASED') AS remarks, c.res_code AS rg_code, r_date, cashier FROM (SELECT trv_name, seller, gf.trv_code, tr.com, res_code, ttl FROM rssys.gfolio gf LEFT JOIN (SELECT SUM(amount) AS ttl, res_code AS rg_code FROM rssys.chgfil WHERE amount >= 0 GROUP BY res_code) tl_t ON tl_t.rg_code = gf.res_code LEFT JOIN rssys.travagnt tr ON tr.trv_code = gf.trv_code) c LEFT JOIN rssys.com_p tr ON (tr.trv_code = c.trv_code AND tr.rg_code = c.res_code) LEFT JOIN (SELECT price, COALESCE(SPLIT_PART(reference, ' ', 1)::numeric(15,0), 0) * ((com * 0.01) * price)::numeric(25,2) AS cpttl, res_code AS rg_code, reg_num AS rg_num FROM (SELECT * FROM rssys.charge WHERE chg_type = 'C' AND UPPER(chg_code) NOT LIKE 'TRNS%' AND UPPER(chg_desc) NOT LIKE 'ENTRANCE%') charge INNER JOIN rssys.chgfil ON charge.chg_code = chgfil.chg_code) cfg ON cfg.rg_code = c.res_code GROUP BY trv_name, ttl, c.com, tr.com, remarks, c.res_code, r_date, cashier) cp ON cp.rg_code  = rf.res_code LEFT JOIN (SELECT SUM(amount) AS ttl, res_code AS rg_code FROM rssys.chgfil WHERE amount >= 0 GROUP BY res_code) prc ON prc.rg_code = rf.res_code" + search + " ORDER BY arr_date, res_code ASC");
        }

        public DataTable get_guest_histforview(String search)
        {
            search = ((search != "") ? " WHERE " + search + "" : "");
            return QueryBySQLCode("SELECT res_code, arr_date, full_name, acct_no, COALESCE(SPLIT_PART(rf.occ_type, ', ', 1), '0') AS adult, COALESCE(SPLIT_PART(rf.occ_type, ', ', 2), '0') AS kid, COALESCE(SPLIT_PART(rf.occ_type, ', ', 3), '0') AS inf, COALESCE(SPLIT_PART(rf.occ_type, ', ', 4), '0') AS ttlpax, hl.name, rom_code, trns, pck.package, pck1.activities, ent, (CASE WHEN cdr = TRUE THEN cdr ELSE FALSE END) AS cdr, prc.ttl AS price, rf.remarks, cp.cpttl AS com, trv_name, rf.seller, (wcd)::numeric(20,2) AS cpr, cp.remarks, r_date, cashier, (COALESCE(prc.ttl, 0.00) - (CASE WHEN (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) < 0 THEN ((COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00))*-1) ELSE (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) END))::numeric(20,2) AS net_income, user_id AS user_id1, (CASE WHEN (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) < 0 THEN ((COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00))*-1) ELSE (COALESCE(cp.cpttl, 0.00) - COALESCE(wcd, 0.00)) END)::numeric(20,2) AS comttl FROM rssys.gfhist rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS package FROM (SELECT DISTINCT res_code, reg_num, chg_code FROM rssys.chghist) cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'PCK%' GROUP BY cf.reg_num, cf.res_code) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS activities FROM rssys.chghist cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'ACT%' GROUP BY cf.reg_num, cf.res_code) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ LEFT JOIN (SELECT STRING_AGG(chg_desc, ', ') AS trns, res_code AS rg_code FROM rssys.chghist INNER JOIN rssys.charge ON charge.chg_code = chghist.chg_code WHERE UPPER(charge.chg_code) LIKE 'TRNS%' GROUP BY rg_code) trn ON trn.rg_code = rf.res_code LEFT JOIN (SELECT STRING_AGG(chg_desc, ', ') AS ent, res_code AS rg_code FROM rssys.chghist INNER JOIN rssys.charge ON charge.chg_code = chghist.chg_code WHERE UPPER(charge.chg_code) LIKE 'ADTL%' AND UPPER(charge.chg_desc) LIKE 'ENTRANCE%' GROUP BY rg_code) ent ON ent.rg_code = rf.res_code LEFT JOIN (SELECT TRUE AS cdr, res_code AS rg_code FROM rssys.chghist INNER JOIN rssys.charge ON charge.chg_code = chghist.chg_code WHERE UPPER(charge.chg_code) LIKE 'ADTL%' AND UPPER(charge.chg_desc) LIKE 'CAMERA%' GROUP BY rg_code) cdr ON cdr.rg_code = rf.res_code LEFT JOIN (SELECT trv_name, SUM(price * (c.com * 0.01)) AS wcd, SUM(cpttl) AS cpttl, ttl, c.com AS cpr, COALESCE(tr.com, 0.00) AS com, COALESCE(remarks, 'NOT RELEASED') AS remarks, c.res_code AS rg_code, r_date, cashier FROM (SELECT trv_name, seller, gf.trv_code, tr.com, res_code, ttl FROM rssys.gfhist gf LEFT JOIN (SELECT SUM(amount) AS ttl, res_code AS rg_code FROM rssys.chghist WHERE amount >= 0 GROUP BY res_code) tl_t ON tl_t.rg_code = gf.res_code LEFT JOIN rssys.travagnt tr ON tr.trv_code = gf.trv_code) c LEFT JOIN rssys.com_p tr ON (tr.trv_code = c.trv_code AND tr.rg_code = c.res_code) LEFT JOIN (SELECT price, COALESCE(SPLIT_PART(reference, ' ', 1)::numeric(15,0), 0) * ((com * 0.01) * price)::numeric(25,2) AS cpttl, res_code AS rg_code, reg_num AS rg_num FROM (SELECT * FROM rssys.charge WHERE chg_type = 'C' AND UPPER(chg_code) NOT LIKE 'TRNS%' AND UPPER(chg_desc) NOT LIKE 'ENTRANCE%') charge INNER JOIN rssys.chghist ON charge.chg_code = chghist.chg_code) cfg ON cfg.rg_code = c.res_code GROUP BY trv_name, ttl, c.com, tr.com, remarks, c.res_code, r_date, cashier) cp ON cp.rg_code  = rf.res_code LEFT JOIN (SELECT SUM(amount) AS ttl, res_code AS rg_code FROM rssys.chghist WHERE amount >= 0 GROUP BY res_code) prc ON prc.rg_code = rf.res_code" + search + " ORDER BY arr_date, res_code ASC");
        }

        //Guest Billing search room or guest name or guest folio
        public DataTable get_guest_foliohist(String acct_no)
        {
            return QueryOnTableWithParams("gfhist", "reg_num AS \"Folio No\", full_name AS \"Full Name\", rom_code AS \"Room\", typ_code AS \"Type\", arr_date AS \"Arrival\", dep_date AS \"Departure\", remarks AS \"Remarks\", rom_rate + govt_tax + serv_chg AS \"Rate\", disc_pct AS \"Disc%\"", "acct_no='" + acct_no + "'", "ORDER BY reg_num ASC");
            //return QueryBySQLCode("SELECT gf.rom_code AS \"Room\", gf.typ_code AS \"Type\", gf.full_name AS \"Full Name\", gf.arr_date AS \"Arrival Date\", gf.dep_date AS \"Departure Date\", gf.reg_num AS \"Guest Folio\", gf.user_id AS User, gf.t_date AS \"Trans. Date\", gf.t_time AS \"Trans. Time\" FROM " + schema + ".gfhist gf LEFT JOIN " + schema + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE (gf.cancel IS NULL OR gf.cancel ='') AND gf.acct_no='" + acct_no + "' ORDER BY gf.rom_code ASC");
            //return this.QueryOnTableWithParams("gfolio", "rom_code AS \"Room\", typ_code AS \"Type\", full_name AS \"Full Name\", arr_date AS \"Arrival Date\", dep_date AS \"Departure Date\", reg_num AS \"Guest Folio\", user_id AS User, t_date AS \"Trans. Date\", t_time AS \"Trans. Time\"", "cancel IS NULL" + search, "ORDER BY rom_code ASC");
        }

        public DataTable get_guestfolio(String reg_num)
        {
            return this.QueryOnTableWithParams("gfolio", "rom_code, typ_code, full_name, arr_date, dep_date, reg_num, user_id, t_date, t_time", "cancel IS NULL AND reg_num='" + reg_num + "' AND rom_code > '200'", "ORDER BY rom_code ASC");
        }

        public DataTable get_guestfolio_all(String reg_num)
        {
            return this.QueryOnTableWithParams("gfolio", "*", "reg_num='" + reg_num + "'", "ORDER BY rom_code ASC");
        }

        public DataTable get_guestfolio_all_withaddr(String reg_num)
        {
            return QueryBySQLCode("SELECT gf.*, g.address1 FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".guest g ON gf.acct_no=g.acct_no WHERE (gf.cancel IS NULL OR gf.cancel='') AND gf.reg_num='" + reg_num + "'");
            //return this.QueryOnTableWithParams("gfolio", "*", "cancel IS NULL AND reg_num='" + reg_num + "'", "ORDER BY rom_code ASC");
        }

        public DataTable get_guest_info(String lcl_acctno)
        {
            return QueryOnTableWithParams("guest", "acct_no, full_name, gender, address1, tel_num, email, cntry_code, title, last_name, first_name, mid_name, birth_date, comp_code, passport_no, passport_issued, passport_expiry, passport_place, mp_code, escaper,  nat_code, cntry_code, g_typ", "acct_no='" + lcl_acctno + "'", "");
        }

        public DataTable get_guest_curchkin_selected(String reg_num)
        {
            return QueryBySQLCode("SELECT gf.rom_code, gf.typ_code, gf.full_name, gf.rmrttyp, gf.occ_type, gf.rate_code, gf.rom_rate, gf.govt_tax, gf.serv_chg, gf.arr_date, gf.dep_date, gf.reg_num, gf.user_id, gf.t_date, gf.t_time, gf.remarks, gf.bill_info, gf.rm_features, gf.disc_code, gf.disc_pct, g.address1 AS address, c.comp_name AS company FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE reg_num='" + reg_num + "'");
            //return this.QueryOnTableWithParams("gfolio", "rom_code, typ_code, full_name, occ_type, rate_code, rom_rate, govt_tax, serv_chg, arr_date, dep_date, reg_num, user_id, t_date, t_time, remarks, bill_info, rm_features, disc_code, disc_pct", "cancel IS NULL AND reg_num='"+reg_num+"'", "");
        }

        public DataTable get_guest_chargefil(String reg_num, Boolean isExceptDeposit)
        {
            String WHERE = "";

            if (isExceptDeposit)
            {
                WHERE = " AND c.isdeposit='FALSE'";
            }

            return this.QueryBySQLCode("SELECT cf.chg_date, cf.chg_code, c.chg_desc, cf.doc_type, CASE WHEN c.chg_class in ('CCARD') THEN cf.reference ||' '|| cf.ccrd_no ||'-'|| cf.trace_no ELSE cf.reference END AS \"reference\", cf.amount AS \"amount\", cf.user_id, cf.t_time, cf.reference AS reference1, cf.ccrd_no, cf.trace_no, c.chg_type AS \"Type\", cf.chg_num  AS \"Chg Num\", cf.soa_code  AS \"SOA Code\", m4.at_code, m4.at_desc  FROM " + schema + ".chgfil cf INNER JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code LEFT JOIN rssys.m04 m4 ON (c.at_code=m4.at_code) WHERE reg_num='" + reg_num + "'" + WHERE + " ORDER BY cf.chg_date desc, c.postcharge, c.utility, cf.t_time desc, soa_code desc");
        }

        //Guest History
        public DataTable get_inhouseguest_ondate(String dt)
        {
            String WHERE = " WHERE gf.arr_date<='" + dt + "' AND gf.co_date>='" + dt + "'";

            return this.QueryBySQLCode("SELECT gf.acct_no AS \"Guest No\", gf.full_name AS \"Guest\", gf.arr_date ||' '|| gf.arr_time AS \"Arrival\", gf.dep_date ||' '|| gf.dep_time AS \"Departure Date\", gf.rom_code AS \"Room\", gf.occ_type AS \"Occ.\", gf.user_id AS \"Check In By\", gf.t_date ||' '|| gf.t_time AS \"Check Date/Time\", gf.co_user AS \"Check Out By\", gf.co_date||' '|| gf.co_time AS \"Check Out Date/Time\" FROM " + schema + ". gfhist gf LEFT JOIN " + schema + ".guest g ON gf.acct_no=g.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code" + WHERE + " ORDER BY gf.arr_date");
        }

        public DataTable get_guest_hist_selected(String reg_num)
        {
            return QueryBySQLCode("SELECT gf.rom_code, gf.typ_code, gf.full_name, gf.occ_type, gf.rate_code, gf.rom_rate, gf.govt_tax, gf.serv_chg, gf.arr_date, gf.dep_date, gf.reg_num, gf.user_id, gf.t_date, gf.t_time, gf.remarks, gf.bill_info, gf.rm_features, gf.disc_code, gf.disc_pct, g.address1 AS address, c.comp_name AS company FROM " + schema + ".gfhist gf LEFT JOIN " + schema + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE reg_num='" + reg_num + "'");
            //return this.QueryOnTableWithParams("gfolio", "rom_code, typ_code, full_name, occ_type, rate_code, rom_rate, govt_tax, serv_chg, arr_date, dep_date, reg_num, user_id, t_date, t_time, remarks, bill_info, rm_features, disc_code, disc_pct", "cancel IS NULL AND reg_num='"+reg_num+"'", "");
        }

        public DataTable get_guest_chargehist(String reg_num)
        {
            return this.QueryBySQLCode("SELECT cf.chg_date AS \"Date\", cf.chg_code AS \"Code\", c.chg_desc  AS \"Description\", cf.doc_type AS \"Doc\", CASE WHEN c.chg_class in ('CCARD')  THEN cf.reference ||' '|| cf.ccrd_no ||'-'|| cf.trace_no ELSE cf.reference END AS \"Reference\", cf.amount AS \"Amount\", cf.user_id AS \"User ID\", cf.t_time AS \"Time\", cf.reference, cf.ccrd_no AS \"Card No\", cf.trace_no AS \"Trace No\", c.chg_type AS \"Type\", cf.chg_num  AS \"Chg Num\" FROM " + schema + ".chghist cf INNER JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE reg_num='" + reg_num + "' ORDER BY cf.t_date ASC, cf.t_time ASC");
        }

        public DataTable search_guesthist(String reg_num, String full_name, String company)
        {
            String WHERE = "";

            if (reg_num != "")
            {
                WHERE = " WHERE gf.reg_num ILIKE '%" + reg_num + "%'";
            }
            if (full_name != "")
            {
                if (WHERE == "")
                {
                    WHERE = " WHERE";
                }
                else
                {
                    WHERE = WHERE + " AND";
                }

                WHERE = WHERE + " gf.full_name ILIKE '%" + full_name + "%'";
            }
            if (company != "")
            {
                if (WHERE == "")
                {
                    WHERE = " WHERE";
                }
                else
                {
                    WHERE = WHERE + " AND";
                }

                WHERE = WHERE + " g.comp_code='" + company + "'";
            }

            return this.QueryBySQLCode("SELECT gf.acct_no AS \"Guest No\", gf.full_name AS \"Guest\", gf.arr_date AS \"Arrival\", gf.dep_date AS \"Departure Date\", gf.rom_code AS \"Room\", gf.occ_type AS \"Occ.\", gf.user_id AS \"Check In By\", gf.t_date ||' '|| gf.t_time AS \"Check Date/Time\", gf.co_user AS \"Check Out By\", gf.co_date||' '|| gf.co_time AS \"Check Out Date/Time\" FROM " + schema + ". gfhist gf LEFT JOIN " + schema + ".guest g ON gf.acct_no=g.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code" + WHERE + " ORDER BY gf.arr_date");
        }

        public Double get_guest_charges_total(String reg_num, Boolean isExceptDeposit)
        {
            Double amt = 0.00;
            DataTable dt = new DataTable();
            String WHERE = "";

            try
            {
                if (isExceptDeposit)
                {
                    WHERE = " AND c.isdeposit='FALSE'";
                }

                dt = this.QueryBySQLCode("SELECT SUM(cf.amount) FROM " + schema + ".chgfil cf INNER JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE reg_num='" + reg_num + "'" + WHERE);
                //("chgfil", "SUM(amount)", "reg_num='" + reg_num + "'", "");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        amt = Convert.ToDouble(row[0].ToString());
                    }
                }
            }
            catch (Exception) { }

            return amt;
        }

        public Double get_guest_charge_deposit(String reg_num)
        {
            Double amt = 0.00;
            DataTable dt = new DataTable();
            String WHERE = "";
            try
            {


                dt = this.QueryBySQLCode("SELECT SUM(cf.amount) FROM " + schema + ".chgfil cf INNER JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE reg_num='" + reg_num + "' AND c.isdeposit='TRUE'" + WHERE);
                //("chgfil", "SUM(amount)", "reg_num='" + reg_num + "'", "");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        amt = Convert.ToDouble(row[0].ToString());
                    }
                }
            }
            catch (Exception) { }

            return amt;
        }

        public Double get_guest_charges_typeC_total(String reg_num)
        {
            Double amt = 0.00;
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryBySQLCode("SELECT SUM(cf.amount) AS amt FROM " + this.schema + ".chgfil cf INNER JOIN " + this.schema + ".charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num='" + reg_num + "' AND c.chg_type='C'");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        amt = Convert.ToDouble(row[0].ToString());
                    }
                }
            }
            catch (Exception) { }

            return amt;
        }

        public Double get_guest_charges_typeP_total(String reg_num)
        {
            Double amt = 0.00;
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryBySQLCode("SELECT SUM(cf.amount) AS amt FROM " + this.schema + ".chgfil cf INNER JOIN " + this.schema + ".charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num='" + reg_num + "' AND c.chg_type='P'");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        amt = Convert.ToDouble(row[0].ToString());
                    }
                }
            }
            catch (Exception) { }

            return amt;
        }

        public Boolean isReservationFolio(String reg_num)
        {
            DataTable dt = new DataTable();

            dt = this.QueryOnTableWithParams("m99", "resv_fol", "resv_fol='" + reg_num + "'", "");

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public Boolean has_vat(String chg_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("charge", "vat_incl", "chg_code='" + chg_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }

                if (val == "Y")
                    return true;
            }

            return false;
        }

        public Boolean has_sc(String chg_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("charge", "sc_rep", "chg_code='" + chg_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }

                if (val == "Y")
                    return true;
            }

            return false;
        }

        public Boolean has_function(String chg_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("charge", "fcharge", "chg_code='" + chg_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }

                if (val == "Y")
                    return true;
            }

            return false;
        }

        public String get_chg_num_latest(String chg_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("charge", "chg_num", "chg_code='" + chg_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        //end of Guest Billing
        public String get_chg_type(String chg_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("charge", "chg_type", "chg_code='" + chg_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public DataTable get_chargedesc_bytype(String chg_type)
        {
            return QueryOnTableWithParams("charge", "chg_code, chg_desc", "chg_type='" + chg_type + "'", "ORDER BY chg_code ASC");
        }

        public DataTable get_resguest(String res_code)
        {
            return this.QueryOnTableWithParams("resguest", "*", "res_code='" + res_code + "'", "");
        }

        //reservation module
        public DataTable get_reservationlist(String srhcode)
        {
            return QueryBySQLCode("SELECT res_code, arr_date, full_name, pp.p_name, hl.name, arr_time, pck.package, COALESCE(SPLIT_PART(rf.occ_type, ', ', 1), '0') AS adult, COALESCE(SPLIT_PART(rf.occ_type, ', ', 2), '0') AS kid, COALESCE(SPLIT_PART(rf.occ_type, ', ', 3), '0') AS inf, pck1.activities, COALESCE(SPLIT_PART(rf.occ_type, ', ', 4), '0') AS ttlpax, res_date, reserv_by, remarks FROM rssys.resfil rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT DISTINCT string_agg(chg_desc, ', ') AS package, res_gfil.rg_code FROM (SELECT DISTINCT rg_code, chg_code FROM rssys.res_gfil) res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'PCK%') GROUP BY res_gfil.rg_code) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS activities, rg_code FROM rssys.res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ACT%') AND UPPER(occ_type) LIKE '%ALL' GROUP BY rg_code) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ" + srhcode + " ORDER BY arr_date, arr_time, hl.name ASC");
        }

        public DataTable get_reservationlistforBilling(String search_resno_or_guestname)
        {
            return QueryBySQLCode("SELECT r.res_code, r.full_name, r.rmrttyp, r.rom_code, r.typ_code, r.arr_date, r.dep_date, r.user_id, r.t_date, r.t_time FROM " + schema + ".resfil r INNER JOIN " + schema + ".guest g ON r.acct_no=g.acct_no WHERE (r.cancel IS NULL OR r.cancel='') AND r.arr_date > '" + this.get_systemdate("") + "' AND (r.full_name ILIKE '%" + search_resno_or_guestname + "%' OR r.res_code ILIKE '%" + search_resno_or_guestname + "%') ORDER BY r.res_code ASC");
        }

        public DataTable get_resguest_id(String res_code)
        {
            return QueryOnTableWithParams("resguest", "acct_no", "res_code='" + res_code + "'", "");
        }

        public Boolean cancel_reservation(String code, String reason, String user, String remark, String turnaway, Boolean isGroup)
        {
            Boolean flag = false;
            String WHERE = "res_code='" + code + "'";

            if (String.IsNullOrEmpty(code) == false)
            {
                if (isGroup)
                {
                    WHERE = "rgrp_code='" + code + "'";
                }
                //'"+ user+"', '"+this.get_systemdate()+"','"+ System.DateTime.Now.ToString("hh:mm")+"','"+remark+"'"
                flag = this.UpdateOnTable("resfil", "cancel='Y', reason='" + reason + "', turnaway='" + turnaway + "', canc_user='" + user + "', canc_date='" + this.get_systemdate("") + "', canc_time='" + System.DateTime.Now.ToString("HH:mm") + "', canc_remarks='" + remark + "'", WHERE);
            }

            return flag;
        }

        public DataTable get_res_info(String code)
        {
            //return QueryOnTableWithParams("resfil", "*", "res_code='" + code + "'", "");
            //res_code, full_name, hl.code, pck.package, pck1.activities, arr_time, pck.adult, pck.kid, pck.inf, pck1.ttlpax, res_date, reserv_by, arr_date, rom_code, mkt_code, remarks, acct_no, disc_code, discount, p_typ, trv_code
            return QueryBySQLCode("SELECT res_code, full_name, hl.code, pck.package, pck1.activities, arr_time, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 1)::numeric(15,0), 0)), 0) AS adult, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 2)::numeric(15,0), 0)), 0) AS kid, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 3)::numeric(15,0), 0)), 0) AS inf, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 4)::numeric(15,0), 0)), 0) AS ttlpax, res_date, reserv_by, arr_date, rom_code, mkt_code, remarks, acct_no, disc_code, discount, p_typ, trv_code, seller FROM rssys.resfil rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS package, COALESCE(adult.occ_type, '0 ADULT') AS adult, COALESCE(kid.occ_type, '0 KID') AS kid, COALESCE(inf.occ_type, '0 INFANT') AS inf, res_gfil.rg_code FROM rssys.res_gfil LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%ADULT') adult ON adult.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%KID') kid ON kid.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%INFANT') inf ON inf.rg_code = res_gfil.rg_code LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'PCK%') GROUP BY res_gfil.rg_code, adult.occ_type, kid.occ_type, inf.occ_type) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS activities, COALESCE((occ_type), '0 All') AS ttlpax, rg_code FROM rssys.res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ACT%') AND UPPER(res_gfil.occ_type) LIKE '%ALL' GROUP BY rg_code, occ_type) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ WHERE res_code = '" + code + "' GROUP BY res_code, full_name, hl.code, pck.package, pck1.activities, arr_time, res_date, reserv_by, arr_date, rom_code, mkt_code, remarks, acct_no, disc_code, discount, p_typ, trv_code, seller");
        }

        public DataTable get_gfolio_info(String code)
        {
            //return QueryOnTableWithParams("resfil", "*", "res_code='" + code + "'", "");
            //res_code, full_name, hl.code, pck.package, pck1.activities, arr_time, pck.adult, pck.kid, pck.inf, pck1.ttlpax, res_date, reserv_by, arr_date, rom_code, mkt_code, remarks, acct_no, disc_code, discount, p_typ, trv_code
            return QueryBySQLCode("SELECT reg_num, res_code, full_name, hl.code, pck.package, pck1.activities, arr_time, pck.adult, pck.kid, pck.inf, pck1.ttlpax, res_date, reserv_by, arr_date, rom_code, mkt_code, remarks, acct_no, disc_code, discount, p_typ, trv_code FROM rssys.gfolio rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS package, COALESCE(adult.occ_type, '0 ADULT') AS adult, COALESCE(kid.occ_type, '0 KID') AS kid, COALESCE(inf.occ_type, '0 INFANT') AS inf, res_gfil.rg_code FROM rssys.res_gfil LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%ADULT') adult ON adult.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%KID') kid ON kid.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%INFANT') inf ON inf.rg_code = res_gfil.rg_code LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_desc) LIKE 'PACKAGE%') GROUP BY res_gfil.rg_code, adult.occ_type, kid.occ_type, inf.occ_type) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS activities, COALESCE((CASE WHEN occ_type LIKE '%All' THEN occ_type END), '0 All') AS ttlpax, rg_code FROM rssys.res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code NOT IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_desc) LIKE 'PACKAGE%') GROUP BY rg_code, occ_type) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ WHERE reg_num = '" + code + "'");
        }
        //end of reservation 

        //grp reservation
        public DataTable get_grpresevationlist(String WHERE)
        {
            return this.QueryOnTableWithParams("resvngrp", "rgrp_code AS \"Resv Grp No\", grp_code AS \"Grp Code\", \"group\" AS \"Group Name\", contact AS \"Contact\", start AS \"Start\", \"end\" AS \"End\", user_id AS \"User Id\"", "(cancel IS NULL OR cancel='') AND (arrived IS NULL OR arrived='')" + WHERE, "");
        }

        public DataTable get_grpreservation_info(String rgrp_code)
        {
            return this.QueryOnTableWithParams("resvngrp", "rgrp_code, grp_code, \"group\", contact, start, \"end\", user_id, t_date, t_time, res_date, mkt_code, rm_features, bill_info, remarks", "rgrp_code='" + rgrp_code + "'", "");
        }

        public DataTable get_grpreservation_resfil(String rgrp_code)
        {
            return this.QueryOnTableWithParams("resfil", "*", "rgrp_code='" + rgrp_code + "'", "");
        }

        public DataTable get_grpreservation_dtl(String rgrp_code)
        {
            return this.QueryOnTableWithParams("resvngrp", "*", "rgrp_code='" + rgrp_code + "'", "");
        }

        public Boolean cancel_grpreservation()
        {
            return false;
        }
        //end of grp reservation

        //arrivals
        public String get_romstatus(String rom_code)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = this.QueryOnTableWithParams("rooms", "stat_code", "rom_code='" + rom_code + "'", "");

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception) { }

            return "";
        }

        public DataTable get_arrivallist(String search, String arr_dt)
        {
            DataTable dt = new DataTable();
            String arrived = "";

            if (Convert.ToDateTime(arr_dt).CompareTo(Convert.ToDateTime(this.get_systemdate(""))) >= 0)
            {
                arrived = " AND r.arrived IS NULL";
            }

            String SQL = "SELECT r.res_code as \"Res Code\", r.full_name AS Guest, r.rom_code AS Room, r.typ_code AS Type, r.arr_date AS Arrival, r.arr_time, r.dep_date AS Departure, r.dep_time, c.comp_name, r.user_id AS \"Check In By\", r.t_date AS \"Res Date\", r.t_time AS \"Res Time\", r.blockby AS \"Blocked By\", r.arrived FROM " + schema + ".resfil r LEFT JOIN " + schema + ".guest g ON r.acct_no=g.acct_no LEFT JOIN " + schema + ".company c ON g.comp_code=c.comp_code WHERE (r.cancel IS NULL OR r.cancel='')" + arrived + " AND r.arr_date = '" + arr_dt + "'" + search + " ORDER BY r.full_name ASC";

            dt = QueryBySQLCode(SQL);

            return dt;
        }
        //end of arrivals

        //housekeeping miscelleneous
        public DataTable get_chargehistory(String reg_num)
        {
            return this.QueryBySQLCode("SELECT cf.t_date, cf.chg_code, c.chg_desc, cf.reference, cf.amount, cf.user_id,cf.bill_amnt,cf.tax FROM " + schema + ".chgfil cf INNER JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE reg_num='" + reg_num + "' AND c.chg_type='C' AND c.ishskp='TRUE'");
        }
        //end of housekeeping

        //update room status
        public DataTable get_rmstatuslist(String rmstatus, String rmtyp)
        {
            String WHERE = "";

            if (rmstatus != "")
            {
                WHERE = " WHERE rm.stat_code='" + rmstatus + "'";
            }
            if (rmtyp != "")
            {
                if (WHERE == "")
                {
                    WHERE = " WHERE rm.typ_code='" + rmtyp + "'";
                }
                else if (WHERE != "")
                {
                    WHERE = WHERE + " AND rm.typ_code='" + rmtyp + "'";
                }
            }

            return this.QueryBySQLCode("SELECT rm.rom_code, typ.typ_desc, rm.stat_code FROM " + schema + ".rooms rm RIGHT JOIN " + schema + ".rtype typ ON rm.typ_code=typ.typ_code" + WHERE + " ORDER BY rom_code ASC");
        }

        public String get_guestname(String rom_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("gfolio", "full_name", "rom_code='" + rom_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public String get_guestregnum(String rom_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("gfolio", "reg_num", "rom_code='" + rom_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public String get_guestarrdate(String rom_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("gfolio", "arr_date", "rom_code='" + rom_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public String get_guestdepdate(String rom_code)
        {
            DataTable dt = new DataTable();
            String val = "";
            dt = this.QueryOnTableWithParams("gfolio", "dep_date", "rom_code='" + rom_code + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public DataTable get_guest_ooohistory(String rom_code)
        {
            return this.QueryOnTableWithParams("rmoorder", "trnx_date AS Date, reason AS Reason, user_id AS User, t_time AS Time", "rom_code='" + rom_code + "'", "");
        }
        //end of update room status

        //meal coupon
        public DataTable get_mealcoupon_history()
        {
            return this.QueryBySQLCode("SELECT mc.meal_num, mc.reg_num, mc.trnx_date, mc.pax_knt, mc.printed FROM " + schema + ".mealcoupn mc WHERE mc.printed='Y' ORDER BY mc.trnx_date DESC;");
        }
        //end of meal coupon

        public DataTable get_alluserid()
        {
            return this.QueryOnTableWithParams("x08", "uid", "", "ORDER BY uid ASC");
        }

        public DataTable get_alluserid_fullname()
        {
            return this.QueryOnTableWithParams("x08", "opr_name", "", "");
        }

        public DataTable get_allshift()
        {
            return this.QueryOnTableWithParams("shift", "shift_no, shift_time", "", "");
        }

        //folio
        public Double get_folio_totalpayment(String reg_num)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = QueryBySQLCode("SELECT SUM(cf.amount) AS totalamount FROM " + schema + ".chgfil cf RIGHT JOIN " + schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND c.chg_type='P'");

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_folio_totalcharges(String reg_num)
        {
            DataTable dt = new DataTable();

            dt = QueryBySQLCode("SELECT SUM(cf.amount) AS totalamount FROM " + schema + ".chgfil cf RIGHT JOIN " + schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND c.chg_type='C'");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }
            return 0.00;
        }

        //

        //////////////////charges
        //doc_type = CC, OR, NA
        public void insert_charges(String reg_num, String guest, String chg_code, String rom_code, String dreference, Double chg_amt, String trnx_dt, String fol_name, String fcontract, String res_code, String tofr_fol, String doc_type, Double vat_amnt, Double sc_amnt, Double or_amnt, String trace_no, String ccrd_no, String currency_code, Boolean print)
        {
            String chg_num = get_chg_num_latest(chg_code);
            String cc_no = "";
            String val = "";
            Report rpt = new Report("", "");
            NumberToEnglish_orig amtinwords = new NumberToEnglish_orig();

            if (get_roomchargetype(chg_code) == "P")
            {
                chg_amt = chg_amt * -1;
            }

            if (doc_type == "CC")
            {
                cc_no = get_pk("next_cc");

                dreference = cc_no;
            }

            val = "'" + reg_num + "', '" + chg_code + "', '" + chg_num + "', '" + rom_code + "', '" + dreference + "', '" + chg_amt.ToString("0.00") + "', '" + GlobalClass.username + "', '" + trnx_dt + "', '" + DateTime.Now.ToString("HH:mm") + "', '1', '" + trnx_dt + "', '" + fol_name + "', '" + fcontract + "', '" + res_code + "', '" + tofr_fol + "', '" + doc_type + "', '" + vat_amnt.ToString() + "', '" + sc_amnt.ToString() + "', '" + trace_no + "', '" + ccrd_no + "', '" + currency_code + "'";

            if (InsertOnTable("chgfil", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, fol_name, fcontract, res_code, tofr_fol, doc_type, vat_amnt, sc_amnt, trace_no, ccrd_no, currency_code", val))
            {
                set_all_pk("charge", "chg_num", chg_num, "chg_code='" + chg_code + "'", chg_num.Length);

                if (get_roomchargetype(chg_code) == "P")
                {
                    chg_amt = chg_amt * -1;
                }
                if (doc_type == "OR")
                {
                    InsertOnTable("or_folio_ref", "reg_num, or_no, or_amnt", "'" + reg_num + "', '" + dreference + "', '" + or_amnt.ToString("0.00") + "'");
                }
                if (doc_type == "CC")
                {
                    set_pkm99("next_cc", get_nextincrementlimitchar(cc_no, cc_no.Length));
                }
                if (doc_type == "CC" && print == true)
                {
                    rpt.Show();
                    rpt.disp_cc_or(dreference, guest, rom_code, amtinwords.changeCurrencyToWords(Convert.ToDouble(chg_amt.ToString("0.00"))), String.Format("{0:#,###,###.##}", chg_amt.ToString("0.00")), GlobalClass.username, "", "", trnx_dt);
                }
            }
            else
            {
                MessageBox.Show("Error occured on saving charge.");
            }

        }

        public void update_charges(String reg_num, String guest, String chg_code, String rom_code, String dreference, Double chg_amt, String trnx_dt, String fol_name, String fcontract, String res_code, String tofr_fol, String doc_type, Double vat_amnt, Double sc_amnt, String chg_num, Double or_amnt, String trace_no, String ccrd_no, String currency_code, Boolean print)
        {
            Report rpt = new Report("", "");
            NumberToEnglish_orig amtinwords = new NumberToEnglish_orig();

            if (get_roomchargetype(chg_code) == "P")
            {
                chg_amt = chg_amt * -1;
            }

            if (UpdateOnTable("chgfil", "reference='" + dreference + "', amount='" + chg_amt.ToString("0.00") + "', user_id='" + GlobalClass.username + "', t_time='" + DateTime.Now.ToString("HH:mm") + "', chg_date='" + trnx_dt + "', doc_type='" + doc_type + "', vat_amnt='" + vat_amnt.ToString("0.00") + "', sc_amnt='" + sc_amnt.ToString("0.00") + "', trace_no='" + trace_no + "', ccrd_no='" + ccrd_no + "', currency_code='" + currency_code + "'", "chg_code='" + chg_code + "' AND chg_num='" + chg_num + "' AND reg_num ='" + reg_num + "'"))
            {
                if (doc_type == "OR")
                {
                    UpdateOnTable("or_folio_ref", "reg_num='" + reg_num + "', or_no='" + dreference + "', or_amnt='" + or_amnt.ToString("0.00") + "'", "reg_num='" + reg_num + "' AND or_no='" + dreference + "'");
                }
                if (doc_type == "CC" && print == true)
                {
                    if (get_roomchargetype(chg_code) == "P")
                    {
                        chg_amt = chg_amt * -1;
                    }

                    rpt.disp_cc_or(dreference, guest, "", amtinwords.changeCurrencyToWords(chg_amt), String.Format("{0:#,###,###.##}", chg_amt.ToString("0.00")), GlobalClass.username, "", "", trnx_dt);
                    rpt.Show();
                }
            }
            else
            {
                MessageBox.Show("Error occured on saving charge.");
            }
        }

        public void transtotemp_chghist(String userid, String tmp_id, Boolean has_co, String query)
        {
            DataTable dt = new DataTable();
            String status;
            String co_date = "";
            String dep_date = "", arr_date = "", chg_date = "", t_date = "";
            String COL_add = "";
            String ROW_add = "";
            String food = "0.00", misc = "0.00", vat_amnt = "0.00", sc_amnt = "0.00", amount = "0.00";
            String doc_or_num = "", doc_cc_num = "";

            try
            {
                dt = this.QueryBySQLCode(query);

                //status: regular guest check in / chekout, senior citizen check in / check out
                foreach (DataRow row in dt.Rows)
                {
                    arr_date = Convert.ToDateTime(row["arr_date"].ToString()).ToString("yyyy-MM-dd");
                    dep_date = Convert.ToDateTime(row["dep_date"].ToString()).ToString("yyyy-MM-dd");
                    chg_date = Convert.ToDateTime(row["chg_date"].ToString()).ToString("yyyy-MM-dd");
                    t_date = Convert.ToDateTime(row["t_date"].ToString()).ToString("yyyy-MM-dd");

                    if (row["disc_code"].ToString() == "009")
                    {
                        status = "Senior Citizen Guest";
                    }
                    else
                    {
                        status = "Regular Guest";
                    }

                    if (has_co)
                    {
                        status = status + " Check Out";
                        co_date = Convert.ToDateTime(row["co_date"].ToString()).ToString("yyyy-MM-dd");
                        COL_add = "co_date, ";
                        ROW_add = co_date + "', '";
                    }
                    else
                    {
                        status = status + " Check In";
                    }

                    if (String.IsNullOrEmpty(row["food"].ToString()) == false)
                    {
                        food = row["food"].ToString();
                    }
                    if (String.IsNullOrEmpty(row["amount"].ToString()) == false)
                    {
                        amount = row["amount"].ToString();
                    }
                    if (String.IsNullOrEmpty(row["misc"].ToString()) == false)
                    {
                        misc = row["misc"].ToString();
                    }
                    if (String.IsNullOrEmpty(row["vat_amnt"].ToString()) == false)
                    {
                        vat_amnt = row["vat_amnt"].ToString();
                    }
                    if (String.IsNullOrEmpty(row["sc_amnt"].ToString()) == false)
                    {
                        sc_amnt = row["sc_amnt"].ToString();
                    }

                    if (row["doc_type"] == "CC")
                    {

                    }

                    if (row["doc_type"] == "OR")
                    {

                    }

                    this.InsertOnTable("temp_chghist", "user_id2, temp_id, reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, jrnlz, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt, full_name, arr_date, dep_date, " + COL_add + "status, doc_or_num, do_cc_num", "'" + userid + "', '" + tmp_id + "', '" + row["reg_num"].ToString() + "', '" + row["chg_code"].ToString() + "', '" + row["chg_num"].ToString() + "', '" + row["rom_code"].ToString() + "', '" + row["reference"].ToString() + "', '" + amount + "', '" + row["user_id"].ToString() + "', '" + t_date + "', '" + row["t_time"].ToString() + "', '" + row["fol_num"].ToString() + "', '" + chg_date + "', '" + row["jrnlz"].ToString() + "', '" + food + "', '" + misc + "', '" + row["fcontract"].ToString() + "', '" + row["res_code"].ToString() + "', '" + row["tofr_fol"].ToString() + "', '" + row["soa_code"].ToString() + "', '" + row["doc_type"].ToString() + "', '" + vat_amnt + "', '" + sc_amnt + "', '" + row["full_name"].ToString() + "', '" + arr_date + "', '" + dep_date + "', '" + ROW_add + status + "', '" + doc_or_num + "', '" + doc_cc_num + "'");
                }
            }
            catch (Exception er) { }
        }

        public DataTable get_guestforcharge()
        { 
            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode("SELECT g.*, d.sen_disc FROM " + schema + ".gfolio g LEFT JOIN " + schema + ".disctbl d ON g.disc_code=d.disc_code WHERE typ_code!='Z'");

            return dt;
        }
        public DataTable get_guestforcharge_daily_weekly()
        {
            DataTable dt = new DataTable();

            dt = this.QueryBySQLCode("SELECT g.*, d.sen_disc FROM " + schema + ".gfolio g LEFT JOIN " + schema + ".disctbl d ON g.disc_code=d.disc_code WHERE typ_code!='Z' AND rmrttyp<>'M'");

            return dt;
        }

        //rGuestBill.rom_code, txt_ref.Text, dtp_tdate.Value.ToString("yyyy-MM-dd")
        public Boolean roomcharge_reg(String gfno, String chg_code, String rom_code, String refer, String chg_date, Double val_amnt, String rmrttyp, DateTime arr_dt, String senior,String posting_status)
        {
            GlobalMethod gm = new GlobalMethod();
            Double amt = 0.00;
            Double amt_tax = 0.00;
            Double amt_sc = 0.00;
            Double disc_amt = 0.00;
            Double disc_pct = 0.00;
            Boolean has_vat = true, has_sc = false, has_senior = false;
            Boolean okToCharge = false;
            DateTime sysdate = Convert.ToDateTime(this.get_systemdate("yyyy-MM-dd"));
            String sytemsdate = sysdate.Day.ToString();
            String sched = get_pk("day_of_posting");
            //for generate room posting
            if (val_amnt == 0)
            {
                //get the gross rate total rate
                val_amnt = get_guest_rmnetrate(gfno) + get_guest_rmtax(gfno); // +get_guest_rmsc(gfno);

                if (rmrttyp == "M")
                {
                    if (sytemsdate == sched)
                    {
                        okToCharge = true;
                    }
                }
                else if (rmrttyp == "W")
                {
                    if (sysdate.DayOfWeek == arr_dt.DayOfWeek)
                    {
                        okToCharge = true;
                    }
                }
                else
                {
                    okToCharge = true;
                }
            }
            //for manual posting
            else
            {
                okToCharge = true;
            }

            if (okToCharge)
            {
                if (String.IsNullOrEmpty(senior) == false)
                {
                    if (senior == "Y")
                    {
                        has_senior = true;
                    }
                }

                amt = val_amnt;

                if (has_vat == true && has_sc == false)
                {
                    amt_tax = get_tax(val_amnt, disc_pct, disc_amt);
                }
                if (has_sc)
                {
                    amt_sc = get_svccharge(val_amnt, disc_pct, disc_amt);
                }

                String chg_num = get_chg_num_latest(chg_code);
                String val = "'" + gfno + "', '" + chg_code + "', '" + chg_num + "', '" + rom_code + "', '" + refer + "', '" + amt.ToString() + "', '" + GlobalClass.username + "', '" + get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '1', '" + chg_date + "','','" + amt_tax.ToString() + "', '" + amt_sc.ToString() + "'";

                //nMessageBox.Show(rom_code + " : " + sysdate.Day.ToString() + " ==  " + arr_dt.Day.ToString() + " amt=" + amt.ToString() + " rmrttyp=" +rmrttyp.ToString() );

                if (InsertOnTable("chgfil", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, fol_name, vat_amnt, sc_amnt", val))
                {
                    set_all_pk("charge", "chg_num", chg_num, "chg_code='" + chg_code + "'", chg_num.Length);

                    return true;
                }
            }

            return false;
        }

        public void clear_temp_chghist(String rpt_no)
        {
            try
            {
                DeleteOnTable("temp_chghist", "user_id2='" + GlobalClass.username + "' AND temp_id='" + rpt_no + "'");
            }
            catch (Exception) { }
        }

        public DataTable get_dsrlist(String dt_frm, String dt_to)
        {
            return this.QueryOnTableWithParams("dsr", "dsr_no AS \"DSR No\", shift_no AS \"Shift\", cashier AS \"Cashier\", dsr_dt AS \"Date\", cash_amnt AS \"Cash\", crd_amnt AS \"Card\", cci_amnt AS \"CC Issued\", cca_amnt AS \"CC Applied\", fb_sales AS \"Food/Bvrg. Sales\", extra AS \"Extra Sales\", sales AS \"Day Sales\", actual AS \"Actual Cash\", generatedby AS \"Generated By\", t_date_gen ||' '|| t_time_gen AS \"Gen.Date/Time\", finalizedby AS \"Finalized By\", t_date_fin ||' '|| t_time_fin AS \"Fin.Date/Time\", dsr_no_cashier AS ID", "dsr_dt >= '" + dt_frm + "' AND dsr_dt <= '" + dt_to + "'", "ORDER BY dsr_dt ASC");
        }

        public DataTable get_dsr(String dsr_no, String cashier)
        {
            return this.QueryOnTableWithParams("dsr", "dsr_no AS \"DSR No\", shift_no AS \"Shift\", cashier AS \"Cashier\", dsr_dt AS \"Date\", cash_amnt AS \"Cash\", crd_amnt AS \"Card\", cci_amnt AS \"CC Issued\", cca_amnt AS \"CC Applied\", fb_sales AS \"Food/Bvrg. Sales\", extra AS \"Extra Sales\", sales AS \"Day Sales\", actual AS \"Actual Cash\", generatedby AS \"Generated By\", t_date_gen ||' '|| t_time_gen AS \"Gen.Date/Time\", finalizedby AS \"Finalized By\", t_date_fin ||' '|| t_time_fin AS \"Fin.Date/Time\", dsr_no_cashier AS ID", "dsr_no='" + dsr_no + "' AND cashier='" + cashier + "'", "ORDER BY dsr_dt ASC");
        }

        public DataTable get_dsr_cash_rpt(String dsr_no, String cashier)
        {
            return this.QueryOnTableWithParams("dsr_rpt", "*", "dsr_no='" + dsr_no + "' AND cashier='" + cashier + "' AND dsr_type='CSH'", "");
        }

        public DataTable get_dsr_ccrd_rpt(String dsr_no, String cashier)
        {
            return this.QueryOnTableWithParams("dsr_rpt", "*", "dsr_no='" + dsr_no + "' AND cashier='" + cashier + "' AND dsr_type='CSH'", "");
        }

        public DataTable get_dsr_cci_rpt(String dsr_no, String cashier)
        {
            return this.QueryOnTableWithParams("dsr_cc", "*", "dsr_no='" + dsr_no + "' AND cashier='" + cashier + "' AND cc_type='i'", "");
        }

        public DataTable get_dsr_cca_rpt(String dsr_no, String cashier)
        {
            return this.QueryOnTableWithParams("dsr_cc", "*", "dsr_no='" + dsr_no + "' AND cashier='" + cashier + "' AND cc_type='a'", "");
        }

        // per or number has total cash amount
        public DataTable get_cash_or_rpt(String dt_frm, String dt_to, String cashier)
        {
            String WHERE = "";

            if (cashier != "")
            {
                WHERE = " AND gf.co_user='" + cashier + "'";
            }

            return QueryBySQLCode("SELECT cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", gf.rom_code AS \"Room\", o.or_no AS \"OR Reference\", o.or_amnt AS \"OR Amount\", (SUM(cf.amount)*-1) AS \"Cash Amount\", (SELECT SUM(cf2.amount) FROM " + schema + ".chghist cf2 JOIN rssys.charge c2 ON c2.chg_code=cf2.chg_code AND c2.chg_class in ('F&B')  WHERE cf2.reg_num=cf.reg_num) AS \"FB Amount\", (SELECT SUM(cf2.amount) FROM " + schema + ".chghist cf2 JOIN rssys.charge c2 ON c2.chg_code=cf2.chg_code AND c2.chg_class in ('PDOUT') WHERE cf2.reg_num=cf.reg_num) AS paidout FROM " + schema + ".chghist cf Left join " + schema + ".gfhist gf on gf.reg_num=cf.reg_num LEFT JOIN " + schema + ".or_folio_ref o ON o.reg_num=gf.reg_num JOIN rssys.charge c ON c.chg_code=cf2.chg_code AND c.chg_class in ('CASH') WHERE gf.co_date='" + dt_to + "'" + WHERE + " GROUP BY cf.reg_num, gf.full_name, gf.rom_code, o.or_no, o.or_amnt ORDER BY o.or_no ASC");
        }

        //per or number has total card amount
        public DataTable get_ccrd_or_rpt(String dt_frm, String dt_to, String cashier)
        {
            String WHERE = "";

            if (cashier != "")
            {
                WHERE = " AND gf.co_user='" + cashier + "'";
            }

            return QueryBySQLCode("SELECT cf.reg_num AS \"Folio No\", gf.full_name AS \"Guest\", gf.rom_code AS \"Room\", o.or_no AS \"OR Reference\", c.card_type AS \"Card Type\", cf.ccrd_no AS \"Card Number\", cf.trace_no AS \"Trace No\", o.or_amnt AS \"OR Amount\", cf.amount AS \"Card Amount\", (SELECT SUM(cf2.amount) FROM " + schema + ".chghist cf2 JOIN rssys.charge c2 ON c2.chg_code=cf2.chg_code AND c2.chg_class in ('F&B')  WHERE cf2.reg_num=cf.reg_num) AS \"FB Amount\" FROM " + schema + ".chghist cf JOIN " + schema + ".charge c ON c.chg_code=cf.chg_code LEFT JOIN " + schema + ".gfhist gf on gf.reg_num=cf.reg_num  LEFT JOIN " + schema + ".or_folio_ref o ON o.reg_num=gf.reg_num WHERE c.chg_class in ('CCARD')  AND cf.amount != '0.00' AND gf.co_date='" + dt_to + "'" + WHERE + " GROUP BY cf.reg_num, gf.full_name, gf.rom_code, o.or_no, o.or_amnt, cf.amount, c.card_type, cf.ccrd_no, cf.trace_no ORDER BY o.or_no ASC");
        }

        public Boolean is_exists_regnumfromcash(String reg_num, String dt_frm, String dt_to, String cashier)
        {
            String WHERE = "";

            if (cashier != "")
            {
                WHERE = " AND user_id='" + cashier + "'";
            }

            //QueryOnTableWithParams("chghist", "1", "reg_num='" + reg_num + "' AND chg_code='101' AND chg_date>='" + dt_frm + "' AND chg_date<='" + dt_to + "'" + WHERE + "", "");
            DataTable dt = QueryBySQLCode("SELECT 1 FROM rssys.chghist ch JOIN rssys.charge c ON c.chg_code=ch.chg_code AND c.chg_class in ('F&B') WHERE ch.reg_num='" + reg_num + "' AND ch.chg_date>='" + dt_frm + "' AND ch.chg_date<='" + dt_to + "'" + WHERE + " ");

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public String get_shift_no(String shift)
        {
            DataTable dt = new DataTable();
            String val = "";

            dt = QueryOnTableWithParams("shift", "shift_no", "shift_time='" + shift + "'", "");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    val = row[0].ToString();
                }
            }

            return val;
        }

        public DataTable get_extraitemsales(String user_id, String dt_frm, String dt_to)
        {
            String extraitem = this.get_pk("extraitem");
            String WHERE = "";

            if (user_id != "")
            {
                WHERE = " AND cf.user_id='" + user_id + "'";
            }

            return this.QueryBySQLCode("SELECT cf.chg_date AS t_date, cf.t_time, cf.user_id, cf.chg_code, c.chg_desc AS description, cf.reference, cf.amount AS totalamount FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".chgfil cf ON cf.reg_num=gf.reg_num LEFT JOIN " + schema + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num='" + extraitem + "' AND cf.chg_date>='" + dt_frm + "' AND cf.chg_date<='" + dt_to + "'" + WHERE);
        }

        public Double get_extraitemsales_amnt(String user_id, String dt_frm, String dt_to)
        {
            String extraitem = this.get_pk("extraitem");
            String WHERE = "";
            DataTable dt = new DataTable();

            if (user_id != "")
            {
                WHERE = " AND cf.user_id='" + user_id + "'";
            }

            dt = this.QueryBySQLCode("SELECT SUM(cf.amount) AS totalamount FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".chgfil cf ON cf.reg_num=gf.reg_num LEFT JOIN " + schema + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num='" + extraitem + "' AND cf.chg_date>='" + dt_frm + "' AND cf.chg_date<='" + dt_to + "'" + WHERE);

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public DataTable get_orlist(String reg_num)
        {
            return this.QueryOnTableWithParams("or_folio_ref", "or_no AS \"OR No\", or_amnt AS \"OR Amount\", user_id AS \"User ID\", t_date AS \"Date\", t_time AS \"Time\", id", "reg_num='" + reg_num + "'", "ORDER BY t_date ASC, t_time ASC");
        }

        public Boolean iscardpayment(String chg_code)
        {
            if (!String.IsNullOrEmpty(get_colval("charge", "chg_class", "chg_code='" + chg_code + "' AND chg_class='CCARD'")))
            {
                return true;
            }

            if (chg_code == "102" || chg_code == "103" || chg_code == "104" || chg_code == "105")
            {
                return true;
            }

            return false;
        }

        public Double get_guestchkin_totalpayment(String reg_num)
        {
            DataTable dt = new DataTable();

            //dt = QueryOnTableWithParams("chgfil", "SUM(amount)", "reg_num='" + reg_num + "' AND ", "");

            dt = QueryBySQLCode("SELECT SUM(cf.amount) FROM " + schema + ".chgfil cf LEFT JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE c.chg_type='P' AND cf.reg_num='" + reg_num + "'");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_guestchkin_totalpaymenthist(String reg_num)
        {
            DataTable dt = new DataTable();

            //dt = QueryOnTableWithParams("chgfil", "SUM(amount)", "reg_num='" + reg_num + "' AND ", "");

            dt = QueryBySQLCode("SELECT SUM(cf.amount) FROM " + schema + ".chghist cf LEFT JOIN " + schema + ".charge c ON cf.chg_code=c.chg_code WHERE c.chg_type='P' AND cf.reg_num='" + reg_num + "'");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public DataTable get_romrate(string reg_num)
        {
            return QueryBySQLCode("SELECT rt.typ_code, rt.typ_desc, pub_rate.rate_code, pub_rate.rate_desc, rr.single, rr.double, rr.triple, rr.quad FROM " + schema + ".rtype rt LEFT JOIN " + schema + ".romrate rr ON rt.typ_code=rr.typ_code LEFT JOIN " + schema + ".ratetype pub_rate ON pub_rate.rate_code=rr.rate_code WHERE rr.single!='0.00' AND rr.double!='0.00' ORDER BY pub_rate.rate_code ASC");
        }

        public DataTable get_romrate_filterby_rtcode(string rt_code)
        {
            String WHERE = "";

            if (rt_code != "")
            {
                WHERE = " AND pub_rate.rate_code = '" + rt_code + "'";
            }

            return QueryBySQLCode("SELECT rt.typ_code, rt.typ_desc, pub_rate.rate_code, pub_rate.rate_desc, rr.single, rr.double, rr.triple, rr.quad FROM " + schema + ".rtype rt LEFT JOIN " + schema + ".romrate rr ON rt.typ_code=rr.typ_code LEFT JOIN " + schema + ".ratetype pub_rate ON pub_rate.rate_code=rr.rate_code WHERE rr.single!='0.00' AND rr.double!='0.00'" + WHERE + " ORDER BY pub_rate.rate_code ASC");
        }

        public DataTable get_cancelandnoshowreshist(String res_code)
        {
            return QueryBySQLCode("SELECT rom_code AS \"Room\", typ_code AS \"Type\", arr_date AS \"Expected Arrival\", dep_date AS \"Expected Departure\", rom_rate AS \"Rate\", CASE WHEN cancel='Y' OR cancel IS NOT NULL THEN 'CANCELLED' WHEN arrived IS NULL OR arrived!='Y' THEN 'NO SHOW' END AS \"Status\", reason ||' | '|| turnaway ||' | '||canc_remarks AS \"Reason | Turnaway | Remarks\", canc_user AS \"Cancelled By\", canc_date||' '|| canc_time AS \"Cancelled On\", blockresv AS \"Blocked Reservation\", user_id AS \"Reservation Clerk\", t_date||' '|| t_time AS \"Reservaton Date/Time\" FROM " + schema + ".resfil WHERE ((cancel='Y' OR cancel IS NOT NULL) OR ((arrived IS NULL OR arrived!='Y') AND arr_date<'" + this.get_systemdate("") + "')) AND res_code='" + res_code + "'");
        }

        public DataTable get_dsrcashlist()
        {
            return null;
        }

        public DataTable get_hibalguestlist()
        {
            Double amtlimit = get_guestballimit();

            return QueryBySQLCode("SELECT gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, sum(cf.amount)AS balance, (SELECT SUM(cf1.amount) FROM " + schema + ".chgfil cf1 LEFT JOIN " + schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='C') AS total_charges, (SELECT SUM(cf1.amount) FROM " + schema + ".chgfil cf1 LEFT JOIN " + schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='P') AS total_payment, gf.reg_num, c.comp_name AS market_name FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".chgfil cf ON cf.reg_num=gf.reg_num LEFT JOIN " + schema + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE gf.rom_code!='" + get_colval("m99", "rom_spc", "") + "' AND (SELECT SUM(cf1.amount) FROM " + schema + ".chgfil cf1 WHERE cf1.reg_num=gf.reg_num)  >= " + amtlimit.ToString() + " GROUP BY gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.reg_num, c.comp_name ORDER BY gf.rom_code");
        }

        public Double get_guestballimit()
        {
            DataTable dt = new DataTable();

            dt = QueryOnTableWithParams("m99", "hi_bal", "", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_transfbal(String regnum)
        {
            DataTable dt = new DataTable();

            dt = QueryOnTableWithParams("chgfil", "SUM(amount)", "reg_num='" + regnum + "' AND chg_code='008'", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_paidout(String regnum)
        {
            DataTable dt = new DataTable();

            dt = QueryOnTableWithParams("chgfil", "SUM(amount)", "reg_num='" + regnum + "' AND chg_code='007'", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_transfbalhist(String regnum)
        {
            DataTable dt = new DataTable();

            dt = QueryOnTableWithParams("chghist", "SUM(amount)", "reg_num='" + regnum + "' AND chg_code='008'", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_paidouthist(String regnum)
        {
            DataTable dt = new DataTable();

            dt = QueryOnTableWithParams("chghist", "SUM(amount)", "reg_num='" + regnum + "' AND chg_code='007'", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception) { }

            return 0.00;
        }

        public DataTable get_tableToSend_NoMasterdata()
        {
            return QueryOnTableWithParams("branchtable", "tablename, partable", "sendbybranch=true AND (masterdata IS false OR masterdata IS NULL)", " ORDER BY  tablename ASC");
        }

        public DataTable get_tableToSend_MasterdataOnly()
        {
            return QueryOnTableWithParams("branchtable", "tablename, partable", "sendbybranch=true AND masterdata=true", " ORDER BY  tablename ASC");
        }

        public DataTable get_dataToSend(String table, String dtfrm, String dtto, Boolean hasparttable, Boolean NoDate)
        {
            String WHERE = "";

            if (table == "gfchange" || table == "gfguest" || table == "resgrp" || table == "powrate" || table == "resguest" || table == "untchg" || table == "resvngrpdtl" || hasparttable == true || NoDate == true)
            {
                WHERE = "";
            }
            else if (table == "gextras" || table == "mealcoupn" || table == "rechdr" || table == "rmoorder" || table == "stkcrd" || table == "itmfifo" || table == "occupanc")
            {
                WHERE = " trnx_date BETWEEN '" + dtfrm + "' AND '" + dtto + "'";
            }
            else if (table == "prhdr")
            {
                WHERE = " pr_date BETWEEN '" + dtfrm + "' AND '" + dtto + "'";
            }
            else if (table == "resfil")
            {
                WHERE = " res_date BETWEEN '" + dtfrm + "' AND '" + dtto + "'";
            }
            else
            {
                WHERE = " t_date BETWEEN '" + dtfrm + "' AND '" + dtto + "'";
            }

            return QueryOnTableWithParams(table,"*", WHERE, "");
        }

        public DataTable get_columns(String table)
        {
            return QueryBySQLCode("SELECT column_name FROM information_schema.columns WHERE table_schema='"+this.schema+"' AND table_name='" + table + "'");
        }

        public DataTable get_columnkey(String table)
        {
            return QueryBySQLCode("SELECT c.column_name FROM information_schema.table_constraints tc JOIN information_schema.constraint_column_usage AS ccu USING (constraint_schema, constraint_name) JOIN information_schema.columns AS c ON c.table_schema = tc.constraint_schema AND tc.table_name = c.table_name AND ccu.column_name = c.column_name WHERE constraint_type = 'PRIMARY KEY' AND tc.table_name='"+table+"'");
        }

        public Boolean iscol_numeric(String table, String columnname)
        {
            DataTable dt = QueryBySQLCode("SELECT 1 FROM information_schema.columns WHERE table_schema='"+ this.schema +"' AND table_name='"+table+"' AND data_type = 'numeric' AND column_name='"+columnname+"'");

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public Boolean iscol_date(String table, String columnname)
        {
            DataTable dt = QueryBySQLCode("SELECT 1 FROM information_schema.columns WHERE table_schema='" + this.schema + "' AND table_name='" + table + "' AND data_type = 'date' AND column_name='" + columnname + "'");

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        public Boolean InsertSelect(String table, String col1, String table2, String col2, String cond)
        {
            Boolean flag = false;

            try
            {
                this.OpenConn();
                String whr = ((cond == "") ? "" : " WHERE " + cond + "");
                string SQL = "INSERT INTO " + this.schema + "." + table + " (" + col1 + ") SELECT " + col2 + " FROM " + this.schema + "." + table2 + "" + whr + "";
                //MessageBox.Show(SQL);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                this.CloseConn();

                flag = true;
            }
            catch (Exception er)
            {
                flag = false;
                MessageBox.Show(er.Message);
            }

            return flag;
        }
    }
}