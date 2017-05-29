using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Repositorys
{
    public class EvenementRepository
    {
        private readonly IEvenementContext iEvenementDao;
        //public List<Evenement> evenementen;

        public EvenementRepository(IEvenementContext iEvenementDao)
        {
            this.iEvenementDao = iEvenementDao;
            //this.evenementen = new List<Evenement>();
        }

        public bool AddEvenement(Evenement evenement, Locatie locatie)
        {
            return iEvenementDao.AddEvenement(evenement, locatie);
        }

        public bool UpdateEvenement(Evenement evenement, Locatie locatie)
        {
            return iEvenementDao.UpdateEvenement(evenement, locatie);
        }

        public bool DeleteEvenement(Evenement evenement)
        {
            return iEvenementDao.DeleteEvenement(evenement);
        }

        public List<Evenement> GetEvenementen()
        {
            return iEvenementDao.GetEvenementen();
        }

        public Evenement GetEvenementById(int id)
        {
            return iEvenementDao.GetEvenementById(id);
        }

        public int GetBezoekersAantal(Evenement evenement)
        {
            return iEvenementDao.GetBezoekersAantal(evenement);
        }
    }
}