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
        /// <param name="masalar">The list of all tables.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetAllMasalar(out List<Masalar> masalar);

        /// <summary>
        /// Updates the status of a table.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string DurumGuncelle(int masaId, string durum);

        /// <summary>
        /// Adds a new table to the database.
        /// </summary>
        /// <param name="masa">The table entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Ekle(Masalar masa);

        /// <summary>
        /// Updates an existing table.
        /// </summary>
        /// <param name="masa">The table entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Guncelle(Masalar masa);

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <param name="masaId">The table ID to delete.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Sil(int masaId);

        /// <summary>
        /// Gets a table by ID.
        /// </summary>
        /// <param name="masaId">The table ID.</param>
        /// <param name="masa">The table entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int masaId, out Masalar masa);
    }
}
