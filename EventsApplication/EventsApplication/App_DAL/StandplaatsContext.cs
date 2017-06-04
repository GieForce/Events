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
                    "SELECT DISTINCT a.ID, a.capaciteit, a.nummer, a.prijs FROM PLEK a FULL OUTER JOIN PLEK_RESERVERING b on b.plek_id = a.ID LEFT JOIN RESERVERING c on c.ID = b.reservering_id WHERE c.locatieID = @locatie_id AND ((@begindatum NOT BETWEEN c.datumStart AND c.datumEinde AND @einddatum NOT BETWEEN c.datumStart AND c.datumEinde) OR (c.datumStart is null AND c.datumEinde is null)) ORDER BY a.prijs; ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);
                cmd.Parameters.AddWithValue("@begindatum", startdatum);
                cmd.Parameters.AddWithValue("@einddatum", einddatum);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"].ToString());
                    int capaciteit = Convert.ToInt32(reader["capaciteit"].ToString());
                    int nummer = Convert.ToInt32(reader["nummer"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["prijs"].ToString());

                    staanplaatsen.Add(new Standplaats(nummer, capaciteit,prijs));
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
                    int capaciteit = Convert.ToInt32(reader["capaciteit"].ToString());
                    int nummer = Convert.ToInt32(reader["nummer"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["prijs"].ToString());

                    staanplaatsen.Add(new Standplaats(nummer, capaciteit, prijs));
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
        public bool Insert(int locatieID, decimal prijs, int capaciteit, int nummer)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "INSERT INTO dbo.standplaatsen(locatie_id, nummer, capaciteit, prijs) VALUES(@locatieID, @nummer, @capaciteit, @prijs)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@locatieID", locatieID);
                cmd.Parameters.AddWithValue("@nummer", nummer);
                cmd.Parameters.AddWithValue("@capaciteit", capaciteit);
                cmd.Parameters.AddWithValue("@prijs", prijs);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }

        public bool Update(int ID, int capaciteit, int nummer, decimal prijs)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "UPDATE standplaatsen SET capaciteit = @capaciteit, nummmer = @nummer, prijs = @prijs WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@nummer", nummer);
                cmd.Parameters.AddWithValue("@capaciteit", capaciteit);
                cmd.Parameters.AddWithValue("@prijs", prijs);
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
                string query = "select MAX(ID) from LOCATIE";
                SqlCommand cmd = new SqlCommand(query, connection);
                Int32 locatieID = (int)cmd.ExecuteScalar();
                return locatieID;
            }
        }
    }
}