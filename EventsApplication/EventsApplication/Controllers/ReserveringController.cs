using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsApplication.Controllers
{
    public class ReserveringController : Controller
    {
        // GET: Reservering
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reservering/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservering/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservering/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservering/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reservering/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservering/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservering/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
