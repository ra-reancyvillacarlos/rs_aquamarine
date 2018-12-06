namespace Hotel_System
{
    partial class Guest
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_complainer = new System.Windows.Forms.CheckBox();
            this.txt_acctno = new System.Windows.Forms.TextBox();
            this.chk_escaper = new System.Windows.Forms.CheckBox();
            this.pnl_guestinfo2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dtp_issuedon = new System.Windows.Forms.DateTimePicker();
            this.dtp_expired = new System.Windows.Forms.DateTimePicker();
            this.txt_contact = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_passport = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.pnl_guestinfo1 = new System.Windows.Forms.Panel();
            this.cbo_title = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.rtxt_remarks = new System.Windows.Forms.RichTextBox();
            this.txt_addr = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbo_company = new System.Windows.Forms.ComboBox();
            this.cbo_gender = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbo_country = new System.Windows.Forms.ComboBox();
            this.dtp_dob = new System.Windows.Forms.DateTimePicker();
            this.txt_mname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_fname = new System.Windows.Forms.TextBox();
            this.txt_lname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_searchcountry = new System.Windows.Forms.ComboBox();
            this.cbo_searchcompany = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_searchfname = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_pgno = new System.Windows.Forms.Label();
            this.btn_noofrows = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_searchlname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_guestlist = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_guestfolliohist = new System.Windows.Forms.DataGridView();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBox1.SuspendLayout();
            this.pnl_guestinfo2.SuspendLayout();
            this.pnl_guestinfo1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_guestlist)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_guestfolliohist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(401, 100);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(94, 27);
            this.btn_cancel.TabIndex = 39;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(400, 70);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(94, 27);
            this.btn_save.TabIndex = 38;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(401, 41);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(94, 27);
            this.btn_edit.TabIndex = 37;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(401, 12);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(94, 27);
            this.btn_new.TabIndex = 36;
            this.btn_new.Text = "New";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_complainer);
            this.groupBox1.Controls.Add(this.txt_acctno);
            this.groupBox1.Controls.Add(this.chk_escaper);
            this.groupBox1.Controls.Add(this.pnl_guestinfo2);
            this.groupBox1.Controls.Add(this.pnl_guestinfo1);
            this.groupBox1.Controls.Add(this.btn_cancel);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_new);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Information";
            // 
            // chk_complainer
            // 
            this.chk_complainer.AutoSize = true;
            this.chk_complainer.ForeColor = System.Drawing.Color.DarkRed;
            this.chk_complainer.Location = new System.Drawing.Point(257, 39);
            this.chk_complainer.Name = "chk_complainer";
            this.chk_complainer.Size = new System.Drawing.Size(78, 17);
            this.chk_complainer.TabIndex = 60;
            this.chk_complainer.Text = "Complainer";
            this.chk_complainer.UseVisualStyleBackColor = true;
            // 
            // txt_acctno
            // 
            this.txt_acctno.Location = new System.Drawing.Point(156, 19);
            this.txt_acctno.Name = "txt_acctno";
            this.txt_acctno.Size = new System.Drawing.Size(83, 20);
            this.txt_acctno.TabIndex = 59;
            // 
            // chk_escaper
            // 
            this.chk_escaper.AutoSize = true;
            this.chk_escaper.ForeColor = System.Drawing.Color.DarkRed;
            this.chk_escaper.Location = new System.Drawing.Point(257, 16);
            this.chk_escaper.Name = "chk_escaper";
            this.chk_escaper.Size = new System.Drawing.Size(65, 17);
            this.chk_escaper.TabIndex = 44;
            this.chk_escaper.Text = "Escaper";
            this.chk_escaper.UseVisualStyleBackColor = true;
            // 
            // pnl_guestinfo2
            // 
            this.pnl_guestinfo2.Controls.Add(this.label18);
            this.pnl_guestinfo2.Controls.Add(this.label17);
            this.pnl_guestinfo2.Controls.Add(this.dtp_issuedon);
            this.pnl_guestinfo2.Controls.Add(this.dtp_expired);
            this.pnl_guestinfo2.Controls.Add(this.txt_contact);
            this.pnl_guestinfo2.Controls.Add(this.label11);
            this.pnl_guestinfo2.Controls.Add(this.txt_passport);
            this.pnl_guestinfo2.Controls.Add(this.label10);
            this.pnl_guestinfo2.Controls.Add(this.label12);
            this.pnl_guestinfo2.Controls.Add(this.txt_email);
            this.pnl_guestinfo2.Location = new System.Drawing.Point(257, 137);
            this.pnl_guestinfo2.Name = "pnl_guestinfo2";
            this.pnl_guestinfo2.Size = new System.Drawing.Size(238, 152);
            this.pnl_guestinfo2.TabIndex = 43;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1, 70);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 49;
            this.label18.Text = "Expired on";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1, 45);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 13);
            this.label17.TabIndex = 48;
            this.label17.Text = "Issued on";
            // 
            // dtp_issuedon
            // 
            this.dtp_issuedon.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_issuedon.Location = new System.Drawing.Point(71, 38);
            this.dtp_issuedon.Name = "dtp_issuedon";
            this.dtp_issuedon.Size = new System.Drawing.Size(110, 20);
            this.dtp_issuedon.TabIndex = 47;
            // 
            // dtp_expired
            // 
            this.dtp_expired.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_expired.Location = new System.Drawing.Point(71, 64);
            this.dtp_expired.Name = "dtp_expired";
            this.dtp_expired.Size = new System.Drawing.Size(110, 20);
            this.dtp_expired.TabIndex = 46;
            // 
            // txt_contact
            // 
            this.txt_contact.Location = new System.Drawing.Point(68, 93);
            this.txt_contact.Name = "txt_contact";
            this.txt_contact.Size = new System.Drawing.Size(166, 20);
            this.txt_contact.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Contact No";
            // 
            // txt_passport
            // 
            this.txt_passport.Location = new System.Drawing.Point(71, 17);
            this.txt_passport.Name = "txt_passport";
            this.txt_passport.Size = new System.Drawing.Size(163, 20);
            this.txt_passport.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Passport No";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 47;
            this.label12.Text = "Email";
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(71, 119);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(163, 20);
            this.txt_email.TabIndex = 48;
            // 
            // pnl_guestinfo1
            // 
            this.pnl_guestinfo1.Controls.Add(this.cbo_title);
            this.pnl_guestinfo1.Controls.Add(this.label8);
            this.pnl_guestinfo1.Controls.Add(this.label16);
            this.pnl_guestinfo1.Controls.Add(this.rtxt_remarks);
            this.pnl_guestinfo1.Controls.Add(this.txt_addr);
            this.pnl_guestinfo1.Controls.Add(this.label15);
            this.pnl_guestinfo1.Controls.Add(this.label14);
            this.pnl_guestinfo1.Controls.Add(this.label13);
            this.pnl_guestinfo1.Controls.Add(this.cbo_company);
            this.pnl_guestinfo1.Controls.Add(this.cbo_gender);
            this.pnl_guestinfo1.Controls.Add(this.label9);
            this.pnl_guestinfo1.Controls.Add(this.cbo_country);
            this.pnl_guestinfo1.Controls.Add(this.dtp_dob);
            this.pnl_guestinfo1.Controls.Add(this.txt_mname);
            this.pnl_guestinfo1.Controls.Add(this.label7);
            this.pnl_guestinfo1.Controls.Add(this.label5);
            this.pnl_guestinfo1.Controls.Add(this.txt_fname);
            this.pnl_guestinfo1.Controls.Add(this.txt_lname);
            this.pnl_guestinfo1.Controls.Add(this.label6);
            this.pnl_guestinfo1.Location = new System.Drawing.Point(6, 16);
            this.pnl_guestinfo1.Name = "pnl_guestinfo1";
            this.pnl_guestinfo1.Size = new System.Drawing.Size(245, 274);
            this.pnl_guestinfo1.TabIndex = 10;
            // 
            // cbo_title
            // 
            this.cbo_title.FormattingEnabled = true;
            this.cbo_title.Items.AddRange(new object[] {
            "MR.",
            "MS.",
            "MRS.",
            "DR."});
            this.cbo_title.Location = new System.Drawing.Point(70, 4);
            this.cbo_title.Name = "cbo_title";
            this.cbo_title.Size = new System.Drawing.Size(74, 21);
            this.cbo_title.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Title";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 202);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 55;
            this.label16.Text = "Remarks";
            // 
            // rtxt_remarks
            // 
            this.rtxt_remarks.Location = new System.Drawing.Point(70, 199);
            this.rtxt_remarks.Name = "rtxt_remarks";
            this.rtxt_remarks.Size = new System.Drawing.Size(163, 65);
            this.rtxt_remarks.TabIndex = 54;
            this.rtxt_remarks.Text = "";
            // 
            // txt_addr
            // 
            this.txt_addr.Location = new System.Drawing.Point(70, 95);
            this.txt_addr.Name = "txt_addr";
            this.txt_addr.Size = new System.Drawing.Size(163, 20);
            this.txt_addr.TabIndex = 53;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 52;
            this.label15.Text = "Address";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 51;
            this.label14.Text = "Birth";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 175);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Company";
            // 
            // cbo_company
            // 
            this.cbo_company.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbo_company.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_company.FormattingEnabled = true;
            this.cbo_company.Location = new System.Drawing.Point(70, 172);
            this.cbo_company.Name = "cbo_company";
            this.cbo_company.Size = new System.Drawing.Size(163, 21);
            this.cbo_company.TabIndex = 49;
            // 
            // cbo_gender
            // 
            this.cbo_gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_gender.FormattingEnabled = true;
            this.cbo_gender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cbo_gender.Location = new System.Drawing.Point(159, 119);
            this.cbo_gender.Name = "cbo_gender";
            this.cbo_gender.Size = new System.Drawing.Size(74, 21);
            this.cbo_gender.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Country";
            // 
            // cbo_country
            // 
            this.cbo_country.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbo_country.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_country.FormattingEnabled = true;
            this.cbo_country.Location = new System.Drawing.Point(70, 144);
            this.cbo_country.Name = "cbo_country";
            this.cbo_country.Size = new System.Drawing.Size(163, 21);
            this.cbo_country.TabIndex = 43;
            // 
            // dtp_dob
            // 
            this.dtp_dob.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_dob.Location = new System.Drawing.Point(70, 121);
            this.dtp_dob.Name = "dtp_dob";
            this.dtp_dob.Size = new System.Drawing.Size(83, 20);
            this.dtp_dob.TabIndex = 42;
            // 
            // txt_mname
            // 
            this.txt_mname.Location = new System.Drawing.Point(70, 73);
            this.txt_mname.Name = "txt_mname";
            this.txt_mname.Size = new System.Drawing.Size(163, 20);
            this.txt_mname.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Mid Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Last Name";
            // 
            // txt_fname
            // 
            this.txt_fname.Location = new System.Drawing.Point(70, 51);
            this.txt_fname.Name = "txt_fname";
            this.txt_fname.Size = new System.Drawing.Size(163, 20);
            this.txt_fname.TabIndex = 39;
            // 
            // txt_lname
            // 
            this.txt_lname.Location = new System.Drawing.Point(70, 29);
            this.txt_lname.Name = "txt_lname";
            this.txt_lname.Size = new System.Drawing.Size(163, 20);
            this.txt_lname.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "First Name";
            // 
            // btn_search
            // 
            this.btn_search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_search.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(314, 16);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(111, 49);
            this.btn_search.TabIndex = 9;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Country";
            this.label4.Visible = false;
            // 
            // cbo_searchcountry
            // 
            this.cbo_searchcountry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbo_searchcountry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_searchcountry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbo_searchcountry.FormattingEnabled = true;
            this.cbo_searchcountry.Location = new System.Drawing.Point(70, 96);
            this.cbo_searchcountry.Name = "cbo_searchcountry";
            this.cbo_searchcountry.Size = new System.Drawing.Size(225, 21);
            this.cbo_searchcountry.TabIndex = 7;
            this.cbo_searchcountry.Visible = false;
            // 
            // cbo_searchcompany
            // 
            this.cbo_searchcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbo_searchcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_searchcompany.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbo_searchcompany.FormattingEnabled = true;
            this.cbo_searchcompany.Location = new System.Drawing.Point(70, 69);
            this.cbo_searchcompany.Name = "cbo_searchcompany";
            this.cbo_searchcompany.Size = new System.Drawing.Size(225, 21);
            this.cbo_searchcompany.TabIndex = 5;
            this.cbo_searchcompany.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Company";
            this.label3.Visible = false;
            // 
            // txt_searchfname
            // 
            this.txt_searchfname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_searchfname.Location = new System.Drawing.Point(70, 42);
            this.txt_searchfname.Name = "txt_searchfname";
            this.txt_searchfname.Size = new System.Drawing.Size(225, 20);
            this.txt_searchfname.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox2.Controls.Add(this.lbl_pgno);
            this.groupBox2.Controls.Add(this.btn_noofrows);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.btn_next);
            this.groupBox2.Controls.Add(this.btn_prev);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbo_searchcountry);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbo_searchcompany);
            this.groupBox2.Controls.Add(this.txt_searchfname);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_searchlname);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dgv_guestlist);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 708);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Tennant";
            // 
            // lbl_pgno
            // 
            this.lbl_pgno.AutoSize = true;
            this.lbl_pgno.Location = new System.Drawing.Point(375, 140);
            this.lbl_pgno.Name = "lbl_pgno";
            this.lbl_pgno.Size = new System.Drawing.Size(13, 13);
            this.lbl_pgno.TabIndex = 14;
            this.lbl_pgno.Text = "0";
            // 
            // btn_noofrows
            // 
            this.btn_noofrows.AutoSize = true;
            this.btn_noofrows.Location = new System.Drawing.Point(67, 143);
            this.btn_noofrows.Name = "btn_noofrows";
            this.btn_noofrows.Size = new System.Drawing.Size(13, 13);
            this.btn_noofrows.TabIndex = 13;
            this.btn_noofrows.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 143);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "Tennant List";
            // 
            // btn_next
            // 
            this.btn_next.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_next.Location = new System.Drawing.Point(394, 132);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(31, 25);
            this.btn_next.TabIndex = 11;
            this.btn_next.Text = ">";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_prev.Location = new System.Drawing.Point(338, 132);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(31, 25);
            this.btn_prev.TabIndex = 10;
            this.btn_prev.Text = "<";
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "First Name";
            // 
            // txt_searchlname
            // 
            this.txt_searchlname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_searchlname.Location = new System.Drawing.Point(70, 16);
            this.txt_searchlname.Name = "txt_searchlname";
            this.txt_searchlname.Size = new System.Drawing.Size(225, 20);
            this.txt_searchlname.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Last Name";
            // 
            // dgv_guestlist
            // 
            this.dgv_guestlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_guestlist.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_guestlist.Location = new System.Drawing.Point(6, 160);
            this.dgv_guestlist.Name = "dgv_guestlist";
            this.dgv_guestlist.ReadOnly = true;
            this.dgv_guestlist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgv_guestlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_guestlist.Size = new System.Drawing.Size(419, 315);
            this.dgv_guestlist.TabIndex = 0;
            this.dgv_guestlist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_guestlist_CellDoubleClick);
            this.dgv_guestlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_guestlist_CellPainting);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 708);
            this.splitContainer1.SplitterDistance = 459;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_guestfolliohist);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 525);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(541, 183);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tennant Folio History";
            // 
            // dgv_guestfolliohist
            // 
            this.dgv_guestfolliohist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_guestfolliohist.Location = new System.Drawing.Point(6, 32);
            this.dgv_guestfolliohist.Name = "dgv_guestfolliohist";
            this.dgv_guestfolliohist.ReadOnly = true;
            this.dgv_guestfolliohist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_guestfolliohist.Size = new System.Drawing.Size(485, 139);
            this.dgv_guestfolliohist.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // Guest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 708);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Guest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tennant";
            this.Load += new System.EventHandler(this.Guest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnl_guestinfo2.ResumeLayout(false);
            this.pnl_guestinfo2.PerformLayout();
            this.pnl_guestinfo1.ResumeLayout(false);
            this.pnl_guestinfo1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_guestlist)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_guestfolliohist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_searchcountry;
        private System.Windows.Forms.ComboBox cbo_searchcompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_searchfname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_searchlname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_guestlist;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_guestfolliohist;
        private System.Windows.Forms.Panel pnl_guestinfo2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtp_issuedon;
        private System.Windows.Forms.DateTimePicker dtp_expired;
        private System.Windows.Forms.TextBox txt_contact;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbo_gender;
        private System.Windows.Forms.Panel pnl_guestinfo1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox rtxt_remarks;
        private System.Windows.Forms.TextBox txt_addr;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbo_company;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_passport;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbo_country;
        private System.Windows.Forms.DateTimePicker dtp_dob;
        private System.Windows.Forms.TextBox txt_mname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_fname;
        private System.Windows.Forms.TextBox txt_lname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label btn_noofrows;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chk_escaper;
        private System.Windows.Forms.Label lbl_pgno;
        private System.Windows.Forms.ComboBox cbo_title;
        private System.Windows.Forms.TextBox txt_acctno;
        private System.Windows.Forms.CheckBox chk_complainer;
    }
}