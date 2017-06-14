using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.ViewModels;

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

        public void PlaatsReservering(string voornaam, string tussenvoegsel, string achternaam, string straat,
            int huisnr, string woonplaats, DateTime start, DateTime eind, int evenementid, List<int> plekids,
            List<AccountViewModel> accounts, List<ProductExemplaar> producten)
        {
            reserveringDao.PlaatsReservering(voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, start,
                eind, evenementid, plekids, accounts, producten);
        }
    }
}