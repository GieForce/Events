using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IReserveringContext
    {
        List<Reservering> GetReservering();

        Reservering GetById(int id);

        Reservering Insert(Reservering reservering, Evenement evenement);

        bool Update(Reservering reservering);

        bool Delete(int id);

        Reservering GetByBezoeker(int bezoekerid);
    }
}