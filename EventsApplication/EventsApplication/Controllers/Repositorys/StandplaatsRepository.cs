using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Repositorys
{
    public class StandplaatsRepository
    {
        private readonly IStandplaatsContext istandplaatsContext;

        public StandplaatsRepository(IStandplaatsContext istandplaatsContext)
        {
            this.istandplaatsContext = istandplaatsContext;
        }

        public List<Standplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime begindatum, DateTime einddatum)
        {
            return istandplaatsContext.GetFreeStaanplaatsenByLocatie(locatie, begindatum, einddatum);
        }

        public List<Standplaats> GetByLocatie(Locatie locatie)
        {
            return istandplaatsContext.GetByLocatie(locatie);
        }

        public Standplaats GetByReservation(int reservationID)
        {
            return istandplaatsContext.GetByReservation(reservationID);
        }

        public bool Insert(Locatie locatie, decimal prijs, int capaciteit, int nummer, string specificatie)
        {
            return istandplaatsContext.Insert(locatie, prijs, capaciteit, nummer, specificatie);
        }

        public bool Delete(int id)
        {
            return istandplaatsContext.Delete(id);
        }

        public bool Update(int ID, int capaciteit, int nummer, decimal prijs)
        {
            return istandplaatsContext.Update(ID, capaciteit, nummer, prijs);
        }

        public int getLatestEventID()
        {
            return istandplaatsContext.getLatestEventID();
        }
    }
}