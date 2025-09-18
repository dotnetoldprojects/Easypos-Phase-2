using Domain.Models;
using GUIForms.Dtos;
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

namespace Easypos.Masters.Subforms
{
    public partial class Itemsales : Form
    {
        Getcentralaizes GC;
        company DC;
        IUnitofwork _IUW;
        item items;
        purchasedetaile Pd;
        public Itemsales()
        {
            InitializeComponent();
            Loading();
        }

        // تعريف المتغيرات العامة
        private int PageSize = 20;
        private int CurrentPage = 1;
        private int TotalPages = 1;
        private List<DataGridViewRow> AllRows = new List<DataGridViewRow>();

        private void Loading()
        {
            GC = new Getcentralaizes();
            Pd = new purchasedetaile();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Getdata();
            CalculateTotalPages();
            DisplayPage(1);
        }

        // تحميل البيانات وتخزينها في AllRows
        void Getdata()
        {
            AllRows.Clear();

            var purchases = _IUW.purchases.GetAll().ToList();
            var purchaseDetails = _IUW.purchasedetailes.GetAll().ToList();

            var result1 = from detail in purchaseDetails
                          join purchase in purchases
                          on detail.InvoiceNo equals purchase.Invoiceno into joined
                          from purchase in joined.DefaultIfEmpty()
                          select new
                          {
                              detail.TDetailNo,
                              detail.TDDesc,
                              TDate = purchase?.TDate.ToString() ?? "",
                              detail.Quantity,
                              detail.InvoiceNo
                          };

            foreach (var item in result1)
            {
                var row = new DataGridViewRow();
                row.CreateCells(DGV,
                    item.TDetailNo.ToString(),
                    item.TDDesc ?? "",
                    item.TDate,
                    item.Quantity.ToString(),
                    "0",
                    "0",
                    item.InvoiceNo,
                    "فاتورة مشتريات"
                );
                AllRows.Add(row);
            }

            var items = _IUW.items.GetAll().ToList();
            var itemsales = _IUW.itemsales.GetAll().ToList();

            var result2 = from its in itemsales
                          join it in items
                          on its.Itemid equals it.ID into joined
                          from it in joined.DefaultIfEmpty()
                          select new
                          {
                              its.ID,
                              Itemname = it?.Itemname ?? "",
                              its.Date,
                              Itemqty = it?.Itemqty ?? 0,
                              its.Quantity,
                              its.invoiceno
                          };

            foreach (var item in result2)
            {
                var row = new DataGridViewRow();
                row.CreateCells(DGV,
                    item.ID.ToString(),
                    item.Itemname,
                    item.Date.ToString(),
                    item.Itemqty,
                    item.Quantity.ToString(),
                    "0",
                    item.invoiceno,
                    "فاتورة مبيعات"
                );
                AllRows.Add(row);
            }
        }

        // حساب عدد الصفحات
        private void CalculateTotalPages()
        {
            TotalPages = (int)Math.Ceiling(AllRows.Count / (double)PageSize);
        }

        // عرض صفحة معينة
        private void DisplayPage(int pageNumber)
        {
            DGV.Rows.Clear();
            CurrentPage = pageNumber;

            int startIndex = (pageNumber - 1) * PageSize;
            var pageRows = AllRows.Skip(startIndex).Take(PageSize).ToList();

            foreach (var row in pageRows)
            {
                var newRow = (DataGridViewRow)row.Clone();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = row.Cells[i].Value;
                }
                DGV.Rows.Add(newRow);
            }

            DGSort(); // ترتيب الرصيد بعد عرض الصفحة
        }

        // ترتيب الرصيد حسب نوع الفاتورة
        void DGSort()
        {
            DGV.Sort(DGV.Columns["Date"], System.ComponentModel.ListSortDirection.Ascending);
            var Balance = 0.0;

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                var Det = DGV.Rows[i].Cells[7].Value.ToString();
                var Stock = Convert.ToDouble(DGV.Rows[i].Cells[3].Value.ToString());
                var Qty = Convert.ToDouble(DGV.Rows[i].Cells[4].Value.ToString());
                var Rem = Convert.ToDouble(DGV.Rows[i].Cells[5].Value.ToString());

                if (i > 0 && Det == "فاتورة مبيعات")
                {
                    DGV.Rows[i].Cells[3].Value = Balance;
                }

                if (Det == "فاتورة مشتريات")
                {
                    Balance += (Stock + Qty);
                    DGV.Rows[i].Cells[5].Value = Balance;
                }
                else if (Det == "فاتورة مبيعات")
                {
                    Balance += (Stock - Qty);
                    DGV.Rows[i].Cells[5].Value = Balance;
                }
            }
        }

        // أزرار التنقل بين الصفحات
        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages)
                DisplayPage(CurrentPage + 1);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                DisplayPage(CurrentPage - 1);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
