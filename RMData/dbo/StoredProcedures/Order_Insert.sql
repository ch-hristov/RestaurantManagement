CREATE PROCEDURE [dbo].[Order_Insert]
	@Id int = 0 output,
	@DiningTableId int,
	@ServerId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2,
	@BillPaid bit
	
AS
	insert into dbo.[Order] (DiningTableId, ServerId, SubTotal, Tax, Total, CreatedDate, BillPaid)
	values (@DiningTableId, @ServerId, @SubTotal, @Tax, @Total, @CreatedDate, @BillPaid);

	select @Id = SCOPE_IDENTITY();

RETURN 0
