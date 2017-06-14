using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventsApplication.ViewModels;

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

        public void PlaatsReservering(string voornaam, string tussenvoegsel, string achternaam, string straat, int huisnr,
            string woonplaats, DateTime start, DateTime eind, int evenementid, List<int> plekids, List<AccountViewModel> accounts, List<ProductExemplaar> producten)
        {
            int persoonID = 0;
            int hoofdAccountId = 0;

            using (SqlConnection connection = Connection.SQLconnection)
            {
                //Insert into table PERSOON
                string queryPERSOON = "INSERT INTO PERSOON (voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, banknr) VALUES (@voornaam, @tussenvoegsel, @achternaam, @straat, @huisnr, @woonplaats, 'n.v.t.');";

                using (SqlCommand command = new SqlCommand(queryPERSOON, connection))
                {
                    command.Parameters.AddWithValue("@voornaam", voornaam);
                    command.Parameters.AddWithValue("@tussenvoegsel", tussenvoegsel);
                    command.Parameters.AddWithValue("@achternaam", achternaam);
                    command.Parameters.AddWithValue("@straat", straat);
                    command.Parameters.AddWithValue("@huisnr", huisnr);
                    command.Parameters.AddWithValue("@woonplaats", woonplaats);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }

                }
                //End table PERSOON

                //Get id from PERSOON
                string queryPERSOONID = "SELECT MAX(ID) as id FROM PERSOON";

                using (SqlCommand command = new SqlCommand(queryPERSOONID, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                persoonID = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        throw;
                    }

                }
                //End get PersonID

                //Insert into table RESERVERING
                string queryRESERVERING = "INSERT INTO RESERVERING (persoon_id, datumStart, datumEinde, betaald, eventID) VALUES (@persoon_id, @datumStart, @datumEind, @betaald, @eventID)";

                using (SqlCommand command = new SqlCommand(queryRESERVERING, connection))
                {
                    command.Parameters.AddWithValue("@persoon_id", persoonID);
                    command.Parameters.AddWithValue("@datumStart", start);
                    command.Parameters.AddWithValue("@datumEind", eind);
                    command.Parameters.AddWithValue("@betaald", true);
                    command.Parameters.AddWithValue("@eventID", evenementid);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }

                }
                //End insert into table RESERVERING

                //Insert into plek_reservering
                string queryPLEKRESERVERING = "INSERT INTO PLEK_RESERVERING (plek_id, reservering_id) VALUES (@plekID, (SELECT MAX(id) FROM RESERVERING))";

                foreach (int plekid in plekids)
                {
                    using (SqlCommand command = new SqlCommand(queryPLEKRESERVERING, connection))
                    {
                        command.Parameters.AddWithValue("@plekID", plekid);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
                    }

                }
                //End insert into plek_reservering

                foreach (AccountViewModel account in accounts)
                {
                    try
                    {
                        string query =
                            "INSERT INTO Account (gebruikersnaam, email, activatiehash, geactiveerd, wachtwoord, telefoonnummer, status) VALUES (@gebruikersnaam, @email, @activatiehash, @geactiveerd, @wachtwoord, @telefoonnummer, 0)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@gebruikersnaam", account.Gebruikersnaam);
                            command.Parameters.AddWithValue("@email", account.Email);
                            command.Parameters.AddWithValue("@activatiehash", Guid.NewGuid());
                            command.Parameters.AddWithValue("@geactiveerd", false);
                            command.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                            command.Parameters.AddWithValue("@telefoonnummer", account.Telefoonnummer);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    string idQuery =
                        "INSERT INTO RESERVERING_POLSBANDJE (reservering_id, polsbandje_id, account_id, aanwezig) VALUES ((SELECT MAX(id) FROM RESERVERING), 1, (SELECT MAX(id) FROM ACCOUNT), 0)";
                    using (SqlCommand command = new SqlCommand(idQuery, connection))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
                    }

                    if (hoofdAccountId == 0)
                    {
                        string hoofdQuery =
                            "SELECT MAX(id) as id FROM RESERVERING_POLSBANDJE";
                        using (SqlCommand command = new SqlCommand(hoofdQuery, connection))
                        {
                            try
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        hoofdAccountId = Convert.ToInt32(reader["id"]);
                                    }
                                }
                            }
                            catch (SqlException)
                            {
                                throw;
                            }
                        }
                    }
                }

                foreach (ProductExemplaar product in producten)
                {
                    Verhuur v = new Verhuur(product.Id, hoofdAccountId, eind, start, 0, true);
                    string queryProduct = "INSERT INTO VERHUUR VALUES (@exemplaarid, @reserveringid, @datumin, @datumuit, 0, 1)";

                    using (SqlCommand command = new SqlCommand(queryProduct, connection))
                    {
                        command.Parameters.AddWithValue("@exemplaarid", product.Id);
                        command.Parameters.AddWithValue("@reserveringid", hoofdAccountId);
                        command.Parameters.AddWithValue("@datumin", eind);
                        command.Parameters.AddWithValue("@datumuit", start);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
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