using System;
using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for note/appointment (Not) service operations.
    /// </summary>
    public interface INot
    {
        /// <summary>
        /// Gets notes by table ID.
        /// </summary>
        /// <param name="masaID">The table ID.</param>
        /// <returns>A list of notes.</returns>
        List<Notlar> GetirByMasaID(int masaID);

        /// <summary>
        /// Gets notes by date.
        /// </summary>
        /// <param name="tarih">The date.</param>
        /// <returns>A list of notes.</returns>
        List<Notlar> GetirByTarih(DateTime tarih);

        /// <summary>
        /// Adds a new note.
        /// </summary>
        /// <param name="not">The note entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Ekle(Notlar not);
    }
}

