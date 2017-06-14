using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        //public ActionResult CreatePost(PostViewModel postmodel)
        //{
        //    Account account = (Account)(Session["user"]);
        //    AccountBijdrage ab = new AccountBijdrage();
        //    Bericht bericht = new Bericht(account, postmodel.bericht.Datum,"bericht", ab ,postmodel.bericht.Titel, postmodel.bericht.Inhoud );
        //    Bestand bestand = new Bestand(account, postmodel.bestand.Datum, "bestand",ab, postmodel.categorie.Id, postmodel.bestand.Bestandlocatie, postmodel.bestand.Grootte);

        //    Categorie categorie = new Categorie( account, DateTime.Now,"categorie" ,ab, postmodel.categorie.CategorieId, postmodel.categorie.Naam);
        //    accountRepository.GetById(account.Id);
        //    PostViewModel pvm = new PostViewModel() {account = account, bericht = bericht, categorie = categorie, bestand = bestand};

        //    repository.insertPVM(pvm);
        //    return PartialView("Create", pvm);
        //}

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

        [HttpPost]
        public ActionResult CreateNewMediaBericht(HttpPostedFileBase file, MediaBerichtViewModel mvm)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), mvm.selectedCategorieId + ".jpg");
                    file.SaveAs(path);
                    Account account = (Account)(Session["user"]);
                    accountRepository.GetById(account.Id);
                    repository.InsertMediaBericht(mvm.selectedCategorieId, mvm.bestandslocatie, account.Id);
                }

                return RedirectToAction("Index", "MediaSharing");
            }
            catch
            {
               return View("Error");
            }
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

           
                return PartialView("Berichten", bvm);
            }

            catch
            {
                return RedirectToAction("Index", "MediaSharing");
            }
         
        }


        public ActionResult DeletePosts(int id)
        {
            List<Bijdrage> bijdrages = repository.GetAllBijdrages();
            Account account = (Account)(Session["user"]);
            accountRepository.GetById(account.Id);
       //     BijdrageViewModel bvm = new BijdrageViewModel { bijdrageList = bijdrages, account = account };
            try
            {

                repository.DeletePost(id);

                return RedirectToAction("ShowPosts", "MediaSharing");
            }

            catch
            {
                return RedirectToAction("ShowPosts", "MediaSharing");
         
            }

        }
       
    }
}