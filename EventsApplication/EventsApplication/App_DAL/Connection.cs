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
                SqlConnection conn = new SqlConnection("Data Source = 192.168.20.22; Initial Catalog = Eyect4Events; User ID = dbi364892; Password = Password123");
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