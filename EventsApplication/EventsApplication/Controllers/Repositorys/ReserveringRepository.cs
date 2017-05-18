using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;

namespace EventsApplication.Controllers.Repositorys
{
    public class ReserveringRepository
    {
        private readonly IReserveringContext reserveringDao;

        public ReserveringRepository(IReserveringContext reserveringDao)
        {
            this.reserveringDao = reserveringDao;
        }

        public Reservering Insert(Reservering reservering, Evenement evenement)
        {

            return reserveringDao.Insert(reservering, evenement);

        }


        public bool Update(Reservering reservering)
        {

            return reserveringDao.Update(reservering);

        }

        public bool Delete(int id)
        {
            return reserveringDao.Delete(id);
        }
    }
}