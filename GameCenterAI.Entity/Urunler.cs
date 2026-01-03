using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a product (Urun) entity in the database.
    /// </summary>
    public class Urunler
    {
        private int _urunID;
        private string _urunAdi;
        private string _kategori;
        private decimal _fiyat;
        private int _stok;
        private string _durum;

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int UrunID
        {
            get { return _urunID; }
            set { _urunID = value; }
        }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string UrunAdi
        {
            get { return _urunAdi; }
            set { _urunAdi = value; }
        }

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        public string Kategori
        {
            get { return _kategori; }
            set { _kategori = value; }
        }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Fiyat
        {
            get { return _fiyat; }
            set { _fiyat = value; }
        }

        /// <summary>
        /// Gets or sets the stock quantity.
        /// </summary>
        public int Stok
        {
            get { return _stok; }
            set { _stok = value; }
        }

        /// <summary>
        /// Gets or sets the status of the product.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}

