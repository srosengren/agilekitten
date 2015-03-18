CREATE TABLE [dbo].[Label]
(
	[Name] NVARCHAR(512) NOT NULL,
	[RepositoryId] INT NOT NULL,
	[Sort] INT NULL, --Null means that it isn't a board list
    CONSTRAINT [PK_Label] PRIMARY KEY ([RepositoryId],[Name]), 
    CONSTRAINT [FK_Label_Repository] FOREIGN KEY ([RepositoryId]) REFERENCES [Repository]([RepositoryId])
)