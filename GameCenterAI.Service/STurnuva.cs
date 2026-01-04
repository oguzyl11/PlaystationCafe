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
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Olustur(Turnuvalar turnuva)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Turnuva oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Lists all tournaments.
        /// </summary>
        /// <returns>A list of all tournaments.</returns>
        public List<Turnuvalar> Listele()
        {
            List<Turnuvalar> turnuvalar = new List<Turnuvalar>();
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
                throw new Exception("Turnuva listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return turnuvalar;
        }

        /// <summary>
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random active users and creates pairs based on user count.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of paired user IDs (each pair represents a match).</returns>
        public List<KeyValuePair<int, int>> EslestirmeleriOlustur(int turnuvaID)
        {
            List<KeyValuePair<int, int>> eslestirmeler = new List<KeyValuePair<int, int>>();
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
                    throw new Exception("Turnuva için en az 2 aktif üye gereklidir.");
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
                throw new Exception("Eşleştirme oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return eslestirmeler;
        }

        /// <summary>
        /// Creates tournament matches in database from pairings.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="eslestirmeler">List of pairings.</param>
        /// <returns>True if successful.</returns>
        public bool MaclariOlustur(int turnuvaID, List<KeyValuePair<int, int>> eslestirmeler)
        {
            try
            {
                Tools.OpenConnection();
                int macNo = 1;

                foreach (var eslestirme in eslestirmeler)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = Tools.Connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                        INSERT INTO TurnuvaMaclari (TurnuvaID, Uye1ID, Uye2ID, Tur, MacNo, Durum)
                        VALUES (@TurnuvaID, @Uye1ID, @Uye2ID, @Tur, @MacNo, 'Beklemede')";

                    command.Parameters.AddWithValue("@TurnuvaID", turnuvaID);
                    command.Parameters.AddWithValue("@Uye1ID", eslestirme.Key);
                    command.Parameters.AddWithValue("@Uye2ID", eslestirme.Value);
                    command.Parameters.AddWithValue("@Tur", "Çeyrek Final");
                    command.Parameters.AddWithValue("@MacNo", macNo);

                    command.ExecuteNonQuery();
                    macNo++;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Maç oluşturma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }
        }

        /// <summary>
        /// Gets a tournament by ID.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>The tournament entity.</returns>
        public Turnuvalar Getir(int turnuvaID)
        {
            Turnuvalar turnuva = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT TurnuvaID, TurnuvaAdi, BaslangicTarihi, Odul, Durum FROM Turnuvalar WHERE TurnuvaID = @TurnuvaID";

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaID);

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
                throw new Exception("Turnuva getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return turnuva;
        }

        /// <summary>
        /// Updates an existing tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to update.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Guncelle(Turnuvalar turnuva)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Turnuva güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Deletes a tournament.
        /// First deletes all related matches, then deletes the tournament.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID to delete.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Sil(int turnuvaID)
        {
            bool result = false;

            try
            {
                Tools.OpenConnection();

                // First, delete all matches related to this tournament
                SqlCommand deleteMaclarCommand = new SqlCommand();
                deleteMaclarCommand.Connection = Tools.Connection;
                deleteMaclarCommand.CommandType = CommandType.Text;
                deleteMaclarCommand.CommandText = "DELETE FROM TurnuvaMaclari WHERE TurnuvaID = @TurnuvaID";
                deleteMaclarCommand.Parameters.AddWithValue("@TurnuvaID", turnuvaID);
                deleteMaclarCommand.ExecuteNonQuery();

                // Then, delete the tournament
                SqlCommand deleteTurnuvaCommand = new SqlCommand();
                deleteTurnuvaCommand.Connection = Tools.Connection;
                deleteTurnuvaCommand.CommandType = CommandType.Text;
                deleteTurnuvaCommand.CommandText = "DELETE FROM Turnuvalar WHERE TurnuvaID = @TurnuvaID";
                deleteTurnuvaCommand.Parameters.AddWithValue("@TurnuvaID", turnuvaID);

                int affectedRows = deleteTurnuvaCommand.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Turnuva silme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Gets all matches for a tournament.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of tournament matches.</returns>
        public List<TurnuvaMaclari> MaclariGetir(int turnuvaID)
        {
            List<TurnuvaMaclari> maclar = new List<TurnuvaMaclari>();
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

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaID);

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
                throw new Exception("Maç listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return maclar;
        }

        /// <summary>
        /// Gets matches for a specific round.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="tur">The round name.</param>
        /// <returns>A list of matches for the round.</returns>
        public List<TurnuvaMaclari> MaclariGetirByTur(int turnuvaID, string tur)
        {
            List<TurnuvaMaclari> maclar = new List<TurnuvaMaclari>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT MacID, TurnuvaID, Uye1ID, Uye2ID, Skor1, Skor2, Tur, MacNo, Durum, KazananID, MacTarihi
                FROM TurnuvaMaclari
                WHERE TurnuvaID = @TurnuvaID AND Tur = @Tur
                ORDER BY MacNo";

            command.Parameters.AddWithValue("@TurnuvaID", turnuvaID);
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
                throw new Exception("Maç listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return maclar;
        }

        /// <summary>
        /// Saves match result (score).
        /// </summary>
        /// <param name="mac">The match entity with results.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool MacSonucuKaydet(TurnuvaMaclari mac)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Maç sonucu kaydetme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Gets a match by ID.
        /// </summary>
        /// <param name="macID">The match ID.</param>
        /// <returns>The match entity.</returns>
        public TurnuvaMaclari MacGetir(int macID)
        {
            TurnuvaMaclari mac = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT MacID, TurnuvaID, Uye1ID, Uye2ID, Skor1, Skor2, Tur, MacNo, Durum, KazananID, MacTarihi
                FROM TurnuvaMaclari
                WHERE MacID = @MacID";

            command.Parameters.AddWithValue("@MacID", macID);

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
                throw new Exception("Maç getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return mac;
        }

        /// <summary>
        /// Advances winners to the next round and creates new matches.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="tur">The current round name.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool SonrakiTuraGec(int turnuvaID, string tur)
        {
            try
            {
                // Get completed matches from current round
                var tamamlananMaclar = MaclariGetirByTur(turnuvaID, tur)
                    .Where(m => m.Durum == "Sonuçlandı" && m.KazananID.HasValue)
                    .OrderBy(m => m.MacNo)
                    .ToList();

                if (tamamlananMaclar.Count == 0)
                {
                    throw new Exception("Sonraki tura geçmek için tüm maçların sonuçlandırılması gerekiyor.");
                }

                string yeniTur = "";
                
                // Tur ismine göre sonraki turu belirle
                if (tur.Contains("Çeyrek Final") || tur.Contains("Son 16") || tur.Contains("İlk Tur"))
                {
                    yeniTur = "Yarı Final";
                    if (tamamlananMaclar.Count < 2)
                    {
                        throw new Exception($"Yarı finale geçmek için en az 2 maçın sonuçlandırılması gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.");
                    }
                    if (tamamlananMaclar.Count % 2 != 0)
                    {
                        throw new Exception($"Yarı finale geçmek için çift sayıda maç gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.");
                    }
                }
                else if (tur.Contains("Yarı Final"))
                {
                    yeniTur = "Final";
                    if (tamamlananMaclar.Count < 2)
                    {
                        throw new Exception($"Finale geçmek için en az 2 maçın sonuçlandırılması gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.");
                    }
                    if (tamamlananMaclar.Count % 2 != 0)
                    {
                        throw new Exception($"Finale geçmek için çift sayıda maç gerekiyor. Şu anda {tamamlananMaclar.Count} maç sonuçlandı.");
                    }
                }
                else if (tur.Contains("Final"))
                {
                    throw new Exception("Turnuva zaten tamamlandı. Final maçı oynandı.");
                }
                else
                {
                    throw new Exception($"Bilinmeyen tur: {tur}. Sonraki tura geçilemiyor.");
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

                        command.Parameters.AddWithValue("@TurnuvaID", turnuvaID);
                        command.Parameters.AddWithValue("@Uye1ID", tamamlananMaclar[i].KazananID.Value);
                        command.Parameters.AddWithValue("@Uye2ID", tamamlananMaclar[i + 1].KazananID.Value);
                        command.Parameters.AddWithValue("@Tur", yeniTur);
                        command.Parameters.AddWithValue("@MacNo", macNo);

                        command.ExecuteNonQuery();
                        macNo++;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Sonraki tura geçiş işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }
        }
    }
}
