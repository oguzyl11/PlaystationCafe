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
        /// <returns>True if faces match, false otherwise.</returns>
        bool CompareFaces(byte[] storedFace, byte[] currentFace);

        /// <summary>
        /// Gets game recommendations for a user using ML.NET.
        /// </summary>
        /// <param name="uyeID">The user ID.</param>
        /// <returns>A list of recommended games based on ML model.</returns>
        List<Oyunlar> OyunOner(int uyeID);
    }
}


