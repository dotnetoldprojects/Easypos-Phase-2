namespace Easypos.Pricing
{
    partial class Frmprice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmprice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Btnadd = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.DTT = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.DTF = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.clientID = new System.Windows.Forms.ComboBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNITID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Btnsave = new System.Windows.Forms.Button();
            this.Btnclose = new System.Windows.Forms.Button();
            this.cmbproducts = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btnchange = new System.Windows.Forms.Button();
            this.Btnbilllist = new System.Windows.Forms.Button();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtTBV = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.unitTypes = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GroupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btnadd
            // 
            resources.ApplyResources(this.Btnadd, "Btnadd");
            this.Btnadd.Name = "Btnadd";
            this.Btnadd.UseVisualStyleBackColor = true;
            this.Btnadd.Click += new System.EventHandler(this.Btnadd_Click);
            // 
            // txtQuantity
            // 
            resources.ApplyResources(this.txtQuantity, "txtQuantity");
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtPrice
            // 
            resources.ApplyResources(this.txtPrice, "txtPrice");
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // GroupBox1
            // 
            resources.ApplyResources(this.GroupBox1, "GroupBox1");
            this.GroupBox1.Controls.Add(this.DTT);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.DTF);
            this.GroupBox1.Controls.Add(this.label15);
            this.GroupBox1.Controls.Add(this.label12);
            this.GroupBox1.Controls.Add(this.clientID);
            this.GroupBox1.Controls.Add(this.lblInvoiceNo);
            this.GroupBox1.Controls.Add(this.label4);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.TabStop = false;
            // 
            // DTT
            // 
            resources.ApplyResources(this.DTT, "DTT");
            this.DTT.Name = "DTT";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // DTF
            // 
            resources.ApplyResources(this.DTF, "DTF");
            this.DTF.Name = "DTF";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // clientID
            // 
            resources.ApplyResources(this.clientID, "clientID");
            this.clientID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.clientID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.clientID.DisplayMember = "Name";
            this.clientID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientID.FormattingEnabled = true;
            this.clientID.Name = "clientID";
            this.clientID.ValueMember = "ID";
            // 
            // lblInvoiceNo
            // 
            resources.ApplyResources(this.lblInvoiceNo, "lblInvoiceNo");
            this.lblInvoiceNo.BackColor = System.Drawing.Color.White;
            this.lblInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.DGV);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FloralWhite;
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.BackgroundColor = System.Drawing.Color.White;
            this.DGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.DGV, "DGV");
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProdName,
            this.UnitType,
            this.UNITID,
            this.Quantity,
            this.UnitPrice,
            this.Discount,
            this.LineTotal,
            this.Delete});
            this.DGV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DGV.EnableHeadersVisualStyles = false;
            this.DGV.GridColor = System.Drawing.Color.White;
            this.DGV.MultiSelect = false;
            this.DGV.Name = "DGV";
            this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.DGV.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DGV.RowTemplate.Height = 40;
            this.DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellContentClick);
            this.DGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellEndEdit);
            // 
            // ProductCode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductCode.FillWeight = 45F;
            resources.ApplyResources(this.ProductCode, "ProductCode");
            this.ProductCode.Name = "ProductCode";
            // 
            // ProdName
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ProdName.DefaultCellStyle = dataGridViewCellStyle4;
            this.ProdName.FillWeight = 150F;
            resources.ApplyResources(this.ProdName, "ProdName");
            this.ProdName.Name = "ProdName";
            // 
            // UnitType
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UnitType.DefaultCellStyle = dataGridViewCellStyle5;
            this.UnitType.FillWeight = 57.44501F;
            resources.ApplyResources(this.UnitType, "UnitType");
            this.UnitType.Name = "UnitType";
            // 
            // UNITID
            // 
            resources.ApplyResources(this.UNITID, "UNITID");
            this.UNITID.Name = "UNITID";
            // 
            // Quantity
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle6;
            this.Quantity.FillWeight = 57.44501F;
            resources.ApplyResources(this.Quantity, "Quantity");
            this.Quantity.Name = "Quantity";
            // 
            // UnitPrice
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UnitPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.UnitPrice.FillWeight = 57.44501F;
            resources.ApplyResources(this.UnitPrice, "UnitPrice");
            this.UnitPrice.Name = "UnitPrice";
            // 
            // Discount
            // 
            this.Discount.FillWeight = 45F;
            resources.ApplyResources(this.Discount, "Discount");
            this.Discount.Name = "Discount";
            // 
            // LineTotal
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LineTotal.DefaultCellStyle = dataGridViewCellStyle8;
            this.LineTotal.FillWeight = 57.44501F;
            resources.ApplyResources(this.LineTotal, "LineTotal");
            this.LineTotal.Name = "LineTotal";
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.FillWeight = 57.44501F;
            this.Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.Delete, "Delete");
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // Btnsave
            // 
            resources.ApplyResources(this.Btnsave, "Btnsave");
            this.Btnsave.BackColor = System.Drawing.Color.PowderBlue;
            this.Btnsave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnsave.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnsave.Name = "Btnsave";
            this.Btnsave.UseVisualStyleBackColor = false;
            this.Btnsave.Click += new System.EventHandler(this.Btnsave_Click);
            // 
            // Btnclose
            // 
            this.Btnclose.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btnclose, "Btnclose");
            this.Btnclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnclose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnclose.Name = "Btnclose";
            this.Btnclose.UseVisualStyleBackColor = false;
            this.Btnclose.Click += new System.EventHandler(this.Btnclose_Click);
            // 
            // cmbproducts
            // 
            resources.ApplyResources(this.cmbproducts, "cmbproducts");
            this.cmbproducts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbproducts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbproducts.DisplayMember = "Description";
            this.cmbproducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbproducts.FormattingEnabled = true;
            this.cmbproducts.Name = "cmbproducts";
            this.cmbproducts.ValueMember = "ProductNo";
            this.cmbproducts.SelectionChangeCommitted += new System.EventHandler(this.cmbproducts_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Btnchange);
            this.panel1.Controls.Add(this.Btnsave);
            this.panel1.Controls.Add(this.Btnclose);
            this.panel1.Controls.Add(this.Btnbilllist);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Btnchange
            // 
            resources.ApplyResources(this.Btnchange, "Btnchange");
            this.Btnchange.BackColor = System.Drawing.Color.PowderBlue;
            this.Btnchange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnchange.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnchange.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnchange.Name = "Btnchange";
            this.Btnchange.UseVisualStyleBackColor = false;
            this.Btnchange.Click += new System.EventHandler(this.Btnchange_Click);
            // 
            // Btnbilllist
            // 
            this.Btnbilllist.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btnbilllist, "Btnbilllist");
            this.Btnbilllist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnbilllist.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnbilllist.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnbilllist.Name = "Btnbilllist";
            this.Btnbilllist.UseVisualStyleBackColor = false;
            this.Btnbilllist.Click += new System.EventHandler(this.Btnbilllist_Click);
            // 
            // txtTax
            // 
            resources.ApplyResources(this.txtTax, "txtTax");
            this.txtTax.Name = "txtTax";
            this.txtTax.ReadOnly = true;
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            // 
            // txtDiscount
            // 
            resources.ApplyResources(this.txtDiscount, "txtDiscount");
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.GroupBox1);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.txtTBV);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtTax);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.txtDiscount);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Name = "label23";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // txtTBV
            // 
            resources.ApplyResources(this.txtTBV, "txtTBV");
            this.txtTBV.Name = "txtTBV";
            this.txtTBV.ReadOnly = true;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.Btnadd);
            this.groupBox3.Controls.Add(this.txtQuantity);
            this.groupBox3.Controls.Add(this.txtPrice);
            this.groupBox3.Controls.Add(this.cmbproducts);
            this.groupBox3.Controls.Add(this.unitTypes);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // unitTypes
            // 
            resources.ApplyResources(this.unitTypes, "unitTypes");
            this.unitTypes.DisplayMember = "UnitType";
            this.unitTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitTypes.FormattingEnabled = true;
            this.unitTypes.Name = "unitTypes";
            this.unitTypes.ValueMember = "ID";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Name = "lblTitle";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // Frmprice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmprice";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btnadd;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox clientID;
        internal System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.DataGridView DGV;
        public System.Windows.Forms.Button Btnsave;
        private System.Windows.Forms.Button Btnclose;
        public System.Windows.Forms.ComboBox cmbproducts;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtTax;
        public System.Windows.Forms.TextBox txtTotal;
        public System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.TextBox txtTBV;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox unitTypes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker DTT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DTF;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button Btnbilllist;
        public System.Windows.Forms.Button Btnchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitType;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNITID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineTotal;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}