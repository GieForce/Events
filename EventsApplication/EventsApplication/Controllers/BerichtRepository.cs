using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL;
using EventsApplication.Models;

namespace EventsApplication.Controllers
{
    public class BerichtRepository
    {
        private readonly IBerichtContext context;

        public BerichtRepository(IBerichtContext context)
        {
            this.context = context;
        }

        public List<Bericht> GetAllBerichten()
        {
            return context.GetAllBerichten();
        }

        public List<Bericht> GetByTitel(Bericht bericht)
        {
            return context.GetByTitel(bericht);
        }

        public bool Insert(Bericht bericht)
        {
            return context.Insert(bericht);
        }

        public bool Update(Bericht bericht)
        {
            return context.Update(bericht);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }


    }
}
