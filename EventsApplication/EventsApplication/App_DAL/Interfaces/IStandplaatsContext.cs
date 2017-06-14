using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IStandplaatsContext
    {
        List<Standplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime startdatum, DateTime einddatum);

        List<Standplaats> GetByLocatie(Locatie locatie);

        Standplaats GetByReservation(int reservationID);

        bool Insert(Locatie locatie, decimal prijs, int capaciteit, int nummer, string specificatie);

        bool Delete(int id);

        bool Update(int ID, int capaciteit, int nummer, decimal prijs);

        int getLatestEventID();

        Standplaats GetById(int id);
    }
}