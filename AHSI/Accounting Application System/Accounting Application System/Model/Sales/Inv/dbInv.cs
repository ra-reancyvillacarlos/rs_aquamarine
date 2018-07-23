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
    class dbInv : thisDatabase
    {
        GlobalClass gc;
        GlobalMethod gm;

        public dbInv()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
        }

        public DataTable  get_assembleditems(String item_code)
        {
            return this.QueryBySQLCode("SELECT i2.*, i.item_desc, i.ave_cost, i2.qty*i.ave_cost AS ln_amt,  u.unit_shortcode FROM " + schema + ".items2 i2 LEFT JOIN " + schema + ".items i ON i2.item_code=i.item_code LEFT JOIN " + schema + ".itmunit u ON i2.sales_unit_id=u.unit_id WHERE i2.item_code='" + item_code + "'");
        }
    }

}
