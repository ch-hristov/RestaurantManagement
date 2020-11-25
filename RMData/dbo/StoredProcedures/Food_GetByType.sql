CREATE PROCEDURE [dbo].[Food_GetByType]
	@FoodType nvarchar(100)
	
AS
	select *
	from dbo.Food
	where @FoodType = FoodType;

RETURN 0
