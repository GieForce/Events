using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventsApplication.App_DAL
{
    public class ReserveringContext : IReserveringContext
    {
        private List<Reservering> reserveringen = new List<Reservering>();

        public Reservering AddReservering(Reservering reservering)
        {
            reserveringen.Add(reservering);
            return reservering;
        }

        public List<Reservering> GetReservering()
        {
            List<Reservering> result = new List<Reservering>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT ID, standplaatsID, reservering_naam, reservering_adres, reservering_woonplaats, reservering_aantal, reservering_startdatum, reservering_einddatum, reservering_betaalstatus" +           //////
                               "FROM Reservering" +
                               "INNER JOIN standplaatsen" +
                               "ON standplaatsen.ID = reservering.standplaatsID" +
                               "ORDER BY reservering.ID ASC";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateReserveringFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        //MessageBox.Show("Kan geen data vinden");
                    }
                }
            }
            return result;
        }

        public Reservering GetById(int id)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM Reservering WHERE Id=:id LIMIT 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreateReserveringFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Reservering GetByBezoeker(int bezoekerid)
        {
            Reservering reservering = null;
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM reservering WHERE ID = (SELECT reserveringID FROM bezoeker_reservering WHERE bezoekerID = @id);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", bezoekerid);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservering = CreateReserveringFromReader(reader);
                        }
                    }
                }
            }

            return reservering;
        }

        public Reservering Insert(Reservering reservering, Evenement evenement)
        {
            Reservering returnReservering = null;
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "INSERT INTO Reservering (standplaatsID, reservering_telefoonnummer, eventID, reservering_naam, reservering_adres, reservering_email, reservering_woonplaats, reservering_aantal, reservering_begindatum, reservering_einddatum, reservering_betaalstatus)" +
               " VALUES (@standplaatsID, @reservering_telefoonnummer, @eventid, @reservering_naam, @reservering_adres, @reservering_email, @reservering_woonplaats, @reservering_aantal, @reservering_begindatum, @reservering_einddatum, @reservering_betaalstatus)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@standplaatsID", reservering.Staanplaats.Plaatsnummer);
                    command.Parameters.AddWithValue("@eventid", evenement.Id);
                    command.Parameters.AddWithValue("@reservering_email", reservering.Bezoekers[0].Email);
                    command.Parameters.AddWithValue("@reservering_telefoonnummer", reservering.Bezoekers[0].Telefoonnummer);
                    command.Parameters.AddWithValue("@reservering_naam", reservering.Naam);
                    command.Parameters.AddWithValue("@reservering_adres", reservering.Adres);
                    command.Parameters.AddWithValue("@reservering_woonplaats", reservering.Woonplaats);
                    command.Parameters.AddWithValue("@reservering_aantal", reservering.Aantal);
                    command.Parameters.AddWithValue("@reservering_begindatum", reservering.StartDatum);
                    command.Parameters.AddWithValue("@reservering_einddatum", reservering.EindDatum);
                    command.Parameters.AddWithValue("@reservering_betaalstatus", reservering.Betaalstatus);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {

                        //System.Windows.Forms.MessageBox.Show(e.Message);

                        throw;
                    }
                }

                string query2 = "SELECT * FROM reservering WHERE id=(SELECT max(id) FROM reservering)";


                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                returnReservering = CreateReserveringFromReader(reader);
                            }
                        }
                    }
                    catch (SqlException e)
                    {

                        //System.Windows.Forms.MessageBox.Show(e.Message);
                        throw;
                    }
                }

            }
            return returnReservering;
        }

        public bool Update(Reservering reservering)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE reservering" +
                    " SET standplaatsID = @staanplaatsID, [reservering_naam] = @reservering_naam, [reservering_adres] = @reservering_adres, [reservering_woonplaats] = @reservering_woonplaats, [reservering_aantal] = @reservering_aantal, [reservering_begindatum] = @reservering_begindatum, [reservering_einddatum] = @reservering_einddatum, [reservering_betaalstatus] = @reservering_Betaalstatus WHERE ID = @id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", reservering.Id);
                    command.Parameters.AddWithValue("staanplaatsID", reservering.Staanplaatsid);
                    command.Parameters.AddWithValue("reservering_naam", reservering.Naam);
                    command.Parameters.AddWithValue("reservering_adres", reservering.Adres);
                    command.Parameters.AddWithValue("reservering_woonplaats", reservering.Woonplaats);
                    command.Parameters.AddWithValue("reservering_aantal", reservering.Aantal);
                    command.Parameters.AddWithValue("reservering_begindatum", reservering.StartDatum);
                    command.Parameters.AddWithValue("reservering_einddatum", reservering.EindDatum);
                    command.Parameters.AddWithValue("reservering_betaalstatus", reservering.Betaalstatus);


                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);
                        return false;
                    }



                }
            }
        }

        public bool Delete(int id)
        {

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "DELETE FROM reservering WHERE Id=" + id;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    try
                    {
                        if (Convert.ToInt32(command.ExecuteNonQuery()) != 1)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }

            return false;
        }

        private Reservering CreateReserveringFromReader(SqlDataReader reader)
        {
            return new Reservering(
            Convert.ToInt32(reader["id"]),
            Convert.ToInt32(reader["eventID"]),
            Convert.ToInt32(reader["standplaatsID"]),
            Convert.ToString(reader["reservering_naam"]),
            Convert.ToString(reader["reservering_adres"]),
            Convert.ToString(reader["reservering_woonplaats"]),
            Convert.ToString(reader["reservering_email"]),
            Convert.ToString(reader["reservering_telefoonnummer"]),
            Convert.ToInt32(reader["reservering_aantal"]),
            Convert.ToDateTime(reader["reservering_begindatum"]),
            Convert.ToDateTime(reader["reservering_einddatum"]),
            Convert.ToBoolean(reader["reservering_betaalstatus"]));
        }
    }
}