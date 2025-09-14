using Domain.Models;
using GUIForms.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOW;

namespace GUIForms.helpers
{
    public class UBLClass
    {
        IUnitofwork _IUW;
        public void SaveUBL(string QRCode, string Invoicehash, string Uuid,string Invoice,int Saleid,string Status, string path,int invid)
        {
            _IUW = new Unitofwork(new EasyposEntities());
            UBL ubl = new UBL
            {
                QRCode = QRCode,
                Invoicehash = Invoicehash,
                Uuid = Uuid,
                Invoice = Invoice,
                Saleid = Saleid, 
                Status = Status,
                Path = path,
                Invoicenumber = invid
            };
            _IUW.UBLS.Insert(ubl);
            _IUW.Complete();
        }
    }
}
