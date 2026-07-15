-- Creates the Countries table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Countries')
BEGIN
    CREATE TABLE Countries
    (
        CountryId  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name       NVARCHAR(100) NOT NULL,
        ShortName  NVARCHAR(10)  NOT NULL
    );
END
