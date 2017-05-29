using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Standplaats
    {
        private int plaatsnummer;
        private decimal prijs;
        private int grootte;
        private bool bezet;
        private string plaatstag;

        public int Plaatsnummer => plaatsnummer;

        public decimal Prijs => prijs;

        public int Grootte => grootte;

        public bool Bezet => bezet;

        public string Plaatstag => plaatstag;

        public Standplaats(int plaatsnummer, decimal prijs, int grootte, bool bezet, string tag)
        {
            this.plaatsnummer = plaatsnummer;
            this.prijs = prijs;
            this.grootte = grootte;
            this.bezet = bezet;
            this.plaatstag = tag;
        }

        public override string ToString()
        {
            return "Plaats " + this.plaatsnummer + " | P/N: €" + this.prijs + " | Pers: " + this.Grootte + " | " + this.Plaatstag;
        }
    }
}