using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Controllers.Repositorys;

namespace EventsApplication.Models
{
    public class Locatie
    {
        private int id;
        private string naam;
        private string adres;
        private int plaatsen;
        private string locatieUrl;

        private List<Standplaats> staanplaatsen;

        public int Id
        {
            get { return id; }
        }
        public string Naam
        {
            get { return naam; }
        }

        public string Adres
        {
            get { return adres; }
        }

        public int Plaatsen
        {
            get { return plaatsen; }
        }

        public List<Standplaats> Standplaatsen
        {
            get { return Standplaatsen; }
        }

        public string LocatieUrl
        {
            get { return locatieUrl; }
        }

        public Locatie(string naam, string adres, int plaatsen, int id, string locatieUrl)
        {
            this.naam = naam;
            this.adres = adres;
            this.plaatsen = plaatsen;
            this.id = id;
            this.staanplaatsen = new StandplaatsRepository(new StandplaatsContext()).GetByLocatie(this);
            this.locatieUrl = locatieUrl;
        }

        public Locatie(string naam, string adres, int plaatsen, string locatieUrl)
        {
            this.naam = naam;
            this.adres = adres;
            this.plaatsen = plaatsen;
            this.staanplaatsen = new StandplaatsRepository(new StandplaatsContext()).GetByLocatie(this);
            this.locatieUrl = locatieUrl;
        }

        public override string ToString()
        {
            return "Naam: " + naam + " Adres: " + adres + " Plaatsen: " + plaatsen;
        }
    }
}