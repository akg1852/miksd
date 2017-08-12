CREATE TABLE [dbo].[CocktailIngredient]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Cocktail] BIGINT NOT NULL, 
    [Ingredient] BIGINT NOT NULL, 
    [IsOptional] BIT NOT NULL DEFAULT 0, 
    [Quantity] DECIMAL NOT NULL, 
    CONSTRAINT [FK_CocktailIngredient_Cocktail] FOREIGN KEY ([Cocktail]) REFERENCES [Cocktail]([id]),
    CONSTRAINT [FK_CocktailIngredient_Ingredient] FOREIGN KEY ([Ingredient]) REFERENCES [Ingredient]([id])
)
