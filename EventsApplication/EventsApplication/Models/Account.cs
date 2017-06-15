using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private bool status;
        private string barcode;
        private bool administrator;

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Gebruikersnaam
        {
            get { return gebruikersnaam; }
            set { gebruikersnaam = value; }
        }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
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
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { wachtwoord = value; }
        }
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
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

            this.status = status;
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

            this.status = status;
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd, string wachtwoord, string telefoonnummer, string barcode)
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

        public Account(string gebruikersnaam, string email, string activatiehash, int geactiveerd, string wachtwoord, string telefoonnummer, bool status)
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
            this.wachtwoord = wachtwoord;
            this.telefoonnummer = telefoonnummer;
            this.status = status;
        }

        public Account(int id, string gebruikersnaam, string telefoonnummer, bool aanwezig)
        {
            this.Id = id;
            this.gebruikersnaam = gebruikersnaam;
            this.telefoonnummer = telefoonnummer;
            this.aanwezig = aanwezig;
            this.status = status;
        }
    }
}