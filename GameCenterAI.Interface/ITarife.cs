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
        /// <returns>A list of all tariffs.</returns>
        List<Tarifeler> Listele();

        /// <summary>
        /// Gets a tariff by ID.
        /// </summary>
        /// <param name="tarifeID">The tariff ID.</param>
        /// <returns>The tariff entity.</returns>
        Tarifeler Getir(int tarifeID);
    }
}

