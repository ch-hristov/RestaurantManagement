CREATE PROCEDURE [dbo].[OrderDetail_UpdateBillPaid]
	@DiningTableId int,
	@OrderId int

AS
		update dbo.OrderDetail
		set OrderId = @OrderId
		where (OrderId IS NULL or OrderId = 0) and DiningTableId = @DiningTableId;

RETURN 0
