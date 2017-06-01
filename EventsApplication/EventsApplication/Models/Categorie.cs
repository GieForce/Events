using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Categorie : Bijdrage
    {
        private string naam;
   

        public string Naam
        {
          get { return naam; }
          set { naam = value; }
        }

        public Categorie(int id, int accountId, DateTime datum, string soort, string naam) : base(id, accountId, datum, soort)
        {
            this.naam = naam;
        }

        public Categorie(int accountId, DateTime datum, string soort, string naam) : base(accountId, datum, soort)
        {
            this.naam = naam;
        }
    }
}
