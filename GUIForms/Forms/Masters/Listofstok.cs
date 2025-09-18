using CrystalDecisions.ReportAppServer;
using Domain;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.models;
using javax.xml.transform;
using net.sf.saxon;
using Reporting;
using Reporting.others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

namespace GUIForms.Forms.Masters
{
    public partial class Listofstok : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Usingnumber _NO;
        public int Stid { get; set; }
        public Listofstok()
        {
            InitializeComponent();
            Loading();
        }
        private void ListofstokLoad()
        {
            var PC = _IUW.products.GetAll().ToList();
            var SC = _IUW.stok_transactions.GetAll().ToList();
            var uc = _IUW.unittypes.GetAll().ToList();
            var CC = _IUW.categories.GetAll().ToList();

            DC = (company)GC.Getcompanydatalist();
            var query = from p in PC
                        join st in SC on p.ProductNo equals st.Proid into stJoin
                        from st in stJoin.DefaultIfEmpty() // LEFT JOIN
                        join u in uc on p.Unitid equals u.ID into uJoin
                        from u in uJoin.DefaultIfEmpty()
                        join c in CC on p.CategoryNo equals c.CategoryNo into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        select new
                        {
                            p.ProductNo,
                            p.ProductCode,
                            p.Description,
                            p.Barcode,
                            Quantity = (st != null ? st.Quantity : 0) + (p.StocksOnHand ?? 0),
                            UName = u != null ? u.UName : "",
                            CategoryName = c != null ? c.CategoryName : "",
                            Note = st?.Note != null ? st.Note : "",
                            Sid = st?.Id ?? 0
                        };
            var bindingSource = new BindingSource();
            bindingSource.DataSource = query;
            DGV.DataSource = bindingSource;
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            ListofstokLoad();
        }
        private void Cleardata()
        {
            textBox2.Clear();
            lblProductNo.Text = "";
            txtBarcode.Clear();
            txtStocksOnHand.Text = "0";
            txtCategory.Clear();
            txtDescription.Clear();
            txtProductCode.Clear();
            textBox1.Clear();
            Btnrep.Enabled = false;
            Loading();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Cleardata();
        }
        private void txtStocksOnHand_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                Btnrep.Enabled = true;
                lblProductNo.Text = DGV.CurrentRow.Cells[0].Value.ToString();
                txtProductCode.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtDescription.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                txtBarcode.Text = DGV.CurrentRow.Cells[3].Value.ToString();
                txtStocksOnHand.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                textBox1.Text = DGV.CurrentRow.Cells[5].Value.ToString();
                txtCategory.Text = DGV.CurrentRow.Cells[6].Value.ToString();
                textBox2.Text = DGV.CurrentRow.Cells[7].Value.ToString();
                Stid = int.Parse(DGV.CurrentRow.Cells[8].Value.ToString());
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var PC = _IUW.products.GetAll().ToList();
                var SC = _IUW.stok_transactions.GetAll().ToList();
                var uc = _IUW.unittypes.GetAll().ToList();
                var CC = _IUW.categories.GetAll().ToList();
                DC = (company)LanguageHelper.ApplyLanguage(this);
                var query = from p in PC
                            join st in SC on p.ProductNo equals st.Proid into stJoin
                            from st in stJoin.DefaultIfEmpty() // LEFT JOIN
                            join u in uc on p.Unitid equals u.ID into uJoin
                            from u in uJoin.DefaultIfEmpty()
                            join c in CC on p.CategoryNo equals c.CategoryNo into cJoin
                            from c in cJoin.DefaultIfEmpty()
                            where p.ProductCode.Contains(txtSearch.Text) || p.Description.Contains(txtSearch.Text) || p.Barcode.Contains(txtSearch.Text)
                            select new
                            {
                                p.ProductNo,
                                p.ProductCode,
                                p.Description,
                                p.Barcode,
                                Quantity = (st != null ? st.Quantity : 0) + (p.StocksOnHand ?? 0),
                                UName = u != null ? u.UName : "",
                                CategoryName = c != null ? c.CategoryName : "",
                                Note = st?.Note != null ? st.Note : "",
                                Sid = st?.Id ?? 0
                            };
                var bindingSource = new BindingSource();
                bindingSource.DataSource = query;
                DGV.DataSource = bindingSource;
            }
            else
            {
                ListofstokLoad();
            }
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            stok_transaction ST = new stok_transaction();
            if (!string.IsNullOrEmpty(lblProductNo.Text))
            {
                if (Stid > 0)
                {
                    ST.Id = Stid;
                }
                ST.Proid = int.Parse(lblProductNo.Text);
                ST.Quantity = int.Parse(txtStocksOnHand.Text);
                ST.Note = textBox2.Text;
                _IUW.stok_transactions.Update(ST);
                _IUW.Complete();
                //_IUW.invtransactions.Insert(new invtransaction
                //{
                //    Proid = ST.Proid,
                //    Quantity = ST.Quantity,
                //    Date = DateTime.Now,
                //    Credit = ST.Quantity > 0 ? ST.Quantity : 0,
                //    Dipt = ST.Quantity < 0 ? ST.Quantity : 0,
                //    type = "Inventory",
                //    transid = ST.Id
                //});
                //_IUW.Complete();
                MessageBox.Show("Updated Successfully");
                Cleardata();
            }
            else
            {
                MessageBox.Show("Please Select Product");
            }
        }
        private void Btnrep_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(lblProductNo.Text);

            // جلب بيانات المنتج
            var product = _IUW.products.GetAll()
                .FirstOrDefault(p => p.ProductNo == productId);

            // جلب الحركات الخاصة بالمنتج
            var report = _IUW.invtransactions.GetAll()
                .Where(t => t.Proid == productId)
                .GroupBy(t => new { t.transid, t.Proid, t.type, t.Date })
                .Select(g => new
                {
                    Billnumber = g.Key.transid,
                    ProductId = g.Key.Proid,
                    Type = g.Key.type,
                    Date = g.Key.Date,
                    Quantity = g.Sum(x => x.Quantity),
                    Credit = g.Sum(x => x.Credit),
                    Dipt = g.Sum(x => x.Dipt),
                    Description = product.Description,
                    StocksOnHand = product.StocksOnHand
                })
                .OrderBy(r => r.Date)
                .ToList();

            // إنشاء التقرير
            Frmreporting FR = new Frmreporting();
            Dataset Ds = new Dataset();
            Stokreport SR = new Stokreport();
            var dt = Ds.Tables["Stokdata"];

            // إضافة الرصيد الافتتاحي كأول سطر
            int runningBalance = product.StocksOnHand ?? 0;

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
            foreach (var item in report)
            {
                var row = dt.NewRow();
                row["Proid"] = item.ProductId;
                switch (item.Type)
                {
                    case "Purchase":
                        row["Description"] = "فاتورة مشتريات";
                        Credit += item.Quantity;
                        break;
                    case "Returned Sales":
                        row["Description"] = "مرتجع مبيعات";
                        Credit += item.Quantity;
                        break;
                    case "Sales":
                        row["Description"] = "فاتورة مبيعات";
                        Dept -= item.Quantity;
                        break;
                    case "Returned Purchases":
                        row["Description"] = "مرتجع مشتريات";
                        Dept -= item.Quantity;
                        break;
                    //case "Inventory":
                    //    row["Description"] = "تعديل مخزون";
                    //    Credit += item.Quantity >= 0 ? item.Quantity : 0;
                    //    Dept -= item.Quantity < 0 ? item.Quantity : 0;
                    //    break;
                }
                row["Credit"] = Credit;
                row["Dept"] = Dept;
                row["Date"] = item.Date.ToString();
                row["Billnumber"] = item.Billnumber;

                // تعديل الرصيد حسب نوع الحركة
                switch (item.Type)
                {
                    case "Purchase":
                    case "Returned Sales":
                        runningBalance += item.Quantity;
                        break;

                    case "Sales":
                    case "Returned Purchases":
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
            SR.SetParameterValue("TOF", "تقرير مخزون : " + txtDescription.Text);
            SR.SetParameterValue("Taxnum", DC.Taxnumber);
            SR.SetParameterValue("Proname", DC.CRN);
            SR.SetParameterValue("English_Shop_name", DC.ENName);
            SR.SetParameterValue("CompanyName", DC.Name);
            FR.CRV.ReportSource = SR;
            FR.Show();
        }
    }
}
