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
                    "LEFT JOIN CATEGORIE c on b.ID = c.bijdrage_id " +
                    "LEFT JOIN BESTAND be on b.ID = be.bijdrage_id " +
                    "LEFT JOIN BERICHT br on b.ID = br.bijdrage_id " +
                    "LEFT JOIN ACCOUNT a on b.account_id = a.ID " +
                    "LEFT JOIN ACCOUNT_BIJDRAGE ab on b.ID = ab.bijdrage_id";



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
                    Convert.ToInt32(reader["categorie_id"] != DBNull.Value ? Convert.ToInt32(reader["categorie_id"]) : 0),
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
                        Convert.ToInt32(reader["categorie_id"] != DBNull.Value ? Convert.ToInt32(reader["categorie_id"]) : 0),
                        Convert.ToString(reader["bestandslocatie"] != DBNull.Value ? Convert.ToString(reader["bestandslocatie"]) : ""),
                        Convert.ToInt32(reader["grootte"] != DBNull.Value ? Convert.ToInt32(reader["grootte"]) : 0)
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