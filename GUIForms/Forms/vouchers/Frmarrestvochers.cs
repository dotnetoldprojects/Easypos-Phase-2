using Aspose.Pdf;
using CrystalDecisions.CrystalReports.Engine;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using iText.Kernel.Pdf;
using java.lang;
using java.util.function;
using MetroFramework.Forms;
using Reporting;
using Reporting.vouchers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
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
                var BN = int.Parse(Billnumber.Text.Trim());
                var result = _IUW.purchases.GetAll()
                             .Where(p => p.Invoiceno == BN)
                             .GroupJoin(
                                 _IUW.paymentouts.GetAll(),
                                 p => p.Invoiceno,
                                 pay => pay.InvoiceNo,
                                 (p, pays) => new { p, pays })
                             .SelectMany(
                                 x => x.pays.DefaultIfEmpty(),
                                 (x, pay) => new
                                 {
                                     PaymentNo = pay.paymentNo,
                                     TotalAmount = x.p.TotalAmount,
                                     Paid = (decimal?)pay.Paid,
                                     Remaining = (decimal?)pay.Remaining
                                 })
                             .FirstOrDefault();
                var PO = _IUW.paymentouts.Get(result.PaymentNo);
                if (PO != null)
                {
                    PO.paymentNo = result.PaymentNo;
                    PO.Paid = PO.Paid + Convert.ToDecimal(txtmony.Text);
                    PO.Remaining = Convert.ToDecimal(result.TotalAmount) - PO.Paid;
                    _IUW.paymentouts.Update(PO);
                    _IUW.Complete();
                }

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
                SalesHelper.Savetransactions(Voch.Id, Voch.Thiredpartyid, Voch.Paid, Vochertypes.Text, _IUW, Voch.Date);
            }
            if (Methode == "سندات قبض")
            {
                if (Vochertypes.Text == "سند ايصال مبيعات")
                {
                    int BN = 0;
                    if (!string.IsNullOrEmpty(Invnum.Text))
                    {
                        BN = int.Parse(Invnum.Text.Trim());
                    }
                    var result = _IUW.sales.GetAll()
                                 .Where(p => p.Invoiceno == BN)
                                 .GroupJoin(
                                     _IUW.payments.GetAll(),
                                     p => p.Invoiceno,
                                     pay => pay.InvoiceNo,
                                     (p, pays) => new { p, pays })
                                 .SelectMany(
                                     x => x.pays.DefaultIfEmpty(),
                                     (x, pay) => new
                                     {
                                         PaymentNo = pay.paymentNo,
                                         TotalAmount = x.p.TotalAmount,
                                         Paid = (decimal?)pay.Paid,
                                         Remaining = (decimal?)pay.Remaining
                                     })
                                 .FirstOrDefault();
                    var PO = _IUW.payments.Get(result.PaymentNo);
                    if (PO != null)
                    {
                        PO.paymentNo = result.PaymentNo;
                        PO.Paid = PO.Paid + Convert.ToDecimal(txtprice.Text);
                        PO.Cash = PO.Paid;
                        PO.Remaining = Convert.ToDecimal(result.TotalAmount) - PO.Paid;
                        if (PO.Remaining >= 0)
                        {
                            _IUW.payments.Update(PO);
                            _IUW.Complete();
                            Voch.Billnumber = Convert.ToInt32(Invnum.Text);
                            if (string.IsNullOrWhiteSpace(txtinv.Text))
                            {
                                Voch.Billnum = null;
                            }
                            else
                            {
                                Voch.Billnum = txtinv.Text;
                            }
                            Voch.Date = date.Value.ToString("dd-MM-yyyy");
                            Voch.Thiredpartyid = Convert.ToInt32(Clients.SelectedValue.ToString());
                            Voch.Vochertypes = Vochertypes.Text;
                            Voch.Paid = decimal.Parse(txtprice.Text);
                            Voch.Paymentmathod = Cmbpricetype.Text;
                            Voch.Note = Note.Text;
                        }
                        else
                        {
                            MessageBox.Show("المبلغ المدفوع اكبر من المبلغ المستحق","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    SalesHelper.Savetransactions(Voch.Id, Voch.Thiredpartyid, Voch.Paid, Vochertype.Text, _IUW, Voch.Date);
                }
                else
                {
                    Voch.Date = date.Value.ToString("dd-MM-yyyy");
                    Voch.Thiredpartyid = Convert.ToInt32(Clients.SelectedValue.ToString());
                    Voch.Vochertypes = Vochertypes.Text;
                    Voch.Paid = decimal.Parse(txtprice.Text);
                    Voch.Paymentmathod = Cmbpricetype.Text;
                    Voch.Note = Note.Text;
                }
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
                SalesHelper.Savetransactions(Voch.Id, Voch.Thiredpartyid, Voch.Paid, Methode, _IUW, Voch.Date);
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
            txtmony.Clear();
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
            //Invnum.Items.Clear();
            //Billnumber.Items.Clear();
            txtpay.Clear();
            txtinv.Clear();
            Cmbpricetype.SelectedIndex = 0;
            txtprice.Clear();
        }
        private void RBCust_CheckedChanged(object sender, EventArgs e)
        {
            Loading();
            Commondatasales.FillCombo(CBMThirdparty, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void RBSup_CheckedChanged(object sender, EventArgs e)
        {
            Loading();
            Commondatasales.FillCombo(CBMThirdparty, GC.Getsupplierdatalist(), "Name", "ID");
        }
        private void Btnclear_Click(object sender, EventArgs e)
        {
            Clearfieldes();
        }
        private void Btnadd_Click(object sender, EventArgs e)
        {
            if (CmbPaymethod.SelectedIndex == 0)
            {
                MessageBox.Show("برجاء اختيار طريقة الدفع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
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
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (Cmbpricetype.SelectedIndex == 0)
            {
                MessageBox.Show("برجاء اختيار نوع الدفع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
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
        private void Vochertype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Vochertype.SelectedIndex != 0)
            {
                if (Vochertype.Text == "سند دفع لمورد")
                {
                    if (!RBSup.Checked)
                    {
                        MessageBox.Show("برجاء اختيار مورد وليس عميل", "خطأ");
                        Vochertype.SelectedIndex = 0;
                        return;
                    }
                    else
                    {
                        var supplierId = Convert.ToInt32(CBMThirdparty.SelectedValue);

                        // هات كل الفواتير اللي لسه عليها باقي من جدول الدفعات
                        var GPR = _IUW.paymentouts.GetAll()
                                    .Where(x => x.ThirdPartyID == supplierId && x.Remaining != 0)
                                    .ToList();

                        // اربط بالكومبو بوكس (WinForms)
                        Billnumber.DataSource = GPR;
                        Billnumber.DisplayMember = "InvoiceNo";   // اللي المستخدم يشوفه
                        Billnumber.ValueMember = "PaymentNo";    // القيمة اللي تتخزن داخليًا
                    }
                }
                if (Vochertype.Text == "سند دفع لعميل")
                {
                    if (!RBCust.Checked)
                    {
                        MessageBox.Show("برجاء اختيار عميل وليس مورد", "خطأ");
                        Vochertype.SelectedIndex = 0;
                        return;
                    }
                }
                lblbill.Visible = true;
                Billnumber.Visible = true;
            }
            else
            {
                lblbill.Visible = false;
                Billnumber.Visible = false;
                Vochertype.SelectedIndex = 0;
                return;
            }
        }
        private void Vochertypes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Vochertypes.Text == "سند ايصال مبيعات")
            {
                if (Clients.SelectedIndex == 0)
                {
                    Vochertypes.SelectedIndex = 0;
                    MessageBox.Show("برجاء اختيار العميل", "خطأ");
                    return;
                }
                else
                {
                    var CustId = Convert.ToInt32(Clients.SelectedValue);

                    // هات كل الفواتير اللي لسه عليها باقي من جدول الدفعات
                    var GPR = _IUW.payments.GetAll()
                                .Where(x => x.ThirdPartyID == CustId && x.Remaining != 0)
                                .ToList();

                    // اربط بالكومبو بوكس (WinForms)
                    Invnum.DataSource = GPR;
                    Invnum.DisplayMember = "InvoiceNo";   // اللي المستخدم يشوفه
                    Invnum.ValueMember = "PaymentNo";    // القيمة اللي تتخزن داخليًا
                    label9.Visible = true;
                    Invnum.Visible = true;
                    lblbill.Visible = true;
                    Billnumber.Visible = true;
                }
            }
            else
            {
                label9.Visible = false;
                Invnum.Visible = false;
                lblbill.Visible = false;
                Billnumber.Visible = false;
            }
        }
        private void Btnprint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpayout.Text))
            {
                MessageBox.Show("برجاء اختيار السند", "السند");
                return;
            }
            else
            {
                var ID = Convert.ToInt32(txtpayout.Text);
                Printvochers(ID, "سندات دفع");
            }
        }
        PayDto GetPayById(int id)
        {
            var result = (from pay in _IUW.vouchers.GetAll()
                          join tp in _IUW.thirdparties.GetAll()
                              on pay.Thiredpartyid equals tp.ID into tpJoin
                          from tp in tpJoin.DefaultIfEmpty()
                          where pay.Id == id
                          select new PayDto
                          {
                              PaymentNo = pay.Id,
                              Date = pay.Date,
                              Paid = pay.Paid,
                              Remaining = pay.Paid,
                              Type = pay.Paymentmathod,
                              ThirdPartyName = tp != null ? tp.Name : null
                          }).FirstOrDefault();

            return result;
        }
        void Printvochers(int id, string screen)
        {
            ReportDocument Rep = new ReportDocument();
            Frmreporting FR = new Frmreporting();
            Dataset Dsx = new Dataset();
            if (screen == "سندات دفع")
            {
                Rep = new PaymentVoucher();
            }
            if (screen == "سندات قبض")
            {
                Rep = new ReceiptVoucher();
            }
            var PMD = GetPayById(id);
            Dsx.Vochers.Rows.Add(new object[] {
                        PMD.PaymentNo,
                        PMD.Date,
                        PMD.Paid,
                        PMD.ThirdPartyName,
                        PMD.Paid,
                        PMD.Type,
                        null,null,
                        null,null
                    });
            Rep.SetDataSource(Dsx);
            Rep.SetParameterValue("CompanyName", DC.Name);
            Rep.SetParameterValue("Address", DC.Address);
            Rep.SetParameterValue("Taxnum", DC.Taxnumber);
            Rep.SetParameterValue("PhoneNo", DC.PhoneNo);
            Rep.SetParameterValue("Proname", DC.CRN);
            Rep.SetParameterValue("English_Shop_name", DC.ENName);
            FR.CRV.ReportSource = Rep;
            FR.Show();
        }
        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpay.Text))
            {
                MessageBox.Show("برجاء اختيار السند", "السند");
                return;
            }
            else
            {
                var ID = Convert.ToInt32(txtpay.Text);
                Printvochers(ID, "سندات قبض");
            }
        }
    }
}
