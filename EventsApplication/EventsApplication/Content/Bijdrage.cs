using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public abstract class Bijdrage
    {

        private int id;
        private Account account;
        private DateTime datum;
        private string soort;
        private AccountBijdrage accountBijdrage;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public string Soort
        {
            get { return soort; }
            set { soort = value; }
        }

        public AccountBijdrage AccountBijdrage
        {
            get { return accountBijdrage; }
            set { accountBijdrage = value; }
        }

        public Bijdrage(int id, Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage)
        {
            this.id = id;
            this.account = account;
            this.datum = datum;
            this.soort = soort;
            this.accountBijdrage = accountBijdrage;
        }

        public Bijdrage(Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage)
        {
            this.account = account;
            this.datum = datum;
            this.soort = soort;
            this.accountBijdrage = accountBijdrage;
        }
    }
}
