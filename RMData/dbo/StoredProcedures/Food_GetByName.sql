CREATE PROCEDURE [dbo].[Food_GetByName]
	@FoodName nvarchar(100)

AS
	select *
	from dbo.Food
	where FoodName = @FoodName;

RETURN 0
