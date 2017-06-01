using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;
using EventsApplication.App_DAL;

namespace EventsApplication.Controllers
{
    public class EventRepository
    {
        private readonly IEventContext context;

        public EventRepository(IEventContext context)
        {
            this.context = context;
        }

        public bool Insert(Event eventi)
        {
            return context.Insert(eventi);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }

        public List<Event> GetAllEvents()
        {
            return context.GetAllEvents();
        }
    }
}