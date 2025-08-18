using Domain.Models;
using Easypos.Masters.Subforms;
using GUIForms.Dtos;
using GUIForms.helpers;
using GUIForms.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
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
            txtStocksOnHand.Clear();
            textBox1.Clear();
            textBox2.Clear();
            txtSearch.Clear();
            DGV.DataSource = null;
            DGV.Rows.Clear();
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
        private void GetItemsData()
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
                It.ID = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                txtItemname.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtUnitPrice.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                txtStocksOnHand.Text = DGV.CurrentRow.Cells[3].Value.ToString();
                textBox1.Text = DGV.CurrentRow.Cells[6].Value.ToString();
                textBox2.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                UId = int.Parse(DGV.CurrentRow.Cells[5].Value.ToString());
            }
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemname.Text) ||
                       string.IsNullOrEmpty(txtUnitPrice.Text) ||
                       string.IsNullOrEmpty(txtStocksOnHand.Text) ||
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
                        It.Itemqty = double.Parse(txtStocksOnHand.Text);
                        It.OpeningBalance = int.Parse(textBox2.Text);
                        _IUW.items.Update(It);
                    }
                    else
                    {
                        It.Itemname = txtItemname.Text;
                        It.Itemprice = Convert.ToDouble(txtUnitPrice.Text);
                        It.Itemqty = double.Parse(txtStocksOnHand.Text);
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
                var Serch = _EVM.Where(x => x.Itemname != null && x.Itemname.Contains(txtSearch.Text)).ToList();
                DGV.DataSource = Serch.Select(p => new {
                    ID = p.ID,
                    Itemname = p.Itemname,
                    Itemprice = p.Itemprice,
                    Itemqty = p.Itemqty,
                    OpeningBalance = p.OpeningBalance,
                    UnitName = p.UnitName,
                    Unitid = p.Unitid
                }).ToList();
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
    }
}
