namespace Hotel_System
{
    partial class GenerateMealCoupon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel35 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_genmeal = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.dtp_searchdate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_meal = new System.Windows.Forms.DataGridView();
            this.panel64 = new System.Windows.Forms.Panel();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_generatedmeal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_trnxdate = new System.Windows.Forms.DateTimePicker();
            this.panel35.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_genmeal)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meal)).BeginInit();
            this.panel64.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel35
            // 
            this.panel35.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel35.Controls.Add(this.groupBox1);
            this.panel35.Controls.Add(this.groupBox5);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel35.Location = new System.Drawing.Point(0, 0);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(399, 742);
            this.panel35.TabIndex = 34;
            this.panel35.Paint += new System.Windows.Forms.PaintEventHandler(this.panel35_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgv_genmeal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(5, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 442);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generated Meal History";
            // 
            // dgv_genmeal
            // 
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgv_genmeal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_genmeal.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_genmeal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_genmeal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_genmeal.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_genmeal.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_genmeal.Location = new System.Drawing.Point(6, 20);
            this.dgv_genmeal.MultiSelect = false;
            this.dgv_genmeal.Name = "dgv_genmeal";
            this.dgv_genmeal.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_genmeal.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgv_genmeal.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_genmeal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_genmeal.Size = new System.Drawing.Size(376, 415);
            this.dgv_genmeal.TabIndex = 1;
            this.dgv_genmeal.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_genmeal_CellPainting);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.btn_search);
            this.groupBox5.Controls.Add(this.dtp_searchdate);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox5.Location = new System.Drawing.Point(5, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(388, 80);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 49;
            this.label2.Text = "Guest Folio Num";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(128, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 22);
            this.textBox1.TabIndex = 48;
            // 
            // btn_search
            // 
            this.btn_search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_search.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(272, 22);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(110, 43);
            this.btn_search.TabIndex = 47;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dtp_searchdate
            // 
            this.dtp_searchdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_searchdate.Location = new System.Drawing.Point(128, 18);
            this.dtp_searchdate.Name = "dtp_searchdate";
            this.dtp_searchdate.Size = new System.Drawing.Size(138, 22);
            this.dtp_searchdate.TabIndex = 45;
            this.dtp_searchdate.Value = new System.DateTime(2013, 2, 4, 0, 0, 0, 0);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 16);
            this.label17.TabIndex = 43;
            this.label17.Text = "Search Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_meal);
            this.groupBox2.Controls.Add(this.panel64);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(399, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(621, 742);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate Meal Coupon";
            // 
            // dgv_meal
            // 
            this.dgv_meal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_meal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_meal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_meal.Location = new System.Drawing.Point(3, 76);
            this.dgv_meal.Name = "dgv_meal";
            this.dgv_meal.Size = new System.Drawing.Size(615, 448);
            this.dgv_meal.TabIndex = 19;
            this.dgv_meal.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_meal_CellPainting);
            // 
            // panel64
            // 
            this.panel64.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel64.Controls.Add(this.btn_print);
            this.panel64.Controls.Add(this.btn_generatedmeal);
            this.panel64.Controls.Add(this.label1);
            this.panel64.Controls.Add(this.dtp_trnxdate);
            this.panel64.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel64.Location = new System.Drawing.Point(3, 18);
            this.panel64.Name = "panel64";
            this.panel64.Size = new System.Drawing.Size(615, 62);
            this.panel64.TabIndex = 18;
            // 
            // btn_print
            // 
            this.btn_print.Image = global::Hotel_System.Properties.Resources.Print;
            this.btn_print.Location = new System.Drawing.Point(659, 13);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(111, 39);
            this.btn_print.TabIndex = 50;
            this.btn_print.Text = "Print";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = true;
            // 
            // btn_generatedmeal
            // 
            this.btn_generatedmeal.Image = global::Hotel_System.Properties.Resources.play;
            this.btn_generatedmeal.Location = new System.Drawing.Point(273, 12);
            this.btn_generatedmeal.Name = "btn_generatedmeal";
            this.btn_generatedmeal.Size = new System.Drawing.Size(195, 40);
            this.btn_generatedmeal.TabIndex = 49;
            this.btn_generatedmeal.Text = "Generate Meal Coupon";
            this.btn_generatedmeal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_generatedmeal.UseVisualStyleBackColor = true;
            this.btn_generatedmeal.Click += new System.EventHandler(this.btn_generatedmeal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "Transaction Date";
            // 
            // dtp_trnxdate
            // 
            this.dtp_trnxdate.Enabled = false;
            this.dtp_trnxdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_trnxdate.Location = new System.Drawing.Point(142, 19);
            this.dtp_trnxdate.Name = "dtp_trnxdate";
            this.dtp_trnxdate.Size = new System.Drawing.Size(111, 22);
            this.dtp_trnxdate.TabIndex = 48;
            this.dtp_trnxdate.Value = new System.DateTime(2013, 2, 4, 0, 0, 0, 0);
            // 
            // GenerateMealCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1020, 742);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel35);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GenerateMealCoupon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate Meal Coupon";
            this.Load += new System.EventHandler(this.GenerateMealCoupon_Load);
            this.panel35.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_genmeal)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meal)).EndInit();
            this.panel64.ResumeLayout(false);
            this.panel64.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_genmeal;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtp_searchdate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_meal;
        private System.Windows.Forms.Button btn_generatedmeal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_trnxdate;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Panel panel35;
        public System.Windows.Forms.Panel panel64;
    }
}