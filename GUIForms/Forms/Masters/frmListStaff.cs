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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using UOW;

namespace Easypos.Masters
{
    public partial class frmListStaff : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        staff _Stf;
        public frmListStaff()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            _Stf = new staff();
            DGV.DataSource = _IUW.staffs.GetAll();
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnaddedit.Text = "Edit";
            }
            else
            {
                Btnaddedit.Text = "تعديل";
            }
            if (DGV.Rows.Count > 0)
            {
                lblProductNo.Text = DGV.CurrentRow.Cells[0].Value.ToString();
                txtFirstname.Text = DGV.CurrentRow.Cells[1].Value.ToString();
                txtLastname.Text = DGV.CurrentRow.Cells[2].Value.ToString();
                txtContractNo.Text = DGV.CurrentRow.Cells[4].Value.ToString();
                txtProvince.Text = DGV.CurrentRow.Cells[5].Value.ToString();
                txtUsername.Text = DGV.CurrentRow.Cells[6].Value.ToString();
            }
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            Clearfieldes();
        }
        private void Clearfieldes()
        {
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                Btnaddedit.Text = "Add";
            }
            else
            {
                Btnaddedit.Text = "أضافة";
            }
            lblProductNo.Text = string.Empty;
            txtContractNo.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPWD.Clear();
            txtFirstname.Clear();
            txtLastname.Clear();
            txtProvince.Clear();
            textBox1.Clear();
            textBox2.Clear();
            DGV.Rows.Clear();
            Loading();
        }
        private void Btndel_Click(object sender, EventArgs e)
        {
            try
            {
                _IUW.staffs.Delbyid(int.Parse(lblProductNo.Text));
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Staff list delete");
            }
        }
        private void Btnaddedit_Click(object sender, EventArgs e)
        {
            if (txtFirstname.Text == "")
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert Name", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل الاسم", "خطأ");
                    return;
                }
            }
            if (txtLastname.Text == "")
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert Name", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل الاسم", "خطأ");
                }
            }
            if (txtUsername.Text == "")
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert User Name", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم المستخدم", "خطأ");
                    return;
                }
            }
            if (txtPassword.Text == "")
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert password", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل كلمة السر", "خطأ");
                    return;
                }
            }
            if (txtPassword.Text != txtConfirmPWD.Text)
            {
                if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
                {
                    MessageBox.Show("Please insert password", "Error");
                    return;
                }
                else
                {
                    MessageBox.Show("من فضلك تحقق من كلمة السر", "خطأ");
                    return;
                }
            }
            _Stf.Firstname = txtFirstname.Text;
            _Stf.Lastname = txtLastname.Text;
            _Stf.Fullname = txtFirstname.Text + " " + txtLastname.Text;
            _Stf.Address = txtProvince.Text;
            _Stf.Phonenumber = txtContractNo.Text;
            _Stf.Username = txtUsername.Text;
            _Stf.UPassword = txtPassword.Text;
            _Stf.Role = "Admin";
            try
            {
                if (Btnaddedit.Text == "أضافة" || Btnaddedit.Text == "Add")
                {
                    _IUW.staffs.Insert(_Stf);
                }
                else
                {
                    _Stf.StaffID = int.Parse(lblProductNo.Text);
                    _IUW.staffs.Update(_Stf);
                }
                _IUW.Complete();
            }
            catch (Exception ex)
            {
                var logger = new ExceptionLogger(_IUW);
                logger.Log(ex, "Staff list");
            }
            if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            {
                MessageBox.Show("Saved Successfully", "Employee");
            }
            else
            {
                MessageBox.Show("تم الحفظ بنجاح", "موظف");
            }
            Clearfieldes();
        }
    }
}
