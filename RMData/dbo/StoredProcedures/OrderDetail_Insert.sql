CREATE PROCEDURE [dbo].[OrderDetail_Insert]
	@Id int = 0 output,
    @DiningTableId int, 
    @ServerId nvarchar(500),
    @FoodId int,
    @Quantity int,
    @OrderDate datetime2,
    @OrderId int,
    @SeatNumber nvarchar(500)

AS
	insert into dbo.OrderDetail (DiningTableId, ServerId, FoodId, Quantity, OrderDate, OrderId, SeatNumber)
    values (@DiningTableId, @ServerId, @FoodId, @Quantity, @OrderDate, @OrderId, @SeatNumber);

    select @Id = SCOPE_IDENTITY();

RETURN 0
