using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IStandplaatsContext
    {
        List<Standplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime begindatum, DateTime einddatum);

        List<Standplaats> GetByLocatie(Locatie locatie);

        bool Insert(int locatieID, decimal prijs, int capaciteit, int nummer);

        bool Delete(int id);

        bool Update(int ID, int capaciteit, int nummer, decimal prijs);

        int getLatestEventID();
    }
}