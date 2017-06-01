using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public interface IEventContext
    {
        List<Event> GetAllEvents();
        bool Insert(Event eventi);
        bool Delete(int id);
        Event GetById(int eventId);
    }
}
