using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Web.Mvc;
using EventsApplication.Controllers.Repositorys;
using EventsApplication.App_DAL;
using EventsApplication.Models;
using System.IO;

namespace EventsApplication.Controllers
{
    public class HostingController : Controller
    {
        AccountRepository accountrepo = new AccountRepository(new AccountContext());
        EventRepository eventrepo = new EventRepository(new EventContext());
        LocatieRepository locatierepo = new LocatieRepository(new LocatieContext());
        StandplaatsRepository standplaatsrepo = new StandplaatsRepository(new StandplaatsContext());
        ProductRepository productrepo = new ProductRepository(new ProductContext());
        ProductCatRepository productcatrepo = new ProductCatRepository(new ProductCatContext());
        ProductExemplaarRepository productexemplaarrepo = new ProductExemplaarRepository(new ProductExemplaarContext());
        

        // GET: Hosting
        public ActionResult HostingHome()
        {
            return View();
        }

        public ActionResult EventIndex()
        {
            List<Event> events = eventrepo.GetAllEvents();
            return View(events);
        }

        public ActionResult ProductIndex()
        {
            List<Product> products = productrepo.GetAll();
            List<ProductCat> productcats = new List<ProductCat>();
            foreach (Product product in products)
            {
                ProductCat productcat = productcatrepo.GetByProduct(product);
                productcats.Add(productcat);
            }
            ViewBag.productcategorie = productcats;
            return View(products);
        }

        public ActionResult ProductCreate()
        {
            return View();
        }

        public ActionResult AccountIndex()
        {
            List<Account> accounts = accountrepo.GetAllAccounts();
            return View(accounts);
        }

        public ActionResult LocatieIndex()
        {
            List<Locatie> locaties = locatierepo.GetAll();
            return View(locaties);
        }

        
        public ActionResult EventDetails(int id)
        {
            Event evento = eventrepo.GetById(id);
            Session["activeevent"] = evento;
            return View(evento);
        }


        public ActionResult AccountCreate()
        {
            return View();
        }

        public ActionResult EventCreate()
        {
            return View();
        }

        public ActionResult StandplaatsCreate()
        {
            return View();
        }

        public ActionResult LocatieCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountCreate(FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here
                Account account = new Account(collection["Gebruikersnaam"], collection["Email"], Convert.ToString(accountrepo.newactivationhash()), 0, collection["Wachtwoord"], collection["Telefoonnummer"]);
                accountrepo.Insert(account);
                return RedirectToAction("AccountIndex");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProductCreate(FormCollection collection)
        {
            int x = Convert.ToInt32(collection["Hoeveelheid"]); 

            try
            {
                if( x == 0)
                {
                    ViewBag.Error = "Voer een getal in";
                    return View();
                }

                ProductCat categorie = new ProductCat(collection["Categorie"]);
                productcatrepo.Insert(categorie);
                categorie = productcatrepo.getlastcategorie();
                Product product = new Product(categorie, collection["Merk"], collection["Serie"], Convert.ToInt32(collection["Typenummer"]), Convert.ToDecimal(collection["Prijs"]));
                productrepo.Insert(product);

                for (int i = 0; i < x; i++)
                {
                    Product product2 = productrepo.getlatestproduct();
                    ProductExemplaar productexemplaar = new ProductExemplaar(product2.Id, i, Convert.ToString(i) + Convert.ToString(product2.Typenummer));
                    productexemplaarrepo.insert(productexemplaar);
                }

                return RedirectToAction("ProductIndex");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LocatieCreate(FormCollection collection, HttpPostedFileBase postedFile)
        {
            try
            {

                // TODO: Add insert logic here
                Locatie locatie = new Locatie(collection["Naam"], collection["Straat"], Convert.ToInt32(collection["Nr"]), collection["Postcode"], collection["Plaats"]);
                locatierepo.Insert(locatie);

                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();

                    string[] validFileTypes = { ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        if (System.IO.File.Exists(path1))
                        {
                            System.IO.File.Delete(path1);
                        }
                        Request.Files["FileUpload1"].SaveAs(path1);
                    if (extension == ".csv")
                    {
                        DataTable dt = Utility.ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                        int a = 0;
                        decimal prijs = 0;
                        int capaciteit = 0;
                        int nummer = 0;
                        string specificatie = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {

                                string volledig = Convert.ToString(dr[column]);
                                using (StringReader sr = new StringReader(volledig))
                                {
                                    string[] dingen = sr.ReadLine().Split(';');
                                    foreach (string header in dingen)
                                    {
                                        if (a == 0)
                                        {
                                            prijs = Convert.ToDecimal(header);
                                        }
                                        if(a == 1)
                                        {
                                            capaciteit = Convert.ToInt32(header);
                                        }
                                        if(a == 2)
                                        {
                                            nummer = Convert.ToInt32(header);
                                        }
                                        if(a == 3)
                                        {
                                            specificatie = header;
                                        }

                                        a = a + 1;
                                    }

                                    

                                }
                                int locatieid = locatierepo.locatieidophalen(locatie.Naam, locatie.Straat, locatie.Nr, locatie.Postcode, locatie.Plaats);
                                Locatie locatie2 = new Locatie(locatie.Naam, locatie.Straat, locatie.Nr, locatieid, locatie.Postcode, locatie.Plaats);
                                standplaatsrepo.Insert(locatie2, prijs, capaciteit, nummer, specificatie);
                                a = 0;
                            }
                           

                        }
                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .csv format";

                    }

                    }


                    return RedirectToAction("LocatieIndex");
                }
                ViewBag.Error = "Please Upload Files in .csv format";
                return View();
            }
            catch
            {
               return View();
            }
        }

        //  [HttpPost]
        //  public ActionResult EventCreate(FormCollection collection)
        // {
        //    try
        //    {

        // TODO: Add insert logic here
        //        Event evento = new Event(collection["Gebruikersnaam"], collection["Email"], Convert.ToString(accountrepo.newactivationhash()), false);
        //        accountrepo.Insert(evento);
        //        return RedirectToAction("AccountIndex");
        //     }
        //     catch
        //     {
        //         return View();
        //      }
        //  }

        // GET: Account/Edit/5
        public ActionResult AccountEdit(int id)
        {
            Account account = accountrepo.GetById(id);
            return View(account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult AccountEdit(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Account accounto = new Account(Convert.ToInt32(collection["Id"]),collection["GebruikersNaam"], collection["Email"], collection["Activatiehash"], true, collection["Wachtwoord"], collection["Telefoonnummer"]);
                accountrepo.Update(accounto);
                return RedirectToAction("AccountIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}