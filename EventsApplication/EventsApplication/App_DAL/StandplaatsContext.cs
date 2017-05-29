using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
    public class StandplaatsContext : IStandplaatsContext
    {
        public List<Standplaats> GetFreeStaanplaatsenByLocatie(Locatie locatie, DateTime startdatum, DateTime einddatum)
        {
            SqlConnection conn = Connection.SQLconnection;
            List<Standplaats> staanplaatsen = new List<Standplaats>();
            try
            {
                string query =
                    "SELECT DISTINCT a.ID, standplaats_grootte, standplaats_prijs, standplaats_kenmerk FROM dbo.Standplaatsen a FULL OUTER JOIN reservering ON reservering.standplaatsID = a.ID WHERE locatieID = @locatieid AND ((@begindatum NOT BETWEEN reservering_begindatum AND reservering_einddatum AND @einddatum NOT BETWEEN reservering_begindatum AND reservering_einddatum) OR (reservering_begindatum is null AND reservering_einddatum is null)) ORDER BY standplaats_prijs;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);
                cmd.Parameters.AddWithValue("@begindatum", startdatum);
                cmd.Parameters.AddWithValue("@einddatum", einddatum);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"].ToString());
                    int grootte = Convert.ToInt32(reader["standplaats_grootte"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["standplaats_prijs"].ToString());
                    string kenmerk = reader["standplaats_kenmerk"].ToString();

                    staanplaatsen.Add(new Standplaats(ID, prijs, grootte, false, kenmerk));
                }
                conn.Close();

            }
            catch
            {
                throw;

            }
            return staanplaatsen;
        }


        public List<Standplaats> GetByLocatie(Locatie locatie)
        {
            SqlConnection conn = Connection.SQLconnection;
            List<Standplaats> staanplaatsen = new List<Standplaats>();
            try
            {
                string query = "SELECT * FROM dbo.Standplaatsen WHERE locatieID = @locatieid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"].ToString());
                    int grootte = Convert.ToInt32(reader["standplaats_grootte"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["standplaats_prijs"].ToString());
                    bool status = Convert.ToBoolean(reader["standplaats_status"].ToString());
                    string kenmerk = reader["standplaats_kenmerk"].ToString();

                    staanplaatsen.Add(new Standplaats(ID, prijs, grootte, status, kenmerk));
                }
                conn.Close();

            }
            catch { }
            return staanplaatsen;

        }

        public bool Delete(int id)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "DELETE FROM dbo.standplaatsen WHERE dbo.standplaatsen.ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", id);
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
        public bool Insert(int locatieID, int prijs, int grootte, bool status, string kenmerk)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "INSERT INTO dbo.standplaatsen(locatieID, standplaats_prijs, standplaats_grootte, standplaats_status, standplaats_kenmerk) VALUES(@locatieID, @standplaats_prijs, @standplaats_grootte, @standplaats_status, @standplaats_kenmerk)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@locatieID", locatieID);
                cmd.Parameters.AddWithValue("@standplaats_prijs", prijs);
                cmd.Parameters.AddWithValue("@standplaats_grootte", grootte);
                cmd.Parameters.AddWithValue("@standplaats_status", status);
                cmd.Parameters.AddWithValue("@standplaats_kenmerk", kenmerk);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }

        public bool Update(int ID, int prijs, int grootte, bool status, string kenmerk)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "UPDATE standplaatsen SET standplaats_prijs = @standplaats_prijs, standplaats_grootte = @standplaats_grootte, standplaats_status = @standplaats_status, standplaats_kenmerk = @standplaats_kenmerk WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@standplaats_prijs", prijs);
                cmd.Parameters.AddWithValue("@standplaats_grootte", grootte);
                cmd.Parameters.AddWithValue("@standplaats_status", status);
                cmd.Parameters.AddWithValue("@standplaats_kenmerk", kenmerk);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }

        public int getLatestEventID()
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "select MAX(ID) from locatie";
                SqlCommand cmd = new SqlCommand(query, connection);
                Int32 locatieID = (int)cmd.ExecuteScalar();
                return locatieID;
            }
        }
    }
}