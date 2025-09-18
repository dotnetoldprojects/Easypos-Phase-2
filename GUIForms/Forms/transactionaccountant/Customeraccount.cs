using Domain;
using Domain.Models;
using GUIForms.Dtos;
using Reporting;
using Reporting.accountant;
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
        public string TT { get; set; }
        private void Loading()
        {
            var Balance = GC.GetBalance(Tid, textBox5.Text,TT);
            var Res = GC.LoadAccounting(Tid, textBox5.Text, textBox6.Text,TT);
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
                    var BN = (item.InvoiceNo == 0) ? item.ID : item.InvoiceNo;
                    decimal Creditor = 0;
                    decimal Dibtor = 0;
                    if (item.Type == "فاتورة مبيعات")
                    {
                        Creditor = 0;
                        if (item.TotalAmount != null)
                        {
                            Dibtor = decimal.Parse(item.TotalAmount);
                        }
                    }
                    if (item.Type == "سند ايصال مبيعات" || item.Type == "ايرادات اخرى")
                    {
                        Dibtor = item.Paid;
                        Creditor = 0;
                    }
                    if (item.Type == "فاتورة مشتريات")
                    {
                        if (item.TotalAmount != null)
                        {
                            Dibtor = decimal.Parse(item.TotalAmount);
                        }
                        Creditor = 0;
                    }
                    if (item.Type == "سند دفع لمورد")
                    {
                        Dibtor = 0;
                        Creditor = item.Paid;
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
                        if (Det == "سند ايصال مبيعات" || Det == "ايرادات اخرى")
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
                        if (Det == "فاتورة مشتريات")
                        {
                            TBalance = TBalance + (Tot - Pay);
                            var GBalnce = Math.Round(TBalance, 2);
                            DGV.Rows[i].Cells[5].Value = GBalnce;
                        }
                        if (Det == "سند دفع لمورد")
                        {
                            TBalance = TBalance + (Tot - Pay);
                            var GBalnce = Math.Round(TBalance, 2);
                            DGV.Rows[i].Cells[5].Value = GBalnce;
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

        private void button3_Click(object sender, EventArgs e)
        {
            Accountantreport AR = new Accountantreport();
            Frmreporting FQR = new Frmreporting();
            Dataset ds = new Dataset();
            byte[] Logo = Convert.FromBase64String(DC.CompanyLogo);
            int i = 0;
            for (i = 0; i < DGV.Rows.Count; i++)
            {
                var DTD = DGV.Rows[i].Cells[1].Value;
                if (DTD == null)
                {
                    DTD = textBox5.Text;
                    DGV.Rows[i].Cells[0].Value = 0;
                }
                ds.Accountants.Rows.Add(new object[] {
                    DGV.Rows[i].Cells[0].Value.ToString(),
                    DTD,
                    DGV.Rows[i].Cells[2].Value.ToString(),
                    DGV.Rows[i].Cells[3].Value.ToString(),
                    DGV.Rows[i].Cells[4].Value.ToString(),
                    DGV.Rows[i].Cells[5].Value.ToString(),
                    Logo
                });
            }
            AR.SetDataSource(ds);
            FQR.CRV.ReportSource = AR;
            FQR.CRV.Refresh();


            AR.SetParameterValue("CompanyName", DC.Name);
            AR.SetParameterValue("Address", DC.Address);
            AR.SetParameterValue("PhoneNo", "PN");
            AR.SetParameterValue("MobileNo", "MN");
            AR.SetParameterValue("Taxnum", DC.Taxnumber);
            AR.SetParameterValue("Fromdate", textBox5.Text);
            AR.SetParameterValue("Todate", textBox6.Text);
            AR.SetParameterValue("Clientname", textBox2.Text);
            AR.SetParameterValue("Clientaddress", textBox3.Text);
            AR.SetParameterValue("Taxnumber", textBox4.Text);
            if ( TT == "2")
            {
                AR.SetParameterValue("TOF", "كشف حساب عميل");
            }
            else
            {
                AR.SetParameterValue("TOF", "كشف حساب مورد");
            }
            AR.SetParameterValue("English_Shop_name", DC.ENName);
            AR.SetParameterValue("Proname", DC.CRN);
            FQR.Show();
        }
    }
}
