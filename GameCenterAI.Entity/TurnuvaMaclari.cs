using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a tournament match (Turnuva Maci) entity in the database.
    /// </summary>
    public class TurnuvaMaclari
    {
        private int _macID;
        private int _turnuvaID;
        private int _uye1ID;
        private int _uye2ID;
        private int? _skor1;
        private int? _skor2;
        private string _tur;
        private int _macNo;
        private string _durum;
        private int? _kazananID;
        private DateTime? _macTarihi;

        /// <summary>
        /// Gets or sets the match ID.
        /// </summary>
        public int MacID
        {
            get { return _macID; }
            set { _macID = value; }
        }

        /// <summary>
        /// Gets or sets the tournament ID.
        /// </summary>
        public int TurnuvaID
        {
            get { return _turnuvaID; }
            set { _turnuvaID = value; }
        }

        /// <summary>
        /// Gets or sets the first player's user ID.
        /// </summary>
        public int Uye1ID
        {
            get { return _uye1ID; }
            set { _uye1ID = value; }
        }

        /// <summary>
        /// Gets or sets the second player's user ID.
        /// </summary>
        public int Uye2ID
        {
            get { return _uye2ID; }
            set { _uye2ID = value; }
        }

        /// <summary>
        /// Gets or sets the first player's score.
        /// </summary>
        public int? Skor1
        {
            get { return _skor1; }
            set { _skor1 = value; }
        }

        /// <summary>
        /// Gets or sets the second player's score.
        /// </summary>
        public int? Skor2
        {
            get { return _skor2; }
            set { _skor2 = value; }
        }

        /// <summary>
        /// Gets or sets the round name (Çeyrek Final, Yarı Final, Final).
        /// </summary>
        public string Tur
        {
            get { return _tur; }
            set { _tur = value; }
        }

        /// <summary>
        /// Gets or sets the match number within the round.
        /// </summary>
        public int MacNo
        {
            get { return _macNo; }
            set { _macNo = value; }
        }

        /// <summary>
        /// Gets or sets the match status (Beklemede, Oynandı, Sonuçlandı).
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// Gets or sets the winner's user ID.
        /// </summary>
        public int? KazananID
        {
            get { return _kazananID; }
            set { _kazananID = value; }
        }

        /// <summary>
        /// Gets or sets the match date.
        /// </summary>
        public DateTime? MacTarihi
        {
            get { return _macTarihi; }
            set { _macTarihi = value; }
        }
    }
}

