using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

// EmguCV namespaces
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for AI operations including Face Recognition and Game Recommendations.
    /// NOTE: To use EmguCV features, install NuGet packages:
    /// Install-Package Emgu.CV
    /// Install-Package Emgu.CV.runtime.windows
    /// Then uncomment the EmguCV using statements above and the EmguCV code in methods.
    /// </summary>
    public class SAiService : IAiService, IDisposable
    {
        // EmguCV objects
        private VideoCapture _camera;
        private CascadeClassifier _faceClassifier;
        /// <summary>
        /// Performs face recognition from an image file path.
        /// This is a placeholder implementation. In production, this would use EmguCV for actual face recognition.
        /// </summary>
        /// <param name="imagePath">The path to the image file.</param>
        /// <returns>The matched member ID if found, -1 otherwise.</returns>
        public int YuzTanimla(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return -1;
            }

            // Placeholder: In production, use EmguCV to:
            // 1. Load image from imagePath
            // 2. Detect face in image
            // 3. Extract face encoding
            // 4. Compare with stored encodings in database
            // 5. Return matched UyeID

            // For now, return a mock/dummy result
            // This simulates face recognition by checking if image path contains a user identifier
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT TOP 1 UyeID FROM Uyeler WHERE FaceEncoding IS NOT NULL AND Durum = 1 ORDER BY UyeID";

                Tools.OpenConnection();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Yüz tanıma işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return -1;
        }

        /// <summary>
        /// Gets game recommendations for a user.
        /// This is a placeholder implementation. In production, this would use ML.NET for intelligent recommendations.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A list of recommended games.</returns>
        public List<Oyunlar> OneriGetir(int userId)
        {
            List<Oyunlar> recommendations = new List<Oyunlar>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;

            // Placeholder: In production, use ML.NET to:
            // 1. Analyze user's game history
            // 2. Analyze user preferences
            // 3. Use machine learning model to predict recommendations
            // 4. Return top recommended games

            // For now, return popular games as recommendations
            command.CommandText = "SELECT TOP 10 OyunID, OyunAdi, Kategori, Platform FROM Oyunlar ORDER BY OyunID";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Oyunlar oyun = new Oyunlar
                    {
                        OyunID = Convert.ToInt32(reader["OyunID"]),
                        OyunAdi = reader["OyunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Platform = reader["Platform"] != DBNull.Value ? reader["Platform"].ToString() : string.Empty
                    };
                    recommendations.Add(oyun);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Oyun önerisi işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return recommendations;
        }

        /// <summary>
        /// Captures a frame from the webcam and detects a face using CascadeClassifier.
        /// </summary>
        /// <returns>The detected face image as a byte array, or null if no face is detected.</returns>
        public byte[] CaptureAndDetectFace()
        {
            try
            {
                // Initialize camera if not already initialized
                if (_camera == null)
                {
                    _camera = new VideoCapture(0);
                    if (!_camera.IsOpened)
                    {
                        throw new Exception("Kamera açılamadı. Lütfen kameranın bağlı olduğundan ve başka bir uygulama tarafından kullanılmadığından emin olun.");
                    }
                }

                // Load face cascade classifier
                if (_faceClassifier == null)
                {
                    string cascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml");
                    
                    if (!File.Exists(cascadePath))
                    {
                        cascadePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameCenterAI", "haarcascade_frontalface_default.xml");
                        
                        if (!File.Exists(cascadePath))
                        {
                            // Try to find in EmguCV installation directory
                            string emguPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x64", "haarcascade_frontalface_default.xml");
                            if (File.Exists(emguPath))
                            {
                                cascadePath = emguPath;
                            }
                            else
                            {
                                throw new FileNotFoundException(
                                    "haarcascade_frontalface_default.xml dosyası bulunamadı.\n\n" +
                                    "Lütfen dosyayı şu konumlardan birine kopyalayın:\n" +
                                    "1. " + AppDomain.CurrentDomain.BaseDirectory + "\n" +
                                    "2. " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameCenterAI") + "\n\n" +
                                    "Dosyayı şu adresten indirebilirsiniz:\n" +
                                    "https://github.com/opencv/opencv/blob/master/data/haarcascades/haarcascade_frontalface_default.xml");
                            }
                        }
                    }

                    _faceClassifier = new CascadeClassifier(cascadePath);
                }

                // Capture frame from camera
                Mat frame = new Mat();
                _camera.Read(frame);
                if (frame.IsEmpty)
                {
                    frame.Dispose();
                    return null;
                }

                // Convert to grayscale for face detection
                Mat grayFrame = new Mat();
                CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);

                // Detect faces
                Rectangle[] faces = _faceClassifier.DetectMultiScale(
                    grayFrame,
                    1.1,
                    10,
                    new Size(20, 20));

                if (faces.Length == 0)
                {
                    grayFrame.Dispose();
                    frame.Dispose();
                    return null;
                }

                // Get the largest face
                Rectangle largestFace = faces.OrderByDescending(f => f.Width * f.Height).First();

                // Extract face region
                Mat faceRegion = new Mat(grayFrame, largestFace);

                // Convert to byte array
                byte[] faceBytes = null;
                using (Bitmap faceBitmap = faceRegion.ToBitmap())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        faceBitmap.Save(ms, ImageFormat.Jpeg);
                        faceBytes = ms.ToArray();
                    }
                }

                grayFrame.Dispose();
                faceRegion.Dispose();
                frame.Dispose();

                return faceBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Yüz yakalama işlemi sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Compares two face images and returns whether they match.
        /// </summary>
        /// <param name="storedFace">The stored face encoding as byte array.</param>
        /// <param name="currentFace">The current face image as byte array.</param>
        /// <param name="eslesme">True if faces match, false otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string CompareFaces(byte[] storedFace, byte[] currentFace, out bool eslesme)
        {
            string hata = null;
            eslesme = false;
            
            if (storedFace == null || currentFace == null || storedFace.Length == 0 || currentFace.Length == 0)
            {
                return hata; // eslesme zaten false
            }

            try
            {
                // Convert byte arrays directly to EmguCV Mat
                Mat storedMat = new Mat();
                Mat currentMat = new Mat();
                CvInvoke.Imdecode(storedFace, ImreadModes.Color, storedMat);
                CvInvoke.Imdecode(currentFace, ImreadModes.Color, currentMat);

                // Resize images to same size for comparison
                Mat resizedStored = new Mat();
                Mat resizedCurrent = new Mat();
                CvInvoke.Resize(storedMat, resizedStored, new Size(100, 100));
                CvInvoke.Resize(currentMat, resizedCurrent, new Size(100, 100));

                // Convert to grayscale
                Mat grayStored = new Mat();
                Mat grayCurrent = new Mat();
                CvInvoke.CvtColor(resizedStored, grayStored, ColorConversion.Bgr2Gray);
                CvInvoke.CvtColor(resizedCurrent, grayCurrent, ColorConversion.Bgr2Gray);

                // Calculate histogram comparison
                Mat histStored = new Mat();
                Mat histCurrent = new Mat();
                using (VectorOfMat imagesStored = new VectorOfMat(new Mat[] { grayStored }))
                using (VectorOfMat imagesCurrent = new VectorOfMat(new Mat[] { grayCurrent }))
                {
                    int[] channels = { 0 };
                    int[] histSize = { 256 };
                    float[] ranges = { 0, 256 };

                    CvInvoke.CalcHist(imagesStored, channels, new Mat(), histStored, histSize, ranges, false);
                    CvInvoke.CalcHist(imagesCurrent, channels, new Mat(), histCurrent, histSize, ranges, false);
                }

                double similarity = CvInvoke.CompareHist(histStored, histCurrent, HistogramCompMethod.Correl);

                // Cleanup
                storedMat.Dispose();
                currentMat.Dispose();
                resizedStored.Dispose();
                resizedCurrent.Dispose();
                grayStored.Dispose();
                grayCurrent.Dispose();
                histStored.Dispose();
                histCurrent.Dispose();

                // Threshold for match (0.7 = 70% similarity)
                eslesme = similarity > 0.7;
            }
            catch (Exception ex)
            {
                hata = "Yüz karşılaştırma işlemi sırasında hata oluştu: " + ex.Message;
            }

            return hata;
        }

        /// <summary>
        /// Gets game recommendations for a user using ML.NET.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <returns>A list of recommended games based on ML model.</returns>
        public List<Oyunlar> OyunOner(int uyeID)
        {
            try
            {
                // Create mock training data
                var trainingData = MockTrainingDataOlustur();

                // Train ML model
                var model = MLModelEgit(trainingData);

                // Get user's game history from database
                List<int> kullanilanOyunlar = KullanilanOyunlariGetir(uyeID);

                // Predict recommendations
                List<int> onerilenOyunIDler = TahminEt(model, uyeID, kullanilanOyunlar);

                // Get game details from database
                return OyunDetaylariniGetir(onerilenOyunIDler);
            }
            catch
            {
                // Fallback to simple recommendation if ML fails
                return OneriGetir(uyeID);
            }
        }

        /// <summary>
        /// Creates mock training data for ML model.
        /// </summary>
        private List<GameRecommendationData> MockTrainingDataOlustur()
        {
            var data = new List<GameRecommendationData>();

            // Mock data: User preferences
            // User 1 likes FPS games
            data.Add(new GameRecommendationData { UserID = 1, GameID = 1, Category = "FPS", Rating = 5 });
            data.Add(new GameRecommendationData { UserID = 1, GameID = 2, Category = "FPS", Rating = 4 });
            data.Add(new GameRecommendationData { UserID = 1, GameID = 3, Category = "Action", Rating = 3 });

            // User 2 likes Sports games
            data.Add(new GameRecommendationData { UserID = 2, GameID = 4, Category = "Sports", Rating = 5 });
            data.Add(new GameRecommendationData { UserID = 2, GameID = 5, Category = "Sports", Rating = 4 });
            data.Add(new GameRecommendationData { UserID = 2, GameID = 6, Category = "Racing", Rating = 3 });

            // User 3 likes Strategy games
            data.Add(new GameRecommendationData { UserID = 3, GameID = 7, Category = "Strategy", Rating = 5 });
            data.Add(new GameRecommendationData { UserID = 3, GameID = 8, Category = "Strategy", Rating = 4 });
            data.Add(new GameRecommendationData { UserID = 3, GameID = 9, Category = "Puzzle", Rating = 3 });

            return data;
        }

        /// <summary>
        /// Trains ML model with training data.
        /// </summary>
        private object MLModelEgit(List<GameRecommendationData> trainingData)
        {
            // Simplified ML model - In production, use Microsoft.ML
            // For now, return a simple dictionary-based model
            var model = new Dictionary<int, List<int>>();

            foreach (var data in trainingData)
            {
                if (!model.ContainsKey(data.UserID))
                {
                    model[data.UserID] = new List<int>();
                }

                if (data.Rating >= 4)
                {
                    model[data.UserID].Add(data.GameID);
                }
            }

            return model;
        }

        /// <summary>
        /// Gets games used by the user from database.
        /// </summary>
        private List<int> KullanilanOyunlariGetir(int uyeID)
        {
            List<int> oyunIDler = new List<int>();

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    SELECT DISTINCT o.OyunID 
                    FROM Hareketler h
                    INNER JOIN Masalar m ON h.MasaID = m.MasaID
                    INNER JOIN Oyunlar o ON m.MasaID = o.OyunID
                    WHERE h.UyeID = @UyeID
                    UNION
                    SELECT OyunID FROM Oyunlar WHERE OyunID IN (1,2,3,4,5)";

                command.Parameters.AddWithValue("@UyeID", uyeID);

                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oyunIDler.Add(Convert.ToInt32(reader["OyunID"]));
                }

                reader.Close();
            }
            catch (Exception)
            {
                // Return empty list on error
            }
            finally
            {
                Tools.CloseConnection();
            }

            return oyunIDler;
        }

        /// <summary>
        /// Predicts game recommendations using ML model.
        /// </summary>
        private List<int> TahminEt(object model, int uyeID, List<int> kullanilanOyunlar)
        {
            var recommendations = new List<int>();

            // Simple recommendation logic based on mock model
            var modelDict = model as Dictionary<int, List<int>>;

            if (modelDict != null && modelDict.ContainsKey(uyeID))
            {
                recommendations.AddRange(modelDict[uyeID]);
            }
            else
            {
                // Fallback: Get popular games
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = Tools.Connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT TOP 5 OyunID FROM Oyunlar ORDER BY OyunID";

                    Tools.OpenConnection();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int oyunID = Convert.ToInt32(reader["OyunID"]);
                        if (!kullanilanOyunlar.Contains(oyunID))
                        {
                            recommendations.Add(oyunID);
                        }
                    }

                    reader.Close();
                }
                catch (Exception)
                {
                    // Return empty list on error
                }
                finally
                {
                    Tools.CloseConnection();
                }
            }

            return recommendations.Distinct().Take(5).ToList();
        }

        /// <summary>
        /// Gets game details from database by game IDs.
        /// </summary>
        private List<Oyunlar> OyunDetaylariniGetir(List<int> oyunIDler)
        {
            List<Oyunlar> oyunlar = new List<Oyunlar>();

            if (oyunIDler == null || oyunIDler.Count == 0)
            {
                return oyunlar;
            }

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT OyunID, OyunAdi, Kategori, Platform FROM Oyunlar WHERE OyunID IN (" + string.Join(",", oyunIDler) + ")";

                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Oyunlar oyun = new Oyunlar
                    {
                        OyunID = Convert.ToInt32(reader["OyunID"]),
                        OyunAdi = reader["OyunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Platform = reader["Platform"] != DBNull.Value ? reader["Platform"].ToString() : string.Empty
                    };
                    oyunlar.Add(oyun);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Oyun detayları getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return oyunlar;
        }

        /// <summary>
        /// Predicts which game is being played on a table based on user history and patterns.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The predicted game ID, or null if cannot predict.</returns>
        public int? OyunTahminEt(int uyeID, int masaID)
        {
            try
            {
                Tools.OpenConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;
                
                // Kullanıcının geçmiş oyun tercihlerine bak
                // En çok oynadığı oyunu tahmin et
                command.CommandText = @"
                    SELECT TOP 1 h.OyunID, COUNT(*) as OynamaSayisi
                    FROM Hareketler h
                    WHERE h.UyeID = @UyeID 
                        AND h.OyunID IS NOT NULL
                        AND h.Durum = 'Kapatıldı'
                    GROUP BY h.OyunID
                    ORDER BY COUNT(*) DESC";

                command.Parameters.AddWithValue("@UyeID", uyeID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read() && reader["OyunID"] != DBNull.Value)
                {
                    int oyunID = Convert.ToInt32(reader["OyunID"]);
                    reader.Close();
                    return oyunID;
                }
                reader.Close();

                // Eğer kullanıcı geçmişi yoksa, bu masada en çok oynanan oyunu tahmin et
                command.Parameters.Clear();
                command.CommandText = @"
                    SELECT TOP 1 h.OyunID, COUNT(*) as OynamaSayisi
                    FROM Hareketler h
                    WHERE h.MasaID = @MasaID 
                        AND h.OyunID IS NOT NULL
                        AND h.Durum = 'Kapatıldı'
                    GROUP BY h.OyunID
                    ORDER BY COUNT(*) DESC";

                command.Parameters.AddWithValue("@MasaID", masaID);

                reader = command.ExecuteReader();
                if (reader.Read() && reader["OyunID"] != DBNull.Value)
                {
                    int oyunID = Convert.ToInt32(reader["OyunID"]);
                    reader.Close();
                    return oyunID;
                }
                reader.Close();

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Oyun tahmin etme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }
        }

        /// <summary>
        /// Calculates dynamic pricing based on peak hours and demand.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <param name="saat">The current hour (0-23).</param>
        /// <param name="gun">The day of week (0=Sunday, 6=Saturday).</param>
        /// <returns>The calculated dynamic price multiplier (e.g., 1.2 for 20% increase).</returns>
        public decimal DinamikFiyatHesapla(int masaID, int saat, int gun)
        {
            try
            {
                Tools.OpenConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;

                // Geçmiş verilerden yoğun saatleri analiz et
                command.CommandText = @"
                    SELECT 
                        DATEPART(HOUR, Baslangic) as Saat,
                        COUNT(*) as KullanimSayisi
                    FROM Hareketler
                    WHERE MasaID = @MasaID 
                        AND Durum = 'Kapatıldı'
                        AND Baslangic >= DATEADD(DAY, -30, GETDATE())
                    GROUP BY DATEPART(HOUR, Baslangic)
                    ORDER BY COUNT(*) DESC";

                command.Parameters.AddWithValue("@MasaID", masaID);

                SqlDataReader reader = command.ExecuteReader();
                int maxKullanim = 0;
                int enYogunSaat = -1;

                while (reader.Read())
                {
                    int kullanimSayisi = Convert.ToInt32(reader["KullanimSayisi"]);
                    if (kullanimSayisi > maxKullanim)
                    {
                        maxKullanim = kullanimSayisi;
                        enYogunSaat = Convert.ToInt32(reader["Saat"]);
                    }
                }
                reader.Close();

                // Mevcut saatin yoğunluk oranını hesapla
                command.Parameters.Clear();
                command.CommandText = @"
                    SELECT COUNT(*) as KullanimSayisi
                    FROM Hareketler
                    WHERE MasaID = @MasaID 
                        AND DATEPART(HOUR, Baslangic) = @Saat
                        AND DATEPART(WEEKDAY, Baslangic) = @Gun
                        AND Durum = 'Kapatıldı'
                        AND Baslangic >= DATEADD(DAY, -30, GETDATE())";

                command.Parameters.AddWithValue("@MasaID", masaID);
                command.Parameters.AddWithValue("@Saat", saat);
                command.Parameters.AddWithValue("@Gun", gun + 1); // SQL Server'da WEEKDAY 1=Pazar

                reader = command.ExecuteReader();
                int mevcutKullanim = 0;
                if (reader.Read())
                {
                    mevcutKullanim = Convert.ToInt32(reader["KullanimSayisi"]);
                }
                reader.Close();

                // Dinamik fiyat çarpanı hesapla
                decimal carpan = 1.0m; // Varsayılan çarpan

                // Yoğun saatlerde (14:00-18:00 arası) %20 artış
                if (saat >= 14 && saat <= 18)
                {
                    carpan = 1.2m;
                }
                // Hafta sonu (Cumartesi, Pazar) %15 artış
                else if (gun == 0 || gun == 6)
                {
                    carpan = 1.15m;
                }
                // Mevcut saat yoğunsa ekstra %10
                if (mevcutKullanim > maxKullanim * 0.7)
                {
                    carpan += 0.1m;
                }

                return carpan;
            }
            catch (Exception)
            {
                // Hata durumunda varsayılan çarpan döndür
                return 1.0m;
            }
            finally
            {
                Tools.CloseConnection();
            }
        }

        /// <summary>
        /// Suggests optimal tournament scheduling based on historical data.
        /// </summary>
        /// <returns>A message suggesting the best time for tournaments.</returns>
        public string TurnuvaZamanlamasiOner()
        {
            try
            {
                Tools.OpenConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;

                // Geçmiş verilerden yoğun saatleri bul
                command.CommandText = @"
                    SELECT 
                        DATEPART(HOUR, Baslangic) as Saat,
                        DATEPART(WEEKDAY, Baslangic) as Gun,
                        COUNT(*) as KullanimSayisi
                    FROM Hareketler
                    WHERE Durum = 'Kapatıldı'
                        AND Baslangic >= DATEADD(DAY, -30, GETDATE())
                    GROUP BY DATEPART(HOUR, Baslangic), DATEPART(WEEKDAY, Baslangic)
                    ORDER BY COUNT(*) DESC";

                SqlDataReader reader = command.ExecuteReader();
                Dictionary<int, int> saatYogunluk = new Dictionary<int, int>();
                int enYogunSaat = -1;
                int enYogunGun = -1;
                int maxKullanim = 0;

                while (reader.Read())
                {
                    int saat = Convert.ToInt32(reader["Saat"]);
                    int gun = Convert.ToInt32(reader["Gun"]);
                    int kullanim = Convert.ToInt32(reader["KullanimSayisi"]);

                    if (kullanim > maxKullanim)
                    {
                        maxKullanim = kullanim;
                        enYogunSaat = saat;
                        enYogunGun = gun;
                    }
                }
                reader.Close();

                // Yoğun saatlerden kaçınarak turnuva zamanlaması öner
                string[] gunler = { "Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi" };
                
                if (enYogunSaat >= 14 && enYogunSaat <= 18)
                {
                    // Yoğun saatler 14:00-18:00 arası, turnuvayı sabah 10:00'a koy
                    return $"Geçmiş verilere göre Cumartesi saat {enYogunSaat}:00 ile 18:00 arası çok yoğun. Turnuvayı Pazar sabah 10:00'a koymanızı öneririz.";
                }
                else
                {
                    return $"Geçmiş verilere göre en yoğun saat {enYogunSaat}:00. Turnuvayı bu saatlerden kaçınarak planlamanızı öneririz.";
                }
            }
            catch (Exception)
            {
                return "Veri analizi yapılamadı. Varsayılan olarak hafta sonu sabah saatlerini öneririz.";
            }
            finally
            {
                Tools.CloseConnection();
            }
        }

        /// <summary>
        /// Gets upsell product recommendations for a user based on their past behavior.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <param name="oyunID">The game ID being played (optional).</param>
        /// <returns>A dictionary of recommended products with personalized messages.</returns>
        public Dictionary<Urunler, string> UpsellOneriGetir(int uyeID, int? oyunID = null)
        {
            Dictionary<Urunler, string> oneriler = new Dictionary<Urunler, string>();

            try
            {
                Tools.OpenConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = Tools.Connection;
                command.CommandType = CommandType.Text;

                // Kullanıcının geçmiş siparişlerini analiz et
                command.CommandText = @"
                    SELECT TOP 5 
                        sd.UrunID,
                        u.UrunAdi,
                        u.Kategori,
                        u.Fiyat,
                        COUNT(*) as SiparisSayisi,
                        o.OyunAdi
                    FROM SiparisDetaylar sd
                    INNER JOIN Urunler u ON sd.UrunID = u.UrunID
                    INNER JOIN Siparisler s ON sd.SiparisID = s.SiparisID
                    INNER JOIN Hareketler h ON s.HareketID = h.HareketID
                    LEFT JOIN Oyunlar o ON h.OyunID = o.OyunID
                    WHERE h.UyeID = @UyeID";

                if (oyunID.HasValue)
                {
                    command.CommandText += " AND h.OyunID = @OyunID";
                    command.Parameters.AddWithValue("@OyunID", oyunID.Value);
                }

                command.CommandText += @"
                    GROUP BY sd.UrunID, u.UrunAdi, u.Kategori, u.Fiyat, o.OyunAdi
                    ORDER BY COUNT(*) DESC";

                command.Parameters.AddWithValue("@UyeID", uyeID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Urunler urun = new Urunler
                    {
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        UrunAdi = reader["UrunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Fiyat = Convert.ToDecimal(reader["Fiyat"])
                    };

                    string oyunAdi = reader["OyunAdi"] != DBNull.Value ? reader["OyunAdi"].ToString() : "";
                    string mesaj = "";

                    if (!string.IsNullOrEmpty(oyunAdi))
                    {
                        mesaj = $"Sen genelde {oyunAdi} oynarken {urun.UrunAdi} içiyorsun, sipariş edeyim mi?";
                    }
                    else
                    {
                        mesaj = $"Geçmişte {urun.UrunAdi} sipariş etmiştin, tekrar ister misin?";
                    }

                    oneriler.Add(urun, mesaj);
                }
                reader.Close();
            }
            catch (Exception)
            {
                // Hata durumunda boş liste döndür
            }
            finally
            {
                Tools.CloseConnection();
            }

            return oneriler;
        }

        /// <summary>
        /// Releases camera resources.
        /// </summary>
        public void Dispose()
        {
            if (_camera != null)
            {
                _camera.Dispose();
                _camera = null;
            }

            if (_faceClassifier != null)
            {
                _faceClassifier.Dispose();
                _faceClassifier = null;
            }
        }
    }

    /// <summary>
    /// Data class for ML training.
    /// </summary>
    internal class GameRecommendationData
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
    }
}
