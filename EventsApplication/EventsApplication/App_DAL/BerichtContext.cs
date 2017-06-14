using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    class BerichtContext : IBerichtContext
    {

        public List<Bericht> GetByTitel(Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public List<Bericht> GetAllBerichten()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bericht bericht)
        {
            throw new NotImplementedException();
        }
    }
}
