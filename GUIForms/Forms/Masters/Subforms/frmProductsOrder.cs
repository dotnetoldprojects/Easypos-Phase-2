using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using GUIForms.models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UOW;

namespace Easypos.Masters.Subforms
{
    public partial class frmProductsOrder : Form
    {
        company DC;
        Getcentralaizes GC;
        product Pro;
        IUnitofwork _IUW;
        public frmProductsOrder()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            Pro = new product();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            cmbCategory.DataSource = GC.Getcategorydatalist();
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var prodata = _IUW.products
                              .GetAll()
                              .Where(p => p.CategoryNo == Convert.ToInt32(cmbCategory.SelectedValue))
                              .Select(p => new ProductViewModel
                              {
                                  ProductNo = p.ProductNo,
                                  Description = p.Description,
                                  Order = p.Order ?? 0,
                              }).ToList();
            // Fix: Change the type of BindingList to match the anonymous type
            var Datapro = new BindingList<ProductViewModel>(prodata);
            //dgvProducts.DataSource = Datapro.Select(p => new
            //{
            //    p.ProductNo,
            //    p.Description,
            //    p.Order,
            //}).ToList();

            // 2️⃣ ربط الـ DataGridView مباشرة
            dgvProducts.DataSource = Datapro;

            // 3️⃣ التأكد من الأعمدة بعد الربط
            dgvProducts.AutoGenerateColumns = true; // لو الأعمدة مش موجودة تلقائي

            // 4️⃣ لف على الأعمدة لتعديل ReadOnly للعمود المطلوب
            foreach (DataGridViewColumn col in dgvProducts.Columns)
            {
                if (col.Name == "Order")  // العمود اللي عايز تعدل عليه
                {
                    col.ReadOnly = false;  // دلوقتي ممكن تعدل
                    col.DisplayIndex = 2;  // ترتيب العمود
                }
                else if (col.Name == "UnitName")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "ProductCode")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "UnitPrice")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "CategoryName")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "CategoryNo")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "Barcode")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "ReorderLevel")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "AllowInventory")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "Unitid")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "ShowInPOS")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "StocksOnHand")
                {
                    col.ReadOnly = false;   // الرقم لا يتغير
                    col.Visible = false;
                }
                else if (col.Name == "ProductCode")
                {
                    col.ReadOnly = true;   // الرقم لا يتغير
                    col.Visible = true;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                var ID = Convert.ToInt32(dgvProducts.Rows[i].Cells["ID"].Value);
                var Order = Convert.ToInt32(dgvProducts.Rows[i].Cells["Order"].Value);
                var Pro = _IUW.products.Get(ID);
                if (Pro == null)
                {
                    MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        Pro.Order = Order;
                        _IUW.products.Update(Pro);
                        _IUW.Complete();
                    }
                    catch (Exception ex)
                    {
                        var logger = new ExceptionLogger(_IUW);
                        logger.Log(ex, "Orders");
                    }
                }
            }
        }
    }
}
