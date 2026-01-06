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
    /// Service class for table/desk (Masa) operations.
    /// </summary>
    public class SMasalar : IMasalar
    {
        /// <summary>
        /// Gets all tables from the database.
        /// </summary>
        /// <param name="masalar">The list of all tables.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetAllMasalar(out List<Masalar> masalar)
        {
            string hata = null;
            masalar = new List<Masalar>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT MasaID, MasaAdi, SaatlikUcret, Durum FROM Masalar ORDER BY MasaID";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Masalar masa = new Masalar
                    {
                        MasaID = Convert.ToInt32(reader["MasaID"]),
                        MasaAdi = reader["MasaAdi"].ToString(),
                        SaatlikUcret = Convert.ToDecimal(reader["SaatlikUcret"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    masalar.Add(masa);
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

        /// <summary>
        /// Updates the status of a table.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string DurumGuncelle(int masaId, string durum)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Masalar SET Durum = @Durum WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaId);
            command.Parameters.AddWithValue("@Durum", durum ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Masa durumu güncelleme işlemi başarısız oldu.";
                }
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

        /// <summary>
        /// Adds a new table to the database.
        /// </summary>
        /// <param name="masa">The table entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Ekle(Masalar masa)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Masalar (MasaAdi, SaatlikUcret, Durum) VALUES (@MasaAdi, @SaatlikUcret, @Durum)";

            command.Parameters.AddWithValue("@MasaAdi", masa.MasaAdi ?? string.Empty);
            command.Parameters.AddWithValue("@SaatlikUcret", masa.SaatlikUcret);
            command.Parameters.AddWithValue("@Durum", masa.Durum ?? "Boş");

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Masa ekleme işlemi başarısız oldu.";
                }
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

        /// <summary>
        /// Updates an existing table.
        /// </summary>
        /// <param name="masa">The table entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Guncelle(Masalar masa)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Masalar SET MasaAdi = @MasaAdi, SaatlikUcret = @SaatlikUcret, Durum = @Durum WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masa.MasaID);
            command.Parameters.AddWithValue("@MasaAdi", masa.MasaAdi ?? string.Empty);
            command.Parameters.AddWithValue("@SaatlikUcret", masa.SaatlikUcret);
            command.Parameters.AddWithValue("@Durum", masa.Durum ?? "Boş");

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Masa güncelleme işlemi başarısız oldu.";
                }
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

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <param name="masaId">The table ID to delete.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Sil(int masaId)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Masalar WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaId);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Masa silme işlemi başarısız oldu.";
                }
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

        /// <summary>
        /// Gets a table by ID.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="masa">The table entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int masaId, out Masalar masa)
        {
            string hata = null;
            masa = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT MasaID, MasaAdi, SaatlikUcret, Durum FROM Masalar WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    masa = new Masalar
                    {
                        MasaID = Convert.ToInt32(reader["MasaID"]),
                        MasaAdi = reader["MasaAdi"].ToString(),
                        SaatlikUcret = Convert.ToDecimal(reader["SaatlikUcret"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
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
