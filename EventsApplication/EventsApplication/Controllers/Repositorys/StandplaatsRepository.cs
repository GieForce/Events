using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Controllers.Repositorys
{
    public class StandplaatsRepository
    {
        private readonly IStaanPlaatsDAO istaanplaatsDAO;

        public StandplaatsRepository(IStaanPlaatsDAO iStaanPlaatsDAO)
        {
            istaanplaatsDAO = iStaanPlaatsDAO;
        }

        public List<Staanplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime begindatum, DateTime einddatum)
        {
            return istaanplaatsDAO.GetFreeStaanplaatsenByLocatie(locatie, begindatum, einddatum);
        }

        public List<Staanplaats> GetByLocatie(Locatie locatie)
        {
            return istaanplaatsDAO.GetByLocatie(locatie);
        }

        public bool Insert(int locatieID, int prijs, int grootte, bool status, string kenmerk)
        {
            return istaanplaatsDAO.Insert(locatieID, prijs, grootte, status, kenmerk);
        }

        public bool Delete(int id)
        {
            return istaanplaatsDAO.Delete(id);
        }

        public bool Update(int ID, int prijs, int grootte, bool status, string kenmerk)
        {
            return istaanplaatsDAO.Update(ID, prijs, grootte, status, kenmerk);
        }

        public int getLatestEventID()
        {
            return istaanplaatsDAO.getLatestEventID();
        }
    }
}