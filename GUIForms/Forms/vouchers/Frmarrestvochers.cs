using Aspose.Pdf;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using iText.Kernel.Pdf;
using java.lang;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UOW;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Color = System.Drawing.Color;
using Exception = System.Exception;

namespace Easypos.Vouchers
{
    public partial class Frmarrestvochers : MetroForm
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        voucher Voch;
        Usingnumber _NO;
        exceptionpro EP;
        ExceptionLogger _EL;
        public string Btnevent { get; set; }
        public Frmarrestvochers()
        {
            InitializeComponent();
            Loading();
        }
        private void Deletevoch()
        {
            var Vochid = 0;
            if (string.IsNullOrEmpty(txtpay.Text))
            {
                Vochid = int.Parse(txtpayout.Text);
            }
            if (string.IsNullOrEmpty(txtpayout.Text))
            {
                Vochid = int.Parse(txtpay.Text);
            }
            _IUW.vouchers.Delbyid(Vochid);
            _IUW.Complete();
            Clearfieldes();
            Getdgv();
            MessageBox.Show("تمت العمليه بنجاح");
        }
        private void SaveVoch(string Methode)
        {
            if (Methode == "سندات دفع")
            {
                if (string.IsNullOrWhiteSpace(txtinvnum.Text))
                {
                    Voch.Billnumber = null;
                }
                else
                {
                    Voch.Billnumber = Convert.ToInt32(txtinvnum.Text);
                }
                Voch.Date = Purdate.Value.ToString("dd-MM-yyyy");
                Voch.Thiredpartyid = Convert.ToInt32(CBMThirdparty.SelectedValue.ToString());
                Voch.Vochertypes = Vochertype.Text;
                Voch.Billnum = Billnumber.Text;
                Voch.Paid = Convert.ToDecimal(txtmony.Text);
                Voch.Paymentmathod = CmbPaymethod.Text;
                Voch.Note = Purnottxt.Text;
            }
            if (Methode == "سندات قبض")
            {
                if (string.IsNullOrWhiteSpace(txtinv.Text))
                {
                    Voch.Billnumber = null;
                }
                else
                {
                    Voch.Billnumber = Convert.ToInt32(txtinv.Text);
                }
                Voch.Date = date.Value.ToString("dd-MM-yyyy");
                Voch.Thiredpartyid = Convert.ToInt32(Clients.SelectedValue.ToString());
                Voch.Vochertypes = Vochertypes.Text;
                Voch.Billnum = Billnumber.Text;
                Voch.Paid = decimal.Parse(txtprice.Text);
                Voch.Paymentmathod = Cmbpricetype.Text;
                Voch.Note = Note.Text;
            }
            try
            {
                Voch.Methode = Methode;
                _IUW.vouchers.Insert(Voch);
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Vochers");
            }
            SalesHelper.Savetransactions(Voch.Id, Voch.Thiredpartyid, Voch.Paid, Methode,_IUW);
            Clearfieldes();
            Loading();
        }
        private void EditVoch(string Methode)
        {
            var Vochid = 0;
            //سندات دفع
            if (string.IsNullOrEmpty(txtpay.Text))
            {
                Vochid = int.Parse(txtpayout.Text);
                if (string.IsNullOrWhiteSpace(txtinvnum.Text))
                {
                    Voch.Billnumber = null;
                }
                else
                {
                    Voch.Billnumber = Convert.ToInt32(txtinvnum.Text);
                }
                Voch.Date = Purdate.Value.ToString("dd-MM-yyyy");
                Voch.Thiredpartyid = Convert.ToInt32(CBMThirdparty.SelectedValue.ToString());
                Voch.Vochertypes = Vochertype.Text;
                Voch.Billnum = Billnumber.Text;
                Voch.Paid = decimal.Parse(txtmony.Text);
                Voch.Paymentmathod = CmbPaymethod.Text;
                Voch.Note = Purnottxt.Text;
            }
            // سندات قبض
            if (string.IsNullOrEmpty(txtpayout.Text))
            {
                Vochid = int.Parse(txtpay.Text);
                if (string.IsNullOrWhiteSpace(txtinv.Text))
                {
                    Voch.Billnumber = null;
                }
                else
                {
                    Voch.Billnumber = Convert.ToInt32(txtinv.Text);
                }
                Voch.Date = date.Value.ToString("dd-MM-yyyy");
                Voch.Thiredpartyid = Convert.ToInt32(Clients.SelectedValue.ToString());
                Voch.Vochertypes = Vochertypes.Text;
                Voch.Billnum = Billnumber.Text;
                Voch.Paid = decimal.Parse(txtprice.Text);
                Voch.Paymentmathod = Cmbpricetype.Text;
                Voch.Note = Note.Text;
            }
            if (Vochid == 0)
            {
                MessageBox.Show("برجاء ادخال السند","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var trn = _IUW.transactions.GetAll().Where(x => x.Type == Methode && x.Invoiceno == Voch.Id).FirstOrDefault();
                if (trn != null)
                {
                    _IUW.transactions.Delbyid(trn.ID);
                }
                Voch.Id = Vochid;
                Voch.Methode = Methode;
                _IUW.vouchers.Update(Voch);
                _IUW.Complete();
                SalesHelper.Savetransactions(Voch.Id, Voch.Thiredpartyid, Voch.Paid, Methode, _IUW);
            }
        }
        private void SaveEdit(string Methode)
        {
            if (Btnevent == "Save")
            {
                SaveVoch(Methode);
            }
            else
            {
                EditVoch(Methode);
            }
            Clearfieldes();
            Getdgv();
            MessageBox.Show("تمت العمليه بنجاح");
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            Voch = new voucher();
            LoadAllCombos();
            Getdgv();
            Vochertypes.SelectedIndex = 0;
            Vochertype.SelectedIndex = 0;
            Cmbpricetype.SelectedIndex = 0;
            CmbPaymethod.SelectedIndex = 0;
        }
        private void Getdgv()
        {
            var data = _IUW.vouchers.GetAll().ToList();
            var Res = data.Where(x => x.Methode == "سندات قبض").ToList();
            DGSales.DataSource = Res;
            var Res2 = data.Where(x => x.Methode == "سندات دفع").ToList();
            DGVPur.DataSource = Res2;
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(Clients, GC.Getcustomerdatalist(), "Name", "ID");
            Commondatasales.FillCombo(CBMThirdparty, GC.Getthirdpartydatalist(), "Name", "ID");
        }
        public void Clearfieldes()
        {
            txtpayout.Clear();
            txtinvnum.Clear();
            txtmony.Clear();
            CmbPaymethod.SelectedIndex = 0;
            Btnsave.Enabled = true;
            Vochertypes.SelectedIndex = 0;
            Vochertype.SelectedIndex = 0;
            date.Value = DateTime.Now;
            Purdate.Value = DateTime.Now;
            Note.Clear();
            Purnottxt.Clear();
            Btnsave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            Btnadd.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            Btnsave.Text = "حفظ";
            Btnadd.Text = "حفظ";
            Btnsave.BackColor = Color.FromArgb(0, 173, 31);
            Btnadd.BackColor = Color.FromArgb(0, 173, 31);
            label9.Visible = false;
            lblbill.Visible = false;
            Invnum.Visible = false;
            Billnumber.Visible = false;
            Invnum.Items.Clear();
            Billnumber.Items.Clear();
            txtpay.Clear();
            txtinv.Clear();
            Cmbpricetype.SelectedIndex = 0;
            txtprice.Clear();
        }
        private void RBCust_CheckedChanged(object sender, EventArgs e)
        {
            Commondatasales.FillCombo(CBMThirdparty, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void RBSup_CheckedChanged(object sender, EventArgs e)
        {
            Commondatasales.FillCombo(CBMThirdparty, GC.Getsupplierdatalist(), "Name", "ID");
        }
        private void Btnclear_Click(object sender, EventArgs e)
        {
            Clearfieldes();
        }
        private void Btnadd_Click(object sender, EventArgs e)
        {
            if (Btnadd.Text == "حفظ")
            {
                Btnevent = "Save";
            }
            else
            {
                Btnevent = "Edit";
            }
            SaveEdit("سندات دفع");
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (Btnsave.Text == "تعديل")
            {
                Btnevent = "Edit";
            }
            else
            {
                Btnevent = "Save";
            }
            SaveEdit("سندات قبض");
        }
        private void Btndelete_Click(object sender, EventArgs e)
        {
            Deletevoch();
        }
        private void Btndel_Click(object sender, EventArgs e)
        {
            Deletevoch();
        }
        private void DGSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGSales.Rows.Count > 0)
            {
                Btnsave.IconChar = FontAwesome.Sharp.IconChar.Pen;
                Btnsave.Text = "تعديل";
                Btnsave.BackColor = Color.FromArgb(255, 184, 128);
                txtpay.Text = DGSales.CurrentRow.Cells[0].Value.ToString();
                var inv = DGSales.CurrentRow?.Cells[1]?.Value?.ToString() ?? "";
                txtinv.Text = inv;
                date.Text = DGSales.CurrentRow.Cells[3].Value.ToString();
                txtprice.Text = DGSales.CurrentRow.Cells[5].Value.ToString();
                Cmbpricetype.Text = DGSales.CurrentRow.Cells[6].Value.ToString();
                Clients.SelectedValue = int.Parse(DGSales.CurrentRow.Cells[2].Value.ToString());
                Vochertypes.Text = DGSales.CurrentRow.Cells[4].Value.ToString();
                Note.Text = DGSales.CurrentRow.Cells[7].Value.ToString();
            }
        }
        private void DGVPur_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVPur.Rows.Count > 0)
            {
                Btnadd.IconChar = FontAwesome.Sharp.IconChar.Pen;
                Btnadd.Text = "تعديل";
                Btnadd.BackColor = Color.FromArgb(255, 184, 128);
                txtinv.Text = DGVPur.CurrentRow?.Cells[1]?.Value?.ToString() ?? "";
                txtpayout.Text = DGVPur.CurrentRow.Cells[0].Value.ToString();
                Billnumber.Text = DGVPur.CurrentRow.Cells[9].Value.ToString();
                Purdate.Text = DGVPur.CurrentRow.Cells[3].Value.ToString();
                CBMThirdparty.SelectedValue = int.Parse(DGVPur.CurrentRow.Cells[2].Value.ToString());
                txtmony.Text = DGVPur.CurrentRow.Cells[5].Value.ToString();
                Vochertype.Text = DGVPur.CurrentRow.Cells[4].Value.ToString();
                CmbPaymethod.Text = DGVPur.CurrentRow.Cells[6].Value.ToString();
                Purnottxt.Text = DGVPur.CurrentRow.Cells[7].Value.ToString();
            }
        }
        private void txtmony_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender,e);
        }
    }
}
