using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class HostingController : Controller
    {
        AccountRepository accountrepo = new AccountRepository(new AccountContext());
        EventRepository eventrepo = new EventRepository(new EventContext());
        LocatieRepository locatierepo = new LocatieRepository(new LocatieContext());

        // GET: Hosting
        public ActionResult EventIndex()
        {
            List<Event> events = eventrepo.GetAllEvents();
            return View(events);
        }

        public ActionResult AccountIndex()
        {
            List<Account> accounts = accountrepo.GetAllAccounts();
            return View(accounts);
        }

        public ActionResult AccountCreate()
        {
            return View();
        }

        public ActionResult EventCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountCreate(FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here
                Account account = new Account(collection["Gebruikersnaam"], collection["Email"], Convert.ToString(accountrepo.newactivationhash()), false, collection["Wachtwoord"], collection["Telefoonnummer"]);
                accountrepo.Insert(account);
                return RedirectToAction("AccountIndex");
            }
            catch
            {
                return View();
            }
        }

      //  [HttpPost]
      //  public ActionResult EventCreate(FormCollection collection)
       // {
        //    try
        //    {

                // TODO: Add insert logic here
        //        Event evento = new Event(collection["Gebruikersnaam"], collection["Email"], Convert.ToString(accountrepo.newactivationhash()), false);
        //        accountrepo.Insert(evento);
        //        return RedirectToAction("AccountIndex");
       //     }
       //     catch
       //     {
       //         return View();
      //      }
      //  }

        // GET: Account/Edit/5
        public ActionResult AccountEdit(int id)
        {
            Account account = accountrepo.GetById(id);
            return View(account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult AccountEdit(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Account accounto = new Account(Convert.ToInt32(collection["Id"]),collection["GebruikersNaam"], collection["Email"], collection["Activatiehash"], true, collection["Wachtwoord"], collection["Telefoonnummer"]);
                accountrepo.Update(accounto);
                return RedirectToAction("AccountIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}