CREATE TABLE [dbo].[IngredientRelationship]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Parent] BIGINT NOT NULL, 
    [Child] BIGINT NOT NULL, 
    CONSTRAINT [FK_IngredientRelationship_ParentIngredient] FOREIGN KEY ([Parent]) REFERENCES [Ingredient]([Id]),
    CONSTRAINT [FK_IngredientRelationship_ChildIngredient] FOREIGN KEY ([Child]) REFERENCES [Ingredient]([Id])
)
