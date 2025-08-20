using Aspose.Pdf;
using Domain.Models;
using Easypos.Payment;
using GUIForms.Dtos;
using GUIForms.helpers;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using UOW;

namespace Easypos.Purchases
{
    public partial class frmPurchases : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        DGVProductHandler _DGVPH;
        Usingnumber _NO;
        public int Invid { get; set; }
        public frmPurchases()
        {
            InitializeComponent();
            Loading();
            DGVH();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Billtype.SelectedIndex = 1;
            paymentMethod.SelectedIndex = 1;
            LoadAllCombos();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getsupplierdatalist(), "Name", "ID");
            Commondatasales.FillCombo(Cattype, GC.Getcategorydatalist(), "CategoryName", "CategoryNo");
            Commondatasales.FillCombo(unitTypes, GC.Getunittypedatalist(), "UName", "ID");
        }
        private void DGVH()
        {
            _DGVPH = new DGVProductHandler(
                            DGV,
                            Cattype,
                            unitTypes,
                            txtQuantity,
                            txtPrice,
                            txtTBV,
                            txtDiscount,
                            txtTax,
                            txtTotal,
                            ISVAT.Checked, // أو VI.ISVAT
                            15 // نسبة الضريبة
                        );
        }
        public void Clearfildes()
        {
            txtQuantity.Clear();
            txtPrice.Clear();
            LoadAllCombos();
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Btnadd_Click(object sender, EventArgs e)
        {
            _DGVPH.AddProductToDGV();
            Clearfildes();
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.CurrentCell.ColumnIndex == 4 || DGV.CurrentCell.ColumnIndex == 5)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                _DGVPH.Isvat = ISVAT.Checked;
                _DGVPH.UpdateDGVSummary2();
            }
            if (DGV.CurrentCell.ColumnIndex == 7)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                _DGVPH.Isvat = ISVAT.Checked;
                _DGVPH.UpdateDGVSummary2();
            }
        }
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (Invid > 0)
                {
                    if (Billtype.Text == "صدرت")
                    {
                        MessageBox.Show("لا يمكن الحذف لأن الفاتوره صدرت", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invid && x.ProductNo == int.Parse(DGV.CurrentRow.Cells[0].Value.ToString())).ToList().ForEach(x => _IUW.salesdetailes.Delete(x));
                        _IUW.Complete();
                        int rowIndex = DGV.CurrentCell.RowIndex;
                        DGV.Rows.RemoveAt(rowIndex);
                        _DGVPH.Isvat = ISVAT.Checked;
                        _DGVPH.UpdateDGVSummary2();
                    }
                }
                else
                {
                    int rowIndex = DGV.CurrentCell.RowIndex;
                    DGV.Rows.RemoveAt(rowIndex);
                    _DGVPH.Isvat = ISVAT.Checked;
                    _DGVPH.UpdateDGVSummary2();
                }
            }
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            var Cust = int.Parse(clientID.SelectedValue.ToString());
            if (Cust == 0)
            {
                MessageBox.Show("الرجاء اختيار عميل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (DGV.Rows.Count == 0)
            {
                MessageBox.Show("لايوجد طلبات متاحه", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                openpayment();
                Clearfildes();
            }
        }
        public void openpayment()
        {
            Frmpayments PAY = new Frmpayments();
            PAY.Formname = "Purchases";
            PAY.Total = decimal.Parse(txtTotal.Text);
            PAY.Purcid = clientID.SelectedValue.ToString();
            PAY.ShowDialog();
        }
        public void Btnsaved()
        {
            if (DGV.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد منتجات لإضافتها", "خطأ");
                return;
            }
            else if (clientID.SelectedValue == null)
            {
                MessageBox.Show("الرجاء اختيار عميل", "خطأ");
                return;
            }
            else
            {
                if (DGV.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد منتجات لإضافتها", "خطأ");
                    return;
                }
                else if (clientID.SelectedValue == null)
                {
                    MessageBox.Show("الرجاء اختيار مورد", "خطأ");
                    return;
                }
                else
                {
                    try
                    {
                        if (Invid > 0)
                        {
                            _IUW.purchasedetailes.GetAll().Where(x => x.InvoiceNo == Invid && x.ProductNo == int.Parse(DGV.CurrentRow.Cells[0].Value.ToString())).ToList().ForEach(x => _IUW.purchasedetailes.Delete(x));
                            _IUW.Complete();
                            Editpur();
                        }
                        else
                        {
                            Savepur();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = new ExceptionLogger(_IUW);
                        logger.Log(ex, "Purchases");
                    }
                }
            }
        }
        private void Editpur()
        {
            var pur = new purchase
            {
                Invoiceno = Invid,
                Billtype = Billtype.Text,
                TDate = DTP.Value.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Supplierbill = BStxt.Text
            };
            var details = new List<purchasedetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                purchasedetaile SD = new purchasedetaile();
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
            }
            // استدعاء الدالة العامة
            PurchaseHelper.EditPurchaseWithDetails(pur, details, _IUW);
            Invid = pur.Invoiceno;
        }
        private void Savepur()
        {
            var pur = new purchase
            {
                Billtype = Billtype.Text,
                TDate = DTP.Value.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Supplierbill = BStxt.Text
            };
            var details = new List<purchasedetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                purchasedetaile SD = new purchasedetaile();
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
            }
            // استدعاء الدالة العامة
            PurchaseHelper.SavePurchaseWithDetails(pur, details, _IUW);
            Invid = pur.Invoiceno;
        }
        private void ISVAT_CheckedChanged(object sender, EventArgs e)
        {
            _DGVPH.Isvat = ISVAT.Checked;
            _DGVPH.UpdateDGVSummary2();
        }
        private void Btnbilllist_Click(object sender, EventArgs e)
        {
            Purbilllist PL = new Purbilllist();
            PL.ShowDialog();
        }
        public void Getsalesbill()
        {
            if (Invid > 0)
            {
                var sale = _IUW.purchases.Get(Invid);
                if (sale != null)
                {
                    Billtype.Text = sale.Billtype;
                    DTP.Value = DateTime.ParseExact(sale.TDate, "dd-MM-yyyy", null);
                    clientID.SelectedValue = sale.ThirdPartyID;
                    txtTBV.Text = sale.NonVatTotal.ToString();
                    txtDiscount.Text = sale.Discount.ToString();
                    txtTax.Text = sale.VatAmount.ToString();
                    txtTotal.Text = sale.TotalAmount;
                    //var details = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invid).ToList();
                    var details = _IUW.purchasedetailes.GetAll()
                                                    .Where(sd => sd.InvoiceNo == Invid)
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

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
