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
    /// Service class for order (Siparis) operations.
    /// </summary>
    public class SSiparis : ISiparis
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="siparis">The order entity to create.</param>
        /// <param name="siparisId">The created order ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Olustur(Siparisler siparis, out int siparisId)
        {
            string hata = null;
            siparisId = 0;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Siparisler (HareketID, SiparisTarihi, ToplamTutar, Durum) OUTPUT INSERTED.SiparisID VALUES (@HareketID, @SiparisTarihi, @ToplamTutar, @Durum)";

            command.Parameters.AddWithValue("@HareketID", siparis.HareketID);
            command.Parameters.AddWithValue("@SiparisTarihi", siparis.SiparisTarihi);
            command.Parameters.AddWithValue("@ToplamTutar", siparis.ToplamTutar);
            command.Parameters.AddWithValue("@Durum", siparis.Durum ?? "Aktif");

            try
            {
                Tools.OpenConnection();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    siparisId = Convert.ToInt32(result);
                }
                else
                {
                    hata = "Sipariş oluşturma işlemi başarısız oldu.";
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
        /// Gets orders by transaction ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="siparisler">The list of orders.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetirByHareketID(int hareketId, out List<Siparisler> siparisler)
        {
            string hata = null;
            siparisler = new List<Siparisler>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT SiparisID, HareketID, SiparisTarihi, ToplamTutar, Durum FROM Siparisler WHERE HareketID = @HareketID AND Durum = 'Aktif'";

            command.Parameters.AddWithValue("@HareketID", hareketId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Siparisler siparis = new Siparisler
                    {
                        SiparisID = Convert.ToInt32(reader["SiparisID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        SiparisTarihi = Convert.ToDateTime(reader["SiparisTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    siparisler.Add(siparis);
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
        /// Gets all orders.
        /// </summary>
        /// <param name="siparisler">The list of all orders.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Listele(out List<Siparisler> siparisler)
        {
            string hata = null;
            siparisler = new List<Siparisler>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT SiparisID, HareketID, SiparisTarihi, ToplamTutar, Durum FROM Siparisler WHERE Durum = 'Aktif' ORDER BY SiparisTarihi DESC";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Siparisler siparis = new Siparisler
                    {
                        SiparisID = Convert.ToInt32(reader["SiparisID"]),
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        SiparisTarihi = Convert.ToDateTime(reader["SiparisTarihi"]),
                        ToplamTutar = Convert.ToDecimal(reader["ToplamTutar"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    siparisler.Add(siparis);
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
        /// Gets order details by order ID.
        /// </summary>
        /// <param name="siparisId">The order ID.</param>
        /// <param name="detaylar">The list of order details.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetDetaylar(int siparisId, out List<SiparisDetaylar> detaylar)
        {
            string hata = null;
            detaylar = new List<SiparisDetaylar>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT SiparisDetayID, SiparisID, UrunID, Adet, BirimFiyat, ToplamFiyat FROM SiparisDetaylar WHERE SiparisID = @SiparisID";

            command.Parameters.AddWithValue("@SiparisID", siparisId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SiparisDetaylar detay = new SiparisDetaylar
                    {
                        SiparisDetayID = Convert.ToInt32(reader["SiparisDetayID"]),
                        SiparisID = Convert.ToInt32(reader["SiparisID"]),
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        Adet = Convert.ToInt32(reader["Adet"]),
                        BirimFiyat = Convert.ToDecimal(reader["BirimFiyat"]),
                        ToplamFiyat = Convert.ToDecimal(reader["ToplamFiyat"])
                    };
                    detaylar.Add(detay);
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
        /// Adds an order detail.
        /// </summary>
        /// <param name="detay">The order detail entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string DetayEkle(SiparisDetaylar detay)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO SiparisDetaylar (SiparisID, UrunID, Adet, BirimFiyat, ToplamFiyat) VALUES (@SiparisID, @UrunID, @Adet, @BirimFiyat, @ToplamFiyat)";

            command.Parameters.AddWithValue("@SiparisID", detay.SiparisID);
            command.Parameters.AddWithValue("@UrunID", detay.UrunID);
            command.Parameters.AddWithValue("@Adet", detay.Adet);
            command.Parameters.AddWithValue("@BirimFiyat", detay.BirimFiyat);
            command.Parameters.AddWithValue("@ToplamFiyat", detay.ToplamFiyat);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Sipariş detayı ekleme işlemi başarısız oldu.";
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
