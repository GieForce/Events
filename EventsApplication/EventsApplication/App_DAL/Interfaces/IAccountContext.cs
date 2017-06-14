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

        List<Account> GetAllAccountsByReservation(int reserveringsID);

        Account GetCompleteAccountsByRRFID(string RFID);

        Account GetCompleteAccountByEmailAndActivationhash(string email, string activatiehash);

        bool Insert(Account account);

        bool Delete(int id);

        Account Login(string wachtwoord, string gebruikersnaam);

        bool Update(Account account);

        void Activeer(string hash);
    }
}
