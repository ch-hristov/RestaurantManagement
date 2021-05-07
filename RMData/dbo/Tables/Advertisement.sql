CREATE TABLE [dbo].[Advertisements]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Ad1] NVARCHAR(MAX) NULL, 
    [Ad2] NVARCHAR(MAX) NULL, 
    [Ad3] NVARCHAR(MAX) NULL, 
    [CreateDate] DATETIME2 NOT NULL, 
    [Ad1Blocked] BIT NOT NULL, 
    [Ad2Blocked] BIT NOT NULL, 
    [Ad3Blocked] BIT NOT NULL
)
