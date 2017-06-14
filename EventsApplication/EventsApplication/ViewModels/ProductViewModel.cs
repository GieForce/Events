using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public ProductCat Categorie { get; set; }
        public string Merk { get; set; }
        public string Serie { get; set; }
        public int Typenummer { get; set; }
        public decimal Prijs { get; set; }
    }
}