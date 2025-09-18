using Aspose.Pdf.Operators;
using Domain.Dtos;
using Domain.Models;
using Easypos.Masters;
using Easypos.Payment;
using Easypos.Salesforms;
using GUI.Helpers;
using GUIForms.Dtos;
using GUIForms.helpers;
using Helpers.Dtos;
using java.security;
using Microsoft.VisualBasic;
using org.apache.xml.resolver.helpers;
using Org.BouncyCastle.Utilities.Collections;
using sun.misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using UOW;

namespace GUIForms.Forms.salesforms.Normal
{
    public partial class frmMSalesBill : Form
    {
        Printinginvoice _PI;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        DGVProductHandler _DGVPH;
        transaction trn;
        Usingnumber _NO;
        public int Invid { get; set; }
        public frmMSalesBill()
        {
            InitializeComponent();
            Loading();
        }
        public void Saveitemsales(int SIN)
        {
            bool IUL = DC.ISUSElineproduction ?? false;
            if (IUL)
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    var result = GC.GetProductItems(DGV.Rows[i].Cells[0].Value.ToString()).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            dynamic dynItem = item;
                            itemsale IS = new itemsale();
                            IS.Date = DateTime.Now.ToString("dd-MM-yyyy");
                            IS.Quantity = int.Parse(dynItem.Quantity.ToString());
                            IS.Itemid = dynItem.ID;
                            IS.invoiceno = SIN;
                            _IUW.itemsales.Insert(IS);
                            _IUW.Complete();
                        }
                    }
                }
            }
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            _PI = new Printinginvoice();
            trn = new transaction();
            Billtype.SelectedIndex = 1;
            LoadAllCombos();
            DGVH();
        }
        private void DGVH()
        {
            _DGVPH = new DGVProductHandler(
                            DGV,
                            cmbproducts,
                            unitTypes,
                            txtQuantity,
                            txtPrice,
                            txtTBV,
                            txtDiscount,
                            txtTax,
                            txtTotal,
                            DC.ISUsePhase2, // أو VI.ISVAT
                            15 // نسبة الضريبة
                        );
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
            clientID.SelectedIndex = 1;
            Commondatasales.FillCombo(cmbproducts, GC.Getproductdatalist(), "Description", "ProductNo");
            Commondatasales.FillCombo(unitTypes, GC.Getunittypedatalist(), "UName", "ID");
        }
        void Clearfildes()
        {
            txtNote.Clear();
            txtBarcode.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            QRPic.Image = null;
            txtRefranse.Clear();
            txtProjectname.Clear();
            Salesorder.Clear();
        }
        public void ClearAll()
        {
            DGV.Enabled = true;
            DGV.ReadOnly = true;
            txtDiscount.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtDiscount.Enabled = false;
            txtTotal.Enabled = false;
            Clearfildes();
            clientID.SelectedIndex = 1;
            cmbproducts.SelectedIndex = 0;
            unitTypes.SelectedIndex = 0;
            DGV.Rows.Clear();
            txtTBV.Text = "0";
            txtTax.Text = "0";
            txtTotal.Text = "0";
            txtDiscount.Text = "0";
            Invid = 0;
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Btncustomers_Click(object sender, EventArgs e)
        {
            frmListThirdParty FTP = new frmListThirdParty();
            FTP.radioClient.Checked = true;
            FTP.Show();
        }
        private void cmbproducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbproducts.SelectedValue != null || cmbproducts.SelectedIndex != 0)
            {
                var UD = this.GC.Getproductdatalist().Where(x => x.ProductNo == int.Parse(cmbproducts.SelectedValue.ToString())).FirstOrDefault();
                if (UD != null)
                {
                    unitTypes.SelectedValue = UD.Unitid;
                    txtPrice.Text = UD.UnitPrice.ToString();
                }
            }
        }
        private void Btnadd_Click(object sender, EventArgs e)
        {
            _DGVPH.AddProductToDGV();
            Clearfildes();
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _DGVPH.AddProductToDGV();
                Clearfildes();
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
                        MessageBox.Show("لا يمكن الحذف لأن الفاتوره صدرت","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invid && x.ProductNo == int.Parse(DGV.CurrentRow.Cells[0].Value.ToString())).ToList().ForEach(x => _IUW.salesdetailes.Delete(x));
                        _IUW.Complete();
                        int rowIndex = DGV.CurrentCell.RowIndex;
                        DGV.Rows.RemoveAt(rowIndex);
                        DGVH();
                    }
                }
                else
                {
                    int rowIndex = DGV.CurrentCell.RowIndex;
                    DGV.Rows.RemoveAt(rowIndex);
                    DGVH();
                }
                _DGVPH.UpdateDGVSummary();
            }
        }
        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.CurrentCell.ColumnIndex == 4 || DGV.CurrentCell.ColumnIndex == 5)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                _DGVPH.UpdateDGVSummary();
            }
            if (DGV.CurrentCell.ColumnIndex == 7)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                _DGVPH.UpdateDGVSummary();
            }
            if (DGV.CurrentCell.ColumnIndex == 6)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var C3 = Convert.ToDouble(DGV.CurrentRow.Cells[6].Value);
                var Res = (C1 * C2) - C3;
                DGV.CurrentRow.Cells[7].Value = Res;
                _DGVPH.UpdateDGVSummary();
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
            }
        }
        private void Editsales()
        {
            var sale = new sale
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
                Note = txtNote.Text,
                Purchaseorder = Salesorder.Text,
                Proname = txtProjectname.Text,
                RN = txtRefranse.Text
            };
            var details = new List<salesdetaile>();
            var Data = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == sale.Invoiceno).ToList();
            foreach (var item in Data)
            {
                _IUW.salesdetailes.Delbyid(Convert.ToInt32(item.InvoiceNo));
                _IUW.Complete();
            }
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                salesdetaile SD = new salesdetaile();
                SD.ProductNo = int.Parse(DGV.Rows[i].Cells[0].Value.ToString());
                SD.TDDesc = DGV.Rows[i].Cells[1].Value.ToString();
                SD.Unitid = int.Parse(DGV.Rows[i].Cells[3].Value.ToString());
                SD.Quantity = double.Parse(DGV.Rows[i].Cells[4].Value.ToString());
                SD.ItemPrice = double.Parse(DGV.Rows[i].Cells[5].Value.ToString());
                SD.Subtotal = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Discount = 0;
                SD.Totafterdiscount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Total = (decimal?)(SD.ItemPrice * SD.Quantity);
                details.Add(SD);
                //_IUW.invtransactions.Insert(new invtransaction
                //{
                //    Proid = int.Parse(SD.ProductNo.ToString()),
                //    Quantity = int.Parse(SD.Quantity.ToString()),
                //    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                //    Credit = 0,
                //    Dipt = decimal.Parse(SD.ItemPrice.ToString()),
                //    type = "Sales",
                //    transid = SD.InvoiceNo
                //});
                //_IUW.Complete();
            }
            // استدعاء الدالة العامة
            SalesHelper.EditSaleWithDetails(sale, details, _IUW);
            Invid = sale.Invoiceno;
        }
        private void Savesales()
        {
            var sale = new sale
            {
                Billtype = Billtype.Text,
                TDate = DTP.Value.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Note = txtNote.Text,
                Purchaseorder = Salesorder.Text,
                Proname = txtProjectname.Text,
                RN = txtRefranse.Text
            };
            var details = new List<salesdetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                salesdetaile SD = new salesdetaile();
                SD.ProductNo = int.Parse(DGV.Rows[i].Cells[0].Value.ToString());
                SD.TDDesc = DGV.Rows[i].Cells[1].Value.ToString();
                SD.Unitid = int.Parse(DGV.Rows[i].Cells[3].Value.ToString());
                SD.Quantity = double.Parse(DGV.Rows[i].Cells[4].Value.ToString());
                SD.ItemPrice = double.Parse(DGV.Rows[i].Cells[5].Value.ToString());
                SD.Discount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Subtotal = SD.Quantity * SD.ItemPrice;
                SD.Totafterdiscount = SD.Subtotal - SD.Discount;
                SD.Total = decimal.Parse(DGV.Rows[i].Cells[7].Value.ToString());
                details.Add(SD);
                //var product = _IUW.products.GetAll().Where(p => p.ProductNo == SD.ProductNo).FirstOrDefault();
                //product.StocksOnHand -= int.Parse(SD.Quantity.ToString());
                //_IUW.products.Update(product);
                var SOH = _IUW.stok_transactions.GetAll().Where(s => s.Proid == SD.ProductNo).FirstOrDefault();
                if (SOH != null)
                {
                    SOH.Quantity -= int.Parse(SD.Quantity.ToString());
                    _IUW.stok_transactions.Update(SOH);
                    _IUW.Complete();
                }
            }
            // استدعاء الدالة العامة
            SalesHelper.SaveSaleWithDetails(sale, details, _IUW);
            Saveitemsales(sale.Invoiceno);
            Invid = sale.Invoiceno;
            //Saveitemsales();
            //Clearfildes();
        }
        public void openpayment()
        {
            Frmpayments PAY = new Frmpayments();
            PAY.Total = decimal.Parse(txtTotal.Text);
            PAY.Cust = clientID.SelectedValue.ToString();
            PAY.Formname = "Sales";
            PAY.ShowDialog();
        }
        private void Btnbilllist_Click(object sender, EventArgs e)
        {
            Frmbilllist frmbilllist = new Frmbilllist();
            frmbilllist.ShowDialog();
        }
        public void Getsalesbill()
        {
            if (Invid > 0)
            {
                var sale = _IUW.sales.Get(Invid);
                if (sale != null)
                {
                    Billtype.Text = sale.Billtype;
                    DTP.Value = DateTime.ParseExact(sale.TDate, "dd-MM-yyyy", null);
                    clientID.SelectedValue = sale.ThirdPartyID;
                    txtTBV.Text = sale.NonVatTotal.ToString();
                    txtDiscount.Text = sale.Discount.ToString();
                    txtTax.Text = sale.VatAmount.ToString();
                    txtTotal.Text = sale.TotalAmount;
                    txtNote.Text = sale.Note;
                    Salesorder.Text = sale.Purchaseorder;
                    txtProjectname.Text = sale.Proname;
                    txtRefranse.Text = sale.RN;
                    //var details = _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invid).ToList();
                    var details = _IUW.salesdetailes.GetAll()
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
        public async void Generatexml()
        {
            if (Billtype.Text == "صدرت")
            {
                List<ProductLine> productLines = new List<ProductLine>();
                Geneatexml GXL = new Geneatexml();
                GXL.Custid = int.Parse(clientID.SelectedValue.ToString());
                GXL.Invtitle = "Inv-" + Invid.ToString();
                const string unitCode = "PCE";
                const decimal taxPercent = 15m;

                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    productLines.Add(new ProductLine
                    {
                        Id = DGV.Rows[i].Cells[0].Value.ToString(),
                        Name = DGV.Rows[i].Cells[1].Value.ToString(),
                        Quantity = int.Parse(DGV.Rows[i].Cells[4].Value.ToString()),
                        UnitCode = unitCode,
                        UnitPrice = decimal.Parse(DGV.Rows[i].Cells[5].Value.ToString()),
                        Discount = decimal.Parse(DGV.Rows[i].Cells[6].Value.ToString()),
                        TaxPercent = taxPercent
                    });
                }
                string InputPath = @"Data/Invoice.xml";
                var data = DC;
                var RBD = Convert.ToDecimal(txtDiscount.Text);
                GXL.Createxmldata(productLines, DC, RB2.Checked, RBD);

                var xmlContent = File.ReadAllText(InputPath);
                var Doc = GC.LoadInvoiceFromString(xmlContent);
                Signdtos Sdtos = new Signdtos();
                Sdtos.Saleid = Invid;
                await Sdtos.Sign(Doc, $"Inv{Invid}");
            }
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
                    MessageBox.Show("الرجاء اختيار عميل", "خطأ");
                    return;
                }
                else
                {
                    try
                    {
                        if (Invid > 0)
                        {
                            _IUW.salesdetailes.GetAll().Where(x => x.InvoiceNo == Invid && x.ProductNo == int.Parse(DGV.CurrentRow.Cells[0].Value.ToString())).ToList().ForEach(x => _IUW.salesdetailes.Delete(x));
                            _IUW.Complete();
                            Editsales();
                        }
                        else
                        {
                            Savesales();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = new ExceptionLogger(_IUW);
                        logger.Log(ex, "Sales");
                    }
                }
            }
        }
        private void Btnsaveandprint_Click(object sender, EventArgs e)
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
                if (Invid != 0)
                {
                    _PI.Invoice(Invid);
                    ClearAll();
                }
            }
        }
        private void RB2_CheckedChanged(object sender, EventArgs e)
        {
            DGV.Enabled = false;
            DGV.ReadOnly = true;
            txtDiscount.ReadOnly = false;
            txtTotal.ReadOnly = false;
            txtDiscount.Enabled = true;
            txtTotal.Enabled = true;
            if (DGV.Rows.Count > 0)
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    DGV.Rows[i].Cells[6].Value = 0;
                    var C1 = Convert.ToDouble(DGV.Rows[i].Cells[4].Value.ToString());
                    var C2 = Convert.ToDouble(DGV.Rows[i].Cells[5].Value);
                    var C3 = Convert.ToDouble(DGV.Rows[i].Cells[6].Value);
                    var Res = (C1 * C2) - C3;
                    DGV.Rows[i].Cells[7].Value = Res;
                    _DGVPH.UpdateDGVSummary();
                }
            }
        }
        private void RB1_CheckedChanged(object sender, EventArgs e)
        {
            DGV.Enabled = true;
            DGV.ReadOnly = false;
            txtDiscount.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtDiscount.Enabled = false;
            txtTotal.Enabled = false;
        }
        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender,e);
            if (!string.IsNullOrEmpty(txtDiscount.Text))
            {
                Changedisc();
            }
        }
        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender,e);
            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                Changetot();
            }
        }
        void Changetot()
        {
            var Price = Convert.ToDouble(txtTotal.Text);
            var TBV = Price / 1.15;
            var GTBV = Math.Round(Convert.ToDecimal(TBV), 2).ToString();
            if (DC.ISUsePhase2)
            {
                var Disc = Convert.ToDouble(txtTBV.Text) - TBV;
                var GDisc = Math.Round(Convert.ToDouble(Disc), 2).ToString();
                txtDiscount.Text = Convert.ToString(GDisc);
            }
            else
            {
                var TB = Convert.ToDouble(txtTBV.Text);
                var Tot = Convert.ToDouble(txtTotal.Text);
                var Res = TB - Tot;
                txtDiscount.Text = Res.ToString();
            }
        }
        void Changedisc()
        {
            var ST = Convert.ToDouble(txtTBV.Text) - Convert.ToDouble(txtDiscount.Text);
            if (DC.ISUsePhase2)
            {
                var Tax = Convert.ToDouble(ST) * 0.15;
                var GTax = Math.Round(Convert.ToDouble(Tax), 2).ToString();
                txtTax.Text = GTax.ToString();

                var Price = ST + Tax;
                var GPrice = Math.Round(Convert.ToDouble(Price), 2).ToString();
                txtTotal.Text = GPrice.ToString();
            }
            else
            {
                txtTotal.Text = ST.ToString();
            }
        }
    }
}
