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

        public List<Reservering> GetAllReserveringen()
        {
            return reserveringDao.GetAllReserveringen();
        }

        public Reservering GetById(int id)
        {
            return reserveringDao.GetById(id);
        }

        public void Insert(Reservering reservering)
        {
            reserveringDao.Insert(reservering);
        }

        public void Delete(Reservering reservering)
        {
            reserveringDao.Delete(reservering);
        }
    }
}