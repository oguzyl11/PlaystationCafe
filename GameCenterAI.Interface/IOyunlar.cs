using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for game (Oyun) service operations.
    /// </summary>
    public interface IOyunlar
    {
        /// <summary>
        /// Gets a game by ID.
        /// </summary>
        /// <param name="oyunId">The game ID.</param>
        /// <param name="oyun">The game entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int oyunId, out Oyunlar oyun);
    }
}

