using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
    public class BezoekerContext : IBezoekerContext
    {
        private List<Bezoeker> bezoekers = new List<Bezoeker>();

        public Bezoeker AddBezoeker(Bezoeker bezoeker)
        {
            bezoekers.Add(bezoeker);
            return bezoeker;
        }

        public List<Bezoeker> GetBezoeker(int eventID)
        {
            List<Bezoeker> result = new List<Bezoeker>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM bezoeker INNER JOIN account ON (bezoeker.ID = account.bezoekerID) WHERE account.eventID = @eventID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@eventID", eventID);
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateBezoekerFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kan geen data vinden");
                    }
                }
            }
            return result;
        }

        public Bezoeker GetById(int id)
        {
            Bezoeker bezoeker = null;
            using (SqlConnection conn = Connection.SQLconnection)
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT * FROM [bezoeker] WHERE ID = @id;", conn))
                using (SqlCommand cmd = new SqlCommand("SELECT* FROM bezoeker INNER JOIN account ON(bezoeker.ID = account.bezoekerID) WHERE bezoeker.ID = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bezoeker = CreateBezoekerFromReader(reader);
                            }
                        }
                        conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Laden bezoeker mislukt.");
                        return null;
                    }
                }
            }

            return bezoeker;
        }

        public Bezoeker GetByName(string naam, string email)
        {
            Bezoeker bezoeker = null;
            using (SqlConnection conn = Connection.SQLconnection)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT* FROM bezoeker INNER JOIN account ON(bezoeker.ID = account.bezoekerID) WHERE [bezoeker_naam] = @naam AND [bezoeker_email] = @email;", conn))
                {
                    cmd.Parameters.AddWithValue("@naam", naam);
                    cmd.Parameters.AddWithValue("@email", email);
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bezoeker = CreateBezoekerFromReader(reader);
                            }
                        }
                        conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Laden bezoeker mislukt.");
                        return null;
                    }
                }
            }

            return bezoeker;
        }

        public Bezoeker Insert(Bezoeker bezoeker, Reservering reservering)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "INSERT INTO Bezoeker (bezoeker_naam, bezoeker_email, bezoeker_telefoonnummer, bezoeker_aanwezig) VALUES (@bezoeker_naam, @bezoeker_email, @bezoeker_telefoonnummer, 0);" +
                    " INSERT INTO [bezoeker_reservering] (reserveringid, bezoekerid) VALUES (@reservering_id, (SELECT MAX(ID) FROM bezoeker))";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("bezoeker_naam", bezoeker.Naam);
                    command.Parameters.AddWithValue("bezoeker_email", bezoeker.Email);
                    command.Parameters.AddWithValue("bezoeker_telefoonnummer", bezoeker.Telefoonnummer);
                    command.Parameters.AddWithValue("bezoeker_aanwezig", bezoeker.Aanwezig);
                    command.Parameters.AddWithValue("reservering_id", reservering.Id);


                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                        throw;
                    }
                }

                query = "SELECT MAX(id) FROM Bezoeker";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    bezoeker = new Bezoeker(id, bezoeker.Naam, bezoeker.Email, bezoeker.Telefoonnummer, bezoeker.Aanwezig);
                }
            }

            return bezoeker;
        }

        public bool Update(Bezoeker bezoeker)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE Bezoeker" +
                    " SET bezoeker_naam = @bezoeker_naam, bezoeker_email = @bezoeker_email, bezoeker_telefoonnummer = @bezoeker_telefoonnummer, bezoeker_aanwezig = @bezoeker_aanwezig WHERE id = @ID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", bezoeker.Id);
                    command.Parameters.AddWithValue("@bezoeker_naam", bezoeker.Naam);
                    command.Parameters.AddWithValue("@bezoeker_email", bezoeker.Email);
                    command.Parameters.AddWithValue("@bezoeker_telefoonnummer", bezoeker.Telefoonnummer);
                    command.Parameters.AddWithValue("@bezoeker_aanwezig", bezoeker.Aanwezig);


                    try
                    {
                        if (Convert.ToInt32(command.ExecuteNonQuery()) > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }
            return false;
        }

        public bool Delete(int id)
        {

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query6 = "DELETE FROM Bezoeker WHERE WHERE ID = @id;";

                using (SqlCommand command = new SqlCommand(query6, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }

                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                        return false;
                    }
                }
            }
        }

        public void checkIn(int ID)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE Bezoeker SET bezoeker_aanwezig = 1 WHERE Id=@id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", ID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void checkOut(int ID)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "UPDATE Bezoeker SET bezoeker_aanwezig = 0 WHERE Id=@id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", ID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public bool getBetaalStatus(int id)
        {
            int betaalStatusINT = -1;
            using (SqlConnection conn = Connection.SQLconnection)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT reservering.reservering_betaalstatus FROM reservering INNER JOIN bezoeker_reservering on(reservering.ID = bezoeker_reservering.reserveringID) INNER JOIN bezoeker ON(bezoeker_reservering.bezoekerID = bezoeker.ID) WHERE bezoeker.ID = @id; ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                betaalStatusINT = Convert.ToInt32(reader["reservering_betaalstatus"]);
                            }
                        }
                        conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Laden betaalstatus mislukt.");

                    }
                }
            }

            if (betaalStatusINT == 0)
            {
                return false;
            }
            else if (betaalStatusINT == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Bezoeker CreateBezoekerFromReader(SqlDataReader reader)
        {
            return new Bezoeker(
            Convert.ToInt32(reader["ID"]),
            Convert.ToString(reader["bezoeker_naam"]),
            Convert.ToString(reader["bezoeker_email"]),
            Convert.ToString(reader["bezoeker_telefoonnummer"]),
            Convert.ToInt32(reader["bezoeker_aanwezig"]),
            Convert.ToInt32(reader["eventID"]));
        }
    }
}