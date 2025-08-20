using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using UOW;

namespace Easypos.Masters
{
    public partial class frmListThirdParty : Form
    {
        company DC;
        Getcentralaizes GC;
        thirdparty TP;
        IUnitofwork _IUW;
        Usingnumber _NO;
        public frmListThirdParty()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            TP = new thirdparty();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            DGV.DataSource = GC.Getthirdpartydatalist();
        }
        private void Clearfieldes(){
            txtNumber.Text = string.Empty;
            txtName.Clear();
            txt_seller_citysubdiv.Clear();
            txt_seller_cityname.Clear();
            textBox4.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            txtOpeningBalance.Clear();
            txtMobile.Clear();
            txtComments.Clear();
            radioSuppliers.Checked = false;
            radioClients.Checked = false;
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnsave.Text = "Add";
            }
            else
            {
                Btnsave.Text = "أضافة";
            }
            cbzatca.Checked = false;
            comdata.Visible = false;
            txt_seller_street.Clear();
            txt_seller_buildingnumber.Clear();
            txt_seller_cityname.Clear();
            txt_seller_citysubdiv.Clear();
            txt_seller_postalzone.Clear();
            txt_location.Clear();
            txtRN.Clear();
            Loading();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Clearfieldes();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnsave.Text = "Edit";
            }
            else
            {
                Btnsave.Text = "تعديل";
            }
            if (DGV.Rows.Count > 0)
            {
                txtNumber.Text = DGV.CurrentRow.Cells[0].Value.ToString();
                TP.ID = int.Parse(txtNumber.Text);
                txtName.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtMobile.Text = DGV.CurrentRow.Cells[3].Value.ToString();
                txt_seller_citysubdiv.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                txt_seller_cityname.Text = DGV.CurrentRow.Cells[5].Value.ToString();
                txtComments.Text = DGV.CurrentRow.Cells[6].Value.ToString();
                txtOpeningBalance.Text = DGV.CurrentRow.Cells[9].Value != null
                                       ? DGV.CurrentRow.Cells[9].Value.ToString()
                                       : "";
                textBox4.Text = DGV.CurrentRow.Cells[10].Value.ToString();
                textBox3.Text = DGV.CurrentRow.Cells[11].Value.ToString();
                // Type Member
                var TM = DGV.CurrentRow.Cells[7].Value.ToString();
                if (TM == "2")
                {
                    radioClient.Checked = true;
                }
                else
                {
                    radioSupplier.Checked = true;
                }
            }
            else
            {
                Clearfieldes();
            }
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (!radioClient.Checked && !radioSupplier.Checked)
            {
                MessageBox.Show("Please enter the type used.", "Error");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please insert name", "Error");
                    return;
                }
                else
                {
                    TP.ISUsePhase2 = cbzatca.Checked;
                    if (cbzatca.Checked)
                    {
                        if (string.IsNullOrEmpty(txt_seller_street.Text))
                        {
                            MessageBox.Show("Please enter the street name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            TP.StreetName = txt_seller_street.Text;
                        }
                        if (string.IsNullOrEmpty(txt_seller_buildingnumber.Text))
                        {
                            MessageBox.Show("Please enter the building number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            TP.BuildingNumber = txt_seller_buildingnumber.Text;
                        }
                        if (string.IsNullOrEmpty(txt_seller_citysubdiv.Text))
                        {
                            MessageBox.Show("Please enter the city subdivision name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            TP.CitySubdivisionName = txt_seller_citysubdiv.Text;
                        }
                        if (string.IsNullOrEmpty(txt_seller_cityname.Text))
                        {
                            MessageBox.Show("Please enter the city name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            TP.CityName = txt_seller_cityname.Text;
                        }
                        if (string.IsNullOrEmpty(txt_seller_postalzone.Text))
                        {
                            MessageBox.Show("Please enter the postal zone.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            TP.PostalZone = txt_seller_postalzone.Text;
                        }
                    }
                    TP.Name = txtName.Text;
                    TP.MobileNumber = txtMobile.Text;
                    TP.Taxnumber = textBox4.Text;
                    TP.Comments = txtComments.Text;
                    if (radioClient.Checked)
                    {
                        TP.Type = 2; // Client
                    }
                    else
                    {
                        TP.Type = 1; // Supplier
                    }
                    if (string.IsNullOrEmpty(txtOpeningBalance.Text))
                    {
                        TP.OpeningBalance = 0;
                    }
                    else
                    {
                        TP.OpeningBalance = double.Parse(txtOpeningBalance.Text);
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(txtNumber.Text.Trim()))
                        {
                            TP.ID = int.Parse(txtNumber.Text);
                            _IUW.thirdparties.Update(TP);
                        }
                        else
                        {
                            _IUW.thirdparties.Insert(TP);
                        }
                        _IUW.Complete();
                    }
                    catch (Exception ex)
                    {
                        var logger = new ExceptionLogger(_IUW);
                        logger.Log(ex, "Third Party");
                    }
                    Clearfieldes();
                }
            }
        }
        private void radioAll_CheckedChanged(object sender, EventArgs e)
        {
            Loading();
        }
        private void radioSuppliers_CheckedChanged(object sender, EventArgs e)
        {
            DGV.DataSource = GC.Getsupplierdatalist();
        }
        private void radioClients_CheckedChanged(object sender, EventArgs e)
        {
            DGV.DataSource = GC.Getcustomerdatalist();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TP.ID != null)
                {
                    _IUW.thirdparties.Delbyid(TP.ID);
                    _IUW.Complete();
                    Clearfieldes();
                }
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Third Party delete");
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                var Data = this.GC.Getthirdpartydatalist();
                var Serch = Data
                    .Where(x => x.Name != null && x.Name.Contains(textBox2.Text))
                    .ToList();
                DGV.DataSource = Serch;
            }
            else
            {
                Loading();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var Data = this.GC.Getthirdpartydatalist();
                var Serch = Data
                    .Where(x => x.MobileNumber != null && x.MobileNumber.Contains(textBox1.Text))
                    .ToList();
                DGV.DataSource = Serch;
            }
            else
            {
                Loading();
            }
        }
        private void cbzatca_CheckedChanged(object sender, EventArgs e)
        {
            if (cbzatca.Checked)
            {
                comdata.Visible = true;
                txtRN.Visible = true;
            }
            else
            {
                comdata.Visible = false;
                txtRN.Visible = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
