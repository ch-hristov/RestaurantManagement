CREATE PROCEDURE [dbo].[DiningTable_Insert]
	@Id int = 0 output,
	@TableNumber int,
	@Seats int

AS
	insert into dbo.DiningTable (TableNumber, Seats)
	values (@TableNumber, @Seats);

	select @Id = SCOPE_IDENTITY();
RETURN 0
