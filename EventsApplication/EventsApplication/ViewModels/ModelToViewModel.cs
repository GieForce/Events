using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public static class ModelToViewModel
    {
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
    }
}