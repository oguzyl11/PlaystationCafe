using System;
using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for invoice operations.
    /// </summary>
    public interface IFatura
    {
        /// <summary>
        /// Creates a new invoice from a transaction.
        /// </summary>
        /// <param name="fatura">The invoice entity to create.</param>
        /// <param name="faturaId">The created invoice ID.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Olustur(Faturalar fatura, out int faturaId);

        /// <summary>
        /// Gets an invoice by ID.
        /// </summary>
        /// <param name="faturaId">The invoice ID.</param>
        /// <param name="fatura">The invoice entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int faturaId, out Faturalar fatura);

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <param name="faturalar">The list of all invoices.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Faturalar> faturalar);

        /// <summary>
        /// Gets invoices by date range.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <param name="faturalar">The list of invoices in the date range.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string TarihAraligindaGetir(DateTime baslangic, DateTime bitis, out List<Faturalar> faturalar);

        /// <summary>
        /// Gets invoice by transaction ID.
        /// </summary>
        /// <param name="hareketId">The transaction ID.</param>
        /// <param name="fatura">The invoice entity if found, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string HareketIDyeGoreGetir(int hareketId, out Faturalar fatura);

        /// <summary>
        /// Generates a unique invoice number.
        /// </summary>
        /// <param name="faturaNo">A unique invoice number.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string FaturaNoOlustur(out string faturaNo);

        /// <summary>
        /// Updates invoice status.
        /// </summary>
        /// <param name="faturaId">The invoice ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string DurumGuncelle(int faturaId, string durum);
    }
}
