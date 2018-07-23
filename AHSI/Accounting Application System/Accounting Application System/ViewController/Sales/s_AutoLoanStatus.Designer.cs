namespace Accounting_Application_System
{
    partial class s_AutoLoanStatus
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
            this.dgvl_finance_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_appno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_custno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_custname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_financer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_trnx_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tbcntrl_left = new System.Windows.Forms.TabControl();
            this.tpg_option = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_upd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tpg_option_2 = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_payadd = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_outlet = new System.Windows.Forms.ComboBox();
            this.cbo_whsname = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_additem = new System.Windows.Forms.Button();
            this.btn_delitem = new System.Windows.Forms.Button();
            this.btn_upditem = new System.Windows.Forms.Button();
            this.btn_saveorder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tbcntrl_left.SuspendLayout();
            this.tpg_option.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tpg_option_2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(170, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 476);
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
            this.dgvl_finance_code,
            this.dgvl_appno,
            this.dgvl_custno,
            this.dgvl_custname,
            this.dgvl_status,
            this.dgvl_financer,
            this.dgvl_trnx_date});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(935, 456);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellDoubleClick);
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            this.dgv_list.DoubleClick += new System.EventHandler(this.dgv_list_DoubleClick);
            // 
            // dgvl_finance_code
            // 
            this.dgvl_finance_code.DataPropertyName = "financer_code";
            this.dgvl_finance_code.HeaderText = "Financer Code";
            this.dgvl_finance_code.Name = "dgvl_finance_code";
            this.dgvl_finance_code.ReadOnly = true;
            // 
            // dgvl_appno
            // 
            this.dgvl_appno.DataPropertyName = "app_no";
            this.dgvl_appno.HeaderText = "Auto Loan No.";
            this.dgvl_appno.Name = "dgvl_appno";
            this.dgvl_appno.ReadOnly = true;
            // 
            // dgvl_custno
            // 
            this.dgvl_custno.DataPropertyName = "m06_finance_code";
            this.dgvl_custno.HeaderText = "Customer No.";
            this.dgvl_custno.Name = "dgvl_custno";
            this.dgvl_custno.ReadOnly = true;
            // 
            // dgvl_custname
            // 
            this.dgvl_custname.DataPropertyName = "m06_finance_name";
            this.dgvl_custname.HeaderText = "Customer Name";
            this.dgvl_custname.Name = "dgvl_custname";
            this.dgvl_custname.ReadOnly = true;
            this.dgvl_custname.Width = 160;
            // 
            // dgvl_status
            // 
            this.dgvl_status.DataPropertyName = "status";
            this.dgvl_status.HeaderText = "Status";
            this.dgvl_status.Name = "dgvl_status";
            this.dgvl_status.ReadOnly = true;
            // 
            // dgvl_financer
            // 
            this.dgvl_financer.DataPropertyName = "cust_name";
            this.dgvl_financer.HeaderText = "Financer";
            this.dgvl_financer.Name = "dgvl_financer";
            this.dgvl_financer.ReadOnly = true;
            // 
            // dgvl_trnx_date
            // 
            this.dgvl_trnx_date.DataPropertyName = "trnx_date";
            this.dgvl_trnx_date.HeaderText = "Trans Date";
            this.dgvl_trnx_date.Name = "dgvl_trnx_date";
            this.dgvl_trnx_date.ReadOnly = true;
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
            this.groupBox2.Location = new System.Drawing.Point(170, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(941, 86);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter By";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(407, 59);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(60, 15);
            this.label31.TabIndex = 120;
            this.label31.Text = "Customer";
            // 
            // txt_customer
            // 
            this.txt_customer.BackColor = System.Drawing.SystemColors.Window;
            this.txt_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_customer.Location = new System.Drawing.Point(483, 56);
            this.txt_customer.Name = "txt_customer";
            this.txt_customer.Size = new System.Drawing.Size(168, 22);
            this.txt_customer.TabIndex = 121;
            // 
            // btn_filter
            // 
            this.btn_filter.BackColor = System.Drawing.Color.Peru;
            this.btn_filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filter.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_filter.Image = global::Accounting_Application_System.Properties.Resources._1343892246_adept_update;
            this.btn_filter.Location = new System.Drawing.Point(749, 24);
            this.btn_filter.Margin = new System.Windows.Forms.Padding(5);
            this.btn_filter.Name = "btn_filter";
            this.btn_filter.Size = new System.Drawing.Size(136, 39);
            this.btn_filter.TabIndex = 119;
            this.btn_filter.Text = "Refresh";
            this.btn_filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_filter.UseVisualStyleBackColor = false;
            this.btn_filter.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(407, 23);
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
            this.cbo_status.Enabled = false;
            this.cbo_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Items.AddRange(new object[] {
            "Walk-In",
            "Online",
            "Government"});
            this.cbo_status.Location = new System.Drawing.Point(483, 19);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(242, 23);
            this.cbo_status.TabIndex = 118;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 59);
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
            // tbcntrl_left
            // 
            this.tbcntrl_left.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_left.Controls.Add(this.tpg_option);
            this.tbcntrl_left.Controls.Add(this.tpg_option_2);
            this.tbcntrl_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbcntrl_left.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_left.Multiline = true;
            this.tbcntrl_left.Name = "tbcntrl_left";
            this.tbcntrl_left.SelectedIndex = 0;
            this.tbcntrl_left.Size = new System.Drawing.Size(170, 562);
            this.tbcntrl_left.TabIndex = 3;
            // 
            // tpg_option
            // 
            this.tpg_option.Controls.Add(this.panel5);
            this.tpg_option.Location = new System.Drawing.Point(4, 4);
            this.tpg_option.Name = "tpg_option";
            this.tpg_option.Size = new System.Drawing.Size(162, 534);
            this.tpg_option.TabIndex = 2;
            this.tpg_option.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Controls.Add(this.groupBox4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(162, 534);
            this.panel5.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.textBox15);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox5.Location = new System.Drawing.Point(0, 387);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(162, 147);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Look Up";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.DarkGray;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 23);
            this.comboBox1.TabIndex = 61;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Peru;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::Accounting_Application_System.Properties.Resources.search_32x32;
            this.button1.Location = new System.Drawing.Point(4, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 51);
            this.button1.TabIndex = 60;
            this.button1.Text = "Search";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBox15
            // 
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.Location = new System.Drawing.Point(7, 50);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(150, 26);
            this.textBox15.TabIndex = 59;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_print);
            this.groupBox4.Controls.Add(this.btn_cancel);
            this.groupBox4.Controls.Add(this.btn_upd);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox4.Size = new System.Drawing.Size(162, 341);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Main Option";
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.Peru;
            this.btn_print.Enabled = false;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_print.Location = new System.Drawing.Point(5, 254);
            this.btn_print.Margin = new System.Windows.Forms.Padding(5);
            this.btn_print.MinimumSize = new System.Drawing.Size(150, 72);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(150, 72);
            this.btn_print.TabIndex = 3;
            this.btn_print.Text = "(Re)Print";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Maroon;
            this.btn_cancel.Enabled = false;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_cancel.Image = global::Accounting_Application_System.Properties.Resources._1343892237_DeleteRed;
            this.btn_cancel.Location = new System.Drawing.Point(5, 176);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(5);
            this.btn_cancel.MinimumSize = new System.Drawing.Size(150, 72);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(150, 72);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel ";
            this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_cancel.UseVisualStyleBackColor = false;
            // 
            // btn_upd
            // 
            this.btn_upd.BackColor = System.Drawing.Color.Peru;
            this.btn_upd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_upd.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_upd.Location = new System.Drawing.Point(5, 98);
            this.btn_upd.Margin = new System.Windows.Forms.Padding(5);
            this.btn_upd.MinimumSize = new System.Drawing.Size(150, 72);
            this.btn_upd.Name = "btn_upd";
            this.btn_upd.Size = new System.Drawing.Size(150, 72);
            this.btn_upd.TabIndex = 1;
            this.btn_upd.Text = "Update Status";
            this.btn_upd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_upd.UseVisualStyleBackColor = false;
            this.btn_upd.Click += new System.EventHandler(this.btn_upd_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Peru;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.Info;
            this.button2.Location = new System.Drawing.Point(6, 20);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.MinimumSize = new System.Drawing.Size(150, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 72);
            this.button2.TabIndex = 0;
            this.button2.Text = "New Status";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // tpg_option_2
            // 
            this.tpg_option_2.Controls.Add(this.panel9);
            this.tpg_option_2.Location = new System.Drawing.Point(4, 4);
            this.tpg_option_2.Name = "tpg_option_2";
            this.tpg_option_2.Size = new System.Drawing.Size(162, 534);
            this.tpg_option_2.TabIndex = 3;
            this.tpg_option_2.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.AutoSize = true;
            this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel9.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel9.Controls.Add(this.groupBox3);
            this.panel9.Controls.Add(this.btn_exit);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.cbo_outlet);
            this.panel9.Controls.Add(this.cbo_whsname);
            this.panel9.Controls.Add(this.groupBox7);
            this.panel9.Controls.Add(this.btn_saveorder);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.MinimumSize = new System.Drawing.Size(1052, 742);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1052, 742);
            this.panel9.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_payadd);
            this.groupBox3.Location = new System.Drawing.Point(4, 270);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 74);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Order Line Option";
            // 
            // btn_payadd
            // 
            this.btn_payadd.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_payadd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_payadd.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_payadd.Location = new System.Drawing.Point(6, 20);
            this.btn_payadd.Name = "btn_payadd";
            this.btn_payadd.Size = new System.Drawing.Size(150, 40);
            this.btn_payadd.TabIndex = 8;
            this.btn_payadd.Text = "Payment";
            this.btn_payadd.UseVisualStyleBackColor = false;
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.SystemColors.Info;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_exit.Image = global::Accounting_Application_System.Properties.Resources._1343907460_go_back;
            this.btn_exit.Location = new System.Drawing.Point(10, 400);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(148, 47);
            this.btn_exit.TabIndex = 87;
            this.btn_exit.Text = "Go Back";
            this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_exit.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 15);
            this.label8.TabIndex = 86;
            this.label8.Text = "Stock Location";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 85;
            this.label5.Text = "Outlet Type";
            // 
            // cbo_outlet
            // 
            this.cbo_outlet.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cbo_outlet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_outlet.Enabled = false;
            this.cbo_outlet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_outlet.FormattingEnabled = true;
            this.cbo_outlet.Location = new System.Drawing.Point(5, 25);
            this.cbo_outlet.Name = "cbo_outlet";
            this.cbo_outlet.Size = new System.Drawing.Size(156, 24);
            this.cbo_outlet.TabIndex = 84;
            // 
            // cbo_whsname
            // 
            this.cbo_whsname.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cbo_whsname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_whsname.Enabled = false;
            this.cbo_whsname.FormattingEnabled = true;
            this.cbo_whsname.Location = new System.Drawing.Point(6, 73);
            this.cbo_whsname.Name = "cbo_whsname";
            this.cbo_whsname.Size = new System.Drawing.Size(156, 23);
            this.cbo_whsname.TabIndex = 80;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_additem);
            this.groupBox7.Controls.Add(this.btn_delitem);
            this.groupBox7.Controls.Add(this.btn_upditem);
            this.groupBox7.Location = new System.Drawing.Point(5, 102);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(165, 162);
            this.groupBox7.TabIndex = 44;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Order Line Option";
            // 
            // btn_additem
            // 
            this.btn_additem.BackColor = System.Drawing.Color.Peru;
            this.btn_additem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_additem.Location = new System.Drawing.Point(6, 20);
            this.btn_additem.Name = "btn_additem";
            this.btn_additem.Size = new System.Drawing.Size(150, 40);
            this.btn_additem.TabIndex = 8;
            this.btn_additem.Text = "Add Item";
            this.btn_additem.UseVisualStyleBackColor = false;
            // 
            // btn_delitem
            // 
            this.btn_delitem.BackColor = System.Drawing.Color.Maroon;
            this.btn_delitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delitem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_delitem.Location = new System.Drawing.Point(6, 112);
            this.btn_delitem.Name = "btn_delitem";
            this.btn_delitem.Size = new System.Drawing.Size(150, 40);
            this.btn_delitem.TabIndex = 7;
            this.btn_delitem.Text = "Remove Item";
            this.btn_delitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_delitem.UseVisualStyleBackColor = false;
            // 
            // btn_upditem
            // 
            this.btn_upditem.BackColor = System.Drawing.Color.Peru;
            this.btn_upditem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_upditem.Location = new System.Drawing.Point(6, 66);
            this.btn_upditem.Name = "btn_upditem";
            this.btn_upditem.Size = new System.Drawing.Size(150, 40);
            this.btn_upditem.TabIndex = 1;
            this.btn_upditem.Text = "Update Item";
            this.btn_upditem.UseVisualStyleBackColor = false;
            // 
            // btn_saveorder
            // 
            this.btn_saveorder.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_saveorder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_saveorder.Image = global::Accounting_Application_System.Properties.Resources._1343908142_database_save;
            this.btn_saveorder.Location = new System.Drawing.Point(10, 350);
            this.btn_saveorder.Name = "btn_saveorder";
            this.btn_saveorder.Size = new System.Drawing.Size(150, 44);
            this.btn_saveorder.TabIndex = 47;
            this.btn_saveorder.Text = "Save As Pending";
            this.btn_saveorder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_saveorder.UseVisualStyleBackColor = false;
            // 
            // s_AutoLoanStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1111, 562);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbcntrl_left);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "s_AutoLoanStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Loan Status";
            this.Load += new System.EventHandler(this.s_AutoLoanStatus_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tbcntrl_left.ResumeLayout(false);
            this.tpg_option.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tpg_option_2.ResumeLayout(false);
            this.tpg_option_2.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tbcntrl_left;
        private System.Windows.Forms.TabPage tpg_option;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_upd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tpg_option_2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_payadd;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbo_outlet;
        private System.Windows.Forms.ComboBox cbo_whsname;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_additem;
        private System.Windows.Forms.Button btn_delitem;
        private System.Windows.Forms.Button btn_upditem;
        private System.Windows.Forms.Button btn_saveorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_finance_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_appno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_custno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_custname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_financer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_trnx_date;
    }
}