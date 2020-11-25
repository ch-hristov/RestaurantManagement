CREATE TABLE [dbo].[OrderDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiningTableId] INT NOT NULL, 
    [ServerId] INT NOT NULL, 
    [FoodId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL DEFAULT getutcdate() ,    
    [OrderId] INT NULL  , 
    CONSTRAINT [FK_OrderDetail_ToFood] FOREIGN KEY (FoodId) REFERENCES Food(Id), 
    CONSTRAINT [FK_OrderDetail_ToOrder] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_OrderDetail_ToPeople] FOREIGN KEY (ServerId) REFERENCES People(Id)
   
)