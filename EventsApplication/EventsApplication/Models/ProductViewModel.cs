using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class ProductViewModel
    {
        private string merk;
        private string serie;
        private int typenummer;
        private decimal prijs;
        private string categorie;
        private int hoeveelheid;

        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Merk => merk;
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Serie => serie;
        public int Typenummer => typenummer;
        public decimal Prijs => prijs;
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 character required")]
        [Required(ErrorMessage = "Dit veld mag niet leeg zijn")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dit veld mag niet leeg zijn")]
        public string Categorie => categorie;
        public int Hoeveelheid => hoeveelheid;

         public ProductViewModel(string merk, string serie, int typenummer, decimal prijs, string categorie, int hoeveelheid)
        {
            this.categorie = categorie;
            this.merk = merk;
            this.serie = serie;
            this.typenummer = typenummer;
            this.prijs = prijs;
            this.hoeveelheid = hoeveelheid;
        }

    }
}