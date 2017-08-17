CREATE TABLE [dbo].[Cocktail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Vessel] BIGINT NOT NULL, 
    CONSTRAINT [FK_Cocktail_Vessel] FOREIGN KEY ([Vessel]) REFERENCES [Vessel]([Id])
)
