using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for tournament operations.
    /// </summary>
    public class STurnuva : ITurnuva
    {
        /// <summary>
        /// Creates a new tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to create.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Olustur(Turnuvalar turnuva)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Turnuvalar (TurnuvaAdi, BaslangicTarihi, Odul, Durum) VALUES (@TurnuvaAdi, @BaslangicTarihi, @Odul, @Durum)";

            command.Parameters.AddWithValue("@TurnuvaAdi", turnuva.TurnuvaAdi);
            command.Parameters.AddWithValue("@BaslangicTarihi", turnuva.BaslangicTarihi);
            command.Parameters.AddWithValue("@Odul", turnuva.Odul);
            command.Parameters.AddWithValue("@Durum", turnuva.Durum ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Turnuva oluşturma işlemi başarısız oldu.";
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
        /// Lists all tournaments.
        /// </summary>
        /// <param name="turnuvalar">The list of all tournaments.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Listele(out List<Turnuvalar> turnuvalar)
        {
            string hata = null;
            turnuvalar = new List<Turnuvalar>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TurnuvaID, TurnuvaAdi, BaslangicTarihi, Odul, Durum FROM Turnuvalar ORDER BY BaslangicTarihi DESC";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Turnuvalar turnuva = new Turnuvalar
                    {
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        TurnuvaAdi = reader["TurnuvaAdi"].ToString(),
                        BaslangicTarihi = Convert.ToDateTime(reader["BaslangicTarihi"]),
                        Odul = Convert.ToDecimal(reader["Odul"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    turnuvalar.Add(turnuva);
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
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random active users and creates pairs based on user count.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="eslestirmeler">A list of paired user IDs (each pair represents a match).</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string EslestirmeleriOlustur(int turnuvaId, out List<KeyValuePair<int, int>> eslestirmeler)
        {
            string hata = null;
            eslestirmeler = new List<KeyValuePair<int, int>>();
            List<int> uyeIDler = new List<int>();

            try
            {
                // Fetch all active users
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    SELECT UyeID 
                    FROM Uyeler 
                    WHERE Durum = 1 
                    ORDER BY NEWID()";

                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    uyeIDler.Add(Convert.ToInt32(reader["UyeID"]));
                }

                reader.Close();

                // Minimum 2 kişi gerekiyor
                if (uyeIDler.Count < 2)
                {
                    return "Turnuva için en az 2 aktif üye gereklidir.";
                }

                // Çift sayıda üye olması gerekiyor (eşleştirme için)
                if (uyeIDler.Count % 2 != 0)
                {
                    uyeIDler.RemoveAt(uyeIDler.Count - 1);
                }

                // Pair users
                for (int i = 0; i < uyeIDler.Count; i += 2)
                {
                    if (i + 1 < uyeIDler.Count)
                    {
                        eslestirmeler.Add(new KeyValuePair<int, int>(uyeIDler[i], uyeIDler[i + 1]));
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
        /// Creates tournament matches in database from pairings.
        /// Dynamically determines the starting round based on the number of matches.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="eslestirmeler">List of pairings.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string MaclariOlustur(int turnuvaId, List<KeyValuePair<int, int>> eslestirmeler)
        {
            string hata = null;
            
            try
            {
                Tools.OpenConnection();
                int macNo = 1;
                int macSayisi = eslestirmeler != null ? eslestirmeler.Count : 0;
                
                // Maç sayısına göre tur belirle
                string tur = "";
                if (macSayisi == 1)
                {
                    tur = "Final";
                }
                else if (macSayisi == 2)
                {
                    tur = "Yarı Final";
                }
                else if (macSayisi == 4)
                {
                    tur = "Çeyrek Final";
                }
                else if (macSayisi == 8)
                {
                    tur = "Son 16";
                }
                else if (macSayisi == 16)
                {
                    tur = "Son 32";
                }
                else
                {
                    // Varsayılan olarak "İlk Tur" kullan
                    tur = "İlk Tur";
                }

                foreach (var eslestirme in eslestirmeler)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = Tools.Connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                        INSERT INTO TurnuvaMaclari (TurnuvaID, Uye1ID, Uye2ID, Tur, MacNo, Durum)
                        VALUES (@TurnuvaID, @Uye1ID, @Uye2ID, @Tur, @MacNo, 'Beklemede')";

                    command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);
                    command.Parameters.AddWithValue("@Uye1ID", eslestirme.Key);
                    command.Parameters.AddWithValue("@Uye2ID", eslestirme.Value);
                    command.Parameters.AddWithValue("@Tur", tur);
                    command.Parameters.AddWithValue("@MacNo", macNo);

                    command.ExecuteNonQuery();
                    macNo++;
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
        /// Gets a tournament by ID.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="turnuva">The tournament entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int turnuvaId, out Turnuvalar turnuva)
        {
            string hata = null;
            turnuva = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TurnuvaID, TurnuvaAdi, BaslangicTarihi, Odul, Durum FROM Turnuvalar WHERE TurnuvaID = @TurnuvaID";

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    turnuva = new Turnuvalar
                    {
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        TurnuvaAdi = reader["TurnuvaAdi"].ToString(),
                        BaslangicTarihi = Convert.ToDateTime(reader["BaslangicTarihi"]),
                        Odul = Convert.ToDecimal(reader["Odul"]),
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

        /// <summary>
        /// Updates an existing tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Guncelle(Turnuvalar turnuva)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Turnuvalar SET TurnuvaAdi = @TurnuvaAdi, BaslangicTarihi = @BaslangicTarihi, Odul = @Odul, Durum = @Durum WHERE TurnuvaID = @TurnuvaID";

            command.Parameters.AddWithValue("@TurnuvaID", turnuva.TurnuvaID);
            command.Parameters.AddWithValue("@TurnuvaAdi", turnuva.TurnuvaAdi ?? string.Empty);
            command.Parameters.AddWithValue("@BaslangicTarihi", turnuva.BaslangicTarihi);
            command.Parameters.AddWithValue("@Odul", turnuva.Odul);
            command.Parameters.AddWithValue("@Durum", turnuva.Durum ?? string.Empty);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Turnuva güncelleme işlemi başarısız oldu.";
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
        /// Deletes a tournament.
        /// First deletes all related matches, then deletes the tournament.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID to delete.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Sil(int turnuvaId)
        {
            string hata = null;

            try
            {
                Tools.OpenConnection();

                // First, delete all matches related to this tournament
                SqlCommand deleteMaclarCommand = new SqlCommand();
                deleteMaclarCommand.Connection = Tools.Connection;
                deleteMaclarCommand.CommandType = CommandType.Text;
                deleteMaclarCommand.CommandText = "DELETE FROM TurnuvaMaclari WHERE TurnuvaID = @TurnuvaID";
                deleteMaclarCommand.Parameters.AddWithValue("@TurnuvaID", turnuvaId);
                deleteMaclarCommand.ExecuteNonQuery();

                // Then, delete the tournament
                SqlCommand deleteTurnuvaCommand = new SqlCommand();
                deleteTurnuvaCommand.Connection = Tools.Connection;
                deleteTurnuvaCommand.CommandType = CommandType.Text;
                deleteTurnuvaCommand.CommandText = "DELETE FROM Turnuvalar WHERE TurnuvaID = @TurnuvaID";
                deleteTurnuvaCommand.Parameters.AddWithValue("@TurnuvaID", turnuvaId);

                int affectedRows = deleteTurnuvaCommand.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Turnuva silme işlemi başarısız oldu.";
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
        /// Deletes tournament matches by tournament ID.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string TurnuvaMaclariSil(int turnuvaId)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM TurnuvaMaclari WHERE TurnuvaID = @TurnuvaID";
            command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);

            try
            {
                Tools.OpenConnection();
                command.ExecuteNonQuery();
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
        /// Gets all matches for a tournament.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="maclar">The list of tournament matches.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string MaclariGetir(int turnuvaId, out List<TurnuvaMaclari> maclar)
        {
            string hata = null;
            maclar = new List<TurnuvaMaclari>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT MacID, TurnuvaID, Uye1ID, Uye2ID, Skor1, Skor2, Tur, MacNo, Durum, KazananID, MacTarihi
                FROM TurnuvaMaclari
                WHERE TurnuvaID = @TurnuvaID
                ORDER BY 
                    CASE Tur 
                        WHEN 'Çeyrek Final' THEN 1
                        WHEN 'Yarı Final' THEN 2
                        WHEN 'Final' THEN 3
                        ELSE 4
                    END,
                    MacNo";

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TurnuvaMaclari mac = new TurnuvaMaclari
                    {
                        MacID = Convert.ToInt32(reader["MacID"]),
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        Uye1ID = Convert.ToInt32(reader["Uye1ID"]),
                        Uye2ID = Convert.ToInt32(reader["Uye2ID"]),
                        Skor1 = reader["Skor1"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor1"]) : null,
                        Skor2 = reader["Skor2"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor2"]) : null,
                        Tur = reader["Tur"].ToString(),
                        MacNo = Convert.ToInt32(reader["MacNo"]),
                        Durum = reader["Durum"].ToString(),
                        KazananID = reader["KazananID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KazananID"]) : null,
                        MacTarihi = reader["MacTarihi"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MacTarihi"]) : null
                    };
                    maclar.Add(mac);
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
        /// Gets matches for a specific round.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="tur">The round name.</param>
        /// <param name="maclar">The list of matches for the round.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string MaclariGetirByTur(int turnuvaId, string tur, out List<TurnuvaMaclari> maclar)
        {
            string hata = null;
            maclar = new List<TurnuvaMaclari>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT MacID, TurnuvaID, Uye1ID, Uye2ID, Skor1, Skor2, Tur, MacNo, Durum, KazananID, MacTarihi
                FROM TurnuvaMaclari
                WHERE TurnuvaID = @TurnuvaID AND Tur = @Tur
                ORDER BY MacNo";

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);
            command.Parameters.AddWithValue("@Tur", tur);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TurnuvaMaclari mac = new TurnuvaMaclari
                    {
                        MacID = Convert.ToInt32(reader["MacID"]),
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        Uye1ID = Convert.ToInt32(reader["Uye1ID"]),
                        Uye2ID = Convert.ToInt32(reader["Uye2ID"]),
                        Skor1 = reader["Skor1"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor1"]) : null,
                        Skor2 = reader["Skor2"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor2"]) : null,
                        Tur = reader["Tur"].ToString(),
                        MacNo = Convert.ToInt32(reader["MacNo"]),
                        Durum = reader["Durum"].ToString(),
                        KazananID = reader["KazananID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KazananID"]) : null,
                        MacTarihi = reader["MacTarihi"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MacTarihi"]) : null
                    };
                    maclar.Add(mac);
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
        /// Saves match result (score).
        /// </summary>
        /// <param name="mac">The match entity with results.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string MacSonucuKaydet(TurnuvaMaclari mac)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                UPDATE TurnuvaMaclari 
                SET Skor1 = @Skor1, Skor2 = @Skor2, Durum = @Durum, KazananID = @KazananID, MacTarihi = @MacTarihi
                WHERE MacID = @MacID";

            command.Parameters.AddWithValue("@MacID", mac.MacID);
            command.Parameters.AddWithValue("@Skor1", mac.Skor1.HasValue ? (object)mac.Skor1.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Skor2", mac.Skor2.HasValue ? (object)mac.Skor2.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Durum", mac.Durum ?? "Sonuçlandı");
            command.Parameters.AddWithValue("@KazananID", mac.KazananID.HasValue ? (object)mac.KazananID.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MacTarihi", mac.MacTarihi.HasValue ? (object)mac.MacTarihi.Value : DBNull.Value);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Maç sonucu kaydetme işlemi başarısız oldu.";
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
        /// Gets a match by ID.
        /// </summary>
        /// <param name="macId">The match ID.</param>
        /// <param name="mac">The match entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string MacGetir(int macId, out TurnuvaMaclari mac)
        {
            string hata = null;
            mac = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT MacID, TurnuvaID, Uye1ID, Uye2ID, Skor1, Skor2, Tur, MacNo, Durum, KazananID, MacTarihi
                FROM TurnuvaMaclari
                WHERE MacID = @MacID";

            command.Parameters.AddWithValue("@MacID", macId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    mac = new TurnuvaMaclari
                    {
                        MacID = Convert.ToInt32(reader["MacID"]),
                        TurnuvaID = Convert.ToInt32(reader["TurnuvaID"]),
                        Uye1ID = Convert.ToInt32(reader["Uye1ID"]),
                        Uye2ID = Convert.ToInt32(reader["Uye2ID"]),
                        Skor1 = reader["Skor1"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor1"]) : null,
                        Skor2 = reader["Skor2"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Skor2"]) : null,
                        Tur = reader["Tur"].ToString(),
                        MacNo = Convert.ToInt32(reader["MacNo"]),
                        Durum = reader["Durum"].ToString(),
                        KazananID = reader["KazananID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KazananID"]) : null,
                        MacTarihi = reader["MacTarihi"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MacTarihi"]) : null
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
        /// Advances winners to the next round and creates new matches.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="tur">The current round name.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string SonrakiTuraGec(int turnuvaId, string tur)
        {
            string hata = null;
            
            try
            {
                // Get completed matches from current round
                string hataMaclar = MaclariGetirByTur(turnuvaId, tur, out List<TurnuvaMaclari> tamamlananMaclar);
                if (hataMaclar != null)
                {
                    return hataMaclar;
                }
                
                tamamlananMaclar = tamamlananMaclar
                    .Where(m => m.Durum == "Sonuçlandı" && m.KazananID.HasValue)
                    .OrderBy(m => m.MacNo)
                    .ToList();

                if (tamamlananMaclar == null || tamamlananMaclar.Count == 0)
                {
                    return "Sonraki tura geçmek için tüm maçların sonuçlandırılması gerekiyor.";
                }

                string yeniTur = "";
                
                // Tur ismine göre sonraki turu belirle
                if (tur.Contains("Çeyrek Final") || tur.Contains("Son 16") || tur.Contains("İlk Tur"))
                {
                    yeniTur = "Yarı Final";
                    if (tamamlananMaclar.Count < 2)
                    {
                        return $"Yarı finale geçmek için en az 2 maçın sonuçlandırılması gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.";
                    }
                    if (tamamlananMaclar.Count % 2 != 0)
                    {
                        return $"Yarı finale geçmek için çift sayıda maç gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.";
                    }
                }
                else if (tur.Contains("Yarı Final"))
                {
                    yeniTur = "Final";
                    if (tamamlananMaclar.Count < 2)
                    {
                        return $"Finale geçmek için en az 2 maçın sonuçlandırılması gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.";
                    }
                    if (tamamlananMaclar.Count % 2 != 0)
                    {
                        return $"Finale geçmek için çift sayıda maç gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.";
                    }
                }
                else if (tur.Contains("Final"))
                {
                    // Final tamamlandı, turnuvayı bitir
                    return "Final maçı tamamlandı. Turnuvayı tamamlamak için 'Turnuvayı Tamamla' butonunu kullanın.";
                }
                else
                {
                    return $"Bilinmeyen tur: {tur}. Sonraki tura geçilemiyor.";
                }

                Tools.OpenConnection();

                // Create matches for next round
                int macNo = 1;
                for (int i = 0; i < tamamlananMaclar.Count; i += 2)
                {
                    if (i + 1 < tamamlananMaclar.Count)
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = Tools.Connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"
                            INSERT INTO TurnuvaMaclari (TurnuvaID, Uye1ID, Uye2ID, Tur, MacNo, Durum)
                            VALUES (@TurnuvaID, @Uye1ID, @Uye2ID, @Tur, @MacNo, 'Beklemede')";

                        command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);
                        command.Parameters.AddWithValue("@Uye1ID", tamamlananMaclar[i].KazananID.Value);
                        command.Parameters.AddWithValue("@Uye2ID", tamamlananMaclar[i + 1].KazananID.Value);
                        command.Parameters.AddWithValue("@Tur", yeniTur);
                        command.Parameters.AddWithValue("@MacNo", macNo);

                        command.ExecuteNonQuery();
                        macNo++;
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
        /// Finalizes the tournament when the final match is completed.
        /// Updates tournament status to "Tamamlandı" and awards prize to winner.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="kazananId">The winner's user ID, or null if tournament is not completed.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string TurnuvayiTamamla(int turnuvaId, out int? kazananId)
        {
            string hata = null;
            kazananId = null;
            
            try
            {
                Tools.OpenConnection();

                // Final maçını kontrol et
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    SELECT TOP 1 MacID, Uye1ID, Uye2ID, Skor1, Skor2, Durum, KazananID
                    FROM TurnuvaMaclari
                    WHERE TurnuvaID = @TurnuvaID AND Tur = 'Final'
                    ORDER BY MacNo";

                command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);

                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    return "Final maçı bulunamadı.";
                }

                string durum = reader["Durum"].ToString();
                kazananId = reader["KazananID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KazananID"]) : null;

                reader.Close();

                if (durum != "Sonuçlandı" || !kazananId.HasValue)
                {
                    return "Final maçı henüz sonuçlandırılmamış.";
                }

                // Turnuva bilgilerini al
                string hataGetir = Getir(turnuvaId, out Turnuvalar turnuva);
                if (hataGetir != null || turnuva == null)
                {
                    return hataGetir ?? "Turnuva bulunamadı.";
                }

                // Turnuva durumunu "Tamamlandı" olarak güncelle
                command.Parameters.Clear();
                command.CommandText = "UPDATE Turnuvalar SET Durum = 'Tamamlandı' WHERE TurnuvaID = @TurnuvaID";
                command.Parameters.AddWithValue("@TurnuvaID", turnuvaId);
                command.ExecuteNonQuery();

                // Kazanan üyeye ödül ekle
                if (turnuva.Odul > 0)
                {
                    command.Parameters.Clear();
                    command.CommandText = "UPDATE Uyeler SET Bakiye = Bakiye + @Odul WHERE UyeID = @UyeID";
                    command.Parameters.AddWithValue("@Odul", turnuva.Odul);
                    command.Parameters.AddWithValue("@UyeID", kazananId.Value);
                    command.ExecuteNonQuery();
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
