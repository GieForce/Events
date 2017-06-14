using EventsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL.Interfaces
{
    public class PolsbandjeContext : IPolsbandjeContext
    {
        public Polsbandje GetByAccountId(Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT POLSBANDJE.ID, POLSBANDJE.barcode, POLSBANDJE.actief, RESERVERING_POLSBANDJE.reservering_id, RESERVERING_POLSBANDJE.polsbandje_id, RESERVERING_POLSBANDJE.account_id, RESERVERING_POLSBANDJE.aanwezig FROM POLSBANDJE INNER JOIN RESERVERING_POLSBANDJE ON (POLSBANDJE.ID=RESERVERING_POLSBANDJE.polsbandje_id) WHERE RESERVERING_POLSBANDJE.account_id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", account.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //return ReaderToAccount(reader);

                            while (reader.Read())
                            {
                                return ReaderToPolsbandje(reader);
                            }
                            return null;
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
        }

        public Polsbandje GetById(Polsbandje polsbandje)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT POLSBANDJE.ID, POLSBANDJE.barcode, POLSBANDJE.actief, RESERVERING_POLSBANDJE.reservering_id, RESERVERING_POLSBANDJE.polsbandje_id, RESERVERING_POLSBANDJE.account_id, RESERVERING_POLSBANDJE.aanwezig FROM POLSBANDJE INNER JOIN RESERVERING_POLSBANDJE ON (POLSBANDJE.ID=RESERVERING_POLSBANDJE.polsbandje_id) WHERE RESERVERING_POLSBANDJE.account_id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", polsbandje.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //return ReaderToAccount(reader);

                            while (reader.Read())
                            {
                                return ReaderToPolsbandje(reader);
                            }
                            return null;
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
        }

        public void Insert(Polsbandje polsbandje, Reservering reservering, Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO polsbandje (barcode, actief) VALUES (@barcode, 0)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@barcode", polsbandje.Barcode);

                        command.ExecuteNonQuery();
                    }

                    string queryReserveringPolsbandje = "INSERT INTO RESERVERING_POLSBANDJE (reservering_id, polsbandje_id, account_id, aanwezig) VALUES (@reserveringId, @polsbandjeId, @accountId, 0)";
                    using (SqlCommand command = new SqlCommand(queryReserveringPolsbandje, connection))
                    {
                        command.Parameters.AddWithValue("@reserveringId", reservering.Id);
                        command.Parameters.AddWithValue("@polsbandjeId", polsbandje.Id);
                        command.Parameters.AddWithValue("@accountId", account.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void ConnectAccountWithRFID(string RFID, Polsbandje polsbandje, Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string queryUpdatePolsbandje = "UPDATE POLSBANDJE SET barcode = @RFID, actief = 1 WHERE ID IN (SELECT polsbandje_id FROM RESERVERING_POLSBANDJE WHERE account_id=@accountId);";
                    using (SqlCommand command = new SqlCommand(queryUpdatePolsbandje, connection))
                    {
                        command.Parameters.AddWithValue("@RFID", RFID);
                        command.Parameters.AddWithValue("@accountId", account.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void Delete(Polsbandje polsbandje)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "DELETE FROM POLSBANDJE WHERE ID = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", polsbandje.Id);
                        command.ExecuteNonQuery();
                    }

                    string queryReserveringPolsbandje =
                    "DELETE FROM RESERVERING_POLSBANDJE WHERE polsbandje_id = @id";
                    using (SqlCommand command = new SqlCommand(queryReserveringPolsbandje, connection))
                    {
                        command.Parameters.AddWithValue("@id", polsbandje.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }
        
        private Polsbandje ReaderToPolsbandje(SqlDataReader reader)
        {
            return new Polsbandje(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["barcode"]),
                Convert.ToInt32(reader["actief"]),
                Convert.ToInt32(reader["reservering_id"]),
                Convert.ToInt32(reader["polsbandje_id"]),
                Convert.ToInt32(reader["account_id"]),
                Convert.ToInt32(reader["aanwezig"])
                );
        }
    }
}