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
        public List<dynamic> LoadAccounting(int Thirdid,string DFT, string DTT)
        {
            DateTime DF = DateTime.Parse(DFT);
            DateTime DT = DateTime.Parse(DTT);
            var result = (from t in _IUOW.transactions.GetAll()
                          where t.ThirdPartyID == Thirdid &&
                                DateTime.Parse(t.TDate) >= DF && DateTime.Parse(t.TDate) <= DT
                          join tp in _IUOW.thirdparties.GetAll() on t.ThirdPartyID equals tp.ID into tpJoin
                          from tp in tpJoin.DefaultIfEmpty()

                          join p in _IUOW.payments.GetAll() on t.Paynum equals p.paymentNo into pJoin
                          from p in pJoin.DefaultIfEmpty()

                          join s in _IUOW.sales.GetAll() on t.Invoiceno equals s.Invoiceno into sJoin
                          from s in sJoin.DefaultIfEmpty()

                          select new
                          {
                              Name = tp.Name,
                              MobileNumber = tp.MobileNumber,
                              Address = tp.Address,
                              Taxnumber = tp.Taxnumber,
                              Type = t.Type,
                              Paynum = t.Paynum,
                              InvoiceNo = t.Invoiceno,
                              TDate = t.TDate,
                              ThirdPartyID = t.ThirdPartyID,
                              TotalAmount = s.TotalAmount,
                              Paid = t.Paid,
                              Remaining = p.Remaining
                          }).ToList().Cast<dynamic>().ToList();
            return result;
        }
        public List<expencestype> LaodETypes()
        {
            return _IUOW.expencestypes.GetAll().ToList();
        }
        public object GetBalance(int tid, string DFT)
        {
            var totalFinancial = _IUOW.payments.GetAll()
                                               .Where(p => p.ThirdPartyID == tid && DateTime.Parse(p.Date) < DateTime.Parse(DFT))
                                               .Sum(p => (decimal?)p.Remaining - (decimal?)p.Paid) ?? 0;
            return totalFinancial;
        }
    }
}
