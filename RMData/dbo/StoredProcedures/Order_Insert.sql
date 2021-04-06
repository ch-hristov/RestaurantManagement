CREATE PROCEDURE [dbo].[Order_Insert]
	@Id int = 0 output,
	@DiningTableId int,
	@ServerId nvarchar(450),
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2,
	@BillPaid bit
	
AS
	insert into dbo.[Order] (DiningTableId, ServerId, SubTotal, Tax, Total, CreatedDate, BillPaid)
	values (@DiningTableId, @ServerId, @SubTotal, @Tax, @Total, @CreatedDate, @BillPaid);

	select @Id = SCOPE_IDENTITY();

	update dbo.OrderDetail
	set OrderId = @Id
	where (OrderId IS NULL or OrderId = 0) and DiningTableId = @DiningTableId;


RETURN 0
	 