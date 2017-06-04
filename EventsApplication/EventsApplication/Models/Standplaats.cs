using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Standplaats
    {
        private int id;
        private int plaatsnummer;
        private int locatieid;
        private int capaciteit;
        private decimal prijs;

        public int Plaatsnummer
        {
            get { return plaatsnummer; }
            set { plaatsnummer = value; }
        }

        public int Locatieid
        {
            get { return locatieid; }
            set { locatieid = value; }
        }

        public int Capaciteit
        {
            get { return capaciteit; }
            set { capaciteit = value; }
        }

        public decimal Prijs
        {
            get { return prijs; }
            set { prijs = value; }
        }

        public Standplaats(int plaatsnummer, int locatieid, int capaciteit, decimal prijs)
        {
            Plaatsnummer = plaatsnummer;
            Locatieid = locatieid;
            Capaciteit = capaciteit;
            Prijs = prijs;
        }

        public Standplaats(int plaatsnummer, int capaciteit, decimal prijs)
        {
            Plaatsnummer = plaatsnummer;
            Capaciteit = capaciteit;
            Prijs = prijs;
        }

        public override string ToString()
        {
            return "Plaats " + this.Plaatsnummer + " | P/N: €" + Prijs + " | Pers: " + Capaciteit;
        }
    }
}