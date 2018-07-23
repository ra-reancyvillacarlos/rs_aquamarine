namespace Hotel_System
{
    partial class z_Research
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.dtp_checkout = new System.Windows.Forms.DateTimePicker();
            this.dtp_checkin = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.cbo_type = new System.Windows.Forms.ComboBox();
            this.txt_noofnights = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_rom = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_res = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_occ = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_row = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_rom_available = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rom_available)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 100);
            this.panel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox5.Controls.Add(this.btn_search);
            this.groupBox5.Controls.Add(this.dtp_checkout);
            this.groupBox5.Controls.Add(this.dtp_checkin);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label93);
            this.groupBox5.Controls.Add(this.cbo_type);
            this.groupBox5.Controls.Add(this.txt_noofnights);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(460, 100);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search for Reservation";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.Info;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_search.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(293, 44);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(145, 50);
            this.btn_search.TabIndex = 47;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dtp_checkout
            // 
            this.dtp_checkout.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_checkout.Location = new System.Drawing.Point(88, 45);
            this.dtp_checkout.Name = "dtp_checkout";
            this.dtp_checkout.Size = new System.Drawing.Size(180, 20);
            this.dtp_checkout.TabIndex = 46;
            this.dtp_checkout.ValueChanged += new System.EventHandler(this.dtp_checkout_ValueChanged);
            // 
            // dtp_checkin
            // 
            this.dtp_checkin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_checkin.Location = new System.Drawing.Point(88, 19);
            this.dtp_checkin.Name = "dtp_checkin";
            this.dtp_checkin.Size = new System.Drawing.Size(180, 20);
            this.dtp_checkin.TabIndex = 45;
            this.dtp_checkin.ValueChanged += new System.EventHandler(this.dtp_checkin_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 44;
            this.label16.Text = "Check Out";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 43;
            this.label17.Text = "Check In";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 78);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 39;
            this.label19.Text = "Unit Type";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(290, 19);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(69, 13);
            this.label93.TabIndex = 41;
            this.label93.Text = "No. of Nights";
            // 
            // cbo_type
            // 
            this.cbo_type.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_type.FormattingEnabled = true;
            this.cbo_type.Location = new System.Drawing.Point(88, 75);
            this.cbo_type.Name = "cbo_type";
            this.cbo_type.Size = new System.Drawing.Size(183, 21);
            this.cbo_type.TabIndex = 38;
            this.cbo_type.SelectedIndexChanged += new System.EventHandler(this.cbo_type_SelectedIndexChanged);
            // 
            // txt_noofnights
            // 
            this.txt_noofnights.Location = new System.Drawing.Point(365, 15);
            this.txt_noofnights.Name = "txt_noofnights";
            this.txt_noofnights.Size = new System.Drawing.Size(54, 20);
            this.txt_noofnights.TabIndex = 40;
            this.txt_noofnights.Text = "1";
            this.txt_noofnights.TextChanged += new System.EventHandler(this.txt_noofnights_TextChanged);
            this.txt_noofnights.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_noofnights_KeyDown);
            this.txt_noofnights.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_noofnights_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 404);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox1.Controls.Add(this.lbl_rom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_res);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbl_occ);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbl_row);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dgv_rom_available);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 404);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Units Available";
            // 
            // lbl_rom
            // 
            this.lbl_rom.AutoSize = true;
            this.lbl_rom.Location = new System.Drawing.Point(86, 22);
            this.lbl_rom.Name = "lbl_rom";
            this.lbl_rom.Size = new System.Drawing.Size(13, 13);
            this.lbl_rom.TabIndex = 9;
            this.lbl_rom.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Total Units:";
            // 
            // lbl_res
            // 
            this.lbl_res.AutoSize = true;
            this.lbl_res.Location = new System.Drawing.Point(47, 42);
            this.lbl_res.Name = "lbl_res";
            this.lbl_res.Size = new System.Drawing.Size(13, 13);
            this.lbl_res.TabIndex = 7;
            this.lbl_res.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Res:";
            // 
            // lbl_occ
            // 
            this.lbl_occ.AutoSize = true;
            this.lbl_occ.Location = new System.Drawing.Point(176, 21);
            this.lbl_occ.Name = "lbl_occ";
            this.lbl_occ.Size = new System.Drawing.Size(13, 13);
            this.lbl_occ.TabIndex = 5;
            this.lbl_occ.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "In-House:";
            // 
            // lbl_row
            // 
            this.lbl_row.AutoSize = true;
            this.lbl_row.Location = new System.Drawing.Point(192, 43);
            this.lbl_row.Name = "lbl_row";
            this.lbl_row.Size = new System.Drawing.Size(13, 13);
            this.lbl_row.TabIndex = 3;
            this.lbl_row.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit Available";
            // 
            // dgv_rom_available
            // 
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgv_rom_available.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_rom_available.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv_rom_available.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_rom_available.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_rom_available.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_rom_available.Location = new System.Drawing.Point(13, 69);
            this.dgv_rom_available.MultiSelect = false;
            this.dgv_rom_available.Name = "dgv_rom_available";
            this.dgv_rom_available.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_rom_available.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.dgv_rom_available.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_rom_available.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rom_available.Size = new System.Drawing.Size(435, 324);
            this.dgv_rom_available.TabIndex = 1;
            this.dgv_rom_available.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rom_available_CellContentClick);
            this.dgv_rom_available.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rom_available_CellDoubleClick);
            // 
            // z_Research
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(460, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "z_Research";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Search List";
            this.Load += new System.EventHandler(this.z_Research_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rom_available)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DateTimePicker dtp_checkout;
        private System.Windows.Forms.DateTimePicker dtp_checkin;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.ComboBox cbo_type;
        private System.Windows.Forms.TextBox txt_noofnights;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_rom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_res;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_occ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_row;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv_rom_available;


    }
}