using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public interface IBerichtContext
    {
        List<Bericht> GetByTitel(Bericht bericht);
        List<Bericht> GetAllBerichten();
        bool Insert(Bericht bericht);
        bool Delete(int id);
        bool Update(Bericht bericht);


    }
}
