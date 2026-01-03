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
        /// <returns>The created order ID.</returns>
        public int Olustur(Siparisler siparis)
        {
            int siparisID = 0;
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
                    siparisID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return siparisID;
        }

        /// <summary>
        /// Gets orders by transaction ID.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>A list of orders.</returns>
        public List<Siparisler> GetirByHareketID(int hareketID)
        {
            List<Siparisler> siparisler = new List<Siparisler>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT SiparisID, HareketID, SiparisTarihi, ToplamTutar, Durum FROM Siparisler WHERE HareketID = @HareketID AND Durum = 'Aktif'";

            command.Parameters.AddWithValue("@HareketID", hareketID);

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
                throw new Exception("Sipariş listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return siparisler;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public List<Siparisler> Listele()
        {
            List<Siparisler> siparisler = new List<Siparisler>();
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
                throw new Exception("Sipariş listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return siparisler;
        }

        /// <summary>
        /// Gets order details by order ID.
        /// </summary>
        /// <param name="siparisID">The order ID.</param>
        /// <returns>A list of order details.</returns>
        public List<SiparisDetaylar> GetDetaylar(int siparisID)
        {
            List<SiparisDetaylar> detaylar = new List<SiparisDetaylar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT SiparisDetayID, SiparisID, UrunID, Adet, BirimFiyat, ToplamFiyat FROM SiparisDetaylar WHERE SiparisID = @SiparisID";

            command.Parameters.AddWithValue("@SiparisID", siparisID);

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
                throw new Exception("Sipariş detayları getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return detaylar;
        }

        /// <summary>
        /// Adds an order detail.
        /// </summary>
        /// <param name="detay">The order detail entity.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DetayEkle(SiparisDetaylar detay)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş detayı ekleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}

