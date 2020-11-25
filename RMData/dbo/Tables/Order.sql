CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiningTableId] INT NOT NULL, 
    [ServerId] INT NOT NULL, 
    [SubTotal] MONEY NOT NULL,
    [Tax] MONEY NOT NULL,
    [Total] MONEY NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate() ,    
    [BillPaid] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Order_ToDiningTable] FOREIGN KEY (ServerId) REFERENCES People(Id), 
    CONSTRAINT [FK_Order_ToPeople] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id) 
   
)
