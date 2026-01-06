using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for AI service operations including Face Recognition and Game Recommendations.
    /// </summary>
    public interface IAiService
    {
        /// <summary>
        /// Performs face recognition from an image file path.
        /// </summary>
        /// <param name="imagePath">The path to the image file.</param>
        /// <returns>The matched member ID if found, -1 otherwise.</returns>
        int YuzTanimla(string imagePath);

        /// <summary>
        /// Gets game recommendations for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A list of recommended games.</returns>
        List<Oyunlar> OneriGetir(int userId);

        /// <summary>
        /// Captures a frame from the webcam and detects a face using CascadeClassifier.
        /// </summary>
        /// <returns>The detected face image as a byte array, or null if no face is detected.</returns>
        byte[] CaptureAndDetectFace();

        /// <summary>
        /// Compares two face images and returns whether they match.
        /// </summary>
        /// <param name="storedFace">The stored face encoding as byte array.</param>
        /// <param name="currentFace">The current face image as byte array.</param>
        /// <param name="eslesme">True if faces match, false otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string CompareFaces(byte[] storedFace, byte[] currentFace, out bool eslesme);

        /// <summary>
        /// Gets game recommendations for a user using ML.NET.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <returns>A list of recommended games based on ML model.</returns>
        List<Oyunlar> OyunOner(int uyeID);

        /// <summary>
        /// Predicts which game is being played on a table based on user history and patterns.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The predicted game ID, or null if cannot predict.</returns>
        int? OyunTahminEt(int uyeID, int masaID);

        /// <summary>
        /// Calculates dynamic pricing based on peak hours and demand.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <param name="saat">The current hour (0-23).</param>
        /// <param name="gun">The day of week (0=Sunday, 6=Saturday).</param>
        /// <returns>The calculated dynamic price multiplier (e.g., 1.2 for 20% increase).</returns>
        decimal DinamikFiyatHesapla(int masaID, int saat, int gun);

        /// <summary>
        /// Suggests optimal tournament scheduling based on historical data.
        /// </summary>
        /// <returns>A message suggesting the best time for tournaments.</returns>
        string TurnuvaZamanlamasiOner();

        /// <summary>
        /// Gets upsell product recommendations for a user based on their past behavior.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <param name="oyunID">The game ID being played (optional).</param>
        /// <returns>A list of recommended products with personalized message.</returns>
        Dictionary<Urunler, string> UpsellOneriGetir(int uyeID, int? oyunID = null);
    }
}


