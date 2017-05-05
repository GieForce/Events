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

        public bool Delete(Account account)
        {
            return context.Delete(account);
        }

        public List<Account> GetAllAccounts()
        {
            return context.GetAllAccounts();
        }
    }
}