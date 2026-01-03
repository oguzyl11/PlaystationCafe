using System;
using System.Data;
using System.Data.SqlClient;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for transaction/movement (Hareket) operations.
    /// </summary>
    public class SHareket : IHareket
    {
        /// <summary>
        /// Starts a new transaction for a table.
        /// </summary>
        /// <param name="hareket">The transaction entity.</param>
        /// <returns>The created transaction ID.</returns>
        public int Baslat(Hareketler hareket)
        {
            int hareketID = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Hareketler (UyeID, MasaID, TarifeID, Baslangic, Ucret, PesinAlinan, SiparisToplami, Durum) OUTPUT INSERTED.HareketID VALUES (@UyeID, @MasaID, @TarifeID, @Baslangic, @Ucret, @PesinAlinan, @SiparisToplami, @Durum)";

            command.Parameters.AddWithValue("@UyeID", hareket.UyeID);
            command.Parameters.AddWithValue("@MasaID", hareket.MasaID);
            command.Parameters.AddWithValue("@Baslangic", hareket.Baslangic);
            command.Parameters.AddWithValue("@Ucret", hareket.Ucret);
            command.Parameters.AddWithValue("@PesinAlinan", hareket.PesinAlinan);
            command.Parameters.AddWithValue("@SiparisToplami", hareket.SiparisToplami);
            command.Parameters.AddWithValue("@Durum", hareket.Durum ?? "Aktif");

            if (hareket.TarifeID.HasValue)
            {
                command.Parameters.AddWithValue("@TarifeID", hareket.TarifeID.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@TarifeID", DBNull.Value);
            }

            try
            {
                Tools.OpenConnection();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    hareketID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hareket başlatma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hareketID;
        }

        /// <summary>
        /// Ends a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Bitir(int hareketID)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET Bitis = @Bitis, Durum = 'Kapatıldı' WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);
            command.Parameters.AddWithValue("@Bitis", DateTime.Now);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Hareket bitirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Gets active transaction for a table.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The active transaction entity.</returns>
        public Hareketler GetirAktifByMasaID(int masaID)
        {
            Hareketler hareket = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT HareketID, UyeID, MasaID, TarifeID, Baslangic, Bitis, Ucret, PesinAlinan, SiparisToplami, Durum FROM Hareketler WHERE MasaID = @MasaID AND Durum = 'Aktif'";

            command.Parameters.AddWithValue("@MasaID", masaID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    hareket = new Hareketler
                    {
                        HareketID = Convert.ToInt32(reader["HareketID"]),
                        UyeID = Convert.ToInt32(reader["UyeID"]),
                        MasaID = Convert.ToInt32(reader["MasaID"]),
                        Baslangic = Convert.ToDateTime(reader["Baslangic"]),
                        Ucret = Convert.ToDecimal(reader["Ucret"]),
                        PesinAlinan = Convert.ToDecimal(reader["PesinAlinan"]),
                        SiparisToplami = Convert.ToDecimal(reader["SiparisToplami"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };

                    if (reader["TarifeID"] != DBNull.Value)
                    {
                        hareket.TarifeID = Convert.ToInt32(reader["TarifeID"]);
                    }

                    if (reader["Bitis"] != DBNull.Value)
                    {
                        hareket.Bitis = Convert.ToDateTime(reader["Bitis"]);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Hareket getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hareket;
        }

        /// <summary>
        /// Calculates the fee for a transaction based on elapsed time.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The calculated fee.</returns>
        public decimal UcretHesapla(int hareketID)
        {
            decimal ucret = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT h.HareketID, h.Baslangic, h.TarifeID, t.SaatlikUcret, m.SaatlikUcret as MasaSaatlikUcret
                FROM Hareketler h
                LEFT JOIN Tarifeler t ON h.TarifeID = t.TarifeID
                INNER JOIN Masalar m ON h.MasaID = m.MasaID
                WHERE h.HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DateTime baslangic = Convert.ToDateTime(reader["Baslangic"]);
                    TimeSpan gecenSure = DateTime.Now - baslangic;
                    double saat = gecenSure.TotalHours;

                    decimal saatlikUcret = 0;
                    if (reader["TarifeID"] != DBNull.Value && reader["SaatlikUcret"] != DBNull.Value)
                    {
                        saatlikUcret = Convert.ToDecimal(reader["SaatlikUcret"]);
                    }
                    else
                    {
                        saatlikUcret = Convert.ToDecimal(reader["MasaSaatlikUcret"]);
                    }

                    ucret = (decimal)saat * saatlikUcret;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ücret hesaplama işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return ucret;
        }

        /// <summary>
        /// Gets elapsed time for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The elapsed time in minutes.</returns>
        public int GecenSureGetir(int hareketID)
        {
            int dakika = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Baslangic FROM Hareketler WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DateTime baslangic = Convert.ToDateTime(reader["Baslangic"]);
                    TimeSpan gecenSure = DateTime.Now - baslangic;
                    dakika = (int)gecenSure.TotalMinutes;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Geçen süre hesaplama işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return dakika;
        }

        /// <summary>
        /// Updates the order total for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="siparisToplami">The order total amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool SiparisToplamiGuncelle(int hareketID, decimal siparisToplami)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET SiparisToplami = SiparisToplami + @SiparisToplami WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);
            command.Parameters.AddWithValue("@SiparisToplami", siparisToplami);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş toplamı güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Updates the prepaid amount for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="pesinAlinan">The prepaid amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool PesinAlinanGuncelle(int hareketID, decimal pesinAlinan)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET PesinAlinan = @PesinAlinan WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);
            command.Parameters.AddWithValue("@PesinAlinan", pesinAlinan);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Peşin alınan güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Updates the tariff for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="tarifeID">The new tariff ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool TarifeGuncelle(int hareketID, int tarifeID)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET TarifeID = @TarifeID WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketID);
            command.Parameters.AddWithValue("@TarifeID", tarifeID);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Tarife güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}

