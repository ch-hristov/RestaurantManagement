CREATE PROCEDURE [dbo].[DiningTable_Update]
	@Id int,
	@TableNumber int,
	@Seats int,
	@IsBlocked bit

AS
	update dbo.DiningTable
	set TableNumber = @TableNumber, Seats = @Seats, IsBlocked = @IsBlocked
	where Id = @Id;

RETURN 0
