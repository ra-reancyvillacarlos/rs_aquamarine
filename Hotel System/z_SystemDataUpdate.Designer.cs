namespace Hotel_System
{
    partial class z_SystemDataUpdate
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
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.cbo_viewas = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgvl_tdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_ttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_ttype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_ipadd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_locfrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_locto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_upload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_dtfrm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_dtto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpg_send = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.cbo_transferto = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.btn_testconect = new System.Windows.Forms.Button();
            this.btn_close1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.tpg_upload = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt2_fileupload = new System.Windows.Forms.TextBox();
            this.btn2_upload = new System.Windows.Forms.Button();
            this.btn2_close = new System.Windows.Forms.Button();
            this.btn2_searchfile = new System.Windows.Forms.Button();
            this.tpg_download = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtp3_to = new System.Windows.Forms.DateTimePicker();
            this.dtp3_frm = new System.Windows.Forms.DateTimePicker();
            this.btn3_close = new System.Windows.Forms.Button();
            this.btn3_openfolder = new System.Windows.Forms.Button();
            this.btn3_download = new System.Windows.Forms.Button();
            this.tpg_logs = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxt_log = new System.Windows.Forms.RichTextBox();
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpg_send.SuspendLayout();
            this.tpg_upload.SuspendLayout();
            this.tpg_download.SuspendLayout();
            this.tpg_logs.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnl_pbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.cbo_viewas);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 440);
            this.panel1.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(8, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 15);
            this.label15.TabIndex = 31;
            this.label15.Text = "View";
            // 
            // cbo_viewas
            // 
            this.cbo_viewas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_viewas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_viewas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_viewas.FormattingEnabled = true;
            this.cbo_viewas.Location = new System.Drawing.Point(53, 12);
            this.cbo_viewas.Name = "cbo_viewas";
            this.cbo_viewas.Size = new System.Drawing.Size(245, 23);
            this.cbo_viewas.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(164, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "To";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 15);
            this.label13.TabIndex = 28;
            this.label13.Text = "Dates";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(193, 45);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(53, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 346);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Data Update Logs";
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl_tdate,
            this.dgvl_ttime,
            this.dgvl_ttype,
            this.dgvl_userid,
            this.dgvl_ipadd,
            this.dgvl_locfrom,
            this.dgvl_locto,
            this.dgvl_upload,
            this.dgvl_dtfrm,
            this.dgvl_dtto});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 20;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(461, 326);
            this.dgv_list.TabIndex = 0;
            // 
            // dgvl_tdate
            // 
            this.dgvl_tdate.HeaderText = "TANS.DATE";
            this.dgvl_tdate.Name = "dgvl_tdate";
            this.dgvl_tdate.ReadOnly = true;
            this.dgvl_tdate.Width = 90;
            // 
            // dgvl_ttime
            // 
            this.dgvl_ttime.HeaderText = "TIME";
            this.dgvl_ttime.Name = "dgvl_ttime";
            this.dgvl_ttime.ReadOnly = true;
            this.dgvl_ttime.Width = 77;
            // 
            // dgvl_ttype
            // 
            this.dgvl_ttype.HeaderText = "TYPE";
            this.dgvl_ttype.Name = "dgvl_ttype";
            this.dgvl_ttype.ReadOnly = true;
            // 
            // dgvl_userid
            // 
            this.dgvl_userid.HeaderText = "USER ID";
            this.dgvl_userid.Name = "dgvl_userid";
            this.dgvl_userid.ReadOnly = true;
            // 
            // dgvl_ipadd
            // 
            this.dgvl_ipadd.HeaderText = "IP ADD RECEIVER";
            this.dgvl_ipadd.Name = "dgvl_ipadd";
            this.dgvl_ipadd.ReadOnly = true;
            this.dgvl_ipadd.Width = 135;
            // 
            // dgvl_locfrom
            // 
            this.dgvl_locfrom.HeaderText = "LOC FROM";
            this.dgvl_locfrom.Name = "dgvl_locfrom";
            this.dgvl_locfrom.ReadOnly = true;
            // 
            // dgvl_locto
            // 
            this.dgvl_locto.HeaderText = "LOC TO";
            this.dgvl_locto.Name = "dgvl_locto";
            this.dgvl_locto.ReadOnly = true;
            // 
            // dgvl_upload
            // 
            this.dgvl_upload.HeaderText = "UPLOAD";
            this.dgvl_upload.Name = "dgvl_upload";
            this.dgvl_upload.ReadOnly = true;
            // 
            // dgvl_dtfrm
            // 
            this.dgvl_dtfrm.HeaderText = "DATE FROM";
            this.dgvl_dtfrm.Name = "dgvl_dtfrm";
            this.dgvl_dtfrm.ReadOnly = true;
            // 
            // dgvl_dtto
            // 
            this.dgvl_dtto.HeaderText = "DATE TO";
            this.dgvl_dtto.Name = "dgvl_dtto";
            this.dgvl_dtto.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpg_send);
            this.tabControl1.Controls.Add(this.tpg_upload);
            this.tabControl1.Controls.Add(this.tpg_download);
            this.tabControl1.Controls.Add(this.tpg_logs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(467, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(526, 428);
            this.tabControl1.TabIndex = 6;
            // 
            // tpg_send
            // 
            this.tpg_send.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_send.Controls.Add(this.label16);
            this.tpg_send.Controls.Add(this.label17);
            this.tpg_send.Controls.Add(this.dateTimePicker3);
            this.tpg_send.Controls.Add(this.dateTimePicker4);
            this.tpg_send.Controls.Add(this.cbo_transferto);
            this.tpg_send.Controls.Add(this.label12);
            this.tpg_send.Controls.Add(this.cbo_branch);
            this.tpg_send.Controls.Add(this.label11);
            this.tpg_send.Controls.Add(this.btn_send);
            this.tpg_send.Controls.Add(this.label10);
            this.tpg_send.Controls.Add(this.txt_password);
            this.tpg_send.Controls.Add(this.label9);
            this.tpg_send.Controls.Add(this.txt_port);
            this.tpg_send.Controls.Add(this.btn_testconect);
            this.tpg_send.Controls.Add(this.btn_close1);
            this.tpg_send.Controls.Add(this.label8);
            this.tpg_send.Controls.Add(this.txt_server);
            this.tpg_send.Location = new System.Drawing.Point(4, 24);
            this.tpg_send.Name = "tpg_send";
            this.tpg_send.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_send.Size = new System.Drawing.Size(518, 400);
            this.tpg_send.TabIndex = 1;
            this.tpg_send.Text = "Send Data via Internet";
            this.tpg_send.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(230, 200);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 15);
            this.label16.TabIndex = 37;
            this.label16.Text = "To";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 200);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(106, 15);
            this.label17.TabIndex = 36;
            this.label17.Text = "Transaction Dates";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker3.Location = new System.Drawing.Point(257, 195);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker3.TabIndex = 35;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker4.Location = new System.Drawing.Point(119, 195);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker4.TabIndex = 34;
            // 
            // cbo_transferto
            // 
            this.cbo_transferto.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_transferto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_transferto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_transferto.FormattingEnabled = true;
            this.cbo_transferto.Location = new System.Drawing.Point(119, 166);
            this.cbo_transferto.Name = "cbo_transferto";
            this.cbo_transferto.Size = new System.Drawing.Size(341, 23);
            this.cbo_transferto.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 15);
            this.label12.TabIndex = 32;
            this.label12.Text = "Transfer To";
            // 
            // cbo_branch
            // 
            this.cbo_branch.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Location = new System.Drawing.Point(119, 137);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(341, 23);
            this.cbo_branch.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 15);
            this.label11.TabIndex = 25;
            this.label11.Text = "Local Branch";
            // 
            // btn_send
            // 
            this.btn_send.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_send.Location = new System.Drawing.Point(349, 290);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(111, 49);
            this.btn_send.TabIndex = 22;
            this.btn_send.Text = "Send Data";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(119, 87);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(162, 22);
            this.txt_password.TabIndex = 19;
            this.txt_password.Text = "Rightech777";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "Port";
            // 
            // txt_port
            // 
            this.txt_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_port.Location = new System.Drawing.Point(119, 59);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(57, 22);
            this.txt_port.TabIndex = 17;
            this.txt_port.Text = "5432";
            // 
            // btn_testconect
            // 
            this.btn_testconect.BackColor = System.Drawing.Color.Peru;
            this.btn_testconect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_testconect.Location = new System.Drawing.Point(316, 79);
            this.btn_testconect.Name = "btn_testconect";
            this.btn_testconect.Size = new System.Drawing.Size(144, 30);
            this.btn_testconect.TabIndex = 15;
            this.btn_testconect.Text = "Test DB Connection";
            this.btn_testconect.UseVisualStyleBackColor = false;
            this.btn_testconect.Click += new System.EventHandler(this.btn_testconect_Click);
            // 
            // btn_close1
            // 
            this.btn_close1.BackColor = System.Drawing.Color.Peru;
            this.btn_close1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close1.Location = new System.Drawing.Point(254, 290);
            this.btn_close1.Name = "btn_close1";
            this.btn_close1.Size = new System.Drawing.Size(89, 49);
            this.btn_close1.TabIndex = 16;
            this.btn_close1.Text = "Close";
            this.btn_close1.UseVisualStyleBackColor = false;
            this.btn_close1.Click += new System.EventHandler(this.btn_close1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Intenet IP Address";
            // 
            // txt_server
            // 
            this.txt_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_server.Location = new System.Drawing.Point(119, 31);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(162, 22);
            this.txt_server.TabIndex = 13;
            this.txt_server.Text = "210.4.59.194";
            // 
            // tpg_upload
            // 
            this.tpg_upload.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_upload.Controls.Add(this.label5);
            this.tpg_upload.Controls.Add(this.label6);
            this.tpg_upload.Controls.Add(this.label7);
            this.tpg_upload.Controls.Add(this.label4);
            this.tpg_upload.Controls.Add(this.label3);
            this.tpg_upload.Controls.Add(this.label2);
            this.tpg_upload.Controls.Add(this.label1);
            this.tpg_upload.Controls.Add(this.txt2_fileupload);
            this.tpg_upload.Controls.Add(this.btn2_upload);
            this.tpg_upload.Controls.Add(this.btn2_close);
            this.tpg_upload.Controls.Add(this.btn2_searchfile);
            this.tpg_upload.Location = new System.Drawing.Point(4, 24);
            this.tpg_upload.Name = "tpg_upload";
            this.tpg_upload.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_upload.Size = new System.Drawing.Size(518, 400);
            this.tpg_upload.TabIndex = 0;
            this.tpg_upload.Text = "Upload Data From other Locations";
            this.tpg_upload.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(78, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "You cannot longer undo it.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(78, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(350, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Once you upload the data and successfully save it to the server, ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(23, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Warning";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(62, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "to another locations.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Select file to upload to update of data that send from one location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Note";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "File Upload";
            // 
            // txt2_fileupload
            // 
            this.txt2_fileupload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2_fileupload.Location = new System.Drawing.Point(99, 28);
            this.txt2_fileupload.Name = "txt2_fileupload";
            this.txt2_fileupload.Size = new System.Drawing.Size(265, 22);
            this.txt2_fileupload.TabIndex = 11;
            // 
            // btn2_upload
            // 
            this.btn2_upload.BackColor = System.Drawing.Color.SteelBlue;
            this.btn2_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_upload.Location = new System.Drawing.Point(370, 298);
            this.btn2_upload.Name = "btn2_upload";
            this.btn2_upload.Size = new System.Drawing.Size(89, 49);
            this.btn2_upload.TabIndex = 8;
            this.btn2_upload.Text = "Upload";
            this.btn2_upload.UseVisualStyleBackColor = false;
            this.btn2_upload.Click += new System.EventHandler(this.btn2_upload_Click);
            // 
            // btn2_close
            // 
            this.btn2_close.BackColor = System.Drawing.Color.Peru;
            this.btn2_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_close.Location = new System.Drawing.Point(275, 298);
            this.btn2_close.Name = "btn2_close";
            this.btn2_close.Size = new System.Drawing.Size(89, 49);
            this.btn2_close.TabIndex = 9;
            this.btn2_close.Text = "Close";
            this.btn2_close.UseVisualStyleBackColor = false;
            this.btn2_close.Click += new System.EventHandler(this.btn2_close_Click);
            // 
            // btn2_searchfile
            // 
            this.btn2_searchfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_searchfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2_searchfile.Image = global::Hotel_System.Properties.Resources.folder_16x16;
            this.btn2_searchfile.Location = new System.Drawing.Point(370, 28);
            this.btn2_searchfile.Name = "btn2_searchfile";
            this.btn2_searchfile.Size = new System.Drawing.Size(37, 24);
            this.btn2_searchfile.TabIndex = 10;
            this.btn2_searchfile.UseVisualStyleBackColor = true;
            this.btn2_searchfile.Click += new System.EventHandler(this.btn2_searchfile_Click);
            // 
            // tpg_download
            // 
            this.tpg_download.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_download.Controls.Add(this.label18);
            this.tpg_download.Controls.Add(this.label19);
            this.tpg_download.Controls.Add(this.dtp3_to);
            this.tpg_download.Controls.Add(this.dtp3_frm);
            this.tpg_download.Controls.Add(this.btn3_close);
            this.tpg_download.Controls.Add(this.btn3_openfolder);
            this.tpg_download.Controls.Add(this.btn3_download);
            this.tpg_download.Location = new System.Drawing.Point(4, 24);
            this.tpg_download.Name = "tpg_download";
            this.tpg_download.Size = new System.Drawing.Size(518, 400);
            this.tpg_download.TabIndex = 2;
            this.tpg_download.Text = "Download System Data";
            this.tpg_download.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(125, 75);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 15);
            this.label18.TabIndex = 41;
            this.label18.Text = "Transaction Dates To";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(125, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(138, 15);
            this.label19.TabIndex = 40;
            this.label19.Text = "Transaction Dates From";
            // 
            // dtp3_to
            // 
            this.dtp3_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp3_to.Location = new System.Drawing.Point(274, 70);
            this.dtp3_to.Name = "dtp3_to";
            this.dtp3_to.Size = new System.Drawing.Size(105, 21);
            this.dtp3_to.TabIndex = 39;
            // 
            // dtp3_frm
            // 
            this.dtp3_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp3_frm.Location = new System.Drawing.Point(274, 43);
            this.dtp3_frm.Name = "dtp3_frm";
            this.dtp3_frm.Size = new System.Drawing.Size(105, 21);
            this.dtp3_frm.TabIndex = 38;
            // 
            // btn3_close
            // 
            this.btn3_close.BackColor = System.Drawing.Color.Peru;
            this.btn3_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_close.Location = new System.Drawing.Point(159, 179);
            this.btn3_close.Name = "btn3_close";
            this.btn3_close.Size = new System.Drawing.Size(89, 49);
            this.btn3_close.TabIndex = 22;
            this.btn3_close.Text = "Close";
            this.btn3_close.UseVisualStyleBackColor = false;
            this.btn3_close.Click += new System.EventHandler(this.btn3_close_Click);
            // 
            // btn3_openfolder
            // 
            this.btn3_openfolder.BackColor = System.Drawing.Color.Peru;
            this.btn3_openfolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_openfolder.Location = new System.Drawing.Point(260, 179);
            this.btn3_openfolder.Name = "btn3_openfolder";
            this.btn3_openfolder.Size = new System.Drawing.Size(89, 49);
            this.btn3_openfolder.TabIndex = 21;
            this.btn3_openfolder.Text = "Open Folder";
            this.btn3_openfolder.UseVisualStyleBackColor = false;
            this.btn3_openfolder.Click += new System.EventHandler(this.btn3_openfolder_Click);
            // 
            // btn3_download
            // 
            this.btn3_download.BackColor = System.Drawing.Color.Peru;
            this.btn3_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_download.Image = global::Hotel_System.Properties.Resources.download;
            this.btn3_download.Location = new System.Drawing.Point(159, 124);
            this.btn3_download.Name = "btn3_download";
            this.btn3_download.Size = new System.Drawing.Size(190, 49);
            this.btn3_download.TabIndex = 9;
            this.btn3_download.Text = "Dowload Data to Send";
            this.btn3_download.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn3_download.UseVisualStyleBackColor = false;
            this.btn3_download.Click += new System.EventHandler(this.btn3_download_Click);
            // 
            // tpg_logs
            // 
            this.tpg_logs.Controls.Add(this.groupBox2);
            this.tpg_logs.Location = new System.Drawing.Point(4, 24);
            this.tpg_logs.Name = "tpg_logs";
            this.tpg_logs.Size = new System.Drawing.Size(518, 400);
            this.tpg_logs.TabIndex = 3;
            this.tpg_logs.Text = "Logs";
            this.tpg_logs.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.rtxt_log);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 362);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // rtxt_log
            // 
            this.rtxt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt_log.Location = new System.Drawing.Point(3, 17);
            this.rtxt_log.Name = "rtxt_log";
            this.rtxt_log.Size = new System.Drawing.Size(510, 342);
            this.rtxt_log.TabIndex = 0;
            this.rtxt_log.Text = "";
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.progressBar1);
            this.pnl_pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_pbar.Location = new System.Drawing.Point(467, 396);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(526, 44);
            this.pnl_pbar.TabIndex = 7;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lbl_status.Location = new System.Drawing.Point(0, 0);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(51, 15);
            this.lbl_status.TabIndex = 9;
            this.lbl_status.Text = "Ready...";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 21);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(526, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // z_SystemDataUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(993, 440);
            this.Controls.Add(this.pnl_pbar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_SystemDataUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Data Update";
            this.Load += new System.EventHandler(this.z_SystemDataUpdate_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpg_send.ResumeLayout(false);
            this.tpg_send.PerformLayout();
            this.tpg_upload.ResumeLayout(false);
            this.tpg_upload.PerformLayout();
            this.tpg_download.ResumeLayout(false);
            this.tpg_download.PerformLayout();
            this.tpg_logs.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnl_pbar.ResumeLayout(false);
            this.pnl_pbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg_send;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Button btn_testconect;
        private System.Windows.Forms.Button btn_close1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.TabPage tpg_upload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt2_fileupload;
        private System.Windows.Forms.Button btn2_searchfile;
        private System.Windows.Forms.Button btn2_upload;
        private System.Windows.Forms.Button btn2_close;
        private System.Windows.Forms.TabPage tpg_download;
        private System.Windows.Forms.Button btn3_close;
        private System.Windows.Forms.Button btn3_openfolder;
        private System.Windows.Forms.Button btn3_download;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbo_viewas;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.Windows.Forms.ComboBox cbo_transferto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_tdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_ttime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_ttype;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_ipadd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_locfrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_locto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_upload;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_dtfrm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_dtto;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtp3_to;
        private System.Windows.Forms.DateTimePicker dtp3_frm;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabPage tpg_logs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxt_log;
    }
}