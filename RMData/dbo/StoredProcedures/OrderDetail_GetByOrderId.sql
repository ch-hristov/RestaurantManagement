CREATE PROCEDURE [dbo].[OrderDetail_GetByOrderId]
	@Id int 

AS
	select *
	from dbo.OrderDetail
	where OrderId = @Id;

RETURN 0