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
    /// Service class for invoice operations.
    /// </summary>
    public class SFatura : IFatura
    {
        /// <summary>
        /// Creates a new invoice from a transaction.
        /// </summary>
        /// <param name="fatura">The invoice entity to create.</param>
        /// <returns>The created invoice ID.</returns>
        public int Olustur(Faturalar fatura)
        {
            int faturaID = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Faturalar (HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar)
                OUTPUT INSERTED.FaturaID
                VALUES (@HareketID, @FaturaNo, @FaturaTarihi, @ToplamTutar, @KdvOrani, @KdvTutari, @GenelToplam, @Durum, @Notlar)";

            command.Parameters.AddWithValue("@HareketID", fatura.HareketID);
            command.Parameters.AddWithValue("@FaturaNo", fatura.FaturaNo);
            command.Parameters.AddWithValue("@FaturaTarihi", fatura.FaturaTarihi);
            command.Parameters.AddWithValue("@ToplamTutar", fatura.ToplamTutar);
            command.Parameters.AddWithValue("@KdvOrani", fatura.KdvOrani);
            command.Parameters.AddWithValue("@KdvTutari", fatura.KdvTutari);
            command.Parameters.AddWithValue("@GenelToplam", fatura.GenelToplam);
            command.Parameters.AddWithValue("@Durum", fatura.Durum ?? "Aktif");
            command.Parameters.AddWithValue("@Notlar", fatura.Notlar ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    faturaID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return faturaID;
        }

        /// <summary>
        /// Gets an invoice by ID.
        /// </summary>
        /// <param name="faturaID">The invoice ID.</param>
        /// <returns>The invoice entity.</returns>
        public Faturalar Getir(int faturaID)
        {
            Faturalar fatura = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                WHERE FaturaID = @FaturaID";

            command.Parameters.AddWithValue("@FaturaID", faturaID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    fatura = new Faturalar
                    {
                        FaturaID = Convert.ToInt32(reader["FaturaID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        FaturaNo = reader["FaturaNo"].ToString(),
                        FaturaTarihi = Convert.ToDateTime(reader["FaturaTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        KdvOrani = Convert.ToDecimal(reader["KdvOrani"]),
                        KdvTutari = Convert.ToDecimal(reader["KdvTutari"]),
                        GenelToplam = Convert.ToDecimal(reader["GenelToplam"]),
                        Durum = reader["Durum"].ToString(),
                        Notlar = reader["Notlar"] != DBNull.Value ? reader["Notlar"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return fatura;
        }

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns>A list of all invoices.</returns>
        public List<Faturalar> Listele()
        {
            List<Faturalar> faturalar = new List<Faturalar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                ORDER BY FaturaTarihi DESC, FaturaID DESC";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Faturalar fatura = new Faturalar
                    {
                        FaturaID = Convert.ToInt32(reader["FaturaID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        FaturaNo = reader["FaturaNo"].ToString(),
                        FaturaTarihi = Convert.ToDateTime(reader["FaturaTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        KdvOrani = Convert.ToDecimal(reader["KdvOrani"]),
                        KdvTutari = Convert.ToDecimal(reader["KdvTutari"]),
                        GenelToplam = Convert.ToDecimal(reader["GenelToplam"]),
                        Durum = reader["Durum"].ToString(),
                        Notlar = reader["Notlar"] != DBNull.Value ? reader["Notlar"].ToString() : string.Empty
                    };
                    faturalar.Add(fatura);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return faturalar;
        }

        /// <summary>
        /// Gets invoices by date range.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A list of invoices in the date range.</returns>
        public List<Faturalar> TarihAraligindaGetir(DateTime baslangic, DateTime bitis)
        {
            List<Faturalar> faturalar = new List<Faturalar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                WHERE CAST(FaturaTarihi AS DATE) BETWEEN @Baslangic AND @Bitis
                ORDER BY FaturaTarihi DESC, FaturaID DESC";

            command.Parameters.AddWithValue("@Baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@Bitis", bitis.Date);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Faturalar fatura = new Faturalar
                    {
                        FaturaID = Convert.ToInt32(reader["FaturaID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        FaturaNo = reader["FaturaNo"].ToString(),
                        FaturaTarihi = Convert.ToDateTime(reader["FaturaTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        KdvOrani = Convert.ToDecimal(reader["KdvOrani"]),
                        KdvTutari = Convert.ToDecimal(reader["KdvTutari"]),
                        GenelToplam = Convert.ToDecimal(reader["GenelToplam"]),
                        Durum = reader["Durum"].ToString(),
                        Notlar = reader["Notlar"] != DBNull.Value ? reader["Notlar"].ToString() : string.Empty
                    };
                    faturalar.Add(fatura);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura tarih aralığı getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return faturalar;
        }

        /// <summary>
        /// Gets invoice by transaction ID.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The invoice entity if found, null otherwise.</returns>
        public Faturalar HareketIDyeGoreGetir(int hareketID)
        {
            Faturalar fatura = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    fatura = new Faturalar
                    {
                        FaturaID = Convert.ToInt32(reader["FaturaID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        FaturaNo = reader["FaturaNo"].ToString(),
                        FaturaTarihi = Convert.ToDateTime(reader["FaturaTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        KdvOrani = Convert.ToDecimal(reader["KdvOrani"]),
                        KdvTutari = Convert.ToDecimal(reader["KdvTutari"]),
                        GenelToplam = Convert.ToDecimal(reader["GenelToplam"]),
                        Durum = reader["Durum"].ToString(),
                        Notlar = reader["Notlar"] != DBNull.Value ? reader["Notlar"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura hareket ID'ye göre getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return fatura;
        }

        /// <summary>
        /// Generates a unique invoice number.
        /// </summary>
        /// <returns>A unique invoice number.</returns>
        public string FaturaNoOlustur()
        {
            string faturaNo = string.Empty;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT ISNULL(MAX(CAST(SUBSTRING(FaturaNo, 4, LEN(FaturaNo)) AS INT)), 0) + 1 AS SonNumara
                FROM Faturalar
                WHERE FaturaNo LIKE 'FAT%' AND YEAR(FaturaTarihi) = YEAR(GETDATE())";

            try
            {
                Tools.OpenConnection();
                object result = command.ExecuteScalar();
                int sonNumara = result != null ? Convert.ToInt32(result) : 1;
                faturaNo = $"FAT{DateTime.Now:yyyy}{sonNumara:D6}";
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura numarası oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return faturaNo;
        }

        /// <summary>
        /// Updates invoice status.
        /// </summary>
        /// <param name="faturaID">The invoice ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DurumGuncelle(int faturaID, string durum)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Faturalar SET Durum = @Durum WHERE FaturaID = @FaturaID";

            command.Parameters.AddWithValue("@FaturaID", faturaID);
            command.Parameters.AddWithValue("@Durum", durum);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura durum güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}

