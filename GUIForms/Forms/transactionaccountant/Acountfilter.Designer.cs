
namespace Easypos.TransactionsAccountant
{
    partial class Acountfilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acountfilter));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.RBSup = new System.Windows.Forms.RadioButton();
            this.RBCust = new System.Windows.Forms.RadioButton();
            this.Phonenumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.clients = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DTT = new System.Windows.Forms.DateTimePicker();
            this.DTF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.RBSup);
            this.GroupBox3.Controls.Add(this.RBCust);
            this.GroupBox3.Controls.Add(this.Phonenumber);
            this.GroupBox3.Controls.Add(this.label7);
            this.GroupBox3.Controls.Add(this.clients);
            this.GroupBox3.Controls.Add(this.label3);
            this.GroupBox3.Controls.Add(this.DTT);
            this.GroupBox3.Controls.Add(this.DTF);
            this.GroupBox3.Controls.Add(this.label2);
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.Controls.Add(this.Label5);
            resources.ApplyResources(this.GroupBox3, "GroupBox3");
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.TabStop = false;
            // 
            // RBSup
            // 
            resources.ApplyResources(this.RBSup, "RBSup");
            this.RBSup.Name = "RBSup";
            this.RBSup.TabStop = true;
            this.RBSup.UseVisualStyleBackColor = true;
            this.RBSup.Click += new System.EventHandler(this.RBSup_Click);
            // 
            // RBCust
            // 
            resources.ApplyResources(this.RBCust, "RBCust");
            this.RBCust.Name = "RBCust";
            this.RBCust.TabStop = true;
            this.RBCust.UseVisualStyleBackColor = true;
            this.RBCust.Click += new System.EventHandler(this.RBCust_Click);
            // 
            // Phonenumber
            // 
            this.Phonenumber.BackColor = System.Drawing.Color.White;
            this.Phonenumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Phonenumber, "Phonenumber");
            this.Phonenumber.ForeColor = System.Drawing.Color.Black;
            this.Phonenumber.Name = "Phonenumber";
            this.Phonenumber.TextChanged += new System.EventHandler(this.Phonenumber_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // clients
            // 
            this.clients.FormattingEnabled = true;
            resources.ApplyResources(this.clients, "clients");
            this.clients.Name = "clients";
            this.clients.SelectionChangeCommitted += new System.EventHandler(this.clients_SelectionChangeCommitted);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // DTT
            // 
            this.DTT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.DTT, "DTT");
            this.DTT.Name = "DTT";
            // 
            // DTF
            // 
            this.DTF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.DTF, "DTF");
            this.DTF.Name = "DTF";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Label5
            // 
            resources.ApplyResources(this.Label5, "Label5");
            this.Label5.Name = "Label5";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Acountfilter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Acountfilter";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox clients;
        public System.Windows.Forms.DateTimePicker DTT;
        public System.Windows.Forms.DateTimePicker DTF;
        private System.Windows.Forms.TextBox Phonenumber;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton RBCust;
        private System.Windows.Forms.RadioButton RBSup;
    }
}