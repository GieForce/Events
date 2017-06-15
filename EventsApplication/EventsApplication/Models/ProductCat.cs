using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using System.ComponentModel.DataAnnotations;

namespace EventsApplication.Models
{
    public class ProductCat
    {
        private int id;
        private List<ProductCat> subCats;
        private string naam;

        public int Id => id;
        public List<ProductCat> SubCats { get {
            ProductCatRepository repo = new ProductCatRepository(new ProductCatContext());
            return this.subCats = repo.GetByProductCat(this);
        } }
        public string Naam => naam;

        public ProductCat(int id , string naam)
        {
            this.id = id;
            this.naam = naam;
        }

        
        public ProductCat(string naam)
        {
            this.naam = naam;
        }


        public ProductCat(List<ProductCat> subcats, string naam)
        {
            this.subCats = subcats;
            this.naam = naam;
        }
    }
}