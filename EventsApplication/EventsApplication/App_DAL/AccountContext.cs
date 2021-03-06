﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class AccountContext : IAccountContext
    {
        /// <summary>
        /// Get an account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetById(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM account WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //return ReaderToAccount(reader);

                            while (reader.Read())
                            {
                                int userid = Convert.ToInt32(reader["ID"]);
                                string gebruikersnaam = (string)reader["gebruikersnaam"];
                                string email = (string)reader["email"];
                                string activatiehash = (string)reader["activatiehash"];
                                bool geactiveerd = Convert.ToBoolean(reader["geactiveerd"]);
                                string wachtwoord = (string)reader["wachtwoord"];
                                string telefoonnummer = (string)reader["telefoonnummer"];
                                bool status = (bool)reader["status"];
                                Account account = new Account(userid, gebruikersnaam, email, activatiehash, geactiveerd, wachtwoord, telefoonnummer, status);
                                return account;
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

        public bool Insert(Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "INSERT INTO Account (gebruikersnaam, email, activatiehash, geactiveerd, wachtwoord, telefoonnummer) VALUES (@gebruikersnaam, @email, @activatiehash, @geactiveerd, @wachtwoord, @telefoonnummer)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gebruikersnaam", account.Gebruikersnaam);
                        command.Parameters.AddWithValue("@email", account.Email);
                        command.Parameters.AddWithValue("@activatiehash", account.Activatiehash);
                        command.Parameters.AddWithValue("@geactiveerd", account.Geactiveerd);
                        command.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                        command.Parameters.AddWithValue("@telefoonnummer", account.Telefoonnummer);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertAdmin(Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "INSERT INTO Account (gebruikersnaam, email, activatiehash, geactiveerd, wachtwoord, telefoonnummer, status) VALUES (@gebruikersnaam, @email, @activatiehash, @geactiveerd, @wachtwoord, @telefoonnummer, @status)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gebruikersnaam", account.Gebruikersnaam);
                        command.Parameters.AddWithValue("@email", account.Email);
                        command.Parameters.AddWithValue("@activatiehash", account.Activatiehash);
                        command.Parameters.AddWithValue("@geactiveerd", account.Geactiveerd);
                        command.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                        command.Parameters.AddWithValue("@telefoonnummer", account.Telefoonnummer);
                        command.Parameters.AddWithValue("@status", account.Status);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete an account from the database
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "DELETE FROM account WHERE ID = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Account account)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "UPDATE ACCOUNT SET gebruikersnaam = @gebruikersnaam, email = @email, activatiehash = @activatiehash, geactiveerd = 1, wachtwoord = @wachtwoord, telefoonnummer = @telefoonnummer WHERE ID = @ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gebruikersnaam", account.Gebruikersnaam);
                        command.Parameters.AddWithValue("@email", account.Email);
                        command.Parameters.AddWithValue("@activatiehash", account.Activatiehash);
                        command.Parameters.AddWithValue("@geactiveerd", true);
                        command.Parameters.AddWithValue("@ID", account.Id);
                        command.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                        command.Parameters.AddWithValue("@telefoonnummer", account.Telefoonnummer);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Activeer(string hash)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "UPDATE ACCOUNT SET geactiveerd = 1 WHERE activatiehash = @hash";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hash", hash);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account Login(string wachtwoord, string gebruikersnaam)
        {
            using (SqlConnection connection = Connection.SQLconnection)
            { 
                string query = "SELECT * FROM Account WHERE gebruikersnaam = @param1 and wachtwoord = @param2";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@param1",gebruikersnaam);
                    command.Parameters.AddWithValue("@param2", wachtwoord);
                  
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return ReaderToAccount(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accountlist = new List<Account>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM account";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountlist.Add(ReaderToAccount(reader));
                        }
                    }
                }
            }
            return accountlist;
        }

        public List<Account> GetAllAccountsByReservation(int reserveringsID)
        {
            List<Account> accountlist = new List<Account>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT ACCOUNT.ID, ACCOUNT.gebruikersnaam, ACCOUNT.email, ACCOUNT.activatiehash, ACCOUNT.geactiveerd, ACCOUNT.wachtwoord, ACCOUNT.telefoonnummer, POLSBANDJE.barcode, ACCOUNT.status FROM ACCOUNT INNER JOIN RESERVERING_POLSBANDJE ON (ACCOUNT.ID=RESERVERING_POLSBANDJE.account_id) INNER JOIN RESERVERING ON (RESERVERING_POLSBANDJE.reservering_id=RESERVERING.ID) INNER JOIN POLSBANDJE ON (RESERVERING_POLSBANDJE.polsbandje_id=POLSBANDJE.ID) WHERE RESERVERING.ID = @reserveringsID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reserveringsID", reserveringsID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountlist.Add(ReaderToAccountBARCODE(reader));
                        }
                    }
                }
            }
            return accountlist;
        }

        public List<Account> GetAllAccountsPresent()
        {
            List<Account> accountlist = new List<Account>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT DISTINCT account.ID, account.gebruikersnaam, account.email, ACCOUNT.activatiehash, ACCOUNT.geactiveerd, account.wachtwoord, ACCOUNT.telefoonnummer, reservering_polsbandje.aanwezig, POLSBANDJE.barcode, ACCOUNT.status FROM account JOIN reservering_polsbandje ON (reservering_polsbandje.account_id=account.ID) INNER JOIN POLSBANDJE ON (RESERVERING_POLSBANDJE.polsbandje_id=POLSBANDJE.ID) WHERE reservering_polsbandje.aanwezig = 1;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountlist.Add(ReaderToAccountBARCODE(reader));
                        }
                    }
                }
            }
            return accountlist;
        }

        public List<Account> GetAllAccountsPresentAtFestival(Event evenement)
        {
            List<Account> accountlist = new List<Account>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT DISTINCT account.ID, account.gebruikersnaam, account.email, ACCOUNT.activatiehash, ACCOUNT.geactiveerd, account.wachtwoord, ACCOUNT.telefoonnummer, reservering_polsbandje.aanwezig, POLSBANDJE.barcode, ACCOUNT.status FROM account JOIN reservering_polsbandje ON (reservering_polsbandje.account_id=account.ID) INNER JOIN POLSBANDJE ON (RESERVERING_POLSBANDJE.polsbandje_id=POLSBANDJE.ID) INNER JOIN RESERVERING ON (RESERVERING_POLSBANDJE.reservering_id=RESERVERING.ID) WHERE RESERVERING.eventID=@eventID AND RESERVERING_POLSBANDJE.aanwezig=1;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@eventID", evenement.ID1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountlist.Add(ReaderToAccountBARCODE(reader));
                        }
                    }
                }
            }
            return accountlist;
        }

        public Account GetCompleteAccountsByRRFID(string RFID)
        {
            Account account = null;

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT ACCOUNT.ID, ACCOUNT.gebruikersnaam, ACCOUNT.email, ACCOUNT.activatiehash, ACCOUNT.geactiveerd, ACCOUNT.wachtwoord, ACCOUNT.telefoonnummer, POLSBANDJE.barcode FROM ACCOUNT INNER JOIN RESERVERING_POLSBANDJE ON (ACCOUNT.ID=RESERVERING_POLSBANDJE.account_id) INNER JOIN RESERVERING ON (RESERVERING_POLSBANDJE.reservering_id=RESERVERING.ID) INNER JOIN POLSBANDJE ON (RESERVERING_POLSBANDJE.polsbandje_id=POLSBANDJE.ID) WHERE POLSBANDJE.barcode=@RFID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RFID", RFID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = ReaderToAccountBARCODE(reader);
                        }
                    }
                }
            }
            return account;
        }

        public Account GetCompleteAccountByEmailAndActivationhash(string email, string activatiehash)
        {
            Account account = null;

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT ACCOUNT.ID, ACCOUNT.gebruikersnaam, ACCOUNT.email, ACCOUNT.activatiehash, ACCOUNT.geactiveerd, ACCOUNT.wachtwoord, ACCOUNT.telefoonnummer, POLSBANDJE.barcode, ACCOUNT.status FROM ACCOUNT INNER JOIN RESERVERING_POLSBANDJE ON (ACCOUNT.ID=RESERVERING_POLSBANDJE.account_id) INNER JOIN RESERVERING ON (RESERVERING_POLSBANDJE.reservering_id=RESERVERING.ID) INNER JOIN POLSBANDJE ON (RESERVERING_POLSBANDJE.polsbandje_id=POLSBANDJE.ID) WHERE ACCOUNT.email=@email AND ACCOUNT.activatiehash=@activationHash;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@activationHash", activatiehash);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = ReaderToAccountBARCODE(reader);
                        }
                    }
                }
            }
            return account;
        }


        private Account ReaderToAccount(SqlDataReader reader)
        {
            return new Account(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["gebruikersnaam"]),
                Convert.ToString(reader["email"]),
                Convert.ToString(reader["activatiehash"]),
                Convert.ToBoolean(reader["geactiveerd"]),
                Convert.ToString(reader["wachtwoord"]),
                Convert.ToString(reader["telefoonnummer"]),
                Convert.ToBoolean(reader["status"])
            );

        }

        //Account whit barcode
        private Account ReaderToAccountBARCODE(SqlDataReader reader)
        {
            return new Account(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["gebruikersnaam"]),
                Convert.ToString(reader["email"]),
                Convert.ToString(reader["activatiehash"]),
                Convert.ToBoolean(reader["geactiveerd"]),
                Convert.ToString(reader["wachtwoord"]),
                Convert.ToString(reader["telefoonnummer"]),
                Convert.ToString(reader["barcode"])
            );

        }
    }
}