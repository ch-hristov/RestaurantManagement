CREATE PROCEDURE [dbo].[DiningTable_Insert]
	@Id int = 0 output,
	@TableNumber int,
	@Seats int,
	@IsBlocked bit,
	@IsHidden bit

AS
	insert into dbo.DiningTable (TableNumber, Seats, IsBlocked, IsHidden)
	values (@TableNumber, @Seats, @IsBlocked, @IsHidden);

	select @Id = SCOPE_IDENTITY();
RETURN 0
