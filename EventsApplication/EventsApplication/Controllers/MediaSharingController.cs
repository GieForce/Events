﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.Controllers
{
    public class MediaSharingController : Controller
    {
        BijdrageRepository repository = new BijdrageRepository(new BijdrageContext());
        AccountRepository accountRepository = new AccountRepository(new AccountContext());
        CategorieRepository categorieRepository = new CategorieRepository(new CategorieContext());
        
        // GET: MediaSharing
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                List<Bijdrage> bijdrages = repository.GetAllBijdrages();
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
                return View(bvm);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult AdminIndex()
        {
            if (Session["user"] != null)
            {
                List<Bijdrage> bijdrages = repository.GetallreportedBijdrages();
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
                return PartialView("Bijdrages",bvm);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AdminPanel()
        {
            if (Session["user"] != null)
            {
                System.Threading.Thread.Sleep(2500);
                List<Bijdrage> bijdrages = repository.GetallreportedBijdrages();
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
                return PartialView("Bijdrages", bvm);
            }
            else
            {
                return View("Error");
            }
        }


        public ActionResult ShowPosts()
        {
            System.Threading.Thread.Sleep(2500);
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
            return PartialView("Bijdrages", bvm);
        }

        public ActionResult ShowAccountByID()
        {
            System.Threading.Thread.Sleep(1500);
            Account account = accountRepository.GetById(1);
            return PartialView("Account", account);
        }

        public ActionResult ShowReactions()
        {
            //System.Threading.Thread.Sleep(2500);
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
            return PartialView("Berichten", bvm);
        }

        public ActionResult ShowBijdragesByUserID()
        {
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            List<Bijdrage> bijdrages = repository.GetAllBijdragesByUserId(account.Id);
            BijdrageViewModel bvm = new BijdrageViewModel{bijdrageList = bijdrages, account = account};
            return PartialView("Bijdrages", bvm);
        }
        public ActionResult CreatePost()
        {

            return PartialView("Create");
        }

        
        public ActionResult LoadBerichtenByPostId(int id)
        {
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            List<Bericht> berichtenList = repository.LoadBerichtenByPostId(id);
            BerichtenViewModel bvm = new BerichtenViewModel {berichtenList = berichtenList, account = account};
            if (berichtenList.Count == 0)
            {
                return PartialView("NoComments");
            }
            else
            {
                return PartialView("Comments", bvm);
               
            }
            
        }

        public ActionResult CreateNewMediaBericht()
        {
            List<Categorie> categorieList = categorieRepository.CategorieList();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            MediaBerichtViewModel bvm = new MediaBerichtViewModel() { categorieList = categorieList, account = account };
            return PartialView("CreateMediaBericht", bvm);
        }

        public ActionResult CreateNewBericht()
        {
            List<Categorie> categorieList = categorieRepository.CategorieList();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            NewBerichtViewModel bvm = new NewBerichtViewModel() { categorieList = categorieList, account = account };
            return PartialView("CreateNewBericht", bvm);
        }

        [HttpPost]
        public ActionResult CreateNewCategorie(MediaBerichtViewModel mvm)
        {
            try
            {
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                try
                {
                    categorieRepository.Insert(new Categorie(0, 0, mvm.categorie.Naam));

                    return RedirectToAction("Index", "MediaSharing");
                }

                catch
                {

                    return RedirectToAction("CreateNewMediaBericht", "MediaSharing");
                }

            }
            catch
            {
                return View("Error");
            }
        }



        [HttpPost]
        public ActionResult CreateNewMediaBericht(HttpPostedFileBase file, MediaBerichtViewModel mvm)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    int latestbijdrage = repository.getLatestBijdrageID();
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), latestbijdrage.ToString() + ".jpg");
                    file.SaveAs(path);
                    Account account = (Account)(Session["user"]);
                    accountRepository.GetById(account.Id);
                    repository.InsertMediaBericht(mvm.selectedCategorieId, latestbijdrage.ToString(), account.Id);
                }

                return RedirectToAction("Index", "MediaSharing");
            }
            catch
            {
               return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CreateNewBericht(NewBerichtViewModel mvm)
        {
            try
            {
                    Account account = (Account)(Session["user"]);
                    accountRepository.GetById(account.Id);
                    repository.InsertPost(mvm.bericht.Titel, mvm.bericht.Inhoud, account.Id, mvm.selectedCategorieId);
                    return RedirectToAction("Index", "MediaSharing");
            }
            catch
            {
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult CreateComment(string id, string text)
        {
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            repository.InsertComment(Convert.ToInt32(id), account.Id, text);
            List<Bericht> berichtenList = repository.LoadBerichtenByPostId(Convert.ToInt32(id));
            BerichtenViewModel bvm = new BerichtenViewModel { berichtenList = berichtenList, account = account };
            return PartialView("Comments", bvm);

        }

        [HttpPost]
        public ActionResult GiveALike(int id)
        {       
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
            try
            {
                repository.InsertLike(new AccountBijdrage(account.Id, id, 1, 0));
                return PartialView("Bijdrages", bvm);
            }

            catch
            {
                return RedirectToAction("Index", "MediaSharing");
            }

        }

        [HttpPost]
        public ActionResult Report(int id)
        {
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
            BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
            try
            {
                repository.InsertLike(new AccountBijdrage(account.Id, id, 0, 1));


                return PartialView("Bijdrages", bvm);
            }

            catch
            {
                return RedirectToAction("Index", "MediaSharing");
            }

        }


        public ActionResult DeletePosts(int id)
        {
            
            try
            {
                repository.DeletePost(id);
                List<Bijdrage> bijdrages = repository.GetAllBijdrages();
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
                return PartialView("Bijdrages", bvm);
            }

            catch
            {
                return View("Error");
         
            }

        }

        public ActionResult AdminDeletePosts(int id)
        {

            try
            {
                repository.DeletePost(id);
                List<Bijdrage> bijdrages = repository.GetAllBijdrages();
                Account account = (Account)(Session["user"]);
                accountRepository.GetById(account.Id);
                BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
                return PartialView("Bijdrages", bvm);
            }

            catch
            {
                return View("Error");

            }

        }

    }
}