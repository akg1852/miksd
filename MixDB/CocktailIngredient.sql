CREATE TABLE [dbo].[CocktailIngredient]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Cocktail] BIGINT NOT NULL, 
    [Ingredient] BIGINT NOT NULL, 
    [IsOptional] BIT NOT NULL DEFAULT 0, 
    [Quantity] DECIMAL(6, 2) NOT NULL, 
    [SpecialPrep] INT NOT NULL, 
    CONSTRAINT [FK_CocktailIngredient_Cocktail] FOREIGN KEY ([Cocktail]) REFERENCES [Cocktail]([Id]),
    CONSTRAINT [FK_CocktailIngredient_Ingredient] FOREIGN KEY ([Ingredient]) REFERENCES [Ingredient]([Id]), 
    CONSTRAINT [FK_CocktailIngredient_SpecialPrep] FOREIGN KEY ([SpecialPrep]) REFERENCES [SpecialPrep]([Id])
)
