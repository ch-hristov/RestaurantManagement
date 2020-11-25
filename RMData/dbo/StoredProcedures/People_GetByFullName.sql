CREATE PROCEDURE [dbo].[People_GetByFullName]
	@FirstName nvarchar(50),
	@LastName nvarchar(50)

AS
	select *
	from dbo.People
	where FirstName = @FirstName and LastName = @LastName;

RETURN 0
