using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class BijdrageRepository
    {
        private readonly IBijdrageContext context;

        public BijdrageRepository(IBijdrageContext context)
        {
            this.context = context;
        }

        public Bijdrage GetById(int id)
        {
            return context.GetById(id);
        }

        public bool Insert(Bericht bericht)
        {
            return context.Insert(bericht);
        }


        //public bool Delete(int id)
        //{
        //    return context.Delete(id);
        //}

        public List<Bijdrage> GetAllBijdrages()
        {
            return context.GetAllBijdrages();
        }

        //public bool Update(Bijdrage bijdrage)
        //{
        //    return context.Update(bijdrage);
        //}
    }
}