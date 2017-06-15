using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsApplication.App_DAL.Interfaces;
using EventsApplication.Controllers;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class CategorieContext : ICategorieContext
    {
        public List<Categorie> GetByBijdrageID(int id)
        {
            List<Categorie> categorielist = new List<Categorie>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "SELECT * FROM categorie WHERE bijdrage_id = @param1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@param1",id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorielist.Add(ReaderToCategorie(reader));
                        }
                    }
                }
            }
            return categorielist;
        }

        public List<Categorie> CategorieList()
        {
            List<Categorie> categorielist = new List<Categorie>();

            using (SqlConnection connection = Connection.SQLconnection)
            {
                string query = "select * from CATEGORIE c left join BIJDRAGE b on c.bijdrage_id = b.ID left join BIJDRAGE_BERICHT bb on b.ID = bb.bijdrage_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {             

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorielist.Add(ReaderToCategorie(reader));
                        }
                    }
                }
            }
            return categorielist;
        }

        public bool Insert(Categorie categorie)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "INSERT INTO Categorie (bijdrage_id, categorie_id, naam) VALUES ((select max(bijdrage_id), @param2, @param3)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                   //     command.Parameters.AddWithValue("@param1", categorie.Id);
                        command.Parameters.AddWithValue("@param2", categorie.CategorieId);
                        command.Parameters.AddWithValue("@param3", categorie.Naam);
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

        private Categorie ReaderToCategorie(SqlDataReader reader)
        {
            AccountRepository accountRepository = new AccountRepository(new AccountContext());
            Account account = accountRepository.GetById(Convert.ToInt32(reader["account_id"]));
            List<AccountBijdrage> accountBijdrage = new List<AccountBijdrage>();
            //try
            //{
            //    accountBijdrage = new AccountBijdrage(Convert.ToInt32(reader["account_id"]), Convert.ToInt32(reader["bijdrage_id"]), Convert.ToInt32(reader["like"]), Convert.ToInt32("ongewenst"));
            //}
            //catch (Exception)
            //{
            //    accountBijdrage = new AccountBijdrage(0, 0, 0, 0, 0);
            //}
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
    }
}
