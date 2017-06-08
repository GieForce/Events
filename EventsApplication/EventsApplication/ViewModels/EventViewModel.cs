using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public class EventViewModel
    {
        public string Naam { get; set; }
        public DateTime DatumStart { get; set; }
        public DateTime DatumEind { get; set; }
        public int MaxBezoekers { get; set; }
        public Locatie Locatie { get; set; }
    }
}