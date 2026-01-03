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
        /// <returns>The created order ID.</returns>
        int Olustur(Siparisler siparis);

        /// <summary>
        /// Gets orders by transaction ID.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>A list of orders.</returns>
        List<Siparisler> GetirByHareketID(int hareketID);

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        List<Siparisler> Listele();

        /// <summary>
        /// Gets order details by order ID.
        /// </summary>
        /// <param name="siparisID">The order ID.</param>
        /// <returns>A list of order details.</returns>
        List<SiparisDetaylar> GetDetaylar(int siparisID);

        /// <summary>
        /// Adds an order detail.
        /// </summary>
        /// <param name="detay">The order detail entity.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool DetayEkle(SiparisDetaylar detay);
    }
}

