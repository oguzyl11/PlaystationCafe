using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a table/desk (Masa) entity in the database.
    /// </summary>
    public class Masalar
    {
        private int _masaID;
        private string _masaAdi;
        private decimal _saatlikUcret;
        private string _durum;

        /// <summary>
        /// Gets or sets the table ID.
        /// </summary>
        public int MasaID
        {
            get { return _masaID; }
            set { _masaID = value; }
        }

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string MasaAdi
        {
            get { return _masaAdi; }
            set { _masaAdi = value; }
        }

        /// <summary>
        /// Gets or sets the hourly rate for the table.
        /// </summary>
        public decimal SaatlikUcret
        {
            get { return _saatlikUcret; }
            set { _saatlikUcret = value; }
        }

        /// <summary>
        /// Gets or sets the status of the table.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}


