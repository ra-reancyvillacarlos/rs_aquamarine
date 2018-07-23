namespace Hotel_System
{
    partial class RoomAvailability
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label151 = new System.Windows.Forms.Label();
            this.btn_enter = new System.Windows.Forms.Button();
            this.dtp_chkin = new System.Windows.Forms.DateTimePicker();
            this.dtp_chkout = new System.Windows.Forms.DateTimePicker();
            this.label153 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.cbo_rmtype = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgv_listview = new System.Windows.Forms.DataGridView();
            this.room_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rom_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listview)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 733);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(3, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 121);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Legend";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(16, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 20);
            this.panel5.TabIndex = 50;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Yellow;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(16, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 20);
            this.panel4.TabIndex = 49;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Purple;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(16, 81);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 20);
            this.panel3.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "Out of Order";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(68, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 16);
            this.label6.TabIndex = 43;
            this.label6.Text = "Occuppied";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(68, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 16);
            this.label8.TabIndex = 41;
            this.label8.Text = "Reserved";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label151);
            this.groupBox1.Controls.Add(this.btn_enter);
            this.groupBox1.Controls.Add(this.dtp_chkin);
            this.groupBox1.Controls.Add(this.dtp_chkout);
            this.groupBox1.Controls.Add(this.label153);
            this.groupBox1.Controls.Add(this.label152);
            this.groupBox1.Controls.Add(this.cbo_rmtype);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 176);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(13, 29);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(50, 13);
            this.label151.TabIndex = 6;
            this.label151.Text = "Check In";
            // 
            // btn_enter
            // 
            this.btn_enter.BackColor = System.Drawing.SystemColors.Info;
            this.btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_enter.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.btn_enter.Location = new System.Drawing.Point(87, 112);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(200, 47);
            this.btn_enter.TabIndex = 2;
            this.btn_enter.Text = "Search for Reservation";
            this.btn_enter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_enter.UseVisualStyleBackColor = false;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // dtp_chkin
            // 
            this.dtp_chkin.Location = new System.Drawing.Point(87, 22);
            this.dtp_chkin.Name = "dtp_chkin";
            this.dtp_chkin.Size = new System.Drawing.Size(200, 20);
            this.dtp_chkin.TabIndex = 0;
            this.dtp_chkin.Value = new System.DateTime(2013, 2, 5, 0, 0, 0, 0);
            // 
            // dtp_chkout
            // 
            this.dtp_chkout.Location = new System.Drawing.Point(87, 52);
            this.dtp_chkout.Name = "dtp_chkout";
            this.dtp_chkout.Size = new System.Drawing.Size(200, 20);
            this.dtp_chkout.TabIndex = 1;
            this.dtp_chkout.Value = new System.DateTime(2013, 2, 5, 0, 0, 0, 0);
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(13, 90);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(53, 13);
            this.label153.TabIndex = 8;
            this.label153.Text = "Unit Type";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(13, 59);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(58, 13);
            this.label152.TabIndex = 7;
            this.label152.Text = "Check Out";
            // 
            // cbo_rmtype
            // 
            this.cbo_rmtype.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_rmtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rmtype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rmtype.FormattingEnabled = true;
            this.cbo_rmtype.Location = new System.Drawing.Point(87, 82);
            this.cbo_rmtype.Name = "cbo_rmtype";
            this.cbo_rmtype.Size = new System.Drawing.Size(200, 21);
            this.cbo_rmtype.TabIndex = 4;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox4);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(382, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(638, 733);
            this.panel9.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgv_listview);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox4.Size = new System.Drawing.Size(638, 733);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Unit Availability Result";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // dgv_listview
            // 
            this.dgv_listview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_listview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.room_no,
            this.rom_code,
            this.type,
            this.c1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_listview.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_listview.Location = new System.Drawing.Point(10, 25);
            this.dgv_listview.MultiSelect = false;
            this.dgv_listview.Name = "dgv_listview";
            this.dgv_listview.ReadOnly = true;
            this.dgv_listview.RowHeadersWidth = 25;
            this.dgv_listview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_listview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_listview.Size = new System.Drawing.Size(618, 698);
            this.dgv_listview.TabIndex = 1;
            this.dgv_listview.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_listview_CellFormatting);
            this.dgv_listview.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_listview_CellPainting);
            // 
            // room_no
            // 
            this.room_no.Frozen = true;
            this.room_no.HeaderText = "ROOM NO";
            this.room_no.Name = "room_no";
            this.room_no.ReadOnly = true;
            this.room_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.room_no.Width = 90;
            // 
            // rom_code
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.rom_code.DefaultCellStyle = dataGridViewCellStyle1;
            this.rom_code.FillWeight = 150F;
            this.rom_code.Frozen = true;
            this.rom_code.HeaderText = "UNIT DESCRIPTION";
            this.rom_code.Name = "rom_code";
            this.rom_code.ReadOnly = true;
            this.rom_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.rom_code.Width = 150;
            // 
            // type
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            this.type.DefaultCellStyle = dataGridViewCellStyle2;
            this.type.Frozen = true;
            this.type.HeaderText = "UNIT TYPE";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // c1
            // 
            this.c1.HeaderText = "Col1";
            this.c1.Name = "c1";
            this.c1.ReadOnly = true;
            this.c1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c1.Width = 70;
            // 
            // RoomAvailability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1020, 733);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RoomAvailability";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UnitAvailability";
            this.Load += new System.EventHandler(this.RoomAvailability_Load);
            this.Shown += new System.EventHandler(this.RoomAvailability_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Button btn_enter;
        private System.Windows.Forms.DateTimePicker dtp_chkin;
        private System.Windows.Forms.DateTimePicker dtp_chkout;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.ComboBox cbo_rmtype;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgv_listview;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn rom_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn c1;
    }
}