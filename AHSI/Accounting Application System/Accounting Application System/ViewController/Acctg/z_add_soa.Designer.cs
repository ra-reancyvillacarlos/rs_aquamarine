namespace Accounting_Application_System
{
    partial class z_add_soa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_viewby = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_soa = new System.Windows.Forms.TextBox();
            this.txt_gfolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgv_list_folio = new System.Windows.Forms.DataGridView();
            this.dgvl1_soa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_billedclient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_rom_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_soadate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_chg_dtfrm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_chg_dtto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_bal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_rmrttyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_acct_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl1_clientcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_folio)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 15);
            this.label4.TabIndex = 77;
            this.label4.Text = "Billed Client";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_viewby);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_soa);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_gfolio);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Location = new System.Drawing.Point(14, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(795, 106);
            this.groupBox2.TabIndex = 76;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // cbo_viewby
            // 
            this.cbo_viewby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_viewby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_viewby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_viewby.FormattingEnabled = true;
            this.cbo_viewby.Items.AddRange(new object[] {
            "SOA with Balances Only",
            "In House Folios"});
            this.cbo_viewby.Location = new System.Drawing.Point(62, 14);
            this.cbo_viewby.Name = "cbo_viewby";
            this.cbo_viewby.Size = new System.Drawing.Size(230, 23);
            this.cbo_viewby.TabIndex = 80;
            this.cbo_viewby.SelectedIndexChanged += new System.EventHandler(this.cbo_viewby_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 79;
            this.label2.Text = "SOA Number";
            // 
            // txt_soa
            // 
            this.txt_soa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_soa.Location = new System.Drawing.Point(392, 53);
            this.txt_soa.Name = "txt_soa";
            this.txt_soa.Size = new System.Drawing.Size(201, 21);
            this.txt_soa.TabIndex = 78;
            // 
            // txt_gfolio
            // 
            this.txt_gfolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gfolio.Location = new System.Drawing.Point(85, 50);
            this.txt_gfolio.Name = "txt_gfolio";
            this.txt_gfolio.Size = new System.Drawing.Size(207, 21);
            this.txt_gfolio.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 74;
            this.label1.Text = "View By";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.Info;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.journal3;
            this.btn_search.Location = new System.Drawing.Point(609, 33);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgv_list_folio);
            this.groupBox5.Location = new System.Drawing.Point(14, 118);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(795, 382);
            this.groupBox5.TabIndex = 74;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SOA / Folio List";
            // 
            // dgv_list_folio
            // 
            this.dgv_list_folio.AllowUserToDeleteRows = false;
            this.dgv_list_folio.AllowUserToResizeRows = false;
            this.dgv_list_folio.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_list_folio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_list_folio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_list_folio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl1_soa,
            this.dgvl1_billedclient,
            this.dgvl1_rom_code,
            this.dgvl_soadate,
            this.dgvl1_chg_dtfrm,
            this.dgvl1_chg_dtto,
            this.dgvl1_bal,
            this.dgvl1_userid,
            this.dgvl1_rmrttyp,
            this.dgvl1_acct_no,
            this.dgvl1_clientcode});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_list_folio.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_list_folio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list_folio.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_list_folio.Location = new System.Drawing.Point(3, 17);
            this.dgv_list_folio.MultiSelect = false;
            this.dgv_list_folio.Name = "dgv_list_folio";
            this.dgv_list_folio.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list_folio.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_list_folio.RowHeadersWidth = 15;
            this.dgv_list_folio.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list_folio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list_folio.Size = new System.Drawing.Size(789, 362);
            this.dgv_list_folio.TabIndex = 19;
            this.dgv_list_folio.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_folio_CellPainting);
            // 
            // dgvl1_soa
            // 
            this.dgvl1_soa.HeaderText = "SOA#";
            this.dgvl1_soa.Name = "dgvl1_soa";
            this.dgvl1_soa.ReadOnly = true;
            this.dgvl1_soa.Width = 77;
            // 
            // dgvl1_billedclient
            // 
            this.dgvl1_billedclient.HeaderText = "BILLED CLIENT";
            this.dgvl1_billedclient.Name = "dgvl1_billedclient";
            this.dgvl1_billedclient.ReadOnly = true;
            this.dgvl1_billedclient.Width = 210;
            // 
            // dgvl1_rom_code
            // 
            this.dgvl1_rom_code.FillWeight = 50F;
            this.dgvl1_rom_code.HeaderText = "ROOM";
            this.dgvl1_rom_code.Name = "dgvl1_rom_code";
            this.dgvl1_rom_code.ReadOnly = true;
            this.dgvl1_rom_code.Width = 50;
            // 
            // dgvl_soadate
            // 
            this.dgvl_soadate.HeaderText = "SOA DATE";
            this.dgvl_soadate.Name = "dgvl_soadate";
            this.dgvl_soadate.ReadOnly = true;
            // 
            // dgvl1_chg_dtfrm
            // 
            this.dgvl1_chg_dtfrm.HeaderText = "CHARGE DATE FROM";
            this.dgvl1_chg_dtfrm.Name = "dgvl1_chg_dtfrm";
            this.dgvl1_chg_dtfrm.ReadOnly = true;
            this.dgvl1_chg_dtfrm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvl1_chg_dtto
            // 
            this.dgvl1_chg_dtto.HeaderText = "CHARGE DATE TO";
            this.dgvl1_chg_dtto.MinimumWidth = 100;
            this.dgvl1_chg_dtto.Name = "dgvl1_chg_dtto";
            this.dgvl1_chg_dtto.ReadOnly = true;
            this.dgvl1_chg_dtto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvl1_bal
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvl1_bal.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvl1_bal.HeaderText = "AMOUNT";
            this.dgvl1_bal.Name = "dgvl1_bal";
            this.dgvl1_bal.ReadOnly = true;
            this.dgvl1_bal.Width = 125;
            // 
            // dgvl1_userid
            // 
            this.dgvl1_userid.HeaderText = "SOA USER ID";
            this.dgvl1_userid.Name = "dgvl1_userid";
            this.dgvl1_userid.ReadOnly = true;
            // 
            // dgvl1_rmrttyp
            // 
            this.dgvl1_rmrttyp.HeaderText = "TYPE";
            this.dgvl1_rmrttyp.Name = "dgvl1_rmrttyp";
            this.dgvl1_rmrttyp.ReadOnly = true;
            this.dgvl1_rmrttyp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvl1_rmrttyp.Width = 50;
            // 
            // dgvl1_acct_no
            // 
            this.dgvl1_acct_no.HeaderText = "CLIENT NO.";
            this.dgvl1_acct_no.Name = "dgvl1_acct_no";
            this.dgvl1_acct_no.ReadOnly = true;
            this.dgvl1_acct_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvl1_acct_no.Width = 77;
            // 
            // dgvl1_clientcode
            // 
            this.dgvl1_clientcode.HeaderText = "BILLED CLIENT CODE";
            this.dgvl1_clientcode.Name = "dgvl1_clientcode";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Peru;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_close.Location = new System.Drawing.Point(581, 503);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 41);
            this.btn_close.TabIndex = 77;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.Peru;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_submit.Location = new System.Drawing.Point(697, 502);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 41);
            this.btn_submit.TabIndex = 75;
            this.btn_submit.Text = "Add to List";
            this.btn_submit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // z_add_soa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(821, 554);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_submit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_add_soa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add SOA";
            this.Load += new System.EventHandler(this.z_add_soa_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_folio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_gfolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_soa;
        private System.Windows.Forms.DataGridView dgv_list_folio;
        private System.Windows.Forms.ComboBox cbo_viewby;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_soa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_billedclient;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_rom_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_soadate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_chg_dtfrm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_chg_dtto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_bal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_rmrttyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_acct_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl1_clientcode;
    }
}