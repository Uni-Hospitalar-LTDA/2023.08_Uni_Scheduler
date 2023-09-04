using _2023._08_Uni_Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public class Connection
    {
        private static readonly Connection iSQL = new Connection();

        public static Connection getInstancia()
        {
            return iSQL;
        }
        public SqlConnection getConnectionApp()
        {
            string conn = null;            

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder                
            ("Server=10.5.1.11;Database=UHCDB");
            builder.UserID = "sa";
            builder.Password = "vls021130";
            conn = builder.ConnectionString;            
            
            return new SqlConnection(conn);
        }
        public SqlConnection getConnectionApp(SchedulerApp_Connection connection)
        {
            string conn = null;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            ($"Server={connection.server};Database=MASTER");
            builder.UserID = connection.login;
            builder.Password = Cryptography.decrypt(connection.password);
            conn = builder.ConnectionString;

            return new SqlConnection(conn); 
        }
    }
}
