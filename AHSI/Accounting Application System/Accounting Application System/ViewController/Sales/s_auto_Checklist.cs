using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class s_auto_Checklist : Form
    {
        private thisDatabase db = new thisDatabase();

        private s_Release_Deliver_Unit frm_reldel;

        //others 
        Boolean seltbp = false;
        private Boolean isnew = false;
        String pkey = null;

        private List<int> list = new List<int>();

        public s_auto_Checklist()
        {
            InitializeComponent();
            //disp_list();
        }
        public s_auto_Checklist(s_Release_Deliver_Unit frm) : this()
        {
            frm_reldel = frm;
            disp_list();
        }


        private void goto_win1()
        {
            seltbp = true;
            tbpg_grp.SelectedTab = tpgi_list;
            tpgi_list.Show();
            seltbp = false;
        }
        //
        private void goto_win2()
        {
            seltbp = true;
            tbpg_grp.SelectedTab = tpgi_info;
            tpgi_info.Show();
            seltbp = false;

        }

        private void tbpg_grp_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_desc.Text = "";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            isnew = true;
            frm_clear();
            goto_win2();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            isnew = false;
            goto_win1();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String code = txt_code.Text;
            String desc = txt_desc.Text;

            try
            {

                if (String.IsNullOrEmpty(code) || String.IsNullOrEmpty(desc))
                {
                    MessageBox.Show("Please input required fields.");
                    return;
                }

                String table = "vehicle_checklist";
                String col = "", val = "";
                Boolean success = false;


                if (isnew)
                {
                    col = "vc_code, vc_desc";
                    val = "" + db.str_E(code) + "," + db.str_E(desc) + "";

                    db.DeleteOnTable(table, "vc_code='" + code + "' AND COALESCE(cancel,cancel,'')='Y'");
                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                    }
                }
                else
                {
                    col = "vc_code=" + db.str_E(code) + ", vc_desc=" + db.str_E(desc) + "";

                    if (db.UpdateOnTable(table, col, "vc_code='" + pkey + "'"))
                    {
                        success = true;
                    }
                }
                if (success)
                {
                    goto_win1();
                    disp_list();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save : Code already used.");
            }
        }

        private void disp_list()
        {
            try{
                dgv_list.Rows.Clear();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.vehicle_checklist WHERE COALESCE(cancel,cancel,'')<>'Y' ORDER BY vc_code");

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int r = dgv_list.Rows.Add();
                        dgv_list["dgvlf_line", r].Value = i + 1;
                        dgv_list["dgvlf_code", r].Value = dt.Rows[i]["vc_code"].ToString();
                        dgv_list["dgvlf_name", r].Value = dt.Rows[i]["vc_desc"].ToString();

                        if (frm_reldel != null)
                        {
                            DataGridView frm_dt = frm_reldel.dgv_itemlist;
                            for (int r2 = 0; r2 < frm_dt.Rows.Count; r2++)
                            {
                                String code = frm_dt["dgvli_code", r2].Value.ToString();
                                if (code.Equals(dt.Rows[i]["vc_code"].ToString()))
                                {
                                    dgv_list["dgvl_chk", r].Value = true;
                                    list.Add(r);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        /*
        
         
         */
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvlf_code", r].Value.ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this checklist?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes) {
                        db.UpdateOnTable("vehicle_checklist", "cancel='Y'", "vc_code='" + code + "'");
                        MessageBox.Show("Successfully cancelled.");
                        list.Remove(r);
                        disp_list();
                    }
                }
                else
                {
                    MessageBox.Show("No selected checklist.");
                }

            }
            catch
            {
                MessageBox.Show("No selected checklist.");
            }

        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            isnew = false;
            disp_info();
            goto_win2();
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvlf_code", r].Value.ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    pkey = code;
                    txt_code.Text = code;
                    txt_desc.Text = dgv_list["dgvlf_name", r].Value.ToString();
                }
            }
            catch { }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try 
            {
                int r = dgv_list.CurrentRow.Index;
                String code = (dgv_list["dgvlf_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    String strbool = (dgv_list["dgvl_chk", r].Value ?? "false").ToString();
                    if (!Convert.ToBoolean(strbool))
                    {
                        if (list.IndexOf(r) == -1)
                        {
                            dgv_list["dgvl_chk", r].Value = true;
                            list.Add(r);
                        }
                    }
                    else
                    {
                        if(dgv_list.CurrentCell.ColumnIndex == 0)
                            list.Remove(r);
                    }
                }
                else {
                    dgv_list.Rows.RemoveAt(r);
                }
            }
            catch {}
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                list.Sort();

                DataGridView frm_dt = frm_reldel.dgv_itemlist;
                frm_dt.Rows.Clear();


                for (int r = 0; r < list.Count; r++)
                {
                    int indx = (int)list[r];

                    int i = frm_dt.Rows.Add();

                    frm_dt["dgvli_lnno", i].Value = dgv_list["dgvlf_line", indx].Value.ToString();
                    frm_dt["dgvli_code", i].Value = dgv_list["dgvlf_code", indx].Value.ToString();
                    frm_dt["dgvli_desc", i].Value = dgv_list["dgvlf_name", indx].Value.ToString();
          
                }
            }
            catch { }
            this.Close();
        }
    }
}
