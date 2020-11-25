CREATE PROCEDURE [dbo].[OrderDetail_Delete]
	@Id int

AS
	delete from dbo.OrderDetail
	where Id = @Id;

RETURN 0
