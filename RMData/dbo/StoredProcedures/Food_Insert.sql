CREATE PROCEDURE [dbo].[Food_Insert]
	@Id int = 0 output,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int,
	@IsBlocked bit,
	@IsPromo bit

AS
	insert into dbo.Food (FoodType, FoodName, Price, TypeId, IsBlocked, IsPromo)
	values (@FoodType, @FoodName, @Price, @TypeId, @IsBlocked, @IsPromo);

	select @Id = SCOPE_IDENTITY();
RETURN 0
