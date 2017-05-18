using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IStandplaatsContext
    {
        List<Staanplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime begindatum, DateTime einddatum);

        List<Staanplaats> GetByLocatie(Locatie locatie);

        bool Insert(int locatieID, int prijs, int grootte, bool status, string kenmerk);

        bool Delete(int id);

        bool Update(int ID, int prijs, int grootte, bool status, string kenmerk);

        int getLatestEventID();
    }
}