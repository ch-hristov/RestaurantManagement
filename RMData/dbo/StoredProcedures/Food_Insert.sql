CREATE PROCEDURE [dbo].[Food_Insert]
	@Id int = 0 output,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int

AS
	insert into dbo.Food (FoodType, FoodName, Price, TypeId)
	values (@FoodType, @FoodName, @Price, @TypeId);

	select @Id = SCOPE_IDENTITY();
RETURN 0
