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
    /// Service class for tariff (Tarife) operations.
    /// </summary>
    public class STarife : ITarife
    {
        /// <summary>
        /// Gets all tariffs.
        /// </summary>
        /// <returns>A list of all tariffs.</returns>
        public List<Tarifeler> Listele()
        {
            List<Tarifeler> tarifeler = new List<Tarifeler>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TarifeID, TarifeAdi, SaatlikUcret, SureSiniri, Durum FROM Tarifeler WHERE Durum = 'Aktif' ORDER BY TarifeAdi";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tarifeler tarife = new Tarifeler
                    {
                        TarifeID = Convert.ToInt32(reader["TarifeID"]),
                        TarifeAdi = reader["TarifeAdi"].ToString(),
                        SaatlikUcret = Convert.ToDecimal(reader["SaatlikUcret"]),
                        SureSiniri = Convert.ToInt32(reader["SureSiniri"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    tarifeler.Add(tarife);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Tarife listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return tarifeler;
        }

        /// <summary>
        /// Gets a tariff by ID.
        /// </summary>
        /// <param name="tarifeID">The tariff ID.</param>
        /// <returns>The tariff entity.</returns>
        public Tarifeler Getir(int tarifeID)
        {
            Tarifeler tarife = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TarifeID, TarifeAdi, SaatlikUcret, SureSiniri, Durum FROM Tarifeler WHERE TarifeID = @TarifeID";

            command.Parameters.AddWithValue("@TarifeID", tarifeID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tarife = new Tarifeler
                    {
                        TarifeID = Convert.ToInt32(reader["TarifeID"]),
                        TarifeAdi = reader["TarifeAdi"].ToString(),
                        SaatlikUcret = Convert.ToDecimal(reader["SaatlikUcret"]),
                        SureSiniri = Convert.ToInt32(reader["SureSiniri"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Tarife getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return tarife;
        }
    }
}

