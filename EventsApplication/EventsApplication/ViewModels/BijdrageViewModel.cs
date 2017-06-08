using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class BerichtenViewModel
    {
        public List<Bericht> berichtenList { get; set; }
        public Account account { get; set; }
    }

    public class BerichtViewModel
    {
        public string titel { get; set; }
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Het bericht dient minimaal 4 tekens te bevatten")]
        [Required(ErrorMessage = "Het bericht mag niet leeg zijn")]
        public string inhoud { get; set; }
    }

    public class MediaBerichtViewModel
    {
        public List<Categorie> categorieList { get; set; }
        public string bestandslocatie { get; set; }
    }
}