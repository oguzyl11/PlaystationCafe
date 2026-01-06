-- =============================================
-- TAM VERİTABANI KURULUM SCRIPTİ
-- =============================================
-- Bu script, tüm tabloları oluşturur ve eksik kolonları ekler.
-- NOT: Bu scripti çalıştırmadan önce doğru veritabanını seçtiğinizden emin olun!
-- USE [VeritabaniAdiniz]; komutunu kullanabilirsiniz.

PRINT '=============================================';
PRINT 'Veritabanı kurulumu başlatılıyor...';
PRINT '=============================================';
GO

-- Önce Database_Schema.sql dosyasını çalıştırmanız gerekiyor
-- Eğer tablolar zaten varsa, bu script sadece eksik kolonları ekler

-- =============================================
-- 1. Hareketler Tablosu - Eksik Kolonları Ekle
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND type in (N'U'))
BEGIN
    PRINT 'Hareketler tablosu bulundu. Eksik kolonlar kontrol ediliyor...';
    
    -- OyunID kolonu ekle (AI için oyun takibi)
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'OyunID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [OyunID] [int] NULL;
        
        -- Foreign key constraint ekle (Oyunlar tablosu varsa)
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Oyunlar]') AND type in (N'U'))
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Hareketler_OyunID')
            BEGIN
                ALTER TABLE [dbo].[Hareketler] 
                ADD CONSTRAINT FK_Hareketler_OyunID 
                FOREIGN KEY ([OyunID]) REFERENCES [Oyunlar]([OyunID]);
            END
        END
        
        PRINT '✓ OyunID kolonu eklendi.';
    END
    ELSE
    BEGIN
        PRINT 'ℹ OyunID kolonu zaten mevcut.';
    END
    
    -- TarifeID kolonu kontrolü
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'TarifeID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [TarifeID] [int] NULL;
        
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifeler]') AND type in (N'U'))
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Hareketler_TarifeID')
            BEGIN
                ALTER TABLE [dbo].[Hareketler] 
                ADD CONSTRAINT FK_Hareketler_TarifeID 
                FOREIGN KEY ([TarifeID]) REFERENCES [Tarifeler]([TarifeID]);
            END
        END
        
        PRINT '✓ TarifeID kolonu eklendi.';
    END
    
    -- PesinAlinan kolonu kontrolü
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'PesinAlinan')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [PesinAlinan] [decimal](18, 2) NOT NULL DEFAULT 0;
        PRINT '✓ PesinAlinan kolonu eklendi.';
    END
    
    -- SiparisToplami kolonu kontrolü
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'SiparisToplami')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [SiparisToplami] [decimal](18, 2) NOT NULL DEFAULT 0;
        PRINT '✓ SiparisToplami kolonu eklendi.';
    END
    
    -- Durum kolonu kontrolü
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'Durum')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] ADD [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif';
        PRINT '✓ Durum kolonu eklendi.';
    END
END
ELSE
BEGIN
    PRINT '✗ HATA: Hareketler tablosu bulunamadı!';
    PRINT 'Lütfen önce Database_Schema.sql dosyasını çalıştırın.';
END
GO

-- =============================================
-- 2. Faturalar Tablosu Oluştur
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND type in (N'U'))
BEGIN
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
        
        PRINT '✓ Faturalar tablosu oluşturuldu.';
    END
    ELSE
    BEGIN
        PRINT 'ℹ Faturalar tablosu zaten mevcut.';
    END
END
ELSE
BEGIN
    PRINT '✗ HATA: Hareketler tablosu bulunamadı! Faturalar tablosu oluşturulamadı.';
    PRINT 'Lütfen önce Database_Schema.sql dosyasını çalıştırın.';
END
GO

PRINT '=============================================';
PRINT 'Veritabanı kurulumu tamamlandı!';
PRINT '=============================================';
GO

