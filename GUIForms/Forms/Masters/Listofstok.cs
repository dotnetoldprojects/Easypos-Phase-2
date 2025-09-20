using CrystalDecisions.ReportAppServer;
using Domain;
using Domain.Models;
using Easypos.Masters;
using Easypos.Purchases;
using GUIForms.Dtos;
using GUIForms.models;
using javax.xml.transform;
using net.sf.saxon;
using net.sf.saxon.functions;
using Reporting;
using Reporting.others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOW;

namespace GUIForms.Forms.Masters
{
    public partial class Listofstok : Form
    {
        //"تعديل مخزون ناقص"
        //"تعديل مخزون زائد"
        public int itemid { get; set; }
        IUnitofwork _IUW;
        company DC;
        Getcentralaizes GC;
        frmproductitemlist PLI;
        public Listofstok()
        {
            InitializeComponent();
            GC = new Getcentralaizes();
            DC = (company)LanguageHelper.ApplyLanguage(this);
            _IUW = new Unitofwork(new EasyposEntities());
            PLI = (Application.OpenForms["frmproductitemlist"] as frmproductitemlist);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Listofstok_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RB1.Checked != true && RB2.Checked != true)
            {
                MessageBox.Show("برجاء اختيار النوع","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtStocksOnHand.Text))
            {
                MessageBox.Show("برجاء ادخال الكميه", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                invtransaction IT = new invtransaction();
                IT.Itemid = itemid;
                IT.Date = DateTime.Now.ToString("dd-MM-yyyy");
                IT.Quantity = Convert.ToInt32(txtStocksOnHand.Text);
                IT.Note = textBox1.Text;
                var ITS = _IUW.items.GetAll().Where(x => x.ID == itemid).FirstOrDefault();
                if (ITS != null)
                {
                    if (RB1.Checked)
                    {
                        ITS.Itemqty += IT.Quantity;
                        IT.type = "تعديل مخزون زائد";

                    }
                    else
                    {
                        ITS.Itemqty -= IT.Quantity;
                        IT.type = "تعديل مخزون ناقص";
                    }
                    _IUW.items.Update(ITS);
                }
                _IUW.invtransactions.Insert(IT);
                _IUW.Complete();
                PLI.Clearitems();
                Close();
            }
        }

        private void RB1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "تعديل مخزون زائد";
        }

        private void RB2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "تعديل مخزون ناقص";
        }
    }
}
