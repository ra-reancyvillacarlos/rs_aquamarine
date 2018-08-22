namespace Accounting_Application_System
{
    partial class z_add_folio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgv_list_folio = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_gfolio = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.dgvl1_chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvl1_lnnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_gfolio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acct_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chg_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chg_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttlpax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_chg_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_invoice_balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_out_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_out_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_chg_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_chg_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_folio)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_close);
            this.groupBox5.Controls.Add(this.dgv_list_folio);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 62);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(806, 492);
            this.groupBox5.TabIndex = 65;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Guest Folio Charges List";
            // 
            // dgv_list_folio
            // 
            this.dgv_list_folio.AllowUserToDeleteRows = false;
            this.dgv_list_folio.AllowUserToResizeRows = false;
            this.dgv_list_folio.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_list_folio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list_folio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_list_folio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_list_folio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl1_chk,
            this.dgvl1_lnnum,
            this.dgvl1_gfolio,
            this.acct_no,
            this.full_name,
            this.name,
            this.chg_code,
            this.chg_desc,
            this.ttlpax,
            this.dgvl1_chg_desc,
            this.dgvl1_invoice,
            this.dgvl1_invoice_balance,
            this.dgvl1_paid,
            this.dgvl1_customer,
            this.dgvl1_date,
            this.dgvl1_time,
            this.dgvl1_description,
            this.dgvl1_out_code,
            this.dgvl1_out_desc,
            this.dgvl1_chg_code,
            this.dgvl1_chg_num});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_list_folio.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_list_folio.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_list_folio.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_list_folio.Location = new System.Drawing.Point(3, 16);
            this.dgv_list_folio.MultiSelect = false;
            this.dgv_list_folio.Name = "dgv_list_folio";
            this.dgv_list_folio.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list_folio.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_list_folio.RowHeadersWidth = 15;
            this.dgv_list_folio.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list_folio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list_folio.Size = new System.Drawing.Size(800, 404);
            this.dgv_list_folio.TabIndex = 16;
            this.dgv_list_folio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_folio_CellContentClick);
            this.dgv_list_folio.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_folio_CellDoubleClick);
            this.dgv_list_folio.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_folio_CellPainting);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_gfolio);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(806, 62);
            this.groupBox2.TabIndex = 72;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Tennant Or  Guest Folio";
            // 
            // txt_gfolio
            // 
            this.txt_gfolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gfolio.Location = new System.Drawing.Point(19, 36);
            this.txt_gfolio.Name = "txt_gfolio";
            this.txt_gfolio.Size = new System.Drawing.Size(261, 21);
            this.txt_gfolio.TabIndex = 75;
            this.txt_gfolio.TextChanged += new System.EventHandler(this.txt_gfolio_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.Info;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.journal3;
            this.btn_search.Location = new System.Drawing.Point(285, 16);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(112, 41);
            this.btn_search.TabIndex = 73;
            this.btn_search.Text = "Search";
            this.btn_search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.Info;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_close.Location = new System.Drawing.Point(562, 439);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 41);
            this.btn_close.TabIndex = 73;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.SystemColors.Info;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_submit.Location = new System.Drawing.Point(678, 501);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 41);
            this.btn_submit.TabIndex = 69;
            this.btn_submit.Text = "Add to List";
            this.btn_submit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // dgvl1_chk
            // 
            this.dgvl1_chk.HeaderText = "";
            this.dgvl1_chk.Name = "dgvl1_chk";
            this.dgvl1_chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvl1_chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvl1_chk.Width = 40;
            // 
            // dgvl1_lnnum
            // 
            this.dgvl1_lnnum.HeaderText = "Line";
            this.dgvl1_lnnum.Name = "dgvl1_lnnum";
            this.dgvl1_lnnum.ReadOnly = true;
            this.dgvl1_lnnum.Width = 50;
            // 
            // dgvl1_gfolio
            // 
            this.dgvl1_gfolio.HeaderText = "GFolio";
            this.dgvl1_gfolio.Name = "dgvl1_gfolio";
            this.dgvl1_gfolio.ReadOnly = true;
            // 
            // acct_no
            // 
            this.acct_no.HeaderText = "Account No.";
            this.acct_no.Name = "acct_no";
            this.acct_no.ReadOnly = true;
            this.acct_no.Visible = false;
            // 
            // full_name
            // 
            this.full_name.HeaderText = "Name";
            this.full_name.Name = "full_name";
            this.full_name.ReadOnly = true;
            this.full_name.Visible = false;
            // 
            // name
            // 
            this.name.HeaderText = "Hotel Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Visible = false;
            // 
            // chg_code
            // 
            this.chg_code.HeaderText = "Charge Code(s)";
            this.chg_code.Name = "chg_code";
            this.chg_code.ReadOnly = true;
            this.chg_code.Visible = false;
            // 
            // chg_desc
            // 
            this.chg_desc.HeaderText = "Description(s)";
            this.chg_desc.Name = "chg_desc";
            this.chg_desc.ReadOnly = true;
            this.chg_desc.Visible = false;
            // 
            // ttlpax
            // 
            this.ttlpax.HeaderText = "Pax";
            this.ttlpax.Name = "ttlpax";
            this.ttlpax.ReadOnly = true;
            this.ttlpax.Visible = false;
            // 
            // dgvl1_chg_desc
            // 
            this.dgvl1_chg_desc.HeaderText = "Charges";
            this.dgvl1_chg_desc.Name = "dgvl1_chg_desc";
            this.dgvl1_chg_desc.ReadOnly = true;
            // 
            // dgvl1_invoice
            // 
            this.dgvl1_invoice.HeaderText = "Invoice";
            this.dgvl1_invoice.Name = "dgvl1_invoice";
            this.dgvl1_invoice.ReadOnly = true;
            this.dgvl1_invoice.Width = 70;
            // 
            // dgvl1_invoice_balance
            // 
            this.dgvl1_invoice_balance.HeaderText = "Invoice Balance";
            this.dgvl1_invoice_balance.Name = "dgvl1_invoice_balance";
            this.dgvl1_invoice_balance.ReadOnly = true;
            // 
            // dgvl1_paid
            // 
            this.dgvl1_paid.HeaderText = "Paid";
            this.dgvl1_paid.Name = "dgvl1_paid";
            this.dgvl1_paid.ReadOnly = true;
            // 
            // dgvl1_customer
            // 
            this.dgvl1_customer.HeaderText = "Customer";
            this.dgvl1_customer.Name = "dgvl1_customer";
            this.dgvl1_customer.ReadOnly = true;
            // 
            // dgvl1_date
            // 
            this.dgvl1_date.HeaderText = "Charge Date";
            this.dgvl1_date.Name = "dgvl1_date";
            this.dgvl1_date.ReadOnly = true;
            // 
            // dgvl1_time
            // 
            this.dgvl1_time.HeaderText = "Time";
            this.dgvl1_time.Name = "dgvl1_time";
            this.dgvl1_time.ReadOnly = true;
            // 
            // dgvl1_description
            // 
            this.dgvl1_description.HeaderText = "Description";
            this.dgvl1_description.Name = "dgvl1_description";
            this.dgvl1_description.ReadOnly = true;
            // 
            // dgvl1_out_code
            // 
            this.dgvl1_out_code.HeaderText = "Out Code";
            this.dgvl1_out_code.Name = "dgvl1_out_code";
            this.dgvl1_out_code.ReadOnly = true;
            this.dgvl1_out_code.Visible = false;
            // 
            // dgvl1_out_desc
            // 
            this.dgvl1_out_desc.HeaderText = "Out Desc";
            this.dgvl1_out_desc.Name = "dgvl1_out_desc";
            this.dgvl1_out_desc.ReadOnly = true;
            this.dgvl1_out_desc.Visible = false;
            // 
            // dgvl1_chg_code
            // 
            this.dgvl1_chg_code.HeaderText = "Chg Code";
            this.dgvl1_chg_code.Name = "dgvl1_chg_code";
            this.dgvl1_chg_code.ReadOnly = true;
            // 
            // dgvl1_chg_num
            // 
            this.dgvl1_chg_num.HeaderText = "Chg Num";
            this.dgvl1_chg_num.Name = "dgvl1_chg_num";
            this.dgvl1_chg_num.ReadOnly = true;
            // 
            // z_add_folio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(806, 554);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_add_folio";
            this.Text = "Add Charges from Hotel";
            this.Load += new System.EventHandler(this.z_add_folio_Load);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_folio)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_gfolio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridView dgv_list_folio;
        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvl1_chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_lnnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_gfolio;
        private System.Windows.Forms.DataGridViewTextBoxColumn acct_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn chg_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn chg_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttlpax;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_chg_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_invoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_invoice_balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_paid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_out_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_out_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_chg_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_chg_num;
    }
}