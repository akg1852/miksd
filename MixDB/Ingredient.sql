﻿CREATE TABLE [dbo].[Ingredient]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Equivalence] BIT NOT NULL DEFAULT 0
)
