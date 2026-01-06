using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for product (Urun) service operations.
    /// </summary>
    public interface IUrun
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <param name="urunler">The list of all products.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Urunler> urunler);

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="kategori">The category name.</param>
        /// <param name="urunler">The list of products in the category.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetirByKategori(string kategori, out List<Urunler> urunler);

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="urun">The product entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int urunId, out Urunler urun);

        /// <summary>
        /// Gets a product by name.
        /// </summary>
        /// <param name="urunAdi">The product name.</param>
        /// <param name="urun">The product entity if found, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GetirByUrunAdi(string urunAdi, out Urunler urun);

        /// <summary>
        /// Updates stock for a product.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="yeniStok">The new stock amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string StokGuncelle(int urunId, int yeniStok);

        /// <summary>
        /// Updates price for a product.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="yeniFiyat">The new price.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string FiyatGuncelle(int urunId, decimal yeniFiyat);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="urun">The product entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Ekle(Urunler urun);
    }
}
