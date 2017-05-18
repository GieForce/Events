using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Materiaal
    {
        private int id;
        private int reserveringID;
        private int eventID;
        private string naam;
        private string omschrijving;
        private decimal prijs;
        private bool bezet;

        public string Naam
        {
            get { return naam; }
        }

        public string Omschrijving
        {
            get { return omschrijving; }
        }

        public decimal Prijs
        {
            get { return prijs; }
        }

        public bool Bezet
        {
            get { return bezet; }
        }

        public int Id
        {
            get
            {
                return id;

            }
        }

        public int ReserveringID
        {
            get
            {
                return reserveringID;
            }
        }

        public int EventID
        {
            get
            {
                return eventID;
            }
        }

        public Materiaal(string naam, string omschrijving, decimal prijs, bool bezet)
        {
            this.naam = naam;
            this.omschrijving = omschrijving;
            this.prijs = prijs;
            this.bezet = bezet;
        }

        public Materiaal(int id, int reserveringID, int eventID, string naam, string omschrijving, decimal prijs, bool bezet)
        {
            this.id = id;
            this.reserveringID = reserveringID;
            this.eventID = eventID;
            this.naam = naam;
            this.omschrijving = omschrijving;
            this.prijs = prijs;
            this.bezet = bezet;
        }

        public override string ToString()
        {
            return naam;
        }
    }
}