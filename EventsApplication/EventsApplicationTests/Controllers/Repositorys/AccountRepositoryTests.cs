using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Tests
{
    [TestClass()]
    public class AccountRepositoryTests
    {
        AccountRepository accountRepository = new AccountRepository(new MemoryAccountContext());

        [TestMethod]
        public void GetByIdTest()
        {
            Account a = accountRepository.GetById(1);
            Assert.AreEqual("henkie", a.Gebruikersnaam);
        }
        public Account GetByAccountTest(Account account)
        {
            try
            {
                Account a = accountRepository.GetById(account.Id);
                Assert.AreEqual(account.Gebruikersnaam, a.Gebruikersnaam);
                return a;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        [TestMethod]
        public void InsertAndLoginTest()
        {
            Account account = new Account(4, "Herman", "herman@mail.com", "FE323IUIE32", false, "Wachtwoord123","06087452365");
            accountRepository.Insert(account);
            Account testaccount = GetByAccountTest(account);
            if (testaccount != null)
            {
                Assert.AreEqual(testaccount.Gebruikersnaam, account.Gebruikersnaam);
                Account logintest = accountRepository.Login(account.Wachtwoord, account.Gebruikersnaam);
                if (logintest != null)
                {
                    Assert.AreEqual(logintest.Gebruikersnaam, account.Gebruikersnaam);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
            
        }

        [TestMethod]
        public void GetAllAccounts()
        {
            List<Account> accountlist = accountRepository.GetAllAccounts();
            Account[] accountarray = accountlist.ToArray();
            Assert.AreEqual("henkie", accountarray[0].Gebruikersnaam);
        }

        [TestMethod]
        public void DeleteAccount()
        {
            if (accountRepository.Delete(1))
            {
                Assert.AreEqual(true, true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetPresentAccounts()
        {
            List<Account> accountPresentList = accountRepository.GetAllAccountsPresent();
            Account[] accountarray = accountPresentList.ToArray();
            if (accountarray.Length != 0)
            {
                Assert.AreEqual(true, accountarray[0].Geactiveerd);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateAccount()
        {
            Account account = new Account(2, "herman", "herman@gmail.com", "AEADLKJALJ", true, "Wachtwoord12", "098203982039283");
            accountRepository.Update(account);
            Account result = accountRepository.GetById(account.Id);
            Assert.AreEqual("herman", result.Gebruikersnaam);
        }
        private class MemoryAccountContext : IAccountContext
        {
            private List<Account> Accounts;

            public MemoryAccountContext()
            {
                Accounts = new List<Account>()
                {
                    new Account(1, "henkie", "henkie@mail.com", "A3FE232AAD", true, "Password123", "0643123254"),
                    new Account(2, "jantje", "jantje@mail.com", "FFE232JKE2355fE", false, "Wachtwoord123", "0642126578"),
                    new Account(3, "Pietje", "piet@mail.com", "FE323IUIE32", false, "Wachtwoord123", "06087452365")
                };
            }

            public Account GetById(int id)
            {
                return Accounts.FirstOrDefault(x => x.Id == id);
            }

            public List<Account> GetAllAccounts()
            {
                return Accounts.ToList();
            }

            public List<Account> GetAllAccountsPresent()
            {
                var accountList = Accounts.Where(a => a.Geactiveerd == true);
                List<Account> presentAccountList = accountList.ToList();
                return presentAccountList;
            }

            public bool Insert(Account account)
            {
                try
                {
                    Accounts.Add(account);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool Delete(int id)
            {
                return true;
            }

            public Account Login(string wachtwoord, string gebruikersnaam)
            {
                Account account = (from u in Accounts
                    where u.Gebruikersnaam.Equals(gebruikersnaam) &&
                          u.Wachtwoord.Equals(wachtwoord)
                    select u).FirstOrDefault();
                if (account == null)
                {
                    return null;
                }
                else
                {
                    return account;
                }
            }

            public bool Update(Account account)
            {
                try
                {
                    foreach (var item in Accounts.Where(w => w.Id == account.Id))
                    {
                        item.Gebruikersnaam = account.Gebruikersnaam;
                        item.Aanwezig = account.Aanwezig;
                        item.Geactiveerd = account.Geactiveerd;
                        item.Wachtwoord = account.Wachtwoord;
                        item.Activatiehash = account.Activatiehash;
                        item.Telefoonnummer = account.Telefoonnummer;
                        item.Id = account.Id;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
        }
    }
}