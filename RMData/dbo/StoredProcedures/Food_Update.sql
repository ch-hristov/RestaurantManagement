CREATE PROCEDURE [dbo].[Food_Update]
	@Id int = 0 output,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int,
	@IsBlocked bit,
	@IsPromo bit,
	@DisplayPhoto1 nvarchar(500),
	@DisplayPhoto2 nvarchar(500),
	@ItemDescription nvarchar(500),
	@FoodNameCR nvarchar(500),
	@FoodNameDE nvarchar(500),
	@FoodNameIT nvarchar(500),
	@FoodNameES nvarchar(500),
	@FoodDescriptionCR nvarchar(500),
	@FoodDescriptionDE nvarchar(500),
	@FoodDescriptionIT nvarchar(500),
	@FoodDescriptionES nvarchar(500),
	@Ingredients nvarchar(1000)
AS
	update dbo.Food 
	set FoodType = @FoodType, FoodName = @FoodName, Price = @Price, TypeId = @TypeId, IsBlocked = @IsBlocked, IsPromo = @IsPromo, ItemDescription = @ItemDescription, DisplayPhoto1 = @DisplayPhoto1, DisplayPhoto2 = @DisplayPhoto2, FoodNameCR = @FoodNameCR, FoodNameDE = @FoodNameDE, FoodNameES = @FoodNameES, FoodDescriptionCR  = @FoodDescriptionCR, FoodDescriptionDE = @FoodDescriptionDE, FoodDescriptionES = @FoodDescriptionES, FoodDescriptionIT = @FoodDescriptionIT, FoodNameIT = @FoodNameIT, Ingredients = @Ingredients
	where Id = @Id;

RETURN 0
