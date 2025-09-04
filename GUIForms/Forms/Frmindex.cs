using Domain.Models;
using Easypos.Masters;
using Easypos.Masters.Subforms;
using Easypos.Pricing;
using Easypos.Purchases;
using Easypos.Salesforms;
using Easypos.Salesforms.Cashier;
using Easypos.Tailoring;
using Easypos.TransactionsAccountant;
using Easypos.Vouchers;
using GUIForms.Dtos;
using GUIForms.Forms.Masters;
using GUIForms.Forms.salesforms.Normal;
using Reporting.Sales;
using Resturantlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Easypos
{
    public partial class Frmindex : Form
    {
        public int staffID { get; set; }
        public string username { get; set; }
        public string deviceID { get; set; }
        company DC;
        public Frmindex()
        {
            DC = (company)LanguageHelper.ApplyLanguage(this);
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {          
            if (DC != null)
            {
                if (DC.Systemlang == "الانجليزية")
                {
                    lblCompanyInfo.Text = DC.LogoENName;
                }
                else
                {
                    lblCompanyInfo.Text = DC.Logoname;
                }
            }
            // Title Alginment
            int x = (Header.Size.Width - HN.Size.Width) / 2;
            HN.Location = new Point(x, HN.Location.Y);
            lbluser.Text = $"Login user : {username}";
            int XCI = (HP.Size.Width - lblCompanyInfo.Size.Width) / 2;
            lblCompanyInfo.Location = new Point(x, lblCompanyInfo.Location.Y);
        }
        private void Frmindex_Load(object sender, EventArgs e)
        {

            //if (Ci.ISUSEResturant)
            //{
            //    Resturantmenu.Visible = true;
            //}
            //else
            //{
            //    Resturantmenu.Visible = false;
            //}



            //////=====================//
            ////SQLConn.getData();
            //if (DC.Systemlang == "الانجليزية" || DC.Systemlang == "English")
            //{
            //    this.lbluser.Text = "User name :  " + Username.ToUpper();
            //}
            //else
            //{
            //    this.lbluser.Text = "اسم المستخدم :  " + Username.ToUpper();
            //}
            //////==================//
            //CompanyInfo CI = new CompanyInfo();
            //CI.GetCompany();
            //if (Ci.Systemlang == "الانجليزية" || Ci.Systemlang == "English")
            //{
            //    lblCompanyInfo.Text = CI.CENName;
            //}
            //else
            //{
            //    lblCompanyInfo.Text = CI.CName;
            //}
            //var TOF = CI.Sysnametype;
            //if (TOF == "طباعة نظام مبيعات صغير خياطه")
            //{
            //    Btntail.Visible = true;
            //    toolStripMenuItem2.Visible = true;
            //}
            //else
            //{
            //    Btntail.Visible = false;
            //    toolStripMenuItem2.Visible = false;
            //}
        }
        private void CloseCurrentShift(int shiftId)
        {
            //int newShift = 0;
            //System.DateTime.Now.ToString("yyyy/MM/dd") + "', '" + System.DateTime.Now.ToString("HH:mm:ss") + ")

            try
            {

                //  var date = System.DateTime.Now.ToString("ddMMyy") + " " + System.DateTime.Now.ToString("hh:mm tt");
                //SQLConn.sqL = "Update SHIFT SET ISOPEN = false,TIMECLOSING = NOW() WHERE SHIFTID = @shiftid";
                //SQLConn.ConnDB();
                //SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
                ////SQLConn.cmd.Parameters.AddWithValue("@timeClosing", DateTime.Now);
                //SQLConn.cmd.Parameters.AddWithValue("@shiftid", shiftId);
                //var x = SQLConn.cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //Interaction.MsgBox(ex.ToString());
            }
            finally
            {
                //SQLConn.cmd.Dispose();
                //SQLConn.conn.Close();
            }
            //return newShift;

        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Btnmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }
        private void تقريرسنداتالصرقToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void Btnemp_Click(object sender, EventArgs e)
        {
            frmListStaff f1 = new frmListStaff();
            f1.ShowDialog();
        }
        private void Btncategories_Click(object sender, EventArgs e)
        {
            frmListCategory lc = new frmListCategory();
            lc.ShowDialog();
        }
        private void Btnpro_Click(object sender, EventArgs e)
        {
            frmListProduct lp = new frmListProduct();
            lp.ShowDialog();
        }
        private void Btnunits_Click(object sender, EventArgs e)
        {
            Frmunits FU = new Frmunits();
            FU.Show();
        }
        private void Btnadd_Click(object sender, EventArgs e)
        {
            //frmListAddons FR = new frmListAddons();
            //FR.ShowDialog();
        }
        //public void IsUseShift()
        //{
        //    try
        //    {
        //        SQLConn.sqL = "SELECT IsUseShift FROM Company";
        //        SQLConn.ConnDB();
        //        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
        //        SQLConn.dr = SQLConn.cmd.ExecuteReader();


        //        if (SQLConn.dr.Read())
        //        {

        //            Data.IsUseShift = Convert.ToBoolean(SQLConn.dr[0]);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox(ex.ToString());
        //    }
        //    finally
        //    {
        //        SQLConn.cmd.Dispose();
        //        SQLConn.conn.Close();
        //    }
        //}
        //Shift
        //private int chickIfOpenedShiftExist(int DeviceID)
        //{
        //    int shiftID = 0;
        //    int OpenedShiftUser = 0;
        //    //Return OpenedShiftID
        //    try
        //    {
        //        SQLConn.sqL = "SELECT * FROM Shift WHERE DeviceID = @DeviceID  and IsOpen = true ";
        //        SQLConn.ConnDB();
        //        SQLConn.cmd = new MySqlCommand(SQLConn.sqL, SQLConn.conn);
        //        SQLConn.cmd.Parameters.Clear();
        //        SQLConn.cmd.Parameters.AddWithValue("@DeviceID", DeviceID);


        //        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        {
        //            if (SQLConn.dr.HasRows == true)
        //            {

        //                while (SQLConn.dr.Read() == true)

        //                    shiftID = int.Parse(SQLConn.dr["ShiftID"].ToString());
        //                OpenedShiftUser = int.Parse(SQLConn.dr["UserID"].ToString());
        //            }
        //        }


        //        Data.OpenedShiftUser = OpenedShiftUser;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return shiftID;
        //}
        private void Btnpos_Click(object sender, EventArgs e)
        {
            frmPOS Pos = new frmPOS();
            Pos.ShowDialog();
        }
        private void Btnsales_Click(object sender, EventArgs e)
        {
            frmMSalesBill frmMSalesBill = new frmMSalesBill();
            //frmMSalesBill.Stid = StaffID;
            //frmMSalesBill.St = Username;
            frmMSalesBill.ShowDialog();
        }
        private void Btntreasury_Click(object sender, EventArgs e)
        {
            //Treasury Frmtru = new Treasury();
            //Frmtru.Show();
        }
        private void Btnexpencess_Click(object sender, EventArgs e)
        {
            frmListExpenses exp = new frmListExpenses();
            exp.ShowDialog();
        }
        private void Btnreturn_Click(object sender, EventArgs e)
        {
            //Fremreturnrequired FRR = new Fremreturnrequired();
            //FRR.Show();
            //Salesreturned FRS = new Salesreturned();
            //Returnedsales FRS = new Returnedsales();
            //FRS.Show();
        }
        private void Btnbank_Click(object sender, EventArgs e)
        {
            //Frmbankaccount FBA = new Frmbankaccount();
            //FBA.Show();
        }
        private void Btnorditem_Click(object sender, EventArgs e)
        {
            frmProductsOrder po = new frmProductsOrder();
            po.ShowDialog();
        }
        private void Btnio_Click(object sender, EventArgs e)
        {
            //frmIncomeMoney frm = new frmIncomeMoney();
            //frm.ShowDialog();
        }
        private void Btnrs_Click(object sender, EventArgs e)
        {
            //frmReceivePurchases frmReceivePurchases = new frmReceivePurchases();
            //frmReceivePurchases.ShowDialog();
        }
        private void Btnpepol_Click(object sender, EventArgs e)
        {
            frmListThirdParty Tp = new frmListThirdParty();
            Tp.ShowDialog();
        }
        private void Btnpb_Click(object sender, EventArgs e)
        {
            frmPurchases FPur = new frmPurchases();
            FPur.Show();
        }
        private void Btnsettings_Click(object sender, EventArgs e)
        {
            frmSystemSetting ss = new frmSystemSetting();
            ss.Show();
        }
        private void Btnaccounts_Click(object sender, EventArgs e)
        {
            Acountfilter Frfa = new Acountfilter();
            Frfa.Show();
        }
        private void Btncash_Click(object sender, EventArgs e)
        {
            Frmarrestvochers AV = new Frmarrestvochers();
            AV.Show();
        }
        private void الاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListProduct lp = new frmListProduct();
            lp.ShowDialog();
        }
        private void الوحداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmunits FU = new Frmunits();
            FU.Show();
        }
        private void التصنيفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListCategory lc = new frmListCategory();
            lc.ShowDialog();
        }
        private void الاضافاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListAddons FR = new frmListAddons();
            //FR.ShowDialog();
        }
        private void الموظفيينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListStaff f1 = new frmListStaff();
            //f1.ShowDialog();
        }
        private void المورديينوالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListThirdParty Tp = new frmListThirdParty();
            Tp.ShowDialog();
        }
        private void ترتيبالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductsOrder po = new frmProductsOrder();
            po.ShowDialog();
        }
        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void فاتورةالمشترباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchases FPur = new frmPurchases();
            FPur.Show();
        }
        private void استلامالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReceivePurchases frmReceivePurchases = new frmReceivePurchases();
            //frmReceivePurchases.ShowDialog();
        }
        private void فاتورةمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMSalesBill salesBill = new frmMSalesBill();
            //salesBill.Stid = StaffID;
            //salesBill.St = Username;
            salesBill.ShowDialog();
        }
        private void نقاطالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPOS Pos = new frmPOS();
            Pos.ShowDialog();
            //Pos.STId = StaffID;
            //Check Shift Get Shift By Device & User ID 
            //IsUseShift();
            //if (Data.IsUseShift == true)
            //{
            //    var ShiftID = chickIfOpenedShiftExist(DeviceID);

            //    if (ShiftID > 0)// Shift Opened
            //    {
            //        if (Data.StaffID == Data.OpenedShiftUser)
            //        {
            //            frmSelectCurrentShift scs = new frmSelectCurrentShift();
            //            Data.ShiftID = ShiftID; // Set exist shiftid 
            //            scs.ShowDialog();
            //        }
            //        //else if (Data.StaffID != Data.OpenedShiftUser)
            //        //{
            //        //    frmCloseCurrentShift c = new frmCloseCurrentShift();
            //        //    Data.ShiftID = ShiftID; // Set exist shiftid 
            //        //    c.ShowDialog();
            //        //}
            //    }
            //    else
            //    {
            //        frmShift s = new frmShift(0); //Show form New Shift
            //        s.ShowDialog();
            //    }
            //}
            //else
            //{
            //    frmPOS lp = new frmPOS(StaffID);
            //    lp.ShowDialog();

            //}
        }
        private void المصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListExpenses exp = new frmListExpenses();
            //exp.ShowDialog();
        }
        private void المرتجعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Returnedsales FRS = new Returnedsales();
            //FRS.Show();
            //Fremreturnrequired FRR = new Fremreturnrequired();
            //FRR.Show();
        }
        private void سندصرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmPayemntsVoucher frm = new frmPayemntsVoucher();
            //frm.ShowDialog();
        }
        private void ايراداتاخرىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmIncomeMoney frm = new frmIncomeMoney();
            //frm.ShowDialog();
        }
        private void كشفحسابعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Acountfilter Frfa = new Acountfilter();
            //Frfa.Show();
        }
        private void Btnim_Click(object sender, EventArgs e)
        {
            //frmReportFilterIncomeMoney frm = new frmReportFilterIncomeMoney();
            //frm.ShowDialog();
        }
        private void تقريرالدخلالعامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportFilterIncomeMoney frm = new frmReportFilterIncomeMoney();
            //frm.ShowDialog();
        }
        private void Btnvectors_Click(object sender, EventArgs e)
        {
            //frmReportFilterPaymentsVochuers frm = new frmReportFilterPaymentsVochuers();
            //frm.ShowDialog();
        }
        private void تقريرسنداتالصرقToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //frmReportFilterPaymentsVochuers frm = new frmReportFilterPaymentsVochuers();
            //frm.ShowDialog();
        }
        private void Btnie_Click(object sender, EventArgs e)
        {
            //frmReportFilterExpensesRevenues frm = new frmReportFilterExpensesRevenues();
            //frm.ShowDialog();
        }
        private void تقريرالايراداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportFilterExpensesRevenues frm = new frmReportFilterExpensesRevenues();
            //frm.ShowDialog();
        }
        private void Btnsv_Click(object sender, EventArgs e)
        {
            //frmReportFilterDailySalesWithVAT rvat = new frmReportFilterDailySalesWithVAT();
            //rvat.ShowDialog();
        }
        private void تقريرالمبيعاتلضريبةالقيمةالمضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportFilterDailySalesWithVAT rvat = new frmReportFilterDailySalesWithVAT();
            //rvat.ShowDialog();
        }
        private void التقريرالضريبيللمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Salestax FST = new Salestax();
            //FST.Show();
        }
        private void Btnsp_Click(object sender, EventArgs e)
        {
            //frmReportFilterSalesPurchases frm = new frmReportFilterSalesPurchases();
            //frm.ShowDialog();
        }
        private void Btnsr_Click(object sender, EventArgs e)
        {
            //frmReportFilterDailySales FR = new frmReportFilterDailySales();
            //FR.ShowDialog();
        }
        private void تقريرالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportFilterDailySales FR = new frmReportFilterDailySales();
            //FR.ShowDialog();
        }
        private void Btntil_Click(object sender, EventArgs e)
        {
            //Frmtailoring FT = new Frmtailoring();
            //FT.STId = StaffID;
            //FT.Show();
        }
        private void التقريرالضريبيالموحدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frmotr FMT = new Frmotr();
            //FMT.Show();
        }
        private void تقريرالمبيعاتالاجلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Customercrditfilter FCCF = new Customercrditfilter();
            //FCCF.Show();
        }
        private void رسائلToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (NetworkInterface.GetIsNetworkAvailable())
            //{
            //    Frmwhatsapp Fw = new Frmwhatsapp();
            //    Fw.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("لا يوجد اتصال بالانترنت برجاء الاتصال والمعاوده مره اخرى", "خطأ", MessageBoxButtons.OK);
            //    return;

            //}
        }
        private void حركاتالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Acountfilter Frfa = new Acountfilter();
            //Frfa.Show();
        }
        private void حركاتالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Supplieraccounts Frmsa = new Supplieraccounts();
            //Frmsa.Show();
        }
        private void اضافةسعرجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Priceoffer PO = new Priceoffer();
            //PO.Show();
        }
        private void تقريرالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReportFilterSalesPurchases frm = new frmReportFilterSalesPurchases();
            //frm.ShowDialog();
        }
        private void تقريرمبيعاتالورديهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Shiftfilter SFF = new Shiftfilter();
            //SFF.Show();
        }
        private void تقريرمختصرمبيعاتالورديهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Productshifftsales PSS = new Productshifftsales();
            //PSS.Show();
        }
        private void قائمةالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmbilllist FBL = new Frmbilllist();
            FBL.Show();
            //FBL.Cashier = Username;
            //FBL.Cashid = StaffID;
            //Frmbilllist open = Application.OpenForms["Frmbilllist"] as Frmbilllist;
            //if (open == null)
            //{
            //    FBL.Show();
            //}
            //else
            //{
            //    open.Activate();
            //    if (open.WindowState == FormWindowState.Minimized)
            //    {
            //        open.WindowState = FormWindowState.Normal;
            //    }
            //}
        }
        private void المنتجاتوالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aboutproducts AP = new Aboutproducts();
            //AP.Show();
        }
        private void تصنيفالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmproductitemlist PI = new frmproductitemlist();
            PI.Show();
        }
        private void تقريرمبيعاتاصنافاوليهللورديToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aboutitems AP = new Aboutitems();
            //AP.Show();
        }
        private void حركةالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Itemsaccount IC = new Itemsaccount();
            //IC.Show();
        }
        private void Btnsv_Click_1(object sender, EventArgs e)
        {

        }
        private void تقريرالمبيعاتToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Salesfiltrations SF = new Salesfiltrations();
            SF.Show();
        }
        private void Btnreportsales_Click(object sender, EventArgs e)
        {
            Salesfiltrations SF = new Salesfiltrations();
            SF.Show();
        }
        private void Restreport_Click(object sender, EventArgs e)
        {
            Reportrestfiltrations RRF = new Reportrestfiltrations();
            RRF.Show();
        }
        private void Btntail_Click(object sender, EventArgs e)
        {
            Frmtailoring FT = new Frmtailoring();
            FT.Show();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Frmprice FP = new Frmprice();
            //FP.St = Username;
            FP.Show();
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Frmpricelist FPL = new Frmpricelist();
            //FPL.Cashiername = Username;
            FPL.Show();
        }
        private void تقريرالمشترياتToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
        private void حركةالاصنافToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //Newtailor FNT = new Newtailor();
            //FNT.staffid = StaffID;
            //FNT.Show();
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            //Frmemployee FEmp = new Frmemployee();
            //FEmp.ShowDialog();
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //Frmfabric FF = new Frmfabric();
            //FF.ShowDialog();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            //Frmtailortype FTT = new Frmtailortype();
            //FTT.ShowDialog();
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //Frmfabrictype FFT = new Frmfabrictype(null);
            //FFT.ShowDialog();
        }
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            //Frmtailorlist FTL = new Frmtailorlist();
            //FTL.Show();
        }
        private void metroTile1_Click(object sender, EventArgs e)
        {
            FrmExpencetype FET = new FrmExpencetype();
            FET.Show();
        }
        private void تصنيفالاصنافToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Listofstok FLS = new Listofstok();
            FLS.Show();
        }
    }
}
