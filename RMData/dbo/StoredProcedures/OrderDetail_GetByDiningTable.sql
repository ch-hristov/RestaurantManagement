CREATE PROCEDURE [dbo].[OrderDetail_GetByDiningTable]
	@DiningTableId int 

AS
	select *
	from dbo.OrderDetail
	where DiningTableId = @DiningTableId and (OrderId IS NULL or OrderId = 0);

RETURN 0
