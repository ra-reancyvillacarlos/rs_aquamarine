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
    class dbHotel : thisDatabase
    {
        public DataTable get_rooms()
        {
            return QueryOnTableWithParams("rooms", "*", "", " ORDER BY rom_code");
        }
    }
}
