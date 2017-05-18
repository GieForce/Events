using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class BijdrageBericht
    {
        private int bijdrageId;
        private int berichtId;

        public int BijdrageId
        {
            get { return bijdrageId; }
            set { bijdrageId = value; }
        }

        public int BerichtId
        {
            get { return berichtId; }
            set { berichtId = value; }
        }

        public BijdrageBericht(int bijdrageId, int berichtId)
        {
            this.bijdrageId = bijdrageId;
            this.berichtId = berichtId;
        }
    }
}