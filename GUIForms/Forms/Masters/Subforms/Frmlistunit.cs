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
using static java.util.jar.Attributes;
using static net.sf.saxon.functions.SuperId;

namespace Easypos.Masters.Subforms
{
    public partial class Frmlistunit : Form
    {
        company DC;
        Getcentralaizes GC;
        unittype UT;
        IUnitofwork _IUW;
        public event Action<int, string> UnitSelected; // ID واسم التصنيف
        public Frmlistunit()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            DGV.DataSource = GC.Getunittypedatalist();
            UT = new unittype();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UT.ID = int.Parse(DGV.CurrentRow.Cells[0].Value.ToString());
            UT.UName = DGV.CurrentRow.Cells[1].Value.ToString();

            // إطلاق الحدث
            UnitSelected?.Invoke(UT.ID, UT.UName);
            Close();
        }
    }
}
