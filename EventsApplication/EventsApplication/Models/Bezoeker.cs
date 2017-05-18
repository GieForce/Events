using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Bezoeker
    {
        private int id;
        private string naam;
        private string email;
        private string telefoonnummer;
        private bool aanwezig;
        private Account account;

        private int eventid;

        private int value;
        public int Id
        {
            get { return id; }
        }

        public Account Account => account;

        public string Naam
        {
            get { return naam; }
        }

        public string Email
        {
            get { return email; }

        }

        public string Telefoonnummer
        {
            get { return telefoonnummer; }
        }

        public bool Aanwezig
        {
            get { return aanwezig; }
            set { }
        }

        public Bezoeker(int id, string naam)
        {
            this.id = id;
            this.naam = naam;
        }

        public int Eventid
        {
            get { return eventid; }
        }

        public Bezoeker(string naam, string email, string telefoonnummer, Account account)
        {
            this.account = account;
            this.naam = naam;
            this.email = email;
            this.telefoonnummer = telefoonnummer;

            if (string.IsNullOrEmpty(naam))
            {
                string message = "Naam is niet correct!";
                throw new FormatException(String.Format(message));
            }

            if (string.IsNullOrEmpty(email))
            {
                string message = "Email address is niet correct!";
                throw new FormatException(String.Format(message));
            }

            bool atMissing = email.IndexOf("@", 2) == -1;
            bool dotMissing = email.IndexOf(".", email.IndexOf("@", 2) + 2) == -1;
            bool toplevelMissing = email.Length - email.IndexOf(".", email.IndexOf("@", 2) + 2) < 2;

            if (atMissing || dotMissing || toplevelMissing || string.IsNullOrEmpty(email))
            {
                string message = "Email address {0} is niet correct!";
                throw new FormatException(String.Format(message, email));
            }

            bool phoneIncorrect = telefoonnummer.Length != 10 || !telefoonnummer.StartsWith("06") || telefoonnummer.Any(x => char.IsLetter(x));

            if (phoneIncorrect || string.IsNullOrEmpty(telefoonnummer))
            {
                string message = "Telefoonnummer {0} is niet correct!";
                throw new FormatException(String.Format(message, telefoonnummer));
            }
        }

        public Bezoeker(int id, string naam, string email, string telefoonnummer, int aanwezigINT, int eventid)
        {
            this.id = id;
            this.naam = naam;
            this.email = email;
            this.telefoonnummer = telefoonnummer;

            if (aanwezigINT == 0)
            {
                aanwezig = false;
            }
            else if (aanwezigINT == 1)
            {
                aanwezig = true;
            }

            this.eventid = eventid;
        }

        public Bezoeker(int id, string naam, string email, string telefoonnummer, int aanwezigINT)
        {
            this.id = id;
            this.naam = naam;
            this.email = email;
            this.telefoonnummer = telefoonnummer;

            if (aanwezigINT == 0)
            {
                aanwezig = false;
            }
            else if (aanwezigINT == 1)
            {
                aanwezig = true;
            }
        }

        public Bezoeker(int id, string naam, string email, string telefoonnummer, bool aanwezig)
        {
            this.id = id;
            this.naam = naam;
            this.email = email;
            this.telefoonnummer = telefoonnummer;
            this.aanwezig = aanwezig;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", naam, "   ", telefoonnummer);
        }
    }
}