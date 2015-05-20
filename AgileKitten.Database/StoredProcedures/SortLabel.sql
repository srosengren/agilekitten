CREATE PROCEDURE [dbo].[SortLabel]
	@Name NVARCHAR(MAX),
	@RepositoryId INT,
	@Sort INT = NULL
AS
	
DECLARE @OriginalSort INT = NULL
SELECT TOP 1 @OriginalSort = Sort FROM Label WHERE Name = @Name AND RepositoryId = @RepositoryId

If @Sort IS NOT NULL AND @Sort < @OriginalSort
BEGIN
    UPDATE Label SET Sort = Sort+1 
    WHERE 
		(@OriginalSort IS NOT NULL AND Sort BETWEEN @Sort AND @OriginalSort-1)
	OR
		(@OriginalSort IS NULL AND Sort > @Sort -1)
END

If @Sort > @OriginalSort
BEGIN
  UPDATE Label SET Sort = Sort-1 
  WHERE Sort BETWEEN @OriginalSort+1 and @Sort
END


UPDATE Label set Sort = @Sort WHERE Name=@Name AND RepositoryId = @RepositoryId