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
        /// <param name="urunler">The list of all products.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Listele(out List<Urunler> urunler)
        {
            string hata = null;
            urunler = new List<Urunler>();
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets products by category.
        /// </summary>
        /// <param name="kategori">The category name.</param>
        /// <param name="urunler">The list of products in the category.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetirByKategori(string kategori, out List<Urunler> urunler)
        {
            string hata = null;
            urunler = new List<Urunler>();
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="urun">The product entity.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Getir(int urunId, out Urunler urun)
        {
            string hata = null;
            urun = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT UrunID, UrunAdi, Kategori, Fiyat, Stok, Durum FROM Urunler WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunId);

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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Gets a product by name.
        /// </summary>
        /// <param name="urunAdi">The product name.</param>
        /// <param name="urun">The product entity if found, null otherwise.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string GetirByUrunAdi(string urunAdi, out Urunler urun)
        {
            string hata = null;
            urun = null;
            
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
                hata = ex.Message;
            }
            finally
            {
                Tools.CloseConnection();
            }

            return hata;
        }

        /// <summary>
        /// Updates stock for a product.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="yeniStok">The new stock amount.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string StokGuncelle(int urunId, int yeniStok)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Urunler SET Stok = @Stok WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunId);
            command.Parameters.AddWithValue("@Stok", yeniStok);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Stok güncelleme işlemi başarısız oldu.";
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
        /// Updates price for a product.
        /// </summary>
        /// <param name="urunId">The product ID.</param>
        /// <param name="yeniFiyat">The new price.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string FiyatGuncelle(int urunId, decimal yeniFiyat)
        {
            string hata = null;
            
            SqlCommand command = new SqlCommand();
            command.Connection = Tools.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Urunler SET Fiyat = @Fiyat WHERE UrunID = @UrunID";

            command.Parameters.AddWithValue("@UrunID", urunId);
            command.Parameters.AddWithValue("@Fiyat", yeniFiyat);

            try
            {
                Tools.OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    hata = "Fiyat güncelleme işlemi başarısız oldu.";
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
        /// Adds a new product.
        /// </summary>
        /// <param name="urun">The product entity to add.</param>
        /// <returns>Error message if operation fails, null otherwise.</returns>
        public string Ekle(Urunler urun)
        {
            string hata = null;
            
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
                if (affectedRows <= 0)
                {
                    hata = "Ürün ekleme işlemi başarısız oldu.";
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
