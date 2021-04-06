CREATE PROCEDURE [dbo].[Order_UpdateBillPaid]
	@Id int,
	@DiningTableId int,
	@OrderId int
AS
	update dbo.[Order]
	set BillPaid = 1
	where Id = @Id;
RETURN 0
