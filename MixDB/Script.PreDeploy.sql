﻿/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT 1 FROM sys.fulltext_catalogs WHERE name = 'MixFullTextCatalog')
	CREATE FULLTEXT CATALOG MixFullTextCatalog AS DEFAULT


-- clear out reference data
-- (it gets regenerated in code where necessary)
DELETE FROM CocktailIngredient
DELETE FROM Cocktail
DELETE FROM SpecialPrep
DELETE FROM Garnish
DELETE FROM PrepMethod
DELETE FROM Vessel
DELETE FROM IngredientRelationship
DELETE FROM Ingredient