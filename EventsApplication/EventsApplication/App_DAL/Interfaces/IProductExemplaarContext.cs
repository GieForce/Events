using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL.Interfaces
{
    public interface IProductExemplaarContext
    {
        List<ProductExemplaar> GetProductExemplaars();

        List<ProductExemplaar> GetProductsByReservation(int reserveringsID);

        void Insert(ProductExemplaar productExemplaar);

        bool Update(ProductExemplaar productExemplaar);

        bool Delete(int id);

        ProductExemplaar GetByProduct(int productId);
    }
}
