using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.ViewModels
{
    public class ProductCatViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; } 
        public List<ProductViewModel> Products { get; set; }
    }
}