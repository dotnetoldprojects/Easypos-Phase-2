using Domain;
using Domain.Models;
using Easypos.Masters.Subforms;
using GUIForms.Dtos;
using GUIForms.Forms.Masters;
using GUIForms.helpers;
using GUIForms.models;
using Reporting;
using Reporting.others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using UOW;

namespace Easypos.Masters
{
    public partial class frmproductitemlist : Form
    {
        public int UId { get; set; }
        company DC;
        Getcentralaizes GC;
        item It;
        IUnitofwork _IUW;
        private List<ItemsViewModel> _EVM;
        Usingnumber _NO;
        List<SalesReportItem> SO;
        public frmproductitemlist()
        {
            InitializeComponent();  
            Loading();
        }
        public void Clearitems()
        {
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnaddedit.Text = "Add";
            }
            else
            {
                Btnaddedit.Text = "اضافة";
            }
            txtItemname.Clear();
            txtUnitPrice.Clear();
            //txtStocksOnHand.Clear();
            textBox1.Clear();
            textBox2.Clear();
            txtSearch.Clear();
            //DGV.DataSource = null;
            //DGV.Rows.Clear();
            Btnaddedit.Enabled = true;
            //txtStocksOnHand.Enabled = true;
            txtRemining.Clear();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            It = new item();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            GetItemsData();
        }
        public void GetItemsData()
        {
            DGV.DataSource = GC.GetItemsdatalist();
        }
        private void Btnunit_Click(object sender, EventArgs e)
        {
            Frmlistunit flu = new Frmlistunit();
            flu.UnitSelected += (id, name) =>
            {
                // هنا استقبلنا البيانات من الفورم التاني
                UId = id;
                textBox1.Text = name;
            };
            flu.ShowDialog();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            Clearitems();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                //Btnaddedit.Enabled = false;
                //txtStocksOnHand.Enabled = false;
                It.ID = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                txtItemname.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtUnitPrice.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                //txtStocksOnHand.Text = DGV.CurrentRow.Cells[3].Value.ToString();
                textBox1.Text = DGV.CurrentRow.Cells[6].Value.ToString();
                textBox2.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                UId = int.Parse(DGV.CurrentRow.Cells[5].Value.ToString());
                var totalQuantity = _IUW.itemsales.GetAll()
                                                  .Where(x => x.Itemid == It.ID)
                                                  .Sum(x => x.Quantity);
                txtRemining.Text = (int.Parse(textBox2.Text) - totalQuantity).ToString();
            }
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemname.Text) ||
                       string.IsNullOrEmpty(txtUnitPrice.Text) ||
                       //string.IsNullOrEmpty(txtStocksOnHand.Text) ||
                       string.IsNullOrEmpty(textBox1.Text) ||
                       string.IsNullOrEmpty(textBox2.Text))
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert all fildes", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("برجاء ادخال جميع الحقول", "خطأ");
                    return;
                }
            }
            else
            {
                try
                {
                    It.UID = UId;
                    if (It.ID != null)
                    {
                        It.Itemname = txtItemname.Text;
                        It.Itemprice = Convert.ToDouble(txtUnitPrice.Text);
                        //It.Itemqty = double.Parse(txtStocksOnHand.Text);
                        It.OpeningBalance = int.Parse(textBox2.Text);
                        _IUW.items.Update(It);
                    }
                    else
                    {
                        It.Itemname = txtItemname.Text;
                        It.Itemprice = Convert.ToDouble(txtUnitPrice.Text);
                        //It.Itemqty = double.Parse(txtStocksOnHand.Text);
                        It.OpeningBalance = int.Parse(textBox2.Text);
                        _IUW.items.Insert(It);
                    }
                    _IUW.Complete();
                }
                catch (Exception ex)
                {
                    var logger = new ExceptionLogger(_IUW);
                    logger.Log(ex, "Product item list");
                }
            }
            Clearitems();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _IUW.items.Delbyid(It.ID);
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Product item list");
            }
            Clearitems();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Itemsales FIS = new Itemsales();
            FIS.Show();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                DC = (company)LanguageHelper.ApplyLanguage(this);
                var searchText = txtSearch.Text?.Trim().ToLower();
                if (!string.IsNullOrEmpty(searchText))
                {
                    var Serch = GC.GetItemsdatalist().Where(x => x.Itemname.Contains(searchText)).ToList();
                    Serch = Serch
                        .Where(x => !string.IsNullOrEmpty(x.Itemname)
                                 && x.Itemname.Contains(searchText))
                        .ToList();
                    DGV.DataSource = Serch.Select(p => new
                    {
                        ID = p.ID,
                        Itemname = p.Itemname,
                        Itemprice = p.Itemprice,
                        Itemqty = p.Itemqty,
                        OpeningBalance = p.OpeningBalance,
                        UnitName = p.UnitName,
                        Unitid = p.Unitid
                    }).ToList();
                }

            }
            else
            {
                Loading();
            }
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
        private void txtStocksOnHand_TextChanged(object sender, EventArgs e)
        {
            //textBox2.Text = txtStocksOnHand.Text;
        }
        void Getdata(int IID)
        {
            SO = new List<SalesReportItem>();
            var items = _IUW.items.GetAll().ToList();
            var purchases = _IUW.purchases.GetAll().ToList();
            var purchaseDetails = _IUW.purchasedetailes.GetAll().ToList();
            var itemsales = _IUW.itemsales.GetAll().ToList();


            var result = from item in items
                         join pd in purchaseDetails
                         on item.ID equals pd.ProductNo into pdGroup
                         from pd in pdGroup.DefaultIfEmpty()

                             // حماية من null قبل الـ join الثاني
                         let invoiceNo = pd != null ? pd.InvoiceNo : 0

                         join p in purchases
                         on invoiceNo equals p.Invoiceno into pGroup
                         from p in pGroup.DefaultIfEmpty()

                         where pd != null && pd.ProductNo == IID
                         select new
                         {
                             Item = item.ID,
                             Itemname = item.Itemname,
                             Date = p.TDate,
                             Quantity = pd.Quantity,
                             Invoiceno = pd.InvoiceNo
                         };
            foreach (var item in result)
            {
                SO.Add(new SalesReportItem
                {
                    ID = item.Item,
                    Itemname = item.Itemname,
                    Date = item.Date.ToString(),
                    Itemqty = 0,
                    Quantity = (int)item.Quantity,
                    Remining = 0,
                    Invoiceno = item.Invoiceno.ToString(),
                    Type = "فاتورة مشتريات"
                });
            }

            var result2 = from its in itemsales
                          join it in items
                          on its.Itemid equals it.ID into joined
                          from it in joined.DefaultIfEmpty()
                          where its.Itemid == IID
                          select new
                          {
                              it.ID,
                              Itemname = it?.Itemname ?? "",
                              its.Date,
                              Itemqty = it?.Itemqty ?? 0,
                              its.Quantity,
                              its.invoiceno
                          };

            foreach (var item in result2)
            {
                SO.Add(new SalesReportItem
                {
                    ID = item.ID,
                    Itemname = item.Itemname,
                    Date = item.Date.ToString(),
                    Itemqty = item.Itemqty,
                    Quantity =item.Quantity,
                    Remining = item.Itemqty - item.Quantity,
                    Invoiceno = item.invoiceno.ToString(),
                    Type = "فاتورة مبيعات"
                });
            }
            
            var itemadjustments = _IUW.invtransactions.GetAll().ToList();
            var result3 = from adj in itemadjustments
                          join it in items
                          on adj.Itemid equals it.ID into joined
                          from it in joined.DefaultIfEmpty()
                          where adj.Itemid == IID
                          select new
                          {
                              it.ID,
                              Itemname = it?.Itemname ?? "",
                              adj.Date,
                              Itemqty = it?.Itemqty ?? 0,
                              adj.Quantity,
                              Remining = (it?.Itemqty ?? 0) - adj.Quantity,
                              adj.type
                          };
            foreach (var item in result3)
            {
                SO.Add(new SalesReportItem
                {
                    ID = item.ID,
                    Itemname = item.Itemname,
                    Date = item.Date.ToString(),
                    Itemqty = item.Itemqty,
                    Quantity = item.Quantity,
                    Remining = item.Itemqty + item.Quantity, // لو الإضافة موجبة
                    Invoiceno = "--",
                    Type = item.type,
                });
            }

            SO = SO.OrderBy(x => DateTime.Parse(x.Date)).ToList();
        }
        private void Btnrep_Click(object sender, EventArgs e)
        {
            if (It.ID != 0)
            {
                Getdata(It.ID);

                // إنشاء التقرير
                Frmreporting FR = new Frmreporting();
                Dataset Ds = new Dataset();
                Stokreport SR = new Stokreport();
                var dt = Ds.Tables["Stokdata"];

                // إضافة الرصيد الافتتاحي كأول سطر
                int runningBalance = int.Parse(textBox2.Text);

                var openingRow = dt.NewRow();
                openingRow["Proid"] = "--";
                openingRow["Description"] = "الرصيد الافتتاحي";
                openingRow["Date"] = "--";
                openingRow["Billnumber"] = "--";
                openingRow["Credit"] = runningBalance > 0 ? runningBalance : 0;
                openingRow["Dept"] = runningBalance < 0 ? runningBalance : 0;
                openingRow["Balance"] = runningBalance;
                dt.Rows.Add(openingRow);

                var Credit = 0.00;
                var Dept = 0.00;
                // إضافة باقي الحركات مع حساب الرصيد
                foreach (var item in SO.ToList())
                {
                    var row = dt.NewRow();
                    row["Proid"] = item.ID;
                    switch (item.Type)
                    {
                        case "فاتورة مشتريات":
                            row["Description"] = "فاتورة مشتريات";
                            Credit += item.Quantity;
                            break;
                        case "مرتجع مبيعات":
                            row["Description"] = "مرتجع مبيعات";
                            Credit += item.Quantity;
                            break;
                        case "فاتورة مبيعات":
                            row["Description"] = "فاتورة مبيعات";
                            Dept -= item.Quantity;
                            break;
                        case "مرتجع مشتريات":
                            row["Description"] = "مرتجع مشتريات";
                            Dept -= item.Quantity;
                            break;
                        case "تعديل مخزون زائد":
                            row["Description"] = "تعديل مخزون زائد";
                            Credit += item.Quantity;
                            break;
                        case "تعديل مخزون ناقص":
                            row["Description"] = "تعديل مخزون ناقص";
                            Dept -= item.Quantity;
                            break;
                    }
                    row["Credit"] = Credit;
                    row["Dept"] = Dept;
                    row["Date"] = item.Date.ToString();
                    row["Billnumber"] = item.Invoiceno;

                    // تعديل الرصيد حسب نوع الحركة
                    switch (item.Type)
                    {
                        case "فاتورة مشتريات":
                        case "تعديل مخزون زائد":
                        case "مرتجع مبيعات":
                            runningBalance += item.Quantity;
                            break;

                        case "فاتورة مبيعات":
                        case "تعديل مخزون ناقص":
                        case "مرتجع مشتريات":
                            runningBalance -= item.Quantity;
                            break;

                            //case "Inventory":
                            //    runningBalance += item.Quantity; // لو تعديل مباشر
                            //    break;
                    }

                    row["Balance"] = runningBalance;
                    dt.Rows.Add(row);
                    Credit = 0.00;
                    Dept = 0.00;
                }

                // إعداد التقرير
                SR.SetDataSource(Ds);
                SR.SetParameterValue("TOF", "تقرير مخزون : " + txtItemname.Text);
                SR.SetParameterValue("Taxnum", DC.Taxnumber);
                SR.SetParameterValue("Proname", DC.CRN);
                SR.SetParameterValue("English_Shop_name", DC.ENName);
                SR.SetParameterValue("CompanyName", DC.Name);
                FR.CRV.ReportSource = SR;
                FR.Show();
            }
            else
            {
                MessageBox.Show("خطأ","برجاء ادخال المنتج",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Listofstok LOS = new Listofstok();
            LOS.itemid = It.ID;
            //LOS.txtStocksOnHand.Text = txtRemining.Text;
            LOS.textBox2.Text = textBox2.Text;
            LOS.ShowDialog();
        }
    }
}
