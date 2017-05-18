using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EventsApplication.Models;

namespace EventsApplication.App_DAL
{
    public class EventContext : IEventContext
    {
        public List<Event> GetAll()
        {
            List<Event> evenementen = new List<Event>();
            try
            {
                using (SqlConnection connection = Connection.SQLconnection)
                {
                    string query = "SELECT * FROM event";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evenementen.Add(ReaderToEvent(reader));
                        }
                        connection.Close();
                    }
                    return evenementen;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Event ReaderToEvent(SqlDataReader reader)
        {
            return new Event(
                Convert.ToInt32(reader["id"]),
                Convert.ToInt32(reader["locatie_id"]),
                Convert.ToString(reader["naam"]),
                Convert.ToDateTime(reader["datumStart"]),
                Convert.ToDateTime(reader["datumEinde"]),
                Convert.ToInt32(reader["maxBezoekers"])
                );
        }
    }
}