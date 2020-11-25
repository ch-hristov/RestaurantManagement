CREATE PROCEDURE [dbo].[OrderDetail_Update]
	@Id int = 0 output,
    @DiningTableId int, 
    @ServerId int,
    @FoodId int,
    @Quantity int,
    @OrderDate datetime2,
    @OrderId int

AS
	update dbo.OrderDetail
    set DiningTableId = @DiningTableId, ServerId = @ServerId, FoodId = @FoodId, Quantity = @Quantity, OrderDate = @OrderDate, OrderId = @OrderId
    where Id = @Id;

RETURN 0
