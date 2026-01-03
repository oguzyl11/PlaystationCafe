using System;
using System.Data.SqlClient;

namespace GameCenterAI.DataAccess
{
    /// <summary>
    /// Provides database connection management using Singleton pattern.
    /// </summary>
    public class Tools
    {
        private static SqlConnection _connection;
        private static readonly object _lockObject = new object();
        private static string _connectionString;

        /// <summary>
        /// Gets the singleton SqlConnection instance.
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (_lockObject)
                    {
                        if (_connection == null)
                        {
                            _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=GameCenterDB;Integrated Security=True";
                            _connection = new SqlConnection(_connectionString);
                        }
                    }
                }
                return _connection;
            }
        }

        /// <summary>
        /// Opens the database connection if it is closed.
        /// </summary>
        public static void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// Closes the database connection if it is open.
        /// </summary>
        public static void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// Disposes the connection instance.
        /// </summary>
        public static void DisposeConnection()
        {
            if (_connection != null)
            {
                CloseConnection();
                _connection.Dispose();
                _connection = null;
            }
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        /// <returns>True if connection is successful, false otherwise.</returns>
        public static bool TestConnection()
        {
            try
            {
                OpenConnection();
                CloseConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


