using System;
using System.Collections.Generic;
using System.Data;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for report/accounting operations.
    /// </summary>
    public interface IRapor
    {
        /// <summary>
        /// Gets daily financial report.
        /// </summary>
        /// <param name="tarih">The date for the report.</param>
        /// <returns>A DataTable containing daily report data.</returns>
        DataTable GunlukRapor(DateTime tarih);

        /// <summary>
        /// Gets monthly financial report.
        /// </summary>
        /// <param name="yil">The year.</param>
        /// <param name="ay">The month.</param>
        /// <returns>A DataTable containing monthly report data.</returns>
        DataTable AylikRapor(int yil, int ay);

        /// <summary>
        /// Gets member-based report.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A DataTable containing member report data.</returns>
        DataTable UyeRaporu(DateTime baslangic, DateTime bitis);

        /// <summary>
        /// Gets table-based report.
        /// </summary>
        /// <param name="baslangic">Start date.</param>
        /// <param name="bitis">End date.</param>
        /// <returns>A DataTable containing table report data.</returns>
        DataTable MasaRaporu(DateTime baslangic, DateTime bitis);
    }
}

