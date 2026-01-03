using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a member (Uye) entity in the database.
    /// </summary>
    public class Uyeler
    {
        private int _uyeID;
        private string _adSoyad;
        private string _kullaniciAdi;
        private string _sifre;
        private byte[] _faceEncoding;
        private decimal _bakiye;
        private bool _durum;

        /// <summary>
        /// Gets or sets the member ID.
        /// </summary>
        public int UyeID
        {
            get { return _uyeID; }
            set { _uyeID = value; }
        }

        /// <summary>
        /// Gets or sets the full name of the member.
        /// </summary>
        public string AdSoyad
        {
            get { return _adSoyad; }
            set { _adSoyad = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string KullaniciAdi
        {
            get { return _kullaniciAdi; }
            set { _kullaniciAdi = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Sifre
        {
            get { return _sifre; }
            set { _sifre = value; }
        }

        /// <summary>
        /// Gets or sets the face encoding data for FaceID recognition.
        /// </summary>
        public byte[] FaceEncoding
        {
            get { return _faceEncoding; }
            set { _faceEncoding = value; }
        }

        /// <summary>
        /// Gets or sets the balance of the member.
        /// </summary>
        public decimal Bakiye
        {
            get { return _bakiye; }
            set { _bakiye = value; }
        }

        /// <summary>
        /// Gets or sets the status of the member (active/inactive).
        /// </summary>
        public bool Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}


