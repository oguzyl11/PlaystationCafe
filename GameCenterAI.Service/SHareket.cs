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
        /// <param name="hareketId">The created transaction ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Baslat(Hareketler hareket, out int hareketId)
        {
            string hata = null;
            hareketId = 0;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Hareketler (UyeID, MasaID, TarifeID, OyunID, Baslangic, Ucret, PesinAlinan, SiparisToplami, Durum) OUTPUT INSERTED.HareketID VALUES (@UyeID, @MasaID, @TarifeID, @OyunID, @Baslangic, @Ucret, @PesinAlinan, @SiparisToplami, @Durum)";

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

            if (hareket.OyunID.HasValue)
            {
                command.Parameters.AddWithValue("@OyunID", hareket.OyunID.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@OyunID", DBNull.Value);
            }

            try
            {
                Tools.OpenConnection();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    hareketId = Convert.ToInt32(result);
                }
                else
                {
                    hata = "Hareket başlatma işlemi başarısız oldu.";
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
        /// Ends a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Bitir(int hareketId)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET Bitis = @Bitis, Durum = 'Kapatıldı' WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);
            command.Parameters.AddWithValue("@Bitis", DateTime.Now);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Hareket bitirme işlemi başarısız oldu.";
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
        /// Gets a transaction by ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="hareket">The transaction entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int hareketId, out Hareketler hareket)
        {
            string hata = null;
            hareket = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            
            // Temel kolonları seç (OyunID ve TarifeID opsiyonel)
            command.CommandText = "SELECT HareketID, UyeID, MasaID, Baslangic, Bitis, Ucret, PesinAlinan, SiparisToplami, Durum FROM Hareketler WHERE HareketID = @HareketID";
            command.Parameters.AddWithValue("@HareketID", hareketId);

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

                    if (reader["Bitis"] != DBNull.Value)
                    {
                        hareket.Bitis = Convert.ToDateTime(reader["Bitis"]);
                    }
                }

                reader.Close();
                
                // Opsiyonel kolonları ayrı sorgu ile oku (varsa)
                if (hareket != null)
                {
                    try
                    {
                        SqlCommand optCommand = new SqlCommand();
                        optCommand.Connection = Tools.Connection;
                        optCommand.CommandType = CommandType.Text;
                        optCommand.CommandText = "SELECT TarifeID, OyunID FROM Hareketler WHERE HareketID = @HareketID";
                        optCommand.Parameters.AddWithValue("@HareketID", hareketId);
                        
                        SqlDataReader optReader = optCommand.ExecuteReader();
                        if (optReader.Read())
                        {
                            if (optReader["TarifeID"] != DBNull.Value)
                            {
                                hareket.TarifeID = Convert.ToInt32(optReader["TarifeID"]);
                            }
                            if (optReader["OyunID"] != DBNull.Value)
                            {
                                hareket.OyunID = Convert.ToInt32(optReader["OyunID"]);
                            }
                        }
                        optReader.Close();
                    }
                    catch
                    {
                        // Opsiyonel kolonlar yoksa sessizce devam et
                    }
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
        /// Gets active transaction for a table.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="hareket">The active transaction entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetirAktifByMasaID(int masaId, out Hareketler hareket)
        {
            string hata = null;
            hareket = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            
            // Temel kolonları seç (OyunID ve TarifeID opsiyonel)
            command.CommandText = "SELECT HareketID, UyeID, MasaID, Baslangic, Bitis, Ucret, PesinAlinan, SiparisToplami, Durum FROM Hareketler WHERE MasaID = @MasaID AND Durum = 'Aktif'";
            command.Parameters.AddWithValue("@MasaID", masaId);

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

                    if (reader["Bitis"] != DBNull.Value)
                    {
                        hareket.Bitis = Convert.ToDateTime(reader["Bitis"]);
                    }
                }

                reader.Close();
                
                // Opsiyonel kolonları ayrı sorgu ile oku (varsa)
                if (hareket != null)
                {
                    try
                    {
                        SqlCommand optCommand = new SqlCommand();
                        optCommand.Connection = Tools.Connection;
                        optCommand.CommandType = CommandType.Text;
                        optCommand.CommandText = "SELECT TarifeID, OyunID FROM Hareketler WHERE HareketID = @HareketID";
                        optCommand.Parameters.AddWithValue("@HareketID", hareket.HareketID);
                        
                        SqlDataReader optReader = optCommand.ExecuteReader();
                        if (optReader.Read())
                        {
                            if (optReader["TarifeID"] != DBNull.Value)
                            {
                                hareket.TarifeID = Convert.ToInt32(optReader["TarifeID"]);
                            }
                            if (optReader["OyunID"] != DBNull.Value)
                            {
                                hareket.OyunID = Convert.ToInt32(optReader["OyunID"]);
                            }
                        }
                        optReader.Close();
                    }
                    catch
                    {
                        // Opsiyonel kolonlar yoksa sessizce devam et
                    }
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
        /// Calculates the fee for a transaction based on elapsed time.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="ucret">The calculated fee.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string UcretHesapla(int hareketId, out decimal ucret)
        {
            string hata = null;
            ucret = 0;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT h.HareketID, h.Baslangic, h.TarifeID, t.SaatlikUcret, m.SaatlikUcret as MasaSaatlikUcret
                FROM Hareketler h
                LEFT JOIN Tarifeler t ON h.TarifeID = t.TarifeID
                INNER JOIN Masalar m ON h.MasaID = m.MasaID
                WHERE h.HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);

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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets elapsed time for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="dakika">The elapsed time in minutes.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GecenSureGetir(int hareketId, out int dakika)
        {
            string hata = null;
            dakika = 0;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Baslangic FROM Hareketler WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);

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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Updates the order total for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="siparisToplami">The order total amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string SiparisToplamiGuncelle(int hareketId, decimal siparisToplami)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET SiparisToplami = SiparisToplami + @SiparisToplami WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);
            command.Parameters.AddWithValue("@SiparisToplami", siparisToplami);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Sipariş toplamı güncelleme işlemi başarısız oldu.";
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
        /// Updates the prepaid amount for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="pesinAlinan">The prepaid amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string PesinAlinanGuncelle(int hareketId, decimal pesinAlinan)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET PesinAlinan = @PesinAlinan WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);
            command.Parameters.AddWithValue("@PesinAlinan", pesinAlinan);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Peşin alınan güncelleme işlemi başarısız oldu.";
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
        /// Updates the tariff for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="tarifeId">The new tariff ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string TarifeGuncelle(int hareketId, int tarifeId)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET TarifeID = @TarifeID WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);
            command.Parameters.AddWithValue("@TarifeID", tarifeId);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Tarife güncelleme işlemi başarısız oldu.";
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
        /// Updates the game ID for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="oyunId">The game ID (nullable).</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string OyunGuncelle(int hareketId, int? oyunId)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Hareketler SET OyunID = @OyunID WHERE HareketID = @HareketID";

            command.Parameters.AddWithValue("@HareketID", hareketId);
            if (oyunId.HasValue)
            {
                command.Parameters.AddWithValue("@OyunID", oyunId.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@OyunID", DBNull.Value);
            }

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Oyun güncelleme işlemi başarısız oldu.";
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
