namespace Accounting_Application_System
{
    partial class auto_loaner
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
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.dgv_list_ca = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_gfolio = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvlca2_chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvlf_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvlf_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvlf_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvlf_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_ca)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.Info;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_search.Image = global::Accounting_Application_System.Properties.Resources.journal3;
            this.btn_search.Location = new System.Drawing.Point(224, 16);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(112, 41);
            this.btn_search.TabIndex = 73;
            this.btn_search.Text = "Search";
            this.btn_search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.Info;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_close.Location = new System.Drawing.Point(432, 319);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 41);
            this.btn_close.TabIndex = 75;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            // 
            // dgv_list_ca
            // 
            this.dgv_list_ca.AllowUserToDeleteRows = false;
            this.dgv_list_ca.AllowUserToResizeRows = false;
            this.dgv_list_ca.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_list_ca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list_ca.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_list_ca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_list_ca.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvlca2_chk,
            this.dgvlf_line,
            this.dgvlf_code,
            this.dgvlf_name,
            this.dgvlf_status});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_list_ca.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_list_ca.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_list_ca.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_list_ca.Location = new System.Drawing.Point(3, 16);
            this.dgv_list_ca.MultiSelect = false;
            this.dgv_list_ca.Name = "dgv_list_ca";
            this.dgv_list_ca.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list_ca.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_list_ca.RowHeadersWidth = 15;
            this.dgv_list_ca.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_list_ca.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list_ca.Size = new System.Drawing.Size(665, 298);
            this.dgv_list_ca.TabIndex = 16;
            this.dgv_list_ca.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_ca_CellClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_close);
            this.groupBox5.Controls.Add(this.btn_submit);
            this.groupBox5.Controls.Add(this.dgv_list_ca);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 71);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(671, 375);
            this.groupBox5.TabIndex = 80;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Conditions of Approval List";
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.SystemColors.Info;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_submit.Location = new System.Drawing.Point(548, 319);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(112, 41);
            this.btn_submit.TabIndex = 74;
            this.btn_submit.Text = "Add to List";
            this.btn_submit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Loaner Name";
            // 
            // txt_gfolio
            // 
            this.txt_gfolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gfolio.Location = new System.Drawing.Point(12, 36);
            this.txt_gfolio.Name = "txt_gfolio";
            this.txt_gfolio.Size = new System.Drawing.Size(207, 21);
            this.txt_gfolio.TabIndex = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_gfolio);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 71);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // dgvlca2_chk
            // 
            this.dgvlca2_chk.HeaderText = "";
            this.dgvlca2_chk.Name = "dgvlca2_chk";
            this.dgvlca2_chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvlca2_chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvlca2_chk.Width = 40;
            // 
            // dgvlf_line
            // 
            this.dgvlf_line.HeaderText = "Line";
            this.dgvlf_line.Name = "dgvlf_line";
            this.dgvlf_line.ReadOnly = true;
            this.dgvlf_line.Width = 40;
            // 
            // dgvlf_code
            // 
            this.dgvlf_code.HeaderText = "Loaner Code ";
            this.dgvlf_code.Name = "dgvlf_code";
            this.dgvlf_code.ReadOnly = true;
            this.dgvlf_code.Width = 70;
            // 
            // dgvlf_name
            // 
            this.dgvlf_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvlf_name.FillWeight = 200F;
            this.dgvlf_name.HeaderText = "Loaner Name";
            this.dgvlf_name.MinimumWidth = 155;
            this.dgvlf_name.Name = "dgvlf_name";
            this.dgvlf_name.ReadOnly = true;
            // 
            // dgvlf_status
            // 
            this.dgvlf_status.HeaderText = "Status";
            this.dgvlf_status.Name = "dgvlf_status";
            this.dgvlf_status.ReadOnly = true;
            // 
            // auto_loaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 446);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Name = "auto_loaner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "auto_loaner";
            this.Load += new System.EventHandler(this.auto_loaner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_ca)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridView dgv_list_ca;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_gfolio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvlca2_chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvlf_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvlf_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvlf_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvlf_status;
    }
}