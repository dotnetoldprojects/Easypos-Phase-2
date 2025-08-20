namespace Easypos.Masters.Subforms
{
    partial class frmProductsOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductsOrder));
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.dgvProducts);
            this.GroupBox3.Controls.Add(this.cmbCategory);
            this.GroupBox3.Controls.Add(this.label1);
            resources.ApplyResources(this.GroupBox3, "GroupBox3");
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.TabStop = false;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ProductName,
            this.Order});
            resources.ApplyResources(this.dgvProducts, "dgvProducts");
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.RowTemplate.Height = 26;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DisplayMember = "Description";
            this.cmbCategory.FormattingEnabled = true;
            resources.ApplyResources(this.cmbCategory, "cmbCategory");
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.ValueMember = "CategoryNo";
            this.cmbCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbCategory_SelectionChangeCommitted);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Name = "lblTitle";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btnclose);
            this.groupBox1.Controls.Add(this.btnSave);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ProductNo";
            resources.ApplyResources(this.ID, "ID");
            this.ID.Name = "ID";
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "Description";
            this.ProductName.FillWeight = 73.85786F;
            resources.ApplyResources(this.ProductName, "ProductName");
            this.ProductName.Name = "ProductName";
            // 
            // Order
            // 
            this.Order.DataPropertyName = "Order";
            this.Order.FillWeight = 73.85786F;
            resources.ApplyResources(this.Order, "Order");
            this.Order.Name = "Order";
            this.Order.ReadOnly = true;
            // 
            // frmProductsOrder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.GroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProductsOrder";
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btnclose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
    }
}