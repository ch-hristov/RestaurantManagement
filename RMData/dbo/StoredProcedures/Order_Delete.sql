CREATE PROCEDURE [dbo].[Order_Delete]
	@Id int 

AS
	delete from dbo.[Order]
	where Id = @Id;

RETURN 0
