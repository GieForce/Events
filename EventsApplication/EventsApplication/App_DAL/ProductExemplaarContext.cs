using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class ProductExemplaarContext : IProductExemplaarContext
    {
        public List<ProductExemplaar> GetProductExemplaars()
        {
            List<ProductExemplaar> result = new List<ProductExemplaar>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM PRODUCTEXEMPLAAR";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateReserveringFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                      
                    }
                }
            }
            return result;
        }

        public List<ProductExemplaar> GetProductsByReservation(int reserveringsID)
        {
            List<ProductExemplaar> result = new List<ProductExemplaar>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT PRODUCTEXEMPLAAR.ID, PRODUCTEXEMPLAAR.product_id, PRODUCTEXEMPLAAR.volgnummer, PRODUCTEXEMPLAAR.barcode FROM PRODUCTEXEMPLAAR INNER JOIN VERHUUR ON (PRODUCTEXEMPLAAR.ID=VERHUUR.productexemplaar_id) INNER JOIN RESERVERING_POLSBANDJE ON (VERHUUR.res_pb_id=RESERVERING_POLSBANDJE.ID) WHERE RESERVERING_POLSBANDJE.reservering_id = @reserveringsID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reserveringsID", reserveringsID);
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateReserveringFromReader(reader));
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return result;
        }

        public ProductExemplaar GetByProduct(int productId)
        {        
            List<ProductExemplaar> rlist = new List<ProductExemplaar>();
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM PRODUCTEXEMPLAAR WHERE product_id = @param1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", productId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rlist.Add(CreateReserveringFromReader(reader));
                            }
                        }
                    }
                }

                return rlist;         
        }

        public void Insert(ProductExemplaar productExemplaar)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO PRODUCTEXEMPLAAR (product_id, volgnummer, barcode) VALUES (@productID, @volgnummer, @barcode)";




                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productID", productExemplaar.Product_id);
                        command.Parameters.AddWithValue("@volgnummer", productExemplaar.Volgnummer);
                        command.Parameters.AddWithValue("@barcode", productExemplaar.Barcode);

                        command.ExecuteNonQuery();
                                           
                    }

                }
            }

            catch
            {
                
            }
           
          
        }

        public bool Update(ProductExemplaar productExemplaar)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE PRODUCTEXEMPLAAR SET product_id= @productID ,volgnummer = @volgnummer, barcode = @barcode WHERE id = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", productExemplaar.Id);
                    command.Parameters.AddWithValue("@volgnummer", productExemplaar.Volgnummer);
                    command.Parameters.AddWithValue("@barcode", productExemplaar.Barcode);
         
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch 
                    {
                    
                    }
                }
            }

            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "DELETE * FROM PRODUCTEXEMPLAAR WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



        private ProductExemplaar CreateReserveringFromReader(SqlDataReader reader)
        {
            return new ProductExemplaar(
                Convert.ToInt32(reader["id"]),
                Convert.ToInt32(reader["product_id"]),
                Convert.ToInt32(reader["volgnummer"]),
                Convert.ToString(reader["barcode"]));

        }
    }
}
