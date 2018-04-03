using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Monster_Crater.DAL
{
    public static class Database
    {
        //To use the database connect to infralab vpn with your i account.
        //For SQL Server manager
        //servername: sqlserver.fhict.local
        //username : User
        //password: PTS07
        public const string _connectionString = "Server=mssql.fhict.local;Database=dbi357119;User Id=dbi357119;Password=Beamen12;";
        //public const string _connectionString = "Data Source=sqlserver.fhict.local;User ID=sa;Password=PTS07";

        public static SqlCommand command;

        public static SqlConnection connection;

        /// <summary>
        /// Creates a new database connection and directly opens it. The caller
        /// is responsible for properly closing the connection.
        /// </summary>
        public static SqlConnection OpenConnection()
        {
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {

                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");
                    case 53:
                        throw new Exception("Cannot connect to server.");
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                    case 4060:
                        //wrong username or password in connectionstring
                        throw new Exception("Cannot contact the database. Access has been denied. Contact administrator");

                    default:
                        MessageBox.Show("Got error code: " + ex);
                        break;

                }

                return connection;

            }
        }

        public static bool _CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}