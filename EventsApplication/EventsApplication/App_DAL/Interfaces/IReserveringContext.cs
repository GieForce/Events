using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.ViewModels;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IReserveringContext
    {
        List<Reservering> GetAllReserveringen();

        Reservering GetById(int id);

        void Insert(Reservering reservering);

        void Delete(Reservering reservering);

        void PlaatsReservering(string voornaam, string tussenvoegsel, string achternaam, string straat,
            int huisnr, string woonplaats, DateTime start, DateTime eind, int evenementid, List<int> plekids,
            List<AccountViewModel> accounts, List<ProductExemplaar> producten);
    }
}