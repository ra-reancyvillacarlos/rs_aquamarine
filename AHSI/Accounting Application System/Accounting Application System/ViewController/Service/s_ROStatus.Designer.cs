namespace Accounting_Application_System
{
    partial class s_ROStatus
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_searchby = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvi_ord_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_outlet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_date_promise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_status_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_whs_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvli_car_plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_ro_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvli_car_plate_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvli_car_engine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvli_car_vin_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_custcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1051, 514);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repair Order Status List";
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToResizeRows = false;
            this.dgv_list.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvi_ord_code,
            this.dgvi_outlet,
            this.dgvi_code,
            this.dgvi_customer,
            this.dgvi_date_promise,
            this.dgvi_status,
            this.dgvi_status_no,
            this.dgvi_whs_code,
            this.dgvli_car_plate,
            this.dgvi_ro_date,
            this.dgvli_car_plate_type,
            this.dgvli_car_engine,
            this.dgvli_car_vin_num,
            this.dgvi_custcode});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(1045, 494);
            this.dgv_list.TabIndex = 96;
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_search);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbo_searchby);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbo_branch);
            this.groupBox2.Controls.Add(this.dtp_to);
            this.groupBox2.Controls.Add(this.dtp_frm);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1051, 92);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter By";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(813, 26);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(113, 48);
            this.btn_search.TabIndex = 126;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(428, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 125;
            this.label1.Text = "Search";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(494, 51);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(290, 21);
            this.txt_search.TabIndex = 124;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(428, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 122;
            this.label2.Text = "Search By";
            // 
            // cbo_searchby
            // 
            this.cbo_searchby.AllowDrop = true;
            this.cbo_searchby.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_searchby.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_searchby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_searchby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_searchby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_searchby.FormattingEnabled = true;
            this.cbo_searchby.Items.AddRange(new object[] {
            "Outlet",
            "R.O. Number",
            "Customer Name",
            "Repair Status"});
            this.cbo_searchby.Location = new System.Drawing.Point(494, 15);
            this.cbo_searchby.Name = "cbo_searchby";
            this.cbo_searchby.Size = new System.Drawing.Size(290, 23);
            this.cbo_searchby.TabIndex = 123;
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
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Items.AddRange(new object[] {
            "Walk-In",
            "Online",
            "Government"});
            this.cbo_branch.Location = new System.Drawing.Point(135, 51);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(242, 23);
            this.cbo_branch.TabIndex = 116;
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(274, 21);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(103, 21);
            this.dtp_to.TabIndex = 64;
            // 
            // dtp_frm
            // 
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(135, 21);
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
            this.label6.Location = new System.Drawing.Point(247, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 15);
            this.label6.TabIndex = 66;
            this.label6.Text = "To";
            // 
            // dgvi_ord_code
            // 
            this.dgvi_ord_code.HeaderText = "ORD CODE";
            this.dgvi_ord_code.Name = "dgvi_ord_code";
            this.dgvi_ord_code.ReadOnly = true;
            // 
            // dgvi_outlet
            // 
            this.dgvi_outlet.HeaderText = "OUTLET";
            this.dgvi_outlet.Name = "dgvi_outlet";
            this.dgvi_outlet.ReadOnly = true;
            this.dgvi_outlet.Width = 200;
            // 
            // dgvi_code
            // 
            this.dgvi_code.HeaderText = "R.O. NO";
            this.dgvi_code.Name = "dgvi_code";
            this.dgvi_code.ReadOnly = true;
            // 
            // dgvi_customer
            // 
            this.dgvi_customer.HeaderText = "CUSTOMER";
            this.dgvi_customer.Name = "dgvi_customer";
            this.dgvi_customer.ReadOnly = true;
            this.dgvi_customer.Width = 200;
            // 
            // dgvi_date_promise
            // 
            this.dgvi_date_promise.HeaderText = "DATE PROMISE";
            this.dgvi_date_promise.Name = "dgvi_date_promise";
            this.dgvi_date_promise.ReadOnly = true;
            // 
            // dgvi_status
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvi_status.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvi_status.HeaderText = "STATUS DESC";
            this.dgvi_status.MinimumWidth = 160;
            this.dgvi_status.Name = "dgvi_status";
            this.dgvi_status.ReadOnly = true;
            this.dgvi_status.Width = 160;
            // 
            // dgvi_status_no
            // 
            this.dgvi_status_no.HeaderText = "STATUS NO";
            this.dgvi_status_no.Name = "dgvi_status_no";
            this.dgvi_status_no.ReadOnly = true;
            // 
            // dgvi_whs_code
            // 
            this.dgvi_whs_code.HeaderText = "WAREHOUSE CODE";
            this.dgvi_whs_code.Name = "dgvi_whs_code";
            this.dgvi_whs_code.ReadOnly = true;
            // 
            // dgvli_car_plate
            // 
            this.dgvli_car_plate.HeaderText = "PLATE NO";
            this.dgvli_car_plate.Name = "dgvli_car_plate";
            this.dgvli_car_plate.ReadOnly = true;
            // 
            // dgvi_ro_date
            // 
            this.dgvi_ro_date.HeaderText = "RO DATE";
            this.dgvi_ro_date.Name = "dgvi_ro_date";
            this.dgvi_ro_date.ReadOnly = true;
            // 
            // dgvli_car_plate_type
            // 
            this.dgvli_car_plate_type.HeaderText = "PLATE TYPE";
            this.dgvli_car_plate_type.Name = "dgvli_car_plate_type";
            this.dgvli_car_plate_type.ReadOnly = true;
            // 
            // dgvli_car_engine
            // 
            this.dgvli_car_engine.HeaderText = "ENGINE";
            this.dgvli_car_engine.Name = "dgvli_car_engine";
            this.dgvli_car_engine.ReadOnly = true;
            // 
            // dgvli_car_vin_num
            // 
            this.dgvli_car_vin_num.HeaderText = "VIN NO.";
            this.dgvli_car_vin_num.Name = "dgvli_car_vin_num";
            this.dgvli_car_vin_num.ReadOnly = true;
            // 
            // dgvi_custcode
            // 
            this.dgvi_custcode.HeaderText = "CUST CODE";
            this.dgvi_custcode.Name = "dgvi_custcode";
            this.dgvi_custcode.ReadOnly = true;
            // 
            // s_ROStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1051, 606);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "s_ROStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_AutoROStatus";
            this.Load += new System.EventHandler(this.s_ROStatus_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_searchby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_ord_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_outlet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_date_promise;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_status_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_whs_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvli_car_plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_ro_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvli_car_plate_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvli_car_engine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvli_car_vin_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_custcode;
    }
}