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
        /// <param name="faturaId">The created invoice ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Olustur(Faturalar fatura, out int faturaId)
        {
            string hata = null;
            faturaId = 0;
            
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
                    faturaId = Convert.ToInt32(result);
                }
                else
                {
                    hata = "Fatura oluşturma işlemi başarısız oldu.";
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
        /// Gets an invoice by ID.
        /// </summary>
        /// <param name="faturaId">The invoice ID.</param>
        /// <param name="fatura">The invoice entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int faturaId, out Faturalar fatura)
        {
            string hata = null;
            fatura = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                WHERE FaturaID = @FaturaID";

            command.Parameters.AddWithValue("@FaturaID", faturaId);

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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <param name="faturalar">The list of all invoices.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Listele(out List<Faturalar> faturalar)
        {
            string hata = null;
            faturalar = new List<Faturalar>();
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets invoices by date range.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <param name="faturalar">The list of invoices in the date range.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string TarihAraligindaGetir(DateTime baslangic, DateTime bitis, out List<Faturalar> faturalar)
        {
            string hata = null;
            faturalar = new List<Faturalar>();
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets invoice by transaction ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="fatura">The invoice entity if found, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string HareketIDyeGoreGetir(int hareketId, out Faturalar fatura)
        {
            string hata = null;
            fatura = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT FaturaID, HareketID, FaturaNo, FaturaTarihi, ToplamTutar, KdvOrani, KdvTutari, GenelToplam, Durum, Notlar
                FROM Faturalar
                WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);

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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Generates a unique invoice number.
        /// </summary>
        /// <param name="faturaNo">A unique invoice number.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string FaturaNoOlustur(out string faturaNo)
        {
            string hata = null;
            faturaNo = string.Empty;
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Updates invoice status.
        /// </summary>
        /// <param name="faturaId">The invoice ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string DurumGuncelle(int faturaId, string durum)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Faturalar SET Durum = @Durum WHERE FaturaID = @FaturaID";

            command.Parameters.AddWithValue("@FaturaID", faturaId);
            command.Parameters.AddWithValue("@Durum", durum);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Fatura durum güncelleme işlemi başarısız oldu.";
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
    }
}
