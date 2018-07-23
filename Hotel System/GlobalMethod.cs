using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Numeric;
using System.Text.RegularExpressions;

namespace Hotel_System
{
    public class GlobalMethod
    {
        thisDatabase db = new thisDatabase();
        public DataGridView CopyDataGridView(DataGridView dgv_org)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < dgv_org.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_copy.Rows.Add(row);
                }
                dgv_copy.AllowUserToAddRows = false;
                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }

            return dgv_copy;
        }
        public String getNumber(String str)
        {
            return Regex.Replace(str, @"[^\d]", "");
        }
        public Double get_amount(String amnt)
        {
            try
            {
                if (amnt.StartsWith("("))
                {
                    amnt = amnt.Replace('(', ' ');
                    amnt = amnt.Replace(')', ' ');

                    return Convert.ToDouble(amnt) * -1;
                }

                return Convert.ToDouble(amnt);
            }
            catch (Exception) { }

            return 0.00;
        }

        public Boolean is_Doublevalid(String amnt)
        {
            try
            {
                Convert.ToDouble(amnt);

                return true;
            }
            catch (Exception) { }

            return false;
        }
        public void load_hkcharge(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "chg_type='C' AND (UPPER(utility)='ELECTRICITY' OR UPPER(utility)='WATER')", " ORDER BY chg_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_company(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "comp_name";
                cbo.ValueMember = "comp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_shift(ComboBox cbo)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_allshift();
            cbo.DataSource = dt;
            cbo.DisplayMember = "shift_time";
            cbo.ValueMember = "shift_no";
            cbo.SelectedIndex = -1;
        }

        public void load_country(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("country", "cntry_code, cntry_desc", "", "ORDER BY cntry_desc ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "cntry_desc";
                cbo.ValueMember = "cntry_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_romratetype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("romratetyp", "code, name", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }


        public void load_romtype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_roomtypExptZ();
                cbo.DataSource = dt;
                cbo.DisplayMember = "typ_desc";
                cbo.ValueMember = "typ_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_mscbo(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_roomtypExptZ();
                cbo.DataSource = dt;
                cbo.DisplayMember = "typ_desc";
                cbo.ValueMember = "typ_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_market(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("market", "mkt_code, mkt_desc", "", "ORDER BY mkt_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mkt_desc";
                cbo.ValueMember = "mkt_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_ratetype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ratetype", "rate_code, rate_desc", "", "ORDER BY rate_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "rate_desc";
                cbo.ValueMember = "rate_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_disctbl(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("disctbl", "disc_code, disc_desc", "", "ORDER BY disc_desc ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "disc_desc";
                cbo.ValueMember = "disc_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_nationality(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("nationality", "nat_code, nat_desc", "", "ORDER BY nat_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "nat_desc";
                cbo.ValueMember = "nat_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_charge_paymentsonly(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "chg_type='P'", "ORDER BY chg_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_charge_chargesonly(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "chg_type='C'", "ORDER BY chg_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_charge_depositonly(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "isdeposit='TRUE'", "ORDER BY chg_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_charge_all(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "", "ORDER BY chg_code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;


            }
            catch (Exception) { }
        }

        public void load_currency_code(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("currency", "code, description", "", "ORDER BY code ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "code";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public String get_amount_negbracket(Double amnt)
        {
            String new_amnt = "0.00";

            if (amnt < 0)
            {
                new_amnt = "(" + Math.Abs(amnt).ToString("0.00") + ")";
            }
            else
            {
                new_amnt = amnt.ToString("0.00");
            }

            return new_amnt;
        }

        public String toAccountingFormat(Double amt)
        {
            try
            {
                return amt.ToString("#,##0.00;(#,##0.00);0.00");
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            return "";
        }

        public Double toNormalDoubleFormat(String acct_amt)
        {
            try
            {
                if (acct_amt.Contains("(") && acct_amt.Contains(")"))
                {
                    return Double.Parse(acct_amt, NumberStyles.AllowParentheses |
                                      NumberStyles.AllowThousands |
                                      NumberStyles.AllowDecimalPoint);
                }
                else
                    return Convert.ToDouble(acct_amt);
            }
            catch (Exception) { }

            return 0.00;
        }

        public Double get_ElectricityReadingRate(Double previousWatt, Double currentWatt, Double get_min, Double get_min_charge, Double get_excess_charge, Double month_charge)
        {
            Double AmountpKwh = 0.00;
            Double cons = currentWatt - previousWatt;
            Double prod_min_chg = 0.00;
            Double prod_excess = 0.00;
            Double prod_mo_chg = 0.00;
            Double total = 0.00, tax_amnt = 0.00;

            //if (cons <= get_min)
            //{
            //    AmountpKwh = get_min;
            //}
            //else {
            //    AmountpKwh = get_excess_charge;
            //}
            //Double AmountpKwh = Double.Parse(db.get_colval("charge", "price", "chg_code='ELTRC'"));
            //Double min_amt = 200.00;
            //Double value = 0.00;

            //value = diff * AmountpKwh;

            //prod_min_chg = get_min * get_min_charge;
            //cons = Math.Round(cons, 0, MidpointRounding.AwayFromZero);
            //if (cons > get_min)
            //{
            //    prod_excess = (cons - get_min) * month_charge;
            //}

            //prod_mo_chg = cons * month_charge;

            //total = prod_min_chg + prod_excess /*+ prod_mo_chg*/;

            prod_min_chg = get_min * get_min_charge;
            cons = Math.Round(cons, 0, MidpointRounding.AwayFromZero);

            
            if (cons > get_min)
            {
                prod_excess = cons * month_charge;
                total = prod_excess /*+ prod_mo_chg*/;

            }
            else
            {
                total = prod_min_chg;
            }

            return total;
        }

        public Double get_WaterReadingRate(Double previousReading, Double currentReading, Double get_min, Double get_min_charge, Double get_excess_charge)
        {
            Double cons = currentReading - previousReading;
            //Double value = Double.Parse(db.get_colval("charge", "price", "chg_code='WTR'"));
            Double value = 0.00;
            Double prod_min_charge = 0.00;
            Double prod_excess = 0.00;

            cons = Math.Round(cons, 0, MidpointRounding.AwayFromZero);
            if (cons > get_min)
            {
                prod_excess = (cons - get_min) * get_excess_charge;
                value = get_min_charge + prod_excess;
            }
            else
            {

                prod_min_charge = get_min_charge;
                value = prod_min_charge ;
            }

            return value;
        }


        //Start By: Roldan, Date: April 6, 2018
        public void load_agency(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("travagnt", "trv_code, trv_name", "", "ORDER BY trv_name ASC;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "trv_name";
                cbo.ValueMember = "trv_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        // End By: Roldan, Date: April 6, 2018

        //Start By: Roldan, Date: April 7, 2018
        
        public void load_year(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("x03", "DISTINCT fy", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "fy";
                cbo.ValueMember = "fy";
                cbo.SelectedIndex = 1;
            }
            catch (Exception)
            { }
        }
        //End By: Roldan, Date: April 7, 2018

    }
}
