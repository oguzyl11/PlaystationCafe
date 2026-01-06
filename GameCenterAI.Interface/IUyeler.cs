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
        /// <param name="uye">The authenticated member entity if successful, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string GirisYap(string kullaniciAdi, string sifre, out Uyeler uye);

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        /// <param name="uye">The member entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Ekle(Uyeler uye);

        /// <summary>
        /// Lists all members.
        /// </summary>
        /// <param name="uyeler">The list of all members.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Listele(out List<Uyeler> uyeler);

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="uyeId">The member ID.</param>
        /// <param name="uye">The member entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Getir(int uyeId, out Uyeler uye);

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        /// <param name="uye">The member entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        string Guncelle(Uyeler uye);
    }
}
