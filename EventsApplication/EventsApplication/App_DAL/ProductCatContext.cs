using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class ProductCatContext : IProductCatContext
    {
        public ProductCat GetById(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM PRODUCTCAT WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return ProductCatFromReader(reader);
                            }
                            return null;
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
        }

        public List<ProductCat> GetAll()
        {
            List<ProductCat> productCats = new List<ProductCat>();
            try
            {

                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM PRODUCTCAT";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productCats.Add(ProductCatFromReader(reader));
                            }

                        }
                    }
                }
                return productCats;
            }
            catch
            {
                return null;
            }
        }

        public ProductCat GetByProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM PRODUCTCAT WHERE id = (SELECT productcat_id FROM product WHERE id = @id)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", product.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return ProductCatFromReader(reader);
                            }
                            return null;
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
        }

        public List<ProductCat> GetByProductCat(ProductCat productCat)
        {
            List<ProductCat> productCats = new List<ProductCat>();
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM productcat WHERE productcat_id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", productCat.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productCats.Add(ProductCatFromReader(reader));
                            }

                        }
                    }
                }
                return productCats;
            }
            catch
            {
                return null;
            }
        }

        public void Insert(ProductCat productCat)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "INSERT INTO PRODUCTCAT VALUES (0,@naam)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@naam", productCat.Naam);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void Insert(ProductCat productCat, ProductCat parentCat)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "INSERT INTO PRODUCTCAT VALUES (@pid ,@naam)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pid", parentCat.Id);
                        command.Parameters.AddWithValue("@naam", productCat.Naam);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void Delete(ProductCat productCat)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "DELETE FROM productcat WHERE ID = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", productCat.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        private ProductCat ProductCatFromReader(SqlDataReader reader)
        {
            return new ProductCat(
                Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["naam"]));
        }
    }
}