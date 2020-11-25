CREATE PROCEDURE [dbo].[FoodType_Insert]
	@Id int = 0 output,	
	@FoodType nvarchar(100)

AS
	insert into dbo.FoodType (FoodType)
	values (@FoodType);

	select @Id = SCOPE_IDENTITY();

RETURN 0
