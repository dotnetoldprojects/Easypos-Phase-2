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
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using UOW;
using Domain.Models;

namespace Easypos
{

   

    public partial class frmLogin : Form
    {
        public const Int32 WM_USER = 1024;
        public const Int32 WM_CSKEYBOARD = WM_USER + 192;
        public const Int32 WM_CSKEYBOARDMOVE = WM_USER + 193;
        public const Int32 WM_CSKEYBOARDRESIZE = WM_USER + 197;

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern Int32 FindWindow(string _ClassName, string _WindowName);
        IUnitofwork _IUOW;

        [DllImport("User32.DLL")]
        public static extern Boolean PostMessage(Int32 hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        public frmLogin()
        {
            InitializeComponent();
            _IUOW = new Unitofwork(new EasyposEntities());
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            GetDeviceID();
        }
        private void Signin()
        {
            var log = _IUOW.staffs.Find(x => x.Username == txtusername.Text && x.UPassword == txtPassword.Text);
            if (log != null)
            {
                Frmindex m = new Frmindex();
                m.staffID = Convert.ToInt32(log.StaffID);
                m.username = log.Fullname;
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("يوجد خطا باسم المستخدم او كلمة المرور الرجاء المحاولة مرة اخرى.", "برنامج نقاط البيع", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Signin();
            }
        }
        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Signin();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Process[] Objprocess;
            Objprocess = System.Diagnostics.Process.GetProcessesByName("TabTip.exe");
            if (Objprocess.Count() > 0)
            {
                Objprocess[0].Kill();
            }
            Application.Exit();
        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                //frmDatabaseConfig dc = new frmDatabaseConfig();
                //dc.ShowDialog();
            }
        }
        byte TxtFlag=0;
        private void Button0_Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;
            String BtnChr=Btn.Name.Substring(Btn.Name.Length-1);                    
        }
        private void TxtPassword_OnEnter(object sender, EventArgs e)
        {
            TxtFlag = 2;
        }
        private void btnOkay_Click(object sender, EventArgs e)
        {
           Signin();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                System.DateTime Etime = DateTime.Parse("10:00");
                System.DateTime Stime = DateTime.Parse("00:00");
                if (System.DateTime.Now > Stime & System.DateTime.Now < Etime)
                {
                    MessageBox.Show("True");
                }
                else
                {
                    MessageBox.Show("False");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OnEnter(object sender, EventArgs e)
        {
            TxtFlag = 1;
        }
        private void GetDeviceID()
        {
            //try
            //{
            //    Devices DVS = new Devices();
            //    DVS.GetDevice();
            //}
            //catch (Exception ex)
            //{
            //    var Qurcat = @"Insert Into Exception(Exceptionname,Form,Note) value('" + "شاشة تسجيل الدخول" + "','" + "جلب الاجهزة" + "','" + ex.Message.ToString() + "')";
            //    var CMO = DAL.Crudopration(Qurcat);
            //}
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
