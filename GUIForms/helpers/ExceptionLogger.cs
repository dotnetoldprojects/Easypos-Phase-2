using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOW;

namespace GUIForms.helpers
{
    public class ExceptionLogger
    {
        private readonly IUnitofwork _uow;

        public ExceptionLogger(IUnitofwork uow)
        {
            _uow = uow;
        }

        public void Log(Exception ex, string screen)
        {
            var EP = new exceptionpro
            {
                Screen = screen,
                Exceptionstring = ex.Message
            };
            _uow.exceptionpros.Insert(EP);
            _uow.Complete();
        }
    }

}
