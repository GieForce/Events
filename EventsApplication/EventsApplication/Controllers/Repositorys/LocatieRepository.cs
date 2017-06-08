using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;

namespace EventsApplication.Controllers.Repositorys
{
    public class LocatieRepository
    {
        private readonly ILocatieContext ilocatieContext;

        public LocatieRepository(ILocatieContext ilocatieContext)
        {
            this.ilocatieContext = ilocatieContext;
        }

        public List<Locatie> GetAll()
        {
            return ilocatieContext.GetAll();
        }

        public Locatie GetByEvenement(Evenement evenement)
        {
            return ilocatieContext.GetByEvenement(evenement);
        }

        public bool Insert(Locatie locatie)
        {
            return ilocatieContext.Insert(locatie);
        }

        public bool Delete(Locatie locatie)
        {
            return ilocatieContext.Delete(locatie);
        }

        public bool Update(Locatie locatie)
        {
            return ilocatieContext.Update(locatie);
        }

        public int locatieidophalen(string naam, string straat, int nummer, string postcode, string plaats)
        {
            return ilocatieContext.locatieidophalen(naam, straat, nummer, postcode, plaats);
        }
    }
}