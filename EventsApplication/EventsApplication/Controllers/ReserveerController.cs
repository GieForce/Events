using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EventsApplication.App_DAL;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.Models;
using EventsApplication.ViewModels;
using ProductViewModel = EventsApplication.ViewModels.ProductViewModel;

namespace EventsApplication.Controllers
{
    public class ReserveerController : Controller
    {
        // GET: Reserveer
        public ActionResult Index()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["reservering"] == null)
            {
                Session["reservering"] = new ReserveringViewModel()
                {
                    Evenement = (Event)Session["event"]
                };
            }
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            return View(reservering);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            reservering.DatumStart = DateTime.ParseExact(collection["start"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            reservering.DatumEind = DateTime.ParseExact(collection["end"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return RedirectToAction("Plaats", "Reserveer");
        }


        // GET: Reserveer/Plaats
        public ActionResult Plaats()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            StandplaatsRepository repo = new StandplaatsRepository(new StandplaatsContext());
            ViewData["plaatsen"] = repo.GetFreeStaanplaatsenByLocatie(reservering.Evenement.Locatie,
                reservering.DatumStart, reservering.DatumEind);
            return View(reservering);
        }

        [HttpPost]
        public ActionResult Plaats(FormCollection collection)
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            StandplaatsRepository repo = new StandplaatsRepository(new StandplaatsContext());
            string[] plaatsids = collection["Plaatsen"].Split(',');
            List<Standplaats> plaatsen = new List<Standplaats>();
            foreach (var s in plaatsids)
            {
                plaatsen.Add(repo.GetById(Convert.ToInt32(s)));
            }
            reservering.Staanplaatsen = plaatsen;
            return RedirectToAction("Gegevens", "Reserveer");
        }

        // GET: Reserveer/Gegevens
        public ActionResult Gegevens()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            ViewData["MaxPersonen"] = reservering.Staanplaatsen.Sum(x => x.Capaciteit);
            return View(reservering);
        }

        [HttpPost]
        public ActionResult Gegevens(ReserveringViewModel model, FormCollection collection)
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            reservering.Voornaam = model.Voornaam;
            if (String.IsNullOrEmpty(model.Tussenvoegsel))
            {
                reservering.Tussenvoegsel = " ";
            }
            else
            {
                reservering.Tussenvoegsel = model.Tussenvoegsel;
            }
            reservering.Achternaam = model.Achternaam;
            reservering.Straat = model.Straat;
            reservering.Huisnummer = model.Huisnummer;
            reservering.Woonplaats = model.Woonplaats;
            reservering.Accounts = new List<AccountViewModel>();
            for (int i = 0; i < Convert.ToInt32(collection["personen"]); i++)
            {
                reservering.Accounts.Add(new AccountViewModel());
            }
            return RedirectToAction("Accounts", "Reserveer");
        }

        public ActionResult Accounts()
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            List<AccountViewModel> accounts = reservering.Accounts;
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(accounts);
        }

        [HttpPost]
        public ActionResult Accounts(List<AccountViewModel> model)
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            reservering.Accounts.Clear();
            reservering.Accounts = model;
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Materiaal");
        }

        public ActionResult Materiaal()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ProductRepository prepo = new ProductRepository(new ProductContext());
            ProductCatRepository crepo = new ProductCatRepository(new ProductCatContext());

            List<Product> allProducts = prepo.GetAll();
            List<ProductCatViewModel> productcats = new List<ProductCatViewModel>();

            foreach (var productCat in crepo.GetAll())
            {
                IEnumerable<Product> products = allProducts.Where(x => x.Categorie.Id == productCat.Id);
                productcats.Add(ModelToViewModel.ConvertProductCatToViewModel(productCat, products));
            }

            ProductVerhuurViewModel model = new ProductVerhuurViewModel()
            {
                AllProducts = ModelToViewModel.ConvertProductToViewModelList(allProducts),
                ProductCats = productcats,
                SelectedProducts = new List<ProductViewModel>()
            };

            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            if (reservering.Producten == null)
            {
                reservering.Producten = new List<Product>();
            }
            ViewData["producten"] = reservering.Producten;
            ViewData["totaalprijs"] = reservering.Producten.Sum(x => x.Prijs);

            return View(model);
        }

        public ActionResult Add(int productid)
        {
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            if (reservering.Producten == null)
            {
                reservering.Producten = new List<Product>();
            }
            if (reservering.Exemplaren == null)
            {
                reservering.Exemplaren = new List<ProductExemplaar>();
            }
            if (productid != 0 || productid != null)
            {
                ProductRepository prepo = new ProductRepository(new ProductContext());
                ProductExemplaarRepository erepo = new ProductExemplaarRepository(new ProductExemplaarContext());
                Product p = prepo.GetAll().First(x => x.Id == productid);
                List<ProductExemplaar> exemplaren = erepo.GetByProduct(p.Id);
                if (exemplaren.Count != 0)
                {
                    reservering.Exemplaren.Add(exemplaren[0]);
                    reservering.Producten.Add(p);
                }
            }
            return RedirectToAction("Materiaal", "Reserveer");
        }

        public ActionResult Finish()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            reservering.Dagen = Convert.ToInt32((reservering.DatumEind - reservering.DatumStart).TotalDays);
            reservering.TotaalPrijs = Convert.ToDouble(reservering.Producten.Sum(x => x.Prijs) +
                                      reservering.Staanplaatsen.Sum(x => x.Prijs * reservering.Dagen));
            return View(reservering);
        }
        
        [AjaxOnly]
        public ActionResult Submit()
        {
            if (Session["event"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ReserveringViewModel reservering = (ReserveringViewModel)Session["reservering"];
            ReserveringRepository repo = new ReserveringRepository(new ReserveringContext());
            AccountRepository arepo = new AccountRepository(new AccountContext());
            List<int> plekids = reservering.Staanplaatsen.Select(x => x.Id).ToList();
            repo.PlaatsReservering(reservering.Voornaam, reservering.Tussenvoegsel, reservering.Achternaam,
                reservering.Straat, reservering.Huisnummer, reservering.Woonplaats, reservering.DatumStart,
                reservering.DatumEind, reservering.Evenement.ID1, plekids, reservering.Accounts,
                reservering.Exemplaren);

            // Email
            foreach (AccountViewModel account in reservering.Accounts)
            {
                string message = "Beste " + account.Gebruikersnaam + ",<br><br>" +
                    "Bedankt voor het reserveren bij " + reservering.Evenement.Naam + "<br>" +
                    "Om uw account te activeren gaat u naar deze link: <a href=\"192.168.20.22/reserveer/activeer?activatiehash=" + arepo.GetAllAccounts().First(x => x.Gebruikersnaam == account.Gebruikersnaam).Activatiehash + "\"> link</a><br><br>" +
                    "Groeten,<br><br>" + "Het EyeCT4Events Team";
                SendMail(account.Email, message);
            }
            
            return Redirect("/reserveer/success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Activeer(string activatiehash)
        {
            AccountRepository repo = new AccountRepository(new AccountContext());
            repo.Activeer(activatiehash);
            return View();
        }

        private void SendMail(string emailaddres, string tekst)
        {
            new SmtpClient
                {
                    Host = "Smtp.Gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("eyect4eventsc@gmail.com", "dbi364892")
                }
                .Send(new MailMessage
                {
                    From = new MailAddress("eyect4eventsc@gmail.com", ""),
                    To = { emailaddres },
                    CC = { "eyect4eventsc@gmail.com" },
                    Subject = "Activatielink EYECT4EVENTS",
                    Body = tekst,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                });
        }
    }

    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}