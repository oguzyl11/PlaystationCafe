-- Turnuva Maçları Tablosu
-- Maç sonuçlarını ve turnuva ağacını saklar

USE GameCenterDB;
GO

-- =============================================
-- TurnuvaMaclari Tablosu
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
        [Tur] [nvarchar](50) NOT NULL, -- Çeyrek Final, Yarı Final, Final
        [MacNo] [int] NOT NULL, -- Tur içindeki maç numarası
        [Durum] [nvarchar](20) NOT NULL DEFAULT 'Beklemede', -- Beklemede, Oynandı, Sonuçlandı
        [KazananID] [int] NULL,
        [MacTarihi] [datetime] NULL,
        FOREIGN KEY ([TurnuvaID]) REFERENCES [Turnuvalar]([TurnuvaID]),
        FOREIGN KEY ([Uye1ID]) REFERENCES [Uyeler]([UyeID]),
        FOREIGN KEY ([Uye2ID]) REFERENCES [Uyeler]([UyeID]),
        FOREIGN KEY ([KazananID]) REFERENCES [Uyeler]([UyeID])
    );
END
GO

PRINT 'TurnuvaMaclari tablosu başarıyla oluşturuldu!';
GO

