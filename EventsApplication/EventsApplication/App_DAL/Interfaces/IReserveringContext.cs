using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IReserveringContext
    {
        List<Reservering> GetAllReserveringen();

        Reservering GetById(int id);

        void Insert(Reservering reservering);

        void Delete(Reservering reservering);
    }
}