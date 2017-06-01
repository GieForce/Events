using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.Models
{
    public class Standplaats
    {
        private int id;
        private int plaatsnummer;
        private int capacitieit;
        private bool comfort;
        private bool handicap;
        private bool kraan;

        public int Id => id;

        public int Plaatsnummer => plaatsnummer;

        public int Capacitieit => capacitieit;

        public bool Comfort => comfort;

        public bool Handicap => handicap;

        public bool Kraan => kraan;

        public Standplaats(int id, int plaatsnummer, int capacitieit, bool comfort, bool handicap, bool kraan)
        {
            this.id = id;
            this.plaatsnummer = plaatsnummer;
            this.capacitieit = capacitieit;
            this.comfort = comfort;
            this.handicap = handicap;
            this.kraan = kraan;
        }
    }
}