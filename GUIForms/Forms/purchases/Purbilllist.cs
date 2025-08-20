using Domain.Models;
using GUIForms.Dtos;
using GUIForms.Forms.salesforms.Normal;
using GUIForms.helpers;
using GUIForms.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Easypos.Purchases
{
    public partial class Purbilllist : Form
    {
        Printinginvoice _PI;
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        Getallsales GAS;
        List<PurchaseViewModel> Res;
        Usingnumber _NO;
        public Purbilllist()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            _PI = new Printinginvoice();
            Getdatalist();
            LoadAllCombos();
            _NO = new Usingnumber();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getsupplierdatalist(), "Name", "ID");
        }
        private void Getdatalist()
        {
            GAS = new Getallsales();
            Res = GAS.GetPurlist();
            DGV.DataSource = Res.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
                x.Type,
            }).ToList();
        }
        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            var SBD = Res.Where(x => DateTime.Parse(x.TDate) >= DateTime.Parse(DTF.Value.ToString("dd-MM-yyyy")) && 
                                     DateTime.Parse(x.TDate) <= DateTime.Parse(DTT.Value.ToString("dd-MM-yyyy"))).ToList();
            DGV.DataSource = SBD.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();

        }
        private void Btnall_Click(object sender, EventArgs e)
        {
            Loading();
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void clientID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var SBD = Res.Where(x => x.ThirdPartyID == int.Parse(clientID.SelectedValue.ToString())).ToList();
            DGV.DataSource = SBD.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void IN_TextChanged(object sender, EventArgs e)
        {
            var SBD = Res.Where(x => x.Invoiceno == int.Parse(IN.Text)).ToList();
            DGV.DataSource = SBD.Select(x => new
            {
                x.Invoiceno,
                x.TDate,
                x.TTime,
                x.NonVatTotal,
                x.Discount,
                x.VatAmount,
                x.TotalAmount,
                ThirdParty = x.ThirdPartyName ?? "عميل افتراضي",
            }).ToList();
        }
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Dataid = DGV.CurrentRow.Cells[1].Value.ToString();
            var Datatye = DGV.CurrentRow.Cells[9].Value.ToString();
            if (DGV.Columns[e.ColumnIndex].Name == "Show")
            {
                frmPurchases FMS = new frmPurchases();
                frmPurchases open = Application.OpenForms["frmPurchases"] as frmPurchases;
                if (open == null)
                {
                    FMS.Clearfildes();
                    FMS.DGV.Rows.Clear();
                    FMS.Invid = int.Parse(Dataid);
                    FMS.Btnsave.Text = "تعديل";
                    if (Datatye == "مسوده")
                    {
                        FMS.Btnsave.Visible = true;
                        FMS.Btnsave.Visible = true;
                        FMS.Btnsave.Text = "تعديل";
                    }
                    else
                    {
                        FMS.Btnsave.Visible = false;
                        FMS.Btnsave.Visible = false;
                        FMS.Billtype.Enabled = false;
                        FMS.Billtype.Text = Datatye;
                    }
                    FMS.Getsalesbill();
                    FMS.Show();
                    this.Close();
                }
                else
                {
                    open.Clearfildes();
                    open.DGV.Rows.Clear();
                    open.Activate();
                    if (open.WindowState == FormWindowState.Maximized)
                    {
                        open.Invid = int.Parse(Dataid);
                        if (Datatye == "مسوده")
                        {
                            open.Btnsave.Visible = true;
                            open.Btnsave.Visible = true;
                            open.Btnsave.Text = "تعديل";
                            open.Billtype.Text = Datatye;
                        }
                        else
                        {
                            open.Btnsave.Visible = false;
                            open.Btnsave.Visible = false;
                            open.Billtype.Text = Datatye;
                            open.Billtype.Enabled = false;
                        }
                        open.Getsalesbill();
                        this.Close();
                    }
                }
            }
        }

        private void IN_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
