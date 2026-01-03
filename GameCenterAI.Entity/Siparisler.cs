using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents an order (Siparis) entity in the database.
    /// </summary>
    public class Siparisler
    {
        private int _siparisID;
        private int _hareketID;
        private DateTime _siparisTarihi;
        private decimal _toplamTutar;
        private string _durum;

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int SiparisID
        {
            get { return _siparisID; }
            set { _siparisID = value; }
        }

        /// <summary>
        /// Gets or sets the transaction ID associated with the order.
        /// </summary>
        public int HareketID
        {
            get { return _hareketID; }
            set { _hareketID = value; }
        }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        public DateTime SiparisTarihi
        {
            get { return _siparisTarihi; }
            set { _siparisTarihi = value; }
        }

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal ToplamTutar
        {
            get { return _toplamTutar; }
            set { _toplamTutar = value; }
        }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}

