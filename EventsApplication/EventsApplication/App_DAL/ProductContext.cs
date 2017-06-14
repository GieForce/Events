using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class ProductContext : IProductContext
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            try
            {

                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM PRODUCT";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(ProductFromReader(reader));

                            }

                        }
                    }
                }
                return products;
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetByProductCat(ProductCat productCat)
        {
            List<Product> products = new List<Product>();
            try
            {

                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM PRODUCT WHERE productcat_id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", productCat.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(ProductFromReader(reader));

                            }

                        }
                    }
                }
                return products;
            }
            catch
            {
                return null;
            }
        }

        public void Delete(Product product)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "DELETE FROM PRODUCT WHERE ID = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", product.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        public void Insert(Product product)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO PRODUCT VALUES (@productcat, @merk, @serie, @typenummer, @prijs)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productcat", product.Categorie.Id);
                        command.Parameters.AddWithValue("@merk", product.Merk);
                        command.Parameters.AddWithValue("@serie", product.Serie);
                        command.Parameters.AddWithValue("@typenummer", product.Typenummer);
                        command.Parameters.AddWithValue("@prijs", product.Prijs);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public Product getlatestproduct()
        {
            Product product = null;
            try
            {

                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM PRODUCT WHERE ID IN (SELECT MAX(ID) FROM PRODUCT)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                product = ProductFromReader(reader);

                            }

                        }
                    }
                }
                return product;
            }
            catch
            {
                return null;
            }
        }

        public Product GetProductByID(int id)
        {
            Product products = null;
            try
            {

                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM PRODUCT WHERE ID = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 products = ProductFromReader(reader);

                            }

                        }
                    }
                }
                return products;
            }
            catch
            {
                return null;
            }
        }

        private Product ProductFromReader(SqlDataReader reader)
        {
            return new Product(
                Convert.ToInt32(reader["id"]),
                Convert.ToInt32(reader["productcat_id"]),
                Convert.ToString(reader["merk"]),
                Convert.ToString(reader["serie"]),
                Convert.ToInt32(reader["typenummer"]),
                Convert.ToDecimal(reader["prijs"])
                );
        }
    }
}