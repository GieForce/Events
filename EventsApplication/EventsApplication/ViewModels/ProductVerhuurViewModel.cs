using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.ViewModels
{
    public class ProductVerhuurViewModel
    {
        public List<ProductCatViewModel> ProductCats { get; set; }
        public List<ProductViewModel> AllProducts { get; set; }
        public List<ProductViewModel> SelectedProducts { get; set; }
    }
}