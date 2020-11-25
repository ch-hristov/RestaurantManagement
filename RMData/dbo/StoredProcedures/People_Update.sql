CREATE PROCEDURE [dbo].[People_Update]
	@Id int = 0 output,
	@EmployeeID int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@EmailAddress nvarchar(50),
	@CellPhoneNumber nvarchar(20),
	@FullName nvarchar(100)

AS
	update dbo.People
	set EmployeeID = @EmployeeID, FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, CellPhoneNumber = @CellPhoneNumber, FullName = @FullName
	where Id = @Id;

RETURN 0
