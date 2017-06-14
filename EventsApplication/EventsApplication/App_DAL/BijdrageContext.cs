using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Controllers;
using EventsApplication.Models;
using EventsApplication.ViewModels;

namespace EventsApplication.App_DAL
{
    public class BijdrageContext : IBijdrageContext
    {
        //public Bijdrage GetById(int id)
        //{

        //}

        //public bool InsertBericht(Bijdrage bijdrage)
        //{

        //}

        //public bool Delete(int id)
        //{

        //}

        public Bijdrage GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bijdrage> GetAllBijdrages()
        {
            List<Bijdrage> bijdrageList = new List<Bijdrage>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "SELECT c.bijdrage_id, c.naam, b.*, be.*, br.*, a.*, ab.*, ab.account_id as abaccountid, ab.bijdrage_id as abbijdrageid, ab.ID abib FROM BIJDRAGE b " +
                    "LEFT JOIN CATEGORIE c on b.ID = c.bijdrage_id " +
                    "LEFT JOIN BESTAND be on b.ID = be.bijdrage_id " +
                    "LEFT JOIN BERICHT br on b.ID = br.bijdrage_id " +
                    "LEFT JOIN ACCOUNT a on b.account_id = a.ID " +
                    "LEFT JOIN ACCOUNT_BIJDRAGE ab on b.ID = ab.bijdrage_id " +
                    "ORDER BY datum DESC";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bijdrage bijdrage = CreateBijdrageFromReader(reader, connection);
                            bijdrageList.Add(bijdrage);
                        }
                    }
                }
            }
            return bijdrageList;
        }

        public List<Bericht> LoadBerichtenByPostId(int id)
        {
            List<Bericht> berichtenList = new List<Bericht>();
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "select * from BERICHT b " +
                    "left join BIJDRAGE_BERICHT bb on bb.bericht_id = b.bijdrage_id left join BIJDRAGE bd on b.bijdrage_id = bd.ID left join ACCOUNT_BIJDRAGE ab on b.bijdrage_id = ab.bijdrage_id where bb.bijdrage_id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bericht bericht = CreateBerichtFromReader(reader);
                            berichtenList.Add(bericht);
                        }
                    }
                }
                //foreach (Bericht item in berichtenList)
                //{
                //    string query2 = "select * from account_bijdrage where bijdrage_id = @id";
                //    using (SqlCommand command = new SqlCommand(query2, connection))
                //    {
                //        command.Parameters.AddWithValue("@id", item.Id);
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                AccountBijdrage accountbijdage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
                //                item.AccountBijdrage.Add(accountbijdage);
                //            }
                //        }
                //    }
                //}

            }
            return berichtenList;
        }

        public List<Bijdrage> GetAllBijdragesByUserId(int userid)
        {
            List<Bijdrage> bijdrageList = new List<Bijdrage>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query =
                    "SELECT * FROM BIJDRAGE b " +
                    "LEFT JOIN CATEGORIE c on b.ID = c.bijdrage_id " +
                    "LEFT JOIN BESTAND be on b.ID = be.bijdrage_id " +
                    "LEFT JOIN BERICHT br on b.ID = br.bijdrage_id " +
                    "LEFT JOIN ACCOUNT acc on b.account_id = acc.ID " +
                    "LEFT JOIN ACCOUNT_BIJDRAGE ab on b.ID = ab.bijdrage_id WHERE acc.ID = @userid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userid);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bijdrage bijdrage = CreateBijdrageFromReader(reader, connection);
                            bijdrageList.Add(bijdrage);
                        }
                    }
                }
            }
            return bijdrageList;
        }

        public bool InsertLike(AccountBijdrage accountBijdrage)
        {

            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "insertLike";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@accountID", accountBijdrage.AccountID);
                        command.Parameters.AddWithValue("@bijdrageID", accountBijdrage.BijdrageID);
                        command.Parameters.AddWithValue("@like", accountBijdrage.Like);
                        command.Parameters.AddWithValue("@ongewenst", accountBijdrage.Ongenwenst);

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

        public bool Insert(Bericht bericht)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO BERICHT (bijdrage_id, titel, inhoud) VALUES (@bijdrageID, @titel, @inhoud)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@bijdrageID", 8);
                        command.Parameters.AddWithValue("@titel", bericht.Titel);
                        command.Parameters.AddWithValue("@inhoud", bericht.Inhoud);

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

        public int getLatestBijdrageID()
        {
            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT TOP 1 MAX(ID) AS ID FROM BIJDRAGE";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            return id;
                        }
                    }
                    return 0;
                }
            }
        }

        public bool InsertMediaBericht(int categorieId, string bestandlocatie, int accountid)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "CreateNew";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.CommandType = CommandType.StoredProcedure;

                    
                        command.Parameters.AddWithValue("@accountID", accountid);
                        command.Parameters.AddWithValue("@datum", DateTime.Now);
                        command.Parameters.AddWithValue("@soort", "bestand");
                        command.Parameters.AddWithValue("@titel", "");
                        command.Parameters.AddWithValue("@inhoud", "");
                        command.Parameters.AddWithValue("@categorieID", categorieId);
                        command.Parameters.AddWithValue("@bestandslocatie", bestandlocatie);
                        command.Parameters.AddWithValue("@grootte", 0);
                        command.Parameters.AddWithValue("@naam", "");
                        command.Parameters.AddWithValue("@id", 0);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertComment(int id, int accountid, string text)
        {
            
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "CreateNew";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.CommandType = CommandType.StoredProcedure;

                    

                    command.Parameters.AddWithValue("@accountID", accountid);
                    command.Parameters.AddWithValue("@datum", DateTime.Now);
                    command.Parameters.AddWithValue("@soort", "bericht");
                    command.Parameters.AddWithValue("@titel", "");
                    command.Parameters.AddWithValue("@inhoud", text);
                    command.Parameters.AddWithValue("@categorieID", 1);
                    command.Parameters.AddWithValue("@bestandslocatie", "");
                    command.Parameters.AddWithValue("@grootte", 0);
                    command.Parameters.AddWithValue("@naam", "");
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Bijdrage CreateBijdrageFromReader(SqlDataReader reader, SqlConnection connection)
        {
            AccountRepository accountRepository = new AccountRepository(new AccountContext());
            
            
            if (reader["soort"].ToString() == "categorie")
            {
                Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
                List<AccountBijdrage> accountBijdrage = new List<AccountBijdrage>();

                try
                {
                    if (reader["abib"].ToString() != "")
                    {
                        string ab = reader["abib"].ToString();
                        string ab1 = reader["abaccountid"].ToString();
                        string ab2 = reader["abbijdrageid"].ToString();
                        string ab3 = reader["like"].ToString();
                        string ab4 = reader["ongewenst"].ToString();
                        AccountBijdrage accountBijdrage1 = new AccountBijdrage(Convert.ToInt32(ab), Convert.ToInt32(ab1),
                            Convert.ToInt32(ab2), Convert.ToInt32(ab3),
                            Convert.ToInt32(ab4));
                        accountBijdrage.Add(accountBijdrage1);
                    }
                }
                catch (Exception)
                {

                }

                return new Categorie(
                    Convert.ToInt32(reader["bijdrage_id"]),
                    account,
                    Convert.ToDateTime(reader["datum"]),
                    Convert.ToString(reader["soort"]),
                    accountBijdrage,
                    Convert.ToInt32(reader["categorie_id"] != DBNull.Value ? Convert.ToInt32(reader["categorie_id"]) : 0),
                    Convert.ToString(reader["naam"])
                );
            }
            else if (reader["soort"].ToString() == "bestand")
            {
                {
                    Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
                    List<AccountBijdrage> accountBijdrage = new List<AccountBijdrage>();

                    try
                    {
                        if (reader["abib"].ToString() != "")
                        {
                            string ab = reader["abib"].ToString();
                            string ab1 = reader["abaccountid"].ToString();
                            string ab2 = reader["abbijdrageid"].ToString();
                            string ab3 = reader["like"].ToString();
                            string ab4 = reader["ongewenst"].ToString();
                            AccountBijdrage accountBijdrage1 = new AccountBijdrage(Convert.ToInt32(ab), Convert.ToInt32(ab1),
                                Convert.ToInt32(ab2), Convert.ToInt32(ab3),
                                Convert.ToInt32(ab4));
                            accountBijdrage.Add(accountBijdrage1);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    return new Bestand(
                        Convert.ToInt32(reader["ID"]),
                        account,
                        Convert.ToDateTime(reader["datum"]),
                        Convert.ToString(reader["soort"]),
                        accountBijdrage,
                        Convert.ToInt32(reader["categorie_id"] != DBNull.Value ? Convert.ToInt32(reader["categorie_id"]) : 0),
                        Convert.ToString(reader["bestandslocatie"] != DBNull.Value ? Convert.ToString(reader["bestandslocatie"]) : ""),
                        Convert.ToInt32(reader["grootte"] != DBNull.Value ? Convert.ToInt32(reader["grootte"]) : 0)
                    );
                }
            }

            else if (reader["soort"].ToString() == "bericht")
            {
                {
                    Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
                    List<AccountBijdrage> accountBijdrage = new List<AccountBijdrage>();

                    try
                    {
                        if (reader["abib"].ToString() != "")
                        {
                            string ab = reader["abib"].ToString();
                            string ab1 = reader["abaccountid"].ToString();
                            string ab2 = reader["abbijdrageid"].ToString();
                            string ab3 = reader["like"].ToString();
                            string ab4 = reader["ongewenst"].ToString();
                            AccountBijdrage accountBijdrage1 = new AccountBijdrage(Convert.ToInt32(ab), Convert.ToInt32(ab1),
                                Convert.ToInt32(ab2), Convert.ToInt32(ab3),
                                Convert.ToInt32(ab4));
                            accountBijdrage.Add(accountBijdrage1);
                        }
                       
                    }
                    catch (Exception)
                    {
                        
                    }


                    return new Bericht(
                        Convert.ToInt32(reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0),
                        account,
                        Convert.ToDateTime(reader["datum"] ),
                        Convert.ToString(reader["soort"] != DBNull.Value ? Convert.ToString(reader["soort"]) : ""),
                        accountBijdrage,
                        Convert.ToString(reader["titel"] != DBNull.Value ? Convert.ToString(reader["titel"]) : ""),
                        Convert.ToString(reader["inhoud"] != DBNull.Value ? Convert.ToString(reader["inhoud"]) : "")
                    );
                }
            }
            else
            {
                return null;
            }
        }

        public bool DeletePost(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "deletePost";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@id", id);
                 
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
         }
        }

        private Bericht CreateBerichtFromReader(SqlDataReader reader)
        {
            AccountRepository accountRepository = new AccountRepository(new AccountContext());
            Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
            List<AccountBijdrage> accountBijdrage = new List<AccountBijdrage>();
            
            return new Bericht(
                Convert.ToInt32(reader["ID"]),
                account,
                Convert.ToDateTime(reader["datum"]),
                Convert.ToString(reader["soort"]),
                accountBijdrage,
                Convert.ToString(reader["titel"]),
                Convert.ToString(reader["inhoud"])
            );
        }

        private AccountBijdrage CreateAccountBijdrageFromReader(SqlDataReader reader)
        {
            return new AccountBijdrage(
                    Convert.ToInt32(reader["abib"]),
                    Convert.ToInt32(reader["abaccountid"]),
                    Convert.ToInt32(reader["abbijdrageid"]),
                    Convert.ToInt32(reader["like"]),
                    Convert.ToInt32("ongewenst"));
        }



    }
}