using Domain.Dtos;
using Domain.Models;
using Easypos.Masters;
using Easypos.Payment;
using GUI.Helpers;
using GUIForms.Dtos;
using GUIForms.Forms.salesforms.Normal;
using GUIForms.helpers;
using GUIForms.models;
using Helpers.Dtos;
using javax.xml.transform;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UOW;

namespace Easypos.Salesforms.Cashier
{
    public partial class frmPOS : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        DGVProductHandler _DGVPH;
        Getallsales GAS;
        List<SaleViewModel> Res;
        public int Invid { get; set; }
        int top = 0;
        Usingnumber _ON;
        public frmPOS()
        {
            InitializeComponent();
            Loading();
        }
        public void Saveitemsales()
        {
            bool IUL = DC.ISUSElineproduction ?? false;
            if (IUL)
            {
                var items = _IUW.items.GetAll();
                var productitems = _IUW.productitems.GetAll();
                var query = from it in items
                            join pi in productitems on it.ID equals int.Parse(pi.itemid) into piGroup
                            from pi in piGroup.DefaultIfEmpty()
                            where pi?.Proid != null
                            select new
                            {
                                ID = it.ID,
                                ItemQty = it.Itemqty,
                                Quantity = pi.Quantity
                            };

                var result = query.ToList();
                List<itemsale> ISList = new List<itemsale>();

                foreach (var item in result)
                {
                    var IS = new itemsale
                    {
                        Date = DateTime.Now.ToString("dd-MM-yyyy"),
                        Quantity = int.Parse(item.Quantity),
                        Itemid = item.ID,
                        invoiceno = Invid,
                    };
                    ISList.Add(IS);     // إضافة العنصر للقائمة لو محتاج تستخدمها لاحقًا
                }
            }
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Billtype.SelectedIndex = 1;
            LoadAllCombos();
            Getcatlist();
            Getdatalist();
            _ON = new Usingnumber();
        }
        private void Getdatalist()
        {
            GAS = new Getallsales();
            Res = GAS.GetSaleslist();
            dgwInvoice.DataSource = Res.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.Type,
                x.Status
            }).ToList();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
        }
        void Getcatlist()
        {
            _IUW.categories.GetAll().ToList().ForEach(c =>
            {
                addCateButton(c.CategoryName, c.CategoryNo.ToString(), c.Color.ToString());
            });
        }
        private void addCateButton(string CateName, string CateNumber, string backcolor)
        {
            Button button = new Button();
            button.Top = top;
            button.Left = 0;
            button.Height = 80;
            button.Width = 100;
            ctpanel.AutoScroll = true;
            button.BackColor = Color.FromArgb(int.Parse(backcolor)); //Color.DarkOrange;
            button.Text = CateName;
            button.Margin = new Padding(0, 5, 0, 5);
            button.FlatStyle = FlatStyle.Flat;
            button.Tag = "ctTlp_" + CateNumber;
            button.Click += new EventHandler(category_Click);
            ctpanel.Controls.Add(button);
            top += button.Height + 5;
            addCatePanelBtn(CateNumber, backcolor);
        }
        private void addCatePanelBtn(string CateNumber, string Backcolor)
        {
            var products = _IUW.products.GetAll()
                                        .Where(p => p.CategoryNo == int.Parse(CateNumber))
                                        .ToList();

            int columnCount = 4;
            int rowCount = (int)Math.Ceiling((double)products.Count / columnCount);

            var tableLayoutPanel1 = new TableLayoutPanel
            {
                ColumnCount = columnCount,
                RowCount = rowCount,
                Height = panelList.Height,
                Width = panelList.Width,
                AutoScroll = true,
                Visible = false,
                Tag = "ctTlp_" + CateNumber
            };

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100f / columnCount)
                );
            }
            for (int i = 0; i < rowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, 70)
                );
            }

            int cl = 0, rw = 0;
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];

                Button button = new Button
                {
                    Tag = product.ProductNo.ToString(),
                    Text = product.Description + "\n" + product.UnitPrice + " ريال",
                    Dock = DockStyle.Top,
                    BackColor = Color.FromArgb(int.Parse(Backcolor)),
                    ForeColor = Color.White,
                    TabIndex = 25,
                    FlatStyle = FlatStyle.Flat,
                    Width = 100,
                    Height = 85
                };

                button.Click += productBtn_Click;

                tableLayoutPanel1.Controls.Add(button, cl, rw);
                cl++;
                if (cl == columnCount)
                {
                    cl = 0;
                    rw++;
                }
            }

            tableLayoutPanel1.Visible = true;
            panelList.Controls.Add(tableLayoutPanel1);
        }
        private void category_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            lblCate.Text = bt.Text;
            string btnTag = bt.Tag.ToString();
            var otherPanels = panelList.Controls.OfType<TableLayoutPanel>().Where(c => c.Tag != null && c.Tag.ToString().Contains("ctTlp_") && c.Tag.ToString() != btnTag).ToList();
            foreach (var cbtn in otherPanels)
            {
                cbtn.Visible = false;
            }
            var showBtns = panelList.Controls.OfType<TableLayoutPanel>().Where(c => c.Tag.ToString() == btnTag).ToList();
            foreach (var cbtn in showBtns)
            {
                cbtn.Visible = true;
            }
        }
        private void productBtn_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string code = bt.Tag.ToString();
            var PWU = _IUW.products.GetAll()
                .Where(p => p.ProductNo == int.Parse(code))
                .Join(_IUW.unittypes.GetAll(),
                      p => p.Unitid,
                      u => u.ID,
                      (p, u) => new { Product = p, UnitType = u })
                .FirstOrDefault();
            decimal Qty = 1;
            bool found = false;

            if (DGV.Rows.Count > 0)
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    if (DGV.Rows[i].Cells[0].Value.ToString() == code)
                    {
                        found = true;
                        Qty = decimal.Parse(DGV.Rows[i].Cells[4].Value.ToString()) + 1;
                        DGV.Rows[i].Cells[4].Value = Qty;
                        DGV.Rows[i].Cells[7].Value = Qty * PWU.Product.UnitPrice; // update total
                        break;
                    }
                }
            }
            // لو المنتج مش موجود في أي صف، ضيفه
            if (!found)
            {
                var Tot = Qty * PWU.Product.UnitPrice;
                DGV.Rows.Add(
                    PWU.Product.ProductNo,
                    PWU.Product.Description,
                    PWU.UnitType.UName,
                    PWU.UnitType.ID,
                    Qty,
                    PWU.Product.UnitPrice,
                    0, // discount
                    Tot // total
                );
            }
            UpdateDGVSummary();
        }
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        private void btnSettlepayment_Click(object sender, System.EventArgs e)
        {
            openpayment();
        }
        private void Editsales()
        {
            var sale = new sale
            {
                Invoiceno = Invid,
                Billtype = Billtype.Text,
                TDate = DateTime.Now.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Note = txtNote.Text,
                Purchaseorder = "",
                Proname = "",
                RN = ""
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
                SD.Subtotal = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Discount = 0;
                SD.Totafterdiscount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Total = (decimal?)(SD.ItemPrice * SD.Quantity);
                details.Add(SD);
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
                TDate = DateTime.Now.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Note = txtNote.Text,
                Purchaseorder = "",
                Proname = "",
                RN = ""
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
            }
            // استدعاء الدالة العامة
            SalesHelper.SaveSaleWithDetails(sale, details, _IUW);
            Invid = sale.Invoiceno;
            Saveitemsales();
        }
        public void openpayment()
        {
            Frmpayments PAY = new Frmpayments();
            PAY.Total = decimal.Parse(txtTotal.Text);
            PAY.clients.SelectedValue = clientID.SelectedValue;
            PAY.Formname = "Sales";
            PAY.ShowDialog();
        }
        public void Getsalesbill()
        {
            if (Invid > 0)
            {
                var sale = _IUW.sales.Get(Invid);
                if (sale != null)
                {
                    Billtype.Text = sale.Billtype;
                    //DTP.Value = DateTime.ParseExact(sale.TDate, "dd-MM-yyyy", null);
                    clientID.SelectedValue = sale.ThirdPartyID;
                    txtTBV.Text = sale.NonVatTotal.ToString();
                    txtDiscount.Text = sale.Discount.ToString();
                    txtTax.Text = sale.VatAmount.ToString();
                    txtTotal.Text = sale.TotalAmount;
                    txtNote.Text = sale.Note;
                    //Salesorder.Text = "";
                    //txtProjectname.Text = sale.Proname;
                    //txtRefranse.Text = sale.RN;
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
                GXL.Createxmldata(productLines, DC);

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
                    logger.Log(ex, "Pos");
                }
            }
        }
        public void Clearfieldes()
        {
            btnSettlepayment.Enabled = true;
            Billtype.Enabled = true;
            btnSettlepayment.Text = "حفظ الفاتورة";
            Billtype.SelectedIndex = 1;
            clientID.SelectedIndex = 1;
            DGV.Rows.Clear();
            dgwInvoice.Visible = false;
            Datecheacked.Checked = false;
            QRPic.Image = null;
            txtcustphone.Clear();
            lblCate.Text = string.Empty;
            btnRemoveItem.Visible = true;
            btnSettlepayment.Visible = true;
            txtNote.Clear();
            Invid = 0;
            UpdateDGVSummary();
        }
        private void RemoveItem()
        {
            int rowIndex = DGV.CurrentCell.RowIndex;
            var CR = Convert.ToInt32(DGV[4, rowIndex].Value.ToString());
            if (CR > 1)
            {
                var Price = Convert.ToDouble(DGV[5, rowIndex].Value.ToString());
                DGV[4, rowIndex].Value = Convert.ToInt32(DGV[4, rowIndex].Value.ToString()) - 1;
                var Q = DGV[4, rowIndex].Value.ToString();
                var Totac = Convert.ToDouble(Price) * Convert.ToInt32(Q);
                DGV[7, rowIndex].Value = Totac;
            }
            else
            {
                DGV.Rows.RemoveAt(rowIndex);
            }
            UpdateDGVSummary();
        }
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (DGV.Rows.Count <= 0)
            {
                return;
            }
            RemoveItem();
        }
        public void UpdateDGVSummary()
        {
            double sumSubtotal = 0;
            double sumDiscount = 0;

            foreach (DataGridViewRow row in DGV.Rows)
            {
                if (row.IsNewRow) continue;

                sumDiscount += Convert.ToDouble(row.Cells[6].Value);
                sumSubtotal += Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);
            }

            txtTBV.Text = sumSubtotal.ToString("N2");
            txtDiscount.Text = sumDiscount.ToString("N2");

            double totalAfterDiscount = sumSubtotal - sumDiscount;

            double taxRate = 0.15;
            double tax = DC.ISUsePhase2 ? taxRate * totalAfterDiscount : 0;
            txtTax.Text = tax.ToString("N2");

            double finalTotal = totalAfterDiscount + tax;
            txtTotal.Text = finalTotal.ToString("N2");
        }
        private void btnRePrint_Click(object sender, EventArgs e)
        {
            Getdatalist();
            dgwInvoice.Visible = true;
        }
        private void Btncustomers_Click(object sender, EventArgs e)
        {
            frmListThirdParty FTP = new frmListThirdParty();
            FTP.radioClient.Checked = true;
            FTP.Show();
        }
        private void btnNewTran_Click(object sender, EventArgs e)
        {
            Clearfieldes();
        }
        private async void dgwInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Dataid = dgwInvoice.CurrentRow.Cells[3].Value.ToString();
            var Datatye = dgwInvoice.CurrentRow.Cells[5].Value.ToString();
            var Datareg = dgwInvoice.CurrentRow.Cells[6].Value.ToString();
            if (dgwInvoice.Columns[e.ColumnIndex].Name == "Show")
            {
                Clearfieldes();
                Billtype.Text = Datatye;
                if (Datatye == "صدرت")
                {
                    btnSettlepayment.Enabled = false;
                    Billtype.Enabled = false;
                }
                Invid = int.Parse(Dataid);
                Getsalesbill();
            }
            else if (dgwInvoice.Columns[e.ColumnIndex].Name == "Delete")
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
                        Clearfieldes();
                        MessageBox.Show("تم حذف الفاتوره بنجاح", "حذف فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("لا يمكن حذف الفاتوره لانها صدرت", "حذف فاتوره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (dgwInvoice.Columns[e.ColumnIndex].Name == "Btnreg")
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
                        SD.Saleid = int.Parse(Dataid);
                        SD.Ublid = GUL.Id;
                        await SD.SendInvoiceAsync(GUL.Invoicehash, GUL.Uuid, GUL.Invoice, GUL.Path, GUL.QRCode);
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
        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.CurrentCell.ColumnIndex == 4 || DGV.CurrentCell.ColumnIndex == 5)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                UpdateDGVSummary();
            }
            if (DGV.CurrentCell.ColumnIndex == 7)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var Res = C1 * C2;
                DGV.CurrentRow.Cells[7].Value = Res;
                UpdateDGVSummary();
            }
            if (DGV.CurrentCell.ColumnIndex == 6)
            {
                var C1 = Convert.ToDouble(DGV.CurrentRow.Cells[4].Value.ToString());
                var C2 = Convert.ToDouble(DGV.CurrentRow.Cells[5].Value);
                var C3 = Convert.ToDouble(DGV.CurrentRow.Cells[6].Value);
                var Res = (C1 * C2) - C3;
                DGV.CurrentRow.Cells[7].Value = Res.ToString();
                UpdateDGVSummary();
            }
        }
    }
}
