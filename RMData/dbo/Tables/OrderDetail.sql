CREATE TABLE [dbo].[OrderDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiningTableId] INT NOT NULL, 
    [ServerId] NVARCHAR(450) NOT NULL, 
    [FoodId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL DEFAULT getutcdate() ,    
    [OrderId] INT NOT NULL  , 
    [SeatNumber] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_OrderDetail_ToFood] FOREIGN KEY (FoodId) REFERENCES Food(Id), 
    CONSTRAINT [FK_OrderDetail_ToOrder] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_OrderDetail_ToUser] FOREIGN KEY (ServerId) REFERENCES AspNetUsers(Id)
   
)