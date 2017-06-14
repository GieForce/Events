using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication
{
    public class ProductRepository
    {
        private IProductContext context;

        public ProductRepository(IProductContext context)
        {
            this.context = context;
        }

        public List<Product> GetAll()
        {
            return context.GetAll();
        }

        public List<Product> GetByProductCat(ProductCat productCat)
        {
            return context.GetByProductCat(productCat);
        }

        public void Delete(Product product)
        {
            context.Delete(product);
        }

        public void Insert(Product product)
        {
            context.Insert(product);
        }

        public Product getlatestproduct()
        {
            return context.getlatestproduct();
        }

    }
}