using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication
{
    public interface IProductCatContext
    {
        ProductCat GetById(int id);

        List<ProductCat> GetAll();

        ProductCat GetByProduct(Product product);

        List<ProductCat> GetByProductCat(ProductCat productCat);

        void Insert(ProductCat productCat);

        void Insert(ProductCat productCat, ProductCat parentCat);

        void Delete(ProductCat productCat);
    }
}
