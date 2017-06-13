using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IPolsbandjeContext
    {
        Polsbandje GetByAccountId(Account account);
        Polsbandje GetById(Polsbandje polsbandje);
        void Insert(Polsbandje polsbandje, Reservering reservering, Account account);
        void ConnectAccountWithRFID(string RFID, Polsbandje polsbandje, Account account);
        void Delete(Polsbandje polsbandje);
    }
}