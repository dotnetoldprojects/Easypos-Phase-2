using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using UOW;


namespace Easypos.Masters
{
    public partial class frmListCategory : Form
    {
        company DC;
        Getcentralaizes GC;
        category cat;
        IUnitofwork _IUW;
        int color = 0;
        public frmListCategory()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            cat = new category();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            DGV.DataSource = GC.Getcategorydatalist();
        }
        private void Clearfild()
        {
            txtCatName.Clear();
            txtDescription.Clear();
            label4.BackColor = base.BackColor;
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnaddedit.Text = "Save";
            }
            else
            {
                Btnaddedit.Text = "حفظ";
            }
            Loading();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                if(DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    Btnaddedit.Text = "Edit";
                }
                else
                {
                    Btnaddedit.Text = "تعديل";
                }
                cat = new category();
                cat.CategoryNo = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                txtCatName.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtDescription.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                var Bcolor = int.Parse(DGV.CurrentRow.Cells[3].Value.ToString());
                label4.BackColor = System.Drawing.Color.FromArgb(Bcolor);
                cat.Color = Bcolor;
            }
            else
            {
                return;
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Clearfild();
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cat.CategoryName))
                {
                    if (cat.CategoryNo != null)
                    {
                        cat.CategoryName = txtCatName.Text;
                        cat.Description = txtDescription.Text;
                        cat.Color = color;
                        _IUW.categories.Update(cat);
                        _IUW.Complete();
                    }
                    else
                    {
                        cat.CategoryName = txtCatName.Text;
                        cat.Description = txtDescription.Text;
                        cat.Color = color;
                        _IUW.categories.Insert(cat);
                        _IUW.Complete();
                    }
                }
                else
                {
                    MessageBox.Show(DC.Systemlang == "الانجليزية" || DC.Systemlang == "English" ? "Please enter a category name." : "الرجاء إدخال اسم الفئة.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Category list");
            }
            Clearfild();
        }
        private void btnSelectcolor_Click(object sender, EventArgs e)
        {
            DialogResult colors = new DialogResult();
            colors = colorDialog1.ShowDialog();
            if (colors == DialogResult.OK)
            {
                label4.BackColor = colorDialog1.Color;
                color = colorDialog1.Color.ToArgb();
            }
        }
        private void Btndel_Click(object sender, EventArgs e)
        {
            try
            {
                var id = cat.CategoryNo;
                _IUW.categories.Delbyid(id);
                _IUW.Complete();
                Clearfild();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Category list delete");
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var Data = this.GC.Getcategorydatalist();
                var Serch = Data
                    .Where(x => x.CategoryName != null && x.CategoryName.Contains(txtSearch.Text))
                    .ToList();
                DGV.DataSource = Serch;
            }
            else
            {
                Loading();
            }
        }
    }
}
