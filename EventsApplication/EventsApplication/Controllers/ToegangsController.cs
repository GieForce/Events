using EventsApplication.App_DAL;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsApplication.Controllers
{
    public class ToegangsController : Controller
    {
        AccountRepository accountRepository = new AccountRepository(new AccountContext());

        // GET: Toegangs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account present at fesftival
        public ActionResult Aanwezig()
        {
            List<Account> accountsPresent = accountRepository.GetAllAccountsPresent();
            return View("Aanwezig", accountsPresent);
        }
    }
}