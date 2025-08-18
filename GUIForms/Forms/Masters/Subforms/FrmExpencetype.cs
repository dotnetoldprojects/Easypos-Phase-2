using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using MetroFramework.Forms;
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
    public partial class FrmExpencetype : MetroForm
    {
        company DC;
        Getcentralaizes GC;
        expencestype ET;
        IUnitofwork _IUW;
        public FrmExpencetype()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            ET = new expencestype();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            textBox1.Clear();
            DGV.DataSource = _IUW.expencestypes.GetAll();
        }
        public void Cleardata()
        {
            textBox1.Clear();
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnaddedit.Text = "Add";

            }
            else
            {
                Btnaddedit.Text = "أضافة";
            }
            Loading();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    Btnaddedit.Text = "Edit";
                }
                else
                {
                    Btnaddedit.Text = "تعديل";
                }
                ET.Id = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                textBox1.Text = DGV.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    ET.Expencestypename = textBox1.Text;
                    if (ET.Id != null)
                    {
                        _IUW.expencestypes.Update(ET);
                    }
                    else
                    {
                        _IUW.expencestypes.Insert(ET);
                    }
                    _IUW.Complete();
                    Cleardata();
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Expences type");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cleardata();
        }
        private void Btndel_Click(object sender, EventArgs e)
        {
            try
            {
                _IUW.expencestypes.Delbyid(ET.Id);
                _IUW.Complete();
                Cleardata();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Expences Type delete");
            }
        }
    }
}
