using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class ReserveerController : Controller
    {
        // GET: Reserveer
        public ActionResult Index()
        {
            Event evenement = (Event) Session["event"];
            StandplaatsRepository repo = new StandplaatsRepository(new StandplaatsContext());
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["standplaatsen"] = repo.GetByLocatie(evenement.Locatieid);
            return View();
        }
    }
}