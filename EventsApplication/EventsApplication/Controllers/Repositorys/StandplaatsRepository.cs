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

        public bool Insert(int locatieID, int prijs, int grootte, bool status, string kenmerk)
        {
            return istandplaatsContext.Insert(locatieID, prijs, grootte, status, kenmerk);
        }

        public bool Delete(int id)
        {
            return istandplaatsContext.Delete(id);
        }

        public bool Update(int ID, int prijs, int grootte, bool status, string kenmerk)
        {
            return istandplaatsContext.Update(ID, prijs, grootte, status, kenmerk);
        }

        public int getLatestEventID()
        {
            return istandplaatsContext.getLatestEventID();
        }
    }
}