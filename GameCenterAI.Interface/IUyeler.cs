using System.Collections.Generic;
using GameCenterAI.Entity;

namespace GameCenterAI.Interface
{
    /// <summary>
    /// Interface for member (Uye) service operations.
    /// </summary>
    public interface IUyeler
    {
        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <param name="kullaniciAdi">The username.</param>
        /// <param name="sifre">The password.</param>
        /// <returns>The authenticated member entity if successful, null otherwise.</returns>
        Uyeler GirisYap(string kullaniciAdi, string sifre);

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        /// <param name="uye">The member entity to add.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Ekle(Uyeler uye);

        /// <summary>
        /// Lists all members.
        /// </summary>
        /// <returns>A list of all members.</returns>
        List<Uyeler> Listele();

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="uyeID">The member ID.</param>
        /// <returns>The member entity.</returns>
        Uyeler Getir(int uyeID);

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        /// <param name="uye">The member entity to update.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        bool Guncelle(Uyeler uye);
    }
}
