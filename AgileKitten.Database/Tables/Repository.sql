CREATE TABLE [dbo].[Repository]
(
	[RepositoryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[OriginId] INT NOT NULL
)

GO

CREATE INDEX [IX_Repository_OriginId] ON [dbo].[Repository] ([OriginId])
