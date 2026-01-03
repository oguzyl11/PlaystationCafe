using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for table/desk (Masa) service operations.
    /// </summary>
    public interface IMasalar
    {
        /// <summary>
        /// Gets all tables from the database.
        /// </summary>
        /// <returns>A list of all tables.</returns>
        List<Masalar> GetAllMasalar();

        /// <summary>
        /// Updates the status of a table.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool DurumGuncelle(int masaID, string durum);

        /// <summary>
        /// Adds a new table to the database.
        /// </summary>
        /// <param name="masa">The table entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Ekle(Masalar masa);

        /// <summary>
        /// Updates an existing table.
        /// </summary>
        /// <param name="masa">The table entity to update.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Guncelle(Masalar masa);

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <param name="masaID">The table ID to delete.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Sil(int masaID);

        /// <summary>
        /// Gets a table by ID.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>The table entity.</returns>
        Masalar Getir(int masaID);
    }
}

