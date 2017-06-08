using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.Controllers
{
    public class MediaSharingController : Controller
    {
        BijdrageRepository repository = new BijdrageRepository(new BijdrageContext());
        AccountRepository accountRepository = new AccountRepository(new AccountContext());
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

        public ActionResult CreatePost(PostViewModel postmodel)
        {
            Account account = (Account)(Session["user"]);
            AccountBijdrage ab = new AccountBijdrage();
            Bericht bericht = new Bericht(account, DateTime.Now,"bericht", ab ,postmodel.bericht.Titel, postmodel.bericht.Inhoud );
            Categorie categorie = new Categorie( account, DateTime.Now,"categorie" ,ab, postmodel.categorie.CategorieId, postmodel.categorie.Naam);
            accountRepository.GetById(account.Id);
            PostViewModel pvm = new PostViewModel() {account = account, bericht = bericht, categorie = categorie};
           
            return PartialView("Create");
        }



    }
}