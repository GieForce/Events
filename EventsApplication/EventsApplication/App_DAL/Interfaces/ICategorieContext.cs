using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface ICategorieContext
    {
        List<Categorie> GetByBijdrageID(int id);
        List<Categorie> CategorieList();
        bool Insert(Categorie categorie);
    }
}
