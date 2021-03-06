﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using EventsApplication.App_DAL;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.Controllers
{
    public class BijdrageRepository
    {
        private readonly IBijdrageContext context;

        public BijdrageRepository(IBijdrageContext context)
        {
            this.context = context;
        }

        public Bijdrage GetById(int id)
        {
            return context.GetById(id);
        }

        public bool Insert(Bericht bericht)
        {
            return context.Insert(bericht);
        }

        //public bool Delete(int id)
        //{
        //    return context.Delete(id);
        //}

        public List<Bijdrage> GetAllBijdrages()
        {
            return context.GetAllBijdrages();
        }

        public List<Bijdrage> GetAllBijdragesByUserId(int userid)
        {
            return context.GetAllBijdragesByUserId(userid);
        }
        //public bool Update(Bijdrage bijdrage)
        //{
        //    return context.Update(bijdrage);
        //}

        public List<Bericht> LoadBerichtenByPostId(int id)
        {
            return context.LoadBerichtenByPostId(id);
        }
        public List<Bijdrage> GetallreportedBijdrages()
        {
            return context.GetAllReportedBijdrages();
        }


        public bool InsertMediaBericht(int categorieId, string bestandlocatie, int accountid)
        {
            return context.InsertMediaBericht(categorieId, bestandlocatie, accountid);
        }

        public bool InsertComment(int id, int accountid, string text)
        {
            return context.InsertComment(id, accountid, text);
        }

        public int getLatestBijdrageID()
        {
            return context.getLatestBijdrageID();
        }

        public bool InsertPost(string titel, string inhoud, int accountid, int categorieId)
        {
            return context.InsertPost(titel, inhoud, accountid, categorieId);
        }

        public bool InsertLike(AccountBijdrage accountBijdrage)
        {
            return context.InsertLike(accountBijdrage);
        }

        public bool DeletePost(int id)
        {
            return context.DeletePost(id);
        }
    }
}