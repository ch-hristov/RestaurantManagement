CREATE PROCEDURE [dbo].[Food_GetTypeIdByFoodType]
	@FoodType nvarchar(100)

AS
	select Id 
	from dbo.FoodType
	where FoodType = @FoodType;

RETURN 0
