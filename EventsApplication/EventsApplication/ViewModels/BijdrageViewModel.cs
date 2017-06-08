using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public class BijdrageViewModel
    {
        public List<Bijdrage> bijdrageList { get; set; }
        public Account account { get; set; }
    }
}