CREATE PROCEDURE [dbo].[Food_Delete]
	@Id int

AS
	delete from dbo.Food
	where @Id = Id;

RETURN 0
