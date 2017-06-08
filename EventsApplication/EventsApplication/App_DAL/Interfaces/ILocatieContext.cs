using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface ILocatieContext
    {
        bool Insert(Locatie locatie);

        List<Locatie> GetAll();

        Locatie GetByEvenement(Event evenement);

        bool Delete(Locatie locatie);

        bool Update(Locatie locatie);

        int locatieidophalen(string naam, string straat, int nummer, string postcode, string plaats);
    }
}