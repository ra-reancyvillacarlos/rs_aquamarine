namespace Accounting_Application_System
{
    partial class call_history_entry
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
            this.btn_customer = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_contact_no = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rtxt_remark = new System.Windows.Forms.RichTextBox();
            this.cbo_status = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_time_to_call = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_date_to_call = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_client = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.dgv_call_history_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_contact_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_date_to_call = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_time_to_call = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_customer);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_contact_no);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rtxt_remark);
            this.groupBox1.Controls.Add(this.cbo_status);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dtp_time_to_call);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtp_date_to_call);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_code);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbo_client);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 504);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Call History Entry";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btn_customer
            // 
            this.btn_customer.BackColor = System.Drawing.SystemColors.Info;
            this.btn_customer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_customer.Image = global::Accounting_Application_System.Properties.Resources.user;
            this.btn_customer.Location = new System.Drawing.Point(392, 51);
            this.btn_customer.Name = "btn_customer";
            this.btn_customer.Size = new System.Drawing.Size(40, 23);
            this.btn_customer.TabIndex = 28;
            this.btn_customer.UseVisualStyleBackColor = false;
            this.btn_customer.Click += new System.EventHandler(this.btn_customer_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(87, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 15);
            this.label10.TabIndex = 27;
            this.label10.Text = "Contact No.";
            // 
            // txt_contact_no
            // 
            this.txt_contact_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_contact_no.Location = new System.Drawing.Point(163, 81);
            this.txt_contact_no.Name = "txt_contact_no";
            this.txt_contact_no.ReadOnly = true;
            this.txt_contact_no.Size = new System.Drawing.Size(185, 21);
            this.txt_contact_no.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(54, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "Remarks";
            // 
            // rtxt_remark
            // 
            this.rtxt_remark.Location = new System.Drawing.Point(163, 196);
            this.rtxt_remark.Name = "rtxt_remark";
            this.rtxt_remark.Size = new System.Drawing.Size(289, 121);
            this.rtxt_remark.TabIndex = 24;
            this.rtxt_remark.Text = "";
            // 
            // cbo_status
            // 
            this.cbo_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Items.AddRange(new object[] {
            "Called ",
            "Not Yet Called",
            "Return Call",
            "Dialled Call",
            "Missed Call",
            "Received Call"});
            this.cbo_status.Location = new System.Drawing.Point(163, 164);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(223, 23);
            this.cbo_status.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(54, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Status";
            // 
            // dtp_time_to_call
            // 
            this.dtp_time_to_call.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_time_to_call.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_time_to_call.Location = new System.Drawing.Point(163, 137);
            this.dtp_time_to_call.Name = "dtp_time_to_call";
            this.dtp_time_to_call.Size = new System.Drawing.Size(185, 21);
            this.dtp_time_to_call.TabIndex = 19;
            this.dtp_time_to_call.ValueChanged += new System.EventHandler(this.dtp_time_to_call_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(79, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Time To Call";
            // 
            // dtp_date_to_call
            // 
            this.dtp_date_to_call.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_date_to_call.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date_to_call.Location = new System.Drawing.Point(163, 109);
            this.dtp_date_to_call.Name = "dtp_date_to_call";
            this.dtp_date_to_call.Size = new System.Drawing.Size(185, 21);
            this.dtp_date_to_call.TabIndex = 17;
            this.dtp_date_to_call.ValueChanged += new System.EventHandler(this.dtp_date_to_call_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(103, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Client";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_code.Location = new System.Drawing.Point(163, 23);
            this.txt_code.Name = "txt_code";
            this.txt_code.ReadOnly = true;
            this.txt_code.Size = new System.Drawing.Size(114, 21);
            this.txt_code.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(79, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date To Call";
            // 
            // cbo_client
            // 
            this.cbo_client.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_client.FormattingEnabled = true;
            this.cbo_client.Location = new System.Drawing.Point(163, 51);
            this.cbo_client.Name = "cbo_client";
            this.cbo_client.Size = new System.Drawing.Size(223, 23);
            this.cbo_client.TabIndex = 1;
            this.cbo_client.SelectedIndexChanged += new System.EventHandler(this.cbo_client_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Maroon;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(315, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "EXIT";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Window;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(419, 512);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 40);
            this.button3.TabIndex = 3;
            this.button3.Text = "SAVE";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox2.Controls.Add(this.dgv_list);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox2.Location = new System.Drawing.Point(3, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 178);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client\'s Call History";
            // 
            // dgv_list
            // 
            this.dgv_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_list.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_call_history_number,
            this.dgv_contact_no,
            this.dgv_date_to_call,
            this.dgv_time_to_call,
            this.dgv_userid,
            this.dgv_status,
            this.dgv_remark});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 20;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(517, 158);
            this.dgv_list.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(40, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 15);
            this.label16.TabIndex = 0;
            // 
            // dgv_call_history_number
            // 
            this.dgv_call_history_number.HeaderText = "Call History #";
            this.dgv_call_history_number.Name = "dgv_call_history_number";
            this.dgv_call_history_number.ReadOnly = true;
            this.dgv_call_history_number.Width = 103;
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
            // call_history_entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(553, 564);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Name = "call_history_entry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Call History Entry";
            this.Load += new System.EventHandler(this.call_history_entry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.DateTimePicker dtp_time_to_call;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_date_to_call;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtxt_remark;
        private System.Windows.Forms.ComboBox cbo_status;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_contact_no;
        private System.Windows.Forms.Button btn_customer;
        public System.Windows.Forms.ComboBox cbo_client;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_call_history_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_contact_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_date_to_call;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_time_to_call;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_remark;
    }
}