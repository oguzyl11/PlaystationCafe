using System;

namespace GameCenterAI.Entity
{
    /// <summary>
    /// Represents a game (Oyun) entity in the database.
    /// </summary>
    public class Oyunlar
    {
        private int _oyunID;
        private string _oyunAdi;
        private string _kategori;
        private string _platform;

        /// <summary>
        /// Gets or sets the game ID.
        /// </summary>
        public int OyunID
        {
            get { return _oyunID; }
            set { _oyunID = value; }
        }

        /// <summary>
        /// Gets or sets the game name.
        /// </summary>
        public string OyunAdi
        {
            get { return _oyunAdi; }
            set { _oyunAdi = value; }
        }

        /// <summary>
        /// Gets or sets the game category.
        /// </summary>
        public string Kategori
        {
            get { return _kategori; }
            set { _kategori = value; }
        }

        /// <summary>
        /// Gets or sets the game platform.
        /// </summary>
        public string Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }
    }
}


