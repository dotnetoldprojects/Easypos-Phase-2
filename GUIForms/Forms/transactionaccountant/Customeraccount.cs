using Domain.Models;
using GUIForms.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

namespace Easypos.TransactionsAccountant
{
    public partial class Customeraccount : Form
    {
        Getcentralaizes GC;
        IUnitofwork _IUW;
        company DC;
        public Customeraccount()
        {
            InitializeComponent();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
        }
        public int Tid { get; set; }
        private void Loading()
        {
            var Balance = GC.GetBalance(Tid, textBox5.Text);
            var Res = GC.LoadAccounting(Tid, textBox5.Text, textBox6.Text);
            foreach (var item in Res)
            {
                textBox1.Text = item.MobileNumber;
                textBox2.Text = item.Name;
                textBox3.Text = item.Address;
                textBox4.Text = item.Taxnumber;
                break;
            }
            DGV.Rows.Add("--", "--", "رصيد افتتاحي", 0.00, Balance, Balance);
            foreach (var item in Res)
            {
                var BT = item.Type;
                if (BT != "مسوده")
                {
                    var BN = item.InvoiceNo;
                    decimal Creditor = 0;
                    decimal Dibtor = 0;
                    if (item.Type == "فاتورة مبيعات")
                    {
                        Creditor = decimal.Parse(item.TotalAmount);
                        Dibtor = 0;
                    }
                    if (item.Type == "سند ايصال مبيعات")
                    {
                        Dibtor = item.Paid;
                        Creditor = 0;
                    }
                    DGV.Rows.Add(BN, item.TDate, item.Type, Dibtor, Creditor, 0.00); 
                    Double TBalance = 0.00;
                    for (int i = 0; i < DGV.Rows.Count; i++)
                    {
                        var Det = DGV.Rows[i].Cells[2].Value.ToString();
                        var Tot = Convert.ToDouble(DGV.Rows[i].Cells[3].Value.ToString());
                        var Pay = Convert.ToDouble(DGV.Rows[i].Cells[4].Value.ToString());
                        if (Det == "رصيد افتتاحي")
                        {
                            TBalance = Pay;
                            var GBalnce = Math.Round(TBalance, 2);
                            DGV.Rows[i].Cells[5].Value = GBalnce;
                        }
                        if (Det == "فاتورة مبيعات")
                        {
                            TBalance = TBalance + (Tot - Pay);
                            var GBalnce = Math.Round(TBalance, 2);
                            DGV.Rows[i].Cells[5].Value = GBalnce;
                        }
                        if (Det == "سند ايصال مبيعات")
                        {
                            TBalance = TBalance + Tot;
                            var GBalnce = Math.Round(TBalance, 2);
                            DGV.Rows[i].Cells[5].Value = GBalnce;
                        }
                        //if (Det == "فاتورة مرتجع")
                        //{
                        //    TBalance = TBalance + Pay;
                        //    var GBalnce = Math.Round(TBalance, 2);
                        //    //CA.DGV.Rows[i].Cells[5].Value = GBalnce;
                        //}
                        if (Det == "سندات دفع")
                        {
                            TBalance = TBalance - Tot;
                            var GBalnce = Math.Round(TBalance, 2);
                            //CA.DGV.Rows[i].Cells[5].Value = GBalnce;
                        }
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Customeraccount_Load(object sender, EventArgs e)
        {
            Loading();
        }
    }
}
