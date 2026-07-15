-- Creates the Hotels table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Hotels')
BEGIN
    CREATE TABLE Hotels
    (
        Id        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name      NVARCHAR(200) NOT NULL,
        Address   NVARCHAR(300) NOT NULL,
        Rating    FLOAT NOT NULL,
        CountryId INT NOT NULL,
        CONSTRAINT FK_Hotels_Countries FOREIGN KEY (CountryId) REFERENCES Countries (CountryId)
    );
END
