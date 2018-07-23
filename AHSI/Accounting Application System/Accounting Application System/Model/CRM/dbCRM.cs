using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

namespace Accounting_Application_System
{
    class dbCRM : thisDatabase
    {
        public DataTable get_customer_contact_list(String cntc_name)
        {
            String WHERE = "";

            if (String.IsNullOrEmpty(cntc_name) == false)
            {
                WHERE = "(d_name LIKE '%" + cntc_name + "%' OR lastname LIKE '%" + cntc_name + "' OR firstname LIKE '%" + cntc_name + "') AND ";
            }

            return this.QueryOnTableWithParams("m06", "*", WHERE + " COALESCE(d_cntc_no,'')<>'' OR COALESCE(d_tel,'')<>''", "ORDER BY d_code ASC");
        }
        public DataTable get_tblist_withstat()
        {
            DataTable dt = null;
            try
            {
                dt = QueryBySQLCode("SELECT t.*, t.tbid , to_char(send_date, 'MM/DD/yyyy') AS date_send, (SELECT COUNT(r.d_code) FROM rssys.tb_recip r WHERE r.tbid=t.tbid) AS RECIP, (SELECT COUNT(r.d_code) FROM rssys.tb_recip r WHERE r.tbid=t.tbid AND send_stat='Y') AS SEND,(SELECT COUNT(r.d_code) FROM rssys.tb_recip r WHERE R.tbid = t.tbid AND send_stat = 'N') AS FAIL FROM rssys.tb_hdr t");
            }
            catch (Exception er)
            {
                MessageBox.Show("Error at DB_CRM : " + er.Message);
            }
            return dt;
        }
    }
}
