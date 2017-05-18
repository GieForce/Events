using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
    public class LocatieContext : ILocatieContext
    {
        public bool Insert(Locatie locatie)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "INSERT INTO dbo.locatie(locatie_naam, locatie_adres, locatie_plaatsen, eventplattegrond_url) VALUES(@locatie_naam, @locatie_adres, @locatie_plaatsen, @locatie_url)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@locatie_naam", locatie.Naam);
                cmd.Parameters.AddWithValue("@locatie_adres", locatie.Adres);
                cmd.Parameters.AddWithValue("@locatie_plaatsen", locatie.Plaatsen);
                cmd.Parameters.AddWithValue("@locatie_url", locatie.LocatieUrl);

                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }

        public List<Locatie> GetAll()
        {
            SqlConnection conn = Connection.SQLconnection;
            List<Locatie> locaties = new List<Locatie>();
            try
            {
                string query = "SELECT * FROM dbo.locatie";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string naam = reader["locatie_naam"].ToString();
                    string adres = reader["locatie_adres"].ToString();
                    int plaatsen = Convert.ToInt32(reader["locatie_plaatsen"].ToString());
                    int id = Convert.ToInt32(reader["ID"].ToString());
                    string locatieUrl = reader["eventplattegrond_url"].ToString();

                    locaties.Add(new Locatie(naam, adres, plaatsen, id, locatieUrl));
                }
                conn.Close();

            }
            catch { }
            return locaties;
        }

        public Locatie GetByEvenement(Evenement evenement)
        {
            try
            {
                Locatie locatieReturn = null;
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM dbo.locatie INNER JOIN [event] ON [event].locatieID = locatie.ID WHERE [event].id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", evenement.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string naam = reader["locatie_naam"].ToString();
                            string adres = reader["locatie_adres"].ToString();
                            int plaatsen = Convert.ToInt32(reader["locatie_plaatsen"].ToString());
                            int id = Convert.ToInt32(reader["ID"].ToString());
                            string locatieUrl = reader["eventplattegrond_url"].ToString();
                            locatieReturn = new Locatie(naam, adres, plaatsen, id, locatieUrl);
                        }
                        connection.Close();
                    }
                }
                return locatieReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(Locatie locatie)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "DELETE FROM dbo.locatie WHERE dbo.locatie.ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", locatie.Id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                conn.Close();
                return false;
            }
        }

        public bool Update(Locatie locatie)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "UPDATE locatie SET locatie_naam = @locatie_naam, locatie_adres = @locatie_adres, locatie_plaatsen = @locatie_plaatsen, eventplattegrond_url = @locatie_url WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", locatie.Id);
                cmd.Parameters.AddWithValue("@locatie_naam", locatie.Naam);
                cmd.Parameters.AddWithValue("@locatie_adres", locatie.Adres);
                cmd.Parameters.AddWithValue("@locatie_plaatsen", locatie.Plaatsen);
                cmd.Parameters.AddWithValue("@locatie_url", locatie.LocatieUrl);

                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }
    }
}