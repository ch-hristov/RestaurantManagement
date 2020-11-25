CREATE PROCEDURE [dbo].[FoodType_Delete]
	@Id int

AS
	delete from dbo.FoodType
	where Id = @Id;

RETURN 0
