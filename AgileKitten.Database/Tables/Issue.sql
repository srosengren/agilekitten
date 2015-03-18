CREATE TABLE [dbo].[Issue]
(
	[Number] INT NOT NULL,
	[RepositoryId] INT NOT NULL,
	[Sort] INT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Issue_Repository] FOREIGN KEY ([RepositoryId]) REFERENCES [Repository]([RepositoryId]), 
    CONSTRAINT [PK_Issue] PRIMARY KEY ([RepositoryId],[Number])
)
