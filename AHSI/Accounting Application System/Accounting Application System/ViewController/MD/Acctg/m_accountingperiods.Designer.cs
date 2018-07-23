namespace Accounting_Application_System
{
    partial class m_accountingperiods
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbcntrl_option = new System.Windows.Forms.TabControl();
            this.tpg_opt_1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbo_viewas = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_searchby = new System.Windows.Forms.ComboBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_additem = new System.Windows.Forms.Button();
            this.btn_closeitem = new System.Windows.Forms.Button();
            this.btn_viewitem = new System.Windows.Forms.Button();
            this.tpg_opt_2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.tbcntrl_main = new System.Windows.Forms.TabControl();
            this.tpg_list = new System.Windows.Forms.TabPage();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.fy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpg_info = new System.Windows.Forms.TabPage();
            this.txt_mo = new System.Windows.Forms.TextBox();
            this.txt_fy = new System.Windows.Forms.TextBox();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_month = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tbcntrl_option.SuspendLayout();
            this.tpg_opt_1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpg_opt_2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbcntrl_main.SuspendLayout();
            this.tpg_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.tpg_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.tbcntrl_option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 530);
            this.panel1.TabIndex = 4;
            // 
            // tbcntrl_option
            // 
            this.tbcntrl_option.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_option.Controls.Add(this.tpg_opt_1);
            this.tbcntrl_option.Controls.Add(this.tpg_opt_2);
            this.tbcntrl_option.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_option.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_option.Name = "tbcntrl_option";
            this.tbcntrl_option.SelectedIndex = 0;
            this.tbcntrl_option.Size = new System.Drawing.Size(221, 530);
            this.tbcntrl_option.TabIndex = 49;
            this.tbcntrl_option.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_option_Selecting);
            // 
            // tpg_opt_1
            // 
            this.tpg_opt_1.Controls.Add(this.panel3);
            this.tpg_opt_1.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_1.Name = "tpg_opt_1";
            this.tpg_opt_1.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_1.Size = new System.Drawing.Size(213, 502);
            this.tpg_opt_1.TabIndex = 0;
            this.tpg_opt_1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 496);
            this.panel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbo_viewas);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Location = new System.Drawing.Point(14, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 71);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Periods";
            // 
            // cbo_viewas
            // 
            this.cbo_viewas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_viewas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_viewas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_viewas.FormattingEnabled = true;
            this.cbo_viewas.Items.AddRange(new object[] {
            "All Periods",
            "Open Periods Only",
            "Closed Periods Only"});
            this.cbo_viewas.Location = new System.Drawing.Point(12, 33);
            this.cbo_viewas.Name = "cbo_viewas";
            this.cbo_viewas.Size = new System.Drawing.Size(160, 23);
            this.cbo_viewas.TabIndex = 10;
            this.cbo_viewas.SelectedIndexChanged += new System.EventHandler(this.cbo_viewas_SelectedIndexChanged);
            this.cbo_viewas.SelectedValueChanged += new System.EventHandler(this.cbo_viewas_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_searchby);
            this.groupBox2.Controls.Add(this.txt_search);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Location = new System.Drawing.Point(14, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 159);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Option";
            // 
            // cbo_searchby
            // 
            this.cbo_searchby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_searchby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_searchby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_searchby.FormattingEnabled = true;
            this.cbo_searchby.Items.AddRange(new object[] {
            "Year",
            "Month"});
            this.cbo_searchby.Location = new System.Drawing.Point(11, 26);
            this.cbo_searchby.Name = "cbo_searchby";
            this.cbo_searchby.Size = new System.Drawing.Size(160, 23);
            this.cbo_searchby.TabIndex = 10;
            this.cbo_searchby.SelectedIndexChanged += new System.EventHandler(this.cbo_searchby_SelectedIndexChanged);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(11, 65);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(160, 21);
            this.txt_search.TabIndex = 9;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Location = new System.Drawing.Point(11, 93);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(160, 55);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "Search Now";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_additem);
            this.groupBox7.Controls.Add(this.btn_closeitem);
            this.groupBox7.Controls.Add(this.btn_viewitem);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox7.Location = new System.Drawing.Point(14, 83);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 210);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Main Option";
            // 
            // btn_additem
            // 
            this.btn_additem.BackColor = System.Drawing.Color.Peru;
            this.btn_additem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_additem.Location = new System.Drawing.Point(12, 21);
            this.btn_additem.Name = "btn_additem";
            this.btn_additem.Size = new System.Drawing.Size(160, 55);
            this.btn_additem.TabIndex = 8;
            this.btn_additem.Text = "Add New";
            this.btn_additem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_additem.UseVisualStyleBackColor = false;
            this.btn_additem.Click += new System.EventHandler(this.btn_additem_Click);
            // 
            // btn_closeitem
            // 
            this.btn_closeitem.BackColor = System.Drawing.Color.Brown;
            this.btn_closeitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_closeitem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_closeitem.Location = new System.Drawing.Point(12, 143);
            this.btn_closeitem.Name = "btn_closeitem";
            this.btn_closeitem.Size = new System.Drawing.Size(160, 55);
            this.btn_closeitem.TabIndex = 7;
            this.btn_closeitem.Text = "Close Period";
            this.btn_closeitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_closeitem.UseVisualStyleBackColor = false;
            this.btn_closeitem.Click += new System.EventHandler(this.btn_closeitem_Click);
            // 
            // btn_viewitem
            // 
            this.btn_viewitem.BackColor = System.Drawing.Color.Peru;
            this.btn_viewitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_viewitem.Location = new System.Drawing.Point(12, 82);
            this.btn_viewitem.Name = "btn_viewitem";
            this.btn_viewitem.Size = new System.Drawing.Size(160, 55);
            this.btn_viewitem.TabIndex = 1;
            this.btn_viewitem.Text = "View";
            this.btn_viewitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_viewitem.UseVisualStyleBackColor = false;
            this.btn_viewitem.Click += new System.EventHandler(this.btn_viewitem_Click);
            // 
            // tpg_opt_2
            // 
            this.tpg_opt_2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tpg_opt_2.Controls.Add(this.panel4);
            this.tpg_opt_2.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_2.Name = "tpg_opt_2";
            this.tpg_opt_2.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_2.Size = new System.Drawing.Size(213, 502);
            this.tpg_opt_2.TabIndex = 1;
            this.tpg_opt_2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 496);
            this.panel4.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_save);
            this.groupBox3.Controls.Add(this.btn_back);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox3.Location = new System.Drawing.Point(15, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 130);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Peru;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save.Location = new System.Drawing.Point(12, 21);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(160, 46);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Peru;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Location = new System.Drawing.Point(12, 73);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(160, 46);
            this.btn_back.TabIndex = 1;
            this.btn_back.Text = "Back";
            this.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // tbcntrl_main
            // 
            this.tbcntrl_main.Controls.Add(this.tpg_list);
            this.tbcntrl_main.Controls.Add(this.tpg_info);
            this.tbcntrl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_main.Location = new System.Drawing.Point(221, 0);
            this.tbcntrl_main.Name = "tbcntrl_main";
            this.tbcntrl_main.SelectedIndex = 0;
            this.tbcntrl_main.Size = new System.Drawing.Size(549, 530);
            this.tbcntrl_main.TabIndex = 5;
            this.tbcntrl_main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_main_Selecting);
            // 
            // tpg_list
            // 
            this.tpg_list.Controls.Add(this.dgv_list);
            this.tpg_list.Location = new System.Drawing.Point(4, 24);
            this.tpg_list.Name = "tpg_list";
            this.tpg_list.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_list.Size = new System.Drawing.Size(541, 502);
            this.tpg_list.TabIndex = 1;
            this.tpg_list.Text = "Accounting Periods List";
            this.tpg_list.UseVisualStyleBackColor = true;
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fy,
            this.month_desc,
            this.closed,
            this.from,
            this.to,
            this.mo});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 3);
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 25;
            this.dgv_list.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(535, 496);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // fy
            // 
            this.fy.HeaderText = "YEAR";
            this.fy.Name = "fy";
            this.fy.ReadOnly = true;
            this.fy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fy.Width = 77;
            // 
            // month_desc
            // 
            this.month_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.month_desc.HeaderText = "MONTH";
            this.month_desc.Name = "month_desc";
            this.month_desc.ReadOnly = true;
            this.month_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // closed
            // 
            this.closed.HeaderText = "CLOSED?";
            this.closed.Name = "closed";
            this.closed.ReadOnly = true;
            this.closed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.closed.Width = 77;
            // 
            // from
            // 
            this.from.HeaderText = "FROM";
            this.from.Name = "from";
            this.from.ReadOnly = true;
            this.from.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.from.Width = 70;
            // 
            // to
            // 
            this.to.HeaderText = "TO";
            this.to.Name = "to";
            this.to.ReadOnly = true;
            this.to.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.to.Width = 70;
            // 
            // mo
            // 
            this.mo.HeaderText = "MO";
            this.mo.Name = "mo";
            this.mo.ReadOnly = true;
            this.mo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mo.Width = 50;
            // 
            // tpg_info
            // 
            this.tpg_info.Controls.Add(this.txt_mo);
            this.tpg_info.Controls.Add(this.txt_fy);
            this.tpg_info.Controls.Add(this.dtp_to);
            this.tpg_info.Controls.Add(this.label2);
            this.tpg_info.Controls.Add(this.dtp_frm);
            this.tpg_info.Controls.Add(this.label1);
            this.tpg_info.Controls.Add(this.cbo_month);
            this.tpg_info.Controls.Add(this.label8);
            this.tpg_info.Controls.Add(this.label10);
            this.tpg_info.Location = new System.Drawing.Point(4, 24);
            this.tpg_info.Name = "tpg_info";
            this.tpg_info.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_info.Size = new System.Drawing.Size(541, 502);
            this.tpg_info.TabIndex = 2;
            this.tpg_info.Text = "Accounting Periods Info";
            this.tpg_info.UseVisualStyleBackColor = true;
            // 
            // txt_mo
            // 
            this.txt_mo.Location = new System.Drawing.Point(239, 28);
            this.txt_mo.Name = "txt_mo";
            this.txt_mo.Size = new System.Drawing.Size(61, 21);
            this.txt_mo.TabIndex = 63;
            // 
            // txt_fy
            // 
            this.txt_fy.Location = new System.Drawing.Point(111, 28);
            this.txt_fy.Name = "txt_fy";
            this.txt_fy.Size = new System.Drawing.Size(108, 21);
            this.txt_fy.TabIndex = 61;
            // 
            // dtp_to
            // 
            this.dtp_to.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dtp_to.CalendarTitleForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(282, 94);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(108, 21);
            this.dtp_to.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "TO";
            // 
            // dtp_frm
            // 
            this.dtp_frm.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dtp_frm.CalendarTitleForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(111, 94);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(108, 21);
            this.dtp_frm.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 56;
            this.label1.Text = "DATE RANGE";
            // 
            // cbo_month
            // 
            this.cbo_month.BackColor = System.Drawing.SystemColors.Control;
            this.cbo_month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_month.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_month.FormattingEnabled = true;
            this.cbo_month.Location = new System.Drawing.Point(111, 56);
            this.cbo_month.Name = "cbo_month";
            this.cbo_month.Size = new System.Drawing.Size(293, 23);
            this.cbo_month.TabIndex = 55;
            this.cbo_month.SelectedIndexChanged += new System.EventHandler(this.cbo_monthperiod_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "YEAR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 33;
            this.label10.Text = "MONTH";
            // 
            // m_accountingperiods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(770, 530);
            this.Controls.Add(this.tbcntrl_main);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "m_accountingperiods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounting Periods";
            this.Load += new System.EventHandler(this.m_accountingperiods_Load);
            this.panel1.ResumeLayout(false);
            this.tbcntrl_option.ResumeLayout(false);
            this.tpg_opt_1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tpg_opt_2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tbcntrl_main.ResumeLayout(false);
            this.tpg_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.tpg_info.ResumeLayout(false);
            this.tpg_info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbcntrl_option;
        private System.Windows.Forms.TabPage tpg_opt_1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbo_searchby;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_additem;
        private System.Windows.Forms.Button btn_closeitem;
        private System.Windows.Forms.Button btn_viewitem;
        private System.Windows.Forms.TabPage tpg_opt_2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.TabControl tbcntrl_main;
        private System.Windows.Forms.TabPage tpg_list;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.TabPage tpg_info;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_month;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbo_viewas;
        private System.Windows.Forms.TextBox txt_fy;
        private System.Windows.Forms.DataGridViewTextBoxColumn fy;
        private System.Windows.Forms.DataGridViewTextBoxColumn month_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn closed;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn mo;
        private System.Windows.Forms.TextBox txt_mo;

    }
}