CREATE PROCEDURE [dbo].[DiningTable_GetById]
	@Id int
	
AS
	select * 
	from dbo.DiningTable
	where Id = @Id;

RETURN 0
