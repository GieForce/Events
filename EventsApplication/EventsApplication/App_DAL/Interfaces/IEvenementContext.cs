using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IEvenementContext
    {
        bool AddEvenement(Evenement evenement, Locatie locatie);

        bool UpdateEvenement(Evenement evenement, Locatie locatie);

        bool DeleteEvenement(Evenement evenement);

        List<Evenement> GetEvenementen();

        Evenement GetEvenementById(int id);

        int GetBezoekersAantal(Evenement evenement);
    }
}