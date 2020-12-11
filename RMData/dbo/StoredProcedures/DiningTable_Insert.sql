CREATE PROCEDURE [dbo].[DiningTable_Insert]
	@Id int = 0 output,
	@TableNumber int,
	@Seats int,
	@IsBlocked bit

AS
	insert into dbo.DiningTable (TableNumber, Seats, IsBlocked)
	values (@TableNumber, @Seats, @IsBlocked);

	select @Id = SCOPE_IDENTITY();
RETURN 0
