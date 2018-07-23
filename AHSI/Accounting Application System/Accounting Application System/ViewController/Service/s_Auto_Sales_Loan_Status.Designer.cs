namespace Accounting_Application_System
{
    partial class s_Auto_Sales_Loan_Status
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_customer = new System.Windows.Forms.TextBox();
            this.btn_filter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvl_appno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_custname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_financer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_custno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_finance_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_trnx_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(874, 476);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Loan Status List";
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToOrderColumns = true;
            this.dgv_list.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl_appno,
            this.dgvl_custname,
            this.dgvl_financer,
            this.dgvl_status,
            this.dgvl_custno,
            this.dgvl_finance_code,
            this.dgvl_trnx_date});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(868, 456);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.txt_customer);
            this.groupBox2.Controls.Add(this.btn_filter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbo_status);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbo_branch);
            this.groupBox2.Controls.Add(this.dtp_to);
            this.groupBox2.Controls.Add(this.dtp_frm);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(874, 86);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter By";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(398, 54);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(60, 15);
            this.label31.TabIndex = 120;
            this.label31.Text = "Customer";
            // 
            // txt_customer
            // 
            this.txt_customer.BackColor = System.Drawing.SystemColors.Window;
            this.txt_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_customer.Location = new System.Drawing.Point(464, 51);
            this.txt_customer.Name = "txt_customer";
            this.txt_customer.Size = new System.Drawing.Size(230, 22);
            this.txt_customer.TabIndex = 121;
            // 
            // btn_filter
            // 
            this.btn_filter.BackColor = System.Drawing.Color.Peru;
            this.btn_filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filter.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_filter.Image = global::Accounting_Application_System.Properties.Resources._1343892246_adept_update;
            this.btn_filter.Location = new System.Drawing.Point(725, 20);
            this.btn_filter.Margin = new System.Windows.Forms.Padding(5);
            this.btn_filter.Name = "btn_filter";
            this.btn_filter.Size = new System.Drawing.Size(136, 49);
            this.btn_filter.TabIndex = 119;
            this.btn_filter.Text = "Refresh";
            this.btn_filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_filter.UseVisualStyleBackColor = false;
            this.btn_filter.Click += new System.EventHandler(this.btn_filter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(388, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 117;
            this.label1.Text = "Status Type";
            // 
            // cbo_status
            // 
            this.cbo_status.AllowDrop = true;
            this.cbo_status.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_status.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_status.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Items.AddRange(new object[] {
            "APPROVED",
            "PENDING"});
            this.cbo_status.Location = new System.Drawing.Point(464, 19);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(230, 23);
            this.cbo_status.TabIndex = 118;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 115;
            this.label4.Text = "Branch Name";
            // 
            // cbo_branch
            // 
            this.cbo_branch.AllowDrop = true;
            this.cbo_branch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_branch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_branch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_branch.Enabled = false;
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Items.AddRange(new object[] {
            "Walk-In",
            "Online",
            "Government"});
            this.cbo_branch.Location = new System.Drawing.Point(123, 51);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(242, 23);
            this.cbo_branch.TabIndex = 116;
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(262, 21);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(103, 21);
            this.dtp_to.TabIndex = 64;
            // 
            // dtp_frm
            // 
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(123, 21);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(103, 21);
            this.dtp_frm.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 15);
            this.label7.TabIndex = 65;
            this.label7.Text = "Transaction Dates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 15);
            this.label6.TabIndex = 66;
            this.label6.Text = "To";
            // 
            // dgvl_appno
            // 
            this.dgvl_appno.DataPropertyName = "app_no";
            this.dgvl_appno.HeaderText = "Auto Loan No.";
            this.dgvl_appno.Name = "dgvl_appno";
            this.dgvl_appno.ReadOnly = true;
            // 
            // dgvl_custname
            // 
            this.dgvl_custname.DataPropertyName = "m06_finance_name";
            this.dgvl_custname.HeaderText = "Customer Name";
            this.dgvl_custname.Name = "dgvl_custname";
            this.dgvl_custname.ReadOnly = true;
            this.dgvl_custname.Width = 160;
            // 
            // dgvl_financer
            // 
            this.dgvl_financer.DataPropertyName = "cust_name";
            this.dgvl_financer.HeaderText = "Financer";
            this.dgvl_financer.Name = "dgvl_financer";
            this.dgvl_financer.ReadOnly = true;
            // 
            // dgvl_status
            // 
            this.dgvl_status.DataPropertyName = "status";
            this.dgvl_status.HeaderText = "Status";
            this.dgvl_status.Name = "dgvl_status";
            this.dgvl_status.ReadOnly = true;
            // 
            // dgvl_custno
            // 
            this.dgvl_custno.DataPropertyName = "m06_finance_code";
            this.dgvl_custno.HeaderText = "Customer No.";
            this.dgvl_custno.Name = "dgvl_custno";
            this.dgvl_custno.ReadOnly = true;
            // 
            // dgvl_finance_code
            // 
            this.dgvl_finance_code.DataPropertyName = "financer_code";
            this.dgvl_finance_code.HeaderText = "Financer Code";
            this.dgvl_finance_code.Name = "dgvl_finance_code";
            this.dgvl_finance_code.ReadOnly = true;
            // 
            // dgvl_trnx_date
            // 
            this.dgvl_trnx_date.DataPropertyName = "trnx_date";
            this.dgvl_trnx_date.HeaderText = "Trans Date";
            this.dgvl_trnx_date.Name = "dgvl_trnx_date";
            this.dgvl_trnx_date.ReadOnly = true;
            // 
            // s_Auto_Sales_Loan_Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(874, 562);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "s_Auto_Sales_Loan_Status";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Loan Status";
            this.Load += new System.EventHandler(this.s_Auto_Sales_Loan_Status_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.Windows.Forms.Button btn_filter;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_customer;
        public System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_appno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_custname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_financer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_custno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_finance_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_trnx_date;
    }
}