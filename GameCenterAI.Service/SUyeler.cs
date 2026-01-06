using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Interface;

namespace GameCenterAI.Service
{
    /// <summary>
    /// Service class for member (Uye) operations.
    /// </summary>
    public class SUyeler : IUyeler
    {
        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <param name="kullaniciAdi">The username.</param>
        /// <param name="sifre">The password.</param>
        /// <param name="uye">The authenticated member entity if successful, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GirisYap(string kullaniciAdi, string sifre, out Uyeler uye)
        {
            string hata = null;
            uye = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UyeID, AdSoyad, KullaniciAdi, Sifre, FaceEncoding, Bakiye, Durum FROM Uyeler WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre AND Durum = 1";

            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", sifre);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    uye = new Uyeler
                    {
                        UyeID = Convert.ToInt32(reader["UyeID"]),
                        AdSoyad = reader["AdSoyad"].ToString(),
                        KullaniciAdi = reader["KullaniciAdi"].ToString(),
                        Sifre = reader["Sifre"].ToString(),
                        Bakiye = Convert.ToDecimal(reader["Bakiye"]),
                        Durum = Convert.ToBoolean(reader["Durum"])
                    };

                    if (reader["FaceEncoding"] != DBNull.Value)
                    {
                        uye.FaceEncoding = (byte[])reader["FaceEncoding"];
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        /// <param name="uye">The member entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Ekle(Uyeler uye)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Uyeler (AdSoyad, KullaniciAdi, Sifre, FaceEncoding, Bakiye, Durum) VALUES (@AdSoyad, @KullaniciAdi, @Sifre, @FaceEncoding, @Bakiye, @Durum)";

            command.Parameters.AddWithValue("@AdSoyad", uye.AdSoyad);
            command.Parameters.AddWithValue("@KullaniciAdi", uye.KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", uye.Sifre);
            command.Parameters.AddWithValue("@Bakiye", uye.Bakiye);
            command.Parameters.AddWithValue("@Durum", uye.Durum);

            SqlParameter faceParam = new SqlParameter("@FaceEncoding", SqlDbType.VarBinary, -1);
            if (uye.FaceEncoding != null && uye.FaceEncoding.Length > 0)
            {
                faceParam.Value = uye.FaceEncoding;
            }
            else
            {
                faceParam.Value = DBNull.Value;
            }
            command.Parameters.Add(faceParam);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Kayıt işlemi başarısız oldu.";
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Lists all members.
        /// </summary>
        /// <param name="uyeler">The list of all members.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Listele(out List<Uyeler> uyeler)
        {
            string hata = null;
            uyeler = new List<Uyeler>();
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UyeID, AdSoyad, KullaniciAdi, Sifre, FaceEncoding, Bakiye, Durum FROM Uyeler WHERE Durum = 1 ORDER BY AdSoyad";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Uyeler uye = new Uyeler
                    {
                        UyeID = Convert.ToInt32(reader["UyeID"]),
                        AdSoyad = reader["AdSoyad"].ToString(),
                        KullaniciAdi = reader["KullaniciAdi"].ToString(),
                        Sifre = reader["Sifre"].ToString(),
                        Bakiye = Convert.ToDecimal(reader["Bakiye"]),
                        Durum = Convert.ToBoolean(reader["Durum"])
                    };

                    if (reader["FaceEncoding"] != DBNull.Value)
                    {
                        uye.FaceEncoding = (byte[])reader["FaceEncoding"];
                    }

                    uyeler.Add(uye);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="uyeId">The member ID.</param>
        /// <param name="uye">The member entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int uyeId, out Uyeler uye)
        {
            string hata = null;
            uye = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UyeID, AdSoyad, KullaniciAdi, Sifre, FaceEncoding, Bakiye, Durum FROM Uyeler WHERE UyeID = @UyeID";

            command.Parameters.AddWithValue("@UyeID", uyeId);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    uye = new Uyeler
                    {
                        UyeID = Convert.ToInt32(reader["UyeID"]),
                        AdSoyad = reader["AdSoyad"].ToString(),
                        KullaniciAdi = reader["KullaniciAdi"].ToString(),
                        Sifre = reader["Sifre"].ToString(),
                        Bakiye = Convert.ToDecimal(reader["Bakiye"]),
                        Durum = Convert.ToBoolean(reader["Durum"])
                    };

                    if (reader["FaceEncoding"] != DBNull.Value)
                    {
                        uye.FaceEncoding = (byte[])reader["FaceEncoding"];
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        /// <param name="uye">The member entity to update.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Guncelle(Uyeler uye)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Uyeler SET AdSoyad = @AdSoyad, KullaniciAdi = @KullaniciAdi, Sifre = @Sifre, FaceEncoding = @FaceEncoding, Bakiye = @Bakiye, Durum = @Durum WHERE UyeID = @UyeID";

            command.Parameters.AddWithValue("@UyeID", uye.UyeID);
            command.Parameters.AddWithValue("@AdSoyad", uye.AdSoyad);
            command.Parameters.AddWithValue("@KullaniciAdi", uye.KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", uye.Sifre);
            command.Parameters.AddWithValue("@Bakiye", uye.Bakiye);
            command.Parameters.AddWithValue("@Durum", uye.Durum);

            SqlParameter faceParam = new SqlParameter("@FaceEncoding", SqlDbType.VarBinary, -1);
            if (uye.FaceEncoding != null && uye.FaceEncoding.Length > 0)
            {
                faceParam.Value = uye.FaceEncoding;
            }
            else
            {
                faceParam.Value = DBNull.Value;
            }
            command.Parameters.Add(faceParam);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Güncelleme işlemi başarısız oldu.";
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }
    }
}
