namespace Reporting.Sales
{
    partial class Salesfiltrations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Salesfiltrations));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btnshowreport = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Chksalesreturned = new System.Windows.Forms.RadioButton();
            this.Chksales = new System.Windows.Forms.RadioButton();
            this.Users = new System.Windows.Forms.ComboBox();
            this.Chkuser = new System.Windows.Forms.RadioButton();
            this.Chkonetax = new System.Windows.Forms.RadioButton();
            this.Chksalestax = new System.Windows.Forms.RadioButton();
            this.Chksalestaxdetailes = new System.Windows.Forms.RadioButton();
            this.Chksalesdetailes = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Typeexp = new System.Windows.Forms.CheckBox();
            this.Explist = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.ChkPayment = new System.Windows.Forms.RadioButton();
            this.ChkPayout = new System.Windows.Forms.RadioButton();
            this.ChkExpenses = new System.Windows.Forms.RadioButton();
            this.ChkPurchases = new System.Windows.Forms.RadioButton();
            this.Chkdate = new System.Windows.Forms.CheckBox();
            this.DTT = new System.Windows.Forms.DateTimePicker();
            this.DTF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Chksalestaxdetaileszatca = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btnshowreport);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // Btnshowreport
            // 
            this.Btnshowreport.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.Btnshowreport, "Btnshowreport");
            this.Btnshowreport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btnshowreport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Btnshowreport.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btnshowreport.Name = "Btnshowreport";
            this.Btnshowreport.UseVisualStyleBackColor = false;
            this.Btnshowreport.Click += new System.EventHandler(this.Btnshowreport_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.tabControl1);
            this.GroupBox3.Controls.Add(this.Chkdate);
            this.GroupBox3.Controls.Add(this.DTT);
            this.GroupBox3.Controls.Add(this.DTF);
            this.GroupBox3.Controls.Add(this.label2);
            this.GroupBox3.Controls.Add(this.label1);
            resources.ApplyResources(this.GroupBox3, "GroupBox3");
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Chksalestaxdetaileszatca);
            this.tabPage1.Controls.Add(this.Chksalesreturned);
            this.tabPage1.Controls.Add(this.Chksales);
            this.tabPage1.Controls.Add(this.Users);
            this.tabPage1.Controls.Add(this.Chkuser);
            this.tabPage1.Controls.Add(this.Chkonetax);
            this.tabPage1.Controls.Add(this.Chksalestax);
            this.tabPage1.Controls.Add(this.Chksalestaxdetailes);
            this.tabPage1.Controls.Add(this.Chksalesdetailes);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Chksalesreturned
            // 
            resources.ApplyResources(this.Chksalesreturned, "Chksalesreturned");
            this.Chksalesreturned.Name = "Chksalesreturned";
            this.Chksalesreturned.UseVisualStyleBackColor = true;
            // 
            // Chksales
            // 
            resources.ApplyResources(this.Chksales, "Chksales");
            this.Chksales.Name = "Chksales";
            this.Chksales.UseVisualStyleBackColor = true;
            this.Chksales.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // Users
            // 
            this.Users.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Users.FormattingEnabled = true;
            resources.ApplyResources(this.Users, "Users");
            this.Users.Name = "Users";
            // 
            // Chkuser
            // 
            resources.ApplyResources(this.Chkuser, "Chkuser");
            this.Chkuser.Name = "Chkuser";
            this.Chkuser.UseVisualStyleBackColor = true;
            this.Chkuser.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // Chkonetax
            // 
            resources.ApplyResources(this.Chkonetax, "Chkonetax");
            this.Chkonetax.Name = "Chkonetax";
            this.Chkonetax.UseVisualStyleBackColor = true;
            this.Chkonetax.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // Chksalestax
            // 
            resources.ApplyResources(this.Chksalestax, "Chksalestax");
            this.Chksalestax.Name = "Chksalestax";
            this.Chksalestax.UseVisualStyleBackColor = true;
            this.Chksalestax.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // Chksalestaxdetailes
            // 
            resources.ApplyResources(this.Chksalestaxdetailes, "Chksalestaxdetailes");
            this.Chksalestaxdetailes.Name = "Chksalestaxdetailes";
            this.Chksalestaxdetailes.UseVisualStyleBackColor = true;
            this.Chksalestaxdetailes.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // Chksalesdetailes
            // 
            resources.ApplyResources(this.Chksalesdetailes, "Chksalesdetailes");
            this.Chksalesdetailes.Name = "Chksalesdetailes";
            this.Chksalesdetailes.UseVisualStyleBackColor = true;
            this.Chksalesdetailes.Click += new System.EventHandler(this.Chksales_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Typeexp);
            this.tabPage2.Controls.Add(this.Explist);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Controls.Add(this.ChkPayment);
            this.tabPage2.Controls.Add(this.ChkPayout);
            this.tabPage2.Controls.Add(this.ChkExpenses);
            this.tabPage2.Controls.Add(this.ChkPurchases);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Typeexp
            // 
            resources.ApplyResources(this.Typeexp, "Typeexp");
            this.Typeexp.Name = "Typeexp";
            this.Typeexp.UseVisualStyleBackColor = true;
            this.Typeexp.CheckedChanged += new System.EventHandler(this.Typeexp_CheckedChanged);
            // 
            // Explist
            // 
            this.Explist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Explist.FormattingEnabled = true;
            resources.ApplyResources(this.Explist, "Explist");
            this.Explist.Name = "Explist";
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.ChkPurchases_Click);
            // 
            // ChkPayment
            // 
            resources.ApplyResources(this.ChkPayment, "ChkPayment");
            this.ChkPayment.Name = "ChkPayment";
            this.ChkPayment.UseVisualStyleBackColor = true;
            this.ChkPayment.Click += new System.EventHandler(this.ChkPurchases_Click);
            // 
            // ChkPayout
            // 
            resources.ApplyResources(this.ChkPayout, "ChkPayout");
            this.ChkPayout.Name = "ChkPayout";
            this.ChkPayout.UseVisualStyleBackColor = true;
            this.ChkPayout.Click += new System.EventHandler(this.ChkPurchases_Click);
            // 
            // ChkExpenses
            // 
            resources.ApplyResources(this.ChkExpenses, "ChkExpenses");
            this.ChkExpenses.Name = "ChkExpenses";
            this.ChkExpenses.UseVisualStyleBackColor = true;
            this.ChkExpenses.CheckedChanged += new System.EventHandler(this.ChkExpenses_CheckedChanged);
            this.ChkExpenses.Click += new System.EventHandler(this.ChkPurchases_Click);
            // 
            // ChkPurchases
            // 
            resources.ApplyResources(this.ChkPurchases, "ChkPurchases");
            this.ChkPurchases.Name = "ChkPurchases";
            this.ChkPurchases.UseVisualStyleBackColor = true;
            this.ChkPurchases.Click += new System.EventHandler(this.ChkPurchases_Click);
            // 
            // Chkdate
            // 
            resources.ApplyResources(this.Chkdate, "Chkdate");
            this.Chkdate.Name = "Chkdate";
            this.Chkdate.UseVisualStyleBackColor = true;
            this.Chkdate.CheckedChanged += new System.EventHandler(this.Chkdate_CheckedChanged);
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
            // Chksalestaxdetaileszatca
            // 
            resources.ApplyResources(this.Chksalestaxdetaileszatca, "Chksalestaxdetaileszatca");
            this.Chksalestaxdetaileszatca.Name = "Chksalestaxdetaileszatca";
            this.Chksalestaxdetaileszatca.UseVisualStyleBackColor = true;
            // 
            // Salesfiltrations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox3);
            this.MaximizeBox = false;
            this.Name = "Salesfiltrations";
            this.Resizable = false;
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.groupBox1.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btnshowreport;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox Users;
        public System.Windows.Forms.DateTimePicker DTT;
        public System.Windows.Forms.DateTimePicker DTF;
        public System.Windows.Forms.RadioButton Chkuser;
        public System.Windows.Forms.RadioButton Chksalestax;
        public System.Windows.Forms.RadioButton Chksalesdetailes;
        public System.Windows.Forms.RadioButton Chksalestaxdetailes;
        public System.Windows.Forms.RadioButton Chkonetax;
        public System.Windows.Forms.RadioButton Chksales;
        public System.Windows.Forms.CheckBox Chkdate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.RadioButton ChkPurchases;
        public System.Windows.Forms.RadioButton ChkPayout;
        public System.Windows.Forms.RadioButton ChkExpenses;
        public System.Windows.Forms.RadioButton ChkPayment;
        public System.Windows.Forms.RadioButton Chksalesreturned;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.ComboBox Explist;
        public System.Windows.Forms.CheckBox Typeexp;
        public System.Windows.Forms.RadioButton Chksalestaxdetaileszatca;
    }
}