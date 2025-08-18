using Centeralized;
using Domain.Models;
using Easypos.Masters;
using Easypos.Payment;
using Easypos.Salesforms;
using GUIForms.Dtos;
using GUIForms.helpers;
using iText.StyledXmlParser.Jsoup.Nodes;
using org.apache.xerces.impl.dv.xs;
using Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UOW;
using static System.Net.WebRequestMethods;

namespace Easypos.Pricing
{
    public partial class Frmprice : Form
    {
        Printinginvoice _PI;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        DGVProductHandler _DGVPH;
        Usingnumber _NO;
        public int Invid { get; set; }
        public Frmprice()
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
            _PI = new Printinginvoice();
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
            Commondatasales.FillCombo(cmbproducts, GC.Getproductdatalist(), "Description", "ProductNo");
            Commondatasales.FillCombo(unitTypes, GC.Getunittypedatalist(), "UName", "ID");
        }
        void Clearfildes()
        {
            txtQuantity.Clear();
            txtPrice.Clear();
        }
        private void Btnbilllist_Click(object sender, EventArgs e)
        {
            Frmpricelist FPL = new Frmpricelist();
            //FPL.Cashiername = Username;
            FPL.Show();
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
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

        }
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (Invid > 0)
                {
                    var data = _IUW.pricedetailes.GetAll().Where(x => x.Priceid == Invid).FirstOrDefault();
                    _IUW.pricedetailes.Delbyid(data.ID);
                    _IUW.Complete();
                }
                int rowIndex = DGV.CurrentCell.RowIndex;
                DGV.Rows.RemoveAt(rowIndex);
                _DGVPH.UpdateDGVSummary();
                LoadAllCombos();
                Clearfildes();
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
            LoadAllCombos();
            Clearfildes();
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Btnsave.Text == "حفظ")
                {
                    Saveprice();
                }
                else
                {
                    Editprice();
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Prices");
            }
            LoadAllCombos();
            Clearfildes();
        }
        private void Saveprice()
        {
            var price = new price
            {
                Datefrom = DTF.Value.ToString("dd-MM-yyyy"),
                Dateto = DTT.Value.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = double.Parse(txtTotal.Text),
            };
            var details = new List<pricedetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                pricedetaile SD = new pricedetaile();
                SD.ProductNo = int.Parse(DGV.Rows[i].Cells[0].Value.ToString());
                SD.TDDesc = DGV.Rows[i].Cells[1].Value.ToString();
                SD.Unitid = int.Parse(DGV.Rows[i].Cells[3].Value.ToString());
                SD.Quantity = double.Parse(DGV.Rows[i].Cells[4].Value.ToString());
                SD.ItemPrice = double.Parse(DGV.Rows[i].Cells[5].Value.ToString());
                SD.Subtotal = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Discount = 0;
                SD.Totafterdiscount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Total = (double?)(SD.ItemPrice * SD.Quantity);
                details.Add(SD);
            }
            // استدعاء الدالة العامة
            SalesHelper.SavepriceWithDetails(price, details, _IUW);
            Invid = price.ID;
        }
        private void Editprice()
        {
            var price = new price
            {
                ID = Invid,
                Datefrom = DTF.Value.ToString("dd-MM-yyyy"),
                Dateto = DTT.Value.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = double.Parse(txtTotal.Text),
            };
            var details = new List<pricedetaile>();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                pricedetaile SD = new pricedetaile();
                SD.ProductNo = int.Parse(DGV.Rows[i].Cells[0].Value.ToString());
                SD.TDDesc = DGV.Rows[i].Cells[1].Value.ToString();
                SD.Unitid = int.Parse(DGV.Rows[i].Cells[3].Value.ToString());
                SD.Quantity = double.Parse(DGV.Rows[i].Cells[4].Value.ToString());
                SD.ItemPrice = double.Parse(DGV.Rows[i].Cells[5].Value.ToString());
                SD.Subtotal = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Discount = 0;
                SD.Totafterdiscount = double.Parse(DGV.Rows[i].Cells[6].Value.ToString());
                SD.Total = (double?)(SD.ItemPrice * SD.Quantity);
                details.Add(SD);
            }
            // استدعاء الدالة العامة
            SalesHelper.EditPriceWithDetails(price, details, _IUW);
            Invid = price.ID;
        }
        public void Getsalesbill()
        {
            if (Invid > 0)
            {
                var sale = _IUW.prices.Get(Invid);
                if (sale != null)
                {
                    DTF.Value = DateTime.ParseExact(sale.Datefrom, "dd-MM-yyyy", null);
                    DTT.Value = DateTime.ParseExact(sale.Dateto, "dd-MM-yyyy", null);
                    clientID.SelectedValue = sale.ThirdPartyID;
                    txtTBV.Text = sale.NonVatTotal.ToString();
                    txtDiscount.Text = sale.Discount.ToString();
                    txtTax.Text = sale.VatAmount.ToString();
                    var details = _IUW.pricedetailes.GetAll()
                                                    .Where(sd => sd.Priceid == Invid)
                                                    .Join(
                                                        _IUW.unittypes.GetAll(),
                                                        sd => sd.Unitid,
                                                        ut => ut.ID,
                                                        (sd, ut) => new
                                                        {
                                                            sd.ID,
                                                            sd.ProductNo,
                                                            sd.TDDesc,
                                                            ut.UName,
                                                            sd.Unitid,
                                                            sd.Quantity,
                                                            sd.ItemPrice,
                                                            sd.Discount,
                                                            sd.Total
                                                        }
                                                    ).OrderByDescending(x => x.ID)
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
            _DGVPH.UpdateDGVSummary();
        }
        private void Changetosales()
        {
            var sale = new sale
            {
                Billtype = "مسوده",
                TDate = DateTime.Now.ToString("dd-MM-yyyy"),
                ThirdPartyID = int.Parse(clientID.SelectedValue.ToString()),
                TTime = DateTime.Now.ToString("hh:mm:ss"),
                NonVatTotal = double.Parse(txtTBV.Text),
                Discount = double.Parse(txtDiscount.Text),
                VatAmount = double.Parse(txtTax.Text),
                TotalAmount = txtTotal.Text,
                Note = "",
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
            SalesHelper.SaveSaleWithDetails(sale, details, _IUW);
            //Invid = sale.Invoiceno;
            _IUW.prices.Delbyid(Invid);
            _IUW.Complete();
            var data = _IUW.pricedetailes.GetAll().Where(x => x.Priceid == Invid).ToList();
            foreach (var item in data)
            {
                _IUW.pricedetailes.Delbyid(item.ID);
                _IUW.Complete();
            }
        }
        private void Btnchange_Click(object sender, EventArgs e)
        {
            Changetosales();
            LoadAllCombos();
            Clearfildes();
            DGV.Rows.Clear();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
