using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class BijdrageController : Controller
    {
        BijdrageRepository repository = new BijdrageRepository(new BijdrageContext());
        // GET: Bijdrage
        public ActionResult Index()
        {
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            return View(bijdrages);
        }

        // GET: Bijdrage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
