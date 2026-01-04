-- TurnuvaMaclari Tablosunun Var Olup Olmadığını Kontrol Et

USE GameCenterDB;
GO

-- Tablo var mı kontrol et
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TurnuvaMaclari]') AND type in (N'U'))
BEGIN
    PRINT '✓ TurnuvaMaclari tablosu MEVCUT';
    
    -- Tablo yapısını göster
    SELECT 
        c.COLUMN_NAME AS 'Kolon Adı',
        c.DATA_TYPE AS 'Veri Tipi',
        c.IS_NULLABLE AS 'Null Olabilir',
        c.COLUMN_DEFAULT AS 'Varsayılan Değer'
    FROM INFORMATION_SCHEMA.COLUMNS c
    WHERE c.TABLE_NAME = 'TurnuvaMaclari'
    ORDER BY c.ORDINAL_POSITION;
END
ELSE
BEGIN
    PRINT '✗ TurnuvaMaclari tablosu BULUNAMADI!';
    PRINT 'Lütfen Create_TurnuvaMaclari_Table.sql scriptini çalıştırın.';
END
GO

