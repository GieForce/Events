using EventsApplication.App_DAL;
using EventsApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.ViewModels;

namespace EventsApplication.Controllers
{
    public class ToegangsController : Controller
    {
        AccountRepository accountRepository = new AccountRepository(new AccountContext());
        PolsbandjeRepository polsbandjeRepository = new PolsbandjeRepository(new PolsbandjeContext());
        ReserveringRepository reserveringRepository = new ReserveringRepository(new ReserveringContext());

        // GET: Toegangs
        public ActionResult Index()
        {
            if (Session["event"] != null)
            {
                if (!string.IsNullOrEmpty(Session["adminLogin"] as string) && Session["adminLogin"].ToString() == "true")
                {
                    if (Session["account"] == null)
                    {
                        return View();
                    }
                    else
                    {
                        AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel((Account)Session["account"]);
                        return View(acwm);
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Account");
                }
            }
            else
            {
                    return RedirectToAction("Index", "Home");
            }
          }

        [HttpPost]
        public ActionResult Index(string email, string activatiehash)
        {
            if (Session["event"] != null)
            {
                if (Session["adminLogin"] != null || Session["adminLogin"].ToString() == "true")
                {
                    Event huidigEvent = (Event)Session["event"];
                    ModelToViewModel.EventToEventViewModel(huidigEvent);

                    Account account = accountRepository.GetCompleteAccountByEmailAndActivationhash(email, activatiehash);

                    if (account != null)
                    {
                        Polsbandje polsbandje = polsbandjeRepository.GetByAccountId(account);
                        Reservering reservering = reserveringRepository.GetById(polsbandje.ReserveringsId);

                        Session["account"] = account;

                        //Check if account made a reservation for current event
                        if (reservering.EvenementIDReservering.ID1 == huidigEvent.ID1)
                        {
                            Session["account"] = account;
                            AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel(account);
                            return View(acwm);
                        }
                        else
                        {
                            return View("Error");
                        }

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Account");
                }
            }
            else
            {
                    return RedirectToAction("Index", "Home");
            }

         }

        public ActionResult AanwezigAfwezig()
        {
            if (Session["adminLogin"] != null || Session["adminLogin"].ToString() == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        [HttpPost] //Actionresult scanning RFID
        public ActionResult AanwezigAfwezig(string RFID)
        {
            Event huidigEvent = (Event)Session["event"];
            ModelToViewModel.EventToEventViewModel(huidigEvent);

            Account account = accountRepository.GetCompleteAccountsByRRFID(RFID);

            if (account != null)
            {
                Polsbandje polsbandje = polsbandjeRepository.GetByAccountId(account);
                Reservering reservering = reserveringRepository.GetById(polsbandje.ReserveringsId);

                if (polsbandje.Aanwezig)
                {
                    polsbandjeRepository.setPolsbandjeAfwezig(polsbandje);
                    Session["in-/uitcheck"] = "Status veranderd naar afwezig";
                    return View();
                }
                else
                {
                    //Check if account made a reservation for current event
                    if (reservering.EvenementIDReservering.ID1 == huidigEvent.ID1)
                    {
                        polsbandjeRepository.setPolsbandjeAanwezig(polsbandje);
                        AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel(account);
                        Session["in-/uitcheck"] = "Status veranderd naar aanwezig";
                        return View(acwm);
                    }
                    else
                    {
                        return View("Error");
                    }
                }

            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        // GET: Account present at festival
        public ActionResult Aanwezig()
        {
            if (Session["event"] != null)
            {
                if (Session["adminLogin"] != null || Session["adminLogin"].ToString() == "true")
                {
                    List<AccountViewModel> acwm = ModelToViewModel.ConvertAccounttoViewModel(accountRepository.GetAllAccountsPresentAtFestival((Event)Session["event"]));
                    return View(acwm);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Account");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Details from account
        public ActionResult Details(int id)
        {
            if (Session["event"] != null)
            {
                if (Session["adminLogin"] != null || Session["adminLogin"].ToString() == "true")
                {
                    AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel(accountRepository.GetById(id));
                    return View(acwm);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Account");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(string gebruikersnaam, string wachtwoord)
        {
            Session["adminLogin"] = "false";

            AccountRepository db = new AccountRepository(new AccountContext());

            Account userLoggedIn = db.Login(wachtwoord, gebruikersnaam);

            if (userLoggedIn != null && userLoggedIn.Administrator == true)
            {
                Session["adminLogin"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        //Uitloggen
        public ActionResult LogOff()
        {
            Session["adminLogin"] = "false";
            Session["RFID"] = null;
            return RedirectToAction("Index", "Home");
        }

        //Koppel RFID
        public ActionResult KoppelRFID()
        {
            if (Session["adminLogin"] != null && Session["adminLogin"].ToString() == "true")
            {
                if (Session["account"] == null)
                {
                    return View();
                }
                else
                {
                    AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel((Account)Session["account"]);
                    return View(acwm);
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        [HttpPost]
        public ActionResult KoppelRFID(string RFID)
        {
            Session["RFID"] = RFID;
            Account account = (Account)Session["account"];
            Polsbandje polsbandje = polsbandjeRepository.GetByAccountId(account);
            polsbandjeRepository.ConnectAccountWithRFID(RFID, polsbandje, account);
            return RedirectToAction("Index");
        }

        public ActionResult Materialen()
        {
            if (Session["adminLogin"].ToString() != "true")
            {
                return RedirectToAction("AdminLogin", "Account");
            }
            else
            {
                if (Session["account"] == null)
                {
                    return View();
                }
                else
                {
                    AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel((Account)Session["account"]);
                    return View(acwm);
                }
            }

        }
    }
}