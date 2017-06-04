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
        private string straat;
        private int nr;
        private string postcode;
        private string plaats;

        private List<Standplaats> staanplaatsen;

        public int Id
        {
            get { return id; }
        }
        public string Naam
        {
            get { return naam; }
        }

        public string Straat
        {
            get { return straat; }
        }

        public int Nr
        {
            get { return nr; }
        }

        public List<Standplaats> Standplaatsen
        {
            get { return Standplaatsen; }
        }

        public string Postcode
        {
            get { return postcode; }
        }

        public string Plaats
        {
            get { return plaats; }
        }

        public Locatie(string naam, string straat, int nr, int id, string postcode, string plaats)
        {
            this.naam = naam;
            this.straat = straat;
            this.nr = nr;
            this.id = id;
            this.staanplaatsen = new StandplaatsRepository(new StandplaatsContext()).GetByLocatie(this);
            this.postcode = postcode;
            this.plaats = plaats;
        }

        public Locatie(string naam, string straat, int nr, string postcode, string plaats)
        {
            this.naam = naam;
            this.straat = straat;
            this.nr = nr;
            this.staanplaatsen = new StandplaatsRepository(new StandplaatsContext()).GetByLocatie(this);
            this.postcode = postcode;
            this.plaats = plaats;
        }

        public override string ToString()
        {
            return "Naam: " + naam + " Straat: " + straat + " nr: " + nr + "Postcode" + postcode + "Plaats" + plaats;
        }
    }
}