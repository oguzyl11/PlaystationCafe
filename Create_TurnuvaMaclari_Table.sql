-- TurnuvaMaclari Tablosunu Oluştur
-- Bu scripti SQL Server Management Studio'da çalıştırın

USE GameCenterDB;
GO

-- Önce tablo varsa sil (isteğe bağlı - dikkatli kullanın!)
-- DROP TABLE IF EXISTS [dbo].[TurnuvaMaclari];
-- GO

-- Tabloyu oluştur
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
        CONSTRAINT [FK_TurnuvaMaclari_Turnuvalar] FOREIGN KEY ([TurnuvaID]) REFERENCES [Turnuvalar]([TurnuvaID]),
        CONSTRAINT [FK_TurnuvaMaclari_Uyeler1] FOREIGN KEY ([Uye1ID]) REFERENCES [Uyeler]([UyeID]),
        CONSTRAINT [FK_TurnuvaMaclari_Uyeler2] FOREIGN KEY ([Uye2ID]) REFERENCES [Uyeler]([UyeID]),
        CONSTRAINT [FK_TurnuvaMaclari_UyelerKazanan] FOREIGN KEY ([KazananID]) REFERENCES [Uyeler]([UyeID])
    );
    
    PRINT 'TurnuvaMaclari tablosu başarıyla oluşturuldu!';
END
ELSE
BEGIN
    PRINT 'TurnuvaMaclari tablosu zaten mevcut!';
END
GO

-- Tabloyu kontrol et
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]') AND type in (N'U'))
BEGIN
    PRINT 'Kontrol: TurnuvaMaclari tablosu başarıyla oluşturuldu ve mevcut!';
    SELECT 'Tablo mevcut!' AS Durum, COUNT(*) AS KolonSayisi 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]');
END
ELSE
BEGIN
    PRINT 'HATA: TurnuvaMaclari tablosu oluşturulamadı!';
END
GO

