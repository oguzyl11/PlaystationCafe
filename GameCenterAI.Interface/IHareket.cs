using System;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for transaction/movement (Hareket) service operations.
    /// </summary>
    public interface IHareket
    {
        /// <summary>
        /// Starts a new transaction for a table.
        /// </summary>
        /// <param name="hareket">The transaction entity.</param>
        /// <param name="hareketId">The created transaction ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Baslat(Hareketler hareket, out int hareketId);

        /// <summary>
        /// Ends a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Bitir(int hareketId);

        /// <summary>
        /// Gets a transaction by ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="hareket">The transaction entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int hareketId, out Hareketler hareket);

        /// <summary>
        /// Gets active transaction for a table.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="hareket">The active transaction entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetirAktifByMasaID(int masaId, out Hareketler hareket);

        /// <summary>
        /// Calculates the fee for a transaction based on elapsed time.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="ucret">The calculated fee.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string UcretHesapla(int hareketId, out decimal ucret);

        /// <summary>
        /// Gets elapsed time for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="dakika">The elapsed time in minutes.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GecenSureGetir(int hareketId, out int dakika);

        /// <summary>
        /// Updates the order total for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="siparisToplami">The order total amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string SiparisToplamiGuncelle(int hareketId, decimal siparisToplami);

        /// <summary>
        /// Updates the prepaid amount for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="pesinAlinan">The prepaid amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string PesinAlinanGuncelle(int hareketId, decimal pesinAlinan);

        /// <summary>
        /// Updates the tariff for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="tarifeId">The new tariff ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string TarifeGuncelle(int hareketId, int tarifeId);

        /// <summary>
        /// Updates the game ID for a transaction.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="oyunId">The game ID (nullable).</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string OyunGuncelle(int hareketId, int? oyunId);
    }
}
