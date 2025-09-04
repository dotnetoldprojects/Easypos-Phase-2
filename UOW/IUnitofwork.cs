using Domain.Models;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOW
{
    public interface IUnitofwork : IDisposable
    {
        IRepository<cashier> cashiers { get; }
        IRepository<category> categories { get; }
        IRepository<company> companies { get; }
        IRepository<expencestype> expencestypes { get; }
        IRepository<expens> expenses { get; }
        IRepository<item> items { get; }
        IRepository<itemsale> itemsales { get; }
        IRepository<order> orders { get; }
        IRepository<payment> payments { get; }
        IRepository<paymentout> paymentouts { get; }
        IRepository<price> prices { get; }
        IRepository<pricedetaile> pricedetailes { get; }
        IRepository<product> products { get; }
        IRepository<productitem> productitems { get; }
        IRepository<purchasedetaile> purchasedetailes { get; }
        IRepository<purchase> purchases { get; }
        IRepository<sale> sales { get; }
        IRepository<salesdetaile> salesdetailes { get; }
        IRepository<staff> staffs { get; }
        IRepository<thirdparty> thirdparties { get; }
        IRepository<transaction> transactions { get; }
        IRepository<unittype> unittypes { get; }
        IRepository<vatsetting> vatsettings { get; }
        IRepository<expensesview> expensesviews { get; }
        IRepository<UBL> UBLS { get; }
        IRepository<voucher> vouchers { get; }
        IRepository<tailorheader> tailorheaders { get; }
        IRepository<tailorhand> tailorhands { get; }
        IRepository<tailorneck> tailornecks{ get; }
        IRepository<tailorjabzor> tailorjabzors { get; }
        IRepository<tailorbocket> tailorbockets{ get; }
        IRepository<tailordetaile> tailordetailes { get; }
        IRepository<alltailoring> alltailorings { get; }
        IRepository<exceptionpro> exceptionpros { get; }
        IRepository<stok_transaction> stok_transactions { get; }
        IRepository<returned> returneds { get; }
        IRepository<returnedetaile> returnedetailes { get; }
        int Complete();
    }
}
