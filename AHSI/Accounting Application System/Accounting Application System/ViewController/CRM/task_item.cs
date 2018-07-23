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
    public partial class task_item : Form
    {
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        thisDatabase db = new thisDatabase();
        to_do _frm_todo = null;
        Boolean newitem = false;
        String curerentindex = "";
        String linenumber = "";
        int line = 1;
        public task_item()
        {
            InitializeComponent();
        }
        public task_item(to_do frm, Boolean iscallback, String line)
        {
        InitializeComponent();
        _frm_todo = frm;
        this.line = int.Parse(line);
        newitem = iscallback;
        if (!iscallback) { 
            int r = -1;
            r = _frm_todo.dgv_itemlist.CurrentRow.Index;
            curerentindex = r.ToString();
            rtxt_task_details.Text = _frm_todo.dgv_itemlist["dgvi_desc", r].Value.ToString();
            //linenumber = _frm_todo.dgv_itemlist["dgvi_line", r].Value.ToString();
        }
        }
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (newitem)
            {
                _frm_todo.dgv_itemlist.Rows.Add();
                _frm_todo.dgv_itemlist["dgvi_lnno", _frm_todo.dgv_itemlist.Rows.Count-1].Value = line.ToString();
                _frm_todo.dgv_itemlist["dgvi_desc", _frm_todo.dgv_itemlist.Rows.Count - 1].Value = rtxt_task_details.Text;
            }
            else 
            {
                //_frm_todo.dgv_itemlist["dgvi_lnno", line].Value = line;
                _frm_todo.dgv_itemlist["dgvi_desc", int.Parse(curerentindex)].Value = rtxt_task_details.Text;
            }
            this.Close();
        }
    }
}
