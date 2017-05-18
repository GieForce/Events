using EventsApplication.Controllers.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;

namespace EventsApplication.Models
{
    public class Evenement
    {
        private string naam;
        private DateTime startDatum;
        private DateTime eindDatum;
        private int id;

        private Locatie locatie;
        private List<Reservering> reserveringen;
        private List<Materiaal> materialen;

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public DateTime StartDatum
        {
            get { return startDatum; }
            set { startDatum = value; }
        }

        public DateTime EindDatum
        {
            get { return eindDatum; }
            set { eindDatum = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Locatie Locatie
        {
            get { return locatie; }
            set { locatie = value; }
        }

        public Evenement(string naam, DateTime startDatum, DateTime eindDatum)
        {
            this.naam = naam;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
        }

        public Evenement(int id, string naam, DateTime startDatum, DateTime eindDatum)
        {
            this.id = id;
            this.naam = naam;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.locatie = new LocatieRepository(new LocatieContext()).GetByEvenement(this);
        }

        public override string ToString()
        {
            return this.naam;
        }
    }
}