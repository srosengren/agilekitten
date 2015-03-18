CREATE PROCEDURE [dbo].[MergeRepository]
	@OriginId INT,
	@Labels LabelType READONLY,
	@Issues IssueType READONLY
AS

DECLARE @RepositoryId INT = NULL
SELECT TOP 1 @RepositoryId = RepositoryId FROM Repository WHERE OriginId = @OriginId
IF @RepositoryId IS NULL
BEGIN
	INSERT INTO Repository(OriginId) VALUES(@OriginId)
	SET @RepositoryId = @@IDENTITY
END

MERGE INTO Label t USING @Labels s ON s.Name = t.Name AND t.RepositoryId = @RepositoryId
WHEN NOT MATCHED BY TARGET THEN
INSERT(Name,RepositoryId)
VALUES(Name,@RepositoryId);

MERGE INTO Issue t USING @Issues s ON s.Number = t.Number AND t.RepositoryId = @RepositoryId
WHEN NOT MATCHED BY TARGET THEN
INSERT(Number,RepositoryId)
VALUES(Number,@RepositoryId);

SELECT * FROM Repository WHERE RepositoryId = @RepositoryId
SELECT * FROM Label WHERE RepositoryId = @RepositoryId
SELECT * FROM Issue WHERE RepositoryId = @RepositoryId