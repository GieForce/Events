using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Categorie : Bijdrage
    {
        private int categorieId;
        private string naam;

        public int CategorieId
        {
            get { return categorieId; }
            set { categorieId = value; }
        }

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public Categorie(int id, int accountId, DateTime datum, string soort, int categorieId, string naam) : base(id, accountId, datum, soort)
        {
            this.categorieId = categorieId;
            this.naam = naam;
        }

        public Categorie(int accountId, DateTime datum, string soort, int categorieId, string naam) : base(accountId, datum, soort)
        {
            this.categorieId = categorieId;
            this.naam = naam;
        }
    }
}
