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
	@ItemDescription nvarchar(500)
AS
	insert into dbo.Food (FoodType, FoodName, Price, TypeId, IsBlocked, IsPromo, DisplayPhoto1, DisplayPhoto2, ItemDescription)
	values (@FoodType, @FoodName, @Price, @TypeId, @IsBlocked, @IsPromo, @DisplayPhoto1, @DisplayPhoto2, @ItemDescription);

	select @Id = SCOPE_IDENTITY();
RETURN 0
