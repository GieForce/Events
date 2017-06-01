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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Bericht bericht)
        {
            bericht.Datum = DateTime.Now;
            try
            {

                repository.Insert(new Bericht(bericht.Id, bericht.AccountId, bericht.Datum, bericht.Soort, bericht.Titel, bericht.Inhoud));
                return RedirectToAction("Index", "MediaSharing");
            }
            catch
            {
                return View();
            }
        }
    }
}
