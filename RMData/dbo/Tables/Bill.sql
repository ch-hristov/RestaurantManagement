CREATE TABLE [dbo].[Bill]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL,
    [DiningTableId] INT NOT NULL, 
    [ServerId] INT NOT NULL, 
    [SubTotal] MONEY NOT NULL,
    [Tax] MONEY NOT NULL,
    [Total] MONEY NOT NULL,
    [BillDate] DATETIME2 NOT NULL DEFAULT getutcdate() ,    
    [BillPaid] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_PaidBill_ToDiningTable] FOREIGN KEY (ServerId) REFERENCES People(Id), 
    CONSTRAINT [FK_PaidBill_ToPeople] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_PaidBill_ToOrder] FOREIGN KEY (OrderId) REFERENCES [Order](Id) 
)
