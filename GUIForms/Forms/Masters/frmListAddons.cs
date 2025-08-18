using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
namespace Easypos.Masters
{
    public partial class frmListAddons : Form
    {
        //public string Catid { get; set; }
        //CompanyInfo CI;
        //public frmListAddons()
        //{
        //    CI = new CompanyInfo();
        //    CI.GetCompany();
        //    Changelang();
        //    InitializeComponent();
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

        //public void LoadProducts(string strSearch)
        //{
        //    //try
        //    //{
        //    //    SQLConn.sqL = "SELECT P.ID, P.AddonCode, P.Barcode, P.ProductName, P.Description, P.UnitPrice, CategoryName FROM addons_items as P LEFT JOIN addons_cates C ON P.CategoryID = C.ID WHERE P.Description LIKE '" + strSearch + "%' ORDER BY Description";
        //    //    SQLConn.ConnDB();
        //    //    SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
        //    //    SQLConn.dr = SQLConn.cmd.ExecuteReader();

        //    //    ListViewItem x = null;
        //    //    ListView1.Items.Clear();


        //    //    while (SQLConn.dr.Read() == true)
        //    //    {
        //    //        x = new ListViewItem(SQLConn.dr["ID"].ToString());
        //    //        x.SubItems.Add(SQLConn.dr["AddonCode"].ToString());
        //    //        x.SubItems.Add(SQLConn.dr["ProductName"].ToString());
        //    //        x.SubItems.Add(SQLConn.dr["CategoryName"].ToString());
        //    //        x.SubItems.Add(Strings.Format(SQLConn.dr["UnitPrice"], "#,##0.00"));
        //    //        x.SubItems.Add(SQLConn.dr["Barcode"].ToString());
        //    //        ListView1.Items.Add(x);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Interaction.MsgBox(ex.ToString());
        //    //}
        //    //finally
        //    //{
        //    //    SQLConn.cmd.Dispose();
        //    //    SQLConn.conn.Close();
        //    //}
        //}

        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    //SQLConn.adding = true;
        //    //SQLConn.updating = false;
        //    //int init = 0;
        //    //frmAddEditAddon aeP = new frmAddEditAddon(init);
        //    //aeP.ShowDialog();
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    //if (ListView1.Items.Count == 0)
        //    //{
        //    //    Interaction.MsgBox("الرجا اختيار الصنف اولا", MsgBoxStyle.Exclamation, "تحديث");
        //    //    return;
        //    //}
        //    //try
        //    //{
        //    //    var text = ListView1.SelectedItems[0].Text;
        //    //    if (text != "" || text != null)
        //    //    {
        //    //        SQLConn.adding = false;
        //    //        SQLConn.updating = true;
        //    //        productID = Convert.ToInt32(ListView1.FocusedItem.Text);
        //    //        frmAddEditAddon aeP = new frmAddEditAddon(productID);
        //    //        aeP.ShowDialog();
        //    //    }
        //    //    else
        //    //    {
        //    //        Interaction.MsgBox("الرجا اختيار الصنف اولا", MsgBoxStyle.Exclamation, "تحديث");
        //    //        return;
        //    //    }
        //    //}
        //    //catch
        //    //{
        //    //    Interaction.MsgBox("الرجا اختيار الصنف اولا", MsgBoxStyle.Exclamation, "تحديث");
        //    //    return;
        //    //}
        //}

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    //SQLConn.strSearch = Interaction.InputBox("ادخل اسم الصنف.", "بحث صنف", " ");

        //    //if (SQLConn.strSearch.Length >= 1)
        //    //{
        //    //    LoadProducts(SQLConn.strSearch.Trim());
        //    //}
        //    //else if (string.IsNullOrEmpty(SQLConn.strSearch))
        //    //{
        //    //    return;
        //    //}
        //}

        //private void frmListAddons_Load(object sender, EventArgs e)
        //{
        //    LoadProducts("");
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void picClose_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //frmListCategoryAddons lc = new frmListCategoryAddons();
        //    //lc.ShowDialog();
        //}

        //private void Button3_Click(object sender, EventArgs e)
        //{
        //    frmSelectCategory flc = new frmSelectCategory(this);
        //    flc.ShowDialog();
        //}
    }
}
