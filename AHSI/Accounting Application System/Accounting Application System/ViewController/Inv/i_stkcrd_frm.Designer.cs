namespace Accounting_Application_System
{
    partial class i_stkcrd_frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.trnx_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trn_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supl_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whs_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.po_so = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_shortcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_in = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty_out = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supl_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cht_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnt_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whs_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_select = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_totalqty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_itemcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_partno = new System.Windows.Forms.TextBox();
            this.txt_itemdesc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Code";
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trnx_date,
            this.part_no,
            this.trn_type,
            this.supl_name,
            this.whs_desc,
            this.reference,
            this.po_so,
            this.unit_shortcode,
            this.qty_in,
            this.qty_out,
            this.price,
            this.fcp,
            this.item_desc,
            this.supl_code,
            this.item_code,
            this.cht_code,
            this.cnt_code,
            this.unit,
            this.whs_code});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 20;
            this.dgv_list.Size = new System.Drawing.Size(1014, 555);
            this.dgv_list.TabIndex = 3;
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // trnx_date
            // 
            this.trnx_date.HeaderText = "Trans.Date";
            this.trnx_date.Name = "trnx_date";
            this.trnx_date.ReadOnly = true;
            this.trnx_date.Width = 77;
            // 
            // part_no
            // 
            this.part_no.HeaderText = "Part No.";
            this.part_no.Name = "part_no";
            this.part_no.ReadOnly = true;
            // 
            // trn_type
            // 
            this.trn_type.HeaderText = "Trans.Type";
            this.trn_type.Name = "trn_type";
            this.trn_type.ReadOnly = true;
            this.trn_type.Width = 77;
            // 
            // supl_name
            // 
            this.supl_name.HeaderText = "Supplier";
            this.supl_name.Name = "supl_name";
            this.supl_name.ReadOnly = true;
            this.supl_name.Width = 150;
            // 
            // whs_desc
            // 
            this.whs_desc.HeaderText = "Location";
            this.whs_desc.Name = "whs_desc";
            this.whs_desc.ReadOnly = true;
            // 
            // reference
            // 
            this.reference.HeaderText = "Reference";
            this.reference.Name = "reference";
            this.reference.ReadOnly = true;
            // 
            // po_so
            // 
            this.po_so.HeaderText = "PO | SO #";
            this.po_so.Name = "po_so";
            this.po_so.ReadOnly = true;
            // 
            // unit_shortcode
            // 
            this.unit_shortcode.HeaderText = "Unit";
            this.unit_shortcode.Name = "unit_shortcode";
            this.unit_shortcode.ReadOnly = true;
            this.unit_shortcode.Width = 77;
            // 
            // qty_in
            // 
            this.qty_in.HeaderText = "Qty In";
            this.qty_in.Name = "qty_in";
            this.qty_in.ReadOnly = true;
            // 
            // qty_out
            // 
            this.qty_out.HeaderText = "Qty Out";
            this.qty_out.Name = "qty_out";
            this.qty_out.ReadOnly = true;
            // 
            // price
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.price.DefaultCellStyle = dataGridViewCellStyle1;
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // fcp
            // 
            this.fcp.HeaderText = "FCP";
            this.fcp.Name = "fcp";
            this.fcp.ReadOnly = true;
            // 
            // item_desc
            // 
            this.item_desc.HeaderText = "Item Description";
            this.item_desc.Name = "item_desc";
            this.item_desc.ReadOnly = true;
            this.item_desc.Width = 250;
            // 
            // supl_code
            // 
            this.supl_code.HeaderText = "Supp Code";
            this.supl_code.Name = "supl_code";
            this.supl_code.ReadOnly = true;
            // 
            // item_code
            // 
            this.item_code.HeaderText = "Item Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // cht_code
            // 
            this.cht_code.HeaderText = "Cht Code";
            this.cht_code.Name = "cht_code";
            this.cht_code.ReadOnly = true;
            // 
            // cnt_code
            // 
            this.cnt_code.HeaderText = "Cost Center";
            this.cnt_code.Name = "cnt_code";
            this.cnt_code.ReadOnly = true;
            // 
            // unit
            // 
            this.unit.HeaderText = "Unit ID";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 77;
            // 
            // whs_code
            // 
            this.whs_code.HeaderText = "Loc ID";
            this.whs_code.Name = "whs_code";
            this.whs_code.ReadOnly = true;
            // 
            // btn_select
            // 
            this.btn_select.BackColor = System.Drawing.Color.Peru;
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Image = global::Accounting_Application_System.Properties.Resources.note_search___32;
            this.btn_select.Location = new System.Drawing.Point(578, 24);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(129, 46);
            this.btn_select.TabIndex = 5;
            this.btn_select.Text = "Select Item";
            this.btn_select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_select.UseVisualStyleBackColor = false;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_itemdesc);
            this.groupBox1.Controls.Add(this.txt_partno);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_itemcode);
            this.groupBox1.Controls.Add(this.txt_totalqty);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_select);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1020, 87);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Item";
            // 
            // txt_totalqty
            // 
            this.txt_totalqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_totalqty.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_totalqty.Location = new System.Drawing.Point(762, 48);
            this.txt_totalqty.Name = "txt_totalqty";
            this.txt_totalqty.ReadOnly = true;
            this.txt_totalqty.Size = new System.Drawing.Size(186, 26);
            this.txt_totalqty.TabIndex = 7;
            this.txt_totalqty.Text = "0.00";
            this.txt_totalqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(759, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Item Quantity Current Balances";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_list);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1020, 575);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Transcation Information List";
            // 
            // txt_itemcode
            // 
            this.txt_itemcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_itemcode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txt_itemcode.Location = new System.Drawing.Point(119, 14);
            this.txt_itemcode.Name = "txt_itemcode";
            this.txt_itemcode.ReadOnly = true;
            this.txt_itemcode.Size = new System.Drawing.Size(142, 22);
            this.txt_itemcode.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Item Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(286, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Part Number";
            // 
            // txt_partno
            // 
            this.txt_partno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_partno.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txt_partno.Location = new System.Drawing.Point(385, 14);
            this.txt_partno.Name = "txt_partno";
            this.txt_partno.ReadOnly = true;
            this.txt_partno.Size = new System.Drawing.Size(142, 22);
            this.txt_partno.TabIndex = 12;
            // 
            // txt_itemdesc
            // 
            this.txt_itemdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_itemdesc.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txt_itemdesc.Location = new System.Drawing.Point(119, 47);
            this.txt_itemdesc.Name = "txt_itemdesc";
            this.txt_itemdesc.ReadOnly = true;
            this.txt_itemdesc.Size = new System.Drawing.Size(453, 22);
            this.txt_itemdesc.TabIndex = 13;
            // 
            // i_stkcrd_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1020, 662);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimizeBox = false;
            this.Name = "i_stkcrd_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Transaction Card";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.i_stkcrd_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_totalqty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn trnx_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn trn_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn supl_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn whs_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn po_so;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_shortcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_in;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty_out;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcp;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn supl_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cht_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnt_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn whs_code;
        private System.Windows.Forms.TextBox txt_itemcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_partno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_itemdesc;
    }
}