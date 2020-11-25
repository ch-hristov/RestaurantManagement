CREATE PROCEDURE [dbo].[OrderDetail_GetById]
	@Id int 

AS
	select *
	from dbo.OrderDetail
	where Id = @Id;

RETURN 0
