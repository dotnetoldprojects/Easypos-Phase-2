namespace GUIForms.Forms.Returned
{
    partial class Returnedlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Returnedlist));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picMinimize = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btnsearch = new System.Windows.Forms.Button();
            this.DTT = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.DTF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Searchbydate = new System.Windows.Forms.CheckBox();
            this.IN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Btnall = new System.Windows.Forms.Button();
            this.Btnclose = new System.Windows.Forms.Button();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Show = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Invoiceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.picMinimize);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(945, 54);
            this.panel2.TabIndex = 51;
            // 
            // picMinimize
            // 
            this.picMinimize.BackColor = System.Drawing.Color.PowderBlue;
            this.picMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMinimize.BackgroundImage")));
            this.picMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMinimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picMinimize.Location = new System.Drawing.Point(12, 16);
            this.picMinimize.Name = "picMinimize";
            this.picMinimize.Size = new System.Drawing.Size(25, 21);
            this.picMinimize.TabIndex = 15;
            this.picMinimize.TabStop = false;
            this.picMinimize.Click += new System.EventHandler(this.picMinimize_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(899, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 44);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(782, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(115, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "قائمة الفواتير";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.GroupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel1.Size = new System.Drawing.Size(945, 73);
            this.panel1.TabIndex = 55;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.Btnsearch);
            this.groupBox3.Controls.Add(this.DTT);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.DTF);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(524, 54);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " بحث بواسطة التاريخ ";
            // 
            // Btnsearch
            // 
            this.Btnsearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btnsearch.Location = new System.Drawing.Point(9, 18);
            this.Btnsearch.Name = "Btnsearch";
            this.Btnsearch.Size = new System.Drawing.Size(75, 25);
            this.Btnsearch.TabIndex = 18;
            this.Btnsearch.Text = "بحث";
            this.Btnsearch.UseVisualStyleBackColor = true;
            this.Btnsearch.Click += new System.EventHandler(this.Btnsearch_Click);
            // 
            // DTT
            // 
            this.DTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DTT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTT.Location = new System.Drawing.Point(102, 21);
            this.DTT.Name = "DTT";
            this.DTT.Size = new System.Drawing.Size(130, 21);
            this.DTT.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(233, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "التاريخ الى :";
            // 
            // DTF
            // 
            this.DTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DTF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTF.Location = new System.Drawing.Point(319, 23);
            this.DTF.Name = "DTF";
            this.DTF.Size = new System.Drawing.Size(130, 21);
            this.DTF.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(450, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "التاريخ من :";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.Searchbydate);
            this.GroupBox1.Controls.Add(this.IN);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GroupBox1.Location = new System.Drawing.Point(533, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(400, 54);
            this.GroupBox1.TabIndex = 53;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "بيانات البحث ";
            // 
            // Searchbydate
            // 
            this.Searchbydate.AutoSize = true;
            this.Searchbydate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Searchbydate.Location = new System.Drawing.Point(18, 28);
            this.Searchbydate.Name = "Searchbydate";
            this.Searchbydate.Size = new System.Drawing.Size(122, 17);
            this.Searchbydate.TabIndex = 14;
            this.Searchbydate.Text = "البحث بواسطة التاريخ";
            this.Searchbydate.UseVisualStyleBackColor = true;
            // 
            // IN
            // 
            this.IN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IN.Location = new System.Drawing.Point(226, 23);
            this.IN.Name = "IN";
            this.IN.Size = new System.Drawing.Size(100, 21);
            this.IN.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(327, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم الفاتورة:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.Btnall);
            this.GroupBox2.Controls.Add(this.Btnclose);
            this.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupBox2.Location = new System.Drawing.Point(0, 517);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(945, 85);
            this.GroupBox2.TabIndex = 56;
            this.GroupBox2.TabStop = false;
            // 
            // Btnall
            // 
            this.Btnall.BackColor = System.Drawing.Color.PowderBlue;
            this.Btnall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnall.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnall.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btnall.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.Btnall.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btnall.Location = new System.Drawing.Point(93, 15);
            this.Btnall.Name = "Btnall";
            this.Btnall.Size = new System.Drawing.Size(75, 60);
            this.Btnall.TabIndex = 15;
            this.Btnall.Text = "الكل";
            this.Btnall.UseVisualStyleBackColor = false;
            this.Btnall.Click += new System.EventHandler(this.Btnall_Click);
            // 
            // Btnclose
            // 
            this.Btnclose.BackColor = System.Drawing.Color.PowderBlue;
            this.Btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnclose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btnclose.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.Btnclose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btnclose.Location = new System.Drawing.Point(12, 15);
            this.Btnclose.Name = "Btnclose";
            this.Btnclose.Size = new System.Drawing.Size(75, 60);
            this.Btnclose.TabIndex = 14;
            this.Btnclose.Text = "الغاء";
            this.Btnclose.UseVisualStyleBackColor = false;
            this.Btnclose.Click += new System.EventHandler(this.Btnclose_Click);
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Show,
            this.Invoiceno,
            this.Date,
            this.Subtotal,
            this.Discount,
            this.Tax,
            this.Total,
            this.Type});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle11;
            this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV.Location = new System.Drawing.Point(0, 127);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.DGV.Size = new System.Drawing.Size(945, 390);
            this.DGV.TabIndex = 57;
            // 
            // Show
            // 
            this.Show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Show.HeaderText = "عرض الفاتورة";
            this.Show.Name = "Show";
            this.Show.ReadOnly = true;
            this.Show.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Show.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Show.Text = "Show";
            this.Show.ToolTipText = "عرض الفاتورة";
            this.Show.UseColumnTextForButtonValue = true;
            // 
            // Invoiceno
            // 
            this.Invoiceno.DataPropertyName = "Invoiceno";
            this.Invoiceno.HeaderText = "رقم الفاتورة";
            this.Invoiceno.Name = "Invoiceno";
            this.Invoiceno.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "TDate";
            this.Date.HeaderText = "التاريخ";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "NonVatTotal";
            this.Subtotal.HeaderText = "الاجمالي الجزئي";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "Discount";
            this.Discount.HeaderText = "الخصم";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            // 
            // Tax
            // 
            this.Tax.DataPropertyName = "VatAmount";
            this.Tax.HeaderText = "الضريبة";
            this.Tax.Name = "Tax";
            this.Tax.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "TotalAmount";
            this.Total.HeaderText = "الاجمالي الكلي";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Returnedtype";
            this.Type.HeaderText = "النوع";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Returnedlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 602);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Returnedlist";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Returnedlist";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picMinimize;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Btnsearch;
        private System.Windows.Forms.DateTimePicker DTT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DTF;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.TextBox IN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Searchbydate;
        internal System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Button Btnall;
        private System.Windows.Forms.Button Btnclose;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.DataGridViewButtonColumn Show;
        private System.Windows.Forms.DataGridViewTextBoxColumn Invoiceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}