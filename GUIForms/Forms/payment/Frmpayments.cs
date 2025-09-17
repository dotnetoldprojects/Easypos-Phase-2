using Domain.Models;
using Easypos.Purchases;
using Easypos.Salesforms.Cashier;
using GUIForms.Dtos;
using GUIForms.Forms.salesforms.Normal;
using GUIForms.helpers;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UOW;

namespace Easypos.Payment
{
    public partial class Frmpayments : Form
    {
        public decimal Total { get; set; }
        public string Formname { get; set; }
        public string Purcid { get; set; }
        public string Cust { get; set; }
        Getcentralaizes GC;
        IUnitofwork _IUW;
        company DC;
        payment pay;
        paymentout payout;
        frmMSalesBill Sb;
        frmPurchases Pur;
        frmPOS Pos;
        Usingnumber _NO;
        public Frmpayments()
        {
            InitializeComponent();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            pay = new payment();
            payout = new paymentout();
            Sb = (Application.OpenForms["frmMSalesBill"] as frmMSalesBill);
            Pos = (Application.OpenForms["frmPOS"] as frmPOS);
            Pur = (Application.OpenForms["frmPurchases"] as frmPurchases);
            _NO = new Usingnumber();
        }
        private void LoadAllCombos()
        {
            if (Formname == "Purchases")
            {
                Commondatasales.FillCombo(clients, GC.Getsupplierdatalist(), "Name", "ID");
                clients.SelectedValue = int.Parse(Purcid);
            }
            else
            {
                Commondatasales.FillCombo(clients, GC.Getcustomerdatalist(), "Name", "ID");
            }
        }
        private void AddCash_Click(object sender, System.EventArgs e)
        {
            txtCash.Text = (decimal.Parse(txtTotal.Text) - decimal.Parse(txtBank.Text)).ToString();
            txtTotalPay.Text = (decimal.Parse(txtCash.Text) + decimal.Parse(txtBank.Text)).ToString();
            txtRem.Text = Convert.ToString((Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalPay.Text)));
        }
        private void AddBank_Click(object sender, EventArgs e)
        {
            txtBank.Text = (decimal.Parse(txtTotal.Text) -  decimal.Parse(txtCash.Text)).ToString();
            txtTotalPay.Text = (decimal.Parse(txtCash.Text) + decimal.Parse(txtBank.Text)).ToString();
            txtRem.Text = Convert.ToString((Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalPay.Text)));
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Frmpayments_Load(object sender, EventArgs e)
        {
            LoadAllCombos();
            txtTotal.Text = Total.ToString();
            if (Cust != null)
            {
                clients.SelectedValue = int.Parse(Cust);
            }
            else
            {
                clients.SelectedValue = int.Parse(Purcid);
            }
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Formname == "Sales")
                {
                    var Rem = Convert.ToDouble(txtRem.Text);
                    if (Rem > 0 && clients.SelectedIndex == 1)
                    {
                        MessageBox.Show("لا يمكن البيع بالآجل لعميل اقتراضي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Rem == 0.00 && clients.SelectedIndex == 1)
                    {
                        MessageBox.Show("لا يمكن البيع بالآجل لعميل اقتراضي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (Sb != null)
                        {
                            Sb.Btnsaved();
                            if (Sb.Billtype.Text == "صدرت")
                            {
                                Savpayment();
                                var Gp = _IUW.payments.GetAll().LastOrDefault();
                                if (Gp.Bank > 0)
                                {
                                    var trans = new transaction();
                                    trans.Invoiceno = Gp.InvoiceNo;
                                    trans.Paynum = Gp.paymentNo;
                                    trans.TDate = Gp.Date;
                                    trans.Type = "سند ايصال مبيعات";
                                    trans.Paytype = "بنكي";
                                    trans.ThirdPartyID = Gp.ThirdPartyID;
                                    trans.Paid = Gp.Bank;
                                    trans.Note = "";
                                    _IUW.transactions.Insert(trans);
                                    _IUW.Complete();
                                }
                                if (Gp.Cash > 0)
                                {
                                    var trans = new transaction();
                                    trans.Invoiceno = Gp.InvoiceNo;
                                    trans.Paynum = Gp.paymentNo;
                                    trans.TDate = Gp.Date;
                                    trans.Type = "سند ايصال مبيعات";
                                    trans.Paytype = "نقدي";
                                    trans.ThirdPartyID = Gp.ThirdPartyID;
                                    trans.Paid = Gp.Cash;
                                    trans.Note = "";
                                    _IUW.transactions.Insert(trans);
                                    _IUW.Complete();
                                }
                                //Sb.Generatexml();
                            }
                            MessageBox.Show("تم حفظ الفاتورة بنجاح", "نجاح");
                        }
                        if (Pos != null)
                        {
                            Pos.Btnsaved();
                            if (Pos.Billtype.Text == "صدرت")
                            {
                                Savpayment();
                                var Gp = _IUW.payments.GetAll().LastOrDefault();
                                if (Gp.Bank > 0)
                                {
                                    var trans = new transaction();
                                    trans.Invoiceno = Gp.InvoiceNo;
                                    trans.Paynum = Gp.paymentNo;
                                    trans.TDate = Gp.Date;
                                    trans.Type = "سند ايصال مبيعات";
                                    trans.Paytype = "بنكي";
                                    trans.ThirdPartyID = Gp.ThirdPartyID;
                                    trans.Paid = Gp.Bank;
                                    trans.Note = "";
                                    _IUW.transactions.Insert(trans);
                                    _IUW.Complete();
                                }
                                if (Gp.Cash > 0)
                                {
                                    var trans = new transaction();
                                    trans.Invoiceno = Gp.InvoiceNo;
                                    trans.Paynum = Gp.paymentNo;
                                    trans.TDate = Gp.Date;
                                    trans.Type = "سند ايصال مبيعات";
                                    trans.Paytype = "نقدي";
                                    trans.ThirdPartyID = Gp.ThirdPartyID;
                                    trans.Paid = Gp.Cash;
                                    trans.Note = "";
                                    _IUW.transactions.Insert(trans);
                                    _IUW.Complete();
                                }
                                //Pos.Generatexml();
                            }
                            MessageBox.Show("تم حفظ الفاتورة بنجاح", "نجاح");
                            //Pos.Clearfieldes();
                        }
                    }
                }
                if (Formname == "Purchases")
                {
                    if (Pur != null)
                    {
                        Pur.Btnsaved();
                        if (Pur.Billtype.Text == "صدرت")
                        {
                            Savpayout();
                        }
                        Pur.Clearall();
                        MessageBox.Show("تم حفظ الفاتورة بنجاح", "نجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Payments");
            }
            Close();
        }
        private void Savpayment()
        {
            var Inv = 0;
            if (Sb != null)
            {
                pay.InvoiceNo = Sb.Invid;
                Inv = Sb.Invid;
            }
            if (Pos != null)
            {
                pay.InvoiceNo = Pos.Invid;
                Inv = Pos.Invid;
            }
            pay.Cash = decimal.Parse(txtCash.Text);
            pay.Bank = decimal.Parse(txtBank.Text);
            pay.Paid = decimal.Parse(txtTotalPay.Text);
            var remaining = decimal.Parse(txtTotal.Text) - decimal.Parse(txtTotalPay.Text);
            pay.Remaining = remaining;
            pay.Date = DateTime.Now.ToString("dd-MM-yyyy");
            pay.Time = DateTime.Now.ToString("hh:mm:ss");
            pay.ThirdPartyID = int.Parse(clients.SelectedValue.ToString());
            pay.PaymentMethod = remaining == 0 ? "نقدي" : "اجل";
            pay.Type = "فاتورة مبيعات";
            _IUW.payments.Insert(pay);
            _IUW.Complete();
            SalesHelper.Savetransactions(Inv, pay.ThirdPartyID, pay.Paid, "فاتورة مبيعات", _IUW, pay.Date);
        }
        private void Savpayout()
        {
            payout.InvoiceNo = Pur.Invid;
            payout.Cash = decimal.Parse(txtCash.Text);
            payout.Bank = decimal.Parse(txtBank.Text);
            payout.Paid = decimal.Parse(txtTotalPay.Text);
            var remaining = decimal.Parse(txtTotal.Text) - decimal.Parse(txtTotalPay.Text);
            payout.Remaining = remaining;
            payout.Date = DateTime.Now.ToString("dd-MM-yyyy");
            payout.Time = DateTime.Now.ToString("hh:mm:ss");
            payout.ThirdPartyID = int.Parse(clients.SelectedValue.ToString());
            payout.PaymentMethod = "Cash & Bank";
            payout.Type = "فاتورة مشتريات";
            _IUW.paymentouts.Insert(payout);
            _IUW.Complete();
            SalesHelper.Savetransactions(Pur.Invid, payout.ThirdPartyID, payout.Paid, "فاتورة مشتريات", _IUW, payout.Date);
            Close();
        }
        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCash.Text))
            {
                txtCash.Text = "0";
            }
            if (txtCash.Text == ".")
            {
                return;
            }
            txtTotalPay.Text = Convert.ToString((Convert.ToDouble(txtCash.Text) + Convert.ToDouble(txtBank.Text)));
            if (Pos != null || Sb != null || Pur != null)
            {
                txtRem.Text = Convert.ToString((Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalPay.Text)));
            }
        }
        private void txtBank_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBank.Text))
            {
                txtBank.Text = "0";
            }
            if (txtBank.Text == ".")
            {
                return;
            }
            txtTotalPay.Text = Convert.ToString((Convert.ToDouble(txtCash.Text) + Convert.ToDouble(txtBank.Text)));
            if (Pos != null || Sb != null || Pur != null)
            {
                txtRem.Text = Convert.ToString((Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalPay.Text)));
            }
        }
    }
}
