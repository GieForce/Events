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
                    "SELECT * FROM (SELECT a.ID, a.capaciteit, a.nummer, a.prijs, S.naam, ROW_NUMBER() OVER (PARTITION BY a.id ORDER BY a.ID) AS RowNumber FROM PLEK a FULL OUTER JOIN PLEK_RESERVERING b on b.plek_id = a.ID LEFT JOIN RESERVERING c on c.ID = b.reservering_id LEFT JOIN PLEK_SPECIFICATIE PS on a.ID = PS.plek_id LEFT JOIN SPECIFICATIE S on PS.specificatie_id = S.ID  WHERE a.locatie_id = 1 AND S.naam != 'coordinaat x' AND S.naam != 'coordinaat y' AND PS.waarde = 'ja' AND (('06/12/2017' NOT BETWEEN c.datumStart AND c.datumEinde AND '06/14/2017' NOT BETWEEN c.datumStart AND c.datumEinde) OR (c.datumStart is null AND c.datumEinde is null))) AS a WHERE a.rownumber = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);
                cmd.Parameters.AddWithValue("@begindatum", startdatum);
                cmd.Parameters.AddWithValue("@einddatum", einddatum);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"]);
                    int capaciteit = Convert.ToInt32(reader["capaciteit"]);
                    int nummer = Convert.ToInt32(reader["nummer"]);
                    decimal prijs = Convert.ToDecimal(reader["prijs"]);
                    string specificatie = reader["naam"].ToString();

                    staanplaatsen.Add(new Standplaats(ID, nummer, capaciteit,prijs, specificatie));
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
                string query = "SELECT P.ID, P.capaciteit, P.nummer, S.naam, P.prijs FROM PLEK P INNER JOIN PLEK_SPECIFICATIE PS ON P.ID = PS.plek_id INNER JOIN SPECIFICATIE S ON PS.specificatie_id = S.ID WHERE S.naam != 'coordinaat x' AND S.naam != 'coordinaat y' AND PS.waarde = 'ja' AND P.locatie_id = @locatieid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["P.ID"].ToString());
                    int capaciteit = Convert.ToInt32(reader["P.capaciteit"].ToString());
                    int nummer = Convert.ToInt32(reader["P.nummer"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["P.prijs"].ToString());
                    string specificatie = reader["S.naam"].ToString();

                    staanplaatsen.Add(new Standplaats(ID, nummer, capaciteit, prijs, specificatie));
                }
                conn.Close();

            }
            catch { }
            return staanplaatsen;

        }

        public Standplaats GetByReservation(int reservationID)
        {
            Standplaats splts = null;
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query =
                    "SELECT P.ID, P.capaciteit, P.nummer, S.naam, P.prijs FROM PLEK P INNER JOIN PLEK_SPECIFICATIE PS ON P.ID = PS.plek_id INNER JOIN SPECIFICATIE S ON PS.specificatie_id = S.ID INNER JOIN PLEK_RESERVERING ON (P.ID=PLEK_RESERVERING.plek_id) WHERE S.naam != 'coordinaat x' AND S.naam != 'coordinaat y' AND PS.waarde = 'ja' AND PLEK_RESERVERING.reservering_id = @reservering_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@reservering_id", reservationID);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["P.ID"].ToString());
                    int capaciteit = Convert.ToInt32(reader["P.capaciteit"].ToString());
                    int nummer = Convert.ToInt32(reader["P.nummer"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["P.prijs"].ToString());
                    string specificatie = reader["S.naam"].ToString();

                    splts = new Standplaats(ID, nummer, capaciteit, prijs, specificatie);
                }
                conn.Close();

            }
            catch
            {

            }

            return splts;

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
        public bool Insert(Locatie locatie, decimal prijs, int capaciteit, int nummer, string specificatie)
        {
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query = "Standplaatstoevoegen";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@locatieid", locatie.Id);
                cmd.Parameters.AddWithValue("@nummer", nummer);
                cmd.Parameters.AddWithValue("@capaciteit", capaciteit);
                cmd.Parameters.AddWithValue("@prijs", prijs);
                cmd.Parameters.AddWithValue("@specificatie", specificatie);
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
                string query = "select MAX(ID) from EVENT";
                SqlCommand cmd = new SqlCommand(query, connection);
                Int32 locatieID = (int)cmd.ExecuteScalar();
                return locatieID;
            }
        }

        public Standplaats GetById(int id)
        {
            Standplaats splts = null;
            SqlConnection conn = Connection.SQLconnection;
            try
            {
                string query =
                    "SELECT P.ID, P.capaciteit, P.nummer, S.naam, P.prijs FROM PLEK P INNER JOIN PLEK_SPECIFICATIE PS ON P.ID = PS.plek_id INNER JOIN SPECIFICATIE S ON PS.specificatie_id = S.ID WHERE S.naam != 'coordinaat x' AND S.naam != 'coordinaat y' AND PS.waarde = 'ja' AND p.id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["ID"].ToString());
                    int capaciteit = Convert.ToInt32(reader["capaciteit"].ToString());
                    int nummer = Convert.ToInt32(reader["nummer"].ToString());
                    decimal prijs = Convert.ToDecimal(reader["prijs"].ToString());
                    string specificatie = reader["naam"].ToString();

                    splts = new Standplaats(ID, nummer, capaciteit, prijs, specificatie);
                }
                conn.Close();

            }
            catch
            {

            }
            return splts;
        }
    }
}