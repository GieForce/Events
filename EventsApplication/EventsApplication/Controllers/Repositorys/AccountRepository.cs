using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class AccountRepository
    {
        private readonly IAccountContext context;

        public AccountRepository(IAccountContext context)
        {
            this.context = context;
        }

        public Account GetById(int id)
        {
            return context.GetById(id);
        }

        public List<Account> GetAllAccounts()
        {
            return context.GetAllAccounts();
        }

        public List<Account> GetAllAccountsPresent()
        {
            return context.GetAllAccountsPresent();
        }

        public List<Account> GetAllAccountsByReservation(int reserveringsID)
        {
            return context.GetAllAccountsByReservation(reserveringsID);
        }

        public Account GetCompleteAccountsByRRFID(string RFID)
        {
            return context.GetCompleteAccountsByRRFID(RFID);
        }

        public Account GetCompleteAccountByEmailAndActivationhash(string email, string activatiehash)
        {
            return context.GetCompleteAccountByEmailAndActivationhash(email, activatiehash);
        }

        public bool Insert(Account account)
        {
            return context.Insert(account);
        }

        public bool Update(Account account)
        {
            return context.Update(account);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }

        public Account Login(string wachtwoord, string gebruikersnaam)
        {
            return context.Login(wachtwoord, gebruikersnaam);
        }

        public void Activeer(string hash)
        {
            context.Activeer(hash);
        }

        public Guid newactivationhash()
        {
            Guid g;
            g = Guid.NewGuid();
            return g;
        }

        public bool insertadmin(Account account)
        {
            return context.InsertAdmin(account);
        }
    }
}