namespace Accounting_Application_System
{
    partial class call_history
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
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgv_call_history_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_client_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_client_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_contact_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_date_to_call = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_time_to_call = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_date_time_last_called = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tpg_right_list = new System.Windows.Forms.TabPage();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.tbcntrl_main = new System.Windows.Forms.TabControl();
            this.pnl_sidebar = new System.Windows.Forms.Panel();
            this.tbcntrl_left = new System.Windows.Forms.TabControl();
            this.tpg_option = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_upd = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tpg_right_list.SuspendLayout();
            this.pnl_main.SuspendLayout();
            this.tbcntrl_main.SuspendLayout();
            this.pnl_sidebar.SuspendLayout();
            this.tbcntrl_left.SuspendLayout();
            this.tpg_option.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_to
            // 
            this.dtp_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(284, 11);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(103, 21);
            this.dtp_to.TabIndex = 64;
            this.dtp_to.ValueChanged += new System.EventHandler(this.dtp_to_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(253, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 66;
            this.label3.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = "Transaction Dates";
            // 
            // groupBox6
            // 
            this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox6.Controls.Add(this.dgv_list);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(0, 41);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(809, 485);
            this.groupBox6.TabIndex = 58;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Call History List";
            // 
            // dgv_list
            // 
            this.dgv_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_list.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_call_history_number,
            this.dgv_client_number,
            this.dgv_client_name,
            this.dgv_contact_no,
            this.dgv_date_to_call,
            this.dgv_time_to_call,
            this.dgv_date_time_last_called,
            this.dgv_userid,
            this.dgv_status,
            this.dgv_remark});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 18);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 20;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(803, 464);
            this.dgv_list.TabIndex = 0;
            // 
            // dgv_call_history_number
            // 
            this.dgv_call_history_number.HeaderText = "Call History #";
            this.dgv_call_history_number.Name = "dgv_call_history_number";
            this.dgv_call_history_number.ReadOnly = true;
            this.dgv_call_history_number.Width = 103;
            // 
            // dgv_client_number
            // 
            this.dgv_client_number.HeaderText = "Client Number";
            this.dgv_client_number.Name = "dgv_client_number";
            this.dgv_client_number.ReadOnly = true;
            this.dgv_client_number.Width = 111;
            // 
            // dgv_client_name
            // 
            this.dgv_client_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_client_name.HeaderText = "Client Name";
            this.dgv_client_name.Name = "dgv_client_name";
            this.dgv_client_name.ReadOnly = true;
            // 
            // dgv_contact_no
            // 
            this.dgv_contact_no.HeaderText = "Contact No";
            this.dgv_contact_no.Name = "dgv_contact_no";
            this.dgv_contact_no.ReadOnly = true;
            this.dgv_contact_no.Width = 92;
            // 
            // dgv_date_to_call
            // 
            this.dgv_date_to_call.HeaderText = "Date To Call";
            this.dgv_date_to_call.Name = "dgv_date_to_call";
            this.dgv_date_to_call.ReadOnly = true;
            this.dgv_date_to_call.Width = 99;
            // 
            // dgv_time_to_call
            // 
            this.dgv_time_to_call.HeaderText = "Time to Call";
            this.dgv_time_to_call.Name = "dgv_time_to_call";
            this.dgv_time_to_call.ReadOnly = true;
            this.dgv_time_to_call.Width = 97;
            // 
            // dgv_date_time_last_called
            // 
            this.dgv_date_time_last_called.HeaderText = "Date/Time Last Called";
            this.dgv_date_time_last_called.Name = "dgv_date_time_last_called";
            this.dgv_date_time_last_called.ReadOnly = true;
            this.dgv_date_time_last_called.Visible = false;
            this.dgv_date_time_last_called.Width = 167;
            // 
            // dgv_userid
            // 
            this.dgv_userid.HeaderText = "UserId";
            this.dgv_userid.Name = "dgv_userid";
            this.dgv_userid.ReadOnly = true;
            this.dgv_userid.Width = 68;
            // 
            // dgv_status
            // 
            this.dgv_status.HeaderText = "Status";
            this.dgv_status.Name = "dgv_status";
            this.dgv_status.ReadOnly = true;
            this.dgv_status.Width = 66;
            // 
            // dgv_remark
            // 
            this.dgv_remark.HeaderText = "Remark";
            this.dgv_remark.MinimumWidth = 150;
            this.dgv_remark.Name = "dgv_remark";
            this.dgv_remark.ReadOnly = true;
            this.dgv_remark.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtp_to);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtp_frm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 41);
            this.panel1.TabIndex = 63;
            // 
            // dtp_frm
            // 
            this.dtp_frm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(144, 11);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(103, 21);
            this.dtp_frm.TabIndex = 63;
            this.dtp_frm.ValueChanged += new System.EventHandler(this.dtp_frm_ValueChanged);
            // 
            // panel7
            // 
            this.panel7.AutoSize = true;
            this.panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel7.BackColor = System.Drawing.SystemColors.Info;
            this.panel7.Controls.Add(this.groupBox6);
            this.panel7.Controls.Add(this.panel1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(809, 526);
            this.panel7.TabIndex = 0;
            // 
            // tpg_right_list
            // 
            this.tpg_right_list.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_right_list.Controls.Add(this.panel7);
            this.tpg_right_list.Location = new System.Drawing.Point(4, 24);
            this.tpg_right_list.Name = "tpg_right_list";
            this.tpg_right_list.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_right_list.Size = new System.Drawing.Size(815, 532);
            this.tpg_right_list.TabIndex = 0;
            this.tpg_right_list.Text = "Call History List";
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.tbcntrl_main);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(197, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(823, 557);
            this.pnl_main.TabIndex = 46;
            // 
            // tbcntrl_main
            // 
            this.tbcntrl_main.Controls.Add(this.tpg_right_list);
            this.tbcntrl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcntrl_main.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_main.MinimumSize = new System.Drawing.Size(600, 560);
            this.tbcntrl_main.Name = "tbcntrl_main";
            this.tbcntrl_main.SelectedIndex = 0;
            this.tbcntrl_main.Size = new System.Drawing.Size(823, 560);
            this.tbcntrl_main.TabIndex = 0;
            // 
            // pnl_sidebar
            // 
            this.pnl_sidebar.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnl_sidebar.Controls.Add(this.tbcntrl_left);
            this.pnl_sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_sidebar.Location = new System.Drawing.Point(0, 0);
            this.pnl_sidebar.Name = "pnl_sidebar";
            this.pnl_sidebar.Size = new System.Drawing.Size(197, 557);
            this.pnl_sidebar.TabIndex = 47;
            // 
            // tbcntrl_left
            // 
            this.tbcntrl_left.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_left.Controls.Add(this.tpg_option);
            this.tbcntrl_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_left.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_left.Name = "tbcntrl_left";
            this.tbcntrl_left.SelectedIndex = 0;
            this.tbcntrl_left.Size = new System.Drawing.Size(197, 557);
            this.tbcntrl_left.TabIndex = 1;
            // 
            // tpg_option
            // 
            this.tpg_option.BackColor = System.Drawing.Color.DarkKhaki;
            this.tpg_option.Controls.Add(this.panel5);
            this.tpg_option.Location = new System.Drawing.Point(4, 4);
            this.tpg_option.Name = "tpg_option";
            this.tpg_option.Size = new System.Drawing.Size(189, 531);
            this.tpg_option.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel5.Controls.Add(this.groupBox4);
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(189, 531);
            this.panel5.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btn_print);
            this.groupBox4.Controls.Add(this.btn_upd);
            this.groupBox4.Controls.Add(this.btn_new);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 337);
            this.groupBox4.TabIndex = 64;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Main Option";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Peru;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Info;
            this.button1.Image = global::Accounting_Application_System.Properties.Resources.aging_report;
            this.button1.Location = new System.Drawing.Point(16, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 72);
            this.button1.TabIndex = 4;
            this.button1.Text = "Go to Service History";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.Peru;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_print.Image = global::Accounting_Application_System.Properties.Resources.print___45x45;
            this.btn_print.Location = new System.Drawing.Point(16, 254);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(156, 72);
            this.btn_print.TabIndex = 3;
            this.btn_print.Text = "Print";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            // 
            // btn_upd
            // 
            this.btn_upd.BackColor = System.Drawing.Color.Peru;
            this.btn_upd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_upd.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_upd.Location = new System.Drawing.Point(16, 98);
            this.btn_upd.Name = "btn_upd";
            this.btn_upd.Size = new System.Drawing.Size(156, 72);
            this.btn_upd.TabIndex = 1;
            this.btn_upd.Text = "Update";
            this.btn_upd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_upd.UseVisualStyleBackColor = false;
            this.btn_upd.Click += new System.EventHandler(this.btn_upd_Click);
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.Color.Peru;
            this.btn_new.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_new.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_new.Location = new System.Drawing.Point(16, 20);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(156, 72);
            this.btn_new.TabIndex = 0;
            this.btn_new.Text = "  Create";
            this.btn_new.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.btn_search);
            this.groupBox5.Controls.Add(this.textBox15);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox5.Location = new System.Drawing.Point(0, 377);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(189, 154);
            this.groupBox5.TabIndex = 63;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.DarkGray;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 21);
            this.comboBox1.TabIndex = 61;
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(16, 87);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(160, 51);
            this.btn_search.TabIndex = 60;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            // 
            // textBox15
            // 
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.Location = new System.Drawing.Point(18, 55);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(158, 26);
            this.textBox15.TabIndex = 59;
            // 
            // call_history
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1020, 557);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_sidebar);
            this.Name = "call_history";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Call History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.call_history_Load_1);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.tpg_right_list.ResumeLayout(false);
            this.tpg_right_list.PerformLayout();
            this.pnl_main.ResumeLayout(false);
            this.tbcntrl_main.ResumeLayout(false);
            this.pnl_sidebar.ResumeLayout(false);
            this.tbcntrl_left.ResumeLayout(false);
            this.tpg_option.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TabPage tpg_right_list;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.TabControl tbcntrl_main;
        private System.Windows.Forms.Panel pnl_sidebar;
        private System.Windows.Forms.TabControl tbcntrl_left;
        private System.Windows.Forms.TabPage tpg_option;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_upd;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_call_history_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_client_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_client_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_contact_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_date_to_call;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_time_to_call;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_date_time_last_called;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_remark;
    }
}