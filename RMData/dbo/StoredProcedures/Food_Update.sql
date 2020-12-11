CREATE PROCEDURE [dbo].[Food_Update]
	@Id int,
	@FoodType nvarchar(100),
	@FoodName nvarchar(100),
	@Price money,
	@TypeId int,
	@IsBlocked bit,
	@IsPromo bit

AS
	update dbo.Food 
	set FoodType = @FoodType, FoodName = @FoodName, Price = @Price, TypeId = @TypeId, IsBlocked = @IsBlocked, IsPromo = @IsPromo
	where Id = @Id;

RETURN 0
