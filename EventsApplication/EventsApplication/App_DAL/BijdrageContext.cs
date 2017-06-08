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
                    "SELECT * FROM BIJDRAGE b " +
                    "LEFT JOIN CATEGORIE c on b.ID = c.bijdrage_id " +
                    "LEFT JOIN BESTAND be on b.ID = be.bijdrage_id " +
                    "LEFT JOIN BERICHT br on b.ID = br.bijdrage_id " +
                    "LEFT JOIN ACCOUNT a on b.account_id = a.ID " +
                    "LEFT JOIN ACCOUNT_BIJDRAGE ab on b.ID = ab.bijdrage_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bijdrage bijdrage = CreateBijdrageFromReader(reader);
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
                            Bijdrage bijdrage = CreateBijdrageFromReader(reader);
                            bijdrageList.Add(bijdrage);
                        }
                    }
                }
            }
            return bijdrageList;
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
                        command.Parameters.AddWithValue("@grootte", "");
                        command.Parameters.AddWithValue("@naam", "");
                        command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private Bijdrage CreateBijdrageFromReader(SqlDataReader reader)
        {
            AccountRepository accountRepository = new AccountRepository(new AccountContext());
            
            
            if (reader["soort"].ToString() == "categorie")
            {
                Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
                AccountBijdrage accountBijdrage;
                try
                {
                    accountBijdrage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
                }
                catch (Exception)
                {
                    accountBijdrage = new AccountBijdrage(0,0,0,0,0);
                }
                 return new Categorie(
                    Convert.ToInt32(reader["ID"]),
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
                    AccountBijdrage accountBijdrage;
                    try
                    {
                        accountBijdrage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
                    }
                    catch (Exception)
                    {
                        accountBijdrage = new AccountBijdrage(0, 0, 0, 0, 0);
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
                    AccountBijdrage accountBijdrage;
                    try
                    {
                        accountBijdrage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
                    }
                    catch (Exception)
                    {
                        accountBijdrage = new AccountBijdrage(0, 0, 0, 0, 0);
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

        private Bericht CreateBerichtFromReader(SqlDataReader reader)
        {
            AccountRepository accountRepository = new AccountRepository(new AccountContext());
            Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
            AccountBijdrage accountBijdrage;
            try
            {
                accountBijdrage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
            }
            catch (Exception)
            {
                accountBijdrage = new AccountBijdrage(0, 0, 0, 0, 0);
            }
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

    }
}