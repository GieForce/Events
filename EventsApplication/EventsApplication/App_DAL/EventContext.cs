using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class EventContext:IEventContext
    {
        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
                
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "SELECT * FROM EVENT";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int eventid = Convert.ToInt32(reader["ID"]);
                                int locatie = Convert.ToInt32(reader["locatie_id"]);
                                string naam = (string)reader["naam"];
                                DateTime start = Convert.ToDateTime(reader["datumstart"]);
                                DateTime einde = Convert.ToDateTime(reader["datumEinde"]);
                                int bezoekers = Convert.ToInt32(reader["maxBezoekers"]);
                                events.Add(new Event(eventid, naam, start, einde, bezoekers));
                               
                            }
                            
                        }
                    }
                }
                return events;
            }
            catch
            {
                return null;
            }
        }
        public bool Insert(Event eventi)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "INSERT INTO EVENT (naam, locatie_id, datumstart, datumEinde, maxBezoekers) VALUES (@naam, @locatieid, @datumstart, @datumeind, @maxbezoekers)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@naam", eventi.Naam);
                        command.Parameters.AddWithValue("@locatieid", eventi.Locatieid);
                        command.Parameters.AddWithValue("@datumstart", eventi.Datumstart);
                        command.Parameters.AddWithValue("@datumeind", eventi.Datumeind);
                        command.Parameters.AddWithValue("@maxbezoekers", eventi.Maxbezoekers);
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

        public bool Delete(int id)
        {
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query =
                        "DELETE FROM EVENT WHERE ID = @id";
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
    }

    
}