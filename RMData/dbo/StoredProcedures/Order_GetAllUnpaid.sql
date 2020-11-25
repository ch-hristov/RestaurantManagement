CREATE PROCEDURE [dbo].[Order_GetAllUnpaid]
	
AS
	select * 
	from dbo.[Order]
	where BillPaid = 0
	order by DiningTableId;

RETURN 0
