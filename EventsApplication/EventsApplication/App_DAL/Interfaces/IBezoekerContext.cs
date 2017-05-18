using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IBezoekerContext
    {
        List<Bezoeker> GetBezoeker(int eventID);

        Bezoeker GetById(int id);

        Bezoeker GetByName(string naam, string email);

        Bezoeker Insert(Bezoeker bezoeker, Reservering reservering);

        bool getBetaalStatus(int id);

        bool Update(Bezoeker bezoeker);

        bool Delete(int id);

        void checkIn(int id);

        void checkOut(int ID);
    }
}