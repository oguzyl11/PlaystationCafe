using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a tournament (Turnuva) entity in the database.
    /// </summary>
    public class Turnuvalar
    {
        private int _turnuvaID;
        private string _turnuvaAdi;
        private DateTime _baslangicTarihi;
        private decimal _odul;
        private string _durum;

        /// <summary>
        /// Gets or sets the tournament ID.
        /// </summary>
        public int TurnuvaID
        {
            get { return _turnuvaID; }
            set { _turnuvaID = value; }
        }

        /// <summary>
        /// Gets or sets the tournament name.
        /// </summary>
        public string TurnuvaAdi
        {
            get { return _turnuvaAdi; }
            set { _turnuvaAdi = value; }
        }

        /// <summary>
        /// Gets or sets the start date of the tournament.
        /// </summary>
        public DateTime BaslangicTarihi
        {
            get { return _baslangicTarihi; }
            set { _baslangicTarihi = value; }
        }

        /// <summary>
        /// Gets or sets the prize amount for the tournament.
        /// </summary>
        public decimal Odul
        {
            get { return _odul; }
            set { _odul = value; }
        }

        /// <summary>
        /// Gets or sets the status of the tournament.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}


