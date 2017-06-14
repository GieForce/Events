using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public class ReserveringViewModel
    {
        public Event Evenement { get; set; }
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public DateTime DatumStart { get; set; }
        public DateTime DatumEind { get; set; }
        public bool Betaalstatus { get; set; }
        public List<Standplaats> Staanplaatsen { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public List<Product> Producten { get; set; }
        public List<ProductExemplaar> Exemplaren { get; set; }
        public int Dagen { get; set; }
        public double TotaalPrijs { get; set; }
    }
}