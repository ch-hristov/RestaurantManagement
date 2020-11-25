CREATE PROCEDURE [dbo].[Food_GetById]
	@Id int

AS
	select * 
	from dbo.Food
	where @Id = Id;

RETURN 0
