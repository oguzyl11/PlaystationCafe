using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for order (Siparis) service operations.
    /// </summary>
    public interface ISiparis
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="siparis">The order entity to create.</param>
        /// <param name="siparisId">The created order ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Olustur(Siparisler siparis, out int siparisId);

        /// <summary>
        /// Gets orders by transaction ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="siparisler">The list of orders.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetirByHareketID(int hareketId, out List<Siparisler> siparisler);

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="siparisler">The list of all orders.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Siparisler> siparisler);

        /// <summary>
        /// Gets order details by order ID.
        /// </summary>
        /// <param name="siparisId">The order ID.</param>
        /// <param name="detaylar">The list of order details.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetDetaylar(int siparisId, out List<SiparisDetaylar> detaylar);

        /// <summary>
        /// Adds an order detail.
        /// </summary>
        /// <param name="detay">The order detail entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string DetayEkle(SiparisDetaylar detay);
    }
}
