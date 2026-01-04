-- HIZLI ÇÖZÜM: TurnuvaMaclari Tablosunu Oluştur
-- Bu scripti SQL Server Management Studio'da çalıştırın (F5)

USE GameCenterDB;
GO

-- Eğer tablo varsa önce sil (dikkatli!)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[TurnuvaMaclari];
    PRINT 'Eski tablo silindi.';
END
GO

-- Tabloyu oluştur
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
    [MacTarihi] [datetime] NULL
);
GO

-- Foreign Key'leri ekle
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TurnuvaMaclari_Turnuvalar')
BEGIN
    ALTER TABLE [dbo].[TurnuvaMaclari]
    ADD CONSTRAINT [FK_TurnuvaMaclari_Turnuvalar] 
    FOREIGN KEY ([TurnuvaID]) REFERENCES [Turnuvalar]([TurnuvaID]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TurnuvaMaclari_Uyeler1')
BEGIN
    ALTER TABLE [dbo].[TurnuvaMaclari]
    ADD CONSTRAINT [FK_TurnuvaMaclari_Uyeler1] 
    FOREIGN KEY ([Uye1ID]) REFERENCES [Uyeler]([UyeID]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TurnuvaMaclari_Uyeler2')
BEGIN
    ALTER TABLE [dbo].[TurnuvaMaclari]
    ADD CONSTRAINT [FK_TurnuvaMaclari_Uyeler2] 
    FOREIGN KEY ([Uye2ID]) REFERENCES [Uyeler]([UyeID]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TurnuvaMaclari_UyelerKazanan')
BEGIN
    ALTER TABLE [dbo].[TurnuvaMaclari]
    ADD CONSTRAINT [FK_TurnuvaMaclari_UyelerKazanan] 
    FOREIGN KEY ([KazananID]) REFERENCES [Uyeler]([UyeID]);
END
GO

-- Kontrol
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]') AND type in (N'U'))
BEGIN
    PRINT '✓✓✓ TurnuvaMaclari tablosu başarıyla oluşturuldu! ✓✓✓';
    SELECT 'BAŞARILI' AS Durum, COUNT(*) AS KolonSayisi 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]');
END
ELSE
BEGIN
    PRINT '✗✗✗ HATA: Tablo oluşturulamadı! ✗✗✗';
END
GO

