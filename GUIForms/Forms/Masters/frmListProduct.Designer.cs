namespace Easypos.Masters
{
    partial class frmListProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListProduct));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btnadd = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.CBItems = new System.Windows.Forms.ComboBox();
            this.GB = new System.Windows.Forms.GroupBox();
            this.DGVProitems = new System.Windows.Forms.DataGridView();
            this.itemid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Btnaddedit = new System.Windows.Forms.Button();
            this.Btndel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.allowInventory = new System.Windows.Forms.CheckBox();
            this.ShowInPOS = new System.Windows.Forms.CheckBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.lblProductNo = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtStocksOnHand = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.ProductNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StocksOnHand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unitid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Showpos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Allowinv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReorderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProitems)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.picClose);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.picClose, "picClose");
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Name = "picClose";
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btnadd);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.CBItems);
            this.groupBox2.Controls.Add(this.GB);
            this.groupBox2.Controls.Add(this.Btnaddedit);
            this.groupBox2.Controls.Add(this.Btndel);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.allowInventory);
            this.groupBox2.Controls.Add(this.ShowInPOS);
            this.groupBox2.Controls.Add(this.Button3);
            this.groupBox2.Controls.Add(this.lblProductNo);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtReorderLevel);
            this.groupBox2.Controls.Add(this.txtUnitPrice);
            this.groupBox2.Controls.Add(this.Label9);
            this.groupBox2.Controls.Add(this.Label7);
            this.groupBox2.Controls.Add(this.txtStocksOnHand);
            this.groupBox2.Controls.Add(this.Label8);
            this.groupBox2.Controls.Add(this.txtCategory);
            this.groupBox2.Controls.Add(this.Label5);
            this.groupBox2.Controls.Add(this.txtBarcode);
            this.groupBox2.Controls.Add(this.Label4);
            this.groupBox2.Controls.Add(this.txtProductCode);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label10);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // Btnadd
            // 
            this.Btnadd.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btnadd, "Btnadd");
            this.Btnadd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnadd.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnadd.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnadd.Name = "Btnadd";
            this.Btnadd.UseVisualStyleBackColor = false;
            this.Btnadd.Click += new System.EventHandler(this.Btnadd_Click);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // CBItems
            // 
            this.CBItems.FormattingEnabled = true;
            resources.ApplyResources(this.CBItems, "CBItems");
            this.CBItems.Name = "CBItems";
            // 
            // GB
            // 
            this.GB.Controls.Add(this.DGVProitems);
            resources.ApplyResources(this.GB, "GB");
            this.GB.Name = "GB";
            this.GB.TabStop = false;
            // 
            // DGVProitems
            // 
            this.DGVProitems.AllowUserToAddRows = false;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVProitems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.DGVProitems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Verdana", 8.25F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVProitems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.DGVProitems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProitems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemid,
            this.itemname,
            this.Quantity,
            this.Delete});
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Verdana", 8.25F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVProitems.DefaultCellStyle = dataGridViewCellStyle21;
            resources.ApplyResources(this.DGVProitems, "DGVProitems");
            this.DGVProitems.Name = "DGVProitems";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Verdana", 8.25F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVProitems.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.DGVProitems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProitems_CellContentClick);
            // 
            // itemid
            // 
            this.itemid.DataPropertyName = "ID";
            resources.ApplyResources(this.itemid, "itemid");
            this.itemid.Name = "itemid";
            // 
            // itemname
            // 
            this.itemname.DataPropertyName = "Itemname";
            resources.ApplyResources(this.itemname, "itemname");
            this.itemname.Name = "itemname";
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            resources.ApplyResources(this.Quantity, "Quantity");
            this.Quantity.Name = "Quantity";
            // 
            // Delete
            // 
            resources.ApplyResources(this.Delete, "Delete");
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // Btnaddedit
            // 
            this.Btnaddedit.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btnaddedit, "Btnaddedit");
            this.Btnaddedit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnaddedit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnaddedit.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnaddedit.Name = "Btnaddedit";
            this.Btnaddedit.UseVisualStyleBackColor = false;
            this.Btnaddedit.Click += new System.EventHandler(this.Btnaddedit_Click);
            // 
            // Btndel
            // 
            this.Btndel.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btndel, "Btndel");
            this.Btndel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btndel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btndel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btndel.Name = "Btndel";
            this.Btndel.UseVisualStyleBackColor = false;
            this.Btndel.Click += new System.EventHandler(this.Btndel_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.button5, "button5");
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // allowInventory
            // 
            resources.ApplyResources(this.allowInventory, "allowInventory");
            this.allowInventory.Name = "allowInventory";
            this.allowInventory.UseVisualStyleBackColor = true;
            // 
            // ShowInPOS
            // 
            resources.ApplyResources(this.ShowInPOS, "ShowInPOS");
            this.ShowInPOS.Name = "ShowInPOS";
            this.ShowInPOS.UseVisualStyleBackColor = true;
            // 
            // Button3
            // 
            this.Button3.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.Button3, "Button3");
            this.Button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button3.Name = "Button3";
            this.Button3.UseVisualStyleBackColor = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // lblProductNo
            // 
            this.lblProductNo.BackColor = System.Drawing.Color.White;
            this.lblProductNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblProductNo, "lblProductNo");
            this.lblProductNo.Name = "lblProductNo";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtReorderLevel
            // 
            this.txtReorderLevel.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtReorderLevel, "txtReorderLevel");
            this.txtReorderLevel.Name = "txtReorderLevel";
            this.txtReorderLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReorderLevel_KeyPress);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtUnitPrice, "txtUnitPrice");
            this.txtUnitPrice.Name = "txtUnitPrice";
            // 
            // Label9
            // 
            resources.ApplyResources(this.Label9, "Label9");
            this.Label9.Name = "Label9";
            // 
            // Label7
            // 
            resources.ApplyResources(this.Label7, "Label7");
            this.Label7.Name = "Label7";
            // 
            // txtStocksOnHand
            // 
            this.txtStocksOnHand.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtStocksOnHand, "txtStocksOnHand");
            this.txtStocksOnHand.Name = "txtStocksOnHand";
            this.txtStocksOnHand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStocksOnHand_KeyPress);
            // 
            // Label8
            // 
            resources.ApplyResources(this.Label8, "Label8");
            this.Label8.Name = "Label8";
            // 
            // txtCategory
            // 
            this.txtCategory.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtCategory, "txtCategory");
            this.txtCategory.Name = "txtCategory";
            // 
            // Label5
            // 
            resources.ApplyResources(this.Label5, "Label5");
            this.Label5.Name = "Label5";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtBarcode, "txtBarcode");
            this.txtBarcode.Name = "txtBarcode";
            // 
            // Label4
            // 
            resources.ApplyResources(this.Label4, "Label4");
            this.Label4.Name = "Label4";
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.Color.White;
            this.txtProductCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtProductCode, "txtProductCode");
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.label11);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNo,
            this.ProductCode,
            this.Description,
            this.Barcode,
            this.UnitPrice,
            this.StocksOnHand,
            this.CategoryName,
            this.UnitName,
            this.Unitid,
            this.CategoryNo,
            this.Showpos,
            this.Allowinv,
            this.ReorderLevel});
            resources.ApplyResources(this.DGV, "DGV");
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            // 
            // ProductNo
            // 
            this.ProductNo.DataPropertyName = "ProductNo";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ProductNo.DefaultCellStyle = dataGridViewCellStyle23;
            resources.ApplyResources(this.ProductNo, "ProductNo");
            this.ProductNo.Name = "ProductNo";
            this.ProductNo.ReadOnly = true;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.ProductCode, "ProductCode");
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            resources.ApplyResources(this.Description, "Description");
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Barcode";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Barcode.DefaultCellStyle = dataGridViewCellStyle24;
            resources.ApplyResources(this.Barcode, "Barcode");
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            // 
            // UnitPrice
            // 
            this.UnitPrice.DataPropertyName = "UnitPrice";
            resources.ApplyResources(this.UnitPrice, "UnitPrice");
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // StocksOnHand
            // 
            this.StocksOnHand.DataPropertyName = "StocksOnHand";
            resources.ApplyResources(this.StocksOnHand, "StocksOnHand");
            this.StocksOnHand.Name = "StocksOnHand";
            this.StocksOnHand.ReadOnly = true;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "CategoryName";
            resources.ApplyResources(this.CategoryName, "CategoryName");
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(this.UnitName, "UnitName");
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            // 
            // Unitid
            // 
            this.Unitid.DataPropertyName = "Unitid";
            resources.ApplyResources(this.Unitid, "Unitid");
            this.Unitid.Name = "Unitid";
            this.Unitid.ReadOnly = true;
            // 
            // CategoryNo
            // 
            this.CategoryNo.DataPropertyName = "CategoryNo";
            resources.ApplyResources(this.CategoryNo, "CategoryNo");
            this.CategoryNo.Name = "CategoryNo";
            this.CategoryNo.ReadOnly = true;
            // 
            // Showpos
            // 
            this.Showpos.DataPropertyName = "ShowInPOS";
            resources.ApplyResources(this.Showpos, "Showpos");
            this.Showpos.Name = "Showpos";
            this.Showpos.ReadOnly = true;
            // 
            // Allowinv
            // 
            this.Allowinv.DataPropertyName = "AllowInventory";
            resources.ApplyResources(this.Allowinv, "Allowinv");
            this.Allowinv.Name = "Allowinv";
            this.Allowinv.ReadOnly = true;
            // 
            // ReorderLevel
            // 
            this.ReorderLevel.DataPropertyName = "ReorderLevel";
            resources.ApplyResources(this.ReorderLevel, "ReorderLevel");
            this.ReorderLevel.Name = "ReorderLevel";
            this.ReorderLevel.ReadOnly = true;
            // 
            // frmListProduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListProduct";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProitems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button button5;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox allowInventory;
        private System.Windows.Forms.CheckBox ShowInPOS;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Label lblProductNo;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtReorderLevel;
        internal System.Windows.Forms.TextBox txtUnitPrice;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtStocksOnHand;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtCategory;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtBarcode;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtProductCode;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Btnaddedit;
        private System.Windows.Forms.Button Btndel;
        private System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.GroupBox GB;
        private System.Windows.Forms.DataGridView DGVProitems;
        private System.Windows.Forms.Button Btnadd;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox CBItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StocksOnHand;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unitid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Showpos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Allowinv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReorderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemid;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}