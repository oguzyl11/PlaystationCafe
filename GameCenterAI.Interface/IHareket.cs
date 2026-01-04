using System;
using System.Collections.Generic;
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
        /// <returns>The created transaction ID.</returns>
        int Baslat(Hareketler hareket);

        /// <summary>
        /// Ends a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Bitir(int hareketID);

        /// <summary>
        /// Gets active transaction for a table.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The active transaction entity.</returns>
        Hareketler GetirAktifByMasaID(int masaID);

        /// <summary>
        /// Calculates the fee for a transaction based on elapsed time.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The calculated fee.</returns>
        decimal UcretHesapla(int hareketID);

        /// <summary>
        /// Gets elapsed time for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The elapsed time in minutes.</returns>
        int GecenSureGetir(int hareketID);

        /// <summary>
        /// Updates the order total for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="siparisToplami">The order total amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool SiparisToplamiGuncelle(int hareketID, decimal siparisToplami);

        /// <summary>
        /// Updates the prepaid amount for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="pesinAlinan">The prepaid amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool PesinAlinanGuncelle(int hareketID, decimal pesinAlinan);

        /// <summary>
        /// Updates the tariff for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="tarifeID">The new tariff ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool TarifeGuncelle(int hareketID, int tarifeID);

        /// <summary>
        /// Updates the game ID for a transaction.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <param name="oyunID">The game ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool OyunGuncelle(int hareketID, int? oyunID);
    }
}

