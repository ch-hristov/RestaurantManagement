CREATE TABLE [dbo].[People]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeID] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NULL, 
    [CellPhoneNumber] NVARCHAR(20) NULL, 
    [FullName] NVARCHAR(100) NULL
)
