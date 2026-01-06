-- =============================================
-- Veritabanı Güncelleme Scripti
-- OyunID kolonunu Hareketler tablosuna ekler
-- =============================================

USE GameCenterDB;
GO

-- OyunID kolonu ekle (AI için oyun takibi)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'OyunID')
BEGIN
    ALTER TABLE [dbo].[Hareketler] ADD [OyunID] [int] NULL;
    
    -- Foreign key constraint ekle
    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Hareketler_OyunID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] 
        ADD CONSTRAINT FK_Hareketler_OyunID 
        FOREIGN KEY ([OyunID]) REFERENCES [Oyunlar]([OyunID]);
    END
    
    PRINT 'OyunID kolonu başarıyla eklendi!';
END
ELSE
BEGIN
    PRINT 'OyunID kolonu zaten mevcut.';
END
GO

-- TarifeID kolonu kontrolü (eğer yoksa ekle)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'TarifeID')
BEGIN
    ALTER TABLE [dbo].[Hareketler] ADD [TarifeID] [int] NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Hareketler_TarifeID')
    BEGIN
        ALTER TABLE [dbo].[Hareketler] 
        ADD CONSTRAINT FK_Hareketler_TarifeID 
        FOREIGN KEY ([TarifeID]) REFERENCES [Tarifeler]([TarifeID]);
    END
    
    PRINT 'TarifeID kolonu başarıyla eklendi!';
END
GO

-- PesinAlinan kolonu kontrolü
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'PesinAlinan')
BEGIN
    ALTER TABLE [dbo].[Hareketler] ADD [PesinAlinan] [decimal](18, 2) NOT NULL DEFAULT 0;
    PRINT 'PesinAlinan kolonu başarıyla eklendi!';
END
GO

-- SiparisToplami kolonu kontrolü
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'SiparisToplami')
BEGIN
    ALTER TABLE [dbo].[Hareketler] ADD [SiparisToplami] [decimal](18, 2) NOT NULL DEFAULT 0;
    PRINT 'SiparisToplami kolonu başarıyla eklendi!';
END
GO

-- Durum kolonu kontrolü
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Hareketler]') AND name = 'Durum')
BEGIN
    ALTER TABLE [dbo].[Hareketler] ADD [Durum] [nvarchar](20) NOT NULL DEFAULT 'Aktif';
    PRINT 'Durum kolonu başarıyla eklendi!';
END
GO

PRINT 'Veritabanı güncelleme tamamlandı!';
GO

