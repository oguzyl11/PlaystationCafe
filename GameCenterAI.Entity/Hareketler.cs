using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a transaction/movement (Hareket) entity in the database.
    /// </summary>
    public class Hareketler
    {
        private int _hareketID;
        private int _uyeID;
        private int _masaID;
        private int? _tarifeID;
        private DateTime _baslangic;
        private DateTime? _bitis;
        private decimal _ucret;
        private decimal _pesinAlinan;
        private decimal _siparisToplami;
        private string _durum;

        /// <summary>
        /// Gets or sets the transaction ID.
        /// </summary>
        public int HareketID
        {
            get { return _hareketID; }
            set { _hareketID = value; }
        }

        /// <summary>
        /// Gets or sets the member ID associated with the transaction.
        /// </summary>
        public int UyeID
        {
            get { return _uyeID; }
            set { _uyeID = value; }
        }

        /// <summary>
        /// Gets or sets the table ID associated with the transaction.
        /// </summary>
        public int MasaID
        {
            get { return _masaID; }
            set { _masaID = value; }
        }

        /// <summary>
        /// Gets or sets the start time of the transaction.
        /// </summary>
        public DateTime Baslangic
        {
            get { return _baslangic; }
            set { _baslangic = value; }
        }

        /// <summary>
        /// Gets or sets the end time of the transaction (nullable).
        /// </summary>
        public DateTime? Bitis
        {
            get { return _bitis; }
            set { _bitis = value; }
        }

        /// <summary>
        /// Gets or sets the fee charged for the transaction.
        /// </summary>
        public decimal Ucret
        {
            get { return _ucret; }
            set { _ucret = value; }
        }

        /// <summary>
        /// Gets or sets the tariff ID.
        /// </summary>
        public int? TarifeID
        {
            get { return _tarifeID; }
            set { _tarifeID = value; }
        }

        /// <summary>
        /// Gets or sets the advance payment amount.
        /// </summary>
        public decimal PesinAlinan
        {
            get { return _pesinAlinan; }
            set { _pesinAlinan = value; }
        }

        /// <summary>
        /// Gets or sets the total order amount.
        /// </summary>
        public decimal SiparisToplami
        {
            get { return _siparisToplami; }
            set { _siparisToplami = value; }
        }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}


