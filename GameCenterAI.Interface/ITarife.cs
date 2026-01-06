using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for tariff (Tarife) service operations.
    /// </summary>
    public interface ITarife
    {
        /// <summary>
        /// Gets all tariffs.
        /// </summary>
        /// <param name="tarifeler">The list of all tariffs.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Tarifeler> tarifeler);

        /// <summary>
        /// Gets a tariff by ID.
        /// </summary>
        /// <param name="tarifeId">The tariff ID.</param>
        /// <param name="tarife">The tariff entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int tarifeId, out Tarifeler tarife);
    }
}
