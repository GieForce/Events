using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication
{
    public class ProductCatRepository
    {
        private IProductCatContext context;

        public ProductCatRepository(IProductCatContext context)
        {
            this.context = context;
        }

        public ProductCat GetById(int id)
        {
            return context.GetById(id);
        }

        public List<ProductCat> GetAll()
        {
            return context.GetAll();
        }

        public ProductCat GetByProduct(Product product)
        {
            return context.GetByProduct(product);
        }

        public List<ProductCat> GetByProductCat(ProductCat productCat)
        {
            return context.GetByProductCat(productCat);
        }

        public void Insert(ProductCat productCat)
        {
            context.Insert(productCat);
        }

        public void Insert(ProductCat productCat, ProductCat parentCat)
        {
            context.Insert(productCat, parentCat);
        }

        public void Delete(ProductCat productCat)
        {
            context.Delete(productCat);
        }

        public ProductCat getlastcategorie()
        {
            return context.getlatestcategorie();
        }
    }
}