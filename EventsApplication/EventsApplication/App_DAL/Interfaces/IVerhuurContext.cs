using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IVerhuurContext
    {
        List<Verhuur> GetProductExemplaars();

        void Insert(Verhuur verhuur);

        bool Update(Verhuur verhuur);

        bool Delete(int id);

        Verhuur GetByProductExemp(int productExempId);
    }
}
