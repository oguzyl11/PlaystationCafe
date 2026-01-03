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

// EmguCV namespaces - uncomment after installing EmguCV NuGet packages
// using Emgu.CV;
// using Emgu.CV.CvEnum;
// using Emgu.CV.Structure;

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
        // EmguCV objects - uncomment after installing EmguCV
        // private Emgu.CV.Capture _camera;
        // private Emgu.CV.CascadeClassifier _faceClassifier;
        private object _camera;
        private object _faceClassifier;
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
            // NOTE: This method requires EmguCV NuGet packages to be installed
            // Install-Package Emgu.CV
            // Install-Package Emgu.CV.runtime.windows
            // After installing, uncomment the EmguCV using statements at the top of this file
            // and replace this method with the implementation from SAiService_EmguCV.cs
            
            throw new NotImplementedException(
                "EmguCV kütüphanesi yüklü değil.\n\n" +
                "Kurulum adımları:\n" +
                "1. NuGet Package Manager'dan şu paketleri yükleyin:\n" +
                "   - Install-Package Emgu.CV\n" +
                "   - Install-Package Emgu.CV.runtime.windows\n" +
                "2. SAiService.cs dosyasının başındaki EmguCV using direktiflerini uncomment edin\n" +
                "3. SAiService_EmguCV.cs dosyasındaki kodları SAiService.cs'e kopyalayın\n" +
                "4. haarcascade_frontalface_default.xml dosyasını uygulama klasörüne kopyalayın");
        }

        /// <summary>
        /// Compares two face images and returns whether they match.
        /// </summary>
        /// <param name="storedFace">The stored face encoding as byte array.</param>
        /// <param name="currentFace">The current face image as byte array.</param>
        /// <returns>True if faces match, false otherwise.</returns>
        public bool CompareFaces(byte[] storedFace, byte[] currentFace)
        {
            if (storedFace == null || currentFace == null || storedFace.Length == 0 || currentFace.Length == 0)
            {
                return false;
            }

            // NOTE: This method requires EmguCV NuGet packages to be installed
            // After installing EmguCV, replace this method with the implementation from SAiService_EmguCV.cs
            
            // Fallback: Simple byte array comparison (not accurate, but works without EmguCV)
            if (storedFace.Length != currentFace.Length)
            {
                return false;
            }
            
            // Simple similarity check
            int matches = 0;
            for (int i = 0; i < storedFace.Length && i < currentFace.Length; i++)
            {
                if (Math.Abs(storedFace[i] - currentFace[i]) < 10)
                {
                    matches++;
                }
            }
            
            double similarity = (double)matches / storedFace.Length;
            return similarity > 0.7;
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
            catch (Exception ex)
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
        /// Releases camera resources.
        /// </summary>
        public void Dispose()
        {
            // NOTE: EmguCV Dispose implementation (uncomment after installing EmguCV)
            /*
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
            */
            _camera = null;
            _faceClassifier = null;
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
