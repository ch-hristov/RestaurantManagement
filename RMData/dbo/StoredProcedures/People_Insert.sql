CREATE PROCEDURE [dbo].[People_Insert]
	@Id int = 0 output,
	@EmployeeID int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@EmailAddress nvarchar(50),
	@CellPhoneNumber nvarchar(20),
	@FullName nvarchar(100)
AS
	insert into dbo.People (EmployeeID, FirstName, LastName, EmailAddress, CellPhoneNumber, FullName)
	values (@EmployeeID, @FirstName, @LastName, @EmailAddress, @CellPhoneNumber, @FullName);

	select @Id = SCOPE_IDENTITY();

RETURN 0
