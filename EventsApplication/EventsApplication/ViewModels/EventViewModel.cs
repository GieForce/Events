using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace EventsApplication.ViewModels
{
    public class EventViewModel
    {
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Naam { get; set; }
        public DateTime DatumStart { get; set; }
        public DateTime DatumEind { get; set; }
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        public int MaxBezoekers { get; set; }
        public Locatie Locatie { get; set; }
    }
}