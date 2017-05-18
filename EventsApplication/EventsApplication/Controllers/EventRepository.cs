using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class EventRepository
    {
        private IEventContext context;

        public EventRepository(IEventContext context)
        {
            this.context = context;
        }

        public List<Event> GetAll()
        {
            return context.GetAll();
        }
    }
}