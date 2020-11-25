CREATE PROCEDURE [dbo].[Food_GetAll]

AS
	select * 
	from dbo.Food
	order by FoodType;

RETURN 0
