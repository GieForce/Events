using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
    public class EvenementContext : IEvenementContext
    {
        //private List<Reservering> reserveringen = new List<Reservering>();
        //private List<Materiaal> materialen = new List<Materiaal>();

        /// <summary>
        /// Aanmaken van evenement
        /// </summary>
        /// <param name="evenement"></param>
        /// <param name="locatie"></param>
        /// <returns></returns>
        public bool AddEvenement(Evenement evenement, Locatie locatie)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "INSERT INTO event (locatieID, event_naam, event_begindatum, event_einddatum)" +
                               " VALUES (@locatieID, @event_naam, @event_begindatum, @event_einddatum)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@locatieID", locatie.Id);
                    command.Parameters.AddWithValue("@event_naam", evenement.Naam);
                    command.Parameters.AddWithValue("@event_begindatum", evenement.StartDatum);
                    command.Parameters.AddWithValue("@event_einddatum", evenement.EindDatum);
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException)
                    {
                        //MessageBox.Show("Deze titel is al reeds ingevoerd");
                        return false;
                    }
                }

            }
        }
        /// <summary>
        /// Het updaten van een evenement
        /// </summary>
        /// <param name="evenement"></param>
        /// <param name="locatie"></param>
        /// <returns></returns>
        public bool UpdateEvenement(Evenement evenement, Locatie locatie)
        {
            string query = "UPDATE event SET locatieID = @locatieID, event_naam = @event_naam, event_begindatum = @event_begindatum, event_einddatum = @event_einddatum WHERE ID = @id";
            using (SqlConnection connection = Connection.SQLconnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", evenement.Id);
                    command.Parameters.AddWithValue("@locatieID", locatie.Id);
                    command.Parameters.AddWithValue("@event_naam", evenement.Naam);
                    command.Parameters.AddWithValue("@event_begindatum", evenement.StartDatum);
                    command.Parameters.AddWithValue("@event_einddatum", evenement.EindDatum);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Updaten niet mogelijk");
                    return false;
                }
            }
        }

        /// <summary>
        /// Het deleten van een evenement
        /// </summary>
        /// <param name="evenement"></param>
        /// <returns></returns>
        public bool DeleteEvenement(Evenement evenement)
        {
            string query = "DELETE FROM event WHERE ID = @id";
            using (SqlConnection connection = Connection.SQLconnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", evenement.Id);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    //MessageBox.Show("De Song is al reeds verwijderd");
                    return false;
                }
            }
        }

        /// <summary>
        /// Get alle evenementen
        /// </summary>
        /// <returns></returns>
        public List<Evenement> GetEvenementen()
        {
            List<Evenement> evenementen = new List<Evenement>();
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM event";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = (int)reader["ID"];
                            string event_naam = (string)reader["event_naam"];
                            DateTime event_begindatum = (DateTime)reader["event_begindatum"];
                            DateTime event_einddatum = (DateTime)reader["event_einddatum"];
                            evenementen.Add(new Evenement(id, event_naam, event_begindatum, event_einddatum));
                        }
                        connection.Close();
                    }
                    return evenementen;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Reading events failed");
                return null;
            }

        }

        /// <summary>
        /// Krijg alle evenementen bij id
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>

        public Evenement GetEvenementById(int eid)
        {
            Evenement evenementReturn = null;
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM event WHERE ID = @id";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        command.Parameters.AddWithValue("@id", eid);
                        while (reader.Read())
                        {
                            int id = (int)reader["ID"];
                            string event_naam = (string)reader["event_naam"];
                            DateTime event_begindatum = (DateTime)reader["event_begindatum"];
                            DateTime event_einddatum = (DateTime)reader["event_einddatum"];
                            evenementReturn = new Evenement(id, event_naam, event_begindatum, event_einddatum);
                        }
                        connection.Close();
                    }
                    return evenementReturn;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Reading events failed");
                return null;
            }
        }

        public int GetBezoekersAantal(Evenement evenement)
        {
            int evenementReturn = 0;
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT COUNT(reservering.id) as amount FROM [event] INNER JOIN reservering ON reservering.eventID = [event].id INNER JOIN bezoeker_reservering ON reservering.id = reserveringID INNER JOIN bezoeker ON bezoeker.ID = bezoekerID WHERE eventID = @id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", evenement.Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            evenementReturn = Convert.ToInt32(reader["amount"]);
                        }
                        connection.Close();
                    }
                    return evenementReturn;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return 0;
            }
        }
    }
}