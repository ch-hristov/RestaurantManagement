CREATE PROCEDURE [dbo].[Order_GetByDiningTable]
	@DiningTableId int

AS
	select *
	from dbo.[Order]
	where DiningTableId = @DiningTableId and BillPaid = 0;

RETURN 0
