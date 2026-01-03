using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents an order detail (SiparisDetay) entity in the database.
    /// </summary>
    public class SiparisDetaylar
    {
        private int _siparisDetayID;
        private int _siparisID;
        private int _urunID;
        private int _adet;
        private decimal _birimFiyat;
        private decimal _toplamFiyat;

        /// <summary>
        /// Gets or sets the order detail ID.
        /// </summary>
        public int SiparisDetayID
        {
            get { return _siparisDetayID; }
            set { _siparisDetayID = value; }
        }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int SiparisID
        {
            get { return _siparisID; }
            set { _siparisID = value; }
        }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int UrunID
        {
            get { return _urunID; }
            set { _urunID = value; }
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Adet
        {
            get { return _adet; }
            set { _adet = value; }
        }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal BirimFiyat
        {
            get { return _birimFiyat; }
            set { _birimFiyat = value; }
        }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        public decimal ToplamFiyat
        {
            get { return _toplamFiyat; }
            set { _toplamFiyat = value; }
        }
    }
}

