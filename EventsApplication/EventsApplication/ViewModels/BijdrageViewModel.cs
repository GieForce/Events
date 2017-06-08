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

    public class PostViewModel
    {
        public Account account { get; set; }

     //   public Bijdrage bijdrage { get; set; }
        public Bericht bericht { get; set; }
        public  Categorie categorie { get; set; }
        public Bestand bestand { get; set; }
    }

}