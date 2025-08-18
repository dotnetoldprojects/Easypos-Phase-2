
using Domain.Models;
using GUI.Helpers;
using GUIForms.Dtos;
using GUIForms.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using System.Xml.Linq;
using UOW;
using static net.sf.saxon.functions.SuperId;

namespace Easypos.Masters
{
    public partial class Frmunits : Form
    {
        company DC;
        Getcentralaizes GC;
        unittype UT;
        IUnitofwork _IUW;
        public Frmunits()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            textBox1.Clear();
            DGV.DataSource = GC.Getunittypedatalist();
        }
        private void BA_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    UT = new unittype();
                    UT.UName = textBox1.Text;
                    _IUW.unittypes.Insert(UT);
                    _IUW.Complete();
                    Loading();
                }
                else
                {
                    MessageBox.Show("Please insert Unit name ... ", "Error");
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Add Unit");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    UT.UName = textBox1.Text;
                    _IUW.unittypes.Insert(UT);
                    _IUW.Complete();
                    Loading();
                }
                else
                {
                    MessageBox.Show("Please insert Unit name ... ", "Error");
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Edit Unit");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _IUW.unittypes.Delbyid(UT.ID);
                _IUW.Complete();
                Loading();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Delete Unit");
            }
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                UT = new unittype();
                UT.ID = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
                textBox1.Text = DGV.CurrentRow.Cells[1].Value.ToString();
            }
            else
            {
                return;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
