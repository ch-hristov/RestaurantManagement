CREATE PROCEDURE [dbo].[DiningTable_GetByTableNumber]
	@TableNumber int

AS
	select *
	from dbo.DiningTable
	where TableNumber = @TableNumber;

RETURN 0
