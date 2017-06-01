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
        //public Bijdrage GetById(int id)
        //{
            
        //}

        //public bool Insert(Bijdrage bijdrage)
        //{
            
        //}

        //public bool Delete(int id)
        //{
            
        //}

        public Bijdrage GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bijdrage> GetAllBijdrages()
        {
            List<Bijdrage> bijdrageList = new List<Bijdrage>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "SELECT * FROM BIJDRAGE b " +
                    "LEFT JOIN CATEGORIE c on c.bijdrage_id = b.ID " +
                    "LEFT JOIN BESTAND be on be.bijdrage_id = b.ID " +
                    "LEFT JOIN BERICHT br on br.bijdrage_id = b.ID " +
                    "LEFT JOIN ACCOUNT a on a.ID = b.account_id " +
                    "LEFT JOIN ACCOUNT_BIJDRAGE ab on ab.bijdrage_id = b.ID";

                    
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bijdrage bijdrage = CreateBijdrageFromReader(reader);
                            bijdrageList.Add(bijdrage);
                        }
                    }
                }
            }
            return bijdrageList;
        }

        //public bool Update(Bijdrage bijdrage)
        //{

        //}

        private Bijdrage CreateBijdrageFromReader(SqlDataReader reader)
        {
            if (reader["soort"].ToString() == "categorie")
            {
                return new Categorie(
                    Convert.ToInt32(reader["ID"]),
                    Convert.ToInt32(reader["account_id"]),
                    Convert.ToDateTime(reader["datum"]),
                    Convert.ToString(reader["soort"]),
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
                        Convert.ToInt32(reader["bijdrage_id"] != DBNull.Value ? Convert.ToInt32(reader["bijdrage_id"]) : 0),
                        Convert.ToInt32(reader["categorie_id"] != DBNull.Value ? Convert.ToInt32(reader["categorie_id"]) : 0),
                        Convert.ToString(reader["bestandslocatie"]),
                        Convert.ToInt32(reader["grootte"])
                    );
                }
            }

            else if (reader["soort"].ToString() == "bericht")
            {
                {
                    return new Bericht(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToInt32(reader["account_id"]),
                        Convert.ToDateTime(reader["datum"]),
                        Convert.ToString(reader["soort"]),
                        Convert.ToString(reader["titel"]),
                        Convert.ToString(reader["inhoud"])
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