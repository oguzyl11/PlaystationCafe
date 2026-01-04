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
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Olustur(Turnuvalar turnuva);

        /// <summary>
        /// Lists all tournaments.
        /// </summary>
        /// <returns>A list of all tournaments.</returns>
        List<Turnuvalar> Listele();

        /// <summary>
        /// Gets a tournament by ID.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>The tournament entity.</returns>
        Turnuvalar Getir(int turnuvaID);

        /// <summary>
        /// Updates an existing tournament.
        /// </summary>
        /// <param name="turnuva">The tournament entity to update.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Guncelle(Turnuvalar turnuva);

        /// <summary>
        /// Deletes a tournament.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID to delete.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Sil(int turnuvaID);

        /// <summary>
        /// Creates tournament pairings (bracket) for a tournament.
        /// Fetches random users and creates pairs for Quarter Finals.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of paired user IDs (each pair represents a match).</returns>
        List<KeyValuePair<int, int>> EslestirmeleriOlustur(int turnuvaID);

        /// <summary>
        /// Creates tournament matches in database from pairings.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="eslestirmeler">List of pairings.</param>
        /// <returns>True if successful.</returns>
        bool MaclariOlustur(int turnuvaID, List<KeyValuePair<int, int>> eslestirmeler);

        /// <summary>
        /// Gets all matches for a tournament.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>A list of tournament matches.</returns>
        List<TurnuvaMaclari> MaclariGetir(int turnuvaID);

        /// <summary>
        /// Gets matches for a specific round.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="tur">The round name (Çeyrek Final, Yarı Final, Final).</param>
        /// <returns>A list of matches for the round.</returns>
        List<TurnuvaMaclari> MaclariGetirByTur(int turnuvaID, string tur);

        /// <summary>
        /// Saves match result (score).
        /// </summary>
        /// <param name="mac">The match entity with results.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool MacSonucuKaydet(TurnuvaMaclari mac);

        /// <summary>
        /// Gets a match by ID.
        /// </summary>
        /// <param name="macID">The match ID.</param>
        /// <returns>The match entity.</returns>
        TurnuvaMaclari MacGetir(int macID);

        /// <summary>
        /// Advances winners to the next round and creates new matches.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <param name="tur">The current round name.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool SonrakiTuraGec(int turnuvaID, string tur);

        /// <summary>
        /// Finalizes the tournament when the final match is completed.
        /// Updates tournament status to "Tamamlandı" and awards prize to winner.
        /// </summary>
        /// <param name="turnuvaID">The tournament ID.</param>
        /// <returns>The winner's user ID, or null if tournament is not completed.</returns>
        int? TurnuvayiTamamla(int turnuvaID);
    }
}
