using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;

namespace EventsApplication.Controllers
{
    public class HomeController : Controller
    {

        

        public ActionResult Index()
        {
            EventRepository eventRepo = new EventRepository(new EventContext());
            return View(eventRepo.GetAll());
        }


    }
}