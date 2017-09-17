CREATE TABLE [dbo].[Cocktail]
(
	[Id] BIGINT NOT NULL , 
    [Name] NVARCHAR(50) NOT NULL, 
    [Vessel] INT NOT NULL, 
    [PrepMethod] INT NOT NULL, 
    [Ice] BIT NOT NULL DEFAULT 0, 
    [Garnish] BIGINT NOT NULL DEFAULT 0 , 
    [Color] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Cocktail_Vessel] FOREIGN KEY ([Vessel]) REFERENCES [Vessel]([Id]), 
    CONSTRAINT [FK_Cocktail_PrepMethod] FOREIGN KEY ([PrepMethod]) REFERENCES [PrepMethod]([Id]), 
    CONSTRAINT [PK_Cocktail] PRIMARY KEY ([Id]) 
)

GO

CREATE FULLTEXT INDEX ON [dbo].[Cocktail] ([Name]) KEY INDEX [PK_Cocktail] WITH CHANGE_TRACKING AUTO
