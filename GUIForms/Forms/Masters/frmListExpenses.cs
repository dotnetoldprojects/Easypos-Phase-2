using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using GUIForms.models;
using Microsoft.VisualBasic;
using net.sf.saxon.@event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using UOW;

namespace Easypos.Masters
{
    public partial class frmListExpenses : Form
    {
        company DC;
        expens exp;
        IUnitofwork _IUW;
        Usingnumber _NO;
        public double Taxes { get; set; }
        public double Taxvat { get; set; }
        private List<ExpenseViwModel> _EVM;
        public frmListExpenses()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            exp = new expens();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            var list = _IUW.expencestypes.GetAll().ToList();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Expencestypename"; // اللي هيظهر للمستخدم
            comboBox1.ValueMember = "Id";     // القيمة المرتبطة
            _EVM = _IUW.expenses.GetAll().Join(
                        _IUW.expencestypes.GetAll(),
                        e => e.Typeid,
                        t => t.Id,
                        (e, t) => new ExpenseViwModel
                        {
                            ID = e.ID,
                            Amount = e.Amount,
                            CDate = e.CDate,
                            Vat = e.Vat,
                            Description = e.Description,
                            TypeName = t.Expencestypename
                        }).ToList();

            DGV.DataSource = _EVM;
        }
        private void Vatchecked()
        {
            if (Vatcheck.Checked)
            {
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (DC.ISUsePhase2)
                    {
                        Taxes = Convert.ToDouble(txtAmount.Text) * (15 / 100);
                        var OGTax = Math.Round(Convert.ToDouble(Taxes), 2).ToString();
                        Taxvat = double.Parse(OGTax);

                        //var Tax = Convert.ToDouble(txtAmount.Text) * (VI.VatPercent / 100);
                        var Tax = Convert.ToDouble(txtAmount.Text) / 1.15;
                        var GTax = Math.Round(Convert.ToDouble(Tax), 2).ToString();
                        //txtVat.Text = Convert.ToString(double.Parse(GTax) + Convert.ToDouble(txtAmount.Text));
                        txtVat.Text = Convert.ToString(double.Parse(GTax));
                    }
                }
                else
                {
                    Vatcheck.Checked = false;
                    return;
                }
            }
            else
            {
                Taxvat = 0;
                txtVat.Text = "0";
            }
        }
        public void Clearfeildes()
        {
            txtDescription.Clear();
            txtAmount.Clear();
            DTF.Value = DateTime.Now;
            Loading();
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnsave.Text = "Add";
            }
            else
            {
                Btnsave.Text = "اضافة";
            }
            Vatcheck.Checked = false;
            txtVat.Clear();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    Btnsave.Text = "Edit";
                }
                else
                {
                    Btnsave.Text = "تعديل";
                }
                exp.ID = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                txtDescription.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                DTF.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                txtVat.Text = DGV.CurrentRow.Cells[3].Value.ToString();
                txtAmount.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                if (txtVat.Text.ToString() != "0")
                {
                    Vatcheck.Checked = true;
                }
                else
                {
                    Vatcheck.Checked = false;
                }
            }
        }
        private void Btnnew_Click(object sender, EventArgs e)
        {
            Clearfeildes();
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("برجاء ادخال وصف المصروف","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                MessageBox.Show("برجاء مبلغ المصروف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            exp.Description = txtDescription.Text;
            exp.Amount = double.Parse(txtAmount.Text);
            exp.CDate = DTF.Value.ToString("yyyy-MM-dd");
            exp.Vat = Convert.ToDouble(txtVat.Text);
            exp.Typeid = int.Parse(comboBox1.SelectedValue.ToString());
            try
            {
                if (exp != null)
                {
                    _IUW.expenses.Update(exp);
                }
                else
                {
                    _IUW.expenses.Insert(exp);
                }
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Expences list");
            }
            Clearfeildes();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var Serch = _EVM.Where(x => x.Description != null && x.Description.Contains(txtSearch.Text))
                            .ToList();
            DGV.DataSource = Serch;
        }
        private void Vatcheck_CheckedChanged(object sender, EventArgs e)
        {
            Vatchecked();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender,e);
        }

        private void Btndel_Click(object sender, EventArgs e)
        {
            try
            {
                if (exp.ID != null)
                {
                    _IUW.expenses.Delbyid(exp.ID);
                    _IUW.Complete();
                    Clearfeildes();
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Expences list delete");
            }
        }
    }
}
