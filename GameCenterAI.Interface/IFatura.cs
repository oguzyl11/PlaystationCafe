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
        /// <returns>The created invoice ID.</returns>
        int Olustur(Faturalar fatura);

        /// <summary>
        /// Gets an invoice by ID.
        /// </summary>
        /// <param name="faturaID">The invoice ID.</param>
        /// <returns>The invoice entity.</returns>
        Faturalar Getir(int faturaID);

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns>A list of all invoices.</returns>
        List<Faturalar> Listele();

        /// <summary>
        /// Gets invoices by date range.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A list of invoices in the date range.</returns>
        List<Faturalar> TarihAraligindaGetir(DateTime baslangic, DateTime bitis);

        /// <summary>
        /// Gets invoice by transaction ID.
        /// </summary>
        /// <param name="hareketID">The transaction ID.</param>
        /// <returns>The invoice entity if found, null otherwise.</returns>
        Faturalar HareketIDyeGoreGetir(int hareketID);

        /// <summary>
        /// Generates a unique invoice number.
        /// </summary>
        /// <returns>A unique invoice number.</returns>
        string FaturaNoOlustur();

        /// <summary>
        /// Updates invoice status.
        /// </summary>
        /// <param name="faturaID">The invoice ID.</param>
        /// <param name="durum">The new status.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool DurumGuncelle(int faturaID, string durum);
    }
}

