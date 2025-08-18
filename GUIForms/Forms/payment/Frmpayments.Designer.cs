namespace Easypos.Payment
{
    partial class Frmpayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmpayments));
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtTotalPay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.clients = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btnsave = new System.Windows.Forms.Button();
            this.Btnclose = new System.Windows.Forms.Button();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.AddCash = new FontAwesome.Sharp.IconButton();
            this.AddBank = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRem = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.panel2.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // txtBank
            // 
            resources.ApplyResources(this.txtBank, "txtBank");
            this.txtBank.Name = "txtBank";
            this.txtBank.TextChanged += new System.EventHandler(this.txtBank_TextChanged);
            this.txtBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCash_KeyPress);
            // 
            // txtTotalPay
            // 
            resources.ApplyResources(this.txtTotalPay, "txtTotalPay");
            this.txtTotalPay.Name = "txtTotalPay";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // Label2
            // 
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.Name = "Label2";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            // 
            // Label1
            // 
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.Name = "Label1";
            // 
            // clients
            // 
            this.clients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.clients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.clients.DisplayMember = "Name";
            resources.ApplyResources(this.clients, "clients");
            this.clients.Name = "clients";
            this.clients.ValueMember = "ID";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // Btnsave
            // 
            resources.ApplyResources(this.Btnsave, "Btnsave");
            this.Btnsave.Name = "Btnsave";
            this.Btnsave.UseVisualStyleBackColor = true;
            this.Btnsave.Click += new System.EventHandler(this.Btnsave_Click);
            // 
            // Btnclose
            // 
            resources.ApplyResources(this.Btnclose, "Btnclose");
            this.Btnclose.Name = "Btnclose";
            this.Btnclose.UseVisualStyleBackColor = true;
            this.Btnclose.Click += new System.EventHandler(this.Btnclose_Click);
            // 
            // txtCash
            // 
            resources.ApplyResources(this.txtCash, "txtCash");
            this.txtCash.Name = "txtCash";
            this.txtCash.TextChanged += new System.EventHandler(this.txtCash_TextChanged);
            this.txtCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCash_KeyPress);
            // 
            // AddCash
            // 
            this.AddCash.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.AddCash, "AddCash");
            this.AddCash.IconChar = FontAwesome.Sharp.IconChar.BackwardStep;
            this.AddCash.IconColor = System.Drawing.Color.Black;
            this.AddCash.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AddCash.IconSize = 15;
            this.AddCash.Name = "AddCash";
            this.AddCash.UseVisualStyleBackColor = true;
            this.AddCash.Click += new System.EventHandler(this.AddCash_Click);
            // 
            // AddBank
            // 
            this.AddBank.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.AddBank, "AddBank");
            this.AddBank.IconChar = FontAwesome.Sharp.IconChar.BackwardStep;
            this.AddBank.IconColor = System.Drawing.Color.Black;
            this.AddBank.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AddBank.IconSize = 15;
            this.AddBank.Name = "AddBank";
            this.AddBank.UseVisualStyleBackColor = true;
            this.AddBank.Click += new System.EventHandler(this.AddBank_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtRem
            // 
            resources.ApplyResources(this.txtRem, "txtRem");
            this.txtRem.Name = "txtRem";
            // 
            // Frmpayments
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRem);
            this.Controls.Add(this.AddBank);
            this.Controls.Add(this.AddCash);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.Btnclose);
            this.Controls.Add(this.Btnsave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clients);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.txtTotalPay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmpayments";
            this.Load += new System.EventHandler(this.Frmpayments_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtTotal;
        internal System.Windows.Forms.Label Label1;
        public System.Windows.Forms.ComboBox clients;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btnsave;
        private System.Windows.Forms.Button Btnclose;
        private FontAwesome.Sharp.IconButton AddCash;
        private FontAwesome.Sharp.IconButton AddBank;
        public System.Windows.Forms.TextBox txtBank;
        public System.Windows.Forms.TextBox txtCash;
        public System.Windows.Forms.TextBox txtTotalPay;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtRem;
    }
}