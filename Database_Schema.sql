-- GameCenterDB Veritabanı Şeması
-- Tüm tabloları ve kolonları oluşturur

USE GameCenterDB;
GO

-- =============================================
-- 1. Uyeler Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Uyeler]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Uyeler](
        [UyeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [AdSoyad] [nvarchar](100) NOT NULL,
        [KullaniciAdi] [nvarchar](50) NOT NULL UNIQUE,
        [Sifre] [nvarchar](50) NOT NULL,
        [FaceEncoding] [varbinary](max) NULL,
        [Bakiye] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [bit] NOT NULL DEFAULT 1
    );
END
GO

-- =============================================
-- 2. Masalar Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Masalar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Masalar](
        [MasaID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [MasaAdi] [nvarchar](50) NOT NULL,
        [SaatlikUcret] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Boş'
    );
END
GO

-- =============================================
-- 3. Oyunlar Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Oyunlar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Oyunlar](
        [OyunID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [OyunAdi] [nvarchar](100) NOT NULL,
        [Kategori] [nvarchar](50) NULL,
        [Platform] [nvarchar](50) NULL
    );
END
GO

-- =============================================
-- 4. Tarifeler Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifeler]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tarifeler](
        [TarifeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [TarifeAdi] [nvarchar](100) NOT NULL,
        [SaatlikUcret] [decimal](18, 2) NOT NULL,
        [SureSiniri] [int] NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif'
    );
END
GO

-- =============================================
-- 5. Urunler Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Urunler]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Urunler](
        [UrunID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UrunAdi] [nvarchar](100) NOT NULL,
        [Kategori] [nvarchar](50) NULL,
        [Fiyat] [decimal](18, 2) NOT NULL,
        [Stok] [int] NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif'
    );
END
GO

-- =============================================
-- 6. Hareketler Tablosu (Güncellenmiş)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Hareketler](
        [HareketID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UyeID] [int] NOT NULL,
        [MasaID] [int] NOT NULL,
        [Baslangic] [datetime] NOT NULL,
        [Bitis] [datetime] NULL,
        [Ucret] [decimal](18, 2) NOT NULL DEFAULT 0,
        [TarifeID] [int] NULL,
        [PesinAlinan] [decimal](18, 2) NOT NULL DEFAULT 0,
        [SiparisToplami] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif',
        FOREIGN KEY ([UyeID]) REFERENCES [Uyeler]([UyeID]),
        FOREIGN KEY ([MasaID]) REFERENCES [Masalar]([MasaID]),
        FOREIGN KEY ([TarifeID]) REFERENCES [Tarifeler]([TarifeID])
    );
END
ELSE
BEGIN
    -- Eksik kolonları ekle
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'TarifeID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [TarifeID] [int] NULL;
        ALTER TABLE [dbo].[Hareketler] ADD CONSTRAINT FK_Hareketler_TarifeID FOREIGN KEY ([TarifeID]) REFERENCES [Tarifeler]([TarifeID]);
    END
    
    -- OyunID kolonu ekle (AI için oyun takibi)
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'OyunID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [OyunID] [int] NULL;
        ALTER TABLE [dbo].[Hareketler] ADD CONSTRAINT FK_Hareketler_OyunID FOREIGN KEY ([OyunID]) REFERENCES [Oyunlar]([OyunID]);
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'PesinAlinan')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [PesinAlinan] [decimal](18, 2) NOT NULL DEFAULT 0;
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'SiparisToplami')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [SiparisToplami] [decimal](18, 2) NOT NULL DEFAULT 0;
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'Durum')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif';
    END
END
GO

-- =============================================
-- 7. Siparisler Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Siparisler]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Siparisler](
        [SiparisID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [HareketID] [int] NOT NULL,
        [SiparisTarihi] [datetime] NOT NULL DEFAULT GETDATE(),
        [ToplamTutar] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif',
        FOREIGN KEY ([HareketID]) REFERENCES [Hareketler]([HareketID])
    );
END
GO

-- =============================================
-- 8. SiparisDetaylar Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SiparisDetaylar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SiparisDetaylar](
        [SiparisDetayID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SiparisID] [int] NOT NULL,
        [UrunID] [int] NOT NULL,
        [Adet] [int] NOT NULL DEFAULT 1,
        [BirimFiyat] [decimal](18, 2) NOT NULL,
        [ToplamFiyat] [decimal](18, 2) NOT NULL,
        FOREIGN KEY ([SiparisID]) REFERENCES [Siparisler]([SiparisID]),
        FOREIGN KEY ([UrunID]) REFERENCES [Urunler]([UrunID])
    );
END
GO

