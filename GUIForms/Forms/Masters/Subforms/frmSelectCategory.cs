using Domain.Models;
using Easypos.Masters;
using GUIForms.Dtos;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UOW;

namespace Easypos.Masters.Subforms
{
    public partial class frmSelectCategory : Form
    {
        company DC;
        Getcentralaizes GC;
        category CT;
        IUnitofwork _IUW;
        public event Action<int, string> CategorySelected; // ID واسم التصنيف
        public frmSelectCategory()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            DGV.DataSource = GC.Getcategorydatalist();
            CT = new category();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CT.CategoryNo = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
            CT.CategoryName = DGV.CurrentRow.Cells[1].Value.ToString();

            // إطلاق الحدث
            CategorySelected?.Invoke(CT.CategoryNo, CT.CategoryName);
            Close();
        }
    }
}
