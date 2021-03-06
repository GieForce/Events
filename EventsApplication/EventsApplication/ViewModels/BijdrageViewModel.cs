﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsApplication.Models;

namespace EventsApplication.ViewModels
{
    public class BijdrageViewModel
    {
        public List<Bijdrage> bijdrageList { get; set; }
        public Account account { get; set; }
    }

    public class PostViewModel
    {
        public Account account { get; set; }

     //   public Bijdrage bijdrage { get; set; }
        public Bericht bericht { get; set; }
        public  Categorie categorie { get; set; }
        public Bestand bestand { get; set; }
    }


    public class BerichtenViewModel
    {
        public List<Bericht> berichtenList { get; set; }
        public Account account { get; set; }
    }

    public class BerichtViewModel
    {
        public string titel { get; set; }
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Het bericht dient minimaal 4 tekens te bevatten")]
        [Required(ErrorMessage = "Het bericht mag niet leeg zijn")]
        public string inhoud { get; set; }
    }

    public class MediaBerichtViewModel
    {
        public Account account { get; set; }
        public  Categorie categorie { get; set; }
        public List<Categorie> categorieList { get; set; }
        
        public string bestandslocatie { get; set; }
        public int selectedCategorieId { get; set; }
    }

    public class NewBerichtViewModel
    {
        public Account account { get; set; }
        public Categorie categorie { get; set; }
        public List<Categorie> categorieList { get; set; }

        public Bericht bericht { get; set; }
        public int selectedCategorieId { get; set; }
    }
}