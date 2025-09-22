using Domain.Models;
using Repo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOW
{
    public class Unitofwork : IUnitofwork
    {
        EasyposEntities _Dbc;
        public IRepository<cashier> cashiers { get; private set; }
        public IRepository<category> categories { get; private set; }
        public IRepository<company> companies { get; private set; }
        public IRepository<expencestype> expencestypes { get; private set; }
        public IRepository<expens> expenses { get; private set; }
        public IRepository<item> items { get; private set; }
        public IRepository<itemsale> itemsales { get; private set; }
        public IRepository<order> orders { get; private set; }
        public IRepository<payment> payments { get; private set; }
        public IRepository<paymentout> paymentouts { get; private set; }
        public IRepository<price> prices { get; private set; }
        public IRepository<pricedetaile> pricedetailes { get; private set; }
        public IRepository<product> products { get; private set; }
        public IRepository<productitem> productitems { get; private set; }
        public IRepository<purchasedetaile> purchasedetailes { get; private set; }
        public IRepository<purchase> purchases { get; private set; }
        public IRepository<sale> sales { get; private set; }
        public IRepository<salesdetaile> salesdetailes { get; private set; }
        public IRepository<staff> staffs { get; private set; }
        public IRepository<thirdparty> thirdparties { get; private set; }
        public IRepository<transaction> transactions { get; private set; }
        public IRepository<unittype> unittypes { get; private set; }
        public IRepository<expensesview> expensesviews { get; private set; }
        public IRepository<UBL> UBLS { get; private set; }
        public IRepository<voucher> vouchers { get; private set; }
        public IRepository<tailorheader> tailorheaders { get; private set; }
        public IRepository<tailorhand> tailorhands { get; private set; }
        public IRepository<tailorneck> tailornecks { get; private set; }
        public IRepository<tailorjabzor> tailorjabzors { get; private set; }
        public IRepository<tailorbocket> tailorbockets { get; private set; }
        public IRepository<tailordetaile> tailordetailes { get; private set; }
        public IRepository<alltailoring> alltailorings { get; private set; }
        public IRepository<exceptionpro> exceptionpros { get; private set; }
        public IRepository<stok_transaction> stok_transactions { get; private set; }
        public IRepository<returned> returneds { get; private set; }
        public IRepository<returnedetaile> returnedetailes { get; private set; }
        public IRepository<invtransaction> invtransactions { get; private set; }
        public Unitofwork(EasyposEntities Dbc)
        {
            _Dbc = Dbc;
            cashiers = new Repository<cashier>(_Dbc);
            categories = new Repository<category>(_Dbc);
            companies = new Repository<company>(_Dbc);
            expencestypes = new Repository<expencestype>(_Dbc);
            expenses = new Repository<expens>(_Dbc);
            items = new Repository<item>(_Dbc);
            itemsales = new Repository<itemsale>(_Dbc);
            orders = new Repository<order>(_Dbc);
            payments = new Repository<payment>(_Dbc);
            paymentouts = new Repository<paymentout>(_Dbc);
            prices = new Repository<price>(_Dbc);
            pricedetailes = new Repository<pricedetaile>(_Dbc);
            products = new Repository<product>(_Dbc);
            productitems = new Repository<productitem>(_Dbc);
            purchasedetailes = new Repository<purchasedetaile>(_Dbc);
            purchases = new Repository<purchase>(_Dbc);
            sales = new Repository<sale>(_Dbc);
            salesdetailes = new Repository<salesdetaile>(_Dbc);
            staffs = new Repository<staff>(_Dbc);
            thirdparties = new Repository<thirdparty>(_Dbc);
            transactions = new Repository<transaction>(_Dbc);
            unittypes = new Repository<unittype>(_Dbc);
            expensesviews = new Repository<expensesview>(_Dbc);
            UBLS = new Repository<UBL>(_Dbc);
            vouchers = new Repository<voucher>(_Dbc);
            tailorheaders = new Repository<tailorheader>(_Dbc);
            tailorhands = new Repository<tailorhand>(_Dbc);
            tailornecks = new Repository<tailorneck>(_Dbc);
            tailorjabzors = new Repository<tailorjabzor>(_Dbc);
            tailorbockets = new Repository<tailorbocket>(_Dbc);
            tailordetailes = new Repository<tailordetaile>(_Dbc);
            alltailorings = new Repository<alltailoring>(_Dbc);
            exceptionpros = new Repository<exceptionpro>(_Dbc);
            stok_transactions = new Repository<stok_transaction>(_Dbc);
            returneds = new Repository<returned>(_Dbc);
            returnedetailes = new Repository<returnedetaile>(_Dbc);
            invtransactions = new Repository<invtransaction>(_Dbc);
        }
        public int Complete()
        {
            return _Dbc.SaveChanges();
        }
        public void Dispose()
        {
            _Dbc.Dispose();
        }
    }
}
