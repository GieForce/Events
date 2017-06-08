using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.Controllers.Repositorys
{
    public class CategorieRepository
    {
        private readonly ICategorieContext context;

        public CategorieRepository(ICategorieContext context)
        {
            this.context = context;
        }

        public List<Categorie> CategorieList()
        {
            return context.CategorieList();
        }

        public List<Categorie> GetByBijdrageId(int id)
        {
            return context.GetByBijdrageID(id);
        }

        public bool Insert(Categorie bericht)
        {
            return context.Insert(bericht);
        }
    }
}
