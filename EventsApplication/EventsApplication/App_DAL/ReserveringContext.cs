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

        public List<Reservering> GetAllReserveringen()
        {
            List<Reservering> result = new List<Reservering>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT reservering.id, voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, betaald, datumStart, datumEinde, plek_id, eventID FROM Reservering INNER JOIN Persoon ON persoon_id = persoon.id INNER JOIN PLEK_RESERVERING ON reservering.ID = PLEK_RESERVERING.plek_id;";


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
                string query = "SELECT reservering.id, voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, betaald, datumStart, datumEinde, plek_id, eventID FROM Reservering INNER JOIN Persoon ON persoon_id = persoon.id INNER JOIN PLEK_RESERVERING ON reservering.ID = PLEK_RESERVERING.plek_id WHERE RESERVERING.ID = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateReserveringFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void Insert(Reservering reservering)
        {
            int persoonID = 0;

            using (SqlConnection connection = Connection.SQLconnection)
            {
                //Insert into table PERSOON
                string queryPERSOON = "INSERT INTO PERSOON (voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, banknr) VALUES (@voornaam, @tussenvoegsel, @achternaam, @straat, @huisnr, @woonplaats, n.v.t.)";
                
                using (SqlCommand command = new SqlCommand(queryPERSOON, connection))
                {
                    command.Parameters.AddWithValue("@voornaam", reservering.Voornaam);
                    command.Parameters.AddWithValue("@tussenvoegsel", reservering.Tussenvoegsel);
                    command.Parameters.AddWithValue("@achternaam", reservering.Achternaam);
                    command.Parameters.AddWithValue("@straat", reservering.Straat);
                    command.Parameters.AddWithValue("@huisnr", reservering.Huisnummer);
                    command.Parameters.AddWithValue("@woonplaats", reservering.Woonplaats);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);

                        throw;
                    }

                }
                //End table PERSOON

                //Get id from PERSOON
                string queryPERSOONID = "SELECT ID FROM PERSOON WHERE voornaam=@voornaam, tussenvoegsel=@tussenvoegsel, achternaam=@achternaam, straat=@straat, huisnr=@huisnr, woonplaats=@woonplaats;";

                using (SqlCommand command = new SqlCommand(queryPERSOONID, connection))
                {
                    command.Parameters.AddWithValue("@voornaam", reservering.Voornaam);
                    command.Parameters.AddWithValue("@tussenvoegsel", reservering.Tussenvoegsel);
                    command.Parameters.AddWithValue("@achternaam", reservering.Achternaam);
                    command.Parameters.AddWithValue("@straat", reservering.Straat);
                    command.Parameters.AddWithValue("@huisnr", reservering.Huisnummer);
                    command.Parameters.AddWithValue("@woonplaats", reservering.Woonplaats);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                persoonID = Convert.ToInt32(reader["ID"]);
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);

                        throw;
                    }

                }
                //End get PersonID

                //Insert into table RESERVERING
                string queryRESERVERING = "INSERT INTO RESERVERING (persoon_id, datumStart, datumEind, betaald, eventID) VALUES (@persoon_id, @datumStart, @datumEind, @betaald, @eventID)";

                using (SqlCommand command = new SqlCommand(queryRESERVERING, connection))
                {
                    command.Parameters.AddWithValue("@persoon_id", persoonID);
                    command.Parameters.AddWithValue("@datumStart", reservering.StartDatum);
                    command.Parameters.AddWithValue("@datumEind", reservering.EindDatum);
                    command.Parameters.AddWithValue("@betaald", reservering.Betaald);
                    command.Parameters.AddWithValue("@eventID", reservering.Huisnummer);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);

                        throw;
                    }

                }
                //End insert into table RESERVERING

                //Insert into plek_reservering
                string queryPLEKRESERVERING = "INSERT INTO PLEK_RESERVERING (plek_id, reservering_id) VALUES (@plekID, @reserveringID)";

                using (SqlCommand command = new SqlCommand(queryPLEKRESERVERING, connection))
                {
                    command.Parameters.AddWithValue("@plekID", reservering.StandplaatsId);
                    command.Parameters.AddWithValue("@reserveringID", reservering.Id);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);

                        throw;
                    }

                }
                //End insert into plek_reservering

            }
        }

        public void Delete(Reservering reservering)
        {

            using (SqlConnection connection = Connection.SQLconnection)
            {
                //Delete row in table reservering
                string query = "DELETE FROM PLEK_RESERVERING WHERE Id= @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", reservering.Id);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }

                //Delete row in table plek_reservering
                string queryPLEKRESERVERING = "DELETE FROM reservering WHERE Id= @id";
                using (SqlCommand command = new SqlCommand(queryPLEKRESERVERING, connection))
                {
                    command.Parameters.AddWithValue("id", reservering.Id);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        //System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }
        }

    private Reservering CreateReserveringFromReader(SqlDataReader reader)
        {
            return new Reservering(
            Convert.ToInt32(reader["id"]),
            Convert.ToString(reader["voornaam"]),
            Convert.ToString(reader["tussenvoegsel"]),
            Convert.ToString(reader["achternaam"]),
            Convert.ToString(reader["straat"]),
            Convert.ToInt32(reader["huisnr"]),
            Convert.ToString(reader["woonplaats"]),
            Convert.ToInt32(reader["betaald"]),
            Convert.ToDateTime(reader["datumStart"]),
            Convert.ToDateTime(reader["datumEinde"]),
            Convert.ToInt32(reader["plek_id"]),
            Convert.ToInt32(reader["eventID"]));
        }
    }
}