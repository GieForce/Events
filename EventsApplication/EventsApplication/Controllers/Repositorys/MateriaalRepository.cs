using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL.Interfaces;

namespace EventsApplication.Controllers.Repositorys
{
    public class MateriaalRepository
    {
        private IMateriaalContext context;

        public MateriaalRepository(IMateriaalContext context)
        {
            this.context = context;
        }

        public List<Materiaal> GetMateriaalReservering(int reserveringsID)
        {
            return context.GetMateriaalReservering(reserveringsID);
        }

        public List<Materiaal> GetMateriaalEvent(Evenement evenement)
        {
            return context.GetMateriaalEvent(evenement);
        }

        public Materiaal Insert(Materiaal materiaal, Reservering reservering)
        {

            return context.Insert(materiaal, reservering);

        }

        public Materiaal Insert(Materiaal materiaal, Evenement evenement)
        {
            return context.Insert(materiaal, evenement);
        }


        public bool Update(Materiaal student)
        {
            return context.Update(student);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }
    }
}