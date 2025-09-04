using Domain.Dtos;
using Domain.Models;
using GUI.Helpers;
using GUIForms.Dtos;
using GUIForms.Forms.salesforms.Normal;
using GUIForms.helpers;
using GUIForms.models;
using Helpers.Dtos;
using javax.xml.transform;
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
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using UOW;
using Zatca.EInvoice.SDK.Contracts.Models;
using static net.sf.saxon.expr.JPConverter;


namespace Easypos.Salesforms
{
    public partial class Frmbilllist : Form
    {
        Printinginvoice _PI;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Getallsales GAS;
        List<SaleViewModel> Res;
        public Frmbilllist()
        {
            InitializeComponent();
            Loading();
        }
        public async void Generatexml(sale sale,List<salesdetaile> Saledetail)
        {
            List<ProductLine> productLines = new List<ProductLine>();
            Geneatexml GXL = new Geneatexml();
            GXL.Custid = int.Parse(clientID.SelectedValue.ToString());
            GXL.Invtitle = "Inv-" + sale.Invoiceno.ToString();
            const string unitCode = "PCE";
            const decimal taxPercent = 15m;

            for (int i = 0; i < Saledetail.Count; i++)
            {
                productLines.Add(new ProductLine
                {
                    Id = Saledetail[i].TDetailNo.ToString(),
                    Name = Saledetail[i].TDDesc.ToString(),
                    Quantity = int.Parse(Saledetail[i].Quantity.ToString()),
                    UnitCode = unitCode,
                    UnitPrice = decimal.Parse(Saledetail[i].ItemPrice.ToString()),
                    Discount = decimal.Parse(Saledetail[i].Discount.ToString()),
                    TaxPercent = taxPercent
                });
            }
            string InputPath = @"Data/Invoice.xml";
            var data = DC;
            var RBD = Convert.ToDecimal(sale.Discount);
            GXL.Custid = sale.ThirdPartyID ?? 10;
            GXL.Createxmldata(productLines, DC, false, RBD);

            var xmlContent = File.ReadAllText(InputPath);
            var Doc = GC.LoadInvoiceFromString(xmlContent);
            Signdtos Sdtos = new Signdtos();
            Sdtos.Saleid = sale.Invoiceno;
            await Sdtos.Sign(Doc, $"Inv{sale.Invoiceno}");
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            _PI = new Printinginvoice();
            Getdatalist();
            LoadAllCombos();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Getdatalist()
        {
            GAS = new Getallsales();
            Res = GAS.GetSaleslist();
            DGV.DataSource = Res.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                x.Cash,
                x.Bank,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
                x.Type,
                x.Status,
                x.Note
            }).ToList();
        }
        private async void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Dataid = DGV.CurrentRow.Cells[4].Value.ToString();
            var Datatye = DGV.CurrentRow.Cells[14].Value.ToString();
            var Datareg = DGV.CurrentRow.Cells[15].Value.ToString();
            if (DGV.Columns[e.ColumnIndex].Name == "Show")
            {
                frmMSalesBill FMS = new frmMSalesBill();
                frmMSalesBill open = Application.OpenForms["frmMSalesBill"] as frmMSalesBill;
                if (open == null)
                {
                    FMS.ClearAll();
                    FMS.Invid = int.Parse(Dataid);
                    FMS.Btnsave.Text = "تعديل";
                    FMS.Btnsaveandprint.Text = "تعديل وطباعه";
                    if (Datatye == "مسوده")
                    {
                        FMS.Btnsaveandprint.Visible = true;
                        FMS.Btnsave.Visible = true;
                        FMS.Btnsave.Visible = true;
                        FMS.Btnsave.Text = "تعديل";
                        FMS.Btnsaveandprint.Text = "تعديل وطباعه";
                    }
                    else
                    {
                        FMS.Btnsaveandprint.Visible = false;
                        FMS.Btnsave.Visible = false;
                        FMS.Btnsave.Visible = false;
                        FMS.Billtype.Enabled = false;
                        FMS.Billtype.Text = Datatye;
                    }
                    FMS.Getsalesbill();
                    FMS.Show();
                    this.Close();
                }
                else
                {
                    open.ClearAll();
                    open.Activate();
                    if (open.WindowState == FormWindowState.Maximized)
                    {
                        open.Invid = int.Parse(Dataid);
                        if (Datatye == "مسوده")
                        {
                            open.Btnsaveandprint.Visible = true;
                            open.Btnsave.Visible = true;
                            open.Btnsave.Visible = true;
                            open.Btnsave.Text = "تعديل";
                            open.Btnsaveandprint.Text = "تعديل وطباعه";
                            open.Billtype.Text = Datatye;
                        }
                        else
                        {
                            open.Btnsaveandprint.Visible = false;
                            open.Btnsave.Visible = false;
                            open.Btnsave.Visible = false;
                            open.Billtype.Text = Datatye;
                            open.Billtype.Enabled = false;
                        }
                        open.Getsalesbill();
                        this.Close();
                    }
                }
            }
            else if (DGV.Columns[e.ColumnIndex].Name == "Print")
            {
                _PI.Invoice(int.Parse(Dataid));
            }
            else if (DGV.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (Datatye == "مسوده")
                {
                    if (MessageBox.Show("هل تريد حذف الفاتوره؟", "حذف فاتوره", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var data = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == int.Parse(Dataid)).ToList();
                        foreach (var item in data)
                        {
                            _IUW.salesdetailes.Delbyid(Convert.ToInt32(item.TDetailNo));
                            _IUW.Complete();
                        }
                        _IUW.sales.Delbyid(int.Parse(Dataid));
                        _IUW.Complete();
                        Loading();
                        MessageBox.Show("تم حذف الفاتوره بنجاح", "حذف فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("لا يمكن حذف الفاتوره لانها صدرت", "حذف فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (DGV.Columns[e.ColumnIndex].Name == "Btnreg")
            {
                if (Datatye == "مسوده")
                {
                    MessageBox.Show("لا يمكن تسجيل الفاتوره لانها مسوده", "تسجيل فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Datareg == "سجلت")
                {
                    MessageBox.Show("لا يمكن تسجيل الفاتوره لانها مسجله مسبقا", "تسجيل فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        Signdtos SD = new Signdtos();
                        var GUL = _IUW.UBLS.GetAll().Where(x => x.Saleid == int.Parse(Dataid)).FirstOrDefault();
                        if (GUL != null) {
                            SD.Saleid = int.Parse(Dataid);
                            SD.Ublid = GUL.Id;
                            await SD.SendInvoiceAsync(GUL.Invoicehash, GUL.Uuid, GUL.Invoice, GUL.Path, GUL.QRCode);
                        }
                        else
                        {
                            var Invdata = _IUW.sales.GetAll().Where(x => x.Invoiceno == int.Parse(Dataid)).FirstOrDefault();
                            var Invsaledetail = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invdata.Invoiceno).ToList();
                            SD.Saleid = int.Parse(Invdata.Invoiceno.ToString());
                            Generatexml(Invdata, Invsaledetail);
                            var GUL2 = _IUW.UBLS.GetAll().Where(x => x.Saleid == int.Parse(Dataid)).FirstOrDefault();
                            SD.Ublid = GUL2.Id;
                            await SD.SendInvoiceAsync(GUL2.Invoicehash, GUL2.Uuid, GUL2.Invoice, GUL2.Path, GUL2.QRCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = new ExceptionLogger(_IUW);
                        logger.Log(ex, "Data Registration");
                    }
                    Getdatalist();
                }
            }
        }
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            Getsalesbyfilters();
        }
        public void Getsalesbyfilters()
        {
            var query = Res.AsQueryable();

            // شرط رقم الفاتورة
            if (!string.IsNullOrEmpty(IN.Text))
            {
                int invoiceNo = int.Parse(IN.Text);
                query = query.Where(x => x.Invoiceno == invoiceNo);
            }

            // شرط العميل
            if (clientID.SelectedValue != null && (int)clientID.SelectedValue != 0)
            {
                int clientId = (int)clientID.SelectedValue;
                query = query.Where(x => x.ThirdPartyID == clientId);
            }

            // شرط التاريخ
            if (Searchbydate.Checked)
            {
                var fromDate = DTF.Value.Date;
                var toDate = DTT.Value.Date;
                query = query.AsEnumerable()
                             .Where(x =>
                             {
                                 DateTime tDate;
                                 return DateTime.TryParse(x.TDate, out tDate)
                                            && tDate.Date >= fromDate
                                            && tDate.Date <= toDate;
                             })
                             .AsQueryable();
            }

            // شرط الوقت
            if (Searchbytime.Checked)
            {
                if (TimeSpan.TryParse(TTF.Text, out var fromTime) &&
                    TimeSpan.TryParse(TTT.Text, out var toTime))
                {
                    query = query.AsEnumerable()
                                 .Where(x =>
                                 {
                                     if (TimeSpan.TryParse(x.TTime, out var tTime))
                                     {
                                         return tTime >= fromTime && tTime <= toTime;
                                     }
                                     return false;
                                 })
                                 .AsQueryable();
                }
            }



            // شرط رقم الهاتف
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                query = query.Where(x => x.Phone.Contains(txtPhone.Text));
            }

            // شرط الملاحظات
            if (!string.IsNullOrEmpty(txtNote.Text))
            {
                query = query.Where(x => x.Note.Contains(txtNote.Text));
            }

            // تحويل النتائج
            var result = query.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                x.Cash,
                x.Bank,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
                x.Type,
                x.Note
            }).ToList();

            // عرض النتائج
            DGV.DataSource = result;
        }
        private void Btnall_Click(object sender, EventArgs e)
        {
            Loading();
        }
    }
}
