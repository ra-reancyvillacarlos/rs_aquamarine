namespace Accounting_Application_System
{
    partial class rpt_supplierledger
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_view = new System.Windows.Forms.Button();
            this.txt_invoice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_trnxdateasof = new System.Windows.Forms.DateTimePicker();
            this.cbo_subsidiary = new System.Windows.Forms.ComboBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.cbo_accttitle = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.t_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ln_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txt_balance = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_total_credit = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_total_debit = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_print);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_view);
            this.groupBox4.Controls.Add(this.txt_invoice);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.dtp_trnxdateasof);
            this.groupBox4.Controls.Add(this.cbo_subsidiary);
            this.groupBox4.Controls.Add(this.btn_generate);
            this.groupBox4.Controls.Add(this.cbo_accttitle);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(703, 109);
            this.groupBox4.TabIndex = 75;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sub Ledger Options";
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.Peru;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_print.Image = global::Accounting_Application_System.Properties.Resources.document_edit___32;
            this.btn_print.Location = new System.Drawing.Point(573, 19);
            this.btn_print.Margin = new System.Windows.Forms.Padding(2);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(120, 40);
            this.btn_print.TabIndex = 1;
            this.btn_print.Text = "Print";
            this.btn_print.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(298, 61);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "Supplier Invoice";
            // 
            // btn_view
            // 
            this.btn_view.BackColor = System.Drawing.Color.Peru;
            this.btn_view.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_view.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_view.Location = new System.Drawing.Point(449, 61);
            this.btn_view.Margin = new System.Windows.Forms.Padding(2);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(120, 40);
            this.btn_view.TabIndex = 0;
            this.btn_view.Text = "View Entry";
            this.btn_view.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_view.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_view.UseVisualStyleBackColor = false;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // txt_invoice
            // 
            this.txt_invoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_invoice.Location = new System.Drawing.Point(301, 80);
            this.txt_invoice.Name = "txt_invoice";
            this.txt_invoice.Size = new System.Drawing.Size(127, 21);
            this.txt_invoice.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 61;
            this.label1.Text = "As of";
            // 
            // dtp_trnxdateasof
            // 
            this.dtp_trnxdateasof.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_trnxdateasof.Location = new System.Drawing.Point(301, 33);
            this.dtp_trnxdateasof.Name = "dtp_trnxdateasof";
            this.dtp_trnxdateasof.Size = new System.Drawing.Size(127, 21);
            this.dtp_trnxdateasof.TabIndex = 60;
            // 
            // cbo_subsidiary
            // 
            this.cbo_subsidiary.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_subsidiary.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_subsidiary.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_subsidiary.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbo_subsidiary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_subsidiary.FormattingEnabled = true;
            this.cbo_subsidiary.Location = new System.Drawing.Point(12, 79);
            this.cbo_subsidiary.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_subsidiary.Name = "cbo_subsidiary";
            this.cbo_subsidiary.Size = new System.Drawing.Size(283, 23);
            this.cbo_subsidiary.TabIndex = 59;
            this.cbo_subsidiary.SelectedIndexChanged += new System.EventHandler(this.cbo_subsidiary_SelectedIndexChanged);
            // 
            // btn_generate
            // 
            this.btn_generate.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_generate.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_generate.Location = new System.Drawing.Point(449, 19);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(2);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(120, 40);
            this.btn_generate.TabIndex = 0;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = false;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // cbo_accttitle
            // 
            this.cbo_accttitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_accttitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_accttitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_accttitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_accttitle.FormattingEnabled = true;
            this.cbo_accttitle.Location = new System.Drawing.Point(12, 35);
            this.cbo_accttitle.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_accttitle.Name = "cbo_accttitle";
            this.cbo_accttitle.Size = new System.Drawing.Size(283, 23);
            this.cbo_accttitle.TabIndex = 58;
            this.cbo_accttitle.SelectedIndexChanged += new System.EventHandler(this.cbo_accttitle_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(9, 61);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(53, 15);
            this.label39.TabIndex = 57;
            this.label39.Text = "Supplier";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(10, 17);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(121, 15);
            this.label38.TabIndex = 55;
            this.label38.Text = "Sub-legdger Account";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_list);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(0, 109);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(1037, 553);
            this.groupBox6.TabIndex = 74;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Supplier Ledger List";
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToResizeColumns = false;
            this.dgv_list.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_list.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_list.ColumnHeadersHeight = 28;
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.t_date,
            this.invoice,
            this.j_code,
            this.j_num,
            this.descripton,
            this.debit,
            this.credit,
            this.balance,
            this.ln_desc,
            this.fy,
            this.mo});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(2, 16);
            this.dgv_list.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(1033, 535);
            this.dgv_list.TabIndex = 1;
            // 
            // t_date
            // 
            this.t_date.HeaderText = "DATE";
            this.t_date.Name = "t_date";
            this.t_date.ReadOnly = true;
            this.t_date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // invoice
            // 
            this.invoice.HeaderText = "SYS. INV.";
            this.invoice.Name = "invoice";
            this.invoice.ReadOnly = true;
            this.invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.invoice.Width = 77;
            // 
            // j_code
            // 
            this.j_code.HeaderText = "JRNL";
            this.j_code.Name = "j_code";
            this.j_code.ReadOnly = true;
            this.j_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.j_code.Width = 77;
            // 
            // j_num
            // 
            this.j_num.HeaderText = "JRNL. #";
            this.j_num.Name = "j_num";
            this.j_num.ReadOnly = true;
            this.j_num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.j_num.Width = 77;
            // 
            // descripton
            // 
            this.descripton.HeaderText = "SUP. INVOICE";
            this.descripton.Name = "descripton";
            this.descripton.ReadOnly = true;
            this.descripton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.descripton.Width = 120;
            // 
            // debit
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.debit.DefaultCellStyle = dataGridViewCellStyle2;
            this.debit.HeaderText = "PAID(DR)";
            this.debit.Name = "debit";
            this.debit.ReadOnly = true;
            this.debit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // credit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.credit.DefaultCellStyle = dataGridViewCellStyle3;
            this.credit.HeaderText = "CREDIT(CR)";
            this.credit.Name = "credit";
            this.credit.ReadOnly = true;
            this.credit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // balance
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.balance.DefaultCellStyle = dataGridViewCellStyle4;
            this.balance.HeaderText = "BALANCE";
            this.balance.Name = "balance";
            this.balance.ReadOnly = true;
            this.balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.balance.Width = 120;
            // 
            // ln_desc
            // 
            this.ln_desc.HeaderText = "REMARK";
            this.ln_desc.Name = "ln_desc";
            this.ln_desc.ReadOnly = true;
            this.ln_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ln_desc.Width = 250;
            // 
            // fy
            // 
            this.fy.HeaderText = "YEAR";
            this.fy.Name = "fy";
            this.fy.ReadOnly = true;
            this.fy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fy.Width = 77;
            // 
            // mo
            // 
            this.mo.HeaderText = "MONTH";
            this.mo.Name = "mo";
            this.mo.ReadOnly = true;
            this.mo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mo.Width = 77;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1037, 109);
            this.panel1.TabIndex = 75;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(703, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 109);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total Amount Summary";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txt_balance);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 77);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(328, 30);
            this.panel4.TabIndex = 74;
            // 
            // txt_balance
            // 
            this.txt_balance.BackColor = System.Drawing.Color.DarkKhaki;
            this.txt_balance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_balance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_balance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_balance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_balance.Location = new System.Drawing.Point(69, 0);
            this.txt_balance.Name = "txt_balance";
            this.txt_balance.ReadOnly = true;
            this.txt_balance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_balance.Size = new System.Drawing.Size(259, 24);
            this.txt_balance.TabIndex = 75;
            this.txt_balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(69, 30);
            this.panel7.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "Balance";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_total_credit);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(328, 30);
            this.panel3.TabIndex = 74;
            // 
            // txt_total_credit
            // 
            this.txt_total_credit.BackColor = System.Drawing.Color.DarkKhaki;
            this.txt_total_credit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_total_credit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_total_credit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_credit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_total_credit.Location = new System.Drawing.Point(71, 0);
            this.txt_total_credit.Name = "txt_total_credit";
            this.txt_total_credit.ReadOnly = true;
            this.txt_total_credit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_total_credit.Size = new System.Drawing.Size(257, 24);
            this.txt_total_credit.TabIndex = 74;
            this.txt_total_credit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(71, 30);
            this.panel6.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 67;
            this.label5.Text = "Total Credit";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txt_total_debit);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 30);
            this.panel2.TabIndex = 1;
            // 
            // txt_total_debit
            // 
            this.txt_total_debit.BackColor = System.Drawing.Color.DarkKhaki;
            this.txt_total_debit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_total_debit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_total_debit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_debit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_total_debit.Location = new System.Drawing.Point(71, 0);
            this.txt_total_debit.Name = "txt_total_debit";
            this.txt_total_debit.ReadOnly = true;
            this.txt_total_debit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_total_debit.Size = new System.Drawing.Size(257, 24);
            this.txt_total_debit.TabIndex = 73;
            this.txt_total_debit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(71, 30);
            this.panel5.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 67;
            this.label2.Text = "Total Debit";
            // 
            // rpt_supplierledger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1037, 662);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimizeBox = false;
            this.Name = "rpt_supplierledger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUPPLIER LEDGER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rpt_supplierledger_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_view;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_trnxdateasof;
        private System.Windows.Forms.ComboBox cbo_subsidiary;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.ComboBox cbo_accttitle;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_invoice;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txt_balance;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txt_total_credit;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_total_debit;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn t_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn j_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripton;
        private System.Windows.Forms.DataGridViewTextBoxColumn debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ln_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fy;
        private System.Windows.Forms.DataGridViewTextBoxColumn mo;
    }
}