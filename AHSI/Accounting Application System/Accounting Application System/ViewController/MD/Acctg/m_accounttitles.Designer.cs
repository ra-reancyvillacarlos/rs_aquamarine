namespace Accounting_Application_System
{
    partial class m_accounttitles
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_searchby = new System.Windows.Forms.ComboBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_excel = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_additem = new System.Windows.Forms.Button();
            this.btn_delitem = new System.Windows.Forms.Button();
            this.btn_upditem = new System.Windows.Forms.Button();
            this.tpg_opt_2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.tpg_opt_3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_import = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tpg_info = new System.Windows.Forms.TabPage();
            this.chk_payment = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_cib_acct = new System.Windows.Forms.CheckBox();
            this.chk_sl = new System.Windows.Forms.CheckBox();
            this.cbo_mag_code = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_at_desc = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tpg_list = new System.Windows.Forms.TabPage();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.at_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.at_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_pl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dr_cr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cib_acct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acc_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcntrl_main = new System.Windows.Forms.TabControl();
            this.tpg_import = new System.Windows.Forms.TabPage();
            this.lbl_max = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_min = new System.Windows.Forms.Label();
            this.txt_filename = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.importBgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.tbcntrl_option.SuspendLayout();
            this.tpg_opt_1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpg_opt_2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpg_opt_3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpg_info.SuspendLayout();
            this.tpg_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.tbcntrl_main.SuspendLayout();
            this.tpg_import.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.tbcntrl_option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 607);
            this.panel1.TabIndex = 4;
            // 
            // tbcntrl_option
            // 
            this.tbcntrl_option.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_option.Controls.Add(this.tpg_opt_1);
            this.tbcntrl_option.Controls.Add(this.tpg_opt_2);
            this.tbcntrl_option.Controls.Add(this.tpg_opt_3);
            this.tbcntrl_option.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_option.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_option.Name = "tbcntrl_option";
            this.tbcntrl_option.SelectedIndex = 0;
            this.tbcntrl_option.Size = new System.Drawing.Size(221, 607);
            this.tbcntrl_option.TabIndex = 49;
            this.tbcntrl_option.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_option_Selecting);
            // 
            // tpg_opt_1
            // 
            this.tpg_opt_1.Controls.Add(this.panel3);
            this.tpg_opt_1.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_1.Name = "tpg_opt_1";
            this.tpg_opt_1.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_1.Size = new System.Drawing.Size(213, 579);
            this.tpg_opt_1.TabIndex = 0;
            this.tpg_opt_1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 573);
            this.panel3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_searchby);
            this.groupBox2.Controls.Add(this.txt_search);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Location = new System.Drawing.Point(15, 377);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 165);
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
            "ID",
            "Name"});
            this.cbo_searchby.Location = new System.Drawing.Point(12, 33);
            this.cbo_searchby.Name = "cbo_searchby";
            this.cbo_searchby.Size = new System.Drawing.Size(160, 23);
            this.cbo_searchby.TabIndex = 10;
            this.cbo_searchby.SelectedIndexChanged += new System.EventHandler(this.cbo_searchby_SelectedIndexChanged);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(12, 72);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(160, 21);
            this.txt_search.TabIndex = 9;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Location = new System.Drawing.Point(12, 100);
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
            this.groupBox7.Controls.Add(this.btn_excel);
            this.groupBox7.Controls.Add(this.btn_print);
            this.groupBox7.Controls.Add(this.btn_additem);
            this.groupBox7.Controls.Add(this.btn_delitem);
            this.groupBox7.Controls.Add(this.btn_upditem);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox7.Location = new System.Drawing.Point(15, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 341);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Main Option";
            // 
            // btn_excel
            // 
            this.btn_excel.BackColor = System.Drawing.Color.Peru;
            this.btn_excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_excel.Location = new System.Drawing.Point(12, 202);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(160, 55);
            this.btn_excel.TabIndex = 11;
            this.btn_excel.Text = "Import/ Excel";
            this.btn_excel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_excel.UseVisualStyleBackColor = false;
            this.btn_excel.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.Peru;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_print.Location = new System.Drawing.Point(12, 263);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(160, 55);
            this.btn_print.TabIndex = 9;
            this.btn_print.Text = "Print List";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
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
            // btn_delitem
            // 
            this.btn_delitem.BackColor = System.Drawing.Color.Maroon;
            this.btn_delitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delitem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_delitem.Location = new System.Drawing.Point(12, 142);
            this.btn_delitem.Name = "btn_delitem";
            this.btn_delitem.Size = new System.Drawing.Size(160, 55);
            this.btn_delitem.TabIndex = 7;
            this.btn_delitem.Text = "Delete";
            this.btn_delitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_delitem.UseVisualStyleBackColor = false;
            this.btn_delitem.Click += new System.EventHandler(this.btn_delitem_Click);
            // 
            // btn_upditem
            // 
            this.btn_upditem.BackColor = System.Drawing.Color.Peru;
            this.btn_upditem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_upditem.Location = new System.Drawing.Point(12, 82);
            this.btn_upditem.Name = "btn_upditem";
            this.btn_upditem.Size = new System.Drawing.Size(160, 55);
            this.btn_upditem.TabIndex = 1;
            this.btn_upditem.Text = "Update";
            this.btn_upditem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_upditem.UseVisualStyleBackColor = false;
            this.btn_upditem.Click += new System.EventHandler(this.btn_upditem_Click);
            // 
            // tpg_opt_2
            // 
            this.tpg_opt_2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tpg_opt_2.Controls.Add(this.panel4);
            this.tpg_opt_2.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_2.Name = "tpg_opt_2";
            this.tpg_opt_2.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_2.Size = new System.Drawing.Size(213, 579);
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
            this.panel4.Size = new System.Drawing.Size(207, 573);
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
            // tpg_opt_3
            // 
            this.tpg_opt_3.Controls.Add(this.panel2);
            this.tpg_opt_3.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_3.Name = "tpg_opt_3";
            this.tpg_opt_3.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_3.Size = new System.Drawing.Size(213, 579);
            this.tpg_opt_3.TabIndex = 2;
            this.tpg_opt_3.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 573);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_import);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Location = new System.Drawing.Point(15, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 128);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // btn_import
            // 
            this.btn_import.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_import.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_import.Location = new System.Drawing.Point(12, 21);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(160, 46);
            this.btn_import.TabIndex = 11;
            this.btn_import.Text = "Import";
            this.btn_import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_import.UseVisualStyleBackColor = false;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Peru;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(12, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "Back";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tpg_info
            // 
            this.tpg_info.Controls.Add(this.chk_payment);
            this.tpg_info.Controls.Add(this.label5);
            this.tpg_info.Controls.Add(this.label3);
            this.tpg_info.Controls.Add(this.chk_cib_acct);
            this.tpg_info.Controls.Add(this.chk_sl);
            this.tpg_info.Controls.Add(this.cbo_mag_code);
            this.tpg_info.Controls.Add(this.label2);
            this.tpg_info.Controls.Add(this.label1);
            this.tpg_info.Controls.Add(this.txt_at_desc);
            this.tpg_info.Controls.Add(this.txt_code);
            this.tpg_info.Controls.Add(this.label8);
            this.tpg_info.Controls.Add(this.label10);
            this.tpg_info.Location = new System.Drawing.Point(4, 24);
            this.tpg_info.Name = "tpg_info";
            this.tpg_info.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_info.Size = new System.Drawing.Size(599, 579);
            this.tpg_info.TabIndex = 2;
            this.tpg_info.Text = "Account Title Info";
            this.tpg_info.UseVisualStyleBackColor = true;
            // 
            // chk_payment
            // 
            this.chk_payment.AutoSize = true;
            this.chk_payment.Location = new System.Drawing.Point(140, 181);
            this.chk_payment.Name = "chk_payment";
            this.chk_payment.Size = new System.Drawing.Size(74, 19);
            this.chk_payment.TabIndex = 55;
            this.chk_payment.Text = "Payment";
            this.chk_payment.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(439, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 20);
            this.label5.TabIndex = 54;
            this.label5.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(439, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "*";
            // 
            // chk_cib_acct
            // 
            this.chk_cib_acct.AutoSize = true;
            this.chk_cib_acct.Location = new System.Drawing.Point(140, 156);
            this.chk_cib_acct.Name = "chk_cib_acct";
            this.chk_cib_acct.Size = new System.Drawing.Size(99, 19);
            this.chk_cib_acct.TabIndex = 52;
            this.chk_cib_acct.Text = "Checkwritting";
            this.chk_cib_acct.UseVisualStyleBackColor = true;
            // 
            // chk_sl
            // 
            this.chk_sl.AutoSize = true;
            this.chk_sl.Location = new System.Drawing.Point(140, 131);
            this.chk_sl.Name = "chk_sl";
            this.chk_sl.Size = new System.Drawing.Size(129, 19);
            this.chk_sl.TabIndex = 51;
            this.chk_sl.Text = "Subledger Account";
            this.chk_sl.UseVisualStyleBackColor = true;
            // 
            // cbo_mag_code
            // 
            this.cbo_mag_code.BackColor = System.Drawing.SystemColors.Control;
            this.cbo_mag_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_mag_code.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_mag_code.FormattingEnabled = true;
            this.cbo_mag_code.Location = new System.Drawing.Point(140, 97);
            this.cbo_mag_code.Name = "cbo_mag_code";
            this.cbo_mag_code.Size = new System.Drawing.Size(293, 23);
            this.cbo_mag_code.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "Account Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(254, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "*";
            // 
            // txt_at_desc
            // 
            this.txt_at_desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_at_desc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_at_desc.Location = new System.Drawing.Point(140, 70);
            this.txt_at_desc.Name = "txt_at_desc";
            this.txt_at_desc.Size = new System.Drawing.Size(293, 21);
            this.txt_at_desc.TabIndex = 36;
            // 
            // txt_code
            // 
            this.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_code.Location = new System.Drawing.Point(140, 42);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(108, 21);
            this.txt_code.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Account Title ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 15);
            this.label10.TabIndex = 33;
            this.label10.Text = "Account Title Name";
            // 
            // tpg_list
            // 
            this.tpg_list.Controls.Add(this.dgv_list);
            this.tpg_list.Location = new System.Drawing.Point(4, 24);
            this.tpg_list.Name = "tpg_list";
            this.tpg_list.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_list.Size = new System.Drawing.Size(599, 579);
            this.tpg_list.TabIndex = 1;
            this.tpg_list.Text = "Account Title List";
            this.tpg_list.UseVisualStyleBackColor = true;
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.at_code,
            this.at_desc,
            this.bs_pl,
            this.dr_cr,
            this.sl,
            this.cib_acct,
            this.acc_code,
            this.payment});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 3);
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 25;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(593, 573);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // at_code
            // 
            this.at_code.FillWeight = 77F;
            this.at_code.HeaderText = "ID";
            this.at_code.Name = "at_code";
            this.at_code.ReadOnly = true;
            this.at_code.Width = 77;
            // 
            // at_desc
            // 
            this.at_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.at_desc.HeaderText = "NAME";
            this.at_desc.Name = "at_desc";
            this.at_desc.ReadOnly = true;
            // 
            // bs_pl
            // 
            this.bs_pl.FillWeight = 250F;
            this.bs_pl.HeaderText = "BS PL";
            this.bs_pl.Name = "bs_pl";
            this.bs_pl.ReadOnly = true;
            this.bs_pl.Visible = false;
            this.bs_pl.Width = 250;
            // 
            // dr_cr
            // 
            this.dr_cr.HeaderText = "TYPE";
            this.dr_cr.Name = "dr_cr";
            this.dr_cr.ReadOnly = true;
            this.dr_cr.Width = 45;
            // 
            // sl
            // 
            this.sl.HeaderText = "SL";
            this.sl.Name = "sl";
            this.sl.ReadOnly = true;
            this.sl.Width = 35;
            // 
            // cib_acct
            // 
            this.cib_acct.HeaderText = "CIB";
            this.cib_acct.Name = "cib_acct";
            this.cib_acct.ReadOnly = true;
            this.cib_acct.Width = 40;
            // 
            // acc_code
            // 
            this.acc_code.HeaderText = "SUB ACCT";
            this.acc_code.Name = "acc_code";
            this.acc_code.ReadOnly = true;
            // 
            // payment
            // 
            this.payment.HeaderText = "IS_PAYMENT";
            this.payment.Name = "payment";
            // 
            // tbcntrl_main
            // 
            this.tbcntrl_main.Controls.Add(this.tpg_list);
            this.tbcntrl_main.Controls.Add(this.tpg_info);
            this.tbcntrl_main.Controls.Add(this.tpg_import);
            this.tbcntrl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_main.Location = new System.Drawing.Point(221, 0);
            this.tbcntrl_main.Name = "tbcntrl_main";
            this.tbcntrl_main.SelectedIndex = 0;
            this.tbcntrl_main.Size = new System.Drawing.Size(607, 607);
            this.tbcntrl_main.TabIndex = 5;
            this.tbcntrl_main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_main_Selecting);
            // 
            // tpg_import
            // 
            this.tpg_import.Controls.Add(this.lbl_max);
            this.tpg_import.Controls.Add(this.label19);
            this.tpg_import.Controls.Add(this.lbl_min);
            this.tpg_import.Controls.Add(this.txt_filename);
            this.tpg_import.Controls.Add(this.label21);
            this.tpg_import.Controls.Add(this.label22);
            this.tpg_import.Controls.Add(this.btnBrowse);
            this.tpg_import.Controls.Add(this.label23);
            this.tpg_import.Controls.Add(this.pbar);
            this.tpg_import.Location = new System.Drawing.Point(4, 24);
            this.tpg_import.Name = "tpg_import";
            this.tpg_import.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_import.Size = new System.Drawing.Size(599, 579);
            this.tpg_import.TabIndex = 3;
            this.tpg_import.Text = "Import Account Title";
            this.tpg_import.UseVisualStyleBackColor = true;
            // 
            // lbl_max
            // 
            this.lbl_max.AutoSize = true;
            this.lbl_max.Location = new System.Drawing.Point(310, 112);
            this.lbl_max.Name = "lbl_max";
            this.lbl_max.Size = new System.Drawing.Size(14, 15);
            this.lbl_max.TabIndex = 36;
            this.lbl_max.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(274, 112);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(10, 15);
            this.label19.TabIndex = 35;
            this.label19.Text = "/";
            // 
            // lbl_min
            // 
            this.lbl_min.AutoSize = true;
            this.lbl_min.Location = new System.Drawing.Point(236, 112);
            this.lbl_min.Name = "lbl_min";
            this.lbl_min.Size = new System.Drawing.Size(14, 15);
            this.lbl_min.TabIndex = 34;
            this.lbl_min.Text = "0";
            // 
            // txt_filename
            // 
            this.txt_filename.AutoSize = true;
            this.txt_filename.Location = new System.Drawing.Point(88, 82);
            this.txt_filename.Name = "txt_filename";
            this.txt_filename.Size = new System.Drawing.Size(10, 15);
            this.txt_filename.TabIndex = 33;
            this.txt_filename.Text = ".";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(80, 82);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(10, 15);
            this.label21.TabIndex = 32;
            this.label21.Text = ":";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(25, 82);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 15);
            this.label22.TabIndex = 31;
            this.label22.Text = "Filename";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Peru;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBrowse.Location = new System.Drawing.Point(28, 40);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(138, 26);
            this.btnBrowse.TabIndex = 30;
            this.btnBrowse.Text = "Browse Excel";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(25, 19);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(121, 15);
            this.label23.TabIndex = 29;
            this.label23.Text = "Suppliers to Upload :";
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(101, 147);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(345, 31);
            this.pbar.TabIndex = 28;
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            // 
            // importBgWorker
            // 
            this.importBgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.importBgWorker_DoWork);
            // 
            // m_accounttitles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(828, 607);
            this.Controls.Add(this.tbcntrl_main);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "m_accounttitles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Titles";
            this.Load += new System.EventHandler(this.m_accounttitles_Load);
            this.panel1.ResumeLayout(false);
            this.tbcntrl_option.ResumeLayout(false);
            this.tpg_opt_1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tpg_opt_2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tpg_opt_3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tpg_info.ResumeLayout(false);
            this.tpg_info.PerformLayout();
            this.tpg_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.tbcntrl_main.ResumeLayout(false);
            this.tpg_import.ResumeLayout(false);
            this.tpg_import.PerformLayout();
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
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_additem;
        private System.Windows.Forms.Button btn_delitem;
        private System.Windows.Forms.Button btn_upditem;
        private System.Windows.Forms.TabPage tpg_opt_2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.TabPage tpg_info;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_cib_acct;
        private System.Windows.Forms.CheckBox chk_sl;
        private System.Windows.Forms.ComboBox cbo_mag_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_at_desc;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tpg_list;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.TabControl tbcntrl_main;
        private System.Windows.Forms.CheckBox chk_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn at_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn at_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn bs_pl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dr_cr;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl;
        private System.Windows.Forms.DataGridViewTextBoxColumn cib_acct;
        private System.Windows.Forms.DataGridViewTextBoxColumn acc_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment;
        private System.Windows.Forms.TabPage tpg_import;
        private System.Windows.Forms.Label lbl_max;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl_min;
        private System.Windows.Forms.Label txt_filename;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.ComponentModel.BackgroundWorker importBgWorker;
        private System.Windows.Forms.TabPage tpg_opt_3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Button btn_excel;
    }
}