-- =============================================
-- 9. Turnuvalar Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Turnuvalar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Turnuvalar](
        [TurnuvaID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [TurnuvaAdi] [nvarchar](100) NOT NULL,
        [BaslangicTarihi] [datetime] NOT NULL,
        [Odul] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif'
    );
END
GO

-- =============================================
-- 10. Notlar Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notlar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Notlar](
        [NotID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [MasaID] [int] NOT NULL,
        [Tarih] [datetime] NOT NULL,
        [Saat] [nvarchar](10) NULL,
        [Aciklama] [nvarchar](500) NULL,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif',
        FOREIGN KEY ([MasaID]) REFERENCES [Masalar]([MasaID])
    );
END
GO

-- =============================================
-- 11. TurnuvaMaclari Tablosu
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TurnuvaMaclari](
        [MacID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [TurnuvaID] [int] NOT NULL,
        [Uye1ID] [int] NOT NULL,
        [Uye2ID] [int] NOT NULL,
        [Skor1] [int] NULL,
        [Skor2] [int] NULL,
        [Tur] [nvarchar](50) NOT NULL,
        [MacNo] [int] NOT NULL,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Beklemede',
        [KazananID] [int] NULL,
        [MacTarihi] [datetime] NULL,
        FOREIGN KEY ([TurnuvaID]) REFERENCES [Turnuvalar]([TurnuvaID]),
        FOREIGN KEY ([Uye1ID]) REFERENCES [Uyeler]([UyeID]),
        FOREIGN KEY ([Uye2ID]) REFERENCES [Uyeler]([UyeID]),
        FOREIGN KEY ([KazananID]) REFERENCES [Uyeler]([UyeID])
    );
END
GO

-- =============================================
-- TEST VERİLERİ
-- =============================================

-- Test Üyeleri
IF NOT EXISTS (SELECT * FROM Uyeler WHERE KullaniciAdi = 'admin')
BEGIN
    INSERT INTO Uyeler (AdSoyad, KullaniciAdi, Sifre, Bakiye, Durum)
    VALUES ('Admin Kullanıcı', 'admin', 'admin', 1000, 1);
END

IF NOT EXISTS (SELECT * FROM Uyeler WHERE KullaniciAdi = 'test')
BEGIN
    INSERT INTO Uyeler (AdSoyad, KullaniciAdi, Sifre, Bakiye, Durum)
    VALUES ('Test Kullanıcı', 'test', 'test', 500, 1);
END

-- Test Masaları
IF NOT EXISTS (SELECT * FROM Masalar WHERE MasaAdi = 'Masa 1')
BEGIN
    INSERT INTO Masalar (MasaAdi, SaatlikUcret, Durum) VALUES
    ('Masa 1', 50.00, 'Boş'),
    ('Masa 2', 50.00, 'Boş'),
    ('Masa 3', 75.00, 'Boş'),
    ('Masa 4', 75.00, 'Boş'),
    ('Masa 5', 100.00, 'Boş');
END

-- Test Tarifeleri
IF NOT EXISTS (SELECT * FROM Tarifeler WHERE TarifeAdi = 'Standart')
BEGIN
    INSERT INTO Tarifeler (TarifeAdi, SaatlikUcret, SureSiniri, Durum) VALUES
    ('Standart', 50.00, 0, 'Aktif'),
    ('Premium', 75.00, 120, 'Aktif'),
    ('VIP', 100.00, 180, 'Aktif');
END

-- Test Ürünleri
IF NOT EXISTS (SELECT * FROM Urunler WHERE UrunAdi = 'Kola')
BEGIN
    INSERT INTO Urunler (UrunAdi, Kategori, Fiyat, Stok, Durum) VALUES
    ('Kola', 'İçecek', 15.00, 100, 'Aktif'),
    ('Çay', 'İçecek', 10.00, 100, 'Aktif'),
    ('Kahve', 'İçecek', 20.00, 100, 'Aktif'),
    ('Sandviç', 'Yiyecek', 25.00, 50, 'Aktif'),
    ('Cips', 'Atıştırmalık', 12.00, 100, 'Aktif');
END

-- Test Oyunları
IF NOT EXISTS (SELECT * FROM Oyunlar WHERE OyunAdi = 'FIFA 2024')
BEGIN
    INSERT INTO Oyunlar (OyunAdi, Kategori, Platform) VALUES
    ('FIFA 2024', 'Spor', 'PlayStation 5'),
    ('Call of Duty', 'Aksiyon', 'PlayStation 5'),
    ('Racing Game', 'Yarış', 'PlayStation 5');
END

PRINT 'Veritabanı şeması başarıyla oluşturuldu!';
GO

