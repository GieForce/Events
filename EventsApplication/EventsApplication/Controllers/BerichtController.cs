using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class BerichtController : Controller
    {
        BerichtRepository berichtRepository = new BerichtRepository(new BerichtContext());

        // GET: Bericht
        public ActionResult Index()
        {
            return View();
        }
    }
}