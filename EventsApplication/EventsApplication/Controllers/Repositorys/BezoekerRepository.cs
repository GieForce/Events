using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Repositorys
{
    public class BezoekerRepository
    {
        private IBezoekerContext context;

        public BezoekerRepository(IBezoekerContext context)
        {
            this.context = context;
        }

        public List<Bezoeker> GetBezoeker(int eventID)
        {
            return context.GetBezoeker(eventID);
        }

        public bool getBetaalStatus(int id)
        {
            return context.getBetaalStatus(id);
        }

        public Bezoeker Insert(Bezoeker bezoeker, Reservering reservering)
        {

            return context.Insert(bezoeker, reservering);

        }


        public bool Update(Bezoeker bezoeker)
        {
            return context.Update(bezoeker);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }

        public Bezoeker GetById(int id)
        {
            return context.GetById(id);
        }

        public Bezoeker GetByName(string naam, string email)
        {
            return context.GetByName(naam, email);
        }

        public void checkIn(int id)
        {
            context.checkIn(id);
        }

        public void checkOut(int id)
        {
            context.checkOut(id);
        }
    }
}