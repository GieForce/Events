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

        void Insert(ProductExemplaar productExemplaar);

        bool Update(ProductExemplaar productExemplaar);

        bool Delete(int id);

        List<ProductExemplaar> GetByProduct(int productId);
    }
}
