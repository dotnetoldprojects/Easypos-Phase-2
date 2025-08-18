namespace Easypos.Masters.Subforms
{
    partial class FrmExpencetype
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpencetype));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expencestypename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btnaddedit = new System.Windows.Forms.Button();
            this.Btndel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // DGV
            // 
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Expencestypename});
            resources.ApplyResources(this.DGV, "DGV");
            this.DGV.Name = "DGV";
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            resources.ApplyResources(this.Id, "Id");
            this.Id.Name = "Id";
            // 
            // Expencestypename
            // 
            this.Expencestypename.DataPropertyName = "Expencestypename";
            resources.ApplyResources(this.Expencestypename, "Expencestypename");
            this.Expencestypename.Name = "Expencestypename";
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmExpencetype
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btnaddedit);
            this.Controls.Add(this.Btndel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.Name = "FrmExpencetype";
            this.Resizable = false;
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Button Btnaddedit;
        private System.Windows.Forms.Button Btndel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expencestypename;
    }
}