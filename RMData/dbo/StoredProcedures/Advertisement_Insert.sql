CREATE PROCEDURE [dbo].[Advertisement_Insert]
	@Id int = 0 output,
	@Ad1 text,
	@Ad2 text,
	@Ad3 text,
	@CreateDate datetime2,
	@Ad1Blocked bit,
	@Ad2Blocked bit,
	@Ad3Blocked bit
AS
	insert into dbo.Advertisements(Ad1, Ad2, Ad3, CreateDate, Ad1Blocked, Ad2Blocked, Ad3Blocked)
	values (@Ad1, @Ad2, @Ad3, @CreateDate, @Ad1Blocked, @Ad2Blocked, @Ad3Blocked);

	select @Id = SCOPE_IDENTITY();
RETURN 0
