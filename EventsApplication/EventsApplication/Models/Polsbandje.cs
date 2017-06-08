using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Polsbandje
    {

        private int id;
        public string barcode;
        public int actief;
        private int reserveringsId;
        private int polsbandjeId;
        private int accountId;
        private bool aanwezig;

        public int Id
        {
            get { return id; }
        }

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public int Actief
        {
            get { return actief; }
            set { actief = value; }
        }

        public int ReserveringsId
        {
            get { return reserveringsId; }
            set { reserveringsId = value; }
        }

        public int PolsbandjeId
        {
            get { return polsbandjeId; }
            set { polsbandjeId = value; }
        }

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        public bool Aanwezig
        {
            get { return aanwezig; }
            set { aanwezig = value; }
        }

        public Polsbandje(int id, string barcode, int actief, int reserveringsId, int polsbandjeId, int accountId, int aanwezig)
        {
            this.id = id;
            this.barcode = barcode;
            this.actief = actief;
            this.reserveringsId = reserveringsId;
            this.polsbandjeId = polsbandjeId;
            this.accountId = accountId;

            if (aanwezig == 1)
            {
                this.aanwezig = true;
            }
            else if (aanwezig == 1)
            {
                this.aanwezig = false;
            }
        }

        public Polsbandje(int reserveringsId, int polsbandjeId, int accountId)
        {
            this.reserveringsId = reserveringsId;
            this.polsbandjeId = polsbandjeId;
            this.accountId = accountId;
            this.aanwezig = false;
        }

    }
}