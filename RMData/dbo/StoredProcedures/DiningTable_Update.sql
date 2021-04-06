CREATE PROCEDURE [dbo].[DiningTable_Update]
	@Id int,
	@TableNumber int,
	@Seats int,
	@IsBlocked bit,
	@IsHidden bit

AS
	update dbo.DiningTable
	set TableNumber = @TableNumber, Seats = @Seats, IsBlocked = @IsBlocked, IsHidden = @IsHidden
	where Id = @Id;

RETURN 0
