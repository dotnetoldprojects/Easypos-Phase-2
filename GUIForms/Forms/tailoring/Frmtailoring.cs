
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Centeralized;
using Domain.Models;
using Easypos.Masters;
using Easypos.Masters.Subforms;
using Easypos.Salesforms;
using Easypos.Salesforms.Cashier;
using GUIForms.Dtos;
using GUIForms.helpers;
using GUIForms.models;
using Microsoft.VisualBasic;
using net.sf.saxon;
using Reporting;
using Reporting.tailor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using UOW;
using static iText.Kernel.Pdf.Colorspace.PdfDeviceCs;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Application = System.Windows.Forms.Application;
using Dataset = Centeralized.Dataset;
using Image = System.Drawing.Image;

namespace Easypos.Tailoring
{
    public partial class Frmtailoring : Form
    {
        company DC;
        Getcentralaizes GC;
        IUnitofwork _IUW;
        tailorheader TH;
        tailorhand THN;
        tailorjabzor TJ;
        tailorneck TN;
        tailorbocket TBD;
        tailordetaile TD;
        alltailoring AT;
        Usingnumber _NO;
        public int TilId { get; set; }
        public Guid TilHId { get; set; }
        public Guid TilDId { get; set; }
        public Frmtailoring()
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
            TilHId = Guid.NewGuid();
            TilDId = Guid.NewGuid();
            TH = new tailorheader();
            THN = new tailorhand();
            TJ = new tailorjabzor();
            TN = new tailorneck();
            TBD = new tailorbocket();
            TD = new tailordetaile();
            AT = new alltailoring();
            Statustype.SelectedIndex = 0;
            _NO = new Usingnumber();
        }
        private void LoadAllCombos()
        {
            Commondatasales.FillCombo(clientID, GC.Getcustomerdatalist(), "Name", "ID");
        }
        public void Clearfildes()
        {
            LoadAllCombos();
            dgw.Rows.Clear();
            Statustype.SelectedIndex = 0;
            Btnsave.Text = "حفظ";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox15.Clear();
            textBox29.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox25.Clear();
            textBox24.Clear();
            textBox23.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox28.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox23.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox31.Checked = false;
            checkBox32.Checked = false;
            checkBox33.Checked = false;
            checkBox34.Checked = false;
            checkBox35.Checked = false;
            checkBox36.Checked = false;
            checkBox37.Checked = false;
            checkBox38.Checked = false;
            checkBox39.Checked = false;
        }
        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Btnclear_Click(object sender, EventArgs e)
        {
            Clearfildes();
        }
        private void Btnreport_Click(object sender, EventArgs e)
        {
            TFR Frmtrf = new TFR();
            Frmtrf.clientID.SelectedValue = clientID.SelectedValue;
            Frmtrf.ShowDialog();
        }
        private void Btnbull_Click(object sender, EventArgs e)
        {
            frmPOS FPos = new frmPOS();
            FPos.Show();
            FPos.clientID.SelectedValue = clientID.SelectedValue;
            FPos.dateTimePicker2.Value = dateTimePicker1.Value;
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            var data = GC.Getcustomerdatalist().ToList();
            var Res = data.Where(x => x.MobileNumber.Contains(textBox15.Text)).FirstOrDefault();
            if (Res != null) { 
                clientID.SelectedValue = Res.ID;
                var deatile = _IUW.tailorheaders.GetAll().Where(x => x.thirdparty.ID == Res.ID).FirstOrDefault();
                if (deatile != null)
                {
                    Getingdata(deatile.Id.ToString());
                }
            }
            else
            {
                LoadAllCombos();
            }
        }
        private void Btncustomers_Click(object sender, EventArgs e)
        {
            frmListThirdParty FTP = new frmListThirdParty();
            FTP.radioClient.Checked = true;
            FTP.Show();
        }
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            Frmclient open = Application.OpenForms["Frmclient"] as Frmclient;
            if (open == null)
            {
                Frmclient FC = new Frmclient();
                FC.Show();
            }
            else
            {
                open.Activate();
                if (open.WindowState == FormWindowState.Minimized)
                {
                    open.WindowState = FormWindowState.Normal;
                }
            }
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (clientID.Text == "--اختر--" || clientID.Text == "")
            {
                MessageBox.Show("برجاء اختيار العميل", "خطأ");
                return;
            }
            else
            {
                try
                {
                    tailorheadersave();
                    tailorhandsave();
                    tailornecksave();
                    tailorjabzorsave();
                    tailorboketsave();
                    tailordeatailesave();
                    if (Btnsave.Text == "حفظ")
                    {
                        if (TilHId == null)
                        {
                            _IUW.tailorheaders.Insert(TH);
                        }
                        _IUW.tailorhands.Insert(THN);
                        _IUW.tailornecks.Insert(TN);
                        _IUW.tailorjabzors.Insert(TJ);
                        _IUW.tailorbockets.Insert(TBD);
                        _IUW.tailordetailes.Insert(TD);
                        Alltailorsave();
                        MessageBox.Show("تم الحفظ بنجاح", "اضافة امر تفصيل");
                    }
                    else
                    {
                        if (TilId == 0)
                        {
                            MessageBox.Show("برجاء اختيار الامر", "خطأ");
                            return;
                        }
                        else
                        {
                            _IUW.tailorheaders.Update(TH);
                            _IUW.tailorhands.Update(THN);
                            _IUW.tailornecks.Update(TN);
                            _IUW.tailorjabzors.Update(TJ);
                            _IUW.tailorbockets.Update(TBD);
                            _IUW.tailordetailes.Update(TD);
                            MessageBox.Show("تم التعديل بنجاح", "تعديل امر تفصيل");
                        }
                    }
                    _IUW.Complete();
                }
                catch (Exception ex)
                {
                    var logger = new ExceptionLogger(_IUW);
                    logger.Log(ex, "Tailoring");
                }
                Clearfildes();
                dgw.Rows.Clear();
                Btnsave.Text = "حفظ";
            }
        }
        void tailorheadersave()
        {
            TH.Id = TilHId.ToString();
            TH.Custid = int.Parse(clientID.SelectedValue.ToString());
            TH.Date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            TH.Clothesnumber = int.Parse(textBox22.Text);
            TH.Clothesrecived = int.Parse(textBox24.Text);
            TH.Clothesremining = int.Parse(textBox25.Text);
            TH.Total = int.Parse(textBox25.Text);
            TH.Paied = decimal.Parse(textBox16.Text);
            //TH.Totalreimining = decimal.Parse(textBox21.Text);
            TH.Totalreimining = 0;
            TH.Reciveddate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            TH.Status = Statustype.Text;
            TH.Note = textBox23.Text;
            
        }
        void tailorhandsave()
        {
            THN.Id = TilDId;
            THN.Texta = textBox22.Text;
            THN.Taxtb = textBox19.Text;
            THN.Inverted = checkBox1.Checked;
            THN.Square = checkBox2.Checked;
            THN.Horny = checkBox10.Checked;
            THN.Round = checkBox12.Checked;
            THN.Plain = checkBox3.Checked;
            THN.Plain_cupcake = checkBox11.Checked;
            THN.Triangle = checkBox9.Checked;
        }
        void tailornecksave()
        {
            TN.Id = TilDId;
            TN.Plain = checkBox16.Checked;
            TN.Plainfrench = checkBox14.Checked;
            TN.Regulartipper = checkBox19.Checked;
            TN.Closedflap = checkBox28.Checked;
            TN.Plainhidden = checkBox15.Checked;
            TN.Chinese = checkBox25.Checked;
            TN.Openflap = checkBox18.Checked;
            TN.Frenchflip = checkBox17.Checked;
        }
        void tailorjabzorsave()
        {
            TJ.Id = TilDId;
            TJ.Jab1 = checkBox20.Checked;
            TJ.Jab2 = checkBox24.Checked;
            TJ.Jab3 = checkBox8.Checked;
            TJ.Jab4 = checkBox27.Checked;
            TJ.Jab5 = checkBox22.Checked;
            TJ.Jab6 = checkBox21.Checked;
            TJ.Jab7 = checkBox26.Checked;
            TJ.Jab8 = checkBox7.Checked;
            TJ.Jab9 = checkBox6.Checked;
            TJ.Jab10 = checkBox5.Checked;
        }
        void tailorboketsave()
        {
            TBD.Id = TilDId;
            TBD.Bok1 = checkBox36.Checked;
            TBD.Bok2 = checkBox32.Checked;
            TBD.Bok3 = checkBox35.Checked;
            TBD.Bok4 = checkBox30.Checked;
            TBD.Bok5 = checkBox34.Checked;
            TBD.Bok6 = checkBox31.Checked;
            TBD.Bok7 = checkBox33.Checked;
            TBD.Bok8 = checkBox29.Checked;
            TBD.Bok9 = checkBox37.Checked;
            TBD.Bok10 = checkBox38.Checked;
            TBD.Bok11 = checkBox39.Checked;
        }
        void tailordeatailesave()
        {
            TD.Id = TilDId;
            TD.Frontlength = textBox1.Text;
            TD.Backlength = textBox26.Text;
            TD.Shoulder = textBox2.Text;
            TD.Handlength = textBox6.Text;
            TD.Neck = textBox3.Text;
            TD.Breadth = textBox5.Text;
            TD.Chest = textBox4.Text;
            TD.Handwidthabove = textBox7.Text;
            TD.Handwidthbelow = textBox27.Text;
            TD.Breadthbelow = textBox8.Text;
            TD.Handpalm = textBox28.Text;
            TD.Weight = textBox29.Text;
            TD.Notes = textBox18.Text;
            TD.Aujra = checkBox23.Checked;
        }
        void Alltailorsave() {
            AT.Tailheaderid = TilHId.ToString();
            AT.Alltable = TilDId;
            _IUW.alltailorings.Insert(AT);
        }
        private void clientID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var Res = int.Parse(clientID.SelectedValue.ToString());
            var deatile = _IUW.tailorheaders.GetAll().Where(x => x.Custid == Res).FirstOrDefault();
            if (deatile != null) {
                TilHId = Guid.Parse(deatile.Id);
                Getingdata(deatile.Id);
            }
        }
        void Getingdata(string GU)
        {
            var Query = _IUW.alltailorings.GetQueryable()
                                         .Include(x => x.tailorheader)
                                         .Where(x => x.Tailheaderid == GU.ToString())
                                         .Select(x => new
                                         {
                                             Id = x.Alltable,
                                             Date = x.tailorheader.Date,
                                             Custname = x.tailorheader.thirdparty.Name,
                                             Clothesnumber = x.tailorheader.Clothesnumber,
                                             Clothesremining = x.tailorheader.Clothesremining,
                                             Total = x.tailorheader.Total,
                                             Paied = x.tailorheader.Paied,
                                             Totalremining = 0,
                                             Recivrddate = x.tailorheader.Reciveddate,
                                             Status = x.tailorheader.Status,
                                             Note = x.tailorheader.Note,
                                         })
                                         .ToList();
            BindingSource bs = new BindingSource();
            bs.DataSource = Query;
            dgw.DataSource = bs;
        }
        void Getheaderdata(tailorheader TH)
        {
            //Clearfildes();
            //TilHId = TH.Id;
            clientID.SelectedValue = TH.Custid;
            dateTimePicker1.Text = TH.Date;
            textBox22.Text = TH.Clothesnumber.ToString();
            textBox24.Text = TH.Clothesrecived.ToString();
            textBox25.Text = TH.Clothesremining.ToString();
            textBox25.Text = TH.Total.ToString();
            textBox16.Text = TH.Paied.ToString();
            textBox21.Text = "0";
            dateTimePicker2.Text = TH.Reciveddate;
            Statustype.Text = TH.Status;
            textBox23.Text = TH.Note;
        }
        void Gethanddata(tailorhand THN)
        {
            textBox22.Text = THN.Texta;
            textBox19.Text = THN.Taxtb;
            checkBox1.Checked = (bool)THN.Inverted;
            checkBox2.Checked = (bool)THN.Square;
            checkBox10.Checked = (bool)THN.Horny;
            checkBox12.Checked = (bool)THN.Round;
            checkBox3.Checked = (bool)THN.Plain;
            checkBox11.Checked = (bool)THN.Plain_cupcake;
            checkBox9.Checked = (bool)THN.Triangle;
        }
        void Getneckdata(tailorneck TN)
        {
            checkBox16.Checked = (bool)TN.Plain;
            checkBox14.Checked = (bool)TN.Plainfrench;
            checkBox19.Checked  = (bool)TN.Regulartipper;
            checkBox28.Checked = (bool)TN.Closedflap;
            checkBox15.Checked = (bool)TN.Plainhidden;
            checkBox25.Checked = (bool)TN.Chinese;
            checkBox18.Checked = (bool)TN.Openflap;
            checkBox17.Checked = (bool)TN.Frenchflip;
        }
        void Getjabzordata(tailorjabzor TJ)
        {
            checkBox20.Checked = (bool)TJ.Jab1;
            checkBox24.Checked = (bool)TJ.Jab2;
            checkBox8.Checked = (bool)TJ.Jab3;
            checkBox27.Checked = (bool)TJ.Jab4; 
            checkBox22.Checked = (bool)TJ.Jab5;
            checkBox21.Checked = (bool)TJ.Jab6;
            checkBox26.Checked = (bool)TJ.Jab7;
            checkBox7.Checked = (bool)TJ.Jab8;
            checkBox6.Checked = (bool)TJ.Jab9;
            checkBox5.Checked = (bool)TJ.Jab10; 
        }
        void Getbocketdata(tailorbocket TB)
        {
            // Fix for CS0266: Explicitly cast 'bool?' to 'bool' using the null-coalescing operator to handle null values.
            checkBox36.Checked = TBD.Bok1 ?? false;
            checkBox32.Checked = TBD.Bok2 ?? false;
            checkBox35.Checked = TBD.Bok3 ?? false;
            checkBox30.Checked = TBD.Bok4 ?? false;
            checkBox34.Checked = TBD.Bok5 ?? false;
            checkBox31.Checked = TBD.Bok6 ?? false;
            checkBox33.Checked = TBD.Bok7 ?? false;
            checkBox29.Checked = TBD.Bok8 ?? false;
            checkBox37.Checked = TBD.Bok9 ?? false;
            checkBox38.Checked = TBD.Bok10 ?? false;
            checkBox39.Checked = TBD.Bok11 ?? false;
        }
        void Gettailordeatail(tailordetaile TD)
        {
            textBox1.Text = TD.Frontlength;
            textBox26.Text = TD.Backlength;
            textBox2.Text = TD.Shoulder;
            textBox6.Text = TD.Handlength;
            textBox3.Text = TD.Neck;
            textBox5.Text = TD.Breadth;
            textBox4.Text = TD.Chest;
            textBox7.Text = TD.Handwidthabove;
            textBox27.Text = TD.Handwidthbelow;
            textBox8.Text = TD.Breadthbelow;
            textBox28.Text = TD.Handpalm;
            textBox29.Text = TD.Weight;
            textBox18.Text = TD.Notes;
            checkBox23.Checked = (bool)TD.Aujra;
        }
        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Btnsave.Text = "تعديل";
            if (dgw.Rows.Count > 0)
            {
                var Id = dgw.CurrentRow.Cells[1].Value.ToString();

                // أولًا: استرجاع الكيان من قاعدة البيانات
                var entity = _IUW.alltailorings.GetQueryable()
                                               .Include(x => x.tailorheader)
                                               .Where(x => x.Alltable.Value.ToString() == Id).FirstOrDefault();

                // ثانيًا: إنشاء كائن tailorheader في الذاكرة
                tailorheader dataheader = null;
                if (entity != null && entity.tailorheader != null)
                {
                    dataheader = new tailorheader
                    {
                        Date = entity.tailorheader.Date,
                        Custid = entity.tailorheader.thirdparty.ID,
                        Clothesnumber = entity.tailorheader.Clothesnumber,
                        Clothesremining = entity.tailorheader.Clothesremining,
                        Total = entity.tailorheader.Total,
                        Paied = entity.tailorheader.Paied,
                        Status = entity.tailorheader.Status,
                        Note = entity.tailorheader.Note,
                    };
                }

                tailorhand datahand = null;
                if (entity != null && entity.tailorhand != null)
                {
                    datahand = new tailorhand
                    {
                        Texta = entity.tailorhand.Texta,
                        Taxtb = entity.tailorhand.Taxtb,
                        Inverted = entity.tailorhand.Inverted,
                        Square = entity.tailorhand.Square,
                        Horny = entity.tailorhand.Horny,
                        Round = entity.tailorhand.Round,
                        Plain = entity.tailorhand.Plain,
                        Plain_cupcake = entity.tailorhand.Plain_cupcake,
                        Triangle = entity.tailorhand.Triangle
                    };
                }

                tailorneck dataneck = null;
                if (entity != null && entity.tailorneck != null)
                {
                    dataneck = new tailorneck
                    {
                        Plain = entity.tailorneck.Plain,
                        Plainfrench = entity.tailorneck.Plainfrench,
                        Regulartipper = entity.tailorneck.Regulartipper,
                        Closedflap = entity.tailorneck.Closedflap,
                        Plainhidden = entity.tailorneck.Plainhidden,
                        Chinese = entity.tailorneck.Chinese,
                        Openflap = entity.tailorneck.Openflap,
                        Frenchflip = entity.tailorneck.Frenchflip,
                    };
                }

                tailorjabzor datajabzor = null;
                if (entity != null && entity.tailorjabzor != null)
                {
                    datajabzor = new tailorjabzor
                    {
                        Jab1 = entity.tailorjabzor.Jab1,
                        Jab2 = entity.tailorjabzor.Jab2,
                        Jab3 = entity.tailorjabzor.Jab3,
                        Jab4 = entity.tailorjabzor.Jab4,
                        Jab5 = entity.tailorjabzor.Jab5,
                        Jab6 = entity.tailorjabzor.Jab6,
                        Jab7 = entity.tailorjabzor.Jab7,
                        Jab8 = entity.tailorjabzor.Jab8,
                        Jab9 = entity.tailorjabzor.Jab9,
                        Jab10 = entity.tailorjabzor.Jab10,
                    };
                }

                tailorbocket tailorbocket = null;
                if (entity != null && entity.tailorbocket != null)
                {
                    tailorbocket = new tailorbocket
                    {
                        Bok1 = entity.tailorbocket.Bok1,
                        Bok2 = entity.tailorbocket.Bok2,
                        Bok3 = entity.tailorbocket.Bok3,
                        Bok4 = entity.tailorbocket.Bok4,
                        Bok5 = entity.tailorbocket.Bok5,
                        Bok6 = entity.tailorbocket.Bok6,
                        Bok7 = entity.tailorbocket.Bok7,
                        Bok8 = entity.tailorbocket.Bok8,
                        Bok9 = entity.tailorbocket.Bok9,
                        Bok10 = entity.tailorbocket.Bok10,
                        Bok11 = entity.tailorbocket.Bok11,
                    };
                }

                tailordetaile tailordetaile = null;
                if (entity != null && entity.tailordetaile != null)
                {
                    tailordetaile = new tailordetaile
                    {
                        Frontlength = entity.tailordetaile.Frontlength,
                        Backlength = entity.tailordetaile.Backlength,
                        Shoulder = entity.tailordetaile.Shoulder,
                        Handlength = entity.tailordetaile.Handlength,
                        Neck = entity.tailordetaile.Neck,
                        Breadth = entity.tailordetaile.Breadth,
                        Chest = entity.tailordetaile.Chest,
                        Handwidthabove = entity.tailordetaile.Handwidthabove,
                        Handwidthbelow = entity.tailordetaile.Handwidthbelow,
                        Breadthbelow = entity.tailordetaile.Breadthbelow,
                        Handpalm = entity.tailordetaile.Handpalm,
                        Weight = entity.tailordetaile.Weight,
                        Notes = entity.tailordetaile.Notes,
                        Aujra = entity.tailordetaile.Aujra,
                    };
                }

                Getheaderdata(dataheader);
                Gethanddata(datahand);
                Getneckdata(dataneck);
                Getjabzordata(datajabzor);
                Getbocketdata(tailorbocket);
                Gettailordeatail(tailordetaile);
            }
        }
        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Columns[e.ColumnIndex].Name == "Delete")
            {
                var Id = dgw.CurrentRow.Cells[1].Value.ToString();
                if (Id == "")
                {
                    MessageBox.Show("برجاء اختيار الامر", "خطأ");
                    return;
                }
                else
                {
                    var Res = MessageBox.Show("هل تريد حذف هذا الامر", "حذف الامر", MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        var data = _IUW.alltailorings.GetAll().Where(x => x.Alltable.Value.ToString() == Id).ToList();
                        foreach (var item in data)
                        {
                            if (item.tailorheader != null)
                            {
                                _IUW.tailorheaders.Delete(item.tailorheader);
                            }
                            if (item.tailorhand != null)
                            {
                                _IUW.tailorhands.Delete(item.tailorhand);
                            }
                            if (item.tailorneck != null)
                            {
                                _IUW.tailornecks.Delete(item.tailorneck);
                            }
                            if (item.tailorjabzor != null)
                            {
                                _IUW.tailorjabzors.Delete(item.tailorjabzor);
                            }
                            if (item.tailorbocket != null)
                            {
                                _IUW.tailorbockets.Delete(item.tailorbocket);
                            }
                            if (item.tailordetaile != null)
                            {
                                _IUW.tailordetailes.Delete(item.tailordetaile);
                            }
                        }
                        _IUW.Complete();
                        Clearfildes();
                        dgw.Rows.Clear();
                        Btnsave.Text = "حفظ";
                        MessageBox.Show("تم الحذف بنجاح", "حذف امر تفصيل");
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private byte[] ConvertImageToByteArray(Image image)
        {
            if (image == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dataset ds = new Dataset();

            //تفاصيل الرأس
            var Date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            var Custnumber = textBox15.Text;
            var Custname = clientID.Text;
            // اليد
            //كبك مقلوب
            byte[] UDH = new byte[0];
            if (checkBox1.Checked)
            {
                UDH = ConvertImageToByteArray(pictureBox1.Image);
                ds.Hand.Rows.Add(UDH);
            }
            //كبك مربع
            byte[] SH = new byte[0];
            if (checkBox2.Checked)
            {
                SH = ConvertImageToByteArray(pictureBox3.Image);
                ds.Hand.Rows.Add(SH);
            }
            // كبك مقرن
            byte[] CH = new byte[0];
            if (checkBox10.Checked)
            {
                CH = ConvertImageToByteArray(pictureBox5.Image);
                ds.Hand.Rows.Add(CH);
            }
            // كبك مدور
            byte[] RH = new byte[0];
            if (checkBox12.Checked)
            {
                RH = ConvertImageToByteArray(pictureBox6.Image);
                ds.Hand.Rows.Add(RH);
            }
            //يد كبك ساده
            byte[] PH = new byte[0];
            if (checkBox3.Checked)
            {
                PH = ConvertImageToByteArray(pictureBox4.Image);
                ds.Hand.Rows.Add(PH);
            }
            // يد ساده
            byte[] RGH = new byte[0];
            if (checkBox11.Checked)
            {
                RGH = ConvertImageToByteArray(pictureBox7.Image);
                ds.Hand.Rows.Add(RGH);
            }
            // يد مثلث
            byte[] TH = new byte[0];
            if (checkBox9.Checked)
            {
                TH = ConvertImageToByteArray(pictureBox8.Image);
                ds.Hand.Rows.Add(TH);
            }


            // الجبزور
            byte[] FJ = new byte[0];
            byte[] SJ = new byte[0];
            byte[] TJ = new byte[0];
            byte[] FOJ = new byte[0];
            byte[] FIJ = new byte[0];
            byte[] SIJ = new byte[0];
            byte[] SVJ = new byte[0];
            byte[] EJ = new byte[0];
            byte[] NJ = new byte[0];
            byte[] TIJ = new byte[0];
            if (checkBox20.Checked)
            {
                FJ = ConvertImageToByteArray(pictureBox9.Image);
                ds.Jabzor.Rows.Add(FJ);
            }
            if (checkBox24.Checked)
            {
                SJ = ConvertImageToByteArray(pictureBox10.Image);
                ds.Jabzor.Rows.Add(SJ);
            }
            if (checkBox8.Checked)
            {
                TJ = ConvertImageToByteArray(pictureBox11.Image);
                ds.Jabzor.Rows.Add(TJ);
            }
            if (checkBox27.Checked)
            {
                FOJ = ConvertImageToByteArray(pictureBox12.Image);
                ds.Jabzor.Rows.Add(FOJ);
            }
            if (checkBox22.Checked)
            {
                FIJ = ConvertImageToByteArray(pictureBox13.Image);
                ds.Jabzor.Rows.Add(FIJ);
            }
            if (checkBox21.Checked)
            {
                SIJ = ConvertImageToByteArray(pictureBox14.Image);
                ds.Jabzor.Rows.Add(SIJ);
            }
            if (checkBox26.Checked)
            {
                SVJ = ConvertImageToByteArray(pictureBox15.Image);
                ds.Jabzor.Rows.Add(SVJ);
            }
            if (checkBox7.Checked)
            {
                EJ = ConvertImageToByteArray(pictureBox16.Image);
                ds.Jabzor.Rows.Add(EJ);
            }
            if (checkBox6.Checked)
            {
                NJ = ConvertImageToByteArray(pictureBox17.Image);
                ds.Jabzor.Rows.Add(NJ);
            }
            if (checkBox5.Checked)
            {
                TIJ = ConvertImageToByteArray(pictureBox18.Image);
                ds.Jabzor.Rows.Add(TIJ);
            }

            // الرقبه
            byte[] PN = new byte[0];
            byte[] PHN = new byte[0];
            byte[] PFN = new byte[0];
            byte[] CN = new byte[0];
            byte[] RT = new byte[0];
            byte[] OF = new byte[0];
            byte[] CF = new byte[0];
            byte[] FFF = new byte[0];
            if (checkBox16.Checked)
            {
                PN = ConvertImageToByteArray(pictureBox19.Image);
                ds.Neck.Rows.Add(PN);
            }
            if (checkBox14.Checked)
            {
                PHN = ConvertImageToByteArray(pictureBox23.Image);
                ds.Neck.Rows.Add(PHN);
            }
            if (checkBox19.Checked)
            {
                PFN = ConvertImageToByteArray(pictureBox20.Image);
                ds.Neck.Rows.Add(PFN);
            }
            if (checkBox28.Checked)
            {
                CN = ConvertImageToByteArray(pictureBox25.Image);
                ds.Neck.Rows.Add(CN);
            }
            if (checkBox15.Checked)
            {
                RT = ConvertImageToByteArray(pictureBox21.Image);
                ds.Neck.Rows.Add(RT);
            }
            if (checkBox25.Checked)
            {
                OF = ConvertImageToByteArray(pictureBox24.Image);
                ds.Neck.Rows.Add(OF);
            }
            if (checkBox18.Checked)
            {
                CF = ConvertImageToByteArray(pictureBox22.Image);
                ds.Neck.Rows.Add(CF);
            }
            if (checkBox17.Checked)
            {
                FFF = ConvertImageToByteArray(pictureBox26.Image);
                ds.Neck.Rows.Add(FFF);
            }

            // الجيب
            byte[] Ba = new byte[0];
            byte[] Bb = new byte[0];
            byte[] Bc = new byte[0];
            byte[] Bd = new byte[0];
            byte[] Be = new byte[0];
            byte[] Bf = new byte[0];
            byte[] Bg = new byte[0];
            byte[] Bh = new byte[0];
            byte[] Bi = new byte[0];
            byte[] Bj = new byte[0];
            byte[] Bk = new byte[0];
            if (checkBox36.Checked)
            {
                Ba = ConvertImageToByteArray(pictureBox34.Image);
                ds.Boket.Rows.Add(Ba);
            }
            if (checkBox32.Checked)
            {
                Bb = ConvertImageToByteArray(pictureBox30.Image);
                ds.Boket.Rows.Add(Bb);
            }
            if (checkBox35.Checked)
            {
                Bc = ConvertImageToByteArray(pictureBox33.Image);
                ds.Boket.Rows.Add(Be);
            }
            if (checkBox30.Checked)
            {
                Bd = ConvertImageToByteArray(pictureBox28.Image);
                ds.Boket.Rows.Add(Bd);
            }
            if (checkBox34.Checked)
            {
                Be = ConvertImageToByteArray(pictureBox32.Image);
                ds.Boket.Rows.Add(Be);
            }
            if (checkBox31.Checked)
            {
                Bf = ConvertImageToByteArray(pictureBox29.Image);
                ds.Boket.Rows.Add(Bf);
            }
            if (checkBox33.Checked)
            {
                Bg = ConvertImageToByteArray(pictureBox31.Image);
                ds.Boket.Rows.Add(Bg);
            }
            if (checkBox29.Checked)
            {
                Bh = ConvertImageToByteArray(pictureBox27.Image);
                ds.Boket.Rows.Add(Bh);
            }
            if (checkBox37.Checked)
            {
                Bi = ConvertImageToByteArray(pictureBox35.Image);
                ds.Boket.Rows.Add(Bi);
            }
            if (checkBox38.Checked)
            {
                Bj = ConvertImageToByteArray(pictureBox36.Image);
                ds.Boket.Rows.Add(Bj);
            }
            if (checkBox39.Checked)
            {
                Bk = ConvertImageToByteArray(pictureBox37.Image);
                ds.Boket.Rows.Add(Bk);
            }

            Tialorder CRA = new Tialorder();
            Frmreporting FCCR = new Frmreporting();
            ds.Custordertile.Rows.Add(new object[] { Date, Custnumber, Custname });
            ds.Detaileordertile.Rows.Add(new object[] {
                textBox1.Text, textBox26.Text,textBox2.Text,
                textBox6.Text,textBox3.Text, textBox5.Text,
                textBox4.Text, textBox7.Text,textBox27.Text,
                textBox8.Text, textBox28.Text,textBox29.Text,
            });
            CRA.SetDataSource(ds);

            CRA.SetParameterValue("CompanyName", DC.Name);
            CRA.SetParameterValue("Taxnum", DC.Taxnumber);
            CRA.SetParameterValue("Proname", DC.CRN);
            CRA.SetParameterValue("English_Shop_name", DC.ENName);
            FCCR.CRV.ReportSource = CRA;
            FCCR.CRV.Refresh();
            FCCR.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            _NO.Usenumber(sender,e);
        }
    }
}
