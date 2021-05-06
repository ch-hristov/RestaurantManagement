CREATE PROCEDURE [dbo].[Food_Update]
	@Id int,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int,
	@IsBlocked bit,
	@IsPromo bit,
	@ItemDescription nvarchar(500),
	@DisplayPhoto1 nvarchar(500),
	@DisplayPhoto2 nvarchar(500),
	@Ingredients nvarchar(1000)
AS
	update dbo.Food 
	set FoodType = @FoodType, FoodName = @FoodName, Price = @Price, TypeId = @TypeId, IsBlocked = @IsBlocked, IsPromo = @IsPromo, ItemDescription = @ItemDescription, DisplayPhoto1 = @DisplayPhoto1, DisplayPhoto2 = @DisplayPhoto2, Ingredients = @Ingredients
	where Id = @Id;

RETURN 0
