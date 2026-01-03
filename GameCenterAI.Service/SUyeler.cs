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
        /// <returns>The authenticated member entity if successful, null otherwise.</returns>
        public Uyeler GirisYap(string kullaniciAdi, string sifre)
        {
            Uyeler uye = null;
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
                throw new Exception("Login işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return uye;
        }

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        /// <param name="uye">The member entity to add.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Ekle(Uyeler uye)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Lists all members.
        /// </summary>
        /// <returns>A list of all members.</returns>
        public List<Uyeler> Listele()
        {
            List<Uyeler> uyeler = new List<Uyeler>();
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
                throw new Exception("Üye listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return uyeler;
        }

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="uyeID">The member ID.</param>
        /// <returns>The member entity.</returns>
        public Uyeler Getir(int uyeID)
        {
            Uyeler uye = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UyeID, AdSoyad, KullaniciAdi, Sifre, FaceEncoding, Bakiye, Durum FROM Uyeler WHERE UyeID = @UyeID";

            command.Parameters.AddWithValue("@UyeID", uyeID);

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
                throw new Exception("Üye getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return uye;
        }

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        /// <param name="uye">The member entity to update.</param>
        /// <returns>True if the operation is successful, false otherwise.</returns>
        public bool Guncelle(Uyeler uye)
        {
            bool result = false;
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
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Üye güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}


