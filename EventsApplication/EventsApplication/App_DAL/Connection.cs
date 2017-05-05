using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventsApplication.App_DAL
{
  internal class Connection
    {
        /// <summary>
        /// Returns an open sql connections
        /// </summary>
        public static SqlConnection SQLconnection
        {
            get
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-U9A7HHV\\SQLEXPRESS;Initial Catalog=EventsDatabase;Integrated Security=True");

                try
                {
                    conn.Open();
                }
                catch
                {
                    //MessageBox.Show("Geen verbinding met Database.");
                }
                return conn;
            }
        }
    }
}