using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Easypos.Masters.Subforms
{
    public partial class Frmclient : Form
    {
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        ////DataAccess DA;
        //////private frmSalesBill FSB = null;
        ////Newtailor TSB = null;
        ////CompanyInfo CI;
        //public string MobileNumber { get; set; }
        //public Frmclient(Form CF)
        //{
        //    //CI = new CompanyInfo();
        //    //CI.GetCompany();
        //    Changelang();
        //    InitializeComponent();
        //    //FSB = CF as frmSalesBill;
        //    //TSB = CF as Newtailor;
        //    //DA = new DataAccess();
        //}
        //public void Changelang()
        //{
        //    if (CI.Systemlang == "الانجليزية" || CI.Systemlang == "English")
        //    {
        //        SetCulture("en");
        //    }
        //    else
        //    {
        //        SetCulture("ar");
        //    }
        //}
        //public void SetCulture(string cultureName)
        //{
        //    System.Threading.Thread.CurrentThread.CurrentUICulture =
        //       System.Globalization.CultureInfo.GetCultureInfo(cultureName);
        //    var resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
        //    GetChildren(this).ToList().ForEach(c => {
        //        resources.ApplyResources(c, c.Name);
        //    });
        //    this.Controls.Clear();
        //}
        //public IEnumerable<Control> GetChildren(Control control)
        //{
        //    var controls = control.Controls.Cast<Control>();
        //    return controls.SelectMany(ctrl => GetChildren(ctrl)).Concat(controls);
        //}
        //public int Id { get; set; }
        //public void Getclients()
        //{
        //    try
        //    {
        //        DGV.Rows.Clear();
        //        var Qur = "SELECT ID,Name,MobileNumber FROM thirdparty where Type = 2 ";
        //        DA.Con = new MySqlConnection(DA.CS);
        //        DA.Con.Open();
        //        DA.Cmd = new MySqlCommand(Qur, DA.Con);
        //        DA.dr = DA.Cmd.ExecuteReader();
        //        while (DA.dr.Read())
        //        {
        //            DataGridViewRow newRow = new DataGridViewRow();

        //            newRow.CreateCells(DGV);
        //            //if (CI.Systemlang == "الانجليزية" || CI.Systemlang == "English")
        //            //{
        //            //    newRow.Cells[0].Value = DA.dr["ID"];
        //            //    newRow.Cells[1].Value = DA.dr["Customername"];
        //            //    newRow.Cells[2].Value = DA.dr["MN"];
        //            //}
        //            //else
        //            //{
        //            //    newRow.Cells[0].Value = DA.dr["المسلسل"];
        //            //    newRow.Cells[1].Value = DA.dr["الاسم"];
        //            //    newRow.Cells[2].Value = DA.dr["رقم الجوال"];
        //            //}
        //            newRow.Cells[0].Value = DA.dr["ID"];
        //            newRow.Cells[1].Value = DA.dr["Name"];
        //            newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //            DGV.Rows.Add(newRow);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var Qurcat = @"Insert Into Exception(Exceptionname,Form,Note) value('" + "شاشة جلب بيانات العملاء والمورديين" + "','" + "Getclients" + "','" + ex.Message.ToString() + "')";
        //        DA = new DataAccess();
        //        var CMO = DA.Crudopration(Qurcat);
        //    }
        //}
        //private void Frmclient_Load(object sender, EventArgs e)
        //{
        //    //Getclients();
        //}
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        //private void dgw_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (DGV.Rows.Count > 0)
        //    {
        //        Id = Convert.ToInt32(DGV.CurrentRow.Cells[0].Value.ToString());
        //        Name = DGV.CurrentRow.Cells[1].Value.ToString();
        //        MobileNumber = DGV.CurrentRow.Cells[2].Value.ToString();
        //        textBox2.Text = DGV.CurrentRow.Cells[1].Value.ToString();
        //        textBox4.Text = DGV.CurrentRow.Cells[2].Value.ToString();

        //        //if (Application.OpenForms["Newtailor"] != null)
        //        if (Application.OpenForms["Frmtailoring"] != null)
        //        {
        //            Frmtailoring p = (Application.OpenForms["Frmtailoring"] as Frmtailoring);
        //            p.Getcustomers();
        //            p.clientID.SelectedValue = Id;
        //            //p.Getcustdata(Id);
        //            //p.clientID.Text = textBox2.Text;
        //            p.textBox15.Text = textBox4.Text;
        //            p.Getcustdetailes();
        //            p.GCS();
        //            this.Close();
        //        }
        //        //if (Application.OpenForms["frmSalesBill"] != null)
        //        //{
        //        //    frmSalesBill p = (Application.OpenForms["frmSalesBill"] as frmSalesBill);
        //        //    p.Getcustomers();
        //        //    p.clientID.SelectedValue = Id;
        //        //    //p.clientID.Text = textBox1.Text;
        //        //    //p.textBox15.Text = textBox3.Text;
        //        //    this.Close();
        //        //}
        //        //this.Close();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    //if (string.IsNullOrEmpty(textBox2.Text))
        //    //{
        //    //    Getclients();
        //    //}
        //    //else
        //    //{
        //    //    DGV.Rows.Clear();
        //    //    var Qur = "SELECT * FROM thirdparty where Type = 2 And Name Like '%" + textBox2.Text + "%';";
        //    //    SQLConn.ConnDB();
        //    //    SQLConn.cmd = new MySqlCommand(Qur, SQLConn.conn);
        //    //    SQLConn.dr = SQLConn.cmd.ExecuteReader();
        //    //    while (SQLConn.dr.Read())
        //    //    {
        //    //        DataGridViewRow newRow = new DataGridViewRow();

        //    //        newRow.CreateCells(DGV);
        //    //        newRow.Cells[0].Value = SQLConn.dr["ID"];
        //    //        newRow.Cells[1].Value = SQLConn.dr["Name"];
        //    //        DGV.Rows.Add(newRow);
        //    //    }
        //    //}
        //}
        //private void Add_Click(object sender, EventArgs e)
        //{
        //    frmListThirdParty aeC = new frmListThirdParty();
        //    aeC.radioClient.Checked = true;
        //    aeC.ShowDialog();
        //}
        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{

        //}
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Getclients();
        //}
        //private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)13)
        //    {
        //        if (string.IsNullOrEmpty(textBox2.Text))
        //        {
        //            Getclients();
        //        }
        //        else
        //        {
        //            DGV.Rows.Clear();
        //            var Qur = "SELECT * FROM thirdparty where Type = 2 And Name Like '%" + textBox2.Text + "%';";
        //            DA.Con = new MySqlConnection(DA.CS);
        //            DA.Con.Open();
        //            DA.Cmd = new MySqlCommand(Qur, DA.Con);
        //            DA.dr = DA.Cmd.ExecuteReader();
        //            while (DA.dr.Read())
        //            {
        //                DataGridViewRow newRow = new DataGridViewRow();

        //                newRow.CreateCells(DGV);
        //                //newRow.Cells[0].Value = DA.dr["ID"];
        //                //newRow.Cells[1].Value = DA.dr["Name"];

        //                //if (CI.Systemlang == "الانجليزية" || CI.Systemlang == "English")
        //                //{
        //                //    newRow.Cells[0].Value = DA.dr["ID"];
        //                //    newRow.Cells[1].Value = DA.dr["Name"];
        //                //    newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //                //}
        //                //else
        //                //{
        //                //    newRow.Cells[0].Value = DA.dr["المسلسل"];
        //                //    newRow.Cells[1].Value = DA.dr["الاسم"];
        //                //    newRow.Cells[2].Value = DA.dr["رقم الجوال"];
        //                //}
        //                newRow.Cells[0].Value = DA.dr["ID"];
        //                newRow.Cells[1].Value = DA.dr["Name"];
        //                newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //                DGV.Rows.Add(newRow);
        //            }
        //        }
        //    }
        //}
        //private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)13)
        //    {
        //        if (string.IsNullOrEmpty(textBox4.Text))
        //        {
        //            Getclients();
        //        }
        //        else
        //        {
        //            DGV.Rows.Clear();
        //            var Qur = "SELECT * FROM thirdparty where Type = 2 And MobileNumber Like '%" + textBox4.Text + "%';";
        //            DA.Con = new MySqlConnection(DA.CS);
        //            DA.Con.Open();
        //            DA.Cmd = new MySqlCommand(Qur, DA.Con);
        //            DA.dr = DA.Cmd.ExecuteReader();
        //            while (DA.dr.Read())
        //            {
        //                DataGridViewRow newRow = new DataGridViewRow();

        //                newRow.CreateCells(DGV);
        //                //newRow.Cells[0].Value = DA.dr["ID"];
        //                //newRow.Cells[1].Value = DA.dr["Name"];
        //                //if (CI.Systemlang == "الانجليزية" || CI.Systemlang == "English")
        //                //{
        //                //    newRow.Cells[0].Value = DA.dr["ID"];
        //                //    newRow.Cells[1].Value = DA.dr["Name"];
        //                //    newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //                //}
        //                //else
        //                //{
        //                //    newRow.Cells[0].Value = DA.dr["المسلسل"];
        //                //    newRow.Cells[1].Value = DA.dr["الاسم"];
        //                //    newRow.Cells[2].Value = DA.dr["رقم الجوال"];
        //                //}
        //                newRow.Cells[0].Value = DA.dr["ID"];
        //                newRow.Cells[1].Value = DA.dr["Name"];
        //                newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //                DGV.Rows.Add(newRow);
        //            }
        //        }
        //    }
        //}

        //private void Btnsearch_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(textBox4.Text) && string.IsNullOrEmpty(textBox2.Text))
        //    {
        //        Getclients();
        //    }
        //    else
        //    {
        //        DGV.Rows.Clear();
        //        var Qur = "SELECT * FROM thirdparty where Type = 2 ";
        //        if (!string.IsNullOrEmpty(textBox4.Text))
        //        {
        //            Qur += "And MobileNumber Like '%" + textBox4.Text + "%' ";
        //        }
        //        if (!string.IsNullOrEmpty(textBox2.Text))
        //        {
        //            Qur += "And Name Like '%" + textBox2.Text + "%' ";
        //        }
        //        DA.Con = new MySqlConnection(DA.CS);
        //        DA.Con.Open();
        //        DA.Cmd = new MySqlCommand(Qur, DA.Con);
        //        DA.dr = DA.Cmd.ExecuteReader();
        //        while (DA.dr.Read())
        //        {
        //            DataGridViewRow newRow = new DataGridViewRow();

        //            newRow.CreateCells(DGV);
        //            //newRow.Cells[0].Value = DA.dr["ID"];
        //            //newRow.Cells[1].Value = DA.dr["Name"];

        //            //if (CI.Systemlang == "الانجليزية" || CI.Systemlang == "English")
        //            //{
        //            //    newRow.Cells[0].Value = DA.dr["ID"];
        //            //    newRow.Cells[1].Value = DA.dr["Name"];
        //            //    newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //            //}
        //            //else
        //            //{
        //            //    newRow.Cells[0].Value = DA.dr["المسلسل"];
        //            //    newRow.Cells[1].Value = DA.dr["الاسم"];
        //            //    newRow.Cells[2].Value = DA.dr["رقم الجوال"];
        //            //}
        //            newRow.Cells[0].Value = DA.dr["ID"];
        //            newRow.Cells[1].Value = DA.dr["Name"];
        //            newRow.Cells[2].Value = DA.dr["MobileNumber"];
        //            DGV.Rows.Add(newRow);
        //        }
        //    }
        //}

        //private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (DGV.Columns[e.ColumnIndex].Name == "Choose")
        //    {
        //        if (DGV.Rows.Count > 0)
        //        {
        //            Id = Convert.ToInt32(DGV.CurrentRow.Cells[0].Value.ToString());
        //            Name = DGV.CurrentRow.Cells[1].Value.ToString();
        //            MobileNumber = DGV.CurrentRow.Cells[2].Value.ToString();
        //            textBox2.Text = DGV.CurrentRow.Cells[1].Value.ToString();
        //            textBox4.Text = DGV.CurrentRow.Cells[2].Value.ToString();

        //            //if (Application.OpenForms["Newtailor"] != null)
        //            if (Application.OpenForms["Frmtailoring"] != null)
        //            {
        //                Frmtailoring p = (Application.OpenForms["Frmtailoring"] as Frmtailoring);
        //                p.Getcustomers();
        //                p.clientID.SelectedValue = Id;
        //                //p.Getcustdata(Id);
        //                //p.clientID.Text = textBox2.Text;
        //                p.textBox15.Text = textBox4.Text;
        //                p.Getcustdetailes();
        //                p.GCS();
        //                this.Close();
        //            }
        //            //if (Application.OpenForms["frmSalesBill"] != null)
        //            //{
        //            //    frmSalesBill p = (Application.OpenForms["frmSalesBill"] as frmSalesBill);
        //            //    p.Getcustomers();
        //            //    p.clientID.SelectedValue = Id;
        //            //    //p.clientID.Text = textBox1.Text;
        //            //    //p.textBox15.Text = textBox3.Text;
        //            //    this.Close();
        //            //}
        //            //this.Close();
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}
    }
}
