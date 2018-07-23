using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class room_rate_info : Form
    {
        Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
        Hotel_System.GlobalClass gc = new Hotel_System.GlobalClass();
        Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
        RoomRates rm_rate_frm = null;
        String rate_code = "", typ_code = "";
        Boolean isnew = true; 
        private string p1;
        private string p2;
        public room_rate_info()
        {
            InitializeComponent();
        }
        public room_rate_info(RoomRates frm, String typ_code, String typ_desc, String rate_code)
        {
            InitializeComponent();

            this.rm_rate_frm = frm;
            this.rate_code = rate_code;
            this.typ_code = typ_code;

            lbl_room_type.Text = typ_desc;

            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT * from rssys.romrate WHERE typ_code = '" + typ_code + "' AND rate_code='" + rate_code + "'");
                if (dt.Rows.Count > 0)
                {
                    isnew = false;
                    txt_single.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["single"].ToString()));
                    txt_double.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["double"].ToString()));
                    txt_triple.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["triple"].ToString()));
                    txt_quad.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[0]["quad"].ToString()));
                }
            }catch{}
        }

        public room_rate_info(string p1, string p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            String single_occ = "", double_occ = "", triple_occ = "", quad_occ = "";
            String col = "",val = "";
            Boolean success = false;
            single_occ = gm.toNormalDoubleFormat(txt_single.Text).ToString("0.00");
            double_occ = gm.toNormalDoubleFormat(txt_double.Text).ToString("0.00");
            triple_occ = gm.toNormalDoubleFormat(txt_triple.Text).ToString("0.00");
            quad_occ = gm.toNormalDoubleFormat(txt_quad.Text).ToString("0.00");

            if (isnew)
            {
                col = "single,double,triple,quad,rate_code,typ_code";
                val = "'" + single_occ + "','" + double_occ + "','" + triple_occ + "','" + quad_occ + "','" + rate_code + "','" + typ_code + "'";
                if (db.InsertOnTable("romrate", col, val))
                {
                    success = true;
                }
            }
            else
            {
                col = "single='" + single_occ + "', double='" + double_occ + "', triple='" + triple_occ + "', quad='" + quad_occ + "' ";
                if (db.UpdateOnTable("romrate", col, "typ_code='" + typ_code + "' AND rate_code='" + rate_code + "'"))
                {
                    success = true;
                }
            }

            if (!success)
            {
                MessageBox.Show("Saving Data Failed.");
                return;
            }
            rm_rate_frm.disp_list();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
