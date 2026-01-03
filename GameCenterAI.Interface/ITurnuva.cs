using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for tournament service operations.
    /// </summary>
    public interface ITurnuva
    {
        /// <summary>
        /// Creates a new tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to create.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Olustur(Turnuvalar turnuva);

        /// <summary>
        /// Lists all tournaments.
        /// </summary>
        /// <returns>A list of all tournaments.</returns>
        List<Turnuvalar> Listele();

        /// <summary>
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random users and creates pairs for Quarter Finals.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of paired user IDs (each pair represents a match).</returns>
        List<KeyValuePair<int, int>> EslestirmeleriOlustur(int turnuvaID);
    }
}
