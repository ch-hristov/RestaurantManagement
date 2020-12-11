CREATE PROCEDURE [dbo].[Advertisement_Insert]
	@Id int = 0 output,
	@Ad1 text,
	@Ad2 text,
	@Ad3 text,
	@CreateDate datetime2
AS
	insert into dbo.Advertisements(Ad1, Ad2, Ad3, CreateDate)
	values (@Ad1, @Ad2, @Ad3, @CreateDate);

	select @Id = SCOPE_IDENTITY();
RETURN 0
