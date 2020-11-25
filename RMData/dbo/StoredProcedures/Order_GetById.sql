CREATE PROCEDURE [dbo].[Order_GetById]
	@Id int 

AS
	select *
	from dbo.[Order]
	where Id = @Id;

RETURN 0
