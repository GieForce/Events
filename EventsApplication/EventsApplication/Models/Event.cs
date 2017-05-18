using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Event
    {
        private int id;
        private int locatieId;
        private string naam;
        private DateTime datumStart;
        private DateTime datumEinde;
        private int maxBezoekers;

        public int Id => id;

        public int LocatieId => locatieId;

        public string Naam => naam;

        public DateTime DatumStart => datumStart;

        public DateTime DatumEinde => datumEinde;

        public int MaxBezoekers => maxBezoekers;

        public Event(int id, int locatieId, string naam, DateTime datumStart, DateTime datumEinde, int maxBezoekers)
        {
            this.id = id;
            this.locatieId = locatieId;
            this.naam = naam;
            this.datumStart = datumStart;
            this.datumEinde = datumEinde;
            this.maxBezoekers = maxBezoekers;
        }
    }
}