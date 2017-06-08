using EventsApplication.App_DAL;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.ViewModels;
using System.Security.Principal;
using EventsApplication.ViewModels;

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

        // GET: Account present at festival
        public ActionResult Aanwezig()
        {
            List<Account> accountsPresent = accountRepository.GetAllAccountsPresent();
            return View("Aanwezig", accountsPresent);
        }

        // GET: Details from account
        public ActionResult Details(int id)
        {
            AanwezigAccountViewModel acwm = ModelToViewModel.ConvertBerichtToViewModel(accountRepository.GetById(id));
            return View(acwm);
        }
    }
}