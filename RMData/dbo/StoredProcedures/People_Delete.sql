CREATE PROCEDURE [dbo].[People_Delete]
	@Id int 

AS
	delete from dbo.People
	where Id = @Id;

RETURN 0
