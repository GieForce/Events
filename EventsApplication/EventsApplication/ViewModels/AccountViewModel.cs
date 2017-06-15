using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsApplication.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public Polsbandje Polsbandje { get; set; }
        public Reservering Reservering { get; set; }
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Gebruikersnaam { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Wachtwoord { get; set; }
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Telefoonnummer { get; set; }

    }
}