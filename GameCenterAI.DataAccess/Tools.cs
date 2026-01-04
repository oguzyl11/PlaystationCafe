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
                            // Try multiple connection strings for compatibility
                            // First try default instance, then try SQLEXPRESS
                            _connectionString = GetConnectionString();
                            _connection = new SqlConnection(_connectionString);
                        }
                    }
                }
                return _connection;
            }
        }

        /// <summary>
        /// Gets the appropriate connection string by testing available SQL Server instances.
        /// </summary>
        /// <returns>A valid connection string.</returns>
        private static string GetConnectionString()
        {
            // Try different connection strings in order
            string[] connectionStrings = new string[]
            {
                "Data Source=.;Initial Catalog=GameCenterDB;Integrated Security=True;Connection Timeout=3",
                "Data Source=.\\SQLEXPRESS;Initial Catalog=GameCenterDB;Integrated Security=True;Connection Timeout=3",
                "Data Source=localhost;Initial Catalog=GameCenterDB;Integrated Security=True;Connection Timeout=3",
                "Data Source=localhost\\SQLEXPRESS;Initial Catalog=GameCenterDB;Integrated Security=True;Connection Timeout=3",
                "Server=.;Database=GameCenterDB;Integrated Security=True;Connection Timeout=3",
                "Server=.\\SQLEXPRESS;Database=GameCenterDB;Integrated Security=True;Connection Timeout=3"
            };

            foreach (string connStr in connectionStrings)
            {
                try
                {
                    using (SqlConnection testConn = new SqlConnection(connStr))
                    {
                        testConn.Open();
                        // Test if database exists by trying to query
                        using (SqlCommand testCmd = new SqlCommand("SELECT 1", testConn))
                        {
                            testCmd.ExecuteScalar();
                        }
                        testConn.Close();
                        return connStr; // This connection string works
                    }
                }
                catch (SqlException)
                {
                    // Try next connection string
                    continue;
                }
                catch
                {
                    // Try next connection string
                    continue;
                }
            }

            // If none work, return the default one (will show error when used)
            // User will see a more descriptive error message
            return "Data Source=.;Initial Catalog=GameCenterDB;Integrated Security=True;Connection Timeout=10";
        }

        /// <summary>
        /// Opens the database connection if it is closed.
        /// </summary>
        public static void OpenConnection()
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = "SQL Server bağlantı hatası:\n\n";
                errorMessage += $"Hata: {sqlEx.Message}\n\n";
                errorMessage += "Çözüm önerileri:\n";
                errorMessage += "1. SQL Server'ın çalıştığından emin olun (Services.msc)\n";
                errorMessage += "2. SQL Server Browser servisinin çalıştığından emin olun\n";
                errorMessage += "3. SQL Server instance adını kontrol edin (. veya .\\SQLEXPRESS)\n";
                errorMessage += "4. Windows Authentication kullanıldığından emin olun\n";
                errorMessage += "5. GameCenterDB veritabanının oluşturulduğundan emin olun";
                throw new Exception(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanı bağlantısı açılamadı: {ex.Message}", ex);
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


