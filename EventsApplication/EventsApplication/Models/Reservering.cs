using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Controllers;
using EventsApplication.Controllers.Repositorys;

namespace EventsApplication.Models
{
    public class Reservering
    {
        StandplaatsRepository standplaatsRepository = new StandplaatsRepository(new StandplaatsContext());
        AccountRepository accountRepository = new AccountRepository(new AccountContext());

        private int id;
        private string voornaam;
        private string tussenvoegsel;
        private string achternaam;
        private string straat;
        private int huisnummer;
        private string woonplaats;
        private int betaald;
        private bool betaalstatus;
        private DateTime startDatum;
        private DateTime eindDatum;
        private int standplaatsID;

        private Standplaats standplaats;
        private List<Account> accounts;

        public int Id
        {
            get { return id; }
        }

        public string Voornaam
        {
            get { return voornaam; }
        }

        public string Tussenvoegsel
        {
            get { return tussenvoegsel; }
        }

        public string Achternaam
        {
            get { return achternaam; }
        }

        public string Straat
        {
            get { return straat; }
        }

        public int Huisnummer
        {
            get { return huisnummer; }
        }

        public string Woonplaats
        {
            get { return woonplaats; }
        }

        public int Betaald
        {
            get { return betaald;}
        }

        public bool Betaalstatus
        {
            get { return betaalstatus; }
        }

        public DateTime StartDatum
        {
            get { return startDatum; }
        }

        public DateTime EindDatum
        {
            get { return eindDatum; }
        }

        public int StandplaatsId
        {
            get { return standplaatsID; }
        }

        public Standplaats Staanplaats
        {
            get { return standplaats; }
            set { standplaats = value; }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        public Reservering(int id, string voornaam, string tussenvoegsel, string achternaam, string straat, int huisnummer, string woonplaats, int betaald, DateTime startDatum, DateTime eindDatum, int standplaatsId)
        {
            this.id = id;
            this.voornaam = voornaam;
            this.tussenvoegsel = tussenvoegsel;
            this.achternaam = achternaam;
            this.straat = straat;
            this.huisnummer = huisnummer;
            this.woonplaats = woonplaats;
            this.betaald = betaald;

            if (betaald == 1)
            {
                betaalstatus = true;
            }
            else if (betaald == 0)
            {
                betaalstatus = false;
            }

            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.standplaatsID = standplaatsId;

            this.standplaats = standplaatsRepository.GetByReservation(id);
            accounts = accountRepository.GetAllAccountsByReservation(id);
        }

        public Reservering(string voornaam, string tussenvoegsel, string achternaam, string straat, int huisnummer, string woonplaats, int betaald, DateTime startDatum, DateTime eindDatum, int standplaatsId)
        {
            this.voornaam = voornaam;
            this.tussenvoegsel = tussenvoegsel;
            this.achternaam = achternaam;
            this.straat = straat;
            this.huisnummer = huisnummer;
            this.woonplaats = woonplaats;
            this.betaald = betaald;

            if (betaald == 1)
            {
                betaalstatus = true;
            }
            else if (betaald == 0)
            {
                betaalstatus = false;
            }

            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.standplaatsID = standplaatsId;
        }
    }
}