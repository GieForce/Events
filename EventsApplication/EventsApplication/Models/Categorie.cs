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

        public Categorie(int id, Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage, int categorieId, string naam) : base(id, account, datum, soort, accountBijdrage)
        {
            this.categorieId = categorieId;
            this.naam = naam;
        }

        public Categorie(Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage, int categorieId, string naam) : base(account, datum, soort, accountBijdrage)
        {
            this.categorieId = categorieId;
            this.naam = naam;
        }

        public Categorie(int id, int categorieId, string naam) : base(id)
        {
            this.categorieId = categorieId;
            this.naam = naam;
        }

        public override string ToString()
        {
            string method = Naam;
            return method;
        }
    }
}
