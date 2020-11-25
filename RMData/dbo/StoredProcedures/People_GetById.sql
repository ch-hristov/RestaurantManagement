CREATE PROCEDURE [dbo].[People_GetById]
	@Id int 

AS
	select * 
	from dbo.People
	where Id = @Id;

RETURN 0
