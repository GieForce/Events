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
        private string wachtwoord;
        private string telefoonnummer;
        private bool aanwezig;

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
        
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { wachtwoord = value; }
        }

        public string Telefoonnummer
        {
            get { return telefoonnummer; }
            set { telefoonnummer = value; }
        }

        public bool Aanwezig
        {
            get { return aanwezig; }
            set { aanwezig = value; }
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer)
        {
            ID = id;
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;
            this.geactiveerd = geactiveerd;
            this.wachtwoord = wachtwoord;
            this.telefoonnummer = telefoonnummer;
        }

        public Account(string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;
            this.geactiveerd = geactiveerd;
            this.telefoonnummer = telefoonnummer;
        }

        public Account(string gebruikersnaam, string telefoonnummer, bool aanwezig)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.telefoonnummer = telefoonnummer;
            this.aanwezig = aanwezig;
        }
    }
}