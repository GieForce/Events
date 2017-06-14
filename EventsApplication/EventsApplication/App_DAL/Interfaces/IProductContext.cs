using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication
{
    public interface IProductContext
    {
        List<Product> GetAll();

        List<Product> GetByProductCat(ProductCat productCat);

        Product GetByID(int ID);

        void Delete(Product product);

        void Insert(Product product);

        Product getlatestproduct();
    }
}
