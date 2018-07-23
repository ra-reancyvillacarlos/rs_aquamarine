namespace Accounting_Application_System
{
    partial class a_postingtoledger
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_post = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_search_jnum = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_journal_id = new System.Windows.Forms.Label();
            this.lbl_period = new System.Windows.Forms.Label();
            this.lbl_journal = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_journal = new System.Windows.Forms.ComboBox();
            this.cbo_period = new System.Windows.Forms.ComboBox();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.ck_select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ref_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.t_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paidto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chk_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ck_selectall = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 562);
            this.panel1.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_print);
            this.groupBox4.Controls.Add(this.btn_post);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(0, 56);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(179, 170);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Main Option";
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.Peru;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_print.Location = new System.Drawing.Point(12, 89);
            this.btn_print.Margin = new System.Windows.Forms.Padding(2);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(155, 60);
            this.btn_print.TabIndex = 3;
            this.btn_print.Text = "View Journal Entry";
            this.btn_print.UseVisualStyleBackColor = false;
            // 
            // btn_post
            // 
            this.btn_post.BackColor = System.Drawing.Color.Peru;
            this.btn_post.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_post.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_post.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_post.Location = new System.Drawing.Point(12, 25);
            this.btn_post.Margin = new System.Windows.Forms.Padding(2);
            this.btn_post.Name = "btn_post";
            this.btn_post.Size = new System.Drawing.Size(155, 60);
            this.btn_post.TabIndex = 0;
            this.btn_post.Text = "Post Selected Entry";
            this.btn_post.UseVisualStyleBackColor = false;
            this.btn_post.Click += new System.EventHandler(this.btn_post_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_search);
            this.groupBox5.Controls.Add(this.txt_search_jnum);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(0, 439);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(179, 123);
            this.groupBox5.TabIndex = 61;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search Journal Number";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.Peru;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(12, 52);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(155, 60);
            this.btn_search.TabIndex = 54;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            // 
            // txt_search_jnum
            // 
            this.txt_search_jnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search_jnum.Location = new System.Drawing.Point(11, 27);
            this.txt_search_jnum.Margin = new System.Windows.Forms.Padding(2);
            this.txt_search_jnum.Name = "txt_search_jnum";
            this.txt_search_jnum.Size = new System.Drawing.Size(156, 21);
            this.txt_search_jnum.TabIndex = 56;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbl_journal_id);
            this.panel3.Controls.Add(this.lbl_period);
            this.panel3.Controls.Add(this.lbl_journal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(179, 56);
            this.panel3.TabIndex = 6;
            // 
            // lbl_journal_id
            // 
            this.lbl_journal_id.AutoSize = true;
            this.lbl_journal_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_journal_id.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_journal_id.Location = new System.Drawing.Point(106, 32);
            this.lbl_journal_id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_journal_id.Name = "lbl_journal_id";
            this.lbl_journal_id.Size = new System.Drawing.Size(96, 15);
            this.lbl_journal_id.TabIndex = 9;
            this.lbl_journal_id.Text = "lbl_journal_id";
            // 
            // lbl_period
            // 
            this.lbl_period.AutoSize = true;
            this.lbl_period.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_period.Location = new System.Drawing.Point(8, 10);
            this.lbl_period.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_period.Name = "lbl_period";
            this.lbl_period.Size = new System.Drawing.Size(49, 15);
            this.lbl_period.TabIndex = 8;
            this.lbl_period.Text = "Period";
            // 
            // lbl_journal
            // 
            this.lbl_journal.AutoSize = true;
            this.lbl_journal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_journal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_journal.Location = new System.Drawing.Point(5, 32);
            this.lbl_journal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_journal.Name = "lbl_journal";
            this.lbl_journal.Size = new System.Drawing.Size(97, 15);
            this.lbl_journal.TabIndex = 7;
            this.lbl_journal.Text = "Journal Name";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.cbo_journal);
            this.groupBox9.Controls.Add(this.cbo_period);
            this.groupBox9.Controls.Add(this.cbo_branch);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.label38);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox9.Location = new System.Drawing.Point(179, 0);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox9.Size = new System.Drawing.Size(845, 48);
            this.groupBox9.TabIndex = 62;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Select Period and Journal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(587, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 119;
            this.label5.Text = "Branch";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cbo_journal
            // 
            this.cbo_journal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_journal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_journal.FormattingEnabled = true;
            this.cbo_journal.Location = new System.Drawing.Point(359, 18);
            this.cbo_journal.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_journal.Name = "cbo_journal";
            this.cbo_journal.Size = new System.Drawing.Size(207, 23);
            this.cbo_journal.TabIndex = 59;
            this.cbo_journal.SelectedIndexChanged += new System.EventHandler(this.cbo_journal_SelectedIndexChanged);
            // 
            // cbo_period
            // 
            this.cbo_period.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_period.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_period.FormattingEnabled = true;
            this.cbo_period.Location = new System.Drawing.Point(69, 18);
            this.cbo_period.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_period.Name = "cbo_period";
            this.cbo_period.Size = new System.Drawing.Size(214, 23);
            this.cbo_period.TabIndex = 58;
            this.cbo_period.SelectedIndexChanged += new System.EventHandler(this.cbo_period_SelectedIndexChanged);
            // 
            // cbo_branch
            // 
            this.cbo_branch.AllowDrop = true;
            this.cbo_branch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_branch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_branch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Location = new System.Drawing.Point(639, 18);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(191, 23);
            this.cbo_branch.TabIndex = 118;
            this.cbo_branch.SelectedIndexChanged += new System.EventHandler(this.cbo_branch_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(303, 22);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(48, 15);
            this.label39.TabIndex = 57;
            this.label39.Text = "Journal";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(18, 23);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(43, 15);
            this.label38.TabIndex = 55;
            this.label38.Text = "Period";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_list);
            this.groupBox6.Controls.Add(this.panel2);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(179, 48);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(845, 514);
            this.groupBox6.TabIndex = 63;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Unposted Entries";
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToResizeColumns = false;
            this.dgv_list.AllowUserToResizeRows = false;
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ck_select,
            this.ref_num,
            this.t_date,
            this.desc,
            this.paidto,
            this.check,
            this.chk_date,
            this.userid,
            this.branch});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(2, 54);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(841, 458);
            this.dgv_list.TabIndex = 3;
            // 
            // ck_select
            // 
            this.ck_select.HeaderText = "Select";
            this.ck_select.Name = "ck_select";
            this.ck_select.Width = 50;
            // 
            // ref_num
            // 
            this.ref_num.FillWeight = 77F;
            this.ref_num.HeaderText = "Ref Num";
            this.ref_num.Name = "ref_num";
            this.ref_num.ReadOnly = true;
            this.ref_num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ref_num.Width = 77;
            // 
            // t_date
            // 
            this.t_date.FillWeight = 77F;
            this.t_date.HeaderText = "Date";
            this.t_date.Name = "t_date";
            this.t_date.ReadOnly = true;
            this.t_date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.t_date.Width = 77;
            // 
            // desc
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.desc.DefaultCellStyle = dataGridViewCellStyle1;
            this.desc.HeaderText = "Description";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.desc.Width = 250;
            // 
            // paidto
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.paidto.DefaultCellStyle = dataGridViewCellStyle2;
            this.paidto.HeaderText = "Paid To";
            this.paidto.Name = "paidto";
            this.paidto.ReadOnly = true;
            this.paidto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.paidto.Width = 220;
            // 
            // check
            // 
            this.check.FillWeight = 220F;
            this.check.HeaderText = "Check";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chk_date
            // 
            this.chk_date.HeaderText = "Ck Date";
            this.chk_date.Name = "chk_date";
            this.chk_date.ReadOnly = true;
            // 
            // userid
            // 
            this.userid.HeaderText = "User ID";
            this.userid.Name = "userid";
            this.userid.ReadOnly = true;
            // 
            // branch
            // 
            this.branch.HeaderText = "Branch";
            this.branch.Name = "branch";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ck_selectall);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(841, 38);
            this.panel2.TabIndex = 4;
            // 
            // ck_selectall
            // 
            this.ck_selectall.AutoSize = true;
            this.ck_selectall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ck_selectall.Location = new System.Drawing.Point(19, 13);
            this.ck_selectall.Name = "ck_selectall";
            this.ck_selectall.Size = new System.Drawing.Size(168, 19);
            this.ck_selectall.TabIndex = 2;
            this.ck_selectall.Text = "Check/Uncheck All Entries";
            this.ck_selectall.UseVisualStyleBackColor = true;
            this.ck_selectall.CheckedChanged += new System.EventHandler(this.ck_selectall_CheckedChanged);
            // 
            // a_postingtoledger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1024, 562);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "a_postingtoledger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Posting To Ledger";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.a_postingtoledger_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_post;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_journal_id;
        private System.Windows.Forms.Label lbl_period;
        private System.Windows.Forms.Label lbl_journal;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cbo_journal;
        private System.Windows.Forms.ComboBox cbo_period;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_search_jnum;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox ck_selectall;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ck_select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn t_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn paidto;
        private System.Windows.Forms.DataGridViewTextBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn chk_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn branch;
    }
}