CREATE PROCEDURE [dbo].[Order_Update]
	@Id int = 0 output,
	@DiningTableId int,
	@ServerId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2,
	@BillPaid bit
AS
	update dbo.[Order]
	set DiningTableId = @DiningTableId, ServerId = @ServerId, SubTotal = @SubTotal, Tax = @Tax, Total = @Total, CreatedDate = @CreatedDate, BillPaid = @BillPaid
	where Id = @Id;

RETURN 0
