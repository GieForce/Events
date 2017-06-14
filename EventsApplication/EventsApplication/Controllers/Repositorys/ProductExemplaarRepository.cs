using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsApplication.Models;
using EventsApplication.App_DAL.Interfaces;

namespace EventsApplication.Controllers.Repositorys
{
    public class ProductExemplaarRepository
    {
        private IProductExemplaarContext context;

        public ProductExemplaarRepository(IProductExemplaarContext context)
        {
            this.context = context;
        }

        public List<ProductExemplaar> getproductexemplaars()
        {
            return context.GetProductExemplaars();
        }

        public List<ProductExemplaar> GetProductsByReservation(int reserveringsID)
        {
            return context.GetProductsByReservation(reserveringsID);
        }

        public void insert(ProductExemplaar productexemplaar)
        {
            context.Insert(productexemplaar);
        }

        public bool update(ProductExemplaar productexemplaar)
        {
            return context.Update(productexemplaar);
        }

        public bool delete(int id)
        {
            return context.Delete(id);
        }

        public List<ProductExemplaar> GetByProduct(int productid)
        {
            return context.GetByProduct(productid);
        }

    }
}