using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;

namespace EventsApplication.Models
{
    public class Product
    {
        private int id;
        private ProductCat categorie;
        private string merk;
        private string serie;
        private int typenummer;
        private decimal prijs;

        public int Id => id;
        public ProductCat Categorie => categorie;
        public string Merk => merk;
        public string Serie => serie;
        public int Typenummer => typenummer;
        public decimal Prijs => prijs;

        public Product(int id, int categorieid, string merk, string serie, int typenummer, decimal prijs)
        {
            this.id = id;
            ProductCatRepository repo = new ProductCatRepository(new ProductCatContext());
            this.categorie = repo.GetById(categorieid);
            this.merk = merk;
            this.serie = serie;
            this.typenummer = typenummer;
            this.prijs = prijs;
        }

        public Product(ProductCat categorie, string merk, string serie, int typenummer, decimal prijs)
        {
            this.categorie = categorie;
            this.merk = merk;
            this.serie = serie;
            this.typenummer = typenummer;
            this.prijs = prijs;
        }
    }
}