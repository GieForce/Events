using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Reservering
    {
        private int id;
        private int eventID;
        private int staanplaatsid;
        private string naam;
        private string adres;
        private string woonplaats;
        private string email;
        private string telefoonnummer;
        private int aantal;
        private bool betaalstatus;
        private DateTime startDatum;
        private DateTime eindDatum;
        private Standplaats staanplaats;
        private List<Materiaal> materialen;

        public List<Materiaal> Materialen
        {
            get { return materialen; }
        }

        public string Naam
        {
            get { return naam; }
        }

        public int EventId
        {
            get { return eventID; }
        }

        public DateTime StartDatum
        {
            get { return startDatum; }
        }

        public DateTime EindDatum
        {
            get { return eindDatum; }
        }

        public string Adres
        {
            get { return adres; }

        }

        public string Woonplaats
        {
            get { return woonplaats; }

        }

        public int Aantal
        {
            get { return aantal; }
        }

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public bool Betaalstatus
        {
            get { return betaalstatus; }
            set { this.betaalstatus = value; }
        }

        public int Staanplaatsid
        {
            get { return staanplaatsid; }
        }

        public Standplaats Staanplaats
        {
            get { return this.staanplaats; }
        }

        public string Email
        {
            get { return email; }
        }

        public string Telefoonnummer
        {
            get { return telefoonnummer; }
        }

        public Reservering(int id, int eventID, int staanplaatsid, string naam, string adres, string woonplaats, string email, string telefoonnummer, int aantal, DateTime startDatum, DateTime eindDatum, bool betaalstatus)
        {
            this.id = id;
            this.eventID = eventID;
            this.staanplaatsid = staanplaatsid;
            this.naam = naam;
            this.adres = adres;
            this.woonplaats = woonplaats;
            this.email = email;
            this.telefoonnummer = telefoonnummer;
            this.aantal = aantal;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.betaalstatus = betaalstatus;
        }

        public Reservering(string naam, string adres, string woonplaats, int aantal, DateTime startDatum, DateTime eindDatum, bool betaalstatus, Standplaats staanplaats)
        {

            bool isNotLetterOrDigit = adres.Any(x => !char.IsLetterOrDigit(x)) && !adres.Any(x => !char.IsWhiteSpace(x));
            bool containsLetter = adres.Any(x => char.IsLetter(x));
            bool containsDigit = adres.Any(x => char.IsNumber(x));
            bool containsSpace = adres.Any(x => char.IsWhiteSpace(x));
            bool containsSpecial = adres.Any(x => char.IsSymbol(x));
            if (string.IsNullOrEmpty(adres) || isNotLetterOrDigit || !containsDigit || !containsLetter || !containsSpace || containsSpecial)
            {
                string message = "Adres {0} is niet correct!";
                throw new FormatException(String.Format(message, adres));
            }
            if (string.IsNullOrEmpty(woonplaats))
            {
                string message = "Woonplaats {0} is niet correct!";
                throw new FormatException(String.Format(message, woonplaats));
            }
            this.naam = naam;
            this.adres = adres;
            this.woonplaats = woonplaats;
            this.aantal = aantal;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.betaalstatus = betaalstatus;
            this.staanplaats = staanplaats;
            this.materialen = new List<Materiaal>();

        }

        public Reservering(int id, int eventID, int staanplaatsid, string naam, string adres, string woonplaats, int aantal, DateTime startDatum, DateTime eindDatum, bool betaalstatus, Standplaats staanplaats)
        {
            this.id = id;
            this.eventID = eventID;
            this.staanplaatsid = staanplaatsid;
            this.naam = naam;
            this.adres = adres;
            this.woonplaats = woonplaats;
            this.aantal = aantal;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.betaalstatus = betaalstatus;
            this.staanplaats = staanplaats;
            this.materialen = new List<Materiaal>();
        }

        public void AddMateriaal(Materiaal materiaal)
        {
            materialen.Add(materiaal);
        }
    }
}