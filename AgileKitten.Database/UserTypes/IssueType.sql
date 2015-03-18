CREATE TYPE [dbo].[IssueType] AS TABLE
(
	[Number] INT NOT NULL,
	[RepositoryId] INT NOT NULL,
	[Sort] INT NULL
)
