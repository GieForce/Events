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
        private RFID rfid;

        // GET: Toegangs
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Session["LoginToegangssysteem"] as string))
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
            else
            {
                return RedirectToAction("Login");
            }

        }
        [HttpPost]
        public ActionResult Index(string email, string activatiehash)
        {
            Account account = accountRepository.GetCompleteAccountByEmailAndActivationhash(email, activatiehash);
                if (account != null)
                {
                    Session["account"] = account;
                    return RedirectToAction("Index");
                }
            else
            {
                return View("Error");
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
            rfid.Antenna = false;
            rfid.LED = false;
            rfid.close();
            rfid = null;

            Session["LoginToegangssysteem"] = "false";
            return RedirectToAction("Index", "Home");
        }

        //Koppel RFID
        public ActionResult KoppelRFID(int accountID)
        {
            rfidReader();
            AccountViewModel acwm = ModelToViewModel.ConvertAccounttoViewModel(accountRepository.GetById(accountID));
            return View(acwm);
        }

        [HttpPost]
        public ActionResult KoppelRFID(int accountID, string RFID)
        {
            Account account = accountRepository.GetById(accountID);
            Polsbandje polsbandje = polsbandjeRepository.GetByAccountId(account);
            polsbandjeRepository.ConnectAccountWithRFID(RFID, polsbandje, account);
            return RedirectToAction("Index");
        }

        public void rfidReader()
        {
            rfid = new RFID();

            rfid.Attach += new AttachEventHandler(rfid_Attach);
            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Error += new ErrorEventHandler(rfid_Error);

            rfid.Tag += new TagEventHandler(rfid_Tag);
            rfid.TagLost += new TagEventHandler(rfid_TagLost);
            rfid.open();

            rfid.waitForAttachment();
            rfid.Antenna = true;
            rfid.LED = true;
        }

        public void rfid_Attach(object sender, AttachEventArgs e)
        {
            ViewBag.error("RFIDReader attached!",e.Device.SerialNumber.ToString());
        }

        public void rfid_Detach(object sender, DetachEventArgs e)
        {
            ViewBag.error("RFID reader detached!", e.Device.SerialNumber.ToString());
        }

        public void rfid_Error(object sender, ErrorEventArgs e)
        {
            ViewBag.error("RFID reader storing");
        }

        public void rfid_Tag(object sender, TagEventArgs e)
        {
            string tag = "0";
            try
            {
                tag = e.Tag;

                ViewBag.Error("Tag " + e.Tag + " scanned", e.Tag);
            }
            catch
            {
                ViewBag.Error("Kan tagnummer niet scannen.");
            }
            try
            {
                Account account = accountRepository.GetCompleteAccountsByRRFID(tag);
                Session["Account"] = account;
            }
            catch
            {
                ViewBag.Error("Onbekende RFID.");
            }
        }

        public void rfid_TagLost(object sender, TagEventArgs e)
        {
            RedirectToAction("Index");
        }
    }
}