using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class VerhuurContext
    {
        public List<Verhuur> GetProductVerhuur()
        {
            List<Verhuur> result = new List<Verhuur>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM VERHUUR";


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

        public Verhuur GetByProductExemp(int productexempId)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * VERHUUR WHERE product_id = @param1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("param1", productexempId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreateReserveringFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void Insert(Verhuur verhuur)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO Reservering (productexemplaar_id, res_pb_id, datumIn, datumUit, prijs, betaald) VALUES (@productExempID, @respbid, @datumIn,@datumUit,@prijs,@betaald)";




                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productExempID", verhuur.ProdcutExemplaar_id);
                        command.Parameters.AddWithValue("@respbid", verhuur.Res_pb_id);
                        command.Parameters.AddWithValue("@datumIn", verhuur.DatumIn);
                        command.Parameters.AddWithValue("@datumUit", verhuur.DatumUit);
                        command.Parameters.AddWithValue("@prijs", verhuur.Prijs);
                        command.Parameters.AddWithValue("@betaald", verhuur.Betaald);

                        command.ExecuteNonQuery();

                    }

                }
            }

            catch
            {

            }


        }

        public bool Update(Verhuur verhuur)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE Verhuur SET productexemplaar_id = @productExempID, res_pb_id = @respbid, datumIn = @datumIn, datumUit = @datumUit, prijs = @prijs, betaald = @betaald WHERE id = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", verhuur.Id);
                    command.Parameters.AddWithValue("@productExempID", verhuur.ProdcutExemplaar_id);
                    command.Parameters.AddWithValue("@respbid", verhuur.Res_pb_id);
                    command.Parameters.AddWithValue("@datumIn", verhuur.DatumIn);
                    command.Parameters.AddWithValue("@datumUit", verhuur.DatumUit);
                    command.Parameters.AddWithValue("@prijs", verhuur.Prijs);
                    command.Parameters.AddWithValue("@betaald", verhuur.Betaald);


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
                        "DELETE * FROM VERHUUR WHERE id = @id";
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



        private Verhuur CreateReserveringFromReader(SqlDataReader reader)
        {
            return new Verhuur(
                Convert.ToInt32(reader["id"]),
                Convert.ToInt32(reader["productexemplaar_id"]),
                Convert.ToInt32(reader["res_pb_id"]),
                Convert.ToDateTime(reader["datumIn"]),
                Convert.ToDateTime(reader["datumUit"]),
                Convert.ToDecimal(reader["prijs"]),
                Convert.ToBoolean(reader["betaald"])
                );

        }
    }
}
