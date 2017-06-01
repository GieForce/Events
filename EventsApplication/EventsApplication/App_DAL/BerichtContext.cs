using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    class BerichtContext : IBerichtContext
    {
        //        public List<Bericht> GetAllBerichten()
        //        {
        //            List<Bericht> berichtlist = new List<Bericht>();

        //            using (SqlConnection connection = Connection.SQLconnection)
        //            {
        //                string query = "SELECT * FROM bericht";
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    using (SqlDataReader reader = command.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            berichtlist.Add(ReaderToBericht(reader));
        //                        }
        //                    }
        //                }
        //            }
        //            return berichtlist;
        //        }

        //        /// <summary>
        //        /// Get an bericht by titel
        //        /// </summary>
        //        /// <param name="id"></param>
        //        /// <returns></returns>
        //        public List<Bericht> GetByTitel(Bericht bericht)
        //        {
        //            List<Bericht> berichtlist = new List<Bericht>();

        //            using (SqlConnection connection = Connection.SQLconnection)
        //            {
        //                string query = "SELECT * FROM bericht WHERE titel = @param1";
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    command.Parameters.AddWithValue("@param1", bericht.Titel);

        //                    using (SqlDataReader reader = command.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            berichtlist.Add(ReaderToBericht(reader));
        //                        }
        //                    }
        //                }
        //            }
        //            return berichtlist;
        //        }

        //        public bool Insert(Bericht bericht)
        //        {
        //            try
        //            {
        //                using (SqlConnection connection = Connection.SQLconnection)
        //                {
        //                    string query =
        //                        "INSERT INTO Account (gebruikersnaam, email, activatiehash, geactiveerd) VALUES (@gebruikersnaam, @email, @activatiehash, @geactiveerd)";
        //                    using (SqlCommand command = new SqlCommand(query, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@param1", bericht.Titel);
        //                        command.Parameters.AddWithValue("@param2", bericht.Inhoud);
        //                        command.ExecuteNonQuery();
        //                    }
        //                }
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }
        //        }

        //        /// <summary>
        //        /// Delete an account from the database
        //        /// </summary>
        //        /// <param name="account"></param>
        //        /// <returns></returns>
        //        public bool Delete(int id)
        //        {
        //            try
        //            {
        //                using (SqlConnection connection = Connection.SQLconnection)
        //                {
        //                    string query =
        //                        "DELETE FROM bericht WHERE ID = @id";
        //                    using (SqlCommand command = new SqlCommand(query, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@id", id);
        //                        command.ExecuteNonQuery();
        //                    }
        //                }
        //                return true;
        //            }
        //            catch
        //            {
        //                return false;
        //            }
        //        }

        //        public bool Update(Bericht bericht)
        //        {
        //            try
        //            {
        //                using (SqlConnection connection = Connection.SQLconnection)
        //                {
        //                    string query = "UPDATE bericht SET titel =@param1, inhoud=@param2 WHERE bijdrage_id = @param3";


        //                    using (SqlCommand command = new SqlCommand(query, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@param1", bericht.Titel);
        //                        command.Parameters.AddWithValue("@param2", bericht.Inhoud);
        //                        command.ExecuteNonQuery();
        //                        return true;
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }
        //        }



        //private Bericht ReaderToBericht(SqlDataReader reader)
        //{
        //    return new Bericht(
        //        Convert.ToInt32(reader["ID"]),
        //        Convert.ToString(reader["titel"]),
        //        Convert.ToString(reader["inhoud"])
        //    );
        //}
        public List<Bericht> GetByTitel(Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public List<Bericht> GetAllBerichten()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bericht bericht)
        {
            throw new NotImplementedException();
        }
    }
}
