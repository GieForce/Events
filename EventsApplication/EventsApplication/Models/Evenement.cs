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
        private int locatieid;
        private int maxbezoekers;

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

        public int Locatieid
        {
            get { return locatieid; }
            set { locatieid = value; }
        }

        public int Maxbezoekers
        {
            get { return maxbezoekers; }
            set { maxbezoekers = value; }
        }

        public Evenement(string naam, DateTime startDatum, DateTime eindDatum, int locatieid, int maxbezoekers)
        {
            this.naam = naam;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.locatieid = locatieid;
            this.maxbezoekers = maxbezoekers;
        }

        public Evenement(int id, string naam, DateTime startDatum, DateTime eindDatum, int locatieid, int maxbezoekers)
        {
            this.id = id;
            this.naam = naam;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.locatieid = locatieid;
            this.maxbezoekers = maxbezoekers;
        }

        public override string ToString()
        {
            return this.naam;
        }
    }
}