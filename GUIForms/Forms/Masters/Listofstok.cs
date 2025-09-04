using Domain.Models;
using GUIForms.Dtos;
using GUIForms.models;
using javax.xml.transform;
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
                _IUW.invtransactions.Insert(new invtransaction
                {
                    Proid = ST.Proid,
                    Quantity = ST.Quantity,
                    Date = DateTime.Now,
                    Credit = ST.Quantity > 0 ? ST.Quantity : 0,
                    Dipt = ST.Quantity < 0 ? ST.Quantity : 0,
                    type = "Adjust",
                    transid = ST.Id
                });
                _IUW.Complete();
                MessageBox.Show("Updated Successfully");
                Cleardata();
            }
            else
            {
                MessageBox.Show("Please Select Product");
            }
        }
    }
}
