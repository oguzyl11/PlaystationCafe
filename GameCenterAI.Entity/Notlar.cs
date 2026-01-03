using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a note/appointment (Not) entity in the database.
    /// </summary>
    public class Notlar
    {
        private int _notID;
        private int _masaID;
        private DateTime _tarih;
        private string _saat;
        private string _aciklama;
        private string _durum;

        /// <summary>
        /// Gets or sets the note ID.
        /// </summary>
        public int NotID
        {
            get { return _notID; }
            set { _notID = value; }
        }

        /// <summary>
        /// Gets or sets the table ID.
        /// </summary>
        public int MasaID
        {
            get { return _masaID; }
            set { _masaID = value; }
        }

        /// <summary>
        /// Gets or sets the date of the note.
        /// </summary>
        public DateTime Tarih
        {
            get { return _tarih; }
            set { _tarih = value; }
        }

        /// <summary>
        /// Gets or sets the time of the note.
        /// </summary>
        public string Saat
        {
            get { return _saat; }
            set { _saat = value; }
        }

        /// <summary>
        /// Gets or sets the description of the note.
        /// </summary>
        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        /// <summary>
        /// Gets or sets the status of the note.
        /// </summary>
        public string Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }
    }
}

