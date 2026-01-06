using System;
using System.Data;
using System.Data.SqlClient;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for game (Oyun) operations.
    /// </summary>
    public class SOyunlar : IOyunlar
    {
        /// <summary>
        /// Gets a game by ID.
        /// </summary>
        /// <param name="oyunId">The game ID.</param>
        /// <param name="oyun">The game entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int oyunId, out Oyunlar oyun)
        {
            string hata = null;
            oyun = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT OyunID, OyunAdi, Kategori, Platform FROM Oyunlar WHERE OyunID = @OyunID";

            command.Parameters.AddWithValue("@OyunID", oyunId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    oyun = new Oyunlar
                    {
                        OyunID = Convert.ToInt32(reader["OyunID"]),
                        OyunAdi = reader["OyunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Platform = reader["Platform"] != DBNull.Value ? reader["Platform"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }
    }
}

