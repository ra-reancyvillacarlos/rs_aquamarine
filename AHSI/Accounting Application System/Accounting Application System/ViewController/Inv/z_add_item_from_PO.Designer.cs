namespace Accounting_Application_System
{
    partial class z_add_item_from_PO
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.ln_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_shortcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ln_amnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cbo_code = new System.Windows.Forms.ComboBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ln_num,
            this.part_no,
            this.item_code,
            this.item_desc,
            this.quantity,
            this.unit_shortcode,
            this.price,
            this.ln_amnt});
            this.dgv_list.Location = new System.Drawing.Point(12, 51);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 20;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(745, 404);
            this.dgv_list.TabIndex = 9;
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // ln_num
            // 
            this.ln_num.DataPropertyName = "ln_num";
            this.ln_num.HeaderText = "LINE";
            this.ln_num.Name = "ln_num";
            this.ln_num.Width = 50;
            // 
            // part_no
            // 
            this.part_no.DataPropertyName = "part_no";
            this.part_no.HeaderText = "PART NO";
            this.part_no.Name = "part_no";
            this.part_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item_code
            // 
            this.item_code.DataPropertyName = "item_code";
            this.item_code.HeaderText = "ITEM CODE";
            this.item_code.Name = "item_code";
            this.item_code.Width = 90;
            // 
            // item_desc
            // 
            this.item_desc.DataPropertyName = "item_desc";
            this.item_desc.HeaderText = "DESCRIPTION";
            this.item_desc.Name = "item_desc";
            this.item_desc.Width = 200;
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            this.quantity.HeaderText = "QUANTITY";
            this.quantity.Name = "quantity";
            this.quantity.Width = 77;
            // 
            // unit_shortcode
            // 
            this.unit_shortcode.HeaderText = "UNIT";
            this.unit_shortcode.Name = "unit_shortcode";
            this.unit_shortcode.ReadOnly = true;
            this.unit_shortcode.Width = 77;
            // 
            // price
            // 
            this.price.DataPropertyName = "price";
            this.price.HeaderText = "COST PRICE";
            this.price.Name = "price";
            // 
            // ln_amnt
            // 
            this.ln_amnt.DataPropertyName = "ln_amnt";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ln_amnt.DefaultCellStyle = dataGridViewCellStyle1;
            this.ln_amnt.HeaderText = "LINE AMOUNT";
            this.ln_amnt.Name = "ln_amnt";
            this.ln_amnt.Width = 120;
            // 
            // cbo_code
            // 
            this.cbo_code.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_code.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_code.FormattingEnabled = true;
            this.cbo_code.Location = new System.Drawing.Point(150, 14);
            this.cbo_code.Name = "cbo_code";
            this.cbo_code.Size = new System.Drawing.Size(500, 21);
            this.cbo_code.TabIndex = 16;
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_search.Location = new System.Drawing.Point(666, 5);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(91, 37);
            this.btn_search.TabIndex = 15;
            this.btn_search.Text = "GO";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click_2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select PO Number";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Peru;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_save.Image = global::Accounting_Application_System.Properties.Resources._1343892363_plus_24;
            this.btn_save.Location = new System.Drawing.Point(638, 461);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(119, 48);
            this.btn_save.TabIndex = 17;
            this.btn_save.Text = "ADD TO LIST";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // z_add_item_from_PO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(772, 514);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.cbo_code);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "z_add_item_from_PO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Item(s) From Purchase Order";
            this.Load += new System.EventHandler(this.z_add_item_from_PO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ComboBox cbo_code;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn ln_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_shortcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn ln_amnt;
    }
}