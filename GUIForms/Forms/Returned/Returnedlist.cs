using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using iText.StyledXmlParser.Jsoup.Nodes;
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

namespace GUIForms.Forms.Returned
{
    public partial class Returnedlist : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        public Returnedlist()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Getdatalist();
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Btnall_Click(object sender, EventArgs e)
        {
            Loading();
        }
        private void Getdatalist()
        {
            var Res = _IUW.returneds.GetAll().ToList();
            DGV.DataSource = Res.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                x.Returnedtype,
            }).ToList();
        }
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            Getsalesbyfilters();
        }
        public void Getsalesbyfilters()
        {
            var Res = _IUW.returneds.GetAll();

            // شرط رقم الفاتورة
            if (!string.IsNullOrEmpty(IN.Text))
            {
                var invoiceNo = Convert.ToUInt32(IN.Text);
                Res = Res.Where(x => x.Invoiceno == invoiceNo);
            }
            // شرط التاريخ
            if (Searchbydate.Checked)
            {
                var fromDate = DTF.Value.Date;
                var toDate = DTT.Value.Date;
                Res = Res.AsEnumerable()
                             .Where(x =>
                             {
                                 DateTime tDate;
                                 return DateTime.TryParse(x.TDate, out tDate)
                                            && tDate.Date >= fromDate
                                            && tDate.Date <= toDate;
                             })
                             .AsQueryable();
            }
            // تحويل النتائج
            var result = Res.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                x.Returnedtype,
            }).ToList();

            // عرض النتائج
            DGV.DataSource = result;
        }
    }
}
