CREATE PROCEDURE [dbo].[Order_GetAll]
	
AS
	select *
	from dbo.[Order]
	where BillPaid = 0;

RETURN 0
