using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System.MD
{
    public partial class PrintBarcode : Form
    {
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        int _copies = 0;

        public PrintBarcode()
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            gc = new GlobalClass();

            gc.load_item(cbo_bcd_1);
            gc.load_item(cbo_bcd_2);
            gc.load_item(cbo_bcd_3);
        }

        private void PrintBarcode_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_barcode_Click(object sender, EventArgs e)
        {
            String bcd1 = get_cbo_value(cbo_bcd_1);
            String bcd2 = get_cbo_value(cbo_bcd_2);
            String bcd3 = get_cbo_value(cbo_bcd_3);
            _copies = gm.toInt(txt_bcd_noofcopies.Text);

            int c1 = gm.toInt(txt_col1.Text);
             int c2 = gm.toInt(txt_col2.Text);
             int c3= gm.toInt(txt_col3.Text);

            if(_copies == 0)
            {
                _copies = 1;
            }

            BarcodePrintOut bcd_print = new BarcodePrintOut(bcd1, bcd2, bcd3, _copies,c1, c2, c3 );

            bcd_print.print();

            //other_classes.ZebraPrint.Print();
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String val = "";

            try
            {
                val = cbo.SelectedValue.ToString();
            }
            catch
            { }

            return val;
        }

    }
}
