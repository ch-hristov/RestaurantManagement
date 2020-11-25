CREATE PROCEDURE [dbo].[Food_Update]
	@Id int,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int

AS
	update dbo.Food 
	set FoodType = @FoodType, FoodName = @FoodName, Price = @Price, TypeId = @TypeId
	where Id = @Id;

RETURN 0
