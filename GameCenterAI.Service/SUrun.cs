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
    /// Service class for product (Urun) operations.
    /// </summary>
    public class SUrun : IUrun
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public List<Urunler> Listele()
        {
            List<Urunler> urunler = new List<Urunler>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UrunID, UrunAdi, Kategori, Fiyat, Stok, Durum FROM Urunler WHERE Durum = 'Aktif' ORDER BY Kategori, UrunAdi";

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Urunler urun = new Urunler
                    {
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        UrunAdi = reader["UrunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Fiyat = Convert.ToDecimal(reader["Fiyat"]),
                        Stok = Convert.ToInt32(reader["Stok"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    urunler.Add(urun);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return urunler;
        }

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="kategori">The category name.</param>
        /// <returns>A list of products in the category.</returns>
        public List<Urunler> GetirByKategori(string kategori)
        {
            List<Urunler> urunler = new List<Urunler>();
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UrunID, UrunAdi, Kategori, Fiyat, Stok, Durum FROM Urunler WHERE Kategori = @Kategori AND Durum = 'Aktif' ORDER BY UrunAdi";

            command.Parameters.AddWithValue("@Kategori", kategori);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Urunler urun = new Urunler
                    {
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        UrunAdi = reader["UrunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Fiyat = Convert.ToDecimal(reader["Fiyat"]),
                        Stok = Convert.ToInt32(reader["Stok"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                    urunler.Add(urun);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün listeleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return urunler;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <returns>The product entity.</returns>
        public Urunler Getir(int urunID)
        {
            Urunler urun = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UrunID, UrunAdi, Kategori, Fiyat, Stok, Durum FROM Urunler WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunID);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    urun = new Urunler
                    {
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        UrunAdi = reader["UrunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Fiyat = Convert.ToDecimal(reader["Fiyat"]),
                        Stok = Convert.ToInt32(reader["Stok"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return urun;
        }

        /// <summary>
        /// Gets a product by name.
        /// </summary>
        /// <param name="urunAdi">The product name.</param>
        /// <returns>The product entity if found, null otherwise.</returns>
        public Urunler GetirByUrunAdi(string urunAdi)
        {
            Urunler urun = null;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UrunID, UrunAdi, Kategori, Fiyat, Stok, Durum FROM Urunler WHERE UrunAdi = @UrunAdi AND Durum = 'Aktif'";

            command.Parameters.AddWithValue("@UrunAdi", urunAdi);

            try
            {
                Tools.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    urun = new Urunler
                    {
                        UrunID = Convert.ToInt32(reader["UrunID"]),
                        UrunAdi = reader["UrunAdi"].ToString(),
                        Kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : string.Empty,
                        Fiyat = Convert.ToDecimal(reader["Fiyat"]),
                        Stok = Convert.ToInt32(reader["Stok"]),
                        Durum = reader["Durum"] != DBNull.Value ? reader["Durum"].ToString() : string.Empty
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün getirme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return urun;
        }

        /// <summary>
        /// Updates stock for a product.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <param name="yeniStok">The new stock amount.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool StokGuncelle(int urunID, int yeniStok)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Urunler SET Stok = @Stok WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunID);
            command.Parameters.AddWithValue("@Stok", yeniStok);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Stok güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Updates price for a product.
        /// </summary>
        /// <param name="urunID">The product ID.</param>
        /// <param name="yeniFiyat">The new price.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool FiyatGuncelle(int urunID, decimal yeniFiyat)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Urunler SET Fiyat = @Fiyat WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunID);
            command.Parameters.AddWithValue("@Fiyat", yeniFiyat);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Fiyat güncelleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="urun">The product entity to add.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Ekle(Urunler urun)
        {
            bool result = false;
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Urunler (UrunAdi, Kategori, Fiyat, Stok, Durum) VALUES (@UrunAdi, @Kategori, @Fiyat, @Stok, @Durum)";

            command.Parameters.AddWithValue("@UrunAdi", urun.UrunAdi);
            command.Parameters.AddWithValue("@Kategori", urun.Kategori ?? string.Empty);
            command.Parameters.AddWithValue("@Fiyat", urun.Fiyat);
            command.Parameters.AddWithValue("@Stok", urun.Stok);
            command.Parameters.AddWithValue("@Durum", urun.Durum ?? "Aktif");

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                result = affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün ekleme işlemi sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                Tools.CloseConnection();
            }

            return result;
        }
    }
}

