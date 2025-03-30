IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Articles' AND type = 'U')
BEGIN
    CREATE TABLE Articles (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Section NVARCHAR(255),
        Subsection NVARCHAR(255),
        Title NVARCHAR(MAX),
        Abstract NVARCHAR(MAX),
        Url NVARCHAR(MAX),
        Uri NVARCHAR(MAX),
        Byline NVARCHAR(255),
        ItemType NVARCHAR(100),
        UpdatedDate DATETIME2 null,
        CreatedDate DATETIME2 null,
        PublishedDate DATETIME2 null,
        MaterialTypeFacet NVARCHAR(255),
        Kicker NVARCHAR(255),
        ShortUrl NVARCHAR(MAX)
    );
END
