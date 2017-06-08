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
        bool Insert(Account account);

        bool Delete(int id);
        List<Account> GetAllAccounts();

        Account Login(string wachtwoord, string gebruikersnaam);

        bool Update(Account account);
    }
}
