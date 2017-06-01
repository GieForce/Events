using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Bericht
    {
        private string titel;
        private string inhoud;

        public string Titel
        {
            get { return titel; }
            set { titel = value; }
        }

        public string Inhoud
        {
            get { return inhoud; }
            set { inhoud = value; }
        }

        public Bericht(int id, string titel, string inhoud)/* : base(id)*/
        {
            this.titel = titel;
            this.inhoud = inhoud;
        }
        public Bericht(string titel, string inhoud)
        {
            this.titel = titel;
            this.inhoud = inhoud;
        }
    }
}
