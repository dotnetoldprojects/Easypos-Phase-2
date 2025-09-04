using Domain.Models;
using Easypos.Payment;
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

namespace GUIForms.Forms.Returned
{
    public partial class Returnedbill : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Usingnumber _NO;
        public int TP { get; set; }
        public Returnedbill()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            txtBarcode.Focus();
            Billtype.SelectedIndex = 0;
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcode.Text))
            {
                MessageBox.Show("برجاء ادخال رقم الفاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Btnsaved();
                openpayment();
            }

        }
        public void openpayment()
        {
            if (Billtype.SelectedIndex == 1)
            {
                Savpayment();
            }
            if (Billtype.SelectedIndex == 2)
            {
                Savpayout();
            }
        }
        public void Btnsaved()
        {
            if (DGV.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد منتجات لإضافتها", "خطأ");
                return;
            }
            else
            {
                try
                {
                    Savereturned();
                }
                catch (Exception ex)
                {
                    var logger = new ExceptionLogger(_IUW);
                    logger.Log(ex, "Purchases");
                }
            }
        }
        private void Savereturned()
        {
            var pur = new returned
            {
                Returnedtype = Billtype.Text,
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
            };
            var details = new List<returnedetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                returnedetaile SD = new returnedetaile();
                SD.ProductNo = int.Parse(DGV.Rows[i].Cells[0].Value.ToString());
                SD.TDDesc = DGV.Rows[i].Cells[1].Value.ToString();
                SD.Unitid = int.Parse(DGV.Rows[i].Cells[3].Value.ToString());
                SD.Quantity = int.Parse(DGV.Rows[i].Cells[4].Value.ToString());
                SD.ItemPrice = double.Parse(DGV.Rows[i].Cells[5].Value.ToString());
                SD.Subtotal = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Discount = 0;
                SD.Totafterdiscount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Total = (decimal?)(SD.ItemPrice * SD.Quantity);
                details.Add(SD);
                var SOH = _IUW.stok_transactions.GetAll().Where(s => s.Proid == SD.ProductNo).FirstOrDefault();
                if (SOH != null)
                {
                    if (Billtype.SelectedIndex == 1)
                    {
                        SOH.Quantity += int.Parse(SD.Quantity.ToString());
                    }
                    if (Billtype.SelectedIndex == 2)
                    {
                        SOH.Quantity -= int.Parse(SD.Quantity.ToString());
                    }
                    _IUW.stok_transactions.Update(SOH);
                    _IUW.Complete();
                }
            }
        }
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Billtype.SelectedIndex == 0)
                {
                    MessageBox.Show("برجاء ادخال نوع الفاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    object Obj = new object();
                    if (Billtype.SelectedIndex == 1)
                    {
                        sale sal = new sale();
                        Obj = _IUW.sales.GetAll().FirstOrDefault(x => x.Invoiceno == int.Parse(txtBarcode.Text));
                        if (Obj != null)
                        {
                            sal = (sale)Obj;
                            if (sal.Billtype != "صدرت")
                            {
                                MessageBox.Show("هذه الفاتورة لم تصدر من قبل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (sal != null)
                                {
                                    TP = int.Parse(sal.ThirdPartyID.ToString());
                                    txtTBV.Text = sal.NonVatTotal.ToString();
                                    txtDiscount.Text = sal.Discount.ToString();
                                    txtTax.Text = sal.VatAmount.ToString();
                                    txtTotal.Text = sal.TotalAmount;
                                    var details = _IUW.salesdetailes.GetAll()
                                                                    .Where(sd => sd.InvoiceNo == sal.Invoiceno)
                                                                    .Join(
                                                                        _IUW.unittypes.GetAll(),
                                                                        sd => sd.Unitid,
                                                                        ut => ut.ID,
                                                                        (sd, ut) => new
                                                                        {
                                                                            sd.InvoiceNo,
                                                                            sd.ProductNo,
                                                                            sd.TDDesc,
                                                                            ut.UName,
                                                                            sd.Unitid,
                                                                            sd.Quantity,
                                                                            sd.ItemPrice,
                                                                            sd.Discount,
                                                                            sd.Total
                                                                        }
                                                                    ).OrderByDescending(x => x.InvoiceNo)
                                                                     .ToList();
                                    foreach (var detail in details)
                                    {
                                        DGV.Rows.Add(detail.ProductNo,
                                                     detail.TDDesc,
                                                     detail.UName,
                                                     detail.Unitid,
                                                     detail.Quantity,
                                                     detail.ItemPrice,
                                                     detail.Discount,
                                                     detail.Total);
                                    }
                                }
                            }
                        }
                    }
                    if (Billtype.SelectedIndex == 2)
                    {
                        purchase pur = new purchase();
                        Obj = _IUW.purchases.GetAll().FirstOrDefault(x => x.Invoiceno == int.Parse(txtBarcode.Text));
                        if (Obj != null)
                        {
                            pur = (purchase)Obj;
                            if (pur.Billtype != "صدرت")
                            {
                                MessageBox.Show("هذه الفاتورة لم تصدر من قبل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (pur != null)
                                {
                                    TP = int.Parse(pur.ThirdPartyID.ToString());
                                    txtTBV.Text = pur.NonVatTotal.ToString();
                                    txtDiscount.Text = pur.Discount.ToString();
                                    txtTax.Text = pur.VatAmount.ToString();
                                    txtTotal.Text = pur.TotalAmount;
                                    var details = _IUW.purchasedetailes.GetAll()
                                                                    .Where(sd => sd.InvoiceNo == pur.Invoiceno)
                                                                    .Join(
                                                                        _IUW.unittypes.GetAll(),
                                                                        sd => sd.Unitid,
                                                                        ut => ut.ID,
                                                                        (sd, ut) => new
                                                                        {
                                                                            sd.InvoiceNo,
                                                                            sd.ProductNo,
                                                                            sd.TDDesc,
                                                                            ut.UName,
                                                                            sd.Unitid,
                                                                            sd.Quantity,
                                                                            sd.ItemPrice,
                                                                            sd.Discount,
                                                                            sd.Total
                                                                        }
                                                                    ).OrderByDescending(x => x.InvoiceNo)
                                                                     .ToList();
                                    foreach (var detail in details)
                                    {
                                        DGV.Rows.Add(detail.ProductNo,
                                                     detail.TDDesc,
                                                     detail.UName,
                                                     detail.Unitid,
                                                     detail.Quantity,
                                                     detail.ItemPrice,
                                                     detail.Discount,
                                                     detail.Total);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Savpayout()
        {
            var obj = _IUW.paymentouts.GetAll().Where(x => x.InvoiceNo == int.Parse(txtBarcode.Text)).FirstOrDefault();
            _IUW.paymentouts.Delete(obj);
            var pur = _IUW.purchases.GetAll().Where(x => x.Invoiceno == int.Parse(txtBarcode.Text)).FirstOrDefault();
            pur.Billtype = "مسودة";
            _IUW.Complete();
        }
        private void Savpayment()
        {
            var obj = _IUW.payments.GetAll().Where(x => x.InvoiceNo == int.Parse(txtBarcode.Text)).FirstOrDefault();
            _IUW.payments.Delete(obj);
            var sal = _IUW.sales.GetAll().Where(x => x.Invoiceno == int.Parse(txtBarcode.Text)).FirstOrDefault();
            sal.Billtype = "مسودة";
            _IUW.Complete();
        }
    }
}
