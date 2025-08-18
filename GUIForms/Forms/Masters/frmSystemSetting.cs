using Domain.Models;
using GUI.Helpers;
using GUIForms.Dtos;
using GUIForms.helpers;
using java.security.cert;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;
using UOW;

namespace Easypos.Masters
{
    public partial class frmSystemSetting : Form
    {
        company DC;
        Company CP;
        IUnitofwork _IUW;
        public int Csid { get; set; }
        Usingnumber _NO;
        public frmSystemSetting()
        {
            _IUW = new Unitofwork(new EasyposEntities());
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            _NO = new Usingnumber();
            DC = new company();
            Loadpriters();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            if (DC != null)
            {
                CP = new CompanyProfile().GetCompanyProfile(DC.CompanyID, DC.ENName, DC.Taxnumber, DC.CRN);
                if (CP != null)
                {
                    txt_commonName.Text = CP.CommonName;
                    txt_serialNumber.Text = CP.SerialNumber;
                    txt_industry.Text = CP.IndustryBusinessCategory;
                }
                txtName.Text = DC.Name;
                txtENName.Text = DC.ENName;
                txtAddress.Text = DC.Address;
                txtPhoneNo.Text = DC.PhoneNo;
                txtTaxnumber.Text = DC.Taxnumber;
                txtRN.Text = DC.CRN;
                txtCompanyname.Text = DC.Logoname;
                txtENCompanyname.Text = DC.LogoENName;
                if (!string.IsNullOrEmpty(DC.CompanyLogo))
                {
                    byte[] bytes = Convert.FromBase64String(DC.CompanyLogo);
                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    txtLogoPath.Text = "الصوره في المعرض";
                    Logo.Image = image;
                }
                if (DC.ISUsePhase2)
                {
                    cbzatca.Checked = true;
                    txt_seller_street.Text = DC.StreetName;
                    txt_seller_buildingnumber.Text = DC.BuildingNumber;
                    txt_seller_citysubdiv.Text = DC.CitySubdivisionName;
                    txt_seller_cityname.Text = DC.CityName;
                    txt_seller_postalzone.Text = DC.PostalZone;
                }
                if (DC.CompanyID != null)
                {
                    GOthers.Visible = true;
                }
                Lineproduction.Checked = DC.ISUSElineproduction ?? false;
                Restlist.Checked = DC.ISUSEResturant ?? false;
                CMDLang.Text = DC.Systemlang ?? "العربية";
                PrintCheak.Checked = DC.ISUSEPrinter ?? false;
                CBPrinters.SelectedItem = DC.Printername;
                if (DC.Sysnametype == "طباعة نظام مبيعات كبير")
                {
                    CBBigPrint.Checked = true;
                }
                else if (DC.Sysnametype == "طباعة نظام مبيعات صغير")
                {
                    CBSmalPrint.Checked = true;
                }
                else
                {
                    CBBigPrint.Checked = false;
                    CBSmalPrint.Checked = false;
                }
            }
            Zatcataxcheack();
            Getalldevices();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Loadpriters()
        {
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {
                CBPrinters.Items.Add(printname);
            }
        }
        public void Cleardevice()
        {
            txt_commonName.Clear();
            txt_serialNumber.Clear();
            txt_industry.Clear();
            txt_csr.Clear();
            txt_privatekey.Clear();
            txt_publickey.Clear();
            txt_secret.Clear();
            Getalldevices();
        }
        public void Getalldevices()
        {
            Cashierdgv.DataSource = _IUW.cashiers.GetAll();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogoPath.Text = openFileDialog1.FileName;
                Logo.Image = new Bitmap(openFileDialog1.FileName);
            }
        }
        private void PrintCheak_CheckedChanged(object sender, EventArgs e)
        {
            //Showhidprinter();
        }
        private void Btncomsave_Click(object sender, EventArgs e)
        {
            DC.Name = txtName.Text;
            DC.ENName = txtENName.Text;
            DC.Address = txtAddress.Text;
            DC.PhoneNo = txtPhoneNo.Text;
            DC.Taxnumber = txtTaxnumber.Text;
            DC.CRN = txtRN.Text;
            DC.Logoname = txtCompanyname.Text;
            DC.LogoENName = txtENCompanyname.Text;
            DC.CompanyLogo = !string.IsNullOrEmpty(txtLogoPath.Text) && txtLogoPath.Text != "الصوره في المعرض"
                ? Convert.ToBase64String(File.ReadAllBytes(txtLogoPath.Text))
                : null;
            DC.ISUsePhase2 = cbzatca.Checked;
            if (cbzatca.Checked)
            {
                if (string.IsNullOrEmpty(txt_seller_street.Text))
                {
                    MessageBox.Show("Please enter the street name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DC.StreetName = txt_seller_street.Text;
                }
                if (string.IsNullOrEmpty(txt_seller_buildingnumber.Text))
                {
                    MessageBox.Show("Please enter the building number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }else
                {
                    DC.BuildingNumber = txt_seller_buildingnumber.Text;
                }
                if (string.IsNullOrEmpty(txt_seller_citysubdiv.Text))
                {
                    MessageBox.Show("Please enter the city subdivision name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }else
                {
                    DC.CitySubdivisionName = txt_seller_citysubdiv.Text;
                }
                if (string.IsNullOrEmpty(txt_seller_cityname.Text))
                {
                    MessageBox.Show("Please enter the city name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DC.CityName = txt_seller_cityname.Text;
                }
                if (string.IsNullOrEmpty(txt_seller_postalzone.Text))
                {
                    MessageBox.Show("Please enter the postal zone.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DC.PostalZone = txt_seller_postalzone.Text;
                }
                Comleatmission();
            }
            else
            {
                Comleatmission();
            }
        }
        private void Comleatmission()
        {
            if (DC == null)
            {
                try
                {
                    _IUW.companies.Insert(DC);
                    _IUW.Complete();
                    MessageBox.Show("Company profile created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    var logger = new ExceptionLogger(_IUW);
                    logger.Log(ex, "System Settings");
                }
            }
            else
            {
                try
                {
                    DC.CompanyID = DC.CompanyID;
                    _IUW.companies.Update(DC);
                    _IUW.Complete();
                    MessageBox.Show("Company profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    var logger = new ExceptionLogger(_IUW);
                    logger.Log(ex, "System Settings");
                }
            }
        }
        private void Zatcataxcheack()
        {
            if (cbzatca.Checked)
            {
                label10.Visible = true;
                label15.Visible = true;
                txtTaxnumber.Visible = true;
                txtRN.Visible = true;
                zatacadata.Visible = true;
            }
            else
            {
                label10.Visible = false;
                label15.Visible = false;
                txtTaxnumber.Visible = false;
                txtTaxnumber.Clear();
                txtRN.Visible = false;
                txtRN.Clear();
                zatacadata.Visible = false;
            }
        }
        private void Btnrest_Click(object sender, EventArgs e)
        {
            DC.ISUSEResturant = Restlist.Checked;
            DC.ISUSElineproduction = Lineproduction.Checked;
            DC.Systemlang = CMDLang.Text;
            DC.ISUSEPrinter = PrintCheak.Checked;
            if (CBBigPrint.Checked)
            {
                DC.Sysnametype = "طباعة نظام مبيعات كبير";
            }
            else if (CBSmalPrint.Checked)
            {
                DC.Sysnametype = "طباعة نظام مبيعات صغير";
            }
            else
            {
                DC.Sysnametype = "طباعة نظام مبيعات صغير خياطه";
            }
            DC.Printername = CBPrinters.Text;
            try
            {
                _IUW.companies.Update(DC);
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "System setting Other");
            }
            MessageBox.Show("تم تعديل بيانات الشركه بنجاح برجاء فلق البرنامج وفتحه مره اخرى للعمل بشكل صحيح","تم");
        }
        private void cbzatca_CheckedChanged(object sender, EventArgs e)
        {
            if (cbzatca.Checked)
            {
                comdata.Visible = true;
                zatacadata.Visible = true;
                label10.Visible = true;
                label15.Visible = true;
                txtTaxnumber.Visible = true;
                txtRN.Visible = true;
                zatacadata.Visible = true;
            }
            else
            {
                comdata.Visible = false;
                zatacadata.Visible = false;
                label10.Visible = false;
                label15.Visible = false;
                txtTaxnumber.Visible = false;
                txtTaxnumber.Clear();
                txtRN.Visible = false;
                txtRN.Clear();
                zatacadata.Visible = false;
            }
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            cashier Dev = new cashier();
            Dev.SystemName = txt_commonName.Text;
            Dev.Systemserial = txt_serialNumber.Text;
            Dev.Materialname = txt_industry.Text;
            Dev.Comid = DC.CompanyID;
            Dev.CSR = txt_csr.Text;
            Dev.PrivateKey = txt_privatekey.Text;
            Dev.PublicKey = txt_publickey.Text;
            Dev.Secret = txt_secret.Text;
            try
            {
                _IUW.cashiers.Insert(Dev);
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "System setting cashier");
            }
            Cleardevice();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _IUW.cashiers.Delbyid(Csid);
            _IUW.Complete();
            Cleardevice();
        }
        private void CSRGenerat()
        {
            CSRDtos csr = new CSRDtos();
            csr.GenerateCSR(
                commonName: CP.CommonName,
                serialNumber: CP.SerialNumber,
                organizationIdentifier: CP.OrganizationIdentifier,
                organizationUnitName: CP.OrganizationUnitName,
                organizationName: CP.Name,
                countryName: CP.CountryName,
                invoiceType: CP.InvoiceType,
                locationAddress: CP.LocationAddress,
                industryBusinessCategory: CP.IndustryBusinessCategory
            );
        }
        private async Task Certificategenerate()
        {
            if (CP != null)
            {
                UCDtos UCD = new UCDtos();
                await UCD.UploadCsrAsync();
                txt_csr.Text = UCD.CSRToken;
                txt_privatekey.Text = UCD.SercretToken;
            }
            else
            {
                MessageBox.Show("Company profile not found.");
            }
        }
        private async void Signcertificate()
        {
            UCSigndtos UCS = new UCSigndtos();
            await UCS.Getcertificate();
            txt_publickey.Text = UCS.CSRToken;
            txt_secret.Text = UCS.SercretToken;
        }
        private async void btn_csid_Click(object sender, EventArgs e)
        {
            CSRGenerat();
            await Certificategenerate();
            Signcertificate();
        }
        private void RefreshSerialNumber()
        {
            string input = txt_serialNumber.Text.Trim();
            int index = input.LastIndexOf("|3-");
            if (index >= 0)
            {
                input = input.Substring(0, index + 3);
                txt_serialNumber.Text = input + Guid.NewGuid().ToString();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            RefreshSerialNumber();
        }
        private void Cashierdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Cashierdgv.Rows.Count > 0)
            {
                Csid = int.Parse(Cashierdgv.CurrentRow.Cells[0].Value.ToString());
                txt_commonName.Text = Cashierdgv.CurrentRow.Cells[1].Value.ToString();
                txt_serialNumber.Text = Cashierdgv.CurrentRow.Cells[2].Value.ToString();
                txt_industry.Text = Cashierdgv.CurrentRow.Cells[3].Value.ToString();
                txt_secret.Text = Cashierdgv.CurrentRow.Cells[8].Value.ToString();
                txt_publickey.Text = Cashierdgv.CurrentRow.Cells[7].Value.ToString();
                txt_privatekey.Text = Cashierdgv.CurrentRow.Cells[6].Value.ToString();
                txt_csr.Text = Cashierdgv.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void txtTaxnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender, e);
        }
    }
}
