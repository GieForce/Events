using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
    public class MateriaalContext : IMateriaalContext
    {
        private List<Materiaal> materialen = new List<Materiaal>();


        public void AddMateriaal(Materiaal materiaal)
        {
            materialen.Add(materiaal);
        }

        public List<Materiaal> GetMateriaalReservering(int reserveringsID)
        {
            List<Materiaal> result = new List<Materiaal>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM Materiaal WHERE reserveringID = @reserveringid;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reserveringid", reserveringsID);
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateMateriaalFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        //System.Windows.Forms.MessageBox.Show("Kan geen data vinden");
                    }
                }
            }
            return result;
        }

        public List<Materiaal> GetMateriaalEvent(Evenement evenement)
        {
            List<Materiaal> result = new List<Materiaal>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "SELECT * FROM Materiaal INNER JOIN[event] ON [event].ID = materiaal.eventID WHERE [event].ID = @id";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@eventid", evenement.Id);
                    try
                    {
                        command.Parameters.AddWithValue("@id", evenement.Id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateMateriaalFromReader(reader));
                            }
                        }

                    }
                    catch
                    {
                        //System.Windows.Forms.MessageBox.Show("Kan geen data vinden");
                    }
                }
            }
            return result;
        }



        public Materiaal GetById(int id)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM Materiaal WHERE Id=:id LIMIT 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreateMateriaalFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Materiaal Insert(Materiaal materiaal, Evenement evenement)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "INSERT INTO Materiaal (eventID, reserveringID, materiaal_status, materiaal_prijs, materiaal_naam, materiaal_omschrijving)" +
                               " VALUES (@eventID , @reserveringID, @materiaal_status, @materiaal_prijs, @materiaal_naam, @materiaal_omschrijving)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reserveringID", 1);
                    command.Parameters.AddWithValue("@materiaal_status", materiaal.Bezet);
                    command.Parameters.AddWithValue("@materiaal_prijs", materiaal.Prijs);
                    command.Parameters.AddWithValue("@materiaal_naam", materiaal.Naam);
                    command.Parameters.AddWithValue("@materiaal_omschrijving", materiaal.Omschrijving);
                    command.Parameters.AddWithValue("@eventID", evenement.Id);

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

                query = "SELECT MAX(id) FROM Materiaal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    materiaal = new Materiaal(id, materiaal.ReserveringID, materiaal.EventID, materiaal.Naam, materiaal.Omschrijving, materiaal.Prijs, materiaal.Bezet);
                }
            }

            return materiaal;
        }

        public Materiaal Insert(Materiaal materiaal, Reservering reservering)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "INSERT INTO Materiaal (reserveringID, materiaal_status, materiaal_prijs, materiaal_naam, materiaal_omschrijving)" +
               " VALUES (@reserveringID, @materiaal_status, @materiaal_prijs, @materiaal_naam, @materiaal_omschrijving)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reserveringID", reservering.Id);
                    command.Parameters.AddWithValue("@materiaal_status", materiaal.Bezet);
                    command.Parameters.AddWithValue("@materiaal_prijs", materiaal.Prijs);
                    command.Parameters.AddWithValue("@materiaal_naam", materiaal.Naam);
                    command.Parameters.AddWithValue("@materiaal_omschrijving", materiaal.Omschrijving);

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

                query = "SELECT MAX(id) FROM Materiaal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    materiaal = new Materiaal(id, materiaal.ReserveringID, materiaal.EventID, materiaal.Naam, materiaal.Omschrijving, materiaal.Prijs, materiaal.Bezet);
                }
            }

            return materiaal;
        }

        public bool Update(Materiaal materiaal)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE Materiaal SET materiaal_prijs = @materiaal_prijs , materiaal_naam = @materiaal_naam, materiaal_omschrijving = @materiaal_omschrijving WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", materiaal.Id);
                    command.Parameters.AddWithValue("@materiaal_prijs", materiaal.Prijs);
                    command.Parameters.AddWithValue("@materiaal_naam", materiaal.Naam);
                    command.Parameters.AddWithValue("@materiaal_omschrijving", materiaal.Omschrijving);

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch (Exception e)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }

            return false;
        }

        public bool Delete(int id)
        {

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "DELETE FROM materiaal WHERE ID = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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


        private Materiaal CreateMateriaalFromReader(SqlDataReader reader)
        {
            return new Materiaal(
            Convert.ToInt32(reader["id"]),
            Convert.ToInt32(reader["reserveringID"]),
            Convert.ToInt32(reader["eventID"]),
            Convert.ToString(reader["materiaal_naam"]),
            Convert.ToString(reader["materiaal_omschrijving"]),
            Convert.ToInt32(reader["materiaal_prijs"]),
            Convert.ToBoolean(reader["materiaal_status"]));
        }
    }
}