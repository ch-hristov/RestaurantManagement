CREATE PROCEDURE [dbo].[DiningTable_Update]
	@Id int,
	@TableNumber int,
	@Seats int

AS
	update dbo.DiningTable
	set TableNumber = @TableNumber, Seats = @Seats
	where Id = @Id;

RETURN 0
