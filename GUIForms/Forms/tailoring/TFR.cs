using Domain;
using Domain.Models;
using GUIForms.Dtos;
using GUIForms.helpers;
using Reporting;
using Reporting.tailor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Easypos.Tailoring
{
    public partial class TFR : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        public TFR()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            LoadAllCombos();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string base64String = DC.CompanyLogo;
            byte[] Logo = Convert.FromBase64String(base64String);
            CRAll CRA = new CRAll();
            Frmreporting FCCR = new Frmreporting();
            Dataset ds = new Dataset();
            var query =
                from a in _IUW.alltailorings.GetAll().ToList()
                join h in _IUW.tailorheaders.GetAll().ToList() on a.Tailheaderid equals h.Id into headerJoin
                from h in headerJoin.DefaultIfEmpty()
                join t in _IUW.thirdparties.GetAll().ToList() on h.Custid equals t.ID into thirdJoin
                from t in thirdJoin.DefaultIfEmpty()
                where h.Custid == t.ID
                select new
                {
                    a.Id,
                    CustomerName = t.Name,
                    h.Total,
                    h.Paied,
                    h.Totalreimining,
                    h.Clothesnumber,
                    h.Clothesremining,
                    h.Clothesrecived,
                    h.Date,
                    h.Reciveddate,
                    CustomerPhone = t.MobileNumber
                };

            var result = query.ToList();
            foreach (var item in result)
            {
                ds.Closes.Rows.Add(
                    item.Id,
                    item.Date,
                    item.Reciveddate,
                    item.Clothesnumber ?? 0,
                    item.Clothesrecived ?? 0,
                    item.Clothesremining ?? 0,
                    item.CustomerName,
                    item.CustomerPhone,
                    item.Total ?? 0,
                    item.Paied ?? 0,
                    item.Totalreimining ?? 0,
                    Logo
                );
            }
            // تطبيق الفلاتر
            if (checkBox1.Checked)
            {
                var fromDate = dateTimePicker1.Value.Date;
                var toDate = dateTimePicker2.Value.Date;
                query = query.Where(x => DateTime.Parse(x.Date) >= fromDate && DateTime.Parse(x.Date) <= toDate);
            }

            if (checkBox2.Checked)
            {
                query = query.Where(x => x.Clothesremining > 0);
            }

            if (clientID.Text != "--اختر--")
            {
                query = query.Where(x => x.CustomerName == clientID.Text);
            }


            CRA.SetDataSource(ds);
            CRA.SetParameterValue("CompanyName", DC.Name);
            CRA.SetParameterValue("Address", DC.Address);
            CRA.SetParameterValue("PhoneNo", DC.PhoneNo);
            CRA.SetParameterValue("MobileNo", DC.PhoneNo);
            CRA.SetParameterValue("Taxnum", DC.Taxnumber);
            CRA.SetParameterValue("Proname", DC.CRN);
            CRA.SetParameterValue("English_Shop_name", DC.ENName);

            FCCR.CRV.ReportSource = CRA;
            FCCR.CRV.Refresh();
            FCCR.Show();

        }
    }
}
