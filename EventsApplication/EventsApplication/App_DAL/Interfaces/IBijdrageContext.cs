using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.App_DAL
{
    public interface IBijdrageContext
    {
        Bijdrage GetById(int id);
        bool Insert(Bericht bijdrage);

        bool InsertMediaBericht(int categorieId, string bestandlocatie, int accountid);
        bool InsertLike(AccountBijdrage accountBijdrage);

        bool DeletePost(int id);
        //bool Delete(int id);
        List<Bijdrage> GetAllBijdrages();

        List<Bijdrage> GetAllBijdragesByUserId(int userid);
        List<Bericht> LoadBerichtenByPostId(int id);
    
    }
}
