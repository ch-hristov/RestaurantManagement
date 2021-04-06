CREATE TABLE [dbo].[DiningTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TableNumber] INT NOT NULL, 
    [Seats] INT NOT NULL, 
    [IsBlocked] BIT NOT NULL DEFAULT 0, 
    [IsHidden] BIT NOT NULL
)
