CREATE PROCEDURE [dbo].[Food_GetByTypeId]
	@TypeId int
	
AS
	select *
	from dbo.Food
	where TypeId = @TypeId;

RETURN 0
