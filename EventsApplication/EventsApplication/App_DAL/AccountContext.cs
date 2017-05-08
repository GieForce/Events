using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                                Account account = new Account(userid, gebruikersnaam, email, activatiehash, geactiveerd);
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
                        "INSERT INTO Account (gebruikersnaam, email, activatiehash, geactiveerd) VALUES (@gebruikersnaam, @email, @activatiehash, @geactiveerd)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("gebruikersnaam", account.Gebruikersnaam);
                        command.Parameters.AddWithValue("@email", account.Email);
                        command.Parameters.AddWithValue("@activatiehash", account.Activatiehash);
                        command.Parameters.AddWithValue("@geactiveerd", account.Geactiveerd);
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

        //public bool Login(Account account, Evenement evenement)
        //{
        //    using (SqlConnection connection = Connection.SQLconnection)
        //    {
        //        string query = "SELECT * FROM account WHERE[account_email] = @param1 and[account_wachtwoord] = @param2 and [account_rol] = 2 or [account_rol] = 0  and eventID = @param3";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@param1", account.Email);
        //            command.Parameters.AddWithValue("@param2", account.Wachtwoord);
        //            command.Parameters.AddWithValue("@param3", evenement.Id);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    reader.Read();
        //                    return true;
        //                }
        //                return false;
        //            }
        //        }
        //    }
        //}

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
        

        private Account ReaderToAccount(SqlDataReader reader)
        {
            return new Account(
                Convert.ToInt32(reader["ID"]),
                Convert.ToString(reader["gebruikersnaam"]),
                Convert.ToString(reader["email"]),
                Convert.ToString(reader["activatiehash"]),
                Convert.ToBoolean(reader["geactiveerd"])
            );
        }
    }
}