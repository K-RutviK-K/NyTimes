IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Facets' AND type = 'U')
BEGIN
    CREATE TABLE Facets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ArticleId INT FOREIGN KEY REFERENCES Articles(Id) ON DELETE CASCADE,
        FacetType NVARCHAR(50), -- Can be 'des_facet', 'org_facet', etc.
        FacetValue NVARCHAR(MAX)
    );
END
