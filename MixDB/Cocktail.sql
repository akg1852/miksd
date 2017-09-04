CREATE TABLE [dbo].[Cocktail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Vessel] INT NOT NULL, 
    [PrepMethod] INT NOT NULL, 
    [Ice] BIT NOT NULL DEFAULT 0, 
    [Garnish] BIGINT NULL , 
    CONSTRAINT [FK_Cocktail_Vessel] FOREIGN KEY ([Vessel]) REFERENCES [Vessel]([Id]), 
    CONSTRAINT [FK_Cocktail_PrepMethod] FOREIGN KEY ([PrepMethod]) REFERENCES [PrepMethod]([Id])
)
