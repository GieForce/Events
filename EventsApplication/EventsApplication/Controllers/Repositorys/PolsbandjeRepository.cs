using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Repositorys
{
    public class PolsbandjeRepository
    {
        private readonly IPolsbandjeContext context;

        public PolsbandjeRepository(IPolsbandjeContext context)
        {
            this.context = context;
        }

        public Polsbandje GetByAccountId(Account account)
        {
            return context.GetByAccountId(account);
        }

        public Polsbandje GetById(Polsbandje polsbandje)
        {
            return context.GetById(polsbandje);
        }

        public void Insert(Polsbandje polsbandje, Reservering reservering, Account account)
        {
            context.Insert(polsbandje, reservering, account);
        }

        public void ConnectAccountWithRFID(string RFID, Polsbandje polsbandje, Account account)
        {
            context.ConnectAccountWithRFID(RFID, polsbandje, account);
        }

        public void Delete(Polsbandje polsbandje)
        {
            context.Delete(polsbandje);
        }
    }
}