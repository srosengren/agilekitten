CREATE TYPE [dbo].[LabelType] AS TABLE
(
	[Name] NVARCHAR(512) NOT NULL,
	[RepositoryId] INT NULL,
	[Sort] INT NULL
)
