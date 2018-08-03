namespace Accounting_Application_System
{
    partial class z_Notification
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbcntrl_res = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_reslist = new System.Windows.Forms.DataGridView();
            this.res_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.res_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arr_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hotel_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttlpax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.package = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cd = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lunch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserv_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserv_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbcntrl_res.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reslist)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcntrl_res
            // 
            this.tbcntrl_res.Controls.Add(this.tabPage3);
            this.tbcntrl_res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_res.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcntrl_res.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_res.Name = "tbcntrl_res";
            this.tbcntrl_res.SelectedIndex = 0;
            this.tbcntrl_res.Size = new System.Drawing.Size(1354, 730);
            this.tbcntrl_res.TabIndex = 38;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1346, 701);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "Reservation List";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox6.Controls.Add(this.dgv_reslist);
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1340, 695);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Reservation List";
            // 
            // dgv_reslist
            // 
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgv_reslist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_reslist.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_reslist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_reslist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.res_code,
            this.res_date,
            this.arr_time,
            this.name,
            this.p_name,
            this.hotel_name,
            this.ttlpax,
            this.package,
            this.activities,
            this.cd,
            this.lunch,
            this.reserv_date,
            this.reserv_by,
            this.remarks,
            this.arrived});
            this.dgv_reslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_reslist.Location = new System.Drawing.Point(3, 18);
            this.dgv_reslist.MultiSelect = false;
            this.dgv_reslist.Name = "dgv_reslist";
            this.dgv_reslist.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_reslist.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_reslist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_reslist.Size = new System.Drawing.Size(1334, 607);
            this.dgv_reslist.TabIndex = 1;
            this.dgv_reslist.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_reslist_CellFormatting);
            // 
            // res_code
            // 
            this.res_code.DataPropertyName = "res_code";
            this.res_code.HeaderText = "Reservation Code";
            this.res_code.Name = "res_code";
            this.res_code.ReadOnly = true;
            // 
            // res_date
            // 
            this.res_date.DataPropertyName = "res_date";
            this.res_date.HeaderText = "Date";
            this.res_date.Name = "res_date";
            this.res_date.ReadOnly = true;
            // 
            // arr_time
            // 
            this.arr_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.arr_time.DataPropertyName = "arr_time";
            this.arr_time.HeaderText = "Time";
            this.arr_time.Name = "arr_time";
            this.arr_time.ReadOnly = true;
            this.arr_time.Width = 64;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.name.DataPropertyName = "full_name";
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 70;
            // 
            // p_name
            // 
            this.p_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.p_name.DataPropertyName = "p_name";
            this.p_name.HeaderText = "Payment";
            this.p_name.Name = "p_name";
            this.p_name.ReadOnly = true;
            this.p_name.Width = 86;
            // 
            // hotel_name
            // 
            this.hotel_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hotel_name.DataPropertyName = "name";
            this.hotel_name.HeaderText = "Hotel";
            this.hotel_name.Name = "hotel_name";
            this.hotel_name.ReadOnly = true;
            this.hotel_name.Width = 65;
            // 
            // ttlpax
            // 
            this.ttlpax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ttlpax.DataPropertyName = "ttlpax";
            this.ttlpax.HeaderText = "Pax";
            this.ttlpax.Name = "ttlpax";
            this.ttlpax.ReadOnly = true;
            this.ttlpax.Width = 56;
            // 
            // package
            // 
            this.package.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.package.DataPropertyName = "package";
            this.package.HeaderText = "Package";
            this.package.Name = "package";
            this.package.ReadOnly = true;
            this.package.Width = 88;
            // 
            // activities
            // 
            this.activities.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.activities.DataPropertyName = "activities";
            this.activities.HeaderText = "Activities";
            this.activities.Name = "activities";
            this.activities.ReadOnly = true;
            this.activities.Width = 86;
            // 
            // cd
            // 
            this.cd.DataPropertyName = "cd";
            this.cd.HeaderText = "CD";
            this.cd.Name = "cd";
            this.cd.ReadOnly = true;
            this.cd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // lunch
            // 
            this.lunch.DataPropertyName = "lunch";
            this.lunch.HeaderText = "Lunch";
            this.lunch.Name = "lunch";
            this.lunch.ReadOnly = true;
            // 
            // reserv_date
            // 
            this.reserv_date.DataPropertyName = "res_date";
            this.reserv_date.HeaderText = "Date of Boooking";
            this.reserv_date.Name = "reserv_date";
            this.reserv_date.ReadOnly = true;
            // 
            // reserv_by
            // 
            this.reserv_by.DataPropertyName = "reserv_by";
            this.reserv_by.HeaderText = "Booked by";
            this.reserv_by.Name = "reserv_by";
            this.reserv_by.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "Remarks";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 88;
            // 
            // arrived
            // 
            this.arrived.DataPropertyName = "arrived";
            this.arrived.HeaderText = "Arrived";
            this.arrived.Name = "arrived";
            this.arrived.ReadOnly = true;
            this.arrived.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 625);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1334, 67);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 67);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reservation total";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LemonChiffon;
            this.label2.Location = new System.Drawing.Point(175, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LemonChiffon;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total Reservations made:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(200, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(394, 67);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Package Pax total";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LemonChiffon;
            this.label7.Location = new System.Drawing.Point(322, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.LemonChiffon;
            this.label8.Location = new System.Drawing.Point(274, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Infant:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LemonChiffon;
            this.label5.Location = new System.Drawing.Point(201, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LemonChiffon;
            this.label6.Location = new System.Drawing.Point(165, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Kid:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LemonChiffon;
            this.label3.Location = new System.Drawing.Point(92, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LemonChiffon;
            this.label4.Location = new System.Drawing.Point(45, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Adult:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(594, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 67);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Activities total";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.LemonChiffon;
            this.label9.Location = new System.Drawing.Point(121, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LemonChiffon;
            this.label10.Location = new System.Drawing.Point(47, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Total Pax:";
            // 
            // z_Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1354, 730);
            this.Controls.Add(this.tbcntrl_res);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "z_Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.z_Notification_Load);
            this.tbcntrl_res.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reslist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcntrl_res;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_reslist;
        private System.Windows.Forms.DataGridViewTextBoxColumn res_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn res_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn arr_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn hotel_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttlpax;
        private System.Windows.Forms.DataGridViewTextBoxColumn package;
        private System.Windows.Forms.DataGridViewTextBoxColumn activities;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn lunch;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserv_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserv_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrived;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}