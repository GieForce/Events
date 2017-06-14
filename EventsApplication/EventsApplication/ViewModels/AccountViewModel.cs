using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public Polsbandje Polsbandje { get; set; }
        public Reservering Reservering { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Telefoonnummer { get; set; }

    }
}