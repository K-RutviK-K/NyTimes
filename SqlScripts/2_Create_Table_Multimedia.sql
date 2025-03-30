IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Multimedia' AND type = 'U')
BEGIN
    CREATE TABLE Multimedia (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ArticleId INT FOREIGN KEY REFERENCES Articles(Id) ON DELETE CASCADE,
        Url NVARCHAR(MAX),
        Format NVARCHAR(255),
        Height INT,
        Width INT,
        Type NVARCHAR(50),
        Subtype NVARCHAR(50),
        Caption NVARCHAR(MAX),
        Copyright NVARCHAR(255)
    );
END
