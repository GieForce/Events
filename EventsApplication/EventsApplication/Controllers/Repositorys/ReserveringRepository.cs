using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Controllers.Repositorys
{
    public class ReserveringRepository
    {
        private readonly IReserveringDAO reserveringDao;

        public ReserveringRepository(IReserveringDAO reserveringDao)
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