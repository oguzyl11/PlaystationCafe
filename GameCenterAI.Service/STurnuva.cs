using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for tournament operations.
    /// </summary>
    public class STurnuva : ITurnuva
    {
        /// <summary>
        /// Creates a new tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to create.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Olustur(Turnuvalar turnuva)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Turnuvalar (TurnuvaAdi, BaslangicTarihi, Odul, Durum) VALUES (@TurnuvaAdi, @BaslangicTarihi, @Odul, @Durum)";

            command.Parameters.AddWithValue("@TurnuvaAdi", turnuva.TurnuvaAdi);
            command.Parameters.AddWithValue("@BaslangicTarihi", turnuva.BaslangicTarihi);
            command.Parameters.AddWithValue("@Odul", turnuva.Odul);
            command.Parameters.AddWithValue("@Durum", turnuva.Durum ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Turnuva oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Lists all tournaments.
        /// </summary>
        /// <returns>A list of all tournaments.</returns>
        public List<Turnuvalar> Listele()
        {
            List<Turnuvalar> turnuvalar = new List<Turnuvalar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TurnuvaID, TurnuvaAdi, BaslangicTarihi, Odul, Durum FROM Turnuvalar ORDER BY BaslangicTarihi DESC";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Turnuvalar turnuva = new Turnuvalar
                    {
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        TurnuvaAdi = reader["TurnuvaAdi"].ToString(),
                        BaslangicTarihi = Convert.ToDateTime(reader["BaslangicTarihi"]),
                        Odul = Convert.ToDecimal(reader["Odul"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    turnuvalar.Add(turnuva);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Turnuva listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return turnuvalar;
        }

        /// <summary>
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random users and creates pairs for Quarter Finals.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of paired user IDs (each pair represents a match).</returns>
        public List<KeyValuePair<int, int>> EslestirmeleriOlustur(int turnuvaID)
        {
            List<KeyValuePair<int, int>> eslestirmeler = new List<KeyValuePair<int, int>>();
            List<int> uyeIDler = new List<int>();

            try
            {
                // Fetch random active users (8 users for Quarter Finals)
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    SELECT TOP 8 UyeID 
                    FROM Uyeler 
                    WHERE Durum = 1 
                    ORDER BY NEWID()";

                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    uyeIDler.Add(Convert.ToInt32(reader["UyeID"]));
                }

                reader.Close();

                // Create pairs (Quarter Finals = 4 matches)
                if (uyeIDler.Count >= 2)
                {
                    // Ensure we have even number of users
                    if (uyeIDler.Count % 2 != 0)
                    {
                        uyeIDler.RemoveAt(uyeIDler.Count - 1);
                    }

                    // Pair users
                    for (int i = 0; i < uyeIDler.Count; i += 2)
                    {
                        if (i + 1 < uyeIDler.Count)
                        {
                            eslestirmeler.Add(new KeyValuePair<int, int>(uyeIDler[i], uyeIDler[i + 1]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eşleştirme oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return eslestirmeler;
        }
    }
}
