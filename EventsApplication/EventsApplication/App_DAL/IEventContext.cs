using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public interface IEventContext 
    {
        List<Event> GetAll();
    }
}