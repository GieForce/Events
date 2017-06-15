using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepository = new AccountRepository(new AccountContext());
        // GET: Account
        public ActionResult Index()
        {
            List<Account> accounts = accountRepository.GetAllAccounts();
            return View(accounts);
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            Account account = accountRepository.GetById(id);
            return View(account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Account/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        Account account = new Account(collection["Gebruikersnaam"], collection["Email"], collection["Activatiehash"], false);
        //        accountRepository.Insert(account);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Account/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Account/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        Account account = new Account(id, collection["Naam"], collection["Email"], collection["Activatiehash"], Convert.ToBoolean(collection["Geactiveerd"]), C);
        //        accountRepository.Update(account);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Account/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Account account = accountRepository.GetById(id);
        //    return View(account);
        //}

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                if (accountRepository.Delete(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Delete");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();  
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string gebruikersnaam, string wachtwoord)
        {
            AccountRepository db = new AccountRepository(new AccountContext());

            Account userLoggedIn = db.Login(wachtwoord, gebruikersnaam);

            if (userLoggedIn != null && userLoggedIn.Status)
            {
                Session["adminLogin"] = "true";
                return RedirectToAction("HostingHome", "Hosting");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string gebruikersnaam, string wachtwoord)
        {
            AccountRepository db = new AccountRepository(new AccountContext());

            Account userLoggedIn = db.Login(wachtwoord, gebruikersnaam);
            
            if (userLoggedIn != null && userLoggedIn.Status == true)
            {
                Session["user"] = userLoggedIn;
                return RedirectToAction("Index", "MediaSharing");
            }
            else if (userLoggedIn != null && userLoggedIn.Status == false)
            {
                Session["user"] = userLoggedIn;
                return RedirectToAction("Index", "MediaSharing");
            }

            else 
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
