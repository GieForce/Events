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

//Make a "Aanwezig account" view Model
        public static AanwezigAccountViewModel ConvertBerichtToViewModel(Account account)
        {
            // convert an result to a viewmodel
            AanwezigAccountViewModel aanwezigAccountModel = new AanwezigAccountViewModel();

            aanwezigAccountModel.Account = accountRepository.GetById(account.Id);
            aanwezigAccountModel.Polsbandje = polsbandjeRepository.GetByAccountId(account);
            aanwezigAccountModel.Reservering = reserveringRepository.GetById(aanwezigAccountModel.Polsbandje.ReserveringsId);
            return aanwezigAccountModel;
        }

        public static List<AanwezigAccountViewModel> ConvertBerichtToViewModel(List<Account> accounts)
        {
            // convert a list of results to viewmodel results
            List<AanwezigAccountViewModel> berichtenViewModelList = new List<AanwezigAccountViewModel>();

            foreach (Account account in accounts)
            {
                berichtenViewModelList.Add(ConvertBerichtToViewModel(account));
            }

            return berichtenViewModelList;
        }

    }
}