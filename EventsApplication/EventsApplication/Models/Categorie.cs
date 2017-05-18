using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Categorie
    {
        private string naam;
   

        public string Naam
        {
          get { return naam; }
          set { naam = value; }
        }

        public Categorie(int id, int categorieID, string naam)  : base(id)
        {
            this.naam = naam;
        }

        public Categorie(int categorieID, string naam) : base(id)
        {
            this.naam = naam;
        }
    }
}
