namespace Accounting_Application_System
{
    partial class z_unpaid_invoices
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_invoicelist = new System.Windows.Forms.DataGridView();
            this.sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvl_j_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inv_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inv_bal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inv_paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inv_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_addnew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_acct_title = new System.Windows.Forms.Label();
            this.lbl_subdry = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_invoicelist)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_invoicelist);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 429);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice Number";
            // 
            // dgv_invoicelist
            // 
            this.dgv_invoicelist.AllowUserToResizeColumns = false;
            this.dgv_invoicelist.AllowUserToResizeRows = false;
            this.dgv_invoicelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_invoicelist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sel,
            this.dgvl_j_code,
            this.invoice_no,
            this.inv_date,
            this.inv_bal,
            this.inv_paid,
            this.inv_desc});
            this.dgv_invoicelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_invoicelist.Location = new System.Drawing.Point(3, 18);
            this.dgv_invoicelist.Name = "dgv_invoicelist";
            this.dgv_invoicelist.RowHeadersVisible = false;
            this.dgv_invoicelist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_invoicelist.Size = new System.Drawing.Size(735, 408);
            this.dgv_invoicelist.TabIndex = 0;
            this.dgv_invoicelist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_invoicelist_CellPainting);
            // 
            // sel
            // 
            this.sel.HeaderText = "Select";
            this.sel.Name = "sel";
            this.sel.Width = 50;
            // 
            // dgvl_j_code
            // 
            this.dgvl_j_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvl_j_code.HeaderText = "JRNL";
            this.dgvl_j_code.Name = "dgvl_j_code";
            this.dgvl_j_code.ReadOnly = true;
            this.dgvl_j_code.Visible = false;
            // 
            // invoice_no
            // 
            this.invoice_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.invoice_no.FillWeight = 77F;
            this.invoice_no.HeaderText = "Invoice";
            this.invoice_no.MinimumWidth = 100;
            this.invoice_no.Name = "invoice_no";
            this.invoice_no.ReadOnly = true;
            this.invoice_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inv_date
            // 
            this.inv_date.FillWeight = 77F;
            this.inv_date.HeaderText = "Date";
            this.inv_date.Name = "inv_date";
            this.inv_date.ReadOnly = true;
            this.inv_date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inv_date.Width = 90;
            // 
            // inv_bal
            // 
            this.inv_bal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.inv_bal.DefaultCellStyle = dataGridViewCellStyle1;
            this.inv_bal.HeaderText = "Invoice Bal";
            this.inv_bal.MinimumWidth = 100;
            this.inv_bal.Name = "inv_bal";
            this.inv_bal.ReadOnly = true;
            this.inv_bal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inv_paid
            // 
            this.inv_paid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.inv_paid.DefaultCellStyle = dataGridViewCellStyle2;
            this.inv_paid.HeaderText = "Paid";
            this.inv_paid.MinimumWidth = 100;
            this.inv_paid.Name = "inv_paid";
            this.inv_paid.ReadOnly = true;
            this.inv_paid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inv_desc
            // 
            this.inv_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.inv_desc.FillWeight = 220F;
            this.inv_desc.HeaderText = "Description";
            this.inv_desc.MinimumWidth = 275;
            this.inv_desc.Name = "inv_desc";
            this.inv_desc.ReadOnly = true;
            this.inv_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inv_desc.Width = 275;
            // 
            // btn_select
            // 
            this.btn_select.BackColor = System.Drawing.Color.Peru;
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Location = new System.Drawing.Point(653, 491);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(95, 43);
            this.btn_select.TabIndex = 1;
            this.btn_select.Text = "Select";
            this.btn_select.UseVisualStyleBackColor = false;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_addnew
            // 
            this.btn_addnew.BackColor = System.Drawing.Color.Peru;
            this.btn_addnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addnew.Location = new System.Drawing.Point(552, 491);
            this.btn_addnew.Name = "btn_addnew";
            this.btn_addnew.Size = new System.Drawing.Size(95, 43);
            this.btn_addnew.TabIndex = 2;
            this.btn_addnew.Text = "Add New";
            this.btn_addnew.UseVisualStyleBackColor = false;
            this.btn_addnew.Click += new System.EventHandler(this.btn_addnew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subsidiary Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Account Title";
            // 
            // lbl_acct_title
            // 
            this.lbl_acct_title.AutoSize = true;
            this.lbl_acct_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_acct_title.ForeColor = System.Drawing.Color.Blue;
            this.lbl_acct_title.Location = new System.Drawing.Point(123, 9);
            this.lbl_acct_title.Name = "lbl_acct_title";
            this.lbl_acct_title.Size = new System.Drawing.Size(85, 16);
            this.lbl_acct_title.TabIndex = 6;
            this.lbl_acct_title.Text = "Account Title";
            // 
            // lbl_subdry
            // 
            this.lbl_subdry.AutoSize = true;
            this.lbl_subdry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subdry.ForeColor = System.Drawing.Color.Blue;
            this.lbl_subdry.Location = new System.Drawing.Point(123, 36);
            this.lbl_subdry.Name = "lbl_subdry";
            this.lbl_subdry.Size = new System.Drawing.Size(112, 16);
            this.lbl_subdry.TabIndex = 5;
            this.lbl_subdry.Text = "Subsidiary Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 492);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 492);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Search Invoice";
            // 
            // z_unpaid_invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(755, 547);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbl_acct_title);
            this.Controls.Add(this.lbl_subdry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_addnew);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_unpaid_invoices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unpaid Invoices";
            this.Load += new System.EventHandler(this.prompt_unpaid_invoices_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_invoicelist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_addnew;
        private System.Windows.Forms.DataGridView dgv_invoicelist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_acct_title;
        private System.Windows.Forms.Label lbl_subdry;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_j_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn inv_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn inv_bal;
        private System.Windows.Forms.DataGridViewTextBoxColumn inv_paid;
        private System.Windows.Forms.DataGridViewTextBoxColumn inv_desc;
    }
}