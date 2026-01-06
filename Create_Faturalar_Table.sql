-- =============================================
-- Faturalar Tablosu Oluşturma Scripti
-- =============================================
-- Bu script, veritabanında Faturalar tablosunu oluşturur.
-- Eğer tablo zaten varsa, hata vermez.
-- NOT: Bu scripti çalıştırmadan önce doğru veritabanını seçtiğinizden emin olun!

-- Önce Hareketler tablosunun varlığını kontrol et
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND type in (N'U'))
BEGIN
    PRINT 'UYARI: Hareketler tablosu bulunamadı!';
    PRINT 'Faturalar tablosu oluşturulmadan önce Hareketler tablosunun oluşturulması gerekiyor.';
    PRINT 'Lütfen önce Database_Schema.sql dosyasını çalıştırın.';
    RETURN;
END

-- Faturalar tablosunu oluştur
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Faturalar]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Faturalar](
        [FaturaID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [HareketID] [int] NOT NULL,
        [FaturaNo] [nvarchar](50) NOT NULL UNIQUE,
        [FaturaTarihi] [datetime] NOT NULL DEFAULT GETDATE(),
        [ToplamTutar] [decimal](18, 2) NOT NULL DEFAULT 0,
        [KdvOrani] [decimal](18, 2) NOT NULL DEFAULT 20,
        [KdvTutari] [decimal](18, 2) NOT NULL DEFAULT 0,
        [GenelToplam] [decimal](18, 2) NOT NULL DEFAULT 0,
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif',
        [Notlar] [nvarchar](500) NULL,
        FOREIGN KEY ([HareketID]) REFERENCES [Hareketler]([HareketID])
    );
    
    PRINT '✓ Faturalar tablosu başarıyla oluşturuldu!';
END
ELSE
BEGIN
    PRINT 'ℹ Faturalar tablosu zaten mevcut.';
END
GO

