using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIForms.helpers
{
    public class DGVProductHandler
    {
        public bool Isvat { get; set; }
        private DataGridView _dgv;
        private ComboBox _cmbProducts;
        private ComboBox _unitTypes;
        private TextBox _txtQuantity;
        private TextBox _txtPrice;
        private TextBox _txtTBV;
        private TextBox _txtDiscount;
        private TextBox _txtTax;
        private TextBox _txtTotal;
        private bool _isVatEnabled;
        private double _vatPercent;
        public DGVProductHandler(DataGridView dgv,
                                 ComboBox cmbProducts,
                                 ComboBox unitTypes,
                                 TextBox txtQuantity,
                                 TextBox txtPrice,
                                 TextBox txtTBV,
                                 TextBox txtDiscount,
                                 TextBox txtTax,
                                 TextBox txtTotal,
                                 bool isVatEnabled = true,
                                 double vatPercent = 15)
        {
            _dgv = dgv;
            _cmbProducts = cmbProducts;
            _unitTypes = unitTypes;
            _txtQuantity = txtQuantity;
            _txtPrice = txtPrice;
            _txtTBV = txtTBV;
            _txtDiscount = txtDiscount;
            _txtTax = txtTax;
            _txtTotal = txtTotal;
            _isVatEnabled = isVatEnabled;
            _vatPercent = vatPercent;
        }

        public void AddProductToDGV()
        {
            if (string.IsNullOrWhiteSpace(_unitTypes.Text))
            {
                MessageBox.Show("برجاء إدخال وحدات لكي يمكن إضافة المنتجات", "خطأ");
                return;
            }

            if (!decimal.TryParse(_txtQuantity.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("ادخل كمية صحيحة أكبر من صفر", "خطأ");
                return;
            }

            if (_dgv.Rows.Count > 0)
            {
                CheckIfExistsThenUpdate();
            }
            else
            {
                AddNewRowToDGV();
            }

            //UpdateDGVSummary();
        }
        private void CheckIfExistsThenUpdate()
        {
            string selectedProductId = _cmbProducts.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(selectedProductId)) return;

            decimal inputQuantity = decimal.TryParse(_txtQuantity.Text, out var q) ? q : 0;

            foreach (DataGridViewRow row in _dgv.Rows)
            {
                var productId = row.Cells[0]?.Value?.ToString();
                if (productId == selectedProductId)
                {
                    decimal existingQty = Convert.ToDecimal(row.Cells[4].Value);
                    decimal newQty = existingQty + inputQuantity;
                    row.Cells[4].Value = newQty;

                    decimal price = Convert.ToDecimal(row.Cells[5].Value);
                    decimal discount = Convert.ToDecimal(row.Cells[6].Value);
                    row.Cells[7].Value = (newQty * price) - discount;
                    return;
                }
            }

            AddNewRowToDGV();
        }
        private void AddNewRowToDGV()
        {
            if (!decimal.TryParse(_txtQuantity.Text, out decimal quantity) || quantity <= 0)
                return;

            if (!decimal.TryParse(_txtPrice.Text, out decimal price) || price <= 0)
                return;

            decimal total = quantity * price;

            _dgv.Rows.Add(
                _cmbProducts.SelectedValue?.ToString() ?? "0",
                _cmbProducts.Text ?? "",
                _unitTypes.Text ?? "",
                _unitTypes.SelectedValue?.ToString() ?? "0",
                quantity,
                price,
                0,
                total
            );
        }
        public void UpdateDGVSummary()
        {

            double sumSubtotal = 0;
            double sumDiscount = 0;

            foreach (DataGridViewRow row in _dgv.Rows)
            {
                if (row.IsNewRow) continue;

                sumDiscount += Convert.ToDouble(row.Cells[6].Value);
                //sumSubtotal += Convert.ToDouble(row.Cells[7].Value);
                sumSubtotal += Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);
            }

            _txtTBV.Text = sumSubtotal.ToString("N2");
            _txtDiscount.Text = sumDiscount.ToString("N2");

            double totalAfterDiscount = sumSubtotal - sumDiscount;

            double tax = _isVatEnabled ? (_vatPercent / 100) * totalAfterDiscount : 0;
            _txtTax.Text = tax.ToString("N2");
            double finalTotal = totalAfterDiscount + tax;
            _txtTotal.Text = finalTotal.ToString("N2");
        }
        public void UpdateDGVSummary2()
        {
            double sumSubtotal = 0;
            double sumDiscount = 0;

            foreach (DataGridViewRow row in _dgv.Rows)
            {
                if (row.IsNewRow) continue;

                sumDiscount += Convert.ToDouble(row.Cells[6].Value);
                sumSubtotal += Convert.ToDouble(row.Cells[7].Value);
            }

            _txtTBV.Text = sumSubtotal.ToString("N2");
            _txtDiscount.Text = sumDiscount.ToString("N2");

            double totalAfterDiscount = sumSubtotal - sumDiscount;

            double tax = 0;
            if (Isvat)
            {
                tax = _isVatEnabled ? (_vatPercent / 100) * totalAfterDiscount : 0;
            }
            _txtTax.Text = tax.ToString("N2");
            double finalTotal = totalAfterDiscount + tax;
            _txtTotal.Text = finalTotal.ToString("N2");
        }
    }
}
