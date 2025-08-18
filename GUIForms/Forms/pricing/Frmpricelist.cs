using Aspose.Pdf.Drawing;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using GUIForms.models;
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

namespace Easypos.Pricing
{
    public partial class Frmpricelist : Form
    {
        Usingnumber _NO;
        Printinginvoice _PI;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Getallsales GAS;
        List<PricesViewModel> Res;
        public Frmpricelist()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            _PI = new Printinginvoice();
            Getdatalist();
            LoadAllCombos();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Getdatalist()
        {
            GAS = new Getallsales();
            Res = GAS.GetPricelist();
            DGV.DataSource = Res.Select(x => new
            {
                x.ID,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void Btnall_Click(object sender, EventArgs e)
        {
            Getdatalist();
        }
        private void IN_TextChanged(object sender, EventArgs e)
        {
            var DS = Res.Where(x => x.ID.ToString().Contains(IN.Text));
                DGV.DataSource = DS.Select(x => new
            {
                x.ID,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void clientID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var DS = Res.Where(x => x.ThirdPartyName.ToString().Contains(clientID.Text));
            DGV.DataSource = DS.Select(x => new
            {
                x.ID,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            var DF = DateTime.Parse(DTF.Value.ToString("dd-MM-yyyy"));
            var DT = DateTime.Parse(DTT.Value.ToString("dd-MM-yyyy"));
            var DS = Res.Where(x => DateTime.Parse(x.Datefrom) >= DF && DateTime.Parse(x.Dateto) <= DT);
            DGV.DataSource = DS.Select(x => new
            {
                x.ID,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Pricid = int.Parse(DGV.CurrentRow.Cells[2].Value.ToString());
            if (DGV.Columns[e.ColumnIndex].Name == "Show")
            {
                Frmprice FPrc = new Frmprice();
                Frmprice open = Application.OpenForms["Frmprice"] as Frmprice;
                if (open == null)
                {
                    FPrc.Invid = Pricid;
                    //FPrc.Btnsave.Text = "تعديل";
                    FPrc.Btnsave.Visible = false;
                    FPrc.Getsalesbill();
                    FPrc.Show();
                    this.Close();
                }
                else
                {
                    open.Activate();
                    if (open.WindowState == FormWindowState.Maximized)
                    {
                        open.Invid = Pricid;
                        open.Btnsave.Enabled = false;
                        //open.Btnsaveandprint.Enabled = false;
                        //open.Ge;
                        this.Close();
                    }
                }
            }
            else
            {
                //Prc.ID = int.Parse(DGV.CurrentRow.Cells[2].Value.ToString());
                //Frmprice FPrc = new Frmprice();
                //FPrc.Prc.ID = Prc.ID;
                //FPrc.Printinv();
            }
        }

        private void IN_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
