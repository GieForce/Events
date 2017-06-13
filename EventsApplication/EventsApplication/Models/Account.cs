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
        private string barcode;
        private bool administrator;

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

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public bool Administrator
        {
            get { return administrator; }
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer)
        {
            this.ID = id;
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;

            if (geactiveerd == true)
            {
                this.geactiveerd = true;
            }
            else if (geactiveerd == false)
            {
                this.geactiveerd = false;
            }
            this.wachtwoord = wachtwoord;
            this.telefoonnummer = telefoonnummer;
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer, bool status)
        {
            this.ID = id;
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;

            if (geactiveerd == true)
            {
                this.geactiveerd = true;
            }
            else if (geactiveerd == false)
            {
                this.geactiveerd = false;
            }
            this.wachtwoord = wachtwoord;
            this.telefoonnummer = telefoonnummer;

            this.administrator = status;
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer, string barcode, bool status)
        {
            this.ID = id;
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;

            if (geactiveerd == true)
            {
                this.geactiveerd = true;
            }
            else if (geactiveerd == false)
            {
                this.geactiveerd = false;
            }
            this.wachtwoord = wachtwoord;
            this.telefoonnummer = telefoonnummer;
            this.barcode = barcode;

            this.administrator = status;
        }

        public Account(string gebruikersnaam, string email, string activatiehash, int geactiveerd, string wachtwoord, string telefoonnummer)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.email = email;
            this.activatiehash = activatiehash;

            if (geactiveerd == 1)
            {
                this.geactiveerd = true;
            }
            else if (geactiveerd == 0)
            {
                this.geactiveerd = false;
            }

            this.telefoonnummer = telefoonnummer;
        }

        public Account(int id, string gebruikersnaam, string telefoonnummer, bool aanwezig, bool status)
        {
            this.Id = id;
            this.gebruikersnaam = gebruikersnaam;
            this.telefoonnummer = telefoonnummer;
            this.aanwezig = aanwezig;
            this.administrator = status;
        }
    }
}