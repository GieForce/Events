using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Controllers;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public static class ModelToViewModel
    {
        public static AccountRepository accountRepository = new AccountRepository(new AccountContext());
        public static PolsbandjeRepository polsbandjeRepository = new PolsbandjeRepository(new PolsbandjeContext());
        public static ReserveringRepository reserveringRepository = new ReserveringRepository(new ReserveringContext());
        public static EventRepository eventRepository = new EventRepository(new EventContext());

        public static EventViewModel EventToEventViewModel(Event evenement)
        {
            return new EventViewModel
            {
                Naam = evenement.Naam,
                DatumStart = evenement.Datumstart,
                DatumEind = evenement.Datumeind,
                MaxBezoekers = evenement.Maxbezoekers,
                Locatie = evenement.Locatie
            };
        }

//Make a "Account" view Model
        public static AccountViewModel ConvertAccounttoViewModel(Account account)
        {
            // convert an result to a viewmodel
            AccountViewModel accountViewModel = new AccountViewModel();

            accountViewModel.Account = accountRepository.GetById(account.Id);
            accountViewModel.Polsbandje = polsbandjeRepository.GetByAccountId(account);
            int id = accountViewModel.Polsbandje.ReserveringsId;
            accountViewModel.Reservering = reserveringRepository.GetById(id);
            return accountViewModel;
        }

        public static List<AccountViewModel> ConvertAccounttoViewModel(List<Account> accounts)
        {
            // convert a list of results to viewmodel results
            List<AccountViewModel> berichtenViewModelList = new List<AccountViewModel>();

            foreach (Account account in accounts)
            {
                berichtenViewModelList.Add(ConvertAccounttoViewModel(account));
            }

            return berichtenViewModelList;
        }

//Make a "Reservering" view model
        public static ReserveringViewModel ConvertReserveringtoViewModel(Reservering reservering)
        {
            // convert an result to a viewmodel
            ReserveringViewModel reserveringViewModel = new ReserveringViewModel();

            reserveringViewModel.Evenement = eventRepository.GetById(reservering.EvenementIDReservering.ID1);
            reserveringViewModel.Id = reservering.Id;
            reserveringViewModel.Voornaam = reservering.Voornaam;
            reserveringViewModel.Tussenvoegsel = reservering.Tussenvoegsel;
            reserveringViewModel.Achternaam = reservering.Achternaam;
            reserveringViewModel.Straat = reservering.Straat;
            reserveringViewModel.Huisnummer = reservering.Huisnummer;
            reserveringViewModel.Woonplaats = reservering.Woonplaats;
            reserveringViewModel.DatumStart = reservering.StartDatum;
            reserveringViewModel.DatumEind = reservering.EindDatum;
            reserveringViewModel.Betaalstatus = reservering.Betaalstatus;
            reserveringViewModel.Staanplaatsen = reservering.Staanplaatsen;
            reserveringViewModel.Accounts = ConvertAccounttoViewModel(reservering.Accounts);
            //reserveringViewModel.Producten = reservering.;
            reserveringViewModel.Exemplaren = reservering.ProductExemplaar;
            reserveringViewModel.Dagen = Convert.ToInt32((reservering.EindDatum - reservering.StartDatum).TotalDays);
            //reserveringViewModel.TotaalPrijs = 


            return reserveringViewModel;
        }


    }
}