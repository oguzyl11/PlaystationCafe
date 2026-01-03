using System;
using System.Data;
using System.Data.SqlClient;
using GameCenterAI.DataAccess;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for report/accounting operations.
    /// </summary>
    public class SRapor : IRapor
    {
        /// <summary>
        /// Gets daily financial report.
        /// </summary>
        /// <param name="tarih">The date for the report.</param>
        /// <returns>A DataTable containing daily report data.</returns>
        public DataTable GunlukRapor(DateTime tarih)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT 
                    h.HareketID,
                    m.MasaAdi,
                    u.AdSoyad AS UyeAdi,
                    h.Baslangic,
                    h.Bitis,
                    h.Ucret AS KullanimUcreti,
                    h.SiparisToplami,
                    h.PesinAlinan,
                    (h.Ucret + h.SiparisToplami - h.PesinAlinan) AS Toplam
                FROM Hareketler h
                INNER JOIN Masalar m ON h.MasaID = m.MasaID
                LEFT JOIN Uyeler u ON h.UyeID = u.UyeID
                WHERE CAST(h.Baslangic AS DATE) = @Tarih AND h.Durum = 'Kapatıldı'
                ORDER BY h.Baslangic";

            command.Parameters.AddWithValue("@Tarih", tarih.Date);

            try
            {
                Tools.OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Günlük rapor oluşturulurken hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return dt;
        }

        /// <summary>
        /// Gets monthly financial report.
        /// </summary>
        /// <param name="yil">The year.</param>
        /// <param name="ay">The month.</param>
        /// <returns>A DataTable containing monthly report data.</returns>
        public DataTable AylikRapor(int yil, int ay)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT 
                    CAST(h.Baslangic AS DATE) AS Tarih,
                    COUNT(*) AS IslemSayisi,
                    SUM(h.Ucret) AS ToplamKullanimUcreti,
                    SUM(h.SiparisToplami) AS ToplamSiparis,
                    SUM(h.PesinAlinan) AS ToplamPesinAlinan,
                    SUM(h.Ucret + h.SiparisToplami - h.PesinAlinan) AS GunlukToplam
                FROM Hareketler h
                WHERE YEAR(h.Baslangic) = @Yil AND MONTH(h.Baslangic) = @Ay AND h.Durum = 'Kapatıldı'
                GROUP BY CAST(h.Baslangic AS DATE)
                ORDER BY Tarih";

            command.Parameters.AddWithValue("@Yil", yil);
            command.Parameters.AddWithValue("@Ay", ay);

            try
            {
                Tools.OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Aylık rapor oluşturulurken hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return dt;
        }

        /// <summary>
        /// Gets member-based report.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A DataTable containing member report data.</returns>
        public DataTable UyeRaporu(DateTime baslangic, DateTime bitis)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT 
                    u.AdSoyad AS UyeAdi,
                    COUNT(*) AS IslemSayisi,
                    SUM(h.Ucret) AS ToplamKullanimUcreti,
                    SUM(h.SiparisToplami) AS ToplamSiparis,
                    SUM(h.Ucret + h.SiparisToplami - h.PesinAlinan) AS Toplam
                FROM Hareketler h
                INNER JOIN Uyeler u ON h.UyeID = u.UyeID
                WHERE CAST(h.Baslangic AS DATE) BETWEEN @Baslangic AND @Bitis AND h.Durum = 'Kapatıldı'
                GROUP BY u.UyeID, u.AdSoyad
                ORDER BY Toplam DESC";

            command.Parameters.AddWithValue("@Baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@Bitis", bitis.Date);

            try
            {
                Tools.OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Üye raporu oluşturulurken hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return dt;
        }

        /// <summary>
        /// Gets table-based report.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A DataTable containing table report data.</returns>
        public DataTable MasaRaporu(DateTime baslangic, DateTime bitis)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT 
                    m.MasaAdi,
                    COUNT(*) AS IslemSayisi,
                    SUM(h.Ucret) AS ToplamKullanimUcreti,
                    SUM(h.SiparisToplami) AS ToplamSiparis,
                    SUM(h.Ucret + h.SiparisToplami - h.PesinAlinan) AS Toplam
                FROM Hareketler h
                INNER JOIN Masalar m ON h.MasaID = m.MasaID
                WHERE CAST(h.Baslangic AS DATE) BETWEEN @Baslangic AND @Bitis AND h.Durum = 'Kapatıldı'
                GROUP BY m.MasaID, m.MasaAdi
                ORDER BY Toplam DESC";

            command.Parameters.AddWithValue("@Baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@Bitis", bitis.Date);

            try
            {
                Tools.OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Masa raporu oluşturulurken hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return dt;
        }
    }
}

