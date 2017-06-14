using EventsApplication.App_DAL;
using EventsApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.ViewModels;
using Phidgets;
using Phidgets.Events;

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
                if (!string.IsNullOrEmpty(Session["LoginToegangssysteem"] as string) && Session["LoginToegangssysteem"].ToString() == "true")
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
                    return RedirectToAction("Login");
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
                if (Session["LoginToegangssysteem"] == null || Session["LoginToegangssysteem"].ToString() != "true")
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
                    return RedirectToAction("Login");
                }

                
            }
            else
            {
                    return RedirectToAction("Index", "Home");
            }

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
            AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel(accountRepository.GetById(id));
            return View(acwm);
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
            Session["LoginToegangssysteem"] = "false";

            AccountRepository db = new AccountRepository(new AccountContext());

            Account userLoggedIn = db.Login(wachtwoord, gebruikersnaam);

            if (userLoggedIn != null && userLoggedIn.Administrator == true)
            {
                Session["LoginToegangssysteem"] = "true";
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
            Session["LoginToegangssysteem"] = "false";
            Session["RFID"] = null;
            return RedirectToAction("Index", "Home");
        }

        //Koppel RFID
        public ActionResult KoppelRFID()
        {
            if (Session["LoginToegangssysteem"] == null || Session["LoginToegangssysteem"].ToString() != "true")
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
                return RedirectToAction("Login");
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
            if (Session["LoginToegangssysteem"].ToString() != "true")
            {
                return RedirectToAction("Login");
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