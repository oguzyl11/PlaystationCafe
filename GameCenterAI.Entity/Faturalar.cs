using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents an invoice (Fatura) entity in the database.
    /// </summary>
    public class Faturalar
    {
        private int _faturaID;
        private int _hareketID;
        private string _faturaNo;
        private DateTime _faturaTarihi;
        private decimal _toplamTutar;
        private decimal _kdvOrani;
        private decimal _kdvTutari;
        private decimal _genelToplam;
        private string _durum;
        private string _notlar;

        /// <summary>
        /// Gets or sets the invoice ID.
        /// </summary>
        public int FaturaID
        {
            get { return _faturaID; }
            set { _faturaID = value; }
        }

        /// <summary>
        /// Gets or sets the transaction ID associated with this invoice.
        /// </summary>
        public int HareketID
        {
            get { return _hareketID; }
            set { _hareketID = value; }
        }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        public string FaturaNo
        {
            get { return _faturaNo; }
            set { _faturaNo = value; }
        }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        public DateTime FaturaTarihi
        {
            get { return _faturaTarihi; }
            set { _faturaTarihi = value; }
        }

        /// <summary>
        /// Gets or sets the total amount (before tax).
        /// </summary>
        public decimal ToplamTutar
        {
            get { return _toplamTutar; }
            set { _toplamTutar = value; }
        }

        /// <summary>
        /// Gets or sets the VAT rate (KDV Oranı).
        /// </summary>
        public decimal KdvOrani
        {
            get { return _kdvOrani; }
            set { _kdvOrani = value; }
        }

        /// <summary>
        /// Gets or sets the VAT amount (KDV Tutarı).
        /// </summary>
        public decimal KdvTutari
        {
            get { return _kdvTutari; }
            set { _kdvTutari = value; }
        }

        /// <summary>
        /// Gets or sets the grand total (including tax).
        /// </summary>
        public decimal GenelToplam
        {
            get { return _genelToplam; }
            set { _genelToplam = value; }
        }

        /// <summary>
        /// Gets or sets the invoice status.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// Gets or sets the notes/comments for the invoice.
        /// </summary>
        public string Notlar
        {
            get { return _notlar; }
            set { _notlar = value; }
        }
    }
}

