using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL;

namespace EventsApplication.Models
{
    public class ProductExemplaar
    {
        ProductRepository productRepository = new ProductRepository(new ProductContext());

        private int id;
        private int product_id;
        private int volgnummer;
        private string barcode;

        private Product product;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Product_id
        {
            get
            {
                return product_id;
            }

            set
            {
                product_id = value;
            }
        }

        public int Volgnummer
        {
            get
            {
                return volgnummer;
            }

            set
            {
                volgnummer = value;
            }
        }

        public string Barcode
        {
            get
            {
                return barcode;
            }

            set
            {
                barcode = value;
            }
        }

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }


        public ProductExemplaar(int id, int product_id, int volgnummer, string barcode)
        {
            this.Id = id;
            this.Product_id = product_id;
            this.Volgnummer = volgnummer;
            this.Barcode = barcode;

            product = productRepository.GetByID(id);
        }

        public ProductExemplaar(int product_id, int volgnummer, string barcode)
        {
            this.Product_id = product_id;
            this.Volgnummer = volgnummer;
            this.Barcode = barcode;
        }
    }
}
