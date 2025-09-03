using Domain.Models;
using GUIForms.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UOW;

namespace GUIForms.Dtos
{
    public class Getcentralaizes
    {
        IUnitofwork _IUOW;
        public Getcentralaizes()
        {
            _IUOW = new Unitofwork(new EasyposEntities());
        }
        public object Getcompanydatalist()
        {
           return _IUOW.companies.GetAll().FirstOrDefault();
        }
        public List<unittype> Getunittypedatalist()
        {
            return _IUOW.unittypes.GetAll().ToList();
        }
        public List<category> Getcategorydatalist()
        {
            return _IUOW.categories.GetAll().ToList();
        }
        public List<product> Getproductdatalist()
        {
            return _IUOW.products.GetAll().ToList();
        }
        public List<thirdparty> Getthirdpartydatalist()
        {
            return _IUOW.thirdparties.GetAll().ToList();
        }
        public List<thirdparty> Getsupplierdatalist()
        {
            return _IUOW.thirdparties.GetAll().Where(x => x.Type == 1).ToList();
        }
        public thirdparty Getbyphonrdatalist(string phone)
        {
            return _IUOW.thirdparties.GetAll()
             .FirstOrDefault(x => x.MobileNumber.Contains(phone));
        }
        public List<thirdparty> Getcustomerdatalist()
        {
            return _IUOW.thirdparties.GetAll().Where(x => x.Type == 2).ToList();
        }
        public List<ItemsViewModel> GetItemsdatalist()
        {
            var items = _IUOW.items.GetAll().ToList();
            var unitTypes = _IUOW.unittypes.GetAll().ToList();
            var list = (from i in items
                    join u in unitTypes on i.UID equals u.ID
                    select new ItemsViewModel
                    {
                        ID = i.ID,
                        Itemname = i.Itemname,
                        Itemprice = i.Itemprice,
                        Itemqty = i.Itemqty,
                        OpeningBalance = i.OpeningBalance,
                        UnitName = u.UName,
                        Unitid = i.UID
                    }).ToList();
            return list;
        }
        public XmlDocument LoadInvoiceFromString(string xmlContent)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            return doc;
        }
        public List<dynamic> LoadAccounting(int Thirdid,string DFT, string DTT, string TT)
        {
            DateTime DF = DateTime.Parse(DFT);
            DateTime DT = DateTime.Parse(DTT);
            List<dynamic> Res = new List<dynamic>();
            List<payment> PL = new List<payment>();
            List<paymentout> PO = new List<paymentout>();
            List<sale> SL = new List<sale>();
            List<purchase> Pur = new List<purchase>();
            if (TT == "2")
            {
                PL = _IUOW.payments.GetAll().ToList();
                SL = _IUOW.sales.GetAll().ToList();
                Res = (from t in _IUOW.transactions.GetAll().ToList()
                              where t.ThirdPartyID == Thirdid &&
                                    DateTime.Parse(t.TDate) >= DF && DateTime.Parse(t.TDate) <= DT
                                    //&& t.Type != "ايرادات اخرى"
                              join tp in _IUOW.thirdparties.GetAll().ToList() on t.ThirdPartyID equals tp.ID into tpJoin
                              from tp in tpJoin.DefaultIfEmpty()

                              join p in PL on t.Paynum equals p.paymentNo into pJoin
                              from p in pJoin.DefaultIfEmpty()

                              join s in SL on t.Invoiceno equals s.Invoiceno into sJoin
                              from s in sJoin.DefaultIfEmpty()

                              select new
                              {
                                  ID = t.ID,
                                  Name = tp.Name,
                                  MobileNumber = tp.MobileNumber,
                                  Address = tp.Address,
                                  Taxnumber = tp.Taxnumber,
                                  Type = t.Type,
                                  Paynum = t.Paynum,
                                  InvoiceNo = t.Invoiceno,
                                  TDate = t.TDate,
                                  ThirdPartyID = t.ThirdPartyID,
                                  TotalAmount = s?.TotalAmount,
                                  Paid = t.Paid,
                                  Remaining = p?.Remaining ?? 0,
                              }).ToList().Cast<dynamic>().ToList();
            }
            else
            {
                PO = _IUOW.paymentouts.GetAll().ToList();
                Pur = _IUOW.purchases.GetAll().ToList();
                Res = (from t in _IUOW.transactions.GetAll().ToList()
                       where t.ThirdPartyID == Thirdid &&
                             DateTime.Parse(t.TDate) >= DF && DateTime.Parse(t.TDate) <= DT
                       join tp in _IUOW.thirdparties.GetAll().ToList() on t.ThirdPartyID equals tp.ID into tpJoin
                       from tp in tpJoin.DefaultIfEmpty()

                       join p in PO on t.Paynum equals p.paymentNo into pJoin
                       from p in pJoin.DefaultIfEmpty()

                       join s in Pur on t.Invoiceno equals s.Invoiceno into sJoin
                       from s in sJoin.DefaultIfEmpty()

                       select new
                       {
                           ID = t.ID,
                           Name = tp.Name,
                           MobileNumber = tp.MobileNumber,
                           Address = tp.Address,
                           Taxnumber = tp.Taxnumber,
                           Type = t.Type,
                           Paynum = t.Paynum,
                           InvoiceNo = t.Invoiceno,
                           TDate = t.TDate,
                           ThirdPartyID = t.ThirdPartyID,
                           TotalAmount = s?.TotalAmount,
                           Paid = t.Paid,
                           Remaining = p?.Remaining ?? 0,
                       }).ToList().Cast<dynamic>().ToList();
            }

            return Res;
        }
        public List<expencestype> LaodETypes()
        {
            return _IUOW.expencestypes.GetAll().ToList();
        }
        public object GetBalance(int tid, string DFT, string TT)
        {
            decimal totalFinancial;
            if (TT == "2")
            {
                totalFinancial = _IUOW.payments.GetAll()
                                   .Where(p => p.ThirdPartyID == tid && DateTime.Parse(p.Date) < DateTime.Parse(DFT))
                                   .Sum(p => (decimal?)p.Remaining ?? 0);
            }
            else
            {
                totalFinancial = _IUOW.paymentouts.GetAll()
                                   .Where(p => p.ThirdPartyID == tid && DateTime.Parse(p.Date) < DateTime.Parse(DFT))
                                   .Sum(p => (decimal?)p.Remaining ?? 0);
            }
            return totalFinancial;
        }
        public IEnumerable<object> GetProductItems(string proId)
        {
            var query = from pi in _IUOW.productitems.GetAll()
                        join it in _IUOW.items.GetAll() on pi.itemid equals it.ID.ToString() into itGroup
                        from it in itGroup.DefaultIfEmpty()
                        where pi.Proid == proId
                        select new
                        {
                            ID = it?.ID,
                            Itemname = it?.Itemname,
                            Quantity = pi.Quantity
                        };

            return query.ToList();
        }
    }
}
