namespace Accounting_Application_System
{
    partial class z_enter_item_simple
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
            this.txt_lnno = new System.Windows.Forms.TextBox();
            this.cbo_purchunit = new System.Windows.Forms.ComboBox();
            this.txt_itemcode = new System.Windows.Forms.TextBox();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.lbl_lnamt = new System.Windows.Forms.Label();
            this.txt_lnamt = new System.Windows.Forms.TextBox();
            this.lbl_costprice = new System.Windows.Forms.Label();
            this.txt_costprice = new System.Windows.Forms.TextBox();
            this.txt_qty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_remark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_partno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_receive_qty = new System.Windows.Forms.TextBox();
            this.lbl_rv = new System.Windows.Forms.Label();
            this.lbl_transpo_amnt = new System.Windows.Forms.Label();
            this.txt_transpo_amnt = new System.Windows.Forms.TextBox();
            this.lbl_subcostcenter = new System.Windows.Forms.Label();
            this.lbl_costcenter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbo_sub_cost_center = new System.Windows.Forms.ComboBox();
            this.cbo_cost_center = new System.Windows.Forms.ComboBox();
            this.cbo_account_link = new System.Windows.Forms.ComboBox();
            this.cbo_itemdesc = new System.Windows.Forms.ComboBox();
            this.cbo_discby = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_discamnt = new System.Windows.Forms.TextBox();
            this.txt_discreason = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_lnno
            // 
            this.txt_lnno.BackColor = System.Drawing.SystemColors.Info;
            this.txt_lnno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_lnno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lnno.Location = new System.Drawing.Point(115, 13);
            this.txt_lnno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_lnno.Name = "txt_lnno";
            this.txt_lnno.ReadOnly = true;
            this.txt_lnno.Size = new System.Drawing.Size(113, 15);
            this.txt_lnno.TabIndex = 10;
            // 
            // cbo_purchunit
            // 
            this.cbo_purchunit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_purchunit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_purchunit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_purchunit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_purchunit.FormattingEnabled = true;
            this.cbo_purchunit.Location = new System.Drawing.Point(235, 96);
            this.cbo_purchunit.Name = "cbo_purchunit";
            this.cbo_purchunit.Size = new System.Drawing.Size(298, 24);
            this.cbo_purchunit.TabIndex = 15;
            this.cbo_purchunit.SelectedIndexChanged += new System.EventHandler(this.cbo_purchunit_SelectedIndexChanged);
            // 
            // txt_itemcode
            // 
            this.txt_itemcode.BackColor = System.Drawing.SystemColors.Info;
            this.txt_itemcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_itemcode.Location = new System.Drawing.Point(115, 36);
            this.txt_itemcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_itemcode.Name = "txt_itemcode";
            this.txt_itemcode.ReadOnly = true;
            this.txt_itemcode.Size = new System.Drawing.Size(165, 22);
            this.txt_itemcode.TabIndex = 11;
            this.txt_itemcode.TextChanged += new System.EventHandler(this.txt_itemcode_TextChanged);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Peru;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(115, 244);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(160, 40);
            this.btn_back.TabIndex = 21;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Peru;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(281, 244);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(160, 40);
            this.btn_save.TabIndex = 20;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_lnamt
            // 
            this.lbl_lnamt.AutoSize = true;
            this.lbl_lnamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lnamt.Location = new System.Drawing.Point(12, 159);
            this.lbl_lnamt.Name = "lbl_lnamt";
            this.lbl_lnamt.Size = new System.Drawing.Size(76, 15);
            this.lbl_lnamt.TabIndex = 5;
            this.lbl_lnamt.Text = "Line Amount";
            // 
            // txt_lnamt
            // 
            this.txt_lnamt.BackColor = System.Drawing.SystemColors.Info;
            this.txt_lnamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lnamt.Location = new System.Drawing.Point(115, 156);
            this.txt_lnamt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_lnamt.Name = "txt_lnamt";
            this.txt_lnamt.ReadOnly = true;
            this.txt_lnamt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_lnamt.Size = new System.Drawing.Size(114, 22);
            this.txt_lnamt.TabIndex = 17;
            this.txt_lnamt.Text = "0.00";
            this.txt_lnamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_costprice
            // 
            this.lbl_costprice.AutoSize = true;
            this.lbl_costprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_costprice.Location = new System.Drawing.Point(12, 129);
            this.lbl_costprice.Name = "lbl_costprice";
            this.lbl_costprice.Size = new System.Drawing.Size(62, 15);
            this.lbl_costprice.TabIndex = 4;
            this.lbl_costprice.Text = "Cost Price";
            // 
            // txt_costprice
            // 
            this.txt_costprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_costprice.Location = new System.Drawing.Point(115, 126);
            this.txt_costprice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_costprice.Name = "txt_costprice";
            this.txt_costprice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_costprice.Size = new System.Drawing.Size(113, 22);
            this.txt_costprice.TabIndex = 16;
            this.txt_costprice.Text = "0.00";
            this.txt_costprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_costprice.Leave += new System.EventHandler(this.txt_costprice_Leave);
            // 
            // txt_qty
            // 
            this.txt_qty.BackColor = System.Drawing.SystemColors.Window;
            this.txt_qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_qty.Location = new System.Drawing.Point(116, 96);
            this.txt_qty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_qty.Name = "txt_qty";
            this.txt_qty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_qty.Size = new System.Drawing.Size(113, 22);
            this.txt_qty.TabIndex = 14;
            this.txt_qty.Text = "0.00";
            this.txt_qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_qty.TextChanged += new System.EventHandler(this.txt_qty_TextChanged);
            this.txt_qty.Leave += new System.EventHandler(this.txt_qty_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantity";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(12, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 15);
            this.label19.TabIndex = 0;
            this.label19.Text = "Line Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Item Code";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(504, 62);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(29, 22);
            this.btn_search.TabIndex = 13;
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Item Description";
            // 
            // txt_remark
            // 
            this.txt_remark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_remark.Location = new System.Drawing.Point(115, 185);
            this.txt_remark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_remark.Size = new System.Drawing.Size(418, 22);
            this.txt_remark.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Remarks";
            // 
            // txt_partno
            // 
            this.txt_partno.BackColor = System.Drawing.SystemColors.Info;
            this.txt_partno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_partno.Location = new System.Drawing.Point(338, 36);
            this.txt_partno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_partno.Name = "txt_partno";
            this.txt_partno.ReadOnly = true;
            this.txt_partno.Size = new System.Drawing.Size(195, 22);
            this.txt_partno.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(286, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Part No.";
            // 
            // txt_receive_qty
            // 
            this.txt_receive_qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_receive_qty.Location = new System.Drawing.Point(336, 129);
            this.txt_receive_qty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_receive_qty.Name = "txt_receive_qty";
            this.txt_receive_qty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_receive_qty.Size = new System.Drawing.Size(166, 22);
            this.txt_receive_qty.TabIndex = 18;
            this.txt_receive_qty.Text = "0.00";
            this.txt_receive_qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_receive_qty.TextChanged += new System.EventHandler(this.txt_receive_qty_TextChanged);
            this.txt_receive_qty.Leave += new System.EventHandler(this.txt_receive_qty_Leave);
            // 
            // lbl_rv
            // 
            this.lbl_rv.AutoSize = true;
            this.lbl_rv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rv.Location = new System.Drawing.Point(236, 132);
            this.lbl_rv.Name = "lbl_rv";
            this.lbl_rv.Size = new System.Drawing.Size(81, 15);
            this.lbl_rv.TabIndex = 8;
            this.lbl_rv.Text = "Received Qty.";
            // 
            // lbl_transpo_amnt
            // 
            this.lbl_transpo_amnt.AutoSize = true;
            this.lbl_transpo_amnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_transpo_amnt.Location = new System.Drawing.Point(252, 159);
            this.lbl_transpo_amnt.Name = "lbl_transpo_amnt";
            this.lbl_transpo_amnt.Size = new System.Drawing.Size(79, 15);
            this.lbl_transpo_amnt.TabIndex = 22;
            this.lbl_transpo_amnt.Text = "Transpo Cost";
            this.lbl_transpo_amnt.Visible = false;
            // 
            // txt_transpo_amnt
            // 
            this.txt_transpo_amnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_transpo_amnt.Location = new System.Drawing.Point(337, 156);
            this.txt_transpo_amnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_transpo_amnt.Name = "txt_transpo_amnt";
            this.txt_transpo_amnt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_transpo_amnt.Size = new System.Drawing.Size(166, 22);
            this.txt_transpo_amnt.TabIndex = 23;
            this.txt_transpo_amnt.Text = "0.00";
            this.txt_transpo_amnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_transpo_amnt.Visible = false;
            this.txt_transpo_amnt.TextChanged += new System.EventHandler(this.txt_transpo_amnt_TextChanged);
            // 
            // lbl_subcostcenter
            // 
            this.lbl_subcostcenter.AutoSize = true;
            this.lbl_subcostcenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subcostcenter.Location = new System.Drawing.Point(232, 157);
            this.lbl_subcostcenter.Name = "lbl_subcostcenter";
            this.lbl_subcostcenter.Size = new System.Drawing.Size(95, 15);
            this.lbl_subcostcenter.TabIndex = 57;
            this.lbl_subcostcenter.Text = "Sub Cost Center";
            this.lbl_subcostcenter.Visible = false;
            // 
            // lbl_costcenter
            // 
            this.lbl_costcenter.AutoSize = true;
            this.lbl_costcenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_costcenter.Location = new System.Drawing.Point(232, 133);
            this.lbl_costcenter.Name = "lbl_costcenter";
            this.lbl_costcenter.Size = new System.Drawing.Size(82, 15);
            this.lbl_costcenter.TabIndex = 56;
            this.lbl_costcenter.Text = "Cost Center    ";
            this.lbl_costcenter.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 55;
            this.label6.Text = "Account Link";
            this.label6.Visible = false;
            // 
            // cbo_sub_cost_center
            // 
            this.cbo_sub_cost_center.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_sub_cost_center.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_sub_cost_center.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_sub_cost_center.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_sub_cost_center.FormattingEnabled = true;
            this.cbo_sub_cost_center.Location = new System.Drawing.Point(327, 154);
            this.cbo_sub_cost_center.Name = "cbo_sub_cost_center";
            this.cbo_sub_cost_center.Size = new System.Drawing.Size(177, 24);
            this.cbo_sub_cost_center.TabIndex = 54;
            this.cbo_sub_cost_center.Visible = false;
            // 
            // cbo_cost_center
            // 
            this.cbo_cost_center.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_cost_center.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_cost_center.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_cost_center.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_cost_center.FormattingEnabled = true;
            this.cbo_cost_center.Location = new System.Drawing.Point(326, 127);
            this.cbo_cost_center.Name = "cbo_cost_center";
            this.cbo_cost_center.Size = new System.Drawing.Size(177, 24);
            this.cbo_cost_center.TabIndex = 53;
            this.cbo_cost_center.Visible = false;
            this.cbo_cost_center.SelectedIndexChanged += new System.EventHandler(this.cbo_cost_center_SelectedIndexChanged);
            // 
            // cbo_account_link
            // 
            this.cbo_account_link.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_account_link.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_account_link.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_account_link.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_account_link.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_account_link.FormattingEnabled = true;
            this.cbo_account_link.Location = new System.Drawing.Point(115, 214);
            this.cbo_account_link.Name = "cbo_account_link";
            this.cbo_account_link.Size = new System.Drawing.Size(418, 24);
            this.cbo_account_link.TabIndex = 52;
            this.cbo_account_link.Visible = false;
            // 
            // cbo_itemdesc
            // 
            this.cbo_itemdesc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_itemdesc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_itemdesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_itemdesc.FormattingEnabled = true;
            this.cbo_itemdesc.Location = new System.Drawing.Point(115, 63);
            this.cbo_itemdesc.Name = "cbo_itemdesc";
            this.cbo_itemdesc.Size = new System.Drawing.Size(418, 21);
            this.cbo_itemdesc.TabIndex = 58;
            // 
            // cbo_discby
            // 
            this.cbo_discby.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_discby.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_discby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_discby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_discby.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_discby.FormattingEnabled = true;
            this.cbo_discby.Location = new System.Drawing.Point(368, 126);
            this.cbo_discby.Name = "cbo_discby";
            this.cbo_discby.Size = new System.Drawing.Size(165, 24);
            this.cbo_discby.TabIndex = 59;
            this.cbo_discby.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(228, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 61;
            this.label7.Text = "Discount    ";
            this.label7.Visible = false;
            // 
            // txt_discamnt
            // 
            this.txt_discamnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_discamnt.Location = new System.Drawing.Point(281, 127);
            this.txt_discamnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_discamnt.Name = "txt_discamnt";
            this.txt_discamnt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_discamnt.Size = new System.Drawing.Size(86, 22);
            this.txt_discamnt.TabIndex = 62;
            this.txt_discamnt.Text = "0.00";
            this.txt_discamnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_discamnt.Visible = false;
            this.txt_discamnt.TextChanged += new System.EventHandler(this.txt_discamnt_TextChanged);
            // 
            // txt_discreason
            // 
            this.txt_discreason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_discreason.Location = new System.Drawing.Point(308, 155);
            this.txt_discreason.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_discreason.Name = "txt_discreason";
            this.txt_discreason.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_discreason.Size = new System.Drawing.Size(225, 22);
            this.txt_discreason.TabIndex = 63;
            this.txt_discreason.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(228, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 64;
            this.label8.Text = "Disc. Reason";
            this.label8.Visible = false;
            // 
            // z_enter_item_simple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(556, 292);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_lnamt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_discreason);
            this.Controls.Add(this.txt_discamnt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbo_discby);
            this.Controls.Add(this.cbo_itemdesc);
            this.Controls.Add(this.lbl_subcostcenter);
            this.Controls.Add(this.lbl_costcenter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbo_sub_cost_center);
            this.Controls.Add(this.cbo_cost_center);
            this.Controls.Add(this.cbo_account_link);
            this.Controls.Add(this.lbl_transpo_amnt);
            this.Controls.Add(this.txt_transpo_amnt);
            this.Controls.Add(this.txt_receive_qty);
            this.Controls.Add(this.lbl_rv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_partno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_remark);
            this.Controls.Add(this.txt_lnno);
            this.Controls.Add(this.cbo_purchunit);
            this.Controls.Add(this.txt_itemcode);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lbl_lnamt);
            this.Controls.Add(this.lbl_costprice);
            this.Controls.Add(this.txt_costprice);
            this.Controls.Add(this.txt_qty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_enter_item_simple";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Item";
            this.Load += new System.EventHandler(this.z_enter_item_simple_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_lnno;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbl_lnamt;
        private System.Windows.Forms.Label lbl_costprice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbo_purchunit;
        public System.Windows.Forms.TextBox txt_itemcode;
        public System.Windows.Forms.TextBox txt_lnamt;
        public System.Windows.Forms.TextBox txt_costprice;
        public System.Windows.Forms.TextBox txt_qty;
        public System.Windows.Forms.TextBox txt_remark;
        public System.Windows.Forms.TextBox txt_partno;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txt_receive_qty;
        private System.Windows.Forms.Label lbl_rv;
        private System.Windows.Forms.Label lbl_transpo_amnt;
        public System.Windows.Forms.TextBox txt_transpo_amnt;
        private System.Windows.Forms.Label lbl_subcostcenter;
        private System.Windows.Forms.Label lbl_costcenter;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbo_sub_cost_center;
        public System.Windows.Forms.ComboBox cbo_cost_center;
        public System.Windows.Forms.ComboBox cbo_account_link;
        public System.Windows.Forms.ComboBox cbo_itemdesc;
        public System.Windows.Forms.ComboBox cbo_discby;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txt_discamnt;
        public System.Windows.Forms.TextBox txt_discreason;
        private System.Windows.Forms.Label label8;
    }
}