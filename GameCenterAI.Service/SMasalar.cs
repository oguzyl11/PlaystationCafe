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
        /// <returns>A list of all tables.</returns>
        public List<Masalar> GetAllMasalar()
        {
            List<Masalar> masalar = new List<Masalar>();
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
                throw new Exception("Masalar yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return masalar;
        }

        /// <summary>
        /// Updates the status of a table.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DurumGuncelle(int masaID, string durum)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Masalar SET Durum = @Durum WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaID);
            command.Parameters.AddWithValue("@Durum", durum ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Masa durumu güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Adds a new table to the database.
        /// </summary>
        /// <param name="masa">The table entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Ekle(Masalar masa)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Masa ekleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Updates an existing table.
        /// </summary>
        /// <param name="masa">The table entity to update.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Guncelle(Masalar masa)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Masa güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <param name="masaID">The table ID to delete.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Sil(int masaID)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Masalar WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaID);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Masa silme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Gets a table by ID.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The table entity.</returns>
        public Masalar Getir(int masaID)
        {
            Masalar masa = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT MasaID, MasaAdi, SaatlikUcret, Durum FROM Masalar WHERE MasaID = @MasaID";

            command.Parameters.AddWithValue("@MasaID", masaID);

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
                throw new Exception("Masa getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return masa;
        }
    }
}

