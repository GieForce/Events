using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class BijdrageContext : IBijdrageContext
    {
        public Bijdrage GetById(int id)
        {
            
        }

        public bool Insert(Bijdrage bijdrage)
        {
            
        }

        public bool Delete(int id)
        {
            
        }

        public List<Bijdrage> GetAllBijdrages()
        {
            List<Bijdrage> bijdrageList = new List<Bijdrage>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM bijdrage";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bijdrageList.Add();
                        }
                    }
                }
            }
            return bijdrageList;
        }

        public bool Update(Bijdrage bijdrage)
        {
            
        }

        private Bijdrage CreateBerichtFromReader(SqlDataReader reader)
        {
            if (reader["soort"].ToString() == "categorie")
            {
                return new Categorie(
                    Convert.ToInt32(reader["categorie_id"]),
                    Convert.ToInt32(reader["bijdrage_id"]),
                    Convert.ToString(reader["naam"])
                );
            }
            else if (reader["soort"].ToString() == "bestand")
            {
                {
                    return new Bestand(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToInt32(reader["account_id"]),
                        Convert.ToDateTime(reader["datum"]),
                        Convert.ToString(reader["soort"]),
                        Convert.ToInt32(reader["bijdrage_id"]),
                        Convert.ToInt32(reader["categorie_id"]),
                        Convert.ToString(reader["bestandslocatie"]),
                        Convert.ToInt32(reader["grootte"])
                    );
                }
            }

            else if (reader["soort"].ToString() == "bericht")
            {
                {
                    return new Bericht(
                        Convert.ToString(reader["mediabestand_titel"]),
                        Convert.ToString(reader["mediabestand_omschrijving"]),
                        Convert.ToString(reader["mediabestand_url"]),
                        Convert.ToInt32(reader["mediabestand_grootte"]),
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToInt32(reader["eventID"]),
                        Convert.ToInt32(reader["accountID"])
                    );
                }
            }
            else
            {
                return null;
            }
        }

    }
}