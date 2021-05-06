CREATE PROCEDURE [dbo].[Food_Insert]
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
	insert into dbo.Food (FoodType, FoodName, Price, TypeId, IsBlocked, IsPromo, DisplayPhoto1, DisplayPhoto2, ItemDescription, FoodNameCR, FoodNameDE, FoodNameIT, FoodNameES, FoodDescriptionCR, FoodDescriptionDE, FoodDescriptionIT, FoodDescriptionES, Ingredients)
	values (@FoodType, @FoodName, @Price, @TypeId, @IsBlocked, @IsPromo, @DisplayPhoto1, @DisplayPhoto2, @ItemDescription, @FoodNameCR, @FoodNameDE, @FoodNameIT, @FoodDescriptionCR, @FoodNameES, @FoodDescriptionDE, @FoodDescriptionIT, @FoodDescriptionES, @Ingredients);

	select @Id = SCOPE_IDENTITY();
RETURN 0
