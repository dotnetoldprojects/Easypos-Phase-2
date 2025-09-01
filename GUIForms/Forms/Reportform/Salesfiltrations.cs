using CrystalDecisions.CrystalReports.Engine;
using Domain;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using MetroFramework.Forms;
using Reporting.others;
using Reporting.sales.Salesreportes;
using Reporting.VM;
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

namespace Reporting.Sales
{
    public partial class Salesfiltrations : MetroForm
    {
        Saleshelper ASR;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        byte[] Logo;
        public Salesfiltrations()
        {
            InitializeComponent();
            Loading();
            ASR = new Saleshelper();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Logo = Convert.FromBase64String(DC.CompanyLogo);
        }
        private void TPACheacked()
        {
            ChkPurchases.Checked = false;
            ChkExpenses.Checked = false;
            ChkPayment.Checked = false;
            ChkPayout.Checked = false;
        }
        private void TPBCheacked()
        {
            Chksales.Checked = false;
            Chksalesdetailes.Checked = false;
            Chksalestaxdetailes.Checked = false;
            Chksalestax.Checked = false;
            Chkonetax.Checked = false;
            Chkuser.Checked = false;
        }
        private void Chkdate_CheckedChanged(object sender, EventArgs e)
        {
            if (Chkdate.Checked)
            {
                label1.Visible = true;
                label2.Visible = true;
                DTF.Visible = true;
                DTT.Visible = true;
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                DTF.Visible = false;
                DTT.Visible = false;
            }
        }
        private void Chksales_Click(object sender, EventArgs e)
        {
            TPACheacked();
        }
        private void ChkExpenses_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkExpenses.Checked)
            {
                Typeexp.Visible = true;
            }
            else
            {
                Typeexp.Visible = false;
            }
        }
        private void Btnshowreport_Click(object sender, EventArgs e)
        {
            Frmreporting FR = new Frmreporting();
            Dataset Dsx = new Dataset();
            ReportDocument Objrpt = new ReportDocument();
            var DF = DTF.Value.ToString("yyyy-MM-dd");
            var DT = DTT.Value.ToString("yyyy-MM-dd");
            if (Chksales.Checked)
            {
                Objrpt = new AllSalesReport();
                List<Saleslist> SL = new List<Saleslist>();
                if (Chkdate.Checked)
                {
                    SL = ASR.Getsaleslist(DF, DT);
                }
                else
                {
                    SL = ASR.Getsaleslist(null, null);
                }
                foreach (var item in SL)
                {
                    Dsx.Sales.Rows.Add(new object[] {
                        item.Invoiceno,
                        item.Name,
                        item.TDate,
                        item.PaymentMethod,
                        item.NonVatTotal,
                        item.Discount,
                        item.VatAmount,
                        item.TotalAmount,
                        Logo,
                        item.Paid,
                        item.Remaining,
                    });
                }
            }
            else if (Chksalesdetailes.Checked)
            {
                Objrpt = new CRSalesrpt();
                List<Saleslist> SL = new List<Saleslist>();
                if (Chkdate.Checked)
                {
                    SL = ASR.Getsalesdetails(DF, DT);
                }
                else
                {
                    SL = ASR.Getsalesdetails(null, null);
                }
                foreach (var item in SL)
                {
                    Dsx.DSI.Rows.Add(new object[] {
                        item.Invoiceno,
                        item.ProductNo,
                        item.ItemPrice,
                        item.Quantity,
                        item.Total,
                        item.TDate,
                        item.TTime,
                        item.Total,
                        null,
                        item.TotalAmount,
                        item.TDDesc,
                        null,
                        Logo,
                    });
                }
            }
            else if (Chksalestaxdetailes.Checked)
            {
                Objrpt = new STPRep();
                List<Saleslist> SL = new List<Saleslist>();
                if (Chkdate.Checked)
                {
                    SL = ASR.Getsaleslist(DF, DT);
                }
                else
                {
                    SL = ASR.Getsaleslist(null, null);
                }
                foreach (var item in SL)
                {
                    Dsx.Sales.Rows.Add(new object[] {
                        item.Invoiceno,
                        item.Name,
                        item.TDate,
                        item.PaymentMethod,
                        item.NonVatTotal - item.Discount,
                        item.Discount,
                        item.VatAmount,
                        item.TotalAmount,
                        Logo,
                    });
                }
            }
            else if (Chksalestax.Checked)
            {
                Objrpt = new CRDSByVAT();
                List<Saleslist> SL = new List<Saleslist>();
                if (Chkdate.Checked)
                {
                    SL = ASR.Getsaleslist(DF, DT);
                }
                else
                {
                    SL = ASR.Getsaleslist(null, null);
                }
                foreach (var item in SL)
                {
                    Dsx.Sales.Rows.Add(new object[] {
                        item.Invoiceno,
                        item.Name,
                        item.TDate,
                        item.PaymentMethod,
                        item.NonVatTotal - item.Discount,
                        item.Discount,
                        item.VatAmount,
                        item.TotalAmount, Logo,
                    });
                }
            }
            else if (Chkonetax.Checked)
            {
                List<Salesonetax> SLO = new List<Salesonetax>();
                Objrpt = new CRonetax();
                if (Chkdate.Checked)
                {
                    SLO = ASR.Getonetax(DF, DT);
                }
                else
                {
                    SLO = ASR.Getonetax(null, null);
                }
                //SOT = CF.SOT;
                foreach (var item in SLO)
                {
                    //Dsx.Onetax.Rows.Add(new object[] {
                    //    item.Tot,
                    //    item.Tax,
                    //    item.Desc,
                    //    Logo,
                    //    item.Totaltax,
                    //    item.Totaltot
                    //});
                }
            }
            else if (ChkExpenses.Checked)
            {
                Objrpt = new ExpensesReport();
                List<EWT> Exp = new List<EWT>();
                if (Chkdate.Checked)
                {
                    Exp = ASR.GetExpenselist(DF, DT, 0);
                }
                else if (Typeexp.Checked)
                {
                    Exp = ASR.GetExpenselist(null, null, int.Parse(Explist.SelectedValue.ToString()));
                }
                else if (Typeexp.Checked && Chkdate.Checked)
                {
                    Exp = ASR.GetExpenselist(DF, DT, int.Parse(Explist.SelectedValue.ToString()));
                }
                else
                {
                    Exp = ASR.GetExpenselist(null, null, 0);
                }
                foreach (var item in Exp)
                {
                    Dsx.ExpensesRevenues.Rows.Add(new object[] {
                        item.ID,
                        item.Amount,
                        item.CDate,
                        item.Vat,
                        Logo,
                        item.Description,
                    });
                }
            }
            else
            {
                MessageBox.Show("برجاء اختيار نوع التقرير", "خطأ");
                return;
            }
            Objrpt.SetDataSource(Dsx);
            Objrpt.SetParameterValue("CompanyName", DC.Name);
            Objrpt.SetParameterValue("Address", DC.Address);
            Objrpt.SetParameterValue("Taxnum", DC.Taxnumber);
            Objrpt.SetParameterValue("PhoneNo", DC.PhoneNo);
            Objrpt.SetParameterValue("Proname", DC.CRN);
            Objrpt.SetParameterValue("English_Shop_name", DC.ENName);
            if (ChkPayout.Checked || ChkPayment.Checked || ChkExpenses.Checked || Chksales.Checked || Chksalestax.Checked || Chksalestaxdetailes.Checked || Chksalesdetailes.Checked || Chkuser.Checked || ChkPurchases.Checked)
            {
                if (Chkdate.Checked)
                {
                    Objrpt.SetParameterValue("SalesDate", $"من {DF} " + " " + $" إلي {DT}");
                }
                else
                {
                    Objrpt.SetParameterValue("SalesDate", "كل الفواتير");
                }
            }
            if (Chkonetax.Checked)
            {
                if (Chkdate.Checked)
                {
                    Objrpt.SetParameterValue("Fromdate", DF);
                    Objrpt.SetParameterValue("Todate", DT);
                }
            }
            if (ChkPayment.Checked)
            {
                Objrpt.SetParameterValue("Vouchertype", "تقرير سندات القبض حسب تاريخ");
            }
            if (ChkPayout.Checked)
            {
                Objrpt.SetParameterValue("Vouchertype", "تقرير سندات الصرف حسب تاريخ");
            }
            // قبل ما تعرض التقرير الجديد، تأكد إنك قفلت اللي قبله
            foreach (Form f in Application.OpenForms)
            {
                if (f is Frmreporting)
                {
                    f.Close();
                    break;
                }
            }
            FR.CRV.ReportSource = Objrpt;
            FR.LoadReport(Objrpt);
            FR.Show();
        }
        private void Typeexp_CheckedChanged(object sender, EventArgs e)
        {
            if (Typeexp.Checked)
            {
                Explist.Visible = true;
                Commondatasales.FillCombo(Explist, GC.LaodETypes(), "Expencestypename", "Id");
            }
            else
            {
                Explist.Visible = false;
            }
        }
        private void ChkPurchases_Click(object sender, EventArgs e)
        {
            TPBCheacked();
        }
    }
}
