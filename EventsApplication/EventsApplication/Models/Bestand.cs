using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Bestand : Bijdrage
    {
        private int categorieId;
        private string bestandlocatie;
        private int grootte;

        public int CategorieId
        {
            get { return categorieId; }
            set { categorieId = value; }
        }

        public string Bestandlocatie
        {
            get { return bestandlocatie; }
            set { bestandlocatie = value; }
        }

        public int Grootte
        {
            get { return grootte; }
            set { grootte = value; }
        }

       

        public Bestand(int id, Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage, int categorieId, string bestandlocatie, int grootte) : base(id, account, datum, soort, accountBijdrage)
        {
            this.categorieId = categorieId;
            this.bestandlocatie = bestandlocatie;
            this.grootte = grootte;
        }

        public Bestand(Account account, DateTime datum, string soort, AccountBijdrage accountBijdrage, int categorieId, string bestandlocatie, int grootte) : base(account, datum, soort, accountBijdrage)
        {
            this.categorieId = categorieId;
            this.bestandlocatie = bestandlocatie;
            this.grootte = grootte;
        }
    }
}