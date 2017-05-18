using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IMateriaalContext
    {
        void AddMateriaal(Materiaal materiaal);

        List<Materiaal> GetMateriaalReservering(int reserveringsID);

        List<Materiaal> GetMateriaalEvent(Evenement evenement);

        Materiaal GetById(int id);

        Materiaal Insert(Materiaal materiaal, Reservering reservering);

        Materiaal Insert(Materiaal materiaal, Evenement evenement);

        bool Update(Materiaal materiaal);

        bool Delete(int id);
    }
}