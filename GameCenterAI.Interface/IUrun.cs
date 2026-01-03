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
        /// <returns>A list of all products.</returns>
        List<Urunler> Listele();

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="kategori">The category name.</param>
        /// <returns>A list of products in the category.</returns>
        List<Urunler> GetirByKategori(string kategori);

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <returns>The product entity.</returns>
        Urunler Getir(int urunID);

        /// <summary>
        /// Gets a product by name.
        /// </summary>
        /// <param name="urunAdi">The product name.</param>
        /// <returns>The product entity if found, null otherwise.</returns>
        Urunler GetirByUrunAdi(string urunAdi);

        /// <summary>
        /// Updates stock for a product.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <param name="yeniStok">The new stock amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool StokGuncelle(int urunID, int yeniStok);

        /// <summary>
        /// Updates price for a product.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <param name="yeniFiyat">The new price.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool FiyatGuncelle(int urunID, decimal yeniFiyat);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="urun">The product entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Ekle(Urunler urun);
    }
}

