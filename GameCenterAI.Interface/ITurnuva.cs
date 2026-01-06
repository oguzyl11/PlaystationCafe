using System;
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
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Olustur(Turnuvalar turnuva);

        /// <summary>
        /// Lists all tournaments.
        /// </summary>
        /// <param name="turnuvalar">The list of all tournaments.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Turnuvalar> turnuvalar);

        /// <summary>
        /// Gets a tournament by ID.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="turnuva">The tournament entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int turnuvaId, out Turnuvalar turnuva);

        /// <summary>
        /// Updates an existing tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Guncelle(Turnuvalar turnuva);

        /// <summary>
        /// Deletes a tournament.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID to delete.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Sil(int turnuvaId);

        /// <summary>
        /// Deletes tournament matches by tournament ID.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string TurnuvaMaclariSil(int turnuvaId);

        /// <summary>
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random users and creates pairs for Quarter Finals.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="eslestirmeler">A list of paired user IDs (each pair represents a match).</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string EslestirmeleriOlustur(int turnuvaId, out List<KeyValuePair<int, int>> eslestirmeler);

        /// <summary>
        /// Creates tournament matches in database from pairings.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="eslestirmeler">List of pairings.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string MaclariOlustur(int turnuvaId, List<KeyValuePair<int, int>> eslestirmeler);

        /// <summary>
        /// Gets all matches for a tournament.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="maclar">The list of tournament matches.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string MaclariGetir(int turnuvaId, out List<TurnuvaMaclari> maclar);

        /// <summary>
        /// Gets matches for a specific round.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="tur">The round name (Çeyrek Final, Yarı Final, Final).</param>
        /// <param name="maclar">The list of matches for the round.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string MaclariGetirByTur(int turnuvaId, string tur, out List<TurnuvaMaclari> maclar);

        /// <summary>
        /// Saves match result (score).
        /// </summary>
        /// <param name="mac">The match entity with results.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string MacSonucuKaydet(TurnuvaMaclari mac);

        /// <summary>
        /// Gets a match by ID.
        /// </summary>
        /// <param name="macId">The match ID.</param>
        /// <param name="mac">The match entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string MacGetir(int macId, out TurnuvaMaclari mac);

        /// <summary>
        /// Advances winners to the next round and creates new matches.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="tur">The current round name.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string SonrakiTuraGec(int turnuvaId, string tur);

        /// <summary>
        /// Finalizes the tournament when the final match is completed.
        /// Updates tournament status to "Tamamlandı" and awards prize to winner.
        /// </summary>
        /// <param name="turnuvaId">The tournament ID.</param>
        /// <param name="kazananId">The winner's user ID, or null if tournament is not completed.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string TurnuvayiTamamla(int turnuvaId, out int? kazananId);
    }
}
