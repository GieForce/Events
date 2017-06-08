using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class CategorieContext
    {
        public List<Categorie> GetByBijdrageID(int id)
        {
            List<Categorie> categorielist = new List<Categorie>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM categorie WHERE bijdrage_id = @param1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@param1",id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorielist.Add(ReaderToCategorie(reader));
                        }
                    }
                }
            }
            return categorielist;
        }

        public List<Categorie> CategorieList()
        {
            List<Categorie> categorielist = new List<Categorie>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM categorie";
                using (SqlCommand command = new SqlCommand(query, connection))
                {             

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorielist.Add(ReaderToCategorie(reader));
                        }
                    }
                }
            }
            return categorielist;
        }

        public bool Insert(Categorie categorie)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO Categorie (bijdrage_id, categorie_id, naam) VALUES (@param1, @param2, @param3)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", categorie.Id);
                        command.Parameters.AddWithValue("@param2", categorie.CategorieId);
                        command.Parameters.AddWithValue("@param3", categorie.Naam);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Categorie ReaderToCategorie(SqlDataReader reader)
        {
            return new Categorie(
                Convert.ToInt32(reader["bijdrage_id"]),
                Convert.ToInt32(reader["categorie_id"]),
                Convert.ToString(reader["naam"])   
            );
        }
    }
}
