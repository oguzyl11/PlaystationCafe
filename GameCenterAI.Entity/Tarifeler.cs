using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a tariff (Tarife) entity in the database.
    /// </summary>
    public class Tarifeler
    {
        private int _tarifeID;
        private string _tarifeAdi;
        private decimal _saatlikUcret;
        private int _sureSiniri;
        private string _durum;

        /// <summary>
        /// Gets or sets the tariff ID.
        /// </summary>
        public int TarifeID
        {
            get { return _tarifeID; }
            set { _tarifeID = value; }
        }

        /// <summary>
        /// Gets or sets the tariff name.
        /// </summary>
        public string TarifeAdi
        {
            get { return _tarifeAdi; }
            set { _tarifeAdi = value; }
        }

        /// <summary>
        /// Gets or sets the hourly rate.
        /// </summary>
        public decimal SaatlikUcret
        {
            get { return _saatlikUcret; }
            set { _saatlikUcret = value; }
        }

        /// <summary>
        /// Gets or sets the time limit in minutes.
        /// </summary>
        public int SureSiniri
        {
            get { return _sureSiniri; }
            set { _sureSiniri = value; }
        }

        /// <summary>
        /// Gets or sets the status of the tariff.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}

