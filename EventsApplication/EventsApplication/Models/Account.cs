using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Account
    {
        private int ID;
        private string gebruikersnaam;
        private string email;
        private string activatiehash;
        private bool geactiveerd;

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public string Gebruikersnaam
        {
            get { return gebruikersnaam; }
            set { gebruikersnaam = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Activatiehash
        {
            get { return activatiehash; }
            set { activatiehash = value; }
        }

        public bool Geactiveerd
        {
            get { return geactiveerd; }
            set { geactiveerd = value; }
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd)
        {
            ID = id;
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;
            this.geactiveerd = geactiveerd;
        }

        public Account(string gebruikersnaam, string email, string activatiehash, bool geactiveerd)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;
            this.geactiveerd = geactiveerd;
        }
    }
}