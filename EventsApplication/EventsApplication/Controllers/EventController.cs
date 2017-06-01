using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.Models;
using EventsApplication.App_DAL;


namespace EventsApplication.Controllers
{   
    public class EventController : Controller
    {
        EventRepository eventrepository = new EventRepository(new EventContext());
        // GET: Event
        public ActionResult Index()
        {
            List<Event> events = eventrepository.GetAllEvents();
            return View(events);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here
                //Account evento = new Event(collection["Naam"], collection["Email"], collection["Activatiehash"], false);
                //eventrepository.Insert(evento);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}