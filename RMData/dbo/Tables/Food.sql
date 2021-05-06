﻿CREATE TABLE [dbo].[Food]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FoodType] NVARCHAR(100) NOT NULL, 
    [FoodName] NVARCHAR(100) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [TypeId] INT NOT NULL, 
    [IsBlocked] BIT NOT NULL DEFAULT 0, 
    [IsPromo] BIT NOT NULL DEFAULT 0, 
    [DisplayPhoto1] NVARCHAR(500) NULL, 
    [DisplayPhoto2] NVARCHAR(500) NULL, 
    [ItemDescription] NVARCHAR(500) NULL, 
    [FoodNameCR] NVARCHAR(500) NULL, 
    [FoodNameDE] NVARCHAR(500) NULL, 
    [FoodNameIT] NVARCHAR(500) NULL, 
    [FoodNameFR] NVARCHAR(500) NULL, 
    [FoodNameES] NVARCHAR(500) NULL, 
    [FoodDescriptionCR] NVARCHAR(500) NULL, 
    [FoodDescriptionDE] NVARCHAR(500) NULL, 
    [FoodDescriptionIT] NVARCHAR(500) NULL, 
    [FoodDescriptionFR] NVARCHAR(500) NULL, 
    [FoodDescriptionES] NVARCHAR(500) NULL, 
    [Ingredients] NVARCHAR(1000) NULL, 
    CONSTRAINT [FK_Food_ToFoodType] FOREIGN KEY (TypeId) REFERENCES FoodType(Id) 
)
