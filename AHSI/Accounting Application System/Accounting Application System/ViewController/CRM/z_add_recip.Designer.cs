namespace Accounting_Application_System
{
    partial class z_add_recip
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
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgvl_ckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvl_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_mobile1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_mobile2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_add_recip = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_search
            // 
            this.txt_search.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(391, 19);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(532, 38);
            this.txt_search.TabIndex = 3;
            this.txt_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_search_KeyPress);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_search.Location = new System.Drawing.Point(929, 19);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(159, 37);
            this.btn_search.TabIndex = 9;
            this.btn_search.Text = "DEEP SEARCH";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToResizeRows = false;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl_ckbox,
            this.dgvl_code,
            this.dgvl_name,
            this.dgvl_mobile1,
            this.dgvl_mobile2,
            this.dgvl_email});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_list.Location = new System.Drawing.Point(0, 65);
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 25;
            this.dgv_list.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(1100, 505);
            this.dgv_list.TabIndex = 0;
            this.dgv_list.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellContentClick);
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // dgvl_ckbox
            // 
            this.dgvl_ckbox.HeaderText = "";
            this.dgvl_ckbox.Name = "dgvl_ckbox";
            this.dgvl_ckbox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvl_ckbox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvl_ckbox.Width = 75;
            // 
            // dgvl_code
            // 
            this.dgvl_code.HeaderText = "CODE";
            this.dgvl_code.Name = "dgvl_code";
            this.dgvl_code.ReadOnly = true;
            this.dgvl_code.Width = 75;
            // 
            // dgvl_name
            // 
            this.dgvl_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvl_name.HeaderText = "NAME";
            this.dgvl_name.Name = "dgvl_name";
            this.dgvl_name.ReadOnly = true;
            // 
            // dgvl_mobile1
            // 
            this.dgvl_mobile1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvl_mobile1.HeaderText = "MOBILE 1";
            this.dgvl_mobile1.Name = "dgvl_mobile1";
            this.dgvl_mobile1.ReadOnly = true;
            // 
            // dgvl_mobile2
            // 
            this.dgvl_mobile2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvl_mobile2.HeaderText = "MOBILE 2";
            this.dgvl_mobile2.Name = "dgvl_mobile2";
            // 
            // dgvl_email
            // 
            this.dgvl_email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvl_email.HeaderText = "EMAIL";
            this.dgvl_email.Name = "dgvl_email";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_list);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 570);
            this.panel1.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btn_add_recip);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.btn_search);
            this.groupBox1.Controls.Add(this.txt_search);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1100, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Look Recipient";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btn_add_recip
            // 
            this.btn_add_recip.BackColor = System.Drawing.Color.Peru;
            this.btn_add_recip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_recip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_recip.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_add_recip.Location = new System.Drawing.Point(11, 16);
            this.btn_add_recip.Margin = new System.Windows.Forms.Padding(2);
            this.btn_add_recip.Name = "btn_add_recip";
            this.btn_add_recip.Size = new System.Drawing.Size(112, 41);
            this.btn_add_recip.TabIndex = 78;
            this.btn_add_recip.Text = "Add to List";
            this.btn_add_recip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_add_recip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_add_recip.UseVisualStyleBackColor = false;
            this.btn_add_recip.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Peru;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_close.Location = new System.Drawing.Point(127, 15);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 41);
            this.btn_close.TabIndex = 79;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            // 
            // z_add_recip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1100, 570);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_add_recip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Recepient";
            this.Load += new System.EventHandler(this.z_add_recip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_add_recip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvl_ckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_mobile1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_mobile2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_email;

    }
}