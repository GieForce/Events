using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public interface IAccountContext
    {
        Account GetById(int id);

        List<Account> GetAllAccounts();

        List<Account> GetAllAccountsPresent();

        bool Insert(Account account);

        bool Delete(int id);

        Account Login(string wachtwoord, string gebruikersnaam);

        bool Update(Account account);
    }
}
