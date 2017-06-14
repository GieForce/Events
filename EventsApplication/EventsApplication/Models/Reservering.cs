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
        ProductExemplaarRepository productExemplaarRepository = new ProductExemplaarRepository(new ProductExemplaarContext());
        EventRepository eventRepository = new EventRepository(new EventContext());

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

        private Event evenementIDReservering;

        private List<Standplaats> staanplaatsen;
        private List<Account> accounts;
        private List<ProductExemplaar> productExemplaar;

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

        public List<Standplaats> Staanplaatsen
        {
            get { return staanplaatsen; }
            set { staanplaatsen = value; }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        public List<ProductExemplaar> ProductExemplaar
        {
            get { return productExemplaar; }
            set { productExemplaar = value; }
        }

        public Event EvenementIDReservering
        {
            get { return evenementIDReservering; }
            set { evenementIDReservering = value; }
        }

        public Reservering(int id, string voornaam, string tussenvoegsel, string achternaam, string straat, int huisnummer, string woonplaats, int betaald, DateTime startDatum, DateTime eindDatum, int standplaatsId, int eventID)
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

            this.evenementIDReservering = eventRepository.GetById(eventID);
            this.staanplaatsen = standplaatsRepository.GetByReservation(id);
            this.accounts = accountRepository.GetAllAccountsByReservation(id);
            this.productExemplaar = productExemplaarRepository.GetProductsByReservation(id);
        }

        public Reservering(string voornaam, string tussenvoegsel, string achternaam, string straat, int huisnummer, string woonplaats, int betaald, DateTime startDatum, DateTime eindDatum, int standplaatsId, int eventID)
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

            this.evenementIDReservering = eventRepository.GetById(eventID);
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            this.standplaatsID = standplaatsId;
        }
    }
}