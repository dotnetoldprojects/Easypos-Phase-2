using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

namespace Easypos.TransactionsAccountant
{
    public partial class Acountfilter : Form
    {
        Getcentralaizes GC;
        IUnitofwork _IUW;
        company DC;
        public int Tid { get; set; }
        public Acountfilter()
        {
            InitializeComponent();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Commondatasales.FillCombo(clients, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void LoadAllCombos()
        {
            if (RBCust.Checked)
            {
                Commondatasales.FillCombo(clients, GC.Getcustomerdatalist(), "Name", "ID");
            }
            if (RBSup.Checked)
            {
                Commondatasales.FillCombo(clients, GC.Getsupplierdatalist(), "Name", "ID");
            }
        }
        private void RBCust_Click(object sender, EventArgs e)
        {
            LoadAllCombos();
        }
        private void RBSup_Click(object sender, EventArgs e)
        {
            LoadAllCombos();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Phonenumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Phonenumber.Text))
            {
                var client = GC.Getbyphonrdatalist(Phonenumber.Text);
                if (client != null)
                {
                    Tid = client.ID;
                    clients.SelectedValue = client.ID;
                }
                else
                {
                    // ممكن تعرض رسالة إن الرقم غير موجود
                    MessageBox.Show("لا يوجد عميل بهذا الرقم");
                }
            }
            else
            {
                LoadAllCombos();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Checkcustomer();
        }
        private void clients_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Checkcustomer();
        }
        void Checkcustomer() {
            Tid = Convert.ToInt32(clients.SelectedIndex);
            if (RBCust.Checked)
            {
                if (Tid == 1)
                {
                    MessageBox.Show("لا يمكن اختيار عميل افتراضي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            Customeraccount CA = new Customeraccount();
            CA.Tid = int.Parse(clients.SelectedValue.ToString());
            if (RBCust.Checked)
            {
                CA.TT = "2";
            }
            else
            {
                CA.TT = "1";
            }
            if (Tid == 0 || Tid == 1)
            {
                MessageBox.Show("برجاء اختيار الموزع او العميل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CA.textBox5.Text = DTF.Value.ToString("dd-MM-yyyy");
            CA.textBox6.Text = DTT.Value.ToString("dd-MM-yyyy");
            CA.ShowDialog();
        }
    }
}
