using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public interface IBijdrageContext
    {
        Bijdrage GetById(int id);
        //bool Insert(Bijdrage bijdrage);

        //bool Delete(int id);
        List<Bijdrage> GetAllBijdrages();

        //bool Update(Bijdrage bijdrage);
    }
}
