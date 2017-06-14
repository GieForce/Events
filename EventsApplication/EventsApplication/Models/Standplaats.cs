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
        private bool kraanbeschikbaar = false;
        private bool handicapbeschikbaar = false;
        private bool comfortplek = false;

        public int Id
        {
            get { return id; }
        }

        public bool Kraanbeschikbaar
        {
            get { return kraanbeschikbaar; }
            set { kraanbeschikbaar = value; }
        }
        public bool Handicapbeschikbaar
        {
            get { return handicapbeschikbaar; }
            set { handicapbeschikbaar = value; }
        }
        public bool Comfortplek
        {
            get { return comfortplek; }
            set { comfortplek = value; }
        }
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
            this.plaatsnummer = plaatsnummer;
            this.locatieid = locatieid;
            this.capaciteit = capaciteit;
            this.prijs = prijs;
        }

        public Standplaats(int id, int plaatsnummer, int capaciteit, decimal prijs, string specificatie)
        {
            this.id = id;
            this.plaatsnummer = plaatsnummer;
            this.capaciteit = capaciteit;
            this.prijs = prijs;
            if(specificatie == "kraan beschikbaar")
            {
                kraanbeschikbaar = true;
            }
            if (specificatie == "handicap geschikt")
            {
                handicapbeschikbaar = true;
            }
            if (specificatie == "comfortplek")
            {
                comfortplek = true;
            }
        }

        public override string ToString()
        {
            return "Plaats " + this.Plaatsnummer + " | P/N: €" + Prijs + " | Pers: " + Capaciteit;
        }
    }
}