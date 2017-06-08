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

        public List<Account> GetAllAccounts()
        {
            return context.GetAllAccounts();
        }

        public Account Login(string wachtwoord, string gebruikersnaam)
        {
            return context.Login(wachtwoord, gebruikersnaam);
        }
    }
}