using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Verhuur
    {
        private int id;
        private int prodcutExemplaar_id;
        private int res_pb_id;
        private DateTime datumIn;
        private DateTime datumUit;
        private decimal prijs;
        private bool betaald;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int ProdcutExemplaar_id
        {
            get
            {
                return prodcutExemplaar_id;
            }

            set
            {
                prodcutExemplaar_id = value;
            }
        }

        public int Res_pb_id
        {
            get
            {
                return res_pb_id;
            }

            set
            {
                res_pb_id = value;
            }
        }

        public DateTime DatumIn
        {
            get
            {
                return datumIn;
            }

            set
            {
                datumIn = value;
            }
        }

        public DateTime DatumUit
        {
            get
            {
                return datumUit;
            }

            set
            {
                datumUit = value;
            }
        }

        public decimal Prijs
        {
            get
            {
                return prijs;
            }

            set
            {
                prijs = value;
            }
        }

        public bool Betaald
        {
            get
            {
                return betaald;
            }

            set
            {
                betaald = value;
            }
        }

        public Verhuur(int id, int prodcutExemplaar_id, int res_pb_id, DateTime datumIn, DateTime datumUit, decimal prijs, bool betaald)
        {
            this.Id = id;
            this.ProdcutExemplaar_id = prodcutExemplaar_id;
            this.Res_pb_id = res_pb_id;
            this.DatumIn = datumIn;
            this.DatumUit = datumUit;
            this.Prijs = prijs;
            this.Betaald = betaald;
        }

        public Verhuur(int prodcutExemplaar_id, int res_pb_id, DateTime datumIn, DateTime datumUit, decimal prijs, bool betaald)
        {
            this.ProdcutExemplaar_id = prodcutExemplaar_id;
            this.Res_pb_id = res_pb_id;
            this.DatumIn = datumIn;
            this.DatumUit = datumUit;
            this.Prijs = prijs;
            this.Betaald = betaald;
        }
    }
}
