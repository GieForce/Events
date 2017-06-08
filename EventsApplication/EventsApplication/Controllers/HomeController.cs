using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EventRepository eventRepo = new EventRepository(new EventContext());
            return View(eventRepo.GetAllEvents());
        }

        [HttpPost]
        public ActionResult Index(int eventid)
        {
            EventRepository eventRepo = new EventRepository(new EventContext());
            Event event1 = eventRepo.GetById(eventid);
            Session["event"] = event1;
            return RedirectToAction("Home", "Home");
        }

        public ActionResult Home()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Event e = (Event) Session["event"];
            return View(ModelToViewModel.EventToEventViewModel(e));
        }
    }
}