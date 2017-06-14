using System;
using System.Collections.Generic;
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


        public string Merk => merk;
        public string Serie => serie;
        public int Typenummer => typenummer;
        public decimal Prijs => prijs;
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