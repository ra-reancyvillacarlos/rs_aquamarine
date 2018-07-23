using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hotel_System
{
   public  class GlobalClass
    {
        private static string l_user = null;
        private static string l_userfullname = null;
        private static string l_branch = null;
        private static string l_schema = null;
        private static DataTable l_gdt = null;
        private static DataRow l_gdr = null;
        private static DataGridView l_gdgv = null;
        private static DataGridViewRow l_gdgvRow = null;
        
        public static string username
        {
            get { return l_user; }
            set { l_user = value; }
        }

        public static string user_fullname
        {
            get { return l_userfullname; }
            set { l_userfullname = value; }
        }
        public static string branch
        {
            get { return l_branch; }
            set { l_branch = value; }
        }

        public static string schema
        {
            get { return l_schema; }
            set { l_schema = value; }
        }

        public static DataTable gdt
        {
            get { return l_gdt; }
            set { l_gdt = value; }
        }

        public static DataRow gdr
        {
            get { return l_gdr; }
            set { l_gdr = value; }
        }

        public static DataGridView gdgv
        {
            get { return l_gdgv; }
            set { l_gdgv = value; }
        }

        public static DataGridViewRow gdgvRow
        {
            get { return l_gdgvRow; }
            set { l_gdgvRow = value; }
        }
    }
}
