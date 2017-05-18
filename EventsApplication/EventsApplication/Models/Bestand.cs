using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Bestand
    {
        private int bijdrageId;
        private int categorieId;
        private string bestandlocatie;
        private int grootte;

        public int BijdrageId
        {
            get { return bijdrageId; }
            set { bijdrageId = value; }
        }

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

        public Bestand(int bijdrageId, int categorieId, string bestandlocatie, int grootte)
        {
            this.bijdrageId = bijdrageId;
            this.categorieId = categorieId;
            this.bestandlocatie = bestandlocatie;
            this.grootte = grootte;
        }

        public Bestand(int categorieId, string bestandlocatie, int grootte)
        {
            this.categorieId = categorieId;
            this.bestandlocatie = bestandlocatie;
            this.grootte = grootte;
        }
    }
}