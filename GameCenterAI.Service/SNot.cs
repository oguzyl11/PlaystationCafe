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
    /// Service class for note/appointment (Not) operations.
    /// </summary>
    public class SNot : INot
    {
        /// <summary>
        /// Gets notes by table ID.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>A list of notes.</returns>
        public List<Notlar> GetirByMasaID(int masaID)
        {
            List<Notlar> notlar = new List<Notlar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT NotID, MasaID, Tarih, Saat, Aciklama, Durum FROM Notlar WHERE MasaID = @MasaID AND Durum = 'Aktif' ORDER BY Tarih, Saat";

            command.Parameters.AddWithValue("@MasaID", masaID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Notlar not = new Notlar
                    {
                        NotID = Convert.ToInt32(reader["NotID"]),
                        MasaID = Convert.ToInt32(reader["MasaID"]),
                        Tarih = Convert.ToDateTime(reader["Tarih"]),
                        Saat = reader["Saat"] != DBNull.Value ? reader["Saat"].ToString() : string.Empty,
                        Aciklama = reader["Aciklama"] != DBNull.Value ? reader["Aciklama"].ToString() : string.Empty,
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    notlar.Add(not);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Not listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return notlar;
        }

        /// <summary>
        /// Gets notes by date.
        /// </summary>
        /// <param name="tarih">The date.</param>
        /// <returns>A list of notes.</returns>
        public List<Notlar> GetirByTarih(DateTime tarih)
        {
            List<Notlar> notlar = new List<Notlar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT NotID, MasaID, Tarih, Saat, Aciklama, Durum FROM Notlar WHERE CAST(Tarih AS DATE) = CAST(@Tarih AS DATE) AND Durum = 'Aktif' ORDER BY Saat";

            command.Parameters.AddWithValue("@Tarih", tarih);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Notlar not = new Notlar
                    {
                        NotID = Convert.ToInt32(reader["NotID"]),
                        MasaID = Convert.ToInt32(reader["MasaID"]),
                        Tarih = Convert.ToDateTime(reader["Tarih"]),
                        Saat = reader["Saat"] != DBNull.Value ? reader["Saat"].ToString() : string.Empty,
                        Aciklama = reader["Aciklama"] != DBNull.Value ? reader["Aciklama"].ToString() : string.Empty,
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    notlar.Add(not);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Not listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return notlar;
        }

        /// <summary>
        /// Adds a new note.
        /// </summary>
        /// <param name="not">The note entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Ekle(Notlar not)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Notlar (MasaID, Tarih, Saat, Aciklama, Durum) VALUES (@MasaID, @Tarih, @Saat, @Aciklama, @Durum)";

            command.Parameters.AddWithValue("@MasaID", not.MasaID);
            command.Parameters.AddWithValue("@Tarih", not.Tarih);
            command.Parameters.AddWithValue("@Saat", not.Saat ?? string.Empty);
            command.Parameters.AddWithValue("@Aciklama", not.Aciklama ?? string.Empty);
            command.Parameters.AddWithValue("@Durum", not.Durum ?? "Aktif");

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Not ekleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}

